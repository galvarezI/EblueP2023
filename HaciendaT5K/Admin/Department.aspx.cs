//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data.SqlClient;
//using System.Data;
//using System.Configuration;
//using System.Web.Security;
//using System.Security.Cryptography;
//using System;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Web.UI;
//using Eblue.Code;


//using static Eblue.Utils.DataTools;

//using static Eblue.Utils.WebTools;
//using static Eblue.Utils.ProjectTools;
//using static Eblue.Utils.SessionTools;
//using Eblue.Utils;
//using System.Web.UI.WebControls;

using System;
using System.Linq;

using static Eblue.Utils.SessionTools;
using Eblue.Utils;
using Eblue.Utils.DataServices;


namespace Eblue.Admin
{
    public partial class Department : DepartmentAdmin
    {
        #region deprecated for better
        //protected new void Page_Load(object sender, EventArgs e)
        //{
        //    base.Page_Load(sender, e);
        //}


        //protected void ButtonNewDepartment_Click(object sender, EventArgs e)
        //{
        //    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

        //    try
        //    {
        //        cn.Open();
        //        string InsertNewFiscalYear = "INSERT INTO Department (DepartmentName, DepartmentCode, DepartmentOf) VALUES (@DepartmentName, @DepartmentCode, @DepartmentOf)";

        //        SqlCommand cmd = new SqlCommand(InsertNewFiscalYear, cn);
        //        cmd.Parameters.AddWithValue("@DepartmentName", TextBoxDPName.Text);
        //        cmd.Parameters.AddWithValue("@DepartmentCode", TextBoxDPCode.Text);
        //        cmd.Parameters.AddWithValue("@DepartmentOf", cmbSubDept.SelectedValue);
        //        cmd.ExecuteNonQuery();
        //        cn.Close();
        //    }
        //    catch (SqlException ex)
        //    {
        //        Message.Text = "opps it happen again" + ex;
        //        ErrorMessage.Visible = true;
        //    }

        //    gv.DataBind();
        //}
        #endregion

       
    }
}

namespace Eblue.Admin
{

    public partial class DepartmentAdmin : Eblue.Code.PageBasic {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.TextBoxName, this.TextBoxCode, this.DropDownListRoster, this.DropDownListDepartmentOf, buttonNewModel, buttonClearModel, this.gv);//, this.DropDownPrincipalInvestigator, this.DropdownlistFiscalYear, buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);


            if (Request.IsAuthenticated)
            {

                var userId = Eblue.Utils.SessionTools.UserId;
                var userLogged = Eblue.Utils.SessionTools.UserInfo;
                //if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsCoordinator))

                if (!EvalIsUserType(UserTypeFlags.UserTypeAdmin).IsTrue)
                {
                    var uri = Request.Url;
                    var route = base.GetAbsoluteUrl(uri);

                    base.GoToUnAuthorizeRoute(route);

                }
            }

            base.PageEventLoadPostBackForGridViewHeaders(gv);

            this.gv.RowUpdated += (gridObject, rowEventArgs) => {

                var c_keys = rowEventArgs.OldValues;
                var c_keyNames = c_keys.Keys;
                var c_keyValues = c_keys.Values;

                var n_keys = rowEventArgs.NewValues;
                var n_keyNames = n_keys.Keys;
                var n_keyValues = n_keys.Values;

                var cValues = c_keyValues.OfType<object>();
                var nValues = n_keyValues.OfType<object>();

                var idObject = cValues.ToArray()[0];
                int.TryParse(idObject.ToString(), out int id);
                var name = nValues.ToArray()[0];
                var code = nValues.ToArray()[1];
                var rosterString = nValues.ToArray()[2];
                var departmentOfString = nValues.ToArray()[3];

                execEditModel(id, name.ToString(), code.ToString(), rosterString.ToString(), departmentOfString.ToString());

            };

