/// <summary>
/// Creado : 25/10/2013 [IvanSa]
/// Funciones de Jquery genericas para controles
/// </summary>

//TextBox
function SetTXT(clientIDControl, value, defaultValue) {
    defaultValue = (defaultValue) ? defaultValue : null;
    var idValue = (value) ? value : defaultValue;
    var check = $("[id$='" + clientIDControl + "cbAll']").get(0);
    var state = true;
    try {
        var control = $("[id$='" + clientIDControl + "']:input").get(0);
        if (idValue) {
            if (control) {
                control.value = idValue;
                state = false;
            }
            else {
                jQuery.error("EL Control " + clientIDControl + " no existe o ha utilizado el metodo incorrecto");
            }
        }
        else {
            control.value = "";
        }
    } catch (err) {
        return false;
    }
    if (check) {
        check.checked = state;
    }
    return false;
}

function GetTXT(clientIDControl) {
    var valueF = null;
    try {
        var control = $("[id$='" + clientIDControl + "']:input").get(0);

        if (control) {
            valueF = control.value;
        }
        else {
            jQuery.error("El control " + clientIDControl + " no existe o ha utilizado mal el metodo Get");
        }
    } catch (err) {
        return null;
    }
    return valueF;
}

//DropDownList
function GetDDL(clientIDControl) {
    var value = null;
    try {
        var control = $("[id$='" + clientIDControl + "']");
        if (control && control.length > 0) {
            value = control.val();
            if ((value && (value == -2 || value == "")) || !value) {
                value = null;
            }
        }
        else {
            jQuery.error("El control " + clientIDControl + " no existe o ha utilizado mal el metodo Get");
        }
    } catch (err) {
        return null;
    }
    return value;
}

function SetDDL(clientIDControl, value, defaultValue) {
    defaultValue = (defaultValue) ? defaultValue : "-2";
    value = (value != null) ? value : defaultValue;
    try {
        var control = $("[id$='" + clientIDControl + "']");
        if (value != null) {
            if (control && control.length > 0) {
                control.val(value);
            }
            else {
                jQuery.error("El Control " + clientIDControl + " no existe o ha utilizado el metodo incorrecto");
            }
        }
    } catch (err) {
        return false;
    }
    return false;
}

function GetDDLTxt(clientIDControl) {
    var valueF = null;
    try {
        var control = $("[id$='" + clientIDControl + "']")[0];
        if (control && control.length > 0) {
            valueF = control[control.selectedIndex].text;
        }
        else {
            jQuery.error("El Control " + clientIDControl + " no existe o ha utilizado el metodo incorrecto");
        }
    } catch (err) {

        return null;
    }
    return valueF;
}