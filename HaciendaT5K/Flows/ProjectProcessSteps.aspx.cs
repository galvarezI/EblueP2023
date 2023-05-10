
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Eblue.Code.Models;
using Eblue.Code;
using Eblue.Utils;
using static Eblue.Utils.DataTools;
using static Eblue.Utils.ConstantsTools;
using static Eblue.Utils.WebTools;

namespace Eblue.Flows
{
    public partial class ProjectProcessSteps : PageBasic
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

            MarkTabIndexWebControls(txtName, txtDescription, this.checkboxIsStarter, this.checkboxIsFinalizer, this.DropDownListProjectStatus, this.checkboxObjectionsAvailabled, this.checkboxAssentsAvailabled, this.DropDownListPreviousProccess, this.DropDownListNextProccess, this.DropDownListAlwaysProccess, this.ListBoxEnabledFor , buttonNewModel, buttonClearModel, gvModel);
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                base.PageEventLoadNotPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;

                //this.gvModel.RowUpdated += GridView_RowUpdated;

                Guid? ProjectWorkFlowDefaultID = SessionTools.ProjectWorkFlowDefaultID;

                this.dataSourceModel.SelectParameters.Clear();
                dataSourceModel.SelectParameters.Add("workflowid", "");
                dataSourceModel.SelectParameters["workflowid"].DefaultValue = ProjectWorkFlowDefaultID?.ToString();

                BindDropDownLists();

                this.gvModel.RowUpdated += gvModel_RowUpdated;

                //SqlDataSourceListPreviousProccess.SelectParameters.Clear();
                //SqlDataSourceListPreviousProccess.SelectParameters.Add("workflowid", "");
                //SqlDataSourceListPreviousProccess.SelectParameters["workflowid"].DefaultValue  = ProjectWorkFlowDefaultID?.ToString();


