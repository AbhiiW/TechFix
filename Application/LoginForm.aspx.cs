using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class LoginForm : System.Web.UI.Page
    {
        UserValidation.UserValidationSoapClient obj = new UserValidation.UserValidationSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var result = obj.loginUser(txtUsername.Text, txtPassword.Text);
            if (result.IsValid)  // Check if the login is valid
            {
                if (result.UserType == "Admin")
                {
                    Response.Redirect("AdminDashboard.aspx");  // Redirect to Admin Dashboard
                }
                else if (result.UserType == "Supplier")
                {
                    // Store the supplier username in session
                    Session["SupplierUsername"] = txtUsername.Text;

                    // Add a console log to check if the session variable is set correctly
                    System.Diagnostics.Debug.WriteLine("Supplier Username stored in session: " + Session["SupplierUsername"]);

                    // Show alert message before redirection
                    string script = "alert('Login successful'); window.location='SupplierDashboard.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "LoginAlert", script, true);
                }
                else
                {
                    lblMsg.Text = "Unauthorized user type.";  // Handle unexpected user types
                }
            }
            else
            {
                lblMsg.Text = "Invalid Username or Password";  // Show error message
            }
        }

    }
}