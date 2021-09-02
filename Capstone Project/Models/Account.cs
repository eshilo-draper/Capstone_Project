using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

// used for password encryption:
using System.Security.Cryptography;
using System.Text;

namespace Capstone_Project.Models
{
    public class Account
    {
        private int userID;
        private string username;
        private string password;
        private string displayName;
        private string email;
        private DateTime dob;
        private string bio;
        private string icon;

        public Account()
        {

        }

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public Account(string username, string password, string displayName, string email, DateTime dob)
        {
            this.username = username;
            this.password = password;
            this.displayName = displayName;
            this.email = email;
            this.dob = dob;
        }

        public Account(int userID, string username, string displayName, string email, string icon, string bio)
        {
            this.userID = userID;
            this.username = username;
            this.displayName = displayName;
            this.email = email;
            this.icon = icon;
            this.bio = bio;
        }

        private string serverAddress()
        {
            return @"Server=sql.neit.edu\studentsqlserver,4500;Database=SE245_EShilo-Draper;User Id=SE245_EShilo-Draper;Password=008011411;";
        }

        public string register()
        {
            // generate password salt
            byte[] saltBytes = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes); // used for db storage

            // salt password
            byte[] saltedPW = Encoding.Unicode.GetBytes(password);
            saltedPW.Concat(saltBytes);

            // hash salted pw
            HashAlgorithm encryptor = new SHA256Managed();
            byte[] hashedBytes = encryptor.ComputeHash(saltedPW);
            string hashedPW = Convert.ToBase64String(hashedBytes);

            string status = "";

            // attempt adding record to db (validity checked by page)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Capstone_Login (username, passwordHash, passwordSalt, displayName, email, dob, icon) VALUES (@username, @passwordHash, @passwordSalt, @displayName, @email, @dob, @icon);";
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@passwordHash", hashedPW);
            cmd.Parameters.AddWithValue("@passwordSalt", salt);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@dob", dob);
            cmd.Parameters.AddWithValue("@icon", "Avatars/Default.png");

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                status = "success";
                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string login()
        {
            string status = "";

            // check database for matching login info
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT userID, passwordHash, passwordSalt FROM Capstone_Login WHERE username = @username;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {
                    // retrieve password salt and append to password attempt
                    byte[] saltedPW = Encoding.Unicode.GetBytes(password);
                    saltedPW.Concat(Convert.FromBase64String(results["passwordSalt"].ToString()));
                    // hash salted pw
                    HashAlgorithm encryptor = new SHA256Managed();
                    byte[] hashedBytes = encryptor.ComputeHash(saltedPW);
                    string hashedPWAttempt = Convert.ToBase64String(hashedBytes);


                    if (results["passwordHash"].ToString() != hashedPWAttempt)
                    {
                        status = "ERROR: invalid login. Please try again.";
                    }
                    else
                    {
                        status = results["userID"].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string checkUsername() // checks if a username already exists in database
        {
            string status = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT username FROM Capstone_Login WHERE username = @username;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                if (results.HasRows)
                {
                    status = "ERROR: username taken";
                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string checkUsername(int userID) // checks if a username already exists in database, with the exception of the userID provided
        {
            string status = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT username FROM Capstone_Login WHERE username = @username AND userID != @userID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@userID", userID);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                if (results.HasRows)
                {
                    status = "ERROR: username taken";
                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string getUsernameByID(int id)
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT username FROM Capstone_Login WHERE userID = @userID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@userID", id);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {

                    status = results["username"].ToString();

                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }
        public string getDisplayNameByID(int id)
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT displayName FROM Capstone_Login WHERE userID = @userID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@userID", id);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {

                    status = results["displayName"].ToString();

                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }public string getAvatarByID(int id)
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT icon FROM Capstone_Login WHERE userID = @userID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@userID", id);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {

                    status = results["icon"].ToString();

                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string getBioByID(int id)
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT bio FROM Capstone_Login WHERE userID = @userID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@userID", id);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {

                    status = results["bio"].ToString();

                }

                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string uploadImageData(int userID, string imagePath, string title, string medium, DateTime completionDate, string description)
        {
            string status = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO Capstone_Uploads (userID, imagePath, title, medium, completionDate, description) VALUES (@userID, @imagePath, @title, @medium, @completionDate, @description);";
            command.Connection = connection;

            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@imagePath", imagePath);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@medium", medium);
            command.Parameters.AddWithValue("@completionDate", completionDate);
            command.Parameters.AddWithValue("@description", description);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }


            return status;
        }

        public string updateImageData(int imageID, string imagePath, string title, string medium, DateTime completionDate, string description)
        {
            string status = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE Capstone_Uploads SET imagePath = @imagePath, title = @title, medium = @medium, completionDate = @completionDate, description = @description WHERE imageID = @imageID;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@imagePath", imagePath);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@medium", medium);
            command.Parameters.AddWithValue("@completionDate", completionDate);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@imageID", imageID);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception err)
            {
                status = err.Message;
            }


            return status;
        }

        public SqlDataReader getUploads(int userID, string condition)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Capstone_Uploads WHERE userID = @userID " + condition + ";"; // condition is always auto-generated, so it is safe to add

            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@condition", condition);

            command.Connection = connection;
            connection.Open();
            SqlDataReader uploads = command.ExecuteReader();

            return uploads;
        }

