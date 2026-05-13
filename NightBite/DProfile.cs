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

namespace NightBite
{
    public partial class DProfile : Form
    {
        private string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True";
        private int did;
        public DProfile(int did)
        {
            InitializeComponent();
            this.did = did;
            LoadDeliveryManInfo(did);

        }
        private void LoadDeliveryManInfo(int did)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                string query = "SELECT DName, DPhone ,DAddress FROM tblDeliveryMan WHERE DId = @dname";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dname", did);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox2.Text = did.ToString();
                    txtUsername.Text = reader["DName"].ToString();
                    txtPhonenumber.Text = reader["DPhone"].ToString();
                    textBox1.Text = reader["DAddress"].ToString();
                }
                else
                {
                    MessageBox.Show("No deliveryman found with that name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                con.Close();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage(did);
            this.Hide();
            homePage.Show();
        }

        private void DProfile_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPhonenumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
