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
    public partial class HomePage : Form
    {
        private int did;
        public HomePage(int did)
        {
            InitializeComponent();
            this.did = did;
        }

        private void home_page_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DProfile profile = new DProfile(did);
            this.Hide();
            profile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OngoingDelivery ongoingDelivery = new OngoingDelivery(did);
            this.Hide();
            ongoingDelivery.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


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
            Histroy histor = new Histroy(did);
            this.Hide();
            histor.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
