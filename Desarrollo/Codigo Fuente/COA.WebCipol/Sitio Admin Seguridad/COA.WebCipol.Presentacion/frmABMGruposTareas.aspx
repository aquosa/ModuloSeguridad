<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmABMGruposTareas.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMGruposTareas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/jsfrmABMGruposTareas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section>
    <div class="container">
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>GRUPOS</h2>
            </div>
        </div>
        
        <!-- Filtro -->
        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('filtro-grupos-tareas', 'img-grupos-tareas');">
                        <div class="col-md-12">
                            <div class="head filter">
                                <span class="white">FILTRO</span>
                                <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-grupos-tareas" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="filtro-grupos-tareas">
                        <div class="col-md-12">
                            <div class=" form form-filter col-md-12">
                                <div class="row body">
                                    <div class="col-md-12">                                         
                                        <div class="row" style="margin:0 0 5px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="lblTabla" class="col-md-4 control-label">Nombre del Grupo: </label>
                                                    <div class="select col-md-8">
                                                        <div id="divcboGrupo"></div>
                                                    </div>
                                                    <input type="text" class="col-md-7" id="txtNombreGrupo" name="txtNombreGrupo" / maxlength="100">
                                                    <span id="txtNombreGrupoRequerido" class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
										    <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="lblUsusario" class="control-label col-md-4">Nombre del Sistema: </label>
                                                     <div class="select col-md-8">
                                                        <div id="divcboSistema"></div>
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
        <!--Fin Filtro -->

            <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">TAREAS</span>
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
												<h3>TAREAS QUE PERTENECEN AL GRUPO RAIZ</h3>
                                                <div id="lbTareasNoAsignadas"></div>
                                            </div>
                                            <div class="col-xs-2 middle-control">
                                                <button type="submit" class="btn big green" id="btnAsignarTodasTareas" >&raquo;</button>
                                                <button type="submit" class="btn big green" id="btnAsignarTarea" >&gt;</button>
                                                <button type="submit" class="btn big green" id="btnDesasignarTarea" >&lt;</button>
                                                <button type="submit" class="btn big green" id="btnDesasignarTodasTareas">&laquo;</button>
                                            </div>
                                            <div class="col-xs-5" style="padding: 0 15px 0 0;">
												<h3>TAREAS ASIGNADAS AL GRUPO</h3>
                                                <div id="lbTareasAsignadas"></div>
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

        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">GRUPOS</span>
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
												<h3>LISTA DE GRUPOS</h3>
                                                <div id="lbGruposNoExcluyentes"></div>
                                            </div> 
                                            <div class="col-xs-2 middle-control">
                                                <button type="submit" class="btn big green" id="btnAsignarTodosGrupos">&raquo;</button>
                                                <button type="submit" class="btn big green" id="btnAsignarGrupo">&gt;</button>
                                                <button type="submit" class="btn big green" id="btnDesasignarGrupo">&lt;</button>
                                                <button type="submit" class="btn big green" id="btnDesasignarTodosGrupos">&laquo;</button>
                                            </div>
                                            <div class="col-xs-5" style="padding: 0 15px 0 0;">
												<h3>GRUPOS EXCLUYENTES DEL ACTUAL</h3>
                                                <div id="lbGruposExcluyentes"></div>
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
                <button type="button" id="btnNuevo" class="btn green">NUEVO</button>
                <button type="button" id="btnGuardar" class="btn green">GUARDAR</button>
                <button type="button" id="btnModificar" class="btn green">MODIFICAR</button>
                <button type="button" id="btnCancelar" class="btn green">CANCELAR</button>
                <button type="button" id="btnEliminar" class="btn green">ELIMINAR</button>
            </div>
        </div>

    </div>
    <!-- fin content -->
</section>
    <%-- <div class="BordeContenido" align="center">
        <h2 class="Titulo">
            <asp:Label ID="lblTitulo" runat="server">Grupos</asp:Label>
        </h2>
        <table cellpadding="0" cellspacing="0" class="BordeContenidoInterno" id="filtro">
            <tr>
                <td>
                    <div id="divmessage">
                    </div>
                    <table align="center">
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label14" runat="server" Text="Nombre del Grupo " Style="font-weight: 700"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align: left">
                                <div id="divcboGrupo">
                                </div>
                                <input id="txtNombreGrupo" runat="server" type="text" maxlength="100" style="width: 350px;
                                    background-color: #FFE0C1" requerito="ererer" mensaje="mensaje" />
                            </td>
                            <td style="text-align: right">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="Nombre del Sistema " Style="font-weight: 700"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align: left">
                                <div id="divcboSistema">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="BordeContenidoInterno" id="grilla"
            runat="server">
            <tr valign="top">
                <td>
                    <br />
                    <asp:Label ID="Label2" CssClass="EncabezadoTabla" runat="server" Text="Tareas que pertenecen al Grupo RAIZ"
                        Width="400px"></asp:Label>
                    <br />
                    <div id="lbTareasNoAsignadas">
                    </div>
                    <br />
                </td>
                <td valign="middle">
                    <br />
                    <br />  
                    <asp:Button ID="btnAsignarTodasTareas" runat="server" Text="&gt;&gt;" Height="30px"
                        Width="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarTarea" runat="server" Width="30px" Text="&gt;" Height="30px" />
                    <br />
                    <asp:Button ID="btnDesasignarTarea" runat="server" Width="30px" Text="&lt;" Height="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnDesasignarTodasTareas" runat="server" Text="&lt;&lt;" Height="30px"
                        Width="30px" />
                </td>
                <td>
                    <br />
                    <asp:Label ID="Label3" CssClass="EncabezadoTabla" runat="server" Text="Tareas asignadas al Grupo"
                        Width="400px"></asp:Label>
                    <br />
                    <div id="lbTareasAsignadas">
                    </div>
                    <br />
                </td>
            </tr>
            <tr valign="top">
                <td class="style4">
                    <br />
                    <asp:Label ID="Label5" CssClass="EncabezadoTabla" runat="server" Text="Lista de Grupos"
                        Width="400px"></asp:Label>
                    <br />
                    <div id="lbGruposNoExcluyentes">
                    </div>
                    <br />
                </td>
                <td valign="middle">
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarTodosGrupos" runat="server" Text="&gt;&gt;" Height="30px"
                        Width="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarGrupo" runat="server" Width="30px" Text="&gt;" Height="30px" />
                    <br />
                    <asp:Button ID="btnDesasignarGrupo" runat="server" Width="30px" Text="&lt;" Height="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnDesasignarTodosGrupos" runat="server" Text="&lt;&lt;" Height="30px"
                        Width="30px" />
                </td>
                <td class="style4">
                    <br />
                    <asp:Label ID="Label4" CssClass="EncabezadoTabla" runat="server" Text="Grupos excluyentes del actual"
                        Width="400px"></asp:Label>
                    <br />
                    <div id="lbGruposExcluyentes">
                    </div>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td colspan="3">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" align="center" />
                    &nbsp;<input type="button" id="btnGuardar" align="center" runat="server" value="Guardar" />
                    &nbsp;<input type="button" id="btnModificar" align="center" runat="server" value="Modificar" />
                    &nbsp<input type="button" id="btnCancelar" align="center" runat="server" value="Cancelar" />
                    &nbsp;<input type="button" id="btnEliminar" align="center" runat="server" value="Eliminar" />
                </td>
            </tr>
        </table>
    </div>--%>
</asp:Content>
