<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProInCommodity.aspx.cs" Inherits="Eblue.ProInCommodity" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="ProInCommodity.aspx.cs" Inherits="Eblue.ProInCommodity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ErrorMessage" runat="server" class="alert alert-danger align-content-center col-md-6 container" visible="false">
        <asp:Label ID="Message" runat="server"></asp:Label>
    </div>
    <div class="container">
        <div class="row">
            <div class="col">
                <h1 class="text-center">E-BLUE</h1>
                <h2 class="text-center">Reports</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <h1>Reports | My Credits</h1>
            <br />
            <div class="col-md-12">
                <asp:DropDownList ID="cmbFiscalYear" runat="server"
                    DataSourceID="sdsFiscalYear" DataTextField="FiscalYearNumber"
                    DataValueField="FiscalYearID" AutoPostBack="True" CssClass="form-control">
                </asp:DropDownList>

                &nbsp;<asp:SqlDataSource ID="sdsFiscalYear" runat="server"
                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                    SelectCommand="SELECT [FiscalYearID], [FiscalYearNumber] FROM [FiscalYear] ORDER BY [FiscalYearNumber] DESC"></asp:SqlDataSource>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="align-content-center">
                        <asp:GridView ID="gv" runat="server"
                            AutoGenerteColumns="False"
                            DataSourceID="sdsSciCred" AutoGenerateColumns="False"
                            CssClass="table-bordered table table-hover table-striped" AllowSorting="True">
                            <RowStyle CssClass="data-row" />
                            <AlternatingRowStyle CssClass="alt-data-row" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                    DataNavigateUrlFormatString="~/Users/Projects/ViewProject.aspx?PID={0}"
                                    DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                    HeaderText="Project Number" SortExpression="ProjectNumber" />
                                <asp:BoundField DataField="ProjectStatusName" HeaderText="Status"
                                    SortExpression="ProjectStatusName" />
                                <asp:BoundField DataField="RosterName" HeaderText="Scientist"
                                    SortExpression="RosterName" />
                                <asp:BoundField DataField="Role" HeaderText="Role"
                                    SortExpression="Role" />
                                <asp:BoundField DataField="TR" HeaderText="TR" SortExpression="TR" />
                                <asp:BoundField DataField="CA" HeaderText="CA" SortExpression="CA" />
                                <asp:BoundField DataField="AH" HeaderText="AH" SortExpression="AH" />
                                <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True"
                                    SortExpression="Total" />
                            </Columns>
                            <EmptyDataTemplate>
                                No Projects here!
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:SqlDataSource ID="sdsSciCred" runat="server"
                            ConflictDetection="CompareAllValues"
                            ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                            OldValuesParameterFormatString="original_{0}"
                            SelectCommand="SELECT
SP.ProjectID, P.ProjectNumber, PST.ProjectStatusName, r.RosterName,PR.Role, TR,CA,AH,(TR+CA+AH) as Total
FROM SciProjects SP
inner join Projects P on P.ProjectID=SP.ProjectID
inner join ProjectStatus PST on PST.ProjectStatusID = P.ProjectStatusID 
inner join ProjectRoles PR on PR.PRID=SP.Role
inner join Roster R on R.RosterID=SP.RosterID  
WHERE SP.RosterID=@RosterID and p.fiscalyearid = @FiscalYear
Order By RosterName, ProjectStatusName, ProjectNumber ">
                            <SelectParameters>
                                <asp:Parameter Name="RosterID" />
                                <asp:Parameter Name="FiscalYear" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



