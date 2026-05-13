using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NightBite
{
    public partial class OnlinePayment : Form
    {
        public string username;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public int total;
        public OnlinePayment(string username,int total)
        {
            InitializeComponent();
            this.username = username;
            this.total = total;
            this.FormClosing += UserForm_FormClosing;
        }
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            ClearCart.ClearCartForUser(username);

        }

        private void OnlinePayment_Load(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }

            else
            {
                textBox2.UseSystemPasswordChar = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cardNumber = textBox1.Text;
            string pin = textBox2.Text;
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                MessageBox.Show("Please enter your card number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(cardNumber, @"^\d+$"))
            {
                MessageBox.Show("Card Number must contain only numbers.", "Invalid Card Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(pin))
            {
                MessageBox.Show("Please enter your pin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(pin, @"^\d{1,5}$"))
            {
                MessageBox.Show("PIN must be a number and can only have up to 5 digits.", "Invalid PIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
            else if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                
                MessageBox.Show($"Order Had Ben Placed?\nThank You {username}For your Purchase ", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfirmOrder.AddOrdrr(username, "Online PAyment", total);
                UserHome homePage = new UserHome(username);
                homePage.Show();
                this.Close();
            }



            else
            {
                MessageBox.Show("Please Select and Mobile Banking Option", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart(username);
            cart.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
