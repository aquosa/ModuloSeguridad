/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        ShowAlertDanger('Error Inesperado: No se pudieron cargar los datos.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});
//function CaracteresExtranios() {
//    if (event.keyCode == 39)
//        event.returnValue = false;
//};
var activarArea = function (pIDSistema, nombre) {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ActivarArea",
            data: "{Id : " + pIDSistema + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'El área <strong>\"' + unescape(nombre) + '\"</strong> ha sido activada correctamente.';
                    bootbox.alert(msg, function () { $("[id$='btnFiltro']").click(); });
                }
                else {
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    } catch (e) {
        ShowAlertDanger('Error Inesperado:  No se activó el area.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
};

var eliminarArea = function (pIDSistema, nombre) {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarArea",
            data: "{Id : " + pIDSistema + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'El área <strong>\"' + unescape(nombre) + '\"</strong> ha sido dada de baja correctamente.';
                    bootbox.alert(msg, function () { $("[id$='btnFiltro']").click(); });
                }
                else {
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            },
            complete: function () {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    } catch (e) {
        ShowAlertDanger('Error Inesperado: No pudo dar de baja el área.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
};

var page = {
    update: true,
    validarCerrar: true,
    haydatosengrilla: false,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga el elemento base.
        page.CargarElementoBase();
        //Configuración Inicial
        page.ConfiguracionInicial();

        $('#dialogAltaEdit').dialog({
            autoOpen: false,
            resizable: false,
            modal: true
        });

        $("[id*='btnNuevo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.update = false; page.AgregarDialog();
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoAreaBase",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                //[MiguelP]         28/10/2014      GCP - Cambios 15598
                appCIPOLPRESENTACION.trxdata = data.d.objBase;
                appCIPOLPRESENTACION.trxFiltro = data.d.objFiltro;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    CargarElemento: function (IdElemento) {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoArea",
            data: "{Id:'" + IdElemento + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                appCIPOLPRESENTACION.trxdata = data.d;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Activar: function (pIDSistema, nombre) {
        var msg = "Va a activar el área <strong>\"" + unescape(nombre) + "\"</strong>. ¿Desea Continuar?";
        bootbox.confirm(msg, function (result) {
            if (result)
                activarArea(pIDSistema, nombre)
        });
    },
    Eliminar: function (pIDSistema, nombre) {
        var msg = "Va a dar de baja el área <strong>\"" + unescape(nombre) + "\"</strong>. ¿Desea Continuar?";
        bootbox.confirm(msg, function (result) {
            if (result)
                eliminarArea(pIDSistema, nombre);
        });
    },
    GuardarDialog: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.getDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarArea",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    bootbox.alert("Los datos han sido guardados", function () {
                        if (page.haydatosengrilla) {
                            $("[id$='btnFiltro']").click();
                        }
                        $('#dialogAltaEdit').dialog('close');
                    });
                }
                else {
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },

    CancelarDialog: function () {
        var msg = "¿Está seguro que desea cancelar? Se perderán todas las modificaciones que no han sido guardadas.";
        bootbox.confirm(msg, function (result) {
            if (result) {
                $('#dialogAltaEdit').dialog('close');
            }
        });
    },
    ConsultarDialog: function (pID) {
        page.update = true;
        //Limpia los campos.
        page.LimpiarCampos();
        page.estadoscontrolesedicion(false);
        //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
        page.CargarElemento(pID);

        //return false;
        //Setea el valor a update.
        var app = appCIPOLPRESENTACION.trxdata;
        app.update = true;
        //Carga los datos en pantalla.
        page.setDatos();


        //Inicializa el textbox como fecha.
        $('#dialogAltaEdit').dialog({
            buttons: {
                "CERRAR": function () { $('#dialogAltaEdit').dialog('close'); }
            }
        });
        //Abre la ventana de dialogo.
        $('#dialogAltaEdit').dialog('open');
        return false;
    },
    ModificarDialog: function (pID) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = true;
            //Limpia los campos.
            page.LimpiarCampos();
            page.estadoscontrolesedicion(true);
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pID);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;

            if (app.BAJA != null && app.BAJA == "S") {
                bootbox.alert('No se puede modificar este registro porque está dado de baja.');
                return false;
            }
            //Carga los datos en pantalla.
            page.setDatos();
            //Inicializa el textbox como fecha.
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "GUARDAR": function () {
                        appCIPOLPRESENTACION.BloquearUI();
                        try {
                            page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado.' );
                        } finally {
                            appCIPOLPRESENTACION.DesBloquearUI();
                        }
                    },
                    "CANCELAR": function () { page.CancelarDialog(); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    AgregarDialog: function () {
        page.LimpiarCampos();
        var app = appCIPOLPRESENTACION.trxdata;
        app.update = false;
        page.estadoscontrolesedicion(true);
        //Inicializa el textbox como fecha.
        $('#dialogAltaEdit').dialog({
            buttons: {
                "GUARDAR": function () {
                    appCIPOLPRESENTACION.BloquearUI();
                    try {
                        //page.validarCerrar = false;
                        page.GuardarDialog();
                    } catch (e) {
                        ShowAlertDanger('Error Inesperado.');
                    } finally {
                        appCIPOLPRESENTACION.DesBloquearUI();
                    }
                },
                "CANCELAR": function () { page.CancelarDialog(); }
            }
        });

        $('#dialogAltaEdit').dialog('open');
    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        //Setea el campo Id
        SetTXT('txtArea', app.NOMBREAREA);
        SetTXT('txtResponsable', app.RESPONSABLE);
        SetTXT('txtCargoResp', app.CARGORESPONSABLE);
        SetTXT('txtComentarios', app.COMENTARIOS)
        //Uso Ficticia
        if (app.blnFICTICIA)
            $("[id*='chkFicticia']").prop('checked', true);
        else
            $("[id*='chkFicticia']").prop('checked', false);

    }, getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        //Setea el campo Id
        app.NOMBREAREA = GetTXT('txtArea');
        app.RESPONSABLE = GetTXT('txtResponsable');
        app.CARGORESPONSABLE = GetTXT('txtCargoResp');
        app.COMENTARIOS = GetTXT('txtComentarios');
        //Uso Ficticia
        app.blnFICTICIA = $("[id*='chkFicticia']").prop('checked');

    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
    },
    LimpiarCampos: function () {
        //Setea el campo Id
        SetTXT('txtArea', '');
        SetTXT('txtResponsable', '');
        SetTXT('txtCargoResp', '');
        SetTXT('txtComentarios', '')
        $("[id*='chkFicticia']").prop('checked', false);


    },
    LimpiarFiltros: function () {
        //Limpieza de los filtros.
        SetTXT('txtFiltro', '');
        SetDDL('cboEstado', ' ');
        page.EstadosControlesFiltro(true);
        page.haydatosengrilla = false;
    },
    EstadosControlesFiltro: function (estado) {
        if (estado) {
            $("[id$='txtFiltro']").removeAttr("disabled");
            $("[id$='cboEstado']").removeAttr("disabled");
            $("[id$='btnFiltro']").removeAttr("disabled");
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltro']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboEstado']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltro']", "disabled", "disable");
        }
    },
    estadoscontrolesedicion: function (estado) {
        if (estado) {

            //Setea el campo Id
            $("[id$='txtArea']").removeAttr("disabled");
            $("[id$='txtResponsable']").removeAttr("disabled");
            $("[id$='txtCargoResp']").removeAttr("disabled");
            $("[id$='txtComentarios']").removeAttr("readOnly");
            //Uso Ficticia
            $("[id$='chkFicticia']").removeAttr("disabled");
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtArea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtResponsable']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtCargoResp']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtComentarios']", "readOnly", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='chkFicticia']", "disabled", "disable");
        }
    }
}















