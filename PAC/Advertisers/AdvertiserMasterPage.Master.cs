using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PAC.Advertisers
{
    public partial class AdvertiserMasterPage : System.Web.UI.MasterPage
    {
        Includes inc = new Includes();

        protected void Page_Load(object sender, EventArgs e)
        {
            NavLabelChange();

        }


        private void NavLabelChange()
        {
            if (null != Session["AdsEmail"])
            {
                LbAccountInfo.Text = "Welcome! " + Session["AdsFirstName"].ToString() + " " + Session["AdsLastName"].ToString();
                LbAccountInfo.Visible = true;
                LbLogout.Visible = true;
            }
        }

        protected void LbAds_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserPortal.aspx");
        }

        protected void LbUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserUsersManage.aspx");
        }

        protected void LbReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Advertisers/AdvertiserReports.aspx");
        }

        protected void LbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/default.aspx");
        }
    }
}