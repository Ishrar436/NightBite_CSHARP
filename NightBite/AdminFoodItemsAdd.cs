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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace NightBite
{
    public partial class AdminFoodItemsAdd : Form
    {
        public string AdminName;
        public int itemprice;
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";

        public AdminFoodItemsAdd(string AdminName)
        {
            InitializeComponent();
            this.AdminName = AdminName;
            LoadData();
            StyleDataGridView();
           /* string imagePath = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 01_22_58 PM.png";
            string imagePath2 = @"C:\Users\abrar\OneDrive\Desktop\Lectures\C# (OOP2)\NightBite\NightBite\NightBite\NightBite\pics\ChatGPT Image Jul 17, 2025, 02_17_00 PM.png";
            
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
            AdminFoodItems adminfood = new AdminFoodItems(AdminName);
            adminfood.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string itemCategory = textBox4.Text.Trim();
            string productAvailable = comboBox1.SelectedItem?.ToString();
            string itemname = textBox1.Text.Trim();
            string temprice = textBox2.Text.Trim();

            if (!int.TryParse(textBox2.Text.Trim(), out itemprice))
            {
                MessageBox.Show("Please Enter valid digit for price Of Product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();

            }
            else if(Convert.ToInt32( itemprice ) <= 0)
            {
                MessageBox.Show("Please Enter valid price Of Product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(itemname) || string.IsNullOrWhiteSpace(productAvailable) || string.IsNullOrWhiteSpace(itemCategory))
            {
                MessageBox.Show("Please Enter All Information Of Product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                 itemprice = Convert.ToInt32(temprice);
                SqlConnection con = new SqlConnection(connectionString);
                 con.Open();
                string checkQuery = "SELECT COUNT(*) FROM tblItem WHERE ItemName = @itemName AND ItemCategory = @itemCategory AND ItemPrice = @itemPrice";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@itemName", itemname);
                    checkCmd.Parameters.AddWithValue("@itemCategory", itemCategory);
                    checkCmd.Parameters.AddWithValue("@itemPrice", itemprice);

                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("Duplicate item found! Item Already in the List.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        comboBox1.SelectedIndex = -1;
                        textBox1.Focus();


                        
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show($"Do you want to add {itemname} , Price : {itemprice} Taka ?", "Add Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            string insertQuery = "INSERT INTO tblItem (ItemName, ItemCategory, ItemAv, ItemPrice) VALUES (@itemName, @itemCategory, @itemAv, @itemPrice)";
                            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                            insertCmd.Parameters.AddWithValue("@itemName", itemname);
                            insertCmd.Parameters.AddWithValue("@itemCategory", itemCategory);
                            insertCmd.Parameters.AddWithValue("@itemAv", productAvailable);
                            insertCmd.Parameters.AddWithValue("@itemPrice", itemprice);

                            insertCmd.ExecuteNonQuery();

                            MessageBox.Show($"{itemname} added to Product List for {itemprice} Taka", "Success ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadData();
                            textBox1.Text =  "";
                            textBox2.Text = "";
                            comboBox1.SelectedIndex = -1;
                            textBox4.Text = "";
                            itemname = null;
                            

                           
                        }

                    }
                    con.Close();

                }

            }
           

            
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AdminFoodItemsAdd_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
