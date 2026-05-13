using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightBite
{
    internal class ClearCart
    {
        public static void ClearCartForUser(string username)
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
