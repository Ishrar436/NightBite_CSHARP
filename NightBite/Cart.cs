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

namespace NightBite
{
    public partial class Cart : Form
    {
        public string username;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private int selectedItemId = -1;
        private string selectedItemName = "";
        private int selectedPrice = 0;
        private int qty = 1;
        private int total;
        private int itemTotal;
        public Cart(string username)
        {
            InitializeComponent();
            this.username = username;
            LoadCartItems();
            SetupControls();
            this.FormClosing += UserForm_FormClosing;


        }
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            ClearCart.ClearCartForUser(username);

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

            
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 10;
            numericUpDown1.Value = 1;

            
            //button3.Text = "Add To Cart";



            
            //button1.Text = "◀ Back";



            
            //button2.Text = "🛒 Cart";

        }
        private void LoadCartItems()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string query = "SELECT ItemId,ItemName, ItemPrice, ItemQuantity, Total FROM tblCart WHERE CusUsername = @username";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            object sumObj = dt.Compute("SUM(Total)", "");
            total = (sumObj == DBNull.Value) ? 0 : Convert.ToInt32(sumObj);
            label5.Text = $"Total : {total}";
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["ItemId"].Visible = false;
            dataGridView1.Columns["ItemName"].HeaderText = "Item";
            dataGridView1.Columns["ItemPrice"].HeaderText = "Unit Price";
            dataGridView1.Columns["ItemQuantity"].HeaderText = "Quantity";
            dataGridView1.Columns["Total"].HeaderText = "Total";

            dataGridView1.Columns["ItemPrice"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C0";

            reader.Close();
            con.Close();
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            selectedItemId = Convert.ToInt32(row.Cells["ItemId"].Value);
            selectedItemName = row.Cells["ItemName"].Value?.ToString() ?? "";
            selectedPrice = Convert.ToInt32(row.Cells["ItemPrice"].Value);
            qty = Convert.ToInt32(row.Cells["ItemQuantity"].Value);
            label1.Text = $"Selected Item : {selectedItemName}";
            label2.Text = $"Price : {selectedPrice}";
           
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private void Cart_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //UserHome userHome  = new UserHome(username);
            //this.Hide();
            //userHome.Show();
            Program.PreviousForm?.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (total > 100)
            {
                if(radioButton1.Checked )
                {
                    DialogResult result2 = MessageBox.Show($"Confirm Order? ?\nTotal : {total}?\nCash On Delivery", "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        ConfirmOrder.AddOrdrr(username, "Cash On Delivery", total);
                        MessageBox.Show($"Order Had Ben Placed?\nThank You {username}For your Purchase ", "Thank You", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserHome userhome = new UserHome(username);
                        this.Hide();
                        userhome.Show();

                    }
                }
                else if (radioButton2.Checked)
                {
                    OnlinePayment onlinePayment = new OnlinePayment(username, total);
                    this.Hide();
                    onlinePayment.Show();
                }
                else if(!radioButton1.Checked || !radioButton2.Checked)
                {
                    MessageBox.Show("Please select an option first.", "Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                MessageBox.Show("Please add 100 0r more to cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear your cart?", "Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand clear = new SqlCommand("DELETE FROM tblCart WHERE CusUsername = @username", con);
                clear.Parameters.AddWithValue("@username", username);
                clear.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Cart cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCartItems();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedItemId == -1)
            {
                MessageBox.Show("Please select an item from table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            } 
            else
            {
                DialogResult result = MessageBox.Show($"Delete {qty} {selectedItemName}  from cart?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand delete = new SqlCommand(
                        "DELETE FROM tblCart WHERE CusUsername = @username AND ItemName = @itemname", con);
                    delete.Parameters.AddWithValue("@username", username);
                    delete.Parameters.AddWithValue("@itemname", selectedItemName);
                    delete.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Item deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label1.Text = $"Selected Item : ";
                    label2.Text = $"Price : ";
                    dataGridView1.CurrentCell = null;
                    LoadCartItems();
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int newQty = (int)numericUpDown1.Value;
            if (selectedItemId == -1)
            {
                MessageBox.Show("Please select an item from table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (newQty != qty)
                {
                    int newTotal = newQty * selectedPrice;
                    string msg = $"Change quantity of {selectedItemName} to {newQty}?\nNew total: {newTotal} Taka";
                    DialogResult result = MessageBox.Show(msg, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SqlConnection con = new SqlConnection(connectionString);
                        con.Open();
                        SqlCommand update = new SqlCommand(
                            "UPDATE tblCart SET ItemQuantity = @qty, Total = @total WHERE CusUsername = @username AND ItemName = @itemname", con);
                        update.Parameters.AddWithValue("@qty", newQty);
                        update.Parameters.AddWithValue("@total", newTotal);
                        update.Parameters.AddWithValue("@username", username);
                        update.Parameters.AddWithValue("@itemname", selectedItemName);
                        update.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Quantity updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.CurrentCell = null;
                        numericUpDown1.Value = 1;
                        qty = 1;
                        selectedItemId = -1;
                        label1.Text = $"Selected Item : ";
                        label2.Text = $"Price : ";
                        LoadCartItems();
                    }
                }
                else
                {
                    MessageBox.Show("Please Change the Quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
