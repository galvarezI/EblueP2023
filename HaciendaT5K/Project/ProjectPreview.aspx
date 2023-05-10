<%@ Page Title="Estación Experimental Agrícola - Project Preview" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProjectPreview.aspx.cs" Inherits="Eblue.Project.ProjectPreview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript">  
        function enablePostBack() {
            //T1 is the first argument(name of our control) I mentioned earlier and give the  
            // value of second argument as "" that's all  
            __TdoPostBack("T1", "");
        }

        function SelectTabFromServer(name, line)
        {
            __TdoPostBack('tab_'+name, line);
        }

        function __TdoPostBack(eventTarget, eventArgument) {
            document.aspnetForm.__EVENTTARGET.value = eventTarget;
            document.aspnetForm.__EVENTARGUMENT.value = eventArgument;
            document.aspnetForm.submit();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManagerEdit" runat="server"></asp:ScriptManager>
    <asp:SqlDataSource ID="SqlDataSourcePlayerProject" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select top 1 
        pp.ProjectID, pp.RosterId, pp.RoleId, pr.RoleName, pr.RoleCategoryId, rc.Description RoleTypeDescription,
        iif(rc.Description is null, cast(0 as bit), cast(1 as bit)) HasRoleType, 
        CONCAT(pr.RoleName, iif(rc.Description is null, '', concat(' (', rc.description, ')' ) )) RoleCaption 
        from PlayerProject pp
        inner join projects p on p.ProjectID = pp.ProjectID
        inner join Roster r on r.RosterID = pp.RosterId
        inner join Roles pr on pr.RoleID = pp.RoleId
        left join RoleCategory rc on rc.UId = pr.RoleCategoryId
        where 
        pp.ProjectID = @ProjectID and pp.RosterID = @RosterID

        "
        
        ></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceProjectSignsSelect" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select 
        ROW_NUMBER() over (order by ps.signdate ) as rowNumber, 
        ps.UId,
        ps.OrderLine, ps.SignDate, ps.SignData, ps.RosterData, ps.ProjectID, ps.RosterID, ps.RoleId, r.RoleName, pr.RosterName 

        from projectSigns ps
        inner join Projects p on p.ProjectID = ps.ProjectID
        inner join roster pr on pr.RosterID = ps.RosterID
        left join Roles r on r.RoleID = ps.RoleId
        left join RoleCategory rc on rc.UId = r.RoleCategoryId
        where 
        ps.ProjectID = @ProjectID

        "
        
        ></asp:SqlDataSource>
   

    <% 
        var sections = this.targetSections;
        Eblue.Code.TargetSection section_a1 = null;
        Eblue.Code.TargetSection section_a2 = null;
        Eblue.Code.TargetSection section_a3 = null;
        Eblue.Code.TargetSection section_a4 = null;
        Eblue.Code.TargetSection section_a5 = null;
        Eblue.Code.TargetSection section_a6 = null;
        Eblue.Code.TargetSection section_a7 = null;
        Eblue.Code.TargetSection section_a8 = null;
        Eblue.Code.TargetSection section_a9 = null;
        Eblue.Code.TargetSection section_b1 = null;
        Eblue.Code.TargetSection section_b2 = null;

        //char circle = char.MinValue;
        //string title = string.Empty;

        //bool whenData = false;
        //bool dataCapEdit = false;
        //bool dataCapDetail = false;

        //bool canDataDetail = false;
        //bool canDataEdit = false;
        //bool canDataEditAndDetail = false;

        //whenData = section_a1.whenData;
        //dataCapEdit = section_a1.dataCapEdit && whenData;
        //dataCapDetail = section_a1.dataCapDetail && whenData;

        //canDataDetail = dataCapDetail || dataCapEdit;
        //canDataEdit = dataCapEdit;
        //canDataEditAndDetail = dataCapEdit && dataCapDetail;

        // bool whenList = false;
        //bool listCapDetail = false;
        //bool listCapAdd = false;
        //bool listCapRemove = false;
        //bool listCapEdit = false;

        //bool canListDetail = false;
        //bool canListAdd = false;
        //bool canListRemove = false;
        //bool canListEdit = false;
        //bool canListEditAndDetail = false;

        if (sections == null)
        {
            //var stop = true;
        }
        else
        {
            var tagier_section_a1 = Eblue.Utils.ConstantsTools.tagier_project_section_a1;
            var tagier_section_a2 = Eblue.Utils.ConstantsTools.tagier_project_section_a2;
            var tagier_section_a3 = Eblue.Utils.ConstantsTools.tagier_project_section_a3;
            var tagier_section_a4 = Eblue.Utils.ConstantsTools.tagier_project_section_a4;
            var tagier_section_a5 = Eblue.Utils.ConstantsTools.tagier_project_section_a5;
            var tagier_section_a6 = Eblue.Utils.ConstantsTools.tagier_project_section_a6;
            var tagier_section_a7 = Eblue.Utils.ConstantsTools.tagier_project_section_a7;
            var tagier_section_a8 = Eblue.Utils.ConstantsTools.tagier_project_section_a8;
            var tagier_section_a9 = Eblue.Utils.ConstantsTools.tagier_project_section_a9;
            var tagier_section_b1 = Eblue.Utils.ConstantsTools.tagier_project_section_b1;
            var tagier_section_b2 = Eblue.Utils.ConstantsTools.tagier_project_section_b2;

            #region MyRegion

            #endregion

            if (sections.ContainsKey(tagier_section_a1))
            {
                section_a1 = sections[tagier_section_a1];
            }
            if (sections.ContainsKey(tagier_section_a2))
            {
                section_a2 = sections[tagier_section_a2];
            }
            if (sections.ContainsKey(tagier_section_a3))
            {
                section_a3 = sections[tagier_section_a3];
            }

            if (sections.ContainsKey(tagier_section_a4))
            {
                section_a4 = sections[tagier_section_a4];
            }

            if (sections.ContainsKey(tagier_section_a5))
            {
                section_a5 = sections[tagier_section_a5];
            }
            if (sections.ContainsKey(tagier_section_a6))
            {
                section_a6 = sections[tagier_section_a6];
            }
            if (sections.ContainsKey(tagier_section_a7))
            {
                section_a7 = sections[tagier_section_a7];
            }
            if (sections.ContainsKey(tagier_section_a8))
            {
                section_a8 = sections[tagier_section_a8];
            }
            if (sections.ContainsKey(tagier_section_a9))
            {
                section_a9 = sections[tagier_section_a9];
            }
            if (sections.ContainsKey(tagier_section_b1))
            {
                section_b1 = sections[tagier_section_b1];
            }
            if (sections.ContainsKey(tagier_section_b2))
            {
                section_b2 = sections[tagier_section_b2];
            }
        }

        #region MyRegion
        const string nodisplay = "display:none";
        const string isactive = "active";
        #endregion
        if (section_a1 != null)
        {
            //whenData = section_a1.whenData;
            //dataCapEdit = section_a1.dataCapEdit && whenData;
            //dataCapDetail = section_a1.dataCapDetail && whenData;

            //canDataDetail = dataCapDetail || dataCapEdit;
            //canDataEdit =  dataCapEdit;
            //canDataEditAndDetail = dataCapEdit && dataCapDetail;
        }


        #region section expression
        //int.TryParse(this.TabSelectedIndex, out int tabSelectedIndex);
        var tabSelectedIndex = Convert.ToInt32(this.TabSelectedIndex ?? "0");

        var exp = new
        {
            _ = new {
                tabStyle = ( tabSelectedIndex !=0) ? string.Empty : isactive,
            },
            a = new
            {   tabStyle = (section_a1 == null || tabSelectedIndex !=1) ? string.Empty : isactive,
                exp = section_a1 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[1],
                cardTitle = (section_a1 == null) ? string.Empty : section_a1.description,
                cardStyle =  (section_a1 == null || section_a1.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a1 == null || !section_a1.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a1 == null || !section_a1.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a1 == null || !section_a1.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a1 == null || !section_a1.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a1 == null || !section_a1.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a1 == null || !section_a1.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            b = new
            {
                tabStyle = (section_a2 == null || tabSelectedIndex !=2) ? string.Empty : isactive,
                exp = section_a2?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[2],
                cardTitle = (section_a2 == null) ? string.Empty : section_a2.description,
                cardStyle = (section_a2 == null || section_a2.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a2 == null || !section_a2.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a2 == null || !section_a2.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a2 == null || !section_a2.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a2 == null || !section_a2.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a2 == null || !section_a2.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a2 == null || !section_a2.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            c = new
            {   tabStyle = (section_a3 == null || tabSelectedIndex !=3) ? string.Empty : isactive,
                exp = section_a3 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[3],
                cardTitle = (section_a3 == null) ? string.Empty : section_a3.description,
                cardStyle = (section_a3 == null || section_a3.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a3 == null || !section_a3.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a3 == null || !section_a3.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a3 == null || !section_a3.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a3 == null || !section_a3.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a3 == null || !section_a3.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a3 == null || !section_a3.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            d = new
            {
                tabStyle = (section_a4 == null || tabSelectedIndex !=4) ? string.Empty : isactive, 
                exp = section_a4?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[4],
                cardTitle = (section_a4 == null) ? string.Empty : section_a4.description,
                cardStyle = (section_a4 == null || section_a4.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a4 == null || !section_a4.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a4 == null || !section_a4.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a4 == null || !section_a4.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a4 == null || !section_a4.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a4 == null || !section_a4.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a4 == null || !section_a4.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            e = new
            {
                tabStyle = (section_a5 == null || tabSelectedIndex !=5) ? string.Empty : isactive, 
                exp = section_a5?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[5],
                cardTitle = (section_a5 == null) ? string.Empty : section_a5.description,
                cardStyle = (section_a5 == null || section_a5.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a5 == null || !section_a5.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a5 == null || !section_a5.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a5 == null || !section_a5.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a5 == null || !section_a5.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a5 == null || !section_a5.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a5 == null || !section_a5.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            f = new
            {
                tabStyle = (section_a6 == null || tabSelectedIndex !=6) ? string.Empty : isactive, 
                exp = section_a6 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[6],
                cardTitle = (section_a6 == null) ? string.Empty : section_a6.description,
                cardStyle = (section_a6 == null || section_a6.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a6 == null || !section_a6.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a6 == null || !section_a6.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a6 == null || !section_a6.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a6 == null || !section_a6.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a6 == null || !section_a6.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a6 == null || !section_a6.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            g = new
            {
                tabStyle = (section_a7 == null || tabSelectedIndex !=7) ? string.Empty : isactive, 
                exp = section_a7 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[7],
                cardTitle = (section_a7 == null) ? string.Empty : section_a7.description,
                cardStyle = (section_a7 == null || section_a7.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a7 == null || !section_a7.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a7 == null || !section_a7.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a7 == null || !section_a7.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a7 == null || !section_a7.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a7 == null || !section_a7.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a7 == null || !section_a7.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            h = new
            {
                tabStyle = (section_a8 == null || tabSelectedIndex !=8) ? string.Empty : isactive, 
                exp = section_a8 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[8],
                cardTitle = (section_a8 == null) ? string.Empty : section_a8.description,
                cardStyle = (section_a8 == null || section_a8.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a8 == null || !section_a8.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a8 == null || !section_a8.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a8 == null || !section_a8.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a8 == null || !section_a8.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a8 == null || !section_a8.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a8 == null || !section_a8.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            i = new
            {
                tabStyle = (section_a9 == null || tabSelectedIndex !=9) ? string.Empty : isactive, 
                exp = section_a9 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[9],
                cardTitle = (section_a9 == null) ? string.Empty : section_a9.description,
                cardStyle = (section_a9 == null || section_a9.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a9 == null || !section_a9.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_a9 == null || !section_a9.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_a9 == null || !section_a9.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_a9 == null || !section_a9.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_a9 == null || !section_a9.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_a9 == null || !section_a9.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            j = new
            {
                tabStyle = (section_b1 == null || tabSelectedIndex !=10) ? string.Empty : isactive, 
                exp = section_b1 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[10],
                cardTitle = (section_b1 == null) ? string.Empty : section_b1.description,
                cardStyle = (section_b1 == null || section_b1.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_b1 == null || !section_b1.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_b1 == null || !section_b1.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_b1 == null || !section_b1.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_b1 == null || !section_b1.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_b1 == null || section_b1.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_b1 == null || !section_b1.listCanRemove) ?  string.Empty: nodisplay,
            }
            ,
            k = new
            {
                tabStyle = (section_b2 == null || tabSelectedIndex !=11) ? string.Empty : isactive, 
                exp = section_b2 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[11],
                cardTitle = (section_b2 == null) ? string.Empty : section_b2.description,
                cardStyle = (section_b2 == null || section_b2.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_b2 == null || !section_b2.dataCanDetail) ?  string.Empty: nodisplay,
                dataEditStyle = (section_b2 == null || !section_b2.dataCanEdit) ?  string.Empty: nodisplay,
                listViewStyle = (section_b2 == null || !section_b2.listCanDetail) ?  string.Empty: nodisplay,
                listEditStyle = (section_b2 == null || !section_b2.listCanEdit) ?  string.Empty: nodisplay,
                listAddStyle = (section_b2 == null || section_b2.listCanAdd) ?  string.Empty: nodisplay,
                listRemoveStyle = (section_b2 == null || !section_b2.listCanRemove) ?  string.Empty: nodisplay,
            }

        };
        #endregion
        

        //var t = true;

        //if (t) 
        //{

        //}

        #region MyRegion

        #endregion


    %>


    <div class="content-wrapper" >
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1><asp:Label ID="labelTitle" runat="server" Text="Project Preview"></asp:Label></h1> 
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>"><asp:Label ID="lblRoot" runat="server" Text="Home"></asp:Label></a></li>
              <li class="breadcrumb-item active"><asp:Label ID="lblCaption" runat="server" Text="Project Preview"></asp:Label></li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">

          <div class="invoice p-3 mb-3">
        <!-- Small boxes (Stat box) -->
        <div class="row">
                <div class="col-12">
                  <h4>
                    <i class="fa fa-globe"></i> AdminLTE, Inc.
                    <small class="float-right">Date: 2/10/2014</small>
                  </h4>
                </div>
                <!-- /.col -->
              </div>
        <!-- /.row -->
        <!-- Main row -->
        <div class="row">
          <!-- Left col -->
          <section class="col-lg-7 connectedSortable ui-sortable">
            <!-- Custom tabs (Charts with tabs)-->
            
            <!-- /.card -->

            

               <!-- DIRECT CHAT -->
            <div class="card direct-chat direct-chat-primary" <%=  string.Format("style = '{0}'", exp.a.cardStyle) %> >
              <div class="card-header ui-sortable-handle" style="cursor: move;">
                <h3 class="card-title"><%= exp.a.cardCircle %> <%= exp.a.cardTitle %></h3>

                
              </div>
              <!-- /.card-header -->
              <div class="card-body" >
                <!-- Conversations are loaded here -->
                <div class="direct-chat-messages" style="height: inherit !important">
                  <!-- Message. Default to the left -->
                  <div class="direct-chat-msg" style="margin-bottom:unset !important">
                      <div class="row">
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="labelProjectnumber">Work Plan For Project: </asp:Label>
                                  <asp:Label runat="server" ID="labelProjectnumberResult"></asp:Label>
                              </div>
                          </div>
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="labelFisicalYear">Fiscal Year: </asp:Label>
                                  <asp:Label runat="server" ID="labelFiscalYearResult"></asp:Label>
                              </div>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="labelLeader">Leader: </asp:Label>
                                  <asp:Label runat="server" ID="labelLeaderResault"></asp:Label>
                              </div>
                          </div>
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="labelStatus">Status: </asp:Label>
                                  <asp:Label runat="server" ID="labelStatusResult"></asp:Label>
                              </div>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="label14">Contract number: </asp:Label>
                                  <asp:TextBox ID="TextBoxContractNumber" CssClass="form-control" runat="server"></asp:TextBox>
                              </div>
                          </div>
                          <div class="col-md-6">
                              <div class="form-group">
                                  <asp:Label runat="server" ID="label16">ORCID: </asp:Label>
                                  <asp:TextBox ID="TextBoxORCID" CssClass="form-control" runat="server"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                    <!-- /.direct-chat-info -->
                    
                    <!-- /.direct-chat-text -->
                  </div>
                  <!-- /.direct-chat-msg -->



                </div>
                <!--/.direct-chat-messages-->

                <!-- /.direct-chat-pane -->
              </div>
              <!-- /.card-body -->
              
            </div>
            <!--/.direct-chat -->

                 <!-- DIRECT CHAT -->
            <div class="card direct-chat direct-chat-primary" <%=  string.Format("style = '{0}'", exp.b.cardStyle) %> >
              <div class="card-header ui-sortable-handle" style="cursor: move;">
                <h3 class="card-title"><%= exp.b.cardCircle %> <%= exp.b.cardTitle %></h3>

                
              </div>
              <!-- /.card-header -->
              <div class="card-body" >
                <!-- Conversations are loaded here -->
                <div class="direct-chat-messages" style="height: inherit !important">
                  <!-- Message. Default to the left -->
                  <div class="direct-chat-msg" style="margin-bottom:unset !important">
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
                                  <asp:Label ID="LabelCommodity" runat="server" Text="Commmodity:"></asp:Label>
                                  <asp:DropDownList ID="DropDownListCommodity" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceCommodity"
                                      DataTextField="CommName" DataValueField="CommID">
                                  </asp:DropDownList>
                                  <asp:SqlDataSource ID="SqlDataSourceCommodity" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Commodity]"></asp:SqlDataSource>
                              </div>
                          </div>
                      </div>
                      <div class="row">
                          <div class="col-md-4">
                              <div class="form-group">
                                  <asp:Label ID="LabelProjectShortTitle" runat="server" Text="Project Short Title:"></asp:Label>
                                  <asp:TextBox ID="TextBoxProjectShortTitle" CssClass="form-control" runat="server"></asp:TextBox>
                              </div>
                          </div>
                          <div class="col-md-4">
                              <div class="form-group">
                                  <asp:Label ID="LabelDepartment" runat="server" Text="Department:"></asp:Label>
                                  <asp:DropDownList ID="DropDownListDepartment" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceDepartment"
                                      DataTextField="DepartmentName" DataValueField="DepartmentID">
                                  </asp:DropDownList>
                                  <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
                              </div>
                          </div>
                          <div class="col-md-4">
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
                    <!-- /.direct-chat-info -->
                    
                    <!-- /.direct-chat-text -->
                  </div>
                  <!-- /.direct-chat-msg -->



                </div>
                <!--/.direct-chat-messages-->

                <!-- /.direct-chat-pane -->
              </div>
              <!-- /.card-body -->
              
            </div>
            <!--/.direct-chat -->

            
            
            <!-- /.card -->
          </section>
          <!-- /.Left col -->
          <!-- right col (We are only adding the ID to make the widgets sortable)-->
          <section class="col-lg-5 connectedSortable ui-sortable">

                 <!-- DIRECT CHAT -->
            <div class="card direct-chat direct-chat-primary" <%=  string.Format("style = '{0}'", exp.j.cardStyle) %> >
              <div class="card-header ui-sortable-handle" style="cursor: move;">
                <h3 class="card-title">  <i class="fa fa-pencil mr-1"></i>   <%= exp.j.cardTitle %></h3>

                
              </div>
              <!-- /.card-header -->
              <div class="card-body" style="display:block" >
                <!-- Conversations are loaded here -->
                <div class="direct-chat-messages" style="height: inherit !important">

                   <%= this.GetProjectSigns() %>



                </div>
                <!--/.direct-chat-messages-->

                <!-- /.direct-chat-pane -->
              </div>
              <!-- /.card-body -->
              
            </div>
            <!--/.direct-chat -->

                 <!-- DIRECT CHAT -->
            <div class="card direct-chat direct-chat-primary" <%=  string.Format("style = '{0}'", exp.k.cardStyle) %> >
              <div class="card-header ui-sortable-handle" style="cursor: move;">
                <h3 class="card-title"> <i class="fa fa-file-text-o mr-1"></i> <%= exp.k.cardTitle %></h3>

                
              </div>
              <!-- /.card-header -->
              <div class="card-body" style="display:block" >
                <!-- Conversations are loaded here -->
                <div class="direct-chat-messages" style="height: inherit !important">
                 
                     <%= this.GetProjectNotes() %>


                </div>
                <!--/.direct-chat-messages-->

                <!-- /.direct-chat-pane -->
              </div>
              <!-- /.card-body -->
              
            </div>
            <!--/.direct-chat -->

           

            
          </section>
          <!-- right col -->
        </div>
        <!-- /.row (main row) -->

              <div class="row no-print">
                <div class="col-12">
                  <a target="_blank" href="<%= this.ResolveClientUrl(string.Format("~/Project/ProjectPrint.aspx?PID={0}", this.GetProjectID())) %>" class="btn btn-default"><i class="fa fa-print"></i> Print</a>
                  
                  
                </div>
              </div>
      </div><!-- /.container-fluid -->
        </div>
    </section>
    <!-- /.content -->
  </div>

    


    
</asp:Content>
