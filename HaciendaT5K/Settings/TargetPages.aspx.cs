using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Eblue.Code;

using System.Linq;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;
using System.Web.UI.WebControls;

namespace Eblue.Settings
{
    public partial class TargetPages : PageBasic
    {
        #region page properties
        public bool GetIsGridModelEditing() => this.GetIsGridVModelEditingFor<GridView>(this.gvModel);
        

        #endregion
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(txtName, txtDescription, txtRoute, checkboxIsNotVisibleForMenu, this.checkboxIsRoot, this.checkboxIsIsAgrupation, DropDownListTargetOf, this.txtIconClass,  buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);
            int editIndex;
            if (!Page.IsPostBack)
            {
                base.PageEventLoadNotPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                this.gvModel.RowUpdated += gvModel_RowUpdated;

                BindDropDownLists();
                editIndex = gvModel.EditIndex;
            }
            else
            {
                editIndex = gvModel.EditIndex;
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;               

                this.gvModel.RowUpdated += gvModel_RowUpdated;
            }

        }       

        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);
            
        }


        #region control's action
        protected void ButtonNewModel_Click(object sender, EventArgs e)
        {
            if (!GetIsGridModelEditing())
            {
                AddModel();
            }
            else 
            {
            
            }
        }
        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            if (!GetIsGridModelEditing())
            {
                Clear();
                this.SetFocus(txtName);
            }
            else
            {

            }
        }

        protected void gvModel_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

            UpdateModelFromGridRow(e);

        }

        #endregion

        #region control's action binds

        protected void AddModel()
        {

            string name = txtName.Text;
            string description = txtDescription.Text;
            string route = txtRoute.Text;
            string iconClass = this.txtIconClass.Text;


            bool isNotVisibleForMenu = checkboxIsNotVisibleForMenu.Checked;
            bool isroot = this.checkboxIsRoot.Checked;
            bool isagrupation = this.checkboxIsIsAgrupation.Checked;


            Guid? targetOfID = null;
            if (Guid.TryParse(this.DropDownListTargetOf.SelectedValue, out Guid tID))
            {
                targetOfID = tID;
            }

            Tuple<bool, bool, bool> checks = new Tuple<bool, bool, bool>(isNotVisibleForMenu, isroot, isagrupation);


            if (AddModelToDB(name: name, description: description, route: route,  targetOfID: targetOfID, checks: checks, iconClass: iconClass))
            {

                Clear();
                //gv.Databind();
                gvModel.DataBind();
                SetFocus(txtName);


            }

        }

        protected void UpdateModelFromGridRow(System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

            int rowCount = e.AffectedRows;
            int editIndex = gvModel.EditIndex;
            object datasourceObject = gvModel.DataSource;

            if (editIndex >= 0)
            {
                int colCount = e.NewValues.Keys.Count;
                if (colCount > 2)
                {                    

                    var datarow = gvModel.Rows[editIndex];
                    

                    var valueUIDStringOld = e.OldValues[0] as string;
                    Guid.TryParse(valueUIDStringOld, out Guid uid);

                    var name = e.NewValues[2] as string;
                    var description = e.NewValues[3] as string;
                    var route = e.NewValues[4] as string;
                    bool.TryParse(e.NewValues[5].ToString().ToLower(), out bool notvisibleformenu);
                    bool.TryParse(e.NewValues[6].ToString().ToLower(), out bool isroot);
                    bool.TryParse(e.NewValues[7].ToString().ToLower(), out bool isagrupation);

                    var targetofIdString = e.NewValues[8] as string;
                    Guid.TryParse(targetofIdString, out Guid targetofID);

                    var iconClass = e.NewValues[9] as string;

                    Tuple<bool, bool, bool> checks = new Tuple<bool, bool, bool>(notvisibleformenu, isroot, isagrupation);

                    if (UpdateModelToDB(uid, name, description, route, targetofID, checks, iconClass))
                    {


                    }


                }

            }


        }

        protected void Clear()
        {
            BindDropDownLists();
            ClearWebControls(txtName, txtDescription, txtRoute, checkboxIsNotVisibleForMenu, this.checkboxIsRoot, this.checkboxIsIsAgrupation, DropDownListTargetOf, this.txtIconClass);
        }

        public void BindDropDownLists()
        {

            BindTargetOfList();
            

        }

        public void BindTargetOfList()
        {
            this.SqlDataSourceListTargetOf.SelectParameters.Clear();
           

            this.DropDownListTargetOf.Items.Clear();


            this.SqlDataSourceListTargetOf.DataBind();
            DropDownListTargetOf.DataBind();
            DropDownListTargetOf.Items.Insert(0, new ListItem("", "None"));


        }
        #endregion


        #region data model actions



        protected bool AddModelToDB(string name, string description, string route, Guid? targetOfID, Tuple<bool, bool, bool> checks, string iconClass)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into UserTarget (uid, name, description, route, targetOf, " +
                    $"NotVisibleForMenu, isroot, isagrupation, iconclass) " +
                    $"values (@uid,@name,@description,@route, " +
                    $"iif(@targetOf = '', null, @targetOf ), @NotVisibleForMenu, @isroot, @isagrupation, @iconClass) ";

                //$"values (@uid, @name, @)" +
                //$"select newid(), '{name}', '{description}', {priority}, " +
                //$"{Convert.ToInt32(whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                //$"{Convert.ToInt32(whichCanDo.Item1)},{Convert.ToInt32(whichCanDo.Item2)},{Convert.ToInt32(whichCanDo.Item3)},{Convert.ToInt32(whichCanDo.Item4)} ";

                var uid = Guid.NewGuid();
                string targetOf = targetOfID == null ? string.Empty : targetOfID.ToString();
               

                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@route", route);
                cmd.Parameters.AddWithValue("@targetOf", targetOf);
                cmd.Parameters.AddWithValue("@NotVisibleForMenu", checks.Item1);
                cmd.Parameters.AddWithValue("@isroot", checks.Item2);
                cmd.Parameters.AddWithValue("@isagrupation", checks.Item3);
                cmd.Parameters.AddWithValue("@iconClass", iconClass);




                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add user target page", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool UpdateModelToDB(Guid uid, string name, string description, string route, Guid? targetOfID, Tuple<bool, bool, bool> checks, string iconClass)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string command =
                    $"update UserTarget set name = @name, description = @description, route = @route, targetOf = @targetOf, " +
                    $"NotVisibleForMenu = @NotVisibleForMenu, isroot = @isroot, isagrupation = @isagrupation, iconClass = @iconClass " +
                    " where uid = @uid ";            
                
                string targetOf = targetOfID == null ? string.Empty : targetOfID.ToString();


                SqlCommand cmd = new SqlCommand(command, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@route", route);
                cmd.Parameters.AddWithValue("@targetOf", targetOf);
                //cmd.Parameters.AddWithValue("@NotVisibleForMenu", isNotVisibleForMenu);
                cmd.Parameters.AddWithValue("@NotVisibleForMenu", checks.Item1);
                cmd.Parameters.AddWithValue("@isroot", checks.Item2);
                cmd.Parameters.AddWithValue("@isagrupation", checks.Item3);
                cmd.Parameters.AddWithValue("@iconClass", iconClass);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add user target page", ex);
            }
            finally
            {

            }

            return result;

        }

        #endregion

      
    }
}