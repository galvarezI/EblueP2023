<%@ Page Title="Estación Experimental Agrícola - Project(s) In Which I Participate" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="whichIparticipate.aspx.cs" Inherits="Eblue.Project.whichIparticipate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .navitemActive {
            border:1px solid;
            border-color: gray !important
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper" >
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>My Projects</h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                                <li class="breadcrumb-item active">My Projects</li>
                            </ol>
                        </div>
                    </div>
                </div>
                <!-- /.container-fluid -->
            </section>

            <!-- Main content -->
    <section class="content">
         <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">Projects by Status</h3>
                            </div>
                            <!-- /.card-header 
                                      OnRowUpdated="gvModel_RowUpdated"
                                    -->
                            <div class="card-body">


                                <asp:GridView ID="GridView1" runat="server" CssClass="gridview table table-bordered table-striped"
                                    DataKeyNames="ProjectID"
                                    AutoGenerateColumns="false"
                                    DataSourceID="Projects">
                                    <RowStyle />
                                    <AlternatingRowStyle />
                                    <Columns>

<%--                                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                            ShowEditButton="True" />--%>

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

                                      
                                        <%--<asp:TemplateField HeaderText="Fiscal Year" SortExpression="FiscalYearNumber">
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
                                        </asp:TemplateField>--%>

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
                        (SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName FROM Projects AS P
                                    "
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
      <div class="row">
        <div class="col-md-3">
          <%--<a href="compose.html" class="btn btn-primary btn-block mb-3">Compose</a>
               <asp:LinkButton ID="btncompose" runat="server" Text="compose" OnClick="btncompose_Click" CssClass="btn btn-primary btn-block mb-3">

            </asp:LinkButton>
              --%>
           
        <%--  <div class="card">
            <div class="card-header">
              <h3 class="card-title">Status</h3>

              <div class="card-tools">
                <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
              </div>
            </div>
            <div class="card-body p-0">
        --%>       <%-- <ul  class="nav nav-pills flex-column">

                    <asp:Panel  ID="navlinkContentArea" runat="server">
                            
                    </asp:Panel>
                    <li class="nav-item active">

                        <asp:LinkButton Visible="false" ID="newButton" runat="server"  CssClass="nav-link" OnClick="newButton_Click"> 
                            <i class="fa fa-inbox" style="display:none !important"></i>New
                   
                            <span class="badge bg-primary float-right">12</span>
                        </asp:LinkButton>
                        
                    </li>

                    <li class="nav-item">

                        <asp:LinkButton Visible="false" ID="closedButton" runat="server"  CssClass="nav-link" OnClick="closedButton_Click"> 
                            <i style="display:none !important" class="fa fa-inbox"></i>Closed
                   
                            <span class="badge bg-warning float-right">65</span>
                        </asp:LinkButton>
                        
                    </li>
                </ul>--%>
              <ul style="display:none !important" class="nav nav-pills flex-column">
                <li class="nav-item active">
                  <a href="#" class="nav-link">
                    <i class="fa fa-inbox"></i> Inbox
                    <span class="badge bg-primary float-right">12</span>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-envelope-o"></i> Sent
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-file-text-o"></i> Drafts
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-filter"></i> Junk
                    <span class="badge bg-warning float-right">65</span>
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-trash-o"></i> Trash
                  </a>
                </li>
              </ul>
            </div>
            <!-- /.card-body -->
          </div>


          <!-- /. box -->
          <div style="display:none !important" class="card">
            <div class="card-header">
              <h3 class="card-title">Labels</h3>
                 que tal
              <div class="card-tools">
                <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
              </div>
            </div>
            <div class="card-body p-0">
              <ul class="nav nav-pills flex-column">
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-circle-o text-danger"></i>
                    Important
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-circle-o text-warning"></i> Promotions
                  </a>
                </li>
                <li class="nav-item">
                  <a href="#" class="nav-link">
                    <i class="fa fa-circle-o text-primary"></i>
                    Social
                  </a>
                </li>
              </ul>
            </div>
            <!-- /.card-body -->
          </div>
          <!-- /.card -->
        </div>
        <!-- /.col -->
       <%-- <div class="col-md-9">
          <div class="card card-primary card-outline">
            <div class="card-header">
              <h3 class="card-title">Records</h3>

              <div class="card-tools">
                <div style="display:none !important" class="input-group input-group-sm">
                  <input type="text" class="form-control" placeholder="Search Mail">
                  <div class="input-group-append">
                    <div class="btn btn-primary">
                      <i class="fa fa-search"></i>
                    </div>
                  </div>
                </div>
              </div>
              <!-- /.card-tools -->
            </div>
            <!-- /.card-header -->
            <div class="card-body p-0">
              <div class="mailbox-controls" style="display:none !important">
                <!-- Check all button -->
                <button type="button" class="btn btn-default btn-sm checkbox-toggle"><i class="fa fa-square-o"></i>
                </button>
                <div class="btn-group">
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>
                </div>
                <!-- /.btn-group -->
                <button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>
                <div class="float-right">
                  1-50/200
                  <div class="btn-group">
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-left"></i></button>
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-right"></i></button>
                  </div>
                  <!-- /.btn-group -->
                </div>
                <!-- /.float-right -->
              </div>--%>

                <%--
                    ShowHeaderWhenEmpty="true"  
                     EmptyDataText="no records found!"
                      Caption="this Caption"  
                      CaptionAlign="Top"
                      ShowFooter="true"
                    EmptyDataText="no data found!"
                    --%>
            
              <div class="table-responsive mailbox-messages">
                  <asp:GridView
                      ID="gvModel" runat="server" CssClass="gridview table table-bordered table-striped"
                                        DataKeyNames="ProjectID"
                                        AutoGenerateColumns="false" 
                                        DataSourceID="Projects" 
                     ShowHeaderWhenEmpty="true" 
                      
                                    
                      >
                    
                      <%--<EmptyDataTemplate>
                          No Records Found!
                           
                        </EmptyDataTemplate>--%>
                  </asp:GridView>
                  <asp:SqlDataSource ID="Projects" runat="server" 
                      ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        SelectCommand="
                      SELECT
                        ROW_NUMBER() over  (order by  prj.lastUpdate) rowNumber,
                        prj.ProjectID, 
                        prj.ProjectNumber,
                        ppw.EstatusId,
                        ps.ProjectStatusName,
                        CONCAT(prj.ProjectTitle, prj.ContractNumber , prj.Objectives) Title  ,
                        --dbo.displayInterval (prj.LastUpdate) interval,
                        
                        prj.DateRegister,
                        prj.LastUpdate,
                        concat(
                        FORMAT( prj.DateRegister, 'd', 'en-US' ) , ' ', FORMAT( prj.DateRegister, 'hh:mm:ss', 'en-US' )
                        )
                         DateRegisterString,
                         concat(
                        FORMAT( prj.LastUpdate, 'd', 'en-US' ) , ' ', FORMAT( prj.LastUpdate, 'hh:mm:ss', 'en-US' )
                        )
                         LastUpdateString,
                       case 
                            when 
                                hStatus.enabledForOnlyDirectiveManager = 1 or hStatus.enabledForDirectiveManager = 1 
                                or hStatus.enabledForInvestigationOfficer = 1 
                                or hStatus.enabledForDirectiveLeader = 1 
                                or hStatus.enabledForAssistantLeader = 1
                                
                                        then cast( 0 as bit)
                            else cast( 1 as bit) end showEmptyHstatus,
                        case 
                            when hStatus.enabledForOnlyDirectiveManager = 1 or hStatus.enabledForDirectiveManager = 1 then cast( 1 as bit)
                            else cast( 0 as bit) end hasHSdm,
                        case 
                            when hStatus.enabledForInvestigationOfficer = 1 then cast( 1 as bit)
                            else cast( 0 as bit) end hasHSio,
                        case 
                            when hStatus.enabledForDirectiveLeader = 1 then cast( 1 as bit)
                            else cast( 0 as bit) end hasHSdl,
                        case 
                            when hStatus.enabledForAssistantLeader = 1 then cast( 1 as bit)
                            else cast( 0 as bit) end hasHSal,
                        hStatus.*

                        FROM Projects AS prj
                        left join ProcessProjectWay ppw on ppw.Uid = prj.ProcessProjectWayID and ppw.ProjectId = prj.ProjectID
                        left join ProjectStatus ps on ps.ProjectStatusID = ppw.EstatusId
                        Cross apply dbo.GetStatusHandlers(prj.ProjectId) hStatus
                        where
                        (prj.ProjectPI = @RosterId and ppw.EstatusId = @EstatusId )
                        or
                      ( 
                        ppw.EstatusId = @EstatusId and
                        exists (select 1 from SciProjects sci where sci.ProjectID = prj.ProjectID and sci.RosterID in (@RosterId) )
                      )
                        or 
                      (
                        ppw.EstatusId = @EstatusId and
                        exists (select 1 from PlayerProject pp where pp.ProjectID = prj.ProjectID and pp.RosterID in (@RosterId) )
                      )
                      " 
                      >
                      <SelectParameters>
                          <%--<asp:SessionParameter Name ="devMode" SessionField="runmode" DefaultValue="{noset}"  Type="String" DbType="String"
                               Size="6" />--%>
                          
                          <asp:Parameter Name="RosterId" DefaultValue="" />
                          <asp:Parameter Name="EstatusId" DefaultValue="0" />
                          <%--<asp:SessionParameter Name ="runmode" SessionField="runmode" DefaultValue="hi" Type="String"  Direction="InputOutput" />--%>
                      </SelectParameters>

                  </asp:SqlDataSource>
                <table style="display:none !important" class="table table-hover table-striped">
                  <tbody>
                  <tr>                   
                    
                    <td class="mailbox-name"><a href="read-mail.html">Alexander Pierce</a></td>
                    <td class="mailbox-subject"><b>AdminLTE 3.0 Issue</b> - Trying to find a solution to this problem...
                    </td>
                    <td class="mailbox-attachment"></td>
                    <td class="mailbox-date">5 mins ago</td>
                  </tr>
                  
                  </tbody>
                </table>
                <!-- /.table -->
              </div>
              <!-- /.mail-box-messages -->
            </div>
            <!-- /.card-body -->
            <div class="card-footer p-0">
              <div class="mailbox-controls" style="display:none !important">
                <!-- Check all button -->
                <button type="button" class="btn btn-default btn-sm checkbox-toggle"><i class="fa fa-square-o"></i>
                </button>
                <div class="btn-group">
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                  <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>
                </div>
                <!-- /.btn-group -->
                <button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>
                <div class="float-right">
                  1-50/200
                  <div class="btn-group">
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-left"></i></button>
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-right"></i></button>
                  </div>
                  <!-- /.btn-group -->
                </div>
                <!-- /.float-right -->
              </div>
            </div>
          </div>
          <!-- /. box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
        </div>
</asp:Content>
