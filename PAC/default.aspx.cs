using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.View.Page
{
    public partial class Home : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lkDog_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Dog";
            Response.Redirect("/AnimalSearch.aspx");
        }

        protected void lkCat_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Cat";
            Response.Redirect("/AnimalSearch.aspx");
        }

        protected void lkOthers_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Other";
            Response.Redirect("/AnimalSearch.aspx");
        }
    }
}