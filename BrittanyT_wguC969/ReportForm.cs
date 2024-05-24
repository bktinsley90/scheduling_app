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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            LoadAppointmentTypesByMonth();
            LoadCustomerAppointmentsReport();
            FillUsernameSelectComboBox();
        }
        private void FillUsernameSelectComboBox()
        {
            try
            {
                // Query to get usernames from the user table
                string query = "SELECT userName FROM user";
                MySqlCommand command = new MySqlCommand(query, DBConnection.conn);

                // Execute the query and read the results
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add each username to the ComboBox
                        UsernameSelect.Items.Add(reader["username"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading usernames: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAppointmentTypesByMonth()
        {
            try
            {
                string query = "SELECT type, start FROM appointment";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, DBConnection.conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Use LINQ to group by type and month and count the number of appointments
                var groupedData = dataTable.AsEnumerable()
                    .GroupBy(row => new
                    {
                        Type = row.Field<string>("type"),
                        Month = row.Field<DateTime>("start").ToString("MMMM")
                    })
                    .Select(group => new
                    {
                        AppointmentType = group.Key.Type,
                        Month = group.Key.Month,
                        Count = group.Count()
                    })
                    .OrderBy(data => data.Month)  // Optional: order by month
                    .ToList();

                // Convert the grouped data to a DataTable
                DataTable reportTable = new DataTable();
                reportTable.Columns.Add("Appointment Type", typeof(string));
                reportTable.Columns.Add("Month", typeof(string));
                reportTable.Columns.Add("Count", typeof(int));

                foreach (var item in groupedData)
                {
                    reportTable.Rows.Add(item.AppointmentType, item.Month, item.Count);
                }

                // Bind the DataTable to the DataGridView
                ApptTypesGridView.DataSource = reportTable;
                CustomizeDataGridView(ApptTypesGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment types by month: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UsernameSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected username
            string selectedUsername = UsernameSelect.SelectedItem.ToString();
            LoadUserAppointments(selectedUsername);
        }
        private void LoadUserAppointments(string username)
        {
            try
            {
                // Query to get appointments for the selected user
                string query = @"
                SELECT 
                    a.appointmentId,
                    a.customerId,
                    a.title,
                    a.description,
                    a.location,
                    a.contact,
                    a.type,
                    a.start,
                    a.end 
                FROM 
                    appointment a
                JOIN 
                    user u ON a.userId = u.userId
                WHERE 
                    u.username = @username";

                MySqlCommand command = new MySqlCommand(query, DBConnection.conn);
                command.Parameters.AddWithValue("@username", username);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                var adjustedData = dataTable.AsEnumerable().Select(row =>
                {
                    row["start"] = ConvertUtcToLocal(DateTime.Parse(row["start"].ToString()));
                    row["end"] = ConvertUtcToLocal(DateTime.Parse(row["end"].ToString()));
                    return row;
                }).CopyToDataTable();

                ScheduleGridView.DataSource = adjustedData;
                CustomizeDataGridView(ScheduleGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCustomerAppointmentsReport()
        {
            try
            {
                // Query to get customer IDs, names, and appointment counts
                string query = @"
                SELECT 
                    c.customerId,
                    c.customerName,
                    a.appointmentId
                FROM 
                    customer c
                LEFT JOIN 
                    appointment a ON c.customerId = a.customerId";

                MySqlCommand command = new MySqlCommand(query, DBConnection.conn);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Use LINQ to group by customer and count appointments
                var groupedData = dataTable.AsEnumerable()
                    .GroupBy(row => new
                    {
                        CustomerId = row.Field<int>("customerId"),
                        CustomerName = row.Field<string>("customerName")
                    })
                    .Select(group => new
                    {
                        CustomerId = group.Key.CustomerId,
                        CustomerName = group.Key.CustomerName,
                        AppointmentCount = group.Count(row => row.Field<int?>("appointmentId") != null)
                    })
                    .ToList();

                // Convert the grouped data to a DataTable
                DataTable reportTable = new DataTable();
                reportTable.Columns.Add("Customer ID", typeof(int));
                reportTable.Columns.Add("Customer Name", typeof(string));
                reportTable.Columns.Add("Appointment Count", typeof(int));

                foreach (var item in groupedData)
                {
                    reportTable.Rows.Add(item.CustomerId, item.CustomerName, item.AppointmentCount);
                }

                // Bind the DataTable to the DataGridView
                CustomerAppointmentsGridView.DataSource = reportTable;
                CustomizeDataGridView(CustomerAppointmentsGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer appointments report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime ConvertUtcToLocal(DateTime utcDateTime)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;
            DateTime userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);
            return userLocalTime;
        }

        private void CustomizeDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.RowHeadersVisible = false;
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
