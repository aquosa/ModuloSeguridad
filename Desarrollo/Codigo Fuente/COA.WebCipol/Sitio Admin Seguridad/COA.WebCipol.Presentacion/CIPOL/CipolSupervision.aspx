<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CipolSupervision.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.CIPOL.CipolSupervision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <base target="_self" />
    <title>AUTORIZAR</title>
    <!-- jQuery -->
    <script type="text/javascript" src="../Scripts/jquery-1.10.2.min.js"></script>
    <!-- jQuery ui -->
    <script type="text/javascript" src="../Scripts/jquery.ui/jquery-ui-1.10.4.custom.min.js"></script>
    <link rel="stylesheet" href="../Scripts/jquery.ui/css/smoothness/jquery-ui-1.10.4.custom.css" />
    <!-- Latest compiled and minified CSS -->
    <!-- Styles -->
    <link rel="stylesheet" href="../Estilos/bootstrap.min.css" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="../Estilos/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="../Estilos/styles.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootbox.js"></script>
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
    <!-- global.js -->
    <script type="text/javascript" src="../Scripts/global.js"></script>
    <!--[if IE]>
    <link rel="stylesheet" href="Estilos/ie.css">
    <![endif]-->
    <script type="text/javascript" src="../Scripts/jsCIPOLPRESENTACION.js"></script>
    <script type="text/javascript" src="../Scripts/blockUI-2.39.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
    <script type="text/javascript" src="../Scripts/jsControles.js"></script>
    <script type="text/javascript" src="jsCipolSupervision.js"></script>
</head>
<body id="cuerpo" runat="server">
    <form id="Form1" runat="server">
    <div class="container">
        <div class="vertical-aligner">
            <div class="row">
                <div class="col-md-offset-1 col-md-10">
                    <div class="row">
                        <div id="login-block" class="col-xs-offset-3 col-xs-6 block white">
                            <div class="form col-md-12">
                                <div class="row">
                                    <div class="col-md-12" style="padding-top: 20px;">
                                        <h2>
                                            Ingrese la autorización</h2>
                                        <div class="form-group">
                                            <label for="user">
                                                Usuario</label>
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="cboSupervisores">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="password">
                                                Contraseña</label>
                                            <asp:TextBox ID="Password" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row bottom">
                                    <div class="col-md-4">
                                        <div id="logo">
                                            <img src="../Imagenes/logo_login.jpg" alt="CIPOL" />
                                        </div>
                                    </div>
                                    <div id="submit">
                                        <input class="btn green button-filtrar" id="cmdAceptar" value="Aceptar" onclick="return page.VerificarClave();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
