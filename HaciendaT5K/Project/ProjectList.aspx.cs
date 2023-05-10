using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Eblue.Code;


using static Eblue.Utils.DataTools;

using static Eblue.Utils.WebTools;
using static Eblue.Utils.ProjectTools;
using static Eblue.Utils.SessionTools;
using  Eblue.Utils;
using System.Web.UI.WebControls;


namespace Eblue.Project
{
    public partial class ProjectList : PageBasic
    {
        private const string sqlFilter = "$filter";
        private const string sqlNullContraint = " and 1 = 0 ";
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.TextBoxProjectNumber, this.TextBoxContractNumber, this.TextBoxORCID, this.DropDownPrincipalInvestigator, this.DropdownlistFiscalYear,  buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);


            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;
                //if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsCoordinator))

                    if (EvalIsUserType(UserTypeFlags.UserTypeCoordinator).IsTrue)
                {

                    if (EvalIsUserType(UserTypeFlags.UserTypeOwner).IsTrue)
                    {
                        this.Projects.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            "";


                        Projects.SelectParameters.Clear();
                        //SqlDataSource1.SelectParameters.Add("ProjectPI", "");
                        //SqlDataSource1.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                    else
                    {
                        //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                        //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                        //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                        //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                        //    "FROM Projects AS P " +
                        //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";
                        Projects.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            $"where (exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";


                        Projects.SelectParameters.Clear();
                        //SqlDataSource1.SelectParameters.Add("ProjectPI", "");
                        //SqlDataSource1.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                }
            }

            var deleteCommand = this.Projects.DeleteCommand;
            //var nullcontraint = " and 1 = 0 ";
            bool filterDeleteFor = deleteCommand.Contains(sqlFilter);

            if (filterDeleteFor)
            {
                this.Projects.DeleteCommand = deleteCommand.Replace(sqlFilter, sqlNullContraint);
            }
            else
            { 

            }

            if (!Page.IsPostBack)
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                BindDropDownLists();

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
            

            }


        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);

        }


        #region control's action
        //protected void ButtonNewModel_Click(object sender, EventArgs e)
        //{
        //    AddModel();
        //}
        protected void ButtonSaveNewProject_Click(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                //if (GetWFProccessStarter(out Tuple< Guid?, int> proccess, out Tuple<bool, Exception> exceptionInfo))
                if (GetWFProccessStarter(out Tuple<Guid?, int,string, string> proccess, out Tuple<bool, Exception> exceptionInfo))
                {
                    
                    //Guid projectProccessWayId;
                    //if (SaveNewProject(out Guid projectID, out Guid projectPIID))
                    if (SaveNewProject(out Guid projectID))
                    {


                        var piString = this.DropDownPrincipalInvestigator.SelectedValue;
                        Guid.TryParse(piString, out Guid investigatorId); ;
                        
                        //deprecated for this momment
                        if (SavePlayerPIProject(projectID, investigatorId))
                            {
                                //var flag = true;
                            }

                        if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, proccess.Item1.Value, proccess.Item2))
                        {


                            if (UpdateProjectNewProjectWay(projectID, projectProccessWayId))
                            {

                                //Get the user information
                                var userLogged = Eblue.Utils.SessionTools.UserInfo;

                                if (userLogged != null && (userLogged.IsAdministrator || userLogged.IsDeveloper  || userLogged.IsBudgetOfficer || userLogged.IsProjectOfficer || userLogged.IsAESOfficer ))
                                {
 
                                    //if (GetCoordinatorRoleDefault(out Tuple<Guid?, int?> coordinator, , out Tuple<bool, Exception> exceptionX))
                                    //{
                                    
                                    
                                    //}
                                    //else
                                    //{
                                    //    var errorMessage = "Error at try getting the coordinator default role for projects";
                                    //    var builder = new System.Text.StringBuilder();

                                    //    HandlerExeption(errorMessage, builder, exceptionInfoForManager);

                                    //}

                                    if ( userLogged.IsAdministrator )
                                    {

                                        //save the admin || developer roster as Coordinator of the project
                                        //if (SavePlayerAsCoordinatorProject(projectID, userLogged.RosterId))
                                        //


                                        //save the admin || developer roster as DirectiveManager of the project
                                        if (SavePlayerAsManagerProject(projectID, userLogged.RosterId))
                                        {
                                            //var flag = true;
                                        }

                                        if (GetManagerDefault(out Guid? managerID, out Tuple<bool, Exception> exceptionInfoForManager))
                                        {
                                            if (SavePlayerAsManagerProject(projectID, managerID.Value))
                                            {
                                                //var flag = true;
                                            }

                                        }

                                        else
                                        {
                                            var errorMessage = "Error at try getting the Research Director default for projects";
                                            var builder = new System.Text.StringBuilder();

                                            HandlerExeption(errorMessage, builder, exceptionInfoForManager);

                                        }



                                        //if (GetCoordinatorDefault(out Guid? coordinatorRosterID, out Tuple<bool, Exception> exceptionInfoForCoordinator))
                                        if (GetCoordinatorRoleDefault(out Tuple<Guid?, int?> coordinator, out Tuple<bool, Exception> exceptionX))
                                        {
                                            if (SavePlayerCoordinatorProject(projectID,coordinator.Item1.Value) ) // coordinatorRosterID.Value))
                                            {
                                                //var flag = true;
                                            }


                                            AddFirstStep(proccess, projectID, coordinator, userLogged );

                                        }

                                        else {
                                            var errorMessage = "Error at try getting the coordinator default for projects";
                                            var builder = new System.Text.StringBuilder();

                                            HandlerExeption(errorMessage, builder, exceptionX);

                                        }

                                    }
                                    else
                                    {
                                        if (SavePlayerCoordinatorProject(projectID, userLogged.RosterId))
                                        {
                                            //var flag = true;

                                            AddFirstStep(proccess, projectID, userLogged);
                                        }

                                        #region deprecated
                                        //if (GetCoordinatorRoleDefault(out Tuple<Guid?, int?> coordinator, out Tuple<bool, Exception> exceptionY))
                                        //{
                                        //    if (SavePlayerCoordinatorProject(projectID, coordinator.Item1.Value)) // coordinatorRosterID.Value))
                                        //    {
                                        //        //var flag = true;
                                        //    }


                                        //    AddFirstStep(proccess, projectID, coordinator, userLogged);

                                        //}

                                        //else
                                        //{
                                        //    var errorMessage = "Error at try getting the coordinator default for projects";
                                        //    var builder = new System.Text.StringBuilder();

                                        //    HandlerExeption(errorMessage, builder, exceptionY);

                                        //}
                                        #endregion


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

                                    }
                                }



                                gvModel.DataBind();


                            }
                        
                        
                        
                        }

                        
                    }

                }

                else 
                {
                    var errorMessage = "Error at try getting the starter project process";
                    var builder = new System.Text.StringBuilder();

                    HandlerExeption(errorMessage, builder, exceptionInfo);
                
                }

                


            }

        }
        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(this.TextBoxProjectNumber);
        }

        protected void gvModel_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

            UpdateModelFromGridRow(e);

        }

        #endregion

        #region control's action binds

        protected void AddFirstStep(Tuple<Guid?, int, string, string> proccess, Guid projectID, Eblue.Code.UserLogged userLogged)
        {

            var proccessID = proccess.Item1.Value;
            var statusID = proccess.Item2;
            var proccessDescription = proccess.Item3;
            var statusDescription = proccess.Item4;


            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            //Guid projectID = Guid.Empty;

            //if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = string.Empty;// this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);

            //if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, projectProccess.Way.Item2, projectProccess.Way.Item3))
            //{

            //}

            //signData = projectProccess.NextProccess?.Item2;

            signData = $"{proccessDescription} {statusDescription}";

            if (AddFirstStatusProject(projectID: projectID, rosterID: rosterId,  noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {

                //this.imagenSign.ImageUrl = GetRosterSignature();
                //GenerateStatus(projectID);

            }

        }

        protected void AddFirstStep(Tuple<Guid?, int, string, string> proccess, Guid projectID, Tuple<Guid?, int?> coordinator, Eblue.Code.UserLogged userLogged)
        {

            var proccessID = proccess.Item1.Value;
            var statusID = proccess.Item2;
            var proccessDescription = proccess.Item3;
            var statusDescription = proccess.Item4;

            var playerRoleID = coordinator.Item2.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            //Guid projectID = Guid.Empty;

            //if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = string.Empty ;// this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);

            //if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, projectProccess.Way.Item2, projectProccess.Way.Item3))
            //{

            //}

            //signData = projectProccess.NextProccess?.Item2;

            signData = $"{proccessDescription} {statusDescription}";

            if (AddStatusProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {

                //this.imagenSign.ImageUrl = GetRosterSignature();
                //GenerateStatus(projectID);

            }

        }

        protected void UpdateModelFromGridRow(System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

            int rowCount = e.AffectedRows;
            int editIndex = gvModel.EditIndex;
            object datasourceObject = gvModel.DataSource;

            if (editIndex >= 0)
            {
                int colCount = e.NewValues.Keys.Count;
                if (colCount > 2)
                {

                    var datarow = gvModel.Rows[editIndex];


                    var valueUIDStringOld = e.OldValues[0] as string;
                    Guid.TryParse(valueUIDStringOld, out Guid uid);

                    var name = e.NewValues[2] as string;
                    var description = e.NewValues[3] as string;
                    var route = e.NewValues[4] as string;
                    bool.TryParse(e.NewValues[5].ToString().ToLower(), out bool notvisibleformenu);
                    bool.TryParse(e.NewValues[6].ToString().ToLower(), out bool isroot);
                    bool.TryParse(e.NewValues[7].ToString().ToLower(), out bool isagrupation);

                    var targetofIdString = e.NewValues[8] as string;
                    Guid.TryParse(targetofIdString, out Guid targetofID);

                    var iconClass = e.NewValues[9] as string;

                    Tuple<bool, bool, bool> checks = new Tuple<bool, bool, bool>(notvisibleformenu, isroot, isagrupation);

                    if (UpdateModelToDB(uid, name, description, route, targetofID, checks, iconClass))
                    {


                    }


                }

            }


        }

        protected void Clear()
        {
            BindDropDownLists();
            ClearWebControls(this.TextBoxProjectNumber, this.TextBoxContractNumber, this.TextBoxORCID, this.DropDownPrincipalInvestigator);
        }

        public void BindDropDownLists()
        {

            BindFYList();
            BindPIList();
            //BindPOList();
        }
        public void BindTFList()
        {
            this.DropDownListTypeOfFunds.Items.Clear();


            //this.SqlDataSourceList
        }

        public void BindPOList()
        {
            this.DropdownListPerformingOrganizations.Items.Clear();

            this.SqlDataSourceListPO.DataBind();
            DropdownListPerformingOrganizations.DataBind();
            DropdownListPerformingOrganizations.Items.Insert(0, new ListItem("None", ""));
            DropdownListPerformingOrganizations.Items[0].Selected = true;

        }

        public void BindFYList()
        {
            this.DropDownPrincipalInvestigator.Items.Clear();


            this.SqlDataSourceListPI.DataBind();
            DropDownPrincipalInvestigator.DataBind();
            DropDownPrincipalInvestigator.Items.Insert(0, new ListItem("None", ""));
            DropDownPrincipalInvestigator.Items[0].Selected = true;
        }
            public void BindPIList()
        {

            this.DropdownlistFiscalYear.Items.Clear();


            this.SqlDataSourceListFY.DataBind();
            DropdownlistFiscalYear.DataBind();
            DropdownlistFiscalYear.Items.Insert(0, new ListItem("None", ""));
            DropdownlistFiscalYear.Items[0].Selected = true;

        }

        protected bool SavePlayerPIProject(Guid projectID, Guid rosterID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                /*
                 * $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsDirectiveLeader = 1), '{projectID}' ";
                 */



                cn.Open();
                string insertCommand = $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1), '{projectID}' ";



                string insertCommand2 = $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1), '{projectID}' ";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to register PI to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SavePlayerCoordinatorProject(Guid projectID, Guid rosterID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1), '{projectID}' ";

                string insertCommand2 =
                   $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                   $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                   $"rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1), '{projectID}' ";



                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to register Coodinator to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SavePlayerCoordinatorProject(Guid projectID, Guid rosterID, int coordinatorDefaultRoleId)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' , {coordinatorDefaultRoleId}, '{projectID}' ";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to register Coodinator to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SavePlayerAsCoordinatorProject(Guid projectID, Guid rosterID)
        {

            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.ExecutiveOfficer = 1), '{projectID}' ";

                /*
                $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsInvestigationOfficer = 1), '{projectID}' "; 
                 
                 */


                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to register Player As Coodinator to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SavePlayerAsManagerProject(Guid projectID, Guid rosterID)
        {

            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1), '{projectID}' ";

                //select  * from roles rl inner join RoleCategory rc on 
                //rc.UId = rl.RoleCategoryId where rc.IsResearchDirector = 1 AND rl.RoleName = 'RDR-Decana Auxiliar a/c Investigaciones'



                /* Old Implementation
                 $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsDirectiveManager = 1), '{projectID}' ";
                 
                 */

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                // throw new Exception (""Error while trying to register Player As Directive Manager to the project", ex")
                throw new Exception("Error while trying to register Player As Research Director to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SaveNewProject(out Guid newID)
        {
            bool result;
            newID = Guid.NewGuid();
            Guid.TryParse(DropDownPrincipalInvestigator.SelectedValue, out Guid projectPIID);
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            //, out Guid projectPIID
            try
            {
                cn.Open();
                string InsertNewProject = 
                    "INSERT INTO Projects (" +
                    "ProjectID, ProjectNumber, ContractNumber, ProjectPI, DateRegister, LastUpdate, FiscalYearID, WFSID, ORCID, ProjectTitle, ProgramAreaID, DepartmentID, CommId, SubStationID, POrganizationsID, FundTypeID)" +
                    "VALUES (@ProjectID, @ProjectNumber, @ContractNumber, @ProjectPI, @DateRegister, @LastUpdate, @FiscalYearID, @WFSID, @ORCID, @ProjectTitle, @ProgramAreaID, @DepartmentID, @CommId, @SubStationID, @POrganizationsId, @FundTypeID)";

                
                       
                    
                SqlCommand cmd = new SqlCommand(InsertNewProject, cn);
                cmd.Parameters.AddWithValue("@ProjectID", newID);
                cmd.Parameters.AddWithValue("@ProjectNumber", TextBoxProjectNumber.Text);
                cmd.Parameters.AddWithValue("@ContractNumber", TextBoxContractNumber.Text);
                cmd.Parameters.AddWithValue("@ProjectPI", projectPIID);
                cmd.Parameters.AddWithValue("@DateRegister", DateTime.Now);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FiscalYearID", DropdownlistFiscalYear.SelectedValue);
                cmd.Parameters.AddWithValue("@WFSID", "1");
                cmd.Parameters.AddWithValue("@ORCID", TextBoxORCID.Text);

                // committing more data to the database
                cmd.Parameters.AddWithValue("@ProjectTitle", TextBoxProjectShortTitle.Text);
                cmd.Parameters.AddWithValue("@ProgramAreaID", DropDownListProgramaticArea.SelectedValue);
                cmd.Parameters.AddWithValue("@DepartmentID", DropDownListDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@CommId", DropDownListCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@SubStationID", DropDownListSubstation.SelectedValue);

                //cmd.Parameters.AddWithValue("@Objectives", TextBoxProjectObjective.Text);


                // committing Type of Funds and Performing Organizations
                cmd.Parameters.AddWithValue("@FundTypeID", DropDownListTypeOfFunds.SelectedValue);
                cmd.Parameters.AddWithValue("@POrganizationsId", DropdownListPerformingOrganizations.SelectedValue);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                



                TextBoxProjectNumber.Text = "";
                TextBoxContractNumber.Text = "";
                TextBoxORCID.Text = "";
                DropDownPrincipalInvestigator.SelectedIndex = 0;
                DropdownlistFiscalYear.SelectedIndex = 0;

                TextBoxProjectShortTitle.Text = "";
                DropDownListProgramaticArea.SelectedIndex = 0;
                DropDownListDepartment.SelectedIndex = 0;
                DropDownListCommodity.SelectedIndex = 0;
                DropDownListSubstation.SelectedIndex = 0;

                //TextBoxProjectObjective.Text = "";

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to create the project", ex);
            }
            finally
            {

            }

            return result;

        }

        //protected bool SaveNewProjectWay(out Guid projectProccessWayId, Guid projectID, Guid proccessId, int StatusID)
        //{
        //    bool result;
        //    projectProccessWayId = Guid.NewGuid();
        //    Guid.TryParse(DropDownPrincipalInvestigator.SelectedValue, out Guid projectPIID);
        //    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
        //    //, out Guid projectPIID
        //    try
        //    {
        //        cn.Open();
        //        string commandString = "insert into ProcessProjectWay(UID, ProjectId, ProcessId, EstatusId) values (@UID, @ProjectId, @ProcessId, @EstatusId)";
               
        //        SqlCommand cmd = new SqlCommand(commandString, cn);
        //        cmd.Parameters.AddWithValue("@UID", projectProccessWayId);
        //        cmd.Parameters.AddWithValue("@ProjectId", projectID);
        //        cmd.Parameters.AddWithValue("@ProcessId", proccessId);
        //        cmd.Parameters.AddWithValue("@EstatusId", StatusID);

        //        cmd.ExecuteNonQuery();
        //        cmd.Dispose();
        //        cn.Close();

               
        //        result = true;

        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception("Error while trying to create the project' way", ex);
        //    }
        //    finally
        //    {

        //    }

        //    return result;

        //}

        
        protected bool SaveNewProject(out Guid newID, out Guid projectPIID)
        {
            bool result;
            newID = Guid.NewGuid();
            Guid.TryParse(DropDownPrincipalInvestigator.SelectedValue, out projectPIID);
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string InsertNewProject = "INSERT INTO Projects (ProjectID, ProjectNumber, ContractNumber, ProjectPI, DateRegister, LastUpdate, FiscalYearID, WFSID, ORCID) VALUES (@ProjectID, @ProjectNumber, @ContractNumber, @ProjectPI, @DateRegister, @LastUpdate, @FiscalYearID, @WFSID, @ORCID)";

                SqlCommand cmd = new SqlCommand(InsertNewProject, cn);
                cmd.Parameters.AddWithValue("@ProjectID", newID);
                cmd.Parameters.AddWithValue("@ProjectNumber", TextBoxProjectNumber.Text);
                cmd.Parameters.AddWithValue("@ContractNumber", TextBoxContractNumber.Text);
                cmd.Parameters.AddWithValue("@ProjectPI", projectPIID);
                cmd.Parameters.AddWithValue("@DateRegister", DateTime.Now);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FiscalYearID", DropdownlistFiscalYear.SelectedValue);
                cmd.Parameters.AddWithValue("@WFSID", "1");
                cmd.Parameters.AddWithValue("@ORCID", TextBoxORCID.Text);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                TextBoxProjectNumber.Text = "";
                TextBoxContractNumber.Text = "";
                TextBoxORCID.Text = "";
                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to create the project", ex);
            }
            finally
            {

            }

            return result;

        }
        #endregion


        #region data model actions

        protected bool GetCoordinatorDefault(out Guid? uid, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            uid = default(Guid?);
            Guid? resultUId = default(Guid?);            

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceCoordinatorDefault.SelectCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, (reader => {

                Guid.TryParse(reader["Uid"]?.ToString(), out Guid guid);
                resultUId = guid;
            }), out exceptionInfo);

            if (result)
                uid = resultUId;

            return result;

        }

        protected bool GetCoordinatorRoleDefault(out Tuple<Guid?, int?> coordinator, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            coordinator = default(Tuple<Guid?, int?>);
            Tuple<Guid?, int?> resultModel = default(Tuple<Guid?, int?>);

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceCoordinatorDefault.SelectCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, (reader => {

                Guid.TryParse(reader["Uid"]?.ToString(), out Guid rosterId);
                int.TryParse(reader["DefaultRoleID"]?.ToString(), out int defaultRoleID);

                resultModel = new Tuple<Guid?, int?>(rosterId, defaultRoleID);
                //resultUId = guid;
            }), out exceptionInfo);

            if (result)
                coordinator = resultModel;

            return result;

        }

        protected bool GetManagerDefault(out Guid? uid, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            uid = default(Guid?);
            Guid? resultUId = default(Guid?);

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceManagerDefault.SelectCommand
            };

           

            result = FetchDataFirstOrDefault(reqInfo, (reader => {

                Guid.TryParse(reader["Uid"]?.ToString(), out Guid guid);
                resultUId = guid;
            }), out exceptionInfo);

            if (result)
                uid = resultUId;

            return result;

        }

        protected bool GetManagerRoleDefault(out int? rid, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            rid = default(int?);
            int? resultUId = default(int?);

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceManagerDefault.SelectCommand
            };            

            result = FetchDataFirstOrDefault(reqInfo, (reader => {

                int.TryParse(reader["Uid"]?.ToString(), out int iid);
                resultUId = iid;
            }), out exceptionInfo);

            if (result)
                rid = resultUId;

            return result;

        }

        protected bool GetWFProccessStarter(out Tuple<Guid?, int> proccess, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            proccess = default(Tuple<Guid?, int>);
            Tuple<Guid?, int> resultProccess = default(Tuple<Guid?, int>);

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceProccessStarter.SelectCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, (reader => {
                //ProjectStatusName
                Guid.TryParse(reader["Uid"]?.ToString(), out Guid guid);
                int.TryParse(reader["EstatusID"]?.ToString(), out int statusID);
                resultProccess = new Tuple<Guid?, int>(guid, statusID) ;
            }), out exceptionInfo);

            if (result)
                proccess = resultProccess;

            return result;

        }

        protected bool GetWFProccessStarter(out Tuple<Guid?, int, string, string> proccess, out Tuple<bool, Exception> exceptionInfo)
        {
            bool result = false;
            proccess = default(Tuple<Guid?, int, string, string>);
            Tuple<Guid?, int, string, string> resultProccess = default(Tuple<Guid?, int, string, string>);

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo()
            {
                commandString = this.SqlDataSourceProccessStarter.SelectCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, (reader => {
                //ProjectStatusName
                Guid.TryParse(reader["Uid"]?.ToString(), out Guid guid);
                int.TryParse(reader["EstatusID"]?.ToString(), out int statusID);

                var description = reader["Description"]?.ToString();
                var statusName = reader["ProjectStatusName"]?.ToString();
                resultProccess = new Tuple<Guid?, int, string, string>(guid, statusID, description, statusName);
            }), out exceptionInfo);

            if (result)
                proccess = resultProccess;

            return result;

        }

        protected bool AddModelToDB(string name, string description, string route, Guid? targetOfID, Tuple<bool, bool, bool> checks, string iconClass)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into UserTarget (uid, name, description, route, targetOf, " +
                    $"NotVisibleForMenu, isroot, isagrupation, iconclass) " +
                    $"values (@uid,@name,@description,@route, " +
                    $"iif(@targetOf = '', null, @targetOf ), @NotVisibleForMenu, @isroot, @isagrupation, @iconClass) ";

                //$"values (@uid, @name, @)" +
                //$"select newid(), '{name}', '{description}', {priority}, " +
                //$"{Convert.ToInt32(whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                //$"{Convert.ToInt32(whichCanDo.Item1)},{Convert.ToInt32(whichCanDo.Item2)},{Convert.ToInt32(whichCanDo.Item3)},{Convert.ToInt32(whichCanDo.Item4)} ";

                var uid = Guid.NewGuid();
                string targetOf = targetOfID == null ? string.Empty : targetOfID.ToString();


                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@route", route);
                cmd.Parameters.AddWithValue("@targetOf", targetOf);
                cmd.Parameters.AddWithValue("@NotVisibleForMenu", checks.Item1);
                cmd.Parameters.AddWithValue("@isroot", checks.Item2);
                cmd.Parameters.AddWithValue("@isagrupation", checks.Item3);
                cmd.Parameters.AddWithValue("@iconClass", iconClass);




                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add user target page", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool UpdateModelToDB(Guid uid, string name, string description, string route, Guid? targetOfID, Tuple<bool, bool, bool> checks, string iconClass)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string command =
                    $"update UserTarget set name = @name, description = @description, route = @route, targetOf = @targetOf, " +
                    $"NotVisibleForMenu = @NotVisibleForMenu, isroot = @isroot, isagrupation = @isagrupation, iconClass = @iconClass " +
                    " where uid = @uid ";

                string targetOf = targetOfID == null ? string.Empty : targetOfID.ToString();


                SqlCommand cmd = new SqlCommand(command, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@route", route);
                cmd.Parameters.AddWithValue("@targetOf", targetOf);
                //cmd.Parameters.AddWithValue("@NotVisibleForMenu", isNotVisibleForMenu);
                cmd.Parameters.AddWithValue("@NotVisibleForMenu", checks.Item1);
                cmd.Parameters.AddWithValue("@isroot", checks.Item2);
                cmd.Parameters.AddWithValue("@isagrupation", checks.Item3);
                cmd.Parameters.AddWithValue("@iconClass", iconClass);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add user target page", ex);
            }
            finally
            {

            }

            return result;

        }

        #endregion

        
    }
}