<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AnimalSearch.aspx.cs" Inherits="PAC.AnimalSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="CSS/AnimalSearch.css" rel="stylesheet" />
    <script src="JS/AnimalSearchjs.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="JS/Favourite.js"></script>


</asp:Content>
    

    
        
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
            SelectCommand="SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID"></asp:SqlDataSource>

        <asp:SqlDataSource ID="ResultsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
            SelectCommand="SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID INNER JOIN SuburbList on SuburbList.SuburbListID = AnimalList.SuburbListID"></asp:SqlDataSource>
        <section>

            <div class="container">
                <div class="filterhearder">
                    <a style="text-decoration: none; color: black;">Filter Results</a>
                    <asp:LinkButton ID="lbReset" runat="server" OnClick="lbReset_Click" Style="margin-left: 60px;">Reset Filters</asp:LinkButton>
                </div>
                <div class="searchbar">

                    <aside>
                        <header style="font-size: large">Species</header>

                        <asp:RadioButtonList ID="rblSpecies" runat="server"
                            DataSourceID="AnimalTypeListSqlDataSource"
                            DataTextField="AnimalType"
                            DataValueField="AnimalTypeListID"
                            AutoPostBack="True">
                        </asp:RadioButtonList>
                        <asp:SqlDataSource ID="AnimalTypeListSqlDataSource" runat="server"
                            ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
                            SelectCommand="SELECT [AnimalTypeListID], [AnimalType] FROM [AnimalTypeList]"></asp:SqlDataSource>
                    </aside>
                    <aside>
                        <header style="font-size: large">Breed</header>
                        <asp:CheckBoxList ID="cblBreed" runat="server"
                            DataSourceID="SqlDataSource2"
                            DataTextField="AnimalBreed"
                            DataValueField="AnimalBreedListID">
                        </asp:CheckBoxList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                            ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
                            SelectCommand="SELECT [AnimalBreedListID], [AnimalBreed] FROM [AnimalBreedList] WHERE ([AnimalTypeListID] = @AnimalTypeListID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="rblSpecies" Name="AnimalTypeListID" PropertyName="SelectedValue" />
                            </SelectParameters>

                        </asp:SqlDataSource>

                    </aside>
                    <aside>
                        <header style="font-size: large">Gender</header>
                        <asp:CheckBoxList ID="cblSex" runat="server">
                            <asp:ListItem Value="M">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:CheckBoxList>
                    </aside>
                    <aside>
                        <header style="font-size: large">Age</header>

                        <asp:RadioButtonList ID="rblAge" runat="server">
                            <asp:ListItem Value="&lt;1">Less than 1 year</asp:ListItem>
                            <asp:ListItem Value="Between 1 And 5">1 ~ 5 years</asp:ListItem>
                            <asp:ListItem Value="Between 6 And 10">6 ~ 10 years</asp:ListItem>
                            <asp:ListItem Value="&gt; 10">Greater than 10 years</asp:ListItem>
                        </asp:RadioButtonList>

                    </aside>
                    <aside>
                        <header style="font-size: large">Within</header>
                        <asp:DropDownList ID="ddlRange" runat="server">
                            <asp:ListItem Selected="True" Value="10">10Km</asp:ListItem>
                            <asp:ListItem Value="30">30Km</asp:ListItem>
                            <asp:ListItem Value="50">50Km</asp:ListItem>
                            <asp:ListItem Value="100">100Km</asp:ListItem>

                        </asp:DropDownList>
                        of
                <asp:TextBox ID="tbPostCode" placeholder="PostCode" runat="server"></asp:TextBox>
                    </aside>
                    <aside>
                        <asp:CheckBoxList ID="cblStateList" runat="server"
                            DataSourceID="StateListDataSource"
                            DataTextField="State"
                            DataValueField="State">
                        </asp:CheckBoxList>
                        <asp:SqlDataSource ID="StateListDataSource" runat="server"
                            ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>"
                            SelectCommand="SELECT DISTINCT State FROM SuburbList"></asp:SqlDataSource>
                    </aside>


                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Label ID="lb" runat="server" Text="Label"></asp:Label>
                </div>
                <!-------------------------------------------------------------------------------------------------->
                <!-------------------------------------------------------------------------------------------------->
                <!-------------------------------------------------------------------------------------------------->
                <!-------------------------------------------------------------------------------------------------->
                <!-------------------------------------------------------------------------------------------------->

                <div class="presentbar">



                    <asp:UpdatePanel ID="panel" runat="server" UpdateMode="Conditional">
                        
                        <ContentTemplate>
                            <span>
                                <span>Order </span>
                                <asp:DropDownList ID="ddlSortList" runat="server"
                                    AppendDataBoundItems="true"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSortList_SelectedIndexChanged">
                                    <asp:ListItem Value="AnimalBreed">Breed</asp:ListItem>
                                    <asp:ListItem>Age</asp:ListItem>
                                    <asp:ListItem>Sex</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                            <span>By </span>
                            <asp:DropDownList ID="ddlDeriction" runat="server"
                                AppendDataBoundItems="true"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDeriction_SelectedIndexChanged">
                                <asp:ListItem Value="ASC" Selected="True">ASC</asp:ListItem>
                                <asp:ListItem Value="DESC">DESC</asp:ListItem>
                            </asp:DropDownList><br />

                            <telerik:RadGrid ID="ResultsRadgrid" runat="server"
                                AutoGenerateColumns="False"
                                DataSourceID="ResultsSqlDataSource"
                                AllowPaging="True"
                                AllowSorting="True"
                                HeaderStyle-CssClass="display:none;"
                                OnItemDataBound="ResultsRadgrid_ItemDataBound">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <MasterTableView DataKeyNames="AnimalListID"
                                    DataSourceID="ResultsSqlDataSource"
                                    PageSize="5"
                                    HeaderStyle-CssClass="display:none;">
                                    <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" />
                                    <NoRecordsTemplate>
                                        <telerik:RadLabel ID="rlb" runat="server" HtmlEncode="True">
                                            No Records Here
                                        </telerik:RadLabel>
                                    </NoRecordsTemplate>
                                    <RowIndicatorColumn Visible="False">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Created="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                                            <ItemTemplate>

                                                <table style="width: 20%;">
                                                    <asp:Image ID="AnimalImg" runat="server" />
                                                    <telerik:RadLabel ID="AnimalListIDRadLabel" runat="server" Text='<%#Eval("AnimalListID") %>' Visible="false"></telerik:RadLabel>
                                                    <tr>
                                                        <td>Name:</td>
                                                        <td>
                                                            <telerik:RadLabel ID="NameRadLabel" runat="server" Text='<%# Eval("Name") %>'></telerik:RadLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Age:</td>
                                                        <td>
                                                            <telerik:RadLabel ID="AgeRadLabel" runat="server"><%# Eval("Age") %></telerik:RadLabel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Sex</td>
                                                        <td>
                                                            <telerik:RadLabel ID="SexRadLabel" runat="server"><%#Eval("Sex") %></telerik:RadLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Type:</td>
                                                        <td>
                                                            <telerik:RadLabel ID="TypeRadLabel" runat="server"><%#Eval("AnimalType") %></telerik:RadLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Color</td>
                                                        <td>
                                                            <telerik:RadLabel ID="ColorRadLabel" runat="server"><%#Eval("Color") %></telerik:RadLabel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Breed</td>
                                                        <td>
                                                            <telerik:RadLabel ID="BreedRadLabel" runat="server"><%#Eval("AnimalBreed") %></telerik:RadLabel>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="MoreInfoBtn" runat="server" CssClass="morebtn" Text="More Info" OnClick="MoreInfoBtn_Click" />
                                                            <asp:Button ID="LikeBtn" runat="server" Text="Like" CssClass="likebtnbefore" OnClick="LikeBtn_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>

                                    <HeaderStyle CssClass="display:none;"></HeaderStyle>
                                </MasterTableView>

                                <HeaderStyle CssClass="display:none;"></HeaderStyle>
                            </telerik:RadGrid>

                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>


        </section>

    </asp:Content>
    

