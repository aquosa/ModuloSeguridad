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
        bootbox.alert('Error Inesperado: Al iniciar');
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var page = {
    init: function () {
        appCIPOLPRESENTACION.onsubmit = false;
        appCIPOLPRESENTACION.urlinicio = 'frmInicio.aspx';
        page.Cerrar();
        $("[id$='btnOK']").button().click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                close();
            } catch (e) {
                bootbox.alert('Error Inesperado.');
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });
    },
    Cerrar: function () {
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/SalirSistema",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                close();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    }
}