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
            lbAccountInfo.Visible = false;
            lbLogout.Visible = false;
            if (Session["RedirectFlag"] == "Member")
            {
                NavLabelChange();
            }
            if (Session["RedirectFlag"] == "Advertiser")
            {
                NavLabelChange();
            }
        }

        private void NavLabelChange()
        {
            thisFname = (string)Session["MemberFirstName"];
            thisLname = (string)Session["MemberLastName"];
            lbAccountInfo.Visible = true;
            lbLogout.Visible = true;
            lbLogin.Visible = false;
            lbSignup.Visible = false;
            LbAdvertiserLogin.Visible = false;
            lbAccountInfo.Text = "Welcome! " + thisFname + " " + thisLname;

        }

        protected void LbAccountInfo_Click(object sender, EventArgs e)
        {
            if(Session["RedirectFlag"] == "Member")
            {
                Response.Redirect("/Members/MemberHome.aspx");
            }
            if (Session["RedirectFlag"] == "Advertiser")
            {
                Response.Redirect("/Advertisers/AdvertiserPortal.aspx");
            }
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