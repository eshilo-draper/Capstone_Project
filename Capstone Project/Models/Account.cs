using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Capstone_Project.Models
{
    public class Account
    {
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

        private string serverAddress()
        {
            return @"Server=sql.neit.edu\studentsqlserver,4500;Database=SE245_EShilo-Draper;User Id=SE245_EShilo-Draper;Password=008011411;";
        }

        public string register()
        {
            string status = "";

            // attempt adding record to db (validity checked by page)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Capstone_Login_TEMP (username, password, displayName, email, dob) VALUES (@username, @password, @displayName, @email, @dob);";
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@displayName", displayName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@dob", dob);

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                status = "success";
            }
            catch(Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string login()
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Capstone_Login_TEMP WHERE username = @username;";
            command.Connection = connection;

            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                SqlDataReader results = command.ExecuteReader();

                while (results.Read()) // should only go through this loop once
                {
                    if (results["password"].ToString() != password)
                    {
                        status = "ERROR: invalid login. Please try again.";
                    }
                    else
                    {
                        status = results["userID"].ToString();
                    }
                }
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
            command.CommandText = "SELECT username FROM Capstone_Login_TEMP WHERE username = @username;";
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
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }

        public string GetUsernameByID(int id)
        {
            string status = "";

            // check database for matching login info (TEMP, NOT SECURE)
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @serverAddress();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Capstone_Login_TEMP WHERE userID = @userID;";
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
            }
            catch (Exception err)
            {
                status = err.Message;
            }

            return status;
        }
    }
}