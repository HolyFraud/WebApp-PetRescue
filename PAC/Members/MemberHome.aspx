<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MemberHome.aspx.cs" Inherits="PAC.Members.MemberHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div>
        <asp:LinkButton ID="lbUpdateInfo" Text="Update My Info" OnClick="lbUpdateInfo_Click"  runat="server"></asp:LinkButton>

    </div>
</asp:Content>
