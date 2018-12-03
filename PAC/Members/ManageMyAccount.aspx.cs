using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace PAC.Members
{
    public partial class ManageMyAccount : System.Web.UI.Page
    {
        private int tmpEditIndex;
        private string inputType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tmpEditIndex = -1;
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


        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();

            string command = "UPDATE MemberList SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Email = @Email, Phone1 = @Phone1, " +
                                                            "Phone2 = @Phone2, Address = @Address, Address2 = @Address2, " +
                                                            "Suburb = @Suburb, State = @State, PostCode = @PostCode, DOB = @DOB, " +
                                                            "Country = @Country WHERE GUID = '" + Session["MemberGUID"].ToString() + "'";

            SqlCommand cmd = new SqlCommand(command, sqlConnection);

            cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
            cmd.Parameters.AddWithValue("@MiddleName", txtmname.Text);
            cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
            cmd.Parameters.AddWithValue("@Email", txtemail.Text);
            cmd.Parameters.AddWithValue("@Phone1", txtphone1.Text);
            cmd.Parameters.AddWithValue("@Phone2", txtphone2.Text);
            cmd.Parameters.AddWithValue("@Address", txtaddress.Text);
            cmd.Parameters.AddWithValue("@Address2", txtaddress2.Text);
            cmd.Parameters.AddWithValue("@Suburb", txtsuburb.Text);
            cmd.Parameters.AddWithValue("@State", txtstate.Text);
            cmd.Parameters.AddWithValue("@PostCode", txtpostcode.Text);
            cmd.Parameters.AddWithValue("@DOB", Util.StringtoDatetime(txtdob.Text));
            cmd.Parameters.AddWithValue("@Country", ddlcountry.Text);

            cmd.ExecuteNonQuery();
            sqlConnection.Close();

        }

        protected void GVQuestion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView GVQuestion = FVAppDetails.FindControl("GVQuestion") as GridView;
            GVQuestion.EditIndex = e.NewEditIndex;
            tmpEditIndex = e.NewEditIndex;
            inputType = GVQuestion.Rows[tmpEditIndex].Cells[3].Text;


        }

        protected void GVQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow) {
                if (e.Row.RowIndex == tmpEditIndex)
                {
                    TextBox txt = (TextBox)e.Row.FindControl("txttype");
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddltype");

                    if (txt != null && ddl != null)
                    {
                        if (string.Equals(inputType, "Textbox"))
                        {
                            txt.Visible = true;
                            ddl.Visible = false;
                        }
                        if (string.Equals(inputType, "DropDownList"))
                        {
                            txt.Visible = false;
                            ddl.Visible = true;
                        }

                    }
                }
            }
        }
    }
}

        /**protected void GVQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridView gv = sender as GridView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txttype = (e.Row.FindControl("txttype") as TextBox);
                txttype.Visible = false;
            }
            
        }*/

        /**protected void GVQuestion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVQuestion.Rows[i];
            TextBox txttype = (row.FindControl("txttype") as TextBox);
            string t = txttype.Text;
        }*/

        /**protected void GVQuestion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gv = sender as GridView;
            TextBox txtAddress = gv.Rows[e.NewEditIndex].FindControl("txttype") as TextBox;
            var xxx = 1;
            /*int i = e.NewEditIndex;
            //GridView gv = sender as GridView;
            //GridView GVQuestion = gv.Parent.FindControl("GVQuestion") as GridView;
            //GridViewRow row = GVQuestion.Rows[e.NewEditIndex];
            //TextBox txttype = sender as TextBox;
            
            
            Label2.Text = i.ToString();
            string inputType = GVQuestion.Rows[i].Cells[3].Text;
            
            //TextBox txt = GVQuestion.Rows[e.NewEditIndex].FindControl("txttype") as TextBox;
            //TextBox txttype = (TextBox)GVQuestion.Rows[i].Cells[2].FindControl("txttype");
            //TextBox txttype = GVQuestion.FindControl("txttype") as TextBox;
            
            if (string.Equals(inputType, "Textbox"))
            {
                (TextBox)GVQuestion.Rows[i].Cells[2].FindControl("txttype");
                //txttype.Visible = false;
            }

            /**TextBox txttype = GVQuestion.Rows[i + 1].FindControl("txttype") as TextBox;
            string test = GVQuestion.Rows[i + 1].Cells[3].Text;
            
            if (GVQuestion.Rows[i + 1].Cells[3].ToString().Equals("TextBox"))
            {
                
                txttype.Visible = true;
            }
            if(GVQuestion.Rows[i + 1].Cells[3].Equals("DropDownList"))
            {
                DropDownList ddl = new DropDownList();
                ddl = sender as DropDownList;
                DropDownList ddltype = new DropDownList();
                ddltype = ddl.Parent.FindControl("ddltype") as DropDownList;
                ddltype.SelectedIndex = 0;
                ddltype.SelectedValue = "Good";
                
            }
        

    }
    
}*/