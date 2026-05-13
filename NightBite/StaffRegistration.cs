using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NightBite
{
    public partial class StaffRegistration : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public StaffRegistration()
        {
            InitializeComponent();
            txtUsername.KeyDown += textBox_KeyDown;
            txtPassword.KeyDown += textBox_KeyDown;
            txtConfirmPassword.KeyDown += textBox_KeyDown;
            txtEmail.KeyDown += textBox_KeyDown;
            txtPhonenumber.KeyDown += textBox_KeyDown;
            textAddress.KeyDown += textBox_KeyDown;
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox current = sender as System.Windows.Forms.TextBox;

            if (e.KeyCode == Keys.Down)
            {
                if (current == txtUsername)
                    txtPassword.Focus();
                else if (current == txtPassword)
                    txtConfirmPassword.Focus();
                else if (current == txtConfirmPassword)
                    txtEmail.Focus();
                else if (current == txtEmail)
                    txtPhonenumber.Focus();
                else if (current == txtPhonenumber)
                    txtUsername.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if(current == txtUsername)
                    txtPhonenumber.Focus();
                else if (current == txtPassword)
                    txtUsername.Focus();
                else if (current == txtConfirmPassword)
                    txtPassword.Focus();
                else if (current == textAddress)
                    txtConfirmPassword.Focus();
                else if (current == txtPhonenumber)
                    txtEmail.Focus();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StaffLogin staffLogin = new StaffLogin();
            staffLogin.Show();
        }

        private void StaffRegistration_Load(object sender, EventArgs e)
        {

        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{11}$");
        }
        private void button2_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhonenumber.Text;
            string checkpass = txtConfirmPassword.Text;
            string address = textAddress.Text;


            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please Enter Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }
            else if (!ValidatePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Phone number must be 11 digits and contain only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhonenumber.Clear();
                txtPhonenumber.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please Enter your Address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter your Email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO tblStaff (StName, StEmail , StPhone , StPass  ,StStatus, StAddress) " +
                        "VALUES (@stname, @stemail, @stphone,@stpass, @stStatus, @staddress)", con);

                insertCmd.Parameters.AddWithValue("@stname", username);
                insertCmd.Parameters.AddWithValue("@staddress", address);
                insertCmd.Parameters.AddWithValue("@stemail", email);
                insertCmd.Parameters.AddWithValue("@stphone", phoneNumber);
                insertCmd.Parameters.AddWithValue("@stStatus", "Pending");
                insertCmd.Parameters.AddWithValue("@stpass", password);



                insertCmd.ExecuteNonQuery();

                string query = @"SELECT StaffId FROM tblStaff WHERE StName = @StName AND StAddress = @StAddress AND StPhone = @StPhone AND StPass = @StPass AND StEmail = @StEmail;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StName", username);
                cmd.Parameters.AddWithValue("@StAddress", address);
                cmd.Parameters.AddWithValue("@StPhone", phoneNumber);
                cmd.Parameters.AddWithValue("@StPass", password);
                cmd.Parameters.AddWithValue("@StEmail", email);
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    MessageBox.Show($"Welcome To Darkside {username} please wait for admin to approve \n Your Staff Id : {result} \n use this  {result} to login as Staff");
                }
                con.Close();



                Home home = new Home();
                this.Hide();
                home.Show();

            }
        }
        

        private void phoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtConfirmPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.Show();
        }
    }
}
    

