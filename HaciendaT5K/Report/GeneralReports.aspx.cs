using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Eblue.Code;

using static Eblue.Utils.ReportTools;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.ReportServices;

using static Eblue.Utils.WebTools;
using static Eblue.Utils.ProjectTools;
using static Eblue.Utils.SessionTools;
using  Eblue.Utils;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Utilities;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Eblue.Reports
{
    using framework.scaffs;
    public partial class GeneralReports : PageBasic
    {
        private const string sqlFilter = "$filter";
        private const string sqlNullContraint = " and 1 = 0 ";
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.TextBoxProjectNumber, this.TextBoxContractNumber, this.TextBoxORCID,  buttonNewModel, buttonHideModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);


            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;
                //if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsCoordinator))

                this.qrysource.SelectCommand = "select * from reportsource";
                this.qrysource.SelectParameters.Clear();
                #region MyRegion
                /*
                 * 
                 * 
                 if (EvalIsUserType(UserTypeFlags.UserTypeCoordinator).IsTrue)
                {

                    if (EvalIsUserType(UserTypeFlags.UserTypeOwner).IsTrue)
                    {
                        this.qrysource.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            "";


                        qrysource.SelectParameters.Clear();
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
                        qrysource.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            $"where (exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";


                        qrysource.SelectParameters.Clear();
                        //SqlDataSource1.SelectParameters.Add("ProjectPI", "");
                        //SqlDataSource1.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                }
                 * */
                #endregion


            }

            //var deleteCommand = this.qrysource.DeleteCommand;
            ////var nullcontraint = " and 1 = 0 ";
            //bool filterDeleteFor = deleteCommand.Contains(sqlFilter);

            //if (filterDeleteFor)
            //{
            //    this.qrysource.DeleteCommand = deleteCommand.Replace(sqlFilter, sqlNullContraint);
            //}
            //else
            //{

            //}

            if (!Page.IsPostBack)
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //BindDropDownLists();
                setOnIsNotPostback();

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
            

            }


        }

        public void setOnIsPostpack() { }
        public void setOnIsNotPostback() {
            setDefaultVarEngine();
        }

        public void setDefaultVarEngine() {

            //TODO for test
            bool flag = HasJsonText;

            HasJsonText = false;

            var txtengine = VarEngineJsonTemplate;

            this.TextBoxORCID.Text = txtengine;
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

                if (SaveNewReport(out Guid guid))
                {
                    gvModel.DataBind();
                    this.pivotpanel.Visible = false;
                }

                //if (GetWFProccessStarter(out Tuple< Guid?, int> proccess, out Tuple<bool, Exception> exceptionInfo))
                /*
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

                                var userLogged = Eblue.Utils.SessionTools.UserInfo;

                                if (userLogged != null && (userLogged.IsAdmin || userLogged.IsDeveloper || userLogged.IsCoordinator))
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

                                    if (userLogged.IsAdmin || userLogged.IsDeveloper)
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
                                            var errorMessage = "Error at try getting the directive manager default for projects";
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
                 */






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
                cn.Open();
                string insertCommand =
                    $"insert into playerProject (uid, rosterid, roleid, projectid) " +
                    $"select newid(), '{rosterID}' ,(select top 1 rl.RoleID from roles rl inner join RoleCategory rc on " +
                    $"rc.UId = rl.RoleCategoryId where rc.IsDirectiveLeader = 1), '{projectID}' ";

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
                    $"rc.UId = rl.RoleCategoryId where rc.IsInvestigationOfficer = 1), '{projectID}' ";

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
                    $"rc.UId = rl.RoleCategoryId where rc.IsInvestigationOfficer = 1), '{projectID}' ";

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
                    $"rc.UId = rl.RoleCategoryId where rc.IsDirectiveManager = 1), '{projectID}' ";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to register Player As Directive Manager to the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool SaveNewReport(out Guid newID)
        {
            bool result;
            newID = Guid.NewGuid();

            /*
             
            TextBoxProjectNumber
name

TextBoxContractNumber
caption

TextBoxORCID


            

             * */

            /*
				drop  table reportsource 
create table reportsource 
(
 Uid uniqueidentifier primary key,
 name nvarchar(128) not null unique,
 caption nvarchar(128),
 varjson nvarchar(max),   --like  var jtype  =  {help:{tooltip:"", purpose:"", help:"", info:""}};
 creationdate datetime default(getdate()),
 lastupdate datetime default(getdate()),
 identityId int identity(1,1) not null,
 declaration nvarchar(max) not null,
 setting nvarchar(max) not null,
 execution nvarchar(max) not null,
 split nvarchar(9) null default(';'),
 xColumns nvarchar(max), 
 yColumns nvarchar(max),
 zColumns nvarchar(max),
 varengine nvarchar(max)
)
*/

            var name = TextBoxProjectNumber.Text;
            var caption = TextBoxContractNumber.Text;
            var varengine = TextBoxORCID.Text;

            JObject var = JObject.Parse(varengine);   //Newtonsoft.Json.JsonConvert.DeserializeObject(varengine);
            var json = var["json"];
            var varjson = json["varjson"];
            var declaration = json["declaration"];
            var setting = json["setting"];
            var execution = json["execution"];
            var split = json["split"];
            var xcolumns = json["xcolumns"];
            var ycolumns = json["ycolumns"];
            var zcolumns = json["zcolumns"];

            var defaulttemplateToken = json["defaulttemplate"];
            var defaultreportToken = json["defaultreport"];

            bool defaulttemplate = (bool)defaulttemplateToken;
            bool defaultreport = (bool)defaultreportToken;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            //, out Guid projectPIID
            try
            {
                cn.Open();
                string InsertNewProject = @"INSERT INTO 
reportsource (Uid, Name, Caption, varjson, declaration, setting, execution, split, xcolumns, ycolumns, zcolumns, varengine, defaultreport, defaulttemplate) 
VALUES (@Uid, @Name, @Caption, @varjson, @declaration, @setting, @execution, @split, @xcolumns, @ycolumns, @zcolumns, @varengine, @defaultreport, @defaulttemplate)";

                SqlCommand cmd = new SqlCommand(InsertNewProject, cn);
                cmd.Parameters.AddWithValue("@Uid", newID);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@caption", caption);
                cmd.Parameters.AddWithValue("@varjson", varjson.ToString());
                cmd.Parameters.AddWithValue("@declaration", declaration.ToString());
                cmd.Parameters.AddWithValue("@setting", setting.ToString());
                cmd.Parameters.AddWithValue("@execution", execution.ToString());
                cmd.Parameters.AddWithValue("@split", caption);
                cmd.Parameters.AddWithValue("@xcolumns", xcolumns.ToString());
                cmd.Parameters.AddWithValue("@ycolumns", ycolumns.ToString());
                cmd.Parameters.AddWithValue("@zcolumns", zcolumns.ToString());
                cmd.Parameters.AddWithValue("@varengine", varengine.ToString());
                cmd.Parameters.AddWithValue("@defaultreport",defaultreport);
                cmd.Parameters.AddWithValue("@defaulttemplate", defaulttemplate);
                //cmd.Parameters.AddWithValue("@ProjectNumber", TextBoxProjectNumber.Text);
                //cmd.Parameters.AddWithValue("@ContractNumber", TextBoxContractNumber.Text);
                ////cmd.Parameters.AddWithValue("@ProjectPI", projectPIID);
                //cmd.Parameters.AddWithValue("@DateRegister", DateTime.Now);
                //cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@FiscalYearID", DropdownlistFiscalYear.SelectedValue);
                //cmd.Parameters.AddWithValue("@WFSID", "1");
                //cmd.Parameters.AddWithValue("@ORCID", TextBoxORCID.Text);

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
                throw new Exception("Error while trying to create the report", ex);
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

        protected void gvModel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmdArgument = e.CommandArgument;
            if (e.CommandName.CompareTo("Select") == 0) // ||        e.CommandName.CompareTo("DecreasePrice") == 0)
            {
                var keyvalues = gvModel.DataKeys;
                int.TryParse(cmdArgument.ToString(), out int keyindex);
                // The Increase Price or Decrease Price Button has been clicked
                // Determine the ID of the product whose price was adjusted
                var keyentry = keyvalues[keyindex];
                var keyvalue = keyentry.Value;// keyvalues[keyindex];

                if (keyvalue is Guid  varguid)
                {
                    GenerateReport(keyguid: varguid);
                }
                else if (keyvalue is string varstring) {
                    GenerateReport(keystring: varstring);
                }



                //int productID =
                //    (int)SuppliersProducts.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                // Determine how much to adjust the price
                //decimal percentageAdjust;
                //if (e.CommandName.CompareTo("IncreasePrice") == 0)
                //    percentageAdjust = 1.1M;
                //else
                //    percentageAdjust = 0.9M;

                //// Adjust the price
                //ProductsBLL productInfo = new ProductsBLL();
                //productInfo.UpdateProduct(percentageAdjust, productID);
            }
        }

        private void GenerateReport(string keystring)
        {

            if (Guid.TryParse(keystring, out Guid keyid))
            {
                GenerateReport(keyguid:keyid);
            
            }
        
        }
        private void GenerateReport(Guid keyguid)
        {
            bool pivotvisibled = false;
            bool hasjsonText = false;

            #region body statement (method instance)
            /*
             * 
             bool result ;

            result = GetProjectRoleTypeHandle(out ProjectRoleType model, roleCategoryID);
            
            if (result)
            {
                this.ProjectRoleBlop = model;
            }
            else
            {
                var stop = true;

                if (stop)
                { }

            }
             * 
             * */
            #endregion

            if (GetReportResultSetHandle(out Rtsql rpt, keyguid)) {
                JsonPivot pivot = new JsonPivot(rpt);
                this.jsonText = pivot.GetJson();
                var items = rpt.items;
                pivotvisibled = true;
                hasjsonText = true;
            }

            
            this.HasJsonText = hasjsonText;
            this.pivotpanel.Visible = pivotvisibled;
            
        }

        protected void buttonHideModel_Click(object sender, EventArgs e)
        {
            this.pivotpanel.Visible = false;
            this.HasJsonText = false;
        }

        public string VarEngineJsonTemplate
        {
            get {
               return @"{
  'json':{
  'varjson':{ 'data':'unset'},
  'declaration':'unset',
  'setting':'unset',
  'execution':'unset',
  'split':'unset',
  'xcolumns':'unset',
  'ycolumns':'unset',
  'zcolumns':'unset',
  'defaulttemplate':false,
  'defaultreport':false,
  }
        }
".Replace("'", "\"");
            }
        
        }

        //public bool? 
        public bool HasJsonText
        {
            get =>this.ViewState.getvalue<bool>("HasJsonText");


            set => this.ViewState.setvalue<bool>("HasJsonText", value);
        }

        public string jsonText
        {
            get {
                string result = string.Empty;
                object value = this.ViewState["jsonText"] ?? result;

                if (value is string var)
                {
                    result = var;
                }
                return result;
            }

            set {
                this.ViewState["jsonText"] = value;
            }        
        }

        protected void buttonGetterModel_Click(object sender, EventArgs e)
        {
            //bool.TryParse
            //
            //TODO GETTING THE DEFAULTTEMPLATE FROM reportsource 
            //like var json = getJsonFrom(txtvarengine||this.TextBoxORCID)
            //like bool? flag = JToken<ext>.ToField<Fbool>() where exp:struct, ASoperationable<class,struct>: ASconvertible<struct>,AScasteable<struct>
            //like setDefaultVarEngine(flag);
            setDefaultVarEngine();
        }
    }
}


