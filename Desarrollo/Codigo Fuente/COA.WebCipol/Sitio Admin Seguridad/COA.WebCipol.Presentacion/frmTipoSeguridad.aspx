<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true"
    CodeBehind="frmTipoSeguridad.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmTipoSeguridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="Scripts/jsfrmTipoSeguridad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="areas">
        <div class="container">
            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>ADMINISTRACIÓN NOMBRE DE DOMINIO</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

                     <!-- Filtro -->
            <div  class="row">
                <div class="col-xs-offset-2 col-xs-8" >
                    <div class="block box1">
                        <div class="row row-filter">
                            <div class="col-md-12">
                                <div class="head">
                                    <span class="white">&nbsp;</span>
                                </div>
                            </div>
                        </div>
                        <div class="row " id="filtro-areas">
                            <div class="col-md-12">
                                <div class="form form-filter col-md-12">
                                    <div class="row body">
                                        <div class="col-md-12">
                                            <div class="row" style="margin: 0 0 0 0;">
                                                <div class="form-group">
                                                    <label class=" col-md-5 control-label">
                                                        Indique el Tipo de Seguridad a Utilizar:
                                                    </label>
                                                    <asp:RadioButton ID="optMIXTA" runat="server" Text="Seguridad de CIPOL o Mixta" GroupName="TipoSeg"
                                                        ClientIDMode="Static" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 12px 0;">
                                                <div class="form-group">
                                                    <div class="col-md-5">
                                                    </div>
                                                    <asp:RadioButton ID="optIntegrada" runat="server" Text="Seguridad Integrada al Dominio de Windows:"
                                                        GroupName="TipoSeg" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 12px 0;">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">
                                                        Nombre del Dominio:
                                                    </label>
                                                    <input type="text" id="txtNombreDominio" class="col-md-7" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 12px 0;">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">
                                                        Usuario:
                                                    </label>
                                                    <input type="text" id="txtUsuario" class="col-md-7" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 12px 0;">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">
                                                        Contraseña:
                                                    </label>
                                                    <input type="password" id="txtClave" class="col-md-7" />
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 0 0 12px 0;">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">
                                                        Nombre de la Organización:
                                                    </label>
                                                    <input type="text" id="txtNombreOrganizacion" class="col-md-7" />
                                                </div>
                                            </div>
                                            <div class="row" style="text-align: center;">
                                                <div class="col-xs-12">
                                                    <input type="button" value="Aceptar" id="cmdAceptar" class="btn green" />
                                                    <input type="button" value="Cancelar" id="cmdCancelar" class="btn green" />
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
        </div>
    </section>
</asp:Content>
