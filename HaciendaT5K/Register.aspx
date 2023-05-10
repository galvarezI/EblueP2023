<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HaciendaT5K.Registrarte" %>

<!DOCTYPE html> 
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>E-blue | Register</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="~/dist/css/adminlte.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="~/plugins/iCheck/square/blue.css">
    <!-- Google Font: Source Sans Pro -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet">
</head>
<body class="hold-transition register-page">
    <form id="form1" runat="server">
        <div class="register-box">
            <div class="register-logo">
                <asp:Image ID="Logoimg" CssClass="img-fluid" runat="server" ImageUrl="~/dist/img/logo-1_2_0.jpg"></asp:Image>
            </div>

            <div class="card">
                <div class="card-body register-card-body">
                    <p class="login-box-msg">Register your E-blue account</p>

                    <form>
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
                    </form>
                </div>
                <!-- /.form-box -->
            </div>
            <!-- /.card -->
        </div>
        <!-- /.register-box -->

        <!-- jQuery -->
        <script src="../../plugins/jquery/jquery.min.js"></script>
        <!-- Bootstrap 4 -->
        <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <!-- iCheck -->
        <script src="../../plugins/iCheck/icheck.min.js"></script>
        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                })
            })
</script>
    </form>
</body>
</html>
