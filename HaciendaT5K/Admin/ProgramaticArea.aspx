<%@ Page Title="Estación Experimental Agrícola - Programatic Areas' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind="ProgramaticArea.aspx.cs" Inherits="HaciendaT5K.Admin.ProgramAreaAdmin" MaintainScrollPositionOnPostback='True' %>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1>Programatic Areas</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class='breadcrumb-item active'>Programatic Areas</li>
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
                <asp:TextBox runat="server" ID="TextBoxProgram" CssClass="form-control" TextMode="SingleLine" TabIndex="1" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRosterName" ControlToValidate="TextBoxProgram" runat="server" CssClass="text-danger" ErrorMessage="Please Add Name" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
                     </div>
                            </div>
                            <!-- /.col -->
                            <div class='col-md-6'>
                                
                                <div class="form-group">
                                    
<asp:Label runat="server" CssClass="form-control-lg" ID="labelRoster">Coordinator: </asp:Label>
                <asp:DropDownList TabIndex="1" ID="DropDownListRoster"  AppendDataBoundItems="true" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceRoster" DataTextField="RosterName" 
                    DataValueField="RosterId">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceRoster" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
                    SelectCommand="select u.RosterId, u.RosterName from Roster u"
                    ></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRoster" ControlToValidate="DropDownListRoster" runat="server" CssClass="text-danger" ErrorMessage="Please Add a Coordinator" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
            
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
                        AutoGenerateColumns='false' DataKeyNames='ProgramAreaID'
                        DataSourceID='ProgramArea' 
                        CssClass='table-bordered table table-hover table-striped'
                     
                        
                        >
                    
                        <Columns>
                           <asp:BoundField DataField="ProgramAreaId" ReadOnly="true" HeaderText="Id" />
                           <asp:BoundField DataField="ProgramAreaName" HeaderText="Name" />
                           <asp:TemplateField HeaderText="Coordinator">
                                <EditItemTemplate>
                                   
                                    <asp:DropDownList  AppendDataBoundItems="true" ID="DropDownListRosterEdition" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceRoster" DataTextField="RosterName" 
                    DataValueField="RosterId"
                                        SelectedValue='<%# Bind("RosterProgramaticCoordinatorId") %>'
                                        >
                                        <asp:ListItem Value="">un set</asp:ListItem>
                    
                </asp:DropDownList>
                             
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RosterName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="Actions"
                                ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID='ProgramArea' runat='server'
                   
                        ConnectionString='<%$ ConnectionStrings:eblueConnectionString %>'
                        
                        OldValuesParameterFormatString='original_{0}'
                        SelectCommand= 'Select 
                        mdl.* ,
                        RosterName = (select top 1 r.RosterName from Roster r where r.RosterId = mdl.RosterProgramaticCoordinatorId)
                        from ProgramArea mdl '
                        UpdateCommand= 'update ProgramArea set ProgramAreaName = null where 1 = 0 '
                        DeleteCommand= 'delete from ProgramArea where 1 = 0'
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

