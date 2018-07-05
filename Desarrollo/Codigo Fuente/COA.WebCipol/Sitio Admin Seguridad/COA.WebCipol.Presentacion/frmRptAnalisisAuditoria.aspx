<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRptAnalisisAuditoria.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmRptAnalisisAuditoria"
 MasterPageFile="~/EstructuraSitioSimplePadre.Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="CuerpoPagina">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divDatos" runat="server" visible="True" style="height: 650px; text-align: center;">
        <div style="margin: 10px 0px 0px 0px;">
             <div class="alert alert-danger" id="divError" runat="server" Visible="False">
                <asp:Label ID="lblError"  runat="server" Font-Size="14pt" />
            </div>
            <div class="alert alert-info" id="divInfo" runat="server" Visible="False">
                <asp:Label ID="lblSinDatos"  runat="server"  Visible="False" Text="No se encontraron resultados relacionados con su búsqueda." />
            </div>
        </div>
    </div>
</asp:Content>
