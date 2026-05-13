using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NightBite
{
    public partial class AdminStaff : Form
    {
        public string AdminName;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminStaff(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadData();
            StyleDataGridView();
            
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_40_18 PM.png";
            string imagePath3 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_40_28 PM.png";
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

            string query = "SELECT StaffId, StName, StEmail, StPhone , StAddress FROM tblStaff WHERE StStatus = @status";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@status", "Approved");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView2.DataSource = dt;

            
           
            dataGridView2.AllowUserToAddRows = false;



            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView2.Columns["StaffId"].HeaderText = "Staff Id";
            dataGridView2.Columns["StName"].HeaderText = "Staff Name";
            dataGridView2.Columns["StPhone"].HeaderText = "Staff Phone No";
            dataGridView2.Columns["StAddress"].HeaderText = "Staff Address";
            dataGridView2.Columns["StEmail"].HeaderText = "Status";
            

            dataGridView2   .SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.CellClick += dataGridView1_CellContentClick;
            dataGridView2.CurrentCell = null;


            con.Close();



        }
        private void StyleDataGridView()
        {
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.AutoResizeColumns();

            dataGridView2.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView2.GridColor = Color.DarkSlateGray;


            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


            dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dataGridView2.DefaultCellStyle.ForeColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Teal;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView2.DefaultCellStyle.Font = new Font("Segoe UI", 10);


            dataGridView2.RowHeadersVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void AdminStaff_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminStaff_Load_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminHome homePage = new AdminHome(AdminName);
            homePage.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminStaffAccept adminStaffAccept = new AdminStaffAccept(AdminName);
            this.Hide();
            adminStaffAccept.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminRemoveStaff adminRemoveStaff = new AdminRemoveStaff(AdminName);
            this.Hide();
            adminRemoveStaff.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
