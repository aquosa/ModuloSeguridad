/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [Lunes, 18 de Noviembre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
/// </history>

var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
});

var PagedGridModel = function VMTerminales(Terminales) {
    var _self = this;

    _self.lstTerminales = ko.observableArray(Terminales);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarTerminales = function () {


        _self.newRecords(-1);
        _self.lstTerminales.removeAll();

        try {
            appCIPOLPRESENTACION.BloquearUI();

            //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
            pdata = appCIPOLPRESENTACION.trxFiltro;

            pdata.filtro = GetTXT('txtFiltroTerminal');
            pdata.Area = GetTXT('txtFiltroArea');

            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarDatosParaABMTerminales",
                //data: "{filtro:'" + filtroTerminal + "',area:'" + filtroArea + "'}",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstTerminales, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                        page.EstadosControlesFiltro(false);
                        page.haydatosengrilla = true;
                    }
                    else {
                        bootbox.alert("No se encontraron resultados relacionados con su búsqueda");
                    };
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    throw xhr.responseJSON;
                }
            });
        } catch (e) {
            bootbox.alert('Error Inesperado: al recuperar las terminales.');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstTerminales,
        newRecords: _self.newRecords,
        columns: [
                        { headerText: "COD TERMINAL", rowText: "CODTERMINAL" },
                        { headerText: "NOMBRE NETBIOS", rowText: "NOMBRECOMPUTADORA" },
                        { headerText: "ÁREA", rowText: "NOMBREAREA" },
                        { headerText: "", rowText: function (item) {
                            if (item.IDTERMINAL != undefined) {
                                var parametrosC = '\'' + escape(item.IDTERMINAL) + '\'';
                                var parametrosE = '\'' + escape(item.IDTERMINAL) + '\',\'' + escape(appCIPOLPRESENTACION.trim(item.CODTERMINAL)) + '\'';

                                var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\"  class=\"btn-search\"></a>';
                                str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosC + ')\" class=\"btn-edit\"></a>';
                                str += '<a href=\'#\' onclick=\"return  page.EliminarSistema(' + parametrosE + ')\" class=\"btn-delete\"></a>';

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
            _self.lstTerminales.removeAll();
            page.LimpiarFiltros();
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    }

}