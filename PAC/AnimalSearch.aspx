<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AnimalSearch.aspx.cs" Inherits="PAC.AnimalSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/AnimalSearch.css" rel="stylesheet" />
    <script src="JS/AnimalSearchjs.js"></script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT AnimalList.AnimalListID, AnimalList.Name, AnimalList.Age, AnimalList.Sex, AnimalTypeList.AnimalType, AnimalList.Color, AnimalBreedList.AnimalBreed FROM AnimalTypeList INNER JOIN AnimalList ON AnimalTypeList.AnimalTypeListID = AnimalList.AnimalTypeListID INNER JOIN AnimalBreedList ON AnimalList.AnimalBreedListID = AnimalBreedList.AnimalBreedListID"></asp:SqlDataSource>
    <section>
        
    <div class="container">
        <div class="filterhearder">
                <a style="text-decoration:none; color:black;">Filter Results</a>
                <asp:LinkButton ID="lbReset" runat="server" OnClick="lbReset_Click" Style="margin-left:60px;">Reset Filters</asp:LinkButton>
            </div>
        <div class="searchbar">
            
            <aside>
                <header style="font-size:large" >Species</header>
                
                <asp:RadioButtonList ID="rblSpecies" runat="server" 
                    DataSourceID="AnimalTypeListSqlDataSource" 
                    DataTextField="AnimalType" 
                    DataValueField="AnimalTypeListID"
                    AutoPostBack="True"
                    ></asp:RadioButtonList>
                <asp:SqlDataSource ID="AnimalTypeListSqlDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
                    SelectCommand="SELECT [AnimalTypeListID], [AnimalType] FROM [AnimalTypeList]">
                </asp:SqlDataSource>
            </aside>
            <aside>
                <header style="font-size:large">Breed</header>
                <asp:CheckBoxList ID="cblBreed" runat="server" 
                    DataSourceID="SqlDataSource2" 
                    DataTextField="AnimalBreed" 
                    DataValueField="AnimalBreedListID"></asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
                    SelectCommand="SELECT [AnimalBreedListID], [AnimalBreed] FROM [AnimalBreedList] WHERE ([AnimalTypeListID] = @AnimalTypeListID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="rblSpecies" Name="AnimalTypeListID" PropertyName="SelectedValue" />
                    </SelectParameters>
                    
                </asp:SqlDataSource>

            </aside>
            <aside>
                <header style="font-size:large">Gender</header>
                <asp:CheckBoxList ID="cblSex" runat="server" >
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                </asp:CheckBoxList>
            </aside>
            <aside>
                <header style="font-size:large">Age</header>

                <asp:CheckBoxList ID="cblAge" runat="server" >
                    <asp:ListItem Value="&lt; 1">Less than 1 years</asp:ListItem>
                    <asp:ListItem Value="BETWEEN 1 AND 5">1 ~ 5 years</asp:ListItem>
                    <asp:ListItem Value="BETWEEN 6 AND 10">6 ~ 10 years</asp:ListItem>
                    <asp:ListItem Value="&gt; 10">Greater than 10 years</asp:ListItem>

                </asp:CheckBoxList>
            </aside>
            <aside>
                <header style="font-size:large">Within</header>
                <asp:DropDownList ID="ddlRange" runat="server" >
                    <asp:ListItem Selected="True" Value="10">10Km</asp:ListItem>
                    <asp:ListItem Value="30">30Km</asp:ListItem>
                    <asp:ListItem Value="50">50KM</asp:ListItem>
                
                </asp:DropDownList>
                of
                <asp:TextBox ID="tbPostCode" placeholder="PostCode"  runat="server"></asp:TextBox>
            </aside>

            
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"/>
        </div>


        <div class="presentbar">
            <span>
                <span>Show</span>
                    <asp:DropDownList ID="ddlResultsCount" runat="server" 
                        OnSelectedIndexChanged="ddlResultsCount_SelectedIndexChanged" 
                        AutoPostBack="true">
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="64">64</asp:ListItem>
                    </asp:DropDownList>
                <span>pets per page </span>
            </span>
            <span>
                <span>Order </span>
                <asp:DropDownList ID="ddlSortList" runat="server" 
                    AppendDataBoundItems="true"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSortList_SelectedIndexChanged">
                    <asp:ListItem  Value="AnimalBreed">Breed</asp:ListItem>
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

            <asp:ListView ID="lvAnimalList" runat="server" 
                DataSourceID="SqlDataSource1"
                DataKeyNames="AnimalListID"
                GroupItemCount="4" 
                GroupPlaceholderID="groupPlaceholder" 
                ItemPlaceholderID="itemPlaceholder">
                <LayoutTemplate>
                    
                    <asp:DataPager  runat="server" PageSize="8">
                        <Fields>
                            
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonType="Link" />
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                        </Fields>

                    </asp:DataPager>
                    <table>
                        <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                    </table>

                    <asp:DataPager ID="dp1" runat="server" PageSize="8">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonType="Link" />
                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                        </Fields>

                    </asp:DataPager>
                </LayoutTemplate>
               <GroupTemplate>
                    <tr>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server" style="">
                        <table border="1" style="width: 200px; height: 100px;">
                        <tr style="display:none;">
                        <td>
                            <asp:Label ID="lbAnimalListID" runat="server" Text='<%# Eval("AnimalListID") %>' style="display:none;"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Name: 
                        </td>
                        <td>
                            
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Age: 
                        </td>
                        <td>
                            <asp:Label ID="AgeLabel" runat="server" Text='<%# Eval("Age") %>' />
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Sex: 
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Sex") %>' />
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Animal Type: 
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("AnimalType") %>' />
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Color: 
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Color") %>' />
                        </td>
                        </tr>
                        <tr>
                        <td>
                            Breed: 
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("AnimalBreed") %>' />
                        </td>
                        </tr>
                        
                            
                        
                        </table>
                        <asp:LinkButton ID="lbDetails" runat="server" Text="Details" OnClick="lbDetails_Click"></asp:LinkButton>
                    </td>
                </ItemTemplate>
                
            </asp:ListView>
        </div>
    </div>

        <asp:Label ID="lb" runat="server" Text="wodemaya"></asp:Label>
    </section>
    

</asp:Content>
