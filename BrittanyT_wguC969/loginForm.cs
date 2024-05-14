using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969
{
    public partial class loginForm : Form
    {
        private MySqlConnection conn;
        private ResourceManager resourceManager;
        public loginForm(MySqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
            LoadLanguage(CultureInfo.CurrentCulture);
        }
        private void LoadLanguage(CultureInfo cultureInfo)
        {
            string languageCode = cultureInfo.TwoLetterISOLanguageName;
            resourceManager = new ResourceManager($"BrittanyT_wguC969.Resources.{languageCode}", typeof(loginForm).Assembly);
            userNameLabel.Text = resourceManager.GetString("userNameLabel");
            passwordLabel.Text = resourceManager.GetString("passwordLabel");
            loginBtn.Text = resourceManager.GetString("loginBtn");
            exitBtn.Text = resourceManager.GetString("exitBtn");
        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = userNameInput.Text;
            string password = passwordInput.Text;

            try
            {
                string query = "SELECT COUNT(*) FROM User WHERE username = @username AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
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
