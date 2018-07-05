/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [Lunes, 11 de Noviembre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
    $("[id$='btnImprimir']").hide();
});

var PagedGridModel = function VMSucesos(Sucesos) {
    var _seft = this;

    _seft.lstSucesos = ko.observableArray(Sucesos);
    _seft.newPages = ko.observable(false);
    _seft.newRecords = ko.observable(0);

    _seft.cmdRecuperarSucesos = function () {
        _seft.newRecords(-1);
        _seft.lstSucesos.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();
            var fechaDesde = GetTXT('txtFechaDesde');
            var fechaHasta = GetTXT('txtFechaHasta');
            var usuarioAdministrador = GetDDLTxt('cboAdministradores');
            var usuarioAfectado = GetDDLTxt('cboAfectados');
            var codigoMensaje = GetDDLTxt('cboCodigoMensaje');
            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarSucesos",
                data: "{fechaDesde:'" + fechaDesde + "',fechaHasta:'" + fechaHasta + "',usuarioAdministrador:'" + escape(usuarioAdministrador) + "',usuarioAfectado:'" + escape(usuarioAfectado) + "',codigoMensaje:'" + escape(codigoMensaje) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_seft.lstSucesos, Jrta.d);
                        _seft.newRecords(Jrta.d.length);
                        page.EstadosControlesFiltro(false);
                        $("[id$='btnImprimir']").show();
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su busqueda.");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        } catch (e) {
            bootbox.alert('Error Inesperado: Al recuperar el log de sucesos.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };

    _seft.ViewModelSucesos = new ko.simpleGrid.viewModel({
        data: _seft.lstSucesos,
        newRecords: _seft.newRecords,
        columns: [
                    { headerText: "FECHA/HORA", rowText: "FechaConFormato", rowWitdh: "100px", setWitdh: true },
                    { headerText: "CÓDIGO", rowText: "CODMENSAJE", rowWitdh: "150px", setWitdh: true },
                    { headerText: "MENSAJE", rowText: "TEXTOMENSAJE"},//function (item) { if (item.TEXTOMENSAJE != undefined) { return item.TEXTOMENSAJE.substring(0, 60); } } },
                    { headerText: "ADMINISTRADOR", rowText: "USUARIOACTUANTE" },
                    { headerText: "AFECTADO", rowText: "USUARIOAFECTADO" },
                    { headerText: "", rowText: function (item) {
                        var parametros = '\'' + escape(item.FechaConFormato) + '\',\'' + escape(item.CODMENSAJE) + '\',\'' +
                                            escape(item.TEXTOMENSAJE) + '\',\'' + escape(item.USUARIOACTUANTE) + '\',\'' + escape(item.USUARIOAFECTADO) + '\'';

                        if (item.FechaConFormato != undefined) {
                            return '<a href=\'#\' onclick=\"return  page.VerDialog(' + parametros + ')\" class=\"btn-search\"></a>'
                        };
                    }, isHtml: true, rowWitdh: "40px", setWitdh: true
                    }
                  ],
        pageSize: 10,
        pagesToShow: 10
    });

    _seft.cmdLimpiarFiltro = function () {
        try {
            appCIPOLPRESENTACION.BloquearUI();
            _seft.newRecords(-1);
            _seft.lstSucesos.removeAll();
            page.Limpiarfiltros();
            page.EstadosControlesFiltro(true);
            $("[id$='btnImprimir']").hide();
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }

    }
}


