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

        private void CustomizeDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
        }

    }
}
