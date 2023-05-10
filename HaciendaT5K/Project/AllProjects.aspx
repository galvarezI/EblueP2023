<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="AllProjects.aspx.cs" Inherits="Eblue.Project.AllProjects" %>

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
                <h2 class="text-center">All Projects</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <h1>ALL PROJECTS | FISCAL YEAR: </h1>
            <br />
            <div class="col-md-12">
                <asp:DropDownList ID="DropDownFiscalYear" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DropDownFiscalYear_SelectedIndexChanged" CssClass="form-control"
                    DataSourceID="SqlDataSourceUser" DataTextField="FiscalYearNumber" AppendDataBoundItems="true"
                    DataValueField="FiscalYearID">
                    <asp:ListItem> new </asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [FiscalYear]"></asp:SqlDataSource>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server"  DataKeyNames="ProjectID"
                        DataSourceID="sdsSubmmited" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped" AllowSorting="True"
                        
                        >
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/ViewProject.aspx?PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" SortExpression="ProjectNumber" />
                            <asp:BoundField DataField="RosterName" HeaderText="PI"
                                SortExpression="RosterName" />
                            <asp:BoundField DataField="LastUpdate" HeaderText="Last Update"
                                SortExpression="LastUpdate" />
                            <asp:BoundField DataField="ProyectStatusName" HeaderText="Status"
                                ReadOnly="True" SortExpression="ProyectStatusName" />
                            <asp:BoundField DataField="WFSName" HeaderText="Workflow"
                                SortExpression="WFSName" />
                            <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/editProjects.aspx?PID={0}"
                                DataTextFormatString="{0}" />
                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/Comments.aspx?Sci=0&amp;PID={0}"
                                DataTextFormatString="{0}" SortExpression="ProjectNumber" Text="Comment" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Projects here!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsSubmmited" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID,
 (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID,
 (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber,
 (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName, 
(SELECT WFSName FROM WorkFlowStatus W  WHERE WFSID=P.WFSID) as WFSName
 FROM Projects AS P WHERE FiscalYearID=@FiscalYearID ORDER BY ProjectNumber ASC">
                        <SelectParameters>
                            <asp:Parameter Name="FiscalYearID" DefaultValue="" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
