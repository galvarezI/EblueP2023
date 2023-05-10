using System;
using System.Linq;

using static Eblue.Utils.SessionTools;
using Eblue.Utils;
using Eblue.Utils.DataServices;


namespace Eblue.Admin
{
    public partial class ProgramaticArea : HaciendaT5K.Admin.ProgramAreaAdmin
    {
        #region deprecated for better
        //protected new void Page_Load(object sender, EventArgs e)
        //{
        //    base.Page_Load(sender, e);
        //}

        ////protected override void OnSaveStateComplete(EventArgs e)
        ////{
        ////    var script = "$(document).ready(function () { $('[data-widget = \"pushmenu\"]').click(); });";
        ////    ClientScript.RegisterStartupScript(this.GetType(), "script", script, true);
        ////}

        //protected void ButtonNewProgram_Click(object sender, EventArgs e)
        //{
        //    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);
        //    try
        //    {
        //        cn.Open();
        //        string InsertNew = "INSERT INTO ProgramArea (ProgramAreaName) VALUES (@ProgramAreaName)";

        //        SqlCommand cmd = new SqlCommand(InsertNew, cn);
        //        cmd.Parameters.AddWithValue("@ProgramAreaName", TextBoxProgram.Text);
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


namespace HaciendaT5K.Admin
{


    public partial class ProgramAreaAdmin: Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.TextBoxProgram, this.DropDownListRoster, buttonNewModel, buttonClearModel, this.gv);//, this.DropDownPrincipalInvestigator, this.DropdownlistFiscalYear, buttonNewModel, buttonClearModel, gvModel);
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

                var idObject = cValues.ToArray()[0] ;
                int.TryParse(idObject.ToString(), out int id);
                var name = nValues.ToArray()[0];
                var rosterString = nValues.ToArray()[1];

                execEditModel(id, name.ToString(), rosterString.ToString());

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
            ClearWebControls(this.TextBoxProgram, this.DropDownListRoster, buttonNewModel, buttonClearModel, this.gv);
            this.SetFocus(this.TextBoxProgram);
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

        protected void ButtonNewModel_Click(object sender, EventArgs e) => execAddModel( this.TextBoxProgram.Text, this.DropDownListRoster.SelectedValue );

        protected void ButtonClearModel_Click(object sender, EventArgs e) => Clear();


        public void execAddModel(string name, string rosterString)
        {

            Guid.TryParse(rosterString, out Guid rosterId);

            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.InsertProgramAreaHandle(out int affectedRows, new Tuple<string, Guid>(name, rosterId), notHandlerException: null))
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

                SetFocus(TextBoxProgram);

            }


        }

        public void execEditModel(int id , string name, string rosterString)
        {

            Guid.TryParse(rosterString, out Guid rosterId);

            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.EditProgramAreaHandle(out int affectedRows, new Tuple<int, string, Guid>(id, name, rosterId), notHandlerException: null))
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

                SetFocus(TextBoxProgram);

            }


        }

        public void execDeleteModel(int id)
        {          

            //TODO - proceder a insertar el model
            if (AdminDataServiceTools.DeleteProgramAreaHandle(out int affectedRows, id, notHandlerException: null))
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

                SetFocus(TextBoxProgram);

            }


        }
    }
}