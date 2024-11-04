using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Techfix
{
    /// <summary>
    /// Summary description for SupplierService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SupplierService : System.Web.Services.WebService
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



        //View Items
        [WebMethod]
        public DataTable ViewItems(string supplierUsername)
        {
            DataTable productTable = new DataTable("ProductTable");

            try
            {
                GetConnection();

                
                using (SqlCommand cmd = new SqlCommand("SELECT ProductId, ProductName, Description, Price, Quantity FROM Products WHERE Supplier = @Supplier", SQLCon))
                {
                    cmd.Parameters.AddWithValue("@Supplier", supplierUsername);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(productTable);
                }

                return productTable;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error retrieving products: " + ex.Message);
            }
            finally
            {
                SQLCon.Close();
            }
        }


        //Add Items

        [WebMethod]
        public string InsertProduct(string productName, string description, decimal price, int quantity, string supplier)
        {
            try
            {
                
                GetConnection();

                
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Products (ProductName, Description, Price, Quantity, Supplier) VALUES (@ProductName, @Description, @Price, @Quantity, @Supplier)", SQLCon))
                {
                    
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Supplier", supplier);

                  
                    int rowsAffected = cmd.ExecuteNonQuery();

                    
                    if (rowsAffected > 0)
                    {
                        return "Product added successfully.";
                    }
                    else
                    {
                        return "Failed to add product.";
                    }
                }
            }
            catch (Exception ex)
            {
                
                return "Error: " + ex.Message;
            }
            finally
            {
                
                SQLCon.Close();
            }
        }




        //Delete Items
        [WebMethod]
        public string DeleteProduct(int productId)
        {
            try
            {
                
                GetConnection();

                
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", SQLCon))
                {
                    
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                   
                    if (rowsAffected > 0)
                    {
                        return "Product deleted successfully.";
                    }
                    else
                    {
                        return "Product not found or unable to delete.";
                    }
                }
            }
            catch (Exception ex)
            {
                
                return "Error: " + ex.Message;
            }
            finally
            {
                
                SQLCon.Close();
            }
        }

        //Update Product

        [WebMethod]
        public string UpdateProduct(int productId, string productName, string description, decimal price, int quantity)
        {
            try
            {
                
                GetConnection();

                
                using (SqlCommand cmd = new SqlCommand("UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price, Quantity = @Quantity WHERE ProductID = @ProductID", SQLCon))
                {
                    
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                  
                    int rowsAffected = cmd.ExecuteNonQuery();

                   
                    if (rowsAffected > 0)
                    {
                        return "Product updated successfully.";
                    }
                    else
                    {
                        return "Product not found or unable to update.";
                    }
                }
            }
            catch (Exception ex)
            {
                
                return "Error: " + ex.Message;
            }
            finally
            {
                
                SQLCon.Close();
            }
        }







    }
}
