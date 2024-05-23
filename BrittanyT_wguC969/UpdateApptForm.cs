using BrittanyT_wguC969.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969
{
    public partial class UpdateApptForm : Form
    {
        private Dictionary<string, string> _apptDetails;
        public event EventHandler AppointmentUpdated;

        public UpdateApptForm(Dictionary<string, string> apptDetails)
        {
            InitializeComponent();
            _apptDetails = apptDetails;
            LoadAppointmentDetails();

            PopulateTimeComboBoxes();
            FillCustomerIdComboBox();
            FillUserIdComboBox();
            FillContactComboBox();

        }
        public static List<string> List24HoursWithHalfHours()
        {
            List<string> times = new List<string>();

            for (int i = 0; i < 24; i++)
            {
                // Add leading zero to single digit hours
                string hour = i.ToString("D2");

                // Add full hour
                times.Add(hour + ":00");

                // Add half hour
                times.Add(hour + ":30");
            }

            return times;
        }

        public void PopulateTimeComboBoxes()
        {
            List<string> times = List24HoursWithHalfHours();

            foreach (string time in times)
            {
                StartTime.Items.Add(time);
                EndTime.Items.Add(time);
            }
        }

        private void FillCustomerIdComboBox()
        {

            string query = "SELECT customerId FROM customer";
            using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
            {
                try
                {
                    if (DBConnection.conn.State != ConnectionState.Open)
                    {
                        DBConnection.conn.Open();
                    }
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerID.Items.Add(reader["customerId"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading Customer IDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void FillUserIdComboBox()
        {

            string query = "SELECT userId FROM user";
            using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
            {
                try
                {
                    if (DBConnection.conn.State != ConnectionState.Open)
                    {
                        DBConnection.conn.Open();
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserID.Items.Add(reader["userId"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading User IDs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void FillContactComboBox()
        {

            string query = "SELECT contact FROM appointment";
            using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
            {
                try
                {
                    if (DBConnection.conn.State != ConnectionState.Open)
                    {
                        DBConnection.conn.Open();
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact.Items.Add(reader["contact"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading Contacts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadAppointmentDetails()
        {
            ApptId.Text = _apptDetails["appointmentId"];
            CustomerID.Text = _apptDetails["customerId"];
            ApptTitleInput.Text = _apptDetails["title"];
            ApptDescriptionInput.Text = _apptDetails["description"];
            ApptLocationInput.Text = _apptDetails["location"];
            UserID.Text = _apptDetails["userId"];
            Contact.Text = _apptDetails["contact"];
            ApptTypeInput.Text = _apptDetails["type"];
            StartDate.Value = DateTime.Parse(_apptDetails["start"]);
            EndDate.Value = DateTime.Parse(_apptDetails["end"]);
            StartTime.Text = DateTime.Parse(_apptDetails["start"]).ToString("HH:mm");
            EndTime.Text = DateTime.Parse(_apptDetails["end"]).ToString("HH:mm");
        }
        private void SaveApptBtn_Click(object sender, EventArgs e)
        {
            // Capture input from the form
            int apptID = int.Parse(ApptId.Text);
            int custID = int.Parse(CustomerID.Text);
            string title = ApptTitleInput.Text.Trim();
            string description = ApptDescriptionInput.Text.Trim();
            string location = ApptLocationInput.Text.Trim();
            string contact = Contact.Text.Trim();
            string type = ApptTypeInput.Text.Trim();
            DateTime start = StartDate.Value.Date.Add(TimeSpan.Parse(StartTime.Text));
            DateTime end = EndDate.Value.Date.Add(TimeSpan.Parse(EndTime.Text));
            int userID = int.Parse(UserID.SelectedItem.ToString());

            // Validate the input
            if (ValidateAppointment(custID, title, description, location, contact, type, start, end))
            {
                // Update the appointment in the database
                UpdateAppointment(apptID, custID, title, description, location, contact, type, start, end, userID);

                MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Raise the AppointmentUpdated event
                AppointmentUpdated?.Invoke(this, EventArgs.Empty);

                // Close the form
                Close();
            }
        }

        private bool ValidateAppointment(int custID, string title, string description, string location, string contact, string type, DateTime start, DateTime end)
        {
         
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(contact) || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("All fields must be filled.");
                return false;
            }

            // Check business hours: 9:00 AM to 5:00 PM, Monday–Friday, Eastern Standard Time
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estStart = TimeZoneInfo.ConvertTimeFromUtc(start.ToUniversalTime(), est);
            DateTime estEnd = TimeZoneInfo.ConvertTimeFromUtc(end.ToUniversalTime(), est);

            if (estStart.Hour < 9 || estStart.Hour >= 17 || estEnd.Hour < 9 || estEnd.Hour >= 17 || estStart.DayOfWeek == DayOfWeek.Saturday || estStart.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Appointments must be scheduled during business hours (9:00 AM to 5:00 PM, Monday–Friday, EST).");
                return false;
            }

            // Check for overlapping appointments
            if (CheckForOverlappingAppointments(custID, start, end))
            {
                MessageBox.Show("The appointment overlaps with another existing appointment.");
                return false;
            }

            return true;
        }
        private bool CheckForOverlappingAppointments(int custID, DateTime start, DateTime end)
        {
            string query = "SELECT COUNT(*) FROM appointment WHERE customerId = @custID AND ((@start BETWEEN start AND end) OR (@end BETWEEN start AND end) OR (start BETWEEN @start AND @end) OR (end BETWEEN @start AND @end))";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;

        }
        private void UpdateAppointment(int apptID, int custID, string title, string description, string location, string contact, string type, DateTime start, DateTime end, int userID)
        {
            try
            {
                string query = @"
            UPDATE appointment 
            SET 
                customerId = @custID, 
                title = @title, 
                description = @description, 
                location = @location, 
                contact = @contact, 
                type = @type, 
                start = @start, 
                end = @end,
                userId = @userID
            WHERE 
                appointmentId = @apptID";

                MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@custID", custID);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@apptID", apptID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


}
