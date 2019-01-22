using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

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
            if (null != Session["FullSearchSQL"])
            {
                ResultsSqlDataSource.SelectCommand = Session["FullSearchSQL"].ToString();
            }
            if (null != Session["GoToSearchFlag"])
            {
                ResultsSqlDataSource.SelectCommand = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID order by AnimalList.Created desc";
            }
        }


        //Get search key word of animal type from home page by passing session var
        private void getSessionSelection()
        {
            rblSpecies.DataBind();
            if ((string)Session["SearchRequest"] == "Dog")
            {
                rblSpecies.Items[0].Selected = true;
                Session["FullSearchSQL"] = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList on SuburbList.SuburbListID = AnimalList.SuburbListID Where AnimalType = 'Dog'";
                Session.Remove("SearchRequest");
            }
            if ((string)Session["SearchRequest"] == "Cat")
            {
                rblSpecies.Items[1].Selected = true;
                Session["FullSearchSQL"] = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList on SuburbList.SuburbListID = AnimalList.SuburbListID Where AnimalType = 'Cat'";
                Session.Remove("SearchRequest");
            }
            if ((string)Session["SearchRequest"] == "Other")
            {
                rblSpecies.SelectedIndex = -1;
                Session["FullSearchSQL"] = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList on SuburbList.SuburbListID = AnimalList.SuburbListID Where AnimalType Not In('Dog','Cat')";
                Session.Remove("SearchRequest");
            }
        }

        /*------------------------------------------------Start Search Condition filter---------------------------------------*/

        //start "SEX" filter condition by returning string type sql where clause 
        public string sexStatement()
        {
            List<string> sexList = new List<string>();
            string sexCondition = "";
            for (int i = 0; i < cblSex.Items.Count; i++)
            {
                if (cblSex.Items[i].Selected)
                {
                    sexList.Add(cblSex.SelectedValue);
                }
            }
            if (sexList.Count > 1 || cblSex.SelectedIndex == -1)
            {
                return null;
            }
            else
            {
                sexCondition = " AND Sex IN ('" + cblSex.SelectedValue + "')";
            }

            return sexCondition;
        }

        //start "AnimalType" filter condition by returning string type sql where clause 
        private string speciesStatement()
        {
            return rblSpecies.SelectedItem == null ? " AnimalType NOT IN ('')" : " AnimalType = '" + rblSpecies.SelectedItem.Text + "'";
        }

        //start "AnimalBreed" filter condition by returning string type sql where clause 
        private string breedStatement()
        {
            string breedCondition = "";

            List<string> breedList = new List<string>();
            for (int i = 0; i < cblBreed.Items.Count; i++)
            {
                if (cblBreed.Items[i].Selected)
                {
                    breedList.Add(cblBreed.Items[i].Text);
                }
            }
            if (cblBreed.SelectedIndex == -1)
            {
                return null;
            }
            else if (breedList.Count == 1)
            {
                breedCondition = " AND AnimalBreed IN ('" + cblBreed.SelectedItem.Text + "')";
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

        //start "Age" filter condition by returning string type sql where clause 
        public string ageStatement()
        {
            return rblAge.SelectedIndex == -1 ? null : " And Age " + rblAge.SelectedValue;

        }

        //start "State" filter condition by returning string type sql where clause 
        private string StateStatement()
        {
            if (string.IsNullOrEmpty(tbPostCode.Text))
            {
                List<string> stateList = new List<string>();
                string stateCondition = "";
                string stateSelected = cblStateList.SelectedValue;
                foreach (ListItem item in cblStateList.Items)
                {
                    if (item.Selected)
                    {
                        stateList.Add(item.Value);
                    }
                }
                if (cblStateList.SelectedIndex == -1)
                {
                    return null;
                }
                else if (stateList.Count == 1)
                {
                    stateCondition = " AND State = '" + cblStateList.SelectedValue + "'";
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
        /*------------------------------------------------End Search Condition filter---------------------------------------*/

        /*--------------------------------Order Function Below---------------------------------------------------------------------------*/
        
        //Using AutoPostBack to Call this funtion to bind datasource to rad grid
        protected void ddlSortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != Session["FullSearchSQL"])
            {
                ResultsSqlDataSource.SelectCommand = Session["FullSearchSQL"].ToString() + " Order By " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
                ResultsRadgrid.DataBind();
            }
            else
            {
                ResultsSqlDataSource.SelectCommand = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID Order By " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
                ResultsRadgrid.DataBind();
            }
        }

        protected void ddlDeriction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != Session["FullSearchSQL"])
            {
                ResultsSqlDataSource.SelectCommand = Session["FullSearchSQL"].ToString() + " Order By " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
                ResultsRadgrid.DataBind();
            }
            else
            {
                ResultsSqlDataSource.SelectCommand = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID Order By " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
                ResultsRadgrid.DataBind();
            }
        }
        /*-----------------------------------------End Order Function----------------------------------------------------*/


        /*--------------------------------------Reset filter Conditon--------------------------------------------------------*/
        protected void lbReset_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Others";
            Session["GoToSearchFlag"] = 1;
            Response.Redirect("/AnimalSearch.aspx");
            Session.Remove("SearchResultsSql");
        }

        /*------------------------------------------End reset filter condition-----------------------------------------------*/

        /*---------------------------------------------Start Particular Animal FurtherDetail function-----------------------*/
        
        protected void MoreInfoBtn_Click(object sender, EventArgs e)
        {
            Button MoreInfoBtn = sender as Button;
            RadLabel AnimalListIDRadLabel = MoreInfoBtn.Parent.FindControl("AnimalListIDRadLabel") as RadLabel;
            Session["AnimalListID"] = AnimalListIDRadLabel.Text;
            Response.Redirect("/AnimalAdoption.aspx");
        }

        /*----------------------------------------------End particular animal furtherdetail function---------------------------*/

        /*------------------------------------------Start Collect user behavior function------------------------------------------*/
        

        private string GetMemberListID()
        {
            return null == Session["MemberMemberListID"] ? "0" : Session["MemberMemberListID"].ToString();
        }

        private string GetAnimalTypeListID()
        {
            return rblSpecies.SelectedIndex == -1 ? "0" : rblSpecies.SelectedItem.Value;
        }

        private string GetAgeFrom()
        {
            if (rblAge.SelectedIndex == -1) return "NULL";
            else
            {
                if (rblAge.Items[0].Selected) return "0";
                if (rblAge.Items[1].Selected) return "1";
                if (rblAge.Items[2].Selected) return "6";
                if (rblAge.Items[3].Selected) return "10";
            }
            return "NULL";
        }

        private string GetAgeTo()
        {
            if (rblAge.SelectedIndex == -1) return "NULL";
            else
            {
                if (rblAge.Items[0].Selected) return "1";
                if (rblAge.Items[1].Selected) return "5";
                if (rblAge.Items[2].Selected) return "10";
                if (rblAge.Items[3].Selected) return "NULL";
            }
            return "NULL";
        }

        private string GetPostCode()
        {
            if (string.IsNullOrEmpty(tbPostCode.Text.Trim()))
            {
                return "NULL";
            }
            else
            {
                return tbPostCode.Text;
            }
        }

        private string GetPostCodeRange()
        {
            return string.IsNullOrEmpty(tbPostCode.Text.Trim()) ? "NULL" : ddlRange.SelectedValue;
        }
        
        //insert user search behavior data into sqlserver
        private void InsertSearchActionCollection()
        {
            //build sql values clause for insert into MemberSearchHistoryList
            string memberlistid = GetMemberListID();
            string animaltypeid = GetAnimalTypeListID();
            string agefrom = GetAgeFrom();
            string ageto = GetAgeTo();
            string postcode = GetPostCode();
            string postcoderange = GetPostCodeRange();

            //insert into values select SCOPE_IDENTITY()
            string GetCurrentSearchIDSQL = "Insert Into MemberSearchHistoryList (MemberListID, AnimalTypeListID, AgeFrom, AgeTo, PostCode, PostCodeRange) Values(" + memberlistid + ", " + animaltypeid + ", " + agefrom + ", " + ageto + ", " + postcode + ", " + postcoderange + ") select SCOPE_IDENTITY()";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetCurrentSearchIDSQL, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Session["CurrentSearchID"] = reader[0];
            reader.Close();
            
        }

        private string GetBreedID(ListItem item)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select AnimalBreedListID From AnimalBreedList Where AnimalBreed = '" + item.Text + "'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader[0].ToString();
        }

        private void InsertAnimalBreedSearchHistory()
        {
            //string GetBreedIdSQL = "Select AnimalBreedListID From AnimalBreedList Where AnimalBreed = '" + item.Text + "'"
            if (cblBreed.SelectedIndex != -1)
            {
                foreach (ListItem item in cblBreed.Items)
                {
                    if (item.Selected)
                    {
                        string breedid = GetBreedID(item);
                        Util.ExecuteQuery("Insert Into MemberSearchHistoryBreedItemList Values (" + breedid + ", " + Session["CurrentSearchID"].ToString() + ")");
                    }
                }
            }
            else
            {
                Util.ExecuteQuery("Insert Into MemberSearchHistoryBreedItemList Values (" + 0 + ", " + Session["CurrentSearchID"].ToString() + ")");
            }
        }
        
        //insert user search state behavior data 
        private void InsertStateSearchHistory()
        {
            if (cblStateList.SelectedIndex != -1)
            {
                foreach (ListItem item in cblStateList.Items)
                {
                    if (item.Selected)
                        Util.ExecuteQuery("Insert Into MemberSearchHistoryStateList Values (" + Session["CurrentSearchID"].ToString() + ", '" + item.Value + "')");
                }
            }
            else
            {
                Util.ExecuteQuery("Insert Into MemberSearchHistoryStateList Values (" + Session["CurrentSearchID"].ToString() + ", 'NULL')");
            }
        }

        /*-----------------------------------------------------End Collect user behavior function--------------------------------*/

        /*----------------------------------------------Telerik Gridview For AnimalList--------------------------------------------------------*/
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string baseSQL = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList on SuburbList.SuburbListID = AnimalList.SuburbListID ";
            ////build where clause
            string typeSQL = speciesStatement();
            string breedSQL = breedStatement();
            string sexSQL = sexStatement();
            string ageSQL = ageStatement();
            string distanceSQL = DistanceStatement();
            string stateSQL = StateStatement();
            //session var
            Session["FullSearchSQL"] = baseSQL + " Where " + typeSQL + breedSQL + sexSQL + ageSQL + stateSQL + distanceSQL;
            //bind data
            ResultsSqlDataSource.SelectCommand = Session["FullSearchSql"].ToString();
            ResultsRadgrid.DataBind();
            
            //collect user search behavior which are search breed, search state for animals, and all info search for members
            //for members
            InsertSearchActionCollection();
            //for breed
            InsertAnimalBreedSearchHistory();
            //for state
            InsertStateSearchHistory();

            //remove flag session
            Session.Remove("GoToSearchFlag");
        }

        private RootObject GetJSONObject()
        {
            var webClient = new WebClient();
            string rowJSON = webClient.DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + tbPostCode.Text + ",+AU&key=AIzaSyAEwsGJsYxlLkADDUif5oZ1oy7UG9VXOic");
            RootObject item = JsonConvert.DeserializeObject<RootObject>(rowJSON);
            return item;
        }

        private string DistanceStatement()
        {
            if (!string.IsNullOrEmpty(tbPostCode.Text))
            {
                double lat = GetJSONObject().results[0].geometry.location.lat;
                double lng = GetJSONObject().results[0].geometry.location.lng;
                return "And (111.045 * DEGREES(ACOS(COS(RADIANS(" + lat + ")) * COS(RADIANS((SuburbList.GPSLat))) * COS(RADIANS(" + lng + ") - RADIANS(SuburbList.GPSLon)) + SIN(RADIANS(" + lat + ")) * SIN(RADIANS(SuburbList.GPSLat))))) < " + ddlRange.SelectedValue;
            }
            else
                return null;
        }

        //Check the favourite pet recordstatus is active or not
        private bool ExistRecord(string animalid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select COUNT(*) From MemberFavouriteList Where MemberListID = " + Session["MemberMemberListID"].ToString() + " And AnimalListID = " + animalid, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int res = Convert.ToInt32(reader[0]);
            if (res < 1)
                return false;
            return true;
        }

        protected void LikeBtn_Click(object sender, EventArgs e)
        {
            Button favouritebtn = sender as Button;
            RadLabel AnimalListIDRadLabel = favouritebtn.Parent.FindControl("AnimalListIDRadLabel") as RadLabel;
            if (null != Session["MemberMemberListID"] && favouritebtn.CssClass == "likebtnbefore")
            {
                if (ExistRecord(AnimalListIDRadLabel.Text))
                {
                    Util.ExecuteQuery("Update MemberFavouriteList Set RecordStatus = 1 Where MemberListID = " + Session["MemberMemberListID"].ToString() + " And AnimalListID = " + AnimalListIDRadLabel.Text);
                    favouritebtn.CssClass = "likebtnafter";
                }
                else
                {
                    Util.ExecuteQuery("Insert Into MemberFavouriteList (MemberListID, AnimalListID) Values (" + Session["MemberMemberListID"].ToString() + ", " + AnimalListIDRadLabel.Text + ")");
                    favouritebtn.CssClass = "likebtnafter";
                }
            }
            else if (null != Session["MemberMemberListID"] && favouritebtn.CssClass == "likebtnafter")
            {
                Util.ExecuteQuery("Update MemberFavouriteList Set RecordStatus = 0 Where MemberListID = " + Session["MemberMemberListID"].ToString() + " And AnimalListID = " + AnimalListIDRadLabel.Text);
                favouritebtn.CssClass = "likebtnbefore";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('Please Login First...!')", true);
            }
        }

        protected void ResultsRadgrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                List<Favourite> favlist = new List<Favourite>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
                conn.Open();
                if (null != Session["MemberMemberListID"])
                {
                    SqlCommand cmd = new SqlCommand("Select MemberListID, AnimalListID, RecordStatus From MemberFavouriteList Where RecordStatus = 1 And MemberListID = " + Session["MemberMemberListID"].ToString(), conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Favourite fav = new Favourite(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                        favlist.Add(fav);//
                    }
                    reader.Close();
                    //
                    foreach (GridDataItem row in ResultsRadgrid.MasterTableView.Items)
                    {
                        RadLabel LikedAnimal = row.FindControl("AnimalListIDRadLabel") as RadLabel;
                        foreach (Favourite fav in favlist)
                        {
                            if (LikedAnimal.Text == fav.AnimalListID)
                            {
                                Button LikeBtn = row.FindControl("LikeBtn") as Button;
                                LikeBtn.CssClass = "likebtnafter";
                            }
                        }
                    }
                }
            }
        }
    }
}