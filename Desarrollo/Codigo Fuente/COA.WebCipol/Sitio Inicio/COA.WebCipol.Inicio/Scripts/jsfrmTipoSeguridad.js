
//Inicialización de la página
$(function () {
    debugger;
    page.init();
});

var page = {
    CambiarDominio: null,
    init: function () {
        appCIPOL.onsubmit = false;
        appCIPOL.urlinicio = 'frmSistemasPermitidos.aspx';
        //Lee el valor del querystring.        
        page.CambiarDominio = $.QueryString["CambiarDominio"];
        if (page.CambiarDominio == null)
            page.CambiarDominio = 'true';
        debugger;
        page.CargarPagina();

        $("[id$='cmdAceptar']").button().click(function () { return page.clickAceptar(true); });
        $("[id$='cmdCancelar']").button().click(function () { page.clickCancelar(); });
    },
    CargarPagina: function () {
        debugger;
        appCIPOL.BloquearUI();
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
                appCIPOL.DesBloquearUI();
            }
        });
    },
    cargarDatos: function (data) {
        appCIPOL.trxdata = data.elemento;
        page.setDatos();

        if (data.EstadoOptCIPOL)
            $("[id*='optMIXTA']").removeAttr("disabled");
        else
            appCIPOL.SetAtributo("[id*='optMIXTA']", "disabled", "disable");
        if (data.EstadoOptIntegrada)
            $("[id*='optIntegrada']").removeAttr("disabled");
        else
            appCIPOL.SetAtributo("[id*='optIntegrada']", "disabled", "disable");
    },
    setDatos: function () {
        var app = appCIPOL.trxdata;
        if (page.CambiarDominio == 'true') {
            SetTXT('txtNombreDominio', app.NombreDominio);
            SetTXT('txtUsuario', app.Usuario);
            SetTXT('txtNombreOrganizacion', app.NombreOrganizacion);
            $("[id*='optMIXTA']").prop('checked', app.optCIPOL);
            $("[id*='optIntegrada']").prop('checked', app.optIntegrada);
        }else
            $("[id*='optMIXTA']").prop('checked', true);
    },
    getDatos: function () {
        var app = appCIPOL.trxdata;
        app.NombreDominio = GetTXT('txtNombreDominio');
        app.Usuario = GetTXT('txtUsuario');
        app.NombreOrganizacion = GetTXT('txtNombreOrganizacion');
        app.optCIPOL = $("[id*='optMIXTA']").prop('checked');
        app.optIntegrada = $("[id*='optIntegrada']").prop('checked');
        app.Clave = GetTXT('txtClave');
    },
    clickAceptar: function (preguntar) {
        appCIPOL.BloquearUI();
        page.getDatos();
        var pdata = appCIPOL.trxdata;
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
                        bootbox.confirm(data.d.MensajeError, function (result) {
                            if (result)
                                page.clickAceptar(false);
                        });
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
                appCIPOL.DesBloquearUI();
            }
        });
    },
    Redirect: function () {
        window.location.href = 'frmSistemasPermitidos.aspx';
    },
    clickCancelar: function () {
        window.location.href = 'frmSistemasPermitidos.aspx';
    }
}