<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoginChoice.aspx.cs" Inherits="PAC.LoginChoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <link href="CSS/LoginChoice.css" rel="stylesheet" />
    <link href="CSS/Style.css" rel="stylesheet" />
    <link href="CSS/bootstrap-theme.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css?family=Leckerli+One|Oleo+Script+Swash+Caps');
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="containerlogin">
            
          <div class="innerleft" onclick="location.window='/MemberLogin.aspx'">
                <a href="/MemberLogin.aspx">To Clients</a>
            </div>
              <div class="innerright">
                <a href="#">To Rescue</a>
            </div>
       </div>
     
        </section>
</asp:Content>