namespace framework {
    using System;

    namespace scaffs {
        using System.Web.UI;
        using bags = StateBag;
        public static class StateBagScaffs
        {

            /*
             *
             *
             get
            {
                bool result = default(bool);
                object value = this.ViewState["HasJsonText"] ?? result;

                if (value is bool var)
                {
                    result = var;
                }
                return result;
            }

            set
            {
                this.ViewState["HasJsonText"] = value;
            }
             *
             * */

            public static exp getvalue<exp>(this StateBag target, string key) 
                //where exp: new() 
            {
                exp res = default(exp);

                try
                {
                    object data = target[key];
                    if (data is exp value)
                    {
                        res = value;
                    }                   

                }

                catch (ArgumentException error)
                {
                    target[key] = res;

                }
                catch (Exception error)
                {

                    throw new Exception($"error at getvalue<exp:{typeof(exp).Name}> handler from ");
                }
                
                return res;
            }

            //public static void setvalue<exp>(this StateBag target, string key, exp value) 
            public static void setvalue<exp>(this StateBag target, string key, object data)
            {
                if (data is exp value) {
                    target[key] = value;
                }
                
            }


        }
    }

}
namespace montanosoft {
    using System.Threading.Tasks;
    using datetime = DateTime;
    using guid = Guid;
    using systemFlags = FlagsAttribute;
    using enumgs = FlagsAttribute;
    using static integers;
    using task = System.Threading.Tasks.Task;

