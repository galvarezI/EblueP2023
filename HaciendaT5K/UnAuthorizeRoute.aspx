<%@ Page Title="Estación Experimental Agrícola - UnAuthorize Page" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="UnAuthorizeRoute.aspx.cs" Inherits="Eblue.UnAuthorizeRoute" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content-wrapper" style="min-height: inherit !important">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>401 UnAuthorize Page</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">401 UnAuthorize Page</li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="error-page">
                <h2 class="headline text-danger">401</h2>

                <div class="error-content">
                    <h3><i class="fa fa-warning text-danger"></i>Oops! Something went wrong.</h3>

                    <p>
                        An expected error occurred on our website. The website administrator has been notified.
        <br />
                        <asp:Label ID="frienlymessage" runat="server"></asp:Label>
                    </p>
                    <p>
                        Contact your administrator to resolve this issue, you may <a href="<%= this.ResolveClientUrl("~/home.aspx") %>">return to home page</a>
                    </p>


                </div>
            </div>
            <!-- /.error-page -->

        </section>
        <!-- /.content -->
    </div>
    
</asp:Content>
