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
    public partial class StaffOrderHistory : Form
    {
        private int sid;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public StaffOrderHistory(int sid)
        {
            InitializeComponent();
            StyleDataGridView();
            LoadData();
            this.sid = sid;
            LoadData2();
            
        }
        private void LoadData2()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT StName, StEmail FROM tblStaff where StaffId = @stid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@stid", sid);
            string email = "";
            string name = "";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                email = reader["StEmail"].ToString();
                name = reader["StName"].ToString();
            }
            label1.Text = $"Staff Id : {sid}";
            label2.Text = $"Staff Name : {name}";
            label3.Text = $"Staff Email : {email}";

            con.Close();
        }
        private void LoadData()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT OrderId, CusUsername,CusAddress ,OrderAmount, Did, PaymentMethod, OrderStatus FROM tblOrder";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            dataGridView1.DataSource = dt;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;


            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


           dataGridView1.Columns["OrderId"].HeaderText = "Order Id";
            dataGridView1.Columns["CusUsername"].HeaderText = "Customer Username";
            dataGridView1.Columns["CusAddress"].HeaderText = "Customer Address";
            dataGridView1.Columns["OrderAmount"].HeaderText = "Order Amount";
            dataGridView1.Columns["Did"].HeaderText = "Delivery Man ID";
            dataGridView1.Columns["PaymentMethod"].HeaderText = "Payment Method";
            dataGridView1.Columns["OrderStatus"].HeaderText = "Order Status";

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtItemname_TextChanged(object sender, EventArgs e)
        {

        }

        private void StaffOrderHistory_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffHomePage staffHomePage = new StaffHomePage(sid);
            this.Hide();
            staffHomePage.Show();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
