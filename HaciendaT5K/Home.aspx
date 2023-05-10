<%@ Title="Estación Experimental Agrícola - Home" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Eblue.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper" >
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1><asp:Label ID="labelTitle" runat="server" Text="Home"></asp:Label></h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <%--<li class="breadcrumb-item"><a href="#">Home</a></li>--%>
              <li class="breadcrumb-item active"><asp:Label ID="lblCaption" runat="server" Text="Home"></asp:Label></li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-12">
            <div class="callout callout-info">
              <h5><i class="fa fa-info"></i> Note:</h5>
              This page has been enhanced for help. comming soon.
            </div>


            <!-- Main content -->
            
            <!-- /.invoice -->
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
  </div>
</asp:Content>
