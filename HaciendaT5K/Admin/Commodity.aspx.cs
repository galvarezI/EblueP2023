using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using Eblue.Code;
namespace Eblue.Admin
{
    public partial class Commodity : PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);

        }

        protected void ButtonNewCommodity_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            try
            {
                cn.Open();
                string InsertNew = "INSERT INTO Commodity (CommName) VALUES (@CommName)";

                SqlCommand cmd = new SqlCommand(InsertNew, cn);
                cmd.Parameters.AddWithValue("@CommName", TextBoxCommodityName.Text);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                Message.Text = "opps it happen again" + ex;
                ErrorMessage.Visible = true;
            }

            gv.DataBind();
        }
    }
}