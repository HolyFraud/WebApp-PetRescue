using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.Members
{
    public partial class MemberHome : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string thisEmail = Session["MemberEmail"].ToString();
            if (null != Session["CurrentAdoptionListID"])
            {
                lb.Text = Session["CurrentAdoptionListID"].ToString();
            }
        }

        protected void lbUpdateInfo_Click(object sender, EventArgs e)
        {
            Session["MemberUpdateState"] = "1";
            Response.Redirect("/Members/ManageMyAccount.aspx");
        }



    }
}