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
        ShowAlertDanger('Error Inesperado: Al recuperar las tareas.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var eliminarTarea = function (pIDSistema, nombre) {
    appCIPOLPRESENTACION.BloquearUI();
    try {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarTareaAutorizante",
            data: "{Id : " + pIDSistema + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'La Tarea <strong>\"' + unescape(nombre) + '\"</strong> fue eliminada correctamente.';
                    bootbox.alert(msg, function () { page.RealizarBusqueda(); });
                    return;
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
        ShowAlertDanger('Error Inesperado: No se eliminó la tarea.');
        return false;
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
};

var page = {
    update: true,
    validarCerrar: true,
    valorDefecto: 0,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga el elemento base.
        page.CargarElementoBase();
        //Carga Areas.
        page.CargarComboSistema();
        //Carga las tareas primitivas
        page.BuscarTareaPrimitivas();

        $("[id$='cboSistema']").addClass("col-md-7 select-no-padding");
        //Inicializa el textbox como fecha.
        $('#dialogAltaEdit').dialog({
            autoOpen: false,
            resizable: false,
            width: 800,
            modal: true,
            create: function () { page.validarCerrar = true; },
            buttons: {
                "Guardar": function () {
                    try {
                        appCIPOLPRESENTACION.BloquearUI();
                        page.GuardarDialog();
                    } catch (e) {
                        ShowAlertDanger('Error Inesperado');
                    } finally {
                        appCIPOLPRESENTACION.DesBloquearUI();
                    }
                },
                "Cancelar": function () { page.CancelarDialog(); }
            }
        });

        $("[id*='btnNuevo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.update = false;
                return page.AgregarDialog();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='cboSistema']").change(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.BuscarTareaPrimitivas();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al recuperar las tareas.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='cmdAsignar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.Asignar();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar tarea');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='cmdDesasignar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.DesAsignar();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al no autorizar tarea.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        page.ConfiguracionInicial();
    },
    Asignar: function () {
        $("[id*='lbTareasPrimitivas'] > option:selected").each(function () {
            //Controla que no haya uno ya cargado.
            var count = $("[id*='lbTareasDosPrimitivas'] option").size();
            if (count == null || count == 0) {
                var id = $(this).val();
                var nombre = $(this).text();
                var option = $(this);
                $.ajax({
                    type: "POST",
                    url: "./PageBuilders/wsAjaxwsSeguridad.asmx/Autorizar",
                    data: "{Id:'" + id + "',nombre:'" + unescape(nombre) + "'}",
                    contentType: "application/json; charset=iso-8859-1",
                    dataType: "json",
                    async: false,
                    success: function (respuesta) {
                        if (respuesta.d == "") {
                            option.remove().appendTo("[id*='lbTareasDosPrimitivas']");
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
        });
    },
    DesAsignar: function () {
        $("[id*='lbTareasDosPrimitivas'] > option:selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/NoAutorizar",
                data: "{Id:'" + id + "',nombre:'" + unescape(nombre) + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lbTareasPrimitivas']");
                    }
                    else {
                        bootbox.alert(respuesta.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });

        });
    },
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTareaAutorizanteBase",
            data: "",
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
    CargarElemento: function (IdElemento) {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTareaAutorizante",
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
    RealizarBusqueda: function () {
        $("[id$='btnFiltro']").click();
    },
    CargarComboSistema: function () {
        //Realiza la búsqueda de los datos y los visualiza en ua grilla.
        $Contenedor = $("#reemplazarcbosistema");
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarCboSistemaTareaAutorizante",
            data: "",
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

                page.valorDefecto = GetDDL('cboSistema');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });

    },
    BuscarTareaPrimitivas: function () {
        //Realiza la búsqueda de los datos y los visualiza en ua grilla.
        $Contenedor = $("#tareasprimitivas");
        var IdSistema = GetDDL('cboSistema');

        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarListBoxTareaPrimitivasTareaAutorizante",
            data: "{Id:'" + IdSistema + "'}",
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
                $("[id$='lbTareasPrimitivas']").addClass("col-xs-12 select-no-padding");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Eliminar: function (pIDSistema, nombre) {

        var msg = "Va a eliminar la tarea <strong>\"" + unescape(nombre) + "\"</strong>. ¿Desea Continuar?";
        bootbox.confirm(msg, function (result) { if (result) { eliminarTarea(pIDSistema, nombre); } });

        //ShowAlertWarningDelete(msg, eliminarTarea, pIDSistema, nombre);
    },
    GuardarDialog: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.getDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarTareasAutorizante",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'Los datos fueron guardados correctamente';
                    bootbox.alert(msg, function () {
                        $('#dialogAltaEdit').dialog('close');
                        page.RealizarBusqueda();
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
        var msg = '¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas';
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
            page.CargarElemento(pID);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            //Carga los datos en pantalla.
            page.setDatos();
            //Carga Areas.
            page.BuscarTareaPrimitivas();
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "CERRAR": function () { $('#dialogAltaEdit').dialog('close'); }
                }
            });
            $('#dialogAltaEdit').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado: Error al recuperar el elemento.');
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
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pID);
            page.estadoscontrolesedicion(true);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            //Carga los datos en pantalla.
            page.setDatos();
            //Carga Areas.
            page.BuscarTareaPrimitivas();
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "Guardar": function () {

                        try {
                            appCIPOLPRESENTACION.BloquearUI();
                            page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos.');
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
            ShowAlertDanger('Error Inesperado');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    AgregarDialog: function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.LimpiarCampos();
            page.estadoscontrolesedicion(true);
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = false;
            //Inicializa el textbox como fecha.
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "Guardar": function () {
                        try {
                            appCIPOLPRESENTACION.BloquearUI();
                            page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado');
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
        $("[id*='txtCodTA']").val(app.CODIGOTAREA);
        $("[id*='txtDescTA']").val(app.DESCRIPCIONTAREA);
        //cboAreas select()
        $("[id*='cboSistema']").val(app.IDSISTEMA);
        //todo:set idtareaAutorizante.
        $("[id*='lbTareasDosPrimitivas']").append('<option value="' + app.IDTAREAAUTORIZANTE + '" selected="selected" >' + app.DESCIDTAREAAUTORIZANTE + '</option>');

        //Desabilita el campo.
        if (page.update) {
            appCIPOLPRESENTACION.SetAtributo("[id*='cboSistema']", "disabled", "disable");
        }

    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        var idTarea = null;
        //app.blnUSOHABILITADO = $("[id*='chkRequiereAuditoria']").prop('checked');
        //Setea el campo Id
        app.IDSISTEMA = $("[id*='cboSistema']").val();
        if (app.IDSISTEMA == null || app.IDSISTEMA == '')
            app.IDSISTEMA = -1;
        app.DESCSISTEMA = $("[id*='cboSistema'] :selected").text();
        //Recupera el id de la tarea.
        app.IDTAREAAUTORIZANTE = -1;
        app.DESCIDTAREAAUTORIZANTE = "";
        $("[id*='lbTareasDosPrimitivas'] option").map(function () {
            app.IDTAREAAUTORIZANTE = $(this).val();
            app.DESCIDTAREAAUTORIZANTE = $(this).text();
        });
        app.DESCRIPCIONTAREA = $("[id*='txtDescTA']").val();
        app.CODIGOTAREA = $("[id*='txtCodTA']").val();
    },
    validarDatos: function () {
    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
    },
    LimpiarCampos: function () {
        //Setea el campo Id
        $("[id*='txtCodTA']").val('');
        $("[id*='txtDescTA']").val('');
        $("[id*='cboSistema']").val(page.valorDefecto);
        $("[id*='lbTareasDosPrimitivas']").children().remove();
        $("[id*='cboSistema']").removeAttr("disabled");
        page.BuscarTareaPrimitivas();
    },
    estadoscontrolesedicion: function (estado) {
        if (estado) {
            $("[id*='txtCodTA']").removeAttr("disabled");
            $("[id*='txtDescTA']").removeAttr("disabled");
            $("[id*='cboSistema']").removeAttr("disabled");
            $("[id*='lbTareasDosPrimitivas']").removeAttr("readOnly");
            $("[id*='cmdAsignar']").removeAttr("disabled");
            $("[id*='cmdDesasignar']").removeAttr("disabled");
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id*='txtCodTA']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtDescTA']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='cboSistema']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='lbTareasDosPrimitivas']", "readOnly", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='cmdAsignar']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='cmdDesasignar']", "disabled", "disable");
        }
    }
}