using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC
{
    public partial class AnimalAdoption : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HideGridView();
            if (!IsPostBack)
            {
                Session.Remove("CurrentAdoptionListID");
            }
        }

        private void ControlVisiblilty(bool vsb1, bool vsb2)
        {
            GridView gvQuestions = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            Button btnComplete = fvAnimalDetails.FindControl("btnComplete") as Button;
            Button btnCancel = fvAnimalDetails.FindControl("btnCancel") as Button;
            Button btnApply = fvAnimalDetails.FindControl("btnApply") as Button;
            gvQuestions.Visible = vsb1;
            btnComplete.Visible = vsb1;
            btnCancel.Visible = vsb1;
            btnApply.Visible = vsb2;
        }

        protected void BtnApply_Click(object sender, EventArgs e)
        {
            if (Session["MemberMemberListID"] != null)
            {
                ControlVisiblilty(true, false);
                lb.Text = "Apply Success...!";
                ControlList();
            }
            else
            {
                lb.Text = "Please Login first...!";
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ControlVisiblilty(false, true);
            Util.controlList.Clear();
            Session.Remove("CurrentAdoptionListID");
        }
        
        protected void BtnComplete_Click(object sender, EventArgs e)
        {
            //lb.Text = Session["AnimalListID"].ToString();
            InsertNewAdoption();
            InsertAllResponseValue();
            Util.controlList.Clear();
            Session.Remove("CurrentAdoptionListID");
            Response.Redirect("/Members/ManageMyAccount.aspx");
        }
        

        private TextBox AddTextBox(int id)
        {
            TextBox txtqa = new TextBox
            {
                ID = "txtqa" + id,
                Text = "Enter Answer Here...!"
            };
            return txtqa;
        }

        private CheckBox AddCheckBox(int id)
        {
            CheckBox chkqa = new CheckBox
            {
                ID = "chkqa" + id,
                Text = "Yes"
            };
            return chkqa;
        }

        private string GetListSelectCommand(int questionListID)
        {
            string state = "";
            return state = "SELECT QuestionItemList.QuestionItemText FROM QuestionItemList INNER JOIN QuestionList ON " +
                "QuestionItemList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTemplateList ON " +
                "QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN QuestionTypeList ON " +
                "QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE QuestionItemList.QuestionListID = " + questionListID;
            
        }

        private Control AddList(int id, string listType, int questionListID)
        {
            SqlGetList.SelectCommand = GetListSelectCommand(questionListID);
            if (listType == "DropDownList")
            {
                DropDownList ddlqa = new DropDownList
                {
                    ID = "ddlqa" + id
                };
                ddlqa.DataSource = SqlGetList;
                ddlqa.DataTextField = "QuestionItemText";
                ddlqa.DataValueField = "QuestionItemText";
                ddlqa.DataBind();
                return ddlqa;
            }
            else if (listType == "CheckBoxList")
            {
                CheckBoxList cblqa = new CheckBoxList
                {
                    ID = "cblqa" + id
                };
                cblqa.DataSource = SqlGetList;
                cblqa.DataTextField = "QuestionItemText";
                cblqa.DataValueField = "QuestionItemText";
                cblqa.DataBind();
                return cblqa;
            }
            else if (listType == "RadioButtonList")
            {
                RadioButtonList rblqa = new RadioButtonList
                {
                    ID = "rblqa" + id
                };
                rblqa.DataSource = SqlGetList;
                rblqa.DataTextField = "QuestionItemText";
                rblqa.DataValueField = "QuestionItemText";
                rblqa.DataBind();
                return rblqa;
            }
            return null;
        }

        private List<Control> ControlList()
        {
            GridView gvQuestions = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                Control ctrl = row.Cells[2].Controls[0];
                Util.controlList.Add(ctrl);
            }
            return Util.controlList;
        }

        private void InsertNewAdoption()
        {
            string AdoptionListID = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO AdoptionList(AnimalListID, MemberListID) VALUES('" + Session["AnimalListID"].ToString() + "', '" + Session["MemberMemberListID"].ToString() + "') SELECT SCOPE_IDENTITY();", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            AdoptionListID = reader[0].ToString();
            reader.Close();
            conn.Close();
            Session["CurrentAdoptionListID"] = AdoptionListID;
        }

        private int GetQuestionListID(int rowIndex)
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            int questionID = Convert.ToInt32(gv.DataKeys[rowIndex].Values[0]);
            return questionID;
        }

        private void InsertCurrentRowResponse(int RowIndex)
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            string controlID = Util.controlList[RowIndex].ID;
            if (Util.controlList[RowIndex] is DropDownList)
            {
                DropDownList ddl = gv.Rows[RowIndex].FindControl(controlID) as DropDownList;
                Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), ddl.SelectedValue));
                
            }
            else if (Util.controlList[RowIndex] is TextBox)
            {
                TextBox txt = gv.Rows[RowIndex].FindControl(controlID) as TextBox;
                Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), txt.Text));
            }
            else if (Util.controlList[RowIndex] is CheckBox)
            {
                CheckBox chk = gv.Rows[RowIndex].FindControl(controlID) as CheckBox;
                if (chk.Checked)
                    Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), "YES"));
                else
                    Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), "NO"));
            }
            else if (Util.controlList[RowIndex] is RadioButtonList)
            {
                RadioButtonList rbl = gv.Rows[RowIndex].FindControl(controlID) as RadioButtonList;
                Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), rbl.SelectedItem.Value));
            }
            else if (Util.controlList[RowIndex] is CheckBoxList)
            {
                CheckBoxList cbl = gv.Rows[RowIndex].FindControl(controlID) as CheckBoxList;
                foreach (ListItem item in cbl.Items)
                {
                    if (item.Selected)
                        Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), item.Value));
                    else
                        Util.ExecuteQuery(Util.InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), null));
                }
            }
            
        }

        private void InsertAllResponseValue()
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                InsertCurrentRowResponse(i);
            }
        }

        private void HideGridView()
        {
            GridView gvQuestions = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string answerType = Util.GetQuestionType(Util.GetQuestionTypeListID(gvQuestions.DataKeys[row.RowIndex].Values[0].ToString()));
                    switch (answerType)
                    {
                        case "Textbox":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddTextBox(row.RowIndex));
                            break;
                        case "Checkbox":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddCheckBox(row.RowIndex));
                            break;
                        case "RadioButtonList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddList(row.RowIndex, "RadioButtonList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
                            break;
                        case "CheckBoxList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddList(row.RowIndex, "CheckBoxList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
                            break;
                        case "DropDownList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddList(row.RowIndex, "DropDownList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
                            break;
                        default:
                            break;
                    }
                }
            }
            gvQuestions.Visible = false;
        }

    }
}