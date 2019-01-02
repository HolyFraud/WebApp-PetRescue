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
    public partial class ResetPassword : System.Web.UI.Page
    {
        string thisguid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ControlVisible();
        }

        private void ControlVisible()
        {
            lblMessage.Visible = false;
            if(null != Request.QueryString["guid"])
            {
                thisguid = Request.QueryString["guid"];
                txtemail.Visible = false;
                txtnewpswd.Visible = true;
                txtconpswd.Visible = true;
                BtnReset.Visible = false;
                BtnSubmit.Visible = true;
            }
        }

        private string QueryUpdateAdvertiserPassword()
        {
            return "Update AdvertiserUserList Set Password = '" + txtnewpswd.Text + "' Where GUID = '" + thisguid + "'";
        }

        private string QueryGetGuidAndFNameByEmail()
        {
            return "Select GUID, FirstName From AdvertiserUserList Where EmailAddress = '" + txtemail.Text + "'";
        }

        private string EmailBody(string guid, string fname)
        {
            string email = "Hello " + fname + "<br/>";
            email += "http://localhost:49962/Advertisers/ResetPassword.aspx?guid=" + guid;
            return email;
        }



        protected void BtnReset_Click(object sender, EventArgs e)
        {
            string guid, fname = "";
            lblEmailError.Visible = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            if (!Util.ExistEmail(txtemail.Text, "AdvertiserUserList", "EmailAddress"))
            {
                lblEmailError.Visible = true;
            }
            else
            {
                SqlCommand cmd = new SqlCommand(QueryGetGuidAndFNameByEmail(), sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                guid = (string)reader[0];
                fname = (string)reader[1];
                reader.Close();
                sqlConnection.Close();
                Util.SendEmail(txtemail.Text, EmailBody(guid, fname));
                lblMessage.Text = "<br />Confirmed E-mail has been sent to your Email...!";
                lblMessage.Visible = true;
            }
            
        }

        

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Util.ExecuteQuery(QueryUpdateAdvertiserPassword());
            lblMessage.Text = "<br />Password has been changed successfully...!";
            lblMessage.Visible = true;
        }
    }
}