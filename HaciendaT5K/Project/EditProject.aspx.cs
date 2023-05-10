using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Eblue.Project
{
    public partial class EditProject : Eblue.Code.PageBasic
    {
        public Eblue.Code.TargetSectionSet targetSections { 
            
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

                                labelLeaderResault.Text = project["RosterName"].ToString();
                                labelProjectnumberResult.Text = project["ProjectNumber"].ToString();
                                labelFiscalYearResult.Text = project["FiscalYearNumber"].ToString();
                                labelStatusResult.Text = project["ProyectStatusName"].ToString();

                                TextBoxContractNumber.Text = project["ContractNumber"].ToString();
                                TextBoxORCID.Text = project["ORCID"].ToString();

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

                                #region 3ra Sections
                                TextBoxProjectObjective.Text = project["Objectives"].ToString();
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
                                Message.Text = "opps it happen again" + ex;
                                ErrorMessage.Visible = true;
                            }
                        }

                    }

                    else
                    {

                        base.GoToUnAuthorizeRoute();
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
                base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
                //this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated += GridView_RowUpdated;

            }
            else
            {

                base.PageEventLoadPostBackForGridViewHeaders(this.gvGradAss, this.gvLab, this.gvOP, gvP3BeInitiated, gvP3Progress, gvSci);
                //base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                ////this.gvModel.RowUpdated(sender:this, e: e);

                //this.gvModel.RowUpdated += GridView_RowUpdated;
            }
        }

        protected bool  GetRoleCategoryFromRoster(out Guid roleCategoryID, Guid projectId, Guid rosterID)
        {
            bool result =false;
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
            catch ( Exception ex)
            {
                throw new Exception("Error at getting the roster role in this project", ex);
            }
            
            return result;
        
        }

        protected bool GetTargetSectionSetFromRosterRole( out Eblue.Code.TargetSectionSet targetSections, Guid roleCategoryID)
        {
            bool result = false;
            targetSections = new Code.TargetSectionSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmdCommand = new SqlCommand(
                        $"select ROW_NUMBER() OVER(ORDER BY rt.Orderline ASC) AS RowNumber, " +
                        $"rspl.RoleTargetID, rt.name as RoleTargetName, rt.Description as RoleTargetDescription, rt.OrderLine as RoleTargetOrderLine, " +
                        $"rspl.uid RoleSetSectionID, rspl.name RoleSetSectionName, " +
                        $"rspl.whenData, rspl.dataCapDetail, rspl.dataCapEdit,  " +
                        $"rspl.whenList, rspl.listCapDetail, rspl.listCapAdd, rspl.listCapRemove, rspl.listCapEdit, " +
                        $"rspl.withTargetOf, rspl.targetOF, rspl.IsForProject, rspl.IsForProcess, rspl.OrderLine RosterSetSectionOrderLine " +
                        $"from rolepermission rp " +
                        $"inner join rolesetpermission rsp on rsp.uid = rp.rolesetpermissionID and rsp.isForProject = 1 " +
                        $"inner join rolesetpermission rspl on rspl.targetOf = rsp.uid and rspl.isForProject = 1 " +
                        $"INNER JOIN roleTarget rt on rt.uid = rspl.RoleTargetID " +
                        $"where rp.RoleCategoryId = '{roleCategoryID}' ", cn);

                    var reader = cmdCommand.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            var target = new Eblue.Code.TargetSection();

                            int.TryParse(reader["RowNumber"].ToString(), out int rownumber);
                            target.rowNumber = rownumber;
                            
                            target.name = reader["RoleTargetName"].ToString();
                            target.description = reader["RoleTargetDescription"].ToString();

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

        protected bool AddPlayerProject(Guid projectID, Guid rosterID, int RoleID)
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
                    $"delete from playerProyect where ProjectID = '{projectID}' and RosterID = '{rosterID}'";

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

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            if (!Page.IsPostBack)
            {
                var statementJS = "$('.card-tools button').click();";

                var script = $"$(document).ready(function () {{{statementJS}}});";
                //ClientScript.RegisterStartupScript(this.GetType(), "scriptCardTools", script, true);

                {
                    statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
               " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡'); " +
               " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); });" +
               "  $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

                    script = $"$(document).ready(function () {{{statementJS}}});";
                    ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
                }

                {
                    //var inputs = $('.gridview input[type = \"text\"]' )
                    statementJS = $" var inputs = $('table input[type = \"text\"]' ).not(\"[class='form-control']\"); $(inputs).addClass('form-control');";
                    

                    script = $"$(document).ready(function () {{{statementJS}}});";
                    ClientScript.RegisterStartupScript(this.GetType(), "scriptGridControls", script, true);
                }
            }
            else {

                var statementJS = string.Empty;
                var script = string.Empty;

                {
                     statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
               " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡');" +
               " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); }); " +
               " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

                    script = $"$(document).ready(function () {{{statementJS}}});";
                    ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
                }
                {
                    //var inputs = $('.gridview input[type = \"text\"]' )
                    statementJS = $" var inputs = $('table input[type = \"text\"]' ).not(\"[class='form-control']\"); $(inputs).addClass('form-control');";


                    script = $"$(document).ready(function () {{{statementJS}}});";
                    ClientScript.RegisterStartupScript(this.GetType(), "scriptGridControls", script, true);
                }

            }
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

        public void SaveChanges ()
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

                    cmd.Parameters.AddWithValue("@ProjectTitle", TextBoxProjectShortTitle.Text);
                    cmd.Parameters.AddWithValue("@ProgramAreaID", DropDownListProgramaticArea.SelectedValue);
                    cmd.Parameters.AddWithValue("@DepartmentID", DropDownListDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@CommId", DropDownListCommodity.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubStationID", DropDownListSubstation.SelectedValue);

                    cmd.Parameters.AddWithValue("@Objectives", TextBoxProjectObjective.Text);
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

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;
                }
                catch (SqlException ex)
                {
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
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

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;


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
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
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

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;

                    TextBoxAnalisysRequired.Text = "";
                    TextBoxNumSample.Text = "";
                    TextBoxProDate.Text = "";

                    gvLab.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
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

                    int.TryParse(txtTR.Text, out int TR);
                    int.TryParse(txtCA.Text, out int CA);
                    int.TryParse(txtHA.Text, out int AH);

                    cmd.Parameters.AddWithValue("@RosterID", rosterID);
                    cmd.Parameters.AddWithValue("@Role", roleID);
                    cmd.Parameters.AddWithValue("@TR", TR);
                    cmd.Parameters.AddWithValue("@CA", CA);
                    cmd.Parameters.AddWithValue("@AH", AH);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;

                    txtTR.Text = "";
                    txtCA.Text = "";
                    txtHA.Text = "";

                    gvSci.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
                }
            }

        }

        protected void ButtonOtherPersonal_Click(object sender, EventArgs e)
        {

            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into OtherPersonel(ProjectID, Name, PerTime, LocationID) values ( " +
                        "@ProjectID, " +
                        "@Name, " +
                        "@PerTime, " +
                        "@LocationID " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    int.TryParse(TextBoxPercentage.Text, out int porcentaje);

                    cmd.Parameters.AddWithValue("@Name", TextBoxOtherPersonal.Text);
                    cmd.Parameters.AddWithValue("@PerTime", porcentaje);
                    cmd.Parameters.AddWithValue("@LocationID", DropDownListOtherPersonalLocation.SelectedValue);

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;

                    TextBoxOtherPersonal.Text = "";
                    TextBoxPercentage.Text = "";

                    gvOP.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
                }
            }

        }

        protected void ButtonStudent_Click(object sender, EventArgs e)
        {

            var pIDString = Request.QueryString["PID"].ToString();

            if (Guid.TryParse(pIDString, out Guid pID))
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
                try
                {
                    cn.Open();
                    string Command_Insert = "Insert into GradAss(ProjectID, Name, Thesis, StudentID, Amountp) values ( " +
                        "@ProjectID, " +
                        "@Name, " +
                        "@Thesis, " +
                        "@StudentID, " +
                        "@Amountp " +
                        ")";

                    SqlCommand cmd = new SqlCommand(Command_Insert, cn);

                    decimal.TryParse(TextBoxStudentAmount.Text, out decimal amount);

                    cmd.Parameters.AddWithValue("@Name", TextBoxStudentName.Text);
                    cmd.Parameters.AddWithValue("@Thesis", TextBoxThesisTittle.Text);
                    cmd.Parameters.AddWithValue("@StudentID", TextBoxStudentID.Text);
                    cmd.Parameters.AddWithValue("@Amountp", amount );

                    cmd.Parameters.AddWithValue("@ProjectID", pID);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cn.Close();

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;

                    TextBoxStudentName.Text = "";
                    TextBoxStudentID.Text = "";
                    TextBoxThesisTittle.Text = "";
                    TextBoxStudentAmount.Text = "";

                    gvGradAss.DataBind();

                    SaveChanges();
                }
                catch (SqlException ex)
                {
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
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

                    LabelInfo.Text = "INFORMATIONS RECORDED";
                    InfoMessage.Visible = true;
                    LabelInfo.Visible = true;
                    ErrorMessage.Visible = false;

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
                    Message.Text = "opps it happen again" + ex;
                    ErrorMessage.Visible = true;
                    LabelInfo.Visible = false;
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

                LabelInfo.Text = "UPDATED INFORMATIONS";
                InfoMessage.Visible = true;
                LabelInfo.Visible = true;
                ErrorMessage.Visible = false;
            }
            catch (SqlException ex)
            {
                Message.Text = "opps it happen again" + ex;
                ErrorMessage.Visible = true;
                LabelInfo.Visible = false;
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

                LabelInfo.Text = "DELETED INFORMATIONS ";
                InfoMessage.Visible = true;
                LabelInfo.Visible = true;
                ErrorMessage.Visible = false;
            }
            catch (SqlException ex)
            {
                Message.Text = "opps it happen again" + ex;
                ErrorMessage.Visible = true;
                LabelInfo.Visible = false;
            }

        }

       
    }
}