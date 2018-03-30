var OldTabReload = null;
$.fn.UniTab = function (options) {
    var pAll = $(this);
    this.fdata = "";
	$(this).each(function () {
	    var pThis = $(this);
		var pForm = $("form");
		var ThisName = pThis.attr("id"); if (ThisName == null || ThisName == "") { ThisName = 0 };
		var curTab = pForm.data("UniTab_" + ThisName);
	    var pInited = pForm.data("UniTabInited_" + ThisName);
	    pForm.data("UniTabInited_" + ThisName,"true");

		var defaults = {
		};
		var opts = $.extend(defaults, options);

		pThis.addClass("UniTab");

		
		var Items = $("a", pThis);
		Items.addClass("UniTabItem ui-state-default ui-corner-top")
		.one("click",function () {
		    var fdata = $(this).data("fparam");
		    if (!fdata) fdata = "";
		    pForm.data("UniTab_" + ThisName, $(this).attr("href"));
		    $.ajax({
		        url: $(this).attr("href"),
		        type: "POST",
		        timeout: 600000,
		        data: fdata,
				async: true,
				dataType: "html",
				success: function (data, status) {
				    pForm.empty()				    
					var pData = $("<div>" + data + "</div>").appendTo(pForm);
					if(OnTabLoad)
					{
						OnTabLoad(null,{panel:pData});
					}
				},
				error: function (data, status, error) {
					MessageBox("连接失败", "", 2);
				}
			});
			return false;
		});

		if (curTab == null || curTab == "") {
			Items.first().addClass("ui-tabs-active ui-state-active");
		} else {
		    Items.each(function (i) {
		        if ($(this).attr("href") == curTab) {
		            $(this).addClass("ui-tabs-active ui-state-active");
		        }
		    });
		}

		if (OldTabReload == null) {
		    OldTabReload = TabReload;
		}
		//alert('t');
		//if (!OldTabReload)
		{		   
		    TabReload = function (fdata) {		        
		        if (fdata == "") {
		            return
		        }
		        
				var url = pForm.data("UniTab_" + ThisName);				
				if (url != null && url + "" != "undefined") {
					$.ajax({
						url: url,
						data: fdata,
						type: "POST",
						timeout: 600000,
						async: true,
						dataType: "html",
						success: function (data, status) {
							pForm.empty()
							var pData = $("<div>" + data + "</div>").appendTo(pForm);
							if (OnTabLoad) {
								OnTabLoad(null, { panel: pData });
							}
						},
						error: function (data, status, error) {
							MessageBox("连接失败", "", 2);
						}
					});
				} else {
					if (OldTabReload != null) {
						OldTabReload(fdata);
					}
				}
			}
		}
	});
};
