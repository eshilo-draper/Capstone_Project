using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;

namespace Capstone_Project
{
    public partial class Upload : System.Web.UI.Page
    {
        string currentImage; // used for retrieving previous uploads
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }

            // if editing a post, hide the upload button and preload post data
            if (Request.QueryString["editimage"] != null)
            {
                btnUpload.Visible = false;

                // retrieve name of image loaded
                Account temp = new Account();
                string[] imageData = temp.getUploadData(int.Parse(Request.QueryString["editimage"]), int.Parse(Session["userID"].ToString()));
                currentImage = imageData[2];
            }
            else
            {
                // otherwise, hide save changes button
                btnSaveChanges.Visible = false;
            }
        }

        public string[] getCurrentMetadata()
        {
            string[] imageData = new string[0];

            if(Request.QueryString["editimage"] != null)
            {
                Account temp = new Account();
                imageData = temp.getUploadData(int.Parse(Request.QueryString["editimage"]), int.Parse(Session["userID"].ToString()));

                imageData[5] = DateTime.Parse(imageData[5]).ToString("yyyy-MM-dd"); // correct formatting of completion date
            }

            return imageData;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string status = "";
            // ensure all fields with max length aren't too long or empty
            if(txt_Title.Text.Length > 100)
            {
                status += "ERROR: title exceeds length limit of 100 characters <br>";
            }
            if(txt_Title.Text.Length < 1)
            {
                status += "ERROR: title required <br>";
            }
            if(txt_Medium.Text.Length > 100)
            {
                status += "ERROR: medium exceeds length limit of 100 characters <br>";
            }
            if(txt_Medium.Text.Length < 1)
            {
                status += "ERROR: medium required <br>";
            }
            if (!imageUpload.HasFile)
            {
                status += "ERROR: no image uploaded";
            }

            Account uploader = new Account();
            string uploadPath = uploader.getUsernameByID(int.Parse(Session["userID"].ToString())) + "_" + imageUpload.FileName;

            if(uploadPath.Length > 100)
            {
                status += "ERROR: file name length exceeds limit.";
            }

            if (status.Contains("ERROR"))
            {
                lblUploadError.Text = status;
            }
            else
            {

                imageUpload.SaveAs(Server.MapPath("Uploads/" + uploadPath));

                uploader.uploadImageData(int.Parse(Session["userID"].ToString()), uploadPath, txt_Title.Text, txt_Medium.Text, DateTime.Parse(dtp_completionDate.Text), txt_Description.Text);

                Response.Redirect("dashboard");
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string status = "";
            // ensure all fields with max length aren't too long or empty
            if (txt_Title.Text.Length > 100)
            {
                status += "ERROR: title exceeds length limit of 100 characters <br>";
            }
            if (txt_Title.Text.Length < 1)
            {
                status += "ERROR: title required <br>";
            }
            if (txt_Medium.Text.Length > 100)
            {
                status += "ERROR: medium exceeds length limit of 100 characters <br>";
            }
            if (txt_Medium.Text.Length < 1)
            {
                status += "ERROR: medium required <br>";
            }
            Account uploader = new Account();

            string uploadPath = currentImage;

            if (imageUpload.HasFile)
            {
                uploadPath = uploader.getUsernameByID(int.Parse(Session["userID"].ToString())) + "_" + imageUpload.FileName;

                imageUpload.SaveAs(Server.MapPath("Uploads/" + uploadPath));
            }

            if (uploadPath.Length > 100)
            {
                status += "ERROR: file name length exceeds limit.";
            }

            if (status.Contains("ERROR"))
            {
                lblUploadError.Text = status;
            }
            else
            {
                uploader.updateImageData(int.Parse(Request.QueryString["editimage"]), uploadPath, txt_Title.Text, txt_Medium.Text, DateTime.Parse(dtp_completionDate.Text), txt_Description.Text);

                Response.Redirect("dashboard");
            }
        }
    }
}