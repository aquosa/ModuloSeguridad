<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitio.Master" AutoEventWireup="true" CodeBehind="frmAcercaDe.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmAcercaDe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
<section>
    <div class="container">
            
            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                   
                </div>
            </div>
            <!-- Fin titulo de la página -->
               
            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                    	<div class="row row-filter">
                            <div class="col-md-12">
                            	<div class="head">
                                	<span class="white">ACERCA DE</span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                             <div class="col-md-12">
                                <div class="body">
                                    <div class="col-md-12">
                                        <div class="row" style="margin:0;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <span class="col-xs-1"><b>Descripción: </b></span>
                                                    <asp:Label ID="lblDescripcion" runat="server" CssClass="col-xs-10"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin:0;">
                                            <div class="col-md-12">
                                                 <div class="form-group">
                                                    <span class="col-xs-1"><b>Detalle: </b></span>
                                                    <asp:Label ID="lblDetalle" runat="server" CssClass="col-xs-10"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin:0;">
                                            <div class="col-md-12">
                                                 <div class="form-group">
                                                    <span class="col-xs-1"><b>Cliente: </b></span>
                                                    <asp:Label ID="lblCliente" runat="server"  CssClass="col-xs-10"></asp:Label>
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
