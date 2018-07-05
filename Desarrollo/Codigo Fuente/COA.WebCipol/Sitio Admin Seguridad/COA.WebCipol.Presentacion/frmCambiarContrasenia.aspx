<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmCambiarContrasenia.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmCambiarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script  type="text/javascript" src="js/jsfrmCambiarContrasenia.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="login" style="height:100%;">
        <div class="container" style="height:100%; ">
           <div style="top:20px !important;">
                <div class="row">
                    <div class="col-md-offset-1 col-md-10">
                        <div class="row">
                            <div class="col-xs-offset-3 col-xs-6 block white">
                                <div class="form col-md-12">
                                    <div class="row" style="margin:15px 0px 0px 0px ">
                                        <div class="col-md-12">
                                            <h1 class="green">CAMBIO DE CONTRASEÑA<br/>
                                                DEL USUARIO ACTIVO
                                            </h1>          
                                            <div class="form-group">
                                                <label for="txtContraseña">Contraseña actual</label>
                                                    <input type="password" class="form-control" name="passActual" id="txtContraseña" value=""/>
                                            </div>
                                            <div class="form-group">
                                                    <label for="txtNuevaContraseña">Nueva Contraseña</label>
                                                <input type="password" class="form-control" name="password" id="txtNuevaContraseña" value=""/>
                                            </div>
                                                <div class="form-group">
                                                    <label for="txtRepetirNuevaContraseña">Nueva Contraseña</label>
                                                <input type="password" class="form-control" name="password" id="txtRepetirNuevaContraseña" value=""/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding:15px 0">
                                        <div class="col-md-12">
                                            <div id="submit" style="margin-left:15px">
                                                <button type="submit" id="cmdAceptar" class="btn green">ACEPTAR</button>
                                                <button type="submit" id="cmdCancelar" class="btn green">CANCELAR</button>  
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div style="clear:both"></div>--%>
        </div>
    </section>
</asp:Content>
