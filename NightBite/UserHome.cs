using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    public partial class UserHome : Form
    {
        public string username;
        public UserHome(string username )
        {
            InitializeComponent();
            this.username = username;
            this.FormClosing += UserForm_FormClosing;
        }
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
           ClearCart.ClearCartForUser(username);
            
        }

        private void UserHome_Load(object sender, EventArgs e)
        {

        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            Program.PreviousForm = this;
            Cart cart = new Cart(username);
            cart.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                ClearCart.ClearCartForUser(username);
                MessageBox.Show($"Thank You for Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Logout?", "Log Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                ClearCart.ClearCartForUser(username);
                MessageBox.Show($"Thank You for Visting {username}", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Home home = new Home();
                home.Show();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browse browse = new Browse(username);
            browse.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserHistory history = new UserHistory(username);
            this.Hide();
            history.Show();
        }
    }
}
