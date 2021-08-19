using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Capstone_Project
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }
        }

        protected void btnLogoutConfirm_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("default");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session["prevURL"].ToString());
        }
    }
}