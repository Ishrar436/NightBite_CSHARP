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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NightBite
{
    public partial class AdminLogin : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public AdminLogin()
        {
            InitializeComponent();
            textBox3.KeyDown += textBox_KeyDown;
            textBox4.KeyDown += textBox_KeyDown;
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox current = sender as System.Windows.Forms.TextBox;

            if (e.KeyCode == Keys.Down)
            {
                if (current == textBox3)
                    textBox4.Focus();
               else if (current == textBox4)
                    textBox3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (current == textBox4)
                    textBox3.Focus();
                else if (current == textBox3)
                    textBox4.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btlogin_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void cbshow_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void AdminLogin_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text;
            string password = textBox4.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string querry = @"SELECT COUNT(*) FROM tblAdmin WHERE AdminName = @username";
            SqlCommand checkUserCmd = new SqlCommand(querry, con);
            checkUserCmd.Parameters.AddWithValue("@username", username);
            int userCount = (int)checkUserCmd.ExecuteScalar();
            if (userCount > 0)
            {
                string querry2 = @"SELECT AdminPass FROM tblAdmin WHERE AdminName = @username";
                SqlCommand checkPasswordCmd = new SqlCommand(querry2, con);
                checkPasswordCmd.Parameters.AddWithValue("@username", username);

                string storedPassword = checkPasswordCmd.ExecuteScalar().ToString();

                if (storedPassword == password)
                {
                    MessageBox.Show($"Login successful! Welcome {username} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    AdminHome homePage = new AdminHome(username);
                    homePage.Show();
                }
                else
                {
                    MessageBox.Show("Wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox2.Focus();
                    return;
                }

            }
            else
            {
                MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox1.Clear();
                return;
            }



            con.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                textBox4.UseSystemPasswordChar = true; // Hide password
            }
        }
    }
}