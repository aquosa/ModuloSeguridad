//Defino esta variable que me determinará si el browser es Google Chrome (para evitar utilizar algunas 
//funciones que rompen el aplicativo cuando se carga desde Chrome).
$.browser.chrome = null;
/// <summary>
/// </summary>
/// <history>
/// [MartinV]          [jueves, 21 de noviembre de 2013]       Modificado  GCP-Cambios 14665
/// </history>
$(function () {
    try {
        appCIPOL.BloquearUI();
        page.init();
        appCIPOL.onsubmit = false;
        appCIPOL.urlinicio = 'frmLogin.aspx';
        $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());
    } catch (e) {
        appCIPOL.messageBox('Error', 'Error Inesperado: Al iniciar', '', 'Aceptar', e);
    } finally {
        appCIPOL.DesBloquearUI();
    }
});

var page = {
    update: true,
    init: function () {
        appCIPOL.onsubmit = false;
        appCIPOL.urlinicio = 'frmSistemasPermitidos.aspx';
        $("[id$='iCambiarContrasenia']").click(function () { window.location.href = 'ChangedPassword\\frmCambiarContrasenia.aspx?url=../frmSistemasPermitidos.aspx'; });
        $("[id$='iSalir']").click(function () { page.Cerrar(); });
        page.MensajeCambioClave();
    },
    MensajeCambioClave: function () {
        if ($.QueryString["Mensaje"] == "1")
            bootbox.alert("Los datos han sido guardados.");
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
                    window.location = URLSesionExpiro;
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


        //TFS 9133 - se comentaron las lineas con "//////" para que no abra un pop-up en blanco. Las comentadas con "//" ya estaban así antes de este TFS.

       //////wOpen = window.open('', aWinName, sOptions);

        //wOpen.location = url;
        //wOpen.focus();
        //SOLO EJECUTO ESTO SI EL BROWSER NO ES CROME

        //////if (!$.browser.chrome) {
        //////    wOpen.moveTo(0, 0);
        //////    wOpen.resizeTo(screen.availWidth, screen.availHeight);
        //////}

        //wOpen = window.open("", "Map", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes");

        ///// if (wOpen) {
            mapForm.submit();
        /////} else {
        /////    alert('Debe permitir pop-ups para trabajar.');
        /////}
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
                window.location.href = appCIPOL.urlinicio;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    }
}











