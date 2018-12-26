<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="PAC.Members.MemberHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/defaultSite.css" rel="stylesheet" />
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/MP.css" rel="stylesheet" />
    <script src="JS/jquery-3.3.1.min.js" type="text/javascript"></script>

    <style>
        @import url('https://fonts.googleapis.com/css?family=Boogaloo|Bree+Serif|Lobster|Merienda+One|PT+Sans|Righteous|Roboto+Slab|Saira|Sriracha|Yanone+Kaffeesatz');
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:LinkButton ID="lbUpdateInfo" Text="Update My Info" OnClick="lbUpdateInfo_Click"  runat="server"></asp:LinkButton>
        <asp:Label ID="lb" runat="server" Text="Label"></asp:Label>
    </div>


</asp:Content>
