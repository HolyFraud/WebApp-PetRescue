<%@ Page Title="" Language="C#" MasterPageFile="~/Advertisers/AdvertiserMasterPage.Master" AutoEventWireup="true" CodeBehind="AdvertiserUsersManage.aspx.cs" Inherits="PAC.Advertisers.AdvertiserUsersManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/AdvertiserUserManageStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT [AdvertiserUserListID], [FirstName], [LastName], [EmailAddress], [Phone1], [Phone2], [IsAdmin] FROM [AdvertiserUserList] WHERE ([AdvertiserListID] = @AdvertiserListID) And (RecordStatus = 1)"
        DeleteCommand="">
        <SelectParameters>
            <asp:SessionParameter Name="AdvertiserListID" SessionField="AdsListID" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    <section class="cover">

    <div>
    <div >
        <asp:GridView ID="gvAdvertiserUsers"  runat="server"
            AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" 
            DataKeyNames="AdvertiserUserListID"
            AllowPaging="True" 
            PageSize="8">
            <Columns>
                <asp:BoundField DataField="AdvertiserUserListID" HeaderText="AdvertiserUserListID" SortExpression="AdvertiserUsertListID"/>
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                <asp:BoundField DataField="Phone1" HeaderText="Phone1" SortExpression="Phone1" />
                <asp:BoundField DataField="Phone2" HeaderText="Phone2" SortExpression="Phone2" />
                <asp:CheckBoxField DataField="IsAdmin" HeaderText="IsAdmin" SortExpression="IsAdmin" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnDetail" runat="server" Text="Details" />
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="BtnDelete" runat="server" CausesValidation="false" Text="Delete" OnClick="BtnDelete_Click"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="BtnAdd" runat="server" Text="Add New User" OnClick="BtnAdd_Click" Visible="true"/>
    </div>
        <asp:Panel ID="PlSteps" runat="server">
            <asp:Label ID="lbStep1" runat="server" Text="Step 1" Visible="false"></asp:Label>
            <asp:Label ID="lbStep2" runat="server" Text="Step 2" Visible="false"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PlNewAdsUser" runat="server" Visible ="false">
            
            <asp:TextBox ID="txtFName" placeholder="First Name" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox>
            <asp:TextBox ID="txtPhone1" placeholder="Mobile Phone" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFName" runat="server" ErrorMessage="First Name is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPhone1" runat="server" ErrorMessage="Mobile Phone is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtLName" placeholder="Last Name" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox>
            <asp:TextBox ID="txtPhone2" placeholder="Cell Phone" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtLName" runat="server" ErrorMessage="Last Name is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtEmail" placeholder="Email Address" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox>
            <%--<asp:CheckBox ID="chkAdmin" Checked="false" Text="Is Admin" runat="server" /><br />--%>
            
            <asp:TextBox ID="txtpswd" placeholder="Password" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtcopswd" placeholder="Confrim Password" runat="server" CssClass="txt1" ValidationGroup="UserValidationGroup"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtpswd" runat="server" ErrorMessage="Password is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtcopswd" runat="server" ErrorMessage="Confirm Password is Empty...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtcopswd" ControlToCompare="txtpswd" runat="server" ErrorMessage="Password Not Match...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtpswd" ValidationExpression="^([a-zA-Z0-9@*#]{8,15})$" runat="server" ErrorMessage="Password Must be 8 - 15...!" Font-Size="Medium" ForeColor="Red" Display="Dynamic" ValidationGroup="UserValidationGroup"></asp:RegularExpressionValidator>
            
            
            
        </asp:Panel>
        <asp:Panel ID="PlSecurityMask" runat="server" Visible="false">
            <asp:CheckBox ID="chkIsAdmin" runat="server" Text="Is Admin" OnCheckedChanged="ChkIsAdmin_CheckedChanged" AutoPostBack="true"/>
            <asp:CheckBoxList ID="cblAuthControl" runat="server">
                <asp:ListItem Value="1">Can Add Users</asp:ListItem>
                <asp:ListItem Value="2">Can Edit Users</asp:ListItem>
                <asp:ListItem Value="4">Can Delete Users</asp:ListItem>
                <asp:ListItem Value="8">Can Add Ads</asp:ListItem>
                <asp:ListItem Value="16">Can Edit Ads</asp:ListItem>
                <asp:ListItem Value="32">Can Delete Ads</asp:ListItem>
            </asp:CheckBoxList>
        </asp:Panel>
        <asp:Panel ID="Plbtn" runat="server">
            <asp:Button ID="BtnNext" runat="server" Text="Next" CssClass="btnstyle" OnClick="BtnNext_Click" ValidationGroup="UserValidationGroup" Visible="false"/>
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btnstyle" OnClick="BtnSave_Click" ValidationGroup="UserValidationGroup" Visible="false"/>
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btnstyle" OnClick="BtnCancel_Click" Visible="false"/>
        </asp:Panel>
        <asp:Label ID="LbMessege" runat="server" Text="Label" Visible="false"></asp:Label>
    </div>
    </section>
</asp:Content>
