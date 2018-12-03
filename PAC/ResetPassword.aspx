<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="PAC.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ForgetPassword.css" rel="stylesheet" />



</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="container">
            <div class="upper1">
                <h3>Reset Your Password</h3>
                <br />
                <div>
                    <asp:TextBox ID="txtemail" Visible="true" placeholder="Enter Your E-mail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server"  ErrorMessage="<br />Invalid E-mail Address...!" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblEmailError" runat="server" Visible="false" Text="<br />E-mail Not Exists...!" Font-Size="Medium" ForeColor="Red"></asp:Label>
                    
                    <asp:TextBox ID="txtnewpswd" Visible="false" placeholder="Create New Password" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ErrorMessage="<br />Password Must be 8 to 15 Characters...!" ControlToValidate="txtnewpswd" ValidationExpression="^([a-zA-Z0-9@*#]{8,15})$"></asp:RegularExpressionValidator>
                    <br />
                    <asp:TextBox ID="txtconpswd" Visible="false" placeholder="Confirm New Password" runat="server"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Not Match...!<br />" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToCompare="txtnewpswd" ControlToValidate="txtconpswd"></asp:CompareValidator>
                    
                    <asp:Button ID="BtnReset" CssClass="btn" OnClick="BtnReset_Click" runat="server" Visible="true" Text="Reset"></asp:Button>
                    <asp:Button ID="BtnSubmit" CssClass="btn" OnClick="BtnSubmit_Click" runat="server" Visible="false" Text="Submit"></asp:Button>
                    <asp:Label ID="lblMessage" runat="server" Visible="false"  Text="<br />Label"></asp:Label>
                </div>
            </div>

        </div>
    </section>

</asp:Content>
