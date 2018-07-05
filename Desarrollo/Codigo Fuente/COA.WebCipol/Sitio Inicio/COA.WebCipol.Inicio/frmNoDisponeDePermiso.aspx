<%@ Page Language="C#" MasterPageFile="~/EstructuraSitioSimplePadre.Master" AutoEventWireup="true" CodeBehind="frmNoDisponeDePermiso.aspx.cs" Inherits="COA.WebCipol.Inicio.frmNoDisponeDePermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript" src="js/jsfrmMensaje.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CuerpoPagina" runat="server">
  <div align="center">
        <div class="MensajeCerrar">        
        <asp:Label ID="lblMENSAJE" runat="server" ></asp:Label>
        </div>
        <asp:Button ID="btnOK" runat="server" Text="Aceptar" />
    </div>
</asp:Content>