<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmABMRoles.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/jsfrmABMRoles.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="roles">
    <div class="container">
        	
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>ADMINISTRACIÓN DE ROLES</h2>
            </div>
        </div>
            <!-- Filtro -->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('filtro-roles', 'img-roles');">
                        <div class="col-md-12">
                            <div class="head filter">
                                <span class="white">ROLES EXISTENTES</span>
                                <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-roles" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="filtro-roles">
                        <div class="col-md-12">
                            <div class="form-filter col-md-12 form">
                                <div class="row body">
                                    <div class="col-md-12">                                         
                                        <div class="row" style="margin:0 0 22px 0;">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <label class="col-xs-1 control-label">Rol:</label>
                                                    <div class="col-xs-10">
                                                        <div id="divcboroles"></div>
                                                    </div>
                                                    <input type="text" class="col-xs-9" id="txtNombreRol" name="txtNombreRol" maxlength="150"/>
                                                    <span class="col-xs-1 required" id="txtNombreRolRequered">*</span>
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

        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">COMPOSICIÓN</span>
                            </div>
                        </div>
                    </div>   
                    <div class="row">
                        <div class="col-md-12 sistemas">
                            <div class="col-md-12 form form-filter">
                                <div class="row body">
							        <div class="col-sm-12">
                                        <div class="row" style="margin:0;">
                                            <div class="col-xs-5" style="padding: 0 0 0 15px;">
										        <h3>PLANTILLA DE GRUPOS DE TAREAS</h3>
                                                <div class="large" id="treeeee"></div>
                                            </div>
                                            <div class="col-xs-2 middle-control">
                                                <button type="submit" class="btn big green" id="cmdAsignar" >&gt;</button>
                                                <button type="submit" class="btn big green" id="cmdDesasignar" >&lt;</button>
                                            </div>
                                            <div class="col-xs-5" style="padding: 0 15px 0 0;">
										        <h3>TAREAS ASIGNADAS AL ROL</h3>
                                                <div class="large" id="treeeee2"></div>
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

        <div class="row" style="text-align:center;">
            <div class="col-md-12">
                    <asp:Label ID="lblMsjError" runat="server" CssClass="error"></asp:Label>
            </div>
        </div>

        <div class="row" style="text-align:center;">
            <div class="col-md-12">
                <button type="button" id="cmdNuevo" class="btn green">NUEVO</button>
                <button type="button" id="cmdGuardar" class="btn green">GUARDAR</button>
                <button type="button" id="cmdModificar" class="btn green">MODIFICAR</button>
                <button type="button" id="cmdCancelar" class="btn green">CANCELAR</button>
                <button type="button" id="cmdEliminar" class="btn green">ELIMINAR</button>
            </div>
        </div>

   </div>
</section>
</asp:Content>
