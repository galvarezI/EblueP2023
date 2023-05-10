<%@ Page Title="Estación Experimental Agrícola - Project Template" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProjectTemplate.aspx.cs" Inherits="Eblue.Project.ProjectTemplate" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript">  
        function enablePostBack() {
            //T1 is the first argument(name of our control) I mentioned earlier and give the  
            // value of second argument as "" that's all  
            __TdoPostBack("T1", "");
        }

        function SelectTabFromServer(name, line) {
            __TdoPostBack('tab_' + name, line);
        }

        function __TdoPostBack(eventTarget, eventArgument) {
            document.aspnetForm.__EVENTTARGET.value = eventTarget; <a href="ProjectPreview.aspx">ProjectPreview.aspx</a>
            document.aspnetForm.__EVENTARGUMENT.value = eventArgument;
            document.aspnetForm.submit(); <a href="ProjectTemplate.aspx">ProjectTemplate.aspx</a>
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManagerEdit" runat="server"></asp:ScriptManager>
    <asp:SqlDataSource ID="SqlDataSourceProjectProccessInfo" runat="server"
        SelectCommand="

         select 
            ROW_NUMBER() over (order by mdl.orderline ) as rowNumber,
            pw.UID,
            prj.ProjectID, prj.ProjectNumber, 
            mdl.Uid as ProcessId, mdl.Name ProccessName, mdl.Description ProccessDescription , mdl.WorkflowId,
            IsActual = (select top 1 cast (1 as bit) from ProcessProjectWay pwt where pwt.UId = prj.ProcessProjectWayID ), 
            mdl.IsStarter, mdl.IsFinalizer, 
            pcN.EstatusId NextStatusID, pcA.EstatusId AlwaysStatusID, 
            mdl.objectionsAvailabled, mdl.assentsAvailabled,
            mdl.enabledForDirectiveManager, mdl.enabledForOnlyDirectiveManager, mdl.enabledForInvestigationOfficer, mdl.enabledForAssistantLeader, mdl.enabledForDirectiveLeader,mdl.enabledForResearchDirector,
            mdl.enabledForExecutiveOfficer,mdl.enabledForExternalResources,
            mdl.PreviousProcessId, mdl.AlwaysProcessId,
            mdl.NextProcessId, mdl.EstatusId, mdl.OrderLine,
            StatusDescription = (select top 1 tmp.ProjectStatusName from ProjectStatus tmp where tmp.ProjectStatusID = mdl.EstatusId),
            PreviousProccessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.PreviousProcessId),
            NextProcessDescription = concat( (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.NextProcessId), 
			' ', ( select top 1 tmp.ProjectStatusName from ProjectStatus tmp where tmp.ProjectStatusID = pcN.EstatusId)
			),
            AlwaysProccessDescription = concat( (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.AlwaysProcessId),
			' ', ( select top 1 tmp.ProjectStatusName from ProjectStatus tmp where tmp.ProjectStatusID = pcA.EstatusId)
			)
            from Process mdl
            inner join ProcessProjectWay pw on pw.ProcessId = mdl.Uid
            inner join projects prj on prj.ProjectID = PW.ProjectID
			left join Process pcN on pcN.Uid = mdl.NextProcessId
			left join Process pcA on pcA.Uid = mdl.AlwaysProcessId
            where prj.ProjectID = @ProjectID
        
        "
        >
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourcePlayerProject" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select
        pp.ProjectID, pp.RosterId, pp.RoleId, pr.RoleName, pr.RoleCategoryId, rc.Description RoleTypeDescription,
        iif(rc.Description is null, cast(0 as bit), cast(1 as bit)) HasRoleType, 
        CONCAT(pr.RoleName, iif(rc.Description is null, '', concat(' (', rc.description, ')' ) )) RoleCaption 
        from PlayerProject pp
        inner join projects p on p.ProjectID = pp.ProjectID
        inner join Roster r on r.RosterID = pp.RosterId
        inner join Roles pr on pr.RoleID = pp.RoleId
        left join RoleCategory rc on rc.UId = pr.RoleCategoryId
        where 
        pp.ProjectID = @ProjectID 

        "
        
        ></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlDataSourcePlayerProject" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
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
        
        ></asp:SqlDataSource>--%>

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

    <asp:SqlDataSource ID="SqlDataSourceProjectStatusSelect" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select 
        ROW_NUMBER() over (order by ps.StatusDate ) as rowNumber, 
        ps.UId,
        ps.OrderLine, ps.StatusDate, ps.StatusData, ps.RosterData, ps.ProjectID, ps.RosterID, ps.RoleId, r.RoleName, pr.RosterName 

        from projectStatuses ps
        inner join Projects p on p.ProjectID = ps.ProjectID
        inner join roster pr on pr.RosterID = ps.RosterID
        left join Roles r on r.RoleID = ps.RoleId
        left join RoleCategory rc on rc.UId = r.RoleCategoryId
        where 
        ps.ProjectID = @ProjectID

        "
        
        ></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="SqlDataSourceProjectAssentsSelect" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select 
        ROW_NUMBER() over (order by ps.Assentdate ) as rowNumber, 
        ps.UId,
        ps.OrderLine, ps.AssentDate, ps.AssentData, ps.RosterData, ps.ProjectID, ps.RosterID, ps.RoleId, r.RoleName, pr.RosterName 

        from projectAssents ps
        inner join Projects p on p.ProjectID = ps.ProjectID
        inner join roster pr on pr.RosterID = ps.RosterID
        left join Roles r on r.RoleID = ps.RoleId
        left join RoleCategory rc on rc.UId = r.RoleCategoryId
        where 
        ps.ProjectID = @ProjectID

        "
        
        ></asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSourceProjectObjetionsSelect" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
        SelectCommand="
        
        select 
        ROW_NUMBER() over (order by ps.Objetiondate ) as rowNumber, 
        ps.UId,
        ps.OrderLine, ps.ObjetionDate, ps.ObjetionData, ps.RosterData, ps.ProjectID, ps.RosterID, ps.RoleId, r.RoleName, pr.RosterName 

        from projectObjetions ps
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
        //var thisInternalSection = this.internalSection;
        //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);
        //bool hasProjectProccess = projectProccess == null;

        ////var PrevDescription = projectProccess?.pre?.Item2;
        //var nextDescription = projectProccess?.NextProccess?.Item2;
        //var AlwayDescription = projectProccess?.AlwaysProccess?.Item2;

        //var statusActions = new Tuple<string, string>();

        Eblue.Code.TargetSection section_a0 = null;
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
        //Eblue.Code.TargetSection section_b3 = null;
        //Eblue.Code.TargetSection section_b4 = null;

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
            var tagier_section_a0 = Eblue.Utils.ConstantsTools.tagier_project_section_a0;
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
            //var tagier_section_b3 = Eblue.Utils.ConstantsTools.tagier_project_section_b3;
            //var tagier_section_b4 = Eblue.Utils.ConstantsTools.tagier_project_section_b4;

            #region MyRegion

            #endregion
            if (sections.ContainsKey(tagier_section_a0))
            {
                section_a0 = sections[tagier_section_a0];
            }
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
        //var tabSelectedIndex = Convert.ToInt32(this.TabSelectedIndex ?? "0");
        var tabSelectedIndex = -1;
        var tabSelectedIndexString = this.TabSelectedIndex;

        tabSelectedIndex = Convert.ToInt32(tabSelectedIndexString);

        if (tabSelectedIndex < 0)
        {
            tabSelectedIndex = 0;
        }

        //if (!string.IsNullOrEmpty(tabSelectedIndexString))
        //{
        //    tabSelectedIndexString = tabSelectedIndexString.Trim();
        //    if (int.TryParse(tabSelectedIndexString, out int tabIndex) && tabIndex >- 1)
        //    {

        //        tabSelectedIndex = tabIndex;

        //    }
        //    else
        //    {


        //    }

        //}
        //else
        //{
        //    var stop = true;

        //    if (stop)
        //    { }

        //}

        //if (section_a0 != null)
        //{
        //    tabSelectedIndex = Convert.ToInt32(this.TabSelectedIndex ?? "0");
        //}
        //else
        //{
        //    if (section_a1 != null)
        //    {
        //        tabSelectedIndex = Convert.ToInt32(this.TabSelectedIndex ?? "1");

        //    }


        //}
        //_ = new {
        //       tabStyle = ( tabSelectedIndex !=0) ? string.Empty : isactive,
        //   },


        var exp = new
        {
            _ = new
            {
                tabStyle = (section_a0 == null || tabSelectedIndex != 0) ? string.Empty : isactive,
                exp = section_a0 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[0],
                cardTitle = (section_a0 == null) ? string.Empty : section_a0.description,
                cardStyle = (section_a0 == null || section_a0.hideSection) ? nodisplay : string.Empty,
            }
            ,
            a = new
            {
                tabStyle = (section_a1 == null || tabSelectedIndex != 1) ? string.Empty : isactive,
                exp = section_a1 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[1],
                cardTitle = (section_a1 == null) ? string.Empty : section_a1.description,
                cardStyle = (section_a1 == null || section_a1.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a1 == null || !section_a1.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a1 == null || !section_a1.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a1 == null || !section_a1.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a1 == null || !section_a1.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a1 == null || !section_a1.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a1 == null || !section_a1.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            b = new
            {
                tabStyle = (section_a2 == null || tabSelectedIndex != 2) ? string.Empty : isactive,
                exp = section_a2 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[2],
                cardTitle = (section_a2 == null) ? string.Empty : section_a2.description,
                cardStyle = (section_a2 == null || section_a2.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a2 == null || !section_a2.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a2 == null || !section_a2.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a2 == null || !section_a2.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a2 == null || !section_a2.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a2 == null || !section_a2.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a2 == null || !section_a2.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            c = new
            {
                tabStyle = (section_a3 == null || tabSelectedIndex != 3) ? string.Empty : isactive,
                exp = section_a3 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[3],
                cardTitle = (section_a3 == null) ? string.Empty : section_a3.description,
                cardStyle = (section_a3 == null || section_a3.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a3 == null || !section_a3.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a3 == null || !section_a3.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a3 == null || !section_a3.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a3 == null || !section_a3.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a3 == null || !section_a3.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a3 == null || !section_a3.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            d = new
            {
                tabStyle = (section_a4 == null || tabSelectedIndex != 4) ? string.Empty : isactive,
                exp = section_a4 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[4],
                cardTitle = (section_a4 == null) ? string.Empty : section_a4.description,
                cardStyle = (section_a4 == null || section_a4.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a4 == null || !section_a4.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a4 == null || !section_a4.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a4 == null || !section_a4.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a4 == null || !section_a4.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a4 == null || !section_a4.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a4 == null || !section_a4.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            e = new
            {
                tabStyle = (section_a5 == null || tabSelectedIndex != 5) ? string.Empty : isactive,
                exp = section_a5 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[5],
                cardTitle = (section_a5 == null) ? string.Empty : section_a5.description,
                cardStyle = (section_a5 == null || section_a5.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a5 == null || !section_a5.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a5 == null || !section_a5.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a5 == null || !section_a5.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a5 == null || !section_a5.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a5 == null || !section_a5.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a5 == null || !section_a5.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            f = new
            {
                tabStyle = (section_a6 == null || tabSelectedIndex != 6) ? string.Empty : isactive,
                exp = section_a6 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[6],
                cardTitle = (section_a6 == null) ? string.Empty : section_a6.description,
                cardStyle = (section_a6 == null || section_a6.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a6 == null || !section_a6.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a6 == null || !section_a6.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a6 == null || !section_a6.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a6 == null || !section_a6.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a6 == null || !section_a6.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a6 == null || !section_a6.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            g = new
            {
                tabStyle = (section_a7 == null || tabSelectedIndex != 7) ? string.Empty : isactive,
                exp = section_a7 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[7],
                cardTitle = (section_a7 == null) ? string.Empty : section_a7.description,
                cardStyle = (section_a7 == null || section_a7.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a7 == null || !section_a7.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a7 == null || !section_a7.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a7 == null || !section_a7.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a7 == null || !section_a7.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a7 == null || !section_a7.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a7 == null || !section_a7.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            h = new
            {
                tabStyle = (section_a8 == null || tabSelectedIndex != 8) ? string.Empty : isactive,
                exp = section_a8 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[8],
                cardTitle = (section_a8 == null) ? string.Empty : section_a8.description,
                cardStyle = (section_a8 == null || section_a8.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a8 == null || !section_a8.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a8 == null || !section_a8.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a8 == null || !section_a8.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a8 == null || !section_a8.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a8 == null || !section_a8.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a8 == null || !section_a8.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            i = new
            {
                tabStyle = (section_a9 == null || tabSelectedIndex != 9) ? string.Empty : isactive,
                exp = section_a9 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[9],
                cardTitle = (section_a9 == null) ? string.Empty : section_a9.description,
                cardStyle = (section_a9 == null || section_a9.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_a9 == null || !section_a9.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_a9 == null || !section_a9.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_a9 == null || !section_a9.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_a9 == null || !section_a9.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_a9 == null || !section_a9.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_a9 == null || !section_a9.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            j = new
            {
                tabStyle = (section_b1 == null || tabSelectedIndex != 10) ? string.Empty : isactive,
                exp = section_b1 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[10],
                cardTitle = (section_b1 == null) ? string.Empty : section_b1.description,
                cardStyle = (section_b1 == null || section_b1.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_b1 == null || !section_b1.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_b1 == null || !section_b1.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_b1 == null || !section_b1.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_b1 == null || !section_b1.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_b1 == null || section_b1.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_b1 == null || !section_b1.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            k = new
            {
                tabStyle = (section_b2 == null || tabSelectedIndex != 11) ? string.Empty : isactive,
                exp = section_b2 ?? new Eblue.Code.TargetSection(),
                cardCircle = Eblue.Utils.ConstantsTools.BlackNumbers[11],
                cardTitle = (section_b2 == null) ? string.Empty : section_b2.description,
                cardStyle = (section_b2 == null || section_b2.hideSection) ? nodisplay : string.Empty,
                dataViewStyle = (section_b2 == null || !section_b2.dataCanDetail) ? string.Empty : nodisplay,
                dataEditStyle = (section_b2 == null || !section_b2.dataCanEdit) ? string.Empty : nodisplay,
                listViewStyle = (section_b2 == null || !section_b2.listCanDetail) ? string.Empty : nodisplay,
                listEditStyle = (section_b2 == null || !section_b2.listCanEdit) ? string.Empty : nodisplay,
                listAddStyle = (section_b2 == null || section_b2.listCanAdd) ? string.Empty : nodisplay,
                listRemoveStyle = (section_b2 == null || !section_b2.listCanRemove) ? string.Empty : nodisplay,
            }
            ,
            l = new
            {
                tabStyle = (tabSelectedIndex != 12) ? string.Empty : isactive,
            }
            ,
            m = new
            {
                tabStyle = (tabSelectedIndex != 13) ? string.Empty : isactive,
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


    <%--    
    fa fa-sign-out
    .fa-sign-out:before {
    content: "\f08b";
}
    <div  style = "<%= (section_a2 != null) ? "" : string.Format("display:none") %>"  >
                <nav class="main-header navbar navbar-expand bg-white navbar-light border-bottom">
    
        <a class="nav-link" > <%= circle %>  <%= title %>  </a>


    <% if (!canDataEdit)
                                {%>
                            <div class="overlay"   >
                            </div>
                            <% } %>

    <div class="content-wrapper" style="min-height: 568px;">

    --%>


    <div class="content-wrapper">
        <%--<input type ="hidden" name ="__EVENTTARGET" value ="" /> 
<input type ="hidden" name ="__EVENTARGUMENT" value ="" />--%>
        
    <%--<input type="text" id="T1" name="T1" onblur="enablePostBack();"  runat="server"/>--%>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1><asp:Label ID="labelTitle" runat="server" Text="Research Work Plan"></asp:Label></h1> 
    
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>"><asp:Label ID="lblRoot" runat="server" Text="Home"></asp:Label></a></li>
                            <li class="breadcrumb-item active"><asp:Label ID="lblCaption" runat="server" Text="Project Template"></asp:Label></li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    
                    <!-- /.col -->
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header p-2">
                                <ul class="nav nav-pills">
                                    <%--<li class="nav-item" <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "_" , "0") %>><a title="Status" class="nav-link<%= string.Format(" {0}", exp._.tabStyle)  %>" href="#internal" data-toggle="tab">
                                        <i class="fa fa-heart"></i></a></li>--%>

<%--                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp._.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "_" , exp._.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp._.tabStyle)  %>" href="#_" data-toggle="tab">
                                        <i class="fa fa-heart"></i></a></li>
                                    
                                    <li class="nav-item" <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "l" , "12") %>><a title="Assents" class="nav-link<%= string.Format(" {0}", exp.l.tabStyle)  %>" href="#l" data-toggle="tab">
                                        <i class="fa fa-thumbs-up"></i></a></li>
                                    
                                    <li class="nav-item" <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "m" , "13") %>><a title="Objetions" class="nav-link<%= string.Format(" {0}", exp.m.tabStyle)  %>" href="#m" data-toggle="tab">
                                       <i class="fa fa-thumbs-down"></i></a></li>
                                    
                            --%>        
                                    
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp._.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "_" , exp._.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp._.tabStyle)  %>" href="#_" data-toggle="tab"><%= exp.a.cardCircle %> </a></li>
                                    <%--<li class="nav-item" <%=  string.Format("style = '{0}'", exp.b.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "b" , exp.b.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.b.tabStyle)  %>" href="#b" data-toggle="tab"><%= exp.b.cardCircle %> </a></li>--%>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.c.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "c" , exp.c.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.c.tabStyle)  %>" href="#c" data-toggle="tab"><%= exp.b.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.d.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "d" , exp.d.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.d.tabStyle)  %>" href="#d" data-toggle="tab"><%= exp.c.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.e.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "e" , exp.e.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.e.tabStyle)  %>" href="#e" data-toggle="tab"><%= exp.d.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.f.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "f" , exp.f.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.f.tabStyle)  %>" href="#f" data-toggle="tab"><%= exp.e.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.g.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "g" , exp.g.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.g.tabStyle)  %>" href="#g" data-toggle="tab"><%= exp.f.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.h.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "h" , exp.h.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.h.tabStyle)  %>" href="#h" data-toggle="tab"><%= exp.g.cardCircle %> </a></li>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.i.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "i" , exp.i.exp.numLine) %>><a class="nav-link<%= string.Format(" {0}", exp.i.tabStyle)  %>" href="#i" data-toggle="tab"><%= exp.h.cardCircle %> </a></li>
                                    <%--<li class="nav-item" <%=  string.Format("style = '{0}'", exp.j.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "j" , exp.j.exp.numLine) %>><a title="Signs" class="nav-link<%= string.Format(" {0}", exp.j.tabStyle)  %>" href="#j" data-toggle="tab"> <i class="fa fa-pencil mr-1"></i>     </a></li>--%>
                                    <li class="nav-item" <%=  string.Format("style = '{0}'", exp.k.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "k" , exp.k.exp.numLine) %>><a title="Notes" class="nav-link<%= string.Format(" {0}", exp.k.tabStyle)  %>" href="#k" data-toggle="tab"> <i class="fa fa-file-text-o mr-1"></i></a></li>
                                    <li class="nav-item"><asp:Button runat="server" class="btn btn-primary" text="Project Preview" OnClick="Unnamed1_Click" /></li>
                                    
                                    
                                    

                                </ul>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <div class="tab-content">
                                    
                                    
                                        <div class="<%= string.Format("{0} ", exp.a.tabStyle)   %>tab-pane" id="a">
                                        <%--
                                            <div class="<%= string.Format("{0} ", exp._.tabStyle)   %>tab-pane" id="internal">
                                            card card-success card-outline direct-chat direct-chat-success
                                            card card-warning card-outline
                                            card card-primary direct-chat direct-chat-primary

                                            card card-info card-outline direct-chat direct-chat-info
                                            --%>

                                        <div class="card card-primary card-outline direct-chat direct-chat-primary">
                                             <div class="card-header ui-sortable-handle" style="cursor: move;">
                                                 <h4 class="title">Status<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />

                                                 <div class="card-tools">
                                                     <span data-toggle="tooltip" title="3 New Messages" class="badge badge-primary" style="display:none !important">3</span>
                                                     <button type="button" class="btn btn-tool" data-widget="collapse">
                                                         <i class="fa fa-minus"></i>
                                                     </button>
                                                     <%--<button style="display:none !important" type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>--%>
                                                     <%--<button  type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>--%>
                                                     <button type="button" class="btn btn-tool" data-widget="remove">
                                                         <i class="fa fa-times"></i>
                                                     </button>
                                                 </div>
                                             </div>
                                             <!-- /.card-header -->
                                             <div class="card-body" style="display: block;">
                                                 <!-- Conversations are loaded here -->
                                                 <div class="direct-chat-messages">
                                                     
                                                     <%= this.GetProjectStatus() %>
                                                    

                                                 </div>
                                                 <!--/.direct-chat-messages-->

                                                 
                                                 
                                             </div>
                                             <!-- /.card-body -->
                                             <div class="card-footer"  >
                                               <div class="input-group">
                                                     <asp:Image  Visible="false" ID="image1"  runat="server"/>
                                                     <%--style="max-width:125px !important"--%>
                                                     <asp:TextBox  Visible="false"  TextMode="MultiLine" Rows="1" style="resize:none !important;border-color:transparent;"    ID="TextBox4"  CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         <asp:Button ID="ButtonNext" CssClass="btn btn-default"  runat="server" Text=">"  OnClick="ButtonNext_Click" />
                                                         <asp:Button ID="ButtonAlways" CssClass="btn btn-primary"   runat="server" Text=">"  OnClick="ButtonAlways_Click" />
                                                     </span>
                                                 </div>

                                                 <div class="input-group">
                                                     <asp:Image  Visible="false" ID="image2"  runat="server"/>
                                                     
                                                     <asp:TextBox Visible="false"  TextMode="MultiLine" Rows="1" style="resize:none !important;border-color:transparent;"    ID="TextBox5"  CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         

                                                     </span>
                                                 </div>
                                             </div>

                                            <%-- <div class="overlay" <%=  string.Format("style = '{0}'", exp.k.listAddStyle) %>>
                                            </div>--%>
                                             <!-- /.card-footer-->
                                         </div>

                                        

                                        
                                        
                                    </div>
                                  

                                    <div class="<%= string.Format("{0} ", exp._.tabStyle)   %>tab-pane" id="_">
                                        <div class="card card-default">
                                            <div class="card-header">
                            <div class="col-md-6">
                            <div class="form-group">
                                <h4><asp:Label runat="server" ID="labelProjectnumber"  >Work Plan For Project: </asp:Label>
                                <asp:Label runat="server" ID="labelProjectnumberResult"></asp:Label></h4>
                            </div>
                        </div>
                                                 <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Label ID="LabelProjectShortTitle"  runat="server" Text="Project Short Title:"></asp:Label>
                                                            <asp:Label runat="server" ID="LabelProjectShortTitleResult"></asp:Label>
                                                            <%--<asp:TextBox ID="" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                                            <div class="card-body">

                                                <div class="row">
                        
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" ID="labelFisicalYear" >Fiscal Year: </asp:Label>
                                <asp:Label runat="server" ID="labelFiscalYearResult"></asp:Label>
                            </div>
                        </div>
                    </div>
                                               
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server"  ID="labelLeader">Leader: </asp:Label>
                                <asp:Label runat="server" ID="labelLeaderResault"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server"  ID="labelStatus">Status: </asp:Label>
                                <asp:Label runat="server" ID="labelStatusResult"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server"  ID="label14">Award/Accession/Contract number: </asp:Label>
                                <asp:TextBox ID="TextBoxContractNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server"  ID="label16">Account number: </asp:Label>
                                <asp:TextBox ID="TextBoxORCID" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                                            

                                                <!-- /.row -->

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="LabelProgramaticArea"  runat="server" Text="Programatic Area:"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListProgramaticArea" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceProgramaticArea" DataTextField="ProgramAreaName" DataValueField="ProgramAreaID"></asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceProgramaticArea" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [ProgramArea]"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="LabelCommodity"  runat="server" Text="Commodity:"></asp:Label>
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
                                                            <asp:Label ID="LabelDepartment"  runat="server" Text="Department:"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListDepartment" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceDepartment"
                                                                DataTextField="DepartmentName" DataValueField="DepartmentID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="LabelSubstation"  runat="server" Text="Substation or Region:"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListSubstation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                                                DataTextField="LocationName" DataValueField="LocationID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                                SelectCommand="SELECT * FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">

                                                     <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblTF"  runat="server" Text="Type of Funds:"></asp:Label>
                                                            <asp:DropDownList ID="DropdownListTypeOfFunds" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceTypeOfFunds"
                                                                DataTextField="FundTypeName" DataValueField="FundTypeID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSourceTypeOfFunds" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [FundType]"></asp:SqlDataSource>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="LbLPO" runat="server" Text="Performing Organization"></asp:Label>
                                                            <asp:DropDownList ID="DropdownListPerformingOrganizations"
                                                                CssClass="form-control" 
                                                    runat="server"
                                                    DataSourceID="SqlDataSourceListPO" 
                                                    DataTextField="description"
                                                    DataValueField="uid">
                                                    </asp:DropDownList>
                                       <asp:SqlDataSource ID="SqlDataSourceListPO" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="select 
                                        POrganizatonId as uid, POrganizationName as description
                                        from POrganization
                                        "></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                    </div>
                                <%--<div class="row">
                                    <div class="form-group">
                                        
                                                            <asp:Label ID="LabelPO"  runat="server" Text="Performing Organizations:"></asp:Label>
                                
                                        <asp:DropDownList ID="DropdownListPerformingOrganizations" runat="server"
                                        CssClass="" TabIndex="2" AppendDataBoundItems="true"
                                        DataSourceID="SqlDataSourceListPO"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                       <asp:SqlDataSource ID="SqlDataSourceListPO" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="select 
                                        POrganizatonId as uid, POrganizationName as description
                                        from POrganization
                                        "></asp:SqlDataSource>
                                    </div>
                                </div>--%>
                                 
                                                       <%--     <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelProjectObjective"  runat="server" Text="Project Objective(s):"></asp:Label>
                                <asp:TextBox ID="TextBoxProjectObjective" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                                   

                                            </div>

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button4" OnClick="SaveChanges_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.a.dataEditStyle) %>>
                                            </div>
                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.c.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "c" , exp.c.exp.numLine) %>><a class="text-white" href="#c" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.b.tabStyle)   %>tab-pane" id="b">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Data Info</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                                          

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <asp:Button TabIndex="6" ID="button3" OnClick="SaveChanges_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />

                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.b.dataEditStyle) %>>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.c.tabStyle)   %>tab-pane" id="c">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                
                            <div class="form-group">
                                <h4 class="title"><asp:Label runat="server" ID="labelProjectNumber2"  >Work Plan For Project: </asp:Label>
                                <asp:Label runat="server" ID="labelProjectNumberResult2"></asp:Label></h4>
                            </div>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                                            <div class="card-body">
    
                                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label20" runat="server" Text="Project Objective(s):"></asp:Label>
                                            <asp:TextBox ID="TextBoxProjectObjectiveToFill" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Labelobjectivefortheyear"  runat="server" Text="Objective of Work Planned for the Year:"></asp:Label>
                                <asp:TextBox ID="TextBoxobjectivefortheyear" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelWorkAccomplished"  runat="server" Text="Work Accomplished and Present Outlook:"></asp:Label>
                                <asp:TextBox ID="TextBoxWorkAccomplished" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                  <%--  <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label4"  runat="server" Text="Work Planned (1) (Field Work):  "></asp:Label>
                                <asp:TextBox ID="TextBoxFieldWork" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>

                </div>

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <asp:Button TabIndex="6" ID="button5" OnClick="SaveChanges_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.c.dataEditStyle) %>>
                                            </div>

                                        </div>

                                    <span class="btn btn-primary"  <%=  string.Format("style = '{0}'", exp.d.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "d" , exp.d.exp.numLine) %>><a class="text-white" href="#d" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.d.tabStyle)   %>tab-pane" id="d">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Work Planned - Field Work</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                                            <div class="card-body">
                                                  <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label4"  runat="server" Text="Work Planned (1) (Field Work):  "></asp:Label>
                                <asp:TextBox ID="TextBoxFieldWork" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelLocation"  runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelArea"  runat="server" Text="Area (Acres) :"></asp:Label>

                                <asp:TextBox ID="fldWork_Area" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStart"  runat="server" Text="Start:"></asp:Label>
                                <asp:TextBox ID="TextBoxStart" autocomplete="off" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TextBoxStart_CalendarExtender" runat="server" BehaviorID="TextBoxStart_CalendarExtender" TargetControlID="TextBoxStart" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelEnd"  runat="server" Text="End:" ></asp:Label>
                                <asp:TextBox ID="TextBoxEnd" autocomplete="off"  CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="TextBoxEnd_CalendarExtender" runat="server" BehaviorID="TextBoxEnd_CalendarExtender" TargetControlID="TextBoxEnd" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <asp:CheckBox ID="ChkInProgress" Text=" In Progress " CssClass="minimal" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <asp:CheckBox ID="ChkInitiated" CssClass="minimal" Text=" Initiated " runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-auto">
                                <% if (exp.d.exp.listCanAdd)
                                    {%>
                                <asp:Button ID="ButtonWorkInprogresInitiatedYes" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonWorkInprogresInitiated_Click" Enabled="true" />
                                <%}
                                    else
                                    { %>
                                <asp:Button ID="ButtonWorkInprogresInitiatedNop" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonWorkInprogresInitiated_Click" Enabled ="false" />
                                <%} %>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center">--%>
                                <h4>Work In Progress</h4>
                                <asp:GridView  ID="gvP3Progress" runat="server" 
                                    DataKeyNames="FieldWorkID" DataSourceID="sdsFWInProgress" AutoGenerateColumns="False"
                                    CssClass="gridview table table-bordered table-striped"
                                    >
                                    <%--<RowStyle CssClass="data-row"CssClass="gridview table-bordered table table-hover table-striped" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Location">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsLocation" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField  DataField="Area" HeaderText="Area (Acres)"  />
                                        <asp:TemplateField HeaderText="Started" >
                                            <EditItemTemplate>
                                                <%--<div style="z-index: 9;">--%>
                                                    <asp:TextBox ID="txtStarted" autocomplete="false" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtStarted_CalendarExtender" runat="server" BehaviorID="txtStarted_CalendarExtender" TargetControlID="txtStarted" />
                                                <%--</div>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ended" >
                                            <EditItemTemplate>
                                                <%--<div style="z-index: 1;">--%>
                                                    <asp:TextBox ID="txtEnded" autocomplete="false" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtEnded_CalendarExtender" runat="server" BehaviorID="txtEnded_CalendarExtender" TargetControlID="txtEnded" />
                                                <%--</div>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField   ShowDeleteButton="true" ShowEditButton="true" ButtonType="Button"  />
                                        

                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsFWInProgress" runat="server" ConflictDetection="CompareAllValues"
                                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" DeleteCommand="DELETE FROM [FieldWork] WHERE [FieldWorkID] = @original_FieldWorkID "
                                    InsertCommand="INSERT INTO [FieldWork] ([ProjectID], [LocationID], [Area], [dateStarted], [dateEnded], [Inprogress]) VALUES (@ProjectID, @LocationID, @Area, @dateStarted, @dateEnded, @Inprogress)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT FieldWorkID, ProjectID, LocationID, Area, dateStarted, dateEnded, Inprogress, (SELECT LocationName FROM Location AS L WHERE (LocationID = FW.LocationID)) AS LocationName FROM FieldWork AS FW 
WHERE Inprogress=1 AND ProjectID=@ProjectID
ORDER BY LocationName"
                                    UpdateCommand="UPDATE [FieldWork] SET [LocationID] = @LocationID, [Area] = @Area, [dateStarted] = @dateStarted, [dateEnded] = @dateEnded WHERE [FieldWorkID] = @original_FieldWorkID">
                                    <SelectParameters>
                                        
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_FieldWorkID" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="original_FieldWorkID" Type="Int32" />
                                        <asp:Parameter Name="original_ProjectID" Type="Object" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="Inprogress" Type="Boolean" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" >
                            <%--<div class="align-center">--%>
                                <h4>Work To Be Initiated</h4>
                                <asp:GridView   ID="gvP3BeInitiated" runat="server" 
                                    DataKeyNames="FieldWorkID" DataSourceID="sdsFWBeInitiated" AutoGenerateColumns="False"
                                    CssClass="gridview table table-bordered table-striped">
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Location" >
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsLocation" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Area" HeaderText="Area (Acres)"  />
                                        <asp:TemplateField HeaderText="Started" >
                                            <EditItemTemplate>
                                                <%--<div style="z-index: 9; ">--%>
                                                    <asp:TextBox ID="txtStarted" autocomplete="false" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtStarted_CalendarExtender" runat="server" BehaviorID="txtStarted_CalendarExtender" TargetControlID="txtStarted" />
                                                <%--</div>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("dateStarted", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ended" >
                                            <EditItemTemplate>
                                                <%--<div style="z-index: 1;">--%>
                                                    <asp:TextBox ID="txtEnded" autocomplete="false" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtEnded_CalendarExtender" runat="server" BehaviorID="txtEnded_CalendarExtender" TargetControlID="txtEnded" />
                                                <%--</div>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("dateEnded", "{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsFWBeInitiated" runat="server" ConflictDetection="CompareAllValues"
                                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" DeleteCommand="DELETE FROM [FieldWork] WHERE [FieldWorkID] = @original_FieldWorkID "
                                    InsertCommand="INSERT INTO [FieldWork] ([ProjectID], [LocationID], [Area], [dateStarted], [dateEnded], [Inprogress]) VALUES (@ProjectID, @LocationID, @Area, @dateStarted, @dateEnded, @Inprogress)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT FieldWorkID, ProjectID, LocationID, Area, dateStarted, dateEnded, Inprogress, (SELECT LocationName FROM Location AS L WHERE (LocationID = FW.LocationID)) AS LocationName FROM FieldWork AS FW 
WHERE ToBeInitiated=1 AND ProjectID=@ProjectID
ORDER BY LocationName"
                                    UpdateCommand="UPDATE [FieldWork] SET [LocationID] = @LocationID, [Area] = @Area, [dateStarted] = @dateStarted, [dateEnded] = @dateEnded WHERE [FieldWorkID] = @original_FieldWorkID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_FieldWorkID" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="original_FieldWorkID" Type="Int32" />
                                        <asp:Parameter Name="original_ProjectID" Type="Object" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="Area" Type="String" />
                                        <asp:Parameter Name="dateStarted" Type="DateTime" />
                                        <asp:Parameter Name="dateEnded" Type="DateTime" />
                                        <asp:Parameter Name="Inprogress" Type="Boolean" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button6" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.d.listEditStyle) %>>
                                            </div>
                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.e.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "e" , exp.e.exp.numLine) %>><a class="text-white" href="#e" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.e.tabStyle)   %>tab-pane" id="e">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Laboratory/Greenhouse:</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                         <div class="card-body">                       
                            <div class="row" >
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelWorkPlanned2"  runat="server" Text="Work Planned:"></asp:Label>
                                <asp:TextBox ID="TextBoxWorkPlanned2" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                   
                    
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelDescription"  runat="server" Text="Description:"></asp:Label>
                                <asp:TextBox ID="TextBoxDescription" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelEstimatedTime"  runat="server" Text="Estimated Time Of Results"></asp:Label>
                                <asp:TextBox ID="TextBoxEstimatedTime" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelFacilitieNeeded"  runat="server" Text="Facilities Needed:"></asp:Label>
                                <asp:TextBox ID="TextBoxFacilitieNeeded" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                </div>
                                            

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <asp:Button TabIndex="6" ID="button6" OnClick="SaveChanges_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.e.dataEditStyle) %>>
                                            </div>
                                        </div>


                                       <div class="card">
                                            <div class="card-header">
                                                <h4>Central Analytical Laboratory Services:</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>
                                           <div class="card-body">

                                            <div class="row">
                   <%--     <div class="col-md-12">
                            <div class="align-center">
                                <h4>Central Analytical Laboratory Services:</h4>
                            </div>
                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelAnalisysRequired"  runat="server" Text="Analysis Required:"></asp:Label>
                                <asp:TextBox ID="TextBoxAnalisysRequired" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabeNumSample"  runat="server" Text="No. of Samples:"></asp:Label>
                                <asp:TextBox ID="TextBoxNumSample" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelProDate"  runat="server" Text="Probable Date:"></asp:Label>
                                <asp:TextBox ID="TextBoxProDate" autocomplete="false" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderProDate" runat="server" BehaviorID="TextBoxProDate_CalendarExtender" TargetControlID="TextBoxProDate" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonNewLaboratory" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewLaboratory_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center">--%>
                                <asp:GridView  ID="gvLab" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="LID" DataSourceID="sdsLaboratory" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <asp:BoundField  DataField="AReq" HeaderText="Analisys Req."  />
                                        <asp:BoundField DataField="NoSamples" HeaderText="No. Samples"  />
                                        <asp:BoundField DataField="SamplesDate" DataFormatString="{0:d}" HeaderText="Probable Date"
                                             />
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True"  ButtonType="Button"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsLaboratory" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [Laboratory] WHERE [LID] = @original_LID" InsertCommand="INSERT INTO [Laboratory] ([AReq], [NoSamples], [SamplesDate], [ProjectID]) VALUES (@AReq, @NoSamples, @SamplesDate, @ProjectID)"
                                    OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [LID], [AReq], [NoSamples], [SamplesDate], [ProjectID] FROM [Laboratory] WHERE ([ProjectID] = @ProjectID) ORDER BY [LID]"
                                    UpdateCommand="UPDATE [Laboratory] SET [AReq] = @AReq, [NoSamples] = @NoSamples, [SamplesDate] = @SamplesDate WHERE [LID] = @original_LID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_LID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="AReq" Type="String" />
                                        <asp:Parameter Name="NoSamples" Type="String" />
                                        <asp:Parameter Name="SamplesDate" Type="DateTime" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="original_LID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="AReq" Type="String" />
                                        <asp:Parameter Name="NoSamples" Type="String" />
                                        <asp:Parameter Name="SamplesDate" Type="DateTime" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                                               </div>
                                            

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button7" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                            <%--<div class="overlay" >
                                                <%=  string.Format("style = '{0}'", exp.e.listEditStyle) %>
                                            </div>--%>
                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.f.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "f" , exp.f.exp.numLine) %>><a class="text-white" href="#f" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.f.tabStyle)   %>tab-pane" id="f">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Scientists working in the project</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>


                                             <div class="card-body">
<%--                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Scientists working in the project:</h4>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelScientest"  runat="server" Text="Scientist:"></asp:Label>
                                <asp:DropDownList ID="DropDownListScientest" CssClass="form-control" runat="server" DataSourceID="sdsRoster" DataTextField="RosterName" DataValueField="RosterID"></asp:DropDownList>
                                
                                <asp:SqlDataSource ID="sdsRoster" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="                                    
                                    select 
                                    r.RosterID, r.RosterName
                                    from roster r
                                    inner join RosterCategory rc on rc.UId = r.rosterCategoryId
                                    where (r.CanBePI = 1 or rc.IsScientist = 1)  order by r.RosterName
                                    ">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="True" Name="CanBePI" Type="Boolean" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelRoleName"  runat="server" Text="Role Name"></asp:Label>
                                <asp:DropDownList ID="DropDownListRole" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceRole" DataTextField="RoleName" DataValueField="RoleID"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceRole" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="select 
                                    r.RoleID, r.RoleName
                                    from roles r
                                    left join RoleCategory rc on rc.UId = r.RoleCategoryId
                                    where 
                                    rc.IsDirectiveLeader = 1 
                                     or rc.OrderPriority in (select rcc.OrderPriority from RoleCategory rcc 
                                     where rcc.IsDirectiveManager =0 and rcc.IsInvestigationOfficer =0  and rcc.OrderPriority >= rc.OrderPriority)
                                    and r.RoleID = 16 or r.RoleID = 1017
                                    order by rc.OrderPriority
                                    "
                                    
                                    ></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center" style="overflow: scroll; width: 100%">--%>
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td>Regular</td>
                                        <td>Additional Compensation</td>
                                        <td>Ad Honorem</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTR" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtCA" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtHA" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                            <%--</div>--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">                                


                                 <% if (exp.f.exp.listCanAdd)
                                     {%>
                                
                                <asp:Button ID="ButtonNewScientestYes" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewScientest_Click" Enabled ="true" />
                                <%}
                                    else
                                    { %>
                                <asp:Button ID="ButtonNewScientestNop" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonNewScientest_Click" Enabled="false" />
                                <%} %>
                                
            
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center">--%>
                                <asp:GridView  ID="gvSci" runat="server"  OnRowUpdated="GridView_RowUpdate"
                                    DataKeyNames="SciID" DataSourceID="sdsSci" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <%--<RowStyle CssClass="data-row"  />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Scientist" SortExpression="RosterName">
                                            <EditItemTemplate>                                                
                                                <asp:DropDownList ID="DropDownListRoster" runat="server" DataSourceID="sdsRoster" 
                                                    DataTextField="RosterName"
                                                    DataValueField="RosterID" SelectedValue='<%# Bind("RosterID") %>' >
                                                    
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="LabelRosterNameItem" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role" >
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSourceRole" DataTextField="RoleName"
                                                    DataValueField="RoleId" SelectedValue='<%# Bind("Role") %>'>
                                                </asp:DropDownList>
                                                <%--<asp:SqlDataSource ID="sdsRoleEdit" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [RoleId], [RoleName] FROM [Roles] ORDER BY [RoleID]"></asp:SqlDataSource>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRoleName" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TR" HeaderText="TR"  />
                                        <asp:BoundField DataField="CA" HeaderText="CA"  />
                                        <asp:BoundField DataField="AH" HeaderText="AH"  />
                                        <asp:TemplateField HeaderText="Total" >
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSumCredits" runat="server" Text='<%# Bind("SumCredits") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:CommandField  ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button"/>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsSci" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [SciProjects] WHERE [SciID] = @original_SciID" InsertCommand="INSERT INTO [SciProjects] ([RosterID], [Role], [Credits], [AdHonorem], [ProjectID]) VALUES (@RosterID, @Role, @Credits, @AdHonorem, @ProjectID)"
                                    OldValuesParameterFormatString="original_{0}" 
                                    SelectCommand="
                                    SELECT 
                                    [SciID], [RosterID], [Role], [TR], [CA], [AH], [ProjectID], (SUM(TR+CA+AH)) as SumCredits,
                                    (SELECT RosterName FROM Roster R Where RosterID=Sci.RosterID) as RosterName,
                                    (SELECT RoleName FROM Roles PR WHERE RoleId=Sci.Role) as RoleName
                                    FROM SciProjects Sci 
                                    WHERE ProjectID =  @ProjectID 
                                    GROUP BY [SciID], [RosterID], [Role], [TR], [CA], [AH], [ProjectID]
                                    ORDER BY [SciID]
                                    
                                    "
                                    UpdateCommand="UPDATE [SciProjects] SET [RosterID] = @RosterID, [Role] = @Role, [TR]=@TR, [CA]=@CA, [AH]=@AH WHERE [SciID] = @original_SciID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_SciID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="RosterID"  />
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                        <asp:Parameter Name="Role" Type="Int32" />
                                        <asp:Parameter Name="TR" Type="Decimal" />
                                        <asp:Parameter Name="CA" Type="Decimal" />
                                        <asp:Parameter Name="AH" Type="Decimal" />
                                        <asp:Parameter Name="original_SciID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="RosterID" />
                                        <asp:Parameter Name="Role" Type="Int32" />
                                        <asp:Parameter Name="Credits" Type="Int32" />
                                        <asp:Parameter Name="AdHonorem" Type="Boolean" />
                                        <asp:Parameter Name="ProjectID" Type="Object" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
                                          

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button7" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                           <%-- <div class="overlay" <%=  string.Format("style = '{0}'", exp.f.listEditStyle) %>>
                                            </div>--%>
                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.g.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "g" , exp.g.exp.numLine) %>><a class="text-white" href="#g" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.g.tabStyle)   %>tab-pane" id="g">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Other personnel working in the project</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>


                                            <div class="card-body">
                   <%-- <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Other personnel working in the project:</h4>
                            </div>
                        </div>
                    </div>--%>

                                                <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label15"  runat="server" Text="Personnel:"></asp:Label>
                                    <asp:TextBox ID="TextBoxPersonnel" runat="server"></asp:TextBox>
                            <%--    <asp:DropDownList ID="DropDownListPersonnel" CssClass="form-control" runat="server"  AppendDataBoundItems="true"
DataSourceID="SqlDataSourcePersonnel" DataTextField="RosterName" DataValueField="RosterID">
                                    <asp:ListItem Value="">None</asp:ListItem>
                                </asp:DropDownList>--%>
                              <%--  <asp:RequiredFieldValidator ID="DropDownListPersonnelValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="DropDownListPersonnel" runat="server" SetFocusOnError="true"  
                                            ErrorMessage="Please Add a Personnel" ValidationGroup="ModelPersonnelAdd">

                                        </asp:RequiredFieldValidator>--%>
                                <asp:SqlDataSource ID="SqlDataSourcePersonnel" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
                                    SelectCommand="
                                    
                                    select 
                                    r.RosterID, r.RosterName
                                    from roster r
                                    inner join RosterCategory rc on rc.UId = r.rosterCategoryId
                                    where r.CanBePI = 0 and rc.IsPersonnel = 1 order by r.RosterName
                                    
                                    ">

                                </asp:SqlDataSource>
                                
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label17"  runat="server" Text="Role Name"></asp:Label>
                                <asp:TextBox ID="TextBoxPersonnelRole" runat="server"></asp:TextBox>

                                <%--<asp:DropDownList ID="DropDownListPersonnelRole" CssClass="form-control" runat="server"  AppendDataBoundItems ="true"

                                    DataSourceID="SqlDataSourcePersonnelRole"
                                    DataTextField="RoleName" DataValueField="RoleID">
                                    <asp:ListItem Value="">None</asp:ListItem>

                                </asp:DropDownList>--%>
                               <%-- <asp:RequiredFieldValidator ID="DropDownListPersonnelRoleValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="DropDownListPersonnelRole" runat="server" SetFocusOnError="true"  
                                            ErrorMessage="Please Select a Personnel Role" ValidationGroup="ModelPersonnelAdd">

                                        </asp:RequiredFieldValidator>--%>

                                <asp:SqlDataSource ID="SqlDataSourcePersonnelRole" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
                                    SelectCommand="
                                    
                                    select 
                                    r.RoleID, r.RoleName
                                    from roles r
                                    left join RoleCategory rc on rc.UId = r.RoleCategoryId
                                    where 
                                    r.RoleID = 1017
                                    
                                    
                                    ">

                                    <%-- db coomands

                                        rc.IsWorkMember = 1 
                                    or rc.OrderPriority in (select rcc.OrderPriority from RoleCategory rcc 
                                    where rcc.IsDirectiveLeader = 0  and rcc.IsDirectiveManager =0 
                                    and rcc.IsInvestigationOfficer =0 )
                                    order by rc.OrderPriority
                                        --%>

                                </asp:SqlDataSource>
                                
                            </div>
                        </div>
                                                    <%--<asp:Button ID="NewPersonelRole" CssClass="btn btn-primary" runat="server" Text="Add" ValidationGroup="ModelPersonnelAdd" OnClick="NewPersonelRole_Click" />--%>

                    </div>
                    <div class="row">
                        <div class="col-md-4" style="display:none !important">
                            <div class="form-group">
                                <asp:Label ID="LabelOtherPersonal"  runat="server" Text="Other personnel:"></asp:Label>
                                <asp:TextBox ID="TextBoxOtherPersonal" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelPercentage"  runat="server" Text="% of Time:"></asp:Label>
                                <asp:TextBox ID="TextBoxPercentage" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="LabelOtherPersonalLocation"  runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListOtherPersonalLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                 <% if (exp.g.exp.listCanAdd)
                                     {%>
                                
                                <asp:Button ID="ButtonOtherPersonalYes" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonOtherPersonal_Click" Enabled="true" ValidationGroup="ModelPersonnelAdd" />
                                <%}
                                    else
                                    { %>
                               <asp:Button ID="ButtonOtherPersonalNop" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonOtherPersonal_Click"  Enabled="false" ValidationGroup="ModelPersonnelAdd" />
                                <%} %>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center">--%>
                                <asp:GridView  ID="gvOP" 
                                    runat="server"  DataKeyNames="OPID, RosterID"
                                    DataSourceID="sdsOtherPersonel" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                   >
                                    <%--<RowStyle CssClass="data-row" />--%>
                                    <Columns>
                                        <%--<asp:BoundField DataField="Name" HeaderText="Name"  Visible="false" />--%>
                                        

                                        <asp:TemplateField HeaderText="Personnel" SortExpression="Personnel">
                                            <EditItemTemplate>                                                
                                                <asp:DropDownList ID="DropDownListPersonnelEdit" runat="server" DataSourceID="SqlDataSourcePersonnel"
                                                    DataTextField="RosterName"
                                                    DataValueField="RosterID" SelectedValue='<%# Bind("RosterID") %>' >
                                                    
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="LabelRosterNameItem" runat="server" Text='<%# Bind("PersonnellManAdded") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role" >
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownListPersonnelRoleEdit" runat="server" DataSourceID="SqlDataSourcePersonnelRole" DataTextField="RoleName"
                                                    DataValueField="RoleId" SelectedValue='<%# Bind("RoleID") %>'>
                                                </asp:DropDownList>
                                                
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRoleNameItem" runat="server" Text='<%# Bind("RoleNameManAdded") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="PerTime" HeaderText="% of Time"  />


                                        <asp:TemplateField HeaderText="Location" >
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsOPLocEdit" DataTextField="LocationName"
                                                    DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsOPLocEdit" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                    SelectCommand="SELECT [LocationID], [LocationName] FROM [Location] order by case(CHARINDEX('-region',LocationName)) when 0	then '0 '+locationName else '1 '+substring(locationName,0,CHARINDEX('-region',LocationName)) end"></asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsOtherPersonel" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [OtherPersonel] WHERE [OPID] = @original_OPID" InsertCommand="INSERT INTO [OtherPersonel] ([Name], [PerTime], [ProjectID], [LocationID]) VALUES (@Name, @PerTime, @ProjectID, @LocationID)"
                                    OldValuesParameterFormatString="original_{0}" 
                                    
                                    SelectCommand="
                                    SELECT 
                                    [OPID], [Name], [PerTime], [ProjectID], [LocationID], [PersonnellManAdded],[RoleNameManAdded], op.RosterID, op.RoleID,
                                    (SELECT LocationName FROM Location L WHERE LocationID=OP.LocationID) as LocationName,
                                    (SELECT RosterName FROM Roster R Where RosterID=op.RosterID) as RosterName,
                                    (SELECT RoleName FROM Roles PR WHERE RoleId=op.RoleID) as RoleName
                                    FROM OtherPersonel OP WHERE ([ProjectID] = @ProjectID) ORDER BY [OPID]
                                    
                                    "
                                    UpdateCommand="UPDATE [OtherPersonel] SET 
                                    [RosterID] = @RosterID, [RoleID] = @RoleID, 
                                    [Name] = @Name, [PerTime] = @PerTime, [LocationID] = @LocationID WHERE [OPID] = @original_OPID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_OPID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="PerTime" Type="Int32" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                        <asp:Parameter Name="RosterID"/>
                                        <asp:Parameter Name="RoleID" Type="Int32" />
                                        <asp:Parameter Name="original_OPID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="PerTime" Type="Int32" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="LocationID" Type="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Add Graduates and undergraduates assistanships:</h4>
                            </div>
                        </div>
                    </div>
                                                <div class="row">
                                                       <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label19" runat="server" Text="Student:"></asp:Label>
                                                            <asp:TextBox ID="TextBoxStudentNames" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>

                                                 
                                                           </div>
                                                    </div>
                                                   <%-- <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label18" runat="server" Text="Student:"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListStudent" CssClass="form-control" runat="server" AppendDataBoundItems="true"
                                                                DataSourceID="SqlDataSourceStudent" DataTextField="RosterName" DataValueField="RosterID">
                                                                <asp:ListItem Value="">None</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                        <%--    <asp:RequiredFieldValidator ID="DropDownListStudentValidatorRequired" Display="Dynamic"
                                                                ControlToValidate="DropDownListStudent" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Please Select a Student" ValidationGroup="ModelStudentAdd">

                                                            </asp:RequiredFieldValidator>--%>
                                                   <%--         <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                                SelectCommand="
                                    
                                                            select 
                                                            r.RosterID, r.RosterName
                                                            from roster r
                                                            inner join RosterCategory rc on rc.UId = r.rosterCategoryId
                                                            where r.CanBePI = 0 and rc.IsStudent = 1 order by r.RosterName
                                    
                                    "></asp:SqlDataSource>

                                                        </div>
                                                    </div>--%>
                                                        
                                                       <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label18" runat="server" Text="Type Of Students:"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListStudent" CssClass="form-control" runat="server" AppendDataBoundItems="true"
                                                                DataSourceID="SqlDataSourceStudent" DataTextField="RosterName" DataValueField="RosterID">
                                                                <asp:ListItem Value="">None</asp:ListItem>
                                                            </asp:DropDownList>
                                                        <%--    <asp:RequiredFieldValidator ID="DropDownListStudentValidatorRequired" Display="Dynamic"
                                                                ControlToValidate="DropDownListStudent" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Please Select a Student" ValidationGroup="ModelStudentAdd">

                                                            </asp:RequiredFieldValidator>--%>
                                                            <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                                SelectCommand="
                                    
                                                            select 
                                                            r.RosterID, r.RosterName
                                                            from roster r
                                                            inner join RosterCategory rc on rc.UId = r.rosterCategoryId
                                                            where r.CanBePI = 0 and rc.IsStudent = 1 order by r.RosterName
                                    
                                    "></asp:SqlDataSource>

                                                        </div>
                                                    </div>

                                                 <%--   <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label19" runat="server" Text="Type Of Students"></asp:Label>
                                                            <asp:DropDownList ID="DropDownListStudentRole" CssClass="form-control" runat="server" AppendDataBoundItems="true"
                                                                DataSourceID="SqlDataSourceStudentRole" DataTextField="RoleName" DataValueField="RoleID">
                                                                <asp:ListItem Value="">None</asp:ListItem>

                                                            </asp:DropDownList>--%>
                                                        <%--    <asp:RequiredFieldValidator ID="DropDownListStudentRoleValidatorRequired" Display="Dynamic"
                                                                ControlToValidate="DropDownListStudentRole" runat="server" SetFocusOnError="true"
                                                                ErrorMessage="Please Select a Student Role" ValidationGroup="ModelStudentAdd">

                                                            </asp:RequiredFieldValidator>--%>

                                                      <%--      <asp:SqlDataSource ID="SqlDataSourceStudentRole" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                                                SelectCommand="
                                    
                                                                select 
                                                                r.RoleID, r.RoleName
                                                                from roles r
                                                                left join RoleCategory rc on rc.UId = r.RoleCategoryId
                                                                where 
                                                                rc.IsWorkMember = 1 
                                                                or rc.OrderPriority in (select rcc.OrderPriority from RoleCategory rcc 
                                                                where rcc.IsDirectiveLeader = 0 and rcc.IsAssistantLeader =0 and rcc.IsDirectiveManager =0 and rcc.IsVisorCompany = 0
                                                                and rcc.IsWorkAdministrator = 0 and rcc.IsInvestigationOfficer =0 and rcc.IsTaskOfficer = 0 )
                                                                order by rc.OrderPriority
                                    
                                    " OnSelecting="SqlDataSourceStudentRole_Selecting"></asp:SqlDataSource>

                                                        </div>
                                                    </div>--%>
                                                </div>
                    <div class="row">
                        <div class="col-md-3" style="display:none !important">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentName"  runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentName" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentID"  runat="server" Text="Student ID:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentID" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelThesisTitle"  runat="server" Text=" Thesis title:"></asp:Label>
                                <asp:TextBox ID="TextBoxThesisTittle" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LabelStudentAmount"  runat="server" Text="Amount:"></asp:Label>
                                <asp:TextBox ID="TextBoxStudentAmount" CssClass="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                 <% if (exp.g.exp.listCanAdd)
                                     {%>
                                
                                <asp:Button ID="ButtonStudentYes" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonStudent_Click" Enabled="true" ValidationGroup="ModelStudentAdd" />
                                <%}
                                    else
                                    { %>
                               <asp:Button ID="ButtonStudentNop" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonStudent_Click"  Enabled="false" ValidationGroup="ModelStudentAdd" />
                                <%} %>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="align-center">--%>
                                <asp:GridView  ID="gvGradAss" 
                                    runat="server" 
                                    DataKeyNames="GAID" DataSourceID="sdsGradAss" AutoGenerateColumns="False" 
                                    CssClass="gridview table table-bordered table-striped"
                                    >
                                    <%--<RowStyle CssClass="data-row"  AllowSorting="True"/>--%>
                                    <Columns>
                                        <%--<asp:BoundField DataField="Name" HeaderText="Name"  />--%>
                                        <asp:TemplateField HeaderText="Student" SortExpression="Student">
                                            <EditItemTemplate>                                                
                                                <asp:DropDownList ID="DropDownListStudentEdit" runat="server" DataSourceID="SqlDataSourceStudent"
                                                    DataTextField="RosterName"
                                                    DataValueField="RosterID" SelectedValue='<%# Bind("RosterID") %>' >
                                                    
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="LabelRosterNameItemX" runat="server" Text='<%# Bind("StudentName") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type of Student" >
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownListStudentRoleEdit" runat="server" DataSourceID="SqlDataSourceStudentRole" DataTextField="RoleName"
                                                    DataValueField="RoleId" SelectedValue='<%# Bind("RoleID") %>'>
                                                </asp:DropDownList>
                                                
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelRoleNameItemX" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Thesis" HeaderText="Thesis or Project"  />
                                        <asp:BoundField DataField="StudentID" HeaderText="Student ID"  />
                                        <asp:BoundField DataField="Amountp" HeaderText="Amount"  />
                                        <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records Found!
                                    </EmptyDataTemplate>
                                    <%--<AlternatingRowStyle CssClass="alt-data-row" />--%>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsGradAss" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    DeleteCommand="DELETE FROM [GradAss] WHERE [GAID] = @original_GAID" InsertCommand="INSERT INTO [GradAss] ([Name], [Thesis], [ProjectID], [StudentID], [Amountp]) VALUES (@Name, @Thesis, @ProjectID, @StudentID, @Amountp)"
                                    OldValuesParameterFormatString="original_{0}" 
                                    SelectCommand="SELECT 
                                    
                                    [GAID], [Name], [Thesis], [ProjectID], [StudentID], [Amountp], [StudentName]
                                    , op.RosterID, op.RoleID,
                                    
                                    (SELECT RosterName FROM Roster R Where RosterID=op.RosterID) as RosterName,
                                    (SELECT RoleName FROM Roles PR WHERE RoleId=op.RoleID) as RoleName
                                    
                                    FROM [GradAss] op WHERE ([ProjectID] = @ProjectID) ORDER BY [GAID]
                                    
                                    "
                                    UpdateCommand="UPDATE [GradAss] SET 
                                    [RosterID] = @RosterID, [RoleID] = @RoleID, 
                                    [Name] = @Name, [Thesis] = @Thesis, [StudentID] = @StudentID, [Amountp] = @Amountp WHERE [GAID] = @original_GAID">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_GAID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="RosterID"/>
                                        <asp:Parameter Name="RoleID" Type="Int32" />
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="Thesis" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="StudentID" Type="String" />
                                        <asp:Parameter Name="Amountp" Type="Decimal" />
                                        <asp:Parameter Name="original_GAID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Name" Type="String" />
                                        <asp:Parameter Name="Thesis" Type="String" />
                                        <asp:Parameter Name="ProjectID" Type="String" />
                                        <asp:Parameter Name="StudentID" Type="String" />
                                        <asp:Parameter Name="Amountp" Type="Decimal" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
                                            

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button7" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                            <%--<div class="overlay" <%=  string.Format("style = '{0}'", exp.g.listEditStyle) %>>
                                            </div>--%>

                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.h.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "h" , exp.h.exp.numLine) %>><a class="text-white" href="#h" data-toggle="tab">Next</a></span>
                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.h.tabStyle)   %>tab-pane" id="h">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Funds for Fiscal Year</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>


                                            <div class="card-body">
 <%--                   <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <h4>Funds for Fiscal Year :</h4>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelFundLocation"  runat="server" Text="Location:"></asp:Label>
                                <asp:DropDownList ID="DropDownListFundLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                    DataTextField="LocationName" DataValueField="LocationID">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSalaries"  runat="server" Text="Salaries:"></asp:Label>
                                <asp:TextBox ID="TextBoxSalaries" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelWages"  runat="server" Text="Wages:"></asp:Label>
                                <asp:TextBox ID="TextBoxWages" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelBenifit"  runat="server" Text="Benefits:"></asp:Label>
                                <asp:TextBox ID="TextBoxBenifit" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAssistant"  runat="server" Text="Assistantships:"></asp:Label>
                                <asp:TextBox ID="TextBoxAssistant" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelMaterials"  runat="server" Text="Materials:"></asp:Label>
                                <asp:TextBox ID="TextBoxMaterials" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelEquipment"  runat="server" Text="Equipment:"></asp:Label>
                                <asp:TextBox ID="TextBoxEquipment" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelTravel"  runat="server" Text="Travel :"></asp:Label>
                                <asp:TextBox ID="TextBoxTravel" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAbroad"  runat="server" Text="Abroad:"></asp:Label>
                                <asp:TextBox ID="TextBoxAbroad" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSubcontracts"  runat="server" Text="Subcontracts:"></asp:Label>
                                <asp:TextBox ID="TextBoxSubcontracts" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label6"  runat="server" Text="Indirect Costs:"></asp:Label>
                                <asp:TextBox ID="TextBoxIndirectCosts" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label5"  runat="server" Text="Others:"></asp:Label>
                                <asp:TextBox ID="TextBoxOthers" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="ButtonFunds" CssClass="btn btn-primary" runat="server" Text="ADD" OnClick="ButtonFunds_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="align-center">
                                <asp:DataList ID="dlFunds" runat="server" CellPadding="4" DataKeyField="FundID"  DataSourceID="sdsFunds"
                                    Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif" Font-Overline="False"
                                    Font-Size="12px" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                    RepeatDirection="Horizontal" CssClass="yui-datatable-theme" RepeatColumns="5"
                                    OnEditCommand="dlFunds_Edit_Command"
                                    OnUpdateCommand="dlFunds_Update_Command"
                                    OnCancelCommand="dlFunds_Cancel_Command"
                                    OnDeleteCommand="dlFunds_Delete_Command"
                                    OnItemDataBound="dlFunds_ItemDataBound"
                                    >
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <AlternatingItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <br />
                                        <table  align="left" class="style1" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="LocationNameLabel" runat="server" Text='<%# Eval("LocationName") %>'
                                                        Font-Bold="True" Font-Size="14px" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="SalariesLabel" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="WagesLabel" runat="server" Text='<%# Eval("Wages", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="BenefitsLabel" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="AssistantLabel" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="MaterialsLabel" runat="server" Text='<%# Eval("Materials", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="EquipmentLabel" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="TravelLabel" runat="server" Text='<%# Eval("Travel", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="AbroadLabel" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="SubcontractsLabel" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>' />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td class="DataListData">
                                                    <asp:Label ID="OthersLabel" runat="server" Text='<%# Eval("Others", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <b>Total:</b>
                                                </td>
                                                <td class="DataListData">
                                                    <b>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalLoc", "{0:C}") %>'></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <%--<asp:LinkButton ID="lbEdit" CommandName="Edit" runat="server">Edit</asp:LinkButton>
                                                    |
                                            <asp:LinkButton ID="lbDelete" CommandName="Delete" runat="server">Delete</asp:LinkButton>--%>
                                                    <asp:Button ID="btnEdit" CommandName="Edit" Text="Edit" runat="server"/>
                                                    <asp:Button ID="btnDelete" CommandName="Delete" Text="Delete" runat="server"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </AlternatingItemTemplate>
                                    <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif"
                                        Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#284775" HorizontalAlign="Left" VerticalAlign="Top" CssClass="alt-data-row" />
                                    <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif"
                                        Font-Overline="False" Font-Size="12px" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="Top" CssClass="data-row" />
                                    <EditItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationNameLabel" runat="server" Visible="false" Font-Bold="True" Font-Size="14px"
                                            Text='<%# Eval("LocationName") %>' />
                                        <asp:DropDownList ID="ComboBoxFundLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceLocation"
                                            DataTextField="LocationName" DataValueField="LocationID" SelectedValue='<%# Bind("LocationID") %>'>
                                        </asp:DropDownList>
                                        <br />
                                        <table align="left" cellpadding="3" cellspacing="3" class="style1">
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rsa" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rwa" runat="server" Text='<%# Eval("Wages", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rbe" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="ras" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rma" runat="server" Text='<%# Eval("Materials", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="req" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rtr" runat="server" Text='<%# Eval("Travel", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rab" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rsu" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rin" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="rot" runat="server" Text='<%# Eval("Others", "{0:N}") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lbUpdate" CommandName="Update" runat="server">Update</asp:LinkButton>
                                                    &nbsp;|
                                            <asp:LinkButton ID="lbCancel" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <SeparatorStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderTemplate>
                                        Budget for Current Project
                                    </HeaderTemplate>
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <ItemTemplate>
                                        <asp:Label ID="FundIdLabel" Visible="false" Text='<%# Eval("FundId") %>' runat="server"></asp:Label>
                                        <asp:Label ID="LocationIdLabel" Visible="false" Text='<%# Eval("LocationId") %>' runat="server"></asp:Label>
                                        <br />
                                        <table align="left" class="style1" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="LocationNameLabel" runat="server" Text='<%# Eval("LocationName") %>'
                                                        Font-Bold="True" Font-Size="14px" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>Salaries:
                                                </td>
                                                <td align="right" class="DataListData">
                                                    <asp:Label ID="SalariesLabel" runat="server" Text='<%# Eval("Salaries", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Wages:
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="WagesLabel" runat="server" Text='<%# Eval("Wages", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Benefits:
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="BenefitsLabel" runat="server" Text='<%# Eval("Benefits", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assistant:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="AssistantLabel" runat="server" Text='<%# Eval("Assistant", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Materials:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="MaterialsLabel" runat="server" Text='<%# Eval("Materials", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Equipment:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="EquipmentLabel" runat="server" Text='<%# Eval("Equipment", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Travel:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="TravelLabel" runat="server" Text='<%# Eval("Travel", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Abroad:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="AbroadLabel" runat="server" Text='<%# Eval("Abroad", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Subcontracts:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="SubcontractsLabel" runat="server" Text='<%# Eval("Subcontracts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Indirect Costs:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("IndirectCosts", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Others:&nbsp;
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <asp:Label ID="OthersLabel" runat="server" Text='<%# Eval("Others", "{0:N}") %>' />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style3">
                                                    <b>Total:</b>
                                                </td>
                                                <td class="DataListData" align="right">
                                                    <b>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TotalLoc", "{0:C}") %>'></asp:Label>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnEdit" CommandName="Edit" Text="Edit" runat="server"/>
                                                    <asp:Button ID="btnDelete" CommandName="Delete" Text="Delete" runat="server"/>
                                                    <%--<asp:LinkButton  ID="lbEdit" CommandName="Edit" runat="server">Edit</asp:LinkButton>--%>
                                                   <%-- |--%>
                                                    <%--<asp:LinkButton ID="lbDelete" CommandName="Delete" runat="server">Delete</asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:SqlDataSource ID="sdsFunds" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                    SelectCommand="SELECT [FundID], [LocationID], [Salaries], [Wages], [Benefits], [Assistant], [Materials], [Equipment], [Travel], [Abroad], [Subcontracts], [IndirectCosts], [Others], [ProjectID],
(SELECT LocationName FROM Location WHERE LocationID=F.LocationID) as LocationName,
(isnull(Salaries,0)+isnull(Wages,0)+isnull(Benefits,0)+isnull(Assistant,0)+ isnull(Materials,0)+ isnull(Equipment, 0)+ isnull(Travel,0)+ isnull(Abroad,0)+ isnull(Subcontracts,0) + isnull(IndirectCosts,0) + isnull(others,0)) as TotalLoc
FROM Funds F WHERE ([ProjectID] = @ProjectID)

union all
SELECT
	FundID = null,
	LocationID = null,
	sum( isnull(fnds.Salaries, 0)) Salaries, 
	sum( isnull(fnds.Wages, 0)) Wages,
	sum( isnull(fnds.Benefits, 0)) Benefits,
	sum( isnull(fnds.Assistant, 0)) Assistant,
	sum( isnull(fnds.Materials, 0)) Materials,
	sum( isnull(fnds.Equipment, 0)) Equipment,
	sum( isnull(fnds.Travel, 0)) Travel,
	sum( isnull(fnds.Abroad, 0)) Abroad,
	sum( isnull(fnds.Subcontracts, 0)) Subcontracts,
    sum( isnull(fnds.IndirectCosts, 0)) IndirectCosts,
	sum( isnull(fnds.Others, 0)) Others,
	fnds.ProjectID,
	LocationName = 'All Locations',
	(	sum( isnull(fnds.Salaries, 0))+
		sum( isnull(fnds.Wages, 0))+
		sum( isnull(fnds.Benefits, 0))+
		sum( isnull(fnds.Assistant, 0))+ 
		sum( isnull(fnds.Materials, 0))+ 
		sum( isnull(fnds.Equipment, 0))+ 
		sum( isnull(fnds.Travel, 0))+ 
		sum( isnull(fnds.Abroad, 0))+ 
		sum( isnull(fnds.Subcontracts, 0)) + 
        sum( isnull(fnds.IndirectCosts, 0)) + 
		sum( isnull(fnds.Others, 0))
	) as TotalLoc
FROM Funds fnds
WHERE (fnds.ProjectID = @ProjectID)
group by fnds.ProjectID                                    
                                    ">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="ProjectID" QueryStringField="PID" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                                            

                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <%--<asp:Button TabIndex="6" ID="button7" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.h.listEditStyle) %>>
                                            </div>
                                        </div>
                                    <span class="btn btn-primary" <%=  string.Format("style = '{0}'", exp.i.cardStyle) %> <%=  string.Format("onclick = \"SelectTabFromServer('{0}', '{1}');\" ", "i" , exp.i.exp.numLine) %>><a class="text-white" href="#i" data-toggle="tab">Next</a></span>

                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.i.tabStyle)   %>tab-pane" id="i">
                                        <div class="card card-default">
                                            <div class="card-header">
                                                <h4 class="title">Budget justification</h4>

                                                <div class="card-tools">
                                                    <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                                                </div>
                                            </div>

                                           
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="LabelProjectImpact"  runat="server" Text="Project Impact:"></asp:Label>
                                <asp:TextBox ID="TextBoxProjectImpact" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelSalariesDesc"  runat="server" Text="Salaries (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxSalariesDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label10"  runat="server" Text="Wages (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxWagesDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="Label13"  runat="server" Text="Benefits (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxBenefitsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label9"  runat="server" Text="Assistantships (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxAssistantshipsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelMaterialDesc"  runat="server" Text="Materials (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxMaterialDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelEquipmentDesc"  runat="server" Text="Equipment (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxEquipmentDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelTravelDesc"  runat="server" Text="Travel (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxTravelDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelAbroadDesc"  runat="server" Text="Abroad (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxAbroadDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label12"  runat="server" Text="Subcontracts (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxSubcontractsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label11"  runat="server" Text="Indirect Costs (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxIndirectCostsDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="LabelOthersDesc"  runat="server" Text="Others (Description):"></asp:Label>
                                <asp:TextBox ID="TextBoxOthersDesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>


                                            <div class="card-footer">

                                                <%--<asp:Button TabIndex="6" ID="buttonSave_a2" OnClick="SaveChangesSection_a1_Click" CssClass="btn btn-primary" runat="server" Text="Save Changes" />--%>
                                                <asp:Button TabIndex="6" ID="button7" CssClass="btn btn-primary" runat="server" Text="Submit!" onCLick="SaveChanges_Click"/>



                                            </div>
                                            <div class="overlay" <%=  string.Format("style = '{0}'", exp.i.dataEditStyle) %>>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.tab-pane 
                                        card card-primary direct-chat direct-chat-primary

                                        <div class="card card-primary direct-chat direct-chat-warning">
                                        -->
                                    <div class="<%= string.Format("{0} ", exp.j.tabStyle)   %>tab-pane" id="j">
                                        <div class="card card-info card-outline direct-chat direct-chat-info">
                                             <div class="card-header ui-sortable-handle" style="cursor: move;">
                                                 <h2 class="card-title">Signs</h2>

                                                 <div class="card-tools">
                                                     <span data-toggle="tooltip" title="3 New Messages" class="badge badge-primary" style="display:none !important">3</span>
                                                     <button type="button" class="btn btn-tool" data-widget="collapse">
                                                         <i class="fa fa-minus"></i>
                                                     </button>
                                                     <button style="display:none !important" type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>
                                                     <button type="button" class="btn btn-tool" data-widget="remove">
                                                         <i class="fa fa-times"></i>
                                                     </button>
                                                 </div>
                                             </div>
                                             <!-- /.card-header -->
                                             <div class="card-body" style="display: block;">
                                                 <!-- Conversations are loaded here -->
                                                 <div class="direct-chat-messages">
                                                     
                                                     <%= this.GetProjectSigns() %>
                                                    

                                                 </div>
                                                 <!--/.direct-chat-messages-->

                                                 
                                             </div>
                                             <!-- /.card-body -->
                                             <div class="card-footer"  <%=  string.Format("style = '{0}'", exp.j.listAddStyle + (string.IsNullOrEmpty(exp.j.listAddStyle) ? "display: block;": "" ) ) %> >
                                                 
                                                 <div class="input-group">
                                                     <asp:Image ID="imagenSign"  runat="server"/>
                                                     
                                                     <asp:TextBox  TextMode="MultiLine" Rows="1" style="resize:none !important;border-color:transparent;"    ID="TextBox1"  CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         <asp:Button ID="button1" CssClass="btn btn-primary" ToolTip="Add Sign" runat="server" Text="+"  OnClick="ButtonSignAdd_Click" />

                                                     </span>
                                                 </div>
                                                 <%--<div class="input-group">
                                                     <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" Display="Dynamic" 
                                                         ControlToValidate="TextBoxNote" runat="server"
                                                         ErrorMessage="Please Add a Message" ValidationGroup="NoteAdd">

                                                     </asp:RequiredFieldValidator>
                                                     
                                                 </div>--%>

                                                
                                             </div>

                                           
                                             <!-- /.card-footer-->
                                         </div>
                                    </div>
                                    <!-- /.tab-pane -->
                                     <div class="<%= string.Format("{0} ", exp.k.tabStyle)   %>tab-pane" id="k">
                                         <%--
                                             this chat is used for classic 
                                             <div class="card direct-chat direct-chat-primary">
                                             card card-primary direct-chat direct-chat-primary
                                             card direct-chat direct-chat-warning
                                             --%>
                                         <div class="card card-primary card-outline direct-chat direct-chat-primary">
                                             <div class="card-header ui-sortable-handle" style="cursor: move;">
                                                 <h2 class="card-title">Notes</h2>

                                                 <div class="card-tools">
                                                     <span data-toggle="tooltip" title="3 New Messages" class="badge badge-primary" style="display:none !important">3</span>
                                                     <button type="button" class="btn btn-tool" data-widget="collapse">
                                                         <i class="fa fa-minus"></i>
                                                     </button>
                                                     <%--<button style="display:none !important" type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>--%>
                                                     <button  type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>
                                                     <button type="button" class="btn btn-tool" data-widget="remove">
                                                         <i class="fa fa-times"></i>
                                                     </button>
                                                 </div>
                                             </div>
                                             <!-- /.card-header -->
                                             <div class="card-body" style="display: block;">
                                                 <!-- Conversations are loaded here -->
                                                 <div class="direct-chat-messages">
                                                     
                                                     <%= this.GetProjectNotes() %>
                                                    

                                                 </div>
                                                 <!--/.direct-chat-messages-->

                                                 
                                                 
                                             </div>
                                             <!-- /.card-body -->
                                             <div class="card-footer"  <%=  string.Format("style = '{0}'", exp.k.listAddStyle + (string.IsNullOrEmpty(exp.k.listAddStyle) ? "display: block;": "" ) ) %> >
                                                 <%--<form action="#" method="post">--%>
                                                 <div class="input-group">
                                                     
                                                     <asp:TextBox TextMode="MultiLine" Rows="1" style="resize:none !important"    ID="TextBoxNote" placeholder="Type Message ..." CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         <asp:Button ID="buttonNote" CssClass="btn btn-primary" runat="server" ToolTip="Add Note" Text="+" ValidationGroup="NoteAdd" OnClick="ButtonNoteAdd_Click" />

                                                     </span>
                                                 </div>
                                                 <div class="input-group">
                                                <%--     <asp:RequiredFieldValidator SetFocusOnError="true" ID="TextBoxNoteValidatorRequired" Display="Dynamic" 
                                                         ControlToValidate="TextBoxNote" runat="server"
                                                         ErrorMessage="Please Add a Message" ValidationGroup="NoteAdd">

                                                     </asp:RequiredFieldValidator>--%>
                                                     
                                                 </div>

                                                 <%--</form>--%>
                                             </div>

                                            <%-- <div class="overlay" <%=  string.Format("style = '{0}'", exp.k.listAddStyle) %>>
                                            </div>--%>
                                             <!-- /.card-footer-->
                                         </div>
                                    </div>
                                    <!-- /.tab-pane -->
                                    <div class="<%= string.Format("{0} ", exp.l.tabStyle)   %>tab-pane" id="l">
                                        <div class="card card-success card-outline direct-chat direct-chat-success">
                                             <div class="card-header ui-sortable-handle" style="cursor: move;">
                                                 <h4 class="title">Assents</h4>

                                                 <div class="card-tools">
                                                     <span data-toggle="tooltip" title="3 New Messages" class="badge badge-primary" style="display:none !important">3</span>
                                                     <button type="button" class="btn btn-tool" data-widget="collapse">
                                                         <i class="fa fa-minus"></i>
                                                     </button>
                                                     <%--<button style="display:none !important" type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>--%>
                                                     <button  type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>
                                                     <button type="button" class="btn btn-tool" data-widget="remove">
                                                         <i class="fa fa-times"></i>
                                                     </button>
                                                 </div>
                                             </div>
                                             <!-- /.card-header -->
                                             <div class="card-body" style="display: block;">
                                                 <!-- Conversations are loaded here -->
                                                 <div class="direct-chat-messages">
                                                     
                                                     <%= this.GetProjectAssents() %>
                                                    

                                                 </div>
                                                 <!--/.direct-chat-messages-->

                                               
                                                 
                                             </div>
                                             <!-- /.card-body -->
                                             <div class="card-footer"  >
                                                 <%--<form action="#" method="post">--%>
                                                 <div class="input-group">
                                                     
                                                     <asp:TextBox TextMode="MultiLine" Rows="1" style="resize:none !important"    ID="TextBoxAssent" placeholder="Type Message ..." CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         <asp:Button ID="ButtonAssent" CssClass="btn btn-primary" runat="server" ToolTip="Add Note" Text="+" ValidationGroup="AssentAdd" OnClick="ButtonAssent_Click" />

                                                     </span>
                                                 </div>
                                                 <div class="input-group">
                                                    <%-- <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" Display="Dynamic" 
                                                         ControlToValidate="TextBoxAssent" runat="server" 
                                                         ErrorMessage="Please Add a Message" ValidationGroup="AssentAdd">

                                                     </asp:RequiredFieldValidator>--%>
                                                     
                                                 </div>

                                                 <%--</form>--%>
                                             </div>

                                            <%-- <div class="overlay" <%=  string.Format("style = '{0}'", exp.k.listAddStyle) %>>
                                            </div>--%>
                                             <!-- /.card-footer-->
                                         </div>
                                        </div>
                                    <div class="<%= string.Format("{0} ", exp.m.tabStyle)   %>tab-pane" id="m">
                                        <div class="card card-warning card-outline  direct-chat-warning">
                                             <div class="card-header ui-sortable-handle" style="cursor: move;">
                                                 <h4 class="title">Objections</h4>

                                                 <div class="card-tools">
                                                     <span data-toggle="tooltip" title="3 New Messages" class="badge badge-primary" style="display:none !important">3</span>
                                                     <button type="button" class="btn btn-tool" data-widget="collapse">
                                                         <i class="fa fa-minus"></i>
                                                     </button>
                                                     <%--<button style="display:none !important" type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>--%>
                                                     <button  type="button" class="btn btn-tool" data-toggle="tooltip" title="Contacts" data-widget="chat-pane-toggle">
                                                         <i class="fa fa-comments"></i>
                                                     </button>
                                                     <button type="button" class="btn btn-tool" data-widget="remove">
                                                         <i class="fa fa-times"></i>
                                                     </button>
                                                 </div>
                                             </div>
                                             <!-- /.card-header -->
                                             <div class="card-body" style="display: block;">
                                                 <!-- Conversations are loaded here -->
                                                 <div class="direct-chat-messages">
                                                     
                                                     <%= this.GetProjectObjetions() %>
                                                    

                                                 </div>
                                                 <!--/.direct-chat-messages-->

                                                 
                                             </div>
                                             <!-- /.card-body -->
                                             <div class="card-footer"  >
                                                 <%--<form action="#" method="post">--%>
                                                 <div class="input-group">
                                                     
                                                     <asp:TextBox TextMode="MultiLine" Rows="1" style="resize:none !important"    ID="TextBoxObjetion" placeholder="Type Message ..." CssClass="form-control" runat="server"></asp:TextBox>
                                                     <span class="input-group-append">
                                                         <asp:Button ID="ButtonObjetion" CssClass="btn btn-primary" runat="server" ToolTip="Add Note" Text="+" ValidationGroup="ObjetionAdd" OnClick="ButtonObjetion_Click" />

                                                     </span>
                                                 </div>
                                                 <div class="input-group">
                                                  <%--   <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" Display="Dynamic" 
                                                         ControlToValidate="TextBoxObjetion" runat="server"
                                                         ErrorMessage="Please Add a Message" ValidationGroup="ObjetionAdd">

                                                     </asp:RequiredFieldValidator>--%>
                                                     
                                                 </div>

                                                 <%--</form>--%>
                                             </div>

                                            <%-- <div class="overlay" <%=  string.Format("style = '{0}'", exp.k.listAddStyle) %>>
                                            </div>--%>
                                             <!-- /.card-footer-->
                                         </div>
                                        </div>
                                   
                                    <!-- /.tab-pane -->
                                </div>
                                <!-- /.tab-content -->
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.nav-tabs-custom -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </section>
        <!-- /.content -->
    </div>


    
</asp:Content>
