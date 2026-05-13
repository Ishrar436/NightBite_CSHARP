using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    public partial class DRegistration : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public DRegistration()
        {
            InitializeComponent();
            txtUsername.KeyDown += textBox_KeyDown;
            txtPassword.KeyDown += textBox_KeyDown;
            txtConfirmPassword.KeyDown += textBox_KeyDown;
            txtPhonenumber.KeyDown += textBox_KeyDown;
            
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
                    txtAddress.Focus();
                else if (current == txtAddress)
                    txtPhonenumber.Focus();
                else if (current == txtPhonenumber)
                    txtUsername.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (current == txtUsername)
                    txtPhonenumber.Focus();
                else if (current == txtPassword)
                    txtUsername.Focus();
                else if (current == txtConfirmPassword)
                    txtPassword.Focus();
                else if (current == txtAddress)
                    txtConfirmPassword.Focus();
                else if (current == txtPhonenumber)
                    txtAddress.Focus();


            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DLogin Dlogin = new DLogin();
            Dlogin.Show();
        }

        private void DRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phoneNumber = txtPhonenumber.Text;
            string checkpass = txtConfirmPassword.Text;
            string address = txtAddress.Text;


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
                txtPhonenumber.Clear();
                txtPhonenumber.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please enter your Address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO tblDeliveryMan (DName , DPhone , DPass  ,DAddress, DStatus) " +
                        "VALUES (@stname, @stphone,@stpass,@daddress, @stStatus)", con);

                insertCmd.Parameters.AddWithValue("@stname", username);

                
                insertCmd.Parameters.AddWithValue("@stphone", phoneNumber);
                insertCmd.Parameters.AddWithValue("@stStatus", "Pending");
                insertCmd.Parameters.AddWithValue("@stpass", password);
                insertCmd.Parameters.AddWithValue("@daddress", address);

                insertCmd.ExecuteNonQuery();
                con.Close();
                con.Open();
                string query = @"SELECT DId FROM tblDeliveryMan WHERE DName = @StName AND DAddress = @StAddress AND DPhone = @StPhone AND DPass = @StPass ;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StName", username);
                cmd.Parameters.AddWithValue("@StAddress", address);
                cmd.Parameters.AddWithValue("@StPhone", phoneNumber);
                cmd.Parameters.AddWithValue("@StPass", password);
                
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    MessageBox.Show($"Welcome To Darkside {username} please wait for admin to approve \n Your Staff Id : {result} \n use this  {result} to login as Staff");
                }


                con.Close();

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
    }
}
