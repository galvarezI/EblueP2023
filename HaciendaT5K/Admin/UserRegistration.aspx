<%@ Page Title="Estación Experimental Agrícola - User Registration" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="Eblue.Admin.UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="content-wrapper" style="min-height: inherit !important">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>User Registration</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="<%= this.ResolveClientUrl("~/Home.aspx") %>">Home</a></li>
                            <li class="breadcrumb-item active">User Registration</li>
                        </ol>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="error-page">
                <h2 class="headline text-info">
                    <img style="height: 64px;width: 64px;"  src="<%= this.ResolveClientUrl("~/dist/img/EBlogo.png") %>"  class="brand-image img-circle elevation-3" style="opacity: .8">

                </h2>

                <div class="error-content">
                    <h3><i class="fa fa-info text-info"></i>Register your EEA account</h3>

                    <div class="form-group has-feedback">
                            <asp:Label ID="EmailLabel" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                            <span class="fa fa-envelope form-control-feedback"></span>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail"
                                runat="server" ControlToValidate="Email"
                                ErrorMessage="This is not the correct E-mail Format"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
          </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                                ErrorMessage="Required Field"
                                CssClass="text-danger"
                                ControlToValidate="Email">
          </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:Label ID="Labelpassword" runat="server" Text="Password"></asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <span class="fa fa-lock form-control-feedback"></span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword"
                                runat="server"
                                ErrorMessage="Required Field"
                                ControlToValidate="Password">
         </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:Label ID="Labelpasswordconf" runat="server" Text="Password confirmation"></asp:Label>
                            <asp:TextBox ID="passwordconfig" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <span class="fa fa-lock form-control-feedback"></span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server"
                                ErrorMessage="Required Field"
                                ControlToValidate="passwordconfig">
          </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1"
                                runat="server"
                                ErrorMessage="Password does not match" ControlToCompare="Password"
                                ControlToValidate="passwordconfig">
          </asp:CompareValidator>
                        </div>
                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="ButtonRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-block btn-flat" OnClick="ButtonRegister_Click" />
                            </div>
                            <!-- /.col -->
                            <div class="col-md-offset-2 col-md-10">
                                <p class="text-danger">
                                    <asp:Literal runat="server" ID="ErrorMessage" />
                                </p>
                            </div>
                        </div>

                </div>
            </div>
            <!-- /.error-page -->

        </section>
        <!-- /.content -->
    </div>
    
</asp:Content>
