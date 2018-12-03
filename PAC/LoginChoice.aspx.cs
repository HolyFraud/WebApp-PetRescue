using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class LoginChoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnToClients_Click(object sender, EventArgs e)
        {
            Response.Redirect("/MemberLogin.aspx");
        }

        
    }
}