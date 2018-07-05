function actualizarContenidoAjax(urlPagina) {
    $contenedor = $("#ContenedorContenido");

    $.get(urlPagina, function (respuesta) {
        $contenedor.html(respuesta);

        if ($.browser.msie) {
            reSizeIFrameContenidoIE();
        }
        else {
            reSizeIFrameContenido();
        }
    });
}

function actualizarContenido(urlContenido) {
    $contenedor = $("#ContenedorContenido");
    $contenedor.attr('src', urlContenido);
    if ($.browser.msie) {
        reSizeIFrameContenidoIE();
    }
    else {
        reSizeIFrameContenido();
    }
}

function reSizeIFrameContenido() {
    $('#ContenedorContenido').load(function () {
        this.style.height = ((this.contentWindow.document.body.offsetHeight) + 120) + 'px';
        //this.style.height = $(this).contents().height() + 'px';
    });
}

function reSizeIFrameContenidoIE() {
    try {
        $('#ContenedorContenido').load(function () {
            var oBody = ContenedorContenido.document.body;
            var oFrame = document.all("ContenedorContenido");            
            oFrame.style.height = document.frames.ContenedorContenido.document.body.offsetHeight + document.frames.ContenedorContenido.document.body.scrollHeight - document.frames.ContenedorContenido.document.body.clientHeight + 50;
        });
    }
    //An error is raised if the IFrame domain != its container's domain
    catch (e) {
        window.status = 'Error: ' + e.number + '; ' + e.description;
    }
}

/////////// FUNCIONES PARA VENTANAS MODALES ///////////

function agregarVentanaModal(contenidoVentana) {
    var $contenidoVentana = $(contenidoVentana);
    var idVentana = $contenidoVentana.attr('id');
    $('body').append(contenidoVentana);
    $('#' + idVentana).dialog({
        modal: true,
        close: function () {
            $(this).remove();
        }
    });
}

function recuperarContenidoASPX(strURL) {
    var contenidoFinal;
    //Llamo al .aspx
    $.ajax({
        type: 'POST',
        url: strURL,
        async: false,
        success: function (retorno) {
            $retorno = $(retorno);
            var contenido = $retorno.find("#wrapper").html();
            contenidoFinal = contenido;
        },
        error: function (error) {
            alert("Ha ocurrido un error: " + error);
            contenidoFinal = null;
        }
    });
    return contenidoFinal;
}


///////////////////////////////////////////////////////