<%@ Page Title="" Language="C#" MasterPageFile="/MasterPage.Master" AutoEventWireup="true" CodeBehind="RescueSignUp.aspx.cs" Inherits="PAC.RescueSignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="SignUpPanel" runat="server">

    
        <div>
            RescueName:<asp:TextBox ID="rescueNameTextBoxID" runat="server"></asp:TextBox>
            <br />
            <br />
            First Name :
            <asp:TextBox ID="firstNameTextBoxID" runat="server" ></asp:TextBox>
            <br />
            <br />
            Last Name :<asp:TextBox ID="lastNameTextBoxID" runat="server" ></asp:TextBox>
            <br />
            <br />
            Address :
            <asp:TextBox ID="addressTextBoxID" runat="server"></asp:TextBox>
            <br />
            <br />
            Suburb :
            <asp:TextBox ID="suburbTextBoxID" runat="server" ></asp:TextBox>
            <br />
            <br />
            Postcode:
            <asp:TextBox ID="postcodeTextBoxID" runat="server"></asp:TextBox>
            <br />
            <br />
            Phone:<asp:TextBox ID="phoneTextBoxID" runat="server"></asp:TextBox>
            <br />
            <br />
            email1:<asp:TextBox ID="email1TextBoxID" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <br />
            <br />
            password: <asp:TextBox ID="passwordTextBoxID" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="signUpButton" runat="server" OnClick="SignUp_Click" Text="SignUp" />
        </div>

        </asp:Panel>
    <asp:Panel ID="ConfirmSignUpPanel" runat="server" Visible="False">

        <asp:Label ID="confirmSignUpLabel" runat="server" Text="Label"></asp:Label>
    </asp:Panel>

      
        <p>
            &nbsp;</p>
   

</asp:Content>
