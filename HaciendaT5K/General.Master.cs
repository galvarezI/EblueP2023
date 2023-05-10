using Eblue.Code;
using System;
using System.Web.Security;

namespace Eblue
{
    public partial class General : MasterPageBasic
    {

        protected new void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
        
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            base.SignOut();
        }

    }
}