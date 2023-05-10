using System;

using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Eblue.Code;
using Eblue.Code.Models;
using Eblue.Utils;

using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;
using static Eblue.Utils.ProjectTools;
using static Eblue.Utils.SessionTools;
using System.Runtime.Serialization;
using System.Security.Permissions;

using uniqueidentifier = System.Guid;
using datetime = System.DateTime;

namespace Eblue.Project
{


    public partial class whichIparticipate : PageBasic
    {

        protected object thisObject;
        protected EventArgs thisEventArgs;
        protected LoadEventArgs thisLoadEventArgs;

        private const string sqlFilter = "$filter";
        private const string sqlNullContraint = " and 1 = 0 ";

        #region page methods
        protected new void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your Project  as been aproved.')</script>");
            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;
                //if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsCoordinator))

                if (EvalIsUserType(UserTypeFlags.UserTypeCoordinator).IsTrue || userLogged != null && (userLogged.IsAdministrator || userLogged.IsProjectOfficer || userLogged.IsBudgetOfficer ||userLogged.IsAESOfficer||userLogged.IsHumanROfficer))
                {
                    //

                    if (EvalIsUserType(UserTypeFlags.UserTypeOwner).IsTrue || userLogged.IsAdministrator || userLogged.IsProjectOfficer ||userLogged.IsBudgetOfficer)
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
                //else
                //{
                //    this.Projects.SelectCommand = "SELECT ORCID, ShowTemplate = 'Show Template', ProjectID, ProjectNumber, ContractNumber, ProjectTitle, ProjectPI, " +
                //            "(SELECT RosterName FROM Roster AS R WHERE (RosterID = P.ProjectPI)) AS RosterName, DepartmentID, CommID, DateRegister, LastUpdate, ProjectStatusID, " +
                //            "(SELECT ProjectStatusName FROM ProjectStatus AS PS WHERE (ProjectStatusID = P.ProjectStatusID)) AS ProyectStatusName, FiscalYearID, " +
                //            "(SELECT FiscalYearName FROM FiscalYear AS FS WHERE (FiscalYearID = P.FiscalYearID)) AS FiscalYearName " +
                //            "FROM Projects AS P " +
                //            "";
                //}
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
                //BindDropDownLists();

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);


            }


        


        #region for essential information(s)
        thisObject = sender;

            //deprecated for the momment
            //thisLoadEventArgs = new LoadEventArgs(isTryOut: true);
            thisLoadEventArgs = new LoadEventArgs(isWhichIparticipate: true);
            thisEventArgs = thisLoadEventArgs;

            //if (!Page.IsPostBack)
            //{

            //    this.newButton.TabIndex = 1;
            //    this.closedButton.TabIndex = 8;

            //}

            //start the request cycle
            execInit();

            
            #endregion


            #region deprecated

            //var requestData = new { requestMethod = (Page.IsPostBack) ? requestmethodFlags.Post: requestmethodFlags.Get, userLogged = UserInfo,
            //    controlNames = base.GetControlNames(),
            //    controlNamesForClear = base.GetControlNames(),
            //    controlNamesForRefresh = base.GetControlNames(), //like dropdownList|gridview
            //};

            //RequestData requestData = new RequestData()
            //{
            //    uniqueId = uniqueidentifier.NewGuid(),
            //    requestMethod = (Page.IsPostBack) ? requestmethodFlags.Post : requestmethodFlags.Get,

            //    controlNames = base.GetControlNames(),
            //    controlNamesForClear = base.GetControlNames(),
            //    controlNamesForRefresh = base.GetControlNames(), //like dropdownList|gridview
            //};


            //Action init = () => {

            //    //do before provisione for data|info|?.. page request
            //    //like base.pageLoad then evalPageLoad():base.evalPageLoad() for validatations|?..  
            //    //   -> as evalSessionExpiration:every ?..elapseSpan  :hasExpired  ? evalNavigationTo (director:Redirect|forLock, execScreenLock() 

            //    //getting basic information for data|info|?.. page request
            //    //RequestFilterBasic requestData = new RequestFilterBasic()
            //    //{
            //    //    //uniqueId = uniqueidentifier.NewGuid(),
            //    //    //requestMethod = (Page.IsPostBack) ? requestmethodFlags.Post : requestmethodFlags.Get,

