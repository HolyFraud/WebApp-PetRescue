using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.Advertisers
{
    public partial class AdvertiserSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cogd"] != null)
                {
                    Session["CompanyGuid"] = Request.QueryString["cogd"];
                    Session["AdvertiserGuid"] = Request.QueryString["adgd"];
                    ExecuteAdsUpdateQuery();
                    ExecuteAdsUserUpdateQuery();
                    Response.Redirect("/Advertisers/AdvertiserLogin.aspx");
                }
                if (Request.QueryString["usfg"] != null)
                {
                    Session["AdvertiserGuid"] = Request.QueryString["usgd"];
                    ExecuteAdsUserUpdateQuery();
                    Response.Redirect("/Advertisers/AdvertiserLogin.aspx");
                }
            }
        }

        private string CountCompanyStatement()
        {
            return "SELECT COUNT(*) FROM AdvertiserList WHERE CompanyName = '" + txtCompanyName.Text + "'";
        }

        private string InsertCompanyQuery(Guid guid)
        {
            return "INSERT INTO AdvertiserList(AdvertiserName, CompanyName, Address, Address2, Suburb, State, PostCode, Phone1, Phone2, Email1, Email2, GUID) Values('" + txtAdvertiserName.Text + "', '" + txtCompanyName.Text + "', '" + txtAddress1.Text + "', '" + txtAddress2.Text + "', '" + txtSuburb.Text + "', '" + txtState.Text + "', '" + txtPostCode.Text + "', '" + txtPhone1.Text + "', '" + txtPhone2.Text + "', '" + txtEmail1.Text + "', '" + txtEmail2.Text + "', '" + guid + "') SELECT SCOPE_IDENTITY()";
        }

        private string InsertAdminQuery(string advertiserid, Guid guid)
        {
            return "INSERT INTO AdvertiserUserList(AdvertiserListID, FirstName, LastName, EmailAddress, Phone1, Phone2, IsAdmin, Password, GUID) Values(" + advertiserid + ", '" + txtAdminFName.Text + "', '" + txtAdminLName.Text + "', '" + txtAdminEmail.Text + "', '" + txtAdminPhone1.Text + "', '" + txtAdminPhone2.Text + "', " + 1 + ", '" + txtPassword.Text + "', '" + guid + "')";
        }

        protected void BtnSignUp_Click(object sender, EventArgs e)
        {
            if (Util.ExistCompany(CountCompanyStatement()))
            {
                LbMessage.Text = "Sign Up Failed, Company is Exists...!";
                LbMessage.Visible = true;
            }
            else
            {
                Guid CoGuid = Util.NewGuid();
                Guid AdminGuid = Util.NewGuid();
                InsertAdvertiserInfo(CoGuid, AdminGuid);
                SendEmail(CoGuid, AdminGuid);
            }
        }

        private void SendEmail(Guid coguid, Guid adminguid)
        {
            Util.SendEmail(txtAdminEmail.Text, EmailBody(coguid, adminguid));
            LbMessage.Visible = true;
        }

        private void InsertAdvertiserInfo(Guid coguid, Guid adminguid)
        {
            
            string AdvertiserListID = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(InsertCompanyQuery(coguid), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            AdvertiserListID = reader[0].ToString();
            reader.Close();
            cmd = new SqlCommand(InsertAdminQuery(AdvertiserListID, adminguid), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private string UpdateConfirmDate(string guid, string column_name)
        {
            return "Update " + column_name + " SET ConfirmDateTime = '" +  Util.GetCurrentDateTime() + "' WHERE GUID = '" + guid + "'";
        }

        private string GetConfirmDateTimeStatement(string guid, string column_name)
        {
            return "SELECT ConfirmDateTime FROM " + column_name + " WHERE GUID = '" + guid + "'";
        }

        private void ExecuteAdsUserUpdateQuery()
        {
            Util.ExecuteQuery(UpdateConfirmDate(Session["AdvertiserGuid"].ToString(), "AdvertiserUserList"));
        }

        private void ExecuteAdsUpdateQuery()
        {
            Util.ExecuteQuery(UpdateConfirmDate(Session["CompanyGuid"].ToString(), "AdvertiserList"));
        }

        private string EmailBody(Guid coguid, Guid adminguid)
        {
            string email = "Hello " + txtAdvertiserName + "<br/>";
            email += "Thanks for signing up to Pet Adoption Central as an advertiser.<br/>";
            email += "You are nearly done.<br/>";
            email += "Click on the link below and will be taken to our website to complete the signup process.<br/>";
            email += "http://localhost:49962/Advertisers/AdvertiserSignUp.aspx?cogd=" + coguid + "&adgd=" + adminguid;
            return email;
        }
    }
}