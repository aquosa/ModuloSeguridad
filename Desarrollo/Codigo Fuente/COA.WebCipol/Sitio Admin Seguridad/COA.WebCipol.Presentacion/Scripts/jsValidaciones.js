
function jfValidarFechaDia(objDia, objMes) {
    var CantDias;
    var Dia;
    var Mes;

    Dia = parseInt(objDia.value, 10);
    Mes = parseInt(objMes.value, 10);
    if (isNaN(Dia)) {
        objDia.focus();
        objDia.select();
        alert("Por favor ingrese un dia valido.");
        return false;
    }
    if (Dia < 0 || Dia > 31) {
        objDia.focus();
        objDia.select();
        alert("Por favor ingrese un dia valido.");
        return false;
    }
    return true;
}
function jfValidarFechaMes(objDia, objMes, objAnio) {
    var CantDias;
    var Dia;
    var Mes;
    var Anio;

    Dia = parseInt(objDia.value, 10);
    Mes = parseInt(objMes.value, 10);
    Anio = parseInt(objAnio.value, 10);

    if (isNaN(Mes)) {
        objMes.focus();
        objMes.select();
        alert("Por favor ingrese un mes valido");
        return false;
    }
    if (Mes < 1 || Mes > 12) {
        objMes.focus();
        objMes.select();
        alert("Por favor ingrese un mes correcto");
        return false;
    }

    CantDias = jfDevCantidadDiasMes(Mes, Anio);

    if (Dia > CantDias) {
        objDia.focus();
        objDia.select();
        alert("La cantidad de dias del mes no es correcta.");
        return false;
    }
    return true;
}
function jfValidarFechaAnio(objAnio) {
    var Anio;

    Anio = parseInt(objAnio.value, 10);

    if (isNaN(Anio)) {
        objAnio.focus();
        objAnio.select();
        alert("Por favor ingrese un año valido");
        return false;
    }

    if (Anio < 1000) {
        objAnio.focus();
        objAnio.select();
        alert("Por favor ingrese el año con el formato AAAA.");
        return false;
    }
    return true;
}

function jfValidarFecha(objDia, objMes, objAnio) {
    var CantDias;
    var Dia;
    var Mes;
    var Anio;

    Dia = parseInt(objDia.value, 10);
    Mes = parseInt(objMes.value, 10);
    Anio = parseInt(objAnio.value, 10);

    if (jfValidarFechaDia(objDia, objMes) == false) {
        return false;
    }
    if (jfValidarFechaMes(objDia, objMes, objAnio) == false) {
        return false;
    }
    if (jfValidarFechaAnio(objAnio) == false) {
        return false;
    }
    return true;
}

function jfDevCantidadDiasMes(Mes, Anio) {
    var CantDias;

    switch (Mes) {
        case 1: CantDias = 31; break;
        case 2:
            if (jfEsBisiesto(Anio) == true) {
                CantDias = 29;
            }
            else {
                CantDias = 28;
            }
            break;
        case 3: CantDias = 31; break;
        case 4: CantDias = 30; break;
        case 5: CantDias = 31; break;
        case 6: CantDias = 30; break;
        case 7: CantDias = 31; break;
        case 8: CantDias = 31; break;
        case 9: CantDias = 30; break;
        case 10: CantDias = 31; break;
        case 11: CantDias = 30; break;
        case 12: CantDias = 31; break;
        default: CantDias = 0; break;
    }

    return CantDias;
}
function jfEsBisiesto(Anio) {
    if ((Anio % 4) == 0) {
        return true;
    }
    return false;
}

function jfValidarTxtBuscar(objTxtBuscar) {
    var TxtBuscar;

    return true;

    TxtBuscar = objTxtBuscar.value;

    if (TxtBuscar.replace(/ /g, '').length <= 2) {
        alert("Por favor complete el contenido de la busqueda. No menos de cuatro caracteres.");
        objTxtBuscar.focus();
        return false;
    }
    return true;
}


