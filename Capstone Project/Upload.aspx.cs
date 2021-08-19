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
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Account uploader = new Account();

            string uploadPath = uploader.getUsernameByID(int.Parse(Session["userID"].ToString())) + "_" + imageUpload.FileName;

            imageUpload.SaveAs(Server.MapPath("Uploads/" + uploadPath));

            uploader.uploadImageData(int.Parse(Session["userID"].ToString()), uploadPath, txt_Title.Text, txt_Medium.Text, DateTime.Parse(dtp_completionDate.Text), txt_Description.Text);
        }
    }
}