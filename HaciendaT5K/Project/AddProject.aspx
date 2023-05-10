<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="HaciendaT5K.plugins._5kform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                <h1 class="text-center">E-BLUE</h1>
                <h2 class="text-center">New Project</h2>
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="labelProjectNumber">Project number: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxProjectNumber" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>

        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="label5">Contract number: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxContractNumber" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>

        <div class="form-row">
            <div class="col-md-12">
                <asp:Label runat="server" CssClass="form-control-lg" ID="label6">ORCID: </asp:Label>
                <asp:TextBox runat="server" ID="TextBoxORCID" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div> 

        <div class="form-row">
            <div class="col-md-12">
                <asp:Label ID="LabelPrincipalInvestigator" CssClass="form-control-lg" runat="server" Text="Principal Investigator:"></asp:Label>
                <asp:DropDownList ID="DropDownPrincipalInvestigator" CssClass="form-control" runat="server" DataSourceID="sdsPI"
                    DataTextField="RosterName" DataValueField="RosterID" AppendDataBoundItems="true">
                    <asp:ListItem Value="">None</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="DropDownListPIValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="DropDownPrincipalInvestigator" runat="server" SetFocusOnError="true"  
                                            ErrorMessage="Please Select a Principal Investigator" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="sdsPI" runat="server"
                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"

                    SelectCommand="
select 
r.RosterID, r.RosterName
from roster r
inner join RosterCategory rc on rc.UId = r.rosterCategoryId
where r.CanBePI = 1 and rc.IsScientist = 1 order by r.RosterName
                    ">


                </asp:SqlDataSource>
            </div>
        </div>
        <div class="form-row">
            <div class="col-md-12">
                <asp:Label ID="LabelFiscalYear" CssClass="form-control-lg" runat="server" Text="Fiscal Year:"></asp:Label>
                <asp:DropDownList ID="DropdownlistFiscalYear" CssClass="form-control" runat="server" DataSourceID="sdsFY"
                    DataTextField="FiscalYearName" DataValueField="FiscalYearID" AutoPostBack="True">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsFY" runat="server"
                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                    SelectCommand="SELECT FiscalYearID, FiscalYearName FROM FiscalYear ORDER BY FiscalYearName DESC"></asp:SqlDataSource>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="col-md-12">
                <asp:Button ID="ButtonSaveNewProject" OnClick="ButtonSaveNewProject_Click" CssClass="btn btn-primary" runat="server" Text="ADD" ValidationGroup="ModelAdd" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="align-content-center">
                    <asp:GridView ID="gv" runat="server"  DataKeyNames="ProjectID"
                        DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                        CssClass="table-bordered table table-hover table-striped"
                        OnRowDataBound ="GridView_RowDataBound"
                        >
                        <RowStyle CssClass="data-row" />
                        <AlternatingRowStyle CssClass="alt-data-row" />
                        <Columns>

                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                ShowEditButton="True" />

                            <%--<asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="/Project/EditProject.aspx?PID={0}"
                                DataTextField="ShowTemplate" DataTextFormatString="{0}"
                                HeaderText="Go To" SortExpression="ProjectID" />--%>

                            <%--<asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Project/EditProject.aspx?PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" SortExpression="ProjectNumber" />--%>

                            <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                DataNavigateUrlFormatString="~/Project/ProjectTemplate.aspx?PID={0}"
                                DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                HeaderText="Project Number" SortExpression="ProjectNumber" />


                            <%--<asp:BoundField DataField="ProjectNumber" HeaderText="Project Number"
                                SortExpression="ProjectNumber" />--%>

                            <asp:BoundField DataField="ContractNumber" HeaderText="Contract Number"
                                SortExpression="ContractNumber" />

                            <asp:BoundField DataField="ORCID" HeaderText="ORCID"
                                SortExpression="ORCID" />

                            <asp:TemplateField HeaderText="PI" SortExpression="RosterName">
                                <EditItemTemplate>
                                    <%--sdsProjectPI--%>
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsPI"
                                        DataTextField="RosterName" DataValueField="RosterID" AppendDataBoundItems="true"
                                        SelectedValue='<%# Bind("ProjectPI") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProjectPI" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT [RosterID], [RosterName] FROM [Roster]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Last Update" SortExpression="LastUpdate">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" Text='<%# DateTime.Now.ToString() %>' runat="server"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelLastUpdate" runat="server" Text='<%# Bind("LastUpdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Status"
                                SortExpression="ProyectStatusName">
                                <EditItemTemplate>
                                    <asp:Label ID="LabelProyectStatusName" runat="server" Text='<%# Eval("ProyectStatusName") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProyectStatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fiscal Year" SortExpression="FiscalYearNumber">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsFYNumber"
                                        DataTextField="FiscalYearNumber" DataValueField="FiscalYearID"
                                        SelectedValue='<%# Bind("FiscalYearID") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsFYNumber" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT [FiscalYearID], [FiscalYearNumber], FiscalYearName FROM [FiscalYear]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelFiscalYearName" runat="server" Text='<%# Eval("FiscalYearName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand="DELETE FROM [Projects] WHERE [ProjectID] = @original_ProjectID"
                        InsertCommand="INSERT INTO [Projects] ([ProjectID], [ProjectNumber], [ProjectTitle], [ProjectPI], [DepartmentID], [CommID], [DateRegister], [LastUpdate], [ProjectStatusID]) VALUES (@ProjectID, @ProjectNumber, @ProjectTitle, @ProjectPI, @DepartmentID, @CommID, @DateRegister, @LastUpdate, @ProjectStatusID)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, 
                        (SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, 
                        (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, 
                        (SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName FROM Projects AS P"
                        UpdateCommand="UPDATE Projects SET  ContractNumber = @ContractNumber, ORCID = @ORCID, 
                        ProjectPI = @ProjectPI, LastUpdate = GETDATE(), FiscalYearID = @FiscalYearID WHERE (ProjectID = @original_ProjectID) 
                        ">
                        <DeleteParameters>
                            <asp:Parameter Name="original_ProjectID" Type="Object" />
                            <asp:Parameter Name="original_ProjectNumber" Type="String" />
                            <asp:Parameter Name="original_LastUpdate" Type="DateTime" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ProjectNumber" Type="String" />
                            <asp:Parameter Name="ProjectPI" Type="String" />
                            <asp:Parameter Name="FiscalYearID" />
                            <asp:Parameter Name="original_ProjectID" Type="Object" />
                            <asp:Parameter Name="original_ProjectNumber" Type="String" />
                            <asp:Parameter Name="original_ProjectPI" Type="String" />
                            <asp:Parameter Name="original_FiscalYearID" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="ProjectID" Type="Object" />
                            <asp:Parameter Name="ProjectNumber" Type="String" />
                            <asp:Parameter Name="ProjectTitle" Type="String" />
                            <asp:Parameter Name="ProjectPI" Type="String" />
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="CommID" Type="Int32" />
                            <asp:Parameter Name="DateRegister" Type="DateTime" />
                            <asp:Parameter Name="LastUpdate" Type="DateTime" />
                            <asp:Parameter Name="ProjectStatusID" Type="Int32" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:Parameter Name="ProjectPI" DefaultValue="" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
