using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            //filterInfo();
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
            return thisRblSpecies.SelectedItem == null ? " AnimalType NOT IN ('')" : " AnimalType = '" + thisRblSpecies.SelectedItem.Text + "'";
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



        public string ageStatement(RadioButtonList thisRblAge)
        {
            return rblAge.SelectedIndex == -1 ? null : " And Age " + thisRblAge.SelectedValue;

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
            return "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList ON AnimalList.SuburbListID = SuburbList.SuburbListID";
        }


        /*--------------------------------Order Function Below---------------------------------------------------------------------------*/

        private string orderByStatement()
        {
            return " Orderby " + ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;

        }

        public void BindSql(string statSpecies, string statSex, string statAge, string statBreed, string statState)
        {
            int i = rblSpecies.SelectedIndex;
            if (cblSex.SelectedIndex == -1 && rblAge.SelectedIndex == -1 && rblSpecies.SelectedItem == null && cblStateList.SelectedIndex == -1 && string.IsNullOrEmpty(tbPostCode.Text))
            {
                ResultsSqlDataSource.SelectCommand = SelectCommand();
                Session["SearchResultsSql"] = ResultsSqlDataSource.SelectCommand;
                ResultsSqlDataSource.DataBind();
                ResultsRadgrid.DataBind();
            }
            else
            {
                if (string.IsNullOrEmpty(tbPostCode.Text))
                {
                    ResultsSqlDataSource.SelectCommand = SelectCommand() + " WHERE " + statSpecies + statSex + statAge + statBreed + statState;
                    Session["SearchResultsSql"] = ResultsSqlDataSource.SelectCommand;
                    ResultsSqlDataSource.DataBind();
                    ResultsRadgrid.DataBind();
                }
                else
                {
                    ResultsSqlDataSource.SelectCommand = QueryGetAnimalListByPostCodeDistance(ddlRange.SelectedValue) + " AND " + statSpecies + statSex + statAge + statBreed + statState;
                    Session["SearchResultsSql"] = ResultsSqlDataSource.SelectCommand;
                    ResultsSqlDataSource.DataBind();
                    ResultsRadgrid.DataBind();
                }
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
            BindSql(speciesStatement(rblSpecies), sexStatement(cblSex), ageStatement(rblAge), breedStatement(cblBreed), StateStatement(cblStateList));
        }

        
        

        protected void ddlSortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //string expression = ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            //SortDirection direction = SortDirection.Ascending;
            //if (ddlDeriction.SelectedValue == "Desc")
            //{
            //    direction = SortDirection.Descending;
            //}
            //lvAnimalList.Sort(expression, direction);
        }

        protected void ddlDeriction_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string expression = ddlSortList.SelectedValue + " " + ddlDeriction.SelectedValue;
            //SortDirection direction = SortDirection.Ascending;
            //if (ddlDeriction.SelectedValue == "Desc")
            //{
            //    direction = SortDirection.Descending;
            //}

            //lvAnimalList.Sort(expression, direction);
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            Session["SearchRequest"] = "Others";
            Response.Redirect("/AnimalSearch.aspx");
            Session.Remove("SearchResultsSql");
        }

        protected void lbDetails_Click(object sender, EventArgs e)
        {
            LinkButton lbDetails = sender as LinkButton;
            Label lbID = lbDetails.Parent.FindControl("lbAnimalListID") as Label;
            Session["AnimalListID"] = lbID.Text;
            Response.Redirect("/AnimalAdoption.aspx");
        }

        /*------------------------------------------Get Public User Search Item Collection------------------------------------------*/

        private string QueryGetAnimalTypeListID(string type)
        {
            return "Select AnimalTypeListID From AnimalTypeList Where AnimalType = '" + type + "'";
        }

        private string QueryGetAnimalBreedListID(string breed)
        {
            return "Select AnimalBreedListID From AnimalBreedList Where AnimalBreed = '" + breed + "'";
        }

        private string QueryInsertAnimalBreed(string breedid, string searchid)
        {
            return "Insert Into MemberSearchHistoryBreedItemList Values (" + breedid + ", " + searchid + ")";
        }

        private string QueryInsertState(string searchid, string state)
        {
            return "Insert Into MemberSearchHistoryStateList Values (" + searchid + ", '" + state + "')";
        }

        private string QueryInsertMemberSearchHistory(string memberid, string typeid, string agefrom, string ageto, string postcode, string postrange)
        {
            return "Insert Into MemberSearchHistoryList (MemberListID, AnimalTypeListID, AgeFrom, AgeTo, PostCode, PostCodeRange) Values(" + memberid + ", " + typeid + ", " + agefrom + ", " + ageto + ", " + postcode + ", " + postrange + ")";
        }

        private string QueryGetCurrentMemberSearchHistoryListID(string memberid, string typeid, string agefrom, string ageto, string postcode, string postrange)
        {
            return "Insert Into MemberSearchHistoryList (MemberListID, AnimalTypeListID, AgeFrom, AgeTo, PostCode, PostCodeRange) Values(" + memberid + ", " + typeid + ", " + agefrom + ", " + ageto + ", " + postcode + ", " + postrange + ") select SCOPE_IDENTITY()";
        }


        private string GetFirstResByRunningQuery(string query)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return reader[0].ToString();
        }



        private string GetMemberListID()
        {
            return null == Session["MemberMemberListID"] ? "0" : Session["MemberMemberListID"].ToString();
        }

        private string GetAnimalTypeListID()
        {
            //return rblSpecies.SelectedIndex == -1 ? "0" : GetFirstResByRunningQuery(QueryGetAnimalTypeListID(rblSpecies.SelectedItem.Text));
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

        private void InsertSearchAnimalBreed()
        {
            if (cblBreed.SelectedIndex != -1)
            {
                foreach (ListItem item in cblBreed.Items)
                {
                    if (item.Selected)
                        Util.ExecuteQuery(QueryInsertAnimalBreed(GetFirstResByRunningQuery(QueryGetAnimalBreedListID(item.Text)), Session["CurrentSearchID"].ToString()));
                }
            }
            else
            {
                Util.ExecuteQuery(QueryInsertAnimalBreed("0", Session["CurrentSearchID"].ToString()));
            }
        }

        private void InsertSearchState()
        {
            if (cblStateList.SelectedIndex != -1)
            {
                foreach (ListItem item in cblStateList.Items)
                {
                    if (item.Selected)
                        Util.ExecuteQuery(QueryInsertState(Session["CurrentSearchID"].ToString(), item.Value));
                }
            }
            else
            {
                Util.ExecuteQuery(QueryInsertState(Session["CurrentSearchID"].ToString(), "NULL"));
            }
        }

        private void InsertSearchActionCollection()
        {
            Session["CurrentSearchID"] = GetFirstResByRunningQuery(QueryGetCurrentMemberSearchHistoryListID(GetMemberListID(), GetAnimalTypeListID(), GetAgeFrom(), GetAgeTo(), GetPostCode(), GetPostCodeRange()));
            InsertSearchAnimalBreed();
            InsertSearchState();
        }

        /*----------------------------------------------Telerik Gridview For AnimalList--------------------------------------------------------*/

        private string QuerySelectAnimalList(string id)
        {
            return "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID Where AnimalListID = " + id;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //InsertSearchActionCollection();
            string baseSQL = "SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID ";

            string typeSQL = "";
            string breedSQL = "";
            string sexSQL = "";
            string ageSQL = "";
            string DistanceSQL = "";
            string stateSQL = "";

            ////build where clause
            //typesql
            if (rblSpecies.SelectedIndex != -1)
            {
                typeSQL = " AnimalTypeListID = " + rblSpecies.SelectedValue.ToString();
            }
            //breed sql
            List<string> List = new List<string>();
            for (int i = 0; i < cblBreed.Items.Count; i++)
            {
                if (cblBreed.Items[i].Selected)
                {
                    List.Add(cblBreed.Items[i].Text);
                }
            }
            if (cblBreed.SelectedIndex == -1)
            {
                //breedCondition = " AND AnimalBreed NOT IN ('')";
            }
            else if (List.Count == 1)
            {
                breedSQL = " AND AnimalBreed IN ('" + cblBreed.SelectedItem.Text + "')";
            }
            else if (List.Count > 1)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (i == 0)
                    {
                        breedSQL = " AND AnimalBreed IN ('" + List[i];
                    }
                    else if (i == List.Count - 1)
                    {
                        breedSQL += "', '" + List[i] + "')";
                    }
                    else
                    {
                        breedSQL += "', '" + List[i];
                    }
                }
            }

            //sex sql
            List = new List<string>();
            for (int i = 0; i < cblSex.Items.Count; i++)
            {
                if (cblSex.Items[i].Selected)
                {
                    List.Add(cblSex.SelectedValue);
                }
            }
            if (List.Count > 1 || cblSex.SelectedIndex == -1)
            {
                //sexCondition = " AND Sex NOT IN ('')";
            }
            else
            {
                sexSQL = " AND Sex IN ('" + cblSex.SelectedValue + "')";
            }

            //age sql
            if (rblAge.SelectedIndex != -1)
            {
                ageSQL = " And Age = " + rblAge.SelectedValue;
            }

            //postcode distance sql
            if (!string.IsNullOrEmpty(tbPostCode.Text.Trim()))
            {
                double lat = GetJSONObject().results[0].geometry.location.lat;
                double lng = GetJSONObject().results[0].geometry.location.lng;
                DistanceSQL = "DECLARE @GEO1 GEOGRAPHY, @LAT VARCHAR(10), @LONG VARCHAR(10) SET @LAT='" + lat + "' SET @LONG='" + lng + "' SET @geo1= geography::Point(@LAT, @LONG, 4326) SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList ON AnimalList.SuburbListID = SuburbList.SuburbListID where(@geo1.STDistance(geography::Point(ISNULL(GPSLat, 0), ISNULL(GPSLon, 0), 4326)) / 1000) < " + ddlRange.SelectedValue;

            }

            List = new List<string>();


            //Session""
            //ResultsSqlDataSource.Select = fullsql
            //ResultsRadgrid.DataBind
            //filterInfo();

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

        protected void rgAnimalList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                RadLabel NameRadLabel = dataItem.FindControl("NameRadLabel") as RadLabel;
                RadLabel AgeRadLabel = dataItem.FindControl("AgeRadLabel") as RadLabel;
                RadLabel SexRadLabel = dataItem.FindControl("SexRadLabel") as RadLabel;
                RadLabel TypeRadLabel = dataItem.FindControl("TypeRadLabel") as RadLabel;
                RadLabel ColorRadLabel = dataItem.FindControl("ColorRadLabel") as RadLabel;
                RadLabel BreedRadLabel = dataItem.FindControl("BreedRadLabel") as RadLabel;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
                conn.Open();
                if (null != Session["SearchResultsSql"])
                {
                    SqlCommand cmd = new SqlCommand(Session["SearchResultsSql"].ToString() + " And AnimalListID = " + dataItem.GetDataKeyValue("AnimalListID").ToString(), conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        NameRadLabel.Text = reader[1].ToString();
                        AgeRadLabel.Text = reader[2].ToString();
                        SexRadLabel.Text = reader[3].ToString();
                        TypeRadLabel.Text = reader[4].ToString();
                        ColorRadLabel.Text = reader[5].ToString();
                        BreedRadLabel.Text = reader[6].ToString();
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(QuerySelectAnimalList(dataItem.GetDataKeyValue("AnimalListID").ToString()), conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        NameRadLabel.Text = reader[1].ToString();
                        AgeRadLabel.Text = reader[2].ToString();
                        SexRadLabel.Text = reader[3].ToString();
                        TypeRadLabel.Text = reader[4].ToString();
                        ColorRadLabel.Text = reader[5].ToString();
                        BreedRadLabel.Text = reader[6].ToString();
                    }
                }
                
            }
            
            //    if (cblSex.SelectedIndex == -1 && rblAge.SelectedIndex == -1 && rblSpecies.SelectedItem == null && cblStateList.SelectedIndex == -1 && string.IsNullOrEmpty(tbPostCode.Text))
            //    {
            //        SqlCommand cmd = new SqlCommand(QuerySelectAnimalList(dataItem.GetDataKeyValue("AnimalListID").ToString()), conn);
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        if (reader.Read())
            //        {
            //            NameRadLabel.Text = reader[1].ToString();
            //            AgeRadLabel.Text = reader[2].ToString();
            //            SexRadLabel.Text = reader[3].ToString();
            //            TypeRadLabel.Text = reader[4].ToString();
            //            ColorRadLabel.Text = reader[5].ToString();
            //            BreedRadLabel.Text = reader[6].ToString();
            //        }
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(tbPostCode.Text))
            //        {
            //            SqlCommand cmd = new SqlCommand(QuerySelectAnimalList(dataItem.GetDataKeyValue("AnimalListID").ToString()) + speciesStatement(rblSpecies) + sexStatement(cblSex) + ageStatement(rblAge) + breedStatement(cblBreed) + StateStatement(cblStateList), conn);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                NameRadLabel.Text = reader[1].ToString();
            //                AgeRadLabel.Text = reader[2].ToString();
            //                SexRadLabel.Text = reader[3].ToString();
            //                TypeRadLabel.Text = reader[4].ToString();
            //                ColorRadLabel.Text = reader[5].ToString();
            //                BreedRadLabel.Text = reader[6].ToString();
            //            }
            //        }
            //        else
            //        {
            //            SqlCommand cmd = new SqlCommand(QueryGetAnimalListByPostCodeDistance(ddlRange.SelectedValue) + " And AnimalListID = " + dataItem.GetDataKeyValue("AnimalListID").ToString() + speciesStatement(rblSpecies) + sexStatement(cblSex) + ageStatement(rblAge) + breedStatement(cblBreed) + StateStatement(cblStateList), conn);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                NameRadLabel.Text = reader[1].ToString();
            //                AgeRadLabel.Text = reader[2].ToString();
            //                SexRadLabel.Text = reader[3].ToString();
            //                TypeRadLabel.Text = reader[4].ToString();
            //                ColorRadLabel.Text = reader[5].ToString();
            //                BreedRadLabel.Text = reader[6].ToString();
            //            }
            //        }
            //    }

            //}
        }
    }

    //private void filterInfo()
    //{
    //    BindSql(speciesStatement(rblSpecies), sexStatement(cblSex), ageStatement(rblAge), breedStatement(cblBreed), StateStatement(cblStateList));
    //}
}
//public void BindSql(string statSpecies, string statSex, string statAge, string statBreed, string statState)
//{
//    int i = rblSpecies.SelectedIndex;
//    if (cblSex.SelectedIndex == -1 && rblAge.SelectedIndex == -1 && rblSpecies.SelectedItem == null && cblStateList.SelectedIndex == -1 && string.IsNullOrEmpty(tbPostCode.Text))
//    {
//        SqlDataSource1.SelectCommand = SelectCommand();
//        SqlDataSource1.DataBind();
//        lvAnimalList.DataBind();
//    }
//    else
//    {
//        if (string.IsNullOrEmpty(tbPostCode.Text))
//        {
//            SqlDataSource1.SelectCommand = SelectCommand() + " WHERE " + statSpecies + statSex + statAge + statBreed + statState;
//            SqlDataSource1.DataBind();
//            lvAnimalList.DataBind();
//        }
//        else
//        {
//            SqlDataSource1.SelectCommand = QueryGetAnimalListByPostCodeDistance(ddlRange.SelectedValue) + " AND " + statSpecies + statSex + statAge + statBreed + statState;
//            SqlDataSource1.DataBind();
//            lvAnimalList.DataBind();
//        }
//    }
//}