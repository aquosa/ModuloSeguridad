<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmABMUsuarios.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMUsuarios" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Usuario/jsfrmABMUsuarios.js"></script>
    <script type="text/javascript" src="js/Usuario/JQueryGridUsuarios.js"></script>
    <link href="Estilos/horarios-style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="usuarios">
        <div class="container">

            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>ADMINISTRACIÓN DE USUARIOS</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                        <div class="row row-filter" onclick="javascript:slideSidebar('filtro-usuarios', 'img-filtro-usuarios');">
                            <div class="col-md-12">
                                <div class="head filter">
                                    <span class="white">FILTRO</span>
                                    <div style="float: right; margin-top: -2px">
                                        <img src="Imagenes/menos.png" id="img-filtro-usuarios" alt="" /></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="filtro-usuarios">
                            <div class="col-md-12">
                                <div class="form form-filter col-md-12">
                                    <div class="row body">
                                        <div class="col-md-12">
                                            <div class="row" style="margin: 0;">
                                                <div class="col-md-12" style="margin: 0 0 5px 0;">
                                                    <span class="bold">Filtrar por:</span>&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbUsu" Text="Nombre de Usuario" runat="server" GroupName="filtro" Checked="true" />&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbNombre" Text="Nombre y Apellido" runat="server" GroupName="filtro" />&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbArea" Text="Área" runat="server" GroupName="filtro" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 5px 0;">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label for="comienzaCon" class="col-md-4 control-label">Comienza con: </label>
                                                        <input type="text" value="" class="col-md-8" id="txtFiltro" name="txtFiltro" maxlength="30" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label for="baja" class="col-md-2 control-label">Baja:</label>
                                                        <div id="reemplazarcboEstado"></div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:CheckBox ID="chkSubCadenas" runat="server" Text="Filtrar por SubCadenas" />

                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0;">
                                                <div class="col-md-12">
                                                    <button id="btnFiltrar" data-bind='click: cmdRecuperarUsuarios' class="btn green">FILTRAR</button>
                                                    <button id="btnLimpiarFiltro" data-bind='click: cmdLimpiarFiltro' class="btn green">LIMPIAR</button>
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
                                    <span class="white">USUARIOS</span>
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
                    <button type="button" id="btnNuevo" class="btn green">NUEVO</button>
                </div>
            </div>

            <!-- PopUP -->
            <div id="dialogAltaEdit" class="ui-dialog">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box1">
                            <div class="row">
                                <div class="head">
                                    <span class="white">USUARIOS</span>
                                </div>
                            </div>
                            <div class="row">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs tabs">
                                    <li class="active"><a href="#usuario" data-toggle="tab">Usuario</a></li>
                                    <li id="literminales"><a href="#terminales" data-toggle="tab">Iniciar desde</a></li>
                                    <li><a href="#roles" id="btnRoles" data-toggle="tab">Roles</a></li>
                                    <li id="lihorarios"><a href="#horarios-tab" data-toggle="tab">Horarios</a></li>
                                </ul>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form col-md-12">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="usuario">
                                                <div class="row">
                                                    <div class="col-xs-6 border">
                                                        <div class="form-group">
                                                            <label for="txtUsuario" class="col-xs-2 control-label">Cuenta de usuario:</label>
                                                            <input type="text" value="" class="col-xs-7" id="txtUsuario" maxlength="50" name="cuentaUsuario" />
                                                            <span class="col-xs-1 required">*</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <button type="button" id="btnVerificarUsuario" class="btn green">VERIFICAR</button>
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <asp:CheckBox ID="chkCuentaBloqueada" class="custom" runat="server" Text="Bloquear Cuenta" />
                                                        <div class="row">
                                                          <label id="lblFechaBloq" for="fechaBloqueo" class="col-xs-2 control-label">Hasta: </label>  <input type="text" value="" class="col-xs-8" id="txtFechaBloq" maxlength="50" name="fechaBloqueo" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row grey">
                                                    <div class="col-xs-6 border">
                                                        <div class="form-group">
                                                            <label for="txtNombres" class="col-xs-2 control-label">Nombre y Apellido: </label>
                                                            <input type="text" value="" class="col-xs-7" id="txtNombres" maxlength="50" name="nombreApellido" />
                                                            <span class="col-xs-1 required">*</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label for="txtAliasUsuario" class="col-xs-2 control-label">Alias de Usuario: </label>
                                                            <input type="text" value="" class="col-xs-8" id="txtAliasUsuario" maxlength="16" name="aliasUsuario" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-6 border">
                                                        <div class="form-group">
                                                            <label for="txtEmail" class="col-xs-2 control-label">Email: </label>
                                                            <input type="text" value="" class="col-xs-8" id="txtEmail" maxlength="50" name="emailUsuario" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label for="txtDomicilio" class="col-xs-2 control-label">Domicilio: </label>
                                                            <input type="text" value="" class="col-xs-8" id="txtDomicilio" maxlength="50" name="domicilio" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row grey">
                                                    <div class="col-xs-5 border">
                                                        <div class="form-group">
                                                            <label for="tipoDocumento" class="col-xs-2 control-label">Tipo de documento: </label>

                                                            <div id="reeemplazarcomboTipoDoc" class="col-xs-7"></div>

                                                        </div>
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <div class="form-group">
                                                            <label for="numeroDocumento" class="col-xs-2 control-label">Número de documento: </label>
                                                            <input type="text" value="" class="col-xs-7" id="txtNroDocumento" maxlength="10" name="docuemnto" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-8">
                                                        <div class="form-group">
                                                            <label for="perteneceArea" class="col-xs-7 control-label">Pertenece al Área: </label>
                                                            <div class="col-xs-7">
                                                                <div id="reemplazarcomboArea"></div>
                                                            </div>
                                                            <span class="col-xs-1 required">*</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row grey">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label for="txtContraseña" class="col-xs-2 control-label">Contraseña: </label>
                                                            <input type="password" value="" class="col-xs-8" id="txtContrasena" maxlength="50" name="contraseña" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <asp:CheckBox ID="chkIntegradoAlDominio" runat="server" Text="Integrado al Dominio" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label for="passwordRepeat" class="col-xs-2 control-label">Repetir Contraseña: </label>
                                                            <input type="password" value="" class="col-xs-8" id="txtRepetirContrasena" name="contraseña" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <asp:CheckBox ID="chkForzar" runat="server" Text="Cambiar contraseña al iniciar la aplicación" />
                                                    </div>
                                                </div>
                                                <div class="row grey" style="padding: 5px 0 5px 0">
                                                    <div class="col-xs-6 border">
                                                        <div class="form-group">
                                                            <label for="fechaAlta" class="col-xs-2 control-label">Fecha alta: </label>
                                                            <input type="text" value="" class="col-xs-8" id="txtFechaAlta" name="fechaAlta" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label for="fechaBaja" class="col-xs-2 control-label">Fecha baja: </label>
                                                            <input type="text" value="" class="col-xs-8" id="txtFechaBaja" name="fechaBaja" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding: 5px 0 5px 0">
                                                    <div class="col-xs-12">
                                                        <div class="form-group">
                                                            <label for="comentarios" class="col-xs-3 control-label">Comentarios: </label>
                                                            <textarea id="txtComentarios" rows="3" cols="70" maxlength="255"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="roles">
                                                <div class="row col-md-12">
                                                    <div class="col-xs-5" style="padding: 0 0 0 15px;">
                                                        <h3>PLANTILLA DE ROLES</h3>
                                                        <div id="treeRoles" class="large"></div>
                                                    </div>
                                                    <div class="col-sm-2 middle-control">
                                                        <button type="submit" id="cmdAsignar" class="btn big green">&gt;</button>
                                                        <button type="submit" id="cmdDesasignar" class="btn big green">&lt;</button>
                                                    </div>
                                                    <div class="col-sm-5" style="padding: 0 15px 0 0;">
                                                        <h3>ROLES ASIGNADOS AL USUARIO</h3>
                                                        <div id="treeasignadosRoles"></div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="terminales">
                                                <div class="row">
                                                    <div class="col-md-12" style="padding: 15px 0 0 0;">
                                                        <div class="form-group">
                                                            <label class="col-md-4">Filtrar Terminales por area: </label>
                                                            <div id="replazarPorFiltroDeAreas" class="large"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-5" style="padding: 0 0 0 15px;">
                                                        <h3>TERMINALES HABILITADAS PARA INICIAR SESIÓN</h3>
                                                        <div id="lbTerminalesHabilitadas" class="large"></div>
                                                    </div>
                                                    <div class="col-sm-2 middle-control">
                                                        <button type="submit" id="btnAsignarTerminalesTodas" class="btn big green">&raquo;</button>
                                                        <button type="submit" id="btnAsignarTerminal" class="btn big green">&gt;</button>
                                                        <button type="submit" id="btnDesasignarTerminal" class="btn big green">&lt;</button>
                                                        <button type="submit" id="btnDesasignarTerminalesTodas" class="btn big green">&laquo;</button>
                                                    </div>
                                                    <div class="col-sm-5" style="padding: 0 15px 0 0;">
                                                        <h3>TERMINALES NO HABILITADAS PARA INICIAR SESIÓN</h3>
                                                        <div id="lbTerminalesNOHabilitadas"></div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="horarios-tab">
                                                <div id="dialogHorarios" align="center">
                                                    <div style="width: 626px; margin-left: 65px;">
                                                        <div style="float: left; margin-left: 124px;">
                                                            <span>00:00 Hs.</span>
                                                        </div>
                                                        <div style="float: left; margin-left: 103px;">
                                                            <span>12:00 Hs.</span>
                                                        </div>
                                                        <div style="float: left; margin-left: 103px;">
                                                            <span>00:00 Hs.</span>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                    </div>
                                                    <div style="width: 452px; height: 35px; border-right: 1px solid #000000;">
                                                        <div style="float: left; width: 91px; height: 35px;">
                                                        </div>
                                                        <div style="width: 181px; height: 35px; border-right: 1px solid #000000; border-left: 1px solid #000000; float: left">
                                                        </div>
                                                    </div>
                                                    <div id="horarios">
                                                    </div>
                                                    <div id="horarios-referencias">
                                                        <div class="referencia left">
                                                            <div class="cuadrado alert-success">
                                                            </div>
                                                            <span>Horarios Permitidos</span>
                                                        </div>
                                                        <div class="referencia right">
                                                            <div class="cuadrado alert-danger">
                                                            </div>
                                                            <span>Horarios NO Permitidos</span>
                                                        </div>
                                                        <div style="clear: both">
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
            </div>










            <%--<div id="dialogHorarios" align="center">
            <div style="width: 530px; margin-left: 65px;">
                <div style="float: left; margin-left: 60px;">
                    <span>00:00 Hs.</span></div>
                <div style="float: left; margin-left: 135px;">
                    <span>12:00 Hs.</span></div>
                <div style="float: left; margin-left: 135px;">
                    <span>00:00 Hs.</span></div>
                <div style="clear: both">
                </div>
            </div>
            <div style="width: 465px; height: 35px; border-right: 1px solid #000000;">
                <div style="float: left; width: 81px; height: 35px;">
                </div>
                <div style="width: 191px; height: 35px; border-right: 1px solid #000000; border-left: 1px solid #000000;
                    float: left">
                </div>
            </div>
            <div id="horarios">
            </div>
            <div id="horarios-referencias">
                <div class="referencia left">
                    <div class="cuadrado">
                    </div>
                    <span>Horarios Permitidos</span>
                </div>
                <div class="referencia right">
                    <div class="cuadrado rojo">
                    </div>
                    <span>Horarios NO Permitidos</span>
                </div>
                <div style="clear: both">
                </div>
            </div>
        </div>--%>
        </div>
    </section>
</asp:Content>
