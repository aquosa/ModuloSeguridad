/// <summary>
/// Desarrollo de las funciones necesarias para el ABM de Sistemas
/// </summary>
/// <history>
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    }
    catch (e) {
        ShowAlertDanger('Error Inesperado.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }

    
});
//function CaracteresExtranios() {
//    if (event.keyCode == 39)
//        event.returnValue = false;
//};
var eliminarSistema = function (pIDSistema, nombre) {
    try {

        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarSistemaHabilitado",
            data: "{Id : " + pIDSistema + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = 'El Sistema <strong>\"' + unescape(nombre) + '\"</strong> fue eliminado correctamente.';
                    bootbox.alert(msg, function () { $("[id$='btnFiltro']").click(); });
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
    }
    catch (e) {
        ShowAlertDanger('Error Inesperado: No se eliminó el sistema seleccionado.');
        return;
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
};

var page = {
    update: true,
    validarCerrar: true,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga el elemento base.
        page.CargarElementoBase();
        //Carga la lista de sitemas
        //page.RealizarBusqueda();
        //Inicializa el textbox como fecha.
        $("[id*='txtIDSistema']").prop('maxLength', 3);
        $("[id*='txtCodSistema']").prop('maxLength', 10);
        $("[id*='txtNombreEXE']").prop('maxLength', 30);
        $("[id*='txtNombreIcono']").prop('maxLength', 20);
        $("[id*='txtPaginaPOrDefecto']").prop('maxLength', 100);
        $("[id*='txtDescripcion']").prop('maxLength', 50);

        $("[id*='txtIDSistema']").validCampoFranz('0123456789');
        $("[id*='txtIDSistema']").NoCtrl_c_v_x();

        $('#dialogAltaEdit').hide();
        // $('#dialogMessageError').hide();
        $('#dialogAltaEdit').dialog({
            autoOpen: false,
            resizable: false,
            modal: true,
            create: function () { page.validarCerrar = true; }
        });

        $("[id*='cmdNuevo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.update = false;
                return page.AgregarDialog();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("#icon-list li").click(function () {
            $("#icon-list").find('li').removeClass("select-icon");
            $(this).addClass("select-icon");
        });

        //        $(".square").click(function () {
        //            $(".square").removeClass("square-select");
        //            $(this).addClass("square-select");
        //        });


        $("[id*='lnkViewPreview']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                return page.AgregarDialogViewPreview();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='btnIconSelect']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.update = false;
                return page.AgregarDialogIcon();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });


    },
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoSistemaBase",
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
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoSistema",
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
    EliminarSistemaConfirm: function (pIDSistema, nombre) {
        var msg = "Va a eliminar el sistema <strong>\"" + unescape(nombre) + "\"</strong>. ¿Desea Continuar?";
        bootbox.confirm(msg, function (result) { if (result) { eliminarSistema(pIDSistema, nombre); } });
    },

    GuardarDialog: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.setDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ActualizarSistemaHabilitado",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    var msg = "Los datos han sido guardados.";
                    bootbox.alert(msg, function () {
                        $('#dialogAltaEdit').dialog('close');
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
    },
    CancelarDialog: function () {
        var msg = '¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas';
        bootbox.confirm(msg, function (result) {
            if (result) {
                $('#dialogAltaEdit').dialog('close');
            }
        });
    },
    ConsultarDialog: function (pIDSistema) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = false;
            //Limpia los campos.
            page.LimpiarCampos();
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pIDSistema);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = false;
            page.estadoscontrolesedicion(false);
            //Carga los datos en pantalla.
            page.cargarDatos();
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "CERRAR": function () { $('#dialogAltaEdit').dialog('close'); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            return false;
        }
        finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    ModificarDialog: function (pIDSistema) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = true;
            //Limpia los campos.
            page.LimpiarCampos();
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pIDSistema);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            page.estadoscontrolesedicion(true);
            //Carga los datos en pantalla.
            page.cargarDatos();
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "GUARDAR": function () {
                        try {
                            appCIPOLPRESENTACION.BloquearUI();
                            page.validarCerrar = false;
                            page.GuardarDialog();
                        } catch (e) {
                            ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos.');
                        }
                        finally {
                            appCIPOLPRESENTACION.DesBloquearUI();
                        }
                    },
                    "CANCELAR": function () { page.CancelarDialog(); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            return false;
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
            $('#dialogAltaEdit').dialog({
                buttons: {
                    "GUARDAR": function () {
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
                    "CANCELAR": function () { page.CancelarDialog(); }
                }
            });
            //Abre la ventana de dialogo.
            $('#dialogAltaEdit').dialog('open');
            return false;
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            return false;
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    AgregarDialogIcon: function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            $('#popupIcon').dialog({
                buttons: {
                    "Cerrar": function () { page.CerrarDialogIcon(); }
                }
            });
            //Abre la ventana de dialogo.
            $('#popupIcon').dialog('open');
            return false;
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            return false;
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    AgregarDialogViewPreview: function () {
        try {


            $("#titlePrev").removeAttr('class');
            $("#iconPrev").removeAttr('class');

            var clases = "tile " + $(".square-select").attr("id");
            $("#titlePrev").addClass(clases);

            var iconCss = $("#iIcon").attr("class");
            $("#iconPrev").addClass(iconCss);

            var descSistema = $("[id*='txtDescripcion']").val();

            $("#name").text(descSistema);


            appCIPOLPRESENTACION.BloquearUI();
            $('#dialogViewPreview').dialog({
                buttons: {
                    "Cerrar": function () { $('#dialogViewPreview').dialog('close'); }
                },
                width: 190
            });
            //Abre la ventana de dialogo.
            $('#dialogViewPreview').dialog('open');
            return false;
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: Al recuperar el elemento.');
            return false;
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }

    },
    CerrarDialogIcon: function () {
        //        $("#iIcon").removeClass(function () {
        //            return $(this).prev().attr("class");
        //        });
        $("#iIcon").removeAttr("class");
        $("#txtIconName").text("");
        $("#iIcon").addClass($(".select-icon").find('i').attr('class'));
        $("#txtIconName").text($(".select-icon").find('i').attr('class'));
        $('#popupIcon').dialog('close');
    },

    cargarDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        //Setea el campo Id
        $("[id*='txtIDSistema']").val(app.IDSISTEMA);
        $("[id*='txtCodSistema']").val(app.CODSISTEMA);
        if (app.blnSISTEMAHABILITADO)
            $("[id*='chkHabilitado']").prop('checked', true);
        else
            $("[id*='chkHabilitado']").prop('checked', false);
        $("[id*='txtNombreEXE']").val(app.NOMBREEXEC);
        $("[id*='txtPaginaPOrDefecto']").val(app.PAGINAPORDEFECTO);
        $("[id*='txtDescripcion']").val(app.DESCSISTEMA);

        if (app.ICONO != undefined && app.ICONO != "") {
            var classIcon = "";
            var classColor = "green";
            var classes = app.ICONO.split('*');
            if (classes.length > 0) {
                classIcon = classes[0];
                classColor = classes[1];
            }

            $("#iIcon").removeAttr('class');
            $("#iIcon").addClass(classIcon);
            $("#txtIconName").text(classIcon);

            $(".square").removeClass("square-select");
            $("#" + classColor + "").addClass("square-select");
        }


    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        if (!page.update)
            app.IDSISTEMA = $("[id*='txtIDSistema']").val();

        if (app.IDSISTEMA == '')
            app.IDSISTEMA = 0;

        app.CODSISTEMA = $("[id*='txtCodSistema']").val();
        //chkhabilitado
        app.blnSISTEMAHABILITADO = $("[id*='chkHabilitado']").prop('checked');
        app.NOMBREEXEC = $("[id*='txtNombreEXE']").val();
        app.ICONO = "";
        app.ICONO = $("#iIcon").attr("class") + "*" + $(".square-select").attr("id");
        app.PAGINAPORDEFECTO = $("[id*='txtPaginaPOrDefecto']").val();
        app.DESCSISTEMA = $("[id*='txtDescripcion']").val();
    },
    validarDatos: function () {
        //todo: realizar la validación
        if (!page.update)
            appCIPOLPRESENTACION.trim($("[id*='txtIDSistema']").val())

        app.CODSISTEMA = $("[id*='txtCodSistema']").val();

        app.DESCSISTEMA = $("[id*='txtDescripcion']").val();
    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
    },
    LimpiarCampos: function () {
        //Setea el campo Id
        $("[id*='txtIDSistema']").val('');
        $("[id*='txtCodSistema']").val('');
        $("[id*='chkHabilitado']").prop('checked', true);
        $("[id*='txtNombreEXE']").val('');
        $("[id*='txtPaginaPOrDefecto']").val('');
        $("[id*='txtDescripcion']").val('');
        $("#txtIconName").text('');
        $("#iIcon").removeAttr('class');
        $(".square").removeClass("square-select");
        //Desabilita el campo.
        //$("[id*='txtIDSistema']").removeAttr("disabled");
    },

    estadoscontrolesedicion: function (estado) {
        var pdata = appCIPOLPRESENTACION.trxdata;
        if (!estado || pdata.update) {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtIDSistema']", "disabled", "disable");
        } else {
            $("[id$='txtIDSistema']").removeAttr("disabled");
        }

        if (estado) {
            $("[id*='txtCodSistema']").removeAttr("disabled");
            $("[id*='chkHabilitado']").removeAttr("disabled");
            $("[id*='txtNombreEXE']").removeAttr("disabled");
            $("[id*='txtPaginaPOrDefecto']").removeAttr("disabled");
            $("[id*='txtDescripcion']").removeAttr("disabled");
            $("[id*='btnIconSelect']").removeAttr("disabled");
            $(".square").on("click", function () {
                $(".square").removeClass("square-select");
                $(this).addClass("square-select");
            });



        } else {
            appCIPOLPRESENTACION.SetAtributo("[id*='txtCodSistema']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='chkHabilitado']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtNombreEXE']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtPaginaPOrDefecto']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='txtDescripcion']", "disabled", "disable");

            appCIPOLPRESENTACION.SetAtributo("[id*='btnIconSelect']", "disabled", "disable");

            $(".square").off("click");


        }
    }
}
function jfNopermitirSPACE(obj, e) {

    if (document.all) caracter = e.keyCode; else caracter = e.which;

    if (caracter == 32)
        if (document.all) {
            window.event.keyCode = 0;
        }
        else {
            return false;
        }
}

function maxlegth()
{
    $("txtDescripcion[maxlength]").bind('input propertychange', function () {
        var maxLength = $(this).attr('maxlength');
        if ($(this).val().length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));
        }
    })
}