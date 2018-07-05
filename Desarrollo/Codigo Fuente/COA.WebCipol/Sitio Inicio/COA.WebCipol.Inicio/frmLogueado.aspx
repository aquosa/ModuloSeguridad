<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitioSimple.Master" AutoEventWireup="true"
    CodeBehind="frmLogueado.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmLogueado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentSimple" runat="server">
    <script src="Scripts/holder.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jsfrmLogueado.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPaginaSimple" runat="server">
     <section id="sistemas-permitidos">
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
                                                	<div class="alert alert-success" style="cursor:pointer; cursor: hand"  onclick="javascript:page.redirectLogin();">
                                                    	<span class="icon"></span>
                                                    	<span>El usuario se ha logueado correctamente.</span>
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
