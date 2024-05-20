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
    }
}
