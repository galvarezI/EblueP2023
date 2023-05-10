using Eblue.Code.Models;
using Eblue.Code;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Eblue.Project;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.WebTools;
using static Eblue.Utils.SectionTools;
using static Eblue.Utils.PageTools;
using TypePermission = Eblue.Code.Models.RolePermission;
namespace Eblue.Utils
{
    public static class ProjectTools
    {

        public static bool GetProjectWorkFlowDefault(out Guid? uid, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            uid = default(Guid?);
            Guid? guidResult = uid;

            var sqlCommandString = @"
                        select top 1
                                        wf.Uid, wf.Name, wf.Description
                                        from WorkFlow wf
                                        where wf.isforproject = 1 and wf.IsDefault =1
";

            RequestDataInfo req = new RequestDataInfo()
            {
                commandString = sqlCommandString
            };

            result = FetchDataFirstOrDefault(req, (reader => {

                Guid.TryParse(reader["Uid"]?.ToString(), out Guid workflowId);
                guidResult = workflowId;
            }), out exceptionInfo);

            if (result)
                uid = guidResult;

            return result;
        }

        public  static bool UpdateProjectNewProjectWay(Guid newID, Guid projectProccessWayId)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            //, out Guid projectPIID
            try
            {
                cn.Open();
                string command = "update Projects set ProcessProjectWayID = @ProcessProjectWayID where ProjectID = @ProjectID  ";
                //"INSERT INTO Projects (ProjectID, ProjectNumber, ContractNumber, ProjectPI, DateRegister, LastUpdate, FiscalYearID, WFSID, ORCID, ProcessProjectWayID) VALUES (@ProjectID, @ProjectNumber, @ContractNumber, @ProjectPI, @DateRegister, @LastUpdate, @FiscalYearID, @WFSID, @ORCID, @ProcessProjectWayID)";

                SqlCommand cmd = new SqlCommand(command, cn);
                cmd.Parameters.AddWithValue("@ProjectID", newID);
                cmd.Parameters.AddWithValue("@ProcessProjectWayID", projectProccessWayId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to update the project's way", ex);
            }
            finally
            {

            }

            return result;

        }

        public static bool AddFirstStatusProject(Guid projectID, Guid rosterID, DateTime noteDate, string noteData, string rosterData)
        {
            bool result;

            try
            {

                Guid uid = Guid.NewGuid();
                Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo
                    (
                     new SqlParameter("@UId", uid),
                     new SqlParameter("@ProjectID", projectID),
                     new SqlParameter("@RosterID", rosterID),
                     //new SqlParameter("@RoleId", roleID),
                     new SqlParameter("@StatusData", noteData),
                     new SqlParameter("@StatusDate", noteDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = @"insert into projectStatuses (UId, ProjectID, RosterID, RoleId, StatusData, StatusDate, RosterData) 
                    values (@UId, @ProjectID, @RosterID, (select top 1 rl.RoleID from roles rl inner join RoleCategory rc on 
                    rc.UId = rl.RoleCategoryId where rc.IsInvestigationOfficer = 1), @StatusData, @StatusDate, @RosterData) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add status to the project", ex);
            }

            return result;

        }

        public static bool AddStatusProject(Guid projectID, Guid rosterID, int roleID, DateTime noteDate, string noteData, string rosterData)
        {
            bool result;

            try
            {

                Guid uid = Guid.NewGuid();
                Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo
                    (
                     new SqlParameter("@UId", uid),
                     new SqlParameter("@ProjectID", projectID),
                     new SqlParameter("@RosterID", rosterID),
                     new SqlParameter("@RoleId", roleID),
                     new SqlParameter("@StatusData", noteData),
                     new SqlParameter("@StatusDate", noteDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = "insert into projectStatuses (UId, ProjectID, RosterID, RoleId, StatusData, StatusDate, RosterData) " +
                    "values (@UId, @ProjectID, @RosterID, @RoleId, @StatusData, @StatusDate, @RosterData) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add status to the project", ex);
            }

            return result;

        }
        public static bool SaveNewProjectWay(out Guid projectProccessWayId, Guid projectID, Guid proccessId, int StatusID)
        {
            bool result;
            projectProccessWayId = Guid.NewGuid();
            //Guid.TryParse(DropDownPrincipalInvestigator.SelectedValue, out Guid projectPIID);
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            //, out Guid projectPIID
            try
            {
                cn.Open();
                string commandString = "insert into ProcessProjectWay(UID, ProjectId, ProcessId, EstatusId) values (@UID, @ProjectId, @ProcessId, @EstatusId)";

                SqlCommand cmd = new SqlCommand(commandString, cn);
                cmd.Parameters.AddWithValue("@UID", projectProccessWayId);
                cmd.Parameters.AddWithValue("@ProjectId", projectID);
                cmd.Parameters.AddWithValue("@ProcessId", proccessId);
                cmd.Parameters.AddWithValue("@EstatusId", StatusID);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();


                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to create the project' way", ex);
            }
            finally
            {

            }

            return result;

        }
        public static bool AddPlayerProject(Guid projectID, Guid rosterID, int RoleID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,{RoleID}, '{projectID}' ";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add roster in the project", ex);
            }
            finally
            {

            }

            return result;

        }

        public static bool RemovePlayerProject(Guid projectID, Guid rosterID, int RoleID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"delete from  playerProject (uid, rosterid, roleid, projectid) " +
                    $"rosterid  = '{rosterID}' and projectId = '{projectID}' ";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add roster in the project", ex);
            }
            finally
            {

            }

            return result;

        }

