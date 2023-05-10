<%@ Page Title="Estación Experimental Agrícola - Role Types" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="RoleTypes.aspx.cs" Inherits="Eblue.Admin.RoleTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="wrapper">--%>

        <div class="content-wrapper" style="min-height: 568px;">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>Role Types</h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                                <li class="breadcrumb-item active">Role Types</li>
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
                                <%--<button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-remove"></i></button>--%>
                            </div>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">                                       

                                        <%--<label for="exampleInputFile">File input</label>
                                        <input type="text" class="form-control" placeholder="Enter ...">--%>
                                        <%--<asp:Label ID="LabelRosterName" CssClass="form-control-lg" runat="server" Text="Roster Name:"></asp:Label>--%>
                                       <%-- CssClass="text-danger" --%>
                                        <label for="txtName">Name</label>
                                        <asp:TextBox runat="server" ID="txtName"  CssClass="form-control" TextMode="SingleLine" TabIndex="1"   />
                                        <asp:RequiredFieldValidator ID="txtNameValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="txtName" runat="server" 
                                            ErrorMessage="Please Add a Name" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">
                                        <label for="txtDescription">Description</label>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="SingleLine" TabIndex="2" />
                                        
                                    </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">
                                        
                                        <label for="txtPriority">Priority</label>
                                        <asp:TextBox runat="server" ID="txtPriority" CssClass="form-control" TextMode="SingleLine" TabIndex="3" type="number" />
                                        <asp:RequiredFieldValidator ID="txtPriorityValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="txtPriority" runat="server" 
                                            ErrorMessage="Please Add a Priority" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /.form-group -->
                                </div>
                                <!-- /.col -->
                                <div class="col-md-6">
                                    <!-- select -->
                                    <div class="form-group">
                    <label>Which are it?</label> (to mark several use the ctrl key)
                                        <asp:ListBox ID="ListBoxWhichAreIt" TabIndex="4" AppendDataBoundItems="true" runat="server" CssClass="form-control" SelectionMode="Multiple" >
                                            <asp:ListItem disabled Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">①Directive Leader</asp:ListItem>
                                            <asp:ListItem Value="2">②Assistant Leader</asp:ListItem>
                                            <asp:ListItem Value="3">③Directive Manager</asp:ListItem>
                                            <asp:ListItem Value="4">④Visor Company</asp:ListItem>
                                            <asp:ListItem Value="5">⑤Work Administrator</asp:ListItem>
                                            <asp:ListItem Value="6">⑥Work Member</asp:ListItem>
                                            <asp:ListItem Value="7">⑦Task Officer</asp:ListItem>
                                            <asp:ListItem Value="8">⑧Investigation Officer</asp:ListItem>
                                            <asp:ListItem Value="9">⑨Research Director</asp:ListItem>
                                            <asp:ListItem Value="10">①⓪Executive  Officer</asp:ListItem>
                                            <asp:ListItem Value="11">①①External Resources Officer</asp:ListItem>
                                        </asp:ListBox>
                    <%--<select multiple="" class="form-control" TabIndex="4">
                      <option>option 1</option>
                      <option>option 2</option>
                      <option>option 3</option>
                      <option>option 4</option>
                      <option>option 5</option>
                    </select>--%>
                  </div>
                                    <!-- /.form-group -->
                                    <!-- Select multiple-->
                                    <div class="form-group">
                    <label>Which can do?</label> (to mark several use the ctrl key)
                                        <asp:ListBox ID="ListBoxWhichCando" TabIndex="5" AppendDataBoundItems="true" runat="server" CssClass="form-control" SelectionMode="Multiple" >
                                            <asp:ListItem disabled  Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">①Can Sign</asp:ListItem>
                                            <asp:ListItem Value="2">②Can Approve</asp:ListItem>
                                            <asp:ListItem Value="3">③Can Reject</asp:ListItem>
                                            <asp:ListItem Value="4">④Can Comment</asp:ListItem>
                                            
                                        </asp:ListBox>
                    <%--<select multiple="" class="form-control" TabIndex="5">
                      <option>option 1</option>
                      <option>option 2</option>
                      <option>option 3</option>
                      <option>option 4</option>
                      <option>option 5</option>
                    </select>--%>
                  </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListPermissionSet">Permission Set</label>
                                    <%--<asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" TextMode="SingleLine" TabIndex="2" />--%>

                                    <asp:DropDownList ID="DropDownListPermissionSet" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourcePermissionSet"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourcePermissionSet" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                          select 
                                          rsp.UId,
                                          rsp.Name, rsp.Description,
                                          concat ( rsp.Name, ' (', rsp.Description, ')') FullDescription from RoleSetPermission rsp
                                          where 
                                          rsp.IsRoot = 1 and rsp.IsForProject = 1
                                          order by OrderLine
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            <asp:Button TabIndex="6" ID="buttonNewModel" ValidationGroup="ModelAdd" OnClick="ButtonNewModel_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                            <%--<button type="submit" class="btn btn-primary">ADD</button>--%>

                            <asp:Button TabIndex="7" ID="buttonClearModel"  OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />
                            <%--<button type="submit" class="btn btn-secondary float-right">Clear Data Info</button>--%>

                        </div>

                        <%--<div class="card-footer">
              Visit <a href="https://select2.github.io/">Select2 documentation</a> for more examples and information about
            the plugin.
          </div>--%>
                    </div>
                    <!-- /.row -->
                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Data List</h3>

                                    <%--<div class="card-tools">
                                        <div class="input-group input-group-sm" style="width: 150px;">
                                            <input type="text" name="table_search" class="form-control float-right" placeholder="Search">

                                            <div class="input-group-append">
                                                <button type="submit" class="btn btn-default"><i class="fa fa-search"></i></button>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body">
                                    <asp:GridView ID="gvModel" runat="server" CssClass="gridview table table-bordered table-striped" 
                                        AutoGenerateColumns="false" 
                                        DataSourceID="dataSourceModel"
                                         
                                        >

                                        <Columns>

                                            <%--<asp:BoundField HeaderText="#" DataField="RowNumber" SortExpression="RowNumber"/>--%>
                                            <asp:TemplateField HeaderText="#" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumerItem" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblUIDEdit" runat="server" Text='<%# Bind("UID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblRowNumerEdit" runat="server" Text='<%# Bind("RowNumber") %>'></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNameItem" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNameEdit" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescriptionItem" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescriptionEdit" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorityItem" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPriorityEdit" type="number" CssClass="form-control" runat="server" Text='<%# Bind("Priority") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Which are it?">

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblWhichAreItItem" runat="server" Text='<%# Bind("RoleIndex") %>'></asp:Label>--%>
                                                    <asp:CheckBox ID="checkboxIsDLItem" runat="server" TextAlign="Right" Text="①" Tooltip="Is Directive Leader " Enabled="false" Checked='<%# Bind("IsDirectiveLeader") %>' />
                                                    <asp:CheckBox ID="checkboxIsALItem" runat="server" TextAlign="Right" Text="②" Tooltip="Is Assistant Leader " Enabled="false" Checked='<%# Bind("IsAssistantLeader") %>' />
                                                    <asp:CheckBox ID="checkboxIsDMItem" runat="server" TextAlign="Right" Text="③" Tooltip="Is Directive Manager " Enabled="false" Checked='<%# Bind("IsDirectiveManager") %>' />
                                                    <asp:CheckBox ID="checkboxIsVCItem" runat="server" TextAlign="Right" Text="④" Tooltip="Is Visor Company " Enabled="false" Checked='<%# Bind("IsVisorCompany") %>' />
                                                    <asp:CheckBox ID="checkboxIsWAItem" runat="server" TextAlign="Right" Text="⑤" Tooltip="Is Work Administrator " Enabled="false" Checked='<%# Bind("IsWorkAdministrator") %>' />
                                                    <asp:CheckBox ID="checkboxIsWMItem" runat="server" TextAlign="Right" Text="⑥" Tooltip="Is Work Member " Enabled="false" Checked='<%# Bind("IsWorkMember") %>' />
                                                    <asp:CheckBox ID="checkboxIsTOItem" runat="server" TextAlign="Right" Text="⑦" Tooltip="Is Task Officer " Enabled="false" Checked='<%# Bind("IsTaskOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxIsIOItem" runat="server" TextAlign="Right" Text="⑧" ToolTip="Is Investigation Officer" Enabled="false"  Checked='<%# Bind("IsInvestigationOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxIsRDRItem" runat="server" TextAlign="Right" Text="⑨" ToolTip="Is Research Director" Enabled="false"  Checked='<%# Bind("IsResearchDirector") %>' />
                                                    <asp:CheckBox ID="checkboxIsEOItem" runat="server" TextAlign="Right" Text="①⓪" ToolTip="Is Executive Officer" Enabled="false"  Checked='<%# Bind("IsExecutiveOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxIsERSItem" runat="server" TextAlign="Right" Text="①①" ToolTip="Is External Resources" Enabled="false"  Checked='<%# Bind("IsExternalResources") %>' />


                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="checkboxIsDLEdit" runat="server" TextAlign="Right" Text="①" Tooltip="Is Directive Leader "  Checked='<%# Bind("IsDirectiveLeader") %>' />
                                                    <asp:CheckBox ID="checkboxIsALEdit" runat="server" TextAlign="Right" Text="②" Tooltip="Is Assistant Leader " Checked='<%# Bind("IsAssistantLeader") %>' />
                                                    <asp:CheckBox ID="checkboxIsDMEdit" runat="server" TextAlign="Right" Text="③" Tooltip="Is Directive Manager " Checked='<%# Bind("IsDirectiveManager") %>' />
                                                    <asp:CheckBox ID="checkboxIsVCEdit" runat="server" TextAlign="Right" Text="④" Tooltip="Is Visor Company " Checked='<%# Bind("IsVisorCompany") %>' />
                                                    <asp:CheckBox ID="checkboxIsWAEdit" runat="server" TextAlign="Right" Text="⑤" Tooltip="Is Work Administrator " Checked='<%# Bind("IsWorkAdministrator") %>' />
                                                    <asp:CheckBox ID="checkboxIsWMEdit" runat="server" TextAlign="Right" Text="⑥" Tooltip="Is Work Member "  Checked='<%# Bind("IsWorkMember") %>' />
                                                    <asp:CheckBox ID="checkboxIsTOEdit" runat="server" TextAlign="Right" Text="⑦" Tooltip="Is Task Officer " Checked='<%# Bind("IsTaskOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxIsIOEdit" runat="server" TextAlign="Right" Text="⑧" ToolTip="Is Investigation Officer" Checked='<%# Bind("IsInvestigationOfficer") %>' />
                                                    <asp:CheckBox ID="checkboxIsRDREdit" runat="server" TextAlign="Right" Text="⑨" ToolTip="Is Research Director"   Checked='<%# Bind("IsResearchDirector") %>' />
                                                    <asp:CheckBox ID="checkboxIsEOEdit" runat="server" TextAlign="Right" Text="①⓪" ToolTip="Is Executive Officer"   Checked='<%# Bind("IsResearchDirector") %>' />
                                                    <asp:CheckBox ID="checkboxIsERSEdit" runat="server" TextAlign="Right" Text="①①" ToolTip="Is External Resources"   Checked='<%# Bind("IsResearchDirector") %>' />
                                                    
                                                   <%--⑨⓪ --%>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Which can do?">

                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblWhichAreItItem" runat="server" Text='<%# Bind("RoleIndex") %>'></asp:Label>--%>
                                                    <asp:CheckBox ID="checkboxHasSignItem"      TextAlign="Right" Text="①" Tooltip="CanSign " runat="server" Enabled="false" Checked='<%# Bind("HasSignature") %>' />
                                                    <asp:CheckBox ID="checkboxHasRejectItem"    TextAlign="Right" Text="②" Tooltip="CanReject " runat="server" Enabled="false" Checked='<%# Bind("HasRejection") %>' />
                                                    <asp:CheckBox ID="checkboxHasApproveItem"   TextAlign="Right" Text="③" Tooltip="CanApprove " runat="server" Enabled="false" Checked='<%# Bind("HasAproval") %>' />
                                                    <asp:CheckBox ID="checkboxHasCommentItem"   TextAlign="Right" Text="④" Tooltip="CanComment " runat="server" Enabled="false" Checked='<%# Bind("HasComment") %>' />
                                                    
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="checkboxHasSignEdit"      TextAlign="Right" Text="CanSign " runat="server"  Checked='<%# Bind("HasSignature") %>' />
                                                    <asp:CheckBox ID="checkboxHasRejectEdit"    TextAlign="Right" Text="CanReject " runat="server"  Checked='<%# Bind("HasRejection") %>' />
                                                    <asp:CheckBox ID="checkboxHasApproveEdit"   TextAlign="Right" Text="CanApprove " runat="server"  Checked='<%# Bind("HasAproval") %>' />
                                                    <asp:CheckBox ID="checkboxHasCommentEdit"   TextAlign="Right" Text="CanComment " runat="server"  Checked='<%# Bind("HasComment") %>' />
                                                    
                                                    
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Permission Set" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPermissionSetItem" runat="server" Text='<%# Bind("PSetDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListPermissionSeEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourcePermissionSet"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("PSetID") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

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
                        UpdateCommand="update rolecategory set orderpriority = 0 where orderline = 0"
                        SelectCommand="
                                        
                                        select 
