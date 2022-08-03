using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capstone_Project.Models;
using MySqlConnector;

namespace Capstone_Project
{
    public partial class User : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] == null)
            {
                Response.Redirect("default");
            }

            // preload user avatar and bio into image and description areas
            Account temp = new Account();
            int userID = int.Parse(Request.QueryString["user"].ToString());
            imageBig.ImageUrl = temp.getAvatarByID(userID);
            lblImageTitle.Text = temp.getDisplayNameByID(userID);
            lblImageInfo.Text = temp.getBioByID(userID);
        }

        public string[,] getImages()
        {
            int galleryID;
            string[,] galleryList;

            if (Request.QueryString != null && int.TryParse(Request.QueryString["user"], out galleryID))
            {
                // create a temp account object and use it to retrieve all images by the selected user (default sort by completionDate ascending)
                Account temp = new Account();
                MySqlDataReader galleryReader = temp.getUploads(galleryID, "ORDER BY CompletionDate DESC");

                // convert galleryReader to a list of lists of length 20
                string[,] tempList = new string[20, 5];

                int row = 0;

                while (galleryReader.Read())
                {
                    tempList[row, 0] = galleryReader["imagePath"].ToString();
                    tempList[row, 1] = galleryReader["title"].ToString();
                    tempList[row, 2] = galleryReader["medium"].ToString();
                    tempList[row, 3] = galleryReader["completionDate"].ToString();
                    tempList[row, 4] = galleryReader["description"].ToString();

                    row++;
                }

                // use row count to generate list of minimum length
                galleryList = new string[row, 5];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        galleryList[i, j] = tempList[i, j];
                    }
                }

                return galleryList;
            }
            else
            {
                return new string[0, 0];
            }
        }
    }
}