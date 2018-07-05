<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmParametros.aspx.cs" Inherits="COA.Cipol.Presentacion.frmParametros" %>

<asp:Content ID="ContenidoHEAD" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/jsfrmParametros.js"></script>
    <script type="text/javascript" src="Scripts/jsValidaciones.js"></script>
</asp:Content>
<asp:Content ID="ContenidoPrincipal" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="politicas-seguridad">
    	<div class="container">
        	<div class="row">
                <div class="col-md-12 subtitle">
                    <h2>PARAMETROS QUE DEFINEN LAS POLÍTICAS DE SEGURIDAD</h2>
                </div>
            </div>
			<div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                        <div class="row">
                            <div class="col-md-12">
                                <div class=" form col-md-12">
                                    <div class="row body">
										<div class="col-md-12">
                                            <div class="row">
                                            	<div class="col-md-12">
                                                	<div class="alert alert-warning">
                                                    	<span>Para desactivar opciones de seguridad establezca valores en 0 (cero)</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="divError" runat="server" visible="false" style="margin:20px 0 20px 0;">
                                            	<div class="col-md-12">
                                                	<div class="alert alert-danger">
                                                        <span class="icon"></span>
                                                        <asp:Label ID="lblErrores" runat="server" ></asp:Label>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="row" id="divSuccess" runat="server" visible="false" style="margin:20px 0 20px 0;">
                                            	<div class="col-md-12">
                                                	<div class="alert alert-success">
                                                        <span class="icon"></span>
                                                        <asp:Label ID="lblSuccess" runat="server" ></asp:Label>     
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-10">
                                                	<span class="field-desc">Cantidad de intentos fallidos de inicio de sesión, al cabo del cual se bloquea la cuenta de usuario:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_1" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Cantidad de tiempo, en minutos,a partir del cual se desbloquea la cuenta de usuario:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_2" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Longitud mínima admitida para la contraseña de usuario:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                        <asp:TextBox ID="txtLongMinPass" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Duración máxima de tiempo, en días, de la contraseña actual del usuario:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_4" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Duración mínima de tiempo, en días, de la contraseña actual del usuario:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_5" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Cantidad de días de antelación para avisar al usuario acerca del próximo vencimiento de la contraseña:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_6" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">Cantidad de contraseñas que se almacenan con el objeto de evitar la repetición al renovarlas:</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_7" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row">
                                               <div class="col-md-10">
                                                	<span class="field-desc">
                                                    	Cantidad de días admitidos sin inicio de sesión por parte del usuario:<br>
                                                        (Transcurrido el lapso se bloquea la cuenta)
													</span>
                                                </div>
                                                <div class="col-md-2">
                                                	<div class="form-group">
                                                		<asp:TextBox ID="txtParametros_8" class="col-md-12 aling-right" runat="server" Text="0" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                           </div>
                                           <div class="row" style="margin:0 0 22px 0;">
                                                <div class="col-md-12" style="padding:0;">
                                                	<span>Modo de asignación de tareas y roles: </span>
                                                    <asp:RadioButton ID="optModoPermisivo" runat="server"  GroupName="modo" Text="Permisivo" />
                                                        
                                                    <asp:RadioButton ID="optModoRestrictivo" runat="server" GroupName="modo" Text="Restrictivo" />
                                                </div>
                                        	</div>
                                            <div class="row">
                                                <div class="float" style="margin:0 15px;">
                                                    <span class="field-desc">
                                                        Nivel de contraseña:
                                                    </span>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <div class="select col-xs-5">
                                                             <asp:DropDownList ID="cboNivelPass" class="col-xs-12 select-no-padding" runat="server"></asp:DropDownList>
                                                        </div>
                                                        <span class="col-md-7 text-danger">Para que el cambio referido a Nivel de contraseña tenga efecto, deberá reiniciar el sistema.</span>
                                                    </div>
                                                </div>
											</div>
                                            <div class="row">
                                               <div class="col-md-12" style="text-align:right;">
                                                    <asp:Button ID="cmdGuardar" class="btn green" runat="server" Text="GUARDAR" OnClick="cmdGuardar_Click" />
                                                    <%--<asp:Button ID="cmdCerrar" class="btn grey" runat="server" Text="CERRAR" OnClientClick="return confirm('¿Está seguro que desea cerrar? Se Perderán todas las modificaciones que no han sido guardadas.');"/>--%>
                                                    <input  type="button" ID="cmdCerrar" class="btn grey" value="CANCELAR"/>
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
	</section>
</asp:Content>