                //SqlDataSourceListNextProccess.SelectParameters.Clear();
                //SqlDataSourceListNextProccess.SelectParameters.Add("workflowid", "");
                //SqlDataSourceListNextProccess.SelectParameters["workflowid"].DefaultValue =  ProjectWorkFlowDefaultID?.ToString();

            }
            else
            {
               base.PageEventLoadPostBackForGridViewHeader(this.gvModel);
                //this.gvModel.RowCommand += GridView_RowCommand;               

                //this.gvModel.RowUpdated += GridView_RowUpdated;
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
            AddModel();
        }
        protected void ButtonClearModel_Click(object sender, EventArgs e)
        {
            Clear();
            this.SetFocus(txtName);
        }

        protected void gvModel_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

            UpdateModelFromGridRow(e);

        }

        #endregion

        #region control's action binds

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

                    var valueUIDStringOld = e.OldValues[0] as string;
                    Guid.TryParse(valueUIDStringOld, out Guid uid);

                    string name = e.NewValues[2] as string;
                    string description = e.NewValues[3] as string;

                    Guid workflowId = SessionTools.ProjectWorkFlowDefaultID == null ? Guid.Empty : SessionTools.ProjectWorkFlowDefaultID.Value;

                    //bool isStarter = checkboxIsStarter.Checked;
                    //bool isFinalizer = checkboxIsFinalizer.Checked;
                    bool.TryParse(e.NewValues[4].ToString().ToLower(), out bool isStarter);
                    bool.TryParse(e.NewValues[5].ToString().ToLower(), out bool isFinalizer);
                    bool.TryParse(e.NewValues[6].ToString().ToLower(), out bool objectionsAvailabled);
                    bool.TryParse(e.NewValues[7].ToString().ToLower(), out bool assentsAvailabled);

                    //if (Guid.TryParse(prevProccessIDString, out Guid prevID))
                    //{ 

                    //}


                    Guid? prevProccessID = null;
                    var prevProccessIDString = e.NewValues[9] as string;                    
                    if (!string.IsNullOrEmpty(prevProccessIDString) && Guid.TryParse(prevProccessIDString, out Guid prevID))
                    {
                        prevProccessID = prevID;
                    }


                    Guid? nextProccessID = null;
                    var nextProccessIDString = e.NewValues[10] as string;
                    if (!string.IsNullOrEmpty(nextProccessIDString) && Guid.TryParse(nextProccessIDString, out Guid nextID))
                    {
                        nextProccessID = nextID;
                    }

                    Guid? alwaysProccessID = null;
                    var alwaysProccessIDString = e.NewValues[11] as string;
                    if (!string.IsNullOrEmpty(alwaysProccessIDString) && Guid.TryParse(alwaysProccessIDString, out Guid alwaysID))
                    {
                        alwaysProccessID = alwaysID;
                    }

                    bool.TryParse(e.NewValues[12].ToString().ToLower(), out bool enabledForDirectiveManager);
                    bool.TryParse(e.NewValues[13].ToString().ToLower(), out bool enabledForInvestigationOfficer);
                    bool.TryParse(e.NewValues[14].ToString().ToLower(), out bool enabledForAssistantLeader);
                    bool.TryParse(e.NewValues[15].ToString().ToLower(), out bool enabledForDirectiveLeader);
                    bool.TryParse(e.NewValues[16].ToString().ToLower(), out bool enabledForOnlyDirectiveLeader);
                    bool.TryParse(e.NewValues[17].ToString().ToLower(), out bool enabledForResearchDirector);
                    bool.TryParse(e.NewValues[18].ToString().ToLower(), out bool enabledForExecutiveOfficer);
                    bool.TryParse(e.NewValues[19].ToString().ToLower(), out bool enabledForExternalResources);

                    //var prevProccessIDString = e.NewValues[7] as string;
                    //if (!string.IsNullOrEmpty(prevProccessIDString) && Guid.TryParse(prevProccessIDString, out Guid prevID))
                    //{
                    //    prevProccessID = prevID;
                    //}


                    //if (Guid.TryParse(DropDownListNextProccess.SelectedValue, out Guid nextID))
                    //{
                    //    nextProccessID = nextID;
                    //}

                    //Guid? alwaysProccessID = null;
                    //if (Guid.TryParse(this.DropDownListAlwaysProccess.SelectedValue, out Guid alwaysID))
                    //{
                    //    alwaysProccessID = alwaysID;
                    //}

                    int.TryParse(e.NewValues[8].ToString(), out int statusID);


                    var checks = new Tuple<bool, bool>(isStarter, isFinalizer);
                    var availabledChecks = new Tuple<bool, bool>(objectionsAvailabled, assentsAvailabled);
                    var enabledChecks = new Tuple<bool, bool, bool, bool, bool>(enabledForDirectiveManager, enabledForInvestigationOfficer, enabledForAssistantLeader, enabledForDirectiveLeader, enabledForOnlyDirectiveLeader);
                    var enabledChecks2 = new Tuple<bool, bool, bool>(enabledForResearchDirector, enabledForExecutiveOfficer, enabledForExternalResources);


                    var foreigns = new Tuple<Guid?, Guid?, Guid?, int>(prevProccessID, nextProccessID, alwaysProccessID, statusID);


                    if (UpdateModelToDB(uid: uid, name: name, description: description, workflowid: workflowId, checks: checks, foreings: foreigns, availabledChecks: availabledChecks, enabledChecks: enabledChecks, enabledChecks2: enabledChecks2))
                    {

                        //Clear();
                        ////gv.Databind();
                        //gvModel.DataBind();
                        //SetFocus(txtName);


                    }

                    //var datarow = gvModel.Rows[editIndex];


                    //var valueUIDStringOld = e.OldValues[0] as string;
                    //Guid.TryParse(valueUIDStringOld, out Guid uid);

                    //var name = e.NewValues[2] as string;
                    //var description = e.NewValues[3] as string;
                    //var route = e.NewValues[4] as string;
                    //bool.TryParse(e.NewValues[5].ToString().ToLower(), out bool notvisibleformenu);
                    //bool.TryParse(e.NewValues[6].ToString().ToLower(), out bool isroot);
                    //bool.TryParse(e.NewValues[7].ToString().ToLower(), out bool isagrupation);

                    //var targetofIdString = e.NewValues[8] as string;
                    //Guid.TryParse(targetofIdString, out Guid targetofID);

                    //var iconClass = e.NewValues[9] as string;

                    //Tuple<bool, bool, bool> checks = new Tuple<bool, bool, bool>(notvisibleformenu, isroot, isagrupation);

                    //if (UpdateModelToDB(uid, name, description, route, targetofID, checks, iconClass))
                    //{


                    //}


                }

            }


        }

        protected void AddModel()
        {

            string name = txtName.Text;
            string description = txtDescription.Text;

            Guid workflowId = SessionTools.ProjectWorkFlowDefaultID == null ? Guid.Empty: SessionTools.ProjectWorkFlowDefaultID.Value;

            bool isStarter = checkboxIsStarter.Checked;
            bool isFinalizer = checkboxIsFinalizer.Checked;

            bool objectionsAvailabled = this.checkboxObjectionsAvailabled.Checked;
            bool assentsAvailabled = this.checkboxAssentsAvailabled.Checked;

            bool enabledForDirectiveManager = false;
            bool enabledForInvestigationOfficer = false;
            bool enabledForAssistantLeader = false;
            bool enabledForDirectiveLeader = false;
            bool enabledForOnlyDirectiveManager = false;
            bool enabledForResearchDirector = false;
            bool enabledForExecutiveOfficer = false;
            bool enabledForExternalResources = false;


            var  selectedIdxs = this.ListBoxEnabledFor.GetSelectedIndices();
            if (selectedIdxs.Length > 0)
            {
                foreach (var idx in selectedIdxs)
                {

                    var selectedValue = this.ListBoxEnabledFor.Items[idx].Value;

                    switch (selectedValue)
                    {
                        case "1":
                            enabledForDirectiveManager = true;
                            break;

                        case "2":
                            enabledForInvestigationOfficer = true;
                            break;
                        case "3":
                            enabledForAssistantLeader = true;
                            break;
                        case "4":
                            enabledForDirectiveLeader = true;
                            break;

                        case "5":
                            enabledForOnlyDirectiveManager = true;
                            break;

                        case "6":
                            enabledForResearchDirector = true;
                            break;

                        case "7":
                            enabledForExecutiveOfficer = true;
                            break;

                        case "8":
                            enabledForExternalResources = true;
                            break;


                    }

                }
            }


            Guid? prevProccessID = null;
            if (Guid.TryParse(DropDownListPreviousProccess.SelectedValue, out Guid prevID))
            {
                prevProccessID = prevID;
            }

            Guid? nextProccessID = null;
            if (Guid.TryParse(DropDownListNextProccess.SelectedValue, out Guid nextID))
            {
                nextProccessID = nextID;
            }

            Guid? alwaysProccessID = null;
            if (Guid.TryParse(this.DropDownListAlwaysProccess.SelectedValue, out Guid alwaysID))
            {
                alwaysProccessID = alwaysID;
            }

            int.TryParse(DropDownListProjectStatus.SelectedValue, out int statusID);
           

            var checks = new Tuple<bool, bool>(isStarter, isFinalizer);

            var availabledChecks = new Tuple<bool, bool>(objectionsAvailabled, assentsAvailabled);
            var enabledChecks = new Tuple<bool, bool, bool, bool, bool>(enabledForDirectiveManager, enabledForInvestigationOfficer, enabledForAssistantLeader, enabledForDirectiveLeader, enabledForOnlyDirectiveManager);
            var enabledChecks2 = new Tuple<bool, bool, bool>(enabledForResearchDirector, enabledForExecutiveOfficer, enabledForExternalResources);
            var foreigns = new Tuple<Guid? , Guid?, Guid?, int>(prevProccessID, nextProccessID, alwaysProccessID, statusID);


            if (AddModelToDB(name: name, description: description, workflowid: workflowId, checks: checks, foreings: foreigns, availabledChecks: availabledChecks, enabledChecks: enabledChecks, enabledChecks2:enabledChecks2  ))
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
            ClearWebControls(txtName, txtDescription, this.checkboxIsStarter, this.checkboxIsFinalizer, this.DropDownListProjectStatus, this.checkboxObjectionsAvailabled, this.checkboxAssentsAvailabled, this.DropDownListProjectStatus, this.DropDownListPreviousProccess, this.DropDownListNextProccess, DropDownListAlwaysProccess, this.ListBoxEnabledFor);
        }
        public void BindDropDownLists()
        {          

            BindPreviousProcessList();
            BindNextProcessList();
            BindAlwaysProcessList();


        }

        public void BindPreviousProcessList()
        {

            Guid? ProjectWorkFlowDefaultID = SessionTools.ProjectWorkFlowDefaultID;

            SqlDataSourceListPreviousProccess.SelectParameters.Clear();
            SqlDataSourceListPreviousProccess.SelectParameters.Add("workflowid", "");
            SqlDataSourceListPreviousProccess.SelectParameters["workflowid"].DefaultValue = ProjectWorkFlowDefaultID?.ToString();

            this.DropDownListPreviousProccess.Items.Clear();


            this.SqlDataSourceListPreviousProccess.DataBind();
            DropDownListPreviousProccess.DataBind();
            DropDownListPreviousProccess.Items.Insert(0, new ListItem("None", ""));
            DropDownListPreviousProccess.Items[0].Selected = true;


        }

        public void BindNextProcessList()
        {

            Guid? ProjectWorkFlowDefaultID = SessionTools.ProjectWorkFlowDefaultID;

            SqlDataSourceListNextProccess.SelectParameters.Clear();
            SqlDataSourceListNextProccess.SelectParameters.Add("workflowid", "");
            SqlDataSourceListNextProccess.SelectParameters["workflowid"].DefaultValue = ProjectWorkFlowDefaultID?.ToString();

            this.DropDownListNextProccess.Items.Clear();


            this.SqlDataSourceListNextProccess.DataBind();
            DropDownListNextProccess.DataBind();
            DropDownListNextProccess.Items.Insert(0, new ListItem("None", ""));
            this.DropDownListNextProccess.Items[0].Selected = true;

        }

        public void BindAlwaysProcessList()
        {

            Guid? ProjectWorkFlowDefaultID = SessionTools.ProjectWorkFlowDefaultID;

            this.SqlDataSourceListAlwaysProccess.SelectParameters.Clear();
            SqlDataSourceListAlwaysProccess.SelectParameters.Add("workflowid", "");
            SqlDataSourceListAlwaysProccess.SelectParameters["workflowid"].DefaultValue = ProjectWorkFlowDefaultID?.ToString();

            this.DropDownListAlwaysProccess.Items.Clear();


            this.SqlDataSourceListAlwaysProccess.DataBind();
            DropDownListAlwaysProccess.DataBind();
            DropDownListAlwaysProccess.Items.Insert(0, new ListItem("None", ""));
            this.DropDownListAlwaysProccess.Items[0].Selected = true;

        }
        #endregion


        #region data model actions



        protected bool AddModelToDB(string name, string description, Guid? workflowid, Tuple<bool, bool> checks, Tuple<Guid?, Guid?, Guid?, int> foreings, Tuple<bool, bool> availabledChecks, Tuple<bool, bool, bool, bool, bool> enabledChecks , Tuple<bool,bool,bool> enabledChecks2)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                string insertCommand =
                    $"insert into Process (uid, name, description, WorkflowId, " +
                    $"IsStarter, IsFinalizer, " +
                    $"PreviousProcessId, NextProcessId, EstatusId, AlwaysProcessId,  " +
                    $"objectionsAvailabled, assentsAvailabled,  " +
                    $"enabledForDirectiveManager, enabledForInvestigationOfficer, enabledForAssistantLeader, enabledForDirectiveLeader, enabledForOnlyDirectiveManager,enabledForResearchDirector,enabledForExecutiveOfficer,enableForExternalResources ) " +
                    $"values (@uid,@name,@description, @WorkflowId, @IsStarter, @IsFinalizer,  " +
                    $"iif(@PreviousProcessId = '', null, @PreviousProcessId )," +
                    $"iif(@NextProcessId = '', null, @NextProcessId )," +
                    $"@EstatusId, " +
                    $"iif(@AlwaysProcessId = '', null, @AlwaysProcessId ) , " +
                    $"@objectionsAvailabled, @assentsAvailabled,  " +
                    $"@enabledForDirectiveManager, @enabledForInvestigationOfficer, @enabledForAssistantLeader, @enabledForDirectiveLeader, @enabledForOnlyDirectiveManager,@enabledForResearchDirector,@enabledForExecutiveOfficer,@enabledForExternalResources " +
                    $")";

                //$"values (@uid, @name, @)" +
                //$"select newid(), '{name}', '{description}', {priority}, " +
                //$"{Convert.ToInt32(whichAreIt.Item1)},{Convert.ToInt32(whichAreIt.Item2)},{Convert.ToInt32(whichAreIt.Item3)},{Convert.ToInt32(whichAreIt.Item4)},{Convert.ToInt32(whichAreIt.Item5)},{Convert.ToInt32(whichAreIt.Item6)},{Convert.ToInt32(whichAreIt.Item7)}, " +
                //$"{Convert.ToInt32(whichCanDo.Item1)},{Convert.ToInt32(whichCanDo.Item2)},{Convert.ToInt32(whichCanDo.Item3)},{Convert.ToInt32(whichCanDo.Item4)} ";

                var uid = Guid.NewGuid();
                string PreviousProcessId = foreings.Item1 == null ? string.Empty : foreings.Item1.ToString();
                string NextProcessId = foreings.Item2 == null ? string.Empty : foreings.Item2.ToString();
                string AlwaysProcessId = foreings.Item3 == null ? string.Empty : foreings.Item3.ToString();

                SqlCommand cmd = new SqlCommand(insertCommand, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@WorkflowId", workflowid.ToString());
                cmd.Parameters.AddWithValue("@IsStarter", checks.Item1);
                cmd.Parameters.AddWithValue("@IsFinalizer", checks.Item2);

                cmd.Parameters.AddWithValue("@objectionsAvailabled", availabledChecks.Item1);
                cmd.Parameters.AddWithValue("@assentsAvailabled", availabledChecks.Item2);

                cmd.Parameters.AddWithValue("@enabledForDirectiveManager", enabledChecks.Item1);
                cmd.Parameters.AddWithValue("@enabledForInvestigationOfficer", enabledChecks.Item2);
                cmd.Parameters.AddWithValue("@enabledForAssistantLeader", enabledChecks.Item3);
                cmd.Parameters.AddWithValue("@enabledForDirectiveLeader", enabledChecks.Item4);
                cmd.Parameters.AddWithValue("@enabledForOnlyDirectiveManager", enabledChecks.Item5);
                cmd.Parameters.AddWithValue("@enabledForResearchDirector", enabledChecks2.Item1);
                cmd.Parameters.AddWithValue("@enabledForExecutiveOfficer", enabledChecks2.Item2);
                cmd.Parameters.AddWithValue("@enabledForExternalResources", enabledChecks2.Item3);

                cmd.Parameters.AddWithValue("@PreviousProcessId", PreviousProcessId);
                cmd.Parameters.AddWithValue("@NextProcessId", NextProcessId);
                cmd.Parameters.AddWithValue("@AlwaysProcessId", AlwaysProcessId);
                cmd.Parameters.AddWithValue("@EstatusId", foreings.Item4);


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to add project process", ex);
            }
            finally
            {

            }

            return result;

        }


        protected bool UpdateModelToDB(Guid uid, string name, string description, Guid? workflowid, Tuple<bool, bool> checks, Tuple<Guid?, Guid?, Guid?, int> foreings, Tuple<bool, bool> availabledChecks, Tuple<bool, bool, bool, bool, bool> enabledChecks, Tuple<bool,bool,bool> enabledChecks2)
        {
            bool result;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["eblueConnectionString"].ConnectionString);

            try
            {
                cn.Open();
                
                string command =
                    $"update Process set WorkflowId = @WorkflowId,  name = @name, description = @description, IsStarter = @IsStarter, IsFinalizer = @IsFinalizer, " +
                    $"PreviousProcessId = iif(@PreviousProcessId = '', null, @PreviousProcessId ), " +
                    $"NextProcessId = iif(@NextProcessId = '', null, @NextProcessId ), EstatusId = @EstatusId, " +
                    $"AlwaysProcessId = iif(@AlwaysProcessId = '', null, @AlwaysProcessId ) ,  " +
                    $"objectionsAvailabled = @objectionsAvailabled , " +
                    $"assentsAvailabled = @assentsAvailabled ,  " +
                    $"enabledForDirectiveManager = @enabledForDirectiveManager , " +
                    $"enabledForInvestigationOfficer = @enabledForInvestigationOfficer ,  " +
                    $"enabledForAssistantLeader = @enabledForAssistantLeader , " +
                    $"enabledForDirectiveLeader = @enabledForDirectiveLeader, " +
                    $"enabledForOnlyDirectiveManager = @enabledForOnlyDirectiveManager, " +
                    $"enabledForResearchDirector = @enabledForResearchDirector, " +
                    $"enabledForExecutiveOfficer = @enabledForExecutiveOfficer, " +
                    $"enabledForExternalResources = @enabledForExternalResources " +
                    " where uid = @uid ";


                string PreviousProcessId = foreings.Item1 == null ? string.Empty : foreings.Item1.ToString();
                string NextProcessId = foreings.Item2 == null ? string.Empty : foreings.Item2.ToString();
                string AlwaysProcessId = foreings.Item3 == null ? string.Empty : foreings.Item3.ToString();

                SqlCommand cmd = new SqlCommand(command, cn);
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@WorkflowId", workflowid);
                cmd.Parameters.AddWithValue("@IsStarter", checks.Item1);
                cmd.Parameters.AddWithValue("@IsFinalizer", checks.Item2);

                cmd.Parameters.AddWithValue("@objectionsAvailabled", availabledChecks.Item1);
                cmd.Parameters.AddWithValue("@assentsAvailabled", availabledChecks.Item2);

                cmd.Parameters.AddWithValue("@enabledForDirectiveManager", enabledChecks.Item1);
                cmd.Parameters.AddWithValue("@enabledForInvestigationOfficer", enabledChecks.Item2);
                cmd.Parameters.AddWithValue("@enabledForAssistantLeader", enabledChecks.Item3);
                cmd.Parameters.AddWithValue("@enabledForDirectiveLeader", enabledChecks.Item4);
                cmd.Parameters.AddWithValue("@enabledForOnlyDirectiveManager", enabledChecks.Item5);
                cmd.Parameters.AddWithValue("@enabledForResearchDirector", enabledChecks2.Item1);
                cmd.Parameters.AddWithValue("@enabledForExecutiveOfficer", enabledChecks2.Item2);
                cmd.Parameters.AddWithValue("@enabledForExternalResources", enabledChecks2.Item3);

                cmd.Parameters.AddWithValue("@PreviousProcessId", PreviousProcessId);
                cmd.Parameters.AddWithValue("@NextProcessId", NextProcessId);
                cmd.Parameters.AddWithValue("@AlwaysProcessId", AlwaysProcessId);
                cmd.Parameters.AddWithValue("@EstatusId", foreings.Item4);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Close();

                result = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error while trying to update project process", ex);
            }
            finally
            {

            }

            return result;

        }

        #endregion



    }
}