<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="PAC.Advertisers.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Advertiser Password</title>
    <link href="/CSS/ForgetPassword.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section>
        <div class="container">
            <div class="upper1">
                <h3>Reset Your Password</h3>
                <br />
                <div>
                    <asp:TextBox ID="txtemail" Visible="true" placeholder="Enter Your E-mail" runat="server" ValidationGroup="ResetValidationGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server"  ErrorMessage="<br />Invalid E-mail Address...!" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ResetValidationGroup"></asp:RegularExpressionValidator>
                    <asp:Label ID="lblEmailError" runat="server" Visible="false" Text="<br />E-mail Not Exists...!" Font-Size="Medium" ForeColor="Red"></asp:Label>
                    
                    <asp:TextBox ID="txtnewpswd" Visible="false" placeholder="Create New Password" runat="server" ValidationGroup="ResetValidationGroup"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ErrorMessage="<br />Password Must be 8 to 15 Characters...!" ControlToValidate="txtnewpswd" ValidationExpression="^([a-zA-Z0-9@*#]{8,15})$" ValidationGroup="ResetValidationGroup"></asp:RegularExpressionValidator>
                    <br />
                    <asp:TextBox ID="txtconpswd" Visible="false" placeholder="Confirm New Password" runat="server" ValidationGroup="ResetValidationGroup"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Not Match...!<br />" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToCompare="txtnewpswd" ControlToValidate="txtconpswd" ValidationGroup="ResetValidationGroup"></asp:CompareValidator>
                    
                    <asp:Button ID="BtnReset" CssClass="btn" OnClick="BtnReset_Click" runat="server" Visible="true" Text="Reset" ValidationGroup="ResetValidationGroup"></asp:Button>
                    <asp:Button ID="BtnSubmit" CssClass="btn" OnClick="BtnSubmit_Click" runat="server" Visible="false" Text="Submit" ValidationGroup="ResetValidationGroup"></asp:Button>
                    <asp:Label ID="lblMessage" runat="server" Visible="false"  Text="<br />Label"></asp:Label>
                </div>
            </div>

        </div>
    </section>
    </form>
</body>
</html>
