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
            string query = "SELECT customerName, address, phone FROM customer c JOIN address a ON c.addressId = a.addressId";
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
        private void AddCustomerBtn_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.CustomerAdded += AddCustomerForm_CustomerAdded;
            addCustomerForm.ShowDialog();

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            var loginForm = new loginForm();
            loginForm.Show();
        }

    }
}
