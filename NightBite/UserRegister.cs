using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NightBite
{
    public partial class UserRegister : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public UserRegister()
        {
            InitializeComponent();
            textBox1.KeyDown += textBox_KeyDown;
            textBox2.KeyDown += textBox_KeyDown;
            textBox3.KeyDown += textBox_KeyDown;
            textBox4.KeyDown += textBox_KeyDown;
            textBox5.KeyDown += textBox_KeyDown;
            textBox6.KeyDown += textBox_KeyDown;
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox current = sender as System.Windows.Forms.TextBox;

            if (e.KeyCode == Keys.Down)
            {
                if (current == textBox1)
                    textBox2.Focus();
                else if (current == textBox2)
                    textBox3.Focus();
                else if (current == textBox3)
                    textBox4.Focus();
                else if (current == textBox4)
                    textBox5.Focus();
                else if (current == textBox5)
                    textBox6.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (current == textBox2)
                    textBox1.Focus();
                else if (current == textBox3)
                    textBox2.Focus();
                else if (current == textBox4)
                    textBox3.Focus();
                else if (current == textBox5)
                    textBox4.Focus();
                else if (current == textBox6)
                    textBox5.Focus();
            }
        }


        private void back_Click(object sender, EventArgs e)
        {
            Userlogin userlogin = new Userlogin();
            userlogin.Show();
            this.Close();
        }

        private void login_Click(object sender, EventArgs e)
        {

            string address = textBox5.Text;
            string username = textBox1.Text;
            string email = textBox6.Text;
            string phoneNumber = textBox4.Text;
            string password = textBox2.Text;
            string checkpass = textBox3.Text;


            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please Enter Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            else if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please Enter your phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits and contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Clear();
                textBox4.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter your address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(checkpass))
            {
                MessageBox.Show("Please enter rewrite password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (password != checkpass)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
                textBox2.Focus();
                return;
            }
            else
            {
                using(SqlConnection con = new SqlConnection(connectionString))
{
                    con.Open();

                    // 🔍 Step 1: Check if username already exists
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM tblCustomer WHERE CusUsername = @username", con);
                    checkCmd.Parameters.AddWithValue("@username", username);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // ❌ Username already exists
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        return;
                    }

                    // ✅ Step 2: Insert new customer
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO tblCustomer (CusEmail, CusPhone, CusPass, CusAddress, CusUsername) " +
                        "VALUES (@useremail, @userphone, @userpass, @useraddress, @username)", con);

                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@useremail", email);
                    insertCmd.Parameters.AddWithValue("@userphone", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@useraddress", address);
                    insertCmd.Parameters.AddWithValue("@userpass", password);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show($"Welcome To NightBite {username}!","Successfully Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    con.Close();
                    Session.Username = username;
                    UserHome homePage = new UserHome(username);
                    homePage.Show();
                    this.Close();
                }
            }
        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{11}$");
        }

        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