function jfValidarNumero(objNro) {
    var Numero;

    Numero = parseInt(objNro.value, 10);

    if (isNaN(Numero)) {
        objNro.focus();
        objNro.select();
        alert("El valor debe ser nÃºmerico.");
        return false;
    }
}
function jfValidarNumeroYvacio(objNro) {
    var Numero;

    Numero = parseInt(objNro.value, 10);

    if (objNro.value != "") {
        if (isNaN(Numero)) {
            objNro.focus();
            objNro.select();
            alert("El valor debe ser nÃºmerico.");
            return false;
        }
    }
}
function jfValidarCuit(NumeroDeCUIT) {
    var a = 0;
    var n;
    var x;

    for (x = 1; x < 12; x++) {
        n = NumeroDeCUIT.substring(x, 1);
        n = n + 48;

        switch (x) {
            case 1: m = 5;
                break;
            case 2: m = 4;
                break;
            case 3: m = 3;
                break;
            case 4: m = 2;
                break;
            case 5: m = 7;
                break;
            case 6: m = 6;
                break;
            case 7: m = 5;
                break;
            case 8: m = 4;
                break;
            case 9: m = 3;
                break;
            case 10: m = 2;
                break;
            case 11: m = 1;
                break;
        }
        a = a + n * m;
    }

    a = a % 11;

    if (a == 3) {
        return true;
    }

    return false;
}
//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
function jfSoloNumeros(obj, e) {

    var valor = "" + obj.value; // Valor anterior al nuevo dígito introducido
    var longitud = valor.length; // Nº de dígitos existentes antes
    var campo;
    var caracter;

    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    // Comprobamos que sea dígito
    //if ((window.event.keyCode < 48 || window.event.keyCode > 57) && (window.event.keyCode != 45)) {
    if (((caracter < 48 || caracter > 57) && (caracter != 45))) {

        // NO ES DÍGITO
        //if (window.event.keyCode == 44 || window.event.keyCode == 46) {
        if (caracter == 44) {

            if ((obj.value.indexOf(",") != -1) || longitud == 0) {

                //window.event.keyCode = 0; // Sólo permitimos un simbolo decimal
                if (document.all) {
                    window.event.keyCode = 0;
                }
                else {
                    return false;
                }
            }
        }
        else if (caracter == 46) {

            if (document.all) {
                if ((obj.value.indexOf(",") != -1) || longitud == 0) {
                    window.event.keyCode = 0;
                } else {

                    window.event.keyCode = 44; // Forzamos a la coma                
                }
            }
            else {
                return false;
            }
        }
        else if (caracter == 8) {
            //caracter de borrado.
        }
        else {
            //window.event.keyCode = 0; // Es letra
            if (document.all) {
                window.event.keyCode = 0;
            }
            else {
                return false;
            }
        }

    }

}

//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
//Verifico si el Textbox comienza con '<'
function jfValidarCharInicial(obj, e) {
    var valor = "" + obj.value; // Valor anterior al nuevo dígito introducido
    var longitud = valor.length; // Nº de dígitos existentes antes
    var caracter;

    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    // Compruebo que no sea < o >.
    //if (window.event.keyCode == 60 || window.event.keyCode == 62) {//&& longitud == 0) {
    if (caracter == 60 || caracter == 62) {
        if (document.all) {
            window.event.keyCode = 0;
        }
        else {
            return false;
        }
    }
}

//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
function jfValidarCharInicialMaxLength(obj, tamanio, e) {
    var valor = "" + obj.value; // Valor anterior al nuevo dígito introducido
    var longitud = valor.length; // Nº de dígitos existentes antes
    var caracter;

    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    if (longitud > tamanio) {
        obj.value = obj.value.substring(0, tamanio);
    } else {
        // Compruebo que no sea < o >.
        //if (window.event.keyCode == 60 || window.event.keyCode == 62) {//&& longitud == 0) {
        if (caracter == 60 || caracter == 62) {
            if (document.all) {
                window.event.keyCode = 0;
            }
            else {
                return false;
            }
        }
        if (longitud > tamanio) {

            if (document.all) {
                window.event.keyCode = 0;
            }
            else {
                return false;
            }
        }
    }
}

