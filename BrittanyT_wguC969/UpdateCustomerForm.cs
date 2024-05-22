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
    public partial class UpdateCustomerForm : Form
    {
        public EventHandler CustomerUpdated;
        public UpdateCustomerForm(Dictionary<string, string> customerDetails)
        {
            InitializeComponent();



            // Populate the fields with customer details
            CustomerIdInput.Text = customerDetails["customerId"];
            CustomerNameInput.Text = customerDetails["customerName"];
            CustomerAddressInput.Text = customerDetails["address"];
            PhoneNumberInput.Text = customerDetails["phone"];
            PostalCodeInput.Text = customerDetails["postalCode"];
            StateInput.Text = customerDetails["address2"];
            CityInput.Text = customerDetails["city"];
            CountryInput.Text = customerDetails["country"];


        }

        private void SaveCustomerBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            string customerId = CustomerIdInput.Text.Trim();
            string customerName = CustomerNameInput.Text.Trim();
            string address = CustomerAddressInput.Text.Trim();
            string phone = PhoneNumberInput.Text.Trim();
            string postalCode = PostalCodeInput.Text.Trim();
            string address2 = StateInput.Text.Trim(); // Assuming StateInput is for address2
            string city = CityInput.Text.Trim();
            string country = CountryInput.Text.Trim();

            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Name, address, and phone number fields are required and cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[0-9-]+$"))
            {
                MessageBox.Show("Phone number field allows only digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Start database transaction
                MySqlTransaction transaction = DBConnection.conn.BeginTransaction();
                // Update country record
                string updateCountryQuery = "UPDATE country SET country = @country WHERE countryId = (SELECT countryId FROM city WHERE cityId = (SELECT cityId FROM address WHERE addressId = (SELECT addressId FROM customer WHERE customerId = @customerId)))";
                MySqlCommand updateCountryCmd = new MySqlCommand(updateCountryQuery, DBConnection.conn);
                updateCountryCmd.Parameters.AddWithValue("@country", country);
                updateCountryCmd.Parameters.AddWithValue("@customerId", customerId);
                updateCountryCmd.ExecuteNonQuery();

                // Update city record
                string updateCityQuery = "UPDATE city SET city = @city WHERE cityId = (SELECT cityId FROM address WHERE addressId = (SELECT addressId FROM customer WHERE customerId = @customerId))";
                MySqlCommand updateCityCmd = new MySqlCommand(updateCityQuery, DBConnection.conn);
                updateCityCmd.Parameters.AddWithValue("@city", city);
                updateCityCmd.Parameters.AddWithValue("@customerId", customerId);
                updateCityCmd.ExecuteNonQuery();

                // Update customer record
                string updateCustomerQuery = "UPDATE customer SET customerName = @customerName WHERE customerId = @customerId";
                MySqlCommand updateCustomerCmd = new MySqlCommand(updateCustomerQuery, DBConnection.conn);
                updateCustomerCmd.Parameters.AddWithValue("@customerId", customerId);
                updateCustomerCmd.Parameters.AddWithValue("@customerName", customerName);
                updateCustomerCmd.ExecuteNonQuery();

                // Update address record
                string updateAddressQuery = "UPDATE address SET address = @address, phone = @phone, postalCode = @postalCode, address2 = @address2 WHERE addressId = (SELECT addressId FROM customer WHERE customerId = @customerId)";
                MySqlCommand updateAddressCmd = new MySqlCommand(updateAddressQuery, DBConnection.conn);
                updateAddressCmd.Parameters.AddWithValue("@address", address);
                updateAddressCmd.Parameters.AddWithValue("@phone", phone);
                updateAddressCmd.Parameters.AddWithValue("@postalCode", postalCode);
                updateAddressCmd.Parameters.AddWithValue("@address2", address2);
                updateAddressCmd.Parameters.AddWithValue("@customerId", customerId);
                updateAddressCmd.ExecuteNonQuery();

                // Commit transaction
                transaction.Commit();
                CustomerUpdated?.Invoke(this, EventArgs.Empty);

                MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the UpdateCustomerForm
                this.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
