/*
 * Clases
 * 
 * Dialog (popup)
 * .ui-dialog
 * .ui-dialog-open
 * data -> dialogid
 * data -> id
 * .ui-dialog-close
 * .ui-dialog-send
 */
var uiDialog;

$(function() {
    
    /*
     * Custom checkbox y radios
     */
    $(document).on('click', '.radio-inline, .checkbox-inline', function(e) {
        e.stopImmediatePropagation();
        e.preventDefault();
        
        var span = $(this).find('span.custom');
        var radio = $(this).find('input[type="radio"]');
        var checkbox = $(this).find('input[type="checkbox"]');
        var input = null;
        var radios = null;
        
        if (radio.length > 0) {
            input = radio;
            radios = $(document).find('input[type="radio"][name="' + input.prop('name') + '"]');
            var char = "&#9679";
        }
        
        if (checkbox.length > 0) {
            input = checkbox;
            var char = "&#10003";
        }
        
        if (input.length > 0 && span.length > 0) {
            span.toggleClass('active');
            if (radios && radios.length > 0) {
                radios.each(function(index, element) {
                    if (input.prop('id') != $(element).prop('id')) {
                        $(element).prop("checked", false);
                        $(element).parents('label').find('span.custom').html('');
                    }
                });
            }
            if (input.prop("checked")) {
                input.prop("checked", false);
                span.html('');
            } else {
                input.prop("checked", true);
                span.html('<span>' + char + '</span>');
            }
        }
    });

    /*
     * Login vertical aligner
     */
    if ($('.vertical-aligner').length > 0) {

        var bodyHeight = $('body').outerHeight();

        var alignerHeight = $('.vertical-aligner .row').outerHeight();
        var top = (bodyHeight - alignerHeight) / 2;
        $('.vertical-aligner').css('top', top + 'px');

        if (bodyHeight > alignerHeight) {
            $('body').css('overflow', 'hidden');
        }

        $(window).resize(function() {
            bodyHeight = $('body').outerHeight();
            if (bodyHeight > alignerHeight) {
                top = (bodyHeight - alignerHeight) / 2;
                $('.vertical-aligner').css('top', top + 'px');
                $('body').css('overflow', 'hidden');
            } else {
                $('body').css('overflow', 'auto');
            }
        });
    }

    /*
     Popups
     */
    /*$('.btn-edit').magnificPopup({
        items: {
            src: '#sistemaPopup',
            type: 'inline',
            midClick: true
        }
    });*/
    /*
     * ui-dialog
     */
    //instancia
    uiDialog = $( ".ui-dialog" ).dialog({
          autoOpen:false,
          draggable: false,
          resizable: false,
          height: 700,
          width: 840,
          modal: true
        });
    //open
    $(document).on("click",".ui-dialog-open",function(e) {
        e.preventDefault();
        var dialogId = $(this).data('dialogid');
        uiDialog.filter("#" + dialogId).dialog("open");
    });
    //close
    $(document).on("click",".ui-dialog-close",function(e) {
        e.preventDefault();
        $(uiDialog).dialog( "close" );
    });
    //send
    $(document).on("click",".ui-dialog-send",function(e){
        e.preventDefault();
        var form = $(this).parents("form"); //Formulario de este dialog
        console.log(form);
        alert("implementar un callback")
    });
    /*
     * ui-datepicker
     */
    uiDatepicker = $(".datepicker").datepicker({
        currentText: "Hoy",
        dateFormat: "dd/mm/yy",
        dayNames: ["Domingo", "Lunes", "Martes", "Mi&eacute;rcoles", "Jueves", "Viernes", "S&aacute;bado"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
        dayNamesShort: ["Dom", "Lun", "Mar", "Mier", "Jue", "Vier", "Sab"],
        monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        monthNamesShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
        onSelect: function(dateText, inst) {
           // Aca se puede agregar un callback de seleccion
        }
    });


   /* $('section#terminales .btn-edit').magnificPopup({
     items: {
     src: '#terminalesPopup',
     type: 'inline',
     midClick: true
     }
     });*/

    /*$('section#usuarios .btn-edit').magnificPopup({
     items: {
     src: '#usuariosPopup',
     type: 'inline',
     midClick: true
     }
     });*/

    /*
     * Tabs
     */
    $('.tabs li a').click(function(e) {
        e.preventDefault()
        $(this).tab('show')
    });

    /*
     * jsTree
     */
    if ($('.roles-tree').length > 0) {
        $('#roles-plantilla').jstree();
        $('#roles-asignados').jstree();
    }

    /*
     Menu toggle on window resize
     */
    $(window).resize(function() {
        if ($('.dropdown.open .dropdown-toggle')) {
            $('.dropdown.open .dropdown-toggle').dropdown('toggle');
        }
    });

    // ADD SLIDEDOWN ANIMATION TO DROPDOWN //
    $('.dropdown').on('show.bs.dropdown', function(e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown(250);
    });

    // ADD SLIDEUP ANIMATION TO DROPDOWN //
    $('.dropdown').on('hide.bs.dropdown', function(e) {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp(250);
    });


});