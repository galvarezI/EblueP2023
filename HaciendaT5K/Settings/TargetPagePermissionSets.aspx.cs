using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using Eblue.Code;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;
using System.Web.UI.WebControls;

namespace Eblue.Settings
{
    public partial class TargetPagePermissionSets : PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(txtName, txtDescription, this.DropDownListRosterType, this.DropDownListUserTarget, buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                base.PageEventLoadNotPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated += GridView_RowUpdated;
                BindDropDownLists();

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;               

                //this.gvModel.RowUpdated += GridView_RowUpdated;
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
            AddModel();
        }
        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(txtName);
        }

        #endregion

        #region control's action binds

        protected void AddModel()
        {

            string name = txtName.Text;
            string description = txtDescription.Text;          


            Guid.TryParse(this.DropDownListRosterType.SelectedValue, out Guid rostertypeID);
            Guid.TryParse(this.DropDownListUserTarget.SelectedValue, out Guid usertargetID);

            if (AddModelToDB(name: name, description: description, rostertypeID: rostertypeID, usertargetID: usertargetID))
            {

                Clear();
                //gv.Databind();
                gvModel.DataBind();
                SetFocus(txtName);


            }

        }

        protected void Clear()
        {
            BindDropDownLists();

            ClearWebControls(txtName, txtDescription, this.DropDownListRosterType, this.DropDownListUserTarget);
        }
        #endregion

        public void BindDropDownLists()
        {

            BindRosterTypeList();
            BindUserTargetList();

        }

        public void BindRosterTypeList()
        {

            

            this.SqlDataSourceRosterType.SelectParameters.Clear();
            

            this.DropDownListRosterType.Items.Clear();


            this.SqlDataSourceRosterType.DataBind();
            DropDownListRosterType.DataBind();
            DropDownListRosterType.Items.Insert(0, new ListItem("None", ""));
            this.DropDownListRosterType.Items[0].Selected = true;


        }

        public void BindUserTargetList()
        {


            this.DropDownListUserTarget.Items.Clear();


            this.SqlDataSourceUserTarget.DataBind();
            DropDownListUserTarget.DataBind();
            DropDownListUserTarget.Items.Insert(0, new ListItem("None", ""));
            this.DropDownListUserTarget.Items[0].Selected = true;

        }


        #region data model actions



        protected bool AddModelToDB(string name, string description, Guid rostertypeID, Guid usertargetID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into UserRolePersimission (uid, name, description, RosterCategoryId, UserTargetId) " +
                    $"values (@uid,@name,@description,@RosterCategoryId, @UserTargetId)";                   

             
                var uid = Guid.NewGuid();               


                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@RosterCategoryId", rostertypeID);
                cmd.Parameters.AddWithValue("@UserTargetId", usertargetID);


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add user permission set", ex);
            }
            finally
            {

            }

            return result;

        }

        #endregion
    }
}