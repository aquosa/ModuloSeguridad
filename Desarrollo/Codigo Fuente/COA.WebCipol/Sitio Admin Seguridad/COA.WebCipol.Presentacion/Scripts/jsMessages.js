
function ShowAlertDanger(messageError) {
    $("[id$='span-alert-danger']").text(messageError);
    $('#dialog-alert-danger').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () { $('#dialog-alert-danger').dialog('close'); }
        }
    });
    $('#dialog-alert-danger').dialog('open');
};

function ShowAlertSuccess(messageError) {
    $("[id$='span-alert-success']").text(messageError);
    $('#dialog-alert-success').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        position: ['center',20],
        buttons: {
            "ACEPTAR": function () { $('#dialog-alert-success').dialog('close'); }
        }
    });
    $('#dialog-alert-success').dialog('open');
};

function ShowAlertInfo(messageError) {
    $("[id$='span-alert-info']").text(messageError);

    $('#dialog-alert-info').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () { $('#dialog-alert-info').dialog('close'); }
        }
    });
    $('#dialog-alert-info').dialog('open');
};

function ShowAlertWarningDelete(messageError, success, p1, p2) {
    $("[id$='span-alert-warning']").text(messageError);
    $('#dialog-alert-warning').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () { success(p1, p2); $('#dialog-alert-warning').dialog('close'); },
            "CANCELAR": function () { $('#dialog-alert-warning').dialog('close'); }
        }
    });
    $('#dialog-alert-warning').dialog('open');
};

function ShowAlertWarningExitPopUP(messageError, popupClose) {
    $("[id$='span-alert-warning']").text(messageError);
    $('#dialog-alert-warning').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () {$('#dialog-alert-warning').dialog('close'); popupClose.dialog('close'); },
            "CANCELAR": function () { $('#dialog-alert-warning').dialog('close'); }
        }
    });
    $('#dialog-alert-warning').dialog('open');
};

function ShowAlertWarningFunctionWithoutParameter(messageError, oneFunction) {
    $("[id$='span-alert-warning']").text(messageError);
    $('#dialog-alert-warning').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () { oneFunction(); $('#dialog-alert-warning').dialog('close'); },
            "CANCELAR": function () { $('#dialog-alert-warning').dialog('close'); }
        }
    });
    $('#dialog-alert-warning').dialog('open');
};

function ShowAlertWarningFunctionWithOneParameter(messageError, oneFunction, p1) {
    $("[id$='span-alert-warning']").text(messageError);
    $('#dialog-alert-warning').dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "ACEPTAR": function () { oneFunction(p1); $('#dialog-alert-warning').dialog('close'); },
            "CANCELAR": function () { $('#dialog-alert-warning').dialog('close'); }
        }
    });
    $('#dialog-alert-warning').dialog('open');
};