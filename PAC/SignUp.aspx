<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="PAC.SignUp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title></title>
    <link href="CSS/Style.css" rel="stylesheet" type="text/css"/>
    <link href="CSS/bootstrap-theme.min.css" rel="stylesheet" />
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <section>
            <div class="container">
                <div class="inner1">
                    <p>Sign in with Social Network</p>
                    <br />
                    <a href="#" class="facebook">Log in with Facebook</a>
                    <br />
                    <a href="#" class="twitter">Log in with Twitter</a>
                    <br />
                    <a href="#" class="google">Log in with Google+</a>
                    <br />
                </div>
                <div class="inner2">
                    <h3>Sign Up</h3>

                    <asp:TextBox ID="txtfname" placeholder="First name" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorfname" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtfname" ErrorMessage="Please Enter Your First Name...!"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="txtlname" placeholder="Last name" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlname" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtlname" ErrorMessage="Please Enter Your Last Name...!"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="txtemail" placeholder="E-mail" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatoremail" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtemail" ErrorMessage="Please Enter Your E-mail...!"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatoremail" runat="server" ErrorMessage="Please Enter Valid Email Address...!" ControlToValidate="txtemail" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    
                    <asp:TextBox ID="txtpasswd" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorpassword" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Password Must be 8 to 15 Characters...!" ValidationExpression="^([a-zA-Z0-9@*#]{8,15})$" ControlToValidate="txtpasswd"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorpassword" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtpasswd" runat="server" ErrorMessage="Please Enter Your Password...!"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="txtcopswd" placeholder="Confirm Password" TextMode="Password" runat="server"></asp:TextBox>
                    <br />
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorcopswd" Font-Size="Medium" ForeColor="Red" Display="Dynamic" runat="server" ControlToValidate="txtcopswd" ></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorcopswd" runat="server" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ControlToValidate="txtcopswd" ControlToCompare="txtpasswd" ErrorMessage="Password Not Match...!"></asp:CompareValidator>

                    <br />
                    <asp:Button ID="btnSignup" OnClick="BtnSignup_Click" runat="server" CssClass="btn" Text="Sign Up" />
                    <br />
                    <asp:Label ID="signupMessage" Visible="false" runat="server" Text="You are Successfully Sign up...!"></asp:Label>
                </div>
            </div>
        </section>
        
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" SelectCommand="SELECT * FROM [MemberList]"></asp:SqlDataSource>

</asp:Content>