    public delegate @operator message<exp, dim>(@params @pars = null)
        where exp: @operator
        where dim: @params
        ;

    // [enumgs] public enum bits : long {@default = longmin,  @bits=0,
    //  (9x7)+1 ; 63+1
    // }
    //public interface mark       //used for new()struct
    // code, 
    //   exact<decimal>, aproximate<double
    //   int
    //   byte
    //  
    // flag  
    // 
    // 
    //
    //public interface mold       //used for new()class,
    //public class reference
    //public class 
    //public class @object { }   

    public class function  : Task{


        public function(): base( action: ()=> { } )
        {

        }
    }
    public class blank: function { }
    public class call: blank {
        void doit<exp>() { }
    }
    public class write: blank {
        void doit<exp>(exp input) { }
    }


    public class empty : function { }
    public class exec: empty {
        dim returnit<exp,dim>() { return default(dim); }

    }
    public class read: empty {
        dim returnit<exp, dim>(exp input) { return default(dim); }

    }



    public class message {
    
    }
    public class @operator: read {

        public virtual string name { get; set; }
    }

    public class mathoperator : @operator
    {
        private aritmethic args;
        private field<KeyValuePair<decimal, double>> data;
        private Func<field<KeyValuePair<decimal, double>>, field<KeyValuePair<decimal, double>>, field<decimal>>serve;
        public mathoperator()
        {
            //serve = (a, b) => { return new field<decimal>(a.code.Key * b.code.Key); };
        }
        public field<decimal> respond()
        {
            return serve(args.left, args.rigth);
        }

    }

