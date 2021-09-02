using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;

namespace Capstone_Project
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }
        }

        protected void btnChangePW_Click(object sender, EventArgs e)
        {
            string errors = "";

            if(txtOldPW.Text == "" || txtNewPW.Text == "" || txtConfirmNewPW.Text == "")
            {
                errors += "ERROR: all fields required </br>";
            }

            if(txtNewPW.Text != txtConfirmNewPW.Text)
            {
                errors += "ERROR: new password fields must match </br>";
            }

            if (errors.Contains("ERROR"))
            {
                lblError.Text = errors;
            }
            else
            {
                Account temp = new Account();
                // immediately reconstruct with password and retrieved username
                temp = new Account(temp.getUsernameByID(int.Parse(Session["userID"].ToString())), txtOldPW.Text);

                // check old password by attempting login
                string loginStatus = temp.login();

                // if old password is correct
                if(loginStatus == Session["userID"].ToString())
                {
                    temp.updatePassword(txtNewPW.Text);
                    Response.Redirect("editaccount");
                }
                else
                {
                    lblError.Text = "ERROR: incorrect old password";
                }
            }
        }
    }
}