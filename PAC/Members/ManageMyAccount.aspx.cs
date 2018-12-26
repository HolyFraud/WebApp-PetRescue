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
        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        private void AddTextBox(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            TextBox txt = new TextBox
            {
                ID = "txtqa" + rowindex,
                Text = Util.GetResponseValue(questionid, adoptionid)
            };
            row.Cells[2].Controls.Add(txt);
        }

        private void AddCheckBox(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            CheckBox chk = new CheckBox
            {
                ID = "chkqa" + rowindex
            };
            if ("YES" == Util.GetResponseValue(questionid, adoptionid))
                chk.Checked = true;
            row.Cells[2].Controls.Add(chk);
        }

        private void AddDropDownList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            DropDownList ddl = new DropDownList
            {
                ID = "ddlqa" + rowindex
            };
            SqlDataSource4.SelectCommand = Util.GetQuestionItemListSelectCMD(questionid);
            ddl.DataSource = SqlDataSource4;
            ddl.DataTextField = "QuestionItemText";
            ddl.DataValueField = "QuestionItemText";
            ddl.DataBind();
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == Util.GetResponseValue(questionid, adoptionid))
                {
                    item.Selected = true;
                }
            }
            row.Cells[2].Controls.Add(ddl);
        }

        private void AddRadioButtonList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            RadioButtonList rbl = new RadioButtonList
            {
                ID = "rblqa" + rowindex
            };
            SqlDataSource4.SelectCommand = Util.GetQuestionItemListSelectCMD(questionid);
            rbl.DataSource = SqlDataSource4;
            rbl.DataTextField = "QuestionItemText";
            rbl.DataValueField = "QuestionItemText";
            rbl.DataBind();
            foreach (ListItem item in rbl.Items)
            {
                if (item.Value == Util.GetResponseValue(questionid, Session["CurrentAdoptionListId"].ToString()))
                    item.Selected = true;
            }
            row.Cells[2].Controls.Add(rbl);
        }

        private void AddCheckBoxList(GridViewRow row, int rowindex, string questionid, string adoptionid)
        {
            row.Cells[2].Controls.Clear();
            CheckBoxList cbl = new CheckBoxList();
            cbl.ID = "cblqa" + rowindex;
            SqlDataSource4.SelectCommand = Util.GetQuestionItemListSelectCMD(questionid);
            cbl.DataSource = SqlDataSource4;
            cbl.DataTextField = "QuestionItemText";
            cbl.DataValueField = "QuestionItemText";
            cbl.DataBind();
            List<string> listItems = new List<string>();
            Util.GetResponseValueList(questionid, Session["CurrentAdoptionListId"].ToString(), listItems);
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
        
        private void AddUpdateControl()
        {
            GridView gvQuestions = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                string questionID = GetQuestionListID(row.RowIndex);
                string type = Util.GetQuestionType(Util.GetQuestionTypeListID(questionID));
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

        private void SaveCurrentRowUpdateResponseValue(int RowIndex)
        {
            GridView gv = FVAppDetails.FindControl("gvQuestionsUpdate") as GridView;
            string controlID = Util.controlList[RowIndex].ID;
            List<string> responseIdList = new List<string>();
            if (Util.controlList[RowIndex] is DropDownList)
            {
                DropDownList ddl = gv.Rows[RowIndex].FindControl(controlID) as DropDownList;
                Util.ExecuteQuery(Util.UpdateResponseValueCommand(ddl.SelectedValue, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
                
            }
            else if (Util.controlList[RowIndex] is TextBox)
            {
                TextBox txt = gv.Rows[RowIndex].FindControl(controlID) as TextBox;
                Util.ExecuteQuery(Util.UpdateResponseValueCommand(txt.Text, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is CheckBox)
            {
                CheckBox chk = gv.Rows[RowIndex].FindControl(controlID) as CheckBox;
                if (chk.Checked)
                    Util.ExecuteQuery(Util.UpdateResponseValueCommand("YES", GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
                else
                    Util.ExecuteQuery(Util.UpdateResponseValueCommand("NO", GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is RadioButtonList)
            {
                RadioButtonList rbl = gv.Rows[RowIndex].FindControl(controlID) as RadioButtonList;
                Util.ExecuteQuery(Util.UpdateResponseValueCommand(rbl.SelectedValue, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString()));
            }
            else if (Util.controlList[RowIndex] is CheckBoxList)
            {
                
                CheckBoxList cbl = gv.Rows[RowIndex].FindControl(controlID) as CheckBoxList;
                
                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Selected)
                        Util.ExecuteQuery(Util.UpdateResponseValueCBLCommand(cbl.Items[i].Value, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), responseIdList, i));
                    else
                        Util.ExecuteQuery(Util.UpdateResponseValueCBLCommand(null, GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), responseIdList, i));
                }
            }
            
        }

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
