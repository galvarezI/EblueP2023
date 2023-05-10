using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Linq.Expressions;
using System.Linq;
using Eblue.Code.Models;
using Eblue.Code;

using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;
using static Eblue.Utils.ProjectTools;
using System.Collections.Generic;

using static Eblue.Utils.SessionTools;

using static Eblue.Utils.LoginTools;

using Eblue.Utils;

namespace HaciendaT5K
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                Response.Redirect(this.ResolveClientUrl("~/project/whichIparticipate.aspx"));
            }

            if (!Page.IsPostBack)
            {
                SetFocus(Email);
            }

        }

        protected void SignIn_Click(object sender, EventArgs e)
        {
            bool EmailVerifed = true;
            string UserID = "";
            Guid? userUId = default(Guid?);
            var securitytoken = "";
            bool hasSigning = false;
            bool existEmail = false;
            bool hasBlocked = false;
            bool hasDenied = false;
            int? attempts = default(int?);

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Users where Email = @email", cn);
                cn.Open();
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@email";
                param.Value = Email.Text;
                cmd.Parameters.Add(param);
                SqlDataReader reader = cmd.ExecuteReader();

                existEmail = reader.HasRows;

                if (existEmail)
                {

                    while (reader.Read())
                    {
                        UserID = reader["UID"].ToString();
                        securitytoken = reader["Salt"].ToString();
                        userUId = Guid.Parse(UserID);
                        bool.TryParse(reader["isblocked"]?.ToString(), out hasBlocked);
                        bool.TryParse(reader["hasdenied"]?.ToString(), out hasDenied);
                        int.TryParse(reader["TryAccessQuantity"]?.ToString(), out int tryAccessQuantity);
                        attempts = tryAccessQuantity;

                    }
                    reader.Close();

                    #region denied
                    {

                        if (hasDenied)
                        {

                            #region blocked and denied
                            //{
                            //    if (hasBlocked)
                            //    {

                            //    }
                            //    else 
                            //    {

                            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('your access has currently been denied, please notify your administrator')</script>");
                            //        SetFocus(Email);
                            //        return;

                            //    }

                            //}
                            #endregion
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('your access has currently been denied, please notify your administrator')</script>");
                            SetFocus(Email);
                            return;

                        }

                    }
                    #endregion

                    #region blocked not denied
                    {
                        if (!hasDenied && hasBlocked)
                        {

                            //TODO - proceder a denegar el accesso
                            if (RegisterDenyAccessHandle(out int affectedRows, userUId.Value, notHandlerException: null))
                            {
                                var success = true;
                                if (success)
                                { }

                            }
                            else
                            {
                                var fail = true;
                                if (fail)
                                { }

                            }

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('your access will be temporarily denied, please notify your administrator')</script>");
                            SetFocus(Email);
                            return;
                        }

                    }
                    #endregion

                    #region not blocked and not denied
                    {

                        if (!hasDenied && !hasBlocked)
                        {

                            var password = Password.Text;
                            var pass = Encryption.hashstring(password);
                            var securetoken = Encryption.hashstring(string.Format("{0}{1}", pass, securitytoken));

                            SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
                            checkcmd.Parameters.AddWithValue("@email", Email.Text);
                            checkcmd.Parameters.AddWithValue("@pass", securetoken);
                            checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

                            int result = Convert.ToInt32(checkcmd.ExecuteScalar());
                            cn.Close();
                            if ((result == 1))
                            {
                                Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
                                //Response.Redirect("~/DashBoard.aspx");
                                //TODO  register the auth try success
                                hasSigning = true;


                                //TODO - proceder a conceder el accesso
                                if (RegisterGrantAccessHandle(out int affectedRows, userUId.Value, notHandlerException: null))
                                {
                                    var success = true;
                                    if (success)
                                    { }

                                }
                                else
                                {
                                    var fail = true;
                                    if (fail)
                                    { }

                                }
                            }
                            else
                            {

                                if (RegisterAttemptAccessHandle(out int affectedRows, userUId.Value, notHandlerException: null))
                                {
                                    var success = true;
                                    if (success)
                                    { }

                                }
                                else
                                {
                                    var fail = true;
                                    if (fail)
                                    { }

                                }


                                Eblue.Utils.SessionTools.UserId = Guid.Empty;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
                                SetFocus(Password);
                                return;
                            }

                        }

                    }

                    #endregion


                }
                else
                {
                    //TODO  register the auth try fail
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('You has enter an email which is not in our database, please try again')</script>");
                    SetFocus(Email);
                    return;
                }

            }

            bool hasProjectWorkflowDefault = false;

            if (hasSigning)
            {

                var session = System.Web.HttpContext.Current.Session;
                //session["@@runmode"] = "";
                session["runmode"] = "development {state: debug}";

                if (GetProjectWorkFlowDefault(out Guid? uid, out Tuple<bool, Exception> exceptionInfo))
                {

                    SessionTools.ProjectWorkFlowDefaultID = uid;

                    hasProjectWorkflowDefault = true;
                }
                else
                {
                    var errorMessage = "Error at try getting the default work process flow of projects";
                    var errorMessageList = new System.Text.StringBuilder();

                    errorMessageList.AppendLine(errorMessage);

                    if (exceptionInfo != null)
                    {

                        if (exceptionInfo.Item1)
                        {
                            var sqlException = exceptionInfo.Item2 as SqlException;
                            var sqlErrors = sqlException.Errors.OfType<SqlError>();

                            if (sqlErrors != null)
                            {
                                var sqlErrorList = sqlErrors.ToList();
                                errorMessageList.AppendLine($"Errors from data-db source [{sqlErrorList.Count}]:");
                                var index = 1;
                                foreach (SqlError error in sqlErrorList)
                                {
                                    errorMessageList.AppendLine($"{index} {error.ToString()}");
                                    index++;

                                }

                                throw new Exception(errorMessageList.ToString());
                            }
                        }
                        else
                        {
                            throw new Exception(errorMessageList.ToString(), exceptionInfo.Item2);
                        }

                    }
                    else
                    {
                        throw new Exception(errorMessageList.ToString());

                    }

                }


                if (hasProjectWorkflowDefault)
                {

                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        var userId = Eblue.Utils.SessionTools.UserId;
                        //string rosterName = "roster name not exist";
                        string rosterName = "-noset-";
                        Eblue.Code.UserLogged userLogged = new Eblue.Code.UserLogged() { UserId = userId };

                        SqlCommand cmdCommand = new SqlCommand(
                            $"select top 1 r.RosterId,r.CanBePI, rc.IsAdmin, rc.IsManager, rc.IsSupervisor, rc.IsPersonnel, rc.IsStudent, rc.IsCoordinator, rc.IsScientist, rc.isdeveloper, rc.IsPlanViewer,rc.IsAdministrator,rc.IsProjectOfficer,rc.IsBudgetOfficer,rc.IsHumanROfficer,rc.IsAESOfficer,  CONCAT(r.RosterName, '|', rc.Description, '|', u.Email   )  RosterName " +
                            $", iif(len(picture) = 0, null, picture) picture,iif(len(signature) = 0, null, signature)signature, " +
                            $"rc.UId rosterTypeID, rc.Name rosterTypeName, rc.Description rosterTypeDescription " +
                            $"from roster r " +
                            $"inner join rosterCategory rc on rc.uid = r.rostercategoryid " +
                            $"inner join users u on u.UID = r.UID where u.uid = '{userId}'", cn);

                        var reader = cmdCommand.ExecuteReader();

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                rosterName = reader["rostername"] as string;
                                userLogged.UserFullDescription = rosterName;

                                var rosterTypeName = reader["rosterTypeName"]?.ToString();
                                userLogged.RosterTypeName = rosterTypeName;

                                var rosterTypeDesc = reader["rosterTypeDescription"]?.ToString();
                                userLogged.RosterTypeDescription = rosterTypeDesc;

                                var rosterTypeIDObject = reader["rosterTypeID"];
                                if (!Convert.IsDBNull(rosterTypeIDObject))
                                {
                                    Guid.TryParse(rosterTypeIDObject.ToString(), out Guid rtId);

                                    userLogged.RosterTypeID = rtId;
                                }

                                Guid.TryParse(reader["RosterId"].ToString(), out Guid rosterID);
                                userLogged.RosterId = rosterID;

                                bool.TryParse(reader["CanBePI"].ToString(), out bool canbePI);
                                userLogged.CanBePI = canbePI;

                                bool.TryParse(reader["IsAdmin"].ToString(), out bool IsAdmin);
                                userLogged.IsAdmin = IsAdmin;

                                bool.TryParse(reader["IsManager"].ToString(), out bool IsManager);
                                userLogged.IsManager = IsManager;

                                bool.TryParse(reader["IsSupervisor"].ToString(), out bool IsSupervisor);
                                userLogged.IsSupervisor = IsSupervisor;

                                bool.TryParse(reader["IsPersonnel"].ToString(), out bool IsPersonnel);
                                userLogged.IsPersonnel = IsPersonnel;

                                bool.TryParse(reader["IsStudent"].ToString(), out bool IsStudent);
                                userLogged.IsStudent = IsStudent;

                                bool.TryParse(reader["IsCoordinator"].ToString(), out bool IsCoordinator);
                                userLogged.IsCoordinator = IsCoordinator;

                                bool.TryParse(reader["IsScientist"].ToString(), out bool IsScientist);
                                userLogged.IsScientist = IsScientist;

                                bool.TryParse(reader["isdeveloper"].ToString(), out bool IsDeveloper);
                                userLogged.IsDeveloper = IsDeveloper;
                                //IsPlanViewer
                                bool.TryParse(reader["IsPlanViewer"].ToString(), out bool IsPlanViewer);
                                userLogged.IsPlanViewer = IsPlanViewer;

                                #region new_implementation
                                  //IsAdministrator
                                  bool.TryParse(reader["IsAdministrator"].ToString(),out bool IsAdministrator);
                                  userLogged.IsAdministrator = IsAdministrator;

                                  //IsProjectOfficer
                                  bool.TryParse(reader["IsProjectOfficer"].ToString(), out bool IsProjectOfficer);
                                  userLogged.IsProjectOfficer = IsProjectOfficer;

                                  //IsBudgetOfficer
                                  bool.TryParse(reader["IsBudgetOfficer"].ToString(), out bool IsBudgetOfficer);
                                  userLogged.IsBudgetOfficer = IsBudgetOfficer;

                                  //IsHumanResourcesO
                                  bool.TryParse(reader["IsHumanROfficer"].ToString(), out bool IsHumanROfficer);
                                  userLogged.IsHumanROfficer = IsHumanROfficer;

                                  //IsAESOfficer
                                  bool.TryParse(reader["IsAESOfficer"].ToString(),out bool IsAESOfficer);
                                  userLogged.IsAESOfficer = IsAESOfficer;


                                    
                                #endregion

                                userLogged.RosterSignature = reader["signature"] as string;

                                userLogged.RosterPicture = reader["picture"] as string;

                                userLogged.RosterPicture = userLogged.RosterPicture ?? string.Empty;
                                //FormsIdentity ident = Page.User.Identity as FormsIdentity;
                                //var identValues = ident.Name?.Split('|');
                                var identValues = rosterName?.Split('|');
                                switch (identValues.Length)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        userLogged.RosterName = identValues[0]; //RosterName = identValues[0];
                                        break;
                                    case 2:
                                        //RosterName = identValues[0];
                                        userLogged.RosterType = identValues[1]; //RosterType = identValues[1];
                                        goto case 1;
                                    case 3:
                                        //RosterName = identValues[0];
                                        //RosterType = identValues[1];
                                        userLogged.RosterEmail = identValues[2]; //RosterEmail = identValues[2];
                                        goto case 2;
                                    //break;

                                    default:
                                        break;


                                }
                            }

                        }

                        cn.Close();

                        Eblue.Utils.SessionTools.UserInfo = userLogged;
                        ActivityInitial = DateTime.Now;


                        //ConstructActionPanels(userId);
                        //var urlRedirection = FormsAuthentication.GetRedirectUrl(rosterName, true).Trim();
                        //if (string.IsNullOrEmpty(urlRedirection))
                        //{
                        //    FormsAuthentication.RedirectFromLoginPage(rosterName, true);
                        //}
                        //else
                        //{
                        //    var hasSingOut = urlRedirection.ToLower().Contains("signout");

                        //    if 

                        //    FormsAuthentication.RedirectFromLoginPage(rosterName, true);
                        //}
                        //FormsAuthentication.RedirectFromLoginPage(rosterName, true);
                        //FormsAuthentication.RedirectFromLoginPage("fmontano", true);
                        FormsAuthentication.RedirectFromLoginPage(userLogged.RosterName, true);
                        Response.Redirect(this.ResolveClientUrl("~/project/whichIparticipate.aspx"));

                    }
                }
                else
                {

                    throw new Exception("the default work process flow of projects has not yet been established");


                }

            }

            #region commented code




            //if (hasSigning && hasProjectWorkflowDefault)
            //{

            //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
            //    {
            //        cn.Open();
            //        var userId = Eblue.Utils.SessionTools.UserId;
            //        string rosterName = "roster name not exist";
            //        Eblue.Code.UserLogged userLogged = new Eblue.Code.UserLogged() { UserId = userId };

            //        //SqlCommand cmdCommand = new SqlCommand(
            //        //    $"select top 1 CONCAT(r.RosterName, ' (', rc.Description, '.', u.Email  ,'', ')' )  RosterName from roster r " +
            //        //    $"inner join rosterCategory rc on rc.uid = r.rostercategoryid " +
            //        //    $"inner join users u on u.UID = r.UID where u.uid = '{userId}'", cn);

            //        //var rosterName = cmdCommand.ExecuteScalar();  
            //        //$"select top 1 r.RosterId,r.CanBePI, rc.IsAdmin, rc.IsManager, rc.IsSupervisor, rc.IsPersonnel, rc.IsStudent,  CONCAT(r.RosterName, ' (', rc.Description, '.', u.Email  ,'', ')' )  RosterName from roster r " +

            //        SqlCommand cmdCommand = new SqlCommand(
            //            $"select top 1 r.RosterId,r.CanBePI, rc.IsAdmin, rc.IsManager, rc.IsSupervisor, rc.IsPersonnel, rc.IsStudent, rc.IsCoordinator, rc.IsScientist, rc.isdeveloper, rc.IsPlanViewer,  CONCAT(r.RosterName, '|', rc.Description, '|', u.Email   )  RosterName " +
            //            $", iif(len(picture) = 0, null, picture) picture,iif(len(signature) = 0, null, signature)signature, " +
            //            $"rc.UId rosterTypeID, rc.Name rosterTypeName, rc.Description rosterTypeDescription " +
            //            $"from roster r " +
            //            $"inner join rosterCategory rc on rc.uid = r.rostercategoryid " +
            //            $"inner join users u on u.UID = r.UID where u.uid = '{userId}'", cn);

            //        var reader = cmdCommand.ExecuteReader();

            //        if (reader.HasRows)
            //        {

            //            while (reader.Read())
            //            {
            //                rosterName = reader["rostername"] as string;
            //                userLogged.UserFullDescription = rosterName;

            //                var rosterTypeName = reader["rosterTypeName"]?.ToString();
            //                userLogged.RosterTypeName = rosterTypeName;

            //                var rosterTypeDesc = reader["rosterTypeDescription"]?.ToString();
            //                userLogged.RosterTypeDescription = rosterTypeDesc;

            //                var rosterTypeIDObject = reader["rosterTypeID"];
            //                if (!Convert.IsDBNull(rosterTypeIDObject))
            //                {
            //                    Guid.TryParse(rosterTypeIDObject.ToString(), out Guid rtId);

            //                    userLogged.RosterTypeID = rtId;
            //                }

            //                Guid.TryParse(reader["RosterId"].ToString(), out Guid rosterID);
            //                userLogged.RosterId = rosterID;

            //                bool.TryParse(reader["CanBePI"].ToString(), out bool canbePI);
            //                userLogged.CanBePI = canbePI;

            //                bool.TryParse(reader["IsAdmin"].ToString(), out bool IsAdmin);
            //                userLogged.IsAdmin = IsAdmin;

            //                bool.TryParse(reader["IsManager"].ToString(), out bool IsManager);
            //                userLogged.IsManager = IsManager;

            //                bool.TryParse(reader["IsSupervisor"].ToString(), out bool IsSupervisor);
            //                userLogged.IsSupervisor = IsSupervisor;

            //                bool.TryParse(reader["IsPersonnel"].ToString(), out bool IsPersonnel);
            //                userLogged.IsPersonnel = IsPersonnel;

            //                bool.TryParse(reader["IsStudent"].ToString(), out bool IsStudent);
            //                userLogged.IsStudent = IsStudent;

            //                bool.TryParse(reader["IsCoordinator"].ToString(), out bool IsCoordinator);
            //                userLogged.IsCoordinator = IsCoordinator;

            //                bool.TryParse(reader["IsScientist"].ToString(), out bool IsScientist);
            //                userLogged.IsScientist = IsScientist;

            //                bool.TryParse(reader["isdeveloper"].ToString(), out bool IsDeveloper);
            //                userLogged.IsDeveloper = IsDeveloper;
            //                //IsPlanViewer
            //                bool.TryParse(reader["IsPlanViewer"].ToString(), out bool IsPlanViewer);
            //                userLogged.IsPlanViewer = IsPlanViewer;

            //                userLogged.RosterSignature = reader["signature"] as string;

            //                userLogged.RosterPicture = reader["picture"] as string;

            //                userLogged.RosterPicture = userLogged.RosterPicture ?? string.Empty;
            //                //FormsIdentity ident = Page.User.Identity as FormsIdentity;
            //                //var identValues = ident.Name?.Split('|');
            //                var identValues = rosterName?.Split('|');
            //                switch (identValues.Length)
            //                {
            //                    case 0:
            //                        break;
            //                    case 1:
            //                        userLogged.RosterName = identValues[0]; //RosterName = identValues[0];
            //                        break;
            //                    case 2:
            //                        //RosterName = identValues[0];
            //                        userLogged.RosterType = identValues[1]; //RosterType = identValues[1];
            //                        goto case 1;
            //                    case 3:
            //                        //RosterName = identValues[0];
            //                        //RosterType = identValues[1];
            //                        userLogged.RosterEmail = identValues[2]; //RosterEmail = identValues[2];
            //                        goto case 2;
            //                    //break;

            //                    default:
            //                        break;


            //                }
            //            }

            //        }

            //        cn.Close();

            //        Eblue.Utils.SessionTools.UserInfo = userLogged;
            //        //EvalIsUserType(UserTypeFlags.UserTypeProject).Isbool

            //        //if (userLogged != null  && (
            //        //    userLogged.IsAdmin || 
            //        //    userLogged.IsManager ||
            //        //    userLogged.IsSupervisor ||
            //        //    userLogged.IsPersonnel || 
            //        //    userLogged.IsScientist || 
            //        //    userLogged.IsStudent ||
            //        //    userLogged.IsDeveloper ||
            //        //    userLogged.IsCoordinator))

            //        #region deprecated for this moment
            //        //if (EvalIsUserType(UserTypeFlags.UserType).Isbool)
            //        //{

            //        //    if (userLogged.RosterTypeID != null)
            //        //    {
            //        //        GenerateTargetPages(userLogged.RosterTypeID.Value);
            //        //    }

            //        //    #region deprecated
            //        //    //if (userLogged.IsAdmin || userLogged.IsDeveloper)
            //        //    //{
            //        //    //    ConstructActionPanelsByGeneral();
            //        //    //}
            //        //    //else
            //        //    //{
            //        //    //    //for stablish 
            //        //    //    //ConstructActionPanels(userId);
            //        //    //    if (userLogged.RosterTypeID != null)
            //        //    //    {
            //        //    //        GenerateTargetPages(userLogged.RosterTypeID.Value);
            //        //    //    }

            //        //    //    ConstructActionPanelsByProfile(userId);

            //        //    //}
            //        //    #endregion


            //        //}
            //        #endregion


            //        //ConstructActionPanels(userId);
            //        //var urlRedirection = FormsAuthentication.GetRedirectUrl(rosterName, true).Trim();
            //        //if (string.IsNullOrEmpty(urlRedirection))
            //        //{
            //        //    FormsAuthentication.RedirectFromLoginPage(rosterName, true);
            //        //}
            //        //else
            //        //{
            //        //    var hasSingOut = urlRedirection.ToLower().Contains("signout");

            //        //    if 

            //        //    FormsAuthentication.RedirectFromLoginPage(rosterName, true);
            //        //}
            //        //FormsAuthentication.RedirectFromLoginPage(rosterName, true);
            //        FormsAuthentication.RedirectFromLoginPage("fmontano", true);
            //    }
            //}
            //else 
            //{

            //    throw new Exception("the default work process flow of projects has not yet been established");


            //}
            #endregion

        }




        #region deprecate by the moment
        //        protected void ConstructActionPanelsByGeneral()
        //        {

        //            Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;

        //            //if (tuples == null)
        //            //{
        //            //tuples = new Eblue.Code.PanelTuple();
        //            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //            {
        //                string stringCommand = @"
        //select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId 
        //inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid
        //where  ut.targetof is null  or (ut.targetof is not null and  ut.isagrupation =1) order by ut.orderline
        //";
        //                //SqlCommand cmd = new SqlCommand(
        //                //    $"select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId " +
        //                //    $"inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid  " +
        //                //    $"where  ut.targetof is null  or (ut.targetof is not null and  ut.isagrupation =1) order by ut.orderline", cn);

        //                SqlCommand cmd = new SqlCommand(stringCommand, cn);

        //                cn.Open();
        //                //SqlParameter param = new SqlParameter();
        //                //param.ParameterName = "@email";
        //                //param.Value = Email.Text;
        //                //cmd.Parameters.Add(param);
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader.HasRows)
        //                {

        //                    int idxPanel = 1;
        //                    Eblue.Code.ActionPanel panel = null;
        //                    while (reader.Read())
        //                    {
        //                        Guid uid = Guid.Parse(reader["UID"].ToString());
        //                        string description = reader["description"].ToString();
        //                        string route = reader["route"].ToString();
        //                        bool notvisibleformenu = (bool)reader["notvisibleformenu"];


        //                        //tuples.Add(new Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>(uid, description, route, new List<Tuple<Guid, string, string>>()));
        //                        //tuples.Add(new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu));
        //                        //int? idxSlash = route?.IndexOf('/');

        //                        //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) route = $"/{route}";

        //                        //route = this.ResolveClientUrl($"~{route}");
        //                        panel = new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                        panel.OrderLine = idxPanel; idxPanel++;
        //                        tuples.Add(panel);
        //                    }
        //                    reader.Close();


        //                    tuples.ForEach(x =>
        //                    {

        //                        //string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where ut.TargetOf = '{x.Item1}'";

        //                        //SqlCommand cmdOptions = new SqlCommand(commandString, cn);
        //                        SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

        //                        SqlDataReader readerOptions = cmdOptions.ExecuteReader();

        //                        if (readerOptions.HasRows)
        //                        {
        //                            int idxLink = 1;
        //                            while (readerOptions.Read())
        //                            {

        //                                Guid uidOption = Guid.Parse(readerOptions["UID"].ToString());
        //                                string descriptionOption = readerOptions["description"].ToString();
        //                                string routeOption = readerOptions["route"].ToString();
        //                                bool notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

        //                                //int? idxSlash = routeOption?.IndexOf('/');

        //                                //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //                                //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //                                //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //                                var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
        //                                link.Parent = x;
        //                                link.OrderLine = idxLink; idxLink++;
        //                                x.ActionPages.Add(link);
        //                            }

        //                        }

        //                        readerOptions.Close();

        //                    });



        //                    //SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
        //                    //checkcmd.Parameters.AddWithValue("@email", Email.Text);
        //                    //checkcmd.Parameters.AddWithValue("@pass", securetoken);
        //                    //checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

        //                    //int result = Convert.ToInt32(checkcmd.ExecuteScalar());
        //                    cn.Close();
        //                    //if ((result == 1))
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
        //                    //    //Response.Redirect("~/DashBoard.aspx");
        //                    //    hasSigning = true;
        //                    //}
        //                    //else
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Empty;
        //                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
        //                    //}

        //                }

        //            }


        //            //if (Eblue.Utils.WebTools.GenerateTextFor(out string innerText, tuples))
        //            //{

        //            //    tuples.InnerText = innerText;

        //            //}


        //            Eblue.Utils.SessionTools.ActionPanels = tuples;
        //            //}

        //        }
        //        protected void ConstructActionPanels(Guid userId)
        //        {

        //            Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;

        //            //if (tuples == null)
        //            //{
        //            //tuples = new Eblue.Code.PanelTuple();
        //            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand(
        //                    $"select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId " +
        //                    $"inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid and ut.targetof is null " +
        //                    $"where u.UID = '{userId}' order by ut.orderline", cn);
        //                cn.Open();
        //                //SqlParameter param = new SqlParameter();
        //                //param.ParameterName = "@email";
        //                //param.Value = Email.Text;
        //                //cmd.Parameters.Add(param);
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader.HasRows)
        //                {

        //                    int idxPanel = 1;
        //                    Eblue.Code.ActionPanel panel = null;
        //                    while (reader.Read())
        //                    {
        //                        Guid uid = Guid.Parse(reader["UID"].ToString());
        //                        string description = reader["description"].ToString();
        //                        string route = reader["route"].ToString();
        //                        bool notvisibleformenu = (bool)reader["notvisibleformenu"];


        //                        //tuples.Add(new Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>(uid, description, route, new List<Tuple<Guid, string, string>>()));
        //                        //tuples.Add(new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu));
        //                        //int? idxSlash = route?.IndexOf('/');

        //                        //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) route = $"/{route}";

        //                        //route = this.ResolveClientUrl($"~{route}");
        //                        panel = new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                        panel.OrderLine = idxPanel; idxPanel++;
        //                        tuples.Add(panel);
        //                    }
        //                    reader.Close();


        //                    tuples.ForEach(x =>
        //                    {

        //                        //string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where ut.TargetOf = '{x.Item1}'";

        //                        //SqlCommand cmdOptions = new SqlCommand(commandString, cn);
        //                        SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

        //                        SqlDataReader readerOptions = cmdOptions.ExecuteReader();

        //                        if (readerOptions.HasRows)
        //                        {
        //                            int idxLink = 1;
        //                            while (readerOptions.Read())
        //                            {

        //                                Guid uidOption = Guid.Parse(readerOptions["UID"].ToString());
        //                                string descriptionOption = readerOptions["description"].ToString();
        //                                string routeOption = readerOptions["route"].ToString();
        //                                bool notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

        //                                //int? idxSlash = routeOption?.IndexOf('/');

        //                                //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //                                //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //                                //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //                                var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
        //                                link.Parent = x;
        //                                link.OrderLine = idxLink; idxLink++;
        //                                x.ActionPages.Add(link);
        //                            }

        //                        }

        //                        readerOptions.Close();

        //                    });



        //                    //SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
        //                    //checkcmd.Parameters.AddWithValue("@email", Email.Text);
        //                    //checkcmd.Parameters.AddWithValue("@pass", securetoken);
        //                    //checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

        //                    //int result = Convert.ToInt32(checkcmd.ExecuteScalar());
        //                    cn.Close();
        //                    //if ((result == 1))
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
        //                    //    //Response.Redirect("~/DashBoard.aspx");
        //                    //    hasSigning = true;
        //                    //}
        //                    //else
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Empty;
        //                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
        //                    //}

        //                }

        //            }


        //            //if (Eblue.Utils.WebTools.GenerateTextFor(out string innerText, tuples))
        //            //{

        //            //    tuples.InnerText = innerText;

        //            //}


        //            Eblue.Utils.SessionTools.ActionPanels = tuples;
        //            //}

        //        }

        //        protected void ConstructActionPanelsByProfile(Guid userId)
        //        {
        //            //Eblue.Code.PanelTuple profiles = new PanelTuple();
        //            Eblue.Code.ProfileTupleList profiles = new ProfileTupleList() { InnerText = "noset" };  /*new ProfileTuple() { InnerText = "noset" };*/
        //            //Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;
        //            Guid uid;
        //            string description;
        //            string route;
        //            bool notvisibleformenu;
        //            string iconClass;
        //            //if (tuples == null)
        //            //{
        //            //tuples = new Eblue.Code.PanelTuple();
        //            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand(
        //                    $"select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId " +
        //                    $"inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid  " +
        //                    $"where ut.isroot = 1 and u.UID = '{userId}' order by ut.orderline", cn);
        //                cn.Open();
        //                //SqlParameter param = new SqlParameter();
        //                //param.ParameterName = "@email";
        //                //param.Value = Email.Text;
        //                //cmd.Parameters.Add(param);
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader.HasRows)
        //                {

        //                    int idxProfile = 1;
        //                    Eblue.Code.ProfileTuple profile = null;
        //                    while (reader.Read())
        //                    {
        //                        uid = Guid.Parse(reader["UID"].ToString());
        //                        description = reader["description"].ToString();
        //                        route = reader["route"].ToString();
        //                        notvisibleformenu = (bool)reader["notvisibleformenu"];


        //                        //tuples.Add(new Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>(uid, description, route, new List<Tuple<Guid, string, string>>()));
        //                        //tuples.Add(new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu));
        //                        //int? idxSlash = route?.IndexOf('/');

        //                        //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) route = $"/{route}";

        //                        //route = this.ResolveClientUrl($"~{route}");
        //                        profile = new ProfileTuple(uid, description, route, new AgrupationTupleList(), notvisibleformenu) { }; // new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                        profile.OrderLine = idxProfile; idxProfile++;
        //                        //tuples.Add(panel);
        //                        profiles.Add(profile);
        //                    }
        //                    reader.Close();

        //                    //from <root> get agrupation
        //                    profiles.ForEach(thisProfile => {

        //                        string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where utp.isagrupation= 1 and utp.TargetOf = '{thisProfile.Item1}'";

        //                        SqlCommand cmdAgrupations = new SqlCommand(commandString, cn);
        //                        //SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

        //                        SqlDataReader readerAgrupations = cmdAgrupations.ExecuteReader();

        //                        if (readerAgrupations.HasRows)
        //                        {
        //                            int idxAgrupation = 1;
        //                            Eblue.Code.AgrupationTuple agrupation = null;
        //                            while (readerAgrupations.Read())
        //                            {

        //                                uid = Guid.Parse(readerAgrupations["UID"].ToString());
        //                                description = readerAgrupations["description"].ToString();
        //                                route = readerAgrupations["route"].ToString();
        //                                notvisibleformenu = (bool)readerAgrupations["notvisibleformenu"];
        //                                iconClass = readerAgrupations["iconClass"]?.ToString();
        //                                //int? idxSlash = routeOption?.IndexOf('/');

        //                                //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //                                //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //                                //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //                                //var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
        //                                //link.Root = w;
        //                                //link.OrderLine = idxLink; idxLink++;
        //                                //w.ActionPages.Add(link);
        //                                agrupation = new AgrupationTuple(uid, description, route, new ActionPageList(), notvisibleformenu, iconClass); // new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                                agrupation.OrderLine = idxAgrupation; idxAgrupation++;
        //                                thisProfile.Agrupations.Add(agrupation);
        //                                //tuples.Add(panel);
        //                                //profiles.Add(profile);
        //                            }

        //                        }

        //                        readerAgrupations.Close();
        //                    });

        //                    //from <agrupation> get routes
        //                    profiles.ForEach(thisProfile => {

        //                        thisProfile.Agrupations.ForEach(thisAgrupation => {


        //                            SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{thisAgrupation.Item1}'", cn);

        //                            SqlDataReader readerOptions = cmdOptions.ExecuteReader();

        //                            if (readerOptions.HasRows)
        //                            {
        //                                int idxLink = 1;
        //                                while (readerOptions.Read())
        //                                {

        //                                    uid = Guid.Parse(readerOptions["UID"].ToString());
        //                                    description = readerOptions["description"].ToString();
        //                                    route = readerOptions["route"].ToString();
        //                                    notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

        //                                    //int? idxSlash = routeOption?.IndexOf('/');

        //                                    //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //                                    //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //                                    //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //                                    var page = new Eblue.Code.ActionPage(uid, description, route, notvisibleformenu);
        //                                    page.Root = thisProfile;
        //                                    page.Agrupation = thisAgrupation;
        //                                    page.OrderLine = idxLink; idxLink++;
        //                                    thisAgrupation.ActionPages.Add(page);
        //                                    //x.ActionPages.Add(link);
        //                                }

        //                            }

        //                            readerOptions.Close();


        //                        });


        //                    });

        //                    //from <agrupation> get routes
        //                    //tuples.ForEach(x =>
        //                    //{

        //                    //    //string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where ut.TargetOf = '{x.Item1}'";

        //                    //    //SqlCommand cmdOptions = new SqlCommand(commandString, cn);
        //                    //    SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

        //                    //    SqlDataReader readerOptions = cmdOptions.ExecuteReader();

        //                    //    if (readerOptions.HasRows)
        //                    //    {
        //                    //        int idxLink = 1;
        //                    //        while (readerOptions.Read())
        //                    //        {

        //                    //            Guid uidOption = Guid.Parse(readerOptions["UID"].ToString());
        //                    //            string descriptionOption = readerOptions["description"].ToString();
        //                    //            string routeOption = readerOptions["route"].ToString();
        //                    //            bool notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

        //                    //            //int? idxSlash = routeOption?.IndexOf('/');

        //                    //            //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //                    //            //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //                    //            //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //                    //            var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
        //                    //            link.Parent = x;
        //                    //            link.OrderLine = idxLink; idxLink++;
        //                    //            x.ActionPages.Add(link);
        //                    //        }

        //                    //    }

        //                    //    readerOptions.Close();

        //                    //});



        //                    //SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
        //                    //checkcmd.Parameters.AddWithValue("@email", Email.Text);
        //                    //checkcmd.Parameters.AddWithValue("@pass", securetoken);
        //                    //checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

        //                    //int result = Convert.ToInt32(checkcmd.ExecuteScalar());
        //                    cn.Close();
        //                    //if ((result == 1))
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
        //                    //    //Response.Redirect("~/DashBoard.aspx");
        //                    //    hasSigning = true;
        //                    //}
        //                    //else
        //                    //{
        //                    //    Eblue.Utils.SessionTools.UserId = Guid.Empty;
        //                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
        //                    //}

        //                }

        //            }


        //            //if (Eblue.Utils.WebTools.GenerateTextFor(out string innerText, tuples))
        //            //{

        //            //    tuples.InnerText = innerText;

        //            //}


        //            Eblue.Utils.SessionTools.UserProfile = profiles;
        //            //}

        //        }

        //        protected void ConstructActionPanelsByTargets(TargetSectionSet model)
        //        {
        //            //Eblue.Code.ProfileTupleList profiles = new ProfileTupleList() { InnerText = "noset" };

        //            Guid uid;
        //            string description;
        //            string route;
        //            bool notvisibleformenu;
        //            string iconClass;

        //            //int idxProfile = 1;
        //            //profile is like a <sys, proj, adm, rep, sett >Panel
        //            var profileList = model.Values.Where(v => v.isroot && v.targetId != null);


        //            Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;



        //            int idxPanel = 1;
        //            Eblue.Code.ActionPanel panel = null;
        //            //var profileList = model.Values.Where(v => v.isroot && v.targetId != null);
        //            foreach (var itemProfile in profileList)
        //            {
        //                //uid = itemProfile.UId;
        //                uid = itemProfile.targetId.Value;
        //                description = itemProfile.description;
        //                route = itemProfile.route;
        //                notvisibleformenu = itemProfile.notvisibleformenu;

        //                panel = new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                panel.OrderLine = idxPanel; idxPanel++;
        //                tuples.Add(panel);
        //            }



        //            tuples.ForEach(x =>
        //            {
        //                var thisPanel = x;

        //                var optionList = model.Values.Where(v => v.parentId == thisPanel.Item1 && v.targetId != null);

        //                int idxLink = 1;
        //                foreach (var option in optionList)
        //                {

        //                    Guid uidOption = option.targetId.Value;
        //                    string descriptionOption = option.description;
        //                    string routeOption = option.route;
        //                    bool notvisibleformenuOption = option.notvisibleformenu;

        //                    var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenuOption);
        //                    link.Parent = x;
        //                    link.OrderLine = idxLink; idxLink++;
        //                    x.ActionPages.Add(link);
        //                }



        //            });







        //            Eblue.Utils.SessionTools.ActionPanels = tuples;


        //        }

        //        protected void ConstructActionPanelsByProfile(TargetSectionSet model)
        //        {
        //            Eblue.Code.ProfileTupleList profiles = new ProfileTupleList() { InnerText = "noset" };

        //            Guid uid;
        //            string description;
        //            string route;
        //            bool notvisibleformenu;
        //            string iconClass;

        //            int idxProfile = 1;
        //            //profile is like a <sys, proj, adm, rep, sett >Panel
        //            var profileList = model.Values.Where(v => v.isroot && v.targetId != null);
        //            foreach (var itemProfile in profileList)
        //            {
        //                //uid = itemProfile.UId;
        //                uid = itemProfile.targetId.Value;
        //                description = itemProfile.description;
        //                route = itemProfile.route;
        //                notvisibleformenu = itemProfile.notvisibleformenu;


        //                var profile = new ProfileTuple(uid, description, route, new AgrupationTupleList(), notvisibleformenu) { }; // new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //                profile.OrderLine = idxProfile; idxProfile++;
        //                //tuples.Add(panel);
        //                profiles.Add(profile);


        //            }

        //            ////agrupation is like 
        //            ////from <root> get agrupation
        //            //profiles.ForEach(thisProfile => {

        //            //    string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where utp.isagrupation= 1 and utp.TargetOf = '{thisProfile.Item1}'";

        //            //    SqlCommand cmdAgrupations = new SqlCommand(commandString, cn);
        //            //    //SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

        //            //    SqlDataReader readerAgrupations = cmdAgrupations.ExecuteReader();

        //            //    if (readerAgrupations.HasRows)
        //            //    {
        //            //        int idxAgrupation = 1;
        //            //        Eblue.Code.AgrupationTuple agrupation = null;
        //            //        while (readerAgrupations.Read())
        //            //        {

        //            //            uid = Guid.Parse(readerAgrupations["UID"].ToString());
        //            //            description = readerAgrupations["description"].ToString();
        //            //            route = readerAgrupations["route"].ToString();
        //            //            notvisibleformenu = (bool)readerAgrupations["notvisibleformenu"];
        //            //            iconClass = readerAgrupations["iconClass"]?.ToString();
        //            //            //int? idxSlash = routeOption?.IndexOf('/');

        //            //            //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //            //            //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //            //            //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //            //            //var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
        //            //            //link.Root = w;
        //            //            //link.OrderLine = idxLink; idxLink++;
        //            //            //w.ActionPages.Add(link);
        //            //            agrupation = new AgrupationTuple(uid, description, route, new ActionPageList(), notvisibleformenu, iconClass); // new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
        //            //            agrupation.OrderLine = idxAgrupation; idxAgrupation++;
        //            //            thisProfile.Agrupations.Add(agrupation);
        //            //            //tuples.Add(panel);
        //            //            //profiles.Add(profile);
        //            //        }

        //            //    }

        //            //    readerAgrupations.Close();
        //            //});

        //            ////from <agrupation> get routes
        //            //profiles.ForEach(thisProfile => {

        //            //thisProfile.Agrupations.ForEach(thisAgrupation => {


        //            //        SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{thisAgrupation.Item1}'", cn);

        //            //        SqlDataReader readerOptions = cmdOptions.ExecuteReader();

        //            //        if (readerOptions.HasRows)
        //            //        {
        //            //            int idxLink = 1;
        //            //            while (readerOptions.Read())
        //            //            {

        //            //                uid = Guid.Parse(readerOptions["UID"].ToString());
        //            //                description = readerOptions["description"].ToString();
        //            //                route = readerOptions["route"].ToString();
        //            //                notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

        //            //                //int? idxSlash = routeOption?.IndexOf('/');

        //            //                //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

        //            //                //routeOption = this.ResolveClientUrl($"~{routeOption}");

        //            //                //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
        //            //                var page = new Eblue.Code.ActionPage(uid, description, route, notvisibleformenu);
        //            //                page.Root = thisProfile;
        //            //                page.Agrupation = thisAgrupation;
        //            //                page.OrderLine = idxLink; idxLink++;
        //            //                thisAgrupation.ActionPages.Add(page);
        //            //                //x.ActionPages.Add(link);
        //            //            }

        //            //        }

        //            //        readerOptions.Close();


        //            //    });


        //            //});



        //            Eblue.Utils.SessionTools.UserProfile = profiles;
        //            //}

        //        }

        //        protected void GenerateTargetPages(Guid rosterCategoryID)
        //        {

        //            bool result;

        //            result = GetPagePermissionsByRosterHandle(out TargetSectionSet model, rosterCategoryID);

        //            if (result)
        //            {
        //                //this.targetSections = model;
        //                var success = true;
        //                ConstructActionPanelsByTargets(model);
        //                if (success)
        //                { }
        //            }
        //            else
        //            {
        //                var stop = true;

        //                if (stop)
        //                { }

        //            }


        //        }
        #endregion


    }

}