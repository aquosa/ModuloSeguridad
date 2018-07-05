/// <summary>
/// Script asociado a todas las operaciones realizadas en la pantalla "Visor de Sucesos -> Seguridad"
/// </summary>
/// <history>
/// [IvanSa]          [Martes, 04 de octubre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        ShowAlertDanger( 'Error Inesperado: Error al recuperar la carga de los datos.' );
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var page = {
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        page.CargarPagina();
        page.ConfiguracionInicial();

        $("[id$='cboAdministradores']").addClass("col-md-9 select-no-padding");
        $("[id$='cboAfectados']").addClass("col-md-9 select-no-padding");
        $("[id$='cboCodigoMensaje']").addClass("col-md-9 select-no-padding");

        $('#dialogAbrirMensaje').dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            buttons: {
                "Aceptar": function () { $(this).dialog('close'); }
            }
        });

        $("[id$='btnAbrirMensaje']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                return page.AgregarDialog();
            } catch (e) {
                ShowAlertDanger( 'Error Inesperado');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }

        });

        $("[id$='btnImprimir']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ImprimirReporte();
                return false;
            } catch (e) {
                ShowAlertDanger( 'Error Inesperado: No se pudo imprimir el log de sucesos.' );
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }

        });
    },
    ValidarFecha: function () {
        var fechaDesde = new Date($("[id$='txtFechaDesde']").datepicker('getDate'));
        var fechaHasta = new Date($("[id$='txtFechaHasta']").datepicker('getDate'));
        var res = true;

        if (fechaHasta < fechaDesde) {
            ShowAlertDanger('La <strong>FECHA HASTA</strong> debe ser mayor o igual que la <strong>FECHA DESDE<strong>.');
            res = false;
        }
        return res;
    },
    AgregarDialog: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        $('#dialogAbrirMensaje').dialog('open');
    },
    CargarPagina: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarSucesosSeguridadCarga",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    page.cargarDatos(data.d);
                } else {
                    ShowAlertDanger( 'Error al recuperar la carga de los datos.' + data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    cargarDatos: function (data) {
        appCIPOLPRESENTACION.trxdata = data.elemento;
        //Carga el combo de administradores    
        try {
            $Contenedor = $("#reemplazarcomboAdministradores");
            var DatosAImprimir = $.parseJSON(data.jsoncboadministradores);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger( 'Error Inesperado: No se pudieron cargar los usuarios administradores.');
        }
        //Carga el combo de afectados    
        try {
            $Contenedor = $("#reemplazarcomboAfectados");
            var DatosAImprimir = $.parseJSON(data.jsoncboafectado);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger( 'Error Inesperado: No se pudieron cargar los usuarios afectados.');
        }
        //Carga el combo de codigo de mensajes   
        try {
            $Contenedor = $("#remplazarcomboMensajes");
            var DatosAImprimir = $.parseJSON(data.jsoncbocodigomensaje);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger( 'Error Inesperado: No se pudieron cargar los códigos de mensajes.');
        }
    },
    ConfiguracionInicial: function () {
        //todo: cargar ele stado inicial de los controles
        $().maxlength();
        $("[id*='txtFechaDesde']").datepicker().attr('readonly', true);
        $("[id*='txtFechaHasta']").datepicker().attr('readonly', true);
        var fecha = $.datepicker.formatDate('dd/mm/yy', new Date())
        SetTXT('txtFechaDesde', fecha);
        SetTXT('txtFechaHasta', fecha);
    },
    Limpiarfiltros: function () {
        //Limpieza de los filtros.
        var fecha = $.datepicker.formatDate('dd/mm/yy', new Date())
        SetTXT('txtFechaDesde', fecha);
        SetTXT('txtFechaHasta', fecha);
        SetDDL('cboAdministradores', '-98');
        SetDDL('cboAfectados', '-98');
        SetDDL('cboCodigoMensaje', '-1');
    },
    EstadosControlesFiltro: function (estado) {
        if (estado) {
            $("[id*='txtFechaDesde']").removeAttr("disabled");
            $("[id*='txtFechaHasta']").removeAttr("disabled");
            $("[id*='cboAdministradores']").removeAttr("disabled");
            $("[id*='cboAfectados']").removeAttr("disabled");
            $("[id*='cboCodigoMensaje']").removeAttr("disabled");
            $("[id*='btnFiltrar']").removeAttr("disabled");
        }
        else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaDesde']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaHasta']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboAdministradores']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboAfectados']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboCodigoMensaje']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltrar']", "disabled", "disable");
        }
    },
    VerDialog: function (fecha, cod, msg, usu, usuAfectado) {
        page.LimpiarCamposDialog();
        $("[id*='lblFechaHora']").text(unescape(fecha));
        $("[id*='lblCodigo']").text(unescape(cod));
        $("[id*='lblMensaje']").text(unescape(msg));
        if (usu != "null") {
            $("[id*='lblUsuarioAdministrador']").text(usu);
        }
        if (usuAfectado != "null") {
            $("[id*='lblUsuarioAfectado']").text(usuAfectado);
        }
        $('#dialogAbrirMensaje').dialog('open');
        return false;
    },
    LimpiarCamposDialog: function () {
        $("[id*='lblFechaHora']").text('');
        $("[id*='lblCodigo']").text('');
        $("[id*='lblMensaje']").text('');
        $("[id*='lblUsuarioAdministrador']").text('');
        $("[id*='lblUsuarioAfectado']").text('');
    },
    ImprimirReporte: function () {
        var fechaDesde = GetTXT('txtFechaDesde');
        var fechaHasta = GetTXT('txtFechaHasta');
        var usuarioAdministrador = GetDDLTxt('cboAdministradores');
        var usuarioAfectado = GetDDLTxt('cboAfectados');
        var codigoMensaje = GetDDLTxt('cboCodigoMensaje');

        var pagina = 'frmRptVisorSucesos.aspx?FechaDesde=' + fechaDesde + '&FechaHasta=' + fechaHasta + '&UsuAdm=' + usuarioAdministrador + '&UsuAfe=' + usuarioAfectado + '&CodMsj=' + codigoMensaje;

        var popup = encodeURI(pagina);

        var wOpen;
        var sOptions;

        sOptions = 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
        sOptions = sOptions + ',outerWidth=' + (screen.availWidth).toString();
        sOptions = sOptions + ',outerHeight=' + (screen.availHeight).toString();
        sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';
        wOpen = window.open('', 'R', sOptions);
        wOpen.location = pagina;
        wOpen.focus();
        wOpen.moveTo(0, 0);
        wOpen.resizeTo(screen.availWidth, screen.availHeight);
        return wOpen;

    }
}