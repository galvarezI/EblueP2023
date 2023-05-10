using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HaciendaT5K.plugins
{
    public partial class _5kform : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //var d = new System.Web.UI.HtmlControls.HtmlForm();
            //SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, (SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName FROM Projects AS P

            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;

                //FormsIdentity ident = User.Identity as FormsIdentity;
                //lblRosterName.Text = ident.Name;

                //if (userLogged != null && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager || userLogged.IsCoordinator ))
                if (userLogged != null && ( userLogged.IsAdmin || userLogged.IsManager || userLogged.IsCoordinator))
                {

                    if (userLogged.IsAdmin || userLogged.IsManager)
                    {
                        SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            "";


                        SqlDataSource1.SelectParameters.Clear();
                        //SqlDataSource1.SelectParameters.Add("ProjectPI", "");
                        //SqlDataSource1.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                    else {
                        //SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                        //    "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                        //    "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                        //    "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                        //    "FROM Projects AS P " +
                        //    "where (ProjectPI = @ProjectPI or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ))";
                        SqlDataSource1.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                            "FROM Projects AS P " +
                            $"where (exists(select 1 from playerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in ('{userLogged.RosterId}')))";


                        SqlDataSource1.SelectParameters.Clear();
                        //SqlDataSource1.SelectParameters.Add("ProjectPI", "");
                        //SqlDataSource1.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gv);

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gv);
                //gv.HeaderRow.TableSection = System.Web.UI.WebControls.TableRowSection.TableHeader;
                //here function to clear literalcontrol from gridview commands
                //if (string.IsNullOrEmpty(DropDownListUser.SelectedValue))
                //{
                //    //DropDownListUser.Items.Clear();
                //    //DropDownListUser.Items.Add(new ListItem(" ", ""));
                //    //DropDownListUser.DataBind();
                //}



            }

        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            //if (!Page.IsPostBack)
            //{
            var statementJS = "var tbl = $('table'); $(tbl).DataTable({'paging': true,'lengthChange': false,'searching': false,'ordering': true,'info': true,'autoWidth': true }); " +
            " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡');";

            var script = $"$(document).ready(function () {{{statementJS}}});";
            ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lnkBtnDelete = e.Row.FindControl("lnkBtnDelete") as LinkButton;
                //System.Web.UI.Control ctrl = e.Row.Cells[7].Controls[2];

                //if (((System.Web.UI.WebControls.Button)ctrl).Text == "Delete" || ((System.Web.UI.WebControls.Button)ctrl).Text == "-")
                //{
                //    ((System.Web.UI.WebControls.Button)ctrl).OnClientClick = "if ( !confirm('Are you sure you want to delete this entry?')) return false;";

                //    ((System.Web.UI.LiteralControl)e.Row.Cells[7].Controls[1]).Text = "";
                //}

                // Use whatever control you want to show in the confirmation message
                //Label lblContactName = e.Row.FindControl("lblContactName") as Label;

                //lnkBtnDelete.Attributes.Add("onclick", string.Format("return confirm('Are you sure you want to delete the contact {0}?');", lblContactName.Text));

            }
            else if (e.Row.RowType == DataControlRowType.Header )
            {

               e.Row.TableSection = TableRowSection.TableHeader;


            }

        }

        protected void ButtonSaveNewProject_Click(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                if (SaveNewProject(out Guid projectID,  out Guid projectPIID))
                {


                    if (SavePlayerPIProject(projectID, projectPIID))
                    {
                        //var flag = true;
                    }

                    var userLogged = Eblue.Utils.SessionTools.UserInfo;

                    if (SavePlayerCoordinatorProject(projectID, userLogged.RosterId))
                    {
                        //var flag = true;
                    }

                    gv.DataBind();
                }

                
            }
                
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
                    $"rc.UId = rl.RoleCategoryId where rc.isdirectivemanager = 1), '{projectID}' ";

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

        protected bool SaveNewProject(out Guid newID, out Guid projectPIID)
        {
            bool result;
            newID =  Guid.NewGuid();
            Guid.TryParse (DropDownPrincipalInvestigator.SelectedValue, out projectPIID);
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
    }
}