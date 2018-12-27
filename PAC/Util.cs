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

        static List<int> indexList = new List<int>();
        public static List<Control> controlList = new List<Control>();
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

        public static string SelectCommand()
        {
            return "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, " +
                            "AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON " +
                            "AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON " +
                            "AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID";
        }
        
        
        public static string GetQuestionType(string questionTypeListID)
        {
            string command = "SELECT QuestionType FROM QuestionTypeList WHERE QuestionTypeListID = " + questionTypeListID;
            string questionType = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    questionType = reader["QuestionType"].ToString();
                }
                conn.Close();
            }
            return questionType;
        }

        public static string GetQuestionTypeListID(string questionListID)
        {
            string command = "SELECT QuestionTypeListID FROM QuestionList WHERE QuestionListID = " + questionListID;
            string questionTypeListID = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    questionTypeListID = reader["QuestionTypeListID"].ToString();
                }
                conn.Close();
            }
            return questionTypeListID;
        }
        
        public static string GetQuestionTemplateListID(string animalListID)
        {
            string command = "SELECT QuestionTemplateListID FROM AnimalList WHERE AnimalListID = " + animalListID;
            string questionTemplateListID = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    questionTemplateListID = reader["QuestionTemplateListID"].ToString();
                }
                conn.Close();
            }
            return questionTemplateListID;
        }

        public static string InsertNewResponseValue(int questionid, string adoptionid, string controlvalue)
        {
            if(controlvalue == null)
                return "INSERT INTO QuestionResponseList(QuestionListID, AdoptionListID, ResponseValue) VALUES(" + questionid + ", " + adoptionid + ", null)";
            return "INSERT INTO QuestionResponseList(QuestionListID, AdoptionListID, ResponseValue) VALUES(" + questionid + ", " + adoptionid + ", '" + controlvalue + "')";
        }
        

        public static string GetResponseValueID(string questionid, string adoptionid)
        {
            string command = "select QuestionResponseListID from QuestionResponseList where QuestionListID = " + questionid + " And AdoptionListID = " + adoptionid;
            string responseId = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            responseId = reader[0].ToString();
            conn.Close();
            return responseId;
        }

        public static string GetResponseValue(string questionid, string adoptionid)
        {
            string command = "select responsevalue from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            string responseValue = ""; SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            responseValue = reader[0].ToString();
            conn.Close();
            return responseValue;
        }

        public static void GetResponseValueList(string questionid, string adoptionid, List<string> responseValuelist)
        {
            string command = "select responsevalue from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    responseValuelist.Add(reader[0].ToString());
                }
            }
            reader.Close();
            conn.Close();
        }

        public static string UpdateResponseValueCBLCommand(string value, string questionid, string adoptionid, List<string> responseidList, int idIndex)
        {
            if (value == null)
                return "UPDATE QuestionResponseList SET ResponseValue = null WHERE QuestionResponseListID = " + GetResponseValueIDList(questionid, adoptionid, responseidList)[idIndex];
            return "UPDATE QuestionResponseList SET ResponseValue = '" + value + "' WHERE QuestionResponseListID = " + GetResponseValueIDList(questionid, adoptionid, responseidList)[idIndex];
        }

        public static string UpdateResponseValueCommand(string value, string questionid, string adoptionid)
        {
            return "UPDATE QuestionResponseList SET ResponseValue = '" + value + "' WHERE QuestionResponseListID = " + GetResponseValueID(questionid, adoptionid);
        }
        
        public static List<string> GetResponseValueIDList(string questionid, string adoptionid, List<string> responseidList)
        {
            string command = "select QuestionResponseListID from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    responseidList.Add(reader[0].ToString());
                }
            }
            return responseidList;
        }

        public static void ExecuteQuery(string command)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        
        public static string GetQuestionItemListSelectCMD(string questionid)
        {
            return "SELECT QuestionItemList.QuestionItemText FROM QuestionItemList INNER JOIN QuestionList ON " +
                "QuestionItemList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTemplateList ON " +
                "QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN QuestionTypeList ON " +
                "QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE QuestionItemList.QuestionListID = " + questionid;
        }
    }
}