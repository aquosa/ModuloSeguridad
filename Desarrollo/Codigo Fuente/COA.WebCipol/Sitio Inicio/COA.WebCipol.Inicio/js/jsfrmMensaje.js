/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOL.BloquearUI();
        page.init();
    } catch (e) {
        appCIPOL.messageBox('Error', 'Error Inesperado: Al iniciar', '', 'Aceptar', e);
    } finally {
        appCIPOL.DesBloquearUI();
    }
});

var page = {
    init: function () {
        appCIPOL.onsubmit = false;
        appCIPOL.urlinicio = 'frmLogin.aspx';
        page.Cerrar();
        $("[id$='btnOK']").button().click(function () {
            try {
                appCIPOL.BloquearUI();
                //window.location.href = appCIPOL.urlinicio;
                close();
            } catch (e) {
                appCIPOL.messageBox('Error', 'Error Inesperado:', '', 'Aceptar', e);
            } finally {
                appCIPOL.DesBloquearUI();
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
                //$("[id$='lblMENSAJE']").text(data.d);
                //appCIPOL.messageBox('Mensaje', data, '', 'Aceptar');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    }
}