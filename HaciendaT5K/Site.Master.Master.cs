using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static Eblue.Utils.SessionTools;
namespace HaciendaT5K
{
    public partial class Site_Master : System.Web.UI.MasterPage
    {

        //public string RosterName
        //{
        //    get
        //    {
        //        var viewState = this.ViewState;
        //        string result = string.Empty;
        //        if (viewState != null) result = viewState["RosterName"] as string;
        //        return result;
        //    }

        //    set
        //    {
        //        var viewState = this.ViewState;
        //        if (viewState != null) viewState["RosterName"] = value;

        //    }

        //}

        //public string RosterType
        //{
        //    get
        //    {
        //        var viewState = this.ViewState;
        //        string result = string.Empty;
        //        if (viewState != null) result = viewState["RosterType"] as string;
        //        return result;
        //    }

        //    set
        //    {
        //        var viewState = this.ViewState;
        //        if (viewState != null) viewState["RosterType"] = value;

        //    }

        //}

        //public string RosterEmail
        //{
        //    get
        //    {
        //        var viewState = this.ViewState;
        //        string result = string.Empty;
        //        if (viewState != null) result = viewState["RosterEmail"] as string;
        //        return result;
        //    }

        //    set
        //    {
        //        var viewState = this.ViewState;
        //        if (viewState != null) viewState["RosterEmail"] = value;

        //    }

        //}

        //public string GetRosterName() => (UserInfo != null)? UserInfo.RosterName: string.Empty;
        //public string GetRosterPicture() => (UserInfo != null &&  !string.IsNullOrEmpty(UserInfo.RosterPicture)) ? UserInfo.RosterPicture : string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                //List<Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>> tuples = new List<Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>>();
                
                var userId = Eblue.Utils.SessionTools.UserId;              


                //if (userId == Guid.Empty || tuples.Count == 0)
                if (userId == Guid.Empty)
                {

                    Eblue.Utils.SessionTools.ActionPanels = null;
                    FormsAuthentication.SignOut();
                    Response.Redirect(this.ResolveClientUrl("~/default.aspx"), true);
                }


                #region construct action panels [obsolted]
                //if (tuples == null)
                //{
                //    tuples = new Eblue.Code.PanelTuple();
                //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString))
                //    {
                //        SqlCommand cmd = new SqlCommand(
                //            $"select distinct ut.* from users u inner join Roster r on r.UID = u.UID inner join rosterCategory rc on rc.UID = r.rosterCategoryId " +
                //            $"inner join UserRolePersimission urp on urp.rostercategoryid = rc.uid inner join UserTarget ut on ut.uid = urp.usertargetid and ut.targetof is null " +
                //            $"where u.UID = '{userId}'", cn);
                //        cn.Open();
                //        //SqlParameter param = new SqlParameter();
                //        //param.ParameterName = "@email";
                //        //param.Value = Email.Text;
                //        //cmd.Parameters.Add(param);
                //        SqlDataReader reader = cmd.ExecuteReader();
                //        if (reader.HasRows)
                //        {

                //            while (reader.Read())
                //            {
                //                Guid uid = Guid.Parse(reader["UID"].ToString());
                //                string description = reader["description"].ToString();
                //                string route = reader["route"].ToString();
                //                bool notvisibleformenu = (bool) reader["notvisibleformenu"];


                //                //tuples.Add(new Tuple<Guid, string, string, List<Tuple<Guid, string, string>>>(uid, description, route, new List<Tuple<Guid, string, string>>()));

                //                tuples.Add(new Eblue.Code.ActionPanel(uid, description, route, new Eblue.Code.ActionPanelList(), notvisibleformenu));
                //            }
                //            reader.Close();

                //            tuples.ForEach(x =>
                //            {


                //                SqlCommand cmdOptions = new SqlCommand($"select utf.* from UserTarget utf where  utf.targetof = '{x.Item1}'", cn);

                //                SqlDataReader readerOptions = cmdOptions.ExecuteReader();

                //                if (readerOptions.HasRows)
                //                {

                //                    while (readerOptions.Read())
                //                    {

                //                        Guid uidOption = Guid.Parse(readerOptions["UID"].ToString());
                //                        string descriptionOption = readerOptions["description"].ToString();
                //                        string routeOption = readerOptions["route"].ToString();
                //                        bool notvisibleformenu = (bool)readerOptions["notvisibleformenu"];

                //                        //x.Item4.Add(new Tuple<Guid, string, string>(uidOption, descriptionOption, routeOption));
                //                        x.ActionPages.Add( new Eblue.Code.ActionPage(uidOption, descriptionOption, routeOption, notvisibleformenu));
                //                    }

                //                }

                //                readerOptions.Close();

                //            });

