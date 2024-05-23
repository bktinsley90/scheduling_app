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
                string query = "SELECT userId FROM User WHERE username = @username AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int userId = Convert.ToInt32(cmd.ExecuteScalar());
                    if (userId > 0)
                    {

                        MainForm mainForm = new MainForm();
                        mainForm.Show();

                        CheckForUpcomingAppointments(userId);
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
        private void CheckForUpcomingAppointments(int userId)
        {
            DateTime now = DateTime.Now;
            DateTime fifteenMinutesLater = now.AddMinutes(15);

            string query = @"
            SELECT 
                appointmentId, customerId, title, description, location, contact, type, start, end 
            FROM 
                appointment 
            WHERE 
                userId = @userId AND 
                start BETWEEN @now AND @fifteenMinutesLater";

            using (MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@now", now);
                cmd.Parameters.AddWithValue("@fifteenMinutesLater", fifteenMinutesLater);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string title = reader["title"].ToString();
                        DateTime start = DateTime.Parse(reader["start"].ToString());
                        MessageBox.Show($"You have an upcoming appointment titled '{title}' at {start}.", "Upcoming Appointment Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

            private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