    public class mathmultiply: @operator
    {
        private aritmethic args;
        private field<KeyValuePair<decimal, double>> data;
        private Func< field<KeyValuePair<decimal, double>>, field<KeyValuePair<decimal, double>>, field<decimal>>
        
          serve;
        public mathmultiply()
        {
            serve = (a, b) => { return new field<decimal>(a.code.Key * b.code.Key); } ;
        }
        public field<decimal> respond() {
            return serve(args.left, args.rigth);
        }

    }

    public class @params: read {}

    public class aritmethic: @params {
        public field<KeyValuePair<decimal, double>> left { get; set; }
        public field<KeyValuePair<decimal, double>> rigth { get; set; }
    }

    public interface facet<ForInterface> { }

    public interface react<ForEvent> {
        event message<@operator, @params> reactly;
    }
    public class properties<ForClass>
    {

    }
    public class broads<ForEvent> { 
       public event message<@operator, @params> broadly;
    }

    public class rutine<ForDelegate> {
        public @operator methody<exp, dim>(@params pars) {
            return null;
        }

    }

    public class method<ForDelegate> {
        //create all delegates than can be covariance with delegate @operator message<exp,dim>(@params pars)
        //public delegate mathoperator mathdelegateHandler<exp, dim>(aritmethic pars) where exp : @operator where dim : @params;
        public delegate mathmultiply multiplyhandler<exp, dim>(aritmethic pars) where exp : @operator where dim : @params;
        //public delegate mathsubstract multiplyhandler<exp, dim>(aritmethic pars) where exp : @operator where dim : @params;
        public message<@operator, @params> invoke { get; set; }
    }

