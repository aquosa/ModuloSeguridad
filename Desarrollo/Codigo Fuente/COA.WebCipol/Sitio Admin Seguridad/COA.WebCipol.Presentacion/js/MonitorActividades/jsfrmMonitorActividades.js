/// <summary>
/// Script asociado a todas las operaciones realizadas en la pantalla "Monitor de Actividades"
/// </summary>
/// <history>
/// [IvanSa]          [Lunes, 21 de octubre de 2013]       Creado GCP-Cambios ID: 14489
/// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        bootbox.alert('Error Inesperado: No se pudieron cargar los datos');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});
//function CaracteresExtranios(objeto, e) {
//    $.browser.firefox = /firefox/.test(navigator.userAgent.toLowerCase());

//    var keynum;

//    if (window.event) {
//        keynum = e.keyCode;
//    }
//    else if (e.which) {
//        keynum = e.which;   
//    };

//    if (keynum == 39)
//        if (!$.browser.firefox) {
//            event.returnValue = false;
//        }
//        else {
//            e.preventDefault();
//        };
//};
var page = {
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;

        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        //Carga el elemento base.
        page.CargarElementoBase();

        $("[id$='btnEliminar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                return page.Eliminar();
            } catch (e) {
                bootbox.alert('Error Inesperado: Ha ocurrido un error no esperado en el proceso de eliminación de sesión, por favor vuelva a intentar');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoMonitorBase",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
                appCIPOLPRESENTACION.trxFiltro = data.d.objFiltro;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Limpiarfiltros: function () {
        //Limpieza de los filtros.
        SetTXT('txtArea', '');
        SetTXT('txtNombre', '');
        SetTXT('txtUsuario', '');
        SetTXT('txtTerminal', '');
        page.EstadosControlesFiltro(true);
    },
    EstadosControlesFiltro: function (estado) {
        if (estado) {
            $("[id$='txtArea']").removeAttr("disabled");
            $("[id$='txtNombre']").removeAttr("disabled");
            $("[id$='txtUsuario']").removeAttr("disabled");
            $("[id$='txtTerminal']").removeAttr("disabled");
            $("[id$='btnFiltro']").removeAttr("disabled");
            //[MiguelP]         28/10/2014      GCP - Cambios 15599 - permite que el boton de limpiar filtros este siempre habilitado
            //appCIPOLPRESENTACION.SetAtributo("[id$='btnLimpiarFiltro']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnEliminar']", "disabled", "disable");

        } else {
            appCIPOLPRESENTACION.SetAtributo("[id$='txtArea']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtNombre']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtUsuario']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='txtTerminal']", "disabled", "disable");
            appCIPOLPRESENTACION.SetAtributo("[id$='btnFiltro']", "disabled", "disable");
            //[MiguelP]         28/10/2014      GCP - Cambios 15599 - permite que el boton de limpiar filtros este siempre habilitado
            //$("[id$='btnLimpiarFiltro']").removeAttr("disabled");
            $("[id$='btnEliminar']").removeAttr("disabled");
        }
    },
    RealizarBusqueda: function () {
        $("[id$='btnFiltro']").click();
    },
    CargarDatosGrilla: function (data) {
        //Carga la grilla de sucesos
        $Contenedor = $("#reemplazarPorGrilla");
        var DatosAImprimir = $.parseJSON(data);
        DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();
        $Contenido = $(DatosAImprimir.Lista);
        $Contenedor.children().remove();
        $Contenido.find("form").children().unwrap();
        $Contenido.css("list-style-type", "none");
        $Contenedor.append($Contenido);
    },
    Eliminar: function () {
        var seleccionados = 0;
        var obj = [];
        // -- Se recorre las filas seleccionadas de la grilla
        $(".grilla input[id*='chkSeleccion']:checkbox").each(function (index) {
            var checked = $(this).prop('checked');
            if (checked) {
                seleccionados = 1;
                var fila = index + 1;
                var usuario = $(".grilla  tr:nth-child(" + fila + ")").children("td:nth-child(2)").text();
                var terminal = $(".grilla  tr:nth-child(" + fila + ")").children("td:nth-child(4)").text();
                obj.push({ 'usuario': usuario, 'terminal': terminal });
            }
        });
        if (seleccionados == 1) {
            bootbox.confirm("¿Va a eliminar las actividades seleccionadas ¿Desea Continuar?", function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "./PageBuilders/wsAjaxwsSeguridad.asmx/Eliminar",
                        data: "{'obj':" + JSON.stringify(obj) + "}",
                        contentType: "application/json; charset=iso-8859-1",
                        dataType: "json",
                        async: false,
                        success: function (respuesta) {
                            if (respuesta.d = 1) {
                                bootbox.alert("La operación se realizó correctamente.", function () { page.RealizarBusqueda(false); });
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            throw xhr.responseJSON;
                        }
                    });
                }
            });
        }
        else {
            bootbox.alert('Debe seleccionar al menos una actividad.');
        }
        return false;
    }, chkTodosSeleccion: function () {
        $(".grilla input[id*='chkSeleccion']:checkbox").prop('checked', $("input[id*='chkTodosSeleccion']:checkbox").prop('checked'));
    }
}