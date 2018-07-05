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
        ShowAlertDanger('Error Inesperado: Al iniciar'+ e);
    } finally {
        appCIPOLPRESENTACION.DesBloquearUI();
    }
});

var page = {
    update: true,
    init: function () {
        appCIPOLPRESENTACION.onsubmit = true;
        page.ConfiguracionInicial();

        $("[id*='cmdCerrar']").click(function () {
            try {
                appCIPOLPRESENTACION.BloquearUI();
                page.Cerrar(); return false;
            } catch (e) {
                ShowAlertDanger('Error Inesperado: Al cancelar. ' + e);
            } finally {
                appCIPOLPRESENTACION.DesBloquearUI();
            }
        });

    },
    //    MostrarMensaje: function (mensaje) {
    //        ShowAlertInfo(mensaje);
    //    },

    setDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;
    }, getDatos: function () {
        var app = appCIPOLPRESENTACION.trxdata;

        app.COMENTARIOS = $("[id*='txtParametros_0']").val();
        app.COMENTARIOS = $("[id*='txtParametros_1']").val();
        app.COMENTARIOS = $("[id*='txtParametros_2']").val();
        app.COMENTARIOS = $("[id*='txtParametros_3']").val();
        app.COMENTARIOS = $("[id*='txtParametros_4']").val();
        app.COMENTARIOS = $("[id*='txtParametros_5']").val();
        app.COMENTARIOS = $("[id*='txtParametros_6']").val();
        app.COMENTARIOS = $("[id*='txtParametros_7']").val();
        app.COMENTARIOS = $("[id*='txtParametros_8']").val();

        app.blnFICTICIA = $("[id*='chkFicticia']").prop('checked');


    },
    ConfiguracionInicial: function () {
        //todo: cargar las validaciones de tipo de datos. Sólo enteres,string,fechas.
        $().maxlength();
    },
    Cerrar: function () {
        bootbox.confirm('¿Está seguro que desea cerrar? Se Perderán todas las modificaciones que no han sido guardadas.', function (result) {
            if (result) { window.location.href = 'frmPrincipal.aspx' }
        });
    }
}










