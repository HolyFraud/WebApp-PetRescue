using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                return null;
                //sexCondition = " AND Sex NOT IN ('')";
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
                return null;
                //breedCondition = " AND AnimalBreed NOT IN ('')";
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
                return null;
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

        private string StateStatement(CheckBoxList cblState)
        {
            if (string.IsNullOrEmpty(tbPostCode.Text))
            {
                List<string> stateList = new List<string>();
                string stateCondition = "";
                string stateSelected = cblState.SelectedValue;
                foreach (ListItem item in cblState.Items)
                {
                    if (item.Selected)
                    {
                        stateList.Add(item.Value);
                    }
                }
                if (cblState.SelectedIndex == -1)
                {
                    return null;
                    //stateCondition = " AND State NOT IN ('')";
                }
                else if (stateList.Count == 1)
                {
                    stateCondition = " AND State = '" + cblState.SelectedValue + "'";
                }
                else if (stateList.Count > 1)
                {
                    for (int i = 0; i < stateList.Count; i++)
                    {
                        if (i == 0)
                        {
                            stateCondition = " AND State IN ('" + stateList[i];
                        }
                        else if (i == stateList.Count - 1)
                        {
                            stateCondition += "', '" + stateList[i] + "')";
                        }
                        else
                        {
                            stateCondition += "', '" + stateList[i];
                        }
                    }
                }
                return stateCondition;
            }
            return null;
        }

        private string PostCode()
        {
            return tbPostCode.Text;
        }

        private string PcodeDistance()
        {
            return ddlRange.SelectedValue;
        }


        private string SelectCommand()
        {
            return "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, " +
                                "AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON " +
                                "AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON " +
                                "AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList ON " +
                                "AnimalList.SuburbListID = SuburbList.SuburbListID";
        }

        private RootObject GetJSONObject()
        {
            var webClient = new WebClient();
            string rowJSON = webClient.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + tbPostCode.Text + ",+AU&key=AIzaSyAEwsGJsYxlLkADDUif5oZ1oy7UG9VXOic");
            RootObject item = JsonConvert.DeserializeObject<RootObject>(rowJSON);
            return item;
        }

        private string QueryGetAnimalListByPostCodeDistance(string distance)
        {
            double lat = GetJSONObject().results[0].geometry.location.lat;
            double lng = GetJSONObject().results[0].geometry.location.lng;
            return "DECLARE @GEO1 GEOGRAPHY, @LAT VARCHAR(10), @LONG VARCHAR(10) SET @LAT='" + lat + "' SET @LONG='" + lng + "' SET @geo1= geography::Point(@LAT, @LONG, 4326) SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList ON AnimalList.SuburbListID = SuburbList.SuburbListID where(@geo1.STDistance(geography::Point(ISNULL(GPSLat, 0), ISNULL(GPSLon, 0), 4326)) / 1000) < " + distance;
            
        }

        /*--------------------------------Order Function Below---------------------------------------------------------------------------*/

        private string orderByStatement()
        {
            return " Orderby " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            
        }

        public void BindSql(string statSpecies, string statSex, string statAge, string statBreed, string statState)
        {
            int i = rblSpecies.SelectedIndex;
            if (cblSex.SelectedIndex == -1 && cblAge.SelectedIndex == -1 && rblSpecies.SelectedItem == null && cblStateList.SelectedIndex == -1 && string.IsNullOrEmpty(tbPostCode.Text))
            {
                SqlDataSource1.SelectCommand = SelectCommand();
                SqlDataSource1.DataBind();
                lvAnimalList.DataBind();
            }
            else if (string.IsNullOrEmpty(tbPostCode.Text))
            {
                SqlDataSource1.SelectCommand = SelectCommand()+ " WHERE " + statSpecies + statSex + statAge + statBreed + statState;

                SqlDataSource1.DataBind();
                lvAnimalList.DataBind();
            }
            else
            {
                SqlDataSource1.SelectCommand = QueryGetAnimalListByPostCodeDistance(ddlRange.SelectedValue) + " AND " + statSpecies + statSex + statAge + statBreed + statState;
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
            
            BindSql(speciesStatement(rblSpecies), sexStatement(cblSex), ageStatement(cblAge), breedStatement(cblBreed), StateStatement(cblStateList));
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
