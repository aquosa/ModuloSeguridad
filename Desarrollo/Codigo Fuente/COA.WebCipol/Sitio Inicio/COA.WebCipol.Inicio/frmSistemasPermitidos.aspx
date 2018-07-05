<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSistemasPermitidos.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.frmSistemasPermitidos" MasterPageFile="~/EstructuraSitioSimple.Master" %>

<asp:Content ID="ContenidoHEAD" ContentPlaceHolderID="HeadContentSimple" runat="server">
    <title>Menú de Sistemas Habilitados</title>
    <script type="text/javascript" src="js/jsfrmSistemasPermitidos.js"></script>
    <link href="css/metro-bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/iconFont.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/holder.js" type="text/javascript"></script>
    <style rel="stylesheet" type="text/css">
        .metro .tile .brand .name, .metro .tile .tile-status .name, .metro .tile .brand .label, .metro .tile .tile-status .label, .metro .tile .brand .text, .metro .tile .tile-status .text
        {
            margin: 0;
            line-height: 14px;
            margin-top: -5px;
        }
        
        .tile.green
        {
            background: #009fa1;
        }
        
        .tile-status
        {
            text-align: center;
        }
        
        .metro .tile .tile-content.icon [class*="icon-"], .metro .tile .tile-content.icon img
        {
            margin-top: -38px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            //setInterval(KeepSessionAlive, 300000);
        });
        var count = 0;
        function KeepSessionAlive() {
            count++;
            $.post("/PageBuilders/KeepSessionAlive.ashx", null, function () {
                //$("#result").append("<p>Session " + count + "<p/>");
            });
        }

        function MantenerSession() {
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/MantenerSession",
                data: "",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (data) {
                    count++;
                    //$("#result").append("<p>Session " + count + data.d + "<p/>");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        }

        var URLSesionExpiro = "<%=URLSesionExpiro%>";

    </script>
</asp:Content>
<asp:Content ID="ContenidoPrincipal" ContentPlaceHolderID="CuerpoPaginaSimple" runat="server">
    <section id="sistemas-permitidos">
        <div class="container">
            <div id="result"></div>

            <!-- Titulo de la página -->
            <div class="row">
                <div class="col-md-12 subtitle">
                    <h2>SISTEMAS PERMITIDOS</h2>
                </div>
            </div>

                        <!-- Filtro -->
            <div class="row">
                <div class="col-md-12">
                    <div class="block box1">
                        <div class="row" id="filtro-terminales">
                             <div class="col-md-12">
                                <div class="body">
                                    <div class="col-md-12">
                                       <div class="row" style="margin:0;">
                                            <div class="sistemas-permitidos metro">
                                                <div id="MenuSys" runat="server" style="width: 100%;" align= "center"></div>
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
                	 <input id='iCambiarContrasenia' class="btn green button-filtrar" type="button" value="Cambiar Contraseña" runat="server" />
                     <%--<button  id="iSalir"class="btn green button-filtrar">SALIR</button>--%>
                </div>
            </div>
            <!--Fin Filtro -->
         </div>
    </section>
</asp:Content>