        public string[] getUploadData(int imageID, int userID)
        {
            string[] dataList = new string[0];

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Capstone_Uploads WHERE imageID = @imageID;";

            command.Parameters.AddWithValue("@imageID", imageID);

            try
            {
                connection.Open();
                SqlDataReader data = command.ExecuteReader();
                while (data.Read()) // should only loop once
                {
                    // ensure user has permission to edit post
                    if (int.Parse(data["userID"].ToString()) == userID)
                    {
                        dataList = new string[7];

                        dataList[0] = data["imageID"].ToString();
                        dataList[1] = data["userID"].ToString();
                        dataList[2] = data["imagePath"].ToString();
                        dataList[3] = data["title"].ToString();
                        dataList[4] = data["medium"].ToString();
                        dataList[5] = data["completionDate"].ToString();
                        dataList[6] = data["Description"].ToString();

                    }
                }

                connection.Close();
            }
            catch(Exception e)
            {
                // TODO: add error handling
            }

            return dataList;
        }

        public SqlDataReader getAccountInfo(int userID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT username, displayName, email, bio, icon FROM Capstone_Login WHERE userID = @userID;";
            command.Parameters.AddWithValue("@userID", userID);

            connection.Open();
            SqlDataReader result = command.ExecuteReader();
            return result;
        }

        public string updateAccountInfo()
        {
            string status = "";
            
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Capstone_Login SET username = @username, displayName = @displayName, email = @email, bio = @bio, icon = @icon WHERE userID = @userID;";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@displayName", displayName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@bio", bio);
            command.Parameters.AddWithValue("@icon", icon);
            command.Parameters.AddWithValue("@userID", userID);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                status = "success";
                connection.Close();
            }
            catch(Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string updatePassword(string newPW)
        {
            password = newPW;

            string status = "";
            string hashedPW = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand getSalt = new SqlCommand();
            getSalt.CommandText = "SELECT passwordSalt FROM Capstone_Login WHERE username = @username;";
            getSalt.Connection = connection;

            getSalt.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                SqlDataReader results = getSalt.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {
                    // retrieve password salt and append to new password
                    byte[] saltedPW = Encoding.Unicode.GetBytes(password);
                    saltedPW.Concat(Convert.FromBase64String(results["passwordSalt"].ToString()));
                    // hash salted pw
                    HashAlgorithm encryptor = new SHA256Managed();
                    byte[] hashedBytes = encryptor.ComputeHash(saltedPW);
                    hashedPW = Convert.ToBase64String(hashedBytes);
                }

                connection.Close();
            }
            catch(Exception e)
            {
                status = e.Message;
            }

            if (status == "") {
                SqlCommand changePW = new SqlCommand();
                changePW.CommandText = "UPDATE Capstone_Login SET passwordHash = @passwordHash WHERE username = @username";
                changePW.Connection = connection;

                changePW.Parameters.AddWithValue("@passwordHash", hashedPW);
                changePW.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    changePW.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception e)
                {
                    status = e.Message;
                }
            }

            return status;
        }
    }
}