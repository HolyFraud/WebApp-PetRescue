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
    public partial class AdvertiserUsersManage : System.Web.UI.Page
    {

        Includes inc = new Includes();

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateAdsIDSession();
            ControlVisbility();
        }


        private string GetAdvertiserListIDQuery()
        {
            return "Select AdvertiserListID FROM AdvertiserUserList WHERE EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetAdvertiserUserListIDQuery()
        {
            return "Select AdvertiserUserListID From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetIsAdminValueQuery()
        {
            return "SELECT IsAdmin FROM AdvertiserUserList WHERE EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetIsAdminValueByEmailQuery()
        {
            return "Select IsAdmin From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string InsertNewAdsUserQuery()
        {
            return "INSERT INTO AdvertiserUserList (AdvertiserListID, FirstName, LastName, EmailAddress, Phone1, Phone2, CreatedBy, IsAdmin, Password, GUID) Values(" + Session["AdsListID"].ToString() + ", '" + txtFName.Text + "', '" + txtLName.Text + "', '" + txtEmail.Text + "', '" + txtPhone1.Text + "', '" + txtPhone2.Text + "', '" + GetAdvertiserUserListID() + "', 0 , '" + txtpswd.Text + "', '" + GetGuid() + "')";
        }

        private string GetAdsNewUserGuidQuery()
        {
            return "Select GUID From AdvertiserUserList Where EmailAddress = '" + txtEmail.Text + "'";
        }

        private string GetCurrentAdsUserGuidQuery()
        {
            return "Select GUID From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private void ExecuteInserNewUserQuery()
        {
            Util.ExecuteQuery(InsertNewAdsUserQuery());
        }

        private Guid GetGuid()
        {
            return Util.NewGuid();
        }

        //private int IsAdminChecked()
        //{
        //    if (!chkAdmin.Checked)
        //    {
        //        return 0;
        //    }
        //    return 1;
        //}

        private string GetAdvertiserUserListID()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetAdvertiserUserListIDQuery(), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = reader[0].ToString();
            reader.Close();
            conn.Close();
            return res;
        }

        private string GetAdvertiserListID()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetAdvertiserListIDQuery(), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = reader[0].ToString();
            reader.Close();
            conn.Close();
            return res;
        }

        private string GetAdsNewUserGuid(string query)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = (string)reader[0];
            conn.Close();
            return res;
        }

        private int GetIsAdminValue(string query)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int res = Convert.ToInt32(reader[0]);
            conn.Close();
            return res;
        }

        /*--------Use Admin Flag to check current user is admin or not--------*/
        private bool IsAdminFlag(int flag)
        {
            if (flag == 1) return true;
            return false;
        }

        private void ControlVisbility()
        {
            HideAdsUsersTable();
            if (IsAdminFlag(GetIsAdminValue(GetIsAdminValueQuery())))
            {
                BtnAdd.Visible = true;
            }
        }

        private int CalculateSecurityMask()
        {
            int securitymask = 0;
            foreach (ListItem item in cblAuthControl.Items)
            {
                if (item.Selected)
                {
                    securitymask += Convert.ToInt32(item.Value);
                }
            }
            return securitymask;
        }
        
        private void GenerateAdsIDSession()
        {
            if(IsAdminFlag(GetIsAdminValue(GetIsAdminValueQuery()))) Session["AdsListID"] = GetAdvertiserListID();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            PlNewAdsUser.Visible = true;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            PlNewAdsUser.Visible = false;
        }


        private string EmailBody(string userguid)
        {
            string email = "Hello " + txtFName.Text + "<br/>";
            email += "Thanks for signing up to Pet Adoption Central as an advertiser.<br/>";
            email += "You are nearly done.<br/>";
            email += "Click on the link below and will be taken to our website to complete the signup process.<br/>";
            email += "http://localhost:49962/Advertisers/AdvertiserSignUp.aspx?usgd=" + userguid + "&usfg=1";
            return email;
        }

        private void SendEmail()
        {
            Util.SendEmail(txtEmail.Text, EmailBody(GetAdsNewUserGuid(GetAdsNewUserGuidQuery())));
        }

        private void HideAdsUsersTable()
        {
            if (GetIsAdminValue(GetIsAdminValueByEmailQuery()) != 1)
            {
                gvAdvertiserUsers.Visible = false;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Util.ExistEmail(txtEmail.Text, "AdvertiserUserList", "EmailAddress"))
            {
                LbMessege.Text = "Email has been Exist...!";
            }
            else
            {
                ExecuteInserNewUserQuery();
                LbMessege.Text = "Confrim Email has been sent to User...!";
                SendEmail();
                gvAdvertiserUsers.DataBind();
                PlNewAdsUser.Visible = false;
            }

        }
    }
}