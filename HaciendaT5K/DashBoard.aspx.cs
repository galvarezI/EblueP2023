using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Web.Security;

namespace HaciendaT5K
{
    public partial class DashBoard : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);

            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;

                //FormsIdentity ident = User.Identity as FormsIdentity;
                //lblRosterName.Text = ident.Name;
                
                if (userLogged != null && (userLogged.CanBePI || userLogged.IsAdmin || userLogged.IsManager || userLogged.IsPersonnel || userLogged.IsStudent || userLogged.IsCoordinator || userLogged.IsScientist))
                {

                    if (userLogged.IsAdmin || userLogged.IsManager)
                    {
                        sdsNewProjects.SelectCommand = "SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID))" +
                        " AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber," +
                        " (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName," +
                        " (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID = 1) " +
                        "AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)";


                        sdsNewProjects.SelectParameters.Clear();
                    }
                    else
                    {

                        sdsNewProjects.SelectCommand = "SELECT ProjectID, ProjectNumber, ProjectPI, LastUpdate, ProjectStatusID, (SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID))" +
                            " AS ProyectStatusName, FiscalYearID, (SELECT FiscalYearNumber FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearNumber," +
                            " (SELECT RosterName FROM Roster AS RS WHERE (RosterID = P.ProjectPI)) AS RosterName," +
                            " (SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FYS FROM Projects AS P WHERE (ProjectStatusID = 1) " +
                            "AND (ProjectPI = @ProjectPI) ";
                        //+
                        //    "or exists (select 1 from SciProjects sci where sci.ProjectID = p.ProjectID and sci.RosterID in (@ProjectPI) ) " +
                        //    "or exists (select 1 from PlayerProject pp where pp.ProjectID = p.ProjectID and pp.RosterID in (@ProjectPI) ) " +
                        //    ") AND ((SELECT FiscalYearStatusID FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) = 1)";


                        sdsNewProjects.SelectParameters.Clear();
                        sdsNewProjects.SelectParameters.Add("ProjectPI", "");
                        sdsNewProjects.SelectParameters["ProjectPI"].DefaultValue = userLogged.RosterId.ToString();
                    }
                }
                //Count for user to activate
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand checkcmd = new SqlCommand("select count(*) from Users ", cn);

                    int result = Convert.ToInt32(checkcmd.ExecuteScalar());
                    LinkButtonUsers.Text = result.ToString();
                    cn.Close();
                }


                //using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                //{
                //    cn.Open();
                    
                //    SqlCommand cmdUsers = new SqlCommand($"select top 1 rosterid from Users where uid = '{userId}'", cn);

                //    var projectPI = cmdUsers.ExecuteScalar();
                //    //e.Command.Parameters[0].Value = projectPI.ToString();
                //    //sdsNewProjects.SelectParameters["ProjectPI"].DefaultValue = projectPI.ToString();
                //    sdsWProjects.SelectParameters["ProjectPI"].DefaultValue = projectPI.ToString();
                //    sdsSubmmited.SelectParameters["ProjectPI"].DefaultValue = projectPI.ToString();
                //    sdsSubmmited.DataBind();
                //    cn.Close();
                //}


                //Count for projects
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand checkcmd = new SqlCommand("select count(*) from Projects ", cn);

                    int result = Convert.ToInt32(checkcmd.ExecuteScalar());
                    LinkButtonProjects.Text = result.ToString();
                    cn.Close();
                }
            }

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            base.SignOut();
            //FormsAuthentication.SignOut();
            //Response.Redirect("default.aspx", true);

        }
        //protected void ds_Selecting(object sender, SqlDataSourceCommandEventArgs e)
        //{

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        var userId = Eblue.Utils.SessionTools.UserId;
        //        SqlCommand cmdUsers = new SqlCommand($"select top 1 rosterid from Users where uid = '{userId}'", cn);

        //        var projectPI = cmdUsers.ExecuteScalar();
        //        e.Command.Parameters["@ProjectPI"].Value = Guid.Parse( projectPI.ToString());
        //        //sdsSubmmited.SelectParameters["ProjectPI"].DefaultValue = projectPI.ToString();
        //        sdsSubmmited.DataBind();
        //        cn.Close();
        //    }

        //}
    }
}