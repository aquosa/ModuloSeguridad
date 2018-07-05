<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/EstructuraSitio.Master"
    CodeBehind="frmABMAreas.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmABMAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Areas/jsfrmABMAreas.js"></script>
    <script type="text/javascript" src="js/Areas/JQueryGridAreas.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="areas">
        <div class="container">
            
            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>ADMINISTRACIÓN DE ÁREAS</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                    	<div class="row row-filter" onclick="javascript:slideSidebar('filtro-areas', 'img-filtro-areas');">
                            <div class="col-md-12">
                            	<div class="head filter">
                                	<span class="white">FILTRO</span>
                                    <div style="float:right;margin-top: -2px"><img src="Imagenes/menos.png"  id="img-filtro-areas" alt=""/></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="filtro-areas">
                             <div class="col-md-12">
                                <div class="form form-filter col-md-12">
                                    <div class="row body">
                                        <div class="col-md-12">
                                           <div class="row" style="margin:0 0 22px 0;">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblFechaDesde" class="col-md-5 control-label">Áreas que comienzan con: </label>
                                                        <input type="text" class="col-md-7" id="txtFiltro"  name="txtFiltro" maxlength="30"/>
                                                    </div>
                                                </div>
											    <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label for="lblFechaHasta" class="col-md-1 control-label">Baja: </label>
                                                        <div class="select col-md-10">
                                                            <select id="cboEstado">
                                                                <option value=" ">Todos</option>
                                                                <option value="S">Si</option>
                                                                <option value="N">No</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                          <button  id="btnFiltro" data-bind='click: cmdRecuperarAreas' class="btn green button-filtrar">FILTRAR</button>
                                                         <button  id="btnLimpiarFiltro" data-bind='click: cmdLimpiarFiltro' class="btn green button-filtrar">LIMPIAR</button>
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
            <div class="row ">
            	<div class="col-md-12">
                    <div class="block box1 table-responsive">
                    	<div class="row">
                            <div class="col-xs-12">
                            	<div class="head">
                                	<span class="white">ÁREAS</span>
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
                	<button type="button" id="btnNuevo" class="btn green">NUEVO</button>
                </div>
            </div>           

            <!-- PopUP-->
             <div id="dialogAltaEdit" class="ui-dialog">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box1">
                            <div class="row">
                                <div class="head">
                                    <span class="white">ÁREA</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form col-md-12">
                                        <div class="row">
                                            <div class="col-xs-8 border">
                                                <div class="form-group">
                                                    <label for="nombreApellido" class="col-xs-2 control-label">Área: </label>
                                                    <input type="text" value="" class="col-xs-7" id="txtArea" maxlength="30" name="nombreApellido"/>
                                                    <span class="col-xs-1 required">*</span>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkFicticia" runat="server" />
                                                    <label class="col-xs-6 control-label">Ficticia: </label>
                                                </div>
                                           </div>
                                        </div>
                                        <div class="row grey">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtResponsable" class="col-xs-2 control-label">Responsable: </label>
                                                    <input type="text" value="" class="col-xs-8" MaxLength="30" id="txtResponsable" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtCargoResp" class="col-xs-2 control-label">Cargo Responsable: </label>
                                                    <input type="text" value="" class="col-xs-8" MaxLength="30" id="txtCargoResp"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row grey">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtComentarios" class="col-xs-2 control-label">Comentarios: </label>
                                                    <textarea id="txtComentarios" rows="0" cols="0" maxlength="250"  class="col-xs-10" ></textarea>
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
            <!--Fin PopUP-->

        </div>
    </section>
</asp:Content>

