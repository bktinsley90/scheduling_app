using BrittanyT_wguC969.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969
{
    public partial class loginForm : Form
    {
       
        private ResourceManager resourceManager;
        public loginForm()
        {
            InitializeComponent();
            LoadLanguage(CultureInfo.CurrentCulture);
        }
        private void LoadLanguage(CultureInfo cultureInfo)
        {
           
            resourceManager = new ResourceManager("BrittanyT_wguC969.loginForm", Assembly.GetExecutingAssembly());
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");
            //MessageBox.Show(Thread.CurrentThread.CurrentUICulture.Name);
            userNameLabel.Text = resourceManager.GetString("userNameLabel.Text");
            passwordLabel.Text = resourceManager.GetString("passwordLabel.Text");
            loginBtn.Text = resourceManager.GetString("loginBtn.Text");
            exitBtn.Text = resourceManager.GetString("exitBtn.Text");
            label1TimeZone.Text = resourceManager.GetString("label1TimeZone.Text");
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = userNameInput.Text;
            string password = passwordInput.Text;

            try
            {
                string query = "SELECT COUNT(*) FROM User WHERE username = @username AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {

                        MainForm mainForm = new MainForm();
                        mainForm.Show();

                        this.Hide();

                    }
                    else
                    {
                        string errorMessage = resourceManager.GetString("errorMsg");
                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
