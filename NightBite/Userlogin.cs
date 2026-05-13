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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NightBite
{
    
    public partial class Userlogin : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public Userlogin()
        {
            InitializeComponent();
            textBox1.KeyDown += textBox_KeyDown;
            textBox2.KeyDown += textBox_KeyDown;
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox current = sender as System.Windows.Forms.TextBox;

            if (e.KeyCode == Keys.Down)
            {
                if (current == textBox1)
                    textBox2.Focus();
               
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (current == textBox2)
                    textBox1.Focus();
                
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();

        }

        private void login_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string querry = @"SELECT COUNT(*) FROM tblCustomer WHERE CusUsername = @username";
            SqlCommand checkUserCmd = new SqlCommand(querry, con);
            checkUserCmd.Parameters.AddWithValue("@username", username);
            int userCount = (int)checkUserCmd.ExecuteScalar();
            if (userCount > 0)
            {
                string querry2 = @"SELECT CusPass FROM tblCustomer WHERE CusUsername = @username";
                SqlCommand checkPasswordCmd = new SqlCommand(querry2, con);
                checkPasswordCmd.Parameters.AddWithValue("@username", username);

                string storedPassword = checkPasswordCmd.ExecuteScalar().ToString();

                if (storedPassword == password)
                {
                    MessageBox.Show($"Login successful! Welcome {username} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Session.Username = username;
                    UserHome homePage = new UserHome(username);
                    homePage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                    textBox2.Focus();
                    textBox2.Focus();
                    textBox2.Focus();
                    return;
                }

            }
            else
            {
                MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox1.Clear();
                textBox1.Focus();
                return;
            }



            con.Close();
        }

        private void register_Click(object sender, EventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                textBox2.UseSystemPasswordChar = true; // Hide password
            }
        }

        private void Userlogin_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
