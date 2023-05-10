<%@ Page Title="Estación Experimental Agrícola - User Permission Set" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="TargetPagePermissionSets.aspx.cs" Inherits="Eblue.Settings.TargetPagePermissionSets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper" >
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>User Permission Set</h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                                <li class="breadcrumb-item active">User Permission Set</li>
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
                                        <asp:TextBox runat="server" ID="txtName"  CssClass="" TextMode="SingleLine" TabIndex="1"   />
                                        <asp:RequiredFieldValidator ID="txtNameValidatorRequired" Display="Dynamic" SetFocusOnError="true" 
                                            ControlToValidate="txtName" runat="server" 
                                            ErrorMessage="Please Add a Name" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">
                                        <label for="txtDescription">Description</label>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="" TextMode="SingleLine" TabIndex="2" />
                                        <asp:RequiredFieldValidator ID="txtDescriptionValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="txtDescription" runat="server"  SetFocusOnError="true" 
                                            ErrorMessage="Please Add a Description" ValidationGroup="ModelAdd">
                                            </asp:RequiredFieldValidator>
                                    </div>
                                
                                    
                                </div>
                                <!-- /.col -->
                                <div class="col-md-6">
                                   
                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListRosterType">Roster Type</label>
                                    

                                    <asp:DropDownList ID="DropDownListRosterType" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceRosterType"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="DropDownListRosterTypeValidatorRequired" Display="Dynamic" SetFocusOnError="true" 
                                            ControlToValidate="DropDownListRosterType" runat="server" 
                                            ErrorMessage="Please Select a Roster Type" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceRosterType" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                             select 
                                         rc.UId, rc.description
                                          from  RosterCategory  rc
                                          order by rc.description
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->

                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListUserTarget">User Target Page</label>
                                    

                                    <asp:DropDownList ID="DropDownListUserTarget" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceUserTarget"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="DropDownListUserTargetValidatorRequired" Display="Dynamic" SetFocusOnError="true" 
                                            ControlToValidate="DropDownListUserTarget" runat="server" 
                                            ErrorMessage="Please Select a User Target Page" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceUserTarget" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                            select 
	                                        ut.UId, ut.Description
                                          from  UserTarget ut
                                          order by ut.Description
                                        
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

                                            <%--<asp:BoundField HeaderText="#" DataField="RowNumber" SortExpression="RowNumber"/>--%>
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
                                                    <asp:TextBox ID="txtNameEdit" CssClass="" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescriptionItem" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescriptionEdit" CssClass="" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Roster Type" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRosterTypeItem" runat="server" Text='<%# Bind("RosterTypeDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListRosterTypeEdit" runat="server" TabIndex="2"  DataSourceID="SqlDataSourceRosterType"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("RosterCategoryId") %>'>
                                        
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User Target Page" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserTargetItem" runat="server" Text='<%# Bind("targetpageDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListUserTargetEdit" runat="server" TabIndex="2"  DataSourceID="SqlDataSourceUserTarget"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("UserTargetId") %>'>
                                        
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
                        UpdateCommand="update usertarget set uid = newid() where uid is null"
                        SelectCommand="
                                       select
                                        ROW_NUMBER() over (order by urp.orderline ) as rowNumber, 
                                        urp.uid, urp.name, urp.description,  urp.UserTargetId, urp.RosterCategoryId,
                                        targetpageDescription = (select top 1 utt.Description from usertarget utt where utt.UId = urp.UserTargetId),
                                        rostertypeDescription = (select top 1 rct.Description from RosterCategory rct where rct.UId = urp.RosterCategoryId)
                                        from UserRolePersimission urp
                                        
                                        
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
