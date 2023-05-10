

using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Eblue.Code.Wraps;

using static Eblue.Utils.SessionTools;

namespace Eblue.Utils
{
    public static class WebTools
    {
        #region commenteds
        //public static bool GetTextFor(out string output, System.Web.UI.Control ctrl)
        //{
        //    bool result = false;
        //    output = string.Empty;

        //    try
        //    {

        //        StringBuilder sb = new StringBuilder();
        //        using (StringWriter tw = new StringWriter(sb))
        //        {

        //            using (HtmlTextWriter hw = new HtmlTextWriter(tw))
        //            {

        //                ctrl.RenderControl(hw);
        //                var html = sb.ToString();
        //                output = html;
        //                result = true;
        //            }

        //        }




        //    }
        //    catch
        //    {


        //    }

        //    return result;

        //}
        //public static bool GenerateTextFor(out string output, Eblue.Code.PanelTuple container)
        //{
        //    bool result = false;
        //    output = string.Empty;



        //    try
        //    {

        //        using (Control ctrl = new System.Web.UI.Control())
        //        {

        //            container.Where(t => t.NotVisibleForMenu == false).ToList().ForEach(panel =>
        //            {
        //                var icoPanelClass = panel.Description?.Trim().Replace(" ", "_").ToLower();

        //                var listitemPanel = new HtmlGenericControl("li") { };
        //                var anchorPanel = new HtmlGenericControl("a") { };
        //                var iconicPanel = new HtmlGenericControl("i") { };
        //                var paragraphPanel = new HtmlGenericControl("p") { InnerText = $"{panel.Description}" };
        //                var iconicparagraphPanel = new HtmlGenericControl("i") { };


        //                iconicPanel.Attributes.Add("class", $"nav-icon fa {icoPanelClass}");
        //                anchorPanel.Controls.Add(iconicPanel);

        //                iconicparagraphPanel.Attributes.Add("class", "right fa fa-angle-left");

        //                paragraphPanel.Controls.Add(iconicparagraphPanel);
        //                anchorPanel.Controls.Add(paragraphPanel);

        //                anchorPanel.Attributes.Add("class", "nav-link");
        //                anchorPanel.Attributes.Add("href", $"{panel.Route}");
        //                listitemPanel.Controls.Add(anchorPanel);

        //                listitemPanel.Attributes.Add("class", "nav-item has-treeview");

        //                var actionPages = panel.ActionPages.Where(u => u.NotVisibleForMenu == false).ToList();

        //                var unorderedlistPage = new HtmlGenericControl("ul") { };
        //                actionPages.ForEach(page =>
        //                {

        //                    var listitemPage = new HtmlGenericControl("li") { };
        //                    var anchorPage = new HtmlGenericControl("a") { };
        //                    var iconicPage = new HtmlGenericControl("i") { };
        //                    var paragraphPage = new HtmlGenericControl("p") { InnerText = $"{page.Description}" };

        //                    iconicPage.Attributes.Add("class", "fa fa-circle-o nav-icon");
        //                    anchorPage.Controls.Add(iconicPage);

        //                    anchorPage.Controls.Add(paragraphPage);

        //                    anchorPage.Attributes.Add("class", "nav-link");
        //                    var route = $"{ page.Route}";
        //                    anchorPage.Attributes.Add("href", page.Route);
        //                    listitemPage.Controls.Add(anchorPage);

        //                    listitemPage.Attributes.Add("class", "nav-item");
        //                    unorderedlistPage.Controls.Add(listitemPage);

        //                });

        //                unorderedlistPage.Attributes.Add("class", "nav nav-treeview");
        //                listitemPanel.Controls.Add(unorderedlistPage);

        //                //menuContentArea.Controls.Add(listitemPanel);
        //                ctrl.Controls.Add(listitemPanel);
        //            });

        //            int count = ctrl.Controls.Count;
        //            if (count > 0 && GetTextFor(out string outputString, ctrl))
        //            {
        //                output = outputString;
        //                result = true;
        //            }
        //        }

        //    }
        //    catch
        //    {


        //    }

        //    return result;


        //}
        #endregion


        public static bool GetTextFor(out string output, System.Web.UI.Control ctrl)
        {
            bool result = false;
            output = string.Empty;
            int v = 5;
            int d = ~v;
            try
            {

                StringBuilder sb = new StringBuilder();
                using (StringWriter tw = new StringWriter(sb))
                {

                    using (HtmlTextWriter hw = new HtmlTextWriter(tw))
                    {

                        ctrl.RenderControl(hw);
                        var html = sb.ToString();
                        output = html;
                        result = true;
                    }

                }




            }
            catch
            {


            }

            return result;

        }

        public static bool RefreshFor(DropDownListWrap select)
        {
            bool result = false;
            bool? hasAppendItems = select.Ctrl?.AppendDataBoundItems;
            bool? hasSources = string.IsNullOrEmpty(select.Source?.SelectCommand);

            if (hasAppendItems != null && hasSources != null)
            {
                var ctrl = select.Ctrl;
                var src = select.Source;

                if (hasAppendItems.Value && hasSources.Value)
                {
                    ctrl.Items.Clear();
                    src.DataBind();
                    ctrl.DataBind();
                    ctrl.Items.Insert(0, new ListItem("None", string.Empty));
                    ctrl.Items[0].Selected = true;
                    result = true;
                }
                //DropDownListUser.Items.Clear();


                //SqlDataSourceUser.DataBind();
                //DropDownListUser.DataBind();
                //DropDownListUser.Items.Insert(0, new ListItem("", " "));

            }

            return result;

        }

