<%@ Page Title="" Language="C#" MasterPageFile="AdvertiserMasterPage.Master" AutoEventWireup="true" CodeBehind="AdvertiserSignUp.aspx.cs" Inherits="PAC.Advertisers.AdvertiserSignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/AdvertiserSignUpStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container">
        <div class="inner1">
            <h3>Sign Up</h3>
            <asp:TextBox ID="txtAdvertiserName" placeholder="Advertiser Name" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtAdvertiserName" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Advertiser Name is Empty...!"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtCompanyName" placeholder="Company Name" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic"  ErrorMessage="Company Name is Empty...!" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
            
            <asp:TextBox ID="txtAddress1" placeholder="Company Address1" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ErrorMessage="Company Address is Empty...!" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtAddress2" placeholder="Company Address2" runat="server"></asp:TextBox>
            

            <asp:TextBox ID="txtSuburb" placeholder="Company Suburb" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Company Suburb is Empty...!" ControlToValidate="txtSuburb"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtState" placeholder="Company State" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ErrorMessage="Company State is Empty...!" ControlToValidate="txtState"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtPostCode" placeholder="Company PostCode" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Company PostCode is Empty...!" ControlToValidate="txtPostCode"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtPhone1" placeholder="Company Cell Phone" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Company Contact Number is Empty...!" ControlToValidate="txtPhone1"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtPhone2" placeholder="Company Mobile Phone" runat="server"></asp:TextBox>

            </div>
        <div class="inner2">
            <asp:TextBox ID="txtEmail1" placeholder="Company Email1" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtEmail1" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Company Email is Empty...!"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Valid Email Address...!" ControlToValidate="txtEmail1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtEmail2" placeholder="Company Email2" runat="server"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail2"  runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ErrorMessage="Valid Email Address...!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

            <asp:TextBox ID="txtAdminFName" placeholder="Admin First Name" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Admin First Name is Empty...!" ControlToValidate="txtAdminFName"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtAdminLName" placeholder="Admin Last Name" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtAdminLName" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Admin Last Name is Empty...!"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtAdminEmail" placeholder="Admin Email" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtAdminEmail" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Admin Email is Empty...!"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtAdminEmail" runat="server" ErrorMessage="Valid Email Address...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

            <asp:TextBox ID="txtAdminPhone1" placeholder="Admin Cell Phone" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtAdminPhone1" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Admin Contact Number is Empty...!"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtAdminPhone2" placeholder="Admin Mobile Phone" runat="server"></asp:TextBox>

            <asp:TextBox ID="txtPassword" placeholder="Admin Password" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword" runat="server" ErrorMessage="Password is Empty...!" ></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPassword"  Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Password Must be 8 to 15 Characters...!" ValidationExpression="^([a-zA-Z0-9@*#]{8,15})$"></asp:RegularExpressionValidator>
            
            <asp:TextBox ID="txtConfirmPassword" placeholder="Confirm Password" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Password NOT Match...!"></asp:CompareValidator>
            <br />
            <asp:Button ID="btnSignUp" CssClass="btn" runat="server" Text="Sign Up" OnClick="BtnSignUp_Click"/>
            <br />
            <asp:Label ID="LbMessage" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" Text="Confrimed Email Has been sent to Your Personal Email Address...!" Visible="false"></asp:Label>

        </div>
        </div>
    </section>
</asp:Content>
