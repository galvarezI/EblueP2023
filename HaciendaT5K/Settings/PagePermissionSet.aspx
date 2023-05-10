<%@ Page Title="Estación Experimental Agrícola - Page Permission Set" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PagePermissionSet.aspx.cs" Inherits="Eblue.Settings.PagePermissionSet" %>

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
                        <h1>Page Permission Set</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">Page Permission Set</li>
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
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control" TextMode="SingleLine" TabIndex="1" />
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
                                    <label for="DropDownListParent">Parent</label>
                                    <%--<asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" TextMode="SingleLine" TabIndex="2" />--%>

                                    <asp:DropDownList ID="DropDownListParent" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceParent"
                                        DataTextField="name" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceParent" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT uid, description as name FROM RosterSetPermission where isRoot = 1"></asp:SqlDataSource>


                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">

                                    <%--<label for="checkboxIsRoot">IsRoot</label>--%>
                                    <asp:CheckBox ID="checkboxIsRoot" runat="server" TextAlign="Right" Text="①IsRoot " />
                                    <asp:CheckBox ID="checkboxIsForProject" runat="server" TextAlign="Right" Text="②ForProject " />
                                    <asp:CheckBox ID="checkboxIsForProcess" runat="server" TextAlign="Right" Text="③ForProccess " />
                                </div>
                                <!-- /.form-group -->
                            </div>
                            <!-- /.col -->
                            <div class="col-md-6">
                                <!-- select -->
                                <div class="form-group">
                                    <label for="ListBoxForData">For Get?</label>
                                    (to mark several use the ctrl key)
                                        <asp:ListBox ID="ListBoxForData" TabIndex="4" AppendDataBoundItems="true" runat="server" CssClass="form-control" SelectionMode="Multiple">
                                            <asp:ListItem disabled Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1" disabled Selected>①When Get</asp:ListItem>
                                            <asp:ListItem Value="2">②Can Open</asp:ListItem>
                                            <asp:ListItem Value="3">③Can Block</asp:ListItem>

                                        </asp:ListBox>

                                </div>
                                <!-- /.form-group -->
                                <!-- Select multiple-->
                                <div class="form-group">
                                    <label for="ListBoxForList">For Post?</label>
                                    (to mark several use the ctrl key)
                                        <asp:ListBox ID="ListBoxForList" TabIndex="4" AppendDataBoundItems="true" runat="server" CssClass="form-control" SelectionMode="Multiple">
                                            <asp:ListItem disabled Value="0">None</asp:ListItem>
                                            <asp:ListItem disabled Value="1" Selected>①When Post</asp:ListItem>
                                            <asp:ListItem Value="2">②Can Close</asp:ListItem>
                                            <asp:ListItem Value="3">③Can Block</asp:ListItem>
                                             <asp:ListItem Value="4">④Can Up</asp:ListItem>
                                            <asp:ListItem Value="5">⑤Can Down</asp:ListItem>
                                        </asp:ListBox>

                                </div>
                                <!-- /.form-group -->

                                <div class="form-group">
                                    <label for="DropDownListSection">Target Page</label>
                                    <%--<asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" TextMode="SingleLine" TabIndex="2" />--%>

                                    <asp:DropDownList ID="DropDownListSection" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceSection"
                                        DataTextField="Description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="DropDownListSectionValidatorRequired" Display="Dynamic"
                                        ControlToValidate="DropDownListSection" runat="server"
                                        ErrorMessage="Please Select a Page" ValidationGroup="ModelAdd">

                                    </asp:RequiredFieldValidator>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceSection" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        select rt.Uid, rt.Name, rt.Description, 
                                        concat(rt.Name, ' (', isnull(rt.Description, 'noset'), ')[', rt.IconClass, ']' ) 
                                        FullDescription  from UserTarget rt
                                        order by rt.Description
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

                        <asp:Button TabIndex="7" ID="buttonClearModel" OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />
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


                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <asp:GridView ID="gvModel" runat="server" CssClass="gridview table table-bordered table-striped"
                                    AutoGenerateColumns="false"
                                    DataSourceID="dataSourceModel">

                                    <Columns>


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

                                                <asp:Label ID="lblNameItem" runat="server" Text='<%# Bind("Name") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNameEdit" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescriptionItem" runat="server" Text='<%# Bind("DisplayDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescriptionEdit" CssClass="form-control" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Parent" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblParentItem" runat="server" Text='<%# Bind("DisplaypermissionSetName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%--"TargetOf"--%>
                                                <asp:DropDownList ID="DropDownListParentEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceParent"
                                                    DataTextField="name" DataValueField="uid" SelectedValue='<%# Bind("ParentId") %>'>
                                                    <asp:ListItem Value="">None</asp:ListItem>
                                                </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Used For">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkboxIsRootItem" runat="server" Enabled="false" Checked='<%# Bind("IsRoot") %>' TextAlign="Right" Text="①" ToolTip="IsRoot " />
         <asp:CheckBox ID="checkboxIsForProjectItem" Enabled="false" runat="server" TextAlign="Right" Text="②" Tooltip="ForProject " Checked='<%# Bind("IsForProject")%>' />
                                                <asp:CheckBox ID="checkboxIsForProcessItem" Enabled="false" runat="server" TextAlign="Right" Text="③" Tooltip="ForProccess " Checked='<%# Bind("IsForProcess")%>' />
                                            
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="checkboxIsRootEdit" runat="server" TextAlign="Right" Text="①" ToolTip="IsRoot " Checked='<%# Bind("IsRoot") %>' />
                                                  <asp:CheckBox ID="checkboxIsForProjectEdit" runat="server" TextAlign="Right" Text="②" Tooltip="ForProject " Checked='<%# Bind("IsForProject")%>' />
                                                <asp:CheckBox ID="checkboxIsForProcessEdit" runat="server" TextAlign="Right" Text="③" Tooltip="ForProccess " Checked='<%# Bind("IsForProcess")%>' />
                                           
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="For-OnGet" >

                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkboxWhenDataItem" runat="server" Enabled="false" Checked='<%# Bind("whenGet") %>' TextAlign="Right" Text="①" Tooltip="Enabled Get  " />
                                                <asp:CheckBox ID="checkboxdataCapDetailItem" runat="server" Enabled="false" Checked='<%# Bind("getCapOpen") %>' TextAlign="Right" Text="②" Tooltip="Can Open  " />
                                                <asp:CheckBox ID="checkboxdataCapEditItem" runat="server" Enabled="false" Checked='<%# Bind("getCapblocked") %>' TextAlign="Right" Text="③" Tooltip="Can Block  " />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                
                                                <asp:CheckBox ID="checkboxWhenDataEdit" runat="server" TextAlign="Right" Text="①" Tooltip="Enabled Get " Checked='<%# Bind("whenGet") %>' />
                                                <asp:CheckBox ID="checkboxdataCapDetailEdit" runat="server" TextAlign="Right" Text="②" Tooltip="Can Open  " Checked='<%# Bind("getCapOpen") %>' />
                                                <asp:CheckBox ID="checkboxdataCapEditEdit" runat="server" TextAlign="Right" Text="③" Tooltip="Can Block  "  Checked='<%# Bind("getCapblocked") %>' />

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="For-OnPost"  >

                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkboxWhenListItem" runat="server" Enabled="false" Checked='<%# Bind("whenPost") %>' TextAlign="Right" Text="①"  Tooltip="Enabled Post  " />
                                                <asp:CheckBox ID="checkboxlistCapDetailItem" runat="server" Enabled="false" Checked='<%# Bind("postCapClose") %>' TextAlign="Right" Text="②" Tooltip="Can Close  " />
                                                <asp:CheckBox ID="checkboxlistCapAddItem" runat="server" Enabled="false" Checked='<%# Bind("postCapBlocked") %>' TextAlign="Right" Text="③" Tooltip="Can Block  " />
                                                <asp:CheckBox ID="checkboxlistCapRemoveItem" runat="server" Enabled="false" Checked='<%# Bind("postCapUp") %>' TextAlign="Right" Text="④"  Tooltip="Can Up  " />
                                                <asp:CheckBox ID="checkboxlistCapEditItem" runat="server" Enabled="false" Checked='<%# Bind("postCapDown") %>' TextAlign="Right" Text="⑤" Tooltip="Can Down  " />
                                            </ItemTemplate>
                                            <EditItemTemplate >
                                                <asp:CheckBox ID="checkboxWhenListEdit" runat="server" TextAlign="Right" Text="①"  Tooltip="Enabled Post  "  Checked='<%# Bind("whenPost") %>' />
                                                <asp:CheckBox ID="checkboxlistCapDetailEdit" runat="server" TextAlign="Right" Text="②" Tooltip="Can Close  " Checked='<%# Bind("postCapClose") %>' />
                                                <asp:CheckBox ID="checkboxlistCapAddEdit" runat="server" TextAlign="Right" Text="③" Tooltip="Can Block  " Checked='<%# Bind("postCapBlocked") %>' />
                                                <asp:CheckBox ID="checkboxlistCapRemoveEdit" runat="server" TextAlign="Right" Text="④"  Tooltip="Can Up  " Checked='<%# Bind("postCapUp") %>' />
                                                <asp:CheckBox ID="checkboxlistCapEditEdit" runat="server" TextAlign="Right" Text="⑤" Tooltip="Can Down  " Checked='<%# Bind("postCapDown") %>' />

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Target Page">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSectionItem" runat="server" Text='<%# Bind("pageDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownListSectionEdit" runat="server" TabIndex="2" DataSourceID="SqlDataSourceSection"
                                                    DataTextField="Description" DataValueField="uid" SelectedValue='<%# Bind("UserTargetId") %>'>
                                                </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField InsertVisible="true" ButtonType="Button" HeaderText="Actions" ShowEditButton="true" ShowDeleteButton="true" />
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
                        ROW_NUMBER() over (order by orderline desc ) as rowNumber,
                        case 
	                        when rsp.isroot =1 then rsp.withTargetOf
	                        when rsp.isroot =0 then rsp.TargetOf
                        end ParentID,
                         dbo.DisplayGridText( (select description from RosterSetPermission where uid = 
                                    case 
	                        when rsp.isroot =1 then rsp.withTargetOf
	                        when rsp.isroot =0 then rsp.TargetOf
                        end
                                    
                                    ) ) DisplaypermissionSetName ,
                        (select description from RosterSetPermission where uid = 
                                    case 
	                        when rsp.isroot =1 then rsp.withTargetOf
	                        when rsp.isroot =0 then rsp.TargetOf
                        end
                                    ) permissionSetName ,
                        rsp.UserTargetId,


                        (select  rt.Description  from UserTarget rt where rt.Uid = rsp.UserTargetId ) pageDescription,
                                    rsp.Uid,  REPLACE( rsp.Name, '.', ' ') Name,
                                   dbo.DisplayGridText( rsp.Description ) DisplayDescription,
                                    rsp.Description, 
                                    rsp.IsRoot
									, 
                        rsp.whenGet, rsp.GetCapOpen,rsp.GetCapBlocked, 
                        rsp.whenPost, rsp.PostCapClose, rsp.PostCapBlocked, rsp.PostCapUp, rsp.PostCapDown,
                        rsp.IsForProject, rsp.IsForProcess,
                        rsp.OrderLine, rsp.TargetOf

                        from RosterSetPermission rsp  order by orderline desc
                                    
                                    "></asp:SqlDataSource>

                            

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
