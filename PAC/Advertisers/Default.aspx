<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PAC.Advertisers.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/CSS/AdvertiserLoginStyle.css" rel="stylesheet" type="text/css"/>
    <link href="/CSS/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="cover">
        <div class="elements">
            <div class="intro">
                <asp:Label ID="Label1" runat="server" CssClass="Login1" Text="Login"></asp:Label>
                <asp:TextBox ID="txtUsername" CssClass="txt1 txtstyle" placeholder="E-mail address" runat="server"></asp:TextBox>
                    
                    <asp:TextBox ID="txtPassword" CssClass="txt2 txtstyle" Placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                    
                    <asp:Label ID="Label2" CssClass="lblerror" runat="server" Visible="false" Font-Size="Medium" ForeColor="Red"  Text="Invalid Username or Password"></asp:Label>

                    <asp:Button ID="BtnLogin" CssClass="btnstyle" runat="server" Text="Login" OnClick="BtnLogin_Click"></asp:Button>
                    
                    <asp:Button ID="BtnSignup" CssClass="btnstyle" runat="server" Text="Sign Up" OnClick="BtnSignup_Click"></asp:Button>
                    <div>
                        <asp:Label ID="LblError"  runat="server" Visible="false" Font-Size="Large" ForeColor="Red"  Text="Invalid Username or Password...!"></asp:Label>
                        <br />
                        <a href="ResetPassword.aspx">Forget Password?</a>
                    </div>
            </div>

        </div>
    </section>
    </form>
</body>
</html>
