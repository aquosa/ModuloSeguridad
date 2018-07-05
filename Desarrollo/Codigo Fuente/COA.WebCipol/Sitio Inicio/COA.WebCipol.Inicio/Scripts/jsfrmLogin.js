//Inicialización de la página
$(function () {
    page.init();
});

var page = {
    init: function () {
        $("[id*='ucLogin_UserName']").focus();
        $("[id$='LoginButton']").button();
        $("[id$='ChangePasswordPushButton']").button();
        $("[id$='CancelPushButton']").button();
    }, PreguntarCambioContrasenia: function (mensaje, url) {
        /*martinv Gcp-CAmbios 14460
        Permite preguntar al usuario si desea cambiar la contraseña porque está próxima a vencer.
        */
        if (!confirm(mensaje)) {
            window.location.href = url;
        }
    }
}

/// <summary>
/// Funcion para obtener el ip del cliente
/// Observación : jsonip.com es un servicio público gratuito que devuelve la dirección IP de un cliente en un objeto JSON 
///               con soporte para JSONP, CORS y solicitudes directas 
/// </summary>
/// <returns> Guarda en un campo oculto (id="hdnIP"), que esta en frmLogin.aspx, el id del cliente </returns>
/// <history>
/// [IvanSa]          [Jueves, 14 de Noviembre de 2013]  Creado GCP-Cambios ID: 14460
/// </history>
function ObtenerIP() {
    $.get('http://jsonip.com', function (res) {
        var direccion = res.ip.split(',');
        $("[id$='hdnIP']").val(direccion[0]);
    });
}