    public struct flaggers<ForEnum> where ForEnum:struct, Enum { }

    public struct fields<ForStruct> where ForStruct:struct { }

    public class indices<ForBlop> { }

    public interface ASanswer { }
    public interface ASblankanswer : ASanswer { }
    public interface ASdemmandanswer : ASanswer { }
    public interface ASmodelanswer : ASanswer { }
    public interface AScodeanswer : ASanswer { }
    public interface AShandanswer : ASanswer { }


    public delegate void eventhandler<OBject, EVentargs>(OBject sender, EVentargs e);

    /// <summary>
    /// used by event methods in host<mouseinput from signalinput like keyword> for client or server 
    /// </summary>
    /// <typeparam name="OBject"></typeparam>
    /// <typeparam name="EVentargs"></typeparam>
    /// <typeparam name="clickEVentargs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void eventhandlerClick<OBject, EVentargs, clickEVentargs>(OBject sender, clickEVentargs e);
    /// <summary>
    /// used by event methods in host<keyinput from stringinput like keyword> for client or server 
    /// </summary>
    /// <typeparam name="OBject"></typeparam>
    /// <typeparam name="EVentargs"></typeparam>
    /// <typeparam name="pressEVentargs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void eventhandlerPress<OBject, EVentargs, pressEVentargs>(OBject sender, pressEVentargs e);
    /// <summary>
    /// used by event methods in client like   animation --> motionEventargs  , movement --> respondEventargs{over,} 
    /// </summary>
    /// <typeparam name="OBject"></typeparam>
    /// <typeparam name="EVentargs"></typeparam>
    /// <typeparam name="effectEVentargs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void eventhandlerEffect<OBject, EVentargs, effectEVentargs>(OBject sender, effectEVentargs e);
    /// <summary>
    /// used by data methods in server or client
    /// </summary>
    /// <typeparam name="OBject"></typeparam>
    /// <typeparam name="EVentargs"></typeparam>
    /// <typeparam name="trigerEVentargs"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void eventhandlerTriger<OBject, EVentargs, trigerEVentargs>(OBject sender, trigerEVentargs e);

    /// <summary>
    /// used by reaction -> events
    /// </summary>
    /// <typeparam name="exp"></typeparam>
    /// <returns></returns>
    public delegate blankAnswer callmethod<exp, arg>(blankDemmand dmd) where exp : blankAnswer where arg : blankDemmand;
    public delegate blankAnswer callsurvey<exp, arg>(surveyDemmand dmd) where exp : blankAnswer where arg : blankDemmand;
    public delegate demmandAnswer execmethod<exp, arg>(blankDemmand dmd) where exp : demmandAnswer where arg : blankDemmand;
    public delegate demmandAnswer execsurvey<exp, arg>(surveyDemmand dmd) where exp : demmandAnswer where arg : blankDemmand;

    public delegate answer routine<exp, arg>(demmand dmd)
        where exp : answer
    where arg : demmand;




    public partial class target { }   //used like object
    public partial class argument { }  //useed like evenargs
    public partial class demmand {
        public virtual target sender { get; set; }
        public virtual argument e { get; set; }

        public var targetAs<var>() where var: target, new() {
            var exp = new var();
            bool flag = sender == null;
            if (flag) return exp;
            
            var dim = sender as var;
            var res = dim ?? exp;
            return res;
        }

        public var argumentAs<var>() where var : argument, new()
        {
            var exp = new var();
            bool flag = sender == null;
            if (flag) return exp;

            var dim = sender as var;
            var res = dim ?? exp;
            return res;
        }

    }

    public class blankDemmand : demmand { }
    public class surveyDemmand : demmand { }

    public class modelDemmand : surveyDemmand { }
    public class codeDemmand : surveyDemmand { }

    public class targetDemmand { }
    public class argumentDemmand { }


