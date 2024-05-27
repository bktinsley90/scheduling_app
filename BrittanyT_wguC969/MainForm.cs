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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadCustomerData();
            LoadAppointmentData();
        }
        private void LoadCustomerData()
        {
            try
            {
               
                // Create a query to select customer name, address, and phone
                string query = @"
                SELECT 
                    c.customerId,
                    c.customerName, 
                    a.address, 
                    a.phone 
                FROM 
                    customer c
                JOIN 
                    address a ON c.addressId = a.addressId";

                // Create a MySqlDataAdapter to execute the query and fill the DataTable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, DBConnection.conn);

                // Create a DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Fill the DataTable with the data from the database
                dataAdapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView
                CustomerGridView.DataSource = dataTable;
                CustomizeDataGridView(CustomerGridView);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadAppointmentData()
        {
            try
            { 
                // Create a query to select appointment details
                string query = @"
                SELECT 
                    appointmentId,
                    customerId,
                    title,
                    description,
                    location,
                    contact,
                    type,
                    start,
                    end 
                FROM 
                    appointment";

                // Create a MySqlDataAdapter to execute the query and fill the DataTable
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, DBConnection.conn);

                // Create a DataTable to hold the data
                DataTable dataTable = new DataTable();

                // Fill the DataTable with the data from the database
                dataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime utcStart = DateTime.Parse(row["start"].ToString());
                    DateTime utcEnd = DateTime.Parse(row["end"].ToString());

                    row["start"] = ConvertUtcToLocal(utcStart);
                    row["end"] = ConvertUtcToLocal(utcEnd);
                }


                // Bind the DataTable to the DataGridView
                ApptGridView.DataSource = dataTable;
                CustomizeDataGridView(ApptGridView);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading appointment data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DateTime ConvertUtcToLocal(DateTime utcDateTime)
        {
            // Get the user's local time zone
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;

            // Convert the UTC time to the user's local time
            DateTime userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);

            return userLocalTime;
        }

        //updating CustomerGridView
        private void AddCustomerForm_CustomerAdded(object sender, EventArgs e)
        {
            UpdateCustomerGridView();
        }
        private void UpdateCustomerGridView()
        {
            string query = "SELECT customerId, customerName, address, phone FROM customer c JOIN address a ON c.addressId = a.addressId";
            DataTable dataTable = new DataTable();

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.conn))
            {
                adapter.Fill(dataTable);
            }

            CustomerGridView.DataSource = dataTable;
            CustomizeDataGridView(CustomerGridView);
        }
        private void CustomizeDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
        }
        //Adding Customer
        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.CustomerAdded += AddCustomerForm_CustomerAdded;
            addCustomerForm.ShowDialog();

        }
        //Updating Customer
        private void UpdateCustomerBtn_Click(object sender, EventArgs e)
        {
            // Ensure a customer is selected
            if (CustomerGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectedCustomerId = Convert.ToInt32(CustomerGridView.SelectedRows[0].Cells["customerId"].Value);

            // Get customer details
            var customerDetails = GetCustomerDetails(selectedCustomerId);

            if (customerDetails != null)
            {
                UpdateCustomerForm updateCustomerForm = new UpdateCustomerForm(customerDetails);
                updateCustomerForm.CustomerUpdated += UpdateForm_CustomerUpdated;
                updateCustomerForm.Show();
            }
            else
            {
                MessageBox.Show("Customer details could not be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        // Function to get customer details
        private Dictionary<string, string> GetCustomerDetails(int customerId)
        {
            Dictionary<string, string> customerDetails = new Dictionary<string, string>();

            try
            {
                
                string query = @"
                SELECT 
                    c.customerId, c.customerName, a.address, a.phone, a.postalCode, a.address2, 
                    ci.city, co.country 
                FROM customer c
                JOIN address a ON c.addressId = a.addressId
                JOIN city ci ON a.cityId = ci.cityId
                JOIN country co ON ci.countryId = co.countryId
                WHERE c.customerId = @customerId";

                    MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                   
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            customerDetails["customerId"] = reader["customerId"].ToString();
                            customerDetails["customerName"] = reader["customerName"].ToString();
                            customerDetails["address"] = reader["address"].ToString();
                            customerDetails["phone"] = reader["phone"].ToString();
                            customerDetails["postalCode"] = reader["postalCode"].ToString();
                            customerDetails["address2"] = reader["address2"].ToString();
                            customerDetails["city"] = reader["city"].ToString();
                            customerDetails["country"] = reader["country"].ToString();
                        }
                    }
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error retrieving customer details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return customerDetails;
        }
        private void UpdateForm_CustomerUpdated(object sender, EventArgs e)
        {
            UpdateCustomerGridView();
        }

        //delete customer 
        private void DeleteCustomerBtn_Click(object sender, EventArgs e)
        {
            // Ensure a customer is selected
            if (CustomerGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Get the selected customer ID
                int selectedRowIndex = CustomerGridView.SelectedRows[0].Index;
                int customerId = Convert.ToInt32(CustomerGridView.Rows[selectedRowIndex].Cells["customerId"].Value);
                // Check if the customer has any appointments
                if (CustomerHasAppointments(customerId))
                {
                    MessageBox.Show("Cannot delete customer with existing appointments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Delete the customer from the database
                DeleteCustomer(customerId);

                // Refresh the CustomerGridView
                UpdateCustomerGridView();
            }
        }
        private void DeleteCustomer(int customerId)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM customer WHERE customerId = @customerId", DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.ExecuteNonQuery();
                    
                }

                MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CustomerHasAppointments(int customerId)
        {
            string query = "SELECT COUNT(*) FROM appointment WHERE customerId = @customerId";
            using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        //Appointment Section

        private void AddApptBtn_Click(object sender, EventArgs e)
        {
            AddApptForm addApptForm = new AddApptForm();
            addApptForm.AppointmentAdded += AddApptForm_AppointmentAdded;
            addApptForm.ShowDialog();
        }
        private void AddApptForm_AppointmentAdded(object sender, EventArgs e)
        {
            UpdateApptGridView();
        }

        private void UpdateApptGridView()
        {
            string query = "SELECT appointmentId, customerId, title, description, location, contact, type, start, end FROM appointment";
            DataTable dataTable = new DataTable();

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.conn))
            {
                adapter.Fill(dataTable);
            }

            ApptGridView.DataSource = dataTable;
            CustomizeDataGridView(ApptGridView);
        }
        //Update Appt
        private void UpdateApptBtn_Click(object sender, EventArgs e)
        {
            // Ensure an appointment is selected
            if (ApptGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectedApptId = Convert.ToInt32(ApptGridView.SelectedRows[0].Cells["appointmentId"].Value);

            // Get appointment details
            var apptDetails = GetAppointmentDetails(selectedApptId);

            if (apptDetails != null)
            {
                UpdateApptForm updateApptForm = new UpdateApptForm(apptDetails);
                updateApptForm.AppointmentUpdated += UpdateForm_AppointmentUpdated;
                updateApptForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Appointment details could not be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private Dictionary<string, string> GetAppointmentDetails(int appointmentId)
        {
            Dictionary<string, string> apptDetails = new Dictionary<string, string>();

            try
            {
                string query = @"
        SELECT 
            appointmentId, customerId, title, description, location, contact, type, start, end, userId
        FROM 
            appointment
        WHERE 
            appointmentId = @appointmentId";

                MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        apptDetails["appointmentId"] = reader["appointmentId"].ToString();
                        apptDetails["customerId"] = reader["customerId"].ToString();
                        apptDetails["title"] = reader["title"].ToString();
                        apptDetails["description"] = reader["description"].ToString();
                        apptDetails["location"] = reader["location"].ToString();
                        apptDetails["contact"] = reader["contact"].ToString();
                        apptDetails["type"] = reader["type"].ToString();
                        apptDetails["start"] = reader["start"].ToString();
                        apptDetails["end"] = reader["end"].ToString();
                        apptDetails["userId"] = reader["userId"].ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error retrieving appointment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return apptDetails;
        }

        private void UpdateForm_AppointmentUpdated(object sender, EventArgs e)
        {
            UpdateApptGridView();
        }


        //Deleting Appt
        private void DeleteApptBtn_Click(object sender, EventArgs e)
        {
            // Ensure an appointment is selected
            if (ApptGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Get the selected appointment ID
                int selectedRowIndex = ApptGridView.SelectedRows[0].Index;
                int appointmentId = Convert.ToInt32(ApptGridView.Rows[selectedRowIndex].Cells["appointmentId"].Value);

                // Delete the appointment from the database
                DeleteAppointment(appointmentId);

                // Refresh the ApptGridView
                UpdateApptGridView();
            }
        }
       
        private void DeleteAppointment(int appointmentId)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM appointment WHERE appointmentId = @appointmentId", DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Appointment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //changing Appt Views 
        private void LoadAllAppointments()
        {
            string query = "SELECT appointmentId, customerId, title, description, location, contact, type, start, end FROM appointment";
            LoadAppointmentData(query);
        }

        private void LoadCurrentMonthAppointments()
        {
            string query = @"
        SELECT appointmentId, customerId, title, description, location, contact, type, start, end 
        FROM appointment 
        WHERE MONTH(start) = MONTH(CURRENT_DATE()) 
        AND YEAR(start) = YEAR(CURRENT_DATE())";
            LoadAppointmentData(query);
        }

        private void LoadCurrentWeekAppointments()
        {
            string query = @"
        SELECT appointmentId, customerId, title, description, location, contact, type, start, end 
        FROM appointment 
        WHERE WEEK(start, 1) = WEEK(CURRENT_DATE(), 1) 
        AND YEAR(start) = YEAR(CURRENT_DATE())";
            LoadAppointmentData(query);
        }

        private void LoadSpecificDayAppointments(DateTime date)
        {
            string query = @"
        SELECT appointmentId, customerId, title, description, location, contact, type, start, end 
        FROM appointment 
        WHERE DATE(start) = @selectedDate";

            using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
            {
                cmd.Parameters.AddWithValue("@selectedDate", date.ToString("yyyy-MM-dd"));
                LoadAppointmentData(cmd);
            }
        }

        private void LoadAppointmentData(string query)
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.conn))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime utcStart = DateTime.Parse(row["start"].ToString());
                DateTime utcEnd = DateTime.Parse(row["end"].ToString());

                row["start"] = ConvertUtcToLocal(utcStart);
                row["end"] = ConvertUtcToLocal(utcEnd);
            }

            ApptGridView.DataSource = dataTable;
            CustomizeDataGridView(ApptGridView);
        }

        private void LoadAppointmentData(MySqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime utcStart = DateTime.Parse(row["start"].ToString());
                DateTime utcEnd = DateTime.Parse(row["end"].ToString());

                row["start"] = ConvertUtcToLocal(utcStart);
                row["end"] = ConvertUtcToLocal(utcEnd);
            }

            ApptGridView.DataSource = dataTable;
            CustomizeDataGridView(ApptGridView);
        }
        //change events
        private void AllApptBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (AllApptsBtn.Checked)
            {
                LoadAllAppointments();
            }
        }

        private void CurrMonthBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrMonthBtn.Checked)
            {
                LoadCurrentMonthAppointments();
            }
        }

        private void CurrWeekBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrWeekBtn.Checked)
            {
                LoadCurrentWeekAppointments();
            }
        }

        private void ApptDatePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = ApptDatePicker.Value;
            LoadSpecificDayAppointments(selectedDate);
        }


        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            var loginForm = new loginForm();
            loginForm.Show();
        }

    }
}
