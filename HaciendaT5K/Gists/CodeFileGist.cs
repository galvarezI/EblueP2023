


namespace CaretNamespace
	{

    #region list-base

    /*
	 * 
	 * <model-identifier>
	 * <model-sqldatasource-identifier>
	 * <model-sqldatasource-select-command>
	 * <model-sqldatasource-select-command-parameters>
	 * <model-sqldatasource-update-command>
	 * <model-sqldatasource-update-command-parameters>
	 * <model-sqldatasource-insert-command>
	 * <model-sqldatasource-insert-command-parameters>
	 * <model-sqldatasource-delete-command>
	 * <model-sqldatasource-delete-command-parameters>
	 <%@ Page Title="Estación Experimental Agrícola - <model-identifier>s' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind='<model-identifier>.aspx.cs' Inherits='HaciendaT5K.Admin.<model-identifier>Admin' MaintainScrollPositionOnPostback='True' %>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1><model-identifier>s</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                            <li class='breadcrumb-item active'><model-identifier>s</li>
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
                               
                            </div>
                            <!-- /.col -->
                            <div class="col-md-6">
                                
                                
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
                                
<asp:GridView TabIndex="9" ID="gvModel" 
                        runat="server"
                        AutoGenerateColumns="false" DataKeyNames="RosterID"
                        DataSourceID="<model-sqldatasource-identifier>" 
                        CssClass="table-bordered table table-hover table-striped"
                        OnRowCommand="GridView_RowCommand" 
                        OnRowDeleted="GridView_RowDeleted"
                        
                        >
                    
                        <Columns>
                           
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID="<model-sqldatasource-identifier>" runat="server"
                        ConflictDetection="CompareAllValues"
                        ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                        DeleteCommand= "<model-sqldatasource-delete-command>"
                        InsertCommand="<model-sqldatasource-insert-command>"
                        OldValuesParameterFormatString="original_{0}"
                        SelectCommand= "<model-sqldatasource-select-command>"
                        UpdateCommand="<model-sqldatasource-update-command>"
                        <DeleteParameters>
                            <model-sqldatasource-delete-command-parameters>
                        </DeleteParameters>
                        <UpdateParameters>
                            <model-sqldatasource-update-command-parameters>
                        </UpdateParameters>
                        <InsertParameters>
                            <model-sqldatasource-insert-command-parameters>
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
	 */
    #endregion

    /*
	 * 
	 * <model-identifier>
	 * <double-quote-char>
	 * <start-binding-token>
	 * <end-binding-token>
	 * <model-sqldatasource-identifier>
	 * <model-sqldatasource-select-command>
	 * <model-sqldatasource-select-command-parameters>
	 * <model-sqldatasource-update-command>
	 * <model-sqldatasource-update-command-parameters>
	 * <model-sqldatasource-insert-command>
	 * <model-sqldatasource-insert-command-parameters>
	 * <model-sqldatasource-delete-command>
	 * <model-sqldatasource-delete-command-parameters>
	 <start-binding-token>@ Page Title="Estación Experimental Agrícola - <model-identifier>s' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind='<model-identifier>.aspx.cs' Inherits='HaciendaT5K.Admin.<model-identifier>Admin' MaintainScrollPositionOnPostback='True' <end-binding-token>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1><model-identifier>s</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href=<double-quote-char><start-binding-token>= this.ResolveClientUrl(<double-quote-char>~/Home.aspx<double-quote-char>) <end-binding-token><double-quote-char>>Home</a></li>
                            <li class='breadcrumb-item active'><model-identifier>s</li>
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
                               
                            </div>
                            <!-- /.col -->
                            <div class='col-md-6'>
                                
                                
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
                                
                    <asp:GridView TabIndex='9' ID='gvModel' 
                        runat='server'
                        AutoGenerateColumns='false' DataKeyNames='<model-identifier>ID'
                        DataSourceID='<model-sqldatasource-identifier>' 
                        CssClass='table-bordered table table-hover table-striped'
                     
                        
                        >
                    
                        <Columns>
                           
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID='<model-sqldatasource-identifier>' runat='server'
                        ConflictDetection='CompareAllValues'
                        ConnectionString='<start-binding-token>$ ConnectionStrings:eblueConnectionString <end-binding-token>'
                        DeleteCommand= '<model-sqldatasource-delete-command>'
                        InsertCommand='<model-sqldatasource-insert-command>'
                        OldValuesParameterFormatString='original_{0}'
                        SelectCommand= '<model-sqldatasource-select-command>'
                        UpdateCommand='<model-sqldatasource-update-command>'
                        <DeleteParameters>
                            <model-sqldatasource-delete-command-parameters>
                        </DeleteParameters>
                        <UpdateParameters>
                            <model-sqldatasource-update-command-parameters>
                        </UpdateParameters>
                        <InsertParameters>
                            <model-sqldatasource-insert-command-parameters>
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
	 */
    public class temp {
        public temp()
        {

            /*
             * <model-identifier>
	 * <double-quote-char>
	 * <start-binding-token>
	 * <end-binding-token>
	 * <model-sqldatasource-identifier>
	 * <model-sqldatasource-select-command>
	 * <model-sqldatasource-select-command-parameters>
	 * <model-sqldatasource-update-command>
	 * <model-sqldatasource-update-command-parameters>
	 * <model-sqldatasource-insert-command>
	 * <model-sqldatasource-insert-command-parameters>
	 * <model-sqldatasource-delete-command>
	 * <model-sqldatasource-delete-command-parameters>
             */

            var listGist = @"

<start-binding-token>@ Page Title='Estación Experimental Agrícola - <model-identifier>s' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind='<model-identifier>.aspx.cs' Inherits='HaciendaT5K.Admin.<model-identifier>Admin' MaintainScrollPositionOnPostback='True' <end-binding-token>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1><model-identifier>s</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href=<double-quote-char><start-binding-token>= this.ResolveClientUrl(<double-quote-char>~/Home.aspx<double-quote-char>) <end-binding-token><double-quote-char>>Home</a></li>
                            <li class='breadcrumb-item active'><model-identifier>s</li>
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
                               
                            </div>
                            <!-- /.col -->
                            <div class='col-md-6'>
                                
                                
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
                                
                    <asp:GridView TabIndex='9' ID='gvModel' 
                        runat='server'
                        AutoGenerateColumns='false' DataKeyNames='<model-identifier>ID'
                        DataSourceID='<model-sqldatasource-identifier>' 
                        CssClass='table-bordered table table-hover table-striped'
                     
                        
                        >
                    
                        <Columns>
                           
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID='<model-sqldatasource-identifier>' runat='server'
                        ConflictDetection='CompareAllValues'
                        ConnectionString='<start-binding-token>$ ConnectionStrings:eblueConnectionString <end-binding-token>'
                        
                        OldValuesParameterFormatString='original_{0}'
                        SelectCommand= '<model-sqldatasource-select-command>' >
                       
                       
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

";

            var a= "ProgramArea";
            var b= "\"";
            var c= "<%";
            var d= "%>";
            var e = "ProgramArea";
            var f = "Select * from ProgramArea";
            var g = string.Empty;
            var h = string.Empty;
            var i = string.Empty;
            var j = string.Empty;
            var k = string.Empty;
            var l = string.Empty;
            var m = string.Empty;

            listGist = listGist.Replace("<model-identifier>", a);
            listGist = listGist.Replace("<double-quote-char>", b);
            listGist = listGist.Replace("<start-binding-token>", c);
            listGist = listGist.Replace("<end-binding-token>", d);
            listGist = listGist.Replace("<model-sqldatasource-identifier>", e);
            listGist = listGist.Replace("<model-sqldatasource-select-command>", f);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
        }
	}

    public class Gists
    {
        public static string listView()
        {

            /*
             * <model-identifier>
	 * <double-quote-char>
	 * <start-binding-token>
	 * <end-binding-token>
	 * <model-sqldatasource-identifier>
	 * <model-sqldatasource-select-command>
	 * <model-sqldatasource-select-command-parameters>
	 * <model-sqldatasource-update-command>
	 * <model-sqldatasource-update-command-parameters>
	 * <model-sqldatasource-insert-command>
	 * <model-sqldatasource-insert-command-parameters>
	 * <model-sqldatasource-delete-command>
	 * <model-sqldatasource-delete-command-parameters>
             */

            var listGist = @"

<start-binding-token>@ Page Title='Estación Experimental Agrícola - <model-identifier>s' Language='C#' MasterPageFile='~/General.Master' AutoEventWireup='true' CodeBehind='<model-identifier>.aspx.cs' Inherits='HaciendaT5K.Admin.<model-identifier>Admin' MaintainScrollPositionOnPostback='True' <end-binding-token>

<asp:Content ID='Content1' ContentPlaceHolderID='head' runat='server'>
</asp:Content>
<asp:Content ID='Content2' ContentPlaceHolderID='ContentPlaceHolder1' runat='server'>


	<div class='content-wrapper' style='min-height: 568px;'>
        <!-- Content Header (Page header) -->
        <section class='content-header'> 
            <div class='container-fluid'>
                <div class='row mb-2'>
                    <div class='col-sm-6'>
                        <h1><model-identifier>s</h1>
                    </div>
                    <div class='col-sm-6'>
                        <ol class='breadcrumb float-sm-right'>
                            <li class='breadcrumb-item'><a href=<double-quote-char><start-binding-token>= this.ResolveClientUrl(<double-quote-char>~/Home.aspx<double-quote-char>) <end-binding-token><double-quote-char>>Home</a></li>
                            <li class='breadcrumb-item active'><model-identifier>s</li>
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
                               
                            </div>
                            <!-- /.col -->
                            <div class='col-md-6'>
                                
                                
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
                                
                    <asp:GridView TabIndex='9' ID='gvModel' 
                        runat='server'
                        AutoGenerateColumns='false' DataKeyNames='<model-identifier>ID'
                        DataSourceID='<model-sqldatasource-identifier>' 
                        CssClass='table-bordered table table-hover table-striped'
                     
                        
                        >
                    
                        <Columns>
                           
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found!
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:SqlDataSource ID='<model-sqldatasource-identifier>' runat='server'
                        ConflictDetection='CompareAllValues'
                        ConnectionString='<start-binding-token>$ ConnectionStrings:eblueConnectionString <end-binding-token>'
                        
                        OldValuesParameterFormatString='original_{0}'
                        SelectCommand= '<model-sqldatasource-select-command>' >
                       
                       
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

";

            //var a = "ProgramArea";
            //var b = "\"";
            //var c = "<%";
            //var d = "%>";
            //var e = "ProgramArea";
            //var f = "Select * from ProgramArea";
            var a = "Department";
            var b = "\"";
            var c = "<%";
            var d = "%>";
            var e = "Department";
            var f = "Select * from Department";
            var g = string.Empty;
            var h = string.Empty;
            var i = string.Empty;
            var j = string.Empty;
            var k = string.Empty;
            var l = string.Empty;
            var m = string.Empty;

            listGist = listGist.Replace("<model-identifier>", a);
            listGist = listGist.Replace("<double-quote-char>", b);
            listGist = listGist.Replace("<start-binding-token>", c);
            listGist = listGist.Replace("<end-binding-token>", d);
            listGist = listGist.Replace("<model-sqldatasource-identifier>", e);
            listGist = listGist.Replace("<model-sqldatasource-select-command>", f);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);
            //listGist = listGist.Replace("<model-sqldatasource-select-command>", a);

            return listGist;
        }


        
    }
}