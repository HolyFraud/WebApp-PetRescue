using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace PAC.Members
{
    public partial class ManageMyAccount : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != Session["CurrentAdoptionListID"])
            {
                AddUpdateControl();
            }
            if (!IsPostBack)
            {
                
                GetMemberInfo();
            }
        }

        private void GetMemberInfo()
        {
            txtfname.Text = Util.NullToString(Session["MemberFirstName"]);
            txtmname.Text = Util.NullToString(Session["MemberMiddleName"]);
            txtlname.Text = Util.NullToString(Session["MemberLastName"]);
            txtemail.Text = Util.NullToString(Session["MemberEmail"]);
            txtphone1.Text = Util.NullToString(Session["MemberPhone1"]);
            txtphone2.Text = Util.NullToString(Session["MemberPhone2"]);
            txtaddress.Text = Util.NullToString(Session["MemberAddress"]);
            txtaddress2.Text = Util.NullToString(Session["MemberAddress2"]);
            txtsuburb.Text = Util.NullToString(Session["MemberSuburb"]);
            txtstate.Text = Util.NullToString(Session["MemberState"]);
            txtpostcode.Text = Util.NullToString(Session["MemberPostCode"]);
            txtdob.Text = Util.NullToString(Util.FormatDOB(Session["MemberDOB"].ToString()));
            ddlcountry.Text = Util.NullToString(Session["MemberCountry"]);
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string command = "UPDATE MemberList SET FirstName = '" + txtfname.Text + "', MiddleName = '" + txtmname.Text + "', LastName = '" + txtlname.Text + "', Email = '" + txtemail.Text + "', Phone1 = '" + txtphone1.Text + "', " +
                                                            "Phone2 = '" + txtphone2.Text + "', Address = '" + txtaddress.Text + "', Address2 = '" + txtaddress2.Text + "', " +
                                                            "Suburb = '" + txtsuburb.Text + "', State = '" + txtstate.Text + "', PostCode = '" + txtpostcode.Text + "', DOB = '" + Util.StringtoDatetime(txtdob.Text) + "', " +
                                                            "Country = '" + ddlcountry.Text + "' WHERE GUID = '" + Session["MemberGUID"].ToString() + "'";
            Util.ExecuteQuery(command);
        }
        /*---------------------------------------------------------------------------------------------------------------*/

        private string GetQuestionItemListCMD(string questionid)
        {
            return "SELECT QuestionItemList.QuestionItemText FROM QuestionItemList INNER JOIN QuestionList ON " +
                "QuestionItemList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTemplateList ON " +
                "QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN QuestionTypeList ON " +
                "QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE QuestionItemList.QuestionListID = " + questionid;
        }


        /*--------------------------------------------------------------Start Manually Add Web Control--------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //the parameters are -
        //Call gridviewrow, 
        //current rowindex, 
        //current questionid in current row, 
        //and current adoptionid in current row

        //Add textbox control
        private void AddTextBox(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            TextBox txt = new TextBox
            {
                ID = "txtqa" + rowindex,
                Text = GetResponseValue(questionid, adoptionid)
            };
            row.Cells[2].Controls.Add(txt);
        }

        //add checkbox control
        private void AddCheckBox(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            CheckBox chk = new CheckBox
            {
                ID = "chkqa" + rowindex
            };
            if ("YES" == GetResponseValue(questionid, adoptionid))
                chk.Checked = true;
            row.Cells[2].Controls.Add(chk);
        }

        
        //add dropdownlist control
        private void AddDropDownList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            DropDownList ddl = new DropDownList
            {
                ID = "ddlqa" + rowindex
            };
            SqlDataSource4.SelectCommand = GetQuestionItemListCMD(questionid);
            ddl.DataSource = SqlDataSource4;
            ddl.DataTextField = "QuestionItemText";
            ddl.DataValueField = "QuestionItemText";
            ddl.DataBind();
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == GetResponseValue(questionid, adoptionid))
                {
                    item.Selected = true;
                }
            }
            row.Cells[2].Controls.Add(ddl);
        }

        //add radiobuttonlist control
        private void AddRadioButtonList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            RadioButtonList rbl = new RadioButtonList
            {
                ID = "rblqa" + rowindex
            };
            SqlDataSource4.SelectCommand = GetQuestionItemListCMD(questionid);
            rbl.DataSource = SqlDataSource4;
            rbl.DataTextField = "QuestionItemText";
            rbl.DataValueField = "QuestionItemText";
            rbl.DataBind();
            foreach (ListItem item in rbl.Items)
            {
                if (item.Value == GetResponseValue(questionid, Session["CurrentAdoptionListId"].ToString()))
                    item.Selected = true;
            }
            row.Cells[2].Controls.Add(rbl);
        }

        //add checkboxlist control
        private void AddCheckBoxList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            CheckBoxList cbl = new CheckBoxList();
            cbl.ID = "cblqa" + rowindex;
            SqlDataSource4.SelectCommand = GetQuestionItemListCMD(questionid);
            cbl.DataSource = SqlDataSource4;
            cbl.DataTextField = "QuestionItemText";
            cbl.DataValueField = "QuestionItemText";
            cbl.DataBind();
            List<string> listItems = new List<string>();
            GetResponseValueList(questionid, Session["CurrentAdoptionListId"].ToString(), listItems);
            foreach (ListItem item in cbl.Items)
            {
                for (int i = 0; i < listItems.Count; i++)
                {
                    if (item.Value == listItems[i])
                    {
                        item.Selected = true;
                    }
                }
            }
            row.Cells[2].Controls.Add(cbl);
        }



        //this method is using "add control" method above 
        private void AddUpdateControl()
        {
            GridView gvQuestions = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                string questionID = GetQuestionListID(row.RowIndex);
                string questiontypeid = GetQuestionTypeListID(questionID);
                string type = GetQuestionType(questiontypeid);
                switch (type)
                {
                    case "Textbox":
                        AddTextBox(row, row.RowIndex, questionID, Session["CurrentAdoptionListId"].ToString());
                        break;
                    case "Checkbox":
                        AddCheckBox(row, row.RowIndex, questionID, Session["CurrentAdoptionListId"].ToString());
                        break;
                    case "RadioButtonList":
                        AddRadioButtonList(row, row.RowIndex, questionID, Session["CurrentAdoptionListId"].ToString());
                        break;
                    case "CheckBoxList":
                        AddCheckBoxList(row, row.RowIndex, questionID, Session["CurrentAdoptionListId"].ToString());
                        break;
                    case "DropDownList":
                        AddDropDownList(row, row.RowIndex, questionID, Session["CurrentAdoptionListId"].ToString());
                        break;
                    default:
                        break;
                }
            }
        }

        /*-----------------------------------------------------End Manually Add Web Control---------------------------*/

        /*-----------------------------------------------------Start Read sql server data----------------------------*/

        //get questiontype by using typelistID
        private string GetQuestionType(string questionTypeListID)
        {
            string command = "SELECT QuestionType FROM QuestionTypeList WHERE QuestionTypeListID = " + questionTypeListID;
            string questionType = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    questionType = reader["QuestionType"].ToString();
                }
                conn.Close();
            }
            return questionType;
        }

        //Get question type id by using questionID
        private string GetQuestionTypeListID(string questionListID)
        {
            string command = "SELECT QuestionTypeListID FROM QuestionList WHERE QuestionListID = " + questionListID;
            string questionTypeListID = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    questionTypeListID = reader["QuestionTypeListID"].ToString();
                }
                conn.Close();
            }
            return questionTypeListID;
        }
        
        //Get question response value id by using questionid and adoptionid
        private string GetResponseValueID(string questionid, string adoptionid)
        {
            string command = "select QuestionResponseListID from QuestionResponseList where QuestionListID = " + questionid + " And AdoptionListID = " + adoptionid;
            string responseId = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            responseId = reader[0].ToString();
            conn.Close();
            return responseId;
        }

        //get question response value by using questionid and adoptionid
        private string GetResponseValue(string questionid, string adoptionid)
        {
            string command = "select responsevalue from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            string responseValue = ""; SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            responseValue = reader[0].ToString();
            conn.Close();
            return responseValue;
        }

        //Get question response value collections
        private void GetResponseValueList(string questionid, string adoptionid, List<string> responseValuelist)
        {
            string command = "select responsevalue from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    responseValuelist.Add(reader[0].ToString());
                }
            }
            reader.Close();
            conn.Close();
        }

        //get collections
        private List<string> GetResponseValueIDList(string questionid, string adoptionid, List<string> responseidList)
        {
            string command = "select QuestionResponseListID from QuestionResponseList where QuestionListID = " + questionid + " and AdoptionListID = " + adoptionid;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(command, conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    responseidList.Add(reader[0].ToString());
                }
            }
            return responseidList;
        }

        //using parameters by method above and generate a sql query below
        private string UpdateResponseValueCBLCommand(string value, string questionid, string adoptionid, List<string> responseidList, int idIndex)
        {
            if (value == null)
                return "UPDATE QuestionResponseList SET ResponseValue = null WHERE QuestionResponseListID = " + GetResponseValueIDList(questionid, adoptionid, responseidList)[idIndex];
            return "UPDATE QuestionResponseList SET ResponseValue = '" + value + "' WHERE QuestionResponseListID = " + GetResponseValueIDList(questionid, adoptionid, responseidList)[idIndex];
        }
        //using parameters by method above and generate a sql query below
        private string UpdateResponseValueCommand(string value, string questionid, string adoptionid)
        {
            return "UPDATE QuestionResponseList SET ResponseValue = '" + value + "' WHERE QuestionResponseListID = " + GetResponseValueID(questionid, adoptionid);
        }
        /*------------------------------------------------End sql server data read-----------------------------------------------------------*/

        /*-----------------------------------------------Start save the question response value into sql server database--------*/
        
        //store current row question response value.
        private void SaveCurrentRowUpdateResponseValue(int RowIndex)
        {
            GridView gv = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            string controlID = Util.controlList[RowIndex].ID;
            List<string> responseIdList = new List<string>();

            if (Util.controlList[RowIndex] is DropDownList)
            {
                DropDownList ddl = gv.Rows[RowIndex].FindControl(controlID) as DropDownList;
                Util.ExecuteQuery(UpdateResponseValueCommand(ddl.SelectedValue, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
                
            }
            else if (Util.controlList[RowIndex] is TextBox)
            {
                TextBox txt = gv.Rows[RowIndex].FindControl(controlID) as TextBox;
                Util.ExecuteQuery(UpdateResponseValueCommand(txt.Text, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is CheckBox)
            {
                CheckBox chk = gv.Rows[RowIndex].FindControl(controlID) as CheckBox;
                if (chk.Checked)
                    Util.ExecuteQuery(UpdateResponseValueCommand("YES", GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
                else
                    Util.ExecuteQuery(UpdateResponseValueCommand("NO", GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is RadioButtonList)
            {
                RadioButtonList rbl = gv.Rows[RowIndex].FindControl(controlID) as RadioButtonList;
                Util.ExecuteQuery(UpdateResponseValueCommand(rbl.SelectedValue, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is CheckBoxList)
            {
                
                CheckBoxList cbl = gv.Rows[RowIndex].FindControl(controlID) as CheckBoxList;
                
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Selected)
                        Util.ExecuteQuery(UpdateResponseValueCBLCommand(cbl.Items[i].Value, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), responseIdList, i));
                    else
                        Util.ExecuteQuery(UpdateResponseValueCBLCommand(null, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), responseIdList, i));
                }
            }
            
        }

        //save all response value by for loop all row question response
        private void SaveAllUpdateResponseValue()
        {
            GridView gvQuestions = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            GridView gv = FVAppDetails.FindControl("GVQuestion") as GridView;
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                SaveCurrentRowUpdateResponseValue(row.RowIndex);
            }
            gv.DataBind();
        }
        /*--------------------------------End save response value to sql server database----------------------------------*/


        private string GetQuestionListID(int rowIndex)
        {
            GridView gvQuestions = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            string questionid = gvQuestions.DataKeys[rowIndex].Values[0].ToString();
            return questionid;
        }

        private void ControlTableVisible(bool vsb1, bool vsb2)
        {
            GridView gvQuestion = FVAppDetails.FindControl("GVQuestion") as GridView;
            GridView gvQuestionUpdate = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            Button btnEdit = FVAppDetails.FindControl("BtnEdit") as Button;
            Button btnCancel = FVAppDetails.FindControl("BtnCancel") as Button;
            Button btnSave = FVAppDetails.FindControl("BtnSave") as Button;
            gvQuestion.Visible = vsb1;
            btnEdit.Visible = vsb1;
            gvQuestionUpdate.Visible = vsb2;
            btnCancel.Visible = vsb2;
            btnSave.Visible = vsb2;
        }

        private List<Control> ControlList()
        {
            GridView gvQuestions = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;

            foreach (GridViewRow row in gvQuestions.Rows)
            {
                Control ctrl = row.Cells[2].Controls[0];
                Util.controlList.Add(ctrl);
            }

            return Util.controlList;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            ControlTableVisible(false, true);
            ControlList();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            SaveAllUpdateResponseValue();
            ControlTableVisible(true, false);
        }

        protected void BtnDetail_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            Session["CurrentAdoptionListID"] = GVAdoptionlist.DataKeys[row.RowIndex].Values[0];
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ControlTableVisible(true, false);
            Util.controlList.Clear();
        }
    }
}