            //    //    //controlNames = base.GetControlNames(),
            //    //    //controlNamesForClear = base.GetControlNames(),
            //    //    //controlNamesForRefresh = base.GetControlNames(), //like dropdownList|gridview
            //    //};

            //    Action<RequestFilterBasic> onRequest = (RequestFilterBasic reqInfo) => {

            //        //do before process request
            //        //like base.pageLoad

            //        switch (reqInfo.requestMethod)
            //        {
            //            //first time
            //            case requestmethodFlags.Get:
            //                break;

            //            //later time
            //            case requestmethodFlags.Post:
            //                break;
            //            //noset
            //            default:
            //                break;


            //        }

            //        //do after process request
            //        //like base.evalPermission|| base.settinggridHeaders
            //    };


            //};

            ////userLogged = UserInfo,

            //LoadEventArgs eArgs = new LoadEventArgs(isHome:true);

            //base.Page_Load(sender, eArgs);

            //var onBasic = new Func<bool>(() => true);
            //var onGet = new Func<bool>(() => true);
            //var onPost = new Func<bool>(() => true);

            //MarkTabIndexWebControls(txtName, txtDescription, this.DropDownListRosterType, this.DropDownListUserTarget, buttonNewModel, buttonClearModel, gvModel);

            /*onRequest = (ip)=> { 
             * gen,get,post,    
               gen out expT = new {x , y, z}
               get out bool, in expT et =>  if (!eval.(usertype,et.x) ) base.gotoUnauthorize(et.y)
             }  
             
             */
            #endregion



        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            //base.OnSaveStateCompleteSentences
            base.OnSaveStateCompleteExtend(this);
        }
        #endregion



        #region helpers

        #region sqldataString sources
        protected string sqlstringGeneral
        {
            get => 
                @"
                select 1 from x where x.t = @value
            ";
        }


        protected string sqlstringProjectStatus => @"
select
ROW_NUMBER() over (order by prc.orderline) as rowNumber,
cast(prc.EstatusId as smallint) EstatusId,
ps.ProjectStatusName as Description
from
	Process prc
	inner join ProjectStatus ps on ps.ProjectStatusID = prc.EstatusId

