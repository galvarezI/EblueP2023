<%@ Page Title="Estación Experimental Agrícola - Project List" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="Eblue.Project.ProjectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSourceCoordinatorDefault" runat="server"
        SelectCommand="
        select 
        r.RosterID as uid, r.RosterName description,
        DefaultRoleID = (select top 1 rl.RoleID from roles rl inner join RoleCategory rc on rc.UId = rl.RoleCategoryId where rc.IsAdministrative = 1)
        from roster r
        inner join RosterCategory rc on rc.UId = r.rosterCategoryId
        where
        rc.UId in (select top 1 rct.UId from RosterCategory rct where rct.IsProjectOfficer = 1)
        
        "></asp:SqlDataSource>
    

    <asp:SqlDataSource ID="SqlDataSourceAdministrator" runat="server"
        SelectCommand="
        select 
        r.RosterID as uid, r.RosterName description,
        DefaultRoleID = (select top 1 rl.RoleID from roles rl inner join RoleCategory rc on rc.UId = rl.RoleCategoryId where  rc.IsResearchDirector = 1)
        from roster r
        inner join RosterCategory rc on rc.UId = r.rosterCategoryId
        where
        rc.UId in (select top 1 rct.UId from RosterCategory rct where rct.IsAdministrator = 1)
        
        "></asp:SqlDataSource>


    <asp:SqlDataSource ID="SqlDataSourceManagerDefault" runat="server"
        SelectCommand="
        select 
        r.RosterID as uid, r.RosterName description
        from roster r
        inner join RosterCategory rc on rc.UId = r.rosterCategoryId
        where
        rc.UId in (select top 1 rct.UId from RosterCategory rct where rct.IsAdministrator = 1)
        
        "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceProccessStarter" runat="server"
        SelectCommand="
        select 
        ps.Uid, ps.Description, ps.EstatusID, pe.ProjectStatusName
        from
        WorkFlow wf
        inner join Process ps on ps.WorkflowId = wf.Uid
        left join ProjectStatus pe on pe.ProjectStatusID = ps.EstatusId
        where wf.IsForProject = 1
        and ps.UId in (select top 1 pst.UId from Process pst where pst.IsStarter = 1)
        
        "></asp:SqlDataSource>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>Project List</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">Project List</li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <!-- SELECT2 EXAMPLE -->
                <div class="card card-default">
                    <div class="card-header">
                        <h3 class="card-title">Data Info</h3>

                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>
                    </div>
                    <!-- /.card-header -->
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Project Short Title: </label>
                                    <%--<asp:Label ID="LabelProjectShortTitle" runat="server" Text="Project Short Title:"></asp:Label>--%>
                                    <asp:TextBox ID="TextBoxProjectShortTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <label for="TextBoxProjectNumber">Project number</label>
                                    <asp:TextBox runat="server" ID="TextBoxProjectNumber" CssClass="" ValidationGroup="RequiredFieldValidatorProjectNumber" TextMode="SingleLine" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProjectNumber" ValidationGroup="ModelAdd" ControlToValidate="TextBoxProjectNumber" runat="server" ErrorMessage="Field is required">
                                    </asp:RequiredFieldValidator>


                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">
                                    <label for="TextBoxContractNumber">Award/Accession/Contract number</label>
                                    <asp:TextBox runat="server" ID="TextBoxContractNumber" CssClass="" TextMode="SingleLine" TabIndex="2" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ModelAdd" ControlToValidate="TextBoxContractNumber" runat="server" ErrorMessage="Field is required">--%>
                                    </asp:RequiredFieldValidator>

                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="TextBoxORCID">Account number</label>
                                    <asp:TextBox runat="server" ID="TextBoxORCID" CssClass="" TextMode="SingleLine" TabIndex="3" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBoxORCID" ValidationGroup="ModelAdd" runat="server" ErrorMessage="Field is required">--%>
                                    </asp:RequiredFieldValidator>

                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="TextBoxTypeOfFunds">Type of Funds: </label>
                                    <asp:DropDownList ID="DropDownListTypeOfFunds" runat="server"
                                        CssClass="" TabIndex="2" AppendDataBoundItems="true"
                                        DataSourceID="SqlDataSourceListTF"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="null">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="DropDownListTypeOfFunds" ValidationGroup="ModelAdd" runat="server" ErrorMessage="Field is required">
                                    </asp:RequiredFieldValidator>


                                    <asp:SqlDataSource ID="SqlDataSourceListTF" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        select 
                                        FundTypeId as uid, FundTypeName as description
                                        from FundType


                    "></asp:SqlDataSource>
                                    <%--  sql command
select 
r.RosterID as uid, r.RosterName as description
from roster r
inner join RosterCategory rc on rc.UId = r.rosterCategoryId
where r.CanBePI = 1  order by r.RosterName --%>
                                </div>

                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="DropdownListPerformingOrganizations">Performing Organizations: </label>
                                    <asp:DropDownList ID="DropdownListPerformingOrganizations" runat="server"
                                        CssClass="" TabIndex="2" AppendDataBoundItems="true"
                                        DataSourceID="SqlDataSourceListPO"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="DropdownListPerformingOrganizations" ValidationGroup="ModelAdd" runat="server" ErrorMessage="Field is required">
                                    </asp:RequiredFieldValidator>

                                    <asp:SqlDataSource ID="SqlDataSourceListPO" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="select 
                                        POrganizatonId as uid, POrganizationName as description
                                        from POrganization


                    "></asp:SqlDataSource>
                                    <%--  sql command