        public static bool GetProjectProccessInfo(out ProjectProccessInfoSet listset, Guid projectID, string stringCommand, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            listset = new ProjectProccessInfoSet();
            var resultList = new ProjectProccessInfoSet();
            var recordItem = default(ProjectProccessInfo);

            var sqlPage = new ProjectSqlDataSourcesPage();

            //sqlPage.Load(sqlPage, null);

            //var stringCommand = sqlPage.SDSProjectProccessInfo.SelectCommand;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@ProjectID", projectID))
            {
                commandString = stringCommand
            };


            result = FetchData(reqInfo, (reader => {

                //Way
                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UID"]?.ToString(), out Guid uid);
                int.TryParse(reader["EstatusId"]?.ToString(), out int statusId);

                ////Way                
                //Guid.TryParse(reader["ProjectID"]?.ToString(), out Guid ProjectID);
                //var projectNumber = reader["ProjectNumber"]?.ToString();

                //Project                
                Guid.TryParse(reader["ProjectID"]?.ToString(), out Guid ProjectID);
                var projectNumber = reader["ProjectNumber"]?.ToString();

                //CurrentProccess
                Guid.TryParse(reader["ProcessId"]?.ToString(), out Guid ProcessId);
                var ProccessDescription = reader["ProccessDescription"]?.ToString();

                //PrevProccess
                Guid.TryParse(reader["PreviousProcessId"]?.ToString(), out Guid PreviousProcessId);
                var PreviousProccessDescription = reader["PreviousProccessDescription"]?.ToString();

                //CurrentProccess
                Guid.TryParse(reader["NextProcessId"]?.ToString(), out Guid NextProcessId);
                var NextProcessDescription = reader["NextProcessDescription"]?.ToString();
                int.TryParse(reader["NextStatusID"]?.ToString(), out int nextStatusId);

                //AlwaysProccess
                Guid.TryParse(reader["AlwaysProcessId"]?.ToString(), out Guid AlwaysProcessId);
                var AlwaysProccessDescription = reader["AlwaysProccessDescription"]?.ToString();
                int.TryParse(reader["AlwaysStatusID"]?.ToString(), out int alwaysStatusId);

                //availabledChecks
                bool.TryParse(reader["IsActual"]?.ToString(), out bool IsActual);
                bool.TryParse(reader["IsStarter"]?.ToString(), out bool IsStarter);
                bool.TryParse(reader["IsFinalizer"]?.ToString(), out bool IsFinalizer);

                bool.TryParse(reader["objectionsAvailabled"]?.ToString(), out bool objectionsAvailabled);
                bool.TryParse(reader["assentsAvailabled"]?.ToString(), out bool assentsAvailabled);

                //enabledChecks
                bool.TryParse(reader["enabledForDirectiveManager"]?.ToString(), out bool enabledForDirectiveManager);
                bool.TryParse(reader["enabledForInvestigationOfficer"]?.ToString(), out bool enabledForInvestigationOfficer );
                bool.TryParse(reader["enabledForAssistantLeader"]?.ToString(), out bool enabledForAssistantLeader);
                bool.TryParse(reader["enabledForDirectiveLeader"]?.ToString(), out bool enabledForDirectiveLeader);
                bool.TryParse(reader["enabledForOnlyDirectiveManager"]?.ToString(), out bool enabledForOnlyDirectiveManager);
                bool.TryParse(reader["enableForResearchDirector"]?.ToString(), out bool enabledForResearchDirector);

                //Guid.TryParse(reader["Uid"]?.ToString(), out Guid guid);
                //resultUId = guid;
                recordItem = new ProjectProccessInfo
                (
                    new Tuple<int, Guid, int>(rowNumber, uid, statusId),
                    new Tuple<Guid, string>(projectID, projectNumber),
                    new Tuple<Guid, string>(ProcessId, ProccessDescription),
                    //new Tuple<Guid, string>(PreviousProcessId, PreviousProccessDescription),
                    new Tuple<Guid, string, int>(NextProcessId, NextProcessDescription, nextStatusId),
                    new Tuple<Guid, string, int>(AlwaysProcessId, AlwaysProccessDescription, alwaysStatusId),
                    new Tuple<bool, bool, bool, bool, bool>(IsActual, IsStarter, IsFinalizer, objectionsAvailabled, assentsAvailabled),
                    new Tuple<bool, bool, bool, bool, bool >(enabledForDirectiveManager, enabledForInvestigationOfficer, enabledForAssistantLeader, enabledForDirectiveLeader, enabledForOnlyDirectiveManager)


                    ) ;
                resultList.Add(recordItem);
            }), out exceptionInfo);

            if (result)
                listset = resultList;

            return result;

            //result = FetchData(reqInfo, reader => {


            //    int.TryParse(reader["roleid"]?.ToString(), out int roleID);
            //    var roleName = reader["rolename"]?.ToString();
            //    Guid.TryParse(reader["rolecategoryid"]?.ToString(), out Guid roletypeID);
            //    var roletypeDescription = reader["roletypedescription"]?.ToString();
            //    var caption = reader["roleCaption"]?.ToString();
            //    Guid.TryParse(reader["rosterid"]?.ToString(), out Guid rosterID);


            //    var model = new PlayerInfo(roleID, roleName, roletypeID, roletypeDescription, caption, rosterID);

            //    list.Add(model);
            //});

            //listset = list;

            //return result;

        }

