//Inicialización de la página
$(function () {
    appCIPOLPRESENTACION.init();
    appCIPOLPRESENTACION.clicked = 1;
});
/*Variable de uso comun en todos los formularios*/
var appCIPOLPRESENTACION = {
    modoDebug: "NO",
    init: function () {
        appCIPOLPRESENTACION.onsubmit = true;
        //Setea de la sessión.
        appCIPOLPRESENTACION.urlinicio = "frmPrincipal.aspx";
        appCIPOLPRESENTACION.timeridtimeout = setInterval("appCIPOLPRESENTACION.VerifyTimeOut()", 1200000);

        $(document).bind("keydown keypress", function (e) {
            if (e.which == 8) { // 8 == backspace
                var blnCtrlPermiteBKPSPC = (e.target.type != "text") && (e.target.type != "textarea") && (e.target.type != "password");
                if (blnCtrlPermiteBKPSPC || e.target.disabled || e.target.readOnly) {
                    e.preventDefault();
                }
            }
        });
        //$(document).ajaxStart($.blockUI({ message: '<h3><img src="Imagenes/loading.gif" />En Proceso...</h3>' })).ajaxStop($.unblockUI());
        appCIPOLPRESENTACION.CargarElementoBase();


        $('#dialogAutorizarTarea').dialog({
            autoOpen: false,
            resizable: false,
            modal: true
        });
    },
    /*Campo destinado a guardar el json del elemento base del formulario*/
    trxdata: null,
    //[MiguelP]         28/10/2014      GCP - Cambios 15598
    trxFiltro:null,
    trxdatapermiso: null,
    trxdataautorizacion: null,
    blnautorizada: false,
    elementoid: null,
    pageprevious: new Array(),
    pagenext: "",
    botonsalir_activo: false,
    htmlwait: "",
    timerbuzontest: 0,
    urlinicio: "",
    timeridtimeout: 0,
    clicked: -1,
    onsubmit: true,
    uidialogbuttonpane: ".ui-dialog-buttonpane",
    Verifyonsubmit: function () {
        return appCIPOLPRESENTACION.onsubmit;
    },
    VerifyTimeOut: function () {

        if (appCIPOLPRESENTACION.clicked == 1) {
            appCIPOLPRESENTACION.clicked = 0;
        }
        else {
            if (appCIPOLPRESENTACION.clicked == 0) {
                clearInterval(appCIPOLPRESENTACION.timeridtimeout);
                window.location.href = appCIPOLPRESENTACION.urlinicio;
            }
        }
    },

    /*Visuliza un mensaje de error*/
    messageBox: function (title, msgerror, postfunction, Nombre, responseJSON) {
        var _msg = "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-check' style='float: left; margin: 0 7px 0;z-index :9999;'></span>" +
                    msgerror + "</p></div>";
        if (responseJSON != null) {
            if (responseJSON.Message != null)
                _msg += "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px  0;z-index :9999;'></span>MENSAJE: " +
                    responseJSON.Message + "</p></div>";
            if (responseJSON.StackTrace != null)
                _msg += "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px  0;z-index :9999;'></span>ORIGEN: " +
                    responseJSON.StackTrace + "</p></div>";
            if (responseJSON.ExceptionType != null)
                _msg += "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px  0;z-index :9999;'></span>TIPO: " +
                    responseJSON.ExceptionType + "</p></div>";
            if (responseJSON.message != null)
                _msg += "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px  0;z-index :9999;'></span>MENSAJE: " +
                    responseJSON.message + "</p></div>";
            if (responseJSON.description != null)
                _msg += "<div style='overflow: hidden; white-space: nowrap; word-wrap: break-word;'><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px  0;z-index :9999;'></span>DESCRIPCIÓN: " +
                    responseJSON.description + "</p></div>";
        }
        $("#popup").attr("title", title);
        $("#popup").html(_msg);
        $("#popup").dialog({
            modal: true,
            width: 700,
            height: 'auto',
            maxWidth: 700,
            position: 'center',
            resizable: false,
            hide: {
                effect: "explode",
                duration: 1000
            },
            buttons: [
                        {
                            text: Nombre,
                            click: function () {
                                $(this).dialog("close");
                                if (postfunction != undefined) {
                                    eval(postfunction);
                                }
                            }
                        }
                    ]
        }).delay(1001);

        var width = $(".ui-dialog").width();
        if (width > 400) {
            if (responseJSON != null)
                $(".ui-dialog").css({ 'width': '700px' }).css({ 'position': 'center' });
            else
                $(".ui-dialog").css({ 'width': '400px' }).css({ 'position': 'center' });
            //fixed if width > 400px, defined width always as 400px 
        }
    },
    messageBoxWarning: function (msgerror) {
        var _msg = "<span class='warning' style='float: left; margin: 0 7px 50px 0;z-index :9999;'>" + msgerror + "</span>";
        //$("#divmessage").attr("title", title);
        $("#divmessage").html(_msg);
        $("#divmessage").hover(function () { $(this).hide(); }, function () { });
        $("#divmessage").fadeIn(2000, function () { $("#divmessage").fadeOut(2000); });
    },
    BloquearUI: function () {
        $.blockUI({ message: '<h3><img src="Imagenes/loading.gif" />En Proceso...</h3>' });
    },
    DesBloquearUI: function () {
        $.unblockUI();
    },
    /*Función trim() de un string*/
    trim: function (data) {
        return data.replace(/^\s+/g, '').replace(/\s+$/g, '')
    },
    /*Setea un atributo de un campo.*/
    SetAtributo: function (filter, atributo, valor) {
        $(filter).attr(atributo, valor);
    },
    rendercontrol: function ($Contenedor, data) {

        var DatosAImprimir = $.parseJSON(data);
        //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
        //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
        //entities y obtener el código HTML original.
        DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();

        $Contenido = $(DatosAImprimir.Lista);
        $Contenedor.children().remove(); //TODO
        var $tablasEnContenido = $Contenido.find("table");
        //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
        //no quede código HTML sucio o incorrecto.
        if ($tablasEnContenido.length > 0) {
            $Contenido.find("form").children().unwrap();
            $Contenido.css("list-style-type", "none");
            $Contenedor.append($Contenido);
        }
        else {
            //Si el contenido no contiene tablas, asumo que el WS me devolvió
            //código HTML correcto y listo para insertarse.
            $Contenedor.append(DatosAImprimir.Lista);
        }
    },
    Supervisar: function (evento) {
        /*llamar al div de autorización*/
        var ret;
        ret = 'ninguno';
        ret = window.showModalDialog(url, '', 'dialogHeight:400px;dialogWidth:600px;titlebar:no;help:no;toolbars:no;scrollbars:no;status:no;resizable:no;');
        if (ret == 'ninguno') {
            //Si la supervisión se realiza al iniciar la página y se cancela
            //redirecciona a la página de inicio, de lo contrario retorna false
            //para que no se ejecute el evento Submit
            if (evento = "onload")
                window.location.href = appCIPOLPRESENTACION.urlinicio;
            else
                return false;
        }
    },
    //AUTORIZACIONES DE TAREAS
    SupervisarModal: function (evento) {
        var app = appCIPOLPRESENTACION.trxdata;
        appCIPOLPRESENTACION.CargarComboSupervisores();
        //Inicializa el textbox como fecha.
        $('#dialogAutorizarTarea').dialog({
            buttons: {
                "ACEPTAR": function () {
                    appCIPOLPRESENTACION.BloquearUI();
                    try {
                        //page.validarCerrar = false;
                        appCIPOLPRESENTACION.ResultadoVerif();
                    } catch (e) {
                        ShowAlertDanger('Error Inesperado.');
                    } finally {
                        appCIPOLPRESENTACION.DesBloquearUI();
                    }
                },
                "CANCELAR": function () { $('#dialogAutorizarTarea').dialog('close'); }
            }
             , close: function () {
                 if (appCIPOLPRESENTACION.blnautorizada) {
                     appCIPOLPRESENTACION.blnautorizada = false;
                 } else {
                     window.history.back();
                 }
             }
        });

        $('#dialogAutorizarTarea').dialog('open');
        return false;
    }, ResultadoVerif: function () {
        var idSupervisor = GetDDL('cboSupervisores');
        if (idSupervisor == -1) return;

        var app = appCIPOLPRESENTACION.trxdataautorizacion;
        app.usuario = idSupervisor;
        app.clave = GetTXT('Password');
        
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/ValidarSupervisor",
            data: "{'obj':" + JSON.stringify(app) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                appCIPOLPRESENTACION.blnautorizada = respuesta.d.ResultadoEjecucion;
                if (!appCIPOLPRESENTACION.blnautorizada) {
                    bootbox.alert(respuesta.d.MensajeError);
                    //window.history.back();
                } else {
                    $('#dialogAutorizarTarea').dialog('close');
                    return false;
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
    },
    CargarElementoBase: function () {
        //url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoTareaPrimitivaBase",
        //Setea la estructura del view
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarElementoUsuarioCipolBase",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                appCIPOLPRESENTACION.trxdataautorizacion = data.d;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });
        //Setea la estructura del view
//        $.ajax({
//            type: "POST",
//            url: "./PageBuilders/wsCipolSupervisionTareas.asmx/RecuperarElementoUsuarioCipolBase",
//            data: "",
//            contentType: "application/json; charset=iso-8859-1",
//            dataType: "json",
//            async: false,
//            success: function (data) {
//                appCIPOLPRESENTACION.trxdataautorizacion = data.d;
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                ShowAlertDanger('Error al recuperar el elemento. ' + xhr.responseJSON);
//            }
//        });
    }, CargarComboSupervisores: function () {
        //Realiza la búsqueda de los datos y los visualiza en ua grilla.
        $Contenedor = $("#reemplazarcbosupervisores");
        $.ajax({
            type: "POST",
            url: "./PageBuilders/wsAjaxwsSeguridad.asmx/RecuperarSupervisores",
            data: "",
            contentType: "application/json; charset=iso-8859-1",
            dataType: "json",
            async: false,
            success: function (data) {
                var DatosAImprimir = $.parseJSON(data.d);
                //NOTA: El webservice me devolverá código HTML encodeado con ENTITIES para evitar cualquier problema
                //con caracteres con acentos o especiales. La siguiente línea de código utiliza JQuery para decodificar los
                //entities y obtener el código HTML original.
                DatosAImprimir.Lista = $("<div/>").html(DatosAImprimir.Lista).text();

                $Contenido = $(DatosAImprimir.Lista);
                $Contenedor.children().remove(); //TODO
                var $tablasEnContenido = $Contenido.find("table");
                //Si la respuesta contiene alguna tabla, limpio el contenido para que sólo se muestre la tabla y 
                //no quede código HTML sucio o incorrecto.
                if ($tablasEnContenido.length > 0) {
                    $Contenido.find("form").children().unwrap();
                    $Contenido.css("list-style-type", "none");
                    $Contenedor.append($Contenido);
                }
                else {
                    //Si el contenido no contiene tablas, asumo que el WS me devolvió
                    //código HTML correcto y listo para insertarse.
                    $Contenedor.append(DatosAImprimir.Lista);
                }
                $("[id$='cboSupervisores']").addClass("col-md-12 select-no-padding");
            },
            error: function (xhr, ajaxOptions, thrownError) {
                throw xhr.responseJSON;
            }
        });

    }
}