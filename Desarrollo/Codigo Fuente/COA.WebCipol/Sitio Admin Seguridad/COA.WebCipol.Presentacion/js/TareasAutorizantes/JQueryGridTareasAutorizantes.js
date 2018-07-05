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

var PagedGridModel = function VMTareasAutorizantes(TareasAutorizantes) {
    var _self = this;

    _self.lstTareasAutorizantes = ko.observableArray(TareasAutorizantes);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarTareasAutorizantes = function () {
        _self.newRecords(-1);
        _self.lstTareasAutorizantes.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarTareasAutorizantes",
                data: "",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstTareasAutorizantes, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda");
                    };
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        } catch (e) {
             bootbox.alert('Error Inesperado: Al recuperar las tareas.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstTareasAutorizantes,
        newRecords: _self.newRecords,
        columns: [
                    { headerText: "SISTEMAS", rowText: "DESCSISTEMA" },
                    { headerText: "DESCRIPCIÓN", rowText: function (item) { return '<div style=\"overflow: hidden; white-space: nowrap; word-wrap: break-word;\">' + item.DESCRIPCIONTAREA + '</div>' },
                        isHtml: true
                    },
                    { headerText: "", rowText: function (item) {
                        if (item.IDTAREA != undefined) {
                            var parametrosC = '\'' + escape(item.IDTAREA) + '\'';
                            var parametrosE = '\'' + escape(item.IDTAREA) + '\',\'' + escape(item.DESCRIPCIONTAREA) + '\'';
                            var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\" class=\"btn-search\"></a>';
                            str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosC + ')\" class=\"btn-edit\"></a>';
                            str += '<a href=\'#\' onclick=\"return  page.Eliminar(' + parametrosE + ')\" class=\"btn-delete\"></a>';

                            return str;
                        }
                    }, isHtml: true, rowWitdh: "100px", setWitdh: true
                    }],
        pageSize: 10,
        pagesToShow: 10
    });
}