﻿using BrittanyT_wguC969.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969
{
    public partial class AddApptForm : Form
    {
        public AddApptForm()
        {
            InitializeComponent();
            FillCustomerIdComboBox();
            FillUserIdComboBox();
            FillContactComboBox();

        }
        private static string dateSQLFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static DateTime getDateTime()
        {
            return DateTime.UtcNow;
        }

        public static int getID(string table, string idColumn)
        {
        
                var query = $"SELECT MAX({idColumn}) FROM {table}";
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    rdr.Read();
                    if (rdr[0] == DBNull.Value)
                    {
                        return 0;
                    }
                    return Convert.ToInt32(rdr[0]);
                }
                return 0;
            
        }
        private void FillCustomerIdComboBox()
        {
            string query = "SELECT customerId FROM customer";
            using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
            {
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerId.Items.Add(reader["customerId"].ToString());
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

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserId.Items.Add(reader["userId"].ToString());
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
            string query = "SELECT contact FROM appointment"; // Assuming you have a contact table
            using (MySqlCommand command = new MySqlCommand(query, DBConnection.conn))
            {
                try
                {

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


        private void SaveApptBtn_Click(object sender, EventArgs e)
        {
            // Capture input from the form
            int custID = int.Parse(CustomerId.Text);
            string title = ApptTitleInput.Text.Trim();
            string description = ApptDescriptionInput.Text.Trim();
            string location = ApptLocationInput.Text.Trim();
            string contact = Contact.Text.Trim();
            string type = ApptTypeInput.Text.Trim();
            DateTime start = StartDate.Value.Date.Add((TimeSpan)StartTime.SelectedValue);
            DateTime end = EndDate.Value.Date.Add((TimeSpan)EndTime.SelectedValue);

            // Validate the input
            if (ValidateAppointment(custID, title, description, location, contact, type, start, end))
            {
                // Save to database
                createAppointment(custID, title, description, location, contact, type, start, end);
                MessageBox.Show("Appointment saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Refresh the appointments grid view
                //RefreshAppointmentGridView();
            }
        }

        private bool ValidateAppointment(int custID, string title, string description, string location, string contact, string type, DateTime start, DateTime end)
        {
            // Check if required fields are non-empty
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

            if (estStart.Hour < 9 || estStart.Hour > 17 || estEnd.Hour < 9 || estEnd.Hour > 17 || estStart.DayOfWeek == DayOfWeek.Saturday || estStart.DayOfWeek == DayOfWeek.Sunday)
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

        public static void createAppointment(int custID, string title, string description, string location, string contact, string type, DateTime start, DateTime endTime)
        {
            int appointID = getID("appointment", "appointmentId") + 1;
            int userID = 1; // Assuming user ID is 1 for simplicity

            DateTime utc = getDateTime();

          
                MySqlTransaction transaction = DBConnection.conn.BeginTransaction();
                try
                {
                    var query = "INSERT INTO appointment (appointmentId, customerId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdateBy) " +
                                $"VALUES (@appointID, @custID, @title, @description, @location, @contact, @type, @custID, @start, @end, @utc, @userID, @userID)";
                    MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
                    cmd.Parameters.AddWithValue("@appointID", appointID);
                    cmd.Parameters.AddWithValue("@custID", custID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@start", dateSQLFormat(start));
                    cmd.Parameters.AddWithValue("@end", dateSQLFormat(endTime));
                    cmd.Parameters.AddWithValue("@utc", dateSQLFormat(utc));
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Transaction = transaction;
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
