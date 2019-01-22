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
            if (null != Session["MemberEmail"])
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
            lbAccountInfo.Text = "Welcome! " + thisFname + " " + thisLname;
        }

        protected void LbAccountInfo_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentAdoptionListID");
            Response.Redirect("/Members/MemberHome.aspx");
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            Session["GoToSearchFlag"] = 1;
            Session.Remove("FullSearchSQL");
            Response.Redirect("/AnimalSearch.aspx");
        }

        protected void LbLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/default.aspx");
        }
    }
}