<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true" CodeBehind="frmABMTerminales.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMTerminales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Terminales/jsfrmABMTerminales.js"></script>
    <script type="text/javascript" src="js/Terminales/JQueryGridTerminales.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="terminales">
        <div class="container">
            
            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>ADMINISTRACIÓN DE TERMINALES</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                    	<div class="row row-filter" onclick="javascript:slideSidebar('filtro-terminales', 'img-filtro-terminales');">
                            <div class="col-md-12">
                            	<div class="head filter">
                                	<span class="white">FILTRO</span>
                                    <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-filtro-terminales" alt=""/></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="filtro-terminales">
                             <div class="col-md-12">
                                <div class="body">
                                    <div class="col-md-12">
                                       <div class="row" style="margin:0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label  class="col-md-7 control-label">Cód. de Terminal que comienzan con:</label>
                                                    <input type="text" class="col-md-5" id="txtFiltroTerminal"  name="terminal" maxlength="20"/>
                                                </div>
                                            </div>
											<div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="txtFiltroArea" class="col-md-7 control-label">Terminales que pertenecen al área:</label>
                                                     <input type="text" class="col-md-5" id="txtFiltroArea"  name="area" maxlength="30"/>
                                                </div>
                                            </div>                                            
                                         </div>
                                         <div class="row" style="margin:0;">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                     <button  id="btnFiltro" data-bind='click: cmdRecuperarTerminales' class="btn green button-filtrar">FILTRAR</button>
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
            <!--Fin Filtro -->

            <!-- Grilla-->
            <div class="row ">
            	<div class="col-md-12">
                    <div class="block box1 table-responsive">
                    	<div class="row bo">
                            <div class="col-md-12">
                            	<div class="head">
                                	<span class="white">TERMINALES</span>
                                </div>
                            </div>
                        </div>
                        <div data-bind='simpleGrid: GridViewModel' class="grilla" > </div>
                    </div>
            	</div>
            </div>
             <!-- Fin Grilla -->

            <div class="row" style="text-align:center;">
            	<div class="col-md-12">
                	<button type="button" id="btnNuevo" class="btn green">NUEVO</button>
                </div>
            </div>

            <!--PopUp-->
            <div id="dialogAltaEdit" class="ui-dialog">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box1">
                            <div class="row">
                                <div class="head">
                                    <span class="white">TERMINAL</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form col-md-12">
                                        <div class="row">
                                            <div class="col-xs-8 border">
                                                <div class="form-group">
                                                   <label for="txtCodTerminal" class="col-xs-2 control-label">Código de terminal: </label>
                                                   <input type="text" value="" class="col-xs-8" id="txtCodTerminal" name="codigoTerminal"/ maxlength="20">
                                                   <span class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <asp:CheckBox ID="chkHabilitada" runat="server" />
                                                <label for="chkHabilitada" class="col-xs-3 control-label">Habilitada para su uso: </label>
                                            </div>
                                        </div>
                                        <div class="row grey">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtNombreTerminal" class="col-xs-4 control-label">Nombre NETBIOS: </label>
                                                    <textarea class="col-xs-8" rows="2" cols="70" id="txtNombreTerminal" maxlength="255" name="nombreNETBIOS"></textarea>
                                                    <span class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 border">
                                                <div class="form-group">
                                                    <label for="txtProcesador" class="col-xs-3 control-label">Procesador: </label>
                                                    <input type="text" value="" class="col-xs-8" id="txtProcesador" maxlength="50" name="procesador"/>
                                                </div>
                                            </div>                                            
                                        </div>
                                        <div class="row grey">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="cboArea" class="col-xs-3 control-label">Area:</label>
                                                    <div id="reemplazarcboArea"></div>
                                                     <span class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-4 border">
                                                <div class="form-group">
                                                    <label for="txtRam" class="col-xs-8 control-label">Memoria RAM: </label>
                                                    <input type="text" value="" class="col-xs-4" id="txtRam" maxlength="5" name="memoriaRAM"/>
                                                    <span>MB</span>
                                                </div>
                                            </div>
                                            <div class="col-xs-4 border">
                                                <div class="form-group">
                                                    <label for="cboModoActualizacion"  class="col-xs-3 control-label">Modo de Actualización: </label>
                                                    <select class="small-caption col-xs-4 select-no-padding" id="cboModoActualizacion" name="monitorActualizacion">
                                                        <option selected value="R">Remoto</option>
                                                        <option value="L">Local</option>
                                                    </select>
                                                    <span class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                                <div class="form-group">
                                                    <label for="txtDisco" class="col-xs-8 control-label">Tamaño del disco: </label>
                                                    <input type="text" value="" class="col-xs-4" id="txtDisco" maxlength="5" name="tamDisco" />
                                                    <span>GB</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row grey">
                                            <div class="col-xs-6 border">
                                                <div class="form-group">
                                                    <label for="txtMonitor" class="col-xs-7 control-label">Monitor: </label>
                                                    <input type="text" value="" class="col-xs-8" id="txtMonitor"  maxlength="50" name="monitor" />
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label for="txtVideo" class="col-xs-7 control-label">Aceleradora de video: </label>
                                                    <input type="text" value="" class="col-xs-7" id="txtVideo" maxlength="50" name="aceleradoraVideo" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtComentarios" class="col-xs-3 control-label">Comentarios: </label>
                                                    <textarea name="comentarios" id="txtComentarios" maxlength="250" class="col-xs-9"></textarea>
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
            <!--fin PopUP-->

        </div>
    </section>
</asp:Content>
