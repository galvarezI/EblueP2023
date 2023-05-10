<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Commodity.aspx.cs" Inherits="Eblue.Admin.Commodity" %>

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
                <h2 class="text-center">Commodity</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelCommodityName">Commodity: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxCommodityName" CssClass="form-control" TextMode="SingleLine" />
                 <asp:RequiredFieldValidator
                    ID="TextBoxCommodityNameValidatorRequired"
                    ControlToValidate="TextBoxCommodityName"
                    runat="server"
                    Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="Field Required"
                    ValidationGroup="CommodityAdd">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonNewCommodity" ValidationGroup="CommodityAdd" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewCommodity_Click" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server" 
                        AutoGenerteColumns="False" DataKeyNames="CommID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>

                            <asp:BoundField DataField="CommID" HeaderText="ID" InsertVisible="False"
                                ReadOnly="True" SortExpression="CommID"></asp:BoundField>
                            <asp:BoundField DataField="CommName" HeaderText="Name"
                                SortExpression="CommName" />
                            <asp:BoundField DataField="CommOldID" HeaderText="Old ID"
                                SortExpression="CommOldID" />
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
                        DeleteCommand="DELETE FROM [Commodity] WHERE [CommID] = @original_CommID AND [CommName] = @original_CommName AND (([CommOldID] = @original_CommOldID) OR ([CommOldID] IS NULL AND @original_CommOldID IS NULL))"
                        InsertCommand="INSERT INTO [Commodity] ([CommName], [CommOldID]) VALUES (@CommName, @CommOldID)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT * FROM [Commodity]"
                        UpdateCommand="UPDATE [Commodity] SET [CommName] = @CommName, [CommOldID] = @CommOldID WHERE [CommID] = @original_CommID AND [CommName] = @original_CommName AND (([CommOldID] = @original_CommOldID) OR ([CommOldID] IS NULL AND @original_CommOldID IS NULL))">
                        <DeleteParameters>
                            <asp:Parameter Name="original_CommID" Type="Int32" />
                            <asp:Parameter Name="original_CommName" Type="String" />
                            <asp:Parameter Name="original_CommOldID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="CommName" Type="String" />
                            <asp:Parameter Name="CommOldID" Type="Int32" />
                            <asp:Parameter Name="original_CommID" Type="Int32" />
                            <asp:Parameter Name="original_CommName" Type="String" />
                            <asp:Parameter Name="original_CommOldID" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="CommName" Type="String" />
                            <asp:Parameter Name="CommOldID" Type="Int32" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
