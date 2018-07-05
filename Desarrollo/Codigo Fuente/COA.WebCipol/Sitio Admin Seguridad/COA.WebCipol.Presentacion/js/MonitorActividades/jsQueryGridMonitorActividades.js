/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// <history>
/// [MartinV]          [miércoles, 24 de septiembre de 2014]       Modificado  GCP-Cambios 
/// </history>
var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
    $("[id$='btnLimpiarFiltro']").click();
});

var PagedGridModel = function VMMonitorActividades(MonitorActividades) {
    var _self = this;

    _self.lstMonitorActividades = ko.observableArray(MonitorActividades);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarMonitorActividades = function () {
        _self.newRecords(-1);
        _self.lstMonitorActividades.removeAll();

        try {

            var pdata = appCIPOLPRESENTACION.trxFiltro;

            pdata.area = GetTXT('txtArea');
            pdata.nombre = GetTXT('txtNombre');
            pdata.usuario = GetTXT('txtUsuario');
            pdata.terminal = GetTXT('txtTerminal');
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarActividades",
                //data: "{area:'" + area + "',nombre:'" + nombre + "',usuario:'" + usuario + "',terminal:'" + terminal + "'}",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        page.EstadosControlesFiltro(false);
                        ko.utils.arrayPushAll(_self.lstMonitorActividades, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                    }
                    else {
                        page.EstadosControlesFiltro(true);
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
        data: _self.lstMonitorActividades,
        newRecords: _self.newRecords,
        columns: [{ headerText: function (item) {
            return '<input id=\"chkTodosSeleccion\" type=\"checkbox\" onclick=\"return  page.chkTodosSeleccion()\" />';
        }, isHtml: true, rowText: function (item) {
            return '<input id=\"chkSeleccion\" type=\"checkbox\"  />';
        }, isHtml: true, rowWitdh: "150px", setWitdh: true
        },
                    { headerText: "USUARIO", rowText: "Usuario" },
                    { headerText: "NOMBRE", rowText: "NOMBRES" },
                    { headerText: "TERMINAL", rowText: "Terminal" },
                    { headerText: "AREA", rowText: "NOMBREAREA" },
                    { headerText: "ULTIMO INICIO", rowText: "INICIOTAREASTR" }
                     ],
        pageSize: 10,
        pagesToShow: 10
    });
    _self.cmdLimpiarFiltro = function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            _self.newRecords(-1);
            _self.lstMonitorActividades.removeAll();
            page.Limpiarfiltros();
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    }
}