//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
function jfSoloNumerosPositivos(obj, e) {
    var valor = "" + obj.value; // Valor anterior al nuevo dígito introducido
    var longitud = valor.length; // Nº de dígitos existentes antes
    var campo;
    var caracter;

    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    // Comprobamos que sea dígito
    if (caracter < 48 || caracter > 57) {
        // NO ES DÍGITO
        //if (window.event.keyCode == 44 || window.event.keyCode == 46) {
        if (caracter == 44) {
            //coma
            if ((obj.value.indexOf(",") != -1) || longitud == 0) {

                //window.event.keyCode = 0; // Sólo permitimos un simbolo decimal
                if (document.all) {
                    window.event.keyCode = 0;
                }
                else {
                    return false;
                }
            }
        }
        else if (caracter == 46) {
            //punto decimal
            if (document.all) {
                if ((obj.value.indexOf(",") != -1) || longitud == 0) {
                    window.event.keyCode = 0;
                } else {

                    window.event.keyCode = 44; // Forzamos a la coma                
                }
            }
            else {
                return false;
            }
        }
        else if (caracter == 8) {
            //caracter de borrado.
        }
        else {
            // Es letra
            if (document.all) {
                window.event.keyCode = 0;
            }
            else {
                return false;
            }
        }
    }
}
//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
function jfSoloNumerosEnteros(obj, e) {
    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    if (((caracter > 47) && (caracter < 58)) || (caracter == 8)) {

    } else {
        //window.event.keyCode = 0; // Es letra
        if (document.all) {
            window.event.keyCode = 0;
        }
        else {
            return false;
        }
    }
}
function jfNopermitirSPACE(obj, e) {

    if (document.all) caracter = e.keyCode; else caracter = e.which;

    if (caracter == 32)
        if (document.all) {
            window.event.keyCode = 0;
        }
        else {
            return false;
        }
}
//[PabloC]          [viernes, 23 de diciembre de 2011]       Modificado para compatibilidad firefox-chrome
function jfSoloNumerosEnterosyENTER(obj, e) {

    if (document.all) {
        caracter = e.keyCode;
    }
    else {
        caracter = e.which;
    }

    //if (((window.event.keyCode > 47) && (window.event.keyCode < 58)) || (window.event.keyCode == 13)) {
    if (((caracter > 47) && (caracter < 58)) || (caracter == 13) || (caracter == 8)) {
    } else {
        //window.event.keyCode = 0; // Es letra
        if (document.all) {
            window.event.keyCode = 0;
        }
        else {
            return false;
        }
    }
}

//valor= valor máximo permitido.
function jfSoloNumerosEnterosMenoresA(obj, valor) {

    var numero = parseInt(obj.value, 10);
    if (isNaN(numero)) {
        alert("Valor no nÃºmerico");
        obj.focus();
        return false;

    }
    if (numero > valor) {
        alert("El valor debe ser menor a " + valor);
        obj.focus();
        return false;
    }
    return true;
}


function jfDecimales(obj, digitos, decimales) {
    var msg = " "
    if (obj.value.match(',') == ',') {
        campo = obj.value.split(",");
        if (campo[1].length > decimales) {
            msg = "La cantidad de decimales es incorrecta, debe ingresar " + decimales + " decimal/es";
        }
        else {
            if (campo[0].length > digitos) {
                msg = msg + "La cantidad de digitos es incorrecta, debe ingresar " + digitos + " digitos";
            }
        }
    }
    else {
        if (obj.value.length > digitos) {
            msg = msg + "La cantidad de digitos es incorrecta, debe ingresar " + digitos + " digitos";
        }
    }

    if (msg != " ") {
        alert(msg)
        obj.focus();
    }
}

