<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="COA.Cipol.Presentacion.frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=9"/>
    <title>INGRESO AL SISTEMA</title>
    <!-- jQuery -->
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <!-- jQuery ui -->
    <script type="text/javascript" src="Scripts/jquery.ui/jquery-ui-1.10.4.custom.min.js"></script>
    <link rel="stylesheet" href="Scripts/jquery.ui/css/smoothness/jquery-ui-1.10.4.custom.css"/>
    <!-- Latest compiled and minified CSS -->
        <!-- Styles -->
    
       <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <!-- Optional theme -->
    <link rel="stylesheet" href="css/bootstrap-theme.min.css"/>

    <link rel="stylesheet" href="css/styles.css"/>


   
    <!-- Latest compiled and minified JavaScript -->
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>



    <!-- html5shiv -->
        <!--[if lt IE 9]>
            <script src="Scripts/html5shiv-printshiv.js"></script>
        <![endif]-->
    <!-- respond.js -->
    <!--[if lt IE 9]>
    <script src="Scripts/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery magnific-popup -->
<%--    <script type="text/javascript" src="Scripts/jquery.magnic-popup/jquery.magnific-popup.min.js"></script>
    <link rel="stylesheet" href="Scripts/jquery.magnic-popup/magnific-popup.css">--%>
    <!-- jQuery jstree -->
    <script type="text/javascript" src="Scripts/jstree/jstree.min.js"></script>
    <link rel="stylesheet" href="Scripts/jstree/themes/default/style.css"/>
    <!-- global.js -->
    <script type="text/javascript" src="Scripts/global.js"></script>
    <!--[if IE]>
    <link rel="stylesheet" href="Estilos/ie.css">
    <![endif]-->


    <script type="text/javascript" src="Scripts/jsValidarCampos.js"></script>

    <script type="text/javascript" src="Scripts/jsCIPOL.js"></script>

    <script type="text/javascript" src="Scripts/blockUI-2.39.js"></script>

    <script type="text/javascript" src="Scripts/json2.js"></script>

    <script type="text/javascript" src="Scripts/jsfrmLogin.js"></script>
</head>
<body>
<form id="formLogin" runat="server" class="col-md-12">
    <section id="login" style="height:100%;">
    	<div class="container" style="height:100%;">
        	<div class="vertical-aligner" style="height:100%;">
                <div class="row">
                	<div class="col-md-offset-1 col-md-10">
                        <div class="row">
                            <div id="login-block" class="col-xs-offset-3 col-xs-6 block white">
                                
                                <asp:Login ID="ucLogin" runat="server" EnableViewState="false" RenderOuterTable="false"
                                    OnAuthenticate="ucLogin_Authenticate" OnLoggedIn="ucLogin_LoggedIn">
                                    <LayoutTemplate>
                            	<div class="form col-md-12">
                                    <div class="row">
                                        <div class="col-md-12" style="padding-top:20px;">
                                            <h1>LOGIN DE USUARIO</h1>
                                            <h2>Ingrese el nombre de usuario y la contraseña para acceder al sistema</h2>         
                                            <div class="form-group">
                                                <label for="user">Usuario</label>
                                                <asp:TextBox ID="UserName" runat="server" class="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                    CssClass="error" ErrorMessage="El Usuario es requerido." ToolTip="El Usuario es requerido."
                                                                    ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group">
                                                <label for="password">Contraseña</label>
                                               <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    CssClass="error" ErrorMessage="La Contraseña es requerida." ToolTip="La Contraseña es requerida."
                                                    ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row bottom">
                                        <div class="col-md-12">
                                            <div id="logo">
                                                <img src="images/logo_login.jpg" alt="CIPOL" />
                                            </div>
                                            <div id="submit">
                                                 <asp:Button ID="LoginButton" CssClass="btn green" runat="server" CommandName="Login"  Text="Ingresar" ValidationGroup="LoginUserValidationGroup" /> 
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                    <asp:Label ID="FailureText" runat="server" CssClass="alert-danger"></asp:Label>
                                    
                                    <%--<asp:ValidationSummary ID="LoginUserValidationSummary" runat="server"  CssClass="alert alert-danger"
                                            ValidationGroup="LoginUserValidationGroup" />--%>
                                </LayoutTemplate>
                                </asp:Login>
                                    <h2><asp:Label ID="lblNombreDominio" runat="server"></asp:Label></h2>
                                    <asp:HiddenField id="hdnIP" runat="server"/>
                            </div>
                        </div>
                	</div>
                </div>
            </div>

        </div>
    </section>
   </form>

</body>
</html>
