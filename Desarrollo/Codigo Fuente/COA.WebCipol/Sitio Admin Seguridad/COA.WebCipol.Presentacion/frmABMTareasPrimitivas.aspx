<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmABMTareasPrimitivas.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMTareasPrimitivas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/TareasPrimitivas/jsfrmABMTareasPrimitivas.js" ></script>
    <script type="text/javascript" src="js/TareasPrimitivas/JQueryGridTareasPrimitivas.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
 <section id="tareas-primitivas">
    <div class="container">
            
        <!-- Titulo de la página -->
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>TAREAS PRIMITIVAS</h2>
            </div>
        </div>
        <!-- Fin titulo de la página -->

        <!-- Filtro -->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('filtro-tareas-primitivas', 'img-filtro-tareas-primitivas');">
                        <div class="col-md-12">
                            <div class="head filter">
                                <span class="white">FILTRO</span>
                                <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-filtro-tareas-primitivas" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="filtro-tareas-primitivas">
                        <div class="col-md-12">
                            <div class="col-md-12 form form-filter">
                                <div class="row body">
                                    <div class="col-md-12">
                                        <div class="row" style="margin:0 0 5px 0;">
                                           <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Id Tarea:</label>
                                                    <input type="text" class="col-md-8" maxlength="5"  id="txtFiltroID" name="id"/>
                                                    <span class="required col-xs-1"></span>
                                                </div>
                                            </div>
										    <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Nombre Tarea:</label>
                                                    <input type="text" class="col-md-8" id="txtFiltroNombre"  maxlength="100" name="nombre"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4  control-label">Sistema:</label>
                                                     <div id="reemplazarcbosisitemafiltro"></div>
                                                </div>
                                            </div>
										    <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="txtFiltroCodigo" class="col-md-4  control-label">Código: </label>
                                                     <input type="text" class="col-md-8" id="txtFiltroCodigo"  maxlength="10" name="codigo"/>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="cboTareaAutorizante" class="col-md-4  control-label">Posee tarea Autorizante: </label>
                                                    <select class="col-md-8 select-no-padding" id="cboTareaAutorizante">
                                                            <option value=" ">(Todas)</option>
                                                            <option value="S">SI</option>
                                                            <option value="N">NO</option>
                                                    </select>
                                                    
                                                </div>
                                            </div>
										    
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                        <div class="col-md-6">
                                                <div class="form-group">
                                                    <button id="btnFiltro"  data-bind='click: cmdRecuperarTareas' class="btn green button-filtrar">FILTRAR</button>
                                                    <button id="btnLimpiarFiltro" data-bind='click: cmdLimpiarFiltro' class="btn green button-filtrar">LIMPIAR</button>
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
                        <div class="col-xs-12">
                            <div class="head">
                                <span class="white">TAREAS PRIMITIVAS</span>
                            </div>
                        </div>
                    </div>
                    <div data-bind='simpleGrid: GridViewModel' class="grilla" > </div>
                </div>
            </div>
        </div>
        <!-- Fin Grilla -->

        <div class="row" style="text-align:center;">
            <div class="col-xs-12">
                <button type="button" id="btnNuevo" class="btn green">NUEVA TAREA</button>
            </div>
        </div>

        <!-- PopUP-->
        <div id="dialogAltaEdit" class="ui-dialog">
            <div class="row">
                <div class="col-md-12">
                    <div class="box1">
                        <div class="row">
                            <div class="head">
                                <span class="white">TAREA</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form col-md-12">
                                    <div class="row">
                                        <div class="col-xs-8 border">
                                            <div class="form-group">
                                                <label for="txtIdTarea" class="col-xs-3 control-label">Id Tarea: </label>
                                                <input type="text" class="col-xs-6" ID="txtIdTarea" MaxLength="5" name="id"/>
                                                <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                 <asp:Button ID="btnVerificarTarea" runat="server" Text="Verificar" class="btn green" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row grey">
                                        <div class="col-xs-5 border">
                                            <div class="form-group">
                                                <label for="txtCodigo" class="col-xs-3 control-label">Código: </label>
                                                <input type="text"  class="col-xs-6" MaxLength="10" id="txtCodigo" name="codigo"/>
                                                <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                        <div class="col-xs-7">
                                             <div class="form-group">
                                                <label for="sistema" class="col-xs-2 control-label">Sistema: </label>
                                                <div id="reemplazarcbosisitema"></div>
                                                 <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-xs-12">
                                             <div class="form-group">
                                                <label for="txtNombreTarea" class="col-xs-3 control-label">Nombre de Tarea: </label>
                                                <textarea id="txtNombreTarea" rows="2" maxlength="100" class="col-xs-8"></textarea>
                                                <span class="col-xs-1 required">*</span>
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
        <!-- Fin PopUP-->

    </div>
</section>
</asp:Content>
