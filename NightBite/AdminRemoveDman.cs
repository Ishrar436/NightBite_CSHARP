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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace NightBite
{
    public partial class AdminRemoveDman : Form
    {
        private string AdminName;
        private int Did = -1;
        private string Dname;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public AdminRemoveDman(string AdminIName)
        {
            InitializeComponent();
            this.AdminName = AdminIName;
            LoadData();
            StyleDataGridView();
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_34_00 PM.png";
            button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;*/
         }
        private void LoadData()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = @"SELECT DId, DName, DAddress, DPhone, DStatus 
                 FROM tblDeliveryMan  WHERE DStatus <> 'Pending'";
            SqlCommand cmd = new SqlCommand(query, con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView2.DataSource = dt;


            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AllowUserToAddRows = false;


            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


            dataGridView2.Columns["DId"].HeaderText = "Deliveryman Id";
            dataGridView2.Columns["DName"].HeaderText = "Deliveryman Name";
            dataGridView2.Columns["DPhone"].HeaderText = "Deliveryman Phone No";
            dataGridView2.Columns["DAddress"].HeaderText = "Deliveryman Address";
            dataGridView2.Columns["DStatus"].HeaderText = "Status";

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
        private void ShowInfo(int Did)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT  DName, DPhone,DAddress FROM tblDeliveryMan WHERE DId = @Did";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Did", Did);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Dname = reader["DName"].ToString();
                //Did = Convert.ToInt32(reader["DId"]);
                label7.Text = $"Deliveryman ID : {Did}";
                label9.Text = $"Deliveryman Name : " + reader["DName"].ToString(); 
                label8.Text = "Deliveryman Phone : " + reader["DPhone"].ToString();
                label10.Text = $"Deliveryman Address :" + reader["DAddress"].ToString();
            }
            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void AdminRemoveDman_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AdminRemoveDman_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminDeliveryman adminDeliveryman = new AdminDeliveryman(AdminName);
            this.Close();
            adminDeliveryman.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView2.Rows[e.RowIndex];
            Did = Convert.ToInt32(row.Cells["DId"].Value);
            ShowInfo(Did);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Did == -1)
            {
                MessageBox.Show("Please select  one Deliveryman.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult result = MessageBox.Show($"Remove Id : {Did} , \n Name :{Dname}  from Office", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand delete = new SqlCommand(
                        "DELETE FROM tblDeliveryMan  WHERE DId = @did ", con);
                    delete.Parameters.AddWithValue("@did", Did);
                    delete.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deliveryman Terminated.", "Successfully Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    label7.Text = $"Deliveryman ID :";
                    label9.Text = $"Deliveryman Name : ";
                    label8.Text = "Deliveryman Phone : ";
                    label10.Text = $"Deliveryman Address :";
                    dataGridView2.CurrentCell = null;
                    LoadData();
                    Did = -1;
                }
            }
        }
    }
}
