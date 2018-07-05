﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRptRolesDetalle.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.frmRptRolesDetalle" MasterPageFile="~/EstructuraReportes.Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContenidoReportViewer">
    <section id="section-rptRolesDetalle">
        <div class="container">
            
            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>DESCRIPCIÓN DE ROLES</h2>
                </div>
            </div>
            <!-- Fin titulo de la página -->

            <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                    	<div class="row row-filter">
                            <div class="col-md-12">
                            	<div class="head filter">
                                	<span class="white">FILTRO</span>
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
                                                        <label for="lblFechaDesde" class="col-md-2 control-label">ROL:</label>
                                                        <asp:DropDownList ID="cboRol" runat="server" class="col-md-10 select cbo222 select-no-padding"> </asp:DropDownList>
                                                                     </div>
                                                </div>
                                            </div>
                                         <div class="row" style="margin:0 0 10px 0;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Button ID="cmdGenerar" runat="server" Text="GENERAR" class="btn green button-filtrar"
                                                        OnClick="cmdGenerar_Click"></asp:Button>   
                                                </div>
                                            </div>
                                        </div>
                                            <div class="row" style="margin:0 0 22px 0;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <div class="alert alert-danger" id="divError" runat="server" Visible="False">
                                                            <asp:Label ID="lblError"  runat="server" Font-Size="14pt" />
                                                        </div>
                                                        <div class="alert alert-info" id="divInfo" runat="server" Visible="False">
                                                            <asp:Label ID="lblSinDatos"  runat="server"  Visible="False" Text="No se encontraron resultados relacionados con su búsqueda." />
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
            </div>
  </section>
</asp:Content>
