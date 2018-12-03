<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignUpFurther.aspx.cs" Inherits="PAC.FurtherSignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title></title>
    <link href="CSS/Style.css" rel="stylesheet" type="text/css"/>
    <link href="CSS/bootstrap-theme.min.css" rel="stylesheet" />
    <style>

        @import url('https://fonts.googleapix.com/family-Bitter|Crete+Round|Pacifico');

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section>
            <div class="container">
                <div class="inner3">
                    <h3>Further Sign Up</h3>
                    <asp:TextBox ID="txtmname" placeholder="Middle Name If You Have" runat="server"></asp:TextBox>

                    <asp:TextBox ID="txtmobile" placeholder="Mobile" runat="server"></asp:TextBox>

                    <asp:TextBox ID="txtphone" placeholder="Cell phone" runat="server"></asp:TextBox>

                    <asp:DropDownList class="dropdown" ID="ddlcountry" placeholder="Choose your country" runat="server">
                        <asp:ListItem>Australia</asp:ListItem>
                        <asp:ListItem>U.S.A</asp:ListItem>
                        <asp:ListItem>China</asp:ListItem>
                        <asp:ListItem>Afghanistan</asp:ListItem>
                        <asp:ListItem>Albania</asp:ListItem>
                        <asp:ListItem>Algeria</asp:ListItem>
                        <asp:ListItem>Andorra</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList class="dropdown" ID="ddlgender" placeholder="Choose your Gender" runat="server">
                        <asp:ListItem>Choose your gender</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                    
                    

                </div>
                <div class="inner2">
                    <h3></h3>

                    <asp:TextBox ID="txtaddress1" placeholder="Address 1" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddress" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtaddress1" runat="server" ErrorMessage="Please Enter Your Address...!"></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtaddress2" placeholder="Address 2" runat="server"></asp:TextBox>


                    <asp:TextBox ID="txtsuburb" placeholder="Suburb" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorsuburb" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtsuburb" ErrorMessage="Please Enter Your Suburb...!"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="txtstate" placeholder="State" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorstate" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtstate" ErrorMessage="Please Enter Your State...!"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="txtpostcode" placeholder="PostCode" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorpostcode" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtpostcode" ErrorMessage="Please Enter Your PostCode...!"></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtdob" placeholder="Date of Birth YYYY-MM-DD" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatordob" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtdob" ErrorMessage="Please Enter Your Date of Birth...!"></asp:RequiredFieldValidator>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatordob" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Wrong Date Format...!" ControlToValidate="txtdob" ValidationExpression="^(19[5-9][0-9]|20[0-4][0-9]|2050)[-/](0?[1-9]|1[0-2])[-](0?[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                    <br />

                    <asp:Button ID="BtnSignup2" runat="server" Text="Finish" CssClass="btn" OnClick="BtnSignup2_Click" />

                    
                    <asp:Label ID="signupMessage" Visible="false" runat="server" Text="You are Entirly Successfully Sign up...!"></asp:Label>
                </div>
            </div>
        </section>
        
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" SelectCommand="SELECT * FROM [MemberList]"></asp:SqlDataSource>

</asp:Content>