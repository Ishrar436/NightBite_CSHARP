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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NightBite
{
    public partial class AssignDeliveryStaff : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private int sid;
        private int selectedOrderId;
        private int did;
        public AssignDeliveryStaff(int sid )
        {
            InitializeComponent();
            this.sid = sid;
            PopulateComboBoxes();
            
        }

        private void PopulateComboBoxes()
        {
            
            string orderQuery = "SELECT OrderId FROM tblOrder WHERE OrderStatus = 'Ordered'";
            
            string deliveryManQuery = "SELECT DId FROM tblDeliveryMan WHERE DStatus IN ('Approved', 'Available')";

            
            comboBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(orderQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["OrderId"].ToString());
                }
                reader.Close();
                conn.Close();
            }

            comboBox2.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(deliveryManQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["DId"].ToString());
                }
                
                reader.Close();
                conn.Close();
            }
        }
        
        private void txtItemname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AssignDeliveryStaff_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                selectedOrderId = Convert.ToInt32(comboBox1.SelectedItem);

                
                string orderDetailsQuery = @"
                SELECT CusUSerName, CusAddress, CusPhone, OrderAmount
                FROM tblOrder 
                WHERE OrderId = @OrderId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(orderDetailsQuery, conn);
                    cmd.Parameters.AddWithValue("@OrderId", selectedOrderId);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            
                            label5.Text = "Customer Name : " + reader["CusUSerName"].ToString();
                            label7.Text = "Customer Address : " + reader["CusAddress"].ToString();
                            label6.Text = "Customer Phone No: " + reader["CusPhone"].ToString();
                            label8.Text = "Order Amount : " +  reader["OrderAmount"].ToString();

                           
                        }
                    }
                    reader.Close();
                    conn.Close();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            did = Convert.ToInt32(comboBox2.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StaffHomePage staffHome = new StaffHomePage(sid);
            this.Hide();
            staffHome.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both Order and DeliveryMan.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Do you want to Assign  {did} deliveryman to order id {selectedOrderId} ", "Assigned Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                selectedOrderId = Convert.ToInt32(comboBox1.SelectedItem);
                int deliveryManId = Convert.ToInt32(comboBox2.SelectedItem);

                string query = "UPDATE tblOrder SET DId = @DId, OrderStatus = @Status WHERE OrderId = @OrderId";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@DId", deliveryManId);
                    cmd.Parameters.AddWithValue("@Status", "Assigned");
                    cmd.Parameters.AddWithValue("@OrderId", selectedOrderId);
                    cmd.ExecuteNonQuery();


                    string updateDeliveryManQuery = "UPDATE tblDeliveryMan SET DStatus = 'PickedUp' WHERE DId = @DId";
                    SqlCommand cmdDeliveryMan = new SqlCommand(updateDeliveryManQuery, con);
                    cmdDeliveryMan.Parameters.AddWithValue("@DId", deliveryManId);
                    cmdDeliveryMan.ExecuteNonQuery();

                    MessageBox.Show("Order successfully assigned!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PopulateComboBoxes();
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    label5.Text = "Customer Name : ";
                    label7.Text = "Customer Address : ";
                    label6.Text = "Customer Phone No: ";
                    label8.Text = "Order Amount : ";


                    con.Close();
                }
  
            }
        }
    }
}