        public static bool GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet targetSections, Guid roleCategoryID)
        {
            bool result = false;
            targetSections = new Code.TargetSectionSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmdCommand = new SqlCommand(
                        $"select ROW_NUMBER() OVER(ORDER BY rt.numLine ASC) AS RowNumber, rt.NumLine, " +
                        $"rspl.RoleTargetID, rt.name as RoleTargetName, rt.Description as RoleTargetDescription, rt.OrderLine as RoleTargetOrderLine, " +
                        $"rspl.uid RoleSetSectionID, rspl.name RoleSetSectionName, " +
                        $"rspl.whenData, rspl.dataCapDetail, rspl.dataCapEdit,  " +
                        $"rspl.whenList, rspl.listCapDetail, rspl.listCapAdd, rspl.listCapRemove, rspl.listCapEdit, " +
                        $"rspl.withTargetOf, rspl.targetOF, rspl.IsForProject, rspl.IsForProcess, rspl.OrderLine RosterSetSectionOrderLine " +
                        $"from rolepermission rp " +
                        $"inner join rolesetpermission rsp on rsp.uid = rp.rolesetpermissionID and rsp.isForProject = 1 " +
                        $"inner join rolesetpermission rspl on rspl.targetOf = rsp.uid and rspl.isForProject = 1 " +
                        $"INNER JOIN roleTarget rt on rt.uid = rspl.RoleTargetID and rt.NotVisibleForMenu =0 " +
                        $"where rp.RoleCategoryId = '{roleCategoryID}' order by rt.numLine ", cn);

                    var reader = cmdCommand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            var target = new Eblue.Code.TargetSection();

                            int.TryParse(reader["RowNumber"].ToString(), out int rownumber);
                            target.rowNumber = rownumber;

                            target.name = reader["RoleTargetName"].ToString();
                            target.description = "";// reader["RoleTargetDescription"].ToString();

                            bool.TryParse(reader["whenData"].ToString(), out bool whenData);
                            target.whenData = whenData;

                            bool.TryParse(reader["dataCapDetail"].ToString(), out bool dataCapDetail);
                            target.dataCapDetail = dataCapDetail;

                            bool.TryParse(reader["dataCapEdit"].ToString(), out bool dataCapEdit);
                            target.dataCapEdit = dataCapEdit;

                            bool.TryParse(reader["whenList"].ToString(), out bool whenList);
                            target.whenList = whenList;

                            bool.TryParse(reader["listCapDetail"].ToString(), out bool listCapDetail);
                            target.listCapDetail = listCapDetail;

                            bool.TryParse(reader["listCapAdd"].ToString(), out bool listCapAdd);
                            target.listCapAdd = listCapAdd;

                            bool.TryParse(reader["listCapRemove"].ToString(), out bool listCapRemove);
                            target.listCapRemove = listCapRemove;

                            bool.TryParse(reader["listCapEdit"].ToString(), out bool listCapEdit);
                            target.listCapEdit = listCapEdit;

                            int.TryParse(reader["RoleTargetOrderLine"].ToString(), out int orderLine);
                            target.orderLine = orderLine;

                            int.TryParse(reader["NumLine"].ToString(), out int numLine);
                            target.numLine = numLine;

                            targetSections.Add(target.name, target);

                        }

                    }

                    cn.Close();
                    result = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error at getting the sections for this role in this project", ex);
            }

            return result;

        }



        #region sections
        public static bool GetProjectPermissionsByRole(out TargetSectionSet listset, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(TargetSectionSet);
            listset = defaultValue;
            TargetSectionSet listResult = defaultValue;
            exceptionInfo = default(Tuple<bool?, Exception>);

            List<TargetSection> tsList = new List<TargetSection>();
            List<RoleSetSectionPermission> rssList = new List<RoleSetSectionPermission>();

            //result = GetTargetSectionSetFromRosterRole(out listResult, primaryKey);

            //if (result)
            //{
            //result = GetRolePermissionHandle(out RolePermission rp, primaryKey);
            result = GetRolePermissionHandle(out RolePermission rp, primaryKey);
            if (result)
            {
                result = GetRoleSetRootPermissionHandle(out RoleSetPermissionRoot rspr, rp.RoleSetPermissionID);
                if (result)
                {
                    rp.Root = rspr;
                    rspr.SectionSet = new RoleSetSectionPermissionSet();

                    //get roleset permission sections
                    bool resultSections = false;
                    resultSections = GetRoleSetSectionsPermissionHandle(out RoleSetSectionPermissionSet rsps, rspr.UId);

                    if (resultSections)
                    {
                        rspr.HasSections = true;
                        rspr.SectionSet = rsps;

                        var sets = rspr.SectionSet.ToList().Select(x => x.Value);
                        rssList.AddRange(sets);

                    }

                    //get roleset permission root (if found)
                    if (rspr.WithTargetOf != null)
                    {

                        result = GetRoleSetGrantPermissionHandle(out RoleSetPermissionGrant rspg, rspr.WithTargetOf.Value);

                        if (result)
                        {
                            rspr.Grant = rspg; rspr.HasGrant = true;
                            rspg.GrantSet = new RoleSetPermissionGrantSet();

                            bool resultZSections = false;
                            resultZSections = GetRoleSetSectionsPermissionHandle(out RoleSetSectionPermissionSet zSections, rspg.UId);

                            if (resultZSections)
                            {
                                rspg.SectionSet = zSections;

                                var sets = rspg.SectionSet.ToList().Select(x => x.Value);
                                rssList.AddRange(sets);

                            }

                            if (rspg.WithTargetOf != null)
                            {

                                result = GetRoleSetGrantNestedPermissionHandle(
                                    out RoleSetPermissionGrant rspgn,
                                    rspg.WithTargetOf.Value,
                                    (RoleSetPermissionGrant rspgnr) =>
                                    {
                                        rspr.HasGrantRecursive = true;

                                        if (!rspg.GrantSet.ContainsKey(rspgnr.UId))
                                        {

                                            bool resultXSections = false;
                                            resultXSections = GetRoleSetSectionsPermissionHandle(out RoleSetSectionPermissionSet xSections, rspgnr.UId);

                                            if (resultXSections)
                                            {
                                                rspgnr.SectionSet = xSections;

                                                var sets = rspgnr.SectionSet.ToList().Select(x => x.Value);
                                                rssList.AddRange(sets);
                                            }

                                            rspg.GrantSet.Add(rspgnr.UId, rspgnr);
                                        }

                                    }
                                    );

                                if (result)
                                {
                                    rspr.HasGrantNested = true;

                                    if (!rspg.GrantSet.ContainsKey(rspgn.UId))
                                    {

                                        bool resultYSections = false;
                                        resultYSections = GetRoleSetSectionsPermissionHandle(out RoleSetSectionPermissionSet ySections, rspgn.UId);

                                        if (resultYSections)
                                        {
                                            rspgn.SectionSet = ySections;

                                            var sets = rspgn.SectionSet.ToList().Select(x => x.Value);
                                            rssList.AddRange(sets);
                                        }

                                        rspg.GrantSet.Add(rspgn.UId, rspgn);
                                    }


                                }

                            }


                        }

                    }


                    //construct TargetSectionSet
                    {
                        var rssHeaderList = rssList.Select(x => x.name).Distinct();

                        foreach (var head in rssHeaderList)
                        {
                            var rssGroupList = rssList.Where(x => string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));

                            if (rssGroupList.Count() > 0 && rssGroupList.Count() > 1)
                            {

                                RoleSetSectionPermission section = rssGroupList.First();
                                //RoleSetSectionPermission sectionResult = default(RoleSetSectionPermission);

                                //fix equals must be where not head.uid 
                                //var rssGroupNestList = rssGroupList.Where(x => !string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));

                                //foreach (var group in rssGroupNestList)
                                foreach (var sectionResult in rssGroupList)
                                {
                                    //sectionResult = rssGroupList.FirstOrDefault(x => string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));


                                    if (sectionResult != null)
                                    {

                                        section.whenData |= sectionResult.whenData;
                                        section.dataCapDetail |= sectionResult.dataCapDetail;
                                        section.dataCapEdit |= sectionResult.dataCapEdit;


                                        section.whenList |= sectionResult.whenList;

                                        section.listCapAdd |= sectionResult.listCapAdd;
                                        section.listCapDetail |= sectionResult.listCapDetail;
                                        section.listCapEdit |= sectionResult.listCapEdit;
                                        section.listCapRemove |= sectionResult.listCapRemove;

                                    }
                                }

                                var sectionType = section.As<TargetSection>();

                                tsList.Add(sectionType);


                            }
                            else
                            {
                                RoleSetSectionPermission section = rssGroupList.First();
                                var sectionType = section.As<TargetSection>();

                                tsList.Add(sectionType);

                            }

                        }


                        if (tsList != null & rssList.Count() > 0)
                        {
                            tsList = tsList.OrderBy(x => x.numLine).ToList();
                            listResult = new TargetSectionSet();
                            foreach (var item in tsList)
                            {
                                listResult.Add(item.name, item);


                            }

                        }

                        //List<TargetSection> tsList = new List<TargetSection>();
                        //List<RoleSetSectionPermission> rssList = new List<RoleSetSectionPermission>();
                        //tsList = new List<TargetSection>();
                        //rssList = new List<RoleSetSectionPermission>();

                        //if (rp.Root.HasSections)
                        //{
                        //    var root = rp.Root;
                        //    var sets = root.SectionSet.ToList().Select(x=> x.Value);
                        //    rssList.AddRange(sets);

                        //}

                        //if (rp.Root.HasGrant)
                        //{
                        //    var grant = rp.Root.Grant;
                        //    var sets = grant.SectionSet.ToList().Select(x => x.Value);
                        //    rssList.AddRange(sets);
                        //}

                        //if (rp.Root.HasGrantNested)
                        //{
                        //    //var grant = rp.Root.Grant;
                        //    //var grantSets = grant.GrantSet.ToList().Select(x => x.Value);

                        //    //if (grantSets != null && grantSets.Count() > 0)
                        //    //{
                        //    //    var nest = rp.Root.Grant;
                        //    //    var sets = grant.SectionSet.ToList().Select(x => x.Value);
                        //    //    rssList.AddRange(sets);

                        //    //}


                        //}
                    }
                }
            }
            //}

            if (result)
                listset = listResult;

            return result;


        }

        public static bool GetProjectPermissionsByRoleHandle(out TargetSectionSet model, Guid primaryKey)
        {
            bool result;

            result = GetProjectPermissionsByRole(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the sections for this role in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }
        #endregion

        #region roleTypes

        public static bool GetProjectRoleType(out ProjectRoleType model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(ProjectRoleType);
            model = defaultValue;
            ProjectRoleType modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@RoleCategoryId", primaryKey))
            {
                commandString = @"
                    SELECT 
                    UId,
                    Name,
                    Description,
                    IsDirectiveLeader,
                    IsAssistantLeader,
                    IsDirectiveManager,
                    IsVisorCompany,
                    IsWorkAdministrator,
                    IsWorkMember,
                    IsTaskOfficer,
                    IsInvestigationOfficer,
                    IsViewerOnly,
                    IsResearchDirector,
                    IsExecutiveOfficer,
                    IsExternalResources
                    FROM RoleCategory rc
                     where rc.uid = @RoleCategoryId
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                modelResult = new ProjectRoleType();
                Guid.TryParse(reader["uid"]?.ToString(), out Guid uId);
                modelResult.uID = uId;
                
                var name = reader["name"]?.ToString();
                modelResult.name = name;
                var description = reader["description"]?.ToString();
                modelResult.description = description;

                bool.TryParse(reader["IsDirectiveManager"].ToString(), out bool isDM);
                modelResult.isDM = isDM;

                bool.TryParse(reader["IsInvestigationOfficer"].ToString(), out bool isIO);
                modelResult.isIO = isIO;

                bool.TryParse(reader["IsDirectiveLeader"].ToString(), out bool isDL);
                modelResult.isDL = isDL;

                bool.TryParse(reader["IsAssistantLeader"].ToString(), out bool isAL);
                modelResult.isAL = isAL;

                bool.TryParse(reader["IsReseearchDirector"].ToString(), out bool isRDR);
                modelResult.isRDR = isRDR;

                bool.TryParse(reader["IsExecutiveOfficer"].ToString(), out bool isEO);
                modelResult.isEO = isEO;

                bool.TryParse(reader["IsExternalResources"].ToString(), out bool isERS);
                modelResult.isERS = isERS;




            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }
        public static bool GetProjectRoleTypeHandle(out ProjectRoleType model, Guid primaryKey)
        {
            bool result;

            result = GetProjectRoleType(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role type in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }
        #endregion


        #region status

        #endregion

        #region pages
        public static bool GetPagePermissionsByRoster(out TargetSectionSet listset, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(TargetSectionSet);
            listset = defaultValue;
            TargetSectionSet listResult = defaultValue;
            exceptionInfo = default(Tuple<bool?, Exception>);

            List<TargetSection> tsList = new List<TargetSection>();
            List<RoleSetSectionPermission> rssList = new List<RoleSetSectionPermission>();

            result = GetRosterPermissionHandle(out RolePermission rp, primaryKey);
            if (result)
            {
                result = GetRosterSetRootPermissionHandle(out RoleSetPermissionRoot rspr, rp.RoleSetPermissionID);
                if (result)
                {
                    rp.Root = rspr;
                    rspr.SectionSet = new RoleSetSectionPermissionSet();

                    //get roleset permission sections
                    bool resultSections = false;
                    resultSections = GetRosterSetSectionsPermissionHandle(out RoleSetSectionPermissionSet rsps, rspr.UId);

                    if (resultSections)
                    {
                        rspr.HasSections = true;
                        rspr.SectionSet = rsps;

                        var sets = rspr.SectionSet.ToList().Select(x => x.Value);
                        rssList.AddRange(sets);

                    }

                    //get roleset permission root (if found)
                    if (rspr.WithTargetOf != null)
                    {

                        result = GetRosterSetGrantPermissionHandle(out RoleSetPermissionGrant rspg, rspr.WithTargetOf.Value);

                        if (result)
                        {
                            rspr.Grant = rspg; rspr.HasGrant = true;
                            rspg.GrantSet = new RoleSetPermissionGrantSet();

                            bool resultZSections = false;
                            resultZSections = GetRosterSetSectionsPermissionHandle(out RoleSetSectionPermissionSet zSections, rspg.UId);

                            if (resultZSections)
                            {
                                rspg.SectionSet = zSections;

                                var sets = rspg.SectionSet.ToList().Select(x => x.Value);
                                rssList.AddRange(sets);

                            }

                            if (rspg.WithTargetOf != null)
                            {

                                result = GetRosterSetGrantNestedPermissionHandle(
                                    out RoleSetPermissionGrant rspgn,
                                    rspg.WithTargetOf.Value,
                                    (RoleSetPermissionGrant rspgnr) =>
                                    {
                                        rspr.HasGrantRecursive = true;

                                        if (!rspg.GrantSet.ContainsKey(rspgnr.UId))
                                        {

                                            bool resultXSections = false;
                                            resultXSections = GetRosterSetSectionsPermissionHandle(out RoleSetSectionPermissionSet xSections, rspgnr.UId);

                                            if (resultXSections)
                                            {
                                                rspgnr.SectionSet = xSections;

                                                var sets = rspgnr.SectionSet.ToList().Select(x => x.Value);
                                                rssList.AddRange(sets);
                                            }

                                            rspg.GrantSet.Add(rspgnr.UId, rspgnr);
                                        }

                                    }
                                    );

                                if (result)
                                {
                                    rspr.HasGrantNested = true;

                                    if (!rspg.GrantSet.ContainsKey(rspgn.UId))
                                    {

                                        bool resultYSections = false;
                                        resultYSections = GetRosterSetSectionsPermissionHandle(out RoleSetSectionPermissionSet ySections, rspgn.UId);

                                        if (resultYSections)
                                        {
                                            rspgn.SectionSet = ySections;

                                            var sets = rspgn.SectionSet.ToList().Select(x => x.Value);
                                            rssList.AddRange(sets);
                                        }

                                        rspg.GrantSet.Add(rspgn.UId, rspgn);
                                    }


                                }

                            }


                        }

                    }


                    //construct TargetSectionSet
                    {
                        var rssHeaderList = rssList.Select(x => x.name).Distinct();

                        foreach (var head in rssHeaderList)
                        {
                            var rssGroupList = rssList.Where(x => string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));

                            if (rssGroupList.Count() > 0 && rssGroupList.Count() > 1)
                            {

                                RoleSetSectionPermission section = rssGroupList.First();
                                //RoleSetSectionPermission sectionResult = default(RoleSetSectionPermission);

                                //fix equals must be where not head.uid 
                                //var rssGroupNestList = rssGroupList.Where(x => !string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));

                                //foreach (var group in rssGroupNestList)
                                foreach (var sectionResult in rssGroupList)
                                {
                                    //sectionResult = rssGroupList.FirstOrDefault(x => string.Equals(head, x.name, StringComparison.InvariantCultureIgnoreCase));


                                    if (sectionResult != null)
                                    {

                                        section.whenData |= sectionResult.whenData;
                                        section.dataCapDetail |= sectionResult.dataCapDetail;
                                        section.dataCapEdit |= sectionResult.dataCapEdit;


                                        section.whenList |= sectionResult.whenList;

                                        section.listCapAdd |= sectionResult.listCapAdd;
                                        section.listCapDetail |= sectionResult.listCapDetail;
                                        section.listCapEdit |= sectionResult.listCapEdit;
                                        section.listCapRemove |= sectionResult.listCapRemove;

                                    }
                                }

                                var sectionType = section.As<TargetSection>();

                                tsList.Add(sectionType);


                            }
                            else
                            {
                                RoleSetSectionPermission section = rssGroupList.First();
                                var sectionType = section.As<TargetSection>();

                                tsList.Add(sectionType);

                            }

                        }


                        if (tsList != null & tsList.Count() > 0)
                        {
                            tsList = tsList.OrderBy(x => x.numLine).ToList();
                            listResult = new TargetSectionSet();
                            foreach (var item in tsList)
                            {
                                listResult.Add(item.name, item);


                            }

                        }

                        //List<TargetSection> tsList = new List<TargetSection>();
                        //List<RoleSetSectionPermission> rssList = new List<RoleSetSectionPermission>();
                        //tsList = new List<TargetSection>();
                        //rssList = new List<RoleSetSectionPermission>();

                        //if (rp.Root.HasSections)
                        //{
                        //    var root = rp.Root;
                        //    var sets = root.SectionSet.ToList().Select(x=> x.Value);
                        //    rssList.AddRange(sets);

                        //}

                        //if (rp.Root.HasGrant)
                        //{
                        //    var grant = rp.Root.Grant;
                        //    var sets = grant.SectionSet.ToList().Select(x => x.Value);
                        //    rssList.AddRange(sets);
                        //}

                        //if (rp.Root.HasGrantNested)
                        //{
                        //    //var grant = rp.Root.Grant;
                        //    //var grantSets = grant.GrantSet.ToList().Select(x => x.Value);

                        //    //if (grantSets != null && grantSets.Count() > 0)
                        //    //{
                        //    //    var nest = rp.Root.Grant;
                        //    //    var sets = grant.SectionSet.ToList().Select(x => x.Value);
                        //    //    rssList.AddRange(sets);

                        //    //}


                        //}
                    }
                }
            }
            //}

            if (result)
                listset = listResult;

            return result;


        }

        public static bool GetPagePermissionsByRosterHandle(out TargetSectionSet model, Guid primaryKey)
        {
            bool result;

            result = GetPagePermissionsByRoster(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the pages for this roster in this app";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }
        #endregion


        /* 
         if (GetManagerDefault(out Guid? managerID, out Tuple<bool, Exception> exceptionInfoForManager))
                                        {
                                            if (SavePlayerAsManagerProject(projectID, managerID.Value))
                                            {
                                                //var flag = true;
                                            }

                                        }

                                        else
                                        {
                                            var errorMessage = "Error at try getting the directive manager default for projects";
                                            var builder = new System.Text.StringBuilder();

                                            HandlerExeption(errorMessage, builder, exceptionInfoForManager);

                                        }
         
         */
    }


    public static class SectionTools 
    {

        public static bool GetRolePermission(out RolePermission model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RolePermission);
            model = defaultValue;
            RolePermission modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@RoleCategoryId", primaryKey))
            {
                commandString = @"
                    select 
                     rp.RoleCategoryID, rp.RoleSetPermissionID
                     from rolepermission rp
                     where rp.RoleCategoryID = @RoleCategoryId
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["RoleCategoryID"]?.ToString(), out Guid rcID);
                Guid.TryParse(reader["RoleSetPermissionID"]?.ToString(), out Guid rspID);

                modelResult = new RolePermission() { RoleCategoryID = rcID, RoleSetPermissionID = rspID };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRolePermissionHandle(out RolePermission model, Guid primaryKey)
        {
            bool result = false;

            result = GetRolePermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role permission for this roletype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRoleSetSectionsPermission(out RoleSetSectionPermissionSet model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetSectionPermissionSet);
            model = defaultValue;
            RoleSetSectionPermissionSet modelResult = new RoleSetSectionPermissionSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@TargetOf", primaryKey))
            {
                commandString = @"
                     select 
                        rspl.Uid, rspl.TargetOf, rspl.withTargetOf,
                        ROW_NUMBER() OVER(ORDER BY rt.numLine ASC) AS RowNumber, rt.NumLine,  
                        rspl.RoleTargetID, rt.name as RoleTargetName, rt.Description as RoleTargetDescription, rt.OrderLine as RoleTargetOrderLine,  
                        rspl.uid RoleSetSectionID, rspl.name RoleSetSectionName,  
                        rspl.whenData, rspl.dataCapDetail, rspl.dataCapEdit,   
                        rspl.whenList, rspl.listCapDetail, rspl.listCapAdd, rspl.listCapRemove, rspl.listCapEdit,  
                        rspl.withTargetOf, rspl.targetOF, rspl.IsRoot, rspl.IsForProject, rspl.IsForProcess, rspl.OrderLine RosterSetSectionOrderLine  
                        from rolepermission rp  
                        inner join rolesetpermission rsp on rsp.uid = rp.rolesetpermissionID and rsp.isForProject = 1  
                        inner join rolesetpermission rspl on rspl.targetOf = rsp.uid and rspl.isForProject = 1  
                        INNER JOIN roleTarget rt on rt.uid = rspl.RoleTargetID and rt.NotVisibleForMenu =0  
                        where rspl.TargetOf = @TargetOf 
                        and rspl.IsRoot = 0 and rsp.IsForProject = 1
                        order by rt.numLine                     
                     
                    "
            };


            result = FetchData(reqInfo, (reader => {


                var target = new RoleSetSectionPermission();

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                target.UId = uId;

                Guid? targetOf = default(Guid?);
                Guid? withTargetOf = default(Guid?);

                var targetOfObject = reader["TargetOf"];
                if (!Convert.IsDBNull(targetOfObject))
                {
                    Guid.TryParse(targetOfObject.ToString(), out Guid tId);
                    targetOf = tId;
                    target.TargetOf = tId;
                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                    target.WithTargetOf = wId;
                }

                int.TryParse(reader["RowNumber"].ToString(), out int rownumber);
                target.rowNumber = rownumber;

                target.name = reader["RoleTargetName"].ToString();
                target.description = "";// reader["RoleTargetDescription"].ToString();
                var testdesc = reader["RoleTargetDescription"];
                if (testdesc != null) target.description = testdesc.ToString();
                bool.TryParse(reader["whenData"].ToString(), out bool whenData);
                target.whenData = whenData;

                bool.TryParse(reader["dataCapDetail"].ToString(), out bool dataCapDetail);
                target.dataCapDetail = dataCapDetail;

                bool.TryParse(reader["dataCapEdit"].ToString(), out bool dataCapEdit);
                target.dataCapEdit = dataCapEdit;

                bool.TryParse(reader["whenList"].ToString(), out bool whenList);
                target.whenList = whenList;

                bool.TryParse(reader["listCapDetail"].ToString(), out bool listCapDetail);
                target.listCapDetail = listCapDetail;

                bool.TryParse(reader["listCapAdd"].ToString(), out bool listCapAdd);
                target.listCapAdd = listCapAdd;

                bool.TryParse(reader["listCapRemove"].ToString(), out bool listCapRemove);
                target.listCapRemove = listCapRemove;

                bool.TryParse(reader["listCapEdit"].ToString(), out bool listCapEdit);
                target.listCapEdit = listCapEdit;

                int.TryParse(reader["RoleTargetOrderLine"].ToString(), out int orderLine);
                target.orderLine = orderLine;

                int.TryParse(reader["NumLine"].ToString(), out int numLine);
                target.numLine = numLine;

                if (!modelResult.ContainsKey(target.UId))
                {
                    modelResult.Add(target.UId, target);
                }


            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRoleSetSectionsPermissionHandle(out RoleSetSectionPermissionSet model, Guid primaryKey)
        {
            bool result;

            result = GetRoleSetSectionsPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role set permission sections for this roletype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRoleSetRootPermission(out RoleSetPermissionRoot model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionRoot);
            model = defaultValue;
            RoleSetPermissionRoot modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf,
                     rsp.Name, rsp.Description,
                     rsp.IsRoot, rsp.IsForProject
                     from RoleSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }


                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionRoot()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRoleSetRootPermissionHandle(out RoleSetPermissionRoot model, Guid primaryKey)
        {
            bool result = false;

            result = GetRoleSetRootPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role set permission root for this roletype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRoleSetGrantPermission(out RoleSetPermissionGrant model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionGrant);
            model = defaultValue;
            RoleSetPermissionGrant modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf, rsp.IsRoot, rsp.IsForProject,
                    rsp.Name, rsp.Description
                     from RoleSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionGrant()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRoleSetGrantPermissionHandle(out RoleSetPermissionGrant model, Guid primaryKey)
        {
            bool result = false;

            result = GetRoleSetGrantPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the role set permission grant for this roletype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRoleSetGrantNestedPermission(out RoleSetPermissionGrant model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionGrant);
            model = defaultValue;
            RoleSetPermissionGrant modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf, rsp.IsRoot, rsp.IsForProject,
                     rsp.Name, rsp.Description
                     from RoleSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionGrant()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRoleSetGrantNestedPermissionHandle(out RoleSetPermissionGrant model, Guid primaryKey, Action<RoleSetPermissionGrant> push)
        {
            bool result;


            result = GetRoleSetGrantNestedPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (result)
            {

                if (model.WithTargetOf != null)
                {
                    bool resultNested = false;

                    resultNested = GetRoleSetGrantNestedPermissionHandle(out RoleSetPermissionGrant modelNested, model.WithTargetOf.Value, push);

                    if (resultNested)
                    {
                        push?.Invoke(modelNested);
                    }
                    else
                    {
                        var errorMessage = "Error at try getting the role set permission grant nested (recursive) for this roletype in this project";
                        var builder = new System.Text.StringBuilder();

                        HandlerExeption(errorMessage, builder, exceptionX);
                    }
                }

                push?.Invoke(model);
            }
            else
            {

                var errorMessage = "Error at try getting the role set permission grant (nested) for this roletype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }


    }

    public static class PageTools 
    {

        public static bool GetRosterPermission(out RolePermission model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RolePermission);
            model = defaultValue;
            RolePermission modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@RosterCategoryId", primaryKey))
            {
                commandString = @"
                    select 
                     rp.RosterCategoryID, rp.RosterSetPermissionID
                     from Rosterpermission rp
                     where rp.RosterCategoryID = @RosterCategoryId
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["RosterCategoryID"]?.ToString(), out Guid rcID);
                Guid.TryParse(reader["RosterSetPermissionID"]?.ToString(), out Guid rspID);

                modelResult = new RolePermission() { RoleCategoryID = rcID, RoleSetPermissionID = rspID };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRosterPermissionHandle(out RolePermission model, Guid primaryKey)
        {
            bool result = false;

            result = GetRosterPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the roster permission for this rostertype in this app";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRosterSetSectionsPermission(out RoleSetSectionPermissionSet model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetSectionPermissionSet);
            model = defaultValue;
            RoleSetSectionPermissionSet modelResult = new RoleSetSectionPermissionSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@TargetOf", primaryKey))
            {
                commandString = $@"
                     select 
                        rspl.Uid, rt.notvisibleformenu,rt.isagrupation, rt.TargetOf parentOf, rt.isroot targetIsRoot,
                        ROW_NUMBER() OVER(ORDER BY rt.orderline ASC) AS RowNumber,rt.route, 
                        rspl.UserTargetID, rt.uid targetID ,rt.name as UserTargetName, rt.Description as UserTargetDescription, rt.OrderLine as UserTargetOrderLine,  
                        rspl.uid RosterSetSectionID, rspl.name RosterSetSectionName,
                        rspl.whenGet whenData, rspl.GetCapOpen dataCapDetail,rspl.GetCapBlocked dataCapEdit, 
                        rspl.whenPost whenList, rspl.PostCapClose listCapDetail, rspl.PostCapBlocked listCapAdd, rspl.PostCapUp listCapRemove, rspl.PostCapDown listCapEdit,                          
                        rspl.withTargetOf, rspl.targetOF, rspl.IsRoot, rspl.IsForProject, rspl.IsForProcess, rspl.OrderLine RosterSetSectionOrderLine  
                        from Rostersetpermission rsp 
                        left join Rostersetpermission rspl on (rspl.targetOf = rsp.uid OR rspl.uid = rsp.uid ) and rspl.isForProject = 1  
                        left JOIN UserTarget rt on rt.uid = rspl.UserTargetID 
                        where rspl.TargetOf = '{primaryKey}'
                        and
                        (
                        (rspl.targetOf = rsp.uid and rspl.IsRoot = 0 ) or (rspl.uid = rsp.uid and rspl.IsRoot = 1 )
                        )
                        and rsp.IsForProject = 1
                                           
                     
                    "
            };

            //rspl.IsRoot = 0 
            //and rt.NotVisibleForMenu = 0
            string commandString = reqInfo.commandString;

            result = FetchData(reqInfo, (reader => {


                var target = new RoleSetSectionPermission();

                bool.TryParse(reader["Isroot"]?.ToString(), out bool isroot);
                target.Isroot = isroot;
                bool.TryParse(reader["TargetIsroot"]?.ToString(), out bool targetIsroot);
                target.isroot = targetIsroot;

                bool.TryParse(reader["isagrupation"]?.ToString(), out bool isagrupation);
                target.isagrupation = isagrupation;
                //
                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                target.UId = uId;

                Guid? targetOf = default(Guid?);
                Guid? withTargetOf = default(Guid?);
                Guid? parentOf = default(Guid?);
                Guid? targetId = default(Guid?);

                bool.TryParse(reader["notvisibleformenu"].ToString(), out bool notvisibleformenu);
                target.notvisibleformenu = notvisibleformenu;

                var targetOfObject = reader["TargetOf"];
                if (!Convert.IsDBNull(targetOfObject))
                {
                    Guid.TryParse(targetOfObject.ToString(), out Guid tId);
                    targetOf = tId;
                    target.TargetOf = tId;
                }

                var targetIdObject = reader["targetId"];
                if (!Convert.IsDBNull(targetOfObject))
                {
                    Guid.TryParse(targetIdObject.ToString(), out Guid utId);
                    targetId = utId;
                    target.targetId = utId;
                }

                var parentOfObject = reader["parentOf"];
                if (!Convert.IsDBNull(parentOfObject))
                {
                    Guid.TryParse(parentOfObject.ToString(), out Guid pId);
                    parentOf = pId;
                    target.parentId = pId;
                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                    target.WithTargetOf = wId;
                }

                int.TryParse(reader["RowNumber"].ToString(), out int rownumber);
                target.rowNumber = rownumber;

                target.name = reader["UserTargetName"].ToString();
                target.description =  reader["UserTargetDescription"]?.ToString();

                target.route = reader["Route"]?.ToString();

                bool.TryParse(reader["whenData"].ToString(), out bool whenData);
                target.whenData = whenData;

                bool.TryParse(reader["dataCapDetail"].ToString(), out bool dataCapDetail);
                target.dataCapDetail = dataCapDetail;

                bool.TryParse(reader["dataCapEdit"].ToString(), out bool dataCapEdit);
                target.dataCapEdit = dataCapEdit;

                bool.TryParse(reader["whenList"].ToString(), out bool whenList);
                target.whenList = whenList;

                bool.TryParse(reader["listCapDetail"].ToString(), out bool listCapDetail);
                target.listCapDetail = listCapDetail;

                bool.TryParse(reader["listCapAdd"].ToString(), out bool listCapAdd);
                target.listCapAdd = listCapAdd;

                bool.TryParse(reader["listCapRemove"].ToString(), out bool listCapRemove);
                target.listCapRemove = listCapRemove;

                bool.TryParse(reader["listCapEdit"].ToString(), out bool listCapEdit);
                target.listCapEdit = listCapEdit;

                int.TryParse(reader["UserTargetOrderLine"].ToString(), out int orderLine);
                target.orderLine = orderLine;

                //int.TryParse(reader["NumLine"].ToString(), out int numLine);
                //target.numLine = numLine;

                if (!modelResult.ContainsKey(target.UId))
                {
                    modelResult.Add(target.UId, target);
                }


            }), out exceptionInfo);

            var hasException = exceptionInfo.Item1 != null && exceptionInfo.Item1.Value;
            result |= !hasException;

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRosterSetSectionsPermissionHandle(out RoleSetSectionPermissionSet model, Guid primaryKey)
        {
            bool result;

            result = GetRosterSetSectionsPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the roster set permission sections for this rostertype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRosterSetRootPermission(out RoleSetPermissionRoot model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionRoot);
            model = defaultValue;
            RoleSetPermissionRoot modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf,
                     rsp.Name, rsp.Description,
                     rsp.IsRoot, rsp.IsForProject
                     from RosterSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }


                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionRoot()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRosterSetRootPermissionHandle(out RoleSetPermissionRoot model, Guid primaryKey)
        {
            bool result = false;

            result = GetRosterSetRootPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the roster set permission root for this rostertype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRosterSetGrantPermission(out RoleSetPermissionGrant model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionGrant);
            model = defaultValue;
            RoleSetPermissionGrant modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf, rsp.IsRoot, rsp.IsForProject,
                    rsp.Name, rsp.Description
                     from RosterSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionGrant()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRosterSetGrantPermissionHandle(out RoleSetPermissionGrant model, Guid primaryKey)
        {
            bool result = false;

            result = GetRosterSetGrantPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (!result)
            {

                var errorMessage = "Error at try getting the roster set permission grant for this rostertype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }

        public static bool GetRosterSetGrantNestedPermission(out RoleSetPermissionGrant model, Guid primaryKey, out Tuple<bool?, Exception> exceptionInfo)
        {
            bool result = false;
            var defaultValue = default(RoleSetPermissionGrant);
            model = defaultValue;
            RoleSetPermissionGrant modelResult = defaultValue;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@UId", primaryKey))
            {
                commandString = @"
                     select 
                     rsp.Uid, rsp.withTargetOf, rsp.IsRoot, rsp.IsForProject,
                     rsp.Name, rsp.Description
                     from RosterSetPermission rsp
                     where rsp.Uid = @UId
                     and rsp.IsRoot = 1 and rsp.IsForProject = 1
                    "
            };


            result = FetchDataFirstOrDefaultBetter(reqInfo, (reader => {

                Guid.TryParse(reader["UId"]?.ToString(), out Guid uId);
                Guid? withTargetOf = default(Guid?);

                string name = reader["name"]?.ToString();
                string description = string.Empty;


                var descriptionObject = reader["description"];
                if (!Convert.IsDBNull(descriptionObject))
                {
                    description = descriptionObject.ToString();

                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }

                var withTargetOfObject = reader["WithTargetOf"];
                if (!Convert.IsDBNull(withTargetOfObject))
                {
                    Guid.TryParse(withTargetOfObject.ToString(), out Guid wId);
                    withTargetOf = wId;
                }
                else
                {
                    var stop = true;

                    if (stop)
                    { }

                }
                modelResult = new RoleSetPermissionGrant()
                {
                    UId = uId,
                    WithTargetOf = withTargetOf,
                    Name = name,
                    Description = description
                };

            }), out exceptionInfo);

            if (result)
                model = modelResult;

            return result;


        }

        public static bool GetRosterSetGrantNestedPermissionHandle(out RoleSetPermissionGrant model, Guid primaryKey, Action<RoleSetPermissionGrant> push)
        {
            bool result;


            result = GetRosterSetGrantNestedPermission(out model, primaryKey, out Tuple<bool?, Exception> exceptionX);
            if (result)
            {

                if (model.WithTargetOf != null)
                {
                    bool resultNested = false;

                    resultNested = GetRosterSetGrantNestedPermissionHandle(out RoleSetPermissionGrant modelNested, model.WithTargetOf.Value, push);

                    if (resultNested)
                    {
                        push?.Invoke(modelNested);
                    }
                    else
                    {
                        var errorMessage = "Error at try getting the roster set permission grant nested (recursive) for this rostertype in this project";
                        var builder = new System.Text.StringBuilder();

                        HandlerExeption(errorMessage, builder, exceptionX);
                    }
                }

                push?.Invoke(model);
            }
            else
            {

                var errorMessage = "Error at try getting the roster set permission grant (nested) for this rostertype in this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
            return result;
        }


    }
}