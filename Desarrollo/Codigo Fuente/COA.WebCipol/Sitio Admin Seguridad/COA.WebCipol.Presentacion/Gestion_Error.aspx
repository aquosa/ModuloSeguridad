<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitioSimplePadre.Master" AutoEventWireup="true" CodeBehind="Gestion_Error.aspx.cs" Inherits="COA.WebCipol.Presentacion.Gestion_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function slideSidebar(idBox, idImg) {
        debugger;
        $("#" + idBox).slideToggle();
        var atrimg = $("#" + idImg).attr("src");
        if (atrimg == "Imagenes/menos.png") {
            $("#" + idImg).attr("src", "Imagenes/mas.png");
        } else {
            $("#" + idImg).attr("src", "Imagenes/menos.png");
        }
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
 <section id="pagina-error">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <div class="block box1">
                    <div class="row row-filter" onclick="javascript:slideSidebar('divMensaje, 'imgMensaje');">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">MENSAJE</span>
                                <div style="float:right;margin-top: -3px"><img src="Imagenes/menos.png"  id="imgMensaje" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divMensaje">
                        <div class="col-md-12">
                            <div class="body">
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtOrigen" runat="server" Rows="4" TextMode="MultiLine" Columns="155" ReadOnly="True"></asp:TextBox>
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
                    <div class="row row-filter" onclick="javascript:slideSidebar('divOrigen, 'imgOrigen');">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">ORIGEN</span>
                                <div style="float:right;margin-top: -3px"><img src="Imagenes/menos.png"  id="imgOrigen" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divOrigen">
                        <div class="col-md-12">
                            <div class="body">
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtMensaje" runat="server" Rows="4" TextMode="MultiLine" Columns="155" ReadOnly="True"></asp:TextBox>
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
                    <div class="row row-filter" onclick="javascript:slideSidebar('divStackTrac, 'imgDatos');">
                        <div class="col-md-12">
                            <div class="head">
                                <span class="white">DATOS TECNICOS</span>
                                <div style="float:right;margin-top: -3px"><img src="Imagenes/menos.png"  id="imgDatos" alt=""/></div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divStackTrac">
                        <div class="col-md-12">
                            <div class="body">
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtStackTrace" runat="server" Rows="10" TextMode="MultiLine" Columns="155" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="text-align:center;">
            <div class="col-md-12">
                <asp:Button ID="cmdContinuar" runat="server" class="btn green" Text="CONTINUAR" onclick="cmdContinuar_Click" />
            </div>
        </div>

    </div>
</section>  
</asp:Content>
