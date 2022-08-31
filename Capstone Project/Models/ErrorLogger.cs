using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Capstone_Project.Models
{
    public class ErrorLogger
    {
        // LOCATION OF ERROR LOG ********************************
        public static string logFilePath = HttpContext.Current.Server.MapPath(".") + @"\ERRORLOG.txt";

        // create log file if not found
        public static void createFile()
        {
            // ensure log file does not currently exist
            if (!File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath,"LOG FILE CREATED: " + DateTime.Now.ToString() + "\n");
            }
        }

        // log error
        public static void logError(Exception err)
        {
            // ensure file exists
            createFile();

            String errorMessage = String.Format("Exception \"{0}\" thrown at {1}\n", err.Message, DateTime.Now.ToString());

            File.AppendAllText(logFilePath,errorMessage);
        }
    }
}