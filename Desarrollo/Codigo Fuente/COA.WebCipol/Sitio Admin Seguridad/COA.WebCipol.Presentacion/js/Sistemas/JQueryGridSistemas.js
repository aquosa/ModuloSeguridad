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
    $("[id$='btnFiltro']").click();
});

var PagedGridModel = function VMSistemas(Sistemas) {
    var _self = this;

    _self.lstSistemas = ko.observableArray(Sistemas);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarSistemas = function () {
        _self.newRecords(-1);
        _self.lstSistemas.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarSistemasHabilitados",
                data: "",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstSistemas, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda.");
                    };
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        }
        catch (e) {
            bootbox.alert('Error al recuperar las Sistemas.');
        } finally { appCIPOLPRESENTACION.DesBloquearUI(); }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstSistemas,
        newRecords: _self.newRecords,
        columns: [
                    { headerText: "Habilitado", rowText: function (item) {
                        if (item.IDSISTEMA != undefined) {
                            if (item.SISTEMAHABILITADO == "S") {
                                return '<input id=\"Checkbox1\" type=\"checkbox\" checked=\"checked\" disabled=\"\"/>';
                            }
                            else {
                                return '<input id=\"Checkbox1\" type=\"checkbox\"  disabled=\"\"/>';
                            }
                        }
                    }, isHtml: true, rowWitdh: "80px", setWitdh: true
                    },
                    { headerText: "ID SISTEMA", rowText: "IDSISTEMA", rowWitdh: "80px", setWitdh: true },
                    { headerText: "CÓDIGO SISTEMA", rowText: "CODSISTEMA" },
                    { headerText: "DESCRIPCIÓN", rowText: "DESCSISTEMA" },
                    { headerText: "NOMBRE DE EXE", rowText: "NOMBREEXEC" },
                    { headerText: "ICONO", rowText: function (item) {
                        var str = "";
                        if (item.ICONO != null) {
                            var icon = item.ICONO.split('*');
                            str = "<div class=\"metro\ " + icon[1] + "\"><i style=\"color:#FFFFFF\" class=\"" + icon[0] + "\"></i></div>";
                        } return str;
                    }, isHtml: true, rowWitdh: "30px", setWitdh: true
                },
                    
                    { headerText: "PÁGINA POR DEFECTO", rowText: "PAGINAPORDEFECTO" }, 
                    {headerText: "", rowText: function (item) {
                        if (item.IDSISTEMA != undefined) {
                            var parametrosC = '\'' + escape(item.IDSISTEMA) + '\'';
                            var parametrosE = '\'' + escape(item.IDSISTEMA) + '\',\'' + escape(item.DESCSISTEMA) + '\'';

                            var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\" class=\"btn-search\"></a>';
                            str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosC + ')\" class=\"btn-edit\"></a>';
                            str += '<a href=\'#\' onclick=\"return  page.EliminarSistemaConfirm(' + parametrosE + ')\" class=\"btn-delete\"></a>';

                            return str;
                        }
                    }, isHtml: true, rowWitdh: "100px", setWitdh: true
                }],
        pageSize: 10,
        pagesToShow: 10

    });
}