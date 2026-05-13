using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace NightBite
{
    public partial class AdminFoodItems : Form
    {
        public string AdminName;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminFoodItems(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadData();
            StyleDataGridView();

            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_17_00 PM.png";
            string imagePath3 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_08_56 PM.png";
            button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button3.BackgroundImage = Image.FromFile(imagePath3);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button3.BackgroundImageLayout = ImageLayout.Stretch;*/

        }
        private void LoadData()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = @"SELECT ItemId, ItemName, ItemCategory, ItemPrice , ItemAv FROM tblItem";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            dataGridView1.DataSource = dt;

           
            
            dataGridView1.Columns["ItemId"].Visible = false;
            dataGridView1.Columns["ItemName"].HeaderText = "Item";
            dataGridView1.Columns["ItemCategory"].HeaderText = "Category";
            dataGridView1.Columns["ItemPrice"].HeaderText = "Price";
            dataGridView1.Columns["ItemPrice"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["ItemAv"].HeaderText = "Availability";
            con.Close();
        }
        private void StyleDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoResizeColumns();

            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30); 
            dataGridView1.GridColor = Color.DarkSlateGray;

            
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48); 
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Teal;        
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            
            dataGridView1.RowHeadersVisible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHome homePage = new AdminHome(AdminName);
            homePage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminFoodItemsAdd adminfoodadd = new AdminFoodItemsAdd(AdminName);
            adminfoodadd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminFoodItemsDelete adminfooddelete = new AdminFoodItemsDelete(AdminName);
            adminfooddelete.Show();
        }

        private void AdminFoodItems_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
