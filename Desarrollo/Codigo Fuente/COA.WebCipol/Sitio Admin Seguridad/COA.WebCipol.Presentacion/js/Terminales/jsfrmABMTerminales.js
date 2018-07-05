/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
/// </history>
$(function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.init();
        } catch (e) {
            ShowAlertDanger('Error Inesperado: Error al recuperar el elemento.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    });

    var eliminarTerminal = function (pIDSistema, nombre) {
        try {
            appCIPOLPRESENTACION.BloquearUI(pIDSistema, nombre);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarTerminal",
                data: "{Id : " + pIDSistema + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        var msg = 'La terminal <strong>\"' + unescape(nombre) + '\"</strong> fue eliminada correctamente.';
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
            ShowAlertDanger('Error Inesperado: No se eliminó la terminal.');
            return false;
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };
    
    
    //function CaracteresExtranios() {
    //    if (event.keyCode == 39)
    //        event.returnValue = false;
    //};
    var page = {
        validarCerrar: true,
        blnestadoscontrolesedicion: false,
        haydatosengrilla: false,
        update: true,
        //spinnerRam: null,
        //spinnerDisco: null,
        init: function () {
            appCIPOLPRESENTACION.onsubmit = false;
            //Carga el elemento base.
            page.CargarElementoBase();
            //Carga Areas.
            page.CargarAreas();
            page.ConfiguracionInicial();

            $("[id*='txtRam']").numeric();
            $("[id*='txtDisco']").numeric();
            $("[id*='cboAreas']").addClass("col-xs-6 select-no-padding")

            //Inicializa el textbox como fecha.
            $('#dialogAltaEdit').hide();
            $('#dialogAltaEdit').dialog({
                autoOpen: false,
                resizable: false,
                modal: true
            });


            $("[id*='btnNuevo']").click(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.update = false;
                    return page.AgregarDialog();
                } catch (e) {
                    ShowAlertDanger('Error Inesperado: No se pudieron guardar los datos.');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });

        },
        CargarElementoBase: function () {
            //Setea la estructura del view
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTerminalBase",
                data: "",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (data) {
                    appCIPOLPRESENTACION.trxdata = data.d;
                    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
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
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTerminal",
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
        CargarAreas: function () {
            //Realiza la búsqueda de los datos y los visualiza en ua grilla.
            $Contenedor = $("#reemplazarcboArea");

            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarCboAreas",
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
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        },
        EliminarSistema: function (pIDSistema, nombre) {

            var msg = "Va a eliminar la terminal <strong>\"" + unescape(nombre) + "\"</strong>. ¿Desea Continuar?";
            bootbox.confirm(msg, function (result) {
                if (result) {
                    eliminarTerminal(pIDSistema, nombre);
                }
            });
        },
        GuardarDialog: function () {
            //Setea los datos. Ver como saber si es update o new.
            page.getDatos();
            //Guarda los datos en el servidor.
            var pdata = appCIPOLPRESENTACION.trxdata;
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarTerminales",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (respuesta) {
                    if (respuesta.d == "") {

                        bootbox.alert('Los datos han sido guardados.', function () {
                            $('#dialogAltaEdit').dialog('close');
                        });

                        if (page.haydatosengrilla) {
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
        EstadosControlesFiltro: function (estado) {
            if (estado) {
                $("[id*='txtFiltroArea']").removeAttr("disabled");
                $("[id*='txtFiltroTerminal']").removeAttr("disabled");
                $("[id*='btnFiltro']").removeAttr("disabled");

            } else {
                appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltroArea']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtFiltroTerminal']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltro']", "disabled", "disable");
            }
        },
        estadoscontrolesedicion: function (estado) {
            page.blnestadoscontrolesedicion = estado;
            if (estado) {
                //Setea el campo Id
                $("[id*='txtCodTerminal']").removeAttr("disabled");
                $("[id*='txtNombreTerminal']").removeAttr("disabled");
                $("[id*='chkHabilitada']").removeAttr("disabled");
                //cboAreas select()
                $("[id*='cboAreas']").removeAttr("disabled");
                $("[id*='txtProcesador']").removeAttr("disabled");
                //select cboModoActualizacion -> remoto.
                $("[id*='cboModoActualizacion']").removeAttr("disabled");
                $("[id*='txtDescripcion']").removeAttr("disabled");
                $("[id*='txtMonitor']").removeAttr("disabled");
                $("[id*='txtVideo']").removeAttr("disabled");
                $("[id*='txtComentarios']").removeAttr("disabled");
                $("[id*='txtRam']").removeAttr("disabled");
                $("[id*='txtDisco']").removeAttr("disabled");

            } else {
                appCIPOLPRESENTACION.SetAtributo("[id$='txtRam']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtDisco']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtCodTerminal']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtNombreTerminal']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='chkHabilitada']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='cboAreas']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtProcesador']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='cboModoActualizacion']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtDescripcion']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtMonitor']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtVideo']", "disabled", "disable");
                appCIPOLPRESENTACION.SetAtributo("[id$='txtComentarios']", "disabled", "disable");
            }
        },
        ConsultarDialog: function (pID) {
            page.update = true;
            //Limpia los campos.
            page.LimpiarCampos();
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pID);
            //return false;
            //Setea el valor a update.
            var app = appCIPOLPRESENTACION.trxdata;
            app.update = true;
            //Carga los datos en pantalla.
            page.setDatos();
            page.estadoscontrolesedicion(false);
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
                //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
                page.CargarElemento(pID);
                //return false;
                //Setea el valor a update.
                var app = appCIPOLPRESENTACION.trxdata;
                app.update = true;
                //Carga los datos en pantalla.
                page.setDatos();
                page.estadoscontrolesedicion(true);

                $('#dialogAltaEdit').dialog({
                    buttons: {
                        "Guardar": function () {
                            try {
                                appCIPOLPRESENTACION.BloquearUI();
                                page.GuardarDialog();
                            } catch (e) {
                                ShowAlertDanger('Error Inesperado: No se pudieron cargar los datos.');
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
                ShowAlertDanger('Error Inesperado: No se pudieron cargar los datos.');
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
                ShowAlertDanger('Error Inesperado: No se pudo cargar el formulario.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        },
        CancelarDialog: function () {
            var msg = '¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas';
            bootbox.confirm(msg, function (result) {
                if (result) {
                    $('#dialogAltaEdit').dialog('close');
                }                
            });
        },
        setDatos: function () {
            var app = appCIPOLPRESENTACION.trxdata;

            SetTXT('txtCodTerminal', app.CODTERMINAL);
            SetTXT('txtNombreTerminal', app.NOMBRECOMPUTADORA)
            if (app.blnUSOHABILITADO)
                $("[id*='chkHabilitada']").prop('checked', true);
            else
                $("[id*='chkHabilitada']").prop('checked', false);
            SetDDL('cboAreas', app.IDAREA);
            var IDAREAAUX = GetDDL('cboAreas');
            if (IDAREAAUX == null || IDAREAAUX == '')
                bootbox.alert('El área actual está dada de Baja. Seleccione nueva área.');
            SetDDL('txtProcesador', app.MODELOPROCESADOR);
            //page.spinnerRam.spinner("value", app.CANTMEMORIARAM);
            //page.spinnerDisco.spinner("value", app.TAMANIODISCO);
            $("[id*='txtRam']").val(app.CANTMEMORIARAM);
            $("[id*='txtDisco']").val(app.TAMANIODISCO);
            SetDDL('txtMonitor', app.MODELOMONITOR);
            SetDDL('txtVideo', app.MODELOACELVIDEO);
            SetDDL('txtComentarios', app.DESCADICIONAL);
            SetDDL('cboModoActualizacion', app.ORIGENACTUALIZACION);
        }, 
        getDatos: function () {
            var app = appCIPOLPRESENTACION.trxdata;

            //Setea el campo Id
            app.CODTERMINAL = $("[id*='txtCodTerminal']").val();

            app.NOMBRECOMPUTADORA = $("[id*='txtNombreTerminal']").val();
            //Uso habilitado
            app.blnUSOHABILITADO = $("[id*='chkHabilitada']").prop('checked');
            //cboAreas select()
            app.IDAREA = $("[id*='cboAreas']").val();
            if (app.IDAREA == null || app.IDAREA == '')
                app.IDAREA = -1;
            app.MODELOPROCESADOR = $("[id*='txtProcesador']").val();
            //Tamaño RAM
            app.CANTMEMORIARAM = $("[id*='txtRam']").val(); ;

            if (app.CANTMEMORIARAM == null || app.CANTMEMORIARAM == '')
                app.CANTMEMORIARAM = 0;
            //Tamaño de disco
            app.TAMANIODISCO = $("[id*='txtDisco']").val(); ;
            if (app.TAMANIODISCO == null || app.TAMANIODISCO == '')
                app.TAMANIODISCO = 0;
            //select cboModoActualizacion -> remoto.
            app.ORIGENACTUALIZACION = $("[id*='cboModoActualizacion']").val();

            app.MODELOMONITOR = $("[id*='txtMonitor']").val();
            app.MODELOACELVIDEO = $("[id*='txtVideo']").val();
            app.DESCADICIONAL = $("[id*='txtComentarios']").val();
        }, validarDatos: function () {
            //todo: realizar la validación
            if (!page.update)
                appCIPOLPRESENTACION.trim($("[id*='txtIDSistema']").val())
        },
        ConfiguracionInicial: function () {
            //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
            $().maxlength();
            //page.spinnerRam = $("[id*='txtRam']").validCampoFranz('0123456789');
            //page.spinnerDisco = $("[id*='txtDisco']").validCampoFranz('0123456789');

        },
        LimpiarCampos: function () {
            //Setea el campo Id
            SetTXT('txtCodTerminal', '');
            SetTXT('txtNombreTerminal', '');
            $("[id*='chkHabilitada']").prop('checked', true);
            SetDDL('cboAreas', '');
            SetTXT('txtProcesador', '');
            SetTXT('txtRam', '');
            SetDDL('cboModoActualizacion', '0');
            SetTXT('txtRam', '');
            SetTXT('txtDisco', '');
            SetTXT('txtDescripcion', '');
            SetTXT('txtMonitor', '');
            SetTXT('txtVideo', '');
            SetTXT('txtComentarios', '');
        },
        LimpiarFiltros: function () {
            //Limpieza de los filtros.
            SetTXT('txtFiltroArea', '');
            SetTXT('txtFiltroTerminal', '');
            page.EstadosControlesFiltro(true);
            page.haydatosengrilla = false;
        }
    }











