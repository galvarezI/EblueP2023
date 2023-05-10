using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eblue.Utils;
using static Eblue.Utils.SessionTools;
namespace Eblue
{
    public partial class Home : Eblue.Code.PageBasic
    {
        
        protected new void Page_Load(object sender, EventArgs e)
        {

            Eblue.Code.LoadEventArgs eventArgs = new Code.LoadEventArgs(isHome:true);

            base.Page_Load(sender, eventArgs);

            //if (Request.IsAuthenticated)
            //{
                
            //    SessionTools.PanelSelectedIndex = "0";
            //    SessionTools.LinkSelectedIndex = "0";
            //}

        }

    }
}