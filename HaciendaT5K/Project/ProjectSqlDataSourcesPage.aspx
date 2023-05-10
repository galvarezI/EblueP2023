<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSqlDataSourcesPage.aspx.cs" Inherits="Eblue.Project.ProjectSqlDataSourcesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSourceProjectProccessInfo" runat="server"
        SelectCommand="

         select 
            ROW_NUMBER() over (order by mdl.orderline ) as rowNumber,
            prj.ProjectID, prj.ProjectNumber, 
            mdl.Uid as ProcessId, mdl.Name ProccessName, mdl.Description ProccessDescription , mdl.WorkflowId,
            IsActual = (select top 1 cast (1 as bit) from ProcessProjectWay pwt where pwt.UId = prj.ProcessProjectWayID ), 
            mdl.IsStarter, mdl.IsFinalizer, 
            mdl.objectionsAvailabled, mdl.assentsAvailabled,
            mdl.enabledForDirectiveManager, mdl.enabledForInvestigationOfficer, mdl.enabledForAssistantLeader, mdl.enabledForDirectiveLeader,
            mdl.PreviousProcessId, mdl.AlwaysProcessId,
            mdl.NextProcessId, mdl.EstatusId, mdl.OrderLine,
            StatusDescription = (select top 1 tmp.ProjectStatusName from ProjectStatus tmp where tmp.ProjectStatusID = mdl.EstatusId),
            PreviousProccessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.PreviousProcessId),
            NextProcessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.NextProcessId),
            AlwaysProccessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.AlwaysProcessId)
            from Process mdl
            inner join ProcessProjectWay pw on pw.ProcessId = mdl.Uid
            inner join projects prj on prj.ProjectID = PW.ProjectID
            where prj.ProjectID = @ProjectID
        
        "
        >
    </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
