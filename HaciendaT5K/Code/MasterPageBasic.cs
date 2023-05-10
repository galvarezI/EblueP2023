using System;
using System.Linq;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using static Eblue.Utils.SessionTools;

using Eblue.Utils;

using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;
using static Eblue.Utils.ProjectTools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace Eblue.Code
{
    public class MasterPageBasic: System.Web.UI.MasterPage
    {
        #region viewState properties
        public string RosterCaption
        {

            get => (HasViewState && ViewState["RosterCaption"] is string textValue) ? textValue : string.Empty;
            set => ((HasViewState && ViewState is StateBag vs) ? vs : new StateBag())["RosterCaption"] = value;



        }
        public string SideBarMenu
        {
            get
            {
                string result = string.Empty;

                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    var resultObject = viewState["SideBarMenu"];

                    if (resultObject != null)
                        result = resultObject as string;

                }

                return result;
            }

            set
            {
                if (HasViewState)
                {
                    var viewState = this.ViewState;
                    viewState["SideBarMenu"] = value;
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
        #endregion

        #region viewState methods helpers
        public string HomeStyle() => (PanelSelectedIndex == "0" & LinkSelectedIndex == "0") ? " active" : string.Empty;
        public string WIpStyle() => (PanelSelectedIndex == "0" & LinkSelectedIndex == "-1") ? " active" : string.Empty;
        public string GetRosterName() => (UserInfo != null) ? UserInfo.RosterName : string.Empty;
        public string GetRosterCaption() => (string.IsNullOrEmpty(RosterCaption)) ? "{noset}" : RosterCaption;
        public string GetRosterPicture() => (UserInfo != null && !string.IsNullOrEmpty(UserInfo.RosterPicture)) ? UserInfo.RosterPicture : Eblue.Utils.ConstantsTools.UserGenericPictureData;

        //public string GetSideBarMenu() => (ActionPanels != null) ? ActionPanels.InnerText : "{noset}";
        public string GetSideBarMenu() => string.IsNullOrEmpty(SideBarMenu) ? "{noset}" : SideBarMenu;
        public bool? GetIsUserEvaluated() => HasUserEvaluated;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                #region for use later (like setting userProfile and isAuthentication.real)
                //FormsIdentity identity = this.Page.User.Identity as FormsIdentity;
                ////lblRosterName.Text = ident.Name;
                //IdentityUserName = identity.Name;

                //CreateNewProfile();

                //var thisProfile = CurrentProfile;

                #endregion


                //List<Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>> tuples = new List<Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>>();

                bool hasErrorEvaluating = (HasErrorEvaluating.HasValue) ? HasErrorEvaluating.Value : false;
                if (hasErrorEvaluating)
                {
                    HasErrorEvaluating = false;
                }
                else
                {
                    HasErrorEvaluating = false;
                    EvalUserHandle();
                }



                var userId = Eblue.Utils.SessionTools.UserId;


                //if (userId == Guid.Empty || tuples.Count == 0)
                if (userId == Guid.Empty)
                {
                    SignOut();
                    //Eblue.Utils.SessionTools.ActionPanels = null;
                    //FormsAuthentication.SignOut();
                    //Response.Redirect(this.ResolveClientUrl("~/default.aspx"), true);
                }


                //var userLogged = Eblue.Utils.SessionTools.UserInfo ;
                //EvalIsUserType(UserTypeFlags.UserType).Isbool

                #region better
                if (EvalIsUserType(UserTypeFlags.UserType).IsTrue)
                {
                    var userLogged = Eblue.Utils.SessionTools.UserInfo;
                    RosterCaption = userLogged.RosterTypeDescription;

                    Eblue.Code.PanelTuple tuples = Eblue.Utils.SessionTools.ActionPanels;
                    if (tuples != null && tuples.Count > 0)
                    {
                        //RenderPanels(tuples);

                        if (GenerateTextFor(out string innertextResult, tuples))
                        {

                            tuples.InnerText = innertextResult;
                            SideBarMenu = innertextResult;
                        }

                    }
                }
                #endregion

                #region deprecated
                //if (userLogged != null && (
                //    userLogged.IsAdmin ||
                //    userLogged.IsManager ||
                //    userLogged.IsSupervisor ||
                //    userLogged.IsPersonnel ||
                //    userLogged.IsScientist ||
                //    userLogged.IsStudent ||
                //    userLogged.IsDeveloper ||
                //    userLogged.IsCoordinator || userLogged.IsPlanViewer))
                //{

                //    if (userLogged.IsAdmin || userLogged.IsDeveloper || userLogged.IsCoordinator)
                //    {
                //        //ConstructActionPanelsByGeneral();
                //        Eblue.Code.PanelTuple tuples = Eblue.Utils.SessionTools.ActionPanels;
                //        if (tuples != null && tuples.Count > 0)
                //        {
                //            //RenderPanels(tuples);

                //            if (GenerateTextFor(out string innertextResult, tuples))
                //            {

                //                tuples.InnerText = innertextResult;
                //                SideBarMenu = innertextResult;
                //            }

                //        }
                //    }
                //    else
                //    {
                //        //for stablish 
                //        //ConstructActionPanels(userId);

                //        //ConstructActionPanelsByProfile(userId);
                //        var userProfile = Eblue.Utils.SessionTools.UserProfile;
                //        var innertextResult = string.Empty;

                //        if (GenerateTextFor(out innertextResult, userProfile))
                //        {
                //            userProfile.InnerText = innertextResult;
                //            SideBarMenu = innertextResult;
                //        }
                //        else
                //        {
                //            Eblue.Code.PanelTuple tuples = Eblue.Utils.SessionTools.ActionPanels;
                //            if (tuples != null && tuples.Count > 0)
                //            {
                //                //RenderPanels(tuples);

                //                if (GenerateTextFor(out innertextResult, tuples))
                //                {

                //                    tuples.InnerText = innertextResult;
                //                    SideBarMenu = innertextResult;
                //                }

                //            }
                //        }
                //    }

                //}
                #endregion






            }

        }



        
        public bool GenerateTextFor(out string output, Eblue.Code.PanelTuple container)
        {
            bool result = false;
            output = string.Empty;

           

            try
            {

                using (Control ctrl = new System.Web.UI.Control())
                {

                    container.Where(t => t.NotVisibleForMenu == false).ToList().ForEach(panel =>
                    {
                        var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();

                        var listitemPanel = new HtmlGenericControl("li") { };
                        var anchorPanel = new HtmlGenericControl("a") { };
                        var iconicPanel = new HtmlGenericControl("i") { };
                        var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
                        var iconicparagraphPanel = new HtmlGenericControl("i") { };


                        iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
                        anchorPanel.Controls.Add(iconicPanel);

                        iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

                        paragraphPanel.Controls.Add(iconicparagraphPanel);
                        anchorPanel.Controls.Add(paragraphPanel);

                        var panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " active" : string.Empty;

                        anchorPanel.Attributes.Add("class", $"nav-link{panelStyle}");
                        anchorPanel.Attributes.Add("href", $"{panel.Route}");
                        listitemPanel.Controls.Add(anchorPanel);

                        panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " menu-open" : string.Empty;

                        listitemPanel.Attributes.Add("class", $"nav-item has-treeview{panelStyle}");

                        var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

                        var unorderedlistPage = new HtmlGenericControl("ul") { };
                        actionPages.ForEach(page =>
                        {

                            var listitemPage = new HtmlGenericControl("li") { };
                            var anchorPage = new HtmlGenericControl("a") { };
                            var iconicPage = new HtmlGenericControl("i") { };
                            var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}" };

                            iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
                            anchorPage.Controls.Add(iconicPage);

                            anchorPage.Controls.Add(paragraphPage);
                            var linkStyle = (PanelSelectedIndex == $"{panel.OrderLine}" & LinkSelectedIndex == $"{page.OrderLine}") ? " active" : string.Empty;
                            anchorPage.Attributes.Add("class", $"nav-link{linkStyle}");
                            //var route = $"{ page.Route}";
                            //anchorPage.Attributes.Add("href", page.Route);
                            var route = $"{this.ResolveClientUrl($"~{ page.Route}")}";
                            anchorPage.Attributes.Add("href", route);

                            listitemPage.Controls.Add(anchorPage);

                            listitemPage.Attributes.Add("class", "nav-item");
                            unorderedlistPage.Controls.Add(listitemPage);

                        });

                        unorderedlistPage.Attributes.Add("class", "nav nav-treeview");
                        listitemPanel.Controls.Add(unorderedlistPage);

                        //menuContentArea.Controls.Add(listitemPanel);
                        ctrl.Controls.Add(listitemPanel);
                    });

                    int count = ctrl.Controls.Count;
                    if (count > 0 && Eblue.Utils.WebTools. GetTextFor(out string outputString, ctrl))
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

        public bool GenerateTextFor(out string output, Eblue.Code.ProfileTupleList userProfile)
        {
            bool result = false;
            output = string.Empty;

            var container = userProfile.SelectMany(thisProfile => thisProfile.Agrupations).Distinct();

            try
            {

                using (Control ctrl = new System.Web.UI.Control())
                {

                    container.Where(t => t.NotVisibleForMenu == false).ToList().ForEach(panel =>
                    {
                        //var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();
                        var icoPanelClass = panel.IconClass?.Trim().Replace(" ", "_").ToLower();

                        var listitemPanel = new HtmlGenericControl("li") { };
                        var anchorPanel = new HtmlGenericControl("a") { };
                        var iconicPanel = new HtmlGenericControl("i") { };
                        var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
                        var iconicparagraphPanel = new HtmlGenericControl("i") { };


                        iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
                        anchorPanel.Controls.Add(iconicPanel);

                        iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

                        paragraphPanel.Controls.Add(iconicparagraphPanel);
                        anchorPanel.Controls.Add(paragraphPanel);

                        var panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " active" : string.Empty;

                        anchorPanel.Attributes.Add("class", $"nav-link{panelStyle}");
                        anchorPanel.Attributes.Add("href", $"{panel.Route}");
                        listitemPanel.Controls.Add(anchorPanel);

                        panelStyle = (PanelSelectedIndex == $"{panel.OrderLine}") ? " menu-open" : string.Empty;

                        listitemPanel.Attributes.Add("class", $"nav-item has-treeview{panelStyle}");

                        var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

                        var unorderedlistPage = new HtmlGenericControl("ul") { };
                        actionPages.ForEach(page =>
                        {

                            var listitemPage = new HtmlGenericControl("li") { };
                            var anchorPage = new HtmlGenericControl("a") { };
                            var iconicPage = new HtmlGenericControl("i") { };
                            var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}" };

                            iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
                            anchorPage.Controls.Add(iconicPage);

                            anchorPage.Controls.Add(paragraphPage);
                            var linkStyle = (PanelSelectedIndex == $"{panel.OrderLine}" & LinkSelectedIndex == $"{page.OrderLine}") ? " active" : string.Empty;
                            anchorPage.Attributes.Add("class", $"nav-link{linkStyle}");
                            //var route = $"{ page.Route}";
                            //anchorPage.Attributes.Add("href", page.Route);
                            var route = $"{this.ResolveClientUrl($"~{ page.Route}")}";
                            anchorPage.Attributes.Add("href", route);

                            listitemPage.Controls.Add(anchorPage);

                            listitemPage.Attributes.Add("class", "nav-item");
                            unorderedlistPage.Controls.Add(listitemPage);

                        });

                        unorderedlistPage.Attributes.Add("class", "nav nav-treeview");
                        listitemPanel.Controls.Add(unorderedlistPage);

                        //menuContentArea.Controls.Add(listitemPanel);
                        ctrl.Controls.Add(listitemPanel);
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


        protected void SignOut()
        {
            WebTools.SignOut(this.Page);
            ////Eblue.Utils.SessionTools.ActionPanels = null;
            ////FormsAuthentication.SignOut();
            ////Response.Redirect(this.ResolveClientUrl("~/default.aspx"), true);
            //HasUserEvaluated = false;
            //Eblue.Utils.SessionTools.UserId = Guid.Empty;
            //Eblue.Utils.SessionTools.ActionPanels = null;

            //PanelSelectedIndex = string.Empty;
            //LinkSelectedIndex = string.Empty;

            //FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
            ////Response.Redirect(this.ResolveClientUrl("~/Default.aspx"), true);

        }

        protected void EvalUser(UserLogged userLogged)
        {
            
            if (EvalIsUserType(userLogged, UserTypeFlags.UserType).IsTrue)
            {


                if (userLogged.IsDeveloper || userLogged.IsAdministrator || userLogged.IsManager)
                {
                    ConstructActionPanelsByGeneral();
                }
                else
                {

                    if (userLogged.RosterTypeID != null)
                    {
                        GenerateTargetPages(userLogged.RosterTypeID.Value);
                    }
                }

               
            }


        }

        protected void EvalUserHandle()
        {
            if (HasUserEvaluated != null && HasUserEvaluated.Value)
            {
                var stop = true;

                if (stop)
                { }

            }
            else 
            {
                UserLogged userLogged = UserInfo;

                EvalUser(userLogged);
                HasUserEvaluated = true;

            }
        
        
        }
        protected void GenerateTargetPages(Guid rosterCategoryID)
        {

            bool result;

            result = GetPagePermissionsByRosterHandle(out TargetSectionSet model, rosterCategoryID);

            if (result)
            {
                //this.targetSections = model;
                var success = true;
                ConstructActionPanelsByTargets(model);
                if (success)
                { }
            }
            else
            {
                var stop = true;

                if (stop)
                { }

            }


        }

        protected void ConstructActionPanelsByTargets(TargetSectionSet model)
        {
            //Eblue.Code.ProfileTupleList profiles = new ProfileTupleList() { InnerText = "noset" };

            Guid uid;
            string description;
            string route;
            bool notvisibleformenu;
            string iconClass;

            //int idxProfile = 1;
            //profile is like a <sys, proj, adm, rep, sett >Panel
            var profileList = model.Values.Where(v => v.isroot && v.targetId != null);


            Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;



            int idxPanel = 1;
            Eblue.Code.ActionPanel panel = null;
            //var profileList = model.Values.Where(v => v.isroot && v.targetId != null);
            foreach (var itemProfile in profileList)
            {
                //uid = itemProfile.UId;
                uid = itemProfile.targetId.Value;
                description = itemProfile.description;
                route = itemProfile.route;
                notvisibleformenu = itemProfile.notvisibleformenu;

                panel = new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
                panel.OrderLine = idxPanel; idxPanel++;
                tuples.Add(panel);
            }



            tuples.ForEach(x =>
            {
                var thisPanel = x;

                var optionList = model.Values.Where(v => v.parentId == thisPanel.Item1 && v.targetId != null);

                int idxLink = 1;
                foreach (var option in optionList)
                {

                    Guid uidOption = option.targetId.Value;
                    string descriptionOption = option.description;
                    string routeOption = option.route;
                    bool notvisibleformenuOption = option.notvisibleformenu;

                    var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenuOption);
                    link.Parent = x;
                    link.OrderLine = idxLink; idxLink++;
                    x.ActionPages.Add(link);
                }



            });







            Eblue.Utils.SessionTools.ActionPanels = tuples;


        }

        protected void ConstructActionPanelsByGeneral()
        {

            Eblue.Code.PanelTuple tuples = new Eblue.Code.PanelTuple() { InnerText = "noset" };// Eblue.Utils.SessionTools.ActionPanels;

            //if (tuples == null)
            //{
            //tuples = new Eblue.Code.PanelTuple();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
            {
                //                string stringCommand = @"
                //select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId 
                //inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid
                //where  ut.targetof is null  or (ut.targetof is not null and  ut.isagrupation =1) order by ut.orderline
                //";
                //SqlCommand cmd = new SqlCommand(
                //    $"select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId " +
                //    $"inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid  " +
                //    $"where  ut.targetof is null  or (ut.targetof is not null and  ut.isagrupation =1) order by ut.orderline", cn);

                string stringCommand = @"
                    select 
                     ut.* 
                    from UserTarget ut 
                    where  ut.targetof is null  or (ut.targetof is not null and  ut.isagrupation =1) 
                    order by ut.orderline
";

                SqlCommand cmd = new SqlCommand(stringCommand, cn);

                cn.Open();
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@email";
                //param.Value = Email.Text;
                //cmd.Parameters.Add(param);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    int idxPanel = 1;
                    Eblue.Code.ActionPanel panel = null;
                    while (reader.Read())
                    {
                        Guid uid = Guid.Parse(reader["UID"].ToString());
                        string description = reader["description"].ToString();
                        string route = reader["route"].ToString();
                        bool notvisibleformenu = (bool)reader["notvisibleformenu"];


                        //tuples.Add(new Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>(uid, description, route, new List<Tuple<Guid, string, string>>()));
                        //tuples.Add(new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu));
                        //int? idxSlash = route?.IndexOf('/');

                        //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) route = $"/{route}";

                        //route = this.ResolveClientUrl($"~{route}");
                        panel = new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu);
                        panel.OrderLine = idxPanel; idxPanel++;
                        tuples.Add(panel);
                    }
                    reader.Close();


                    tuples.ForEach(x =>
                    {

                        //string commandString = $"select utp.* from UserTarget ut inner join UserTarget utp on utp.TargetOf = ut.UId where ut.TargetOf = '{x.Item1}'";

                        //SqlCommand cmdOptions = new SqlCommand(commandString, cn);
                        SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where utf.isroot = 0 and  utf.targetof = '{x.Item1}'", cn);

                        SqlDataReader readerOptions = cmdOptions.ExecuteReader();

                        if (readerOptions.HasRows)
                        {
                            int idxLink = 1;
                            while (readerOptions.Read())
                            {

                                Guid uidOption = Guid.Parse(readerOptions["UID"].ToString());
                                string descriptionOption = readerOptions["description"].ToString();
                                string routeOption = readerOptions["route"].ToString();
                                bool notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

                                //int? idxSlash = routeOption?.IndexOf('/');

                                //if (idxSlash == null || idxSlash > 0 || idxSlash < 0) routeOption = $"/{routeOption}";

                                //routeOption = this.ResolveClientUrl($"~{routeOption}");

                                //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
                                var link = new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu);
                                link.Parent = x;
                                link.OrderLine = idxLink; idxLink++;
                                x.ActionPages.Add(link);
                            }

                        }

                        readerOptions.Close();

                    });



                    //SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
                    //checkcmd.Parameters.AddWithValue("@email", Email.Text);
                    //checkcmd.Parameters.AddWithValue("@pass", securetoken);
                    //checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

                    //int result = Convert.ToInt32(checkcmd.ExecuteScalar());
                    cn.Close();
                    //if ((result == 1))
                    //{
                    //    Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
                    //    //Response.Redirect("~/DashBoard.aspx");
                    //    hasSigning = true;
                    //}
                    //else
                    //{
                    //    Eblue.Utils.SessionTools.UserId = Guid.Empty;
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
                    //}

                }

            }


            //if (Eblue.Utils.WebTools.GenerateTextFor(out string innerText, tuples))
            //{

            //    tuples.InnerText = innerText;

            //}


            Eblue.Utils.SessionTools.ActionPanels = tuples;
            //}

        }

    }
}