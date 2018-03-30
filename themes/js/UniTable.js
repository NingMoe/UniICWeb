
$.fn.UniTable = function (options) {
    var pTbls = $(this);
    $(this).each(function () {
        var pThis = $(this);
        var defaults = {
            ShowCheck: false,
            HeaderOrder: true
        };
        var opts = $.extend(defaults, options);
        pThis.addClass("UniTable");
        GetOptsFromAttr(opts, pThis);

        if (opts.ShowCheck) {
            var trs = $("tbody tr", pThis);
            var FirstChildCheck;

            trs.children().click(function () {
                var pfcheck = $(this).siblings().first().find("input");
                var domCheck = pfcheck.get(0);
                if (domCheck.checked) {
                    FirstChildCheck.each(function () { this.checked = false; $(this).parent().parent().removeClass("selected"); });
                    domCheck.checked = false; $(this).parent().removeClass("selected");
                } else {
                    FirstChildCheck.each(function () { this.checked = false; $(this).parent().parent().removeClass("selected"); });
                    domCheck.checked = true; $(this).parent().addClass("selected");
                }
            });

            FirstChildCheck = trs.prepend($('<td><input type="checkbox" name="tblSelect"/></td>')).find("input");
            FirstChildCheck.each(function () { $(this).val($(this).parent().next().data("id")) }).change(function () {
                if (this.checked) {
                    $(this).parent().parent().addClass("selected");
                } else {
                    $(this).parent().parent().removeClass("selected");
                }
            });

            $("thead tr", pThis).prepend($('<td><input type="checkbox"/></td>')).find("input").change(function () {
                if (this.checked) {
                    FirstChildCheck.each(function () { this.checked = true; $(this).parent().parent().addClass("selected"); });
                } else {
                    FirstChildCheck.each(function () { this.checked = false; $(this).parent().parent().removeClass("selected"); });
                }
            });
        }
        if(opts.HeaderOrder)
        {
            var ths = $("thead th", pThis);
            var objOrderKey = $("<input name='_szOrderKey' type='hidden'>");
            var objOrderMode = $("<input name='_szOrderMode' type='hidden'>");
            pThis.prepend(objOrderKey);
            pThis.prepend(objOrderMode);
            
            ths.click(function(){
	            OnTableOrder(pThis,objOrderKey,objOrderMode,$(this).attr("name"));
	        });
	        
            function OnInitTableHeader()
            {
	            var szOrderField = objOrderKey.val();
	            var szOrderMode = objOrderMode.val();        	
	            ths.html(function(nindex,szhtml){
	                var thisID = $(this).attr("name");
	                if(thisID != null && thisID != "")
	                {
	                    if(thisID == szOrderField)
	                    {
	                        if(szOrderMode == "asc")
	                        {
                                return "<div class=\'canOrder orderAsc\'>" + szhtml+"</div>";  //当前排序行，正序
	                        }else{
                                return "<div class=\'canOrder orderDesc\'>" + szhtml+"</div>";  //当前排序行，侄序
	                        }
	                    }else{
                            return "<div class=\'canOrder\'>" + szhtml+"</div>";  //可排序行
	                    }
                    }else{
                        return szhtml;  //不能排序行，原样输出
                    }
                });
            }
            setTimeout(OnInitTableHeader,0);
        }
    });
};

function OnTableOrder(pThis,oOrder,oOrderMode,szColID)
{
    if(szColID == null || szColID == "")
    {
        return;
    }
    var order = oOrder.val();
    var ordermode = oOrderMode.val();

    if(szColID == order)
    {
        if(ordermode == "asc")
        {
            oOrderMode.val("desc");
        }else{
            oOrderMode.val("asc");
        }
    }else{
        oOrder.val(szColID);
        oOrderMode.val("asc");
    }
    pThis.parents("form").submit();    
}
