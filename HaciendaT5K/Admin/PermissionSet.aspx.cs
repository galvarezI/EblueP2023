using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eblue.Admin
{
    public partial class PermissionSet : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(txtName, txtDescription, DropDownListParent, checkboxIsRoot, checkboxIsForProject, checkboxIsForProcess, ListBoxForData, ListBoxForList, DropDownListSection, buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                base.PageEventLoadNotPostBackForGridViewHeader(this.gvModel);
                this.gvModel.RowCommand += GridView_RowCommand;

                this.gvModel.RowUpdated += GridView_RowUpdated;

            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated(sender:this, e: e);

                this.gvModel.RowUpdated += GridView_RowUpdated;
            }

            

        }


        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);
            //if (!Page.IsPostBack)
            //{
            //    var statementJS = "$('.card-tools button').click();";

            //    var script = $"$(document).ready(function () {{{statementJS}}});";
            //    //ClientScript.RegisterStartupScript(this.GetType(), "scriptCardTools", script, true);

            //    {
            //        statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': false });  " +
            //   " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡'); " +
            //   " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); });" +
            //   "  $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

            //        script = $"$(document).ready(function () {{{statementJS}}});";
            //        ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
            //    }
            //}
            //else
            //{
            //    {
            //        var statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': false }); " +
            //   " $('input[value = \"Delete\"]').attr('value', '-'); $('input[value = \"Edit\"]').attr('value', '≡'); $('a[value = \"Delete\"]').attr('value', '-'); $('a[value = \"Edit\"]').attr('value', '≡');" +
            //   " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Edit', '≡')); }); " +
            //   " $('.gridview td a').each(function() { var txt = $(this).text(); $(this).text(txt.replace('Delete', '-')); });";

            //        var script = $"$(document).ready(function () {{{statementJS}}});";
            //        ClientScript.RegisterStartupScript(this.GetType(), "scriptGridCommands", script, true);
            //    }
            //}
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            int rowCount = e.AffectedRows;
            int editIndex = gvModel.EditIndex;
            object datasourceObject = gvModel.DataSource;

            if (editIndex >= 0)
            {
                int colCount = e.NewValues.Keys.Count;
                if (colCount > 2)
                {
                    //var keyUID = e.OldValues[0] as string;

                    var datarow = gvModel.Rows[editIndex];
                    var ctrlListBoxWhichAreItEdit = datarow.FindControl("ListBoxWhichAreItEdit");

                    if (ctrlListBoxWhichAreItEdit != null)
                    {


                    }

                    var idxs = new { 
                        uid=0, name =2, description = 3, parentId = 4, 
                        isRoot = 5, isForProject = 6, isForProccess =7, 
                        whenData = 8, dataCanView = 9, dataCanEdit = 10,
                        whenList = 11, listCanView = 12, listCanAdd =13, listCanRemove = 14, listCanEdit = 15, sectionID = 16
                    };

                    var newValues = e.NewValues;
                    var oldValues = e.OldValues;

                    Guid.TryParse(oldValues[idxs.uid] as string, out Guid uid);

                    string name = newValues[idxs.name] as string;
                    string description = newValues[idxs.description] as string;
                    string parentString = newValues[idxs.parentId] as string;
                    string sectionString = newValues[idxs.sectionID] as string;

                    Guid? parentId = null;
                    Guid.TryParse(sectionString, out Guid sectionID);

                    if (!string.IsNullOrEmpty(parentString))
                    {

                        Guid.TryParse(parentString, out Guid guid);
                        parentId = guid;
                    }

                    Func<object, bool > convertToBoolean = (object exp) => {

                        bool result =false;
                        try
                        {
                            result = Convert.ToBoolean(exp);
                        }
                        catch 
                        {

                        }
                        return result;
                    
                    };

                    bool isRoot = convertToBoolean(newValues[idxs.isRoot]);
                    bool isForProject = convertToBoolean(newValues[idxs.isForProject]);
                    bool isForProccess = convertToBoolean(newValues[idxs.isForProccess]);

                    bool whenData = convertToBoolean(newValues[idxs.whenData]);
                    bool whenList = convertToBoolean(newValues[idxs.whenList]);

                    bool dataCanView = convertToBoolean(newValues[idxs.dataCanView]);
                    bool dataCanEdit = convertToBoolean(newValues[idxs.dataCanEdit]);

                    bool listCanView = convertToBoolean(newValues[idxs.listCanView]);
                    bool listCanAdd = convertToBoolean(newValues[idxs.listCanAdd]);
                    bool listCanRemove = convertToBoolean(newValues[idxs.listCanRemove]);
                    bool listCanEdit = convertToBoolean(newValues[idxs.listCanEdit]);


                    //var keysValues = e.NewValues.Keys;
                    //var keys = keysValues.OfType<string>().ToList();


                    //var keyUID = keys[0] as string;
                    //var keyName = keys[2] as string;
                    //var keyDescription = keys[3] as string;
                    //var keyPriority = keys[4] as string;

                    //var valueUIDStringOld = e.OldValues[keyUID] as string;
                    //Guid.TryParse(valueUIDStringOld, out Guid valueUIDOld);

                    //var valueNameNew = e.NewValues[keyName] as string;
                    //var valueDescriptionNew = e.NewValues[keyDescription] as string;
                    //var valuePriorityStringNew = e.NewValues[keyPriority] as string;
                    //Guid.TryParse(valueUIDStringOld, out Guid uid);
                    //int.TryParse(valuePriorityStringNew, out int valuePriorityNew);
                    //bool.TryParse(e.NewValues[keys[5]].ToString().ToLower(), out bool isDL);
                    //bool.TryParse(e.NewValues[keys[6]].ToString().ToLower(), out bool isAL);
                    //bool.TryParse(e.NewValues[keys[7]].ToString().ToLower(), out bool isDM);
                    //bool.TryParse(e.NewValues[keys[8]].ToString().ToLower(), out bool isVC);
                    //bool.TryParse(e.NewValues[keys[9]].ToString().ToLower(), out bool isWA);
                    //bool.TryParse(e.NewValues[keys[10]].ToString().ToLower(), out bool isWM);
                    //bool.TryParse(e.NewValues[keys[11]].ToString().ToLower(), out bool isTO);

                    //bool.TryParse(e.NewValues[keys[12]].ToString().ToLower(), out bool canSign);
                    //bool.TryParse(e.NewValues[keys[13]].ToString().ToLower(), out bool canReject);
                    //bool.TryParse(e.NewValues[keys[14]].ToString().ToLower(), out bool canApprove);
                    //bool.TryParse(e.NewValues[keys[15]].ToString().ToLower(), out bool canComment);

                    //if (datasourceObject is System.Data.DataTable)
                    //{

                    var varUsedFor = new Tuple<bool, bool, bool>(isRoot, isForProject, isForProccess);
                    var varForData = new Tuple<bool, bool, bool>(whenData, dataCanView, dataCanEdit);
                    var varForList = new Tuple<bool, bool, bool, bool, bool>(whenList, listCanView, listCanAdd, listCanRemove, listCanEdit);

                    if (UpdateModelToDB(uid: uid ,name: name, description: description, parentId: parentId, usedFor: varUsedFor, forData: varForData, forList: varForList, sectionID: sectionID))
                    {

                        //Clear();
                        ////gv.Databind();
                        //gvModel.DataBind();
                        //SetFocus(txtName);


                    }

                    //if (datasourceObject is System.Data.DataTable)
                    //{




                    //}


                }

            }



        }

        protected void ButtonNewModel_Click(object sender, EventArgs e)
        {
            AddModel();
        }

        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(txtName);
        }


        protected void AddModel()
        {

          

            string name = txtName.Text;
            string description = txtDescription.Text;

            Guid? parentId = null;
            Guid.TryParse(DropDownListSection.SelectedValue, out Guid sectionID);

            bool isRoot = checkboxIsRoot.Checked;
            bool isForProject = checkboxIsForProject.Checked;
            bool isForProccess = checkboxIsForProcess.Checked;

            bool whenData = false;
            bool whenList = false;

            bool dataCanView = false;
            bool dataCanEdit = false;

            bool listCanView = false;
            bool listCanAdd = false;
            bool listCanRemove = false;
            bool listCanEdit = false;

            if (!string.IsNullOrEmpty(DropDownListParent.SelectedValue))
            {

                Guid.TryParse(DropDownListParent.SelectedValue, out Guid guid);

                parentId = guid;
            }   

            var selectedIdxs = this.ListBoxForData.GetSelectedIndices();
            if (selectedIdxs.Length > 0)
            {
                //hasAreIt = true;
                foreach (var idx in selectedIdxs)
                {
                    var selectedValue = this.ListBoxForData.Items[idx].Value;

                    switch (selectedValue)
                    {
                        case "2":
                            whenData = true;
                            dataCanView = true;
                            break;

                        case "3":
                            whenData = true;
                            dataCanEdit = true;
                            break; 
                    }

                }
            }

            selectedIdxs = this.ListBoxForList.GetSelectedIndices();
            if (selectedIdxs.Length > 0)
            {
                //hasCanDo = true;
                foreach (var idx in selectedIdxs)
                {

                    var selectedValue = this.ListBoxForList.Items[idx].Value;

                    switch (selectedValue)
                    {                        

                        case "2":
                            whenList = true;
                            listCanView = true;
                            break;
                        case "3":
                            whenList = true;
                            listCanAdd = true;
                            break;
                        case "4":
                            whenList = true;
                            listCanRemove = true;
                            break;
                        case "5":
                            whenList = true;
                            listCanEdit = true;
                            break;


                    }

                }
            }

            var varUsedFor = new Tuple<bool, bool, bool>(isRoot, isForProject, isForProccess);
            var varForData = new Tuple<bool, bool, bool>(whenData, dataCanView, dataCanEdit);
            var varForList = new Tuple<bool, bool, bool, bool, bool>(whenList, listCanView, listCanAdd, listCanRemove, listCanEdit);

            if (AddModelToDB(name: name, description: description, parentId: parentId, usedFor: varUsedFor, forData: varForData, forList: varForList, sectionID: sectionID))
            {

                Clear();
                //gv.Databind();
                gvModel.DataBind();
                SetFocus(txtName);


            }

        }


        protected void Clear()
        {
            DropDownListParent.Items.Clear();
            

            SqlDataSourceParent.DataBind();
            DropDownListParent.DataBind();
            DropDownListParent.Items.Insert(0, new ListItem("None",""));
            this.DropDownListParent.Items[0].Selected = true;
            ClearWebControls(txtName, txtDescription, DropDownListParent, checkboxIsRoot, checkboxIsForProject, checkboxIsForProcess, ListBoxForData, ListBoxForList, DropDownListSection, buttonNewModel, buttonClearModel, gvModel);
        }

        protected bool AddModelToDB(string name, string description, Guid? parentId,Tuple<bool, bool, bool> usedFor , Tuple<bool, bool, bool> forData, Tuple<bool, bool, bool, bool, bool> forList, Guid sectionID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into RoleSetPermission (uid, name, description, targetof, " +
                    $"isroot, isforproject, isforprocess, " +
                    $"whenData, dataCapDetail, dataCapEdit, " +
                    $"whenList, listCapDetail, listCapAdd, listCapRemove, listCapEdit, roleTargetID, withTargetOf) " +
                    $"values (@uid,@name,@description, iif(@parentid = '', null, @parentid )," +
                    $"@isroot,@isforproject,@isforprocess," +
                    $"@whenData,@dataCapDetail,@dataCapEdit," +
                    $"@whenList,@listCapDetail,@listCapAdd,@listCapRemove,@listCapEdit, @sectionId, iif(@withTargetOf = '', null, @withTargetOf )  ) ";

                //$"values (@uid, @name, @)" +
                //$"select newid(), '{name}', '{description}', {priority}, " +
                //$"{Convert.ToInt32(whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                //$"{Convert.ToInt32(whichCanDo.Item1)},{Convert.ToInt32(whichCanDo.Item2)},{Convert.ToInt32(whichCanDo.Item3)},{Convert.ToInt32(whichCanDo.Item4)} ";

                var uid = Guid.NewGuid();
                string parentString = parentId == null ? string.Empty : parentId.ToString();
                
                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid",uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);

                bool isRoot = usedFor.Item1;
                if (isRoot)
                {
                    cmd.Parameters.AddWithValue("@withTargetOf", parentString);
                    cmd.Parameters.AddWithValue("@parentid", string.Empty);
                }
                else 
                {
                    cmd.Parameters.AddWithValue("@withTargetOf", string.Empty);
                    cmd.Parameters.AddWithValue("@parentid", parentString);
                }
                
                cmd.Parameters.AddWithValue("@isroot", usedFor.Item1);
                cmd.Parameters.AddWithValue("@isforproject", usedFor.Item2);
                cmd.Parameters.AddWithValue("@isforprocess", usedFor.Item3);

                cmd.Parameters.AddWithValue("@whenData", forData.Item1);
                cmd.Parameters.AddWithValue("@dataCapDetail", forData.Item2);
                cmd.Parameters.AddWithValue("@dataCapEdit", forData.Item3);

                cmd.Parameters.AddWithValue("@whenList", forList.Item1);
                cmd.Parameters.AddWithValue("@listCapDetail", forList.Item2);
                cmd.Parameters.AddWithValue("@listCapAdd", forList.Item3);
                cmd.Parameters.AddWithValue("@listCapRemove", forList.Item4);
                cmd.Parameters.AddWithValue("@listCapEdit", forList.Item5);

                cmd.Parameters.AddWithValue("@sectionId", sectionID);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add permission set", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool UpdateModelToDB(Guid uid, string name, string description, Guid? parentId, Tuple<bool, bool, bool> usedFor, Tuple<bool, bool, bool> forData, Tuple<bool, bool, bool, bool, bool> forList, Guid sectionID)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string updateCommand =
                    $"update RoleSetPermission "+
                    $"set name = @name, description = @description, targetof= iif(@parentid = '', null, @parentid )," +
                    $"isroot= @isroot, isforproject = @isforproject, isforprocess = @isforprocess," +
                    $"whendata = @whenData,datacapdetail = @dataCapDetail, datacapedit = @dataCapEdit," +
                    $"whenlist = @whenList, listcapdetail = @listCapDetail, listcapadd = @listCapAdd, listcapremove = @listCapRemove,listcapedit = @listCapEdit, roletargetid = @sectionId, " +
                    $"withTargetOf = iif(@withTargetOf = '', null, @withTargetOf ) " +
                    $"where uid = @uid ";

                string parentString = parentId == null ? string.Empty : parentId.ToString();

                SqlCommand cmd = new SqlCommand(updateCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                

                bool isRoot = usedFor.Item1;
                if (isRoot)
                {
                    cmd.Parameters.AddWithValue("@withTargetOf", parentString);
                    cmd.Parameters.AddWithValue("@parentid", string.Empty);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@withTargetOf", string.Empty);
                    cmd.Parameters.AddWithValue("@parentid", parentString);
                }

                cmd.Parameters.AddWithValue("@isroot", usedFor.Item1);

                cmd.Parameters.AddWithValue("@isforproject", usedFor.Item2);
                cmd.Parameters.AddWithValue("@isforprocess", usedFor.Item3);

                cmd.Parameters.AddWithValue("@whenData", forData.Item1);
                cmd.Parameters.AddWithValue("@dataCapDetail", forData.Item2);
                cmd.Parameters.AddWithValue("@dataCapEdit", forData.Item3);

                cmd.Parameters.AddWithValue("@whenList", forList.Item1);
                cmd.Parameters.AddWithValue("@listCapDetail", forList.Item2);
                cmd.Parameters.AddWithValue("@listCapAdd", forList.Item3);
                cmd.Parameters.AddWithValue("@listCapRemove", forList.Item4);
                cmd.Parameters.AddWithValue("@listCapEdit", forList.Item5);
                cmd.Parameters.AddWithValue("@sectionId", sectionID);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to update permission set", ex);
            }
            finally
            {

            }

            return result;

        }

    }
}