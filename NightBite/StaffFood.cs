using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NightBite
{
    public partial class StaffFood : Form
    {
        public int itemprice;
        private int itemid;
        private int selectedPrice;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        public int sid;

        public StaffFood(int sid)
        {
            InitializeComponent();
            this.sid = sid;
            LoadData();
            StyleDataGridView();
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

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CurrentCell = null;

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

        private void button1_Click(object sender, EventArgs e)
        {
            StaffHomePage staffHomePage = new StaffHomePage(sid);
            this.Hide();
            staffHomePage.Show();
        }

        private string GetItemNameById(int itemId)
        {
            string itemName = ""; // Variable to store the item name

            string query = "SELECT ItemName FROM tblItem WHERE ItemId = @ItemId"; // SQL query to get ItemName by ItemId

            // Open a connection to the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Create a command object with the SQL query and connection
                SqlCommand cmd = new SqlCommand(query, con);

                // Add the parameter to the SQL query
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                // Execute the query and get the result
                SqlDataReader reader = cmd.ExecuteReader();

                // Check if the query returns any results
                if (reader.Read())
                {
                    itemName = reader["ItemName"].ToString(); // Get the ItemName from the result
                }

                reader.Close();
                con.Close();
            }

            return itemName; // Return the ItemName
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string itemnewprice = textBox1.Text;
            if (string.IsNullOrEmpty(itemnewprice))
            {
                MessageBox.Show("Please Enter new price for item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(selectedPrice.ToString()))
            {
                MessageBox.Show("Please Select an  item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!ValidatePrice(itemnewprice))
            {
                MessageBox.Show("Enter Valid Price. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            else if(selectedPrice == Convert.ToInt32(itemnewprice))
            {
                MessageBox.Show("New Price Cannot be Same. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox1.Focus();
                return;
            }
            else
            {
                
                DialogResult result = MessageBox.Show($"Do you want to Update Price of {GetItemNameById(selectedPrice)} \n Old Price : {selectedPrice} \n New Price : {itemnewprice}", "Log Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    string query = "UPDATE tblItem SET ItemPrice = @newitem  WHERE ItemId = @ItemId";
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ItemId", itemid);
                    cmd.Parameters.AddWithValue("@newitem", Convert.ToInt32(itemnewprice));
                    int rowsAffected = cmd.ExecuteNonQuery(); // Execute the query and get the number of affected rows

                    if (rowsAffected > 0)
                    {
                        LoadData();
                        textBox1.Clear();
                        MessageBox.Show("Item price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the item price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                
            }

        }
        private bool ValidatePrice(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+$");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            itemid= Convert.ToInt32(row.Cells["ItemId"].Value);
            string selectedItemName = row.Cells["ItemName"].Value?.ToString() ?? "";
            selectedPrice = Convert.ToInt32(row.Cells["ItemPrice"].Value);
            string selectedAv = row.Cells["ItemAv"].Value?.ToString() ?? "";
            string selecetedCategory = row.Cells["ItemCategory"].Value?.ToString() ?? "";
            label1.Text = $"Item Id : {itemid.ToString()}";
            label2.Text = $"Selected Item : {selectedItemName}";
            label3.Text = $"Item Price : {selectedPrice.ToString()}";
            label4.Text = $"Item Catagory : {selecetedCategory}";
            label5.Text = $"Item Availability : {selectedAv}";
        }

        private void StaffFood_Load(object sender, EventArgs e)
        {

        }
    }
}
