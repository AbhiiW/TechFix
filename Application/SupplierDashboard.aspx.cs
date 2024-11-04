using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class SupplierDashboard : System.Web.UI.Page
    {
        AddProduct.SupplierServiceSoapClient obj = new AddProduct.SupplierServiceSoapClient();
        Admin.AdminServiceSoapClient objj = new Admin.AdminServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["SupplierUsername"] == null)
            {
                
                Response.Redirect("LoginForm.aspx");
            }
            else
            {
                
                System.Diagnostics.Debug.WriteLine("User is logged in with username: " + Session["SupplierUsername"]);
            }

            if (!IsPostBack)
            {
              
            }
        }


        protected void btnRefreshStock_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of your web service
                AddProduct.SupplierServiceSoapClient obj = new AddProduct.SupplierServiceSoapClient();

                // Retrieve the supplier username from the session
                string supplierUsername = Session["SupplierUsername"].ToString();

                // Call the ViewItems method with the supplier username to get the updated product table
                DataTable productTable = obj.ViewItems(supplierUsername);

                // Bind the retrieved data to the GridView
                GridViewStock.DataSource = productTable;
                GridViewStock.DataBind();
            }
            catch (Exception ex)
            {
                // Optionally handle the exception (e.g., display an error message)
                Response.Write("<script>alert('Error retrieving stock: " + ex.Message + "');</script>");
            }
        }


        protected void btnInsertProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from the textboxes
                string productName = txtProductName.Text;
                string description = txtDescription.Text;

                // Parse price and quantity to the appropriate types
                decimal price = decimal.Parse(txtPrice.Text);  // Convert price from string to decimal
                int quantity = int.Parse(txtQuantity.Text);    // Convert quantity from string to int

                // Retrieve the supplier username from the session
                string supplierUsername = Session["SupplierUsername"].ToString();

                // Call the web method to insert the product
                AddProduct.SupplierServiceSoapClient obj = new AddProduct.SupplierServiceSoapClient();
                string result = obj.InsertProduct(productName, description, price, quantity, supplierUsername);

                // Display the result (success/failure message)
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{result}');", true);

                // Optionally, clear the textboxes after successful insertion
                if (result == "Product added successfully.")
                {
                    txtProductName.Text = "";
                    txtDescription.Text = "";
                    txtPrice.Text = "";
                    txtQuantity.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
            }
        }




        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the ProductID from the text box
                int productId = int.Parse(txtDeleteProductId.Text);

                // Call the web method to delete the product
                AddProduct.SupplierServiceSoapClient obj = new AddProduct.SupplierServiceSoapClient();
                string result = obj.DeleteProduct(productId);

                // Display the result to the user
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{result}');", true);

                // Optionally, clear the text box after successful deletion
                if (result == "Product deleted successfully.")
                {
                    txtDeleteProductId.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
            }

        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the values from the textboxes
                int productId = int.Parse(txtUpdateProductId.Text);  // Get the product ID
                string productName = txtUpdateProductName.Text;      // New product name
                string description = txtUpdateDescription.Text;      // New description

                // Parse price and quantity from the textboxes
                decimal price = decimal.Parse(txtUpdatePrice.Text);  // New price
                int quantity = int.Parse(txtUpdateQuantity.Text);    // New quantity

                // Call the web method to update the product
                AddProduct.SupplierServiceSoapClient obj = new AddProduct.SupplierServiceSoapClient();
                string result = obj.UpdateProduct(productId, productName, description, price, quantity);

                // Display the result (success/failure message)
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{result}');", true);

                // Optionally, clear the textboxes after successful update
                if (result == "Product updated successfully.")
                {
                    txtUpdateProductId.Text = "";
                    txtUpdateProductName.Text = "";
                    txtUpdateDescription.Text = "";
                    txtUpdatePrice.Text = "";
                    txtUpdateQuantity.Text = "";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void btnRefreshQuotations_Click(object sender, EventArgs e)
        {
            try
            {
                // Call the web service method to get the quotations
                DataTable quotationsTable = objj.ViewQuotations();

                // Bind the retrieved data to the GridView
                GridViewQuotations.DataSource = quotationsTable;
                GridViewQuotations.DataBind();
            }
            catch (Exception ex)
            {
                // Optionally handle the exception (e.g., display an error message)
                Response.Write("<script>alert('Error retrieving quotations: " + ex.Message + "');</script>");
            }
        }

        protected void btnRefreshOrders_Click(object sender, EventArgs e)
        {

        }
    }
}