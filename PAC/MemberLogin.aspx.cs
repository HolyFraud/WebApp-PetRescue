using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.Members
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            string sqlQuery = "SELECT COUNT(1) FROM MemberList WHERE Email = @Email AND Password = @Password";
            string getNameQuery = "SELECT * FROM Memberlist WHERE Email = '" + txtUsername.Text.Trim() + "'";
            SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection);
            SqlCommand cmd1 = new SqlCommand(getNameQuery, sqlConnection);
            cmd.Parameters.AddWithValue("@Email", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            string[] columnList = DBManager.getColumnName("MemberList");
            

            //Check the username validation
            if (count == 1)
            {
                using (SqlDataReader reader = cmd1.ExecuteReader()) {
                    if (reader.Read())
                    {
                        for (int i = 0; i < columnList.Length; i++)
                        {
                            Session["Member" + columnList[i]] = reader[columnList[i]];
                        }
                    }
                    reader.Close();
                }
                    //sent login info to home page
                    
                
                Response.Redirect("/Members/MemberHome.aspx");
            }
            else
                LblError.Visible = true;

            sqlConnection.Close();

        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }
    }
}