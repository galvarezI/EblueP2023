<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSpecialRep1.aspx.cs" Inherits="Eblue.AdminSpecialRep1" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="AdminSpecialRep1.aspx.cs" Inherits="Eblue.AdminSpecialRep1" %>

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
            <h1>Reports | Admin Credit Project</h1>
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
                            DataSourceID="sdsUFIS" AutoGenerateColumns="False"
                            CssClass="table-bordered table table-hover table-striped" AllowSorting="True" EnableModelValidation="True">
                            <RowStyle CssClass="data-row" />
                            <AlternatingRowStyle CssClass="alt-data-row" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectNumber"
                                    DataNavigateUrlFormatString="~/Users/Projects/ViewProject.aspx?PID={0}"
                                    DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                    HeaderText="Project Number" SortExpression="ProjectNumber" />
                                <asp:BoundField DataField="LocationName" HeaderText="Location"
                                    SortExpression="LocationName" />
                                <asp:BoundField DataField="balance" HeaderText="Balance"
                                    SortExpression="balance" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="UFISaccount" HeaderText="UFIS Account Number"
                                    SortExpression="UFISaccount" />
                            </Columns>
                            <EmptyDataTemplate>
                                No Projects here!
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                        <asp:SqlDataSource ID="sdsUFIS" runat="server"
                            ConflictDetection="CompareAllValues"
                            ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                            OldValuesParameterFormatString="original_{0}"
                            SelectCommand="select 
p.ProjectNumber, l.LocationName, 
(f.Salaries+f.Wages+f.Benefits+f.Assistant+f.Materials+f.Equipment+f.Travel+f.Abroad+f.Subcontracts+f.Others) as balance, f.UFISaccount 
from Funds f
inner join Projects p on f.ProjectID = p.ProjectID 
inner join Location l on f.LocationID = l.LocationID 
inner join FiscalYear fy on p.FiscalYearID = fy.FiscalYearID 
where p.WFSID=5 and p.ProjectPI=@RosterID and p.FiscalYearID=@FiscalYear
order by p.projectnumber">
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

