using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public class Util
    {
        
        public static List<Control> controlList = new List<Control>();

        public static void SendEmail(string email, string body)
        {
            MailMessage MailMsg = new MailMessage(new MailAddress("pacsignup@passionforpets.com.au"), new MailAddress(email));
            MailMsg.Priority = MailPriority.High;
            MailMsg.IsBodyHtml = true;
            MailMsg.BodyEncoding = Encoding.Default;
            MailMsg.Subject = "Pet Adoption Central";
            MailMsg.Body = body;
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Send(MailMsg);
        }

        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static DateTime StringtoDatetime(string inputString)
        {
            DateTime dateTime = Convert.ToDateTime(inputString);
            return dateTime;
        }
        
        public static Guid NewGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid;
        }

        public static string[] getColumnName(string tableName)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tableName + "'";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            List<string> columnList = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    columnList.Add(reader.GetString(0));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return columnList.ToArray();
        }

        public static bool ExistEmail(string email, string table_name, string column_name)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM " + table_name + " WHERE " + column_name + " = '" + email + "'", conn);
            int memberCount = (int)cmd.ExecuteScalar();
            using (SqlDataReader reader = cmd.ExecuteReader()){
                
                if (memberCount > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        public static bool ExistCompany(string command)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            int count = (int)cmd.ExecuteScalar();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (count > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        public static string NullToString(Object value)
        {
            return value == null ? "" : value.ToString();
        }

        public static string FormatDOB(string dob) {
            int foundIndex = dob.IndexOf(" ");
            dob = dob.Remove(foundIndex);
            return dob;
        }
        

        public static void ExecuteQuery(string command)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        
    }
}