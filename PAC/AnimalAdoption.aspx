<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AnimalAdoption.aspx.cs" Inherits="PAC.AnimalAdoption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/AnimalAdoption.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT Name, Color, Age, Sex, DOB, Description, AdoptionFee FROM AnimalList WHERE (AnimalListID = @AnimalListID)">
        <SelectParameters>
            <asp:SessionParameter Name="AnimalListID" SessionField="AnimalListID" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="SELECT QuestionList.QuestionListID, QuestionList.QuestionText AS QUESTIONS FROM QuestionTypeList INNER JOIN QuestionList ON QuestionTypeList.QuestionTypeListID = QuestionList.QuestionTypeListID INNER JOIN QuestionTemplateList ON QuestionList.QuestionTemplateListID = QuestionTemplateList.QuestionTemplateListID INNER JOIN AnimalList ON QuestionTemplateList.QuestionTemplateListID = AnimalList.QuestionTemplateListID WHERE (AnimalList.AnimalListID = @AnimalListID)">
        <SelectParameters>
            <asp:SessionParameter Name="AnimalListID" SessionField="AnimalListID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlGetList" runat="server" ConnectionString="<%$ ConnectionStrings:SQLConnectionString %>" 
        SelectCommand="">
    </asp:SqlDataSource>
    <section>
        <div class="container">
            
              <asp:FormView ID="fvAnimalDetails" runat="server" DataSourceID="SqlDataSource1">
                  <ItemTemplate>
                      Name:
                      <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                      <br />
                      Color:
                      <asp:Label ID="ColorLabel" runat="server" Text='<%# Bind("Color") %>' />
                      <br />
                      Age:
                      <asp:Label ID="AgeLabel" runat="server" Text='<%# Bind("Age") %>' />
                      <br />
                      Sex:
                      <asp:Label ID="SexLabel" runat="server" Text='<%# Bind("Sex") %>' />
                      <br />
                      DOB:
                      <asp:Label ID="DOBLabel" runat="server" Text='<%# Bind("DOB","{0:d}") %>' />
                      <br />
                      Description:
                      <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>' />
                      <br />
                      AdoptionFee:
                      <asp:Label ID="AdoptionFeeLabel" runat="server" Text='<%# Bind("AdoptionFee") %>' />
                      <br />
                      <asp:Button ID="BtnApply" runat="server" Text="Apply" OnClick="BtnApply_Click"/>
                      <asp:GridView ID="gvQuestions" runat="server" 
                          AutoGenerateColumns="False" 
                          DataKeyNames="QuestionListID" 
                          DataSourceID="SqlDataSource2">
                          <Columns>
                              <asp:BoundField DataField="QuestionListID" 
                                  HeaderText="QuestionListID" 
                                  InsertVisible="False" 
                                  ReadOnly="True" 
                                  SortExpression="QuestionListID" 
                                  HeaderStyle-CssClass="hidden"
                                  ItemStyle-CssClass="hidden"
                                  FooterStyle-CssClass="hidden"/>
                              <asp:BoundField DataField="QUESTIONS" HeaderText="QUESTIONS" SortExpression="QUESTIONS" ReadOnly="true"/>
                              <asp:TemplateField HeaderText="Answer"></asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                      <br />

                      <asp:Button ID="BtnComplete" runat="server" Text="Complete" Visible="false" OnClick="BtnComplete_Click"/>
                      <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Visible="false" OnClick="BtnCancel_Click"/>

                  </ItemTemplate>
            </asp:FormView>
            
              <asp:Label ID="lb" runat="server" Text="Label"></asp:Label>
        </div>
        
    </section>
</asp:Content>