function jfLimitaCampo(objCampo, limite) {
    if (objCampo.value.length > limite) {
        objCampo.value = objCampo.value.substring(0, limite);
    }
}
function jfDevDecimalConPunto(Decimal) {
    var DecimalConPunto;

    DecimalConPunto = Decimal.toString();
    DecimalConPunto = DecimalConPunto.replace(".", "");
    DecimalConPunto = DecimalConPunto.replace(",", ".");

    return DecimalConPunto;
}
function jfDevDecimalConComa(Decimal) {
    var DecimalConComa;

    DecimalConComa = Decimal.toString();
    DecimalConComa = DecimalConComa.replace(".", ",");

    return DecimalConComa;
}
Number.prototype.decimal = function (num) {
    pot = Math.pow(10, num);
    return parseInt(this * pot) / pot;
}


function formatCurrency(n) // n = number, d = delimeter
{
    var d;
    d = '.';
    // round to 2 decimals if cents present
    n = (Math.round(n * 100) / 100).toString().split('.');
    var
				myNum = n[0].toString(),
				fmat = new Array(),
				len = myNum.length,
				i = 1, deci = (d == '.') ? ',' : '.';
    for (i; i < len + 1; i++) fmat[i] = myNum.charAt(i - 1);
    fmat = fmat.reverse();
    for (i = 1; i < len; i++) {
        if (i % 3 == 0) {
            fmat[i] += d;
        }
    }
    var val = fmat.reverse().join('') +
			(n[1] == null ? deci + '00' :
	 	    (deci + n[1])
		 );
    return val;
}



//martinv falta terminar función de validación de fechas.
function jfValidarFechas(obj) {

    var Fecha = new Array();
    var len = obj.value.length;
    var i = 1;

    if (len == 9) {
        for (i; i < len + 1; i++) Fecha[i] = obj.value.charAt(i - 1);
        if (Fecha[3] == "/" && Fecha[6] == "/") {

            var dia = Fecha[1] + Fecha[2];
            var mes = Fecha[4] + Fecha[5];
            var anio = Fecha[7] + Fecha[10];

            if (jfValidarFecha(dia, mes, anio) == false) {

                alert("Fecha o Formato incorrecto");
            }

        } else {

            alert("Formato de fecha Incorrecto.(dd/mm/aaaa)");
        }
    }

}
/// <summary>
/// Valida el maxlegth de un control.
/// </summary>
/// <param name="Control">control a validar.</param>
/// <param name="maxlength">tamaño máximo de caracteres.</param>
/// <history>maritnv    05/08/2008     Gcp-Cabmbios 7222.</history>
function ValidarCaracteres(Control, maxlength) {
    if (Control.value.length > maxlength) {
        Control.value = Control.value.substring(0, maxlength);
        alert("Debe ingresar hasta un mÃ¡ximo de " + maxlength + " caracteres");
    }
}

function Validarmaxlenght(Control, maxlength) {
    if (Control.value.length > maxlength) {
        Control.value = Control.value.substring(0, maxlength);
        //window.event.keyCode = 0; // Es letra
    }
}

//< 60 > 62
function ValidarHTML(sender, args) {
    if (args.Value.indexOf('<', 0) >= 0) {
        args.IsValid = false;
        return;
    }

    if (args.Value.indexOf('>', 0) >= 0) {
        args.IsValid = false;
        return;
    }

    args.IsValid = true;
}

function CaracteresExtranios(objeto, e) {
    $.browser.firefox = /firefox/.test(navigator.userAgent.toLowerCase());

    var keynum;

    if (window.event) {
        keynum = e.keyCode;
    }
    else if (e.which) {
        keynum = e.which;
    };

    if (keynum == 39)
        if (!$.browser.firefox) {
            event.returnValue = false;
        }
        else {
            e.preventDefault();
        };
};