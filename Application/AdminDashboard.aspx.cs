using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        Admin.AdminServiceSoapClient obj = new Admin.AdminServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStockItems();
                LoadSuppliers();
            }
        }

        private void LoadStockItems()
        {
            try
            {
                // Call the ViewItems method from AdminService to get the stock items
                DataTable stockItems = obj.ViewItems();

                // Bind the data to GridViewStock
                GridViewStock.DataSource = stockItems;
                GridViewStock.DataBind();
            }
            catch (Exception ex)
            {
                // Log the error (you can replace this with actual logging)
                Response.Write("<script>alert('Error loading stock items: " + ex.Message + "');</script>");
            }
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            // Reload stock items for partial refresh
            LoadStockItems();
        }

        protected void GridViewStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RequestQuotation")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split('|');
                string productId = commandArgs[0];
                string productName = commandArgs[1];

                // Set the product ID and name in the modal text boxes
                txtModalProductID.Text = productId;
                txtModalProductName.Text = productName;

                // Show the modal using JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", $"openQuotationModal('{productId}', '{productName}');", true);
            }
        }

        protected void btnCreateQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the input values from the modal text boxes
                int productId = int.Parse(txtModalProductID.Text);
                string productName = txtModalProductName.Text;
                int quantity = int.Parse(txtModalQuantity.Text);

                // Call the web service method to create the quotation
                bool quotationResult = obj.CreateQuotation(productId, quantity);

                if (quotationResult)
                {
                    // Call the web service method to update the product quantity
                    bool updateQuantityResult = obj.UpdateProductQuantity(productId, quantity);

                    if (updateQuantityResult)
                    {
                        // Display a success message
                        Response.Write("<script>alert('Quotation created successfully for Product: " + productName + ". Stock updated successfully.');</script>");

                        // Refresh the stock view to reflect the updated quantity
                        LoadStockItems();

                        // Optionally refresh the quotations view to show the new entry
                        LoadQuotations();
                    }
                    else
                    {
                        // Display an error if quantity couldn't be updated
                        Response.Write("<script>alert('Quotation created, but failed to update product quantity.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Failed to create quotation.');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., display an error message)
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void btnModalUpdateQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the input values from the modal text boxes
                int quotationId = int.Parse(txtModalUpdateQuotationID.Text);
                decimal newAmount = decimal.Parse(txtModalUpdateAmount.Text);

                // Call the web service method to update the quotation
                bool result = obj.UpdateQuotation(quotationId, newAmount);

                if (result)
                {
                    // Optionally, display a success message
                    Response.Write("<script>alert('Quotation updated successfully!');</script>");

                    // Refresh the quotations view to reflect the changes
                    LoadQuotations();
                }
                else
                {
                    Response.Write("<script>alert('Failed to update quotation. Quotation ID not found.');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., display an error message)
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }


        // Load quotations method
        private void LoadQuotations()
        {
            try
            {
                DataTable quotationsTable = obj.ViewQuotations();
                GridViewQuotations.DataSource = quotationsTable;
                GridViewQuotations.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error retrieving quotations: " + ex.Message + "');</script>");
            }
        }

        protected void GridViewQuotations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateQuotation")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split('|');
                string quotationId = commandArgs[0];
                string amount = commandArgs[1];

                // Set the quotation ID and amount in the modal text boxes
                txtModalUpdateQuotationID.Text = quotationId;
                txtModalUpdateAmount.Text = amount;

                // Show the modal using JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowUpdateModal", $"openUpdateQuotationModal('{quotationId}', '{amount}');", true);
            }
            else if (e.CommandName == "DeleteQuotation")
            {
                try
                {
                    int quotationId = int.Parse(e.CommandArgument.ToString());

                    // Call the web service method to delete the quotation
                    bool result = obj.DeleteQuotation(quotationId);

                    if (result)
                    {
                        // Display a success message using JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "DeleteSuccess", "alert('Quotation deleted successfully!');", true);

                        // Refresh the quotations view to reflect the changes
                        LoadQuotations();
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed to delete quotation. Quotation ID not found.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., display an error message)
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }

        // Load existing suppliers
        private void LoadSuppliers()
        {
            try
            {
                DataTable suppliersTable = obj.ViewSuppliers();
                GridViewSuppliers.DataSource = suppliersTable;
                GridViewSuppliers.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading suppliers: " + ex.Message + "');</script>");
            }
        }

        // Add new supplier
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtSupplierUsername.Text.Trim();
                string password = txtSupplierPassword.Text.Trim();
                string confirmPassword = txtSupplierConfirmPassword.Text.Trim();

                if (password != confirmPassword)
                {
                    Response.Write("<script>alert('Passwords do not match.');</script>");
                    return;
                }

                bool result = obj.AddSupplier(username, password);
                if (result)
                {
                    Response.Write("<script>alert('Supplier added successfully!');</script>");
                    LoadSuppliers(); // Refresh the supplier list
                }
                else
                {
                    Response.Write("<script>alert('Failed to add supplier.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        // Handle supplier delete command
        protected void GridViewSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSupplier")
            {
                try
                {
                    string username = e.CommandArgument.ToString();
                    bool result = obj.DeleteSupplier(username);

                    if (result)
                    {
                        Response.Write("<script>alert('Supplier deleted successfully!');</script>");
                        LoadSuppliers(); // Refresh the supplier list
                    }
                    else
                    {
                        Response.Write("<script>alert('Failed to delete supplier.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