            this.gv.RowDeleted += (gridObject, rowEventArgs) => {
                var keys = rowEventArgs.Keys;
                var keyNames = keys.Keys;
                var keyValues = keys.Values;

                var cValues = keyValues.OfType<object>();
                var idObject = cValues.ToArray()[0];
                int.TryParse(idObject.ToString(), out int id);

                execDeleteModel(id);
            };

            //var deleteCommand = this.Projects.DeleteCommand;
            ////var nullcontraint = " and 1 = 0 ";
            //bool filterDeleteFor = deleteCommand.Contains(sqlFilter);

            //if (filterDeleteFor)
            //{
            //    this.Projects.DeleteCommand = deleteCommand.Replace(sqlFilter, sqlNullContraint);
            //}
            //else
            //{

            //}

            //if (!Page.IsPostBack)
            //{
            //    base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
            //    //BindDropDownLists();

            //}
            //else
            //{
            //    //base.PageEventLoadPostBackForGridViewHeader(this.gvModel);


            //}


        }

        protected void Clear()
        {
            //DropDownListParent.Items.Clear();


            //SqlDataSourceParent.DataBind();
            //DropDownListParent.DataBind();
            //DropDownListParent.Items.Insert(0, new ListItem("None", ""));
            //BindRecallUsers();
            ClearWebControls(this.TextBoxName, this.TextBoxCode, this.DropDownListRoster, this.DropDownListDepartmentOf, buttonNewModel, buttonClearModel, this.gv);
            this.SetFocus(this.TextBoxName);
            //TextBoxRosterName, DropDownListUser, DropDownListUserType, DropDownListDepartment, cmbLocation, DropDownListCanBePI
            //, FileSignature, UploadButtonSignature, FilePicture, UploadButtonPicture,
            //buttonNewModel, buttonClearModel, gv);
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);

            base.OnSaveStateCompleteExtend(this);
            //if (!Page.IsPostBack)
            //{
            //var statementJS = "var tbl = $('table'); $(tbl).DataTable({'paging': true,'lengthChange': false,'searching': false,'ordering': true,'info': true,'autoWidth': false }); " +
            //" $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡');";

            //var script = $"$(document).ready(function () {{{statementJS}}});";
            //ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);


            //}
        }

        protected void ButtonNewModel_Click(object sender, EventArgs e) => execAddModel(
            this.TextBoxName.Text, this.TextBoxCode.Text, 
            this.DropDownListRoster.SelectedValue, this.DropDownListDepartmentOf.SelectedValue
            );

        protected void ButtonClearModel_Click(object sender, EventArgs e) => Clear();


        public void execAddModel(string name, string code, string rosterString, string departmentOfString)
        {

            Guid.TryParse(rosterString, out Guid rosterId);
            int.TryParse(departmentOfString, out int departmentOfId);

            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.InsertDepartmentHandle(out int affectedRows, new Tuple<string,string, Guid, int>(name, code, rosterId, departmentOfId), notHandlerException: null))
            {
                Clear();
                //gv.Databind();
                gv.DataBind();



                var success = true;
                if (success)
                {

                }

            }
            else
            {
                var fail = true;
                if (fail)
                { }

                SetFocus(TextBoxName);

            }


        }

        public void execEditModel(int id, string name, string code, string rosterString, string departmentOfString)
        {

            Guid.TryParse(rosterString, out Guid rosterId);
            int.TryParse(departmentOfString, out int departmentOfId);

            
            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.EditDepartmentHandle(out int affectedRows, new Tuple<int, string, string, Guid, int>(id, name, code, rosterId, departmentOfId), notHandlerException: null))
            {

                //gv.Databind();
                gv.DataBind();



                var success = true;
                if (success)
                {

                }

            }
            else
            {
                var fail = true;
                if (fail)
                { }

                SetFocus(TextBoxName);

            }


        }

        public void execDeleteModel(int id)
        {

            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.DeleteDepartmentHandle(out int affectedRows, id, notHandlerException: null))
            {

                //gv.Databind();
                gv.DataBind();



                var success = true;
                if (success)
                {

                }

            }
            else
            {
                var fail = true;
                if (fail)
                { }

                SetFocus(TextBoxName);

            }


        }
    }

}