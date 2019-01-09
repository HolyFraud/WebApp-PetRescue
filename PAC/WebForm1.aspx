<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PAC.WebForm1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <telerik:RadGrid ID="ResultsRadGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" CellPadding="2" CellSpacing="5" CommandItemStyle-Wrap="True" DataSourceID="ResultsSqlDataSource" GridLines="Both" MasterTableView-BorderStyle="None" ShowHeader="False" Skin="" Visible="False" Width="100%">
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <MasterTableView AllowPaging="True" BorderStyle="None" CommandItemDisplay="Top" CommandItemStyle-BorderStyle="None" DataKeyNames="EmployeeListID" DataSourceID="ResultsSqlDataSource" EnableSplitHeaderText="False" GridLines="None" HeaderStyle-BorderStyle="None" HeaderStyle-Wrap="True" IsFilterItemExpanded="False" ItemStyle-BorderStyle="None" NoMasterRecordsText="Sorry, we couldn't find anyone that fits your selection." ShowHeadersWhenNoRecords="False" TableLayout="Auto" Width="100%">
            <NoRecordsTemplate>
                <br />
                <br />
                <br />
                <br />
                <br />
                <div class="roundedCorners" style="border: 1px solid red; text-align: center; background-color: pink">
                    <br />
                    <br />
                    <br />
                    <span style="font-weight: bold; font-size: 150%">Sorry</span>
                    <br />
                    <br />
                    We couldn&#39;t find any matches.
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </NoRecordsTemplate>
            <CommandItemSettings ExportToCsvImageUrl="mvwres://Telerik.Web.UI, Version=2016.3.1018.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Grid.ExportToCsv.gif" ExportToExcelImageUrl="mvwres://Telerik.Web.UI, Version=2016.3.1018.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Grid.ExportToExcel.gif" ExportToPdfImageUrl="mvwres://Telerik.Web.UI, Version=2016.3.1018.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Grid.ExportToPdf.gif" ExportToWordImageUrl="mvwres://Telerik.Web.UI, Version=2016.3.1018.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Grid.ExportToWord.gif" />
            <RowIndicatorColumn Visible="False">
            </RowIndicatorColumn>
            <ExpandCollapseColumn Created="True">
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <div style="font-family: Arial, Helvetica, sans-serif; background-color: #E6E6E6;top: 0px; left: 0px; ">
                            <div style="border: 2px solid #999999; width: 100%; background-color: #FFFFFF;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="font-weight: bold; text-align: left; padding-left: 5px;">
                                            <telerik:RadLabel ID="NameRadLabel" runat="server" Font-Size="150%" ForeColor="#0001FD">
                                            </telerik:RadLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellspacing="0" style="width: 100%; background-color: #FFFFFF;">
                                                <tr>
                                                    <td valign="top" width="40%">
                                                        <table border="0" cellpadding="3" cellspacing="0" style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadLabel ID="SuburbRadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadLabel ID="AgeRadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                    yo
                                                                    <telerik:RadLabel ID="GenderRadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Phone 1:
                                                                    <telerik:RadLabel ID="Phone1RadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Phone 2:
                                                                    <telerik:RadLabel ID="Phone2RadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right" valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0" style=" width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Image ID="ProfilePicImage" runat="server" Width="128px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=" text-align: center;">
                                                                    <telerik:RadRating ID="RatingRadRating" runat="server" ItemHeight="15px" ReadOnly="True" style="float:right;">
                                                                    </telerik:RadRating>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <telerik:RadLabel ID="TotalJobsRadLabel" runat="server">
                                                                    </telerik:RadLabel>
                                                                    &nbsp;Jobs</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                                <table bgcolor="White" border="0" cellpadding="3" cellspacing="0" style="border-style: solid; border-color: #FFFFFF; background-color: white; " width="100%">
                                    <tr>
                                        <td style="text-align: left; " width="50%">
                                            <div id="SendInviteDiv" runat="server" class="roundedCorners" style="padding: 4px 0px 4px 0px;
                                                                    border: 1px solid #009900; text-align: center; background-color: #3AB54A; color: #FFFFFF;
                                                                    text-decoration: none; width: 80%">
                                            </div>
                                        </td>
                                        <td style="text-align: right;" width="50%">
                                            <asp:UpdatePanel ID="CallButtonUpdatePanel" runat="server">
                                                <ContentTemplate>
                                                    <div id="CallButtonDIV" runat="server" class="roundedCorners" style="padding: 4px 0px 4px 0px;
                                                                            border: 1px solid #009900; text-align: center;float:right; background-color: #3AB54A; color: #FFFFFF;
                                                                            text-decoration: none; width: 80%">
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 2px solid #C0C0C0; width: 100%;margin-top: 5px;">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="background-color: #333333; font-weight: bold; text-align: left; color: #FFFFFF;">About Me </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFFF">
                                            <div style="display: block; word-wrap: break-word; word-break: break-all; width: 100%;
                                                        text-align: left;">
                                                <telerik:RadLabel ID="AboutRadLabel" runat="server">
                                                </telerik:RadLabel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 2px solid #C0C0C0; width: 100%;margin-top: 5px;">
                                <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="background-color: #333333; font-weight: bold; text-align: left; color: #FFFFFF;">I Can Work as&nbsp;
                                            <telerik:RadLabel ID="SelectedStaffTypeRadLabel" runat="server" Font-Bold="True" Font-Italic="True" Font-Underline="True" ForeColor="#FFFF66">
                                            </telerik:RadLabel>
                                            &nbsp;and also </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFFF">
                                            <div id="StaffTypeDIV" runat="server" style="text-align: left;">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 2px solid #C0C0C0; width: 100%;margin-top: 5px;">
                                <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="background-color: #333333; font-weight: bold; text-align: left; color: #FFFFFF;">Qualifications I Have</td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFFF">
                                            <telerik:RadLabel ID="QualificationsRadLabel" runat="server">
                                            </telerik:RadLabel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 2px solid #C0C0C0; width: 100%;margin-top: 5px;">
                                <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="background-color: #333333; font-weight: bold; text-align: left; color: #FFFFFF;">Languages I Can Speak</td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #FFFFFF">
                                            <telerik:RadLabel ID="LanguagesRadLabel" runat="server">
                                            </telerik:RadLabel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table cellpadding="0" cellspacing="0" style="border-width: 3px; border-color: #000000; width: 100%; border-top-style: solid;margin-top: 5px;">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <ItemStyle BorderStyle="None" />
            <AlternatingItemStyle BorderStyle="None" />
            <PagerStyle AlwaysVisible="True" Height="50px" Mode="Slider" PageSizeControlType="RadComboBox" VerticalAlign="Middle" />
            <HeaderStyle BorderStyle="None" Wrap="True" />
            <CommandItemStyle BorderStyle="None" />
            <PagerTemplate>
                <div align="center" style="position: relative; width: 100%; padding-top: 60px; padding-bottom: 50px;
                            vertical-align: middle;">
                    <telerik:RadButton ID="MoreRecordsRadButton" runat="server" BackColor="#333333" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" CommandName="MoreRecords" CssClass="roundedCorners" Font-Size="120%" ForeColor="White" Height="50px" RenderMode="Classic" Skin="" Text="More Specials">
                        <ContentTemplate>
                            <div id="moreRecordsTextDiv" runat="server" align="center" style="line-height: 25px; width: 100%;">
                            </div>
                        </ContentTemplate>
                    </telerik:RadButton>
                </div>
            </PagerTemplate>
            <CommandItemTemplate>
                <telerik:RadLabel ID="SearchDetailsRadLabel" runat="server">
                </telerik:RadLabel>
            </CommandItemTemplate>
        </MasterTableView>
        <CommandItemStyle Wrap="True" />
    </telerik:RadGrid>



</asp:Content>
