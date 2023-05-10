<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="HaciendaT5K.DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Dashboard</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="#">Home</a></li>
                            

                            <%--<li class="breadcrumb-item active">Dashboard E-BLUE</li>--%>
                            <%--<li class="breadcrumb-item active"><asp:Label ID="lblRosterName" runat ="server"></asp:Label></li>
                            <li >
                            
                                <asp:Button ID="btnSignOut"  Text="Logout" Width="60px" Height="30px" runat="server" OnClick="btnSignOut_Click"  />

                            </li>--%>
                        </ol>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </section>
        <!-- /.content-header -->

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <!-- Small boxes (Stat box) -->
                <div class="row">
                    <!-- ./col -->
                    <!-- ./col -->
                    <div class="col-lg-6 col-6">
                        <!-- small box -->
                        <div class="small-box bg-warning">
                            <div class="inner">
                                <h3>
                                    <asp:LinkButton ID="LinkButtonUsers" runat="server" Text="0"></asp:LinkButton></h3>

                                <p>Active User</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-person-add"></i>
                            </div>
                            <a href="Admin/ManageUser.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-6 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger">
                            <div class="inner">
                                <h3>
                                    <asp:LinkButton ID="LinkButtonProjects" runat="server" Text="0"></asp:LinkButton></h3>
                                <p>Projects</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>
                            <a href="Project/Projects.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
                <!-- /.row -->
                <!-- Main row -->
                <div class="row">
                    <!-- Left col -->
                    <section class="col-lg-7 connectedSortable">
                        <!-- Custom tabs (Charts with tabs)-->
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">
                                    <i class="fas fa-chart-pie mr-1"></i>
                                    Projects
                                </h3>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
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
                                                            DataNavigateUrlFormatString="~/Project/projecttemplate.aspx?PID={0}"
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
                                            <h2 class="align-content-center">Work in Progress Projects</h2>
                                            <div class="align-content-center">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerteColumns="False" DataKeyNames="ProjectID"
                                                    DataSourceID="sdsWProjects" AutoGenerateColumns="False"
                                                    CssClass="table-bordered table table-hover table-striped" AllowSorting="True">
                                                    <RowStyle CssClass="data-row" />
                                                    <AlternatingRowStyle CssClass="alt-data-row" />
                                                    <Columns>
                                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                                            DataNavigateUrlFormatString="~/Project/projecttemplate.aspx?PID={0}"
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
                                                    SelectCommand="SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber, (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName, (SELECT WFSName FROM WorkFlowStatus AS W WHERE (WFSID = P.WFSID)) AS WFSName, (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID IN (1, 3, 4, 5)) AND (ProjectPI = @ProjectPI) AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="ProjectPI" DbType="Guid" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </section>
                    <!-- /.Left col -->
                    <!-- right col (We are only adding the ID to make the widgets sortable)-->
                    <section class="col-lg-5 connectedSortable">

                        <!-- Map card -->
                        <div class="card bg-gradient-primary">
                            <div class="card-header border-0">
                                <h3 class="card-title">
                                    <i class="fas fa-map-marker-alt mr-1"></i>
                                    User Not Added to Roster</h3>
                            </div>
                            <div class="card-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewUsers" CssClass="table-bordered table table-hover table-striped" runat="server" DataSourceID="SqlDataSourceuser" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" />
                                                    <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSourceuser" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT [Email], [EmailVerified] FROM [Users] WHERE ([EmailVerified] = @EmailVerified) ORDER BY [SignUpdate] DESC">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="False" Name="EmailVerified" Type="Boolean" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.card-body-->
                            <div class="card-footer bg-transparent">
                                <div class="row">
                                    <div class="col-4 text-center">
                                        <div id="sparkline-1"></div>
                                        <div class="text-white">Visitors</div>
                                    </div>
                                    <!-- ./col -->
                                    <div class="col-4 text-center">
                                        <div id="sparkline-2"></div>
                                        <div class="text-white">Online</div>
                                    </div>
                                    <!-- ./col -->
                                    <div class="col-4 text-center">
                                        <div id="sparkline-3"></div>
                                        <div class="text-white">Sales</div>
                                    </div>
                                    <!-- ./col -->
                                </div>
                                <!-- /.row -->
                            </div>
                        </div>
                        <!-- /.card -->

                    </section>
                    <!-- right col -->
                </div>
                <!-- /.row (main row) -->
            </div>
            <!-- /.container-fluid -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
