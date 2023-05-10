<%@ Page Title="Estación Experimental Agrícola - Error Page" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="Eblue.ErrorPages.Oops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper" style="min-height: inherit !important">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>500 Error Page</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">500 Error Page</li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="error-page">
                <h2 class="headline text-danger">500</h2>

                <div class="error-content">
                    <h3><i class="fa fa-warning text-danger"></i>Oops! Something went wrong.</h3>

                    <p>
<%--                        An unexpected error occurred on our website. The website administrator has been notified.
        <br />--%>
                        <asp:Label ID="frienlymessage" runat="server" BackColor="DodgerBlue" ForeColor="White"></asp:Label>
                    </p>
                    <p>
                        We will work on fixing that right away.
            Meanwhile, you may <a href="<%= this.ResolveClientUrl("~/project/whichiparticipate.aspx") %>">return to home page</a>
                    </p>


                </div>
            </div>
            <!-- /.error-page -->

        </section>
        <!-- /.content -->
    </div>


</asp:Content>
