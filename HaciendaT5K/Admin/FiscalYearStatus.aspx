<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FiscalYearStatus.aspx.cs" Inherits="HaciendaT5K.Admin.FiscalYearStatus" %>

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
                <h2 class="text-center">Fiscal Year Status</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-group">
            <div class="col-md-12">
                <asp:Label  runat="server" CssClass="form-text" ID="labelStatusName">Fiscal Year Status: </asp:Label>
                <%--<label for="TextBoxStatusName">Fiscal Year Status: </label>--%>
                <asp:TextBox
                    ID="TextBoxStatusName"
                    runat="server"
                    CssClass=""
                    TextMode="SingleLine"
                    TabIndex="1"></asp:TextBox> 
                <asp:RequiredFieldValidator
                    ID="TextBoxStatusNameValidatorRequired"
                    ControlToValidate="TextBoxStatusName"
                    runat="server"
                    Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Please Add a Status Name"
                    ValidationGroup="ModelAdd">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-md-12">
                <asp:Button ID="ButtonNewFiscalYearStatus" ValidationGroup="ModelAdd"  CssClass="btn btn-primary" runat="server" OnClick="ButtonNewFiscalYearStatus_Click" Text="ADD" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server" AutoGenerteColumns="False" DataKeyNames="FiscalYearStatusID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:BoundField DataField="FiscalYearStatusID" HeaderText="ID" InsertVisible="False"
                                ReadOnly="True" SortExpression="FiscalYearStatusID"></asp:BoundField>
                            <asp:BoundField DataField="FiscalYearStatusName" HeaderText="Name"
                                SortExpression="FiscalYearStatusName" />
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
                        DeleteCommand="DELETE FROM [FiscalYearStatus] WHERE [FiscalYearStatusID] = @original_FiscalYearStatusID AND [FiscalYearStatusName] = @original_FiscalYearStatusName"
                        InsertCommand="INSERT INTO [FiscalYearStatus] ([FiscalYearStatusName]) VALUES (@FiscalYearStatusName)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT * FROM [FiscalYearStatus]"
                        UpdateCommand="UPDATE [FiscalYearStatus] SET [FiscalYearStatusName] = @FiscalYearStatusName WHERE [FiscalYearStatusID] = @original_FiscalYearStatusID AND [FiscalYearStatusName] = @original_FiscalYearStatusName">
                        <DeleteParameters>
                            <asp:Parameter Name="original_FiscalYearStatusID" Type="Int32" />
                            <asp:Parameter Name="original_FiscalYearStatusName" Type="String" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="FiscalYearStatusName" Type="String" />
                            <asp:Parameter Name="original_FiscalYearStatusID" Type="Int32" />
                            <asp:Parameter Name="original_FiscalYearStatusName" Type="String" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="FiscalYearStatusName" Type="String" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
