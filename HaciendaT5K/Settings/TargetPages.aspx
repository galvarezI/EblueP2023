<%@ Page Title="Estación Experimental Agrícola - User Target Pages" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="TargetPages.aspx.cs" Inherits="Eblue.Settings.TargetPages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper" >
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>User Target Pages</h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                                <li class="breadcrumb-item active">User Target Pages</li>
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
                                    <!-- /.form-group -->
                                    <div class="form-group">
                                        
                                        <label for="txtRoute">Route</label>
                                        <asp:TextBox runat="server" ID="txtRoute" CssClass="" TextMode="SingleLine" TabIndex="3"  />
                                        <asp:RequiredFieldValidator ID="txtRouteValidatorRequired" Display="Dynamic"  
                                            ControlToValidate="txtRoute" runat="server" 
                                            ErrorMessage="Please Add a Route" ValidationGroup="ModelAdd">

                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /.form-group -->

                                    
                                </div>
                                <!-- /.col -->
                                <div class="col-md-6">
                                   <div class="form-group">

                                    <%--
                                        ʘԈΛ
                                        --%>
                                    <asp:CheckBox ID="checkboxIsNotVisibleForMenu" CssClass="" runat="server" TextAlign="Right" Text="ʘ IsNotVisibleForMenu " />
                                       <asp:CheckBox ID="checkboxIsRoot" CssClass="" runat="server" TextAlign="Right" Text="Ԉ IsRoot " />
                                       <asp:CheckBox ID="checkboxIsIsAgrupation" CssClass="" runat="server" TextAlign="Right" Text="Λ IsAgrupation " />
                                    
                                </div>
                                    <!-- /.form-group -->
                                    <div class="form-group">

                                    <label for="DropDownListTargetOf">Member Of</label>
                                    

                                    <asp:DropDownList ID="DropDownListTargetOf" runat="server" CssClass="" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListTargetOf"
                                        DataTextField="description" DataValueField="uid">
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:SqlDataSource
                                        ID="SqlDataSourceListTargetOf" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="
                                        
                                        select distinct ut.UId, ut.Description

                                        from UserTarget ut
                                        where ut.targetof is null 
                                        or (ut.isroot =1 or ut.isagrupation =1)
                                        
                                        "></asp:SqlDataSource>


                                </div>
                                    <!-- /.form-group -->

                                    <div class="form-group">
                                        
                                        <label for="txtIconClass">Icon Class</label>
                                        <asp:TextBox runat="server" ID="txtIconClass" CssClass="" TextMode="SingleLine" TabIndex="3"  />
                                        


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
                                        DataKeyNames="UId"
                                        AutoGenerateColumns="false" 
                                        DataSourceID="UserTarget" 
                                         OnRowUpdated="gvModel_RowUpdated"
                                         
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
                                                    <asp:Label ID="lblNameItem" runat="server" Text='<%# Bind("displayName") %>'></asp:Label>
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

                                            <asp:TemplateField HeaderText="Route" SortExpression="Route">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRouteItem" runat="server" Text='<%# Bind("displayRoute") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRouteEdit" CssClass="" runat="server" Text='<%# Bind("Route") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Questions">
                                                <%--
                                        ʘԈΛ
                                        --%>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkboxIsNotVisibleForMenuItem" runat="server" Enabled="false" Checked='<%# Bind("NotVisibleForMenu") %>' TextAlign="Right" Text="ʘ" ToolTip="Is Not Visible For Menu" />
                                                <asp:CheckBox ID="checkboxIsRootItem" CssClass="" Enabled="false" Checked='<%# Bind("IsRoot") %>' runat="server" TextAlign="Right" Text="Ԉ" ToolTip="Is Root" />
                                                <asp:CheckBox ID="checkboxIsIsAgrupationItem" CssClass="" Enabled="false" Checked='<%# Bind("IsAgrupation") %>' runat="server" TextAlign="Right" Text="Λ" ToolTip="Is Agrupation" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="checkboxIsNotVisibleForMenuEdit" runat="server" TextAlign="Right" Checked='<%# Bind("NotVisibleForMenu") %>' Text="ʘ" ToolTip="Is Not Visible For Menu" />
                                               <asp:CheckBox ID="checkboxIsRootEdit" CssClass=""  Checked='<%# Bind("IsRoot") %>' runat="server" TextAlign="Right" Text="Ԉ" ToolTip="Is Root" />
                                                <asp:CheckBox ID="checkboxIsIsAgrupationEdit" CssClass=""  Checked='<%# Bind("IsAgrupation") %>' runat="server" TextAlign="Right" Text="Λ" ToolTip="Is Agrupation" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Member Of" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblTargetOfItem" runat="server" Text='<%# Bind("TargetOfDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                

                                                 <asp:DropDownList ID="DropDownListTargetOfEdit" runat="server" TabIndex="2" AppendDataBoundItems="true" DataSourceID="SqlDataSourceListTargetOf"
                                        DataTextField="description" DataValueField="uid"  SelectedValue='<%# Bind("TargetOfID") %>'>
                                        <asp:ListItem Value="">None</asp:ListItem>
                                    </asp:DropDownList>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Icon Class" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIconClassItem" runat="server" Text='<%# Bind("IConClass") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtIconClassEdit" CssClass="" runat="server" Text='<%# Bind("IConClass") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField  InsertVisible="true" ButtonType="Button" HeaderText ="Actions" ShowEditButton="true" ShowDeleteButton="true"  />
                                        </Columns>

                                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                                    </asp:GridView>

                                    <asp:SqlDataSource ID="UserTarget" runat="server" 
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        OldValuesParameterFormatString="original_{0}"
                        UpdateCommand="UPDATE [UserTarget] SET [Name] = @Name, [Description] = @Description, [Route] = @Route, [TargetOF] = @TargetOF, [NotVisibleForMenu] = @NotVisibleForMenu  WHERE [UID] = @original_UID"

                        SelectCommand="
                                        select 

                                        ROW_NUMBER() over (order by ut.orderline ) as rowNumber,    
                                        ut.UId, 
                                        ut.name, dbo.DisplayGridText(ut.Name) displayName,  
                                        ut.description, 
                                        ut.Route, dbo.DisplayGridText(ut.Route) displayRoute, 
                                        targetof as targetofId, ut.NotVisibleForMenu,isRoot, isAgrupation, iconClass,
                                        targetofDescription = (select top 1 utt.Description from usertarget utt where utt.UId = ut.TargetOf)
                                        from UserTarget ut 
      
                                        
                                        ">

                                        <UpdateParameters>
                            <asp:Parameter Name="name" Type="String" />
                            <asp:Parameter Name="description" Type="String" />
                            <asp:Parameter Name="route" Type="String" />
                            <asp:Parameter Name="NotVisibleForMenu" Type="Boolean" />
                            <asp:Parameter Name="original_UID" Type="Object"  />                            
                            <asp:Parameter Name="TargetOF" />
                        </UpdateParameters>
                       
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
