using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Eblue.Utils.DataTools;

namespace Eblue.Admin
{
    public partial class UserTypes : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.txtName, this.txtDescription, this.txtPriority, this.ListBoxWhichAreIt, this.ListBoxWhichCando, DropDownListPermissionSet, buttonNewModel, buttonClearModel, gvModel);

            //base.Page_Load(sender, e);

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            //if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsAdmin))
            //{

            //}
            //else 
            //{
            //    base.GoToUnAuthorizeRoute();
            //}

                if (!Page.IsPostBack)
            {
                base.PageEventLoadNotPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                this.gvModel.RowUpdated += GridView_RowUpdated;
                
            }
            else
            {
                base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated(sender:this, e: e);

                this.gvModel.RowUpdated += GridView_RowUpdated;
            }

        }


        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            base.OnSaveStateCompleteExtend(this);
            //base.OnSaveStateComplete(e);
            //if (!Page.IsPostBack)
            //{
            //    var statementJS = "$('.card-tools button').click();";

            //    var script = $"$(document).ready(function () {{{statementJS}}});";
            //    //ClientScript.RegisterStartupScript(this.GetType(), "scriptCardTools", script, true);

            //    {
            //        statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
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
            //        var statementJS = "var tbl = $('.gridview'); $(tbl).DataTable({'paging': false,'lengthChange': false,'searching': true,'ordering': true,'info': true,'autoWidth': true }); " +
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
                int colCount = e.NewValues. Keys.Count;
                if (colCount > 2)
                {
                    //var keyUID = e.OldValues[0] as string;

                    var datarow = gvModel.Rows[editIndex];
                    var ctrlListBoxWhichAreItEdit =  datarow.FindControl("ListBoxWhichAreItEdit");

                    if (ctrlListBoxWhichAreItEdit != null)
                    {
                    
                    
                    }


                    var keysValues = e.NewValues.Keys;
                    var keys = keysValues.OfType<string>().ToList();
                    

                    var keyUID = keys[0] as string;
                    var keyName = keys[2] as string;
                    var keyDescription = keys[3] as string;
                    var keyPriority = keys[4] as string;

                    var valueUIDStringOld = e.OldValues[keyUID] as string;
                    Guid.TryParse(valueUIDStringOld, out Guid valueUIDOld);

                    var valueNameNew = e.NewValues[keyName] as string;
                    var valueDescriptionNew = e.NewValues[keyDescription] as string;
                    var valuePriorityStringNew = e.NewValues[keyPriority] as string;
                    Guid.TryParse(valueUIDStringOld, out Guid uid);
                    int.TryParse(valuePriorityStringNew, out int valuePriorityNew);
                    bool.TryParse(e.NewValues[keys[5]].ToString().ToLower(), out bool isDL);
                    bool.TryParse(e.NewValues[keys[6]].ToString().ToLower(), out bool isAL);
                    bool.TryParse(e.NewValues[keys[7]].ToString().ToLower(), out bool isDM);
                    bool.TryParse(e.NewValues[keys[8]].ToString().ToLower(), out bool isVC);
                    bool.TryParse(e.NewValues[keys[9]].ToString().ToLower(), out bool isWA);
                    bool.TryParse(e.NewValues[keys[10]].ToString().ToLower(), out bool isWM);
                    bool.TryParse(e.NewValues[keys[11]].ToString().ToLower(), out bool isTO);
                    bool.TryParse(e.NewValues[keys[12]].ToString().ToLower(), out bool isIO);

                    bool.TryParse(e.NewValues[keys[13]].ToString().ToLower(), out bool canSign);
                    bool.TryParse(e.NewValues[keys[14]].ToString().ToLower(), out bool canReject);
                    bool.TryParse(e.NewValues[keys[15]].ToString().ToLower(), out bool canApprove);
                    bool.TryParse(e.NewValues[keys[16]].ToString().ToLower(), out bool canComment);

                    //bool.TryParse(e.NewValues[keys[12]].ToString().ToLower(), out bool canSign);
                    //bool.TryParse(e.NewValues[keys[13]].ToString().ToLower(), out bool canReject);
                    //bool.TryParse(e.NewValues[keys[14]].ToString().ToLower(), out bool canApprove);
                    //bool.TryParse(e.NewValues[keys[15]].ToString().ToLower() , out bool canComment);

                    var permissionSetIDString = e.NewValues[keys[17]] as string;
                    Guid.TryParse(permissionSetIDString, out Guid permissionSetID);

                    //if (datasourceObject is System.Data.DataTable)
                    //{

                    var varWhichAreIt = new Tuple<bool, bool, bool, bool, bool, bool, bool>(isDL, isAL, isDM, isVC, isWA, isWM, isTO);
                    var varWhichCanDo = new Tuple<bool, bool, bool, bool, bool>(canSign, canReject, canApprove, canComment, isIO);

                    if (UpdateUserTypeToDB(uid, valueNameNew, valueDescriptionNew, valuePriorityNew,
                        varWhichAreIt, varWhichCanDo))
                    {

                        //get the Usertype Userpermission (uid); if null then addUserTypePermission

                        if (GetUserTypePermission(out Guid rpID, uid))
                        {
                            //YES ALREADY A UserTYPE PERMISSION


                            if (string.IsNullOrEmpty(permissionSetIDString))
                            {
                                //YES THEN REMOVE THE UserTYPE PERMISSION
                                if (RemoveUserTypePermission(rpID))
                                {
                                    if (true) { }
                                
                                }
                            }
                            else                             
                            {
                                //NOP THEN UPDATE THE User TYPE PERMISSION
                                if (UpdateUserTypePermission(rpID, permissionSetID))
                                {
                                    if (true) { }

                                }

                            }

                        }
                        else
                        {

                            //NOP THERE A UserTYPE PERMISSION

                            if (string.IsNullOrEmpty(permissionSetIDString))
                            {
                                //YES THEN DO NOTHING
                                if (true) { }
                            }
                            else
                            {
                                //NOP THEN INSERT NEW UserTYPE PERMISSION
                                if (AddUserTypePermissionToDB(uid, permissionSetID))
                                {
                                    if (true) { }

                                }

                            }

                           

                        }

                       
                    
                    }
                    //}


                }
            
            }

           

        }

        protected void ButtonNewModel_Click(object sender, EventArgs e)
        {
            AddUserType();
        }

        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(txtName);
        }


        protected void AddUserType()
        {

            //<asp:ListItem Value="0">None</asp:ListItem>
            //                                <asp:ListItem Value="1">Directive Leader</asp:ListItem>
            //                                <asp:ListItem Value="2">Assistant Leader</asp:ListItem>
            //                                <asp:ListItem Value="3">Directive Manager</asp:ListItem>
            //                                <asp:ListItem Value="4">Visor Company</asp:ListItem>
            //                                <asp:ListItem Value="5">Work Administrator</asp:ListItem>
            //                                <asp:ListItem Value="6">Work Member</asp:ListItem>
            //                                <asp:ListItem Value="7">Task Officer</asp:ListItem>

            string name = txtName.Text;
            string description = txtDescription.Text;
            string priorityString = txtPriority.Text?.Trim();
            int.TryParse(priorityString, out int priority);

            bool hasAreIt = false;
            bool hasCanDo = false;
            bool isDL = false;
            bool isAl = false;
            bool isDM = false;
            bool isVC = false;
            bool isWA = false;
            bool isWM = false;
            bool isTO = false;
            bool isIO = false;

            bool isSign = false;
            bool isApprove = false;
            bool isReject = false;
            bool isComment = false;

            var selectedIdxs = this.ListBoxWhichAreIt.GetSelectedIndices();
            if (selectedIdxs.Length > 0)
            {
                hasAreIt = true;
                foreach (var idx in selectedIdxs)
                {
                    var selectedValue = this.ListBoxWhichAreIt.Items[idx].Value;

                    switch (selectedValue)
                    {
                        case "1":
                            isDL = true;
                            break;

                        case "2":
                            isAl = true;
                            break;
                        case "3":
                            isDM = true;
                            break;
                        case "4":
                            isVC = true;
                            break;
                        case "5":
                            isWA = true;
                            break;
                        case "6":
                            isWM = true;
                            break;
                        case "7":
                            isTO = true;
                            break;
                        case "8":
                            isIO = true;
                            break;

                    }

                }
            }

            selectedIdxs = this.ListBoxWhichCando.GetSelectedIndices();
            if (selectedIdxs.Length > 0)
            {
                hasCanDo = true;
                foreach (var idx in selectedIdxs)
                {

                    var selectedValue = this.ListBoxWhichCando.Items[idx].Value;

                    switch (selectedValue)
                    {
                        case "1":
                            isSign = true;
                            break;

                        case "2":
                            isApprove = true;
                            break;
                        case "3":
                            isReject = true;
                            break;
                        case "4":
                            isComment = true;
                            break;


                    }

                }
            }

            var varWhichAreIt = new Tuple<bool, bool, bool, bool, bool, bool, bool>(isDL, isAl, isDM, isVC, isWA, isWM, isTO);
            var varWhichCanDo = new Tuple<bool, bool, bool, bool, bool>(isSign, isApprove, isReject, isComment, isIO);

            var UsersetPermissionIDString = DropDownListPermissionSet.SelectedValue;
            var isValidUserSetPermission = Guid.TryParse(UsersetPermissionIDString, out Guid UsersetPermissionID);


            if (AddUserTypeToDB(out Guid uid ,name, description, priority, whichAreIt: varWhichAreIt, whichCanDo: varWhichCanDo ))
            {

                if (isValidUserSetPermission && AddUserTypePermissionToDB(uid, UsersetPermissionID))
                {
                   

                }
                Clear();
                //gv.Databind();
                gvModel.DataBind();
                SetFocus(txtName);

            }

        }


        protected void Clear()
        {
            ClearWebControls(this.txtName, this.txtDescription, this.txtPriority, this.ListBoxWhichAreIt, this.ListBoxWhichCando, DropDownListPermissionSet);
        }
        
        protected bool AddUserTypeToDB(out Guid uid,string name, string description, int priority, Tuple<bool, bool, bool, bool, bool, bool, bool> whichAreIt, Tuple<bool, bool, bool, bool, bool> whichCanDo )
        {
            bool result;
            uid = Guid.NewGuid();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into RosterCategory (uid, name, description, orderpriority, " +
                    $"isSupervisor, isAdmin, isManager, IsScientist, isPersonnel, isStudent, isplanViewer, " +
                    $"isCoordinator) " +
                    $"values ( @uid, '{name}', '{description}', {priority}, " +
                    $"{Convert.ToInt32( whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                    $"{Convert.ToInt32(whichCanDo.Item5)} )";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.ExecuteNonQuery();
                
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add Roster type", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool UpdateUserTypeToDB(Guid uid,string name, string description, int priority, Tuple<bool, bool, bool, bool, bool, bool, bool> whichAreIt, Tuple<bool, bool, bool, bool, bool> whichCanDo)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {   
                cn.Open();
                string commandString =
                    $"update RosterCategory set name = '{name}', description = '{description}', orderpriority= {priority}, " +
                    $"isSupervisor= {Convert.ToInt32(whichAreIt.Item1)}, " +
                    $"isAdmin= {Convert.ToInt32(whichAreIt.Item2)}, " +
                    $"isManager= {Convert.ToInt32(whichAreIt.Item3)}, " +
                    $"IsScientist= {Convert.ToInt32(whichAreIt.Item4)}, " +
                    $"isPersonnel= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"isStudent= {Convert.ToInt32(whichAreIt.Item6)}, " +
                    $"isplanViewer= {Convert.ToInt32(whichAreIt.Item7)}, " +
                    $"isCoordinator= {Convert.ToInt32(whichCanDo.Item5)} " +                    
                    $"where uid= '{uid}'  ";

                SqlCommand cmd = new SqlCommand(commandString, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to update Roster type", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool GetUserTypePermission(out Guid uid, Guid UsertypeId)
        {

            var result = false;
            uid = default(Guid);

            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@UsercategoryID", UsertypeId))
            {
            commandString = "select top 1 uid from RosterPermission where RosterCategoryID = @UsercategoryID"
            };


            if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
            {
                string valueString = value?.ToString();
                result = Guid.TryParse(valueString, out uid);               
                
            }



            return result;
        
        }

        protected bool RemoveUserTypePermission(Guid uid)
        {

            var result = false;
            

            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@uid", uid))
            {
                commandString = "delete from RosterPermission  where uid = @uid"
            };


            if (ExecuteOnly(out int affectedRows, reqInfo))
            {

                if (affectedRows> -1)
                { }
            }

            return result;

        }

        protected bool UpdateUserTypePermission(Guid uid, Guid UsersetPermissionID)
        {

            var result = false;


            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@uid", uid), new SqlParameter("@RSetPermissionID", UsersetPermissionID))
            {
                commandString = "update RosterPermission set RosterSetpermissionID = @RSetPermissionID  where uid = @uid"
            };


            if (ExecuteOnly(out int affectedRows, reqInfo))
            {
                result = true;
                if (affectedRows > -1)
                { }
            }

            return result;

        }


        protected bool AddUserTypePermissionToDB(Guid UserTypeID, Guid permissionSetID)
        {
            bool result;
            
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into RosterPermission (UId, RosterCategoryID, RosterSetPermissionID) " +
                    $"values (@uid, @UsercategoryID, @UsersetPermissionID)";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.Parameters.AddWithValue("@uid", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@UsercategoryID", UserTypeID);
                cmd.Parameters.AddWithValue("@UsersetPermissionID", permissionSetID);
                cmd.ExecuteNonQuery();
               
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add User type", ex);
            }
            finally
            {

            }

            return result;

        }

    }
}