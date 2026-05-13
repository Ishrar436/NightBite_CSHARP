using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    public partial class AdminStaffAccept : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private string AdminName;
        private string Stname;
        private int Sid;
        
        public AdminStaffAccept(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadItems();
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_40_39 PM.png";
            string imagePath3 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_40_48 PM.png";
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
            string query = "SELECT StaffId  FROM tblStaff WHERE StStatus = 'Pending'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader["StaffId"].ToString());
            }

            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void ShowInfo(int Sid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT StName, StEmail, StPhone, StAddress FROM tblStaff WHERE StaffId = @stId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@stId", Sid);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Stname = reader["StName"].ToString();
                label8.Text = $"Staff Name : {Stname}";
                label11.Text = "Staff Email : " + reader["StEmail"].ToString();
                label9.Text = "Staff Phone : " + reader["StPhone"].ToString();
                label10.Text = "Staff Address : " + reader["StAddress"].ToString();


            }
            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void AdminStaffAccept_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminStaffAccept_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                Sid = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                ShowInfo(Sid);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminStaff adminStaff = new AdminStaff(AdminName);
            this.Hide();
            adminStaff.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select  one Staff.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult result = MessageBox.Show($"Remove Id : {Sid} , /n Name :{Stname}  from Office", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand delete = new SqlCommand(
                        "DELETE FROM tblStaff  WHERE StaffId = @sid ", con);
                    delete.Parameters.AddWithValue("@sid", Sid);
                    delete.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Item deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    label8.Text = $"Staff Name :";
                    label11.Text = "Staff Email : ";
                    label9.Text = "Staff Phone : ";
                    label10.Text = "Staff Address : ";
                    comboBox2.Items.Clear();
                    LoadItems();
                    Sid = -1;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Staff id.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string message = $"Do you want to Register staff ID : {Sid} ,Name : {Stname}, ?";
                DialogResult result = MessageBox.Show(message, "Confirm Add to Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string updateQuery = "UPDATE tblStaff SET StStatus = 'Approved' WHERE StaffId = @stId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@stId", Sid);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Staff approved successfully.", "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboBox2.Items.Remove(comboBox2.SelectedItem);
                       
                        comboBox2.Items.Clear();
                        LoadItems();

                        label8.Text = $"Staff Name :";
                        label11.Text = "Staff Email : ";
                        label9.Text = "Staff Phone : ";
                        label10.Text = "Staff Address : ";
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Staff not found.");
                    }

                    con.Close();
                }
            }
        }
    }
}
