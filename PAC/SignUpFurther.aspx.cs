using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class FurtherSignUp : System.Web.UI.Page
    {
        public string thisGuid;
        public string thisEmail;

        protected void Page_Load(object sender, EventArgs e)
        {
            thisGuid = Request.QueryString["guid"];
            Session["thisGuid"] = thisGuid;
        }

        protected void BtnSignup2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            
                sqlConnection.Open();
                string command = "UPDATE MemberList SET MiddleName = @MiddleName, Phone1 = @Phone1, " +
                                                            "Phone2 = @Phone2, Address = @Address, Address2 = @Address2, " +
                                                            "Suburb = @Suburb, State = @State, PostCode = @PostCode, DOB = @DOB, " +
                                                            "Country = @Country, Gender = @Gender WHERE GUID = '" + thisGuid + "'";
                 SqlCommand cmd = new SqlCommand(command, sqlConnection);

                cmd.Parameters.AddWithValue("@MiddleName", txtmname.Text);
                cmd.Parameters.AddWithValue("@Phone1", txtmobile.Text);
                cmd.Parameters.AddWithValue("@Phone2", txtphone.Text);
                cmd.Parameters.AddWithValue("@Address", txtaddress1.Text);
                cmd.Parameters.AddWithValue("@Address2", txtaddress2.Text);
                cmd.Parameters.AddWithValue("@Suburb", txtsuburb.Text);
                cmd.Parameters.AddWithValue("@State", txtstate.Text);
                cmd.Parameters.AddWithValue("@PostCode", txtpostcode.Text);
                cmd.Parameters.AddWithValue("@DOB", Util.StringtoDatetime(txtdob.Text));
                cmd.Parameters.AddWithValue("@Country", ddlcountry.Text);
                cmd.Parameters.AddWithValue("@Gender", ddlgender.Text);


                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT Email FROM MemberList WHERE GUID = '" + thisGuid + "'", sqlConnection);
                SqlDataReader reader = cmd2.ExecuteReader();
                reader.Read();
                thisEmail = (string)reader[0];
                    

                string emailBody = "All Finished...!";
                emailBody = emailBody + "Thanks for your registration...!";
                emailBody = emailBody + "Now you can apply for pet adoption";
                Util.SendEmail(thisEmail, emailBody);
                signupMessage.Visible = true;

            
        }
    }
}