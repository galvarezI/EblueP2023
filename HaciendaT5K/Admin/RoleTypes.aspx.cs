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
    public partial class RoleTypes : Eblue.Code.PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(this.txtName, this.txtDescription, this.txtPriority, this.ListBoxWhichAreIt, this.ListBoxWhichCando, DropDownListPermissionSet, buttonNewModel, buttonClearModel, gvModel);

            base.Page_Load(sender, e);

            var userLogged = Eblue.Utils.SessionTools.UserInfo;

            if (userLogged != null && (userLogged.IsManager || userLogged.IsDeveloper || userLogged.IsAdmin || userLogged.IsAdministrator || userLogged.IsProjectOfficer))
            {

            }
            else 
            {
                base.GoToUnAuthorizeRoute();
            }

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

                    bool.TryParse(e.NewValues[keys[5]].ToString().ToLower(), out bool isRDR);
                    bool.TryParse(e.NewValues[keys[6]].ToString().ToLower(), out bool isEO);
                    bool.TryParse(e.NewValues[keys[7]].ToString().ToLower(), out bool isERS);
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

                    var varWhichAreIt = new Tuple<bool, bool, bool, bool, bool, bool, bool>(isRDR, isEO, isERS, isVC, isWA, isWM, isTO);
                    // var varWhichAreIt2 = new Tuple<bool, bool, bool, bool, bool, bool, bool>();
                    var varWhichCanDo = new Tuple<bool, bool, bool, bool, bool>(canSign, canReject, canApprove, canComment, isIO);

                    //Tuple<bool, bool, bool, bool, bool, bool, bool> varWhichAreIt1 = varWhichAreIt;
                    if (UpdateRoleTypeToDB(uid, valueNameNew, valueDescriptionNew, valuePriorityNew,
                        varWhichAreIt, varWhichCanDo))
                    {

                        //get the roletype rolepermission (uid); if null then addRoleTypePermission

                        if (GetRoleTypePermission(out Guid rpID, uid))
                        {
                            //YES ALREADY A ROLETYPE PERMISSION


                            if (string.IsNullOrEmpty(permissionSetIDString))
                            {
                                //YES THEN REMOVE THE ROLETYPE PERMISSION
                                if (RemoveRoleTypePermission(rpID))
                                {
                                    if (true) { }
                                
                                }
                            }
                            else                             
                            {
                                //NOP THEN UPDATE THE ROLE TYPE PERMISSION
                                if (UpdateRoleTypePermission(rpID, permissionSetID))
                                {
                                    if (true) { }

                                }

                            }

                        }
                        else
                        {

                            //NOP THERE A ROLETYPE PERMISSION

                            if (string.IsNullOrEmpty(permissionSetIDString))
                            {
                                //YES THEN DO NOTHING
                                if (true) { }
                            }
                            else
                            {
                                //NOP THEN INSERT NEW ROLETYPE PERMISSION
                                if (AddRoleTypePermissionToDB(uid, permissionSetID))
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

        private bool UpdateRoleTypeToDB(Guid uid, string valueNameNew, string valueDescriptionNew, int valuePriorityNew, Tuple<bool, bool, bool, bool, bool, bool, bool> varWhichAreIt, Tuple<bool, bool, bool, bool, bool> varWhichCanDo)
        {
            throw new NotImplementedException();
        }

        private bool UpdateRoleTypeToDB(Guid uid, string valueNameNew, string valueDescriptionNew, int valuePriorityNew, Tuple<bool, bool, bool, bool, bool, bool, bool, bool> varWhichAreIt, Tuple<bool, bool, bool, bool, bool> varWhichCanDo)
        {
            throw new NotImplementedException();
        }

        protected void ButtonNewModel_Click(object sender, EventArgs e)
        {
            AddRoleType();
        }

        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(txtName);
        }


        protected void AddRoleType()
        {

            //<asp:ListItem Value="0">None</asp:ListItem>
            //                                <asp:ListItem Value="1">Directive Leader</asp:ListItem>
            //                                <asp:ListItem Value="2">Assistant Leader</asp:ListItem>
            //                                <asp:ListItem Value="3">Directive Manager</asp:ListItem>
            //                                <asp:ListItem Value="4">Visor Company</asp:ListItem>
            //                                <asp:ListItem Value="5">Work Administrator</asp:ListItem>
            //                                <asp:ListItem Value="6">Work Member</asp:ListItem>
            //                                <asp:ListItem Value="7">Task Officer</asp:ListItem>
            //                              
            //                                  New Integration:                          
            //
            //                                <asp:ListItem Value="8">Research Director or Representative</asp:ListItem>
            //                                <asp:ListItem Value="9">Department Director</asp:ListItem>
            //                                <asp:ListItem Value="10">Program or Critical Issue Coordinator</asp:ListItem>
            //                                <asp:ListItem Value="11">Commodity Leader</asp:ListItem>
            //                                <asp:ListItem Value="12">Project Leader</asp:ListItem>
            //                                <asp:ListItem Value="13">Administrator (Substation or Research Center)</asp:ListItem>
            //                                <asp:ListItem Value="14"> Administrative Officer</asp:ListItem>
            //                                <asp:ListItem Value="22"> Executive Officer EEA-INV</asp:ListItem>
            //                                <asp:ListItem Value="22"> External Resources Specialist-EEA_INV</asp:ListItem> 
            //                                <asp:ListItem Value="15">Project CO-PI</asp:ListItem>
            //                                <asp:ListItem Value="16">Project Collaborator</asp:ListItem>                                
            //                                <asp:ListItem Value="17">Budget Officer</asp:ListItem>
            //                                <asp:ListItem Value="18">Human Resources Officer</asp:ListItem>
            //                                <asp:ListItem Value="19">Executive Officer EEA-Associate Dean Office</asp:ListItem>
            //                                <asp:ListItem Value="20">Executive Officer EEA- Dean & DIRECTOR Office</asp:ListItem>
            //                                <asp:ListItem Value="21">OSI (information System) Workplan-liason staff</asp:ListItem>
            //                               







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

            //New Integration roles
            /*
                isRDR = Value = 9
             
             */
            bool isRDR = false;
            bool isDP = false;
            bool isCIC = false;
            bool isCL = false;
            bool isPL = false;
            bool isAO = false;
            bool isEO = false;
            bool isERS = false;
            bool isADSRC = false;
            bool isSAD = false;
            bool isPCPI = false;
            bool isPCO = false;
            bool isBO = false;
            bool isHRO = false;
            bool isEODO = false;
            bool isEODDO = false;
            bool isOSI = false;




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
                            isRDR = true;
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

                        case "9":
                            isRDR = true;
                            break;

                        case "10":
                            isEO = true;
                            break;

                        case "11":
                            isERS =true;
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
            var varWhichAreIt2 = new Tuple<bool, bool, bool, bool, bool, bool, bool>(isRDR, isEO, isAO, isERS, isADSRC, isPL, isSAD);

            var rolesetPermissionIDString = DropDownListPermissionSet.SelectedValue;
            var isValidRoleSetPermission = Guid.TryParse(rolesetPermissionIDString, out Guid rolesetPermissionID);


            if (AddRoleTypeToDB(out Guid uid ,name, description, priority, whichAreIt: varWhichAreIt, whichCanDo: varWhichCanDo, whichAreIt2: varWhichAreIt2  ))
            {

                if (isValidRoleSetPermission && AddRoleTypePermissionToDB(uid, rolesetPermissionID))
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
        
        protected bool AddRoleTypeToDB(out Guid uid,string name, string description, int priority, Tuple<bool, bool, bool, bool, bool, bool, bool> whichAreIt, Tuple<bool, bool, bool, bool, bool> whichCanDo, Tuple<bool, bool, bool, bool, bool, bool, bool> whichAreIt2)
        {
            bool result;
            uid = Guid.NewGuid();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {

                //IsResearchDirector,IsDepartmentDirector,IsCriticalIssueC,IsCommodityLeader,IsCommodityLeader,IsAdministrator,IsAdministrative,IsExecutiveOfficer,IsExternalResources,IsSecretary,IsProjectLeader,"+
                //$"IsProjectCOPI,IsProjectCollaborator,IsAssistantLeader,IsHumanResourcesO,IsEEAAsociateDO,IsEEADeanDirector,IsOSI) " +
                //$"values ( @uid, '{name}', '{description}', {priority}, " +
            cn.Open();
                string insertCommand =
                    $"insert into roleCategory (uid, name, description, orderpriority, " +
                    $"IsDirectiveLeader, IsAssistantLeader, IsDirectiveManager, IsVisorCompany, IsWorkAdministrator, IsWorkMember, IsTaskOfficer, " +
                    $"HasSignature, HasAproval, HasRejection, HasComment, IsInvestigationOfficer,IsResearchDirector,IsExecutiveOfficer,IsExternalResources)" +
                    $"values ( @uid, '{name}', '{description}', {priority}, " +
                    $"{Convert.ToInt32( whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                    $"{Convert.ToInt32(whichCanDo.Item1)},{Convert.ToInt32(whichCanDo.Item2)},{Convert.ToInt32(whichCanDo.Item3)},{Convert.ToInt32(whichCanDo.Item4)}, {Convert.ToInt32(whichCanDo.Item5)},{Convert.ToInt32(whichAreIt2.Item1)},{Convert.ToInt32(whichAreIt2.Item2)},{Convert.ToInt32(whichAreIt2.Item4)})";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.ExecuteNonQuery();
                
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add role type", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool UpdateRoleTypeToDB(Guid uid,string name, string description, int priority, Tuple<bool, bool, bool, bool, bool, bool, Tuple <bool,bool,bool,bool,bool,bool,bool>> whichAreIt, Tuple<bool, bool, bool, bool, bool> whichCanDo, Tuple<bool, bool, bool, bool, bool, bool, bool> whichAreIt2)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {   
                cn.Open();
                string commandString =
                    $"update roleCategory set name = '{name}', description = '{description}', orderpriority= {priority}, " +
                    $"IsResearchDirector= {Convert.ToInt32(whichAreIt.Item1)}, " +
                    $"IsAssistantLeader= {Convert.ToInt32(whichAreIt.Item2)}, " +
                    $"IsDirectiveManager= {Convert.ToInt32(whichAreIt.Item3)}, " +
                    $"IsVisorCompany= {Convert.ToInt32(whichAreIt.Item4)}, " +
                    $"IsWorkAdministrator= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"IsWorkMember= {Convert.ToInt32(whichAreIt.Item6)}, " +
                    $"IsTaskOfficer= {Convert.ToInt32(whichAreIt.Item7)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichCanDo.Item5)}, " +
                    $"IsResearchDirector= {Convert.ToInt32(whichAreIt2.Item1)}, " +
                    $"IsExecutiveOfficer= {Convert.ToInt32(whichAreIt2.Item2)}, " +
                    $"IsExternalResources= {Convert.ToInt32(whichAreIt2.Item4)}, " +
                    $"HasSignature= {Convert.ToInt32(whichCanDo.Item1)}, " +
                    $"HasAproval= {Convert.ToInt32(whichCanDo.Item3)}, " +
                    $"HasRejection= {Convert.ToInt32(whichCanDo.Item2)}, " +
                    $"HasComment= {Convert.ToInt32(whichCanDo.Item4)} " +
                    $"where uid= '{uid}'  ";
                /* New Integration  
                string commandString2 =
                    $"update roleCategory set name = '{name}', description = '{description}', orderpriority= {priority}, " +
                    $"IsResearchDirector= {Convert.ToInt32(whichAreIt.Item1)}, " +
                    $"IsAssistantLeader= {Convert.ToInt32(whichAreIt.Item2)}, " +
                    $"IsDirectiveManager= {Convert.ToInt32(whichAreIt.Item3)}, " +
                    $"IsVisorCompany= {Convert.ToInt32(whichAreIt.Item4)}, " +
                    $"IsWorkAdministrator= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"IsWorkMember= {Convert.ToInt32(whichAreIt.Item6)}, " +
                    $"IsTaskOfficer= {Convert.ToInt32(whichAreIt.Item7)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt2.Item5)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt2.Item5)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt.Item5)}, " +
                    $"IsInvestigationOfficer= {Convert.ToInt32(whichAreIt.Item5)}, " +




                    $"HasSignature= {Convert.ToInt32(whichCanDo.Item1)}, " +
                    $"HasAproval= {Convert.ToInt32(whichCanDo.Item3)}, " +
                    $"HasRejection= {Convert.ToInt32(whichCanDo.Item2)}, " +
                    $"HasComment= {Convert.ToInt32(whichCanDo.Item4)} " +
                    $"where uid= '{uid}'  ";
                 
                 */




                SqlCommand cmd = new SqlCommand(commandString, cn);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to update role type", ex);
            }
            finally
            {

            }

            return result;

        }

        protected bool GetRoleTypePermission(out Guid uid, Guid roletypeId)
        {

            var result = false;
            uid = default(Guid);

            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@rolecategoryID", roletypeId))
            {
            commandString = "select top 1 uid from rolePermission where rolecategoryID = @rolecategoryID"
            };


            if (FirstOrDefaultRowAndColumn(out object value, reqInfo))
            {
                string valueString = value?.ToString();
                result = Guid.TryParse(valueString, out uid);               
                
            }



            return result;
        
        }

        protected bool RemoveRoleTypePermission(Guid uid)
        {

            var result = false;
            

            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@uid", uid))
            {
                commandString = "delete from RolePermission  where uid = @uid"
            };


            if (ExecuteOnly(out int affectedRows, reqInfo))
            {

                if (affectedRows> -1)
                { }
            }

            return result;

        }

        protected bool UpdateRoleTypePermission(Guid uid, Guid rolesetPermissionID)
        {

            var result = false;


            var reqInfo = new Eblue.Utils.RequestDataInfo(new SqlParameter("@uid", uid), new SqlParameter("@RSetPermissionID", rolesetPermissionID))
            {
                commandString = "update RolePermission set rolesetpermissionID = @RSetPermissionID  where uid = @uid"
            };


            if (ExecuteOnly(out int affectedRows, reqInfo))
            {
                result = true;
                if (affectedRows > -1)
                { }
            }

            return result;

        }


        protected bool AddRoleTypePermissionToDB(Guid roleTypeID, Guid permissionSetID)
        {
            bool result;
            
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into rolePermission (UId, rolecategoryID, rolesetPermissionID) " +
                    $"values (@uid, @rolecategoryID, @rolesetPermissionID)";

                SqlCommand cmd = new SqlCommand(insertCommand, cn);

                cmd.Parameters.AddWithValue("@uid", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@rolecategoryID", roleTypeID);
                cmd.Parameters.AddWithValue("@rolesetPermissionID", permissionSetID);
                cmd.ExecuteNonQuery();
               
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add role type", ex);
            }
            finally
            {

            }

            return result;

        }

    }
}