    public interface @default{}

    public interface @new { }
    public interface @new<exp> { }
    public interface @class { }
    public interface @class<exp> { }

    public interface @delegate { }
    public interface @delegate<exp> { }

    public interface @interface { }
    public interface @interface<exp> { }

    public interface @event { }
    public interface @event<exp,arg> {
    
    
    }
    public interface @event<exp> { }

    public interface reaction { 
        
    }

    public interface click {
        event routine<clickAnswer, clickDemmand> onclick;
    
    }
    public interface press {
        event routine<pressAnswer, pressDemmand> onpress;
    }
    public interface effect {
    
    }
    public interface comver {
        event routine<comverAnswer, comverDemmand> oncomver;
    }

    public class clickAnswer : codeAnswer { }

    public class clickDemmand : codeDemmand {
       
    
    }

    public class pressAnswer : codeAnswer { }

    public class pressDemmand : codeDemmand { }

    public class comverAnswer : modelAnswer { }

    public class comverDemmand : modelDemmand { }

    //routine

    //method


    //all they are interfaces 
    //new
    //new<
    //event(delegate )  implicit operator delegate(
    //delegate      implicit operator event(delegate 
    //delegate<exp
    //class         --> abstract
    //class<exp>    --> @new  where exp: @new
    //interface     --> abstract
    //interface<exp>  --> abstract where exp:
    //event      --> @interface
    //event<exp,arg>  --> where arg: @delegate
    //event<exp>  --> new<exp>, event<exp, 
    //@object

    //class variant : answer {} 
    //class blankAnswer: variant {}
    //class demmandAnswer : variant {}

    public class answer : task
    {
        //public property<result> data { get; set; }
        public answer()
            : base(action: () => { }, creationOptions: TaskCreationOptions.None)
        {
        }
        //public answer(): base()
        //{

        //}
        //result data;
    }   //inherits from task 
    public class result { }   // used with answer<result> for 

    public class blankAnswer : answer, ASblankanswer { }   //used for referenceType   task<>
    public class demmandAnswer : answer, ASdemmandanswer { }   //used for referenceType   task<>
    public class modelAnswer : demmandAnswer, ASmodelanswer { }   //used for referenceType   task<>
    public class codeAnswer : demmandAnswer, AScodeanswer { }    //used for valueType        valuetask<>
    public class handAnswer : demmandAnswer, AShandanswer { }    //used for pointType          valuetask<,>

    public class lambda { }  //like JS function   call<void>, exec<object> or call<args>, or exec<args>
    public interface ASvaluetype { }
    public interface ASvaluetype<exp> where exp:struct , ASvaluetype, ASconvertible {
        field<exp> readvalue { get; }
        field<exp> writevalue { set; }
    }
    public interface ASreferencetype { }
    public interface ASreferencetype<exp> where exp : class, ASreferencetype { }
    public interface ASpointtype { }

    public interface ASspot { }
    public interface ASspot<exp, arg>
        where arg : struct, ASspot
        where exp : struct , ASvaluetype
    { }
    
    public interface ASpointtype<exp> where exp : struct, ASvaluetype, ASspot<exp, enumeration> { }

    public interface ASconvertible {
        enumeration ToEnumeration(ASformatprovider arg = default(ASformatprovider));
        //IConvertible
        //long ToInt64(IFormatProvider provider);
    }

    public interface ASconvertible<exp> { }

    public interface ASformatprovider { }
    public interface AScasteable {
    
    
    }
    public interface AScasteable<exp> {
        exp parse(string arg);
    }
    public interface ASensureable { }
    public interface ASensureable<exp> {
        bool parseif(out exp ret, string arg);

    }
    public interface ASoperationable { }

    public class property<exp> where exp:class, new() { 
      public exp model { get; set; } 
    }
    public struct field<exp> where exp :  struct { 
      public exp code { get; set; }
        public field(exp arg)
        {
            code = arg;
        }
    }

