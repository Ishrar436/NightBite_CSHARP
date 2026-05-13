using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    public partial class UserHistory : Form
    {
        public string username;
        public string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public UserHistory(string username)
        {
            this.username = username;
            InitializeComponent();
            LoadOrderHistory();
            UserInfo();
           
        }
        private void LoadOrderHistory()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Step 1: Get all order IDs for current user
                string getOrdersQuery = "SELECT OrderId , OrderAMount FROM tblOrder WHERE CusUsername = @username";
                SqlCommand cmdOrders = new SqlCommand(getOrdersQuery, con);
                cmdOrders.Parameters.AddWithValue("@username", username);

                List<int> orderIds = new List<int>();
                List<int> orderAmount = new List<int>();
                SqlDataReader reader = cmdOrders.ExecuteReader();
                while (reader.Read())
                {
                    orderIds.Add(Convert.ToInt32(reader["OrderId"]));
                    orderAmount.Add(Convert.ToInt32(reader["OrderAmount"]));
                }
                reader.Close();

                // ✅ Check if no orders found
                if (orderIds.Count == 0)
                {
                    richTextBox1.AppendText("No orders placed yet.\n");
                }
                else
                {
                    // Step 2: For each orderId, get details from tblOrderDetails
                    for (int i = 0; i < orderIds.Count; i++)
                    {
                        int orderId = orderIds[i];
                        int orderTotal = orderAmount[i];

                        richTextBox1.AppendText($"Order ID: {orderId}\n");

                        string getDetailsQuery = "SELECT ItemId, ItemPrice, ItemQuantity FROM tblOrderDetails WHERE OrderId = @orderId";
                        SqlCommand cmdDetails = new SqlCommand(getDetailsQuery, con);
                        cmdDetails.Parameters.AddWithValue("@orderId", orderId);

                        SqlDataReader detailReader = cmdDetails.ExecuteReader();
                        while (detailReader.Read())
                        {
                            string itemId = detailReader["ItemId"].ToString();
                            string price = detailReader["ItemPrice"].ToString();
                            int quantity = Convert.ToInt32(detailReader["ItemQuantity"]);

                            richTextBox1.AppendText($"   - Item ID: {itemId}, Price: {price}, Qty: {quantity}\n");
                        }
                        detailReader.Close();

                        richTextBox1.AppendText($"   ➤ Total: {orderTotal}\n\n");
                    }
                }

                con.Close();
            }
        }
        private void UserInfo()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string getOrdersQuery = "SELECT CusAddress,CusPhone,CusEmail FROM tblCustomer WHERE CusUsername = @username";
            SqlCommand cmdDetails = new SqlCommand(getOrdersQuery, con);
            cmdDetails.Parameters.AddWithValue("@username", username);
            SqlDataReader detailReader = cmdDetails.ExecuteReader();
            while (detailReader.Read())
            {
                string useraddress = detailReader["CusAddress"].ToString();
                string userphoneno = detailReader["CusPhone"].ToString();
                string useremail = detailReader["CusEmail"].ToString();
                label1.Text = $"USER NAME : {username}";
                label2.Text = $"USER PHONE NO : {userphoneno}";
                label3.Text = $"USER EMAIL :{useremail}";
                label4.Text = $"USER ADDRESS : {useraddress}";

                
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserHome homePage = new UserHome(username);
            this.Close();
            homePage.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserHistory_Load(object sender, EventArgs e)
        {

        }
    }
}
