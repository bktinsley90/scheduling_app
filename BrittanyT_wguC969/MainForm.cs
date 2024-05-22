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

                // Bind the DataTable to the DataGridView
                ApptGridView.DataSource = dataTable;
                CustomizeDataGridView(ApptGridView);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading appointment data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        //Appointment Section

        private void AddApptBtn_Click(object sender, EventArgs e)
        {
            AddApptForm addApptForm = new AddApptForm();
            addApptForm.ShowDialog();
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            var loginForm = new loginForm();
            loginForm.Show();
        }

    }
}
