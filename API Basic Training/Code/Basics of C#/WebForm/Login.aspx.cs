using System;
using System.Web.UI;

namespace WebForm
{
    public partial class Login : System.Web.UI.Page
    {

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check the credentials
            if (username == "prince" && password == "prince123")
            {
                // Redirect to contact.aspx
                Response.Redirect("contact.aspx");
            }
            else
            {
                // Display an error message or handle authentication failure
                // For simplicity, let's show an alert for now
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid credentials');", true);
            }
        }
    }
}
