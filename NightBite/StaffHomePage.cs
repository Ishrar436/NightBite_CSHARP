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
    public partial class StaffHomePage : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private int sid;
        public StaffHomePage(int sid)
        {
            InitializeComponent();
            this.sid = sid;
            label2.Text = $"Welcom Staff  No : {sid}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void StaffHomePage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You for Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            StaffFood staffFood = new StaffFood(sid);
            this.Hide();
            staffFood.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Logout?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You for Visting", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Home home = new Home();
                home.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssignDeliveryStaff assignDeliveryStaff = new AssignDeliveryStaff(sid);
            this.Hide();
            assignDeliveryStaff.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StaffOrderHistory staffOrderHistory = new StaffOrderHistory(sid);
            this.Close();
            staffOrderHistory.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
