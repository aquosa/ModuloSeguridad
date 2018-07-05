<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmNoDisponeDePermiso.aspx.cs" MasterPageFile="~/EstructuraSitio.Master"
Inherits="COA.WebCipol.Presentacion.frmNoDisponeDePermiso" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dialog").dialog({ autoOpen: true,
                postition: "top",
                show: "blind", hide: "explode",
                modal: false
            });
        })

        function abrirModal() {
            $("#dialog").dialog("open");
        }
    </script>

 <div id="contenedorLista">
        <div id="dialog" title="Acceso denegado">
            <p class="form_texto">
                No dispone de permisos para esta función.</p>
        </div>
    </div>
</asp:Content>

