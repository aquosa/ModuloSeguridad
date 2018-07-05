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
        ShowAlertDanger('Error Inesperado: Error al iniciar.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var eliminarGrupo = function () {
    appCIPOLPRESENTACION.BloquearUI();
    var idGrupo = $("[id*='cboGrupos'] :selected").val();
    //Se carga el elemento base, los combos y los datos de los listbox.
    //Setea la estructura del view
    $.ajax({
        type: "POST",
        url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarGrupo",
        data: "{Id:'" + idGrupo + "'}",
        contentType: "application/json; charset=iso-8859-1",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.d.ResultadoEjecucion) {
                var msg = 'El grupo <strong>\"' + $("[id*='cboGrupos'] :selected").text() + '\"</strong> fue eliminado correctamente';
                bootbox.alert(msg, function () {
                    page.cargarDatos(data.d);
                    page.estadoscontrolesedicion(false);
                });

            } else {
                bootbox.aler(data.d.MensajeError);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            ShowAlertDanger('Error al recuperar la carga de los datos. ' + xhr.responseJSON);
        },
        complete: function () {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    });

};

var page = {
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga Areas.
        page.CargarPagina();
        //Aplica el maxlenght.
        $().maxlength();
        /*Acciones de los botones del formulario*/
        $("[id*='txtNombreGrupo']").hide();
        $("[id*='btnNuevo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickNuevo(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnGuardar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickGuardar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnModificar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickModificar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnCancelar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickCancelar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnEliminar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickEliminar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='btnAsignarTodasTareas']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarTodasTareas(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarTarea']").click(function () {
            page.clickAsignarTarea(); return false;

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickGuardar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarTarea']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarTarea(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarTodasTareas']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarTodasTareas(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarTodosGrupos']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarTodosGrupos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarGrupo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarGrupo(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarGrupo']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarGrupo(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarTodosGrupos']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarTodosGrupos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al iniciar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        //Seta el binding de los controles.
        page.bindingControles();
        page.estadocontroles(false);
        page.estadoscontrolesedicion(false);

        //$("[id$='cboGrupos']").addClass("select-no-padding col-md-12");

    },
    CargarPagina: function () {
        //Se carga el elemento base, los combos y los datos de los listbox.
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarGrupoTareaCarga",
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
    cargarDatos: function (data) {
        //Carga el elemento base del ABM.
        appCIPOLPRESENTACION.trxdata = data.elemento;

        //COMBO SISTEMAS.
        try {

            $ContenedorCboSistema = $("#divcboSistema");
            var DatosAImprimir = $.parseJSON(data.jsoncbosistemas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $ContenedorCboSistema.children().remove();
            $ContenedorCboSistema.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los sistemas.');
        }
        //FIN COMBO SISTEMAS.

        //COMBO GRUPO.
        try {
            $ContenedorcboGrupos = $("#divcboGrupo");
            var DatosAImprimirGrupo = $.parseJSON(data.jsoncbogrupo);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirGrupo.Lista = $("<div/>").html(DatosAImprimirGrupo.Lista).text();
            $ContenedorcboGrupos.children().remove(); //TODO
            $ContenedorcboGrupos.append(DatosAImprimirGrupo.Lista);
            $("[id$='cboGrupos']").addClass("select-no-padding col-md-12");
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los grupos.');
        }
        //FIN COMBO GRUPO.

        //LISTBOX TAREA NO ASIGNADAS.
        try {

            $ContenedorlbTareasNoAsignadas = $("#lbTareasNoAsignadas");
            var DatosAImprimirlbTareasNoAsignadas = $.parseJSON(data.jsontareasnoasignandas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbTareasNoAsignadas.Lista = $("<div/>").html(DatosAImprimirlbTareasNoAsignadas.Lista).text();
            $ContenedorlbTareasNoAsignadas.children().remove(); //TODO
            $ContenedorlbTareasNoAsignadas.append(DatosAImprimirlbTareasNoAsignadas.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas no asignadas.');
        }
        //FIN LISTBOX TAREA NO ASIGNADAS.

        //LISTBOX TAREAS ASIGNADAS.
        try {

            $ContenedorlbTareasAsignadas = $("#lbTareasAsignadas");
            var DatosAImprimirlbTareasAsignadas = $.parseJSON(data.jsontareasasignadas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbTareasAsignadas.Lista = $("<div/>").html(DatosAImprimirlbTareasAsignadas.Lista).text();
            $ContenedorlbTareasAsignadas.children().remove(); //TODO
            $ContenedorlbTareasAsignadas.append(DatosAImprimirlbTareasAsignadas.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas asignadas.');
        }
        //FIN LISTBOX TAREAS ASIGNADAS.

        //LISTBOX GRUPO NO ASIGNADAS.
        try {

            $ContenedorlbGruposNoExcluyentes = $("#lbGruposNoExcluyentes");
            var DatosAImprimirlbGruposNoExcluyentes = $.parseJSON(data.jsongruposnoexcluyentes);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbGruposNoExcluyentes.Lista = $("<div/>").html(DatosAImprimirlbGruposNoExcluyentes.Lista).text();
            $ContenedorlbGruposNoExcluyentes.children().remove(); //TODO
            $ContenedorlbGruposNoExcluyentes.append(DatosAImprimirlbGruposNoExcluyentes.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los grupos no excluyentes.');
        }
        //FIN LISTBOX GRUPOS NO ASIGNADAS.

        //LISTBOX GRUPOS ASIGNADAS.
        try {

            $ContenedorlbGruposExcluyentes = $("#lbGruposExcluyentes");
            var DatosAImprimirlbGruposExcluyentes = $.parseJSON(data.jsongrupoexcluyentes);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbGruposExcluyentes.Lista = $("<div/>").html(DatosAImprimirlbGruposExcluyentes.Lista).text();
            $ContenedorlbGruposExcluyentes.children().remove(); //TODO
            $ContenedorlbGruposExcluyentes.append(DatosAImprimirlbGruposExcluyentes.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los grupos excluyentes.');
        }
        //FIN LISTBOX GRUPOS ASIGNADAS.
        page.bindingControles();
    },
    ChangeGrupos: function () {
        //Se carga el elemento base, los combos y los datos de los listbox.
        //Setea la estructura del view
        var IdGrupo = $("[id*='cboGrupos'] :selected").val();
        if (IdGrupo != null) {
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarDatosDelGrupo",
                data: "{Id:'" + IdGrupo + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (data) {
                    page.cargarDatosGrupo(data.d);

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        }
    },
    cargarDatosGrupo: function (data) {
        //LISTBOX TAREAS ASIGNADAS.
        try {

            $ContenedorlbTareasAsignadas = $("#lbTareasAsignadas");
            var DatosAImprimirlbTareasAsignadas = $.parseJSON(data.jsontareasasignadas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbTareasAsignadas.Lista = $("<div/>").html(DatosAImprimirlbTareasAsignadas.Lista).text();
            $ContenedorlbTareasAsignadas.children().remove(); //TODO
            $ContenedorlbTareasAsignadas.append(DatosAImprimirlbTareasAsignadas.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas asignadas.');
        }
        //FIN LISTBOX TAREAS ASIGNADAS.

        //LISTBOX GRUPO NO ASIGNADAS.
        try {

            $ContenedorlbGruposNoExcluyentes = $("#lbGruposNoExcluyentes");
            var DatosAImprimirlbGruposNoExcluyentes = $.parseJSON(data.jsongruposnoexcluyentes);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbGruposNoExcluyentes.Lista = $("<div/>").html(DatosAImprimirlbGruposNoExcluyentes.Lista).text();
            $ContenedorlbGruposNoExcluyentes.children().remove(); //TODO
            $ContenedorlbGruposNoExcluyentes.append(DatosAImprimirlbGruposNoExcluyentes.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los grupos no excluyentes.');
        }
        //FIN LISTBOX GRUPOS NO ASIGNADAS.

        //LISTBOX GRUPOS ASIGNADAS.
        try {

            $ContenedorlbGruposExcluyentes = $("#lbGruposExcluyentes");
            var DatosAImprimirlbGruposExcluyentes = $.parseJSON(data.jsongrupoexcluyentes);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbGruposExcluyentes.Lista = $("<div/>").html(DatosAImprimirlbGruposExcluyentes.Lista).text();
            $ContenedorlbGruposExcluyentes.children().remove(); //TODO
            $ContenedorlbGruposExcluyentes.append(DatosAImprimirlbGruposExcluyentes.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los grupos excluyentes.');
        }
        //FIN LISTBOX GRUPOS ASIGNADAS.
        page.bindingControles();
    },
    ChangeSistemas: function () {
        //Se carga el elemento base, los combos y los datos de los listbox.
        //Setea la estructura del view
        var IdSistema = $("[id*='cboSistemas']").val();

        if (IdSistema != null) {
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarTareasPorSistemaYGrupo",
                data: "{Id:'" + IdSistema + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (data) {
                    //LISTBOX TAREA NO ASIGNADAS.
                    try {

                        $ContenedorlbTareasNoAsignadas = $("#lbTareasNoAsignadas");
                        var DatosAImprimirlbTareasNoAsignadas = $.parseJSON(data.d);
                        //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
                        //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
                        //entities y obtener el código HTML original.
                        DatosAImprimirlbTareasNoAsignadas.Lista = $("<div/>").html(DatosAImprimirlbTareasNoAsignadas.Lista).text();
                        $ContenedorlbTareasNoAsignadas.children().remove(); //TODO
                        $ContenedorlbTareasNoAsignadas.append(DatosAImprimirlbTareasNoAsignadas.Lista);

                        page.bindingControles();
                    }
                    catch (e) {
                        ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas no asignadas.');
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        }
    },
    clickNuevo: function () {
        $("[id*='lstGrupoExcluyente']").children().remove();
        $("[id*='lstGrupoNoexcluyente']").children().remove();
        $("[id*='lstTareasAsignadas']").children().remove();

        $("[id*='cboGrupos'] option").each(function () {
            $(this).clone().appendTo("[id*='lstGrupoNoexcluyente']");
        });

        var app = appCIPOLPRESENTACION.trxdata;
        app.idgrupo = -1;
        app.update = false;

        $("[id*='txtNombreGrupo']").val('');
        //Actuliza el las tareas no asignadas
        page.ChangeSistemas();
        //Habilita los controles.
        page.estadocontroles(true);
        page.estadoscontrolesedicion(true);

        slideSidebarOpen('filtro-grupos-tareas', 'img-grupos-tareas');

        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarGrupoTareaNew",
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
    clickGuardar: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.getDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarGrupoTareas",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d.ResultadoEjecucion) {
                    var msg = 'Los datos fueron guardados correctamente.';
                    bootbox.alert(msg, function () { page.postGuardar(respuesta.d.idgrupro); });
                    //ShowAlertSuccess('Los datos fueron guardados correctamente.');
                    //page.postGuardar(respuesta.d.idgrupro)
                    //Seteo de estado luego de guardar.
                }
                else {
                    //ShowAlertDanger(respuesta.d.MensajeError);
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
            $("[id*='cboGrupos']").append('<option value="' + id + '" selected="selected" >' + app.nombregrupo + '</option>');
        } else {
            $("[id*='cboGrupos'] :selected").text(app.nombregrupo);
        }
        page.ChangeGrupos();
        page.ChangeSistemas();
        page.estadocontroles(false);
        page.estadoscontrolesedicion(false);
    },
    clickModificar: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        app.idgrupo = $("[Id*='cboGrupos'] :selected").val();
        app.update = true;
        $("[id*='txtNombreGrupo']").val($("[Id*='cboGrupos'] :selected").text());
        page.estadocontroles(true);
        page.estadoscontrolesedicion(true);
    },
    clickCancelar: function () {
        //         if (confirm('¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas.')) {
        //             page.estadocontroles(false);
        //             $("[id*='cboGrupos']").show();
        //             $("[id*='txtNombreGrupo']").hide();
        //             page.ChangeGrupos();
        //             page.ChangeSistemas();
        //             page.estadoscontrolesedicion(false);
        //         }
        var msg = '¿Está seguro que desea cancelar? Se perderán todas las modificaciones que no han sido guardadas.';
        bootbox.confirm(msg, function (result) {
            if (result) {
                page.estadocontroles(false);
                $("[id*='cboGrupos']").show();
                $("[id*='txtNombreGrupo']").hide();
                page.ChangeGrupos();
                page.ChangeSistemas();
                page.estadoscontrolesedicion(false);
            }
        });
    },
    estadoscontrolesedicion: function (estado) {
        if (estado) {
            $("[id*='cboGrupos']").hide();
            $("[id*='txtNombreGrupo']").show();
            $("[id*='btnGuardar']").show();
            $("[id*='btnCancelar']").show();
            $("[id*='btnModificar']").hide();
            $("[id*='btnNuevo']").hide();
            appCIPOLPRESENTACION.SetAtributo("[id*='btnEliminar']", "disabled", "disable");
        } else {
            $("[id*='cboGrupos']").show();
            $("[id*='txtNombreGrupo']").hide();
            $("[id*='btnGuardar']").hide();
            $("[id*='btnCancelar']").hide();
            $("[id*='btnModificar']").show();
            $("[id*='btnNuevo']").show();
            if ($("[id*='cboGrupos'] option").length > 0) {
                $("[id*='btnEliminar']").removeAttr("disabled");
                $("[id*='btnModificar']").removeAttr("disabled");
            }
            else {
                appCIPOLPRESENTACION.SetAtributo("[id*='btnEliminar']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id*='btnModificar']", "disabled", "disable");
            }
        }

    },
    clickEliminar: function () {
        var msg = 'Va a eliminar el Grupo <strong>\"' + $("[id*='cboGrupos'] :selected").text() + '\"</strong>. ¿Desea continuar?';

        bootbox.confirm(msg, function (result) {
            if (result) {
                eliminarGrupo();
            }
        });

    },
    bindingControles: function () {
        $("[id*='cboGrupos']").off("change.grupos");
        $("[id*='cboSistemas']").off("change.sistema");
        /*Acciones de los combos*/
        $("[id*='cboGrupos']").on("change.grupos", function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ChangeGrupos();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Error al recuperar la carga de los datos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='cboSistemas']").on("change.sistema", function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ChangeSistemas();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Error al recuperar la carga de los datos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    estadocontroles: function (estado) {
        //Deshabilita los controles
        if (!estado) {
            appCIPOLPRESENTACION.SetAtributo("[id*='cboSistemas']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarTodasTareas']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarTarea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarTarea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarTodasTareas']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarTodosGrupos']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarGrupo']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarGrupo']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarTodosGrupos']", "disabled", "disable");
        } else {
            $("[id*='cboSistemas']").removeAttr("disabled");
            $("[id*='btnAsignarTodasTareas']").removeAttr("disabled");
            $("[id*='btnAsignarTarea']").removeAttr("disabled");
            $("[id*='btnDesasignarTarea']").removeAttr("disabled");
            $("[id*='btnDesasignarTodasTareas']").removeAttr("disabled");
            $("[id*='btnAsignarTodosGrupos']").removeAttr("disabled");
            $("[id*='btnAsignarGrupo']").removeAttr("disabled");
            $("[id*='btnDesasignarGrupo']").removeAttr("disabled");
            $("[id*='btnDesasignarTodosGrupos']").removeAttr("disabled");
        }
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        if (!(app.update)) {
            app.idgrupo = -1;
        } else {
            app.idgrupo = $("[Id*='cboGrupos'] :selected").val(); ;
        }
        app.nombregrupo = $("[Id*='txtNombreGrupo']").val();
        app.lstTareas = [];
        //Recupera las tareas seleccionadas.
        $("[id*='lstTareasAsignadas'] option").map(function () {
            app.lstTareas.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        app.lstGrupos = [];
        //Recupera los grupos excluyentes seleccionados
        $("[id*='lbGruposExcluyentes'] option").map(function () {
            app.lstGrupos.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });

    },
    clickAsignarTodasTareas: function () {
        var obj = [];
        $("[id*='lstTareasRaiz'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarTareaGrupoTareaTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstTareasRaiz'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstTareasAsignadas']");
                    });
                }
                else {
                    //ShowAlertDanger(respuesta.d);
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });

    },
    clickAsignarTarea: function () {
        $("[id*='lstTareasRaiz'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarTareaGrupoTarea",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstTareasAsignadas']");
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
    clickDesasignarTarea: function () {
        $("[id*='lstTareasAsignadas'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarTareaGrupoTarea",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstTareasRaiz']");
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
    clickDesasignarTodasTareas: function () {
        var obj = [];
        $("[id*='lstTareasAsignadas'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarTareaGrupoTareaTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstTareasAsignadas'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstTareasRaiz']");
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
    clickAsignarTodosGrupos: function () {
        var obj = [];
        $("[id*='lstGrupoNoexcluyente'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarGrupoGrupoTareaTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstGrupoNoexcluyente'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstGrupoExcluyente']");
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
    clickAsignarGrupo: function () {
        $("[id*='lstGrupoNoexcluyente'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarGrupoGrupoTarea",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstGrupoExcluyente']");
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
    clickDesasignarGrupo: function () {
        $("[id*='lstGrupoExcluyente'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarGrupoGrupoTarea",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstGrupoNoexcluyente']");
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
    clickDesasignarTodosGrupos: function () {
        var obj = [];
        $("[id*='lstGrupoExcluyente'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarGrupoGrupoTareaTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstGrupoExcluyente'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstGrupoNoexcluyente']");
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
    }
}

