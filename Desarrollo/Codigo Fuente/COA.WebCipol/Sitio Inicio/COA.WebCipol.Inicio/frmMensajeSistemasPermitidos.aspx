<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraSitioSimple.Master" AutoEventWireup="true" CodeBehind="frmMensajeSistemasPermitidos.aspx.cs" Inherits="COA.WebCipol.Presentacion.frmMensajeSistemasPermitidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentSimple" runat="server">
<script language="javascript" type="text/javascript" src="js/jsfrmMensajeSistemasPermitidos.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPaginaSimple" runat="server">
  <div align="center">
        <div class="MensajeCerrar">        
        <asp:Label ID="lblMENSAJE" runat="server" ></asp:Label>
        </div>
        <asp:Button ID="btnOK" runat="server" Text="Aceptar" />
    </div>
</asp:Content>
