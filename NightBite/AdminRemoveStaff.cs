using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NightBite
{
    public partial class AdminRemoveStaff : Form
    {
        private string AdminName;
        private int sid = -1;
        private string sname;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public AdminRemoveStaff(string sname)
        {
            InitializeComponent();
            this.sname = sname;
            LoadData();
            StyleDataGridView();
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_40_28 PM.png";
           button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;*/
            
        }
        private void LoadData()
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = @"SELECT StaffId, StName, StAddress, StPhone, StStatus ,StEmail
                 FROM tblStaff WHERE StStatus <> 'Pending';";
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


            dataGridView2.Columns["StaffId"].HeaderText = "Staff Id";
            dataGridView2.Columns["StName"].HeaderText = "Staff Name";
            dataGridView2.Columns["StPhone"].HeaderText = "Staff Phone No";
            dataGridView2.Columns["StAddress"].HeaderText = "Staff Address";
            dataGridView2.Columns["StEmail"].HeaderText = "Staff Email";
            dataGridView2.Columns["StStatus"].HeaderText = "Status";

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
        private void ShowInfo(int Sid)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT  StName, StPhone,StAddress, StStatus ,StEmail FROM tblStaff WHERE StaffId = @sid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@sid", Sid);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                sname = reader["StName"].ToString();
                //Did = Convert.ToInt32(reader["DId"]);
                label8.Text = $"Staff ID : {Sid}";
                label9.Text = $"Staff Name : " + reader["StName"].ToString();
                label10.Text = "Staff Phone : " + reader["StPhone"].ToString();
                label12.Text = $"Staff Address :" + reader["StAddress"].ToString();
                label11.Text = $"Staff Email :" + reader["StEmail"].ToString();
            }
            reader.Close();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void AdminRemoveStaff_Load(object sender, EventArgs e)
        {

        }

        private void AdminRemoveStaff_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView2.Rows[e.RowIndex];
            sid = Convert.ToInt32(row.Cells["StaffId"].Value);
            ShowInfo(sid);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminStaff adminStaff = new AdminStaff(AdminName);
            this.Hide();
            adminStaff.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sid == -1)
            {
                MessageBox.Show("Please select  one Staff.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DialogResult result = MessageBox.Show($"Terminate Id : {sid} , \n Name :{sname}  from Office", "Confirm Termination", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand delete = new SqlCommand(
                        "DELETE FROM tblStaff  WHERE StaffId = @sid ", con);
                    delete.Parameters.AddWithValue("@sid", sid);
                    delete.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show($"{sid} Succsessfully terminated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    label8.Text = $"Staff ID : ";
                    label9.Text = $"Staff Name : " ;
                    label10.Text = "Staff Phone : " ;
                    label12.Text = $"Staff Address :";
                    label11.Text = $"Staff Email :";

                    dataGridView2.CurrentCell = null;
                    LoadData();
                    sid = -1;
                }
            }
        }
    }
}
