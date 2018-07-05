<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIdgcboAreas.ascx.cs" Inherits="COA.WebCipol.Presentacion.UIControlsHelper.DropDownList.UIdgcboAreas" %>

<asp:DropDownList runat="server" ID="cboAreas" DataTextField="NOMBREAREA" DataValueField="IDAREA" >
    <asp:ListItem Text="Todos" Value="" />
</asp:DropDownList>