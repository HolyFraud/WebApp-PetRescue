<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMyAccount.aspx.cs" Inherits="PAC.Members.ManageMyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ManageMyAccount.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Update Personal Information"></asp:Label>
        <asp:TextBox ID="txtfname" runat="server" Text="FirstName"></asp:TextBox>
        <asp:TextBox ID="txtmname" runat="server" Text="MiddleName"></asp:TextBox>
        <asp:TextBox ID="txtlname" runat="server" Text="LastName"></asp:TextBox>
        <asp:TextBox ID="txtemail" runat="server" Text="E-mail"></asp:TextBox>
        <asp:TextBox ID="txtphone1" runat="server" Text="Mobile"></asp:TextBox>
        <asp:TextBox ID="txtphone2" runat="server" Text="Cell Phone"></asp:TextBox>
        <asp:TextBox ID="txtaddress" runat="server" Text="Address 1"></asp:TextBox>
        <asp:TextBox ID="txtaddress2" runat="server" Text="Address 2"></asp:TextBox>
        <asp:TextBox ID="txtsuburb" runat="server" Text="Suburb"></asp:TextBox>
        <asp:TextBox ID="txtstate" runat="server" Text="State"></asp:TextBox>
        <asp:TextBox ID="txtpostcode" runat="server" Text="PostCode"></asp:TextBox>
        <asp:TextBox ID="txtdob" runat="server" Text="DOB"></asp:TextBox>
        <asp:DropDownList ID="ddlcountry" runat="server">
            <asp:ListItem>Choose Your Country</asp:ListItem>
            <asp:ListItem>Australia</asp:ListItem>
            <asp:ListItem>CN</asp:ListItem>
            <asp:ListItem>USA</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="BtnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
    </asp:Panel>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
        DeleteCommand="DELETE FROM AdoptionList WHERE (AdoptionListID = @AdoptionListID)"
        SelectCommand="SELECT ROW_NUMBER() OVER(ORDER BY AdoptionList.AdoptionListID) AS ID, AdoptionList.AdoptionListID, AnimalList.Age, AnimalList.Sex, AnimalList.DOB, AnimalList.AdoptionFee, AnimalTypeList.AnimalType FROM AdoptionList INNER JOIN AnimalList ON AdoptionList.AnimalListID = AnimalList.AnimalListID INNER JOIN AnimalTypeList ON AnimalList.AnimalTypeListID = AnimalTypeList.AnimalTypeListID WHERE AdoptionList.MemberListID = @MemberListID">
        <DeleteParameters>
            <asp:Parameter Name="AdoptionListID" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="MemberListID" SessionField="MemberMemberListID" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
        SelectCommand="SELECT AdoptionList.AdoptionListID, CONCAT(MemberList.FirstName, ' ', MemberList.LastName) AS Name, AnimalTypeList.AnimalType, AnimalList.Age, AnimalList.Sex, AnimalList.Color, AnimalList.DOB, AnimalList.AdoptionFee FROM AdoptionList INNER JOIN AnimalList ON AdoptionList.AnimalListID = AnimalList.AnimalListID INNER JOIN MemberList ON AdoptionList.MemberListID = MemberList.MemberListID INNER JOIN AnimalTypeList ON AnimalList.AnimalTypeListID = AnimalTypeList.AnimalTypeListID WHERE (AdoptionList.AdoptionListID = @AdoptionListID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="gvAdoptionlist" DefaultValue="" Name="AdoptionListID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" SelectCommand="SELECT QuestionResponseList.QuestionResponseListID, QuestionList.QuestionText, QuestionResponseList.ResponseValue, QuestionTypeList.QuestionType FROM QuestionResponseList INNER JOIN QuestionList ON QuestionResponseList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTypeList ON QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID INNER JOIN AdoptionList ON QuestionResponseList.AdoptionListID = AdoptionList.AdoptionListID WHERE (AdoptionList.AdoptionListID = @AdoptionListID)" UpdateCommand="UPDATE QuestionResponseList SET ResponseValue = @ResponseValue FROM QuestionResponseList INNER JOIN QuestionList ON QuestionResponseList.QuestionListID = QuestionList.QuestionListID INNER JOIN QuestionTypeList ON QuestionList.QuestionTypeListID = QuestionTypeList.QuestionTypeListID WHERE (QuestionResponseList.QuestionResponseListID = @QuestionResponseListID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GVAdoptionlist" Name="AdoptionListID" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ResponseValue" />
            <asp:Parameter Name="QuestionResponseListID" />
        </UpdateParameters>
    </asp:SqlDataSource>


    <asp:GridView ID="GVAdoptionlist" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowSorting="True" DataKeyNames="AdoptionListID">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
            <asp:BoundField DataField="AdoptionListID" HeaderText="AdoptionListID" SortExpression="AdoptionListID" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
            <asp:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex" />
            <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" />
            <asp:BoundField DataField="AdoptionFee" HeaderText="AdoptionFee" SortExpression="AdoptionFee" />
            <asp:BoundField DataField="AnimalType" HeaderText="AnimalType" SortExpression="AnimalType" />
            <asp:CommandField SelectText="Details" DeleteText="Cancel" ShowDeleteButton="True" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>



    <asp:FormView ID="FVAppDetails" runat="server" DataSourceID="SqlDataSource2" DataKeyNames="AdoptionListID">
        <EditItemTemplate>
            AdoptionListID:
            <asp:Label ID="AdoptionListIDLabel1" runat="server" Text='<%# Eval("AdoptionListID") %>' />
            <br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            AnimalType:
            <asp:TextBox ID="AnimalTypeTextBox" runat="server" Text='<%# Bind("AnimalType") %>' />
            <br />
            Age:
            <asp:TextBox ID="AgeTextBox" runat="server" Text='<%# Bind("Age") %>' />
            <br />
            Sex:
            <asp:TextBox ID="SexTextBox" runat="server" Text='<%# Bind("Sex") %>' />
            <br />
            Color:
            <asp:TextBox ID="ColorTextBox" runat="server" Text='<%# Bind("Color") %>' />
            <br />
            DOB:
            <asp:TextBox ID="DOBTextBox" runat="server" Text='<%# Bind("DOB") %>' />
            <br />
            AdoptionFee:
            <asp:TextBox ID="AdoptionFeeTextBox" runat="server" Text='<%# Bind("AdoptionFee") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            AnimalType:
            <asp:TextBox ID="AnimalTypeTextBox" runat="server" Text='<%# Bind("AnimalType") %>' />
            <br />
            Age:
            <asp:TextBox ID="AgeTextBox" runat="server" Text='<%# Bind("Age") %>' />
            <br />
            Sex:
            <asp:TextBox ID="SexTextBox" runat="server" Text='<%# Bind("Sex") %>' />
            <br />
            Color:
            <asp:TextBox ID="ColorTextBox" runat="server" Text='<%# Bind("Color") %>' />
            <br />
            DOB:
            <asp:TextBox ID="DOBTextBox" runat="server" Text='<%# Bind("DOB") %>' />
            <br />
            AdoptionFee:
            <asp:TextBox ID="AdoptionFeeTextBox" runat="server" Text='<%# Bind("AdoptionFee") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            AdoptionListID:
            <asp:Label ID="AdoptionListIDLabel" runat="server" Text='<%# Eval("AdoptionListID") %>' />
            <br />
            Name:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
            <br />

            AnimalType:
            <asp:Label ID="AnimalTypeLabel" runat="server" Text='<%# Bind("AnimalType") %>' />
            <br />

            Age:
            <asp:Label ID="AgeLabel" runat="server" Text='<%# Bind("Age") %>' />
            <br />
            Sex:
            <asp:Label ID="SexLabel" runat="server" Text='<%# Bind("Sex") %>' />
            <br />
            Color:
            <asp:Label ID="ColorLabel" runat="server" Text='<%# Bind("Color") %>' />
            <br />
            DOB:
            <asp:Label ID="DOBLabel" runat="server" Text='<%# Bind("DOB") %>' />
            <br />
            AdoptionFee:
            <asp:Label ID="AdoptionFeeLabel" runat="server" Text='<%# Bind("AdoptionFee") %>' />


            <asp:GridView ID="GVQuestion" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionResponseListID" DataSourceID="SqlDataSource3" OnRowDataBound="GVQuestion_RowDataBound" OnRowEditing="GVQuestion_RowEditing">
                <Columns>
                    <asp:BoundField DataField="QuestionResponseListID" HeaderText="QuestionResponseListID" InsertVisible="False" ReadOnly="True" SortExpression="QuestionResponseListID" />
                    <asp:BoundField DataField="QuestionText" HeaderText="QuestionText" SortExpression="QuestionText" />
                    <asp:TemplateField HeaderText="ResponseValue" SortExpression="ResponseValue">
                        <EditItemTemplate>
                            <asp:TextBox ID="txttype" runat="server" Text='<%# Bind("ResponseValue") %>'></asp:TextBox>
                            <asp:DropDownList ID="ddltype" runat="server">
                                <asp:ListItem>qwe</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ResponseValue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuestionType" HeaderStyle-CssClass="hidden" HeaderText="QuestionType" ItemStyle-CssClass="hidden" SortExpression="QuestionType">
                    <HeaderStyle CssClass="hidden" />
                    <ItemStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>


        </ItemTemplate>



    </asp:FormView>


    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>weq</asp:ListItem>
        <asp:ListItem>21e</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </div>

</asp:Content>
