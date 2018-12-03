<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="PAC.Members.MemberLogin" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Login</title>

    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/MemberLoginSite.css" rel="stylesheet" type="text/css"/>

     <style>
        @import url('https://fonts.googleapis.com/css?family=Leckerli+One|Oleo+Script+Swash+Caps');
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="cover">
        <div>
            <div class="elements">
                <div class="intro">
                    <asp:Label ID="Login1" CssClass="Login1" runat="server" Text="Login"></asp:Label>
                    
                    <asp:TextBox ID="txtUsername" CssClass="txt1 txtstyle" placeholder="E-mail address" runat="server"></asp:TextBox>
                    
                    <asp:TextBox ID="txtPassword" CssClass="txt2 txtstyle" Placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                    
                    <asp:Label ID="Label1" CssClass="lblerror" runat="server" Visible="false" Font-Size="Medium" ForeColor="Red"  Text="Invalid Username or Password"></asp:Label>

                    <asp:Button ID="btnLogin" CssClass="btnstyle" runat="server" Text="Login" OnClick="BtnLogin_Click"></asp:Button>
                    
                    <asp:Button ID="btnSignup" CssClass="btnstyle" runat="server" Text="Sign Up" OnClick="BtnSignup_Click"></asp:Button>
                    <div>
                        <asp:Label ID="LblError"  runat="server" Visible="false" Font-Size="Large" ForeColor="Red"  Text="Invalid Username or Password...!"></asp:Label>
                        <br />
                        <a href="ResetPassword.aspx">Forget Password?</a>
                    </div>

                </div>

            </div>


        </div>
    </section>

</asp:Content>
