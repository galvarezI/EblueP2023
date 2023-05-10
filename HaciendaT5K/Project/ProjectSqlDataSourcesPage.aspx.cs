using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eblue.Project
{
    public partial class ProjectSqlDataSourcesPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public System.Web.UI.WebControls.SqlDataSource SDSProjectProccessInfo
        {
            get {

                return this.SqlDataSourceProjectProccessInfo;
            
            }
        
        }
    }
}