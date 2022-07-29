using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;
using System.Data;
//using System.Data.SqlClient;
using MySqlConnector;
using System.Net.Mail;
using System.Drawing; // used to verify avatar dimensions
using System.IO; // used to get file extension of upload

namespace Capstone_Project
{
    public partial class EditAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }

            if (!Page.IsPostBack)
            {
                Account temp = new Account();
                MySqlDataReader accountInfo = temp.getAccountInfo(int.Parse(Session["userID"].ToString()));

                while (accountInfo.Read())
                {
                    txt_username.Text = accountInfo["username"].ToString();
                    txt_displayName.Text = accountInfo["displayName"].ToString();
                    txt_email.Text = accountInfo["email"].ToString();
                    txt_bio.Text = accountInfo["bio"].ToString();
                    avatarPreview.ImageUrl = accountInfo["icon"].ToString();
                }
            }
        }

        protected void btnSubmitEdit_Click(object sender, EventArgs e)
        {
            string status = "";

            // ensure all required fields have been filled out
            if (txt_username.Text == "" || txt_displayName.Text == "" || txt_email.Text == "")
            {
                status += "ERROR: username, display name and email required <br />";
            }

            if(txt_username.Text.Length > 20)
            {
                status += "ERROR: username exceeds maximum length of 20 characters <br />";
            }

            if(txt_displayName.Text.Length > 50)
            {
                status += "ERROR: display name exceeds maximum length of 50 characters <br />";
            }

            if(txt_email.Text.Length > 320)
            {
                status += "ERROR: email address exceeds maximum length of 320 characters <br />";
            }

            if (status.Contains("ERROR"))
            {
                lblEditError.Text = status;
            }
            else
            {
                // otherwise, safe to proceed

                string errors = "";

                // check if username contains space
                Boolean unHasSpace = false;
                foreach (char c in txt_username.Text)
                {
                    if (c == ' ')
                    {
                        unHasSpace = true;
                    }
                }
                if (unHasSpace)
                {
                    errors += "ERROR: username cannot contain space <br />";
                }

                // check for email validity
                try
                {
                    MailAddress email_test = new MailAddress(txt_email.Text);
                }
                catch
                {
                    errors += "ERROR: invalid e-mail address <br />";
                }

                // create avatar string
                string avatarString = avatarPreview.ImageUrl;
                if (btnUploadAvatar.HasFile)
                {
                    Bitmap bmpAvatar = new Bitmap(btnUploadAvatar.FileContent);
                    avatarString = "Avatars/" + txt_username.Text + "_avatar" + Path.GetExtension(btnUploadAvatar.FileName);
                    // check if avatar being uploaded has a 1:1 aspect ratio
                    if (bmpAvatar.Width != bmpAvatar.Height)
                    {
                        errors += "ERROR: avatar must be square.";
                    }

                    if(avatarString.Length > 100)
                    {
                        errors += "ERROR: avatar has excessive filename length";
                    }
                }

                // create Account object for final validation and submission
                Account temp = new Account(int.Parse(Session["userID"].ToString()), txt_username.Text, txt_displayName.Text, txt_email.Text, avatarString, txt_bio.Text);

                // check if username is taken
                errors += temp.checkUsername(int.Parse(Session["userID"].ToString()));

                // if no errors, upload icon and register. otherwise, show errors
                if (errors == "")
                {
                    errors = temp.updateAccountInfo();
                    if (errors != "success")
                    {
                        lblEditError.Text = errors;
                    }
                    else
                    {
                        if (btnUploadAvatar.HasFile)
                        {
                            btnUploadAvatar.SaveAs(Server.MapPath(avatarString));
                        }
                        Response.Redirect("dashboard");
                    }
                }
                else
                {
                    lblEditError.Text = errors;
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("changepassword");
        }
    }
}