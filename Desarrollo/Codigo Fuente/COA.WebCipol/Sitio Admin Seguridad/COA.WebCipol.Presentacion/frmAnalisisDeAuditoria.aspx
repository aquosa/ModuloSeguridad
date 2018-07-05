<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true" CodeBehind="frmAnalisisDeAuditoria.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmAnalisisDeAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        var CantidadRegistrosDefault = "<%=CantidadRegistrosDefault%>";
        var CantidadRegistrosTotales;
    </script>

    <script type="text/javascript" src="js/AnalisisAuditoria/jsfrmAnalisisDeAuditoria.js"></script>
    <script type="text/javascript" src="js/AnalisisAuditoria/JQueryGridAuditoria.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="analisis-de-auditoria">
        <div class="container">

            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>AUDITORÍA DE EVENTOS DEL SISTEMA</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                        <div class="row row-filter" onclick="javascript:slideSidebar('filtro-analisis-auditoria', 'img-filtro-analisis-auditoria');">
                            <div class="col-md-12">
                                <div class="head filter">
                                    <span class="white">FILTRO</span>
                                    <div style="float: right; margin-top: -2px">
                                        <img src="Imagenes/menos.png" id="img-filtro-analisis-auditoria" alt="" /></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="filtro-analisis-auditoria">
                            <div class="col-md-12">
                                <div class="form form-filter col-md-12">
                                    <div class="row body">
                                        <div class="col-md-12">
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblFechaDesde" class="col-md-3 control-label">Fecha Desde: </label>
                                                        <input type="text" class="col-md-9" id="txtFechaDesde" name="txtFechaDesde">
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblFechaHasta" class="col-md-3 control-label">Fecha Hasta: </label>
                                                        <input type="text" class="col-md-9" id="txtFechaHasta" name="txtFechaHasta">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblTabla" class="col-md-3 control-label">Tabla: </label>
                                                        <div id="reemplazarcomboTabla"></div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblUsusario" class="col-md-3 control-label">Usuario: </label>
                                                        <div id="reemplazarcomboUsuario"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblOperacion" class="col-md-3 control-label">Operación: </label>
                                                        <select class="col-md-9 select-no-padding" id="cboOperacion">
                                                            <option value="T">(TODAS)</option>
                                                            <option value="A">ALTAS</option>
                                                            <option value="B">BAJAS</option>
                                                            <option value="M">MODIFICACIONES</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblNombrePC" class="col-md-3 control-label">Nombre PC: </label>
                                                        <div id="remplazarcomboNombrePC"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblSistema" class="col-md-3 control-label">Sistema: </label>
                                                        <div id="remplazarcomboSistema"></div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblTxtBusqueda" class="col-md-3 control-label">Texto de búsqueda: </label>
                                                        <input type="text" class="col-md-9" id="txtTextoBusqueda" onkeypress="CaracteresExtranios(this, event)" name="txtTextoBusqueda" maxlength="30">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <button id="btnFiltro" data-bind='click: cmdRecuperarEventos' class="btn green button-filtrar">FILTRAR</button>
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
                                    <span class="white">EVENTOS DEL SISTEMA</span>
                                </div>
                            </div>
                        </div>
                        <div data-bind='simpleGrid: GridViewModel' class="grilla"></div>
                    </div>
                </div>
            </div>
            <!-- Fin Grilla -->

            <div class="row" style="text-align: center;">
                <div class="col-md-12">
                    <button type="button" id="btnExportarExcel" class="btn green">EXPORTAR EXCEL</button>
                </div>
            </div>

            <div id="dialogStringSQL" class="ui-dialog">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box1">
                            <div class="row">
                                <div class="head">
                                    <span class="white">STRING SQL</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form form-filter col-md-12">
                                        <div class="col-xs-12">
                                            <div class="row" style="margin: 0 0 22px 0;">
                                                <textarea id="txtStringSQL" readonly="readonly" rows="5" class="col-md-12"></textarea>
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
    </section>
</asp:Content>
