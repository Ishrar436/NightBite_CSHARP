using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    public partial class AdminDmanAccept : Form
    {
        private string AdminName;
        private int Did;
        private string Dname;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminDmanAccept(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadItems();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
           
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_39_50 PM.png";
            string imagePath3= @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_39_59 PM.png";
            button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button3.BackgroundImage = Image.FromFile(imagePath3);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button3.BackgroundImageLayout = ImageLayout.Stretch;*/


        }




        private void LoadItems()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT DId  FROM tblDeliveryMan WHERE DStatus = 'Pending'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["DId"]);
            }

            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Did = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                ShowInfo(Did);
            }
        }
        private void ShowInfo(int Did)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT  DName, DPhone,DAddress FROM tblDeliveryMan WHERE DId = @Did";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Did", Did);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {


                label1.Text = $"Deliveryman Name : "+ reader["DName"].ToString();
                label4.Text = "Deliveryman Phone : " + reader["DPhone"].ToString();
                label5.Text = "Deliveryman Status: Pending";
                label3.Text = $"Deliveryman Address :" + reader["DAddress"];
            }
            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }


        private void AdminDmanAccept_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDeliveryman adminDeliveryman = new AdminDeliveryman(AdminName);
            this.Hide();
            adminDeliveryman.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please Select a deliveryman id.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string message = $"Do you want to Register Deliveryman ID : {Did} ,Name : {Dname}, ?";
                DialogResult result = MessageBox.Show(message, "Approve Deliveryman", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string updateQuery = "UPDATE tblDeliveryMan SET DStatus = 'Approved' WHERE DId = @dId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@dId", Did);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Deliveryman approved successfully.", "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                        label4.Text = "Deliveryman Status : Approved";

                        comboBox1.Items.Clear();
                        LoadItems();
                        label1.Text = $"Deliveryman Name : ";
                        label4.Text = "Deliveryman Phone : ";
                        label5.Text = "Deliveryman Status: Pending";
                        label3.Text = $"Deliveryman Address :";
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Deliveryman not found.");
                    }

                    con.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid product .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Do you want to delete {Dname} , Id : {Did}  ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string deleteQuery = "DELETE FROM tblDeliveryMan WHERE Did = @did";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                    deleteCmd.Parameters.AddWithValue("@did", Did);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"{Dname}  deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItems();

                        label1.Text = $"Deliveryman Name : ";
                        label4.Text = "Deliveryman Phone : ";
                        label5.Text = "Deliveryman Status: Pending";
                        label3.Text = $"Deliveryman Address :";
                        Dname = null;
                        Did = -1;
                        comboBox1.Items.Clear();
                        LoadItems();


                    }
                    else
                    {
                        MessageBox.Show("No item found with that name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Did = -1;
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
