using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NightBite
{
    public partial class Histroy : Form
    {
        public int did;
        public string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public Histroy(int did)
        {
            this.did = did;
            InitializeComponent();
            
            

        }
        
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Histroy_Load(object sender, EventArgs e)
        {
            string query = @"
        SELECT o.OrderId, o.CusUsername, o.CusAddress, o.CusPhone, o.OrderAmount, 
               i.ItemId, i.ItemPrice, i.ItemQuantity
        FROM tblOrder o
        INNER JOIN tblOrderDetails i ON o.OrderId = i.OrderId
        WHERE o.DId = @DId AND o.OrderStatus = 'Delivered'";// Combine both order and item details using JOIN

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query to get all orders and item details based on DId
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DId", did);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check if no results are found
                    if (!reader.HasRows)
                    {
                        richTextBox1.AppendText("No history available.\n");
                    }
                    else
                    {
                        int previousOrderId = -1;
                        while (reader.Read())
                        {
                            int orderId = Convert.ToInt32(reader["OrderId"]);
                            string cusUsername = reader["CusUsername"].ToString();
                            string cusAddress = reader["CusAddress"].ToString();
                            string cusPhone = reader["CusPhone"].ToString();
                            string orderAmount = reader["OrderAmount"].ToString();

                            // If a new order starts (i.e., OrderId changes), print the order details
                            if (orderId != previousOrderId)
                            {
                                richTextBox1.AppendText("-------------------------------------------------------------\n");
                                richTextBox1.AppendText($"Order ID: {orderId}\n");
                                richTextBox1.AppendText($"Customer Username: {cusUsername}\n");
                                richTextBox1.AppendText($"Customer Address: {cusAddress}\n");
                                richTextBox1.AppendText($"Customer Phone: {cusPhone}\n");
                                richTextBox1.AppendText($"Order Amount: {orderAmount}\n");

                                previousOrderId = orderId; // Update previousOrderId to the current one
                            }

                            // Print item details associated with the current order
                            int itemId = Convert.ToInt32(reader["ItemId"]);
                            int itemPrice = Convert.ToInt32(reader["ItemPrice"]);
                            int itemQuantity = Convert.ToInt32(reader["ItemQuantity"]);

                            richTextBox1.AppendText($"Item ID: {itemId}, Price: {itemPrice:C}, Quantity: {itemQuantity}\n");

                            richTextBox1.AppendText("\n"); // Add a space between items
                        }
                    }
                     // Add a space between items

                } // Close the reader after processing all rows

                con.Close(); // Close the database connection
            }



        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            HomePage homePage = new HomePage(did);
            this.Close();
            homePage.Show();
        }
    }
}
