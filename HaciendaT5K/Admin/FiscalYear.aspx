<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FiscalYear.aspx.cs" Inherits="HaciendaT5K.Admin.FiscalYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div id="ErrorMessage" runat="server" class="alert alert-danger align-content-center col-md-6 container" visible="false">
        <asp:Label ID="Message" runat="server"></asp:Label>
    </div>
    <div class="container">
        <div class="row">
            <div class="col">
                <h1 class="text-center">WorkPlan</h1>
                <h2 class="text-center">Fiscal Year</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelName" for="TextBoxName">Name: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxName" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator ID="TextBoxNameValidatorRequired" Display="Dynamic" SetFocusOnError="true" ControlToValidate="TextBoxName" runat="server" ErrorMessage="Field is required" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>

            </div>
            <%--<label for="TextBoxProjectNumber">Project number</label>
            <asp:TextBox runat="server" ID="TextBoxProjectNumber" CssClass="" TextMode="SingleLine" TabIndex="1" />
            <asp:RequiredFieldValidator ID="TextBoxProjectNumberValidatorRequired" Display="Dynamic" SetFocusOnError="true"
                ControlToValidate="TextBoxProjectNumber" runat="server"
                ErrorMessage="Please Add a Project Number" ValidationGroup="ModelAdd">

            </asp:RequiredFieldValidator>--%>
        </div>
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label ID="LabelNumber" CssClass="form-control-lg" runat="server" Text="Number:"></asp:Label>
                <asp:TextBox runat="server" ID="TextBoxNumber" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator ID="TextBoxNumberValidatorRequired" Display="Dynamic" SetFocusOnError="true" ControlToValidate="TextBoxNumber" runat="server" ErrorMessage="Field is required" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>



            </div>
        </div>
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label ID="LabelFiscalYear" CssClass="form-control-lg" runat="server" Text="Status:"></asp:Label>
                <asp:DropDownList ID="DropDownListStatus" CssClass="form-control" runat="server" DataSourceID="SDSStatus"
                    DataTextField="FiscalYearStatusName" DataValueField="FiscalYearStatusID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SDSStatus" runat="server"
                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                    SelectCommand="SELECT * FROM [FiscalYearStatus]"></asp:SqlDataSource>
            </div>
        </div>
        <br />

        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonSaveNewProject" ValidationGroup="ModelAdd"  OnClick="ButtonSaveNewProject_Click" CssClass="btn btn-primary" runat="server" Text="ADD" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server" AutoGenerteColumns="False" DataKeyNames="FiscalYearID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:BoundField DataField="FiscalYearID" HeaderText="ID" InsertVisible="False"
                                ReadOnly="True" SortExpression="FiscalYearID"></asp:BoundField>
                            <asp:BoundField DataField="FiscalYearName" HeaderText="Name"
                                SortExpression="FiscalYearName" />
                            <asp:BoundField DataField="FiscalYearNumber" HeaderText="Number"
                                SortExpression="FiscalYearNumber"></asp:BoundField>
                            <asp:BoundField DataField="LastUpdate" HeaderText="Last Update"
                                SortExpression="LastUpdate" />
                            <asp:TemplateField HeaderText="Status" SortExpression="FiscalYearStatusName">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SDSFYStatus"
                                        DataTextField="FiscalYearStatusName" AppendDataBoundItems="True" DataValueField="FiscalYearStatusID"
                                        SelectedValue='<%# Bind("FiscalYearStatusID") %>'>
                                        <asp:ListItem Value="0">(None)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDSFYStatus" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT FiscalYearStatusID, FiscalYearStatusName FROM [FiscalYearStatus]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"
                                        Text='<%# Bind("FiscalYearStatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand="DELETE FROM [FiscalYear] WHERE [FiscalYearID] = @original_FiscalYearID "
                        InsertCommand="INSERT INTO [FiscalYear] ([FiscalYearName], [FiscalYearNumber], [LastUpdate], [FiscalYearStatusID]) VALUES (@FiscalYearName, @FiscalYearNumber, @LastUpdate, @FiscalYearStatusID)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT FiscalYearID, FiscalYearName, FiscalYearNumber, LastUpdate, FiscalYearStatusID, (SELECT FiscalYearStatusName from FiscalYearStatus as FYS where FYS.FiscalYearStatusID = FY.FiscalYearStatusID) as FiscalYearStatusName FROM FiscalYear as FY"
                        UpdateCommand="UPDATE [FiscalYear] SET [FiscalYearName] = @FiscalYearName, [FiscalYearNumber] = @FiscalYearNumber, [LastUpdate] = @LastUpdate, [FiscalYearStatusID] = @FiscalYearStatusID WHERE [FiscalYearID] = @original_FiscalYearID ">
                        <DeleteParameters>
                            <asp:Parameter Name="original_FiscalYearID" Type="Int32" />
                            <asp:Parameter Name="original_FiscalYearName" Type="String" />
                            <asp:Parameter Name="original_FiscalYearNumber" Type="String" />
                            <asp:Parameter Name="original_LastUpdate" Type="DateTime" />
                            <asp:Parameter Name="original_FiscalYearStatusID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="FiscalYearName" Type="String" />
                            <asp:Parameter Name="FiscalYearNumber" Type="String" />
                            <asp:Parameter Name="LastUpdate" Type="DateTime" />
                            <asp:Parameter Name="FiscalYearStatusID" Type="Int32" />
                            <asp:Parameter Name="original_FiscalYearID" Type="Int32" />
                            <asp:Parameter Name="original_FiscalYearName" Type="String" />
                            <asp:Parameter Name="original_FiscalYearNumber" Type="String" />
                            <asp:Parameter Name="original_LastUpdate" Type="DateTime" />
                            <asp:Parameter Name="original_FiscalYearStatusID" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="FiscalYearName" Type="String" />
                            <asp:Parameter Name="FiscalYearNumber" Type="String" />
                            <asp:Parameter Name="LastUpdate" Type="DateTime" />
                            <asp:Parameter Name="FiscalYearStatusID" Type="Int32" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
