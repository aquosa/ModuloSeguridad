/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        ShowAlertDanger('Error Inesperado: Al iniciar.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var eliminarRol = function (validar) {
    var Id = $("[id*='cboRol']").val();
    //Se carga el elemento base, los combos y los datos de los listbox.
    //Setea la estructura del view
    $.ajax({
        type: "POST",
        url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarRol",
        data: "{Id:'" + Id + "',validar:'" + validar + "'}",
        contentType: "application/json; charset=iso-8859-1",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d.ResultadoEjecucion) {
                if (data.d.preguntausuarios) {

                    bootbox.confirm(data.d.MensajeError, function (result) {
                        if (result) {
                            page.clickEliminar(false);
                        }


                    });
                }
                else {
                    //ShowAlertSuccess('El ' + $("[id*='cboRol'] :selected").text() + ' fue eliminado correctamente');
                    var msg = 'El <strong>\"' + $("[id*='cboRol'] :selected").text() + '\"</strong> fue eliminado correctamente';
                    bootbox.alert(msg, function () { page.cargarDatos(data.d.rolgetcarga); });
                    //Carga la página.
                    //var data2 = $.parseJSON(data.d);
                    //page.cargarDatos(data.d.rolgetcarga);
                    //page.cargarDatos(data2.rolgetcarga);
                }
            } else {

                bootbox.alert(data.d.MensajeError);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            throw xhr.responseJSON;
        }
    });
};