";
        #endregion

        #region aspx helper methods

        #region handler methods

        #endregion
        #region selection methods

        #endregion

        #region exec methods
        protected void execInit()
        {

            //validate general basic informations
            if (evalPageLoad(thisObject, thisEventArgs))
            {

                RequestFilterList filterList = evalFilterListFor();
                base.RequestDataInfo = filterList;

                Action<RequestFilterBasic> dataFilters = (filter) => {
                    base.ControlNames = filter.FilterList?.ControlNames;
                };

                Action<RequestFilterBasic> dataOptions = (filter) => {
                    execOnDataOptions();
                };

                Action<RequestFilterBasic> doBefore = (RequestFilterBasic input) => {
                    
                    dataFilters(input);

                    dataOptions(input);

                };


                Action<string, int> dataSettings = (RosterId, EstatusId) => {

                    this.Projects.SelectParameters.Clear();
                    this.Projects.SelectParameters.Add(nameof(RosterId), "");
                    this.Projects.SelectParameters[nameof(RosterId)].DefaultValue = RosterId;

                    this.Projects.SelectParameters.Add(nameof(EstatusId), "");
                    this.Projects.SelectParameters[nameof(EstatusId)].DefaultValue = EstatusId.ToString();


                };
                Action dataClausules = () => { };
                Action dataSession = () => {

                    ActivityCurrent = datetime.Now;
                
                };

                Action dataGridSetup = () => {

                    base.OnGridViewHeaders(this.gvModel, true);
                    base.OnGridViewrRowEvents(this.gvModel);
                    base.OnGridViewrRowDataEvents(this.gvModel);

                };

                Action<UserLogged> doGet = (userLogged) => {
                    if (short.TryParse(this.EstatusIdString, out short estatusId))
                    {
                        dataSettings(userLogged.RosterId.ToString(), estatusId);
                    }
                    else
                    {
                        var stop = true;

                        if (stop)
                        { }

                    }
                    
                    //bindCombos

                };

                Action<UserLogged> doPost = (userLogged) => {
                    

                    if (short.TryParse(this.EstatusIdString, out short estatusId))
                    {
                        dataSettings(userLogged.RosterId.ToString(), estatusId);
                    }
                    else
                    {
                        var stop = true;

                        if (stop)
                        { }
                    
                    }
                    
                    //deprecated for momment
                    //dataSettings(userLogged.RosterId.ToString(), this.closedButton.TabIndex);

                };


                Action<RequestFilterBasic> doAfter = (RequestFilterBasic filter) => {

                   
                    dataSession(); //captureActivity
                    dataClausules();//refreshClausules
                    dataGridSetup();
                };


                doBefore(filterList);

                switch (filterList.RequestMethod)
                {
                    //first time
                    case requestmethodFlags.Get:
                        doGet(UserInfo);
                        break;

                    //later time
                    case requestmethodFlags.Post:
                        doPost(UserInfo);
                        break;
                    //noset
                    default:
                        break;


                }

                doAfter(filterList);
            }
        
        
        }
        protected void execOnRequest(RequestFilterBasic filter)
        {


            switch (filter.RequestMethod)
            {
                //first time
                case requestmethodFlags.Get:
                    break;

                //later time
                case requestmethodFlags.Post:
                    break;
                //noset
                default:
                    break;


            }


        }

        protected void execOnBefore()
        { }

        protected void execGetFor()
        {


        }

        protected void execPostFor()
        { 
        
        
        }

        protected void execOnAfter()
        { }


        #endregion

        #region eval methods
        protected requestmethodFlags evalRequestMethod() => (Page.IsPostBack) ? requestmethodFlags.Post : requestmethodFlags.Get;
        protected RequestFilterList evalFilterListFor()
        {

            RequestFilterList filterList = new RequestFilterList()
            {
                UniqueId = uniqueidentifier.NewGuid(),
                RequestMethod = evalRequestMethod(),
                ControlNames = base.GetControlNames(),
                ControlNamesForClausule = base.GetControlNames(),
                ControlNamesForSyncData = base.GetControlNames()
            };

            return filterList;
        }

        protected RequestFilterDataList evalFilterDataListFor()
        {

            RequestFilterDataList filterDataList = new RequestFilterDataList()
            {
                UniqueId = uniqueidentifier.NewGuid(),
                RequestMethod = evalRequestMethod(),
                ControlNames = base.GetControlNames(),
                ControlNamesForClear = base.GetControlNames()
                //ControlNamesForClausule = base.GetControlNames(),
                //ControlNamesForSyncData = base.GetControlNames()
            };

            return filterDataList;
        }

        public bool evalPageLoad(object sender, EventArgs e)
        {
            bool result ;

            base.Page_Load(sender, e);
            result = true;

            return result;
        }



        #endregion

        #endregion

        #region data helper methods
        #region handler methods

        #endregion

        #region selection methods

        #endregion

        #endregion

        #endregion

        #region viewstates
        #region viewstate properties

        
        public string EstatusIdString
        {

            get => (HasBasicViewState && ViewState[nameof(EstatusIdString)] is string exp) ? exp : string.Empty;
            set => ((HasBasicViewState && ViewState is StateBag vs) ? vs : new StateBag())[nameof(EstatusIdString)] = value;

        }

        #endregion

        #region viewstate methods

        #endregion

        #endregion

        public int evalStatusIdFor(WebControl ctrl)
        {
            int result = 0;

            return result;
        
        }

        public void execOnDataSettings(string RosterId, int EstatusId)
        {
            this.Projects.SelectParameters.Clear();
            this.Projects.SelectParameters.Add(nameof(RosterId), "");
            this.Projects.SelectParameters[nameof(RosterId)].DefaultValue = RosterId;

            this.Projects.SelectParameters.Add(nameof(EstatusId), "");
            this.Projects.SelectParameters[nameof(EstatusId)].DefaultValue = EstatusId.ToString();

        }

        public void execOnDataOptions()
        {
            //var panel = this.navlinkContentArea;
            Dictionary<int, ProjectStatus> modelResult = new System.Collections.Generic.Dictionary<int, ProjectStatus>();
            var defaultValue = default(Dictionary<int, ProjectStatus>);
            var model = defaultValue;
            ProjectStatus target = default(ProjectStatus);
            Utils.RequestDataInfo req = new RequestDataInfo()
            {
                commandString = this.sqlstringProjectStatus
            };

            Action<SqlDataReader> callBack = (reader) => {
                target = new ProjectStatus();

                int.TryParse(reader["rowNumber"]?.ToString(), out int rowNumber);
                target.RowNumber = rowNumber;

                short.TryParse(reader["EstatusId"]?.ToString(), out short estatusId);
                target.EstatusId = estatusId;

                target.Description = reader["Description"]?.ToString();

                if (!modelResult.ContainsKey(target.RowNumber))
                {
                    modelResult.Add(target.RowNumber, target);
                }
            };

            bool result = DataTools.FetchData(info: req, push: callBack, exceptionInfo: out Tuple<bool?, Exception> exInfo) ;

            if (result)
                model = modelResult;

            //if (result)
            //    execOnConstructOptions(panel,model);


            //return result;

        }

        public void execOnConstructOptions(Panel container ,Dictionary<int, ProjectStatus> model)
        {

            var list = model.ToList();
            LinkButton firstButton = null;

            bool hasModels = model.Any();

            foreach (var item in list)
            {
                var desc = item.Value.Description?.Trim();
                var name = desc?.Replace(" ", string.Empty);
                var pfix = "button";
                var @class = "nav-link";
                var id = $"{name}{pfix}_";
                LinkButton button = new LinkButton() { ID = id, Text= desc, CssClass =  @class, TabIndex = item.Value.EstatusId };                
                
                //button.Click += dataButton_Click;
                //container.Controls.Add(button);

                if (firstButton == null)
                    firstButton = button;
            }

            if (hasModels)
            {

                if (!Page.IsPostBack)
                {

                    var first = model.First();
                    this.EstatusIdString = first.Value.EstatusId.ToString();

                    firstButton.CssClass = $"{firstButton.CssClass} navitemActive";

                    //execOnDataSettings(UserInfo.RosterId.ToString(), firstButton.TabIndex);
                }
                else 
                {
                
                }
                



                //this.newButton.TabIndex = 1;
                //this.newButton.Visible = true;

                //this.closedButton.TabIndex = 8;
                //this.newButton.Visible = true;

            }
            else 
            {
                //this.newButton.TabIndex = 1;
                //this.newButton.Visible = true;

                //this.closedButton.TabIndex = 8;
                //this.newButton.Visible = true;


                //if (!Page.IsPostBack)
                //{

                //    this.newButton.TabIndex = 1;
                //    this.closedButton.TabIndex = 8;

                //}

            }

        }

        protected void btncompose_Click(object sender, EventArgs e)
        {

        }

        protected void newButton_Click(object sender, EventArgs e)
        {
            //IConvertible facet = this.newButton.TabIndex;
            //short index = facet.ToInt16(null);

            //execOnDataSettings(UserInfo.RosterId.ToString(), this.newButton.TabIndex);

        }

        protected void closedButton_Click(object sender, EventArgs e)
        {
            //IConvertible facet = this.closedButton.TabIndex;
            //short index = facet.ToInt16(null);

            //execOnDataSettings(UserInfo.RosterId.ToString(), this.closedButton.TabIndex);
        }

        //protected void dataButton_Click(object sender, EventArgs e)
        //{
        //    //IConvertible facet = this.closedButton.TabIndex;
        //    //short index = facet.ToInt16(null);

        //    if (sender is LinkButton btn)
        //    {
        //        var cssClass = btn.CssClass?.Trim().ToLower();
        //        var activeNavitem = "navitemActive";
        //        var cssNavitem = activeNavitem.ToLower();
        //        if (!cssClass.Contains(activeNavitem))
        //        {

        //           var ctrls =  this.navlinkContentArea.Controls.OfType<WebControl>();
        //            var ctrlsTo = ctrls.Where(ctrl => ctrl is LinkButton);

        //           if (ctrlsTo.Any())
        //            {

        //                var navItems = ctrlsTo.OfType<LinkButton>();
        //                //var navItemsActives = navItems.Where(ctrl => ctrl.CssClass?.Trim().ToLower().Contains(cssNavitem));
        //                //var navItemsActivesAny = navItemsActives.Any();

        //                foreach (var item in navItems)
        //                {
        //                    item.CssClass = item.CssClass.Replace(activeNavitem, string.Empty);
        //                }

        //                btn.CssClass = $"{btn.CssClass} {activeNavitem}";
                    
                    
        //            }

        //        }
        //        else 
        //        {
        //            var stop = true;

        //            if (stop)
        //            { }
                
        //        }

        //        execOnDataSettings(UserInfo.RosterId.ToString(), btn.TabIndex);
        //    }

        //    //OnDataSettings(UserInfo.RosterId.ToString(), this.closedButton.TabIndex);
        //}

    }
}