using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAC.Advertisers
{
    public partial class AdvertiserUsersManage : System.Web.UI.Page
    {
        
        Includes inc = new Includes();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            GenerateAdsIDSession();
            ControlLoginUserAuthority();
        }


        private string GetAdvertiserListIDQuery()
        {
            return "Select AdvertiserListID FROM AdvertiserUserList WHERE EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetAdvertiserUserListIDQuery()
        {
            return "Select AdvertiserUserListID From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetIsAdminValueQuery()
        {
            return "SELECT IsAdmin FROM AdvertiserUserList WHERE EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string GetIsAdminValueByEmailQuery()
        {
            return "Select IsAdmin From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string InsertNewAdsUserQuery()
        {
            return "INSERT INTO AdvertiserUserList (AdvertiserListID, FirstName, LastName, EmailAddress, Phone1, Phone2, CreatedBy, IsAdmin, Password, GUID, SecurityMask) Values(" + Session["AdsListID"].ToString() + ", '" + txtFName.Text + "', '" + txtLName.Text + "', '" + txtEmail.Text + "', '" + txtPhone1.Text + "', '" + txtPhone2.Text + "', '" + GetAdvertiserUserListID() + "', " + IsAdminSelected() + ", '" + txtpswd.Text + "', '" + Util.NewGuid() + "', " + CalculateSecurityMask() + ")";
        }

        private string ActiveAdsUserQuery()
        {
            return "Update AdvertiserUserList Set FirstName = '" + txtFName.Text + "', LastName = '" + txtLName.Text + "', Phone1 = '" + txtPhone1.Text + "', Phone2 = '" + txtPhone2.Text + "', Password = '" + txtpswd.Text + "', CreatedBy = " + GetAdvertiserUserListID() + ", IsAdmin = " + IsAdminSelected() + ", SecurityMask = " + CalculateSecurityMask() + ", RecordStatus = 1 Where EmailAddress = '" + txtEmail.Text + "'";
        }

        private string GetAdsNewUserGuidQuery()
        {
            return "Select GUID From AdvertiserUserList Where EmailAddress = '" + txtEmail.Text + "'";
        }

        private string GetCurrentAdsUserGuidQuery()
        {
            return "Select GUID From AdvertiserUserList Where EmailAddress = '" + Session["AdsEmail"].ToString() + "'";
        }

        private string ActiveUserIfIsMyselfQuery(string id)
        {
            return "Update AdvertiserUserList Set RecordStatus = 1 Where AdvertiserUserListID = " + id;
        }

        private string DeactivateUserQuery(string id)
        {
            return "Update AdvertiserUserList Set RecordStatus = 0 Where AdvertiserUserListID = " + id;
        }

        private string UpdateAdsUserSecurityMask(int securitymask, string id)
        {
            return "Update AdvertiserUserList Set SecurityMask = " + securitymask + " Where AdvertiserUserListID = " + id;
        }

        private void ExecuteInsertNewUserQuery()
        {
            Util.ExecuteQuery(InsertNewAdsUserQuery());
        }
        
        private void ExecuteActiveUserQuery()
        {
            Util.ExecuteQuery(ActiveAdsUserQuery());
        }


        private string GetAdvertiserUserListID()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetAdvertiserUserListIDQuery(), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = reader[0].ToString();
            reader.Close();
            conn.Close();
            return res;
        }

        private string GetAdvertiserListID()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GetAdvertiserListIDQuery(), conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = reader[0].ToString();
            reader.Close();
            conn.Close();
            return res;
        }

        private string GetAdsNewUserGuid(string query)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string res = (string)reader[0];
            conn.Close();
            return res;
        }

        private int GetIsAdminValue(string query)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int res = Convert.ToInt32(reader[0]);
            conn.Close();
            return res;
        }

        /*--------Use Admin Flag to check current user is admin or not--------*/
        private bool IsAdminFlag(int flag)
        {
            if (flag == 1) return true;
            return false;
        }

        private int IsAdminSelected()
        {
            if (!chkIsAdmin.Checked)
            {
                return 0;
            }
            return 1;
        }

        private void Calculator(int securitymask, CheckBoxList cbl)
        {
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    securitymask += Convert.ToInt32(item.Value);
                }
            }
        }

        private int CalculateSecurityMask()
        {
            int securitymask = 0;
            if (IsAdminSelected() == 1)
            {
                return 63;
            }
            else
            {
                Calculator(securitymask, cblAuthControl);
            }
            return securitymask;
        }

        private bool CanViewAdsUserList()
        {
            int securitymask = Convert.ToInt32(Session["CurrentUserSecurityMask"]);
            if (inc.CanAddUsers(securitymask)) return true;
            if (inc.CanEditUsers(securitymask)) return true;
            if (inc.CanDeleteUsers(securitymask)) return true;
            return false;
        }

        private bool CanViewAdsList()
        {
            int securitymask = Convert.ToInt32(Session["CurrentUserSecurityMask"]);
            if (inc.CanAddAds(securitymask)) return true;
            if (inc.CanEditAds(securitymask)) return true;
            if (inc.CanDeleteAds(securitymask)) return true;
            return false;
        }

        private bool CanEditAdsUserList()
        {
            int securitymask = Convert.ToInt32(Session["CurrentUserSecurityMask"]);
            if (inc.CanEditUsers(securitymask))
            {
                return true;
            }
            return false;
        }

        private bool CanDeleteAdsUserList()
        {
            int securitymask = Convert.ToInt32(Session["CurrentUserSecurityMask"]);
            if (inc.CanDeleteUsers(securitymask))
            {
                return true;
            }
            return false;
        }

        private bool CanAddAdsUser()
        {
            int securitymask = Convert.ToInt32(Session["CurrentUserSecurityMask"]);
            if (inc.CanAddUsers(securitymask))
            {
                return true;
            }
            return false;
        }

        private void HideAddNewUserButton()
        {
            if (!CanAddAdsUser())
            {
                BtnAdd.Visible = false;
            }
        }

        private void HideUserListEditColumn()
        {
            if (!CanEditAdsUserList())
            {
                gvAdvertiserUsers.Columns[7].Visible = false;
            }
        }

        private void HideUserListDeleteColumn()
        {
            if (!CanDeleteAdsUserList())
            {
                gvAdvertiserUsers.Columns[8].Visible = false;
            }
        }

        private void ControlLoginUserAuthority()
        {
            HideUserListEditColumn();
            HideUserListDeleteColumn();
            HideAddNewUserButton();
        }

        private void GenerateAdsIDSession()
        {
            if (IsAdminFlag(GetIsAdminValue(GetIsAdminValueQuery())))
                Session["AdsListID"] = GetAdvertiserListID();
            if(CanViewAdsUserList())
                Session["AdsListID"] = GetAdvertiserListID();
        }
        
        private string EmailBody(string userguid)
        {
            string email = "Hello " + txtFName.Text + "<br />";
            email += "Thanks for signing up to Pet Adoption Central as an advertiser.<br/>";
            email += "You are nearly done.<br/>";
            email += "Click on the link below and will be taken to our website to complete the signup process.<br/>";
            email += "http://localhost:49962/Advertisers/AdvertiserSignUp.aspx?usgd=" + userguid + "&usfg=1";
            return email;
        }

        private void SendEmail()
        {
            Util.SendEmail(txtEmail.Text, EmailBody(GetAdsNewUserGuid(GetAdsNewUserGuidQuery())));
        }

        private void ControlVisible(bool vsb1, bool vsb2, bool vsb3)
        {
            PlNewAdsUser.Visible = vsb1;
            BtnNext.Visible = vsb1;
            BtnCancel.Visible = vsb3;
            lbStep1.Visible = vsb3;
            lbStep2.Visible = vsb3;
            PlSecurityMask.Visible = vsb2;
            BtnSave.Visible = vsb2;
        }

        private void ControlLabelHighlight(Color color1, Color color2)
        {
            lbStep1.BackColor = color1;
            lbStep2.BackColor = color2;
        }
        
        private bool NoItemChecked()
        {
            if (chkIsAdmin.Checked)
            {
                return false;
            }
            else
            {
                foreach (ListItem item in cblAuthControl.Items)
                {
                    if (item.Selected)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        

        

        private void BtnSave_Click()
        {
            if (NoItemChecked())
            {
                LbMessege.Text = "Please Choose At Least One...!";
            }
            else
            {
                if (Util.ExistEmail(txtEmail.Text, "AdvertiserUserList", "EmailAddress"))
                {
                    if (Util.RecordStatusActive(txtEmail.Text, "AdvertiserUserList", "EmailAddress"))
                    {
                        LbMessege.Text = "User has been Exist...!";
                    }
                    else
                    {
                        ExecuteActiveUserQuery();
                        SendEmail();
                        gvAdvertiserUsers.DataBind();
                        PlNewAdsUser.Visible = false;
                        ControlVisible(false, false, false);
                    }
                }
                else
                {
                    ExecuteInsertNewUserQuery();
                    LbMessege.Text = "Confrim Email has been sent to User...!";
                    SendEmail();
                    gvAdvertiserUsers.DataBind();
                    PlNewAdsUser.Visible = false;
                    ControlVisible(false, false, false);
                }
                
            }
            
        }
        

        protected void ChkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            bool b = (sender as CheckBox).Checked;
            foreach (ListItem item in cblAuthControl.Items)
            {
                item.Selected = b;
            }
        }


        private bool CanDeleteUsers(object sender, string id)
        {
            if (GetAdvertiserUserListID() == id)
            {
                return false;
            }
            return true;
        }

        private void HideAuthEditButton()
        {
            Button btn = fvUserInfo.FindControl("BtnChangeAuth") as Button;
            CheckBox chk = fvUserInfo.FindControl("IsAdminCheckBox") as CheckBox;
            
        }
        
        private void BtnDeleteCall(object sender, string id)
        {
            if (!CanDeleteUsers(sender, id))
            {
                LbMessege.Visible = true;
                LbMessege.Text = "You Can't Delete Yourself...!";
            }
            else
            {
                Util.ExecuteQuery(DeactivateUserQuery(id));
                gvAdvertiserUsers.DataBind();
                ControlVisible(false, false, false);
            }
        }
        
        private void BtnChangeAuthCall()
        {
            CheckBox chk = fvUserInfo.FindControl("IsAdminCheckBox") as CheckBox;
            if (chk.Checked)
            {
                LbMessege.Visible = true;
                LbMessege.Text = "Admin Already Has Full Control...!";
            }
            else PlAdsUserEditAuth.Visible = true;
        }

        private bool NoItemSelected()
        {
            foreach (ListItem item in cblAdsEditUserAuth.Items)
            {
                if (item.Selected) return false;
            }
            return true;
        }

        private void BtnAuthSaveCall()
        {
            int securitymask = 0;
            if (NoItemSelected()) LbMessege.Text = "Please Choose At Least One...!";
            else
            {
                foreach (ListItem item in cblAdsEditUserAuth.Items)
                {
                    if (item.Selected) securitymask += Convert.ToInt32(item.Value);
                }
                Util.ExecuteQuery(UpdateAdsUserSecurityMask(securitymask, Session["SelectedAdsUserID"].ToString()));
                LbMessege.Visible = true;
                LbMessege.Text = "Auth Change Successfully...!";
                PlAdsUserEditAuth.Visible = false;
            }
        }
        
        private int Row_Index(object sender)
        {
            GridViewRow row = ((sender as Button).NamingContainer) as GridViewRow;
            return row.RowIndex;
        }

        private void UpdateButtonCall()
        {
            CheckBox chk = fvUserInfo.FindControl("IsAdminCheckBox") as CheckBox;
            if (chk.Checked)
            {
                Util.ExecuteQuery(UpdateAdsUserSecurityMask(63, Session["SelectedAdsUserID"].ToString()));
            }
        }

        protected void Button_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "BtnAdsUserEdit":
                    fvUserInfo.ChangeMode(FormViewMode.Edit);
                    break;
                case "BtnNext":
                    ControlVisible(false, true, true);
                    ControlLabelHighlight(Color.White, Color.Blue);
                    break;
                case "BtnSave":
                    BtnSave_Click();
                    break;
                case "BtnCancel":
                    ControlVisible(false, false, false);
                    break;
                case "Update":
                    UpdateButtonCall();
                    break;
                case "BtnDelete":
                    string id = gvAdvertiserUsers.DataKeys[Row_Index(sender)].Values[0].ToString();
                    BtnDeleteCall(sender, id);
                    break;
                case "BtnDetail":
                    Session["SelectedAdsUserID"] = gvAdvertiserUsers.DataKeys[Row_Index(sender)].Value;
                    fvUserInfo.Visible = true;
                    break;
                case "BtnAdd":
                    ControlVisible(true, false, true);
                    ControlLabelHighlight(Color.Blue, Color.White);
                    break;
                case "BtnChangeAuth":
                    BtnChangeAuthCall();
                    break;
                case "BtnAuthCancel":
                    PlAdsUserEditAuth.Visible = false;
                    break;
                case "BtnAuthSave":
                    BtnAuthSaveCall();
                    break;
                case "BtnCancelAuth":
                    fvUserInfo.Visible = false;
                    LbMessege.Visible = false;
                    break;
                default:break;
            }
        }
    }
}