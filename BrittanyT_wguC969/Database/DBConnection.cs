using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrittanyT_wguC969.Database
{
    public class DBConnection
    {
        public static MySqlConnection conn { get; set; }

        public static string CurrentUser{ get; set; }
        
        public static void startConnection()
        {
            try
            {
                string construct = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
                conn = new MySqlConnection(construct);

                conn.Open();
                //MessageBox.Show("Connection is open");

            }
            catch(MySqlException ex) {
                MessageBox.Show(ex.Message);
            }

        }
      
        public static void closeConnection()
        {
            try
            {
                if(conn != null)
                {
                    conn.Close();
                }
                conn = null;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
