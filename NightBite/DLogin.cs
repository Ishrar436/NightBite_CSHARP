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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NightBite
{
    public partial class DLogin : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private string did;
        public DLogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void DLogin_Load(object sender, EventArgs e)
        {

        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+$");
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            did = txtUsername.Text;
            string password = txPass.Text;

            if (!ValidatePhoneNumber(did))
            {
                MessageBox.Show("Please enter a valid Staff Id (numeric value).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtUsername.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(did) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string query = "SELECT COUNT(*) FROM tblDeliveryMan WHERE DId = @username AND DPass = @password AND DStatus != 'Pending'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", Convert.ToInt32(did));
            cmd.Parameters.AddWithValue("@password", password);


            int count = (int)cmd.ExecuteScalar();


            if (count == 1)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HomePage homePage = new HomePage(Convert.ToInt32(did));
                this.Hide();
                homePage.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials or your account is not approved.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txPass.Clear();
            }
            con.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txPass.UseSystemPasswordChar = false; // Show password
            }
            else
            {
                txPass.UseSystemPasswordChar = true; // Hide password
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DRegistration dRegistration = new DRegistration();
            this.Hide();
            dRegistration.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home(); 
            this.Hide(); 
            home.Show();
        }
    }
}
