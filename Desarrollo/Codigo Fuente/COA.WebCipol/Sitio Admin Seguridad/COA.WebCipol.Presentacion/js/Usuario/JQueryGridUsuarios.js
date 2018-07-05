/// <summary>
/// Desarrollo de las funcionalidades para: 
///     - Filtro de Busqueda
///     - Visualización de datos mediante una gilla con paginacíon implementado las caracteristicas de Knockaout 
/// </summary>
/// <history>
/// [IvanSa]          [Viernes, 15 de Noviembre de 2013]       Creado GCP-Cambios ID:
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// [MiguelP]         28/10/2014      GCP - Cambios 15598
/// </history>
var count = 0;

$(document).ready(function () {
    ko.applyBindings(new PagedGridModel([[]]));
});

var PagedGridModel = function VMUsuarios(Usuarios) {
    var _self = this;

    _self.lstUsuarios = ko.observableArray(Usuarios);
    _self.newPages = ko.observable(false);
    _self.newRecords = ko.observable(0);

    _self.cmdRecuperarUsuarios = function () {
        _self.newRecords(-1);
        _self.lstUsuarios.removeAll();
        try {
            //Realiza la búsqueda de los datos y los visualiza en ua grilla
            appCIPOLPRESENTACION.BloquearUI();

            //[MiguelP]         28/10/2014      GCP - Cambios 15598
            var pdata = appCIPOLPRESENTACION.trxFiltro;

            pdata.filtro = GetTXT('txtFiltro');
            pdata.chkUsu = $("[id$='rbUsu']").prop('checked');
            pdata.chkNombre = $("[id$='rbNombre']").prop('checked');
            pdata.chkArea = $("[id$='rbArea']").prop('checked');
            pdata.filtrobaja = $("[id*='cboEstado'] :selected").text();
            pdata.chkSubCadenas = $("[id$='chkSubCadenas']").prop('checked');

            //var chkUsu = $("[id$='rbUsu']").prop('checked');
            //var chkNombre = $("[id$='rbNombre']").prop('checked');
            //var chkArea = $("[id$='rbArea']").prop('checked');
            //var filtro = GetTXT('txtFiltro');
            //var filtrobaja = $("[id*='cboEstado'] :selected").text();
            //var chkSubCadenas = $("[id$='chkSubCadenas']").prop('checked');

            $.ajax({
                type: "POST",
                url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarUsuarios",
                //data: "{filtro:'" + filtro + "',filtrobaja:'" + filtrobaja + "',chkUsu:'" + chkUsu + "',chkNombre:'" + chkNombre + "',chkArea:'" + chkArea + "',chkSubCadenas:'" + chkSubCadenas + "'}",
                data: "{'obj':" + JSON.stringify(pdata) + "}",
                contentType: "application/json; charset=iso-8859-1",
                dataType: "json",
                async: false,
                success: function (Jrta) {
                    if (Jrta.d.length > 0) {
                        ko.utils.arrayPushAll(_self.lstUsuarios, Jrta.d);
                        _self.newRecords(Jrta.d.length);
                        page.haydatosengrilla = true;
                        page.DeshabilitarControles();
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
            bootbox.alert('Error Inesperado: Al recuperar los usuarios');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    };

    _self.GridViewModel = new ko.simpleGrid.viewModel({
        data: _self.lstUsuarios,
        newRecords: _self.newRecords,
        columns: [
                        { headerText: "USUARIO", rowText: "Usuario"},
                        { headerText: "NOMBRE Y APELLIDO", rowText: "Nombres"},
                        { headerText: "AREA", rowText: "NombreArea" },
                        { headerText: "FECHA BAJA", rowText: "fechaBajaStr", rowWitdh: "100px", setWitdh: true },
                        { headerText: "", rowText: function (item) {
                            if (item.IdUsuario != undefined) {
                                var parametrosC = '\'' + escape(item.IdUsuario) + '\'';
                                var str = '<a href=\'#\' onclick=\"return  page.ConsultarDialog(' + parametrosC + ')\" class=\"btn-search ui-dialog-open\"></a>';
                                if (item.IdUsuario != undefined && appCIPOLPRESENTACION.trxdatapermiso.blnModificarVisible) {
                                    var parametrosM = '\'' + escape(item.IdUsuario) + '\'';
                                    str += '<a href=\'#\' onclick=\"return  page.ModificarDialog(' + parametrosM + ')\" class=\"btn-edit\"></a>';
                                }
                                if (item.IdUsuario != undefined && appCIPOLPRESENTACION.trxdatapermiso.blnEliminarVisible) {
                                    var fecha = "";
                                    if (item.fechaBajaStr != null) {
                                        fecha = item.fechaBajaStr
                                    };
                                    var parametrosE = '\'' + escape(item.IdUsuario) + '\',\'' + escape(item.Usuario) + '\',\'' + escape(fecha) + '\',\'true\'';


                                    var url = "";
                                    if (item.FechaBaja != undefined && item.FechaBaja != undefined) {
                                        str += '<a href=\'#\' onclick=\"return page.Eliminar(' + parametrosE + ')\" class=\"btn-active\"></a>';
                                    } else {
                                        str += '<a href=\'#\' onclick=\"return page.Eliminar(' + parametrosE + ')\" class=\"btn-delete\"></a>';
                                    };
                                }
                                return str;
                            }
                        }, isHtml: true , rowWitdh: "100px", setWitdh: true
                        }],
        pageSize: 10,
        pagesToShow: 10

    });
    _self.cmdLimpiarFiltro = function () {

        try {
            appCIPOLPRESENTACION.BloquearUI();
            _self.newRecords(-1);
            _self.lstUsuarios.removeAll();
            page.LimpiarFiltros();
        } catch (e) {
            bootbox.alert('Error Inesperado: No se pudieron limpiar los datos');
        } finally {
            appCIPOLPRESENTACION.DesBloquearUI();
        }
    }
}