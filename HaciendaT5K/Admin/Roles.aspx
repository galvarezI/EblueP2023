<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master"  AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Eblue.Admin.Roles" %>

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
                <h2 class="text-center">Roles</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelRoleName">Role: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxRoleName" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="label3">Role Type: </asp:Label>
                <asp:DropDownList ID="DropDownListType" AutoPostBack="True" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceType" DataTextField="Description"
                    DataValueField="UID">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceType" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT Uid, Description FROM [RoleCategory] order by orderline"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DropDownListType" runat="server" CssClass="text-danger" ErrorMessage="Please Select a Role Type" ValidationGroup="RoleAdd"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonNewRole" ValidationGroup="RoleAdd" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewRole_Click" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server"
                        AutoGenerteColumns="False" DataKeyNames="RoleID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>

                            <asp:BoundField DataField="RoleID" HeaderText="RoleID" InsertVisible="False"
                                ReadOnly="True" SortExpression="RoleID"></asp:BoundField>
                            <asp:BoundField DataField="RoleName" HeaderText="Role Name"
                                SortExpression="RoleName" />

                            <asp:TemplateField HeaderText="Role Type" SortExpression="RoleCategoryName">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSourceType" AppendDataBoundItems="true"
                                        DataTextField="Description" DataValueField="UID"
                                        SelectedValue='<%# Bind("RoleCategoryID") %>'>
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:SqlDataSource ID="sdsFYNumber" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT [FiscalYearID], [FiscalYearNumber], FiscalYearName FROM [FiscalYear]"></asp:SqlDataSource>--%>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("RoleCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CheckBoxField DataField="Enable" HeaderText="Enable"
                                SortExpression="Enable" />
                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand="DELETE FROM [Roles] WHERE [RoleID] = @original_RoleID"
                        InsertCommand="INSERT INTO [Roles] ([RoleName], [Enable]) VALUES (@RoleName, @Enable)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT [RoleID], [RoleName], [Enable], RoleCategoryID, (SELECT Description FROM RoleCategory Rc WHERE (rc.uid = r.RoleCategoryID)) AS RoleCategoryName FROM [Roles] r  ORDER BY OrderLine"
                        UpdateCommand="UPDATE [Roles] SET [RoleName] = @RoleName, [Enable] = @Enable, RoleCategoryID= @RoleCategoryID WHERE [RoleID] = @original_RoleID">
                        <DeleteParameters>
                            <asp:Parameter Name="original_RoleID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="RoleName" Type="String" />
                            <asp:Parameter Name="Enable" Type="Boolean" />
                            <asp:Parameter Name="original_RoleID" Type="Int32" />
                            <asp:Parameter Name="RoleCategoryID" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="RoleName" Type="String" />
                            <asp:Parameter Name="Enable" Type="Boolean" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
