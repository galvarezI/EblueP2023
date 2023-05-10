<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="StatusForProjects.aspx.cs" Inherits="Eblue.Admin.ProjectStatus" %>

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
                <h2 class="text-center">Status For Projects</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelRoleName">Status: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxNameStatus" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator
                    ID="TextBoxNameStatusValidatorRequired"
                    ControlToValidate="TextBoxNameStatus"
                    runat="server"
                    Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Please Add a Status Name"
                    ValidationGroup="ModelAdd">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonNewStatus" ValidationGroup="ModelAdd" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewStatus_Click" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server"
                        AutoGenerteColumns="False" DataKeyNames="ProjectStatusID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:BoundField DataField="ProjectStatusID" HeaderText="ID" InsertVisible="False"
                                ReadOnly="True" SortExpression="ProjectStatusID"></asp:BoundField>
                            <asp:BoundField DataField="ProjectStatusName" HeaderText="Name"
                                SortExpression="ProjectStatusName" />
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
                        DeleteCommand="DELETE FROM [ProjectStatus] WHERE [ProjectStatusID] = @original_ProjectStatusID AND [ProjectStatusName] = @original_ProjectStatusName"
                        InsertCommand="INSERT INTO [ProjectStatus] ([ProjectStatusName]) VALUES (@ProjectStatusName)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT * FROM [ProjectStatus]"
                        UpdateCommand="UPDATE [ProjectStatus] SET [ProjectStatusName] = @ProjectStatusName WHERE [ProjectStatusID] = @original_ProjectStatusID AND [ProjectStatusName] = @original_ProjectStatusName">
                        <DeleteParameters>
                            <asp:Parameter Name="original_ProjectStatusID" Type="Int32" />
                            <asp:Parameter Name="original_ProjectStatusName" Type="String" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ProjectStatusName" Type="String" />
                            <asp:Parameter Name="original_ProjectStatusID" Type="Int32" />
                            <asp:Parameter Name="original_ProjectStatusName" Type="String" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="ProjectStatusName" Type="String" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
