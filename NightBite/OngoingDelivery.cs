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
    public partial class OngoingDelivery : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public int did;
        public int orderid;
        public OngoingDelivery(int did)
        {
            InitializeComponent();
            this.did = did;
            LoadData();
        }
        private void LoadData()
        {
            string dStatusQuery = @"SELECT DStatus FROM tblDeliveryMan WHERE DId = @DId";
            string orderDetailsQuery = @"
SELECT o.CusUsername, o.CusAddress, o.CusPhone, o.OrderAmount, o.OrderId 
FROM tblOrder o 
WHERE o.DId = @DId AND o.OrderStatus = 'Assigned'"; // Filter by DId and OrderStatus

            string itemDetailsQuery = @"
SELECT ItemId, ItemPrice, ItemQuantity 
FROM tblOrderDetails 
WHERE OrderId = @OrderId";

            richTextBox1.Clear(); // Clear the RichTextBox before displaying data

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Query the DStatus of the delivery man
                SqlCommand cmdDStatus = new SqlCommand(dStatusQuery, con);
                cmdDStatus.Parameters.AddWithValue("@DId", did);
                string dStatus = (string)cmdDStatus.ExecuteScalar();  // Get the DStatus

                if (dStatus != "PickedUp")
                {
                    richTextBox1.AppendText("No delivery is ongoing.\n");
                    return;
                }
                else
                {
                    // Open a new connection for the orders
                    using (SqlConnection conOrder = new SqlConnection(connectionString))
                    {
                        conOrder.Open();
                        SqlCommand cmdOrders = new SqlCommand(orderDetailsQuery, conOrder);
                        cmdOrders.Parameters.AddWithValue("@DId", did);
                        SqlDataReader orderReader = cmdOrders.ExecuteReader();

                        while (orderReader.Read())
                        {
                            string cusUsername = orderReader["CusUsername"].ToString();
                            string cusAddress = orderReader["CusAddress"].ToString();
                            string cusPhone = orderReader["CusPhone"].ToString();
                            string orderAmount = orderReader["OrderAmount"].ToString();
                            int orderId = Convert.ToInt32(orderReader["OrderId"]);

                            // Display the order details in the RichTextBox
                            richTextBox1.AppendText($"Order ID: {orderId}\n");
                            richTextBox1.AppendText($"Customer Username: {cusUsername}\n");
                            richTextBox1.AppendText($"Customer Address: {cusAddress}\n");
                            richTextBox1.AppendText($"Customer Phone: {cusPhone}\n");
                            richTextBox1.AppendText($"Order Amount: {orderAmount}\n");

                            // Now fetch the item details for the specific OrderId
                            using (SqlConnection conItem = new SqlConnection(connectionString))
                            {
                                conItem.Open();
                                SqlCommand cmdItems = new SqlCommand(itemDetailsQuery, conItem);
                                cmdItems.Parameters.AddWithValue("@OrderId", orderId);
                                SqlDataReader itemReader = cmdItems.ExecuteReader();

                                while (itemReader.Read())
                                {
                                    int itemId = Convert.ToInt32(itemReader["ItemId"]);
                                    int itemPrice = Convert.ToInt32(itemReader["ItemPrice"]);
                                    int itemQuantity = Convert.ToInt32(itemReader["ItemQuantity"]);

                                    // Display item details in the RichTextBox
                                    richTextBox1.AppendText($"Item ID: {itemId}, Price: {itemPrice:C}, Quantity: {itemQuantity}\n");
                                }
                                itemReader.Close(); // Ensure itemReader is closed before continuing
                            }

                            richTextBox1.AppendText("\n"); // Add a space between different orders
                        }
                        orderReader.Close(); // Ensure orderReader is closed before closing connection
                    }
                }

                con.Close();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void OngoingDelivery_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(did);
            this.Close();
            homePage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            // Option to confirm delivery if status is PickedUp
            DialogResult result = MessageBox.Show("Confirm delivery?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Update the order status to "Delivered" and reset delivery man status to "Available"
                string updateOrderQuery = "UPDATE tblOrder SET OrderStatus = 'Delivered' WHERE DId = @DId AND OrderStatus = 'Assigned'";
                string updateDeliveryManQuery = "UPDATE tblDeliveryMan SET DStatus = 'Available' WHERE DId = @DId";

                SqlCommand cmdUpdateOrder = new SqlCommand(updateOrderQuery, con);
                cmdUpdateOrder.Parameters.AddWithValue("@DId", did);
                cmdUpdateOrder.ExecuteNonQuery();

                SqlCommand cmdUpdateDeliveryMan = new SqlCommand(updateDeliveryManQuery, con);
                cmdUpdateDeliveryMan.Parameters.AddWithValue("@DId", did);
                cmdUpdateDeliveryMan.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Delivery confirmed and statuses updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }

}
