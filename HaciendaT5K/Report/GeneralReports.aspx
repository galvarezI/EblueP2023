<%@ Page Title="Estación Experimental Agrícola - General Reports" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="GeneralReports.aspx.cs" Inherits="Eblue.Reports.GeneralReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <asp:SqlDataSource ID="SqlDataSourceCoordinatorDefault" runat="server"
        SelectCommand="
        select 
        r.RosterID as uid, r.RosterName description,
        DefaultRoleID = (select top 1 rl.RoleID from roles rl inner join RoleCategory rc on rc.UId = rl.RoleCategoryId where rc.IsInvestigationOfficer = 1)
        from roster r
        inner join RosterCategory rc on rc.UId = r.rosterCategoryId
        where
        rc.UId in (select top 1 rct.UId from RosterCategory rct where rct.IsCoordinator = 1)
        
        "></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceManagerDefault" runat="server"
        SelectCommand="
        select 
        r.RosterID as uid, r.RosterName description
        from roster r
        inner join RosterCategory rc on rc.UId = r.rosterCategoryId
        where
        rc.UId in (select top 1 rct.UId from RosterCategory rct where rct.IsManager = 1)
        
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
                        <h1>General Reports</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">General Reports</li>
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
                            <div class="col-md-6">
                                <div class="form-group">

                                    <label for="TextBoxProjectNumber">Name</label>
                                    <asp:TextBox runat="server" ID="TextBoxProjectNumber" CssClass="" TextMode="SingleLine" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="TextBoxProjectNumberValidatorRequired" Display="Dynamic" SetFocusOnError="true"
                                        ControlToValidate="TextBoxProjectNumber" runat="server"
                                        ErrorMessage="Please Add a Report Name" ValidationGroup="ModelAdd">

                                    </asp:RequiredFieldValidator>

                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">
                                    <label for="TextBoxContractNumber">Caption</label>
                                    <asp:TextBox runat="server" ID="TextBoxContractNumber" CssClass="" TextMode="SingleLine" TabIndex="2" />
                                    <asp:RequiredFieldValidator ID="TextBoxContractNumberValidatorRequired" Display="Dynamic"
                                        ControlToValidate="TextBoxContractNumber" runat="server" SetFocusOnError="true"
                                        ErrorMessage="Please Add a Report Caption" ValidationGroup="ModelAdd">
                                    </asp:RequiredFieldValidator>
                                </div>



                            </div>
                            <!-- /.col -->
                            <div class="col-md-6">

                                <!-- /.form-group -->
                                <div class="form-group">

                                    <label for="TextBoxORCID">Engine</label>
                                    <asp:TextBox runat="server" ID="TextBoxORCID" CssClass="" TextMode="MultiLine" Rows="6" TabIndex="3" />
                                    <asp:RequiredFieldValidator ID="TextBoxORCIDValidatorRequired" Display="Dynamic"
                                        ControlToValidate="TextBoxORCID" runat="server"
                                        ErrorMessage="Please Add a Report Engine" ValidationGroup="ModelAdd">

                                    </asp:RequiredFieldValidator>

                                </div>
                                <!-- /.form-group -->

                                <!-- /.form-group -->
                                <div class="form-group" style="display: none">

                                    <label for="DropDownPrincipalInvestigator">Principal Investigator</label>


                                    <asp:DropDownList ID="DropDownPrincipalInvestigator" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true"
                                        DataSourceID="SqlDataSourceListPI"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>



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
                                <div class="form-group" style="display: none">

                                    <label for="DropdownlistFiscalYear">Fiscal Year</label>


                                    <asp:DropDownList ID="DropdownlistFiscalYear" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListFY"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>


                                    <asp:SqlDataSource ID="SqlDataSourceListFY" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT FiscalYearID as uid, FiscalYearName as description FROM FiscalYear ORDER BY FiscalYearName DESC"></asp:SqlDataSource>

                                </div>
                                <!-- /.form-group -->

                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.card-body -->
                    <div class="card-footer">
                        <asp:Button TabIndex="6" ID="buttonNewModel" ValidationGroup="ModelAdd" OnClick="ButtonSaveNewProject_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                        <asp:Button TabIndex="6" ID="buttonHideModel" OnClick="buttonHideModel_Click" CssClass="btn btn-default" runat="server" Text="Hide Report" />


                        <asp:Button TabIndex="7" ID="buttonGetterModel" OnClick="buttonGetterModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Template" />

                        <asp:Button TabIndex="7" ID="buttonClearModel" OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />
                    </div>


                </div>
                <asp:Panel runat="server" ID="pivotpanel" Visible="false">
                    <input type="hidden" id="pivotcaption" name="pivotcaption" value="General Report Generator" />
                    <div class="row pivotstackclass" id="pivotstacker">
                        <div class="col-12">
                            <div class="card">
                                <%-- <div class="card-header"></div>
                                   cellpadding="5"
                                   style="margin: 30px;"
                                   style="display:none"
                                --%>
                                <div class="card-body">

                                    <div id="output">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <!-- /.row -->
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
                                    DataKeyNames="Uid"
                                    AutoGenerateColumns="false"
                                    DataSourceID="qrysource"
                                    OnRowCommand="gvModel_RowCommand">
                                    <RowStyle />
                                    <AlternatingRowStyle />
                                    <Columns>

                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                            ShowEditButton="True" ShowSelectButton="true" SelectText="🔍" />

                                        <asp:HyperLinkField DataNavigateUrlFields="Uid"
                                            DataNavigateUrlFormatString="~/Report/GeneralReport.aspx?UID={0}"
                                            DataTextField="Name" DataTextFormatString="{0}"
                                            HeaderText="Report Name" SortExpression="ReportName" />


                                        <%--<asp:BoundField DataField="ProjectNumber" HeaderText="Project Number"
                                SortExpression="ProjectNumber" />--%>

                                        <asp:BoundField DataField="caption" HeaderText="Caption"
                                            SortExpression="Caption" />

                                        <asp:BoundField DataField="varengine" HeaderText="Engine"
                                            SortExpression="Engine" />



                                        <%--<asp:ButtonField ButtonType="Button" CommandName="ViewReport" command
            Text="View Report" />
                                DeleteCommand="DELETE FROM [Projects] WHERE [ProjectID] = @original_ProjectID $filter"
                                <asp:Parameter Name="original_ProjectNumber" Type="String" />
                            <asp:Parameter Name="original_LastUpdate" Type="DateTime" />
                                InsertCommand="INSERT INTO [Projects] ([ProjectID], [ProjectNumber], [ProjectTitle], [ProjectPI], [DepartmentID], [CommID], [DateRegister], [LastUpdate], [ProjectStatusID]) VALUES (@ProjectID, @ProjectNumber, @ProjectTitle, @ProjectPI, @DepartmentID, @CommID, @DateRegister, @LastUpdate, @ProjectStatusID)"
                        
                              SelectCommand="SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, 
                        (SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, 
                        (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, 
                        (SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName FROM Projects AS P"
                          
                                UpdateCommand="UPDATE Projects SET  ContractNumber = @ContractNumber, ORCID = @ORCID, 
                        ProjectPI = @ProjectPI, LastUpdate = GETDATE(), FiscalYearID = @FiscalYearID WHERE (ProjectID = @original_ProjectID) 
                        ">
                                        --%>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:SqlDataSource ID="qrysource" runat="server"
                                    ConflictDetection="CompareAllValues"
                                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM reportsource WHERE [Uid] = @original_Uid "
                                    OldValuesParameterFormatString="original_{0}"
                                    SelectCommand="SELECT * from reportsource"
                                    UpdateCommand="UPDATE reportsource SET caption = @caption, varengine = @varengine, name= @name WHERE (Uid = @original_Uid) 
                        ">
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_Uid" Type="Object" />

                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="varengine" Type="String" />

                                    </UpdateParameters>

                                    <SelectParameters>
                                        <asp:Parameter Name="Uid" DefaultValue="" />
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

    <script>

        var varhasjson;
        var varjsontext;
        var varjsonpivot;
        var tagcounter = 0;
        var tagpivotno = '';
        var tagpivotid = 'output';
        var tagpivot = document.getElementById(tagpivotid);

        var varjson = {
            vartmold: { caption: "General Report Generator" },
            varthead: { items: [{ rowno: 1, id: 'guid-value', status: 'new', contractno: '344555', pinvestigator: 'julia' }] },
            vartbody: { rows: ['rowno', 'status'], cols: ['contractno', 'pinvestigator'] }
        };
        function generatepivot() {
            var divoutput = newtagpivot();// document.getElementById('output');
            //clearpivotoutput(divoutput);


            binddivpivot(divoutput, varjson);
        }

        function newtagpivot() {
            var tagparent = $(tagpivot).parent()[0];
            //document.getElementById(tagpivotid);
            if (tagcounter == 0) { }
            else { }
            tagcounter++;
            tagpivotid = tagpivotid + '_' + tagcounter;
            tagpivot = document.createElement("DIV");
            tagpivot.id = tagpivotid;
            tagparent.appendChild(tagpivot);
            return tagpivot;
        }

        function clearpivotoutput(tag) {

            $(tag).find('table').remove();
        }
        function binddivpivot(tag, exp) {
            //var data = getdata(customData);
            var tagcaption = document.getElementById('pivotcaption');
            $(tagcaption).val(exp.vartmold.caption);
            var thead = exp.varthead.items;
            var tbody = exp.vartbody;
            setpivottag(tag, thead, tbody);
        }
        function setpivottag(tag, lthead, ltbody) {
            $(tag).pivotUI(
                lthead,
                ltbody
            );

        }
        function getdatapivot(flagdata) {
            var iscustomdata = flagdata || customData;
            var result;

            if (iscustomdata) {
                var dataitems = getarrayFromSet(dataItemSet);
                var datarows = getarrayFromSet(rowsItemSet);
                var datacols = getarrayFromSet(colsItemSet);

                varthead.items = dataitems;
                vartbody.rows = datarows;
                vartbody.cols = datacols;
            }

            result = { thead: varthead.items, tbody: vartbody };

            return result;

        }
        window.addEventListener('DOMContentLoaded', (event) => {

            console.clear();
            console.log('<head>DOM fully loaded and parsed');

            <% if (this.HasJsonText)
        {  %>
            varjsontext = '<%= this.jsonText %>';
            varhasjson = new Boolean( <%= this.HasJsonText.ToString().ToLower() %> )
            <% } %>

            if (varhasjson) {
                var rpt = JSON.parse(varjsontext);
                varjson.vartmold.caption = rpt.caption;
                varjson.varthead.items = rpt.items;
                varjson.vartbody.rows = rpt.rows;
                varjson.vartbody.cols = rpt.cols;
                varjsonpivot = rpt;
                generatepivot();
            }

            /*
             var varjson = {
            vartmold: {caption:"General Report Generator"},
            varthead: { items: [{ rowno: 1, id: 'guid-value', status: 'new', contractno: '344555', pinvestigator: 'julia' }] },
            vartbody: { rows: ['rowno', 'status'], cols: ['contractno', 'pinvestigator'] }
                };
             */

            //var starterbutton = document.getElementById("defaultbutton");
            //if (starterbutton) addeventhandler(starterbutton, doclick, clickMember);
            //initpivot();
            /*
            setTimeout(() => {

                var pivottables = document.querySelectorAll("#output table");
                $(pivottables).each(function () {
                    //$(this).attr("style", "width:100%; height:100%");
                });
            }, 5000);*/

        });
        //initpivot();
    </script>
</asp:Content>
