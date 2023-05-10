<%@ Page Title="Estación Experimental Agrícola - Project Workflow Processes" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProjectProcessSteps.aspx.cs" Inherits="Eblue.Flows.ProjectProcessSteps" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper" >
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>Project Workflow Processes</h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                                <li class="breadcrumb-item active">Project Workflow Processes</li>
                            </ol>
                        </div>
                    </div>
                </div>
                <!-- /.container-fluid -->
            </section>

            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <!-- SELECT2 EXAMPLE -->
                    <div class="card card-default">
                        <div class="card-header">
                            <h3 class="card-title">Data Info</h3>

                            <div class="card-tools">
                                <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                
                            </div>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">  
                                    
                                        <label for="txtName">Name</label>
                                        <asp:TextBox runat="server" ID="txtName"  CssClass="form-control" TextMode="SingleLine" TabIndex="1"   />
                                        <asp:RequiredFieldValidator ID="txtNameValidatorRequired" Display="Dynamic" SetFocusOnError="true" 
                                            ControlToValidate="txtName" runat="server" 
                                            ErrorMessage="Please Add a Name" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">
                                        <label for="txtDescription">Description</label>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="SingleLine" TabIndex="2" />
                                        <asp:RequiredFieldValidator ID="txtDescriptionValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="txtDescription" runat="server"  SetFocusOnError="true" 
                                            ErrorMessage="Please Add a Description" ValidationGroup="ModelAdd">
                                            </asp:RequiredFieldValidator>
                                    </div>
                                    

                                    <div class="form-group">

                                    
                                    <asp:CheckBox ID="checkboxIsStarter"  CssClass="form-control" runat="server" TextAlign="Right" Text="①IsStarter " />
                                    <asp:CheckBox ID="checkboxIsFinalizer"  CssClass="form-control" runat="server" TextAlign="Right" Text="②IsFinalizer " />
                                        <asp:CheckBox ID="checkboxObjectionsAvailabled"  CssClass="form-control" runat="server" TextAlign="Right" Text="③Objections Availabled " />
                                        <asp:CheckBox ID="checkboxAssentsAvailabled"  CssClass="form-control" runat="server" TextAlign="Right" Text="④Assents Availabled " />
                                </div>

                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListProjectStatus">Estatus</label>
                                    

                                    <asp:DropDownList ID="DropDownListProjectStatus"  CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListProjectStatus"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="DropDownListProjectStatusValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="DropDownListProjectStatus" runat="server"  SetFocusOnError="true" 
                                            ErrorMessage="Please Select a ProjectStatus" ValidationGroup="ModelAdd">
                                            </asp:RequiredFieldValidator>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceListProjectStatus" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                        select distinct ps.ProjectStatusID uid, ps.ProjectStatusName description

                                        from ProjectStatus ps
                                        
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->


                                </div>
                                <!-- /.col -->
                                <div class="col-md-6">
                                   <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListPreviousProccess">Previous Proccess</label>
                                    

                                    <asp:DropDownList ID="DropDownListPreviousProccess"  CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListPreviousProccess"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceListPreviousProccess" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                        select distinct pcs.uid, pcs.description

                                        from process pcs
                                        where pcs.workflowid = @workflowid
                                        
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->

                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListNextProccess">Next Proccess</label>
                                    

                                    <asp:DropDownList ID="DropDownListNextProccess" CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListNextProccess"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceListNextProccess" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                        select distinct pcs.uid, pcs.description

                                        from process pcs
                                        where pcs.workflowid = @workflowid
                                        
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->

                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListAlwaysProccess">Always Proccess</label>
                                    

                                    <asp:DropDownList ID="DropDownListAlwaysProccess" CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListAlwaysProccess"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceListAlwaysProccess" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                        select distinct pcs.uid, pcs.description

                                        from process pcs
                                        where pcs.workflowid = @workflowid
                                        
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">
                    <label>Enabled For?</label> (to mark several use the ctrl key)
                                        <asp:ListBox ID="ListBoxEnabledFor" TabIndex="5" AppendDataBoundItems="true" runat="server" CssClass="form-control" SelectionMode="Multiple" >
                                            <asp:ListItem disabled  Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">①Directive Manager</asp:ListItem>
                                            <asp:ListItem Value="2">②Investigation Officer</asp:ListItem>
                                            <asp:ListItem Value="3">③Assistant Leader</asp:ListItem>
                                            <asp:ListItem Value="4">④Directive Leader</asp:ListItem>
                                            <asp:ListItem Value="5">⑤(Only)Directive Manager</asp:ListItem>
                                            <asp:ListItem Value="6">⑤Research Director</asp:ListItem>
                                            <asp:ListItem Value="7">⑤Executive Officer</asp:ListItem>
                                            <asp:ListItem Value="8">⑤External Resources</asp:ListItem>


                                        </asp:ListBox>

                  </div>
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            <asp:Button TabIndex="6" ID="buttonNewModel" ValidationGroup="ModelAdd" OnClick="ButtonNewModel_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                            

                            <asp:Button TabIndex="7" ID="buttonClearModel"  OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />
                           

                        </div>

                       
                    </div>
                    <!-- /.row -->
                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Data List</h3>                                      
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body">
                                    <asp:GridView ID="gvModel" runat="server" CssClass="gridview table table-bordered table-striped" 
                                        AutoGenerateColumns="false" 
                                        DataSourceID="dataSourceModel"
                                         
                                        >

                                        <Columns>

                                            
                                            <asp:TemplateField HeaderText="#" SortExpression="RowNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumerItem" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblUIDEdit" runat="server" Text='<%# Bind("UID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblRowNumerEdit" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNameItem" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNameEdit" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescriptionItem" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescriptionEdit" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>                                            

                                             <asp:TemplateField HeaderText="Questions">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkboxIsStarterItem" runat="server" Enabled="false" Checked='<%# Bind("IsStarter") %>' TextAlign="Right" Text="①" Tooltip="IsStarter " />
                                                <asp:CheckBox ID="checkboxIsFinalizerItem" runat="server" Enabled="false" Checked='<%# Bind("IsFinalizer") %>' TextAlign="Right" Text="②"  Tooltip="IsFinalizer " />
                                                <asp:CheckBox ID="checkboxobjectionsAvailabledItem" runat="server" Enabled="false" Checked='<%# Bind("objectionsAvailabled") %>' TextAlign="Right" Text="③" Tooltip="Objections Availabled " />
                                                <asp:CheckBox ID="checkboxassentsAvailabledItem" runat="server" Enabled="false" Checked='<%# Bind("assentsAvailabled") %>' TextAlign="Right" Text="④" tooltip="Assents Availabled " />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                
                                               <asp:CheckBox ID="checkboxIsStarterEdit" runat="server"  Checked='<%# Bind("IsStarter") %>' TextAlign="Right" Text="①" Tooltip="IsStarter " />
                                                <asp:CheckBox ID="checkboxIsFinalizerEdit" runat="server"  Checked='<%# Bind("IsFinalizer") %>' TextAlign="Right" Text="②"  Tooltip="IsFinalizer " />
                                                <asp:CheckBox ID="checkboxobjectionsAvailabledEdit" runat="server"  Checked='<%# Bind("objectionsAvailabled") %>' TextAlign="Right" Text="③" Tooltip="Objections Availabled " />
                                                <asp:CheckBox ID="checkboxassentsAvailabledEdit" runat="server" Checked='<%# Bind("assentsAvailabled") %>' TextAlign="Right" Text="④" tooltip="Assents Availabled " />
                                           
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Status" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusItem" runat="server" Text='<%# Bind("StatusDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListProjectStatusEdit" runat="server" TabIndex="2" DataSourceID="SqlDataSourceListProjectStatus"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("EStatusID") %>'>
                                        
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Previous Proccess" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPreviousProccessDescriptionItem" runat="server" Text='<%# Bind("PreviousProccessDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListPreviousProccessEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListPreviousProccess"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("PreviousProcessId") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Next Proccess" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNextProcessDescriptionItem" runat="server" Text='<%# Bind("NextProcessDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListNextProccessEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListNextProccess"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("NextProcessId") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Always Proccess" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlwaysProccessDescriptionItem" runat="server" Text='<%# Bind("AlwaysProccessDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListAlwaysProccessEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListAlwaysProccess"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("AlwaysProcessId") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Enabled For?">

                                                <ItemTemplate>
                                                   
                                                    <asp:CheckBox ID="checkboxenabledForDirectiveManagerItem"      TextAlign="Right" Text="①" Tooltip="Directive Manager " runat="server" Enabled="false" Checked='<%# Bind("enabledForDirectiveManager") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForInvestigationOfficerItem"    TextAlign="Right" Text="②" Tooltip="Investigation Officer " runat="server" Enabled="false" Checked='<%# Bind("enabledForInvestigationOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForAssistantLeaderItem"   TextAlign="Right" Text="③" Tooltip="Assistant Leader " runat="server" Enabled="false" Checked='<%# Bind("enabledForAssistantLeader") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForDirectiveLeaderItem"   TextAlign="Right" Text="④" Tooltip="Directive Leader " runat="server" Enabled="false" Checked='<%# Bind("enabledForDirectiveLeader") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForOnlyDirectiveManagerItem"   TextAlign="Right" Text="⑤" Tooltip="(Only)Directive Manager " runat="server" Enabled="false" Checked='<%# Bind("enabledForOnlyDirectiveManager") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="checkboxenabledForDirectiveManagerEdit"      TextAlign="Right" Text="①" Tooltip="Directive Manager " runat="server"  Checked='<%# Bind("enabledForDirectiveManager") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForInvestigationOfficerEdit"    TextAlign="Right" Text="②" Tooltip="Investigation Officer " runat="server"  Checked='<%# Bind("enabledForInvestigationOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForAssistantLeaderEdit"   TextAlign="Right" Text="③" Tooltip="Assistant Leader " runat="server"  Checked='<%# Bind("enabledForAssistantLeader") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForDirectiveLeaderEdit"   TextAlign="Right" Text="④" Tooltip="Directive Leader " runat="server"  Checked='<%# Bind("enabledForDirectiveLeader") %>' />
                                                    <asp:CheckBox ID="checkboxenabledForOnlyDirectiveManagerEdit"   TextAlign="Right" Text="⑤" Tooltip="(Only)Directive Manager " runat="server"  Checked='<%# Bind("enabledForOnlyDirectiveManager") %>' />
                                                    
                                                    
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField  InsertVisible="true" ButtonType="Button" HeaderText ="Actions" ShowEditButton="true" ShowDeleteButton="true"  />
                                        </Columns>

                                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                                    </asp:GridView>

                                    <asp:SqlDataSource ID="dataSourceModel" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        OldValuesParameterFormatString="original_{0}"
                        UpdateCommand="update usertarget set uid = newid() where uid is null"
                        SelectCommand="
                                        select 

                                        ROW_NUMBER() over (order by mdl.orderline ) as rowNumber, 
                                        mdl.Uid, mdl.WorkflowId, mdl.Name, mdl.Description, 
                                        mdl.IsStarter, mdl.IsFinalizer, 
                                        mdl.objectionsAvailabled, mdl.assentsAvailabled,enabledForOnlyDirectiveManager,
                                        mdl.enabledForDirectiveManager, mdl.enabledForInvestigationOfficer, mdl.enabledForAssistantLeader, mdl.enabledForDirectiveLeader,mdl.enabledForResearchDirector,
                                        mdl.enabledForExecutiveOfficer,mdl.enabledForExternalResources,
                                        mdl.PreviousProcessId, mdl.AlwaysProcessId,
                                        mdl.NextProcessId, mdl.EstatusId, mdl.OrderLine,
                                        StatusDescription = (select top 1 tmp.ProjectStatusName from ProjectStatus tmp where tmp.ProjectStatusID = mdl.EstatusId),
                                        PreviousProccessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.PreviousProcessId),
                                        NextProcessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.NextProcessId),
                                        AlwaysProccessDescription = (select top 1 tmp.Description from Process tmp where tmp.UId = mdl.AlwaysProcessId)
                                        from Process mdl 
                                        where mdl.WorkflowId = @WorkflowId
      
                                        
                                        ">
                       
                    </asp:SqlDataSource>
                                    
                                </div>
                                <!-- /.card-body -->
                            </div>
                            <!-- /.card -->
                        </div>

                    </div>
                    <!-- /.row -->
                </div>
                <!-- /.container-fluid -->
            </section>
            <!-- /.content -->
        </div>
</asp:Content>
