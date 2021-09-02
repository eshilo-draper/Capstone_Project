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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userID"] != null)
            {
                // if a user is logged in, hides login and register buttons, show icon, name, account settings, and logout
                loggedOut.Attributes.Add("class", "hidden");
                loggedIn.Attributes.Remove("class");

                // retrieve username and avatar based on user ID and display on label
                Account temp = new Account();
                avatar.ImageUrl = temp.getAvatarByID(int.Parse(Session["userID"].ToString()));
                loggedIn_un.Text = temp.getDisplayNameByID(int.Parse(Session["userID"].ToString()));
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("login");
        }

        protected void btnAccountSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("editaccount");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // store current location in case of cancellation, then redirect to logout confirmation page
            Session["prevURL"] = HttpContext.Current.Request.Url.AbsolutePath;
            Response.Redirect("logout");
        }
    }
}