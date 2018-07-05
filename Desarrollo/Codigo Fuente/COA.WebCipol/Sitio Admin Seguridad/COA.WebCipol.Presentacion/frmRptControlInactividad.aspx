<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRptControlInactividad.aspx.cs"
    Inherits="COA.WebCipol.Presentacion.frmRptControlInactividad" MasterPageFile="~/EstructuraReportes.Master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContenidoReportViewer">
    <script type="text/javascript">

        $(document).ready(function () {
            $("[id*='txtFechaHasta']").datepicker().attr('readonly', true);
            var fecha = $.datepicker.formatDate('dd/mm/yy', new Date())
            SetTXT('txtFechaHasta', fecha);       
        });     

             
        function HabilitarFiltroFechas() {

            if ($("#rdbPeriodo").is(":checked")) {
                $("#txtFechaHasta").attr("disabled", "disable");
                $("#cboLapso").removeAttr("disabled");
                $("#cboLapso").css("background-color", "white");
            } else {
                $("#cboLapso").attr("disabled", "disable");
                $("#txtFechaHasta").removeAttr("disabled");
                $("#txtFechaHasta").css("background-color", "white");
            }
        }

    </script>
    <div class="container">
        <!-- Titulo de la página -->
        <div class="row">
            <div class="col-md-12 subtitle">
                <h2>CONTROL DE INACTIVIDAD</h2>
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
                                        <div class="row" style="margin: 0 0 22px 0;">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <input type="radio" id="rdbRangoFechas" runat="server" checked="true" groupname="RangoFechas"
                                                        clientidmode="Static" onclick="HabilitarFiltroFechas();" class="col-md-1" />
                                                    <label for="lblFechaDesde" class="col-md-2 control-label">
                                                        Fecha Hasta:</label>
                                                    <input type="text" id="txtFechaHasta" runat="server" class="col-md-2 control-label" clientidmode="Static" />
                                                    <input type="radio" id="rdbPeriodo" runat="server" class="col-md-1" groupname="RangoFechas" clientidmode="Static"
                                                        onclick="HabilitarFiltroFechas();" />
                                                        <label for="lblFechaDesde" class="col-md-2 control-label">
                                                       Lapso de Inactividad:</label>
                                                    <asp:DropDownList ID="cboLapso" runat="server" class="col-md-3 select cbo222 select-no-padding">
                                                        <asp:ListItem Selected="True" Value="MENOR30">Menor o igual de 30 días</asp:ListItem>
                                                        <asp:ListItem Value="MAYOR30">Mayor de 30 días</asp:ListItem>
                                                    </asp:DropDownList>
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
                                        <div class="row" style="margin: 0 0 22px 0;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="alert alert-danger" id="divError" runat="server" Visible="False">
                                                            <asp:Label ID="lblError"  runat="server" />
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
</asp:Content>
