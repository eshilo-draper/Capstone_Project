using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;
using System.Net.Mail; // used for email validation

namespace Capstone_Project
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void register_Click(object sender, EventArgs e)
        {
            // ensure all fields have been filled out
            if(txt_username.Text == "" || txt_password.Text == "" || txt_confirmPassword.Text == "" || txt_displayName.Text == "" || txt_email.Text == "" || dtp_dob.Text == "")
            {
                lblError.Text = "ERROR: all fields required <br />";
            }
            else
            {
                // otherwise, safe to proceed

                string errors = "";

                // check if username contains space
                Boolean unHasSpace = false;
                foreach(char c in txt_username.Text)
                {
                    if(c == ' ')
                    {
                        unHasSpace = true;
                    }
                }
                if (unHasSpace)
                {
                    errors += "ERROR: username cannot contain space <br />";
                }

                // check for password matching confirmation
                if (txt_password.Text != txt_confirmPassword.Text)
                {
                    errors += "ERROR: passwords need to match <br />";
                }

                // check for email validity
                try
                {
                    MailAddress email_test = new MailAddress(txt_email.Text);
                }
                catch (Exception err)
                {
                    errors += "ERROR: invalid e-mail address <br />";
                }

                // create Account object for final validation and submission
                Account temp = new Account(txt_username.Text, txt_password.Text, txt_displayName.Text, txt_email.Text, DateTime.Parse(dtp_dob.Text));

                // check if username is taken
                errors += temp.checkUsername();

                // if no errors, register. otherwise, show errors
                if (errors == "")
                {
                    errors = temp.register();
                    if (errors != "success")
                    {
                        lblError.Text = errors;
                    }
                    else
                    {
                        Session["userID"] = temp.login();
                        Response.Redirect("default"); // CHANGE THIS REDIRECT
                    }
                }
                else
                {
                    lblError.Text = errors;
                }
            }
        }
    }
}