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
        ShowAlertDanger('Error Inesperado: Al recuperar el elemento. '+e);
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});
//function CaracteresExtranios() {
//    if (event.keyCode == 39)
//        event.returnValue = false;
//};
var eliminarTarea = function (pIDSistema, nombre) {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarTareaPrimitiva",
            data: "{Id : " + unescape(pIDSistema) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'La Tarea <strong>\"' + unescape(nombre) + '\"</strong> fue eliminada correctamente.';
                    bootbox.alert(msg, function () {
                        $("[id$='btnFiltro']").click();
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
    } catch (e) {
        ShowAlertDanger('Error Inesperado: No se eliminó la tarea.' + e);
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    };
};

var page = {
    validarCerrar: true,
    update: true,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga el elemento base.
        page.CargarElementoBase();
        //Carga Areas.
        page.CargarComboSistema();
        page.CargarComboSistemaFiltro();

        page.ConfiguracionInicial();

        $("[id$='cboSistemafiltro']").addClass("col-md-8 select-no-padding");
        $("[id$='cboSistemaCarga']").addClass("col-md-8 select-no-padding");

        $('#dialogAltaEdit').hide();
        $('#dialogAltaEdit').dialog({
            autoOpen: false,
            resizable: false,
            modal: true,
            create: function () { page.validarCerrar = true; }
        });

        $("[id$='btnNuevo']").button().click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.update = false;
                return page.AgregarDialog();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id$='btnVerificarTarea']").button().click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.VerificarTarea();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: No se pudo verificar la tarea.' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTareaPrimitivaBase",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                appCIPOLPRESENTACION.trxdata = data.d;
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
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTareaPrimitiva",
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
    CargarComboSistemaFiltro: function () {
        //Realiza la búsqueda de los datos y los visualiza en ua grilla.
        $Contenedor = $("#reemplazarcbosisitemafiltro");
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarCboSistema",
            data: "{IdControl:'cboSistemafiltro'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                var DatosAImprimir = $.parseJSON(data.d);
                //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
                //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
                //entities y obtener el código HTML original.
                DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();

                $Contenido = $(DatosAImprimir.Lista);
                $Contenedor.children().remove(); //TODO
                var $tablasEnContenido = $Contenido.find("table");
                //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
                //no quede código HTML sucio o incorrecto.
                if ($tablasEnContenido.length > 0) {
                    $Contenido.find("form").children().unwrap();
                    $Contenido.css("list-style-type", "none");
                    $Contenedor.append($Contenido);
                }
                else {
                    //Si el contenido no contiene tablas, asumo que el WS me devolvió
                    //código HTML correcto y listo para insertarse.
                    $Contenedor.append(DatosAImprimir.Lista);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    CargarComboSistema: function () {
        //Realiza la búsqueda de los datos y los visualiza en ua grilla.
        $Contenedor = $("#reemplazarcbosisitema");
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarCboSistema",
            data: "{IdControl:'cboSistemaCarga'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                var DatosAImprimir = $.parseJSON(data.d);
                //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
                //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
                //entities y obtener el código HTML original.
                DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();

                $Contenido = $(DatosAImprimir.Lista);
                $Contenedor.children().remove(); //TODO
                var $tablasEnContenido = $Contenido.find("table");
                //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
                //no quede código HTML sucio o incorrecto.
                if ($tablasEnContenido.length > 0) {
                    $Contenido.find("form").children().unwrap();
                    $Contenido.css("list-style-type", "none");
                    $Contenedor.append($Contenido);
                }
                else {
                    //Si el contenido no contiene tablas, asumo que el WS me devolvió
                    //código HTML correcto y listo para insertarse.
                    $Contenedor.append(DatosAImprimir.Lista);
                }
                $('select#docsList').attr('selectedIndex', 0);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Eliminar: function (pIDSistema, nombre) {
        var msg = "Va a eliminar la tarea <strong>\"" + unescape(nombre) + "\"</strong>.¿Desea Continuar?";
        bootbox.confirm(msg, function (result) {
            if (result) {
                eliminarTarea(pIDSistema, nombre);
            }
        });
    },
    CancelarDialog: function () {
        var msg = '¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas'
        bootbox.confirm(msg, function (result) {
            if (result) {
                $('#dialogAltaEdit').dialog('close');
            }
        });

    },
    ConsultarDialog: function (pID) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = true;
            //Limpia los campos.
            page.LimpiarCampos();
            page.estadoscontrolesedicion(false);
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(unescape(pID));
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            //Carga los datos en pantalla.
            page.setDatos();

            $('#dialogAltaEdit').dialog({
                buttons: {
                    "CERRAR": function () { $('#dialogAltaEdit').dialog('close'); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado. ' + e);
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    ModificarDialog: function (pID) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = true;
            //Limpia los campos.
            page.LimpiarCampos();
            page.estadoscontrolesedicion(true);
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(unescape(pID));
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            //Carga los datos en pantalla.
            page.setDatos();

            $('#dialogAltaEdit').dialog({
                buttons: {
                    "Guardar": function () {
                        try {
                            appCIPOLPRESENTACION.BloquearUI();
                            //page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos. ' + e);
                        } finally {
                            appCIPOLPRESENTACION.DesBloquearUI();
                        }
                    },
                    "Cancelar": function () { page.CancelarDialog(); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado: ' + e);
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    AgregarDialog: function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.LimpiarCampos();
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = false;
            page.estadoscontrolesedicion(true);
            //Inicializa el textbox como fecha.
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "Guardar": function () {
                        try {
                            appCIPOLPRESENTACION.BloquearUI();
                            //page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos.' + e);
                        } finally {
                            appCIPOLPRESENTACION.DesBloquearUI();
                        }
                    },
                    "Cancelar": function () { page.CancelarDialog(); }
                }
            });
            $('#dialogAltaEdit').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado: ' + e);
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        //Setea el campo Id
        SetTXT('txtIdTarea', app.IDTAREA);
        SetTXT('txtCodigo', app.IDTAREA);
        SetDDL('cboSistemaCarga', app.IDSISTEMA)
        SetTXT('txtNombreTarea', app.DESCRIPCIONTAREA);

        //Desabilita el campo.
        if (page.update) {
            appCIPOLPRESENTACION.SetAtributo("[id*='txtIdTarea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnVerificarTarea']", "disabled", "disable");
        }
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        //Setea el campo Id
        app.IDTAREA = GetTXT('txtIdTarea');
        if (app.IDTAREA == null || app.IDTAREA == '')
            app.IDTAREA = -1;

        app.CODIGOTAREA = GetTXT('txtCodigo');
        //cboAreas select()
        app.IDSISTEMA = GetDDL('cboSistemaCarga');
        if (app.IDSISTEMA == null || app.IDSISTEMA == '')
            app.IDSISTEMA = -1;
        app.DESCRIPCIONTAREA = GetTXT('txtNombreTarea');

    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
        $("[id*='txtFiltroID']").validCampoFranzEnter('0123456789');
        $("[id*='txtIdTarea']").validCampoFranz('0123456789');
    },
    EstadosControlesFiltro: function (estado) {
        if (estado) {
            $("[id*='txtFiltroID']").removeAttr("disabled");
            $("[id*='txtFiltroNombre']").removeAttr("disabled");
            $("[id*='txtFiltroCodigo']").removeAttr("disabled");
            $("[id*='cboSistemafiltro']").removeAttr("disabled");
            $("[id*='cboTareaAutorizante']").removeAttr("disabled");
            $("[id*='btnFiltro']").removeAttr("disabled");
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltroID']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltroNombre']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltroCodigo']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboSistemafiltro']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboTareaAutorizante']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltro']", "disabled", "disable");
        }
    },
    estadoscontrolesedicion: function (estado) {
        if (estado) {
            $("[id*='txtIdTarea']").removeAttr("disabled");
            $("[id*='txtCodigo']").removeAttr("disabled");
            $("[id*='cboSistemaCarga']").removeAttr("disabled");
            $("[id*='txtNombreTarea']").removeAttr("readOnly");
            $("[id*='btnVerificarTarea']").removeAttr("disabled");
            $("[id$='btnFiltrar']").removeAttr('disabled');
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id*='txtIdTarea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtCodigo']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='cboSistemaCarga']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtNombreTarea']", "readOnly", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnVerificarTarea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltrar']", 'disabled', 'disabled');
        }
    },
    LimpiarCampos: function () {
        //Setea el campo Id
        SetTXT('txtIdTarea', '');
        SetTXT('txtCodigo', '');
        //cboAreas select()
        SetDDL('cboSistemaCarga', '0')
        SetTXT('txtNombreTarea', '');
        //Deshabilita el campo.
        $("[id*='txtIdTarea']").removeAttr("disabled");
        $("[id*='btnVerificarTarea']").removeAttr("disabled");
    },
    LimpiarFiltros: function () {
        //Limpieza de los filtros.
        SetTXT('txtFiltroID', '');
        SetTXT('txtFiltroNombre', '');
        SetTXT('txtFiltroCodigo', '');
        SetDDL('cboSistemafiltro', '0');
        SetDDL('cboTareaAutorizante', ' ');
    },
    GuardarDialog: function () {
        //Guarda los datos en el servidor.
        page.getDatos();
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarTareasPrimitivas",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    bootbox.alert("Los datos han sido guardados.", function () {
                        $('#dialogAltaEdit').dialog('close');
                        $("[id$='btnFiltro']").click();
                    });
                    if (page.update) {
                        $("[id$='btnFiltro']").click();
                    }
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
    VerificarTarea: function () {
        var IdTarea = $("[id*='txtIdTarea']").val();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/VerificarTareasPrimitiva",
            data: "{'IdTarea':" + JSON.stringify(IdTarea) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'No se pudo realizar la verificación. Verifique';
                    bootbox.alert(msg);
                }
                else {
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });

    }
}











