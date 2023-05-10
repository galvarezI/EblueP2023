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

namespace HaciendaT5K.Admin
{
    public partial class FiscalYear : Eblue.Code.PageBasic
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

        protected void ButtonSaveNewProject_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string InsertNewFiscalYear = "INSERT INTO FiscalYear (FiscalYearName, FiscalYearNumber, LastUpdate, FiscalYearStatusID) VALUES (@FiscalYearName, @FiscalYearNumber, @LastUpdate, @FiscalYearStatusID)";

                SqlCommand cmd = new SqlCommand(InsertNewFiscalYear, cn);
                cmd.Parameters.AddWithValue("@FiscalYearName", TextBoxName.Text);
                cmd.Parameters.AddWithValue("@FiscalYearNumber", TextBoxNumber.Text);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FiscalYearStatusID", DropDownListStatus.SelectedValue);
                cmd.ExecuteNonQuery();
                cn.Close();

                TextBoxName.Text = "";
                TextBoxNumber.Text = "";
                DropDownListStatus.SelectedIndex = 0;
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