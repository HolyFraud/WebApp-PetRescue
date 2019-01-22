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
            RenderGridView();
            if (!IsPostBack)
            {
                Session.Remove("CurrentAdoptionListID");
            }
        }

        //this method control all web controls visibility
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

        //Render all controls needed
        private List<Control> RenderControlsList()
        {
            GridView gvQuestions = fvAnimalDetails.FindControl("gvQuestions") as GridView;

            foreach (GridViewRow row in gvQuestions.Rows)
            {
                Control ctrl = row.Cells[2].Controls[0];
                Util.controlList.Add(ctrl);
            }
            return Util.controlList;
        }

        protected void BtnApply_Click(object sender, EventArgs e)
        {
            if (Session["MemberMemberListID"] != null)
            {
                ControlVisiblilty(true, false);
                RenderControlsList();
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
            InsertNewAdoption();
            InsertAllResponseValue();
            Util.controlList.Clear();//using static List because we dont want to auto new List in page_load(), so we need manually clear the List item
            Session.Remove("CurrentAdoptionListID");
            Response.Redirect("/Members/ManageMyAccount.aspx");
        }
        
        /*--------------------Start manually add web controls---------------------------*/
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

        private Control AddControlList(int id, string listType, int questionListID)
        {
            SqlGetList.SelectCommand = GetQuestionItemListSelectCommand(questionListID);
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
        /*--------------End manually add web controls--------------------------*/

        private string GetQuestionItemListSelectCommand(int questionListID)
        {
            return "SELECT QuestionItemList.QuestionItemText FROM QuestionItemList INNER JOIN QuestionList ON " +
                "QuestionItemList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTemplateList ON " +
                "QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN QuestionTypeList ON " +
                "QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE QuestionItemList.QuestionListID = " + questionListID;

        }

        
        //get question id by using current row index
        private int GetQuestionListID(int rowIndex)
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            int questionID = Convert.ToInt32(gv.DataKeys[rowIndex].Values[0]);
            return questionID;
        }

        /*------------------start Get Parameter By reading sql command----------------------*/
        private bool RadioButtonListNotSelected(int questionid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(QuestionItemList.QuestionItemText) FROM QuestionItemList INNER JOIN QuestionList ON QuestionItemList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTemplateList ON QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN QuestionTypeList ON QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE QuestionItemList.QuestionListID = " + questionid, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) > 0) return false;
            }
            return true;
        }

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
        /*------------------End Get Parameter By reading sql command----------------------*/

        //Start Insert Adoption Recode
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

        //Insert new question response value recorde
        private string InsertNewResponseValue(int questionid, string adoptionid, string controlvalue)
        {
            if (controlvalue == null)
                return "INSERT INTO QuestionResponseList(QuestionListID, AdoptionListID, ResponseValue) VALUES(" + questionid + ", " + adoptionid + ", null)";
            return "INSERT INTO QuestionResponseList(QuestionListID, AdoptionListID, ResponseValue) VALUES(" + questionid + ", " + adoptionid + ", '" + controlvalue + "')";
        }

        //insert current row question response value
        private void InsertCurrentRowResponse(int RowIndex)
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;

            string controlID = Util.controlList[RowIndex].ID;
            

            if (Util.controlList[RowIndex] is DropDownList)
            {
                DropDownList ddl = gv.Rows[RowIndex].FindControl(controlID) as DropDownList;
                Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), ddl.SelectedValue));
                
            }
            else if (Util.controlList[RowIndex] is TextBox)
            {
                TextBox txt = gv.Rows[RowIndex].FindControl(controlID) as TextBox;
                Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), txt.Text));
            }
            else if (Util.controlList[RowIndex] is CheckBox)
            {
                CheckBox chk = gv.Rows[RowIndex].FindControl(controlID) as CheckBox;
                if (chk.Checked)
                    Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), "YES"));
                else
                    Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), "NO"));
            }
            else if (Util.controlList[RowIndex] is RadioButtonList)
            {
                RadioButtonList rbl = gv.Rows[RowIndex].FindControl(controlID) as RadioButtonList;
                if (rbl.SelectedIndex == -1)
                {
                    string value = "";
                    Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), value));
                }
                else
                    Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), rbl.SelectedItem.Value));
                
            }
            else if (Util.controlList[RowIndex] is CheckBoxList)
            {
                CheckBoxList cbl = gv.Rows[RowIndex].FindControl(controlID) as CheckBoxList;
                foreach (ListItem item in cbl.Items)
                {
                    if (item.Selected)
                        Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), item.Value));
                    else
                        Util.ExecuteQuery(InsertNewResponseValue(GetQuestionListID(RowIndex), Session["CurrentAdoptionListID"].ToString(), null));
                }
            }
            
        }

        //for loop all gridview row and insert all question response value
        private void InsertAllResponseValue()
        {
            GridView gv = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                InsertCurrentRowResponse(i);
            }
        }

        
        //render question gridview but the visible is false because this is the first time post back on the page_load()
        private void RenderGridView()
        {
            GridView gvQuestions = fvAnimalDetails.FindControl("gvQuestions") as GridView;
            
            foreach (GridViewRow row in gvQuestions.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string questiontypeid = GetQuestionTypeListID(gvQuestions.DataKeys[row.RowIndex].Values[0].ToString());
                    string questiontype = GetQuestionType(questiontypeid);
                    string answerType = questiontype;
                    switch (answerType)
                    {
                        case "Textbox":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddTextBox(row.RowIndex));
                            break;
                        case "Checkbox":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddCheckBox(row.RowIndex));
                            break;
                        case "RadioButtonList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddControlList(row.RowIndex, "RadioButtonList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
                            break;
                        case "CheckBoxList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddControlList(row.RowIndex, "CheckBoxList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
                            break;
                        case "DropDownList":
                            gvQuestions.Rows[row.RowIndex].Cells[2].Controls.Add(AddControlList(row.RowIndex, "DropDownList", Convert.ToInt32(gvQuestions.DataKeys[row.RowIndex].Values[0])));
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