select 
r.RosterID as uid, r.RosterName as description
from roster r
inner join RosterCategory rc on rc.UId = r.rosterCategoryId
where r.CanBePI = 1  order by r.RosterName --%>
                                </div>
                                <!-- /.form-group -->


                            </div>
                            <!-- /.col -->
                            <div class="col-md-6">

                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="DropDownPrincipalInvestigator">Principal Investigator</label>


                                    <asp:DropDownList ID="DropDownPrincipalInvestigator" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true"
                                        DataSourceID="SqlDataSourceListPI"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDropDownPrincipalInvestigator" ValidationGroup="ModelAdd" ControlToValidate="DropDownPrincipalInvestigator" runat="server" ErrorMessage="Field Required"></asp:RequiredFieldValidator>



                                    <asp:SqlDataSource ID="SqlDataSourceListPI" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
select 
r.RosterID as uid, r.RosterName as description
from roster r
inner join RosterCategory rc on rc.UId = r.rosterCategoryId
where r.CanBePI = 1  order by r.RosterName
                    "></asp:SqlDataSource>


                                </div>
                                <!-- /.form-group -->


                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="DropdownlistFiscalYear">Fiscal Year</label>


                                    <asp:DropDownList ID="DropdownlistFiscalYear" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListFY"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFiscalYear" ValidationGroup="ModelAdd" ControlToValidate="DropdownlistFiscalYear" runat="server" ErrorMessage="Field Required"></asp:RequiredFieldValidator>

                                    <asp:SqlDataSource ID="SqlDataSourceListFY" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT FiscalYearID as uid, FiscalYearName as description FROM FiscalYear ORDER BY FiscalYearName DESC"></asp:SqlDataSource>


                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LabelProgramaticArea" runat="server" Text="Programatic Area:"></asp:Label>
                                            <asp:DropDownList ID="DropDownListProgramaticArea" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceProgramaticArea" DataTextField="ProgramAreaName" DataValueField="ProgramAreaID"></asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceProgramaticArea" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [ProgramArea]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LabelCommodity" runat="server" Text="Commodity:"></asp:Label>
                                            <asp:DropDownList ID="DropDownListCommodity" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceCommodity"
                                                DataTextField="CommName" DataValueField="CommID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceCommodity" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Commodity]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LabelDepartment" runat="server" Text="Department:"></asp:Label>
                                            <asp:DropDownList ID="DropDownListDepartment" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceDepartment"
                                                DataTextField="DepartmentName" DataValueField="DepartmentID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LabelSubstation" runat="server" Text="Substation or Region:"></asp:Label>
                                            <asp:DropDownList ID="DropDownListSubstation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                                DataTextField="LocationName" DataValueField="LocationID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                SelectCommand="SELECT * FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="LabelProjectObjective" runat="server" Text="Project Objective(s):"></asp:Label>
                                            <asp:TextBox ID="TextBoxProjectObjective" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
                                <!-- /.form-group -->

                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.card-body -->
                    <div class="card-footer">
                        <asp:Button TabIndex="6" ID="buttonNewModel" ValidationGroup="ModelAdd" OnClick="ButtonSaveNewProject_Click" CssClass="btn btn-primary" runat="server" Text="Add" />


                        <asp:Button TabIndex="7" ID="buttonClearModel" OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />


                    </div>


                </div>
                <!-- /.row -->

                <!--List "Data List" -->
                <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">Data List</h3>
                            </div>
                            <!-- /.card-header 
                                      OnRowUpdated="gvModel_RowUpdated"
                                    -->
                            <div class="card-body">


                                <asp:GridView ID="gvModel" runat="server" CssClass="gridview table table-bordered table-striped"
                                    DataKeyNames="ProjectID"
                                    AutoGenerateColumns="false"
                                    DataSourceID="Projects">
                                    <RowStyle />
                                    <AlternatingRowStyle />
                                    <Columns>

                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                            ShowEditButton="True" />

                                        <asp:HyperLinkField DataNavigateUrlFields="ProjectID"
                                            DataNavigateUrlFormatString="~/Project/ProjectTemplate.aspx?PID={0}"
                                            DataTextField="ProjectNumber" DataTextFormatString="{0}"
                                            HeaderText="Project Number" SortExpression="ProjectNumber" />


                                        <%--<asp:BoundField DataField="ProjectNumber" HeaderText="Project Number"
                                SortExpression="ProjectNumber" />--%>

                                        <asp:BoundField DataField="ContractNumber" HeaderText="Award/Accession/Contract number"
                                            SortExpression="ContractNumber" />

                                        <asp:BoundField DataField="ORCID" HeaderText="Account number"
                                            SortExpression="ORCID" />

                                        <asp:TemplateField HeaderText="PI" SortExpression="RosterName">
                                            <EditItemTemplate>
                                                <%--sdsProjectPI--%>
                                                <asp:DropDownList ID="DropDownListProjectPI" runat="server" DataSourceID="SqlDataSourceListPI"
                                                    DataTextField="description" DataValueField="uid"
                                                    SelectedValue='<%# Bind("ProjectPI") %>'>
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

                                <asp:SqlDataSource ID="Projects" runat="server"
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
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>

                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
