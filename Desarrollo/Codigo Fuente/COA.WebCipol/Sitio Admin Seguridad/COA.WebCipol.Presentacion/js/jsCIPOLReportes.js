//Inicialización de la página
$(function () {
    appCIPOLPRESENTACIONReportes.init();
});
/*Variable de uso comun en todos los formularios*/
var appCIPOLPRESENTACIONReportes = {
    init: function () {
        $("[id$='cmdGenerar']").button();
    }
}


function f_open_window_max(aURL, aWinName) {
    var wOpen;
    var sOptions;

    sOptions = 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
    sOptions = sOptions + ',outerWidth=' + (screen.availWidth + 2).toString();
    sOptions = sOptions + ',outerHeight=' + (screen.availHeight + 2).toString();
    sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';

    wOpen = window.open('', aWinName, sOptions);
    wOpen.location = aURL;
    wOpen.focus();
    //wOpen.moveTo(0, 0);
    wOpen.resizeTo(screen.availWidth, screen.availHeight);
    return wOpen;
}