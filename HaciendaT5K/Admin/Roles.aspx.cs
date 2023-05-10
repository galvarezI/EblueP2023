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

namespace Eblue.Admin
{
    public partial class Roles : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        //protected override void OnSaveStateComplete(EventArgs e)
        //{
        //    var script = "$(document).ready(function () { $('[data-widget = \"pushmenu\"]').click(); });";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", script, true);
        //}

        protected void ButtonNewRole_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
            try
            {
                cn.Open();
                string InsertNew = "INSERT INTO Roles (RoleName, Enable, RoleCategoryId) VALUES (@RoleName, @Enable, @RoleCategoryId)";

                SqlCommand cmd = new SqlCommand(InsertNew, cn);
                cmd.Parameters.AddWithValue("@RoleName", TextBoxRoleName.Text);
                cmd.Parameters.AddWithValue("@Enable", true);
                cmd.Parameters.AddWithValue("@RoleCategoryId", DropDownListType.SelectedValue);
                cmd.ExecuteNonQuery();
                cn.Close();
                this.TextBoxRoleName.Text = string.Empty;
                DropDownListType.ClearSelection();
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