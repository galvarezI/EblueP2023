<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="Eblue.Admin.Location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- MasterPageFile="~/Site.Master.Master"--%>
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
                <h2 class="text-center">Locations</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelLocation">Location: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxLocation" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator
                    ID="TextBoxLocationValidatorRequired"
                    ControlToValidate="TextBoxLocation"
                    runat="server"
                    Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Field Required"
                    ValidationGroup="LocationAdd">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonNewLocation" ValidationGroup="LocationAdd" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewLocation_Click" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gvLocations" runat="server"
                        AutoGenerteColumns="False" DataKeyNames="LocationID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:BoundField DataField="LocationName" HeaderText="Location Name"
                                SortExpression="LocationName"></asp:BoundField>
                            <asp:BoundField DataField="LocationOldID" HeaderText="Old ID"
                                SortExpression="LocationOldID" />
                            <asp:BoundField DataField="FundsVar" HeaderText="Funds Variable"
                                SortExpression="FundsVar" />
                            <asp:CommandField ButtonType="Button" HeaderText="Action" ShowDeleteButton="True"
                                ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand="DELETE FROM [Location] WHERE [LocationID] = @original_LocationID"
                        InsertCommand="INSERT INTO [Location] ([LocationName], [LocationOldID], [FundsVar]) VALUES (@LocationName, @LocationOldID, @FundsVar)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT [LocationName], [LocationOldID], [FundsVar], [LocationID] FROM [Location] ORDER BY [LocationName]"
                        UpdateCommand="UPDATE [Location] SET [LocationName] = @LocationName, [LocationOldID] = @LocationOldID, [FundsVar] = @FundsVar WHERE [LocationID] = @original_LocationID">
                        <DeleteParameters>
                            <asp:Parameter Name="original_LocationID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="LocationName" Type="String" />
                            <asp:Parameter Name="LocationOldID" Type="Int32" />
                            <asp:Parameter Name="FundsVar" Type="String" />
                            <asp:Parameter Name="original_LocationID" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="LocationName" Type="String" />
                            <asp:Parameter Name="LocationOldID" Type="Int32" />
                            <asp:Parameter Name="FundsVar" Type="String" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