    public struct guide<exp,arg>
        where arg: struct , ASspot
        where exp : struct, ASvaluetype, ASspot<exp, enumeration>
    {
        public exp hand { get; set; }

        //public exp limit() {
        //    //  = &hand;
        //}
    }

    public struct integers {
        #region long values
        public const long longMaxValue = long.MaxValue;
        public const long longMinValue = long.MinValue;
        #endregion

        #region int values
        public const int intMaxValue = int.MaxValue;
        public const int intMinValue = int.MinValue;
        public const int byTwo = 2;
        #endregion
    }

    [systemFlags] public enum flags: long { min = longMinValue,
       defaults = default(long),
       @null,@checked,
        @default = @checked * byTwo,
        @unchecked = @default * byTwo,
        @true = @unchecked * byTwo,
        @params = @true * byTwo,
        @false = @params * byTwo,
        @typeof = @false * byTwo,

        max = longMaxValue
    }

    
    public struct enumeration : ASconvertible, ASspot, ASvaluetype
    {
        private flags thisFeed;
        private flags thisDisplay;
        public flags feed { get => thisFeed; set => thisFeed = value; }
        public flags display { get => thisDisplay; set => thisDisplay = value; }

        public enumeration ToEnumeration(ASformatprovider arg = null)
        {
            //numberflags {
            //
            //
            //}
            //
            //numeric       --> used by ValueType like of IConvertible,
            //ocurrence       --> used by ValueType like of datetime<knd:exact,approximate>, timespan, 
            //text          --> used by RefenceType like String
            //arrange       --> used by RefenceType Array like of byte[] 


            //Jint32 {}
            //Jdatetimeflags {
            //   "kindYear": {normal:@virtual, bisiesto: @override}
            //    enumeration        }
            //
            //Jdatetime { Jdatetimeflags,    Jint32 numberofyear,   }
            //92,183,274,365
            //convert<datetime,out:string>(datatypeString, valueObject, datetimeConvert<string> ), cast
            //convert<string,out:datetime>(datatypeString, valueObject, stringConvert<datetime> ), cast
            //datetimeconverting
            // parts-name -> day[sunday:'dddd'], week[], month[august:'MMMM||MMMM'], period-season[summer:'pppp'],year , hour, minute, second, frequency, locale
            // parts-shorts-name  -> day[sun:'ddd'] month[aug:'ddd||ddd'] period-season[summ:'ppp']   spring, summer, autumn, and winter
            // parts-shorts-hand  -> day[sun:'d||dd'] month[aug:'d||dd'] period-season[summ:'p||pp']
            // parts-shorts-code  -> day[sun:'1||01'] month[aug:'8||08'] period-season[summ:'2||02']
            //
            //stringconverting
            enumeration result = this;
            bool flag = true;
            if (arg == null) flag = false;
            //throw new NotImplementedException();

            return result;
        }


        //TODO generate enumerationDemmand:demmand {target->,argument->{kind{constructor,method{call{1,2},exec{1,2}}}}}
        //public enumeration(flags )
        //public enumeration(flags )
        //{

        //}
    }

    //public struct @struct<exp> { }

    #region for enumeration
    //public struct numericstyle<Enum> where Enum:struct,  { }

    //public struct numericstyles { }

    //public struct typecode<Enum>
    //{
    //    //public typecode()
    //    //{
    //    //    //this.GetType().MemberType

    //    //}
    //}

    //public struct typecodes { }
    #endregion



    public class Jstring { }

    public struct Jenumeration : ASvaluetype, ASvaluetype<enumeration>,
        ASspot<Jenumeration, enumeration>,
        ASpointtype<Jenumeration>
    {
        private field<enumeration> item;
        public field<enumeration> readvalue { get => item; }

        public field<enumeration> writevalue { set => item = value; }
    }
    public struct Jguid { }
    public struct Jdatetime { }
    public struct Jboolean {
        //bool
    }
    public struct Jbyte
    {
        //byte
    }
    public struct Jsingle {
      //float
    }
    public struct Jdouble { }

    public struct Jdecimal { }

    public struct Jint32 {
       //int
    }

    public struct Jint64
    {
        //long
    }

}