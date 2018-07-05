/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [Martes, 12 de Noviembre de 2013]       Creado GCP-Cambios ID: 14499
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
    $("[id$='btnExportarExcel']").hide();
});

var PagedGridModel = function VMEventos(Eventos) {
    var _seft = this;

    _seft.lstEventos = ko.observableArray(Eventos);
    _seft.newPages = ko.observable(false);
    _seft.newRecords = ko.observable(0);
    _seft.CantidadRegistros = ko.observable(0);;

    _seft.cmdRecuperarEventos = function (LimitaRegistrosMAX) { RecuperarRegistros(LimitaRegistrosMAX); }

    function RecuperarRegistros(LimitaRegistrosMAX) {
        appCIPOLPRESENTACION.BloquearUI();

        _seft.newRecords(-1);
        _seft.lstEventos.removeAll();
        _seft.CantidadRegistros(0);

        try {
            appCIPOLPRESENTACION.BloquearUI();

            //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
            pdata = appCIPOLPRESENTACION.trxFiltro;

            pdata.fechaDesde = GetTXT('txtFechaDesde');
            pdata.fechaHasta = GetTXT('txtFechaHasta');
            pdata.tabla = GetDDL('cboTabla');
            pdata.usuario = GetDDL('cboUsuario');
            pdata.operacion = GetDDL('cboOperacion');
            pdata.nombrePC = GetDDL('cboTerminales');
            pdata.sistema = GetDDL('cboSistema');
            pdata.supervisor = "(TODOS)";
            pdata.textoBusqueda = GetTXT('txtTextoBusqueda');

            pdata.CantidadRegistrosDefault = 0; //Sin limite de registros maximos
            if (LimitaRegistrosMAX)
                pdata.CantidadRegistrosDefault = CantidadRegistrosDefault;

            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarEventosAuditoria",
                //data: "{fechaDesde:'" + fechaDesde + "',fechaHasta:'" + fechaHasta + "',tabla:'" + tabla + "',usuario:'" + usuario + "',operacion:'" + operacion + "',supervisor:'" + supervisor + "',nombrePC:'" + nombrePC + "',sistema:'" + sistema + "',textoBusqueda:'" + textoBusqueda + "'}",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {

                    if (!Jrta.d.Validaciones.ResultadoEjecucion) {
                        bootbox.alert(Jrta.d.Validaciones.Mensaje);
                        return;
                    }

                    ko.utils.arrayPushAll(_seft.lstEventos, Jrta.d.Sist_Eventos);
                    if (Jrta.d.Sist_Eventos.length > 0) {
                        var intCantTotal = Jrta.d.CantRegistrosTotal;
                        //Si esta establecido el limite de datos a recuperar y la cantidad recuperada supera ese numero
                        if (CantidadRegistrosDefault > 0 && intCantTotal > CantidadRegistrosDefault) {
                            bootbox.confirm({
                                message: "Solo se han recuperado los primeros " + CantidadRegistrosDefault + " de los " + intCantTotal + " registros que se corresponden con su consulta. Tenga en cuenta que si decide visualizarlos a todos podría ocasionar problemas de performance en los aplicativos que comparten el servidor.",
                                buttons: {
                                    confirm: {
                                        label: 'Ver solo lo recuperado'
                                    },
                                    cancel: {
                                        label: 'Continuar y Mostrar Todo'
                                    }
                                },
                                callback: function (result) {
                                    if (!result) {
                                        appCIPOLPRESENTACION.BloquearUI();
                                        //Ejecuta la busqueda sin limite de registros
                                        return RecuperarRegistros(0);
                                    }
                                }
                            });
                        }
                        CantidadRegistrosTotales = Jrta.d.Sist_Eventos.length;
                        _seft.newRecords(CantidadRegistrosTotales);
                        page.EstadosControlesFiltro(false);
                        $("[id$='btnExportarExcel']").show();
                        _seft.CantidadRegistros(CantidadRegistrosTotales);
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    bootbox.alert("Ha ocurrido un error al recuperar todos los registros. Las causas posibles pueden ser errores de comunicación con el servidor, o que se ha excedido el límite permitido por la organización. Consulte al administrador del sistema.");
                }
            });
        }
        catch (e) {
            bootbox.alert('Error al recuperar la carga de los datos.');
        } finally { appCIPOLPRESENTACION.DesBloquearUI(); }

    }

    _seft.GridViewModel = new ko.simpleGrid.viewModel({
        data: _seft.lstEventos,
        newRecords: _seft.newRecords,
        columns: [
                    { headerText: "FECHA", rowText: "FechaConFormato", rowWitdh: "60px", setWitdh: true },
                    { headerText: "NOMBRE PC", rowText: "NOMBREPC" },
                    { headerText: "OPERACIÓN", rowText: "OPERACION" },
                    { headerText: "SISTEMA", rowText: "SISTEMA" },
                    { headerText: "STRING SQL", rowText: function (item) { if (item.STRINGSQL != undefined) { return item.STRINGSQL.substring(0, 20); } } },
                    { headerText: "TABLA", rowText: "TABLA" },
                    { headerText: "USUARIO", rowText: "USUARIO" },
                    {
                        headerText: "", rowText: function (item) {
                            if (item.FechaConFormato != undefined) {
                                return '<a href=\'#\' onclick=\"return  page.VerStringSQLGrid(\'' + escape(item.STRINGSQL) + '\')\" class=\"btn-search\"></a>';
                            }
                        }, isHtml: true, rowWitdh: "40px", setWitdh: true
                    }
        ],
        pageSize: 10,
        pagesToShow: 10
    });

    _seft.cmdLimpiarFiltro = function () {
        try {
            _seft.newRecords(-1);
            _seft.lstEventos.removeAll();
            _seft.CantidadRegistros(0);
            $("[id$='btnExportarExcel']").hide();
            page.LimpiarFiltros();
            page.EstadosControlesFiltro(true);
        }
        catch (e) {
            bootbox.alert('Error al limpiar los filtros.');
        } finally { appCIPOLPRESENTACION.DesBloquearUI(); }
    }
}
