using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Eblue.Code.Models;
using Eblue.Code;

using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;


namespace Eblue.Project
{  

    public partial class ProjectPreview : Eblue.Code.PageBasic
    {

        #region projectpreview properties
        public Guid? ProjectID
        {
            get
            {
                Guid? result = default(Guid?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    result = new Guid(viewState["ProjectID"].ToString());
                }

                return result;
            }

            set
            {


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["ProjectID"] = value;
                }


            }

        }

        public string PlayerCaption
        {
            get
            {
                string result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    result = viewState["PlayerCaption"]?.ToString();

                }

                return result;
            }

            set
            {


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["PlayerCaption"] = value;
                }


            }

        }

        public int? PlayerRoleId
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["PlayerRoleId"];

                    if (intObject != null && intObject is IConvertible)
                    {
                        IConvertible exp = intObject as IConvertible;
                        if (exp != null)
                        {
                            result = exp.ToInt32(null);

                        }
                        else
                        {
                            result = Convert.ToInt32(intObject);
                        }
                    }

                }

                return result;
            }

            set
            {


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["PlayerRoleId"] = value;
                }


            }

        }

        public int? NotesQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["NotesQuantity"];

                    if (intObject != null && intObject is IConvertible)
                    {
                        IConvertible exp = intObject as IConvertible;
                        if (exp != null)
                        {
                            result = exp.ToInt32(null);

                        }
                        else
                        {
                            result = Convert.ToInt32(intObject);
                        }
                    }

                }

                return result;
            }

            set
            {


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["NotesQuantity"] = value;
                }


            }

        }

        public int? SignsQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["SignsQuantity"];

                    if (intObject != null && intObject is IConvertible)
                    {
                        IConvertible exp = intObject as IConvertible;
                        if (exp != null)
                        {
                            result = exp.ToInt32(null);

                        }
                        else
                        {
                            result = Convert.ToInt32(intObject);
                        }
                    }

                }

                return result;
            }

            set
            {


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["SignsQuantity"] = value;
                }


            }

        }

        public bool HasViewState
        {
            get
            {
                var viewState = this.ViewState;
                bool result = true;
                if (viewState == null) result = false;

                return result;

            }
        }

        public string TabSelectedIndex
        {
            get
            {
                var viewState = this.ViewState;
                string result = string.Empty;
                if (viewState != null) result = viewState["TabSelectedIndex"] as string;
                return result;
            }

            set
            {
                var viewState = this.ViewState;
                if (viewState != null) viewState["TabSelectedIndex"] = value;

            }

        }

        public string InnerProjectNotes
        {
            get
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var objectValue = viewState["InnerProjectNotes"];
                    if (objectValue != null)
                    {
                        result = objectValue.ToString();
                    }
                }


                return result;
            }

            set
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["InnerProjectNotes"] = value;

                }


            }

        }

        public string InnerProjectSigns
        {
            get
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var objectValue = viewState["InnerProjectSigns"];
                    if (objectValue != null)
                    {
                        result = objectValue.ToString();
                    }
                }


                return result;
            }

            set
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["InnerProjectSigns"] = value;

                }


            }

        }
        #endregion



        public string GetPlayerCaption() => (string.IsNullOrEmpty(PlayerCaption)) ? "{noset}" : PlayerCaption;
        public string GetProjectNotes() => (string.IsNullOrEmpty(InnerProjectNotes)) ? "{no notes}" : InnerProjectNotes;
        public string GetProjectSigns() => (string.IsNullOrEmpty(InnerProjectSigns)) ? "{no signs}" : InnerProjectSigns;
        public int GetNotesQuantity() => (NotesQuantity == null) ? 0 : NotesQuantity.Value;
        public int GetSignsQuantity() => (SignsQuantity == null) ? 0 : SignsQuantity.Value;

        public Guid? GetProjectID() => ProjectID;

        public int GetMembersQuantity() => 4;

        public Eblue.Code.PlayerInfoSet PlayerInfoSet
        {

            get
            {
                Eblue.Code.PlayerInfoSet result = new PlayerInfoSet();

                if (HasViewState)
                {
                    var viewstateSession = this.ViewState;
                    object objectValue = viewstateSession["PlayerInfoSet"];
                    if (objectValue != null)
                        if (objectValue is PlayerInfoSet exp)
                        {
                            result = exp;
                        }

                }
                return result;

            }
            //set
            //{
            //    if (HasViewState)
            //    {
            //        var viewstateSession = this.ViewState;
            //        viewstateSession["PlayerInfoSet"] = value;


            //    }
            //}

        }
        public Eblue.Code.TargetSectionSet targetSections
        {

            get
            {
                Eblue.Code.TargetSectionSet result = null;
                object viewstateSession = this.ViewState;

                if (viewstateSession == null)
                {
                    //var stop = true;
                    result = new Code.TargetSectionSet();

                }
                else
                {
                    result = this.ViewState["targetSections"] as Eblue.Code.TargetSectionSet;

                }

                return result;

            }
            set
            {
                object viewstateSession = this.ViewState;

                if (viewstateSession == null)
                {
                    //var stop = true;

                }
                else
                {
                    this.ViewState["targetSections"] = value;

                }
            }

        }
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {

                if (Request.IsAuthenticated)
                {

                    var userId = Eblue.Utils.SessionTools.UserId;
                    var userLogged = Eblue.Utils.SessionTools.UserInfo;
                    var isPlayer = false;
                    var pIDString = Request.QueryString["PID"].ToString();

                    bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

                    if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager || userLogged.IsPersonnel || userLogged.IsScientist || userLogged.IsStudent || userLogged.IsCoordinator))
                    {

                        if (userLogged.IsAdmin || userLogged.IsManager)
                        {
                            isPlayer = true;
                        }
                        else
                        {
                            //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            //    "FROM Projects AS P " +
                            //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";

                            //var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                            //    $"where (p.ProjectID = '{pIDString}') and " +
                            //    $"(p.ProjectPI = '{userLogged.RosterId}' " +
                            //    $"or exists(select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in ('{userLogged.RosterId}')))";

                            var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                                $"where (p.ProjectID = '{pIDString}') and " +
                                $"(p.ProjectPI = '{userLogged.RosterId}' " +
                                $"or exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";

                            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                            {
                                cn.Open();
                                SqlCommand checkcmd = new SqlCommand(strCommand, cn);

                                isPlayer = Convert.ToBoolean(checkcmd.ExecuteScalar());

                                cn.Close();
                            }
                        }
                    }

                    if (isPlayer)
                    {
                        this.ProjectID = projectId;
                        var rosterId = userLogged.RosterId;

                        if (GeneratePlayerInfoSet(projectId, rosterId))
                        {


                        }

                        else
                        {
                            throw new Exception("Error at try getting the roster role info for this project");

                        }

                        if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
                        {

                            if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                            {

                                GenerateNotes(projectId);
                                GenerateSigns(projectId);

                                //if (GetProjectNotes(out ProjectNoteSet notes, projectId))
                                //{


                                //    if (notes != null && notes.Count > 0)
                                //    {
                                //        //renderNotes

                                //        if (GenerateNotesTextFor(out string innerText, notes))
                                //        {

                                //            notes.InnerText = innerText;
                                //            InnerProjectNotes = innerText;
                                //        }

                                //    }




                                //}
                                this.targetSections = tss;
                            }
                            else
                            {
                                throw new Exception("Error when trying to get the roster capacities in the sections of this projects");
                            }
                        }
                        else
                        {

                            throw new Exception("Error at try getting the roster role category in this project");
                        }
                        //utilizado para habilitar las secciones segun las jerarquias
                        ViewState["HasSection1"] = false;

                        if (Guid.TryParse(pIDString, out Guid pID))
                        {
                            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

                            try
                            {
                                string SelectAProject = "Select *, " +
                                    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, " +
                                    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, " +
                                    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber from Projects p where ProjectID = @ProjectID";

                                SqlCommand cmd = new SqlCommand(SelectAProject, cn);
                                cmd.Parameters.AddWithValue("@ProjectID", pID);

                                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                                DataSet dst = new DataSet();

                                adap.Fill(dst);

                                DataRow project = dst.Tables[0].Rows[0];

                                #region bind data controls


                                #region first section                               

                                labelLeaderResault.Text = project["RosterName"].ToString();
                                labelProjectnumberResult.Text = project["ProjectNumber"].ToString();
                                labelFiscalYearResult.Text = project["FiscalYearNumber"].ToString();
                                labelStatusResult.Text = project["ProyectStatusName"].ToString();

                                TextBoxContractNumber.Text = project["ContractNumber"].ToString();
                                TextBoxORCID.Text = project["ORCID"].ToString();
                                #endregion
                                #region second section
                                TextBoxProjectShortTitle.Text = project["ProjectTitle"].ToString();

                                DropDownListProgramaticArea.DataBind();
                                var programaticAreaId = project["ProgramAreaID"];

                                if (programaticAreaId != null && !string.IsNullOrEmpty(programaticAreaId.ToString()))
                                {
                                    DropDownListProgramaticArea.Items.FindByValue(programaticAreaId.ToString()).Selected = true;
                                }

                                DropDownListDepartment.DataBind();
                                var departmentId = project["DepartmentID"];

                                if (departmentId != null && !string.IsNullOrEmpty(departmentId.ToString()))
                                {
                                    DropDownListDepartment.Items.FindByValue(departmentId.ToString()).Selected = true;
                                }

                                DropDownListSubstation.DataBind();
                                var subStationId = project["SubStationID"];

                                if (subStationId != null && !string.IsNullOrEmpty(subStationId.ToString()))
                                {
                                    DropDownListSubstation.Items.FindByValue(subStationId.ToString()).Selected = true;
                                }

                                DropDownListCommodity.DataBind();
                                var commodityId = project["CommId"];

                                if (commodityId != null && !string.IsNullOrEmpty(commodityId.ToString()))
                                {
                                    DropDownListCommodity.Items.FindByValue(commodityId.ToString()).Selected = true;
                                }
                                #endregion


                                //#region 3ra Sections
                                //TextBoxProjectObjective.Text = project["Objectives"].ToString();
                                //TextBoxobjectivefortheyear.Text = project["ObjWorkPlan"].ToString();
                                //TextBoxWorkAccomplished.Text = project["PresentOutlook"].ToString();
                                //TextBoxFieldWork.Text = project["WP1FieldWork"].ToString();
                                //#endregion

                                //#region 4ta Sections
                                //// no neede come from grid
                                //#endregion

                                //#region 5ta Sections
                                //TextBoxWorkPlanned2.Text = project["WorkPlan2"].ToString();
                                //TextBoxDescription.Text = project["WorkPlan2Desc"].ToString();
                                //TextBoxEstimatedTime.Text = project["ResultsAvailable"].ToString();
                                //TextBoxFacilitieNeeded.Text = project["Facilities"].ToString();
                                //#endregion

                                //#region 5ta Sections  
                                //TextBoxProjectImpact.Text = project["Impact"].ToString();
                                //TextBoxSalariesDesc.Text = project["Salaries"].ToString();
                                //TextBoxMaterialDesc.Text = project["Materials"].ToString();
                                //TextBoxEquipmentDesc.Text = project["Equipment"].ToString();
                                //TextBoxTravelDesc.Text = project["Travel"].ToString();
                                //TextBoxAbroadDesc.Text = project["Abroad"].ToString();
                                //TextBoxOthersDesc.Text = project["Others"].ToString();

                                //TextBoxWagesDesc.Text = project["wages"].ToString();
                                //TextBoxBenefitsDesc.Text = project["benefits"].ToString();
                                //TextBoxAssistantshipsDesc.Text = project["assistant"].ToString();
                                //TextBoxSubcontractsDesc.Text = project["subcontracts"].ToString();
                                //TextBoxIndirectCostsDesc.Text = project["IndirectCosts"].ToString();
                                //#endregion


                                #endregion

                                cmd.Dispose();
                                cn.Close();
                            }
                            catch (SqlException ex)
                            {
                                //Message.Text = "opps it happen again" + ex;
                                //ErrorMessage.Visible = true;
                            }
                        }

                    }

                    else
                    {
                        var uri = Request.Url;
                        var route = uri.LocalPath;
                        var queryParams = uri.Query ?? string.Empty;
                        base.GoToUnAuthorizeRoute(route: route, query: queryParams);
                    }



                }

            }
            else
            {
                if (Request.IsAuthenticated)
                {

                    var userId = Eblue.Utils.SessionTools.UserId;
                    var userLogged = Eblue.Utils.SessionTools.UserInfo;
                    var isPlayer = false;
                    var pIDString = Request.QueryString["PID"].ToString();

                    bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

                    if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager))
                    {

                        if (userLogged.IsAdmin || userLogged.IsManager)
                        {
                            isPlayer = true;
                        }
                        else
                        {
                            //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            //    "FROM Projects AS P " +
                            //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";

                            //var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                            //    $"where (p.ProjectID = '{pIDString}') and " +
                            //    $"(p.ProjectPI = '{userLogged.RosterId}' " +
                            //    $"or exists(select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in ('{userLogged.RosterId}')))";

                            var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                                $"where (p.ProjectID = '{pIDString}') and " +
                                $"(p.ProjectPI = '{userLogged.RosterId}' " +
                                $"or exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";

                            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                            {
                                cn.Open();
                                SqlCommand checkcmd = new SqlCommand(strCommand, cn);

                                isPlayer = Convert.ToBoolean(checkcmd.ExecuteScalar());

                                cn.Close();
                            }
                        }
                    }

                    if (isPlayer)
                    {
                        if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
                        {

                            if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                            {
                                this.targetSections = tss;
                            }
                            else
                            {
                                throw new Exception("Error at try getting the roster role sections capabilities in this project");
                            }
                        }
                        else
                        {

                            throw new Exception("Error at try getting the roster role category in this project");
                        }


                    }

                    else
                    {

                        base.GoToUnAuthorizeRoute();
                    }



                }




            }

            if (!Page.IsPostBack)
            {
                TabSelectedIndex = "0";
                //TO show at reset
                //base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
               

            }
            else
            {
                //used only for target
                if (GetRequestParamEventTarget(out string eventTarget, this.Page))
                {
                    var tabPrefix = "tab_";
                    bool isTab = eventTarget.IndexOf(tabPrefix, StringComparison.InvariantCultureIgnoreCase) == 0;

                    if (isTab && GetRequestParamEventArgument(out string eventArgument, this.Page))
                    {
                        var args = eventArgument.Split('|');
                        if (args.Length == 1)
                        {
                            TabSelectedIndex = args[0];
                        }
                    }

                }

                var ctrl = base.GetControlThatCausedPostBack(Page);
                //TO show at reset
                //base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);

            }



            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a4;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                //TO show at reset
                //EvalGridPermission(section.Value, gvP3BeInitiated);
                //EvalGridPermission(section.Value, gvP3Progress);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a5;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                //TO show at reset
                //EvalGridPermission(section.Value, gvLab);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a6;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                //TO show at reset
                //EvalGridPermission(section.Value, gvSci);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a7;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                //TO show at reset
                //EvalGridPermission(section.Value, gvGradAss);
                //EvalGridPermission(section.Value, gvOP);

            }

         

            {
                //TO show at reset
                //this.imagenSign.ImageUrl = GetRosterSignature();
            }

        }

        protected void T1__TextChanged(object sender, System.EventArgs e)
        {
            var type = sender?.GetType();
            //Just assume 1Dollar = 45 Rupees  
            //T2.Value = (Int16.Parse(T1.Value) * 45).ToString();
        }


        #region Player
        protected bool GeneratePlayerInfoSet(Guid projectId, Guid rosterId)
        {

            bool result = false;

            if (GetProjectPlayersInfo(out PlayerInfoSet listset, projectId, rosterId))
            {
                result = true;


                if (listset != null && listset.Count > 0)
                {
                    //PlayerInfoSet = listset;

                    var player = listset.First();
                    this.PlayerCaption = player.Caption;
                    this.PlayerRoleId = player.RoleId;

                }
                else
                {
                    //PlayerInfoSet = new PlayerInfoSet();

                }




            }

            return result;
        }

        protected bool GetProjectPlayersInfo(out PlayerInfoSet listset, Guid projectID, Guid rosterID)
        {
            bool result = false;
            listset = new PlayerInfoSet();
            var list = new PlayerInfoSet();

            var stringCommand = SqlDataSourcePlayerProject.SelectCommand;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@ProjectID", projectID), new SqlParameter("@RosterID", rosterID))
            {
                commandString = stringCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, reader => {


                int.TryParse(reader["roleid"]?.ToString(), out int roleID);
                var roleName = reader["rolename"]?.ToString();
                Guid.TryParse(reader["rolecategoryid"]?.ToString(), out Guid roletypeID);
                var roletypeDescription = reader["roletypedescription"]?.ToString();
                var caption = reader["roleCaption"]?.ToString();


                var model = new PlayerInfo(roleID, roleName, roletypeID, roletypeDescription, caption, rosterID);

                list.Add(model);
            });

            listset = list;

            return result;

        }

        #endregion

        #region Notes
        protected void GenerateNotes(Guid projectId)
        {



            if (GetProjectNotes(out ProjectNoteSet notes, projectId))
            {

                NotesQuantity = notes.Count;

                if (notes != null && notes.Count > 0)
                {
                    //renderNotes

                    if (GenerateNotesTextFor(out string innerText, notes))
                    {

                        notes.InnerText = innerText;
                        InnerProjectNotes = innerText;
                    }

                }




            }
        }
        protected bool GetProjectNotes(out ProjectNoteSet notes, Guid projectID)
        {
            bool result = false;
            notes = default(ProjectNoteSet);
            var pnoteSet = new ProjectNoteSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(new SqlParameter("@projectID", projectID))
            {
                commandString = "" +
             "select " +
             "ROW_NUMBER() over (order by iif( (select top 1 OrderLine from projectNotes where uid = pn.ResponseNoteID ) is null, pn.orderline, (select top 1 OrderLine from projectNotes where uid = pn.ResponseNoteID ) ), pn.notedate ) as rowNumber, pn.UId,  " +
             "HierarchyOrderLine = iif( (select top 1 OrderLine from projectNotes where uid = pn.ResponseNoteID ) is null, pn.orderline, (select top 1 OrderLine from projectNotes where uid = pn.ResponseNoteID ) )," +
             "pn.OrderLine, pn.NoteDate, pn.NoteData, pn.RosterData, pn.ProjectID, pn.RosterID, pn.RoleId, pn.ResponseNoteID, r.RoleName, pr.RosterName  " +
             "from projectNotes pn inner join Projects p on p.ProjectID = pn.ProjectID inner join roster pr on pr.RosterID = pn.RosterID " +
             "left join Roles r on r.RoleID = pn.RoleId left join RoleCategory rc on rc.UId = r.RoleCategoryId " +
             "left join projectNotes pnr on pnr.ResponseNoteID = pn.UId " +
             "where pn.projectID = @projectID "
            };

            result = FetchData(reqInfo, reader => {

                //rosterName = reader["rostername"] as string;
                //userLogged.UserFullDescription = rosterName;

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                DateTime.TryParse(reader["NoteDate"]?.ToString(), out DateTime noteDate);
                var noteData = reader["noteData"]?.ToString();
                var rosterName = reader["rosterName"]?.ToString();
                var rosterPicture = reader["rosterData"]?.ToString();
                rosterPicture = (string.IsNullOrEmpty(rosterPicture)) ? UserGenericPictureData : rosterPicture;
                int.TryParse(reader["HierarchyOrderLine"]?.ToString(), out int HierarchyOrderLine);

                var projectNote = new ProjectNote(rowNumber, Uid, noteDate, noteData, rosterName, rosterPicture, HierarchyOrderLine);

                pnoteSet.Add(projectNote);
            });

            notes = pnoteSet;

            return result;

        }
        public bool GenerateNotesTextFor(out string output, ProjectNoteSet container)
        {
            bool result = false;
            output = string.Empty;

            try
            {

                using (Control ctrl = new System.Web.UI.Control())
                {

                    container
                        //.Where(t => t.NotVisibleForMenu == false)
                        .OrderByDescending(exp => exp.Item1)
                        .ToList().ForEach(note =>
                        {

                            var divContainer = new HtmlGenericControl("div");
                            var classContainer = "";
                            var divHeader = new HtmlGenericControl("div");
                            var classHeader = "";
                            var spanRoster = new HtmlGenericControl("span") { InnerText = $"{note.RosterName}" };
                            var classRoster = "";
                            var dateTime = note.NoteDate.ToString("d MMM hh:mm");
                            var dtTT = note.NoteDate.ToString("tt").ToLower();
                            var dateTimeStamp = $"{dateTime} {dtTT}";
                            var spanDayTime = new HtmlGenericControl("span") { InnerText = $"{dateTimeStamp}" };
                            var classDayTime = "";

                            var img = new HtmlGenericControl("img");
                            var classPicture = "direct-chat-img";
                            var divBody = new HtmlGenericControl("div") { InnerText = $"{note.NoteData}" };
                            var classNote = "direct-chat-text";


                            if (note.RowNumber % 2 != 0)
                            {
                                classContainer = "direct-chat-msg";
                                classRoster = "direct-chat-name float-left";
                                classDayTime = "direct-chat-timestamp float-right";
                                classHeader = "direct-chat-info clearfix";
                            }
                            else
                            {
                                classContainer = "direct-chat-msg right";
                                classRoster = "direct-chat-name float-right";
                                classDayTime = "direct-chat-timestamp float-left";
                                classHeader = "direct-chat-info clearfix";
                            }

                            divContainer.Attributes.Add("class", $"{classContainer}");

                            spanRoster.Attributes.Add("class", $"{classRoster}");
                            spanDayTime.Attributes.Add("class", $"{classDayTime}");

                            img.Attributes.Add("class", $"{classPicture}");
                            img.Attributes.Add("src", $"{note.RosterPicture}");

                            divBody.Attributes.Add("class", $"{classNote}");

                            divHeader.Attributes.Add("class", $"{classHeader}");
                            divHeader.Controls.Add(spanRoster);
                            divHeader.Controls.Add(spanDayTime);

                            divContainer.Controls.Add(divHeader);
                            divContainer.Controls.Add(img);
                            divContainer.Controls.Add(divBody);


                            ctrl.Controls.Add(divContainer);

                            //var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();

                            //var listitemPanel = new HtmlGenericControl("li") { };
                            //var anchorPanel = new HtmlGenericControl("a") { };
                            //var iconicPanel = new HtmlGenericControl("i") { };
                            //var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
                            //var iconicparagraphPanel = new HtmlGenericControl("i") { };


                            //iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
                            //anchorPanel.Controls.Add(iconicPanel);

                            //iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

                            //paragraphPanel.Controls.Add(iconicparagraphPanel);
                            //anchorPanel.Controls.Add(paragraphPanel);

                            //var panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " active" : string.Empty;

                            //anchorPanel.Attributes.Add("class", $"nav-link{panelStyle}");
                            //anchorPanel.Attributes.Add("href", $"{panel.Route}");
                            //listitemPanel.Controls.Add(anchorPanel);

                            //panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " menu-open" : string.Empty;

                            //listitemPanel.Attributes.Add("class", $"nav-item has-treeview{panelStyle}");

                            //var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

                            //var unorderedlistPage = new HtmlGenericControl("ul") { };
                            //actionPages.ForEach(page =>
                            //{

                            //    var listitemPage = new HtmlGenericControl("li") { };
                            //    var anchorPage = new HtmlGenericControl("a") { };
                            //    var iconicPage = new HtmlGenericControl("i") { };
                            //    var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}" };

                            //    iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
                            //    anchorPage.Controls.Add(iconicPage);

                            //    anchorPage.Controls.Add(paragraphPage);
                            //    var linkStyle = (PanelSelectedIndex == $"{panel.OrderLine}" & LinkSelectedIndex == $"{page.OrderLine}") ? " active" : string.Empty;
                            //    anchorPage.Attributes.Add("class", $"nav-link{linkStyle}");
                            //    //var route = $"{ page.Route}";
                            //    //anchorPage.Attributes.Add("href", page.Route);
                            //    var route = $"{this.ResolveClientUrl($"~{ page.Route}")}";
                            //    anchorPage.Attributes.Add("href", route);

                            //    listitemPage.Controls.Add(anchorPage);

                            //    listitemPage.Attributes.Add("class", "nav-item");
                            //    unorderedlistPage.Controls.Add(listitemPage);

                            //});

                            //unorderedlistPage.Attributes.Add("class", "nav nav-treeview");
                            //listitemPanel.Controls.Add(unorderedlistPage);

                            ////menuContentArea.Controls.Add(listitemPanel);
                            //ctrl.Controls.Add(listitemPanel);
                        });

                    int count = ctrl.Controls.Count;
                    if (count > 0 && Eblue.Utils.WebTools.GetTextFor(out string outputString, ctrl))
                    {
                        output = outputString;
                        result = true;
                    }
                }

            }
            catch
            {


            }

            return result;


        }

        #endregion

        #region Signs
        protected void GenerateSigns(Guid projectId)
        {



            if (GetProjectSigns(out ProjectSignSet signs, projectId))
            {

                SignsQuantity = signs.Count;

                if (signs != null && signs.Count > 0)
                {
                    //renderSigns

                    if (GenerateSignsTextFor(out string innerText, signs))
                    {

                        signs.InnerText = innerText;
                        InnerProjectSigns = innerText;
                    }

                }




            }
        }

        protected bool GetProjectSigns(out ProjectSignSet signs, Guid projectID)
        {
            bool result = false;
            signs = default(ProjectSignSet);
            var listset = new ProjectSignSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(new SqlParameter("@ProjectID", projectID))
            {
                commandString = SqlDataSourceProjectSignsSelect.SelectCommand
            };

            result = FetchData(reqInfo, reader => {

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                DateTime.TryParse(reader["SignDate"]?.ToString(), out DateTime signDate);
                var signData = reader["signData"]?.ToString();
                signData = (string.IsNullOrEmpty(signData)) ? UserGenericSignData : signData;
                var rosterName = reader["rosterName"]?.ToString();
                var rosterPicture = reader["rosterData"]?.ToString();
                rosterPicture = (string.IsNullOrEmpty(rosterPicture)) ? UserGenericPictureData : rosterPicture;

                var projectSign = new ProjectSign(rowNumber, Uid, signDate, signData, rosterName, rosterPicture);

                listset.Add(projectSign);
            });

            signs = listset;

            return result;

        }

        public bool GenerateSignsTextFor(out string output, ProjectSignSet container)
        {
            bool result = false;
            output = string.Empty;

            try
            {

                using (Control ctrl = new System.Web.UI.Control())
                {

                    container
                        //.Where(t => t.NotVisibleForMenu == false)
                        .OrderByDescending(exp => exp.Item1)
                        .ToList().ForEach(member =>
                        {

                            var divContainer = new HtmlGenericControl("div");
                            var classContainer = "";
                            var divHeader = new HtmlGenericControl("div");
                            var classHeader = "";
                            var spanRoster = new HtmlGenericControl("span") { InnerText = $"{member.RosterName}" };
                            var classRoster = "";
                            var dateTime = member.SignDate.ToString("d MMM hh:mm");
                            var dtTT = member.SignDate.ToString("tt").ToLower();
                            var dateTimeStamp = $"{dateTime} {dtTT}";
                            var spanDayTime = new HtmlGenericControl("span") { InnerText = $"{dateTimeStamp}" };
                            var classDayTime = "";

                            var img = new HtmlGenericControl("img");
                            var classPicture = "direct-chat-img";
                            //var divBody = new HtmlGenericControl("div") { InnerText = $"{member.SignData}" };
                            var divBody = new HtmlGenericControl("div") { };
                            //<img src="http://placehold.it/150x100" alt="..." class="margin">
                            var imgMember = new HtmlGenericControl("img");
                            var classSign = "margin";
                            var classMember = "direct-chat-text";


                            if (member.RowNumber % 2 != 0)
                            {
                                classContainer = "direct-chat-msg";
                                classRoster = "direct-chat-name float-left";
                                classDayTime = "direct-chat-timestamp float-right";
                                classHeader = "direct-chat-info clearfix";
                            }
                            else
                            {
                                classContainer = "direct-chat-msg right";
                                classRoster = "direct-chat-name float-right";
                                classDayTime = "direct-chat-timestamp float-left";
                                classHeader = "direct-chat-info clearfix";
                            }

                            divContainer.Attributes.Add("class", $"{classContainer}");

                            spanRoster.Attributes.Add("class", $"{classRoster}");
                            spanDayTime.Attributes.Add("class", $"{classDayTime}");

                            img.Attributes.Add("class", $"{classPicture}");
                            img.Attributes.Add("src", $"{member.RosterData}");


                            imgMember.Attributes.Add("class", $"{classSign}");
                            imgMember.Attributes.Add("src", $"{member.SignData}");


                            divBody.Attributes.Add("class", $"{classMember}");
                            divBody.Controls.Add(imgMember);

                            divHeader.Attributes.Add("class", $"{classHeader}");
                            divHeader.Controls.Add(spanRoster);
                            divHeader.Controls.Add(spanDayTime);

                            divContainer.Controls.Add(divHeader);
                            divContainer.Controls.Add(img);
                            divContainer.Controls.Add(divBody);


                            ctrl.Controls.Add(divContainer);

                            //var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();

                            //var listitemPanel = new HtmlGenericControl("li") { };
                            //var anchorPanel = new HtmlGenericControl("a") { };
                            //var iconicPanel = new HtmlGenericControl("i") { };
                            //var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
                            //var iconicparagraphPanel = new HtmlGenericControl("i") { };


                            //iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
                            //anchorPanel.Controls.Add(iconicPanel);

                            //iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

                            //paragraphPanel.Controls.Add(iconicparagraphPanel);
                            //anchorPanel.Controls.Add(paragraphPanel);

                            //var panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " active" : string.Empty;

                            //anchorPanel.Attributes.Add("class", $"nav-link{panelStyle}");
                            //anchorPanel.Attributes.Add("href", $"{panel.Route}");
                            //listitemPanel.Controls.Add(anchorPanel);

                            //panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " menu-open" : string.Empty;

                            //listitemPanel.Attributes.Add("class", $"nav-item has-treeview{panelStyle}");

                            //var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

                            //var unorderedlistPage = new HtmlGenericControl("ul") { };
                            //actionPages.ForEach(page =>
                            //{

                            //    var listitemPage = new HtmlGenericControl("li") { };
                            //    var anchorPage = new HtmlGenericControl("a") { };
                            //    var iconicPage = new HtmlGenericControl("i") { };
                            //    var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}" };

                            //    iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
                            //    anchorPage.Controls.Add(iconicPage);

                            //    anchorPage.Controls.Add(paragraphPage);
                            //    var linkStyle = (PanelSelectedIndex == $"{panel.OrderLine}" & LinkSelectedIndex == $"{page.OrderLine}") ? " active" : string.Empty;
                            //    anchorPage.Attributes.Add("class", $"nav-link{linkStyle}");
                            //    //var route = $"{ page.Route}";
                            //    //anchorPage.Attributes.Add("href", page.Route);
                            //    var route = $"{this.ResolveClientUrl($"~{ page.Route}")}";
                            //    anchorPage.Attributes.Add("href", route);

                            //    listitemPage.Controls.Add(anchorPage);

                            //    listitemPage.Attributes.Add("class", "nav-item");
                            //    unorderedlistPage.Controls.Add(listitemPage);

                            //});

                            //unorderedlistPage.Attributes.Add("class", "nav nav-treeview");
                            //listitemPanel.Controls.Add(unorderedlistPage);

                            ////menuContentArea.Controls.Add(listitemPanel);
                            //ctrl.Controls.Add(listitemPanel);
                        });

                    int count = ctrl.Controls.Count;
                    if (count > 0 && Eblue.Utils.WebTools.GetTextFor(out string outputString, ctrl))
                    {
                        output = outputString;
                        result = true;
                    }
                }

            }
            catch
            {


            }

            return result;


        }

      
        #endregion



        protected void EvalGridPermission(Eblue.Code.TargetSection section, GridView grid)
        {
            var rows = grid.Rows.OfType<GridViewRow>();
            foreach (var row in rows)
            {

                PreventGridPermission(section, row);


            }

        }

        protected void PreventGridPermission(Eblue.Code.TargetSection section, GridViewRow row)
        {
            if (section != null && row.RowType == DataControlRowType.DataRow)
            {
                var rowCellList = row.Controls.OfType<System.Web.UI.Control>();
                var ctrlCellList = rowCellList.Where(ctrl => ctrl is DataControlFieldCell);

                if (ctrlCellList != null && ctrlCellList.Count() > 0)
                {
                    var cellList = ctrlCellList.OfType<DataControlFieldCell>();
                    var cmdCellList = cellList.Where(cell => cell.ContainingField is System.Web.UI.WebControls.CommandField);
                    var tplCellList = cellList.Where(cell => cell.ContainingField is System.Web.UI.WebControls.TemplateField);



                    if (cmdCellList != null && cmdCellList.Count() > 0)
                    {
                        var cmdCell = cmdCellList.First();

                        //var ctrlList = e.Row.Controls.OfType<System.Web.UI.Control>();
                        var ctrlList = cmdCell.Controls.OfType<System.Web.UI.Control>();

                        var ctrlButtonList = ctrlList.Where(ctrl => ctrl is System.Web.UI.WebControls.Button);
                        var ctrlLiteralControlList = ctrlList.Where(ctrl => ctrl is System.Web.UI.LiteralControl);

                        if (ctrlButtonList != null && ctrlButtonList.Count() > 0)
                        {

                            //if (section.listCanEdit)
                            //{

                            var buttonList = ctrlButtonList.OfType<System.Web.UI.WebControls.Button>();
                            var deleteButton = buttonList.FirstOrDefault(ctrl => string.Equals("Delete", ctrl.Text, StringComparison.InvariantCultureIgnoreCase)
                            || string.Equals("-", ctrl.Text, StringComparison.InvariantCultureIgnoreCase));

                            var editButton = buttonList.FirstOrDefault(ctrl => string.Equals("Edit", ctrl.Text, StringComparison.InvariantCultureIgnoreCase)
                               || string.Equals("-", ctrl.Text, StringComparison.InvariantCultureIgnoreCase));

                            if (deleteButton != null)
                            {
                                deleteButton.OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";
                                deleteButton.Visible = section.listCanRemove;
                            }

                            if (editButton != null)
                            {

                                editButton.Visible = section.listCanEdit;
                            }
                            //}

                        }

                        if (ctrlLiteralControlList != null && ctrlLiteralControlList.Count() > 0)
                        {
                            var literalList = ctrlLiteralControlList.OfType<System.Web.UI.LiteralControl>();
                            var literalControl = literalList.FirstOrDefault(ctrl => string.Equals("&nbsp;", ctrl.Text, StringComparison.InvariantCultureIgnoreCase));

                            if (literalControl != null)
                            {
                                literalControl.Text = string.Empty;
                            }
                        }
                    }

                }




            }


        }


        protected bool GetRoleCategoryFromRoster(out Guid roleCategoryID, Guid projectId, Guid rosterID)
        {
            bool result = false;
            roleCategoryID = Guid.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();


                    SqlCommand cmdCommand = new SqlCommand(
                        $"select pp.RosterID, pp.RoleID, pp.ProjectID, r.RoleName, r.Enable as RoleEnable, r.Level as RoleLevel, " +
                        $"r.OrderLine as RoleOrderLine, rc.UId RoleTypeID, rc.Name RoleTypeName, rc.Description RoleTypeDescription, " +
                        $"rc.OrderLine as RoleTypeOrderLine from playerProject pp inner join roles r on r.RoleID = pp.RoleID " +
                        $"inner join RoleCategory rc on rc.UId = r.RoleCategoryId where pp.ProjectID = '{projectId}' and pp.RosterId = '{rosterID}' ", cn);

                    var reader = cmdCommand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            result = Guid.TryParse(reader["RoleTypeID"].ToString(), out roleCategoryID);

                        }

                    }

                    cn.Close();

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error at getting the roster role in this project", ex);
            }

            return result;

        }

        protected bool GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet targetSections, Guid roleCategoryID)
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
                        $"INNER JOIN roleTarget rt on rt.uid = rspl.RoleTargetID " +
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





        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            //base.OnSaveStateCompleteSentences
            base.OnSaveStateCompleteExtend(this);
        }

       


        
    }
}