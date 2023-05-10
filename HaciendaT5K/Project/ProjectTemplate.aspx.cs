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
using static Eblue.Utils.ProjectTools;

using static Eblue.Utils.SessionTools;
using Eblue.Utils;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Eblue.Project
{

    public partial class ProjectTemplate : Eblue.Code.PageBasic
    {


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
                    var idObject = Request.QueryString["PID"];

                    if (string.IsNullOrEmpty(idObject))
                    {
                        var errorMessage = "Error at try getting the project id information";
                        var builder = new System.Text.StringBuilder();

                        var ex = new Tuple<bool?, Exception>(null, new Exception("the parameter Id is null"));

                        HandlerExeption(errorMessage, builder, ex);


                    }

                    var pIDString = Request.QueryString["PID"].ToString();

                    bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);
                    //EvalIsUserType( UserTypeFlags.UserType).Isbool
                    //if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager || userLogged.IsPersonnel || userLogged.IsScientist || userLogged.IsStudent || userLogged.IsCoordinator || userLogged.IsDeveloper))

                    if (isValidProject && EvalIsUserType(UserTypeFlags.UserTypeProject).IsTrue)
                    {
                        //if (userLogged.IsAdmin || userLogged.IsManager)
                        if (EvalIsUserType(UserTypeFlags.UserTypeOwner).IsTrue)
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

                            var strCommand3 = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                                $"where (p.ProjectID = '{pIDString}') and " +
                                $"(p.ProjectPI = '{userLogged.RosterId}' " +
                                $"or exists(select 1 from playerProject pp   where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";

                            var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
                                $"where (p.ProjectID = '{pIDString}') and " +
                                $"(p.ProjectPI = '{userLogged.RosterId}' " +
                                $"or exists(select 1 from playerProject pp inner join Roster r on r.RosterID = pp.RosterID  where pp.ProjectID = p.ProjectID and r.RosterID in ('{userLogged.RosterId}')))";



                            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                            {
                                cn.Open();
                                SqlCommand checkcmd = new SqlCommand(strCommand, cn);

                                isPlayer = Convert.ToBoolean(checkcmd.ExecuteScalar());

                                cn.Close();
                            }
                        }
                    }



                    if (isPlayer )
                    {
                        this.ProjectID = projectId;
                        var rosterId = userLogged.RosterId;


                        GenerateProjectProccessInfo(projectId);




                        if (GeneratePlayerInfoSet(projectId, rosterId))
                        {


                        }

                        else
                        {
                            throw new Exception("Error at try getting the roster role info for this project");

                        }

                        /*test
                         if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
                        {
                            GenerateProjectSections(rcID);

                            #region deprecated
                            //if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                            //{

                            //    GenerateNotes(projectId);
                            //    GenerateSigns(projectId);

                            //    GenerateAssents(projectId);
                            //    GenerateObjetions(projectId);
                            //    GenerateStatus(projectId);

                            //    //if (GetProjectNotes(out ProjectNoteSet notes, projectId))
                            //    //{


                            //    //    if (notes != null && notes.Count > 0)
                            //    //    {
                            //    //        //renderNotes

                            //    //        if (GenerateNotesTextFor(out string innerText, notes))
                            //    //        {

                            //    //            notes.InnerText = innerText;
                            //    //            InnerProjectNotes = innerText;
                            //    //        }

                            //    //    }




                            //    //}
                            //    this.targetSections = tss;
                            //}
                            //else
                            //{
                            //    throw new Exception("Error when trying to get the roster capacities in the sections of this projects");
                            //}
                            #endregion

                            GenerateNotes(projectId);
                            GenerateSigns(projectId);
                            GenerateAssents(projectId);
                            GenerateObjetions(projectId);
                            GenerateStatus(projectId);
                        }
                        else
                        {

                            throw new Exception("Error at try getting the roster's role category in this project");
                        }
                         */


                        /*
                         
                         */

                        if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
                        {
                            GenerateProjectSections(rcID);

                            #region deprecated
                            //if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                            //{

                            //    GenerateNotes(projectId);
                            //    GenerateSigns(projectId);

                            //    GenerateAssents(projectId);
                            //    GenerateObjetions(projectId);
                            //    GenerateStatus(projectId);

                            //    //if (GetProjectNotes(out ProjectNoteSet notes, projectId))
                            //    //{


                            //    //    if (notes != null && notes.Count > 0)
                            //    //    {
                            //    //        //renderNotes

                            //    //        if (GenerateNotesTextFor(out string innerText, notes))
                            //    //        {

                            //    //            notes.InnerText = innerText;
                            //    //            InnerProjectNotes = innerText;
                            //    //        }

                            //    //    }




                            //    //}
                            //    this.targetSections = tss;
                            //}
                            //else
                            //{
                            //    throw new Exception("Error when trying to get the roster capacities in the sections of this projects");
                            //}
                            #endregion

                            GenerateNotes(projectId);
                            GenerateSigns(projectId);
                            GenerateAssents(projectId);
                            GenerateObjetions(projectId);
                            GenerateStatus(projectId);
                        }
                        else
                        {

                            throw new Exception("Error at try getting the roster's role category in this project");
                        }

                        GenerateProjectSections(rcID);

                        #region deprecated
                        //if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                        //{

                        //    GenerateNotes(projectId);
                        //    GenerateSigns(projectId);

                        //    GenerateAssents(projectId);
                        //    GenerateObjetions(projectId);
                        //    GenerateStatus(projectId);

                        //    //if (GetProjectNotes(out ProjectNoteSet notes, projectId))
                        //    //{


                        //    //    if (notes != null && notes.Count > 0)
                        //    //    {
                        //    //        //renderNotes

                        //    //        if (GenerateNotesTextFor(out string innerText, notes))
                        //    //        {

                        //    //            notes.InnerText = innerText;
                        //    //            InnerProjectNotes = innerText;
                        //    //        }

                        //    //    }




                        //    //}
                        //    this.targetSections = tss;
                        //}
                        //else
                        //{
                        //    throw new Exception("Error when trying to get the roster capacities in the sections of this projects");
                        //}
                        #endregion

                        GenerateNotes(projectId);
                        GenerateSigns(projectId);
                        GenerateAssents(projectId);
                        GenerateObjetions(projectId);
                        GenerateStatus(projectId);



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

                                labelLeaderResault.Text = project["RosterName"].ToString();
                                labelProjectnumberResult.Text = project["ProjectNumber"].ToString();
                                labelProjectNumberResult2.Text = project["ProjectNumber"].ToString();
                                labelFiscalYearResult.Text = project["FiscalYearNumber"].ToString();
                                labelStatusResult.Text = project["ProyectStatusName"].ToString();

                                TextBoxContractNumber.Text = project["ContractNumber"].ToString();
                                TextBoxORCID.Text = project["ORCID"].ToString();

                                LabelProjectShortTitleResult.Text = project["ProjectTitle"].ToString();

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

                                DropdownListPerformingOrganizations.DataBind();
                                var pOrganizationId = project["POrganizationsId"];

                                if (pOrganizationId.ToString() == "0")
                                {
                                    pOrganizationId = null;
                                }

                                if (pOrganizationId != null && !string.IsNullOrEmpty(pOrganizationId.ToString()))
                                {
                                    DropdownListPerformingOrganizations.Items.FindByValue(pOrganizationId.ToString()).Selected = true;
                                }

                                DropdownListTypeOfFunds.DataBind();
                                var typeOfFundsId = project["FundTypeID"];
                                if (typeOfFundsId.ToString() == "0")
                                {
                                    typeOfFundsId = null;
                                }

                                if (typeOfFundsId != null && !string.IsNullOrEmpty(typeOfFundsId.ToString()))
                                {
                                    DropdownListTypeOfFunds.Items.FindByValue(typeOfFundsId.ToString()).Selected = true;
                                }

                                


                                #region 3ra Sections
                                TextBoxProjectObjectiveToFill.Text = project["Objectives"].ToString();
                                TextBoxobjectivefortheyear.Text = project["ObjWorkPlan"].ToString();
                                TextBoxWorkAccomplished.Text = project["PresentOutlook"].ToString();
                                TextBoxFieldWork.Text = project["WP1FieldWork"].ToString();
                                #endregion

                                #region 4ta Sections
                                // no neede come from grid
                                #endregion

                                #region 5ta Sections
                                TextBoxWorkPlanned2.Text = project["WorkPlan2"].ToString();
                                TextBoxDescription.Text = project["WorkPlan2Desc"].ToString();
                                TextBoxEstimatedTime.Text = project["ResultsAvailable"].ToString();
                                TextBoxFacilitieNeeded.Text = project["Facilities"].ToString();
                                #endregion

                                #region 5ta Sections  
                                TextBoxProjectImpact.Text = project["Impact"].ToString();
                                TextBoxSalariesDesc.Text = project["Salaries"].ToString();
                                TextBoxMaterialDesc.Text = project["Materials"].ToString();
                                TextBoxEquipmentDesc.Text = project["Equipment"].ToString();
                                TextBoxTravelDesc.Text = project["Travel"].ToString();
                                TextBoxAbroadDesc.Text = project["Abroad"].ToString();
                                TextBoxOthersDesc.Text = project["Others"].ToString();

                                TextBoxWagesDesc.Text = project["wages"].ToString();
                                TextBoxBenefitsDesc.Text = project["benefits"].ToString();
                                TextBoxAssistantshipsDesc.Text = project["assistant"].ToString();
                                TextBoxSubcontractsDesc.Text = project["subcontracts"].ToString();
                                TextBoxIndirectCostsDesc.Text = project["IndirectCosts"].ToString();
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

                    //if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager))
                    if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager || userLogged.IsScientist || userLogged.IsStudent || userLogged.IsCoordinator || userLogged.IsDeveloper || userLogged.IsAdministrator || userLogged.IsProjectOfficer || userLogged.IsBudgetOfficer))
                    {

                        if (  userLogged.IsAdministrator || userLogged.IsProjectOfficer || userLogged.IsBudgetOfficer || userLogged.IsAESOfficer)
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

                        GenerateProjectProccessInfo(projectId);
                        //var strcommand = this.SqlDataSourceProjectProccessInfo.SelectCommand;
                        //if (GetProjectProccessInfo(out ProjectProccessInfoSet listset, projectId, strcommand, out Tuple<bool?, Exception> exceptionInfoForManager))
                        //{

                        //    internalSection = listset;

                        //    //if (SavePlayerAsManagerProject(projectID, managerID.Value))
                        //    //{
                        //    //    //var flag = true;
                        //    //}

                        //}

                        //else
                        //{
                        //    var errorMessage = "Error at try getting the proccess info for this project";
                        //    var builder = new System.Text.StringBuilder();

                        //    HandlerExeption(errorMessage, builder, exceptionInfoForManager);

                        //}
                        if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
                        {
                            GenerateProjectSections(rcID);

                            #region deprecated
                            //if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
                            //{
                            //    this.targetSections = tss;
                            //}
                            //else
                            //{
                            //    throw new Exception("Error at try getting the roster role sections capabilities in this project");
                            //}
                            #endregion

                            GenerateNotes(projectId);
                            GenerateSigns(projectId);
                            GenerateAssents(projectId);
                            GenerateObjetions(projectId);
                            GenerateStatus(projectId);
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

                //if (gvGradAss.HeaderRow != null)
                //    gvGradAss.HeaderRow.TableSection = TableRowSection.TableHeader;

                //if (gvLab.HeaderRow != null)
                //    gvLab.HeaderRow.TableSection = TableRowSection.TableHeader;

                //if (gvOP.HeaderRow != null)
                //    gvOP.HeaderRow.TableSection = TableRowSection.TableHeader;

                //if (gvP3BeInitiated.HeaderRow != null)
                //    gvP3BeInitiated.HeaderRow.TableSection = TableRowSection.TableHeader;

                //if (gvP3Progress.HeaderRow != null)
                //    gvP3Progress.HeaderRow.TableSection = TableRowSection.TableHeader;

                //if (gvSci.HeaderRow != null)
                //    gvSci.HeaderRow.TableSection = TableRowSection.TableHeader;


                //gvLab.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gvOP.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gvP3BeInitiated.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gvP3Progress.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gvSci.HeaderRow.TableSection = TableRowSection.TableHeader;


            }

            if (!Page.IsPostBack)
            {
                if (this.targetSections != null && this.targetSections.Count() > 0)
                {
                    var sections = this.targetSections.OrderBy(section => section.Value.numLine);
                    var defSection = sections.First();
                    TabSelectedIndex = $"{defSection.Value.numLine}";
                }
                else
                {
                    //TabSelectedIndex = "-1";
                    var stop = true;

                    if (stop)
                    { }
                }

                //TabSelectedIndex = "0";
                base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
                //this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated += GridView_RowUpdated;

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

                base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);

                //base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                ////this.gvModel.RowUpdated(sender:this, e: e);

                //this.gvModel.RowUpdated += GridView_RowUpdated;
            }

            //this.linkbutton1.Click += (exp, t) => {
            //    TabSelectedIndex = "1";
            //};

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a4;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);


                EvalGridPermission(section.Value, gvP3BeInitiated);
                EvalGridPermission(section.Value, gvP3Progress);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a5;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                EvalGridPermission(section.Value, gvLab);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a6;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                EvalGridPermission(section.Value, gvSci);

            }

            {
                var key = Eblue.Utils.ConstantsTools.tagier_project_section_a7;
                var section = this.targetSections.FirstOrDefault(t => t.Key == key);

                EvalGridPermission(section.Value, gvGradAss);
                EvalGridPermission(section.Value, gvOP);

            }

            {

                //SetControlFor(buttonNote, new ButtonInfoActionFilter() { Model = "Note", Flags = ActionFilterFlags.AddToConfirm });
            }

            {

                this.imagenSign.ImageUrl = GetRosterSignature();

            }

            {
                EvalStatusActions();

                this.gvOP.RowDeleted += (x, y) =>
                {

                    if (y.AffectedRows > 0)
                    {
                        Guid.TryParse(y.Keys[1].ToString(), out Guid rosterId);
                        var projectId = this.ProjectID.Value;
                        RemovePlayerProject(projectID: projectId, rosterID: rosterId);

                    }

                };

            }

        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            //base.OnSaveStateCompleteSentences
            base.OnSaveStateCompleteExtend(this);
        }

        #region viewstate properties

        public Eblue.Code.Models.ProjectProcess ProjectState
        {

            get => (HasViewState && ViewState["ProjectState"] is ProjectProcess pp) ? pp : new ProjectProcess();
            set => ((HasViewState && ViewState is StateBag vs) ? vs : new StateBag())["ProjectState"] = value;



        }

        public Eblue.Code.Models.ProjectRoleType ProjectRoleBlop
        {

            get => (HasViewState && ViewState["ProjectRoleBlop"] is ProjectRoleType pp) ? pp : new ProjectRoleType();
            set => ((HasViewState && ViewState is StateBag vs) ? vs : new StateBag())["ProjectRoleBlop"] = value;



        }


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
                TargetSectionSet result = new TargetSectionSet();

                if (HasViewState)
                {
                    result = this.ViewState["targetSections"] as TargetSectionSet;

                    if (result == null) result = new TargetSectionSet();
                }


                //Eblue.Code.TargetSectionSet result = new Code.TargetSectionSet();
                //object viewstateSession = this.ViewState;

                //if (viewstateSession != null)
                //{
                ////    //var stop = true;
                ////    result = new Code.TargetSectionSet();

                ////}
                ////else
                ////{
                //    result = this.ViewState["targetSections"] as Eblue.Code.TargetSectionSet;

                //}

                return result;

            }
            set
            {
                if (HasViewState)
                {
                    if (value == null) value = new TargetSectionSet();
                    this.ViewState["targetSections"] = value;


                }
                //object viewstateSession = this.ViewState;

                //if (viewstateSession == null)
                //{
                //    //var stop = true;

                //}
                //else
                //{
                //    this.ViewState["targetSections"] = value;

                //}
            }

        }

        public Eblue.Code.Models.ProjectProccessInfoSet internalSection
        {

            get
            {
                ProjectProccessInfoSet result = new ProjectProccessInfoSet();


                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    result = viewState["internalSection"] as ProjectProccessInfoSet;
                }

                return result;

            }
            set
            {
                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["internalSection"] = value;
                }

            }

        }
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

        public bool? MustBlockByCurrentProcessIsCloser
        {
            get
            {
                bool? result = default(bool?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var boolObject = viewState["MustBlockByCurrentProcessIsCloser"];

                    if (boolObject != null && boolObject is IConvertible)
                    {
                        IConvertible exp = boolObject as IConvertible;
                        if (exp != null)
                        {
                            result = exp.ToBoolean(null);

                        }
                        else
                        {
                            result = Convert.ToBoolean(boolObject);
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
                    viewState["MustBlockByCurrentProcessIsCloser"] = value;
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

        public int? AssentsQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["AssentsQuantity"];

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
                    viewState["AssentsQuantity"] = value;
                }


            }

        }

        public int? ObjetionsQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["ObjetionsQuantity"];

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
                    viewState["ObjetionsQuantity"] = value;
                }


            }

        }

        public int? StatusQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["StatusQuantity"];

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
                    viewState["StatusQuantity"] = value;
                }


            }

        }

        public int? MembersQuantity
        {
            get
            {
                int? result = default(int?);

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var intObject = viewState["MembersQuantity"];

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
                    viewState["MembersQuantity"] = value;
                }


            }

        }

        public new bool HasViewState
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
                string result = "-1";
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

        public string InnerProjectAssents
        {
            get
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var objectValue = viewState["InnerProjectAssents"];
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
                    viewState["InnerProjectAssents"] = value;

                }


            }

        }

        public string InnerProjectObjetions
        {
            get
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var objectValue = viewState["InnerProjectObjetions"];
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
                    viewState["InnerProjectObjetions"] = value;

                }


            }

        }

        public string InnerProjectStatus
        {
            get
            {
                var result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var objectValue = viewState["InnerProjectStatus"];
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
                    viewState["InnerProjectStatus"] = value;

                }


            }

        }
        #endregion

        #region viewstate methods

        public string GetPlayerCaption() => (string.IsNullOrEmpty(PlayerCaption)) ? "{noset}" : PlayerCaption;
        public string GetProjectNotes() => (string.IsNullOrEmpty(InnerProjectNotes)) ? "{no notes}" : InnerProjectNotes;
        public string GetProjectSigns() => (string.IsNullOrEmpty(InnerProjectSigns)) ? "{no signs}" : InnerProjectSigns;
        public string GetProjectAssents() => (string.IsNullOrEmpty(InnerProjectAssents)) ? "{no assents}" : InnerProjectAssents;
        public string GetProjectObjetions() => (string.IsNullOrEmpty(InnerProjectObjetions)) ? "{no objetions}" : InnerProjectObjetions;
        public string GetProjectStatus() => (string.IsNullOrEmpty(InnerProjectStatus)) ? "{no status}" : InnerProjectStatus;
        public int GetNotesQuantity() => (NotesQuantity == null) ? 0 : NotesQuantity.Value;
        public int GetSignsQuantity() => (SignsQuantity == null) ? 0 : SignsQuantity.Value;
        public int GetAssentsQuantity() => (AssentsQuantity == null) ? 0 : AssentsQuantity.Value;
        public int GetObjetionsQuantity() => (ObjetionsQuantity == null) ? 0 : ObjetionsQuantity.Value;
        public int GetMembersQuantity() => (MembersQuantity == null) ? 0 : MembersQuantity.Value;
        public Guid? GetProjectID() => ProjectID;

        #endregion




        protected void T1__TextChanged(object sender, System.EventArgs e)
        {
            var type = sender?.GetType();
            //Just assume 1Dollar = 45 Rupees  
            //T2.Value = (Int16.Parse(T1.Value) * 45).ToString();
        }



        #region ProjectState

        protected void GenerateProjectState(ProjectProccessInfo projectProccess, TargetSectionSet sections)
        {


            ProjectProcess prjProcess = new ProjectProcess();

            //var PrevDescription = projectProccess?.pre?.Item2;
            var currentProcc = new
            {
                IsStarter = projectProccess.availabledChecks.Item2,
                IsCloser = projectProccess.availabledChecks.Item3,
                ObjetionsAvailabled = projectProccess.availabledChecks.Item4,
                AssentsAvailabled = projectProccess.availabledChecks.Item5,
                IsUsedForDirectiveManager = projectProccess.enabledChecks.Item1,
                IsUsedForInvestigationOfficer = projectProccess.enabledChecks.Item2,
                IsUsedForAssistantLeader = projectProccess.enabledChecks.Item3,
                IsUsedForDirectiveLeader = projectProccess.enabledChecks.Item4,
                IsUsedForOnlyDirectiveManager = projectProccess.enabledChecks.Item5
            };
            var nextProc = new { UId = projectProccess?.NextProccess.Item1, Description = projectProccess?.NextProccess.Item2 };
            var alwaysProc = new { UId = projectProccess?.AlwaysProccess.Item1, Description = projectProccess?.AlwaysProccess.Item2 };

            #region commented
            //var nextDescription = projectProccess?.NextProccess?.Item2?.Trim();
            //var AlwayDescription = projectProccess?.AlwaysProccess?.Item2?.Trim();

            //if (!string.IsNullOrEmpty(nextDescription))
            //{
            //    this.ButtonNext.Visible = true;
            //    this.ButtonNext.Text = nextDescription;
            //}
            //else
            //{
            //    this.ButtonNext.Visible = false;

            //}

            //if (!string.IsNullOrEmpty(AlwayDescription))
            //{
            //    this.ButtonAlways.Visible = true;
            //    this.ButtonAlways.Text = AlwayDescription;
            //}
            //else
            //{
            //    this.ButtonAlways.Visible = false;

            //}
            #endregion




            var roleType = ProjectRoleBlop;
            prjProcess.isDM = roleType.isDM;
            prjProcess.isIO = roleType.isIO;
            prjProcess.isDL = roleType.isDL;
            prjProcess.isAL = roleType.isAL;

            prjProcess.isND = roleType.isND;

            prjProcess.nextUId = nextProc.UId;
            prjProcess.alwaysUId = alwaysProc.UId;

            prjProcess.isStarter = currentProcc.IsStarter;
            prjProcess.isCloser = currentProcc.IsCloser;

            prjProcess.objetionsAvailableds = currentProcc.ObjetionsAvailabled;
            prjProcess.assentsAvailableds = currentProcc.AssentsAvailabled;

            prjProcess.usedForDM = currentProcc.IsUsedForDirectiveManager;
            prjProcess.usedForIO = currentProcc.IsUsedForInvestigationOfficer;
            prjProcess.usedForDL = currentProcc.IsUsedForDirectiveLeader;
            prjProcess.usedForAL = currentProcc.IsUsedForAssistantLeader;
            prjProcess.onlyForDM = currentProcc.IsUsedForOnlyDirectiveManager;


            this.ProjectState = prjProcess;

            #region decition(s)
            var prj = prjProcess;

            var isCloser = (prj.isCloser & !prj.isStarter);

            this.MustBlockByCurrentProcessIsCloser = isCloser;

            if (isCloser)
            {
                foreach (var section in sections)
                {
                    section.Value.displayBlockDataSection = this.MustBlockByCurrentProcessIsCloser.Value;
                    section.Value.displayBlockListSection = this.MustBlockByCurrentProcessIsCloser.Value;

                    section.Value.dataCapEdit = !this.MustBlockByCurrentProcessIsCloser.Value;

                    section.Value.listCapAdd = !this.MustBlockByCurrentProcessIsCloser.Value;
                    section.Value.listCapEdit = !this.MustBlockByCurrentProcessIsCloser.Value;

                }
            }

            this.targetSections = sections;

            this.ButtonNext.Enabled = false;
            this.ButtonAlways.Enabled = false;

            this.ButtonObjetion.Enabled = false;
            this.ButtonAssent.Enabled = false;

            if (prj.displayIsNotDriver)
            {
                //TODO when generate project state and the roleBlop is not a driver for the momment -> do nothing

            }
            else
            {

                #region status decition
                //TODO when generate project state and the roleBlop is a driver for the momment in status tab disabled buttons, but in production hide the buttons
                //used for -> dm, io, dl
                var showNext = prj.displayShowNextButton;
                var showAlways = prj.displayShowAlwaysButton;
                var usedForDM = prj.usedForDM;
                var usedForDL = prj.usedForDL;
                var usedForAL = prj.usedForAL;
                var usedForIO = prj.usedForIO;
                var usedForOnlyDM = prj.onlyForDM;

                if (prj.isDM)
                {

                    this.ButtonNext.Enabled = (usedForDM || usedForOnlyDM) & showNext;
                    this.ButtonAlways.Enabled = (usedForDM || usedForOnlyDM) & showAlways;

                    //(!prj.isAL & (showAlways & (usedForDM || usedForIO || usedForDL))) & (prj.isDM || prj.isDL || prj.isIO);

                }

                if (prj.isIO)
                {
                    this.ButtonNext.Enabled = (usedForIO) & showNext;
                    this.ButtonAlways.Enabled = (usedForIO) & showAlways;

                }

                if (prj.isDL)
                {
                    this.ButtonNext.Enabled = (usedForDL) & showNext;
                    this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }




                //this.ButtonNext.Enabled = (!prj.isAL & (showNext & (usedForDM || usedForIO || usedForDL) )) & (prj.isDM || prj.isDL || prj.isIO) ;
                //this.ButtonAlways.Enabled = (!prj.isAL & (showAlways & (usedForDM || usedForIO || usedForDL))) & (prj.isDM || prj.isDL || prj.isIO)  ;
                #endregion

                #region objetions decition
                //TODO when generate project state and the roleBlop is a driver for the momment in objetions tab disabled buttons, but in production hide the buttons
                //used for -> al
                var enabledObjetions = prj.displayObjetionsAvailableds;
                //this.ButtonObjetion.Enabled = (prj.isAL & enabledObjetions & usedForAL) & !(prj.isDM || prj.isDL || prj.isIO) ;

                if (prj.isAL)
                {
                    this.ButtonObjetion.Enabled = (usedForAL) & enabledObjetions;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                if (prj.isDM)
                {
                    this.ButtonObjetion.Enabled = (usedForDM | usedForOnlyDM) & enabledObjetions;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                if (prj.isIO)
                {
                    this.ButtonObjetion.Enabled = (usedForIO) & enabledObjetions;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                if (prj.isDL)
                {
                    this.ButtonObjetion.Enabled = (usedForDL) & enabledObjetions;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }
                #endregion

                #region assents decition
                //TODO when generate project state and the roleBlop is a driver for the momment in assents tab disabled buttons, but in production hide the buttons
                var enabledAssents = prj.displayAssentsAvailableds;
                //this.ButtonAssent.Enabled = (prj.isAL & enabledAssents & usedForAL) & !(prj.isDM || prj.isDL || prj.isIO); 
                if (prj.isAL)
                {
                    this.ButtonAssent.Enabled = (usedForAL) & enabledAssents;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                if (prj.isDM)
                {
                    this.ButtonAssent.Enabled = (usedForDM | usedForOnlyDM) & enabledAssents;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                if (prj.isIO)
                {
                    this.ButtonAssent.Enabled = (usedForIO) & enabledAssents;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }
                if (prj.isDL)
                {
                    this.ButtonAssent.Enabled = (usedForDL) & enabledAssents;
                    //this.ButtonNext.Enabled = (usedForDL) & showNext;
                    //this.ButtonAlways.Enabled = (usedForDL) & showAlways;

                }

                #endregion

            }


            #endregion




        }
        #endregion

        #region RoleBlop
        protected void GenerateProjectRoleBlop(Guid roleCategoryID)
        {

            bool result;

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


        }
        #endregion

        #region Player
        protected bool GeneratePlayerInfoSet(Guid projectId, Guid rosterId)
        {

            bool result = false;

            if (GetProjectPlayersInfo(out PlayerInfoSet listset, projectId))
            {
                result = true;
                int? membersQuatity = 0;


                if (listset != null && listset.Count > 0)
                {
                    //PlayerInfoSet = listset;

                    var player = listset.FirstOrDefault(model => model.RosterID == rosterId);

                    this.PlayerCaption = player?.Caption;
                    this.PlayerRoleId = player?.RoleId;
                    membersQuatity = listset.Count;

                }
                else
                {
                    //PlayerInfoSet = new PlayerInfoSet();

                }

                this.MembersQuantity = membersQuatity;



            }

            return result;
        }

        protected bool GetProjectPlayersInfo(out PlayerInfoSet listset, Guid projectID, Guid rosterID)
        {
            bool result = false;
            listset = new PlayerInfoSet();
            var list = new PlayerInfoSet();

            var stringCommandBasic = SqlDataSourcePlayerProject.SelectCommand;
            var stringCommand = String.Concat(stringCommandBasic, " and pp.RosterID = @RosterID");



            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@ProjectID", projectID), new SqlParameter("@RosterID", rosterID))
            {
                commandString = stringCommand
            };

            result = FetchDataFirstOrDefault(reqInfo, reader =>
            {


                int.TryParse(reader["roleid"]?.ToString(), out int roleID);
                var roleName = reader["rolename"]?.ToString();
                Guid.TryParse(reader["rolecategoryid"]?.ToString(), out Guid roletypeID);
                var roletypeDescription = reader["roletypedescription"]?.ToString();
                var caption = reader["roleCaption"]?.ToString();
                //Guid.TryParse(reader["rosterid"]?.ToString(), out Guid rosterUID);


                var model = new PlayerInfo(roleID, roleName, roletypeID, roletypeDescription, caption, rosterID);

                list.Add(model);
            });

            listset = list;

            return result;

        }

        protected bool GetProjectPlayersInfo(out PlayerInfoSet listset, Guid projectID)
        {
            bool result = false;
            listset = new PlayerInfoSet();
            var list = new PlayerInfoSet();

            var stringCommand = SqlDataSourcePlayerProject.SelectCommand;

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(
                new SqlParameter("@ProjectID", projectID))
            {
                commandString = stringCommand
            };

            result = FetchData(reqInfo, reader =>
            {


                int.TryParse(reader["roleid"]?.ToString(), out int roleID);
                var roleName = reader["rolename"]?.ToString();
                Guid.TryParse(reader["rolecategoryid"]?.ToString(), out Guid roletypeID);
                var roletypeDescription = reader["roletypedescription"]?.ToString();
                var caption = reader["roleCaption"]?.ToString();
                Guid.TryParse(reader["rosterid"]?.ToString(), out Guid rosterID);


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

            result = FetchData(reqInfo, reader =>
            {

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
        protected void ButtonNoteAdd_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var noteData = this.TextBoxNote.Text?.Trim();
            DateTime noteDate = DateTime.Now;

            if (AddNoteProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: noteDate, noteData: noteData, rosterData: rosterPicture))
            {

                this.TextBoxNote.Text = string.Empty;
                GenerateNotes(projectID);

            }


            //var pIDString = Request.QueryString["PID"].ToString();

            //bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

            //var pIDString = Request.QueryString["PID"].ToString();
            //var roleString = DropDownListRole.SelectedValue;

            //int.TryParse(roleString, out int roleID);
            //Guid.TryParse(rosterString, out Guid rosterID);
            //bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);


            //if () { }


        }
        public bool AddNoteProject(Guid projectID, Guid rosterID, int roleID, DateTime noteDate, string noteData, string rosterData)
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
                     new SqlParameter("@NoteData", noteData),
                     new SqlParameter("@NoteDate", noteDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = "insert into projectNotes (UId, ProjectID, RosterID, RoleId, NoteData, NoteDate, RosterData , ResponseNoteID) " +
                    "values (@UId, @ProjectID, @RosterID, @RoleId, @NoteData, @NoteDate, @RosterData , null) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add note to the project", ex);
            }

            return result;

        }
        #endregion


        #region Assents
        protected void GenerateAssents(Guid projectId)
        {



            if (GetProjectAssents(out ProjectNoteSet notes, projectId))
            {

                AssentsQuantity = notes.Count;

                if (notes != null && notes.Count > 0)
                {
                    //renderNotes

                    if (GenerateAssentsTextFor(out string innerText, notes, true))
                    {

                        notes.InnerText = innerText;
                        InnerProjectAssents = innerText;
                    }

                }




            }
        }
        protected bool GetProjectAssents(out ProjectNoteSet list, Guid projectID)
        {
            bool result = false;
            list = default(ProjectNoteSet);
            var listset = new ProjectNoteSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(new SqlParameter("@ProjectID", projectID))
            {
                commandString = SqlDataSourceProjectAssentsSelect.SelectCommand
            };

            result = FetchData(reqInfo, reader =>
            {

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                DateTime.TryParse(reader["assentDate"]?.ToString(), out DateTime signDate);
                var signData = reader["assentData"]?.ToString();
                signData = (string.IsNullOrEmpty(signData)) ? UserGenericSignData : signData;
                var rosterName = reader["rosterName"]?.ToString();
                var rosterPicture = reader["rosterData"]?.ToString();
                rosterPicture = (string.IsNullOrEmpty(rosterPicture)) ? UserGenericPictureData : rosterPicture;

                var projectSign = new ProjectNote(rowNumber, Uid, signDate, signData, rosterName, rosterPicture, 0);

                listset.Add(projectSign);
            });

            list = listset;

            return result;

        }
        public bool GenerateAssentsTextFor(out string output, ProjectNoteSet container, bool? useSameBackcolor = null)
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
                            //var classPicture = "direct-chat-img";
                            var classPicture = string.Empty;
                            var divBody = new HtmlGenericControl("div") { InnerText = $"{note.NoteData}" };
                            var classNote = "direct-chat-text";
                            //var classNote = string.Empty;
                            var classRight = (useSameBackcolor != null && useSameBackcolor.Value) ? " right" : string.Empty;

                            if (note.RowNumber % 2 != 0)
                            {
                                classPicture = "direct-chat-img";
                                classContainer = "direct-chat-msg";
                                //classContainer = $"direct-chat-msg{classRight}";
                                classRoster = "direct-chat-name float-left";
                                classDayTime = "direct-chat-timestamp float-right";
                                classHeader = "direct-chat-info clearfix";
                                //classNote = "direct-chat-text-assent";
                            }
                            else
                            {
                                classPicture = "direct-chat-img";
                                classContainer = "direct-chat-msg right";
                                classRoster = "direct-chat-name float-right";
                                classDayTime = "direct-chat-timestamp float-left";
                                classHeader = "direct-chat-info clearfix";
                                //classNote = "direct-chat-text";
                            }

                            divContainer.Attributes.Add("class", $"{classContainer}");

                            spanRoster.Attributes.Add("class", $"{classRoster}");
                            spanDayTime.Attributes.Add("class", $"{classDayTime}");

                            img.Attributes.Add("class", $"{classPicture}");
                            img.Attributes.Add("src", $"{note.RosterPicture}");

                            if (note.RowNumber % 2 != 0)
                            {
                                //img.Attributes.Add("style", $"float: left !important");
                            }
                            else
                            {

                            }

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
        //protected void ButtonAssentAdd_Click(object sender, EventArgs e)
        //{

        //    var userLogged = Eblue.Utils.SessionTools.UserInfo;

        //    var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

        //    Guid rosterId = userLogged.RosterId;
        //    string rosterPicture = userLogged.RosterPicture;
        //    Guid projectID = Guid.Empty;

        //    if (this.ProjectID != null) projectID = this.ProjectID.Value;

        //    var noteData = this.TextBoxAssent.Text?.Trim();
        //    DateTime noteDate = DateTime.Now;

        //    if (AddNoteProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: noteDate, noteData: noteData, rosterData: rosterPicture))
        //    {

        //        this.TextBoxNote.Text = string.Empty;
        //        GenerateNotes(projectID);

        //    }


        //    //var pIDString = Request.QueryString["PID"].ToString();

        //    //bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

        //    //var pIDString = Request.QueryString["PID"].ToString();
        //    //var roleString = DropDownListRole.SelectedValue;

        //    //int.TryParse(roleString, out int roleID);
        //    //Guid.TryParse(rosterString, out Guid rosterID);
        //    //bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);


        //    //if () { }


        //}
        public bool AddAssentProject(Guid projectID, Guid rosterID, int roleID, DateTime noteDate, string noteData, string rosterData)
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
                     new SqlParameter("@AssentData", noteData),
                     new SqlParameter("@AssentDate", noteDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = "insert into projectAssents (UId, ProjectID, RosterID, RoleId, AssentData, AssentDate, RosterData) " +
                    "values (@UId, @ProjectID, @RosterID, @RoleId, @AssentData, @AssentDate, @RosterData) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add assent to the project", ex);
            }

            return result;

        }
        #endregion

        #region Objetions
        protected void GenerateObjetions(Guid projectId)
        {



            if (GetProjectObjetions(out ProjectNoteSet notes, projectId))
            {

                ObjetionsQuantity = notes.Count;

                if (notes != null && notes.Count > 0)
                {
                    //renderNotes

                    if (GenerateObjetionsTextFor(out string innerText, notes, true))
                    {

                        notes.InnerText = innerText;
                        InnerProjectObjetions = innerText;
                    }

                }




            }
        }
        protected bool GetProjectObjetions(out ProjectNoteSet list, Guid projectID)
        {
            bool result = false;
            list = default(ProjectNoteSet);
            var listset = new ProjectNoteSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(new SqlParameter("@ProjectID", projectID))
            {
                commandString = SqlDataSourceProjectObjetionsSelect.SelectCommand
            };

            result = FetchData(reqInfo, reader =>
            {

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                DateTime.TryParse(reader["objetionDate"]?.ToString(), out DateTime signDate);
                var signData = reader["objetionData"]?.ToString();
                signData = (string.IsNullOrEmpty(signData)) ? UserGenericSignData : signData;
                var rosterName = reader["rosterName"]?.ToString();
                var rosterPicture = reader["rosterData"]?.ToString();
                rosterPicture = (string.IsNullOrEmpty(rosterPicture)) ? UserGenericPictureData : rosterPicture;

                var projectSign = new ProjectNote(rowNumber, Uid, signDate, signData, rosterName, rosterPicture, 0);

                listset.Add(projectSign);
            });

            list = listset;

            return result;

        }
        public bool GenerateObjetionsTextFor(out string output, ProjectNoteSet container, bool? useSameBackcolor = null)
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

                            var classRight = (useSameBackcolor != null && useSameBackcolor.Value) ? " right" : string.Empty;



                            if (note.RowNumber % 2 != 0)
                            {
                                classContainer = "direct-chat-msg";
                                //classContainer = $"direct-chat-msg{classRight}";
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
        protected void ButtonObjetionAdd_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var noteData = this.TextBoxObjetion.Text?.Trim();
            DateTime noteDate = DateTime.Now;

            if (AddObjetionProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: noteDate, noteData: noteData, rosterData: rosterPicture))
            {

                this.TextBoxObjetion.Text = string.Empty;
                GenerateObjetions(projectID);

            }


            //var pIDString = Request.QueryString["PID"].ToString();

            //bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

            //var pIDString = Request.QueryString["PID"].ToString();
            //var roleString = DropDownListRole.SelectedValue;

            //int.TryParse(roleString, out int roleID);
            //Guid.TryParse(rosterString, out Guid rosterID);
            //bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);


            //if () { }


        }
        public bool AddObjetionProject(Guid projectID, Guid rosterID, int roleID, DateTime noteDate, string noteData, string rosterData)
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
                     new SqlParameter("@ObjetionData", noteData),
                     new SqlParameter("@ObjetionDate", noteDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = "insert into projectObjetions (UId, ProjectID, RosterID, RoleId, ObjetionData, ObjetionDate, RosterData) " +
                    "values (@UId, @ProjectID, @RosterID, @RoleId, @ObjetionData, @ObjetionDate, @RosterData) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add objetion to the project", ex);
            }

            return result;

        }
        #endregion


        #region Sections
        protected void GenerateProjectSections(Guid roleCategoryID)
        {

            bool result = false;

            result = GetProjectPermissionsByRoleHandle(out TargetSectionSet model, roleCategoryID);

            if (result)
            {
                this.targetSections = model;

                //TODO include generate roleBlop
                GenerateProjectRoleBlop(roleCategoryID);
            }
            else
            {
                var stop = true;

                if (stop)
                { }

            }

            //if (GetSectionByRolePermission(out Eblue.Code.TargetSectionSet listset, roleCategoryID, out Tuple<bool?, Exception> exceptionX))
            //{

            //    this.targetSections = listset;

            //}

            //else
            //{
            //    var errorMessage = "Error at try getting the sections for this role in this project";
            //    var builder = new System.Text.StringBuilder();

            //    HandlerExeption(errorMessage, builder, exceptionX);

            //}
        }
        #endregion

        #region internal
        protected void GenerateProjectProccessInfo(Guid projectId)
        {
            var strcommand = this.SqlDataSourceProjectProccessInfo.SelectCommand;
            if (GetProjectProccessInfo(out ProjectProccessInfoSet listset, projectId, strcommand, out Tuple<bool?, Exception> exceptionX))
            {

                internalSection = listset;

                //if (SavePlayerAsManagerProject(projectID, managerID.Value))
                //{
                //    //var flag = true;
                //}

            }

            else
            {
                var errorMessage = "Error at try getting the proccess info for this project";
                var builder = new System.Text.StringBuilder();

                HandlerExeption(errorMessage, builder, exceptionX);

            }
        }
        #endregion

        #region Status

        protected void EvalStatusActions()
        {


            //var projectProccess = this.internalSection?.FirstOrDefault(section => section.availabledChecks.Item1);
            var projectProccess = this.internalSection?.LastOrDefault();
            bool hasProjectProccess = !(projectProccess == null);

            if (hasProjectProccess)
            {

                var nextDescription = projectProccess?.NextProccess?.Item2?.Trim();
                var AlwayDescription = projectProccess?.AlwaysProccess?.Item2?.Trim();

                if (!string.IsNullOrEmpty(nextDescription))
                {
                    this.ButtonNext.Visible = true;
                    this.ButtonNext.Text = nextDescription;
                }
                else
                {
                    this.ButtonNext.Visible = false;

                }

                if (!string.IsNullOrEmpty(AlwayDescription))
                {
                    this.ButtonAlways.Visible = true;
                    this.ButtonAlways.Text = AlwayDescription;
                }
                else
                {
                    this.ButtonAlways.Visible = false;

                }


                var sections = this.targetSections;
                if (sections != null && sections.Count > 0)
                {
                    GenerateProjectState(projectProccess, sections);

                }





            }



        }
        protected void GenerateStatus(Guid projectId)
        {



            if (GetProjectStatus(out ProjectNoteSet Status, projectId))
            {

                StatusQuantity = Status.Count;

                if (Status != null && Status.Count > 0)
                {
                    //renderStatus

                    if (GenerateStatusTextFor(out string innerText, Status))
                    {

                        Status.InnerText = innerText;
                        InnerProjectStatus = innerText;
                    }

                }




            }
        }
        protected bool GetProjectStatus(out ProjectNoteSet status, Guid projectID)
        {
            bool result = false;
            status = default(ProjectNoteSet);
            var listset = new ProjectNoteSet();

            Utils.RequestDataInfo reqInfo = new Utils.RequestDataInfo(new SqlParameter("@ProjectID", projectID))
            {
                commandString = SqlDataSourceProjectStatusSelect.SelectCommand
            };

            result = FetchData(reqInfo, reader =>
            {

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                Guid.TryParse(reader["UId"]?.ToString(), out Guid Uid);
                DateTime.TryParse(reader["statusDate"]?.ToString(), out DateTime signDate);
                var signData = reader["statusData"]?.ToString();
                signData = (string.IsNullOrEmpty(signData)) ? UserGenericSignData : signData;
                var rosterName = reader["rosterName"]?.ToString();
                var rosterPicture = reader["rosterData"]?.ToString();
                rosterPicture = (string.IsNullOrEmpty(rosterPicture)) ? UserGenericPictureData : rosterPicture;

                var projectSign = new ProjectNote(rowNumber, Uid, signDate, signData, rosterName, rosterPicture, 0);

                listset.Add(projectSign);
            });

            status = listset;

            return result;

        }
        public bool GenerateStatusTextFor(out string output, ProjectNoteSet container)
        {
            bool result = false;
            output = string.Empty;

            bool isFirstStatus = true;
            var hrDivider = new HtmlGenericControl("hr");
            //#6610f2   #bbb;
            hrDivider.Attributes.Add("style", "border-top: 1px solid #6610f2;");

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

                            if (isFirstStatus)
                            {
                                //divContainer.Controls.Add(hrDivider);
                                //divBody.Attributes.Add("style", "background: #6610f2; border-color:#6610f2");
                            }

                            divContainer.Controls.Add(divHeader);
                            divContainer.Controls.Add(img);
                            divContainer.Controls.Add(divBody);

                            if (isFirstStatus)
                            {
                                divContainer.Controls.Add(hrDivider);
                                isFirstStatus = false;
                            }


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
        //protected void ButtonStatusAdd_Click(object sender, EventArgs e)
        //{

        //    var userLogged = Eblue.Utils.SessionTools.UserInfo;

        //    var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

        //    Guid rosterId = userLogged.RosterId;
        //    string rosterPicture = userLogged.RosterPicture;
        //    Guid projectID = Guid.Empty;

        //    if (this.ProjectID != null) projectID = this.ProjectID.Value;

        //    var noteData = this.TextBoxNote.Text?.Trim();
        //    DateTime noteDate = DateTime.Now;

        //    if (AddStatusProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: noteDate, noteData: noteData, rosterData: rosterPicture))
        //    {

        //        this.TextBoxNote.Text = string.Empty;
        //        GenerateStatus(projectID);

        //    }


        //    var pIDString = Request.QueryString["PID"].ToString();

        //    bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

        //    var pIDString = Request.QueryString["PID"].ToString();
        //    var roleString = DropDownListRole.SelectedValue;

        //    int.TryParse(roleString, out int roleID);
        //    Guid.TryParse(rosterString, out Guid rosterID);
        //    bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);


        //    if () { }


        //}

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

            result = FetchData(reqInfo, reader =>
            {

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

        protected void ButtonSignAdd_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            if (AddSignProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, signDate: signDate, signData: signData, rosterData: rosterPicture))
            {

                this.imagenSign.ImageUrl = GetRosterSignature();
                GenerateSigns(projectID);

            }




        }

        public bool AddSignProject(Guid projectID, Guid rosterID, int roleID, DateTime signDate, string signData, string rosterData)
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
                     new SqlParameter("@SignData", signData),
                     new SqlParameter("@SignDate", signDate),
                     new SqlParameter("@RosterData", rosterData)
                    )
                {
                    commandString = "insert into projectSigns (UId, ProjectID, RosterID, RoleId, SignData, SignDate, RosterData) " +
                    "values (@UId, @ProjectID, @RosterID, @RoleId, @SignData, @SignDate, @RosterData) "
                };

                result = ExecuteOnly(out int affectedRows, reqInfo);
                result &= affectedRows > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while trying to add sign to the project", ex);
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
           Guid rostercategoryID = Guid.Empty;
           

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();


                    SqlCommand cmdCommand2 = new SqlCommand(
                        $"select pp.RosterID, pp.RoleID, pp.ProjectID, r.RoleName, r.Enable as RoleEnable, r.Level as RoleLevel, " +
                        $"r.OrderLine as RoleOrderLine, rc.UId RoleTypeID, rc.Name RoleTypeName, rc.Description RoleTypeDescription,  " +
                        $"rc.OrderLine as RoleTypeOrderLine from playerProject pp inner join roles r on r.RoleID = pp.RoleID " +
                        $"inner join RoleCategory rc on rc.UId = r.RoleCategoryId where pp.ProjectID = '{projectId}' and pp.RosterId = '{rosterID}' ", cn);


                    SqlCommand cmdCommand = new SqlCommand(
                        $"select pp.RosterID, pp.RoleID, pp.ProjectID, r.RoleName, r.Enable as RoleEnable, r.Level as RoleLevel, " +
                        $"r.OrderLine as RoleOrderLine, rc.UId RoleTypeID, rc.Name RoleTypeName, rc.Description RoleTypeDescription,  " +
                        $"rc.OrderLine as RoleTypeOrderLine from playerProject pp inner join roles r on r.RoleID = pp.RoleID " +
                        $"inner join RoleCategory rc on rc.UId = r.RoleCategoryId  inner join Roster rr on rr.RosterID = pp.RosterID where pp.ProjectID = '{projectId}' and rr.RosterId = '{rosterID}' ", cn);



                    //SqlCommand sqlCommand = new SqlCommand(cn);

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



        protected bool EditPlayerProject(Guid projectID, Guid rosterOldID, Guid rosterNewID, int RoleID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string updateCommand =
                    $"update playerProject set rosterID = '{rosterNewID}', RoleId = {RoleID} where ProjectID = '{projectID}' and RosterID = '{rosterOldID}' ";

                SqlCommand cmd = new SqlCommand(updateCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to edit roster in the project", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool RemovePlayerProject(Guid projectID, Guid rosterID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string deleteCommand =
                    $"delete from playerProject where ProjectID = '{projectID}' and RosterID = '{rosterID}'";

                SqlCommand cmd = new SqlCommand(deleteCommand, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to remove roster in the project", ex);
            }
            finally
            {

            }

            return result;

        }





        //protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    string guidviewId = e.Row.Parent.ID;



        //}


        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        protected void SaveChangesSection_a1_Click(object sender, EventArgs e)
        {
            //var stop = true;
            //SaveChanges();
        }

        public void SaveChanges()
        {

            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string UpdateProject = "Update Projects SET " +
                        "Objectives = @Objectives, " +
                        "ObjWorkPlan = @ObjWorkPlan, " +
                        "PresentOutlook = @PresentOutlook, " +
                        "ProjectTitle = @ProjectTitle, " +
                        "ProgramAreaID = @ProgramAreaID, DepartmentID = @DepartmentID, " +
                        "CommId = @CommId," +
                        "WP1FieldWork = @WP1FieldWork, " +
                        "WorkPlan2Desc = @WorkPlan2Desc, " +
                        "ResultsAvailable = @ResultsAvailable, " +
                        "Facilities = @Facilities," +
                        "Impact = @Impact," +
                        "Salaries = @Salaries," +
                        "Materials = @Materials," +
                        "Equipment = @Equipment," +
                        "Travel = @Travel," +
                        "Abroad = @Abroad," +
                        "Others= @Others," +
                        "Wages= @Wages," +
                        "Benefits= @Benefits," +
                        "Assistant= @Assistant," +
                        "Subcontracts= @Subcontracts," +
                        "IndirectCosts= @IndirectCosts," +
                        "ContractNumber= @ContractNumber," +
                        "ORCID= @ORCID," +
                        "WorkPlan2 = @WorkPlan2 " +
                        "where ProjectID = @ProjectID";

                    SqlCommand cmd = new SqlCommand(UpdateProject, cn);


                    cmd.Parameters.AddWithValue("@Wages", TextBoxWagesDesc.Text);
                    cmd.Parameters.AddWithValue("@Benefits", TextBoxBenefitsDesc.Text);
                    cmd.Parameters.AddWithValue("@Assistant", TextBoxAssistantshipsDesc.Text);
                    cmd.Parameters.AddWithValue("@Subcontracts", TextBoxSubcontractsDesc.Text);
                    cmd.Parameters.AddWithValue("@IndirectCosts", TextBoxIndirectCostsDesc.Text);

                    cmd.Parameters.AddWithValue("@ContractNumber", TextBoxContractNumber.Text);
                    cmd.Parameters.AddWithValue("@ORCID", TextBoxORCID.Text);

                    cmd.Parameters.AddWithValue("@ProjectTitle", LabelProjectShortTitleResult.Text);
                    cmd.Parameters.AddWithValue("@ProgramAreaID", DropDownListProgramaticArea.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepartmentID", DropDownListDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@CommId", DropDownListCommodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubStationID", DropDownListSubstation.SelectedValue);

                    cmd.Parameters.AddWithValue("@Objectives", TextBoxProjectObjectiveToFill.Text);
                    cmd.Parameters.AddWithValue("@ObjWorkPlan", TextBoxobjectivefortheyear.Text);
                    cmd.Parameters.AddWithValue("@PresentOutlook", TextBoxWorkAccomplished.Text);

                    cmd.Parameters.AddWithValue("@WP1FieldWork", TextBoxFieldWork.Text);
                    cmd.Parameters.AddWithValue("@WorkPlan2Desc", TextBoxDescription.Text);
                    cmd.Parameters.AddWithValue("@ResultsAvailable", TextBoxEstimatedTime.Text);
                    cmd.Parameters.AddWithValue("@Facilities", TextBoxFacilitieNeeded.Text);

                    cmd.Parameters.AddWithValue("@Impact", TextBoxProjectImpact.Text);

                    cmd.Parameters.AddWithValue("@Salaries", TextBoxSalariesDesc.Text);
                    cmd.Parameters.AddWithValue("@Materials", TextBoxMaterialDesc.Text);
                    cmd.Parameters.AddWithValue("@Equipment", TextBoxEquipmentDesc.Text);
                    cmd.Parameters.AddWithValue("@Travel", TextBoxTravelDesc.Text);
                    cmd.Parameters.AddWithValue("@Abroad", TextBoxAbroadDesc.Text);
                    cmd.Parameters.AddWithValue("@Others", TextBoxOthersDesc.Text);
                    cmd.Parameters.AddWithValue("@WorkPlan2", TextBoxWorkPlanned2.Text);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;
                }
                catch (SqlException ex)
                {
                    //Message.Text = "opps it happen again" + ex;
                    //ErrorMessage.Visible = true;
                    //LabelInfo.Visible = false;
                }
            }

        }

        protected void ButtonWorkInproges_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonWorkInprogresInitiated_Click(object sender, EventArgs e)
        {

            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string FieldWork_Insert = "Insert into FieldWork(ProjectID, LocationID, Area, dateStarted, dateEnded, Inprogress, ToBeInitiated ) values ( " +
                        "@ProjectID, " +
                        "@LocationID, " +
                        "@Area, " +
                        "@dateStarted, " +
                        "@dateEnded, " +
                        "@Inprogress, " +
                        "@ToBeInitiated " +
                        ")";

                    SqlCommand cmdFieldWork = new SqlCommand(FieldWork_Insert, cn);

                    cmdFieldWork.Parameters.AddWithValue("@LocationID", DropDownListLocation.SelectedValue);
                    cmdFieldWork.Parameters.AddWithValue("@Area", fldWork_Area.Text);
                    cmdFieldWork.Parameters.AddWithValue("@dateStarted", TextBoxStart.Text);
                    cmdFieldWork.Parameters.AddWithValue("@dateEnded", TextBoxEnd.Text);
                    cmdFieldWork.Parameters.AddWithValue("@Inprogress", ChkInProgress.Checked);
                    cmdFieldWork.Parameters.AddWithValue("@ToBeInitiated", ChkInitiated.Checked);

                    cmdFieldWork.Parameters.AddWithValue("@ProjectID", pID);
                    cmdFieldWork.ExecuteNonQuery();
                    cmdFieldWork.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;


                    ChkInitiated.Checked = false;
                    ChkInProgress.Checked = false;
                    fldWork_Area.Text = "";
                    TextBoxStart.Text = "";
                    TextBoxEnd.Text = "";

                    gvP3Progress.DataBind();
                    gvP3BeInitiated.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    //Message.Text = "opps it happen again" + ex;
                    ////ErrorMessage.Visible = true;
                    ////LabelInfo.Visible = false;
                }
            }

        }

        protected void ButtonNewLaboratory_Click(object sender, EventArgs e)
        {

            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into Laboratory(ProjectID, AReq, NoSamples, SamplesDate) values ( " +
                        "@ProjectID, " +
                        "@AReq, " +
                        "@NoSamples, " +
                        "@SamplesDate " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    cmd.Parameters.AddWithValue("@AReq", TextBoxAnalisysRequired.Text);
                    cmd.Parameters.AddWithValue("@NoSamples", TextBoxNumSample.Text);
                    cmd.Parameters.AddWithValue("@SamplesDate", TextBoxProDate.Text);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    ////InfoMessage.Visible = true;
                    ////LabelInfo.Visible = true;
                    ////ErrorMessage.Visible = false;

                    TextBoxAnalisysRequired.Text = "";
                    TextBoxNumSample.Text = "";
                    TextBoxProDate.Text = "";

                    gvLab.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    //Message.Text = "opps it happen again" + ex;
                    ////ErrorMessage.Visible = true;
                    ////LabelInfo.Visible = false;
                }
            }

        }

        protected void ButtonNewScientest_Click(object sender, EventArgs e)
        {
            string rosterString = DropDownListScientest.SelectedValue;
            var pIDString = Request.QueryString["PID"].ToString();
            var roleString = DropDownListRole.SelectedValue;

            int.TryParse(roleString, out int roleID);
            Guid.TryParse(rosterString, out Guid rosterID);
            bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);
            bool hasRosterAdded = false;

            if (isValidProjectID && AddPlayerProject(pID, rosterID, roleID))
            {
                hasRosterAdded = true;


            }


            if (isValidProjectID && hasRosterAdded)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into SciProjects(ProjectID, RosterID, Role, TR, CA, AH) values ( " +
                        "@ProjectID, " +
                        "@RosterID, " +
                        "@Role, " +
                        "@TR, " +
                        "@CA, " +
                        "@AH " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    double.TryParse(txtTR.Text, out double TR);
                    double.TryParse(txtCA.Text, out double CA);
                    double.TryParse(txtHA.Text, out double AH);

                    cmd.Parameters.AddWithValue("@RosterID", rosterID);
                    cmd.Parameters.AddWithValue("@Role", roleID);
                    cmd.Parameters.AddWithValue("@TR", TR);
                    cmd.Parameters.AddWithValue("@CA", CA);
                    cmd.Parameters.AddWithValue("@AH", AH);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;

                    txtTR.Text = "";
                    txtCA.Text = "";
                    txtHA.Text = "";

                    gvSci.DataBind();

                    SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception("Error while trying to add scientist to the project", ex);
                    //Message.Text = "opps it happen again" + ex;
                    //ErrorMessage.Visible = true;
                    //LabelInfo.Visible = false;
                }
            }

        }

        //protected void NewPersonelRole_Click(object sender, EventArgs e)
        //{
        //    var userId = Eblue.Utils.SessionTools.UserId;

        //    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
        //    try
        //    {
        //        cn.Open();
        //        string Command_Insert = "insert into roles(RoleName, Enable, UID, OrderLine, Level) values ( " +
        //            "@RoleName, " +
        //            "@Enable, " +
        //            "@UID, " +
        //            "@OrderLine, " +
        //            "@Level)";

        //        SqlCommand cmd = new SqlCommand(Command_Insert, cn);

        //        cmd.Parameters.AddWithValue("@RoleName", TextBoxPersonnelRole.Text);
        //        cmd.Parameters.AddWithValue("@Enable", 1);
        //        cmd.Parameters.AddWithValue("@UID",userId);
        //        cmd.Parameters.AddWithValue("@OrderLine", 0);
        //        cmd.Parameters.AddWithValue("@Level", 0);

        //        cmd.ExecuteNonQuery();
        //        cmd.Dispose();

        //        cn.Close();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        protected void ButtonOtherPersonal_Click(object sender, EventArgs e)
        {

            string rosterString = TextBoxPersonnel.Text;
            //var pIDString = Request.QueryString["PID"].ToString();
            var roleString = TextBoxOtherPersonal.Text;

            int.TryParse(roleString, out int roleID);
            Guid.TryParse(rosterString, out Guid rosterID);
            //bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);
            bool hasRosterAdded = false;
            var pID = this.ProjectID.Value;
            if (AddPlayerProject(pID, rosterID, roleID))
            {
                hasRosterAdded = true;


            }


            if (hasRosterAdded)
            {

                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into OtherPersonel(ProjectID, Name, PerTime, LocationID, PersonnellManAdded, RoleNameManAdded) values ( " +
                        "@ProjectID, " +
                        "@Name, " +
                        "@PerTime, " +
                        "@LocationID," +
                        //"@RosterID, " +
                        //"@RoleID, " +
                        "@PersonnellManAdded," +
                        "@RoleNameManAdded" +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    int.TryParse(TextBoxPercentage.Text, out int porcentaje);

                    cmd.Parameters.AddWithValue("@Name", TextBoxOtherPersonal.Text);
                    cmd.Parameters.AddWithValue("@PerTime", porcentaje);
                    cmd.Parameters.AddWithValue("@LocationID", DropDownListOtherPersonalLocation.SelectedValue);

                    //cmd.Parameters.AddWithValue("@RosterID", DropDownListPersonnel.SelectedValue);
                    //cmd.Parameters.AddWithValue("@RoleID", DropDownListPersonnel.SelectedValue);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);

                    cmd.Parameters.AddWithValue("@PersonnellManAdded", TextBoxPersonnel.Text);
                    cmd.Parameters.AddWithValue("@RoleNameManAdded", TextBoxPersonnelRole.Text);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;

                    TextBoxOtherPersonal.Text = "";
                    TextBoxPercentage.Text = "";

                    TextBoxPersonnelRole.Text = "";
                    TextBoxPersonnel.Text = "";
                    //ClearSelectionFor(DropDownListPersonnel);
                    //ClearSelectionFor(DropDownListPersonnelRole);

                    //this.DropDownListPersonnel.ClearSelection();
                    //this.DropDownListPersonnelRole.ClearSelection();                    

                    gvOP.DataBind();

                    SaveChanges();

                    //this.SetFocus(this.DropDownListPersonnel);
                }
                catch (Exception ex)
                {

                    throw new Exception("Error while trying to add personnel to the project", ex);
                    //Message.Text = "opps it happen again" + ex;
                    //ErrorMessage.Visible = true;
                    //LabelInfo.Visible = false;
                }
            }



        }
        //protected void ButtonPersonnelAndRole_Click()
        //{
        //    SqlConnection cn = new SqlConnection();
        //    try
        //    {
        //        cn.Open();
        //        string commandInsert = "INSERT INTO Roster(RosterID)";
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        protected void ButtonStudent_Click(object sender, EventArgs e)
        {

            string rosterString = DropDownListStudent.SelectedValue;
            //var pIDString = Request.QueryString["PID"].ToString();
            var roleString = TextBoxStudentNames.Text;

            int.TryParse(roleString, out int roleID);
            Guid.TryParse(rosterString, out Guid rosterID);
            //bool isValidProjectID = Guid.TryParse(pIDString, out Guid pID);
            bool hasRosterAdded = false;
            var pID = this.ProjectID.Value;
            if (AddPlayerProject(pID, rosterID, roleID))
            {
                hasRosterAdded = true;


            }


            if (hasRosterAdded)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into GradAss(ProjectID, Name, Thesis, StudentID, Amountp, RosterID, StudentName) values ( " +
                        "@ProjectID, " +
                        "@Name, " +
                        "@Thesis, " +
                        "@StudentID, " +
                        "@Amountp ," +
                        "@RosterID, " +
                        "@StudentName " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    decimal.TryParse(TextBoxStudentAmount.Text, out decimal amount);

                    cmd.Parameters.AddWithValue("@RosterID", DropDownListStudent.SelectedValue);
                    cmd.Parameters.AddWithValue("@StudentName", TextBoxStudentNames.Text);

                    cmd.Parameters.AddWithValue("@Name", TextBoxStudentName.Text);
                    cmd.Parameters.AddWithValue("@Thesis", TextBoxThesisTittle.Text);
                    cmd.Parameters.AddWithValue("@StudentID", TextBoxStudentID.Text);
                    cmd.Parameters.AddWithValue("@Amountp", amount);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;

                    TextBoxStudentName.Text = "";
                    TextBoxStudentID.Text = "";
                    TextBoxThesisTittle.Text = "";
                    TextBoxStudentAmount.Text = "";
                    TextBoxStudentNames.Text = "";


                    gvGradAss.DataBind();

                    SaveChanges();
                    ClearSelectionFor(DropDownListStudent);

                    this.SetFocus(this.DropDownListStudent);
                }
                catch (Exception ex)
                {

                    throw new Exception("Error while trying to add student to the project", ex);
                    //Message.Text = "opps it happen again" + ex;
                    //ErrorMessage.Visible = true;
                    //LabelInfo.Visible = false;
                }
            }


        }

        protected void ButtonFunds_Click(object sender, EventArgs e)
        {
            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into Funds(ProjectID, LocationID, Salaries, Wages, Benefits, Assistant, Materials, Equipment, Travel, Abroad, Subcontracts, indirectCosts, others) values ( " +
                        "@ProjectID, " +
                        "@LocationID, " +
                        "@Salaries, " +
                        "@Wages, " +
                        "@Benefits, " +
                        "@Assistant, " +
                        "@Materials, " +
                        "@Equipment, " +
                        "@Travel, " +
                        "@Abroad, " +
                        "@Subcontracts," +
                        "@IndirectCosts," +
                        "@others " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    cmd.Parameters.AddWithValue("@LocationID", DropDownListFundLocation.SelectedValue);
                    cmd.Parameters.AddWithValue("@Salaries", TextBoxSalaries.Text);
                    cmd.Parameters.AddWithValue("@Wages", TextBoxWages.Text);
                    cmd.Parameters.AddWithValue("@Benefits", TextBoxBenifit.Text);
                    cmd.Parameters.AddWithValue("@Assistant", TextBoxAssistant.Text);
                    cmd.Parameters.AddWithValue("@Materials", TextBoxMaterials.Text);
                    cmd.Parameters.AddWithValue("@Equipment", TextBoxEquipment.Text);
                    cmd.Parameters.AddWithValue("@Travel", TextBoxTravel.Text);
                    cmd.Parameters.AddWithValue("@Abroad", TextBoxAbroad.Text);
                    cmd.Parameters.AddWithValue("@Subcontracts", TextBoxSubcontracts.Text);
                    cmd.Parameters.AddWithValue("@IndirectCosts", TextBoxIndirectCosts.Text);
                    cmd.Parameters.AddWithValue("@others", TextBoxOthers.Text);


                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    //LabelInfo.Text = "INFORMATIONS RECORDED";
                    //InfoMessage.Visible = true;
                    //LabelInfo.Visible = true;
                    //ErrorMessage.Visible = false;

                    TextBoxSalaries.Text = "";
                    TextBoxWages.Text = "";
                    TextBoxBenifit.Text = "";
                    TextBoxAssistant.Text = "";
                    TextBoxMaterials.Text = "";
                    TextBoxEquipment.Text = "";
                    TextBoxTravel.Text = "";
                    TextBoxAbroad.Text = "";
                    TextBoxSubcontracts.Text = "";
                    TextBoxOthers.Text = "";
                    TextBoxIndirectCosts.Text = "";
                    dlFunds.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    //Message.Text = "opps it happen again" + ex;
                    //ErrorMessage.Visible = true;
                    //LabelInfo.Visible = false;
                }
            }
        }

        protected void GridView_RowUpdate(object sender, GridViewUpdatedEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //LinkButton lnkBtnDelete = e.Row.FindControl("lnkBtnDelete") as LinkButton;
            //    //System.Web.UI.Control ctrl = e.Row.Cells[7].Controls[2];

            //    //if (((System.Web.UI.WebControls.Button)ctrl).Text == "Delete" || ((System.Web.UI.WebControls.Button)ctrl).Text == "-")
            //    //{
            //    //    ((System.Web.UI.WebControls.Button)ctrl).OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";

            //    //    ((System.Web.UI.LiteralControl)e.Row.Cells[7].Controls[1]).Text = "";
            //    //}
            //    //else
            //    //{

            //    //}
            //    // Use whatever control you want to show in the confirmation message
            //    //Label lblContactName = e.Row.FindControl("lblContactName") as Label;

            //    //lnkBtnDelete.Attributes.Add("onclick", string.Format("return confirm('Are you sure you want to delete the contact {0}?');", lblContactName.Text));

            //}

        }

        protected void dlFunds_Edit_Command(Object sender, DataListCommandEventArgs e)
        {

            // Set the EditItemIndex property to the index of the item clicked
            // in the DataList control to enable editing for that item. Be sure
            // to rebind the DataList to the data source to refresh the control.
            //< asp:Label ID = "FundIdLabel" Visible = "false" Text = '<%# Eval("FundId") %>'  runat = "server" ></ asp:Label >
            //< asp:Label ID = "LocationIdLabel" Visible = "false" Text = '<%# Eval("LocationId") %>'  runat = "server" ></ asp:Label >

            var itemIndex = -1;

            var fundIdControl = e.Item.FindControl("FundIdLabel");

            if (fundIdControl != null)
            {
                Label fundIdLabel = (Label)fundIdControl;
                if (!string.IsNullOrEmpty(fundIdLabel.Text))
                {
                    itemIndex = e.Item.ItemIndex;
                }
            }

            dlFunds.EditItemIndex = itemIndex;
            dlFunds.DataBind();

        }
        protected void dlFunds_Update_Command(Object sender, DataListCommandEventArgs e)
        {


            // Set the EditItemIndex property to -1 to exit editing mode. 
            // Be sure to rebind the DataList to the data source to refresh
            // the control.

            var fundId = -1;
            var isFund = false;

            var fundIdControl = e.Item.FindControl("FundIdLabel");
            if (fundIdControl != null)
            {
                Label fundIdLabel = (Label)fundIdControl;
                if (!string.IsNullOrEmpty(fundIdLabel.Text))
                {
                    if (int.TryParse(fundIdLabel.Text, out fundId))
                        isFund = true;
                }
            }

            var fundLocation = (DropDownList)e.Item.FindControl("ComboBoxFundLocation");
            int.TryParse(fundLocation.SelectedValue, out int fundLocationId);

            var rsaCtrl = (TextBox)e.Item.FindControl("rsa");
            decimal.TryParse(rsaCtrl.Text, out decimal rsa);

            var rwaCtrl = (TextBox)e.Item.FindControl("rwa");
            decimal.TryParse(rwaCtrl.Text, out decimal rwa);

            var rbeCtrl = (TextBox)e.Item.FindControl("rbe");
            decimal.TryParse(rbeCtrl.Text, out decimal rbe);

            var rasCtrl = (TextBox)e.Item.FindControl("ras");
            decimal.TryParse(rasCtrl.Text, out decimal ras);

            var rmaCtrl = (TextBox)e.Item.FindControl("rma");
            decimal.TryParse(rmaCtrl.Text, out decimal rma);

            var reqCtrl = (TextBox)e.Item.FindControl("req");
            decimal.TryParse(reqCtrl.Text, out decimal req);

            var rtrCtrl = (TextBox)e.Item.FindControl("rtr");
            decimal.TryParse(rtrCtrl.Text, out decimal rtr);

            var rabCtrl = (TextBox)e.Item.FindControl("rab");
            decimal.TryParse(rabCtrl.Text, out decimal rab);

            var rsuCtrl = (TextBox)e.Item.FindControl("rsu");
            decimal.TryParse(rsuCtrl.Text, out decimal rsu);

            var rotCtrl = (TextBox)e.Item.FindControl("rot");
            decimal.TryParse(rotCtrl.Text, out decimal rot);

            var rinCtrl = (TextBox)e.Item.FindControl("rin");
            decimal.TryParse(rinCtrl.Text, out decimal rin);

            if (isFund)
            {
                Update_Funds(fundId, fundLocationId, rsa, rwa, rbe, ras, rma, req, rtr, rab, rsu, rot, rin);
            }

            dlFunds.EditItemIndex = -1;
            dlFunds.DataBind();

        }

        private void Update_Funds(int fundId, int locationId, decimal salaries, decimal wages, decimal benefits, decimal assistant, decimal materials, decimal equipment, decimal travel, decimal abroad, decimal subcontracts, decimal others, decimal indirectCosts)
        {

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            try
            {
                cn.Open();
                string UpdateFunds = "Update Funds " +
                    "set LocationId = @LocationId," +
                    "Salaries = @Salaries," +
                    "Wages = @Wages," +
                    "Benefits = @Benefits," +
                    "Assistant = @Assistant," +
                    "Materials = @Materials," +
                    "Equipment = @Equipment," +
                    "Travel = @Travel," +
                    "Abroad = @Abroad," +
                    "Subcontracts = @Subcontracts," +
                    "IndirectCosts = @IndirectCosts," +
                    "Others = @Others " +
                    "where FundId = @FundId";

                SqlCommand cmd = new SqlCommand(UpdateFunds, cn);

                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Salaries", salaries);
                cmd.Parameters.AddWithValue("@Wages", wages);
                cmd.Parameters.AddWithValue("@Benefits", benefits);
                cmd.Parameters.AddWithValue("@Assistant", assistant);
                cmd.Parameters.AddWithValue("@Materials", materials);
                cmd.Parameters.AddWithValue("@Equipment", equipment);
                cmd.Parameters.AddWithValue("@Travel", travel);
                cmd.Parameters.AddWithValue("@Abroad", abroad);
                cmd.Parameters.AddWithValue("@Subcontracts", subcontracts);
                cmd.Parameters.AddWithValue("@IndirectCosts", indirectCosts);
                cmd.Parameters.AddWithValue("@Others", others);
                cmd.Parameters.AddWithValue("@FundId", fundId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                //LabelInfo.Text = "UPDATED INFORMATIONS";
                //InfoMessage.Visible = true;
                //LabelInfo.Visible = true;
                //ErrorMessage.Visible = false;
            }
            catch (SqlException ex)
            {
                //Message.Text = "opps it happen again" + ex;
                //ErrorMessage.Visible = true;
                //LabelInfo.Visible = false;
            }

        }

        protected void dlFunds_Cancel_Command(Object sender, DataListCommandEventArgs e)
        {

            // Set the EditItemIndex property to -1 to exit editing mode. Be sure
            // to rebind the DataList to the data source to refresh the control.
            dlFunds.EditItemIndex = -1;
            dlFunds.DataBind();

        }
        protected void dlFunds_Delete_Command(Object sender, DataListCommandEventArgs e)
        {
            var fundId = -1;

            var fundIdControl = e.Item.FindControl("FundIdLabel");

            if (fundIdControl != null)
            {
                Label fundIdLabel = (Label)fundIdControl;
                if (!string.IsNullOrEmpty(fundIdLabel.Text))
                {
                    if (int.TryParse(fundIdLabel.Text, out fundId))
                    {
                        Delete_Funds(fundId);
                    }
                }
            }

            dlFunds.DataBind();

        }

        private void Delete_Funds(int fundId)
        {

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            try
            {
                cn.Open();
                string DeleteFunds = "Delete Funds where FundId = @FundId";

                SqlCommand cmd = new SqlCommand(DeleteFunds, cn);

                cmd.Parameters.AddWithValue("@FundId", fundId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                //LabelInfo.Text = "DELETED INFORMATIONS ";
                //InfoMessage.Visible = true;
                //LabelInfo.Visible = true;
                //ErrorMessage.Visible = false;
            }
            catch (SqlException ex)
            {
                //Message.Text = "opps it happen again" + ex;
                //ErrorMessage.Visible = true;
                //LabelInfo.Visible = false;
            }

        }

        protected void linkbutton1_Click(object sender, EventArgs e)
        {

        }

        protected void txtSample_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvOP_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void dlFunds_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void ButtonAlways_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = string.Empty; // this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);
            var projectProccess = this.internalSection.LastOrDefault();
            var ProccessId = projectProccess.AlwaysProccess.Item1;
            var StatusId = projectProccess.AlwaysProccess.Item3;
            //if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, projectProccess.Way.Item2, projectProccess.Way.Item3))
            if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, ProccessId, StatusId))
            {

                if (UpdateProjectNewProjectWay(projectID, projectProccessWayId))
                {

                }

            }

            //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);

            //if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, projectProccess.Way.Item2, projectProccess.Way.Item3))
            //{

            //}

            signData = projectProccess.AlwaysProccess?.Item2;
            //Guid projectID, Guid rosterID, int roleID, DateTime noteDate, string noteData, string rosterData
            if (AddStatusProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {

                this.imagenSign.ImageUrl = GetRosterSignature();
                GenerateStatus(projectID);

            }

            GenerateProjectProccessInfo(projectID);

            EvalStatusActions();

        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            //var projectProccess = this.internalSection.FirstOrDefault(section => section.availabledChecks.Item1);
            var projectProccess = this.internalSection.LastOrDefault();
            var nextProccessId = projectProccess.NextProccess.Item1;
            var nextStatusId = projectProccess.NextProccess.Item3;
            //if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, projectProccess.Way.Item2, projectProccess.Way.Item3))
            if (SaveNewProjectWay(out Guid projectProccessWayId, projectID, nextProccessId, nextStatusId))
            {

                if (UpdateProjectNewProjectWay(projectID, projectProccessWayId))
                {

                }

            }

            signData = projectProccess.NextProccess?.Item2;

            if (AddStatusProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {

                this.imagenSign.ImageUrl = GetRosterSignature();
                GenerateStatus(projectID);

            }

            GenerateProjectProccessInfo(projectID);

            EvalStatusActions();

        }

        protected void ButtonAssent_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = this.TextBoxAssent.Text?.Trim(); // this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            if (AddAssentProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {
                this.TextBoxAssent.Text = string.Empty;
                this.imagenSign.ImageUrl = GetRosterSignature();
                GenerateAssents(projectID);

            }

        }

        protected void ButtonObjetion_Click(object sender, EventArgs e)
        {

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            var playerRoleID = this.PlayerRoleId.Value; //this.PlayerInfoSet.First();

            Guid rosterId = userLogged.RosterId;
            string rosterPicture = userLogged.RosterPicture;
            Guid projectID = Guid.Empty;

            if (this.ProjectID != null) projectID = this.ProjectID.Value;

            var signData = this.TextBoxObjetion.Text?.Trim();// this.imagenSign.ImageUrl?.Trim();
            DateTime signDate = DateTime.Now;

            if (AddObjetionProject(projectID: projectID, rosterID: rosterId, roleID: playerRoleID, noteDate: signDate, noteData: signData, rosterData: rosterPicture))
            {

                this.TextBoxObjetion.Text = string.Empty;
                this.imagenSign.ImageUrl = GetRosterSignature();
                GenerateObjetions(projectID);

            }
        }


        //Report Section
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            var projectId = this.ProjectID.Value;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand($"" +
                "SELECT P.ProjectNumber 'Project Number', P.ProjectTitle 'Project Title', fy.FiscalYearName 'Fiscal Year',  R.RosterName 'Project PI'," +
                "P.ContractNumber 'Contract Number',PS.ProjectStatusName 'Status',PA.ProgramAreaName 'Programatic Area',Comm.CommName 'Commodity'," +
                "Dept.DepartmentName 'Department',Subs.LocationName 'Location',FT.FundTypeName 'Type Of Funds',PO.POrganizationName 'Performing Organization',P.Objectives 'Project Objective(s)' " +
                "FROM Projects P " +
                "LEFT OUTER JOIN FiscalYear FY ON P.FiscalYearID = FY.FiscalYearID LEFT OUTER JOIN Roster R ON P.ProjectPI = R.RosterID " +
                "LEFT OUTER JOIN ProjectStatus PS ON P.ProjectStatusID = PS.ProjectStatusID LEFT OUTER JOIN ProgramArea PA ON P.ProgramAreaID = PA.ProgramAreaID " +
                "LEFT OUTER JOIN Commodity Comm ON P.CommID = Comm.CommID LEFT OUTER JOIN Department Dept ON P.DepartmentID = Dept.DepartmentID " +
                "LEFT OUTER JOIN Location Subs ON P.SubStationID = Subs.LocationID LEFT OUTER JOIN FundType FT ON P.FundTypeID = FT.FundTypeID " +
                "LEFT OUTER JOIN POrganization PO ON P.POrganizationsId = PO.POrganizatonId" +
                $" WHERE ProjectId = '{projectId}' ", cn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            ReportDocument crd = new ReportDocument();
            crd.Load(Server.MapPath("~/Report/GeneralReport.rpt"));
            crd.SetDataSource(ds.Tables["table"]);

            //CrystalReportView
            CrystalReportViewer1.ReportSource = crd;
            crd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "General Project Report");
        }

        protected void SqlDataSourceStudentRole_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        //protected void NewPersonelRole_Click(object sender, EventArgs e)
        //{

        //}


        //    #region Properties
        //    public string TabSelectedIndex
        //    {
        //        get
        //        {
        //            var viewState = this.ViewState;
        //            string result = string.Empty;
        //            if (viewState != null) result = viewState["TabSelectedIndex"] as string;
        //            return result;
        //        }

        //        set
        //        {
        //            var viewState = this.ViewState;
        //            if (viewState != null) viewState["TabSelectedIndex"] = value;

        //        }

        //    }
        //    public Eblue.Code.TargetSectionSet targetSections
        //    {

        //        get
        //        {
        //            Eblue.Code.TargetSectionSet result = null;
        //            object viewstateSession = this.ViewState;

        //            if (viewstateSession == null)
        //            {
        //                //var stop = true;
        //                result = new Code.TargetSectionSet();

        //            }
        //            else
        //            {
        //                result = this.ViewState["targetSections"] as Eblue.Code.TargetSectionSet;

        //            }

        //            return result;

        //        }
        //        set
        //        {
        //            object viewstateSession = this.ViewState;

        //            if (viewstateSession == null)
        //            {
        //                //var stop = true;

        //            }
        //            else
        //            {
        //                this.ViewState["targetSections"] = value;

        //            }
        //        }

        //    }
        //    #endregion


        //    protected new void Page_Load(object sender, EventArgs e)
        //    {
        //        //base.Page_Load(sender, e);

        //        if (!Page.IsPostBack)
        //        {

        //            if (Request.IsAuthenticated)
        //            {

        //                var userId = Eblue.Utils.SessionTools.UserId;
        //                var userLogged = Eblue.Utils.SessionTools.UserInfo;
        //                var isPlayer = false;
        //                var pIDString = Request.QueryString["PID"].ToString();

        //                bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

        //                if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager))
        //                {

        //                    if (userLogged.IsAdmin || userLogged.IsManager)
        //                    {
        //                        isPlayer = true;
        //                    }
        //                    else
        //                    {
        //                        //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
        //                        //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
        //                        //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
        //                        //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
        //                        //    "FROM Projects AS P " +
        //                        //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";

        //                        //var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
        //                        //    $"where (p.ProjectID = '{pIDString}') and " +
        //                        //    $"(p.ProjectPI = '{userLogged.RosterId}' " +
        //                        //    $"or exists(select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in ('{userLogged.RosterId}')))";

        //                        var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
        //                            $"where (p.ProjectID = '{pIDString}') and " +
        //                            $"(p.ProjectPI = '{userLogged.RosterId}' " +
        //                            $"or exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";

        //                        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //                        {
        //                            cn.Open();
        //                            SqlCommand checkcmd = new SqlCommand(strCommand, cn);

        //                            isPlayer = Convert.ToBoolean(checkcmd.ExecuteScalar());

        //                            cn.Close();
        //                        }
        //                    }
        //                }

        //                if (isPlayer)
        //                {
        //                    if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
        //                    {

        //                        if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
        //                        {
        //                            this.targetSections = tss;
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("Error when trying to get the roster capacities in the sections of this projects");
        //                        }
        //                    }
        //                    else
        //                    {

        //                        throw new Exception("Error at try getting the roster role category in this project");
        //                    }


        //                }

        //                else
        //                {

        //                    base.GoToUnAuthorizeRoute();
        //                }



        //            }

        //        }
        //        else
        //        {
        //            if (Request.IsAuthenticated)
        //            {

        //                var userId = Eblue.Utils.SessionTools.UserId;
        //                var userLogged = Eblue.Utils.SessionTools.UserInfo;
        //                var isPlayer = false;
        //                var pIDString = Request.QueryString["PID"].ToString();

        //                bool isValidProject = Guid.TryParse(pIDString, out Guid projectId);

        //                if (userLogged != null && isValidProject && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager))
        //                {

        //                    if (userLogged.IsAdmin || userLogged.IsManager)
        //                    {
        //                        isPlayer = true;
        //                    }
        //                    else
        //                    {
        //                        //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
        //                        //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
        //                        //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
        //                        //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
        //                        //    "FROM Projects AS P " +
        //                        //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";

        //                        //var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
        //                        //    $"where (p.ProjectID = '{pIDString}') and " +
        //                        //    $"(p.ProjectPI = '{userLogged.RosterId}' " +
        //                        //    $"or exists(select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in ('{userLogged.RosterId}')))";

        //                        var strCommand = $"select top 1 convert(bit, 1) IsPlayer from projects p " +
        //                            $"where (p.ProjectID = '{pIDString}') and " +
        //                            $"(p.ProjectPI = '{userLogged.RosterId}' " +
        //                            $"or exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";

        //                        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //                        {
        //                            cn.Open();
        //                            SqlCommand checkcmd = new SqlCommand(strCommand, cn);

        //                            isPlayer = Convert.ToBoolean(checkcmd.ExecuteScalar());

        //                            cn.Close();
        //                        }
        //                    }
        //                }

        //                if (isPlayer)
        //                {
        //                    if (GetRoleCategoryFromRoster(out Guid rcID, projectId, userLogged.RosterId))
        //                    {

        //                        if (GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet tss, rcID))
        //                        {
        //                            this.targetSections = tss;
        //                        }
        //                        else
        //                        {
        //                            throw new Exception("Error at try getting the roster role sections capabilities in this project");
        //                        }
        //                    }
        //                    else
        //                    {

        //                        throw new Exception("Error at try getting the roster role category in this project");
        //                    }


        //                }

        //                else
        //                {

        //                    base.GoToUnAuthorizeRoute();
        //                }



        //            }

        //            //if (gvGradAss.HeaderRow != null)
        //            //    gvGradAss.HeaderRow.TableSection = TableRowSection.TableHeader;

        //            //if (gvLab.HeaderRow != null)
        //            //    gvLab.HeaderRow.TableSection = TableRowSection.TableHeader;

        //            //if (gvOP.HeaderRow != null)
        //            //    gvOP.HeaderRow.TableSection = TableRowSection.TableHeader;

        //            //if (gvP3BeInitiated.HeaderRow != null)
        //            //    gvP3BeInitiated.HeaderRow.TableSection = TableRowSection.TableHeader;

        //            //if (gvP3Progress.HeaderRow != null)
        //            //    gvP3Progress.HeaderRow.TableSection = TableRowSection.TableHeader;

        //            //if (gvSci.HeaderRow != null)
        //            //    gvSci.HeaderRow.TableSection = TableRowSection.TableHeader;


        //            //gvLab.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            //gvOP.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            //gvP3BeInitiated.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            //gvP3Progress.HeaderRow.TableSection = TableRowSection.TableHeader;
        //            //gvSci.HeaderRow.TableSection = TableRowSection.TableHeader;


        //        }

        //        if (!Page.IsPostBack)
        //        {
        //            //base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
        //            //this.gvModel.RowCommand += GridView_RowCommand;

        //            //this.gvModel.RowUpdated += GridView_RowUpdated;

        //        }
        //        else
        //        {

        //            //base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
        //            //base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
        //            //this.gvModel.RowCommand += GridView_RowCommand;

        //            ////this.gvModel.RowUpdated(sender:this, e: e);

        //            //this.gvModel.RowUpdated += GridView_RowUpdated;
        //        }

        //        if (Page.IsPostBack)
        //        {

        //            var ctrl = GetControlThatCausedPostBack(Page);
        //            if (ctrl != null)
        //            { }

        //        }
        //    }

        //    /// <summary>
        //    /// Retrieves the control that caused the postback.
        //    /// </summary>
        //    /// <param name="page"></param>
        //    /// <returns></returns>
        //    private Control GetControlThatCausedPostBack(Page page)
        //    {
        //        //initialize a control and set it to null
        //        Control ctrl = null;

        //        //get the event target name and find the control
        //        string ctrlName = page.Request.Params.Get("__EVENTTARGET");
        //        if (!String.IsNullOrEmpty(ctrlName))
        //            ctrl = page.FindControl(ctrlName);

        //        //return the control to the calling method
        //        return ctrl;
        //    }

        //    protected bool GetRoleCategoryFromRoster(out Guid roleCategoryID, Guid projectId, Guid rosterID)
        //    {
        //        bool result = false;
        //        roleCategoryID = Guid.Empty;

        //        try
        //        {
        //            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //            {
        //                cn.Open();


        //                SqlCommand cmdCommand = new SqlCommand(
        //                    $"select pp.RosterID, pp.RoleID, pp.ProjectID, r.RoleName, r.Enable as RoleEnable, r.Level as RoleLevel, " +
        //                    $"r.OrderLine as RoleOrderLine, rc.UId RoleTypeID, rc.Name RoleTypeName, rc.Description RoleTypeDescription, " +
        //                    $"rc.OrderLine as RoleTypeOrderLine from playerProject pp inner join roles r on r.RoleID = pp.RoleID " +
        //                    $"inner join RoleCategory rc on rc.UId = r.RoleCategoryId where pp.ProjectID = '{projectId}' and pp.RosterId = '{rosterID}' ", cn);

        //                var reader = cmdCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {

        //                        result = Guid.TryParse(reader["RoleTypeID"].ToString(), out roleCategoryID);

        //                    }

        //                }

        //                cn.Close();

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error at getting the roster role in this project", ex);
        //        }

        //        return result;

        //    }

        //    protected bool GetTargetSectionSetFromRosterRole(out Eblue.Code.TargetSectionSet targetSections, Guid roleCategoryID)
        //    {
        //        bool result = false;
        //        targetSections = new Code.TargetSectionSet();

        //        try
        //        {
        //            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //            {
        //                cn.Open();

        //                SqlCommand cmdCommand = new SqlCommand(
        //                    $"select ROW_NUMBER() OVER(ORDER BY rt.Orderline ASC) AS RowNumber, " +
        //                    $"rspl.RoleTargetID, rt.name as RoleTargetName, rt.Description as RoleTargetDescription, rt.OrderLine as RoleTargetOrderLine, " +
        //                    $"rspl.uid RoleSetSectionID, rspl.name RoleSetSectionName, " +
        //                    $"rspl.whenData, rspl.dataCapDetail, rspl.dataCapEdit,  " +
        //                    $"rspl.whenList, rspl.listCapDetail, rspl.listCapAdd, rspl.listCapRemove, rspl.listCapEdit, " +
        //                    $"rspl.withTargetOf, rspl.targetOF, rspl.IsForProject, rspl.IsForProcess, rspl.OrderLine RosterSetSectionOrderLine " +
        //                    $"from rolepermission rp " +
        //                    $"inner join rolesetpermission rsp on rsp.uid = rp.rolesetpermissionID and rsp.isForProject = 1 " +
        //                    $"inner join rolesetpermission rspl on rspl.targetOf = rsp.uid and rspl.isForProject = 1 " +
        //                    $"INNER JOIN roleTarget rt on rt.uid = rspl.RoleTargetID " +
        //                    $"where rp.RoleCategoryId = '{roleCategoryID}' ", cn);

        //                var reader = cmdCommand.ExecuteReader();

        //                if (reader.HasRows)
        //                {

        //                    while (reader.Read())
        //                    {

        //                        var target = new Eblue.Code.TargetSection();

        //                        int.TryParse(reader["RowNumber"].ToString(), out int rownumber);
        //                        target.rowNumber = rownumber;

        //                        target.name = reader["RoleTargetName"].ToString();
        //                        target.description = reader["RoleTargetDescription"].ToString();

        //                        bool.TryParse(reader["whenData"].ToString(), out bool whenData);
        //                        target.whenData = whenData;

        //                        bool.TryParse(reader["dataCapDetail"].ToString(), out bool dataCapDetail);
        //                        target.dataCapDetail = dataCapDetail;

        //                        bool.TryParse(reader["dataCapEdit"].ToString(), out bool dataCapEdit);
        //                        target.dataCapEdit = dataCapEdit;

        //                        bool.TryParse(reader["whenList"].ToString(), out bool whenList);
        //                        target.whenList = whenList;

        //                        bool.TryParse(reader["listCapDetail"].ToString(), out bool listCapDetail);
        //                        target.listCapDetail = listCapDetail;

        //                        bool.TryParse(reader["listCapAdd"].ToString(), out bool listCapAdd);
        //                        target.listCapAdd = listCapAdd;

        //                        bool.TryParse(reader["listCapRemove"].ToString(), out bool listCapRemove);
        //                        target.listCapRemove = listCapRemove;

        //                        bool.TryParse(reader["listCapEdit"].ToString(), out bool listCapEdit);
        //                        target.listCapEdit = listCapEdit;

        //                        int.TryParse(reader["RoleTargetOrderLine"].ToString(), out int orderLine);
        //                        target.orderLine = orderLine;

        //                        targetSections.Add(target.name, target);

        //                    }

        //                }

        //                cn.Close();
        //                result = true;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error at getting the sections for this role in this project", ex);
        //        }

        //        return result;

        //    }

        //    protected void btn1_Click(object sender, EventArgs e)
        //    {
        //        TabSelectedIndex = "1";
        //    }

        //    protected void Button1_Click(object sender, EventArgs e)
        //    {
        //        TabSelectedIndex = "1";
        //    }

        //    protected void Button2_Click(object sender, EventArgs e)
        //    {
        //        TabSelectedIndex = "2";
        //    }
    }
}