using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;

namespace Capstone_Project
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            String UN = username.Text;
            String PW = password.Text;

            Account temp = new Account(username.Text, password.Text);

            string status = temp.login();

            int id;
            if(int.TryParse(status, out id))
            {
                Session["userID"] = id;
                Response.Redirect("dashboard");
            }
            else
            {
                errorLabel.Text = status;
            }
        }
    }
}