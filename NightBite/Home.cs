using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NightBite
{
    public partial class Home : Form
    {

        public Home()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            Userlogin userlogin = new Userlogin();
            this.Hide();
            userlogin.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin adminlogin = new AdminLogin();
            adminlogin.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StaffLogin staffreg = new StaffLogin();
            staffreg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DLogin dmanreg = new DLogin();
            dmanreg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You for Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
