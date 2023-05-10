using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eblue.Dome
{
    public partial class WebView : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            Eblue.Code.LoadEventArgs eventArgs = new Code.LoadEventArgs(isHome: true);

            base.Page_Load(sender, eventArgs);

            if (Page.IsPostBack)
            {
                #region page form properties
                var controls = Page.Form.Controls.OfType<WebControl>();
                var buttons = controls.Where(ctrl => ctrl is Button).OfType<Button>();
                #endregion

                #region page form properties

                GetRequestParamEventTarget(out string eventTarget, this.Page);
                GetRequestParamEventArgument(out string eventArgument, this.Page);

                var vars = this.Request.Form;
                
                if (sender is Button button) {

                    var stop = true;
                    if (stop) {
                    }

                }

                if (sender is GridView gridview)
                {
                    var stop = true;
                    if (stop)
                    {
                    }

                }




                //if (GetRequestParamEventTarget(out string eventTarget, this.Page))
                //{
                //    //var tabPrefix = "tab_";
                //    //bool isTab = eventTarget.IndexOf(tabPrefix, StringComparison.InvariantCultureIgnoreCase) == 0;

                //    //if (isTab && GetRequestParamEventArgument(out string eventArgument, this.Page))
                //    //{
                //    //    //var args = eventArgument.Split('|');
                //    //    //if (args.Length == 1)
                //    //    //{
                //    //    //    //TabSelectedIndex = args[0];
                //    //    //}
                //    //}

                //}
                #endregion
            }

        }

        protected void controlButton_Click(object sender, EventArgs e)
        {

        }

        protected void controlDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}