(function (a) {
    a.fn.validCampoFranz = function (b) {
        a(this).on(
        { keypress: function (a) {
            var c = a.which, d = a.keyCode, e = String.fromCharCode(c).toLowerCase(), f = b;
            (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()
        }
        })
    }
})(jQuery);
/*No permite realizar crtl c,ctrl v o ctrl x sobre el control.*/
(function (a) {
    a.fn.NoCtrl_c_v_x = function () {
        a(this).bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    }
})(jQuery);

(function ($) {

    $.fn.maxlength = function () {
        
        $("textarea, input").NoCtrl_c_v_x();

        var onEditCallback = function(remaining){}
        var onLimitCallback = function(){}

         $('textarea[maxlength], input[maxlength]').limitMaxlength({
            onEdit: onEditCallback,
            onLimit: onLimitCallback
        });
        }
})(jQuery);

(function ($) {
    $.QueryString = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'))
})(jQuery);


jQuery.fn.limitMaxlength = function(options){

    var settings = jQuery.extend({
        attribute: "maxlength",
        onLimit: function(){},
        onEdit: function(){}
    }, options);

    // Event handler to limit the textarea
    var onEdit = function(){
        var textarea = jQuery(this);
        var maxlength = parseInt(textarea.attr(settings.attribute));

        if(textarea.val().length > maxlength){
            textarea.val(textarea.val().substr(0, maxlength));

            // Call the onlimit handler within the scope of the textarea
            jQuery.proxy(settings.onLimit, this)();
        }

        // Call the onEdit handler within the scope of the textarea
        jQuery.proxy(settings.onEdit, this)(maxlength - textarea.val().length);
    }

    this.each(onEdit);

    return this.keyup(onEdit)
                .keydown(onEdit)
                .focus(onEdit)
                .live('input paste', onEdit);
}