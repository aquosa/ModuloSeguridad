/// <summary>
/// Script asociado a todas las operaciones realizadas en la pantalla "Analisis de Auditoria"
/// </summary>
/// <history>
/// [IvanSa]          [Lunes, 21 de octubre de 2013]       Creado GCP-Cambios ID: 14499
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    }
    catch (e) {
       ShowAlertDanger( 'Error al recuperar la carga de los datos.' );
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});
function CaracteresExtranios(objeto, e) {
    $.browser.firefox = /firefox/.test(navigator.userAgent.toLowerCase());

    var keynum;

    if (window.event) {
        keynum = e.keyCode;
    }
    else if (e.which) {
        keynum = e.which;
    };

    if (keynum == 39)
        if (!$.browser.firefox) {
            event.returnValue = false;
        }
        else {
            e.preventDefault();
        };
};
var page = {
    mensaje: "",
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        page.CargarPagina();
        page.ConfiguracionInicial();

        $("[id$='btnGenerarSQL']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.GenerarSQL();
                return false;
            } catch (e) {
                ShowAlertDanger('Error al recuperar el log eventos de auditoria.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        $("[id$='btnExportarExcel']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.ImprimirReporte();
                return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: No se pudo imprimir el análisis de auditoría.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

        $('#dialogStringSQL').dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            buttons: {
                "COPIAR": function () {
                    try {
                        appCIPOLPRESENTACION.BloquearUI();
                        page.CopiarStringSQL(GetTXT('txtStringSQL'));
                        return false;
                    } catch (e) {
                        ShowAlertDanger('Error Inesperado: No se pudo copiar.');
                    } finally {
                        appCIPOLPRESENTACION.DesBloquearUI();
                    }
                },
                "EXPORTAR": function () {
                    try {
                        appCIPOLPRESENTACION.BloquearUI();
                        page.ExportarStringSQL(GetTXT('txtStringSQL'));
                        return false;
                    } catch (e) {
                        ShowAlertDanger('Error al Generar el archivo SQL.');
                    } finally {
                        appCIPOLPRESENTACION.DesBloquearUI();
                    }
                },
                "CERRAR": function () { $(this).dialog('close'); }
            }
        });
    },
    //Limpieza de los filtros.
    LimpiarFiltros: function () {
        var fecha = $.datepicker.formatDate('dd/mm/yy', new Date())
        SetTXT('txtFechaDesde', fecha);
        SetTXT('txtFechaHasta', fecha);
        SetDDL('cboTabla', '(TODAS)');
        SetDDL('cboUsuario', '(TODOS)');
        SetDDL('cboOperacion', 'T');
        SetDDL('cboSistema', '(TODOS)');
        SetDDL('cboTerminales', '(TODAS)');
        //[MiguelP]         28/10/2014      GCP - Cambios 15599 - Limpia el campo de filtrado
        SetDDL('txtTextoBusqueda', '');
    },
    CargarPagina: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/CargarPagina",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {
                    page.cargarDatos(data.d);
                }
                else {
                    ShowAlertDanger('Error al recuperar la carga de los datos.' + data.d.MensajeError);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON
            }
        });
    },
    cargarDatos: function (data) {
        appCIPOLPRESENTACION.trxdata = data.elemento;

        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        appCIPOLPRESENTACION.trxFiltro = data.objFiltro;

        //Carga el combo de tablas    
        try {
            $Contenedor = $("#reemplazarcomboTabla");
            var DatosAImprimir = $.parseJSON(data.jsoncbotablas);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudo cargar el combo de tablas.');
        }
        //Carga el combo de usuarios    
        try {
            $Contenedor = $("#reemplazarcomboUsuario");
            var DatosAImprimir = $.parseJSON(data.jsoncbousuarios);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudo cargar el combo de usuarios.');
        }
        //Carga el combo de codigo de nombre pc   
        try {
            $Contenedor = $("#remplazarcomboNombrePC");
            var DatosAImprimir = $.parseJSON(data.jsoncbonombrepc);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudo cargar el combo con los nombre de las PCs.');
        }
        //Carga el combo de codigo de sistemas 
        try {
            $Contenedor = $("#remplazarcomboSistema");
            var DatosAImprimir = $.parseJSON(data.jsoncbosistemas);
            DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
            $Contenido = $(DatosAImprimir.Lista);
            $Contenedor.children().remove();
            $Contenedor.append(DatosAImprimir.Lista);
        }
        catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudo cargar el combo con los sistemas.');
        }
    },
    ConfiguracionInicial: function () {
        //todo: cargar el estado inicial de los controles
        $().maxlength();
        $("[id$='txtFechaDesde']").datepicker().attr('readonly', true);
        $("[id$='txtFechaHasta']").datepicker().attr('readonly', true);
        var fecha = $.datepicker.formatDate('dd/mm/yy', new Date());
        SetTXT('txtFechaDesde', fecha);
        SetTXT('txtFechaHasta', fecha);
        page.EstilosControlesGenericos();
    },
    VerStringSQLGrid: function (data) {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            SetTXT('txtStringSQL', unescape(data));
            $('#dialogStringSQL').dialog('open');
            return false;
        } catch (e) {
            ShowAlertDanger('Error Inesperado: No se pudieron mostrar los datos.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    },
    VerStringSQL: function (data) {
        var obj = $.parseJSON(data);
        SetTXT('txtStringSQL', obj.STRINGSQL);
        $('#dialogStringSQL').dialog('open');
        return false;
    },
    // - Habilita/Deshabilitas los controles del filtro
    EstadosControlesFiltro: function (estado) {
        if (estado) {
            $("[id$='txtFechaDesde']").removeAttr("disabled");
            $("[id$='txtFechaHasta']").removeAttr("disabled");
            $("[id$='cboTabla']").removeAttr("disabled");
            $("[id$='cboUsuario']").removeAttr("disabled");
            $("[id$='cboOperacion']").removeAttr("disabled");
            $("[id$='cboTerminales']").removeAttr("disabled");
            $("[id$='cboSistema']").removeAttr("disabled");
            $("[id$='txtTextoBusqueda']").removeAttr("disabled");
            $("[id$='btnFiltro']").removeAttr("disabled");
        } else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaDesde']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtFechaHasta']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboTabla']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboOperacion']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboTerminales']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='cboSistema']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtTextoBusqueda']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltro']", "disabled", "disable");
        }
    },
    GenerarSQL: function () {
        var fechaDesde = GetTXT('txtFechaDesde');
        var fechaHasta = GetTXT('txtFechaHasta');
        var tabla = GetDDL('cboTabla');
        var usuario = GetDDL('cboUsuario');
        var operacion = GetDDL('cboOperacion');
        var nombrePC = GetDDL('cboTerminales');
        var sistema = GetDDL('cboSistema');
        var supervisor = "(TODOS)";
        var textoBusqueda = GetTXT('txtTextoBusqueda');
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RetornarStringSQL",
            data: "{fechaDesde:'" + fechaDesde + "',fechaHasta:'" + fechaHasta + "',tabla:'" + tabla + "',usuario:'" + usuario + "',operacion:'" + operacion + "',supervisor:'" + supervisor + "',nombrePC:'" + nombrePC + "',sistema:'" + sistema + "',textoBusqueda:'" + textoBusqueda + "'}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                page.VerStringSQL(data.d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Exportar el string Sql a un archivo .sql
    ExportarStringSQL: function (sql) {
        var obj = { 'STRINGSQL': sql };
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ExportarSQL",
            data: "{'obj':" + JSON.stringify(obj) + "}",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                window.open('ExportarSQL.aspx', 'Popup', '');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    //Funcionalidad para el boton copiar
    CopiarStringSQL: function (sql) {
        if (window.clipboardData && clipboardData.setData) {
            clipboardData.setData('text', sql);
        }
    },
    //Validar que la Fecha Hasta no sea menor que la Fecha Desde
    ValidarFecha: function () {
        var fechaDesde = new Date($("[id$='txtFechaDesde']").datepicker('getDate'));
        var fechaHasta = new Date($("[id$='txtFechaHasta']").datepicker('getDate'));
        var res = true;

        if (fechaHasta < fechaDesde) {
            alert('La <strong>FECHA HASTA</strong> debe ser mayor o igual que la <strong>FECHA DESDE</strong>');
            res = false;
        }
        return res;
    },
    ImprimirReporte: function () {
        var fechaDesde = GetTXT('txtFechaDesde');
        var fechaHasta = GetTXT('txtFechaHasta');
        var tabla = GetDDL('cboTabla');
        var usuario = GetDDL('cboUsuario');
        var operacion = GetDDL('cboOperacion');
        var nombrePC = GetDDL('cboTerminales');
        var sistema = GetDDL('cboSistema');
        var supervisor = "(TODOS)";
        var textoBusqueda = GetTXT('txtTextoBusqueda');
       
        var pagina = 'frmRptAnalisisAuditoria.aspx?fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&tabla=' + tabla +
                                                 '&usuario=' + usuario + '&operacion=' + operacion + '&supervisor=' + supervisor +
                                                 '&nombrePC=' + nombrePC + '&sistema=' + sistema + '&textoBusqueda=' + textoBusqueda +
                                                 '&CantidadRegistrosDefault=' + CantidadRegistrosTotales;

        var popup = encodeURI(pagina);

        var wOpen;
        var sOptions;

        sOptions = 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
        sOptions = sOptions + ',outerWidth=' + (screen.availWidth).toString();
        sOptions = sOptions + ',outerHeight=' + (screen.availHeight).toString();
        sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';
        wOpen = window.open('', 'R', sOptions);
        wOpen.location = pagina;
        wOpen.focus();
        //wOpen.moveTo(0, 0);
        wOpen.resizeTo(screen.availWidth, screen.availHeight);
        return wOpen;
    },
    EstilosControlesGenericos: function () {
        $("[id$='cboSistema']").addClass("col-md-9 select-no-padding");
        $("[id$='cboTabla']").addClass("col-md-9 select-no-padding");
        $("[id$='cboUsuario']").addClass("col-md-9 select-no-padding");
        $("[id$='cboTerminales']").addClass("col-md-9 select-no-padding");
    }
}
