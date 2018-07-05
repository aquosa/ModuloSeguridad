/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [martes, 12 de Noviembre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>

var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
});

var PagedGridModel = function VMTareas(Tareas) {
    var _self = this;

    _self.lstTareas = ko.observableArray(Tareas);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarTareas = function () {
        _self.newRecords(-1);
        _self.lstTareas.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();

            var pData = appCIPOLPRESENTACION.trxFiltro;

            pData.filtroIDTAREA = GetTXT('txtFiltroID');
            pData.filtroDESCRIPCIONTAREA = GetTXT('txtFiltroNombre');
            pData.filtroSistema = GetDDL('cboSistemafiltro');
            pData.filtroCODIGOTAREA = GetTXT('txtFiltroCodigo');
            pData.filtroIDAUTORIZACION = GetDDL('cboTareaAutorizante');

            if (pData.filtroSistema == null || pData.filtroSistema == '')
                pData.filtroSistema = 0;
            if (pData.filtroIDTAREA == null || pData.filtroIDTAREA == '')
                pData.filtroIDTAREA = 0;

            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarTareasPrimitivas",
                //data: "{filtroCODIGOTAREA:'" + filtroCodigo + "',filtroDESCRIPCIONTAREA:'" + filtroFiltroNombre + "',filtroIDAUTORIZACION:'" + filtroTareaAutorizante + "',filtroIDTAREA:'" + filtroFiltroID + "',filtroSistema:'" + filtroSistema + "'}",
                data: "{'obj':" + JSON.stringify(pData) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstTareas, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                        page.EstadosControlesFiltro(false);
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        } catch (e) {
            bootbox.alert('Error Inesperado:  Al recuperar las tareas.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstTareas,
        newRecords: _self.newRecords,
        columns: [
                        { headerText: "ID TAREA", rowText: "IDTAREA" },
                        { headerText: "POSEE TA", rowText: function (item) {
                            if (item.IDTAREA != undefined) {
                                if (item.IDAUTORIZACIONBOOL == true) {
                                    return '<input id=\"Checkbox1\" type=\"checkbox\" checked=\"checked\" disabled=\"\"/>';
                                }
                                else {
                                    return '<input id=\"Checkbox1\" type=\"checkbox\"  disabled=\"\"/>';
                                }
                            }
                        }, isHtml: true ,rowWitdh: "100px", setWitdh: true
                        },
                        { headerText: "CÓDIGO", rowText: "CODIGOTAREA" },
                        { headerText: "NOMBRE TAREA", rowText: "DESCRIPCIONTAREA" },
                        { headerText: "SISTEMA", rowText: "DESCSISTEMA" },
                        { headerText: "", rowText: function (item) {
                            if (item.IDTAREA != undefined) {
                                var parametrosC = '\'' + escape(item.IDTAREA) + '\'';
                                var parametrosE = '\'' + escape(item.IDTAREA) + '\',\'' + escape(item.DESCRIPCIONTAREA) + '\'';
                                var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\" class=\"btn-search\"></a>';
                                str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosC + ')\" class=\"btn-edit\"></a>';
                                str += '<a href=\'#\' onclick=\"return page.Eliminar(' + parametrosE + ')\" class=\"btn-delete\"></a>';

                                return str;
                            }
                        }, isHtml: true, rowWitdh: "100px", setWitdh: true
                        }],
        pageSize: 10,
        pagesToShow: 10

    });
    _self.cmdLimpiarFiltro = function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            _self.newRecords(-1);
            _self.lstTareas.removeAll();
            page.LimpiarFiltros();
            page.EstadosControlesFiltro(true);
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    }
}