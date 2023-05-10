<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HaciendaT5K.WebForm1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Estación Experimental Agrícola | Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style 
      <link rel="stylesheet" href="~/dist/css/adminlte.min-Copy.css">
      <link rel="stylesheet" href="~/dist/css/adminlte.min.css">
      -->
    <link rel="stylesheet" href="~/dist/css/adminlte.min-Copy.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="~/plugins/iCheck/square/blue.css">
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <div class="login-logo" style="text-align: left">

            <a href="#">

                <b>Login</b>UPRM
       
                <%--
            "../dist/img/EBlogo.png" 
            "<%= this.ResolveClientUrl("~/dist/img/EBlogo.png") %>"
        --%>
               <%--  <img src="<%= this.ResolveClientUrl("~/dist/img/EBlogo.png") %>" alt="Estación Experimental Agrícola Logo" width="200px" height="200px" class="brand-image img-circle elevation-3" style="opacity: .8">
            </a>
                   --%>

        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">Sign in to start your session</p>

            <form id="FormLogin" runat="server">
                <asp:SqlDataSource
                    ID="SqlDataSourceProjectWorkFlow" runat="server"
                    ConnectionString="<%$ ConnectionStrings:eblueConnectionString %>"
                    SelectCommand="
                                        
                                        select top 1
                                        wf.Uid, wf.Name, wf.Description
                                        from WorkFlow wf
                                        where wf.isforproject = 1 and wf.IsDefault =1
                                        
                                        "></asp:SqlDataSource>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">@</span>
                    </div>
                    <%--<input type="text" class="form-control" placeholder="Username">--%>
                    <asp:TextBox ID="Email" CssClass="form-control" runat="server" type="email" placeholder="Username"></asp:TextBox>
                </div>

                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text">**</span>
                    </div>
                    <%--<input type="text" class="form-control" placeholder="Username">--%>
                    <asp:TextBox ID="Password" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                </div>
                <div class="form-group has-feedback">


                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">


                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">

                    <!-- /.col -->
                    <div class="col-xs-4">

                        <asp:Button ID="SignIn" runat="server" Text="Sign In" CssClass="btn btn-primary btn-block btn-flat" OnClick="SignIn_Click" />

                    </div>
                    <!-- /.col -->
                </div>
            </form>


        </div>
        <!-- /.login-box-body -->
    </div>

    <!-- jQuery 
    <script src="../../plugins/jquery/jquery.min-Copy.js"></script>
    -->
    <script src="<%= this.ResolveClientUrl("~/plugins/jquery/jquery.min-Copy.js") %>">