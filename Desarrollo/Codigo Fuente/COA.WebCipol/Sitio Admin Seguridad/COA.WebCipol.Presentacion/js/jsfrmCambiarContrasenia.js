/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOLPRESENTACION.BloquearUI();
        page.init();
    } catch (e) {
        ShowAlertDanger('Error Inesperado: Al iniciar.');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var page = {
    obligatorio: "",
    update: true,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        //Lee el valor del querystring.        
        obligatorio = $.QueryString["Obligatorio"];

        //Carga el elemento base.
        page.CargarElementoBase();
        //Setea el valor de obligatoriedad.
        var app = appCIPOLPRESENTACION.trxdata;
        app.OBLIGATORIO = obligatorio;

        $("[id*='cmdAceptar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.Guardar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al aceptar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
        //$("[id*='cmdCancelar']").click(function () { page.Cancelar(); });
        $("[id*='cmdCancelar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.Cancelar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al cancelar.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    CargarElementoBase: function () {
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoContraseniaBase",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                appCIPOLPRESENTACION.trxdata = data.d;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                ShowAlertDanger('Error al recuperar el elemento. ' + xhr.responseJSON);
            }
        });
    }, Guardar: function () {
        //Setea los datos. Ver como saber si es update o new.
        page.getDatos();
        //Guarda los datos en el servidor.
        var pdata = appCIPOLPRESENTACION.trxdata;
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/AdministrarContrasenia",
            data: "{'obj':" + JSON.stringify(pdata) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                appCIPOLPRESENTACION.DesBloquearUI();
                if (respuesta.d == "") {
                    bootbox.alert('Los datos han sido guardados.');
                    page.LimpiarCampos();
                }
                else {
                    bootbox.alert(respuesta.d);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    Cancelar: function () {
        var msg = '¿Está seguro que cancela la operación?';
        bootbox.confirm(msg, function (result) {
            if (result) {
                page.LimpiarCampos();
            }
        });
    },
    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

    }, getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
        //Setea el campo Id
        app.CONTRASENIA = $("[id*='txtContraseña']").val();
        app.NUEVACONTRASENIA = $("[id*='txtNuevaContraseña']").val();
        app.REPETIRNUEVACONTRASENIA = $("[id*='txtRepetirNuevaContraseña']").val();

    }, validarDatos: function () {
        //todo: realizar la validación
        appCIPOLPRESENTACION.trim($("[id*='txtContraseña']").val());
        appCIPOLPRESENTACION.trim($("[id*='txtNuevaContraseña']").val());
        appCIPOLPRESENTACION.trim($("[id*='txtRepetirNuevaContraseña']").val());
    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
    },
    LimpiarCampos: function () {
        //Setea el campo Id
        $("[id*='txtContraseña']").val('');
        $("[id*='txtNuevaContraseña']").val('');
        $("[id*='txtRepetirNuevaContraseña']").val('');
    }
}











