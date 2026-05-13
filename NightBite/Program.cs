using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NightBite
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.ApplicationExit += OnApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
        public static string Username { get; set; }
        public static Form PreviousForm { get; set; }
        static void OnApplicationExit(object sender, EventArgs e)
        {

            ClearCartForUser(Session.Username);

        }
        static void ClearCartForUser(string username)
        {
            string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand clear = new SqlCommand("DELETE FROM tblCart WHERE CusUsername = @username", con);
            clear.Parameters.AddWithValue("@username", username);
            clear.ExecuteNonQuery();
            con.Close();
        }
    }
}
