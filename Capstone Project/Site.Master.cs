using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;

namespace Capstone_Project
{
    public partial class SiteMaster : MasterPage
    {
        private String connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userID"] != null)
            {
                // if a user is logged in, hides login and register buttons, show icon, name, account settings, and logout
                loggedOut.Attributes.Add("class", "hidden");
                loggedIn.Attributes.Remove("class");

                // retrieve username based on user ID and display on label
                Account temp = new Account();
                loggedIn_un.Text = "Logged in as " + temp.GetUsernameByID(int.Parse(Session["userID"].ToString()));
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("login");
        }

        protected void btnAccountSettings_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}