
//Inicialización de la página
$(function () {
    page.init();
});

var page = {
    CambiarDominio: null,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        appCIPOLPRESENTACION.urlinicio = 'frmPrincipal.aspx';
        //Lee el valor del querystring.        
        page.CambiarDominio = $.QueryString["CambiarDominio"];
        if (page.CambiarDominio == null)
            page.CambiarDominio = 'true';

        page.CargarPagina();

        $("[id$='cmdAceptar']").button().click(function () { page.clickAceptar(true); });
        $("[id$='cmdCancelar']").button().click(function () { page.clickCancelar(); });
    },
    CargarPagina: function () {
        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/ws/wsTipoSeguridad.asmx/RecuperarTipoSeguridadCarga",
            data: "{CambiarDominio:'" + page.CambiarDominio + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                page.cargarDatos(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowAlertDanger('Error al recuperar la carga de los datos. ' + xhr.responseJSON);
            },
            complete: function () {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    cargarDatos: function (data) {
        appCIPOLPRESENTACION.trxdata = data.elemento;
        page.setDatos();

        if (data.EstadoOptCIPOL)
            $("[id*='optMIXTA']").removeAttr("disabled");
        else
            appCIPOLPRESENTACION.SetAtributo("[id*='optMIXTA']", "disabled", "disable");
        if (data.EstadoOptIntegrada)
            $("[id*='optIntegrada']").removeAttr("disabled");
        else
            appCIPOLPRESENTACION.SetAtributo("[id*='optIntegrada']", "disabled", "disable");
    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        SetTXT('txtNombreDominio', app.NombreDominio);
        SetTXT('txtUsuario', app.Usuario);
        SetTXT('txtNombreOrganizacion', app.NombreOrganizacion);
        $("[id*='optMIXTA']").prop('checked', app.optCIPOL);
        $("[id*='optIntegrada']").prop('checked', app.optIntegrada);
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        app.NombreDominio = GetTXT('txtNombreDominio');
        app.Usuario = GetTXT('txtUsuario');
        app.NombreOrganizacion = GetTXT('txtNombreOrganizacion');
        app.optCIPOL = $("[id*='optMIXTA']").prop('checked');
        app.optIntegrada = $("[id*='optIntegrada']").prop('checked');
        app.Clave = GetTXT('txtClave');
    },
    clickAceptar: function (preguntar) {
        appCIPOLPRESENTACION.BloquearUI();
        page.getDatos();
        var pdata = appCIPOLPRESENTACION.trxdata;
        pdata.validar = preguntar;
        pdata.validarDominio = !preguntar;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/ws/wsTipoSeguridad.asmx/AdministrarTipoSeguridad",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    if (data.d.preguntar) {
                        //preguntar.
                        if (confirm(data.d.MensajeError)) {
                            page.clickAceptar(false);
                        }
                    } else {
                        bootbox.alert(data.d.MensajeServicio, function () { page.Redirect(); });
                    }
                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                bootbox.alert('Error al recuperar la carga de los datos.');
            },
            complete: function () {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    Redirect: function () {
        window.location.href = 'frmPrincipal.aspx';
    },
    clickCancelar: function () {
        window.location.href = 'frmPrincipal.aspx';
    }
}