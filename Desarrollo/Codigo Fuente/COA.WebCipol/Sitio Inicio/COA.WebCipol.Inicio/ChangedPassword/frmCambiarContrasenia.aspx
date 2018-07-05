<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCambiarContrasenia.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.ChangedPassword.frmCambiarContrasenia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=9"/>
    <title>INGRESO AL SISTEMA</title>
    
    <link rel="stylesheet" href="~/css/bootstrap.min.css"/>
    <!-- Optional theme -->
    <link rel="stylesheet" href="~/css/bootstrap-theme.min.css"/>

    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.10.3.custom.js"></script>

    <link href="~/css/overcast/jquery-ui-1.8.18.custom.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Scripts/jsfrmLogin.js"></script>
</head>
<body>
    <header>
    	<div class="title">
        	<h1>CIPOL / Sistema de seguridad</h1>
        </div>
    </header>
    <form id="formLogin" runat="server">
<%--    <div>
        <table width="100%">
            <tr>
                <td colspan="4" style="height: 30px">
                </td>
            </tr>
            <tr>
                <td style="width: 10%" colspan="2">
                </td>
                <td align="center" width="80%" valign="middle">
                    <table style="vertical-align: middle" align="center" cellpadding="5" class="login">
                        <tr>
                            <td align="left">
                                <img alt="CIPOL" src="../Images/prod_cipol.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="80%" valign="middle">
                                <table cellpadding="1" cellspacing="5" style="border-collapse: collapse;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCambiarContrasenia" runat="server" Font-Bold="True">Cambiar la contraseña:</asp:Label>
                                            <asp:Label ID="lblUsuario" runat="server" CssClass="LoginLabel"></asp:Label>
                                        </td>
                                    </tr>--%>

      <section id="login" style="height:100%;">
        <div class="container" style="height:100%;">
            <div class="vertical-aligner" style="height:100%; top:20px !important;">
                <div class="row">
                    <div class="col-md-offset-1 col-md-10">
                        <div class="row">
                            <div class="col-xs-offset-3 col-xs-6 block white">
                                <div class="form col-md-12">
                                    <div class="row" style="margin:15px 0px 0px 0px ">
                                        <div class="col-md-12">
                                            <h2 class="green">CAMBIO DE CONTRASEÑA DEL USUARIO ACTIVO </h2>
                                            <h4>Usuario : <asp:Label ID="lblUsuario" runat="server" CssClass="LoginLabel"></asp:Label></h4>
                                             <div class="form-group">
                                                <label for="txtContraseña">Contraseña actual</label>
                                                <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"  CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                                            ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                                            CssClass="error" ValidationGroup="ucChangePasswordValidationSummary"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                 <label for="txtNuevaContraseña">Nueva Contraseña</label>
                                                 <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                                            ErrorMessage="La nueva contraseña es obligatoria." ToolTip="La nueva contraseña es obligatoria."
                                                            CssClass="error" ValidationGroup="ucChangePasswordValidationSummary"></asp:RequiredFieldValidator>
                                            </div>
                                                <div class="form-group">
                                                    <label for="txtRepetirNuevaContraseña">Nueva Contraseña</label>
                                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                                            ErrorMessage="Confirmar la nueva contraseña es obligatorio." ToolTip="Confirmar la nueva contraseña es obligatorio."
                                                            ValidationGroup="ucChangePasswordValidationSummary" CssClass="error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding:15px 0">
                                        <div class="col-md-12">
                                            <div id="Div1">
                                              <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                                Text="Cambiar contraseña" CssClass="btn green"
                                                ValidationGroup="ucChangePasswordValidationSummary" 
                                                onclick="ChangePasswordPushButton_Click" />
                                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="Cancelar" onclick="CancelPushButton_Click" CssClass="btn green" />
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row"  style="padding:15px 0">
                                        <asp:Label ID="FailureText" runat="server" CssClass="alert alert-dange"></asp:Label>
                                        <%--<asp:ValidationSummary ID="ucChangePasswordValidationSummary" runat="server" CssClass="alert alert-danger"
                                         ValidationGroup="ucChangePasswordValidationSummary" />--%>
                                           
                                        <asp:Label ID="lblNombreDominio" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>     
                                   <%-- <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"
                                                            CssClass="LoginLabel">Contraseña:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                                            ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                                            CssClass="error" ValidationGroup="ucChangePasswordValidationSummary">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"
                                                            CssClass="LoginLabel">Nueva contraseña:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                                            ErrorMessage="La nueva contraseña es obligatoria." ToolTip="La nueva contraseña es obligatoria."
                                                            CssClass="error" ValidationGroup="ucChangePasswordValidationSummary">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword"
                                                            CssClass="LoginLabel">Confirmar la nueva contraseña:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                                            ErrorMessage="Confirmar la nueva contraseña es obligatorio." ToolTip="Confirmar la nueva contraseña es obligatorio."
                                                            ValidationGroup="ucChangePasswordValidationSummary" CssClass="error">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                                            ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Confirmar la nueva contraseña debe coincidir con la entrada Nueva contraseña."
                                                            ValidationGroup="ucChangePasswordValidationSummary"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-top-style: solid; border-top-width: 1px; border-top-color: #D9D9D9;
                                            background-color: #F4F4F4;">
                                            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                                Text="Cambiar contraseña" 
                                                ValidationGroup="ucChangePasswordValidationSummary" 
                                                onclick="ChangePasswordPushButton_Click" />
                                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="Cancelar" onclick="CancelPushButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <span class="error">
                                    <asp:Literal ID="FailureText" runat="server" />
                                </span>
                                <asp:ValidationSummary ID="ucChangePasswordValidationSummary" runat="server" CssClass="error"
                                    ValidationGroup="ucChangePasswordValidationSummary" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="2" width="10%">
                </td>
            </tr>
            <tr>
                <td style="width: 10%" colspan="2">
                </td>
                <td align="center" width="80%" valign="middle">
                    <asp:Label ID="lblNombreDominio" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td colspan="2" width="10%">
                </td>
            </tr>
        </table>
    </div>--%>
    </form>
    
</body>
</html>
