<%@ Page Title="Estación Experimental Agrícola - Rosters" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Roster.aspx.cs" Inherits="HaciendaT5K.Admin.RosterAdmin" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper" style="min-height: 568px;">
        <!-- Content Header (Page header) -->
        <section class="content-header"> 
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>Rosters</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">Rosters</li>
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

               <asp:Label ID="LabelRosterName" CssClass="form-control-lg" runat="server" Text="Roster Name:"></asp:Label>
                <asp:TextBox runat="server" ID="TextBoxRosterName" CssClass="form-control" TextMode="SingleLine" TabIndex="1" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRosterName" ControlToValidate="TextBoxRosterName" runat="server" CssClass="text-danger" ErrorMessage="Please Add a Roster Name" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
                     

                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">
                                    
<asp:Label runat="server" CssClass="form-control-lg" ID="labelUser">User: </asp:Label>
                <asp:DropDownList TabIndex="1" ID="DropDownListUser"  AppendDataBoundItems="true" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceUser" DataTextField="Email" 
                    DataValueField="UID">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" 
                    SelectCommand="select u.* from users u
                    left join roster r on r.UID = u.UID
                    where 
                    u.EmailVerified = 1 and
                    r.UID is null"
                    ></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser" ControlToValidate="DropDownListUser" runat="server" CssClass="text-danger" ErrorMessage="Please Add a User" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
            
                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">
                                    
<asp:Label runat="server" CssClass="form-control-lg" ID="label3">User Type: </asp:Label>
                <asp:DropDownList TabIndex="2" ID="DropDownListUserType"  AppendDataBoundItems="true" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceUserType" DataTextField="Description"
                    DataValueField="UID">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUserType" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT Uid, Description FROM [rosterCategory] order by orderline desc"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DropDownListUserType" runat="server" CssClass="text-danger" ErrorMessage="Please Select a User Type" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
            

                                </div>
                                <!-- /.form-group -->
                                <div class="form-group">
<asp:Label ID="LabelDepartment" CssClass="form-control-lg" runat="server" Text="Deparment:"></asp:Label>
                <asp:DropDownList TabIndex="3" ID="DropDownListDepartment"  runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceDeparment1" DataTextField="DepartmentName"
                    DataValueField="DepartmentID" AppendDataBoundItems="true">
                    <asp:ListItem ></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="DropDownListDepartment" runat="server" CssClass="text-danger" ErrorMessage="Please Select a Deparment" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
                <asp:SqlDataSource ID="SqlDataSourceDeparment1" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Department]"></asp:SqlDataSource>
            
                                   
                                </div>

                                            <div class="form-group">
                                    
<asp:Label runat="server" CssClass="form-control-lg" ID="label7">Role Type: </asp:Label>
                <asp:DropDownList TabIndex="2" ID="DropDownListRoleType"  AppendDataBoundItems="true" runat="server" CssClass="form-control"
                    DataSourceID="SqlDataSourceRoleType" DataTextField="Description"
                    DataValueField="UID">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceRoleType" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT Uid, Name, Description FROM [RoleCategory] order by orderline desc"></asp:SqlDataSource>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="DropDownListRoleType" runat="server" CssClass="text-danger" ErrorMessage="Please Select a Role Type" ValidationGroup="ModelAdd"></asp:RequiredFieldValidator>
            

                                </div>
                                <!-- /.form-group -->
                            </div>
                            <!-- /.col -->
                            <div class="col-md-6">
                                
                                <div class="form-group">
                                                    <asp:Label ID="LabelLocation" CssClass="form-control-lg" runat="server" Text="Location:"></asp:Label>
                <asp:DropDownList TabIndex="4" ID="cmbLocation" CssClass="form-control" runat="server" DataSourceID="SqlDataSourceEblue"
                    DataTextField="LocationName" DataValueField="LocationID" AutoPostBack="True" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceEblue" runat="server" ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>" SelectCommand="SELECT * FROM [Location]"></asp:SqlDataSource>

                                </div>
                                <!-- /.form-group -->
                                
                                <div class="form-group">
                                   <asp:Label ID="LabeCanBePI" CssClass="form-control-lg" runat="server" Text="Can Be Principal Investigator:"></asp:Label>
                <asp:DropDownList TabIndex="5" ID="DropDownListCanBePI" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text=" "></asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList> 

                                </div>
                                <!-- /.form-group -->

                                <div class="form-group">
                                   <asp:Label ID="Label5" CssClass="form-control-lg" runat="server" Text="Signature:"></asp:Label>
                <asp:FileUpload TabIndex="6" ID="FileSignature" CssClass="form-control" runat="server" AllowMultiple="false" accept="image/*" />
                <asp:Image TabIndex="-1" ID="ImageSignature" runat="server" CssClass="form-control" />
                <asp:HiddenField ID="SignatureValue" runat="server" />
                <asp:Label TabIndex="-1" runat="server" ID="SignatureStatusLabel" Text="" />
