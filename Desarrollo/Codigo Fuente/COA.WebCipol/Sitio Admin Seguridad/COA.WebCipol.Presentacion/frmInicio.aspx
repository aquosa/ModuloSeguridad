<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmInicio.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.frmInicio" MasterPageFile="~/EstructuraSitioSimplePadre.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <section id="inicio">
        <div class="container">
             
             <div class="row">
                <div class="col-md-12 subtitle">
                    <h2></h2>
                </div>
            </div>

           <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                        <div class="row">
                            <div class="col-md-12">
                                <form id="politicas-seguridad" name="politicas-seguridad" class="col-md-12" role="form" enctype="multipart/form-data" action="">
                                    <div class="row body">
										<div class="col-md-12">
                                            <div class="row" style="text-align:center;">
                                            	<div class="col-md-12">
                                                	<div class="alert alert-info">
                                                    	<span class="icon"></span>
                                                        <asp:Label id="mensajesession" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                    	</div>
                    </div>
                </div>
            </div>
         </div>
    </section>


</asp:Content>
