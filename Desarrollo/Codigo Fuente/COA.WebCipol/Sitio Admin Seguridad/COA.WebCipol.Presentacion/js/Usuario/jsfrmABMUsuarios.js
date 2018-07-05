/// <summary>
/// 
/// </summary>
/// <history>
/// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
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

var page = {
    claseSelect: 'SelectRolLista',
    blnestadoscontrolesedicion: false,
    update: true,
    LoguinSSO: false,
    haydatosengrilla: false,
    tempBloqAsig: true,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        page.CargarPagina();
        //Configuración Inicial
        page.ConfiguracionInicial();
        //binding de los controles.
        page.bindingControles();


        $("[id$='btnVerificarUsuario']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.VerificarUsuario();
            } catch (e) {
                ShowAlertDanger('Error Inesperado:');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $("[id*='btnAsignarTerminalesTodas']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ClickAsignarTerminalesTodas();
                return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al asignar las terminales.');
                return false;
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarTerminal']").click(function () {
            page.ClickDesasignarTerminal(); return false;
            try {
                appCIPOLPRESENTACION.BloquearUI();
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnAsignarTerminal']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ClickAsignarTerminal(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id*='btnDesasignarTerminalesTodas']").click(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ClickDesasignarTerminalesTodas(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id$='cboAreaFiltro']").change(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.BuscarTerminalesPorArea(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id$='chkCuentaBloqueada']").change(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.HabilitarBloqueo();
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id$='chkIntegradoAlDominio']").change(function () {

            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.HabilitarContrasena();
            } catch (e) {
                ShowAlertDanger('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        //Deja la marca de que hubo cambio de Contrasena
        $("[id*='txtContrasena']").keypress(function () {
            var app = appCIPOLPRESENTACION.trxdata;
            app.blnCambioContrasenia = true;
        });
        page.initRoles();
    },
    HabilitarContrasena: function () {
        var estado = $("[id$='chkIntegradoAlDominio']").prop('checked');
        if (estado) {
            $("[id$='txtContrasena']").attr("disabled", "disabled");
            $("[id$='txtContrasena']").val('');
            $("[id$='txtRepetirContrasena']").attr("disabled", "disabled");
            $("[id$='txtRepetirContrasena']").val('');
            $("input[id$='chkForzar'], label[for$='chkForzar']").hide();
        } else {
            $("[id$='txtContrasena']").removeAttr("disabled");
            $("[id$='txtRepetirContrasena']").removeAttr("disabled");
            $("input[id$='chkForzar'], label[for$='chkForzar']").show();
        }
    },
    HabilitarBloqueo: function () {
        var estado = $("[id$='chkCuentaBloqueada']").prop('checked');

        if (estado) {
            $("[id$='txtFechaBloq']").show();
            $("[id$='lblFechaBloq']").show();
        } else {
            $("[id$='txtFechaBloq']").hide();
            $("[id$='lblFechaBloq']").hide();
        }
    },
    CargarPagina: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarUsuariosCarga",
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
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14583
    /// </history>
    cargarDatos: function (data) {
        appCIPOLPRESENTACION.trxdata = data.elemento;
        //Seta los permisos.
        appCIPOLPRESENTACION.trxdatapermiso = data.permiso;
        //[MiguelP]         28/10/2014      GCP - Cambios 15598
        appCIPOLPRESENTACION.trxFiltro = data.filtro;

        if (data.blnNombreDominio) {
            $("input[id$='btnVerificarUsuario'], label[for$='btnVerificarUsuario']").hide();
            $("input[id$='chkIntegradoAlDominio'], label[for$='chkIntegradoAlDominio']").hide();
        }
        //Carga el combo de estado    
        try {
            $Contenedor = $("#reemplazarcboEstado");
            var DatosAImprimir = $.parseJSON(data.jsoncboestado);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron los estados.');
        }
        try {
            $Contenedor = $("#reeemplazarcomboTipoDoc");
            var DatosAImprimir = $.parseJSON(data.jsoncbotipodocumento);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar los tipo de documento.');
        }
        try {
            $Contenedor = $("#reemplazarcomboArea");
            var DatosAImprimir = $.parseJSON(data.jsoncboarea);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);

        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las áreas.');
        }
        //Carga el combo area del filtro de terminales    
        try {
            $Contenedor = $("#replazarPorFiltroDeAreas");
            var DatosAImprimir = $.parseJSON(data.jsoncbofiltroareaterminal);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron cargar las areas del combo filtro de terminales por area.');
        }
    },
    GuardarDialog: function () {
        //Guarda los datos en el servidor.
        page.getDatos();
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarUsuario",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d.ResultadoEjecucion) {
                    bootbox.alert('Los datos se guardaron exitosamente!', function () {
                        $('#dialogAltaEdit').dialog('close');
                        if (page.haydatosengrilla) {
                            $("[id$='btnFiltrar']").click();
                        }
                    });
                }
                else {
                    bootbox.alert(respuesta.d.MensajeError)
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    CancelarDialog: function () {
        var msg = '¿Está seguro que desea Cancelar? Se perderán todas las modificaciones que no han sido guardadas.';
        bootbox.confirm(msg, function (result) {
            if (result) {
                $('#dialogAltaEdit').dialog('close');
            }
        });
    },
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14583
    /// </history>
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
        $("[id$='txtNroDocumento']").validCampoFranz('0123456789');
        //$("[id*='txtUsuario']").val(app.Usuario);
        $("[id$='txtFechaBloq']").hide();
        $("[id$='txtFechaBloq']").datepicker().attr('readonly', true);
        appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaBaja']", "disabled", "true");
        appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaAlta']", "disabled", "true");
        $("[id$='rowErrorUsuario']").css({ 'display': 'none' });
        /*Seteo de los permisos*/
        if (appCIPOLPRESENTACION.trxdatapermiso.blnNuevoVisible)
            $("[id*='btnNuevo']").click(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.AgregarDialog();
                } catch (e) {
                    ShowAlertDanger('Error Inesperado');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });
        else
            $("[id$='btnNuevo']").hide();

        if (appCIPOLPRESENTACION.trxdatapermiso.blnTerminalVisible)
            $("[id$='literminales']").show();
        else
            $("[id$='literminales']").hide();


        if (appCIPOLPRESENTACION.trxdatapermiso.blnHorarioVisible)
            $("[id$='lihorarios']").show();
        else
            $("[id$='lihorarios']").hide();


        if (appCIPOLPRESENTACION.trxdatapermiso.blnAsignarRolVisible)
            $("[id*='cmdAsignar']").click(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.ClickAsignarRol(); return false;
                } catch (e) {
                    ShowAlertDanger('Error Inesperado: Al asignar un rol.');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });
        else
            $("[id$='cmdAsignar']").hide();


        if (appCIPOLPRESENTACION.trxdatapermiso.blnDesasignarRolVisible)
            $("[id*='cmdDesasignar']").click(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.ClickDesasignarRol(); return false;
                } catch (e) {
                    ShowAlertDanger('Error Inesperado: Al desasignar un rol.');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });
        else {
            $("[id$='cmdDesasignar']").hide();
        }
        LoguinSSO = page.ValidarLoginSSO();
        if (LoguinSSO)
        {//Valida autenticación SSO [JorgeI] [19/02/2018] TFS: 10686
            appCIPOLPRESENTACION.SetAtributo("[id$='txtContrasena']", "disabled", "disable");
            $("[id$='txtContrasena']").val('********');
            appCIPOLPRESENTACION.SetAtributo("[id$='txtRepetirContrasena']", "disabled", "disable");
            $("[id$='txtRepetirContrasena']").val('********');
            $("input[id$='chkIntegradoAlDominio'], label[for$='chkIntegradoAlDominio']").hide();
            $("input[id$='chkForzar'], label[for$='chkForzar']").hide();
            $("[id$='btnVerificarUsuario']").hide();
        }
    },
    ValidarLoginSSO: function ()
    {
        //Valida autenticación SSO [JorgeI] [19/02/2018] TFS: 10686
        var urlSSO = false;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ValidarLoginSSO",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                urlSSO = data.d;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
        return urlSSO;
    },
    LimpiarFiltros: function () {
        //Limpieza de los filtros.
        SetTXT('txtFiltro', '');
        $("[id*='cboEstado']").val('0');
        $("[id*='chkSubCadenas']").prop('checked', false);
        $("[id*='rbUsu']").prop('checked', true);
        $("[id*='rbNombre']").prop('checked', false);
        $("[id*='rbArea']").prop('checked', false);
        page.haydatosengrilla = false;
        page.HabilitarControles();
    },
    HabilitarControles: function () {
        $("[id$='btnFiltrar']").removeAttr("disabled");
        $("[id$='txtFiltro']").removeAttr("disabled");
        $("[id$='cboEstado']").removeAttr("disabled");
        $("[id$='chkSubCadenas']").removeAttr("disabled");
        $("[id$='rbUsu']").removeAttr("disabled");
        $("[id$='rbNombre']").removeAttr("disabled");
        $("[id$='rbArea']").removeAttr("disabled");
    },
    DeshabilitarControles: function () {
        $("[id$='btnFiltrar']").attr("disabled", "disabled");
        $("[id$='txtFiltro']").attr("disabled", "disabled");
        $("[id$='cboEstado']").attr("disabled", "disabled");
        $("[id$='chkSubCadenas']").attr("disabled", "disabled");
        $("[id$='rbUsu']").attr("disabled", "disabled");
        $("[id$='rbNombre']").attr("disabled", "disabled");
        $("[id$='rbArea']").attr("disabled", "disabled");
    },
    AgregarDialog: function () {
        $("[id$='rowErrorUsuario']").css({ 'display': 'none' });

        $("[id$='btnAceptar']").css({ 'display': 'block' });

        $("[id$='btnCancelar']").css({ 'display': 'block' });

        $("[id$='btnCerrar']").css({ 'display': 'none' });

        page.LimpiarCampos();
        $("[id*='txtFechaAlta']").prop('value', '');
        var app = appCIPOLPRESENTACION.trxdata;
        app.update = false;
        page.NewElemento();
        page.estadoscontrolesedicion(true);
    },
    ConsultarDialog: function (pID) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = false;

            $("[id$='btnAceptar']").css({ 'display': 'none' });

            $("[id$='btnCancelar']").css({ 'display': 'none' });

            $("[id$='btnCerrar']").css({ 'display': 'block' });
            //Limpia los campos.
            page.LimpiarCampos();
            page.estadoscontrolesedicion(false);
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pID, "C");

        } catch (e) {
            ShowAlertDanger('Error Inesperado: Al consultar un usuario.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }

    },
    ModificarDialog: function (pID) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.update = true;

            $("[id$='btnAceptar']").css({ 'display': 'block' });

            $("[id$='btnCancelar']").css({ 'display': 'block' });

            $("[id$='btnCerrar']").css({ 'display': 'none' });
            //Limpia los campos.
            page.LimpiarCampos();
            page.estadoscontrolesedicion(true);
            //Recupera los datos del elemento.//todo: ver para cancelar sino recupera el elemento.
            page.CargarElemento(pID, "M");

        } catch (e) {
            ShowAlertDanger('Error Inesperado: Al iniciar modificación de usuario.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    bindingControles: function () {

    },
    estadoscontrolesedicion: function (estado) {
        page.blnestadoscontrolesedicion = estado;
        var pdata = appCIPOLPRESENTACION.trxdata;
        if (!estado || pdata.update) {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtUsuario']", "disabled", "disable");
        } else {
            $("[id$='txtUsuario']").removeAttr("disabled");
        }

        if (estado) {
            $("[id*='txtUsuario']").removeAttr("disabled");
            $("[id*='chkCuentaBloqueada']").removeAttr("disabled");
            $("[id$='txtFechaBloq']").removeAttr("disabled");
            $("[id*='txtNombres']").removeAttr("disabled");
            $("[id*='txtFechaBloq']").removeAttr("disabled");
            $("[id*='txtAliasUsuario']").removeAttr("disabled");
            $("[id*='txtDomicilio']").removeAttr("disabled");
            $("[id*='cboTipoDoc']").removeAttr("disabled");
            $("[id*='txtNroDocumento']").removeAttr("disabled");
            $("[id*='txtEmail']").removeAttr("disabled");
            $("[id$='cboArea']").removeAttr("disabled");
            if (LoguinSSO) {
                //Valida autenticación SSO [JorgeI] [19/02/2018] TFS: 10686
                //no se debe ingresar contraseñas por eso se deshabilitan
                appCIPOLPRESENTACION.SetAtributo("[id$='txtContrasena']", "disabled", "disable");
                $("[id$='txtContrasena']").val('********');
                appCIPOLPRESENTACION.SetAtributo("[id$='txtRepetirContrasena']", "disabled", "disable");
                $("[id$='txtRepetirContrasena']").val('********');
                //se ocultan todos los controles de validación de integración de dominio
                $("input[id$='chkIntegradoAlDominio'], label[for$='chkIntegradoAlDominio']").hide();
                $("input[id$='chkForzar'], label[for$='chkForzar']").hide();
                $("[id$='btnVerificarUsuario']").hide();
            }
            else {
                //mixto o integrado
                $("[id$='txtContrasena']").removeAttr("disabled");
                $("[id$='txtRepetirContrasena']").removeAttr("disabled");
                $("[id*='chkForzar']").removeAttr("disabled");
                $("[id*='chkIntegradoAlDominio']").removeAttr("disabled");
                $("[id$='btnVerificarUsuario']").removeAttr("disabled");
            }
            $("[id*='txtComentarios']").removeAttr("disabled");
            $("[id$='cboArea']").removeAttr("disabled");
            $("[id$='btnAsignarTerminalesTodas']").removeAttr("disabled");
            $("[id$='btnDesasignarTerminal']").removeAttr("disabled");
            $("[id$='btnAsignarTerminal']").removeAttr("disabled");
            $("[id$='btnDesasignarTerminalesTodas']").removeAttr("disabled");

            $("[id$='cmdAsignar']").removeAttr("disabled");
            $("[id$='cmdDesasignar']").removeAttr("disabled");
        }
        else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='chkCuentaBloqueada']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaBloq']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtNombres']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaBloq']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtAliasUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtDomicilio']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboTipoDoc']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtNroDocumento']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtEmail']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboArea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtContrasena']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtRepetirContrasena']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='chkIntegradoAlDominio']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='chkForzar']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtComentarios']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboArea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnVerificarUsuario']", "disabled", "disable");
            //terminales
            appCIPOLPRESENTACION.SetAtributo("[id$='btnAsignarTerminalesTodas']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnDesasignarTerminal']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnAsignarTerminal']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnDesasignarTerminalesTodas']", "disabled", "disable");
            //Roles
            appCIPOLPRESENTACION.SetAtributo("[id$='cmdAsignar']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cmdDesasignar']", "disabled", "disable");

        }
    },
    NewElemento: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/NewElementoUsuario",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    $('#dialogAltaEdit').dialog({
                        autoOpen: false,
                        modal: true,
                        buttons: {
                            "Guardar": function () {
                                try {
                                    appCIPOLPRESENTACION.BloquearUI();
                                    page.GuardarDialog();
                                } catch (e) {
                                    ShowAlertDanger('Error Inesperado: Al guardar los datos.');
                                } finally {
                                    appCIPOLPRESENTACION.DesBloquearUI();
                                }
                            },
                            "Cancelar": function () { page.CancelarDialog(); }
                        }
                    });
                    $('#dialogAltaEdit').dialog('open');

                    page.estadoscontrolesedicion(true);
                    page.cargarUsuarioRolesasignadas(data.d.jsontareasasignadas);
                    page.SetTerminales(data.d);
                    page.CargarHorarios(data.d);
                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //[GonzaloP]          [viernes, 15 de julio de 2016]       Work-Item: 7193 - Se agrega el TipoAccion para diferencia si es una Consulta o Modificacion
    CargarElemento: function (IdElemento, TipoAccion) {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoUsuario",
            data: "{Id:'" + IdElemento + "',blnModificar:'" + page.blnestadoscontrolesedicion + "',TipoAccion:'" + TipoAccion + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    appCIPOLPRESENTACION.trxdata = data.d.elemento;
                    appCIPOLPRESENTACION.trxdata.update = true;
                    //Carga los datos en pantalla.
                    page.setDatos();
                    if (page.blnestadoscontrolesedicion) {
                        $('#dialogAltaEdit').dialog({
                            autoOpen: false,
                            modal: true,
                            resizable: false,
                            buttons: {
                                "Guardar": function () {
                                    try {
                                        appCIPOLPRESENTACION.BloquearUI();
                                        page.GuardarDialog();
                                    } catch (e) {
                                        ShowAlertDanger('Error Inesperado: Al guardar los datos.');
                                    } finally {
                                        appCIPOLPRESENTACION.DesBloquearUI();
                                    }
                                },
                                "Cancelar": function () { page.CancelarDialog(); }
                            }
                        });
                    } else {
                        $('#dialogAltaEdit').dialog({
                            autoOpen: false,
                            resizable: false,
                            modal: true,
                            buttons: {
                                "Aceptar": function () { $('#dialogAltaEdit').dialog('close'); }
                            }
                        });
                    }
                    //Abre la ventana de dialogo.
                    $('#dialogAltaEdit').dialog('open');

                    page.SetTerminales(data.d);
                    page.cargarUsuarioRolesHabilitadas(data.d.jsontareasdisponibles);
                    page.cargarUsuarioRolesasignadas(data.d.jsontareasasignadas);
                    //                    page.cargarcboUsuarioRolesasignadas(data.d.jsoncbotareasasignadas);
                    page.CargarHorarios(data.d);

                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    GuardarDatos: function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            page.GuardarDialog();
        } catch (e) {
            ShowAlertDanger('Error Inesperado: Al guardar los datos.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        app.Usuario = $("[id*='txtUsuario']").val();
        app.blnCtaBloqueada = $("[id*='chkCuentaBloqueada']").prop('checked');
        app.Nombres = $("[id*='txtNombres']").val();
        app.strFechaBloqueo = $("[id*='txtFechaBloq']").val();
        app.ALIAS_USUARIO = $("[id*='txtAliasUsuario']").val();
        app.Domicilio = $("[id*='txtDomicilio']").val();
        app.IdTipoDoc = $("[id*='cboTipoDoc']").val();
        if (app.IdTipoDoc == null || app.IdTipoDoc == '')
            app.IdTipoDoc = -1;
        app.NroDocumento = $("[id*='txtNroDocumento']").val();
        app.Email = $("[id*='txtEmail']").val();
        app.idArea = $("[id$='cboArea']").val();
        if (app.idArea == null || app.idArea == '')
            app.idArea = -1;
        app.blnIntegradoAlDominio = $("[id*='chkIntegradoAlDominio']").prop('checked');
        app.blnForzarCambios = $("[id*='chkForzar']").prop('checked');
        app.Comentario = $("[id*='txtComentarios']").val();
        app.strFechaAlta = $("[id*='txtFechaAlta']").val();
        app.strFechaBaja = $("[id*='txtFechaBaja']").val();
        //Contrasena.
        app.contrasenia = $("[id$='txtContrasena']").val();
        app.repetircontrasenia = $("[id$='txtRepetirContrasena']").val();

        //Recuperación de las tareas asginadas al rol.
        app.lsttareas = [];
        //Recupera las tareas seleccionadas.
        $("[id*='treeasignadosRoles'] li:not(:has(ul))").map(function () {
            app.lsttareas.push({ 'Id': $(this).attr('id'), 'nombre': $(this).find("a").first().text(), 'asignada': ($(this).attr("class").toLowerCase().indexOf('asignada'.toLowerCase()) != -1) });
        });

        //Guarda los horarios
        app.lstHorarios = [];
        $("#horarios .dia-hora").not(".alert-danger").each(function () {
            var item = $(this).attr('id').split('-');
            app.lstHorarios.push({ 'idDia': item[0], 'idHorario': item[1] });
        });



    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        $("[id*='txtUsuario']").val(app.Usuario);
        $("[id*='chkCuentaBloqueada']").prop('checked', app.blnCtaBloqueada);
        $("[id*='txtNombres']").val(app.Nombres);
        $("[id*='txtFechaBloq']").val(app.strFechaBloqueo);
        $("[id*='txtAliasUsuario']").val(app.ALIAS_USUARIO);
        $("[id*='txtDomicilio']").val(app.Domicilio);
        $("[id*='cboTipoDoc']").val(app.IdTipoDoc);
        $("[id*='txtNroDocumento']").val(app.NroDocumento);
        $("[id*='txtEmail']").val(app.Email);
        $("[id$='cboArea']").val(app.idArea);

        //[MiguelP]         28/10/2014      GCP - Cambios 15598
        if ($("[id$='cboArea']").val() == null) {
            bootbox.alert('El área actual está dada de baja. Seleccione nueva área.');
        }

        if (app.FICTICIA == 'S') {
            $("[id$='cboArea']").prepend($("<option></option>").attr("value", '-1').text(''));
            $("[id$='lblMensajeArea']").text('Usuario asociado al área ficticia <strong>\"' + app.NombreArea + '\"</strong>, debe asignar otra área.');
            $("[id$='lblMensajeArea']").show();
        }
        $("[id*='txtContrasena']").val('');
        $("[id*='txtRepetirContrasena']").val('');

        $("[id*='txtComentarios']").val(app.Comentario);
        $("[id*='txtFechaAlta']").prop('value', app.strFechaAlta);
        $("[id*='txtFechaBaja']").val(app.strFechaBaja);
        //Contrasena.
        $("[id$='txtContrasena']").val(app.contrasenia);
        $("[id$='txtRepetirContrasena']").val(app.repetircontrasenia);

        $("[id*='chkIntegradoAlDominio']").prop('checked', app.blnIntegradoAlDominio);
        $("[id*='chkForzar']").prop('checked', app.blnForzarCambios);

        page.HabilitarBloqueo();
        //page.HabilitarContrasena();
    },
    LimpiarCampos: function () {

        $("[id*='txtUsuario']").val('');
        $("[id*='chkCuentaBloqueada']").prop('checked', false);
        page.HabilitarBloqueo();
        $("[id*='txtNombres']").val('');
        $("[id*='txtAliasUsuario']").val('');
        $("[id*='txtDomicilio']").val('');
        $("[id*='cboTipoDoc']").val('0');
        $("[id*='txtNroDocumento']").val('');
        $("[id*='txtEmail']").val('');
        $("[id$='cboArea']").val('0');
        $("[id*='txtContrasena']").val('');
        $("[id*='txtRepetirContrasena']").val('');
        $("[id*='chkIntegradoAlDominio']").prop('checked', false);
        $("[id*='chkForzar']").prop('checked', false);
        page.HabilitarContrasena();
        $("[id*='txtComentarios']").val('');
        $("[id*='txtFechaBaja']").val('');
        $("[id$='lblMensajeArea']").text('');
        $("[id$='lblMensajeArea']").hide();
        //Remueve la opción.
        $("[id$='cboArea'] option[value='-1']").remove();
        $("[id$='rowErrorUsuario']").css({ 'display': 'none' });
    },
    VerificarUsuario: function () {
        var usuario = $("[id*='txtUsuario']").val();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/Verificarusuario",
            data: "{usuario:'" + escape(usuario) + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                bootbox.alert(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Eliminar: function (id, usuario, fechabaja, validar) {

        if (id == 0) {
            bootbox.alert('El superusuario <strong>\"' + unescape(usuario) + '\"</strong> no puede ser Eliminado. Imposible continuar');
            return;
        }
        try {

            appCIPOLPRESENTACION.BloquearUI();
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/EliminarUsuario",
                data: "{Id : " + id + ",usuario : '" + usuario + "',fechabaja : '" + fechabaja + "',validar : " + validar + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.ResultadoEjecucion) {
                        if (data.d.pregunta) {
                            bootbox.confirm(data.d.MensajeError, function (result) {
                                if (result) {
                                    page.Eliminar(id, usuario, fechabaja, false);
                                }
                            });
                        }
                        else {
                            //Carga la página.
                            bootbox.alert(data.d.MensajeServicio, function () { $("[id$='btnFiltrar']").click(); });
                        }
                    } else {

                        bootbox.alert(data.d.MensajeError);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        } catch (e) {
            ShowAlertDanger('Error Inesperado: Al eliminar.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    //TERMINALES
    //    AgregarDialogTerminales: function () {
    //        page.InicializarVariales();
    //        var app = appCIPOLPRESENTACION.trxdata;
    //        page.InicializarFormularioAlertas();
    //        if (page.blnestadoscontrolesedicion) {
    //            $('#dialogTerminales').dialog({
    //                autoOpen: false,
    //                modal: true,
    //                title: "Acceso Desde Terminales - Usuario: " + app.Usuario,
    //                buttons: {
    //                    "Aceptar": function () {
    //                        try {
    //                            appCIPOLPRESENTACION.BloquearUI();
    //                            page.AceptarTerminalesDialog();
    //                        } catch (e) {
    //                            ShowAlertDanger('Error Inesperado: Al guardar los datos. ' + e);
    //                        } finally {
    //                            appCIPOLPRESENTACION.DesBloquearUI();
    //                        }
    //                    },
    //                    "Cerrar": function () { $(this).dialog("close"); }
    //                },
    //                close: function () { page.CerrarTerminalesDialog(); }
    //            });
    //        } else {
    //            $('#dialogTerminales').dialog({
    //                autoOpen: false,
    //                modal: true,
    //                title: "Acceso Desde Terminales - Usuario: " + app.Usuario,
    //                buttons: {
    //                    "Aceptar": function () { $(this).dialog("close"); }
    //                },
    //                close: function () { page.CerrarTerminalesDialog(); }
    //            });
    //        }
    //        $('#dialogTerminales').dialog('open');

    //page.estadoscontrolesedicion(page.blnestadoscontrolesedicion);
    //   },
    //inicializa las variables de sesión 
    InicializarVariales: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/InicializarVariales",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function () {

            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    BindingControlesTerminales: function () {
        if (page.blnestadoscontrolesedicion) {
            $("[id$='lstTerminalesHabilitadas']").dblclick(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.ClickAsignarTerminal(); return false;
                } catch (e) {
                    ShowAlertDanger('Error Inesperado: Al asignar la terminal.');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });
            $("[id$='lstTerminalesNOHabilitadas']").dblclick(function () {
                try {
                    appCIPOLPRESENTACION.BloquearUI();
                    page.ClickDesasignarTerminal(); return false;
                } catch (e) {
                    ShowAlertDanger('Error Inesperado: Al desasignar la terminal.');
                } finally {
                    appCIPOLPRESENTACION.DesBloquearUI();
                }
            });
        }
    },
    //Se incializa el formulario de terminales
    InicializarFormularioAlertas: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/InicializarFormularioAlertas",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                page.cargarTerminalesHabilitadas(respuesta.d.jsonterminaleshabilitadas);
                page.cargarTerminalesNOHabilitadas(respuesta.d.jsonterminalesnohabilitadas);
                page.BindingControlesTerminales()
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Establece el estado incial de las listas de temrianles habilitadas y terminales NO habilitadas
    SetTerminales: function (data) {
        //Carga el elemento base del ABM.
        page.cargarTerminalesHabilitadas(data.jsonterminaleshabilitadas);
        page.cargarTerminalesNOHabilitadas(data.jsonterminalesnohabilitadas);
    },
    //Función que maneja el evento que se dispara cuando se pasan las terminales de habilitadas a NO habilitadas (>>)
    ClickAsignarTerminalesTodas: function () {
        var obj = [];
        $("[id*='lstTerminalesHabilitadas'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AsignarTerminalesTodas",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstTerminalesHabilitadas'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstTerminalesNOHabilitadas']");
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
    //Función que maneja el evento que se dispara cuando se pasa una terminal de habilitada a NO habilitada (>)
    ClickAsignarTerminal: function () {
        $("[id*='lstTerminalesHabilitadas'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AsignarTerminal",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstTerminalesNOHabilitadas']");;
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
    //Función que maneja el evento que se dispara cuando se pasa una terminal de NO habilitada a habilitada (<)
    ClickDesasignarTerminal: function () {
        $("[id*='lstTerminalesNOHabilitadas'] :selected").each(function () {
            var id = $(this).val();
            var nombre = $(this).text();
            var option = $(this);
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/DesasignarTerminal",
                data: "{Id:'" + id + "',nombre:'" + nombre + "'}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (respuesta) {
                    if (respuesta.d == "") {
                        option.remove().appendTo("[id*='lstTerminalesHabilitadas']");
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
    //Función que maneja el evento que se dispara cuando se pasan todas las terminales de NO habilitadas a habilitadas (<<)
    ClickDesasignarTerminalesTodas: function () {
        var obj = [];
        $("[id*='lstTerminalesNOHabilitadas'] option").each(function () {
            obj.push({ 'Id': $(this).val(), 'nombre': $(this).text() });
        });
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/DesasignarTerminalesTodas",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d == "") {
                    $("[id*='lstTerminalesNOHabilitadas'] option").each(function () {
                        $(this).remove().appendTo("[id*='lstTerminalesHabilitadas']");
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
    //Función que filtra las terminales habilitadas por area
    BuscarTerminalesPorArea: function () {
        var idArea = $("[id$='cboAreaFiltro']").val();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/BuscarTerminalesPorArea",
            data: "{idArea:'" + idArea + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                if (respuesta.d != "") {
                    page.cargarTerminalesHabilitadas(respuesta.d);
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
    cargarTerminalesNOHabilitadas: function (jsonterminalesnohabilitadas) {
        //LISTBOX TERMINALES NO HABILITADASS.
        try {

            $ContenedorlbTerminalesNOHabilitadas = $("#lbTerminalesNOHabilitadas");
            var DatosAImprimirlbTerminalesNOHabilitadas = $.parseJSON(jsonterminalesnohabilitadas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbTerminalesNOHabilitadas.Lista = $("<div/>").html(DatosAImprimirlbTerminalesNOHabilitadas.Lista).text();
            $ContenedorlbTerminalesNOHabilitadas.children().remove(); //TODO
            $ContenedorlbTerminalesNOHabilitadas.append(DatosAImprimirlbTerminalesNOHabilitadas.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las terminales NO habilitadas para el usuario.');
        }
        //FIN LISTBOX TERMINALES NO HABILITADAS.
    },
    cargarTerminalesHabilitadas: function (jsonterminaleshabilitadas) {
        //LISTBOX TERMINALES HABILITADAS.
        try {

            $ContenedorlbTerminalesHabilitadas = $("#lbTerminalesHabilitadas");
            var DatosAImprimirlbTerminalesHabilitadas = $.parseJSON(jsonterminaleshabilitadas);
            //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
            //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
            //entities y obtener el código HTML original.
            DatosAImprimirlbTerminalesHabilitadas.Lista = $("<div/>").html(DatosAImprimirlbTerminalesHabilitadas.Lista).text();
            $ContenedorlbTerminalesHabilitadas.children().remove(); //TODO
            $ContenedorlbTerminalesHabilitadas.append(DatosAImprimirlbTerminalesHabilitadas.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las terminales habilitadas para el usuario.');
        }
        //FIN LISTBOX TERMINALES HABILITADAS.
    },
    // Funcion que se ejecuta cuando se cierra el dialog de terminales desde el boton "Cerrar" o desde el boton superior derecho del formulario
    CerrarTerminalesDialog: function () {
        appCIPOLPRESENTACION.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/CerrarFormularioTerminales",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                $('#dialogTerminales').dialog('close');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                bootbox.alert('Error al filtrar las terminales habilitadas.');
            },
            complete: function () { appCIPOLPRESENTACION.DesBloquearUI(); }
        });
    },
    // Funcion que se ejecuta cuando se cierra el dialog de terminales desde el boton "Aceptar"
    AceptarTerminalesDialog: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AceptarTerminalesDialog",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (respuesta) {
                $('#dialogTerminales').dialog('close');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //FIN TERMINALES
    //ROLES
    initRoles: function () {
        $("[id*='btnRoles']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                $("[id$='cbousuariorolesasignados']").addClass("col-md-6");
                page.AgregarDialogRoles();
            } catch (e) {
                ShowAlertDanger('Error Inesperado: ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

    },
    AddRol: function ($Rol) {
        $("[id*='cbousuariorolesasignados']").append('<option value="' + $Rol.attr('id') + '" selected="selected" >' + $Rol.find('a').first().text() + '</option>');
        $("[id*='cbousuariorolesasignados']").addClass("col-xs-4 select-no-padding");
    },

    SetAutocompleteRoles: function () {
        $("[id*='cbousuariorolesasignados']").combobox({
            select: function (event, ui) {
                page.selectrol(ui.item.id);
            }
        });
    },
    AgregarDialogRoles: function () {
        var app = appCIPOLPRESENTACION.trxdata; //+ app.Usuario
        page.RecuperarDatosUsuarioRoles();

        //        if (page.blnestadoscontrolesedicion) {
        //            $('#dialogRoles').dialog({
        //                autoOpen: false,
        //                modal: true,
        //                title: "Asignación de roles a " + app.Usuario,
        //                buttons: {
        //                    "Aceptar": function () {
        //                        try {
        //                            appCIPOLPRESENTACION.BloquearUI();
        //                            page.AceptarRolDialog();
        //                        } catch (e) {
        //                            ShowAlertDanger('Error Inesperado: Al guardar los datos. ' + e);
        //                        } finally {
        //                            appCIPOLPRESENTACION.DesBloquearUI();
        //                        }
        //                    },
        //                    "Cerrar": function () { $(this).dialog("close"); }
        //                },
        //                close: function () { page.CerrarRolDialog(); }
        //            });
        //        }
        //        else {
        //            $('#dialogRoles').dialog({
        //                autoOpen: false,
        //                modal: true,
        //                title: "Asignación de roles a " + app.Usuario,
        //                buttons: {
        //                    "Aceptar": function () { $(this).dialog("close"); }
        //                },
        //                close: function () { page.CerrarRolDialog(); }
        //            });
        //        }
        //        $('#dialogRoles').dialog('open');
        //page.SetAutocompleteRoles();
    },
    //    AceptarRolDialog: function () {

    //        $.ajax({
    //            type: "POST",
    //            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AceptarFormularioUsuarioRoles",
    //            data: "",
    //            contentType: "application/json; charset=iso-8859-1",
    //            dataType: "json",
    //            async: false,
    //            success: function (respuesta) {
    //                $('#dialogRoles').dialog('close');
    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                throw xhr.responseJSON;
    //            }
    //        });
    //    },
    //    CerrarRolDialog: function () {
    //        appCIPOLPRESENTACION.BloquearUI();
    //        $.ajax({
    //            type: "POST",
    //            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/CerrarFormularioUsuarioRoles",
    //            data: "",
    //            contentType: "application/json; charset=iso-8859-1",
    //            dataType: "json",
    //            async: false,
    //            success: function (respuesta) {
    //                $('#dialogRoles').dialog('close');
    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                ShowAlertDanger('Error al filtrar las terminales habilitadas.');
    //            },
    //            complete: function () { appCIPOLPRESENTACION.DesBloquearUI(); }
    //        });
    //    },
    RecuperarDatosUsuarioRoles: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarUsuarioRolCarga",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    page.cargarUsuarioRolesHabilitadas(data.d.jsontareasdisponibles);
                    page.cargarUsuarioRolesasignadas(data.d.jsontareasasignadas);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    cargarUsuarioRolesHabilitadas: function (json) {
        try {
            //martinv GCP 15576
            if (json != '') {
                appCIPOLPRESENTACION.rendercontrol($("#treeRoles"), json);
                page.vincularClickLista($("#treeRoles"), false, false, page.claseSelect, false);
            }
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas habilitadas para el usuario.');
        }
    },
    cargarUsuarioRolesasignadas: function (json) {
        try {
            appCIPOLPRESENTACION.rendercontrol($("[id*='treeasignadosRoles']"), json);
            page.vincularClickLista($("[id*='treeasignadosRoles']"), false, false, 'selectedAsig', true);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas del usuario.');
        }
    },
    ////    cargarcboUsuarioRolesasignadas: function (json) {
    //        try {
    //            appCIPOLPRESENTACION.rendercontrol($("[id*='cbotreeasignadosRoles']"), json);
    //        }
    //        catch (e) {
    //            ShowAlertDanger('Error Inesperado: No se pudieron recuperar las tareas del usuario.');
    //        }
    //    },
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
			})
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
            $Entorno.find('.nivel4 a').off('dblclick.bloquear');
            $Entorno.find('.nivel4 a').on('dblclick.bloquear', function () {
                if (!page.blnestadoscontrolesedicion)
                    return;
                page.tempBloqAsig = null;
                page.AccionBloqueadaAsignada($(this));
            });
            $Entorno.find(':not(.nivel4)').find('a').off('dblclick.bloquearInterior');
            $Entorno.find(':not(.nivel4)').find('a').on('dblclick.bloquearInterior', function () {
                if (!page.blnestadoscontrolesedicion)
                    return;
                page.tempBloqAsig = null;
                $(this).parent().find('.nivel4 a').each(function () { page.AccionBloqueadaAsignada($(this)); });
            });

        }
    },
    selectrol: function (Id) {
        $Asignadas = $("#treeasignadosRoles");
        $find = $Asignadas.find("[Id='" + Id + "']");
        var cssSelected = 'selectedAsig';
        if ($find.length > 0) {
            $Asignadas.find('a').removeClass(cssSelected);
            $find.find('a').first().addClass(cssSelected);
        }
    },
    AccionBloqueadaAsignada: function ($Tarea) {
        if (page.tempBloqAsig == null)
            page.tempBloqAsig = ($Tarea.parent().find('img').attr("src").toLowerCase().indexOf('block'.toLowerCase()) != -1);

        if (page.tempBloqAsig) {
            $Tarea.parent().find('img').attr("src", "./Imagenes/task-assign-icon.png");
            $Tarea.parent().removeClass('bloqueada');
            $Tarea.parent().addClass('asignada');
        } else {
            $Tarea.parent().find('img').attr("src", "./Imagenes/task-block-icon.png");
            $Tarea.parent().removeClass('asignada');
            $Tarea.parent().addClass('bloqueada');
        }
    },
    ClickAsignarRol: function () {
        var Id = $('#treeRoles .' + page.claseSelect).parent().attr('id');
        if (Id == null)
            return;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/asignarTareaUsuarioRol",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    page.asignartarea($('#treeRoles .' + page.claseSelect));
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
        $Asignadas = $("#treeasignadosRoles");
        var position = "";
        var Id = $seleccion.parent().attr('id');
        var valores = Id.split('/');
        //Ejemplo: "0/2/3/1095"
        switch (valores.length) {
            case 1:
                //mover Rol
                $find = $Asignadas.find("[Id='" + Id + "']");
                if ($find.length > 0) {
                    //existe el rol
                    $seleccion.parent().find('li').each(function () {
                        $findGrupo = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                        if ($findGrupo.length > 0) {
                            $findGrupo.parent().find('li').each(function () {
                                $findSistema = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                                if ($findSistema.length > 0) {
                                    //Existe el sistema
                                    $(this).find('li').each(function () {
                                        $findtarea = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                                        if ($findtarea.length > 0)
                                        { } else {
                                            $Tarea = $(this).clone();
                                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                                            $Tarea.addClass('asignada');
                                            $findGrupo.find("ul").first().append($Tarea);
                                        }
                                    });
                                } else {
                                    //no existe el sistema.
                                    $Sistema = $(this).clone();
                                    $Sistema.find('a').removeClass(page.claseSelect);
                                    $Sistema.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                                    $Sistema.find('.nivel4 li').addClass('asignada');
                                    $findGrupo.find("ul").first().append($Sistema);
                                }
                            });

                        } else {
                            //no existe el grupo.
                            $Grupo = $(this).clone();
                            $Grupo.find('a').removeClass(page.claseSelect);
                            $Grupo.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Grupo.find('.nivel4 li').addClass('asignada');
                            $find.find("ul").first().append($Grupo);
                        }
                    });
                } else {
                    //No existe el rol.
                    $Rol = $seleccion.parent().clone();
                    $Rol.find('a').removeClass(page.claseSelect);
                    $Rol.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                    $Rol.find('.nivel4 li').addClass('asignada');
                    $Asignadas.find("ul").first().append($Rol);
                    page.AddRol($Rol);
                }
                break;
            case 2:
                //MOVER GRUPO.
                $find = $Asignadas.find("[Id='" + Id + "']");
                if (!($find.length > 0)) {
                    //No existe el GRUPO
                    $findRol = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().attr('id') + "']");
                    if (!($findRol.length > 0)) {
                        //No existe el Rol
                        $Rol = $seleccion.parent().parent().parent().clone();
                        $Rol.find('li').remove();
                        $Grupo = $seleccion.parent().clone();
                        $Grupo.find('a').removeClass(page.claseSelect);
                        $Grupo.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Grupo.find('.nivel4 li').addClass('asignada');
                        $Rol.find('ul').append($Grupo);
                        $Asignadas.find("ul").first().append($Rol);
                        page.AddRol($Rol);
                    } else {
                        //Existe el rol
                        $Grupo = $seleccion.parent().clone();
                        $Grupo.find('a').removeClass(page.claseSelect);
                        $Grupo.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Grupo.find('.nivel4 li').addClass('asignada');
                        $findRol.find("ul").first().append($Grupo);
                    }

                } else {
                    //Existe el Grupo.
                    $seleccion.parent().find('li').each(function () {
                        $findSistema = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                        if ($findSistema.length > 0) {
                            //Existe el sistema
                            $(this).find('li').each(function () {
                                $findtarea = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                                if ($findtarea.length > 0)
                                { } else {
                                    $Tarea = $(this).clone();
                                    $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                                    $Tarea.addClass('asignada');
                                    $find.find("ul").first().append($Tarea);
                                }

                            });
                        } else {
                            //no existe el sistema.
                            $Sistema = $(this).clone();
                            $Sistema.find('a').removeClass(page.claseSelect);
                            $Sistema.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Sistema.find('.nivel4 li').addClass('asignada');
                            $find.find("ul").first().append($Sistema);
                        }
                    });
                }
                break;
            case 3:
                //MOVER SISTEMA.
                $find = $Asignadas.find("[Id='" + Id + "']");
                if (!($find.length > 0)) {
                    //No existe el sistema
                    $findGrupo = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().attr('id') + "']");
                    if (!($findGrupo.length > 0)) {
                        //No existe el grupo
                        $findRol = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().parent().parent().attr('id') + "']");
                        if (!($findRol.length > 0)) {
                            //No existe el rol.
                            $Rol = $seleccion.parent().parent().parent().parent().parent().clone();
                            $Rol.find('li').remove();
                            $Grupo = $seleccion.parent().parent().parent().clone();
                            $Grupo.find('li').remove();
                            $Sistema = $seleccion.parent().clone();
                            $Sistema.find('a').removeClass(page.claseSelect);
                            $Sistema.find('.nivel3 img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Sistema.find('.nivel3 li').addClass('asignada');
                            $Grupo.find('ul').append($Sistema);
                            $Rol.find('ul').append($Grupo);
                            $Asignadas.find("ul").first().append($Rol);
                            page.AddRol($Rol);
                        } else {
                            //Existe el rol
                            $Grupo = $seleccion.parent().clone();
                            $Grupo.find('a').removeClass(page.claseSelect);
                            $Grupo.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Grupo.find('.nivel4 li').addClass('asignada');
                            $findRol.find("ul").first().append($Grupo);
                        }
                    }
                    else {
                        //Existe el grupo
                        $Sistema = $seleccion.parent().clone();
                        $Sistema.find('a').removeClass(page.claseSelect);
                        $Sistema.find('.nivel4 img').attr("src", "./Imagenes/task-assign-icon.png");
                        $Sistema.find('.nivel4 li').addClass('asignada');
                        $findGrupo.find("ul").first().append($Sistema);
                    }
                } else {
                    //Existe el sistema.
                    $seleccion.parent().find('li').each(function () {
                        $findtarea = $Asignadas.find("[Id='" + $(this).attr('id') + "']");
                        if ($findtarea.length > 0)
                        { } else {
                            $Tarea = $(this).clone();
                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Tarea.addClass('asignada');
                            $find.find("ul").first().append($Tarea);
                        }
                    });
                }
                break;
            case 4:
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
                            $findRol = $Asignadas.find("[Id='" + $seleccion.parent().parent().parent().parent().parent().parent().parent().attr('id') + "']");
                            if (!($findRol.length > 0)) {
                                //Np existe rol.
                                $Rol = $seleccion.parent().parent().parent().parent().parent().parent().parent().clone();
                                $Rol.find('li').remove();
                                $Grupo = $seleccion.parent().parent().parent().parent().parent().clone();
                                $Grupo.find('li').remove();
                                $Sistema = $seleccion.parent().parent().parent().clone();
                                $Sistema.find('li').remove();
                                $Tarea = $seleccion.parent().clone();
                                $Tarea.find('a').removeClass(page.claseSelect);
                                $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                                $Tarea.addClass('asignada');
                                $Sistema.find('ul').append($Tarea);
                                $Grupo.find('ul').append($Sistema);
                                $Rol.find('ul').append($Grupo);
                                $Asignadas.find("ul").first().append($Rol);
                                page.AddRol($Rol);
                            } else {
                                //Existe Rol.
                                $Grupo = $seleccion.parent().parent().parent().parent().parent().clone();
                                $Grupo.find('li').remove();
                                $Sistema = $seleccion.parent().parent().parent().clone();
                                $Sistema.find('li').remove();
                                $Tarea = $seleccion.parent().clone();
                                $Tarea.find('a').removeClass(page.claseSelect);
                                $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                                $Tarea.addClass('asignada');
                                $Sistema.find('ul').append($Tarea);
                                $Grupo.find('ul').append($Sistema);
                                $findRol.find("ul").first().append($Grupo);
                            }
                        }
                        else {
                            $Sistema = $seleccion.parent().parent().parent().clone();
                            $Sistema.find('li').remove();
                            $Tarea = $seleccion.parent().clone();
                            $Tarea.find('a').removeClass(page.claseSelect);
                            $Tarea.find('img').attr("src", "./Imagenes/task-assign-icon.png");
                            $Tarea.addClass('asignada');
                            $Sistema.find('ul').append($Tarea);
                            $findgrupo.find("ul").first().append($Sistema);
                        }
                    }
                    else {
                        $Tarea = $seleccion.parent().clone();
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

        page.vincularClickLista($("[id*='treeasignadosRoles']"), false, true, 'selectedAsig', true);
        //alert($("#dvTareasAsignadas").scrollTop());
        //$("#dvTareasAsignadas").scrollTop($("#dvTareasAsignadas").scrollTop() + position);
    },
    ClickDesasignarRol: function () {
        var Id = $('.selectedAsig').parent().attr('id');
        if (Id == null)
            return;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/desasignarTareaUsuarioRol",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    page.desasignarTarea($('.selectedAsig'));
                } else {
                    bootbox.alert(data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //martinv -> Desasignar una tarea,sistema,grupo o rol. Siempre se quita el rol completo.
    desasignarTarea: function ($seleccion) {

        $Asignadas = $("[id*='treeasignadosRoles']")
        var Id = $seleccion.parent().attr('id');
        var valores = Id.split('/');
        //Ejemplo: "0/2/1095".
        var idRol = valores[0];
        $Asignadas.find("[Id='" + idRol + "']").remove();
    },
    //FINROLES

    //HORARIOS
    //    AgregarDialogHorarios: function () {
    //        var app = appCIPOLPRESENTACION.trxdata;
    //        page.CargarHorarios();
    //        if (page.blnestadoscontrolesedicion) {
    //            $('#dialogHorarios').dialog({
    //                autoOpen: false,
    //                title: "Horarios - Usuario: " + app.Usuario,
    //                modal: true,
    //                buttons: {
    //                    "Aceptar": function () {
    //                        try {
    //                            appCIPOLPRESENTACION.BloquearUI();
    //                            page.GuardarHorarios();
    //                        } catch (e) {
    //                            ShowAlertDanger('Error Inesperado: Al guardar los datos. ' + e);
    //                        } finally {
    //                            appCIPOLPRESENTACION.DesBloquearUI();
    //                        }
    //                    },
    //                    "Cerrar": function () { $(this).dialog("close"); }
    //                }
    //            });
    //        }
    //        else {
    //            $('#dialogHorarios').dialog({
    //                autoOpen: false,
    //                title: "Horarios - Usuario: " + app.Usuario,
    //                modal: true,
    //                buttons: {
    //                    "Aceptar": function () { $(this).dialog("close"); }
    //                }
    //            });
    //        }
    //        $('#dialogHorarios').dialog('open');
    //    },

    CargarHorarios: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarHorarios",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                appCIPOLPRESENTACION.rendercontrol($("#horarios"), data.d);

                if (page.blnestadoscontrolesedicion) {
                    $('.seleccionar-columna').click(function () {
                        var columna = $(this).attr('id');
                        page.SeleccionarColumnaHora(columna);
                    });
                    $('.seleccionar-fila').click(function () {
                        var fila = $(this).attr('id');
                        page.SeleccionarFilaDia(fila);
                    });

                    $(".dia-hora").click(function () {
                        $(this).toggleClass("alert-danger");
                    });
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowAlertDanger('Error al recuperar la carga de los datos.');
            }
        });
    },
    //    GuardarHorarios: function () {
    //        var obj = [];
    //        $(".seleccionado").each(function () {
    //            var item = $(this).attr('id').split('-');
    //            obj.push({ 'idDia': item[0], 'idHorario': item[1] });
    //        });
    //        $.ajax({
    //            type: "POST",
    //            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/GuardarHorarios",
    //            data: "{'dias_horas':" + JSON.stringify(obj) + "}",
    //            contentType: "application/json; charset=iso-8859-1",
    //            dataType: "json",
    //            async: false,
    //            success: function (data) {
    //                $('#dialogHorarios').dialog('close');
    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                throw xhr.responseJSON;
    //            }
    //        });
    //    },
    SeleccionarColumnaHora: function (columna) {
        var cantidadElemetnos = $("[id$=-" + columna + "].alert-danger").length;
        if (cantidadElemetnos > 0) {
            $("[id$=-" + columna + "].dia-hora").removeClass('alert-danger');
        }
        else {
            $("[id$=-" + columna + "].dia-hora").addClass('alert-danger');
        }
    },
    SeleccionarFilaDia: function (fila) {
        var cantidadElemetnos = $("[id^=" + fila + "-].alert-danger").length;
        if (cantidadElemetnos > 0) {
            $("[id^=" + fila + "-].dia-hora").removeClass('alert-danger');
        }
        else {
            $("[id^=" + fila + "-].dia-hora").addClass('alert-danger');
        }
    }
    //FINHORARIOS
}
//function CaracteresExtranios() {
//    if (event.keyCode == 39)
//        event.returnValue = false;
//};














