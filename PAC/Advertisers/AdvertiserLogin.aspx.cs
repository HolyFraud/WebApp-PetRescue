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
    public partial class AdvertiserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        

        private string CountStatement(string email, string password)
        {
            return "SELECT COUNT(*) FROM AdvertiserUserList WHERE EmailAddress = '" + email + "' AND Password = '" + password + "'";
        }

        private string GetFullNameQuery()
        {
            return "SELECT FirstName, LastName FROM AdvertiserUserList WHERE EmailAddress = '" + txtUsername.Text + "'";
        }

        private bool LoginBool()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(CountStatement(txtUsername.Text, txtPassword.Text), conn);
            int count = (int)cmd.ExecuteScalar();
            if (count == 1)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        private void PassAdvertiserInfoValue()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetFullNameQuery(), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Session["MemberFirstName"] = reader[0].ToString();
            Session["MemberLastName"] = reader[1].ToString();
            Session["MemberEmail"] = txtUsername.Text;
            Session["RedirectFlag"] = "Advertiser";
            Response.Redirect("/Advertisers/AdvertiserPortal.aspx");
        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserSignUp.aspx");
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (LoginBool())
            {
                PassAdvertiserInfoValue();
            }
            else
            {
                LblError.Visible = true;
            }
        }
    }
}