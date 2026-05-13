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
    public partial class AdminHome : Form
    {
        public string AdminName;
        public AdminHome(string AdminName)
        {
            
            InitializeComponent();
            this.AdminName = AdminName;
           
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_18_01 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath3 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_07_53 PM.png";
            string imagePath4 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_08_56 PM.png";
            string imagePath5 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_08_04 PM.png";
            string imagePath6 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_08_12 PM.png";
            // Load the image from the path and set it to the button's background image
            button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button3.BackgroundImage = Image.FromFile(imagePath3);
            button4.BackgroundImage = Image.FromFile(imagePath4);
            button5.BackgroundImage = Image.FromFile(imagePath5);
            button6.BackgroundImage = Image.FromFile(imagePath6);
            // Optional: Adjust the layout of the image (stretch, tile, or center it)
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button6.BackgroundImageLayout = ImageLayout.Stretch;*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Thank You for Visting {AdminName}", "Exit ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"{AdminName}, Do you want to Logout?", "LogOut Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                this.Hide();
                Home start = new Home();
                start.Show();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminFoodItems adminfood = new AdminFoodItems(AdminName);
            adminfood.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminOrders adminorder = new AdminOrders(AdminName);
            adminorder.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminStaff adminstaff = new AdminStaff(AdminName);
            adminstaff.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDeliveryman admindeliveryman = new AdminDeliveryman(AdminName);
            admindeliveryman.Show();
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {

        }
    }
}
