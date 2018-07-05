<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true" CodeBehind="frmVisorSucesosSeguridad.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmVisorSucesosSeguridad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        
    <script type="text/javascript" src="js/Seguridad/jsfrmVisorSucesosSeguridad.js"></script>
    <script type="text/javascript" src="js/Seguridad/JQueryGridSeguridad.js" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
 <section>
    <div class="container">
            
        <!-- Titulo de la página -->
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>VISOR DE SUCESOS DE SEGURIDAD</h2>
            </div>
        </div>
        <!-- Fin titulo de la página -->

        <!-- Filtro -->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('filtro-sucesos-seguridad', 'img-filtro-sucesos-seguridad');">
                        <div class="col-md-12">
                            <div class="head filter">
                                <span class="white">FILTRO</span>
                                <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-filtro-sucesos-seguridad" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="filtro-sucesos-seguridad">
                        <div class="col-md-12">
                            <div class="form form-filter col-md-12" >
                                <div class="row body">
                                    <div class="col-md-12">
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="txtFechaDesde" class="col-md-3 control-label">Fecha Desde: </label>
                                                    <input type="text" class="col-md-9" id="txtFechaDesde" name="txtFechaDesde">
                                                </div>
                                            </div>
									        <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="txtFechaHasta" class="col-md-3 control-label">Fecha Hasta: </label>
                                                    <input type="text" class="col-md-9" id="txtFechaHasta" name="txtFechaHasta">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label  class="col-md-3 control-label">Afectado: </label>
                                                    <div id="reemplazarcomboAfectados"></div>
                                                </div>
                                            </div>
									        <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Administrador: </label>
                                                    <div id="reemplazarcomboAdministradores"></div>
                                                </div>
                                            </div>
                                         </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Mensaje: </label>
                                                    <div id="remplazarcomboMensajes"></div>                                                  
                                                </div>
                                            </div>
										 
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <button  id="btnFiltrar" data-bind='click: cmdRecuperarSucesos' class="btn green button-filtrar">FILTRAR</button>
                                                    <button  id="btnLimpiarFiltro" data-bind='click: cmdLimpiarFiltro' class="btn green button-filtrar">LIMPIAR</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--Fin Filtro -->

        <!-- Grilla-->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1 table-responsive">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">SUCESOS</span>
                            </div>
                        </div>
                    </div>
                    <div data-bind='simpleGrid: ViewModelSucesos' class="grilla" > </div>
                </div>
            </div>
        </div>
        <!-- Fin Grilla -->

        <div class="row" style="text-align:center;">
            <div class="col-md-12">
                <button type="button" id="btnImprimir" class="btn green">IMPRIMIR</button>
            </div>
        </div>

        <!-- PopUp-->
        <div id="dialogAbrirMensaje" class="ui-dialog">
            <div class="row">
                <div class="col-md-12">
                    <div class="box1">
                        <div class="row">
                            <div class="head">
                                <span class="white">DETALLE DEL SUCESO</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form col-md-12">
                                    <div class="row">
                                        <div class="col-xs-12" style=" margin-top :15PX;">
                                            <div class="form-group">
                                                <label class="col-xs-2 control-label">Hora/Fecha: </label>
                                                <label ID="lblFechaHora"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label class="col-xs-2 control-label">Código: </label>
                                                <label ID="lblCodigo"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label class="col-xs-2 control-label">Mensaje: </label>
                                                <label ID="lblMensaje" ></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label class="col-xs-2 control-label">Usuario Administrador: </label>
                                                <label ID="lblUsuarioAdministrador"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label class="col-xs-2 control-label">Usuario Afectado: </label>
                                                <label ID="lblUsuarioAfectado"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--Fin PopUp-->


    </div>
</section>
            
</asp:Content>