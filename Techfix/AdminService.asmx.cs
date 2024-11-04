using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Techfix
{
    /// <summary>
    /// Summary description for AdminService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminService : System.Web.Services.WebService
    {
        SqlConnection SQLCon;

        public SqlConnection GetConnection()
        {
            try
            {
                SQLCon = new SqlConnection("data source=Abhi\\SQLEXPRESS;initial catalog=TechFix;Integrated Security=True");
                SQLCon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Connecting to DB:" + ex);
            }
            return SQLCon;
        }

        //View Item
        [WebMethod]
        public DataTable ViewItems()
        {
            DataTable productTable = new DataTable("ProductTable");

            try
            {
                GetConnection();

                // Fetch all products without filtering by supplier
                using (SqlCommand cmd = new SqlCommand("SELECT ProductId, ProductName, Description, Price, Quantity, Supplier FROM Products", SQLCon))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(productTable);
                }

                // Check if the datatable has rows to ensure it fetched correctly
                if (productTable.Rows.Count == 0)
                {
                    throw new Exception("No products retrieved from the database.");
                }

                return productTable;
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                throw new Exception("Error retrieving products: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }


        //Create Quotation
        [WebMethod]
        public bool CreateQuotation(int productId, decimal amount)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Quotations (ProductID, Amount, CreatedAt) VALUES (@ProductID, @Amount, @CreatedAt)", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                return true; 
            }
            catch (Exception ex)
            {
                // Log the error (implement logging as needed)
                throw new Exception("Error creating quotation: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }


        //view Quotations

        [WebMethod]
        public DataTable ViewQuotations()
        {
            DataTable quotationsTable = new DataTable("QuotationsTable");

            try
            {
                GetConnection();

                // Update the SQL query to retrieve quotations
                using (SqlCommand cmd = new SqlCommand("SELECT QuotationID, ProductID, Amount, CreatedAt FROM Quotations", SQLCon))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(quotationsTable);
                }

                return quotationsTable;
            }
            catch (Exception ex)
            {
                // Log the error (implement logging as needed)
                throw new Exception("Error retrieving quotations: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }


        //Update Quotation

        [WebMethod]
        public bool UpdateQuotation(int quotationId, decimal newAmount)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("UPDATE Quotations SET Amount = @NewAmount WHERE QuotationID = @QuotationID", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@NewAmount", newAmount);
                    cmd.Parameters.AddWithValue("@QuotationID", quotationId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if at least one row was updated
                }
            }
            catch (Exception ex)
            {
                // Log the error (implement logging as needed)
                throw new Exception("Error updating quotation: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }

        //Delete Quotation

        [WebMethod]
        public bool DeleteQuotation(int quotationId)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Quotations WHERE QuotationID = @QuotationID", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@QuotationID", quotationId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error deleting quotation: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }

        //Add Suplier
        [WebMethod]
        public bool AddSupplier(string username, string password)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, UserType) VALUES (@Username, @Password, @UserType)", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); 
                    cmd.Parameters.AddWithValue("@UserType", "Supplier");

                    cmd.ExecuteNonQuery();
                }

                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding supplier: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }

        //View Supplier
        [WebMethod]
        public DataTable ViewSuppliers()
        {
            DataTable suppliersTable = new DataTable("SuppliersTable");

            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT Username FROM Users WHERE UserType = 'Supplier'", SQLCon))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(suppliersTable);
                }

                return suppliersTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving suppliers: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }

        //Delete Supplier
        [WebMethod]
        public bool DeleteSupplier(string username)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Username = @Username AND UserType = 'Supplier'", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting supplier: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }

        //update inventory qty
        [WebMethod]
        public bool UpdateProductQuantity(int productId, int quantityToReduce)
        {
            try
            {
                GetConnection();

                using (SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity = Quantity - @QuantityToReduce WHERE ProductID = @ProductID AND Quantity >= @QuantityToReduce", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@QuantityToReduce", quantityToReduce);
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error updating product quantity: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }












    }
}