iif (rsp.PSetID is null, cast(0 as bit), cast(1 as bit) ) hasRolePermissionSet, rsp.PSetID, rsp.PSetDescription ,
iif (rp.RoleCategoryID is null, cast(0 as bit), cast(1 as bit) ) hasRolePermission,

ROW_NUMBER() over (order by orderpriority ) as rowNumber, 0 as whichAreIt , uid, name, description, orderpriority as priority,
                                        IsDirectiveLeader, IsAssistantLeader, IsDirectiveManager, IsVisorCompany, IsWorkAdministrator, IsWorkMember, IsTaskOfficer,
                                        IsInvestigationOfficer,IsResearchDirector,IsExecutiveOfficer,IsExternalResources,
                                        HasSignature, HasRejection, HasAproval, HasComment, 
                                        case 
                                        when (IsResearchDirector =1 AND IsExecutiveOfficer =1 AND IsExternalResources =1 ) then '(1)(2)(3)'
                                        when (IsResearchDirector =1 AND IsExecutiveOfficer =1 AND IsExternalResources =0) then '(1)(2)'
                                        ELSE 'undefined' END AS roleindex,
                                        iif(IsDirectiveLeader=1, 'True', 'False') DLSelected,
                                        iif(IsAssistantLeader=1, 'True', 'False') ALSelected,
                                        iif(IsDirectiveManager=1, 'True', 'False') DMSelected,
                                        iif(IsResearchDirector =1, 'True', 'False') RDRSelected,
                                        iif(IsExecutiveOfficer =1, 'True', 'False') EOSelected,
                                        iif(IsExternalResources =1, 'True', 'False') ERSSelected
                                        from roleCategory 
										left join
										(
										select RoleCategoryID, RoleSetPermissionID from RolePermission
										) as rp on rp.RoleCategoryID = UId
										left join 
										(
										select 
  UId PSetID, Description PSetDescription from RoleSetPermission 
  where 
  IsRoot = 1 and IsForProject = 1
										) as rsp on rsp.PSetID = rp.RoleSetPermissionID
										order by RowNumber
                                        
                                        ">
                       
                    </asp:SqlDataSource>
                                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        OldValuesParameterFormatString="original_{0}"
                        UpdateCommand="update rolecategory set orderpriority = 0 where orderline = 0"
                        SelectCommand="
                                        
                                        select 
