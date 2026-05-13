using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NightBite
{
    public partial class StaffLogin : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public StaffLogin()
        {
            InitializeComponent();
            txtPassword.KeyDown += textBox_KeyDown;
            txtUsername.KeyDown += textBox_KeyDown;
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox current = sender as System.Windows.Forms.TextBox;

            if (e.KeyCode == Keys.Down)
            {
                if (current == txtUsername)
                    txtPassword.Focus();
                else if(current == txtPassword)
                    txtUsername.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                if (current == txtPassword)
                    txtUsername.Focus();
                if (current == txtUsername)
                    txtPassword.Focus();

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void StaffLogin_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StaffRegistration staffRegistration = new StaffRegistration();
            this.Hide();
            staffRegistration.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
           string sid = txtUsername.Text;
            string password = txtPassword.Text;
            
            if (!ValidatePhoneNumber(sid))
            {
                MessageBox.Show("Please enter a valid Staff Id (numeric value).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtUsername.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             int Stid = Convert.ToInt32(sid);
            string query = "SELECT COUNT(*) FROM tblStaff WHERE StaffId = @username AND StPass = @password AND StStatus = 'Approved'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", Stid);
            cmd.Parameters.AddWithValue("@password", password);
     
            int count = (int)cmd.ExecuteScalar();
            

            if (count == 1)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StaffHomePage staffHomePage = new StaffHomePage(Stid);
                this.Hide();
                staffHomePage.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials or your account is not approved.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtUsername.Focus();
                txtPassword.Clear();
                return;
            }
            con.Close();
        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+$");
        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true; // Hide password
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Close();
            home.Show();
        }
    }
}
