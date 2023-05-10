<%@ Page Title="Estación Experimental Agrícola - Departments' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind='Department.aspx.cs' Inherits='Eblue.Admin.DepartmentAdmin' MaintainScrollPositionOnPostback='True' %>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1>Departments</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class='breadcrumb-item active'>Departments</li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>


     <!-- Main content -->
        <section class='content'>
            <div class='container-fluid'>
                <!-- SELECT2 EXAMPLE -->
                <div class='card card-default'>
                    <div class='card-header'>
                        <h3 class='card-title'>Data Info</h3>

                        <div class='card-tools'>
                            <button type='button' class='btn btn-tool' data-widget='collapse'><i class='fa fa-minus'></i></button>
                            
                        </div>
                    </div>
                    <!-- /.card-header -->
                    <div class='card-body'>
                        <div class='row'>
                            <div class='col-md-6'>
                                <div class="form-group">
                                    <asp:Label ID="LabelName" CssClass="form-control-lg" runat="server" Text="Name:"></asp:Label>
                                    <asp:TextBox runat="server" ID="TextBoxName" CssClass="form-control" TextMode="SingleLine" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRosterName" ControlToValidate="TextBoxName" runat="server" CssClass="text-danger" ErrorMessage="Please Add Name" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label1" CssClass="form-control-lg" runat="server" Text="Code:"></asp:Label>
                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBoxCode" CssClass="form-control" TextMode="SingleLine" TabIndex="1" />
                                 </div>
                            </div>
                            <!-- /.col -->
                            <div class='col-md-6'>

                                <div class="form-group">

                                    <asp:Label runat="server" CssClass="form-control-lg" ID="labelRoster">Director: </asp:Label>
                                    <asp:DropDownList TabIndex="1" ID="DropDownListRoster" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                        DataSourceID="SqlDataSourceRoster" DataTextField="RosterName"
                                        DataValueField="RosterId">
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceRoster" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="select u.RosterId, u.RosterName from Roster u"></asp:SqlDataSource>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRoster" ControlToValidate="DropDownListRoster" runat="server" CssClass="text-danger" ErrorMessage="Please Add a Director" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group">

                                    <asp:Label runat="server" CssClass="form-control-lg" ID="label2">DepartmentOf: </asp:Label>
                                    <asp:DropDownList TabIndex="1" ID="DropDownListDepartmentOf" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                        DataSourceID="SqlDataSourceDepartment" DataTextField="Description"
                                        DataValueField="Id">
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="select u.DepartmentId Id, u.DepartmentName Description from Department u"></asp:SqlDataSource>
                                    
                                </div>

                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /.card-body -->
                    <div class='card-footer'>
                        <asp:Button TabIndex='6' ID='buttonNewModel' ValidationGroup='ModelAdd' OnClick='ButtonNewModel_Click' CssClass='btn btn-primary' runat='server' Text='Add' />
                       

                        <asp:Button TabIndex='7' ID='buttonClearModel' OnClick='ButtonClearModel_Click' CssClass='btn btn-secondary float-right' runat='server' Text='Clear' />
                     
                    </div>

                    
                </div>
                <!-- /.row -->
                <div class='row'>
                    <div class='col-12'>
                        <div class='card'>
                            <div class='card-header'>
                                <h3 class='card-title'>Data List</h3>


                            </div>
                            <!-- /.card-header -->
                            <div class='card-body'>
                                
                    <asp:GridView TabIndex='9' ID='gv' 
                        runat='server'
                        AutoGenerateColumns='false' DataKeyNames='DepartmentID'
                        DataSourceID='Department' 
                        CssClass='table-bordered table table-hover table-striped'
                     
                        
                        >
                    
                        <Columns>
                           <asp:BoundField DataField="DepartmentId" ReadOnly="true" HeaderText="Id" />
                           <asp:BoundField DataField="DepartmentName" HeaderText="Name" />
                            <asp:BoundField DataField="DepartmentCode" HeaderText="Code" />
                           <asp:TemplateField HeaderText="Director">
                                <EditItemTemplate>

                                    <asp:DropDownList ID="DropDownListRosterEdition" runat="server" CssClass="form-control"
                                        DataSourceID="SqlDataSourceRoster" DataTextField="RosterName" AppendDataBoundItems="true"
                                        DataValueField="RosterId"
                                        SelectedValue='<%# Bind("RosterDepartmentDirectorId") %>'>
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRosterEdition" ControlToValidate="DropDownListRosterEdition" 
                                        runat="server" CssClass="text-danger" ErrorMessage="Please Add a Director" ValidationGroup="ModelGridEdit"></asp:RequiredFieldValidator>


                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelRosterItem" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DepartmentOf">
                                <EditItemTemplate>
                                   
                                    <asp:DropDownList  ID="DropDownListDepartmentEdition" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceDepartment" DataTextField="Description" 
                    DataValueField="Id"
                                        SelectedValue='<%# Bind("DepartmentOf") %>' AppendDataBoundItems="true"
                                        >
                                        <asp:ListItem  Value="0" Text="" />
                </asp:DropDownList>
                             
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelDepartmentOfItem" runat="server" Text='<%# Bind("DepartmentOfName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="Actions"  ValidationGroup="ModelGridEdit"
                                ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID='Department' runat='server'
                        ConflictDetection='CompareAllValues'
                        ConnectionString='<%$ ConnectionStrings:eblueConnectionString %>'
                        
                        OldValuesParameterFormatString='original_{0}'
                        SelectCommand= 'Select 
                        mdl.* ,
                        RosterName = (select top 1 r.RosterName from Roster r where r.RosterId = mdl.RosterDepartmentDirectorId),
                        DepartmentOfName = (select top 1 r.DepartmentName from Department r where r.DepartmentId = mdl.DepartmentOf)
                        from Department mdl '
                        UpdateCommand= 'update Department set DepartmentName = null where 1 = 0 '
                        DeleteCommand= 'delete from Department where 1 = 0'
                        
                        >
                       
                       
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