iif (rsp.PSetID is null, cast(0 as bit), cast(1 as bit) ) hasRolePermissionSet, rsp.PSetID, rsp.PSetDescription ,
iif (rp.RoleCategoryID is null, cast(0 as bit), cast(1 as bit) ) hasRolePermission,

ROW_NUMBER() over (order by orderpriority ) as rowNumber, 0 as whichAreIt , uid, name, description, orderpriority as priority,
                                        IsDirectiveLeader, IsAssistantLeader, IsDirectiveManager, IsVisorCompany, IsWorkAdministrator, IsWorkMember, IsTaskOfficer,
                                        HasSignature, HasRejection, HasAproval, HasComment, 
                                        case 
                                        when (IsDirectiveLeader =1 AND IsAssistantLeader=1 AND IsDirectiveManager=1) then '(1)(2)(3)'
                                        when (IsDirectiveLeader =1 AND IsAssistantLeader=1 AND IsDirectiveManager=0) then '(1)(2)'
                                        ELSE 'undefined' END AS roleindex,
                                        iif(IsDirectiveLeader=1, 'True', 'False') DLSelected,
                                        iif(IsAssistantLeader=1, 'True', 'False') ALSelected,
                                        iif(IsDirectiveManager=1, 'True', 'False') DMSelected
                                        from roleCategory 
										left join
										(
										select RoleCategoryID, RoleSetPermissionID from RolePermission
										) as rp on rp.RoleCategoryID = UId
										left join 
										(
										select 
  UId PSetID, concat ( Name, ' (',Description, ')') PSetDescription from RoleSetPermission 
  where 
  IsRoot = 1 and IsForProject = 1
										) as rsp on rsp.PSetID = rp.RoleSetPermissionID
										order by RowNumber
                                        
                                        ">
                       
                    </asp:SqlDataSource>--%>
                                    <%--<div id="example1_wrapper" class="dataTables_wrapper container-fluid dt-bootstrap4">
                                        <div class="row">
                                            <div class="col-sm-12 col-md-6">
                                                <div class="dataTables_length" id="example1_length">
                                                    <label>Show
                                                        <select name="example1_length" aria-controls="example1" class="form-control form-control-sm">
                                                            <option value="10">10</option>
                                                            <option value="25">25</option>
                                                            <option value="50">50</option>
                                                            <option value="100">100</option>
                                                        </select>
                                                        entries</label></div>
                                            </div>
                                            <div class="col-sm-12 col-md-6">
                                                <div id="example1_filter" class="dataTables_filter">
                                                    <label>Search:<input type="search" class="form-control form-control-sm" placeholder="" aria-controls="example1"></label></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="example1" class="table table-bordered table-striped dataTable" role="grid" aria-describedby="example1_info">
                                                    <thead>
                                                        <tr role="row">
                                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Rendering engine: activate to sort column ascending" style="width: 166px;">Rendering engine</th>
                                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending" style="width: 215px;">Browser</th>
                                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending" style="width: 191px;">Platform(s)</th>
                                                            <th class="sorting" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending" style="width: 141px;">Engine version</th>
                                                            <th class="sorting_asc" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column descending" aria-sort="ascending" style="width: 99px;">CSS grade</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>

























































                                                        <tr role="row" class="odd">
                                                            <td class="">Gecko</td>
                                                            <td>Firefox 1.0</td>
                                                            <td>Win 98+ / OSX.2+</td>
                                                            <td>1.7</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="even">
                                                            <td class="">Gecko</td>
                                                            <td>Firefox 1.5</td>
                                                            <td>Win 98+ / OSX.2+</td>
                                                            <td>1.8</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="odd">
                                                            <td class="">Gecko</td>
                                                            <td>Firefox 2.0</td>
                                                            <td>Win 98+ / OSX.2+</td>
                                                            <td>1.8</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="even">
                                                            <td class="">Gecko</td>
                                                            <td>Firefox 3.0</td>
                                                            <td>Win 2k+ / OSX.3+</td>
                                                            <td>1.9</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="odd">
                                                            <td class="">Gecko</td>
                                                            <td>Camino 1.0</td>
                                                            <td>OSX.2+</td>
                                                            <td>1.8</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="even">
                                                            <td class="">Gecko</td>
                                                            <td>Camino 1.5</td>
                                                            <td>OSX.3+</td>
                                                            <td>1.8</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="odd">
                                                            <td class="">Gecko</td>
                                                            <td>Netscape 7.2</td>
                                                            <td>Win 95+ / Mac OS 8.6-9.2</td>
                                                            <td>1.7</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="even">
                                                            <td class="">Gecko</td>
                                                            <td>Netscape Browser 8</td>
                                                            <td>Win 98SE+</td>
                                                            <td>1.7</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="odd">
                                                            <td class="">Gecko</td>
                                                            <td>Netscape Navigator 9</td>
                                                            <td>Win 98+ / OSX.2+</td>
                                                            <td>1.8</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                        <tr role="row" class="even">
                                                            <td class="">Gecko</td>
                                                            <td>Mozilla 1.0</td>
                                                            <td>Win 95+ / OSX.1+</td>
                                                            <td>1</td>
                                                            <td class="sorting_1">A</td>
                                                        </tr>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr>
                                                            <th rowspan="1" colspan="1">Rendering engine</th>
                                                            <th rowspan="1" colspan="1">Browser</th>
                                                            <th rowspan="1" colspan="1">Platform(s)</th>
                                                            <th rowspan="1" colspan="1">Engine version</th>
                                                            <th rowspan="1" colspan="1">CSS grade</th>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12 col-md-5">
                                                <div class="dataTables_info" id="example1_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                                            </div>
                                            <div class="col-sm-12 col-md-7">
                                                <div class="dataTables_paginate paging_simple_numbers" id="example1_paginate">
                                                    <ul class="pagination">
                                                        <li class="paginate_button page-item previous disabled" id="example1_previous"><a href="#" aria-controls="example1" data-dt-idx="0" tabindex="0" class="page-link">Previous</a></li>
                                                        <li class="paginate_button page-item active"><a href="#" aria-controls="example1" data-dt-idx="1" tabindex="0" class="page-link">1</a></li>
                                                        <li class="paginate_button page-item "><a href="#" aria-controls="example1" data-dt-idx="2" tabindex="0" class="page-link">2</a></li>
                                                        <li class="paginate_button page-item "><a href="#" aria-controls="example1" data-dt-idx="3" tabindex="0" class="page-link">3</a></li>
                                                        <li class="paginate_button page-item "><a href="#" aria-controls="example1" data-dt-idx="4" tabindex="0" class="page-link">4</a></li>
                                                        <li class="paginate_button page-item "><a href="#" aria-controls="example1" data-dt-idx="5" tabindex="0" class="page-link">5</a></li>
                                                        <li class="paginate_button page-item "><a href="#" aria-controls="example1" data-dt-idx="6" tabindex="0" class="page-link">6</a></li>
                                                        <li class="paginate_button page-item next" id="example1_next"><a href="#" aria-controls="example1" data-dt-idx="7" tabindex="0" class="page-link">Next</a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
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

    <%--</div>--%>
</asp:Content>
