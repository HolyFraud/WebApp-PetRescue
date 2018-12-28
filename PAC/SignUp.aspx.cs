using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;


namespace PAC
{
    public partial class SignUp : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            

            if (Util.ExistEmail(txtemail.Text, "MemberList", "Email"))
            {
                signupMessage.Text = "Email is exist...!";
                signupMessage.Visible = true;
            }
            else {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
                Guid thisGuid = Util.NewGuid();
                try
                {
                    sqlConnection.Open();
                    string command = "INSERT INTO MemberList(FirstName, LastName, Email, Password, GUID) VALUES(@FirstName, @LastName, @Email, @Password, @GUID)";
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);

                    cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtpasswd.Text);
                    cmd.Parameters.AddWithValue("@GUID", thisGuid);



                    cmd.ExecuteNonQuery();
                    string emailBody = "Hello " + txtfname.Text + "<br/>";
                    emailBody = emailBody + "Thanks for signing up to Pet Adoption Central as a public user.<br/>";
                    emailBody = emailBody + "You are nearly done.<br/>";
                    emailBody = emailBody + "Click on the link below and you will be taken to our website to complete the signup process.<br/>";
                    emailBody = emailBody + "We just need a few more details and then you will be able to list animals<br/><br/>";
                    emailBody = emailBody + "http://localhost:49962/signupfurther.aspx?guid=" + thisGuid;
                    Util.SendEmail(txtemail.Text, emailBody);
                    signupMessage.Visible = true;

                }
                catch (Exception)
                {
                    signupMessage.Text = "Sign Up Failed...!";
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            
            
            
                
            
        }

    }
}