var page = {
    claseSelect: 'SelectRolLista',
    blnestadoscontrolesedicion: "",
    init: function () {
        page.CargarPagina();
        //Aplica el maxlenght.
        $().maxlength();
        $("[id*='cboRol']").addClass("col-md-8 select-no-padding");
        $("[id*='cmdAsignar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.Asignar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar un rol.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdDesasignar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.DesAsignar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al desasignar un rol.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdNuevo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickNuevo(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al inicar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdModificar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickModificar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar modificación.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdEliminar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickEliminar(true); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al eliminar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdGuardar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickGuardar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al guardar los datos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cmdCancelar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickCancelar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al cancelar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        page.estadoscontrolesedicion(false);
    }, bindingControles: function () {
        $("[id*='cboRol']").off("change.cboRol");
        $("[id*='cboRol']").on("change.cboRol", function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ChangeRol(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    estadoscontrolesedicion: function (estado) {
        page.blnestadoscontrolesedicion = estado;
        if (estado) {
            $("[id*='cboRol']").hide();
            $("[id*='txtNombreRol']").show();
            $("[id*='cmdGuardar']").show();
            $("[id*='cmdCancelar']").show();
            $("[id*='cmdModificar']").hide();
            $("[id*='cmdNuevo']").hide();
            appCIPOLPRESENTACION.SetAtributo("[id*='cmdEliminar']", "disabled", "disable");
            $("[id*='cmdAsignar']").removeAttr("disabled");
            $("[id*='cmdDesasignar']").removeAttr("disabled");
            //Seta el foco en el nombre.
            $("[id$='txtNombreRol']").focus();
        } else {
            $("[id*='cboRol']").show();
            $("[id*='txtNombreRol']").hide();
            $("[id*='cmdGuardar']").hide();
            $("[id*='cmdCancelar']").hide();
            $("[id*='cmdModificar']").show();
            $("[id*='cmdNuevo']").show();
            $("[id*='cmdEliminar']").removeAttr("disabled");
            appCIPOLPRESENTACION.SetAtributo("[id*='cmdAsignar']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='cmdDesasignar']", "disabled", "disable");
            //Sino hay role, deshabilito el botón modificar.
            if ($("[id*='cboRol']").length > 0)
                $("[id*='cmdModificar']").removeAttr("disabled");
            else
                appCIPOLPRESENTACION.SetAtributo("[id*='cmdModificar']", "disabled", "disable");
        }
    },
    clickNuevo: function () {
        $("[id*='dvtareasasignadas']").find("ul").first().children().remove();

        var app = appCIPOLPRESENTACION.trxdata;
        app.idrol = -1;
        app.update = false;

        $("[id$='txtNombreRol']").val('');

        //Habilita los controles.
        page.estadoscontrolesedicion(true);

        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarRolNew",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    clickModificar: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        if ($("[Id*='cboRol'] option").length == 0) {
            return;
        }
        app.idrol = $("[Id*='cboRol']").val();
        app.update = true;
        $("[id*='txtNombreRol']").val($("[Id*='cboRol'] :selected").text());
        $("[id$='txtNombreRol']").focus();
        page.estadoscontrolesedicion(true);
    },
    clickEliminar: function (validar) {

        var msg = 'Va a eliminar el Rol <strong>\"' + $("[id*='cboRol'] :selected").text() + '\"</strong>.¿Desea continuar?';

        bootbox.confirm(msg, function (result) {
            if (result) {
                eliminarRol(validar);
            }
        });
        //ShowAlertWarningFunctionWithOneParameter(msg, eliminarRol, validar);

    },
    clickGuardar: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.getDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarRoles",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d.ResultadoEjecucion) {
                    var msg = 'Los datos fueron guardados correctamente';
                    bootbox.alert(msg, function () { page.postGuardar(respuesta.d.idrol); });
                    //ShowAlertSuccess('Los datos fueron guardados correctamente');
                    //page.postGuardar(respuesta.d.idrol);
                }
                else {
                    bootbox.alert(respuesta.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    postGuardar: function (id) {
        var app = appCIPOLPRESENTACION.trxdata;
        if (!(app.update)) {
            $("[id*='cboRol']").append('<option value="' + id + '" selected="selected" >' + app.nombrerol + '</option>');
        } else {
            $("[id*='cboRol'] :selected").text(app.nombrerol);
        }
        page.ChangeRol();
        page.estadoscontrolesedicion(false);
    },
    clickCancelar: function () {
        var msg = '¿Está seguro que desea cancelar? Se perderán todas las modificaciones que no han sido guardadas.';
        bootbox.confirm(msg, function (result) {
            if (result) {
                page.estadoscontrolesedicion(false);
                page.ChangeRol();
            }
        });
    },
    ChangeRol: function () {
        var Id = $("[id*='cboRol']").val();
        if (Id == null)
            return;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarDatosRol",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                page.cargarDatosRol(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    cargarDatosRol: function (data) {
        //LISTBOX TAREA ASIGNADAS.
        try {

            if (data.jsontvtareasasignadas == null)
                return;

            $ContenedortvTareasDisponibles2 = $("#treeeee2");
            var DatosAImprimirtvTareasDisponibles2 = $.parseJSON(data.jsontvtareasasignadas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirtvTareasDisponibles2.Lista = $("<div/>").html(DatosAImprimirtvTareasDisponibles2.Lista).text();
            $Contenido2 = $(DatosAImprimirtvTareasDisponibles2.Lista);
            $ContenedortvTareasDisponibles2.children().remove(); //TODO
            var $tablasEnContenido2 = $Contenido2.find("table");
            //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
            //no quede código HTML sucio o incorrecto.
            if ($tablasEnContenido2.length > 0) {
                $Contenido2.find("form").children().unwrap();
                $Contenido2.css("list-style-type", "none");
                $ContenedortvTareasDisponibles2.append($Contenido2);
            }
            else {
                //Si el contenido no contiene tablas, asumo que el WS me devolvió
                //código HTML correcto y listo para insertarse.
                //$Contenedor.append(DatosAImprimir.Lista);
                $ContenedortvTareasDisponibles2.append(DatosAImprimirtvTareasDisponibles2.Lista);
            }
            page.vincularClickLista($("[id*='treeeee2']"), false, false, 'selectedAsig', true);

        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas disponibles.');
        }
    },
    CargarPagina: function () {
        //Se carga el elemento base, los combos y los datos de los listbox.
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarRolCarga",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                page.cargarDatos(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        if (!(app.update)) {
            app.idrol = -1;
        }
        app.nombrerol = $("[Id*='txtNombreRol']").val();
        app.lsttareas = [];
        //Recupera las tareas seleccionadas.
        $("[id*='dvtareasasignadas'] li:not(:has(ul))").map(function () {
            app.lsttareas.push({ 'Id': $(this).attr('id'), 'nombre': $(this).find("a").first().text(), 'asignada': ($(this).attr("class").toLowerCase().indexOf('asignada'.toLowerCase()) != -1) });
        });

    },
    cargarDatos: function (data) {
        //LISTBOX TAREA DISPONIBLES.
        appCIPOLPRESENTACION.trxdata = data.elemento;
        try {

            $ContenedortvTareasDisponibles = $("#treeeee");
            var DatosAImprimirtvTareasDisponibles = $.parseJSON(data.jsontvtareasdisponibles);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirtvTareasDisponibles.Lista = $("<div/>").html(DatosAImprimirtvTareasDisponibles.Lista).text();
            $Contenido = $(DatosAImprimirtvTareasDisponibles.Lista);
            $ContenedortvTareasDisponibles.children().remove(); //TODO
            var $tablasEnContenido = $Contenido.find("table");
            //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
            //no quede código HTML sucio o incorrecto.
            if ($tablasEnContenido.length > 0) {
                $Contenido.find("form").children().unwrap();
                $Contenido.css("list-style-type", "none");
                $ContenedortvTareasDisponibles.append($Contenido);
            }
            else {
                //Si el contenido no contiene tablas, asumo que el WS me devolvió
                //código HTML correcto y listo para insertarse.
                //$Contenedor.append(DatosAImprimir.Lista);
                $ContenedortvTareasDisponibles.append(DatosAImprimirtvTareasDisponibles.Lista);
            }
            //page.vincularClickLista($('#treeeee'), false, false, 'selected');
            page.vincularClickLista($('#treeeee'), false, false, page.claseSelect);

        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas disponibles.');
        }


        //FIN LISTBOX TAREA DISPONIBLES.
        page.cargarDatosRol(data);

        //FIN LISTBOX TAREA ASIGNADAS.
        //COMBO SISTEMAS.
        try {

            $ContenedorCboSistema = $("#divcboroles");
            var DatosAImprimir = $.parseJSON(data.jsoncboroles);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $ContenedorCboSistema.children().remove();
            $ContenedorCboSistema.append(DatosAImprimir.Lista);
            page.bindingControles();
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los roles.');
        }
        //FIN COMBO SISTEMAS.

    },

    Asignar: function () {
        //var Id = $('.selected').parent().attr('id');
        var Id = $('.' + page.claseSelect).parent().attr('id');
        if (Id == null)
            return;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarTareaRol",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    //page.asignartarea($('.selected'));
                    page.asignartarea($('.' + page.claseSelect));
                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });

    },
    asignartarea: function ($seleccion) {
        $Asignadas = $("#treeeee2")

        var Id = $seleccion.parent().attr('id');
        var valores = Id.split('/');
        //Ejemplo: "0/2/1095"
        switch (valores.length) {
            case 1:
                //mover GRUPO
                $find = $Asignadas.find("[Id='" + Id + "']");
                if ($find.length > 0) {
                    $seleccion.parent().find('li').each(function () {
                        $findSistema = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                        if ($findSistema.length > 0) {
                            $(this).find('li').each(function () {
                                $findtarea = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                                if ($findtarea.length > 0)
                                { } else {
                                    $Tarea = $(this).clone();
                                    $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                                    $Tarea.addClass('asignada');
                                    $findSistema.find("ul").first().append($Tarea);
                                }
                            });
                        } else {
                            $Sistema = $(this).clone();
                            $Sistema.find('.nivel3 img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Sistema.find('.nivel3 li').addClass('asignada');
                            $find.find("ul").first().append($Sistema);
                        }
                    });
                } else {
                    $Grupo = $seleccion.parent().clone();
                    //$Grupo.find('a').removeClass('selected');
                    $Grupo.find('a').removeClass(page.claseSelect);
                    $Grupo.find('.nivel3 img').attr("src", "./Imagenes/task-assign-icon.png");
                    $Grupo.find('.nivel3 li').addClass('asignada');
                    $Asignadas.find("ul").first().append($Grupo);
                }
                break;
            case 2:
                //MOVER SISTEMA.
                $find = $Asignadas.find("[Id='" + Id + "']");
                if (!($find.length > 0)) {
                    //No existe el sistema
                    $findGrupo = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().attr('id') + "']");
                    if (!($findGrupo.length > 0)) {
                        //NO existe el grupo.
                        $Grupo = $seleccion.parent().parent().parent().clone();
                        $Grupo.find('li').remove();
                        $Grupo.find('ul').first().find('ul').remove();
                        $Sistema = $seleccion.parent().clone();
                        //$Sistema.find('a').removeClass('selected');
                        $Sistema.find('a').removeClass(page.claseSelect);
                        $Sistema.find('.nivel3 img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Sistema.find('.nivel3 li').addClass('asignada');
                        $Grupo.find('ul').append($Sistema);
                        $Asignadas.find("ul").first().append($Grupo);
                    }
                    else {
                        $Sistema = $seleccion.parent().clone();
                        //$Sistema.find('a').removeClass('selected');
                        $Sistema.find('a').removeClass(page.claseSelect);
                        $Sistema.find('.nivel3 img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Sistema.find('.nivel3 li').addClass('asignada');
                        $findGrupo.find("ul").first().append($Sistema);
                    }
                } else {
                    $seleccion.parent().find('li').each(function () {
                        $findTarea = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                        if ($findTarea.length > 0) {

                        } else {
                            $Tarea = $(this).clone();
                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Tarea.addClass('asignada');
                            $find.find("ul").first().append($Tarea);
                        }
                    });
                }

                break;
            case 3:
                //MOVER TAREA.
                $find = $Asignadas.find("[Id='" + Id + "']");
                if (!($find.length > 0)) {
                    //No existe la tareas
                    $findSistema = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().attr('id') + "']");
                    if (!($findSistema.length > 0)) {
                        //no existe el sistema.
                        $findgrupo = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().parent().parent().attr('id') + "']");
                        if (!($findgrupo.length > 0)) {
                            //No existe el grupo
                            $Grupo = $seleccion.parent().parent().parent().parent().parent().clone();
                            $Grupo.find('li').remove();
                            $Grupo.find('ul').first().find('ul').remove();
                            $Sistema = $seleccion.parent().parent().parent().clone();
                            $Sistema.find('li').remove();
                            $Sistema.find('ul').first().find('ul').remove();
                            $Tarea = $seleccion.parent().clone();
                            //$Tarea.find('a').removeClass('selected');
                            $Tarea.find('a').removeClass(page.claseSelect);
                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Tarea.addClass('asignada');
                            $Sistema.find('ul').append($Tarea);
                            $Grupo.find('ul').append($Sistema);
                            //alert($Sistema[0].innerHTML);
                            //alert($Grupo[0].innerHTML);
                            $Asignadas.find("ul").first().append($Grupo);
                        }
                        else {
                            $Sistema = $seleccion.parent().parent().parent().clone();
                            $Sistema.find('li').remove();
                            $Sistema.find('ul').first().find('ul').remove();
                            $Tarea = $seleccion.parent().clone();
                            //$Tarea.find('a').removeClass('selected');
                            $Tarea.find('a').removeClass(page.claseSelect);
                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Tarea.addClass('asignada');
                            $Sistema.find('ul').append($Tarea);
                            //alert($Sistema[0].innerHTML);
                            //alert($Grupo[0].innerHTML);
                            $findgrupo.find("ul").first().append($Sistema);
                        }
                    }
                    else {
                        $Tarea = $seleccion.parent().clone();
                        //$Tarea.find('a').removeClass('selected');
                        $Tarea.find('a').removeClass(page.claseSelect);
                        $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Tarea.addClass('asignada');
                        $findSistema.find("ul").first().append($Tarea);
                    }
                } else {
                    //nada
                }
                break;
        }
        page.vincularClickLista($("[id*='treeeee2']"), false, true, 'selectedAsig', true);

    },
    DesAsignar: function () {
        var Id = $('.selectedAsig').parent().attr('id');
        if (Id == null)
            return;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarRolTarea",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    $seleccion = $('.selectedAsig');
                    var Id = $seleccion.parent().attr('id');
                    var valores = Id.split('/');
                    //Ejemplo: "0/2/1095"
                    switch (valores.length) {
                        case 1:
                            $seleccion.parent().remove();
                            break;
                        case 2:

                            if ($seleccion.parent().parent().parent().find('li.sistema').length == 1)
                                $seleccion.parent().parent().parent().remove();
                            else
                                $seleccion.parent().remove();
                            break;
                        case 3:

                            if ($seleccion.parent().parent().parent().find('ul li').length == 1)
                                if ($seleccion.parent().parent().parent().parent().parent().find('li.sistema').length == 1)
                                    $seleccion.parent().parent().parent().parent().parent().remove();
                                else
                                    $seleccion.parent().parent().parent().remove();
                            else
                                $seleccion.parent().remove();
                            break;
                    }
                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    vincularClickLista: function ($Entorno, EsParaArbolCompleto, Solobinding, cssSelected, Aplicardbclick) {
        if (typeof EsParaArbolCompleto == 'undefined') {
            EsParaArbolCompleto = false;
        }
        if (typeof $Entorno == 'undefined') {
            $Entorno = $("body");
        }

        //$Entorno.find('li').css('pointer', 'default').css('list-style-image', 'none');

        $Entorno.find('li:has(ul)')
			.click(function (event) {
			    //Si ya hay una animación en curso, no hago nada.
			    if ($(this).find('ul').is(':animated')) return false;

			    if (this == event.target) {
			        if (!$(this).find('ul').is(':hidden')) {
			            $(this).css('list-style-image', 'url(./Imagenes/plusbox.gif)');
			            $(this).find('ul').toggle('slow'); //children()
			            //$(this).children().remove();
			            $(this).find('ul').append("<ul style=\"display:none;\"></ul>");
			        }
			        else {
			            $(this).css('list-style-image', 'url(./Imagenes/minusbox.gif)');
			            $(this).find('ul').toggle('slow');
			        }
			    }
			    //return false;
			});

        $Entorno.find('li:not(:has(ul))').css({ cursor: 'default', 'list-style-type': 'none', 'list-style-image': 'none' });
        if (!Solobinding) {
            if (!EsParaArbolCompleto) {
                $Entorno.find('li:has(ul)').css({ cursor: 'pointer', 'list-style-image': 'url(./Imagenes/plusbox.gif)' }); //
                $Entorno.find('li:has(ul)').find('li:has(ul)').css({ cursor: 'pointer', 'list-style-image': 'url(./Imagenes/minusbox.gif)' }); //
                $Entorno.find('li:has(ul)').find('ul').hide();
            } else {
                $Entorno.find('li:has(ul)').css({ cursor: 'pointer', 'list-style-image': 'url(./Imagenes/minusbox.gif)' }); //
            }
        }

        $Entorno.find('a').each(function () {
            var $img = $(this).find('img');
            $(this).css({ cursor: 'pointer' });
            if (!($img.length > 0)) {
                $(this).removeAttr('href');
                $(this).click(function () {
                    //page.SelectTareaDisponible($(this));
                    $Entorno.find('a').removeClass(cssSelected);
                    $(this).addClass(cssSelected);
                    //alert($(this).parent().attr('id'));
                    return false;
                });
            }
        });

        if (Aplicardbclick) {
            $Entorno.find('.nivel3 a').dblclick(function () {
                if (!page.blnestadoscontrolesedicion)
                    return;
                if ($(this).parent().find('img').attr("src").toLowerCase().indexOf('block'.toLowerCase()) != -1) {
                    $(this).parent().find('img').attr("src", "./Imagenes/task-assign-icon.png");
                    $(this).parent().removeClass('bloqueada');
                    $(this).parent().addClass('asignada');
                } else {
                    $(this).parent().find('img').attr("src", "./Imagenes/task-block-icon.png");
                    $(this).parent().removeClass('asignada');
                    $(this).parent().addClass('bloqueada');
                }
            });
        }
    }
}