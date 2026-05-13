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
    public partial class Browse : Form
    {
        
        private string _cs = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private readonly string _username;

        private int selectedItemId = -1;
        private string selectedItemName = "";
        private int selectedPrice = 0;
        private int qty = 1;
        public Browse(string username)
        {
            _username = username;
            InitializeComponent();
            SetupControls();
            LoadItems();
            this.FormClosing += UserForm_FormClosing;


        }
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            ClearCart.ClearCartForUser(_username);

        }
        private void SetupControls()
        {
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.CurrentCell = null;

            // NumericUpDown setup
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 10;
            numericUpDown1.Value = 1;

            // AddToCart Button
           
            
        }
        
        private void LoadItems()
        {
            using (SqlConnection con = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ItemId, ItemName, ItemCategory, ItemPrice, ItemAV FROM tblItem  where ItemAv = 'Available' ORDER BY ItemId ", con))
                {
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(rdr);
                        dataGridView1.DataSource = dt;
                    }
                }
                con.Close();
            }

            dataGridView1.Columns["ItemId"].Visible = false;
            dataGridView1.Columns["ItemName"].HeaderText = "Item";
            dataGridView1.Columns["ItemCategory"].HeaderText = "Category";
            dataGridView1.Columns["ItemPrice"].HeaderText = "Price";
            dataGridView1.Columns["ItemPrice"].DefaultCellStyle.Format = "C0"; 
            

        }
        
        
        private void Browse_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        

        
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            selectedItemId = Convert.ToInt32(row.Cells["ItemId"].Value);
            selectedItemName = row.Cells["ItemName"].Value?.ToString() ?? "";
            selectedPrice = Convert.ToInt32(row.Cells["ItemPrice"].Value);
            label2.Text = $"Selected Item : {selectedItemName}";
            label3.Text = $"Price : {selectedPrice}";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            Program.PreviousForm = this;
            dataGridView1.CurrentCell = null;
            numericUpDown1.Value = 1;
            qty = 1;
            selectedItemId = -1;
            label2.Text = $"Selected Item : ";
            label3.Text = $"Price : ";
            Cart cart = new Cart(_username);
            this.Hide();
            cart.Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UserHome home = new UserHome(_username);
            home.Show();
            this.Hide();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedItemId == -1)
            {

                MessageBox.Show("Please select an item first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            qty = (int)numericUpDown1.Value;
            int totalPrice = selectedPrice * qty;
            DialogResult result = MessageBox.Show($" Item Name : '{selectedItemName}' \n Quantity : {qty} \n total : {totalPrice} Taka to your cart?", "Add to Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using(SqlConnection con = new SqlConnection(_cs))
                {
                    con.Open();
                    using (SqlTransaction tran = con.BeginTransaction())
                    {
                        using (SqlCommand updateCmd = new SqlCommand(
                            "UPDATE tblCart SET ItemQuantity = ItemQuantity + @qty, Total = Total + @total " +
                            "WHERE CusUsername = @user AND ItemId = @itemId", con, tran))
                        {
                            updateCmd.Parameters.AddWithValue("@qty", qty);
                            updateCmd.Parameters.AddWithValue("@total", totalPrice);
                            updateCmd.Parameters.AddWithValue("@user", _username);
                            updateCmd.Parameters.AddWithValue("@itemId", selectedItemId);

                            int affected = updateCmd.ExecuteNonQuery();

                            if (affected == 0)
                            {
                                using (SqlCommand insertCmd = new SqlCommand(
                                    "INSERT INTO tblCart (CusUsername, ItemId, ItemName, ItemPrice, ItemQuantity, Total) " +
                                    "VALUES (@user, @itemId, @name, @price, @qty, @total)", con, tran))
                                {
                                    insertCmd.Parameters.AddWithValue("@user", _username);
                                    insertCmd.Parameters.AddWithValue("@itemId", selectedItemId);
                                    insertCmd.Parameters.AddWithValue("@name", selectedItemName);
                                    insertCmd.Parameters.AddWithValue("@price", selectedPrice);
                                    insertCmd.Parameters.AddWithValue("@qty", qty);
                                    insertCmd.Parameters.AddWithValue("@total", totalPrice);
                                    insertCmd.ExecuteNonQuery();
                                    MessageBox.Show($"Added {qty} of '{selectedItemName}' of total {totalPrice} Taka to your cart.", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
}
                        }
                        tran.Commit();
                        dataGridView1.CurrentCell = null; 
                        numericUpDown1.Value = 1;
                        qty = 1;
                        selectedItemId = -1;
                        label2.Text = $"Selected Item : ";
                        label3.Text = $"Price : ";


                    }
                }

            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
