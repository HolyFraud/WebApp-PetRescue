using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PAC
{
    public class Util
    {
        public static void SendEmail(string email, string body)
        {
            MailMessage MailMsg = new MailMessage(new MailAddress("pacsignup@passionforpets.com.au"), new MailAddress(email));
            MailMsg.Priority = MailPriority.High;
            MailMsg.IsBodyHtml = true;
            MailMsg.BodyEncoding = Encoding.Default;
            MailMsg.Subject = "Pet Adoption Central - Signup as a Rescue";
            MailMsg.Body = body;
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Send(MailMsg);
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

        public static bool ExistEmail(string email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MemberList WHERE Email = '" + email + "'", conn);
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

        public static bool ValidEmail(string email)
        {
            const string Pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex regex = new Regex(Pattern);
            Match match = regex.Match(email);

            if (match.Success)
            {
                return false;
            }
            return true;
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

        /**public static void GetParentWebControl<T>(T args, string control) where T : System.Web.UI.WebControls.View
        {
            //args = 
            
            //GridView gv = sender as GridView;
            //GridView GVQuestion = gv.Parent.FindControl("GVQuestion") as GridView;
        }*/

    }
}