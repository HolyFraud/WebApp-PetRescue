﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class AnimalSearch : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                getSessionSelection();
            }
            filterInfo();
        }

        private void getSessionSelection()
        {
            rblSpecies.DataBind();
            if ((string)Session["SearchRequest"] == "Dog")
            {
                rblSpecies.Items[0].Selected = true;
                Session.Remove("SearchRequest");
            }
            if ((string)Session["SearchRequest"] == "Cat")
            {
                rblSpecies.Items[1].Selected = true;
                Session.Remove("SearchRequest");
            }
            if ((string)Session["SearchRequest"] == "Other")
            {
                rblSpecies.SelectedIndex = -1;
                Session.Remove("SearchRequest");
            }
        }

        public string sexStatement(CheckBoxList thisCblSex)
        {
            List<string> sexList = new List<string>();
            string sexCondition = "";
            for (int i = 0; i < thisCblSex.Items.Count; i++)
            {
                if (thisCblSex.Items[i].Selected)
                {
                    sexList.Add(thisCblSex.SelectedValue);
                }
            }
            if (sexList.Count > 1 || thisCblSex.SelectedIndex == -1)
            {
                sexCondition = " AND Sex NOT IN ('')";
            }
            else
            {
                sexCondition = " AND Sex IN ('" + thisCblSex.SelectedValue + "')";
            }

            return sexCondition; 
        }



        private string speciesStatement(RadioButtonList thisRblSpecies)
        {
            
            string speciesCondition = "";
            ListItem li = thisRblSpecies.SelectedItem;
            if (thisRblSpecies.SelectedItem == null)
            {
                speciesCondition = " AnimalType NOT IN ('')";
            }
            else
            {
                speciesCondition = " AnimalType = '" + thisRblSpecies.SelectedItem.Text + "'";
            }
            
            return speciesCondition;
        }



        private string breedStatement(CheckBoxList thisCbl)
        {
            string breedCondition = "";

            List<string> breedList = new List<string>();
            for (int i = 0; i < thisCbl.Items.Count; i++)
            {
                if (thisCbl.Items[i].Selected)
                {
                    breedList.Add(thisCbl.Items[i].Text);
                }
            }
            if (thisCbl.SelectedIndex == -1)
            {
                breedCondition = " AND AnimalBreed NOT IN ('')";
            }
            else if (breedList.Count == 1)
            {
                breedCondition = " AND AnimalBreed IN ('" + thisCbl.SelectedItem.Text + "')";
            }
            else if (breedList.Count > 1)
            {
                for (int i = 0; i < breedList.Count; i++)
                {
                    if (i == 0)
                    {
                        breedCondition = " AND AnimalBreed IN ('" + breedList[i];
                    }
                    else if (i == breedList.Count - 1)
                    {
                        breedCondition += "', '" + breedList[i] + "')";
                    }
                    else
                    {
                        breedCondition += "', '" + breedList[i];
                    }
                }
            }

            return breedCondition;
        }

        

        public string ageStatement(CheckBoxList thisCblAge)
        {
            List<string> ageList = new List<string>();
            string sltAge = thisCblAge.SelectedValue;
            string ageCondition = "";
            for (int i = 0; i < thisCblAge.Items.Count; i++)
            {
                if (thisCblAge.Items[i].Selected)
                {
                    ageList.Add(thisCblAge.Items[i].Value);
                }
            }
            if (thisCblAge.SelectedIndex == -1)
            {
                ageCondition = " AND Age NOT IN ('')";
            }
            else if (ageList.Count == 1)
            {
                ageCondition = " AND Age " + sltAge;

            }
            else if (ageList.Count > 1)
            {
                for (int j = 0; j < ageList.Count; j++)
                {
                    if (j == 0)
                    {
                        ageCondition = " AND (Age " + ageList[j];
                    }
                    else if (j == ageList.Count - 1)
                    {
                        ageCondition += " OR Age " + ageList[j] + ")";
                    }
                    else
                    {
                        ageCondition += " OR Age " + ageList[j];
                    }
                }
            }
            return ageCondition;
        }

        private string orderByStatement()
        {
            return " Orderby " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            
        }

        public void BindSql(string statSpecies, string statSex, string statAge, string statBreed)
        {
            int i = rblSpecies.SelectedIndex;
            if (cblSex.SelectedIndex == -1 && cblAge.SelectedIndex == -1 && rblSpecies.SelectedItem == null)
            {
                SqlDataSource1.SelectCommand = Util.SelectCommand();
                SqlDataSource1.DataBind();
                lvAnimalList.DataBind();
            }
            else
            {
                SqlDataSource1.SelectCommand = Util.SelectCommand()+ " WHERE " + statSpecies + statSex + statAge + statBreed;

                SqlDataSource1.DataBind();
                lvAnimalList.DataBind();
            }
        }
        
        private void uncheckAllBreed()
        {
            foreach (ListItem listItem in cblBreed.Items)
            {
                listItem.Selected = false;
            }
        }

        private void filterInfo()
        {
            
            BindSql(speciesStatement(rblSpecies), sexStatement(cblSex), ageStatement(cblAge), breedStatement(cblBreed));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }
        

        protected void ddlResultsCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager dataPager = (DataPager)lvAnimalList.FindControl("dp1");
            if (dataPager != null)
            {
                dataPager.PageSize = Convert.ToInt32(ddlResultsCount.SelectedValue);
                lvAnimalList.DataBind();
            }
        }

        protected void ddlSortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string expression = ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            SortDirection direction = SortDirection.Ascending;
            if (ddlDeriction.SelectedValue == "Desc")
            {
                direction = SortDirection.Descending;
            }
            lvAnimalList.Sort(expression, direction);
        }

        protected void ddlDeriction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string expression = ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            SortDirection direction = SortDirection.Ascending;
            if (ddlDeriction.SelectedValue == "Desc")
            {
                direction = SortDirection.Descending;
            }
            lvAnimalList.Sort(expression, direction);
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Others";
            Response.Redirect("/AnimalSearch.aspx");
        }

        protected void lbDetails_Click(object sender, EventArgs e)
        {
            LinkButton lbDetails = sender as LinkButton;
            Label lbID = lbDetails.Parent.FindControl("lbAnimalListID") as Label;
            Session["AnimalListID"] = lbID.Text;
            Response.Redirect("/AnimalAdoption.aspx");
        }
    }
}
