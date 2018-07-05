/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [Lunes, 18 de Noviembre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
});

var PagedGridModel = function VMAreas(Areas) {
    var _self = this;

    _self.lstAreas = ko.observableArray(Areas);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarAreas = function () {
        _self.newRecords(-1);
        _self.lstAreas.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();

            var pdata = appCIPOLPRESENTACION.trxFiltro;

            //[MiguelP]         28/10/2014      GCP - Cambios 15598
            pdata.area = GetTXT('txtFiltro');
            pdata.baja = GetDDL('cboEstado');


            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarAreas",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstAreas, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                        page.EstadosControlesFiltro(false);
                        page.haydatosengrilla = true;
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda");
                    };
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        }
        catch (e) {
            bootbox.alert('Error al recuperar las áreas.');
        } finally { appCIPOLPRESENTACION.DesBloquearUI(); }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstAreas,
        newRecords: _self.newRecords,
        columns: [
                    { headerText: "ID ÁREA", rowText: "IDAREA", rowWitdh: "150px", setWitdh: true },
                    { headerText: "FICTICIA", rowText: function (item) {
                        if (item.IDAREA != undefined) {
                            if (item.FICTICIA == "S") {
                                return '<input id=\"Checkbox1\" type=\"checkbox\" checked=\"checked\" disabled=\"\"/>';
                            }
                            else {
                                return '<input id=\"Checkbox1\" type=\"checkbox\"  disabled=\"\"/>';
                            }
                        }
                    }, isHtml: true, rowWitdh: "150px", setWitdh: true
                    },
                    { headerText: "ÁREA", rowText: "NOMBREAREA" },
                    { headerText: "RESPONSABLE", rowText: "RESPONSABLE" },
                    { headerText: "BAJA", rowText: function (item) {
                        if (item.IDAREA != undefined) {
                            if (item.BAJA == "S") {
                                return '<input id=\"Checkbox1\" type=\"checkbox\" checked=\"checked\" disabled=\"\"/>';
                            }
                            else {
                                return '<input id=\"Checkbox1\" type=\"checkbox\"  disabled=\"\"/>';
                            }
                        }
                    }, isHtml: true, rowWitdh: "150px", setWitdh: true
                    },
                    { headerText: "", rowText: function (item) {
                        if (item.IDAREA != undefined) {
                            var parametrosC = '\'' + escape(item.IDAREA) + '\'';
                            var parametrosE = '\'' + escape(item.IDAREA) + '\',\'' + escape(item.NOMBREAREA) + '\'';
                            
                            var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\" class=\"btn-search\"></a>';
                            str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosC + ')\" class=\"btn-edit\"></a>';
                            if (item.BAJA == "S") {
                                str += '<a href=\'#\' onclick=\"return page.Activar(' + parametrosE + ')\"  class=\"btn-active\"></a>';
                            }
                            else {
                                str += '<a href=\'#\' onclick=\"return page.Eliminar(' + parametrosE + ')\" class=\"btn-delete\"></a>';
                            };
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
            _self.lstAreas.removeAll();
            page.LimpiarFiltros();
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    }
}