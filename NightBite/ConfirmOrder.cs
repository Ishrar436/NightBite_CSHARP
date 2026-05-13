using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightBite
{
    internal static class ConfirmOrder
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=NiggasDen;Integrated Security=True;MultipleActiveResultSets=true";

        public static void AddOrdrr(string username,string paymentmethod,int total)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Step 0: Get address and phone from tblCustomer
                string getCustomerQuery = @"
        SELECT CusAddress, CusPhone
        FROM tblCustomer
        WHERE CusUsername = @username";

                SqlCommand cmdCustomer = new SqlCommand(getCustomerQuery, con);
                cmdCustomer.Parameters.AddWithValue("@username", username);

                string address = "";
                string phone = "";

                using (SqlDataReader reader = cmdCustomer.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = reader["CusAddress"].ToString();
                        phone = reader["CusPhone"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Customer info not found.");
                        return;
                    }
                }

                // Step 1: Begin transaction
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Step 2: Insert into tblOrder
                    string insertOrderQuery = @"
            INSERT INTO tblOrder (CusUsername, CusAddress, CusPhone, OrderAmount, OrderStatus, PaymentMethod)
            VALUES (@username, @address, @phone, @amount, @status, @payment);
            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdOrder = new SqlCommand(insertOrderQuery, con, transaction);
                    cmdOrder.Parameters.AddWithValue("@username", username);
                    cmdOrder.Parameters.AddWithValue("@address", address);
                    cmdOrder.Parameters.AddWithValue("@phone", phone);
                    cmdOrder.Parameters.AddWithValue("@amount", total);  // You'll need to calculate this from cart
                    cmdOrder.Parameters.AddWithValue("@status", "Ordered");
                    cmdOrder.Parameters.AddWithValue("@payment", paymentmethod);

                    int newOrderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

                    // Step 3: Get cart items for current user
                    string getCartQuery = "SELECT ItemId, ItemPrice, ItemQuantity FROM tblCart WHERE CusUsername = @username";
                    SqlCommand cmdCart = new SqlCommand(getCartQuery, con, transaction);
                    cmdCart.Parameters.AddWithValue("@username", username);

                    SqlDataReader cartReader = cmdCart.ExecuteReader();

                    List<(string ItemId, decimal Price, int Quantity)> cartItems = new List<(string, decimal, int)>();

                    while (cartReader.Read())
                    {
                        cartItems.Add((
                            cartReader["ItemId"].ToString(),
                            Convert.ToDecimal(cartReader["ItemPrice"]),
                            Convert.ToInt32(cartReader["ItemQuantity"])
                        ));
                    }
                    cartReader.Close();

                    // Step 4: Insert into tblOrderDetails
                    foreach (var item in cartItems)
                    {
                        string insertDetailQuery = @"
                INSERT INTO tblOrderDetails (OrderId, ItemId, ItemPrice, ItemQuantity)
                VALUES (@orderId, @itemId, @price, @qty);";

                        SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, con, transaction);
                        cmdDetail.Parameters.AddWithValue("@orderId", newOrderId);
                        cmdDetail.Parameters.AddWithValue("@itemId", item.ItemId);
                        cmdDetail.Parameters.AddWithValue("@price", item.Price);
                        cmdDetail.Parameters.AddWithValue("@qty", item.Quantity);
                        cmdDetail.ExecuteNonQuery();
                    }

                    // Step 5: Optionally clear cart
                    string clearCartQuery = "DELETE FROM tblCart WHERE CusUsername = @username";
                    SqlCommand cmdClear = new SqlCommand(clearCartQuery, con, transaction);
                    cmdClear.Parameters.AddWithValue("@username", username );
                    cmdClear.ExecuteNonQuery();

                    // Commit all
                    transaction.Commit();
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Order failed: " + ex.Message);
                }
            }


        }

    }
}
