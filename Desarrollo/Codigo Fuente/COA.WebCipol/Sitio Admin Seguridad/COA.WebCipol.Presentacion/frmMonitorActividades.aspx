<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmMonitorActividades.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmMonitorActividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/MonitorActividades/jsfrmMonitorActividades.js"></script>
    <script type="text/javascript" src="js/MonitorActividades/jsQueryGridMonitorActividades.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="monitor-actividades">
    <div class="container">
            
        <!-- Titulo de la página -->
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>MONITOR DE ACTIVIDADES</h2>
            </div>
        </div>
        <!-- Fin titulo de la página -->

        <!-- Filtro -->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('filtro-monitor-actividades', 'img-filtro-monitor-actividades');">
                        <div class="col-md-12">
                            <div class="head filter">
                                <span class="white">FILTRO</span>
                                <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-filtro-monitor-actividades" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="filtro-monitor-actividades">
                        <div class="col-md-12">
                            <div class="col-md-12 form form-filter">
                                <div class="row body">
                                <div class="col-md-12">
                                    <div class="row" style="margin:0 0 5px 0;">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Usuario:</label>
                                                <input type="text" class="col-md-8" id="txtUsuario" name="txtUsusario"  maxlength="50">
                                            </div>
                                        </div>
										<div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Terminal:</label>
                                                <input type="text" class="col-md-8" id="txtTerminal" name="txtTerminal"  maxlength="20">
                                            </div>
                                        </div>
                                        </div>
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4  control-label">Nombre:</label>
                                                    <input type="text" class="col-md-8"  id="txtNombre" name="txtNombre" maxlength="30">
                                                </div>
                                            </div>
										    <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Área:</label>
                                                     <input type="text" class="col-md-8"  id="txtArea" name="txtArea"  maxlength="30">
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <button  id="btnFiltro"class="btn green button-filtrar" data-bind='click: cmdRecuperarMonitorActividades'>FILTRAR</button>
                                                     <button  id="btnLimpiarFiltro"class="btn green button-filtrar" data-bind='click: cmdLimpiarFiltro'>LIMPIAR</button>
                                                      <button  id="btnEliminar"class="btn green button-filtrar">ELIMINAR</button>
                                                </div>
                                            </div>
										    <div class="col-md-6">
                                                <div class="form-group">
                                                   
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
                    <div data-bind='simpleGrid: GridViewModel' class="grilla" > </div>
                </div>
            </div>
        </div>

    
    </div>
</section>
    <%--<div class="BordeContenido" align="center">
        <h2 class="Titulo">Monitor de actividades</h2>
         <table cellpadding="0" cellspacing="0" class="BordeContenidoInterno" id="filtro" runat="server">
            <tr>
                <th class="Subtitulo">
                    <asp:Label ID="lblFiltro" runat="server">Filtro</asp:Label>
                </th>
            </tr>
            <tr>
                <td>
                    <table align="center">
                         <tr>
                            <td class="tdLabel"><span>Filtrar por usuario </span></td>
                            <td class="tdInput"><asp:TextBox ID="txtUsuario" runat="server" Width="170px"></asp:TextBox></td>
                            <td class="tdLabel"><span>Filtrar por terminal </span></td>
                            <td class="tdInput"><asp:TextBox ID="txtTerminal" runat="server" Width="170px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><span>Filtrar por nombre </span></td>
                            <td class="tdInput"><asp:TextBox ID="txtNombre" runat="server" Width="170px"></asp:TextBox></td>
                            <td class="tdLabel"><span>Filtrar por área </span></td>
                            <td class="tdInput"><asp:TextBox ID="txtArea" runat="server" Width="170px"></asp:TextBox></td>
                        </tr>
                        <tr>
                             <td><input id="btnFiltrar" type="button" runat="server" value="Filtrar" /></td>
                             <td><input id="btnLimpiarFiltro" type="button" runat="server" value="Limpiar" /></td>
                            <td><input id="btnEliminar" type="button" runat="server" value="Eliminar" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="" id="grilla" runat="server">
            <tr valign="top">
                <td class="style1">
                    <br />
                    <div id="reemplazarPorGrilla" class="BordeContenido" align="center">
                    </div>
                    <br />
                </td>
            </tr>
        </table>
   </div>--%>
</asp:Content>
