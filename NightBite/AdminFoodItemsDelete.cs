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
    public partial class AdminFoodItemsDelete : Form
    {
        public string AdminName;
        private string selectedItemName;
        private int selectedPrice;
        private string selectedAv;
        private string selecetedCategory;
        private int selectedItemId;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminFoodItemsDelete(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadData();
            StyleDataGridView();
            /*string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2= @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_08_56 PM.png";
            button1.BackgroundImage = Image.FromFile(imagePath);
            button2.BackgroundImage = Image.FromFile(imagePath2);
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button2.BackgroundImageLayout = ImageLayout.Stretch;*/
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

            
            con.Close();
            dataGridView1.Columns["ItemId"].Visible = false;
            dataGridView1.Columns["ItemName"].HeaderText = "Item";
            dataGridView1.Columns["ItemCategory"].HeaderText = "Category";
            dataGridView1.Columns["ItemPrice"].HeaderText = "Price";
            dataGridView1.Columns["ItemPrice"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["ItemAv"].DefaultCellStyle.Format = "Availability";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            dataGridView1.CurrentCell = null;


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
            AdminFoodItems adminfood = new AdminFoodItems(AdminName);
            adminfood.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedItemName))
            {
                MessageBox.Show("Please select a valid product .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Do you want to delete {selectedItemName} , Price : {selectedPrice} Taka ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    string deleteQuery = "DELETE FROM tblItem WHERE ItemName = @item";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                    deleteCmd.Parameters.AddWithValue("@item", selectedItemName);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"{selectedItemName}  deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        label1.Text = $"Item Id : ";
                        label2.Text = $"Selected Item :";
                        label3.Text = $"Item Price : ";
                        label4.Text = $"Item Catagory : ";
                        label5.Text = $"Item Availability : ";
                        selectedItemName = null;
                        //AdminFoodItems adminHome = new AdminFoodItems(AdminName);
                        //this.Hide();
                        //adminHome.Show();

                    }
                    else
                    {
                        MessageBox.Show("No item found with that name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AdminFoodItemsDelete_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            selectedItemId = Convert.ToInt32(row.Cells["ItemId"].Value);
            selectedItemName = row.Cells["ItemName"].Value?.ToString() ?? "";
            selectedPrice = Convert.ToInt32(row.Cells["ItemPrice"].Value);
            selectedAv = row.Cells["ItemAv"].Value?.ToString() ?? "";
            selecetedCategory = row.Cells["ItemCategory"].Value?.ToString() ?? "";
            label1.Text = $"Item Id : {selectedItemId.ToString()}";
            label2.Text = $"Selected Item : {selectedItemName}";
            label3.Text = $"Item Price : {selectedPrice.ToString()}";
            label4.Text = $"Item Catagory : {selecetedCategory}";
            label5.Text = $"Item Availability : {selectedAv}";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
