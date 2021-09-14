using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;
using System.Data.SqlClient;

namespace Capstone_Project
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("default");
            }
        }

        protected void btnUpload_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Upload");
        }

        // COPIED FROM Gallery.aspx.cs, switched to using session instead of URL param (re-copy if changed in gallery)
        public string[,] getImages()
        {
            string[,] galleryList;

            // create a temp account object and use it to retrieve all images by the selected user (default sort by completionDate ascending)
            Account temp = new Account();
            SqlDataReader galleryReader = temp.getUploads(int.Parse(Session["userID"].ToString()), "ORDER BY CompletionDate DESC");

            // convert galleryReader to a list of lists of excessive length
            string[,] tempList = new string[10000, 6];

            int row = 0;

            while (galleryReader.Read())
            {
                tempList[row, 0] = galleryReader["imagePath"].ToString();
                tempList[row, 1] = galleryReader["title"].ToString();
                tempList[row, 2] = galleryReader["medium"].ToString();
                tempList[row, 3] = galleryReader["completionDate"].ToString();
                tempList[row, 4] = galleryReader["description"].ToString();
                tempList[row, 5] = galleryReader["imageID"].ToString();

                row++;
            }

            // use row count to generate list of minimum length
            galleryList = new string[row, 6];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    galleryList[i, j] = tempList[i, j];
                }
            }

            return galleryList;
        }

        public string getUserID()
        {
            return Session["userID"].ToString();
        }
    }
}