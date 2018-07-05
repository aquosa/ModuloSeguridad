<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true" CodeBehind="frmABMSistemasBloqueados.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMSistemasBloqueados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript" src="js/jsfrmABMSistemasBloqueados.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
<section id="sistemas">
    	<div class="container">
        	<div class="row">
                <div class="col-md-12 subtitle">
                    <h2>SISTEMAS BLOQUEADOS</h2>
                </div>
            </div>

			<div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                    	<div class="row">
                            <div class="col-md-12">
                            	<div class="head">
                                	<span class="white">SISTEMAS</span>
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
													<h3>DESBLOQUEADOS</h3>
                                                     <div id="lbSiestemasDesbloqueados"></div>
                                                </div>  
                                                <div class="col-xs-2 middle-control">
                                                	<button type="submit" id="btnAsignarSistemasTodos" class="btn big green">&raquo;</button>
                                                    <button type="submit" id="btnAsignarSistema" class="btn big green">&gt;</button>
                                                    <button type="submit" id="btnDesasignarSistema" class="btn big green">&lt;</button>
                                                    <button type="submit" id="btnDesasignarSistemasTodos" class="btn big green">&laquo;</button>
                                                </div>
                                                <div class="col-xs-5" style="padding: 0 15px 0 0;">
													<h3>BLOQUEADOS</h3>
                                                    <div id="lbSistemasBloqueados"></div>
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
                                	<span class="white">USUARIOS</span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 usuarios">
                                <div class="col-md-12 form form-filter">
                                    <div class="row body">
										<div class="col-md-12">
                                            <div class="row" style="margin:0;">
                                                <div class="col-xs-5" style="padding: 0 0 0 15px;">
													<h3>BLOQUEADOS</h3>
                                                    <div id="lbUsuariosBloqueados"></div>
                                                </div>    
                                                <div class="col-xs-2 middle-control">
                                                	<button type="submit" id="btnAsignarUsuariosTodos" class="btn big green">&raquo;</button>
                                                    <button type="submit" id="btnAsignarUsuario" class="btn big green">&gt;</button>
                                                    <button type="submit" id="btnDesasignarUsuario" class="btn big green">&lt;</button>
                                                    <button type="submit" id="btnDesasignarUsuariosTodos" class="btn big green">&laquo;</button>
                                                </div>
                                                <div class="col-xs-5" style="padding: 0 15px 0 0;">
													<h3>DESBLOQUEADOS</h3>
                                                     <div id="lbUsuariosDesbloqueados"></div>
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
                    <button type="button" id="btnGuardar" class="btn green">GUARDAR</button>
                    <button type="button" id="btnModificar" class="btn green">MODIFICAR</button>
                    <button type="button" id="btnCancelar" class="btn green">CANCELAR</button>
                </div>
            </div>
            
        </div>
    </section>
<%--<div class="BordeContenido" align="center">
        <h2 class="Titulo">
            <asp:Label ID="lblTitulo" runat="server">Sistemas Bloqueados</asp:Label>
        </h2>
        <table cellpadding="0" cellspacing="0" class="BordeContenidoInterno" id="grilla"
            runat="server">
            <tr valign="top">
                <td>
                    <br />
                    <asp:Label ID="Label2" CssClass="EncabezadoTabla" runat="server" Text="Sistemas Desbloqueados"
                        Width="300px"></asp:Label>
                    <br />
                    <div id="lbSiestemasDesbloqueados">
                    </div>
                    <br />
                </td>
                <td valign="middle">
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarSistemasTodos" runat="server" Text="&gt;&gt;" Height="30px"
                        Width="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarSistema" runat="server" Width="30px" Text="&gt;" Height="30px" />
                    <br />
                    <asp:Button ID="btnDesasignarSistema" runat="server" Width="30px" Text="&lt;" Height="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnDesasignarSistemasTodos" runat="server" Text="&lt;&lt;" Height="30px"
                        Width="30px" />
                </td>
                <td>
                    <br />
                    <asp:Label ID="Label3" CssClass="EncabezadoTabla" runat="server" Text="Sistemas Bloqueados"
                        Width="300px"></asp:Label>
                    <br />
                    <div id="lbSistemasBloqueados">
                    </div>
                    <br />
                </td>
            </tr>
            <tr valign="top">
                <td class="style4">
                    <br />
                    <asp:Label ID="Label5" CssClass="EncabezadoTabla" runat="server" Text="Lista de Usuarios Bloqueados"
                        Width="300px"></asp:Label>
                    <br />
                    <div id="lbUsuariosBloqueados">
                    </div>
                    <br />
                </td>
                <td valign="middle">
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarUsuariosTodos" runat="server" Text="&gt;&gt;" Height="30px"
                        Width="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnAsignarUsuario" runat="server" Width="30px" Text="&gt;" Height="30px" />
                    <br />
                    <asp:Button ID="btnDesasignarUsuario" runat="server" Width="30px" Text="&lt;" Height="30px" />
                    <br />
                    <br />
                    <asp:Button ID="btnDesasignarUsuariosTodos" runat="server" Text="&lt;&lt;" Height="30px"
                        Width="30px" />
                </td>
                <td class="style4">
                    <br />
                    <asp:Label ID="Label4" CssClass="EncabezadoTabla" runat="server" Text="Lista de Usuarios Desbloqueados"
                        Width="300px"></asp:Label>
                    <br />
                    <div id="lbUsuariosDesbloqueados">
                    </div>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td colspan="3">
                    <input type="button" ID="btnGuardar" align="center" runat="server" value="Guardar"  />
                    &nbsp;<input type="button" ID="btnModificar" align="center" runat="server" value="Modificar" />
                    &nbsp<input type="button" ID="btnCancelar" align="center" runat="server" value="Cancelar"  />
                </td>
            </tr>
        </table>
    </div>--%>
</asp:Content>

