using System;
using System.Collections.Generic;
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

        private string SelectStatement(string email, string password)
        {
            return "SELECT * FROM AdvertiserUserList";
        }

        protected void BtnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserSignUp.aspx");
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}