        public static bool ClearSelectionFor(DropDownListWrap select)
        {
            bool result = false;
            bool? hasAppendItems = select.Ctrl?.AppendDataBoundItems;

            if (hasAppendItems != null)
            {
                var ctrl = select.Ctrl;

                ctrl.ClearSelection();

                if (hasAppendItems.Value)
                {
                    var li = ctrl.Items.FindByValue(string.Empty);
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }                

                result = true;
            }

            return result;

        }

        public static bool ClearSelectionFor(DropDownList select)
        {
            bool result = false;
            bool? hasAppendItems = select?.AppendDataBoundItems;

            if (hasAppendItems != null)
            {
                var ctrl = select;

                ctrl.ClearSelection();

                if (hasAppendItems.Value)
                {
                    var li = ctrl.Items.FindByValue(string.Empty);
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }

                result = true;
            }

            return result;

        }

        public static void HandlerExeption(string errorMessage ,StringBuilder builder ,Tuple<bool, Exception> exceptionInfo)
        {
            string result = string.Empty;
            //var errorMessage = "Error at try getting the default work process flow of projects";
            //var errorMessageList = new System.Text.StringBuilder();

            //errorMessageList.AppendLine(errorMessage);
            builder.AppendLine(errorMessage);

            if (exceptionInfo != null)
            {

                if (exceptionInfo.Item1)
                {
                    var sqlException = exceptionInfo.Item2 as SqlException;
                    var sqlErrors = sqlException.Errors.OfType<SqlError>();

                    if (sqlErrors != null)
                    {

                        var sqlErrorList = sqlErrors.ToList();
                        builder.AppendLine($"Errors from data-db source [{sqlErrorList.Count}]:");
                        var index = 1;
                        foreach (SqlError error in sqlErrorList)
                        {
                            builder.AppendLine($"{index} {error.ToString()}");
                            index++;

                        }

                        //result = builder.ToString();

                        throw new Exception(builder.ToString());

                    }

                }
                else
                {
                    throw new Exception(builder.ToString(), exceptionInfo.Item2);
                }

            }


        }

        public static void HandlerFailure(string txt) {

            throw new Exception(txt);
        
        }

        public static void HandlerFailure(core.failure exc)
        {
            if (exc is core.sqlfailure sql)
            {
                //HandlerFailure(dbe: sql);
                var msg = $"({sql.getkind.display})'{sql.GetText()}'";
                HandlerFailure(msg);
            }
            else {
                //var msg = exc.based.Message;
                //HandlerFailure(txt: msg);
                var msg = $"({exc.getkind.display})'{exc.GetText()}'";
                HandlerFailure(msg);
            }
           

        }
 
        public static void HandlerFailure(core.sqlfailure dbe)
        {
            var builder = new StringBuilder();
            var msg = string.Empty;
            var exc = dbe.based.ensure<SqlException>();
            int errCode = exc.ErrorCode;
            int lines = exc.Errors.Count;
            builder.AppendLine($"(code:{errCode}) '{exc.Message}' reason(s):");
            for (int idx=0; idx<lines; idx++) {
                var err = exc.Errors[idx];
                builder.AppendLine($"(number:{err.Number}) '{err.Message}'");
            }
            msg = builder.ToString();
            HandlerFailure(txt: msg);

        }

        public static void HandlerExeption(string errorMessage, StringBuilder builder, Tuple<bool?, Exception> exceptionInfo)
        {
            string result = string.Empty;
            //var errorMessage = "Error at try getting the default work process flow of projects";
            //var errorMessageList = new System.Text.StringBuilder();

            //errorMessageList.AppendLine(errorMessage);
            builder.AppendLine(errorMessage);

            if (exceptionInfo != null)
            {

                if (exceptionInfo.Item1 != null && exceptionInfo.Item1.Value)
                {
                    var sqlException = exceptionInfo.Item2 as SqlException;
                    var sqlErrors = sqlException.Errors.OfType<SqlError>();

                    if (sqlErrors != null)
                    {

                        var sqlErrorList = sqlErrors.ToList();
                        builder.AppendLine($"Errors from data-db source [{sqlErrorList.Count}]:");
                        var index = 1;
                        foreach (SqlError error in sqlErrorList)
                        {
                            builder.AppendLine($"{index} {error.ToString()}");
                            index++;

                        }

                        result = builder.ToString();

                        throw new Exception(result);

                    }

                }
                else
                {
                    result = builder.ToString();
                    throw new Exception(result, exceptionInfo.Item2);
                }
            }
        }


        #region about system.web.security

        public static void SignOut(Page thisPage)
        {

            HasUserEvaluated = false;
            HasErrorEvaluating = false;
            UserId = Guid.Empty;
            ActionPanels = null;

            PanelSelectedIndex = string.Empty;
            LinkSelectedIndex = string.Empty;

            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();

            thisPage.Response.Redirect(thisPage.ResolveClientUrl("~/default.aspx"), true);

        }

        #endregion


    }
}