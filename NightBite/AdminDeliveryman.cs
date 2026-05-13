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
    public partial class AdminDeliveryman : Form
    {
        public string AdminName;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminDeliveryman(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadData();
            StyleDataGridView();
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\picsChatGPT Image Jul 17, 2025, 02_33_49 PM.png";
            string imagePath3 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_34_00 PM.png";
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

            string query = @"SELECT DId, DName, DAddress, DPhone, DStatus 
                 FROM tblDeliveryMan 
                 WHERE DStatus IN (@status1, @status2, @status3, @status4)";

            SqlCommand cmd = new SqlCommand(query, con);

            // Add each status you want
            cmd.Parameters.AddWithValue("@status1", "Approved");
            cmd.Parameters.AddWithValue("@status2", "PickedUp");
            cmd.Parameters.AddWithValue("@status3", "NoDuty");
            cmd.Parameters.AddWithValue("@status4", "Assigned");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;


            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


            dataGridView1.Columns["DId"].HeaderText = "Deliveryman Id";
            dataGridView1.Columns["DName"].HeaderText = "Deliveryman Name";
            dataGridView1.Columns["DPhone"].HeaderText = "Deliveryman Phone No";
            dataGridView1.Columns["DAddress"].HeaderText = "Deliveryman Address";
            dataGridView1.Columns["DStatus"].HeaderText = "Status";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            dataGridView1.CurrentCell = null;

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


        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminHome adminhomePage = new AdminHome(AdminName);
            this.Hide();
            adminhomePage.Show();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            AdminDmanAccept admindmanaccept = new AdminDmanAccept(AdminName);
            this.Hide();
            admindmanaccept.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminDeliveryman_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminRemoveDman adminRemoveDman = new AdminRemoveDman(AdminName);
            this.Hide();
            adminRemoveDman.Show();
        }
    }
}
