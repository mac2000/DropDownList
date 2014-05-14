(function (document) {
    function closeAll() {
        var list = document.querySelectorAll('.ddl > .value.active');
        for (var i = 0; i < list.length; i++) {
            list[i].classList.remove('active');
        }
    }

    document.addEventListener('click', function (e) {
        if (e.target && e.target.nodeType === 1 && e.target.className.match(/value/) && e.target.parentNode.nodeType === 1 && e.target.parentNode.className.match(/ddl/)) { // .ddl > .value
            e.target.classList.toggle('active'); // show/hide options
        }
        else if (e.target && e.target.nodeType === 1 && e.target.className.match(/option/) && e.target.parentNode.nodeType === 1 && e.target.parentNode.className.match(/options/) && e.target.parentNode.parentNode.nodeType === 1 && e.target.parentNode.parentNode.className.match(/ddl/)) { // .ddl > .options > .option
            e.target.parentNode.querySelector('.active').classList.remove('active'); // visually unselect previous option
            e.target.classList.add('active'); // visually select current one
            e.target.parentNode.parentNode.querySelector('.value').innerHTML = e.target.innerHTML; // set value text
            e.target.parentNode.parentNode.querySelector('.hidden').value = e.target.getAttribute('data-value'); // set value
            closeAll();

            var event = document.createEvent('HTMLEvents');
            event.initEvent('change', true, false);
            e.target.parentNode.parentNode.dispatchEvent(event);
        }
        else { // outside of .ddl
            closeAll();
        }
    });
})(document);

if (typeof jQuery != 'undefined') {
    jQuery.valHooks.span = {
        get: function (el) {
            if (jQuery(el).hasClass('ddl')) {
                return jQuery(el).find('input').val();
            }
        },
        set: function (el, val) {
            if (jQuery(el).hasClass('ddl')) {
                var li = jQuery(el).find('.option[data-value="' + val + '"]');

                if (!li.size()) {
                    li = jQuery(el).find('.option:first');
                }

                jQuery(el).find('input').val(val);
                jQuery(el).find('.value').html(li.html());
                jQuery(el).find('.option.active').removeClass('active');
                li.addClass('active');
            }
        }
    };
}