                //            //SqlCommand checkcmd = new SqlCommand("select count(*) from Users where Email = @email and Password = @pass and EmailVerified = @emailverify", cn);
                //            //checkcmd.Parameters.AddWithValue("@email", Email.Text);
                //            //checkcmd.Parameters.AddWithValue("@pass", securetoken);
                //            //checkcmd.Parameters.AddWithValue("@emailverify", EmailVerifed);

                //            //int result = Convert.ToInt32(checkcmd.ExecuteScalar());
                //            cn.Close();
                //            //if ((result == 1))
                //            //{
                //            //    Eblue.Utils.SessionTools.UserId = Guid.Parse(UserID);
                //            //    //Response.Redirect("~/DashBoard.aspx");
                //            //    hasSigning = true;
                //            //}
                //            //else
                //            //{
                //            //    Eblue.Utils.SessionTools.UserId = Guid.Empty;
                //            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Your password is incorrect or your user does not exist, please try again')</script>");
                //            //}

                //        }

                //    }

                //    Eblue.Utils.SessionTools.ActionPanels = tuples;
                //}
                #endregion

                Eblue.Code.PanelTuple tuples = Eblue.Utils.SessionTools.ActionPanels;
                if (tuples != null && tuples.Count > 0)
                {
                    RenderPanels(tuples);

                }

                

            }

            //FormsIdentity ident = Page.User.Identity as FormsIdentity;
            //lblRosterName.Text = ident.Name;

            //FormsIdentity ident = Page.User.Identity as FormsIdentity;
            //var identValues = ident.Name?.Split('|');
            //switch (identValues.Length)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        RosterName = identValues[0];
            //        break;
            //    case 2:
            //        //RosterName = identValues[0];
            //        RosterType = identValues[1];
            //        goto case 1;
            //    case 3:
            //        //RosterName = identValues[0];
            //        //RosterType = identValues[1];
            //        RosterEmail = identValues[2];
            //        goto case 2;
            //        //break;

            //    default:
            //        break;
            
            
            //}

        }

        protected void RenderPanels(Eblue.Code.PanelTuple  actionPanels)
        {
            //LiteralControl ctrl = new LiteralControl();

            //var st = new System.IO.MemoryStream();
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(st);

            //Control ctrl = new Control();


            var ctrl = menuContentArea;
            ctrl.Controls.Clear();

            actionPanels.Where(t=> t.NotVisibleForMenu == false).ToList().ForEach(panel=> {
                var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();

                var listitemPanel = new HtmlGenericControl("li") { };
                var anchorPanel = new HtmlGenericControl("a") {  };
                var iconicPanel = new HtmlGenericControl("i") {  };
                var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
                var iconicparagraphPanel = new HtmlGenericControl("i") {  };


                iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
                anchorPanel.Controls.Add(iconicPanel);

                iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

                paragraphPanel.Controls.Add(iconicparagraphPanel);
                anchorPanel.Controls.Add(paragraphPanel);

                anchorPanel.Attributes.Add("class", "nav-link");
                anchorPanel.Attributes.Add("href", $"{panel.Route}");
                listitemPanel.Controls.Add(anchorPanel);

                listitemPanel.Attributes.Add("class", "nav-item has-treeview");

                var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

                var unorderedlistPage = new HtmlGenericControl("ul") { };
                actionPages.ForEach(page=> {
                    
                    var listitemPage = new HtmlGenericControl("li") {  };
                    var anchorPage = new HtmlGenericControl("a") {  };
                    var iconicPage = new HtmlGenericControl("i") { };
                    var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}"  };

                    iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
                    anchorPage.Controls.Add(iconicPage);

                    anchorPage.Controls.Add(paragraphPage);

                    anchorPage.Attributes.Add("class", "nav-link");
                    //var route = $"{this.ResolveClientUrl($"~{ page.Route}")}";
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

            //var count = menuContentArea.Controls.Count;
            //var count = ctrl.Controls.Count;

            //StringBuilder sb = new StringBuilder();
            //StringWriter tw = new StringWriter(sb);
            //HtmlTextWriter hw = new HtmlTextWriter(tw);
            //ctrl.RenderControl(hw);
            //var html = sb.ToString();

            // System.IO.TextWriter tw = sw;
            // HtmlTextWriter ht = new HtmlTextWriter(tw);
            // menuContentArea.RenderControl(ht);
            // Control ctrl = menuContentArea;

            // char[] result;
            // StringBuilder builder = new StringBuilder();

            // using (StreamReader reader = new StreamReader(st))
            // {
            //     result = new char[reader.BaseStream.Length];
            //     reader.Read(result, 0, (int)reader.BaseStream.Length);
            // }

            // foreach (char c in result)
            // {
            //     if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
            //     {
            //         builder.Append(c);
            //     }
            // }
            //var  FileOutput = builder.ToString();

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {

            FormsAuthentication.SignOut();
            Response.Redirect(this.ResolveClientUrl("~/Default.aspx"), true);

        }
        
    }
}