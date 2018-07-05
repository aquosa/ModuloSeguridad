/// <summary>
/// Script asociado a todas las operaciones realizadas en la pantalla "Sistemas Bloqueados"
/// </summary>
/// <history>
/// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        ShowAlertDanger( 'Error Inesperado: Al iniciar.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var page = {
    blnEstadoControler: null,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Carga Sistemas Bloqueados.
        page.CargarPagina();
        /*Acciones de los botones del formulario*/
        $("[id*='btnGuardar']").hide();
        $("[id*='btnCancelar']").hide();
        $("[id*='btnModificar']").show();

        $("[id*='btnModificar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickModificar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al inicar modificación.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnCancelar']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickCancelar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al cancelar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnGuardar']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickGuardar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al guardar datos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });


        $("[id*='btnAsignarSistemasTodos']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarSistemasTodos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar todos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarSistema']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarSistema(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al desasignar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarSistema']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarSistema(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarSistemasTodos']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarSistemasTodos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al desasignar todos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarUsuariosTodos']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarUsuariosTodos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar todos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarUsuario']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickAsignarUsuario(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al Asginar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarUsuariosTodos']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarUsuariosTodos(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al desasignar todos.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarUsuario']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.clickDesasignarUsuario(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al desasignar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        page.estadocontroles(false);
    },
    bindingControles: function () {
        $("[id*='lstSistemasBloqueados']").dblclick(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                if (!page.blnEstadoControler) return; page.clickDesasignarSistema(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='lstSistemasDesbloqueados']").dblclick(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                if (!page.blnEstadoControler) return; page.clickAsignarSistema(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado. ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='lstUsuariosDesbloqueados']").dblclick(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                if (!page.blnEstadoControler) return; page.clickDesasignarUsuario(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='lstUsuariosBloqueados']").dblclick(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                if (!page.blnEstadoControler) return; page.clickAsignarUsuario(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    CargarPagina: function () {
        //Se carga el elemento base y los datos de los listbox.
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarSistBloqueados",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                page.cargarDatos(data.d);
                page.cargarDatosUsuarios(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    cargarDatos: function (data) {
        //Carga el elemento base del ABM.
        appCIPOLPRESENTACION.trxdata = data.elemento;

        //LISTBOX SISTEMAS DESBLOQUEADOS.
        try {

            $ContenedorlbSiestemasDesbloqueados = $("#lbSiestemasDesbloqueados");
            var DatosAImprimirlbSiestemasDesbloqueados = $.parseJSON(data.jsonsistemasdesbloqueados);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbSiestemasDesbloqueados.Lista = $("<div/>").html(DatosAImprimirlbSiestemasDesbloqueados.Lista).text();
            $ContenedorlbSiestemasDesbloqueados.children().remove(); //TODO
            $ContenedorlbSiestemasDesbloqueados.append(DatosAImprimirlbSiestemasDesbloqueados.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los sistemas Desbloqueados.' + e.Message);
        }
        //FIN LISTBOX SISTEMAS DESBLOQUEADOS.

        //LISTBOX SISTEMAS BLOQUEADOS.
        try {

            $ContenedorlbSistemasBloqueados = $("#lbSistemasBloqueados");
            var DatosAImprimirlbSistemasBloqueados = $.parseJSON(data.jsonsistemasbloqueados);
            DatosAImprimirlbSistemasBloqueados.Lista = $("<div/>").html(DatosAImprimirlbSistemasBloqueados.Lista).text();
            $ContenedorlbSistemasBloqueados.children().remove(); //TODO
            $ContenedorlbSistemasBloqueados.append(DatosAImprimirlbSistemasBloqueados.Lista);

            //Selecciona el primer elemento de la lista de sistemas bloqueados
            if (DatosAImprimirlbSistemasBloqueados.Lista != null) {
                $('[id*="lstSistemasBloqueados"] option:nth(0)').attr("selected", "selected");
            }
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los sistemas bloqueados. ' + e.Message);
        }
        //FIN LISTBOX SISTEMAS BLOQUEADOS.  
    },
    cargarDatosUsuarios: function (data) {
        //LISTBOX USUSARIOS BLOQUEADOS.
        try {

            $ContenedorlbUsuariosBloqueados = $("#lbUsuariosBloqueados");
            var DatosAImprimirlbUsuariosBloqueados = $.parseJSON(data.jsonusuariosbloqueados);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbUsuariosBloqueados.Lista = $("<div/>").html(DatosAImprimirlbUsuariosBloqueados.Lista).text();
            $ContenedorlbUsuariosBloqueados.children().remove(); //TODO
            $ContenedorlbUsuariosBloqueados.append(DatosAImprimirlbUsuariosBloqueados.Lista);

        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los usuarios bloqueados.');
        }
        //FIN LISTBOX SISTEMAS BLOQUEADOS.

        //LISTBOX USUARIOS DESBLOQUEADOS.
        try {
            $ContenedorlbUsuariosDesbloqueados = $("#lbUsuariosDesbloqueados");
            var DatosAImprimirlbUsuariosDesbloqueados = $.parseJSON(data.jsonusuariosdesbloqueados);
            DatosAImprimirlbUsuariosDesbloqueados.Lista = $("<div/>").html(DatosAImprimirlbUsuariosDesbloqueados.Lista).text();
            $ContenedorlbUsuariosDesbloqueados.children().remove(); //TODO
            $ContenedorlbUsuariosDesbloqueados.append(DatosAImprimirlbUsuariosDesbloqueados.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los usuarios desbloqueados.');
        }

    },
    //Función que se ejecuta cuando se hace click en diferente sistemas bloqueados
    changeSistemasBloqueados: function () {
        $("[id*='lstSistemasBloqueados'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ActualizarUsuarios",
                data: "{'id':" + id + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (data) {
                    page.cargarDatosUsuarios(data.d);
                    page.bindingControles();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        });
    },
    clickModificar: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        app.update = true;
        page.estadocontroles(true);
        page.estadoscontrolesedicion(true);
    },
    clickCancelar: function () {
        //        if (confirm('¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas')) {
        //            page.CargarPagina();
        //            page.estadocontroles(false);
        //            page.estadoscontrolesedicion(false);
        //            page.blnEstadoControler = false;
        //        }

        var msg = '¿Está seguro que desea cancelar? Se perderán todas las modificaciones que no han sido guardadas.';
        bootbox.confirm(msg, function (result) {
            if (result) {
                page.CargarPagina();
                page.estadocontroles(false);
                page.estadoscontrolesedicion(false);
                page.blnEstadoControler = false;
            }
        });
    },
    //Función que guarda las modificaciones, realizadas por el ususario en pantalla, en base de datos. 
    clickGuardar: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarSistemasBloqueados",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta.d.ResultadoEjecucion) {
                    var msg = 'Los datos han sido guardados.';
                    bootbox.alert(msg, function () {
                        page.postGuardar(respuesta);
                        page.estadocontroles(false);
                    });

                    //ShowAlertSuccess('Los datos han sido guardados.');
                    //  page.postGuardar(respuesta);
                    // page.estadocontroles(false);
                }
                else {
                    bootbox.alert(respuesta.d.MensajeError);
                    //ShowAlertDanger(respuesta.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Función que se ejecuta luego de persistir las modificaciones realizadas en la base de datos
    postGuardar: function (data) {
        var app = appCIPOLPRESENTACION.trxdata;
        page.estadocontroles(false);
        page.estadoscontrolesedicion(false);
        page.CargarPagina();
    },
    estadocontroles: function (estado) {
        //Deshabilita los controles
        page.blnEstadoControler = estado;
        if (!estado) {
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarSistemasTodos']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarSistema']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarSistema']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarSistemasTodos']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarUsuariosTodos']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnAsignarUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id*='btnDesasignarUsuariosTodos']", "disabled", "disable");

        }
        else {
            $("[id*='btnAsignarSistemasTodos']").removeAttr("disabled");
            $("[id*='btnAsignarSistema']").removeAttr("disabled");
            $("[id*='btnDesasignarSistema']").removeAttr("disabled");
            $("[id*='btnDesasignarSistemasTodos']").removeAttr("disabled");
            $("[id*='btnAsignarUsuariosTodos']").removeAttr("disabled");
            $("[id*='btnAsignarUsuario']").removeAttr("disabled");
            $("[id*='btnDesasignarUsuario']").removeAttr("disabled");
            $("[id*='btnDesasignarUsuaiosTodos']").removeAttr("disabled");

            page.bindingControles();
        }
        $("[id*='lstSistemasBloqueados']").change(function () { page.changeSistemasBloqueados(); return false; });
    },
    //Segun si estamos modificando o no se muestan y ocultan controles
    estadoscontrolesedicion: function (estado) {
        if (estado) {
            $("[id*='btnGuardar']").show();
            $("[id*='btnCancelar']").show();
            $("[id*='btnModificar']").hide();
        } else {
            $("[id*='btnGuardar']").hide();
            $("[id*='btnCancelar']").hide();
            $("[id*='btnModificar']").show();
        }
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        app.lstSistemas = [];
        //Recupera las tareas seleccionadas.
        $("[id*='lstSistemasBloqueados'] option").map(function () {
            app.lstSistemas.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        app.lstUsuarios = [];
        //Recupera los grupos excluyentes seleccionados
        $("[id*='lbUsuariosDesbloqueados'] option").map(function () {
            app.lstUsuarios.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
    },
    //Función que maneja el evento que se dispara cuando se pasan los sistemas de desbloqueado a bloqueado (>>)
    clickAsignarSistemasTodos: function () {
        var obj = [];
        $("[id*='lstSistemasDesbloqueados'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarSistemasTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstSistemasDesbloqueados'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstSistemasBloqueados']");
                        page.changeSistemasBloqueados();
                    });
                }
                else {
                    bootbox.alert(respuesta.d);
                    //ShowAlertDanger(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Función que maneja el evento que se dispara cuando se pasa un sistema de desbloqueado a bloqueado (>)
    clickAsignarSistema: function () {
        $("[id*='lstSistemasDesbloqueados'] :selected").each(function () {
            var id = $(this).val();
            var desc = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarSistema",
                data: "{Id:'" + id + "',desc:'" + desc + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstSistemasBloqueados']");
                        page.changeSistemasBloqueados();
                    }
                    else {
                        bootbox.alert(respuesta.d);
                        //ShowAlertDanger(respuesta.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        });
    },
    //Función que maneja el evento que se dispara cuando se pasa un sistema de bloqueado a desbloqueado (<)
    clickDesasignarSistema: function () {
        $("[id*='lstSistemasBloqueados'] :selected").each(function () {
            var id = $(this).val();
            var desc = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarSistema",
                data: "{Id:'" + id + "',desc:'" + desc + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstSistemasDesbloqueados']");
                        page.clickDesasignarUsuariosTodos();
                        $("[id*='lstUsuariosBloqueados'] option").each(function () {
                            $(this).remove();
                        });
                    }
                    else {
                        bootbox.alert(respuesta.d);
                        //ShowAlertDanger(respuesta.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        });
    },
    //Función que maneja el evento que se dispara cuando se pasan todos los sistemas de bloqueado a desbloqueado (<<)
    clickDesasignarSistemasTodos: function () {
        var obj = [];
        $("[id*='lstSistemasBloqueados'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarSistemasTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstSistemasBloqueados'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstSistemasDesbloqueados']");
                    });
                    page.clickDesasignarUsuariosTodos();
                    $("[id*='lstUsuariosBloqueados'] option").each(function () {
                        $(this).remove();
                    });
                }
                else {
                    bootbox.alert(respuesta.d);
                    //ShowAlertDanger(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },

    //USUARIOS
    //Función que maneja el evento que se dispara cuando se pasan todos los usuarios de bloqueado a desbloqueado (>>)
    clickAsignarUsuariosTodos: function () {
        var idSistemaBloqueado = $("[id*='lstSistemasBloqueados'] :selected").val();
        var obj = [];
        $("[id*='lstUsuariosBloqueados'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text(), 'IdSistemaBloqueado': idSistemaBloqueado });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarUsuariosTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstUsuariosBloqueados'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstUsuariosDesbloqueados']");
                    });
                }
                else {
                    //bootbox.alert(respuesta.d);
                    ShowAlertDanger(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Función que maneja el evento que se dispara cuando se pasa un usuario de bloqueado a desbloqueado (>)
    clickAsignarUsuario: function () {
        var idSistemaBloqueado = $("[id*='lstSistemasBloqueados'] :selected").val();
        $("[id*='lstUsuariosBloqueados'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarUsuario",
                data: "{Id:'" + id + "',nombre:'" + nombre + "',IdSistemaBloqueado:'" + idSistemaBloqueado + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstUsuariosDesbloqueados']");
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
        });
    },
    //Función que maneja el evento que se dispara cuando se pasan todos los usuarios de desbloqueados a bloqueados (<<)
    clickDesasignarUsuariosTodos: function () {
        var idSistemaBloqueado = $("[id*='lstSistemasBloqueados'] :selected").val();
        var obj = [];
        $("[id*='lstUsuariosDesbloqueados'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text(), 'IdSistemaBloqueado': idSistemaBloqueado });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarUsuariosTodos",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstUsuariosDesbloqueados'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstUsuariosBloqueados']");
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
    //Función que maneja el evento que se dispara cuando se pasa un usuario de desbloqueado a bloqueado (<)
    clickDesasignarUsuario: function () {
        var idSistemaBloqueado = $("[id*='lstSistemasBloqueados'] :selected").val();
        $("[id*='lstUsuariosDesbloqueados'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarUsuario",
                data: "{Id:'" + id + "',nombre:'" + nombre + "',IdSistemaBloqueado:'" + idSistemaBloqueado + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstUsuariosBloqueados']");
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
    }
}