<asp:Button TabIndex="7" runat="server" ID="UploadButtonSignature" Text="Load Signature" CssClass="btn btn-file" OnClick="UploadButton_Click" /> 

                                </div>
                                <!-- /.form-group -->
				<div class="form-group">
                                    
<asp:Label ID="Label6" CssClass="form-control-lg" runat="server" Text="Picture:"></asp:Label>
                <asp:FileUpload TabIndex="6" ID="FilePicture" CssClass="form-control" runat="server" AllowMultiple="false" accept="image/*" />
                <asp:Image TabIndex="-1" ID="ImagePicture" runat="server" CssClass="form-control" />
                <asp:HiddenField ID="PictureValue" runat="server" />
                <asp:Label TabIndex="-1" runat="server" ID="PictureStatusLabel" Text="" />
<asp:Button TabIndex="7" runat="server" ID="UploadButtonPicture" Text="Load Picture" CssClass="btn btn-file" OnClick="UploadButtonPicture_Click" />
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
                       

                        <asp:Button TabIndex="7" ID="buttonClearModel" OnClick="ButtonClearModel_Click" CssClass="btn btn-secondary float-right" runat="server" Text="Clear" />
                     
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
                                
<asp:GridView TabIndex="9" ID="gv" 
                        runat="server"
                        AutoGenerateColumns="false" DataKeyNames="RosterID"
                        DataSourceID="SqlDataSource1" 
                        CssClass="table-bordered table table-hover table-striped"
                        OnRowCommand="GridView_RowCommand" 
                        OnRowDeleted="GridView_RowDeleted"
                        
                        >
                    
                        <Columns>
                            <asp:BoundField DataField="RosterName" HeaderText="Name" 
                                 />
                           
                            <asp:TemplateField HeaderText="User Name">
                                <EditItemTemplate>
                                    
                                    <asp:TextBox  ID="UserNameEdit" Text='<%# Bind("UserName") %>' runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblDisplayUserName" runat="server" Text='<%# Bind("DisplayUserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roster Type">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListUserTypeEdition" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceUserType" AppendDataBoundItems="true"
                                        DataTextField="Description" DataValueField="UID"
                                        SelectedValue='<%# Bind("RosterCategoryID") %>'>
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                             
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RosterCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department" >
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"
                                        DataSourceID="sdsDepartment" DataTextField="DepartmentName"
                                        DataValueField="DepartmentID" SelectedValue='<%# Bind("DepartmentID") %>' AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsDepartment" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT [DepartmentID], [DepartmentName] FROM [Department]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" >
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsLocation" CssClass="form-control"
                                        DataTextField="LocationName" DataValueField="LocationID" AppendDataBoundItems="true"
                                        SelectedValue='<%# Bind("LocationID") %>'>
                                        <asp:ListItem Value="0">None</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsLocation" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                                        SelectCommand="SELECT [LocationID], [LocationName] FROM [Location]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="CanBePI" HeaderText="Can Be PI"
                                 />

                            <asp:TemplateField HeaderText="Signature" >
                                <EditItemTemplate>
                                    <asp:FileUpload TabIndex="-1" ID="FileSignatureEdition" CssClass="form-control" runat="server" AllowMultiple="false" accept="image/*" />
                                    <asp:Image TabIndex="-1" ID="ImageSignatureEdition" runat="server" CssClass="form-control" ImageUrl='<%# Bind("Signature") %>' />
                                    <asp:Button Text="Load" runat="server" CommandName="ChangeSignatureEdition" CommandArgument="<%# Container.DataItemIndex %>" />
                                    <asp:Button Text="Remove" runat="server" CommandName="RemoveSignatureEdition" CommandArgument="<%# Container.DataItemIndex %>" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    
                                    <asp:Image TabIndex="-1" ID="ImageSignatureItem" runat="server" CssClass="form-control" ImageUrl='<%# Bind("Signature") %>' Visible='<%# Bind("IsSignatureVisible") %>' />
                                
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Picture" >
                                <EditItemTemplate>
                                    <asp:FileUpload TabIndex="-1" ID="FilePictureEdition" CssClass="form-control" runat="server" AllowMultiple="false" accept="image/*" />
                                    <asp:Image TabIndex="-1" ID="ImagePictureEdition" runat="server" CssClass="form-control" ImageUrl='<%# Bind("Picture") %>' />
                                    <asp:Button Text="Load" runat="server" CommandName="ChangePictureEdition" CommandArgument="<%# Container.DataItemIndex %>" />
                                    <asp:Button Text="Remove" runat="server" CommandName="RemovePictureEdition" CommandArgument="<%# Container.DataItemIndex %>" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    
                                    <asp:Image TabIndex="-1" ID="ImagePictureItem" runat="server" CssClass="form-control" ImageUrl='<%# Bind("Picture") %>' Visible='<%# Bind("IsPictureVisible") %>' />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Role Type">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListRoleTypeEdition" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceRoleType" AppendDataBoundItems="true"
                                        DataTextField="Description" DataValueField="UID"
                                        SelectedValue='<%# Bind("RoleCategoryId") %>'>
                                        <asp:ListItem Value="" Text=" "></asp:ListItem>
                                    </asp:DropDownList>
                             
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("RoleCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           

                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True"
                                ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand="DELETE FROM [Roster] WHERE [RosterID] = @original_RosterID"
                        InsertCommand="INSERT INTO [Roster] ([RosterID], [RosterName], [DepartmentID], [LocationID], [CanBePI]) VALUES (@RosterID, @RosterName, @DepartmentID, @LocationID, @CanBePI)"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT     RosterID, RosterName,r.UID UserID, 
                        Signature, 
                        convert(bit, IIF([signature] is null or [signature] ='' , 0, 1)) IsSignatureVisible, 
                        Picture, 
                        convert(bit, IIF([picture] is null or [picture] ='' , 0, 1)) IsPictureVisible, 
                        (SELECT top 1    Email
                            FROM          Users u
                            WHERE      (u.UID = R.UID)) AS UserName,
                        (SELECT top 1    replace( replace(Email,'@',' @'), '.', ' . ')
                            FROM          Users u
                            WHERE      (u.UID = R.UID)) AS DisplayUserName,
                        DepartmentID, LocationID, RosterCategoryID,
                          (SELECT     DepartmentName
                            FROM          Department
                            WHERE      (DepartmentID = R.DepartmentID)) AS DepartmentName,
                          (SELECT     LocationName
                            FROM          Location
                            WHERE      (LocationID = R.LocationID)) AS LocationName, 
                        (SELECT     Description
                            FROM          RosterCategory rc
                            WHERE      (rc.uid = R.RosterCategoryID)) AS RosterCategoryName, 
                        CanBePI,RoleCategoryId,
                        (SELECT top 1 Description
                        
                        FROM        RoleCategory rl
                        WHERE (rl.UId = R.RoleCategoryId)) AS RoleCategoryName

FROM         Roster R 
             LEFT JOIN RosterCategory RCC ON rcc.UId = r.rosterCategoryId
                        ORDER BY 
                        ISNULL(rcc.ORDERLINE, 0) DESC,
                        RosterName ASC"
                        UpdateCommand="UPDATE [Roster] SET [RosterName] = @RosterName, [DepartmentID] = @DepartmentID, [LocationID] = @LocationID, [CanBePI] = @CanBePI, [RosterCategoryID] = @RosterCategoryID, Signature = @Signature, Picture = @Picture,[RoleCategoryId]  = @RoleCategoryId WHERE [RosterID] = @original_RosterID AND [RosterName] = @original_RosterName AND [DepartmentID] = @original_DepartmentID AND [LocationID] = @original_LocationID AND [CanBePI] = @original_CanBePI  " >
                        <DeleteParameters>
                            <asp:Parameter Name="original_RosterID" Type="Object" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="RosterName" Type="String" />
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="LocationID" Type="Int32" />
                            <asp:Parameter Name="CanBePI" Type="Boolean" />
                            <asp:Parameter Name="original_RosterID" Type="Object" />
                            <asp:Parameter Name="original_RosterName" Type="String" />
                            <asp:Parameter Name="original_DepartmentID" Type="Int32" />
                            <asp:Parameter Name="original_LocationID" Type="Int32" />
                            <asp:Parameter Name="original_CanBePI" Type="Boolean" />
                            <asp:Parameter Name="Signature" />
                            <asp:Parameter Name="Picture" />
                            <asp:Parameter Name="RosterCategoryID" />
                            <asp:Parameter Name="RoleCategoryId" />
                            
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="RosterID" Type="Object" />
                            <asp:Parameter Name="RosterName" Type="String" />
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="LocationID" Type="Int32" />
                            <asp:Parameter Name="CanBePI" Type="Boolean" />
                        </InsertParameters>
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
