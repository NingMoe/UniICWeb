$(function () {
    var its = $(".uni_menu").children("li");
    its.each(function () {
        var it = $(this);
        it.addClass("mu_it");
        var m2 = it.children("ul:first");
        if (m2.length > 0) {
            var offset = it.offset();
            m2.addClass("menu2");
            m2.find('li').css('width', (it.width()-2));
            m2.offset({ top: it.height(), left: 0 });
            var sel = it.children("a:first");
            it.hover(function () {
                //m2.css('display', 'block');
                m2.slideDown(300);
            },
            function () {
                //m2.css('display', 'none');
                m2.slideUp(300);
            });
            //it.mouseover(function () {
            //    debugger;
            //    m2.css('display', 'block');
            //    //m2.slideDown(500);
            //});
            //it.mouseout(function (e) {
            //    debugger;
            //    //var fs = sel.offset();
            //    //var xx = e.originalEvent.x || e.originalEvent.layerX || 0;
            //    //var yy = e.originalEvent.y || e.originalEvent.layerY || 0;
            //    //e.stopPropagation();
            //    //if ((xx - fs.left) > 0 && (xx - fs.left) < it.width()) {
            //    //    if ((yy - fs.top) > 0) {
            //    //        return false;
            //    //    }
            //    //}
            //    m2.css('display', 'none');
            //    //m2.slideUp(500);
            //});
            //m2.mouseover(function () {
            //    debugger;
            //    m2.css('display', 'block');
            //    //m2.slideUp(500);
            //});
            //m2.mouseout(function () {
            //    debugger;
            //    m2.css('display', 'none');
            //    //m2.slideUp(500);
            //});
        }
    });
});