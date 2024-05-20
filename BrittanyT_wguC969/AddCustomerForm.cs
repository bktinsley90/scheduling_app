using BrittanyT_wguC969.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969
{
    public partial class AddCustomerForm : Form
    {
        public event EventHandler CustomerAdded;
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private string dateSQLFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void SaveCustomerBtn_Click(object sender, EventArgs e)
        {
            // Get input values
            string customerName = CustomerNameInput.Text.Trim();
            string address = CustomerAddressInput.Text.Trim();
            string phone = PhoneNumberInput.Text.Trim();
            string country = CountryInput.Text.Trim();
            string city = CityInput.Text.Trim();
            string postalCode = PostalCodeInput.Text.Trim();
            string state = StateInput.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Name, Address, and Phone number fields cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[\d-]+$"))
            {
                MessageBox.Show("Phone number can only contain digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                
                DateTime dateTime = DateTime.UtcNow;
                string user = getCurrentUserName();

                using (var transaction = DBConnection.conn.BeginTransaction())
                {
                    try
                    {
                        // Create country, city, and address records
                        int countryID = createCountry(country, transaction);
                        int cityID = createCity(countryID, city, transaction);
                        int addressID = createAddress(cityID, address, state, postalCode, phone, transaction);

                        // Create customer record
                        int customerID = getID("customer", "customerId", transaction) + 1;
                        createCustomer(customerID, customerName, addressID, 1, dateTime, user, transaction);

                        transaction.Commit();
                        MessageBox.Show("Customer saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the input fields after saving
                        // ClearCustomerInputFields();
                        CustomerAdded?.Invoke(this, EventArgs.Empty);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("An error occurred while saving the customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while connecting to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        // Helper method to clear input fields
        private void ClearCustomerInputFields()
        {
            CustomerNameInput.Text = string.Empty;
            CustomerAddressInput.Text = string.Empty;
            PhoneNumberInput.Text = string.Empty;
            CountryInput.Text = string.Empty;
            CityInput.Text = string.Empty;
            PostalCodeInput.Text = string.Empty;
            StateInput.Text = string.Empty; 
        }

        
        private string getCurrentUserName()
        {
            // Return the current user name
            return "test"; 
        }

    
        private int createCountry(string country, MySqlTransaction transaction)
        {
            int countryID = getID("country", "countryID", transaction) + 1;
            string user = getCurrentUserName();
            DateTime utc = DateTime.UtcNow;

            var query = "INSERT INTO country (countryID, country, createDate, createdBy, lastUpdateBy) " +
                        $"VALUES ('{countryID}', '{country}', '{dateSQLFormat(utc)}', '{user}', '{user}')";
            using (var cmd = new MySqlCommand(query, DBConnection.conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
            return countryID;
        }

        private int createCity(int countryID, string city, MySqlTransaction transaction)
        {
            int cityID = getID("city", "cityId", transaction) + 1;
            string user = getCurrentUserName();
            DateTime utc = DateTime.UtcNow;

            var query = "INSERT INTO city (cityId, city, countryId, createDate, createdBy, lastUpdateBy) " +
                        $"VALUES ('{cityID}', '{city}', '{countryID}', '{dateSQLFormat(utc)}', '{user}', '{user}')";
            using (var cmd = new MySqlCommand(query, DBConnection.conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
            return cityID;
        }

        private int createAddress(int cityID, string address, string state, string postalCode, string phone, MySqlTransaction transaction)
        {
            int addressID = getID("address", "addressId", transaction) + 1;
            string user = getCurrentUserName();
            DateTime utc = DateTime.UtcNow;
            string address2 = state;

            var query = "INSERT INTO address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdateBy) " +
                        $"VALUES ('{addressID}', '{address}', '{address2}','{cityID}', '{postalCode}', '{phone}', '{dateSQLFormat(utc)}', '{user}', '{user}')";
            using (var cmd = new MySqlCommand(query, DBConnection.conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
            return addressID;
        }

        private void createCustomer(int id, string name, int addressId, int active, DateTime dateTime, string user, MySqlTransaction transaction)
        {
            var query = "INSERT INTO customer (customerId, customerName, addressId, active, createDate, createdBy, lastUpdateBy) " +
                        $"VALUES ('{id}', '{name}', '{addressId}', '{active}', '{dateSQLFormat(dateTime)}', '{user}', '{user}')";
            using (var cmd = new MySqlCommand(query, DBConnection.conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private int getID(string table, string id, MySqlTransaction transaction)
        {
            var query = $"SELECT MAX({id}) FROM {table}";
            using (var cmd = new MySqlCommand(query, DBConnection.conn, transaction))
            {
                using (var rdr = cmd.ExecuteReader())
                {
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
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
