
$.fn.UISelect = function (options) {
    var pAll = $(this);
    $(this).each(function () {
        var pThis = $(this);
        var defaults = {
        };
        var opts = $.extend(defaults, options);
		
        var pInited = pThis.data("UISelectInited");
        if (pInited != null && pInited != "") {
            return;
        } else {
            pThis.data("UISelectInited","true");
        }

		var pthisName = pThis.data("id");
		var pthisNameText = pThis.data("name");
		var datasource = pThis.data("source");
		var single = pThis.data("single");
		var searchTip = pThis.data("tip");
		var depend = pThis.data("depend");
		var mode = pThis.data("mode");
				
		var searchCls = $("<input/>").prependTo(pThis);
		searchCls.addClass("searchCls");
		if(mode == "select")
		{
		    searchCls.attr("readonly","readonly");
		    searchCls.addClass("searchClsReadonly");
		}
		
		var GroupList = $("input[name='"+pthisName+"']",pThis);
		if(GroupList.length == 0)
		{
		    GroupList = $("<input name='"+pthisName+"'/>").appendTo(pThis);
		}
		GroupList.addClass("InputStealth");
		if(pThis.hasClass("validate[required]"))
		{
			GroupList.addClass("validate[required]");
		}
		var GroupListName = $("input[name='"+pthisNameText+"']",pThis);
		if(GroupListName.length == 0)
		{
		    GroupListName = $("<input name='"+pthisNameText+"'/>").appendTo(pThis);
		}
		GroupListName.addClass("InputStealth");
		
		var resultCls = $("<ul class=\"ClsItem\"/>").appendTo(pThis);
		$("<div class=\"ui-state-disabled emptyCls\">未选择</div>").appendTo(resultCls);
		if(single)
		{
			resultCls.css("display","none");
		}else{
			pThis.addClass("UIPSelect");
		}
		var trash = $("<ul class=\"ClsItem miniTrash\"/>").appendTo(pThis);
		$("<div class=\"ui-state-disabled emptyCls\">未选择</div>").appendTo(trash);
		
		var oldGroupList = "";
		
		//setTimeout(function(){
		    if(depend)
            {
                var arrdepend = depend.split(",");
                for(var i = 0; i < arrdepend.length; i++)
                {
                    if(typeof(arrdepend[i])=="string" && arrdepend[i].length > 0)
                    {
                        var d = $("*[name='"+arrdepend[i]+"']");                        
                        d.change(function(){
                            GroupList.val("");
                            GroupListName.val("");
                            searchCls.val(searchTip).css("color", "#aaaaaa");
                        });
                    }
                }
            }
        //},1);
        
        function ShowCls() {

            var clsItems = $("li",resultCls);
            if (clsItems.length == 0) {
                $(".emptyCls",resultCls).show();
                GroupList.val("");
                GroupListName.val("");
            } else {
                $(".emptyCls",resultCls).hide();
                var newv = "";
                var newv_name = "";
                for (var i = 0; i < clsItems.length; i++) {
                    newv += $(clsItems[i]).data("id") + ",";
                    var snvame = $(clsItems[i]).text();
                    snvame = snvame.replace(/\,/g, " ");
					if(single){
					    newv = $(clsItems[i]).data("id");
						newv_name = snvame;
						break;
					}
                    newv_name += snvame + ",";
                }
                GroupList.val(newv);
                GroupListName.val(newv_name);
				if(single)
				{
					searchCls.val(newv_name).css("color", "#000000");;
				}
            }
            if ($("li",trash).length == 0) {
                $(".emptyCls",trash).show();
            } else {
                $(".emptyCls",trash).hide();
            }
        }

        function ClsItemClick() {
            var pThis = $(this);
            if (pThis.parent().get(0) == resultCls.get(0)) {
                pThis.addClass("ui-state-disabled");
                pThis.appendTo(trash);
            } else {
                pThis.removeClass("ui-state-disabled");
                pThis.appendTo(resultCls);
            }
            ShowCls();
        }

        function AddClsItem(tid, tvalue) {
            if ($("li[data-id='" + tid + "']", resultCls).length == 0) {
                $('<li class="ui-widget-content" data-id="' + tid + '"><span class="ui-icon ui-icon-pencil"></span>' + tvalue + '</li>')
                    .click(ClsItemClick)
                    .appendTo(resultCls)
                    .hover(function () {
                        var icon = $("span", this);

                        $(this).addClass("ui-state-highlight");

                        icon.removeClass("ui-icon-pencil");
                        icon.addClass("ui-icon-circle-close");
                    }, function () {
                        var icon = $("span", this);

                        $(this).removeClass("ui-state-highlight");

                        icon.addClass("ui-icon-pencil");
                        icon.removeClass("ui-icon-circle-close");
                    });
                ShowCls();
            }
        }

		setTimeout(function(){
            var szDefGrpLst = GroupList.val();
            if (szDefGrpLst) {
                var arrayGrp = szDefGrpLst.split(",");
                var arrayGrpName = GroupListName.val().split(",");
                for (var i = 0; i < arrayGrp.length && i < arrayGrpName.length; i++) {
                    if (arrayGrp[i] && arrayGrpName[i])
                    {
                        AddClsItem(arrayGrp[i], arrayGrpName[i]);
                    }
                }
            }
        },1);
        
        searchCls.focus(function () {
			if($(this).val() == searchTip)
			{
				$(this).val("").css("color", "#000000");
			}
			$(this).autocomplete("search");
        }).blur(function () {
			if(single && GroupListName.val()!="")
			{
				$(this).val(GroupListName.val()).css("color", "#000000");
			}else{
				$(this).val(searchTip).css("color", "#aaaaaa");
			}
			if(oldGroupList != GroupList.val())
			{
			    GroupList.change();
			    oldGroupList = GroupList.val();
			}
        }).val(searchTip).css("color", "#aaaaaa").autocomplete({
            source: function(prequest,response){
                if(depend)
                {
                    var arrdepend = depend.split(",");
                    for(var i = 0; i < arrdepend.length; i++)
                    {
                        if(typeof(arrdepend[i])=="string" && arrdepend[i].length > 0)
                        {
                            prequest[arrdepend[i]] = $("*[name='"+arrdepend[i]+"']").val();
                        }
                    }
                }
                $.ajax({
                    url:datasource,
                    dataType: "json",
                    data:prequest,
                    success: function(data){
                        response(data);
                    }
                });
            },
            minLength: 0,
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
						if(single)
						{
							resultCls.empty();
						}
                        AddClsItem(ui.item.id, ui.item.label);
                    }
                }
                $(this).val("");
                $(this).blur();
                return false;
            },
            response: function (event, ui) {
				if(single)
				{
				    if(GroupListName.val() != searchCls.val())
				    {
					    resultCls.empty();
					    ShowCls();
					}
				}
                if (ui.content.length == 0) {
                    ui.content.push({ label: " 未找到配置项 " });
                }else{
                    var findGroup = ","+GroupList.val()+",";
                    if(findGroup != ",,")
                    {
                        var newcontent = [];
                        $.each( ui.content,function(key, val){
                            if(val && findGroup.indexOf(","+val.id+",") < 0)
                            {
                                newcontent.push(val);
                            }
                        });
                        ui.content.splice(0,ui.content.length);
                        $.each( newcontent,function(key, val){
                            ui.content.push(val);
                        });
                    }
                    if (ui.content.length == 0) {
                        ui.content.push({ label: " 未找到配置项 " });
                    }
                }
            }
        });
		
		
    });
};
