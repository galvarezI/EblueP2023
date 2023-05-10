<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptProject.aspx.cs" Inherits="Eblue.DeptProject" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="DeptProject.aspx.cs" Inherits="Eblue.DeptProject" %>

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
            <h1>Reports | Deparment Projects</h1>
            <br />
            <div class="col-md-12">
                <asp:DropDownList ID="DropDownDepartment" AutoPostBack="True" runat="server" CssClass="form-control"
                    DataSourceID="admindept" DataTextField="DepartmentName"
                    DataValueField="DepartmentID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="admindept" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="align-content-center">
                        <asp:GridView ID="gv" runat="server"
                            AutoGenerteColumns="False"
                            DataSourceID="sdsDeptProjects" AutoGenerateColumns="False"
                            CssClass="table-bordered table table-hover table-striped" AllowSorting="True" DataKeyNames="projectid">
                            <RowStyle CssClass="data-row" />
                            <AlternatingRowStyle CssClass="alt-data-row" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                    DataNavigateUrlFormatString="~/Users/Projects/ViewProject.aspx?PID={0}"
                                    DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                    HeaderText="Project Number" SortExpression="ProjectNumber" />
                                <asp:BoundField DataField="RosterName" HeaderText="PI"
                                    SortExpression="RosterName" />
                                <asp:BoundField DataField="ProjectStatusName" HeaderText="Project Status"
                                    SortExpression="ProjectStatusName" />
                                <asp:BoundField DataField="WFSName" HeaderText="Workflow"
                                    SortExpression="WFSName" />
                            </Columns>
                            <EmptyDataTemplate>
                                No Projects here!
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsDeptProjects" runat="server"
                            ConflictDetection="CompareAllValues"
                            ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                            OldValuesParameterFormatString="original_{0}"
                            SelectCommand="select projectid,p.ProjectNumber ,r.RosterName, ps.ProjectStatusName, w.WFSName 
from Projects p
inner join roster r on p.ProjectPI = r.RosterID
inner join ProjectStatus ps on p.ProjectStatusID=ps.ProjectStatusID
inner join WorkFlowStatus w on p.WFSID=w.WFSID  
where p.DepartmentID=@DepartmentID">
                            <SelectParameters>
                                <asp:Parameter Name="DepartmentID" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

