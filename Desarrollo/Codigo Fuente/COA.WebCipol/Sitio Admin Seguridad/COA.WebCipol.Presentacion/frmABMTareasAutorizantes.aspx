<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmABMTareasAutorizantes.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMTareasAutorizantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .lbTareasPrimitivas
        {
            overflow: scroll;
        }
    </style>
    <script type="text/javascript" src="js/TareasAutorizantes/jsfrmABMTareasAutorizantes.js"></script>
    <script type="text/javascript" src="js/TareasAutorizantes/JQueryGridTareasAutorizantes.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="areas">
    <div class="container">
            
        <!-- Titulo de la página -->
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>ADMINISTRACIÓN DE TAREAS AUTORIZANTES</h2>
            </div>
        </div>
        <!-- Fin titulo de la página -->           

        <button id="btnFiltro" style="display: none" data-bind='click: cmdRecuperarTareasAutorizantes'></button>
        <!-- Grilla-->
        <div class="row ">
            <div class="col-md-12">
                <div class="block box1 table-responsive">
                    <div class="row bo">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">TAREAS AUTORIZANTES</span>
                            </div>
                        </div>
                    </div>
                    <div data-bind='simpleGrid: GridViewModel' class="grilla" > </div>
                </div>
            </div>
        </div>
        <!-- Fin Grilla -->

        <div class="row" style="text-align:center; margin-bottom:15px">
            <div class="col-md-12">
                <button type="button" id="btnNuevo" class="btn green">NUEVO</button>
            </div>
        </div>

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
                                        <div class="col-xs-5 border">
                                            <div class="form-group">
                                                <label for="codigoTerminal" class="col-xs-3 control-label">Código de Tarea: </label>
                                                <input type="text" class="col-xs-6" ID="txtCodTA" MaxLength="10" name="codigo-tarea"/>
                                                <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                        <div class="col-xs-7">
                                            <div class="form-group">
                                                <label for="codigoTerminal" class="col-xs-2 control-label">Sistema:</label>
                                                <div id="reemplazarcbosistema"></div>
                                                <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row grey">
                                        <div class="col-xs-12 border">
                                            <div class="form-group">
                                                <label for="decripcion" class="col-xs-3 control-label">Descripción:</label>
                                                <input type="text"  class="col-xs-8" MaxLength="100" id="txtDescTA" name="descripcion"/>
                                                <span class="col-xs-1 required">*</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-5" style="padding: 15px 0 0 15px;">
                                            <h3>TAREAS PRIMITIVAS</h3>
                                            <div id="tareasprimitivas"></div>
                                        </div>
                                        <div class="col-sm-2 middle-control">
                                            <button type="submit" ID="cmdAsignar" class="btn big green">&gt;</button>
                                            <button type="submit" ID="cmdDesasignar" class="btn big green">&lt;</button>
                                        </div>
                                        <div class="col-sm-5" style="padding: 15px 15px 0 0;">
                                            <h3>DESCRIPCIÓN TAREA</h3>
                                            <%--<asp:ListBox ID="lbTareasDosPrimitivas" runat="server" class="large" style="margin:0px; overflow:scroll" ></asp:ListBox>                                                       --%>
                                            <div id="lbTareasDosPrimitivas"></div>
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
