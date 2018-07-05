//Defino esta variable que me determinará si el browser es Google Chrome (para evitar utilizar algunas 
//funciones que rompen el aplicativo cuando se carga desde Chrome).
$.browser.chrome = null;
//Inicialización de la página

$(function () {
});

$(document).ready(function () {
    page.init();
})

var page = {
    init: function () {
        page.abrirAplicativo();
        $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());
    },
    abrirAplicativo: function () {

        setTimeout("page.AbrirSistemaCIPOL('" + $.QueryString["urlPost"] + "', '" + $.QueryString["target"] + "');", 1000);
    },
    AbrirSistemaCIPOL: function (url, target) {
        appCIPOL.BloquearUI();
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsInterfaceSistemas.asmx/ObtenerObjetoCIPOL",
            data: "{ IDSistemaActual : '" + target + "' }",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.d.ResultadoEjecucion) {

                    page.AbrirSistemaSeguro(url, data.d.strcipol, target);
                } else {
                    appCIPOL.messageBox('Error', data.d.MensajeError, '', 'Aceptar');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                appCIPOL.messageBox('Error', 'Error al intentar abrir el sistema. Consulte al administrador', '', 'Aceptar', xhr.responseJSON);
            },
            complete: function () {
                appCIPOL.DesBloquearUI();
            }
        });

    },
    AbrirSistemaSeguro: function (url, data, target) {
        var mapForm = document.createElement("form");
        //todo: martinv -> deberia cambiar el target para poder abrir más de un sistema a la vez.
        mapForm.target = target;
        mapForm.method = "POST"; // or "post" if appropriate
        mapForm.action = url;

        var mapInput = document.createElement("input");
        mapInput.type = "hidden";
        mapInput.name = "stringcipol";
        mapInput.value = data;
        mapForm.appendChild(mapInput);

        document.body.appendChild(mapForm);

        var wOpen;
        var sOptions;
        var aWinName = target;

        sOptions = 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
        sOptions = sOptions + ',outerWidth=' + (screen.availWidth).toString();
        sOptions = sOptions + ',outerHeight=' + (screen.availHeight).toString();
        sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';
        wOpen = window.open('', aWinName, sOptions);
        //wOpen.location = url;
        //wOpen.focus();
        //SOLO EJECUTO ESTO SI EL BROWSER NO ES CROME
        if (!$.browser.chrome) {
            //wOpen.moveTo(0, 0);
            wOpen.resizeTo(screen.availWidth, screen.availHeight);
        }
        else {
            wOpen.resizeTo(screen.availWidth, screen.availHeight);
            wOpen.moveTo(0, 0);
        }
        //wOpen = window.open("", "Map", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
        if (wOpen) {
            mapForm.submit();
        } else {
            alert('Debe permitir pop-ups para trabajar.');
        }
    },
    redirectLogin: function () {
        window.location.href = 'frmSistemasPermitidos.aspx';
    }
}

