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

        public Account()
        {
            this.username = "";
            this.password = "";
        }

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        private string serverAddress()
        {
            return @"Server=sql.neit.edu\studentsqlserver,4500;Database=SE245_EShilo-Draper;User Id=SE245_EShilo-Draper;Password=008011411;";
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