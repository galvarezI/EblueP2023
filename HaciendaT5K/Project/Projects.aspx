<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="Eblue.Project.Projects" %>

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
                <h2 class="text-center">Projects</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2 class="align-content-center">New Projects</h2>
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server" AutoGenerteColumns="False" DataKeyNames="ProjectID"
                        DataSourceID="sdsNewProjects" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped" AllowSorting="True">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/editProjects.aspx?PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" />
                            <asp:BoundField DataField="RosterName" HeaderText="Leader" ReadOnly="True"
                                SortExpression="RosterName" />
                            <asp:BoundField DataField="LastUpdate" HeaderText="Last Update"
                                SortExpression="LastUpdate" />
                            <asp:BoundField DataField="ProyectStatusName" HeaderText="Status"
                                SortExpression="ProyectStatusName" ReadOnly="True" />
                            <asp:BoundField DataField="FiscalYearNumber" HeaderText="FY"
                                SortExpression="FiscalYearNumber" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Projects here!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsNewProjects" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber, (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName, (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID = 1) AND (ProjectPI = @ProjectPI) AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)">
                        <SelectParameters>
                            <asp:Parameter Name="ProjectPI" DefaultValue="" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2 class="align-content-center">Work in Progess Projects</h2>
                <div class="align-content-center">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerteColumns="False" DataKeyNames="ProjectID"
                        DataSourceID="sdsWProjects" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped" AllowSorting="True">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/editProjects.aspx?PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" />
                            <asp:BoundField DataField="RosterName" HeaderText="Leader" ReadOnly="True"
                                SortExpression="RosterName" />
                            <asp:BoundField DataField="LastUpdate" HeaderText="Last Update"
                                SortExpression="LastUpdate" />
                            <asp:BoundField DataField="ProyectStatusName" HeaderText="Status"
                                ReadOnly="True" SortExpression="ProyectStatusName" />
                            <asp:BoundField DataField="FiscalYearNumber" HeaderText="FY"
                                SortExpression="FiscalYearNumber" />
                            <asp:HyperLinkField Text="Submit" DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/ReviewSubmit.aspx?PID={0}"
                                DataTextFormatString="{0}" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Projects here!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsWProjects" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber, (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName, (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID IN (2, 6)) AND (ProjectPI = @ProjectPI) AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)">
                        <SelectParameters>
                            <asp:Parameter Name="ProjectPI" DefaultValue="" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2 class="align-content-center">Submmited Projects</h2>
                <div class="align-content-center">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerteColumns="False" DataKeyNames="ProjectID"
                        DataSourceID="sdsSubmmited" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped" AllowSorting="True">
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/Comments.aspx?Sci=1&amp;PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" />
                            <asp:BoundField DataField="LastUpdate" HeaderText="Last Update"
                                SortExpression="LastUpdate" />
                            <asp:BoundField DataField="ProyectStatusName" HeaderText="Status"
                                ReadOnly="True" SortExpression="ProyectStatusName" />
                            <asp:BoundField DataField="WFSName" HeaderText="Workflow"
                                SortExpression="WFSName" />
                            <asp:BoundField DataField="FiscalYearNumber" HeaderText="FY"
                                SortExpression="FiscalYearNumber" />
                            <asp:HyperLinkField Text="History" DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Users/Projects/ProjectHistory.aspx?PID={0}" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Projects here!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsSubmmited" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber, (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName, (SELECT WFSName FROM WorkFlowStatus AS W WHERE (WFSID = P.WFSID)) AS WFSName, (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID IN (3, 4, 5)) AND (ProjectPI = @ProjectPI) AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)">
                        <SelectParameters>
                            <asp:Parameter Name="ProjectPI" DefaultValue="" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
