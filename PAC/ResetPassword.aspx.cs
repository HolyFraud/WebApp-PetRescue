using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        string guid = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (!string.IsNullOrEmpty(Request.QueryString["guid"]))
            {
                guid = Request.QueryString["guid"];
                txtemail.Visible = false;
                txtnewpswd.Visible = true;
                txtconpswd.Visible = true;

                BtnReset.Visible = false;
                BtnSubmit.Visible = true;
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            lblEmailError.Visible = false;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            if (!Util.ExistEmail(txtemail.Text, "MemberList", "Email"))
            {
                lblEmailError.Visible = true;
            }
            else
            {
                
                string sqlQuery = "SELECT GUID, FirstName FROM MemberList WHERE Email = '" + txtemail.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
                string fname = "";
                string thisGuid = "";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        thisGuid = (string)reader["GUID"];
                        fname = (string)reader["FirstName"];

                    }
                    reader.Close();
                }
                sqlConnection.Close();

                string emailBody = "Hello " + fname + "<br/>";
                emailBody = emailBody + "http://localhost:49962/ResetPassword.aspx?guid=" + thisGuid;
                Util.SendEmail(txtemail.Text, emailBody);
                lblMessage.Text = "<br />Confirmed E-mail has been sent to your Email...!";
                lblMessage.Visible = true;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();

            string sqlQuery = "UPDATE MemberList SET Password = '" + txtnewpswd.Text + "' WHERE GUID = '" + guid + "'";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            lblMessage.Text = "<br />Password has been changed successfully...!";
            lblMessage.Visible = true;
        }
    }
}