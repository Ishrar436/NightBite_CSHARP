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
    public partial class AdminOrders : Form
    {
        public string AdminName;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminOrders(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            StyleDataGridView();
            LoadData();
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            button1.BackgroundImage = Image.FromFile(imagePath);
            button1.BackgroundImageLayout = ImageLayout.Stretch;*/
        }
        private void LoadData()
        {
            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT OrderId, CusUsername, CusAddress , OrderAmount, OrderStatus, PaymentMethod , Did  FROM tblOrder where OrderStatus =@orderstat ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@orderstat", "Delivered");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

           
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false; 

            
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            
            dataGridView1.Columns["CusUsername"].HeaderText = "Customer Username";
            dataGridView1.Columns["Did"].HeaderText = "Deliveryman ID";
            dataGridView1.Columns["Orderid"].HeaderText = "Order Id";
            dataGridView1.Columns["CusAddress"].HeaderText = "Customer Address";
            dataGridView1.Columns["OrderAmount"].HeaderText = "Total Amount";
            dataGridView1.Columns["OrderStatus"].HeaderText = "Status";
            dataGridView1.Columns["PaymentMethod"].HeaderText = "Payment Method";





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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminOrders_Load(object sender, EventArgs e)
        {

        }
    }
}
