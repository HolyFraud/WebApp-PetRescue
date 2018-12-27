using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.View.Page
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private string thisFname, thisLname;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberEmail"] == null || string.IsNullOrEmpty(Session["MemberEmail"].ToString()))
            {
                lbAccountInfo.Visible = false;
                lbLogout.Visible = false;
            }
            else
            {
                thisFname = (string)Session["MemberFirstName"];
                thisLname = (string)Session["MemberLastName"];
                lbLogin.Visible = false;
                lbSignup.Visible = false;
                lbAccountInfo.Text = "Welcome! " +thisFname + " " + thisLname;
            }
        }

        protected void LbAccountInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Members/MemberHome.aspx");
        }

        protected void LbAdvertiserLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserLogin.aspx");
        }

        protected void LbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/default.aspx");
        }
    }
}