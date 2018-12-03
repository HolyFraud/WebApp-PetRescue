using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PAC
{
    public partial class RescueSignUp : System.Web.UI.Page
    {
        public SqlConnection mysqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
        public SqlDataReader mySqlDatareader; //Use for Retrive data 
        public SqlCommand mysqlCommand;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                SignUpPanel.Visible = false;
                ConfirmSignUpPanel.Visible = true;

                string tempGUID = Request.QueryString["guid"].ToString();
                string GuidVerifycommand = "select RescueListID from rescuelist where GUID = '" + tempGUID + "'";

                mysqlConnection.Open();
                mysqlCommand = new SqlCommand(GuidVerifycommand, mysqlConnection);
                mySqlDatareader = mysqlCommand.ExecuteReader(); //myReader object is for iterating through retrived data from SQL command

                if (!mySqlDatareader.HasRows)
                {
                    mysqlConnection.Close();
                    //Response.Write("Invalid GUID!!");
                    //Response.End();
                    
                    confirmSignUpLabel.Text="INVALID GUID!!";
                    return;

                }
                

                mysqlConnection.Close();

                mysqlConnection.Open();
                GuidVerifycommand = "Update RescueList SET SignupCompleted = getdate() where GUID = '" + tempGUID + "'";
                mysqlCommand = new SqlCommand(GuidVerifycommand, mysqlConnection);
                mySqlDatareader = mysqlCommand.ExecuteReader(); //myReader object is for iterating through retrived data from SQL command
                mysqlConnection.Close();
                confirmSignUpLabel.Text = "Thanks for signing up!!!";
            }
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            

            try

            {

                mysqlConnection.Open();


                //create GUID 
                string tmpGUID = Guid.NewGuid().ToString();



                string insertRescueListCommand = "INSERT into RescueList(RescueName, Address, Suburb, PostCode, Phone1, GUID) Values('" + rescueNameTextBoxID.Text + "','" + addressTextBoxID.Text + "' , '" + suburbTextBoxID.Text + "','" + postcodeTextBoxID.Text + "','" + phoneTextBoxID.Text + "', '" + tmpGUID + "')";


                mysqlCommand = new SqlCommand(insertRescueListCommand, mysqlConnection);
                
                mysqlCommand.ExecuteNonQuery(); // Returns Row Affected 

                mysqlConnection.Close();
                mysqlConnection.Open();

                //temprescueListID is for store rescuelistid from rescuelist table
                string tempRescueListID = "";

                string command1 = "select RescueListID from rescuelist where GUID = '" + tmpGUID + "'";
                mysqlCommand = new SqlCommand(command1, mysqlConnection);
                mySqlDatareader = mysqlCommand.ExecuteReader(); //myReader object is for iterating through retrived data from SQL command

                while (mySqlDatareader.Read())
                {
                    tempRescueListID = mySqlDatareader["rescuelistid"].ToString();
                }

                mysqlConnection.Close();
                mysqlConnection.Open();



                string insertRescueUserListcommand = "INSERT into RescueuserList(Rescuelistid, firstname, lastname, email1, password) Values('" + tempRescueListID.ToString() + "', '" + firstNameTextBoxID.Text + "','" + lastNameTextBoxID.Text + "', '" + email1TextBoxID.Text + "', '" + passwordTextBoxID.Text + "')";
                mysqlCommand = new SqlCommand(insertRescueUserListcommand, mysqlConnection);

          

                mysqlCommand.ExecuteNonQuery();
                mysqlConnection.Close();



                string emailBody = "Hello " + firstNameTextBoxID.Text + "<br/>";
                emailBody = emailBody + "Thanks for signing up to Pet Adoption Central as a Rescue.<br/>";
                emailBody = emailBody + "You are nearly done.<br/>";
                emailBody = emailBody + "Click on the link below and you will be taken to our website to complete the signup process.<br/>";
                emailBody = emailBody + "We just need a few more details and then you will be able to list animals<br/><br/>";
                emailBody = emailBody + "http://localhost:49962/RescueSignUp.aspx?guid=" + tmpGUID ;
                Util.SendEmail(email1TextBoxID.Text, emailBody);
                //signupMessage.Visible = true;








            }

            catch (Exception ex)
            {

                string xxx = ex.Message;
                

            }

            finally
            {

                mysqlConnection.Close();

            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}