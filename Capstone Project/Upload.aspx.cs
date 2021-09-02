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
        protected void Page_Load(object sender, EventArgs e)
        {
            // hide label used to store id of image being edited
            lblImageID.Visible = false;

            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }

            // if editing a post, hide the upload button and preload post data
            if (Request.QueryString["editimage"] != null)
            {
                btnUpload.Visible = false;
                tempLabel.Visible = false;

                Account temp = new Account();
                string[] imageData = temp.getUploadData(int.Parse(Request.QueryString["editimage"]), int.Parse(Session["userID"].ToString()));

                lblImageID.Text = imageData[0]; // store image ID to label for later use
                imagePreview.ImageUrl = "Uploads/" + imageData[2]; // load existing image into preview

                // prefill form
                txt_Title.Text = imageData[3];
                txt_Medium.Text = imageData[4];
                dtp_completionDate.Text = DateTime.Parse(imageData[5]).ToString("yyyy-MM-dd");
                txt_Description.Text = imageData[6];
            }
            else
            {
                // otherwise, hide save changes button
                btnSaveChanges.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Account uploader = new Account();

            string uploadPath = uploader.getUsernameByID(int.Parse(Session["userID"].ToString())) + "_" + imageUpload.FileName;

            imageUpload.SaveAs(Server.MapPath("Uploads/" + uploadPath));

            uploader.uploadImageData(int.Parse(Session["userID"].ToString()), uploadPath, txt_Title.Text, txt_Medium.Text, DateTime.Parse(dtp_completionDate.Text), txt_Description.Text);
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            Account uploader = new Account();

            string uploadPath = imagePreview.ImageUrl.Substring(8, imagePreview.ImageUrl.Length - 8);

            if (imageUpload.HasFile)
            {
                uploadPath = uploader.getUsernameByID(int.Parse(Session["userID"].ToString())) + "_" + imageUpload.FileName;

                imageUpload.SaveAs(Server.MapPath("Uploads/" + uploadPath));
            }

            uploader.updateImageData(int.Parse(lblImageID.Text), uploadPath, txt_Title.Text, txt_Medium.Text, DateTime.Parse(dtp_completionDate.Text), txt_Description.Text);
        }
    }
}