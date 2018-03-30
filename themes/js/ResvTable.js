function InitResvTable(resvMode,totalwidth,devcolwidth,rowheight,colcount,szClientID,AutoScroll,ScrollSpeed,CanResv,MyVPath,szSecAreaMap,ServerCtrls,SetDate)
{
    //if (IsOldIE6Version()) {
    //    MessageBox("您的浏览器版本过低，请升级到最新版本。");
    //    return;
    //}

	var rcolwidth = parseInt((totalwidth - devcolwidth) /colcount);
	var weekday = 7;
	var bTblHover_C = false;
	var colwidth = rcolwidth - 1;

	var t_C;
	var objPdevtbl = $("#pdevtbl_"+szClientID);
	if($( document ).tooltip)
	{
		$( document ).tooltip();
	}
	
	function sortNumber(a,b){return parseFloat(a) - parseFloat(b)}

	function DisableDevs(nroom)
	{
		var otherDevs = $("tbody tr",objPdevtbl);
		otherDevs.each(function(index){
			if($(".bgWeek",this).data("id") != nroom)
			{
				$(this).addClass("disabled");
			}
		});
	}
	function EnableDevs()
	{
	    var otherDevs = $("tbody tr.disabled", objPdevtbl);
	    otherDevs.removeClass("disabled");
    }
	
	var resvBtn = $("<div class='resvBtn'><span>我要预约</span></div>");
	resvBtn.hide().appendTo(objPdevtbl);
	resvBtn.children().click(function(){
		ServerCtrls.HF_Resv.val(resvSelect.getText());
		ResvHandle(resvSelect.getValue(), ServerCtrls.HF_Week.val());
		resvSelect.clear();
		EnableDevs();
		HideResvBtn();
	});
	function ShowResvBtn()
	{
		if(ResvHandle)
		{
			var first = resvSelect.getItem(0);
			var linknx = [];
			for(var i = 0; i < resvSelect.m_data.length; i++)
			{
				linknx.push(parseFloat(resvSelect.m_data[i].nx));
			}
			linknx.sort(sortNumber);
			sec = $(".hour .bgWeek div",objPdevtbl).eq(linknx[0]-1);
			endsec = linknx[linknx.length-1] - linknx[0] + 1;
			var dev = $(".bgWeek[data-id='" + first.ny + "']", objPdevtbl).parent().prev();
			var preDevWd = dev.outerWidth()+2;
			var preDevHh = $(".hour", objPdevtbl).position().top + $(".hour", objPdevtbl).outerHeight();
			
			var left = sec.position().left+preDevWd + (endsec * colwidth / 2) - (resvBtn.outerWidth() /2);
			resvBtn.show();
			resvBtn.animate({
				left:left,
				top:dev.position().top + rowheight,
				opacity: 1
			});
		}
	}
	function HideResvBtn()
	{
		resvBtn.css({
			left:0,
			opacity: 0
		});
		resvBtn.hide();
	}
	
	//==============选择块管理=================
	var resvSelect = {
		m_data: [],
		getLength: function()
		{
			return this.m_data.length;
		},
		getItem: function(index){return this.m_data[index]},
		alloc: function(nsec,nroom)
		{
			var gAreaBlock = $("<div>√</div>");
			gAreaBlock.addClass("AreaTip AreaSelectBlock").appendTo(objPdevtbl);

			var nx = nsec;
			var ny = nroom;
			var sec = $(".hour .bgWeek div",objPdevtbl).eq(nx-1);
			var dev = $(".bgWeek[data-id='" + ny + "']", objPdevtbl).parent().prev();

			var preDevWd = dev.outerWidth()+2;
			var preDevHh = $(".hour").position().top + $(".hour").outerHeight();

			gAreaBlock.data("nx",nx);
			gAreaBlock.data("ny",ny);
			gAreaBlock.nx = nx;
			gAreaBlock.ny = ny;
			gAreaBlock.css({
				left:sec.position().left+preDevWd,
				top:dev.position().top,
				height: rowheight,
				width: colwidth
			});
			var pThis = this;
		    gAreaBlock.hover(function () {
				$(this).addClass("RBhover");
				$(this).text("×");
		    }, function () {
				$(this).removeClass("RBhover");
				$(this).text("√");
		    }).click(function () {
				if(pThis.del(nx,ny))
				{
				    if(resvSelect.getLength()>0)
			        {
				        ShowResvBtn();
			        }else{
				        HideResvBtn();
			        }
					return "";
				}
			});
			gAreaBlock.show();
			return gAreaBlock;
		},
		pop: function()
		{
			var gAreaBlock = this.m_data.pop();
			gAreaBlock.hide();
			gAreaBlock.remove();
			return gAreaBlock;
		},
		clear: function()
		{
			while(this.getLength() > 0)
			{
				this.pop();
			}
		},
		delCheck:function(){
			var msg;
			while(true)
			{
				msg = this.check(true);
				if(msg && msg != "OK")
				{
					this.pop();
				}else{
					break;
				}
			}
			if(msg == "OK")
			{
			}
		},
		check:function(bDel)
		{
			if(this.m_data.length == 0)return null;
			if(!ResvRule)return null;

			var ret = "OK";
			var bFindSecLink = false;
			for(var r = 0; r < ResvRule.length; r++)
			{
				var Rule = ResvRule[r];
				var AData = $(this.m_data);
				if(Rule.dev != "*")
				{
					AData = AData.filter(function(index){ return (this.ny == Rule.dev);});
				}

				var mydevcount = 0;
				var mydevmap = [];
				var mynx = [];
				for(var i = 0; i < AData.length; i++)
				{
					mynx.push(parseFloat(AData[i].nx));
					if(mydevmap[AData[i].ny] != 1)
					{
						mydevcount++;
					}
					mydevmap[AData[i].ny] = 1;
				}
				
				if(!bFindSecLink && Rule.secLink && Rule.secLink.length > 0)
				{
					var szmynx = ","+mynx.join(",")+",";
					for(var i = 0; i < Rule.secLink.length; i++)
					{
					    var sl = Rule.secLink[i];
					    var bFind = false;
					    var bUnFind = false;
						
						for(var j = 0; j < sl.length; j++)
						{
							if(szmynx.indexOf(","+sl[j]+",")>=0)
							{
							    bFind = true;
							}else{
								bUnFind=true;
							}
						}
						var newbdata  = 0;
						if (bFind && bUnFind)
						{
							for(var j = 0; j < sl.length; j++)
							{
								if(bDel)
								{
									if(szmynx.indexOf(","+sl[j]+",") >= 0)
									{
										for(var k = 0; k < AData.length; k++)
										{
											for(var kk = 0; kk < this.m_data.length; kk++)
											{
												if(parseFloat(this.m_data[kk].nx) == parseFloat(sl[j]) && parseFloat(this.m_data[kk].ny) == parseFloat(AData[k].ny))
												{
													this.m_data[kk].hide();
													this.m_data[kk].remove();
													this.m_data.splice(kk,1);
												}
											}
										}
									}
								}else{
									if(szmynx.indexOf(","+sl[j]+",") < 0)
									{
										for(var k = 0; k < AData.length; k++)
										{
											this.m_data.push(this.alloc(sl[j],AData[k].ny));
											newbdata++;
										}
									}
								}
							}
							if(newbdata > 0)
							{
								var tmsg = this.check();
								if(tmsg && tmsg != "OK")
								{
									for(var k = 0; k < newbdata; k++)
									{
										this.pop();
									}
									return tmsg;
								}
							}
						}
						if(bFind) bFindSecLink = bFind;
					}
				}

				if(Rule.onlySecCount && AData.length > Rule.onlySecCount)
				{
					return "一次预约不能超过"+ Rule.onlySecCount +"节课";
				}

				if(Rule.onlySingle && mydevcount > 1)
				{
					return "一次只能选一台设备";
				}
				mynx.sort(sortNumber);

				if(Rule.continuous)
				{
					for(var i = 1; i < mynx.length; i++)
					{
						if(parseFloat(mynx[i]) - parseFloat(mynx[i-1]) > 1)
						{
							return "不能跨时间选择";
						}
					}
				}                        
				if(Rule.RetOK)
				{
					if(Rule.needSecCount && Rule.needSecCount > mynx.length)
					{
						ret = null;
					}
				}
			}
			return ret;
		},
		getIsOnlySingle:function(nroom)
		{
			if(!ResvRule)return null;
			for(var r = 0; r < ResvRule.length; r++)
			{
				var Rule = ResvRule[r];
				if(Rule.dev != "*" && Rule.dev != nroom)
				{
					continue;
				}
				if(Rule.onlySingle)
				{
					return true;
				}
			}
			return false;
		},
		getLinkSec:function(nsec,nroom)
		{
			ret = [];
			if(!ResvRule)return null;
			for(var r = 0; r < ResvRule.length; r++)
			{
				var Rule = ResvRule[r];
				if(Rule.dev != "*" && Rule.dev != nroom)
				{
					continue;
				}
				if(Rule.secLink && Rule.secLink.length > 0)
				{
					for(var i = 0; i < Rule.secLink.length; i++)
					{
						var sl = Rule.secLink[i];
						for(var j = 0; j < sl.length; j++)
						{
							if(sl[j] == nsec)
							{
								ret = sl.slice(0);
								return ret;
								break;
							}
						}
					}
				}
			}
			return ret;
		},
		del: function(nsec,nroom)
		{
			for(var i = 0; i < this.m_data.length; i++)
			{
				if(this.m_data[i].nx == nsec && this.m_data[i].ny == nroom)
				{
					this.m_data[i].hide();
					this.m_data[i].remove();
					this.m_data.splice(i, 1);
					this.delCheck();
					if(this.m_data.length==0)
					{
						HideResvBtn();
						EnableDevs();
					}
					return true;
				}
			}
			return false;
		},
		push:function(nsec,nroom)
		{
			if(this.del(nsec,nroom))
			{
				return "";
			}
			this.m_data.push(this.alloc(nsec,nroom));
			
			var msg = this.check();
			if(msg == "OK")
			{
			}else if(msg)
			{
				this.pop();
				//MessageBox(msg,null,1);
			}
		},
		getValue:function(){
			var val = [];
			for(var i = 0; i < this.m_data.length; i++)
			{
				val.push({sec:this.m_data[i].nx , dev:this.m_data[i].ny});
			}
			return val;
		},
		getText:function(){
			var val = [];
			for(var i = 0; i < this.m_data.length; i++)
			{
				val.push(this.m_data[i].nx +","+ this.m_data[i].ny);
			}
			val.sort();
			return val.join(";");
		},
		getDetailText:function(){
			if(this.m_data.length == 0)return null;
			if(this.getIsOnlySingle(this.m_data[0].ny))
			{
				var val = "";
				var dev = $(".bgWeek[data-id='" + this.m_data[0].ny + "']", objPdevtbl).parent().prev();
				var devname = dev.children().children().attr("title");
				if(!devname)
				{
					devname = dev.children().text();
				}
				val += devname+"： ";
				var anx = [];
				for(var i = 0; i < this.m_data.length; i++)
				{
					anx.push(parseFloat(this.m_data[i].nx));
				}
				anx.sort(sortNumber);
				for(var i = 0; i < anx.length; i++)
				{
					var sec = $(".hour .bgWeek div",objPdevtbl).eq(anx[i]-1);
					val += sec.text();
					if(i < this.m_data.length - 1)
					{
						val += "，";
					}
				}
				return val;
			}else{
				var val = [];
				for(var i = 0; i < this.m_data.length; i++)
				{
					var dev = $(".bgWeek[data-id='" + this.m_data[i].ny + "']", objPdevtbl).parent().prev();
					var devname = dev.attr("title");
					if(!devname)
					{
						devname = dev.children().attr("title");
					}
					if(!devname)
					{
						devname = dev.children().text();
					}					
					var sec = $(".hour .bgWeek div",objPdevtbl).eq(this.m_data[i].nx-1);
					val.push(devname + "：" + sec.text());
				}
				val.sort();
				return val.join("，");
			}
		}
	};

	//=============选择块管理==================

	objPdevtbl.mouseout(function(){
		bTblHover_C = false;
		HideAreaTip();
	});
	$("tbody .bgWeek",objPdevtbl).hover(function () {
		bTblHover_C = true;
	},function () {
		bTblHover_C = false;
	}).each(function(i){            
		$("<map name='IM_"+szClientID+"_"+i+"' id='IM_"+szClientID+"_"+i+"'>"+szSecAreaMap+"</map>").appendTo($(this));
		var bgImg = $("<img usemap='#IM_"+szClientID+"_"+i+"' src='"+MyVPath+"themes/img/none.png'/>");
		bgImg.addClass("bgWeekImg");
		bgImg.appendTo($(this));
	});

	function GoResv(nsec,nroom)
	{
		if(CanResv){
		    if(resvMode == 0)
		    {
			    ShowWait();
			    ServerCtrls.HF_Resv.val(nsec + ',' + nroom);
			    ServerCtrls.Button_Resv.click();
		    }else if(resvMode == 1)
		    {
		        var linknx = resvSelect.getLinkSec(nsec, nroom);
			    if(linknx == null || linknx.length == 0)
			    {
				    linknx.push(nsec);
			    }
			    if(!IsAllCanResv(nroom,linknx))
			    {
				    return false;
			    }
			    var msg = resvSelect.push(nsec,nroom);
			    if(resvSelect.getLength()>0)
			    {
				    ShowResvBtn();
			    }else{
				    HideResvBtn();
			    }
			    if(msg==null || msg == "OK")
			    {
				    if(resvSelect.getIsOnlySingle(nroom))
				    {
					    DisableDevs(nroom);
				    }
			    }
		    }
		}
	}

	var gAreaMask = $("<div/>");
	var gAreaTipX = $("<div/>");
	var gAreaTipY = $("<div/>");
	var gAreaTipT = $("<div id='gAreaTipT'>选择</div>");
	gAreaMask.addClass("AreaTip").animate({ opacity: 0.50 }, 0).appendTo(objPdevtbl);
	gAreaTipX.addClass("AreaTip").animate({ opacity: 0.08 },0).appendTo(objPdevtbl);
	gAreaTipY.addClass("AreaTip").animate({ opacity: 0.08 },0).appendTo(objPdevtbl);
	gAreaTipT.addClass("AreaTip").click(function(){
		GoResv($(this).data("nx"),$(this).data("ny"));
	}).appendTo(objPdevtbl);

	gAreaMask.css({
	    left: 0,
	    top: 0,
	    width: 3 * colwidth,
	    height: objPdevtbl.height()
	});

	var t_HideAreaTip = null;
	function HideAreaTip(t)
	{
		if(!t)t = 200;
		if(t_HideAreaTip)clearTimeout(t_HideAreaTip);
		if(t == 0)
		{
			t_HideAreaTip = null;
			gAreaTipX.hide();
			gAreaTipY.hide();
			gAreaTipT.hide();
		}else{
			t_HideAreaTip = setTimeout(function(){
				t_HideAreaTip = null;
				gAreaTipX.hide();
				gAreaTipY.hide();
				gAreaTipT.hide();
			},t);
		}
	}

	gAreaTipT.mouseout(HideAreaTip);

	$(".AreaTip").mouseover(function(){
		bTblHover_C = true;
		if($(this).attr("id") != "gAreaTipT")
		{
			HideAreaTip(0);
		}else{
			if(t_HideAreaTip)clearTimeout(t_HideAreaTip);
			gAreaTipT.show();
			gAreaTipX.show();
			gAreaTipY.show(); 
		}
	});

	function IsAllCanResv(nroom,linknx)
	{
		var ret = true;
		$(".Resv",objPdevtbl).each(function(){
			if($(this).data("ny") == nroom+"")
			{
				for(var i = 0; i < linknx.length; i++)
				{
					if(parseInt($(this).data("nxs")) <= linknx[i]-1 && linknx[i] <= parseInt($(this).data("nxe")))
					{
						ret = false;
						return;
					}
				}
			}
		});
		return ret;
	}

	$("area",objPdevtbl).click(function(){
		if($(this).parents("tr.disabled").length>0)return;
		var nsec = $(this).data("id");
		var nroom = $(this).parent().parent().data("id");
		GoResv(nsec,nroom);
		return false;
	}).hover(function(){
	    if(!CanResv){return}

	    if ($(this).parents("tr.disabled").length > 0) return;

		bTblHover_C = true;
		if(t_HideAreaTip)clearTimeout(t_HideAreaTip);
		var nx = $(this).data("id");
		var ny = $(this).parent().parent().data("id");
		var sec = $(".hour .bgWeek div",objPdevtbl).eq(nx-1);
		var dev = $(this).parent().parent().parent().prev();

		var endsec = 1;
		if(resvMode == 1)
		{
			gAreaTipT.text("选择");
			gAreaTipT.removeClass("SpTipT");
			gAreaTipT.attr("title","");
			var linknx = resvSelect.getLinkSec(nx,ny);
			if(linknx!= null && linknx.length > 0)
			{
				linknx.sort(sortNumber);
				sec = $(".hour .bgWeek div",objPdevtbl).eq(linknx[0]-1);
				endsec = linknx[linknx.length-1] - linknx[0] + 1;
			}else{
				linknx.push(nx);
			}
			if(!IsAllCanResv(ny,linknx))
			{
				gAreaTipT.text("已被占用");
				gAreaTipT.addClass("SpTipT");
			}else{
				var len = resvSelect.getLength();
				
				resvSelect.m_data.push(resvSelect.alloc(nx,ny));
				var msg = resvSelect.check();
				if(msg && msg != "OK")
				{
					gAreaTipT.text("不可选");
					gAreaTipT.addClass("SpTipT");
					gAreaTipT.attr("title",msg);
				}
				while(resvSelect.getLength() > len)
				{
					resvSelect.pop();
				}
			}
		}

		var preDevWd = dev.outerWidth()+2;
		var preDevHh = $(".hour", objPdevtbl).position().top + $(".hour", objPdevtbl).outerHeight();

		gAreaTipT.data("nx",nx);
		gAreaTipT.data("ny",ny);
		gAreaTipT.css({
			left:sec.position().left+preDevWd,
			top:dev.position().top,
			height: rowheight,
			width: endsec * colwidth
		});
		gAreaTipT.show();

		gAreaTipX.css({
			left:sec.position().left+preDevWd,
			top: preDevHh,
			width: endsec * colwidth,
			height:dev.position().top - preDevHh
		});
		gAreaTipY.css({
			left: preDevWd,
			width:sec.position().left,
			height: rowheight,
			top:dev.position().top
		});
		gAreaTipX.show();
		gAreaTipY.show();
	},function(){});

	$("td:first-child,th:first-child",objPdevtbl).css("width", (totalwidth - rcolwidth * colcount) + "px");
	$(".DevName",objPdevtbl).css("width", (totalwidth - rcolwidth * colcount) + "px");

	$(".bgWeek",objPdevtbl).css("width", (rcolwidth * colcount) + "px");
	$(".bgWeek div",objPdevtbl).css("width", (colwidth) + "px");
	$(".week .bgWeek div",objPdevtbl).css("width", (((totalwidth - devcolwidth) / weekday) - 1) + "px").click(function ()
	{
		ServerCtrls.HF_WinScroll.val($(window).scrollTop());
		ServerCtrls.HF_Week.val($(this).data("d"));
		if(resvMode == 0)
		{
			ServerCtrls.Button_Week.click();
		}else if(resvMode == 1)
		{
			if(ChangeWeek)
			{
				ChangeWeek($(this).data("d"));
			}
		}
	});
	var prewtop = ServerCtrls.HF_WinScroll.val();
	ServerCtrls.HF_WinScroll.val("");
	if(prewtop != null && prewtop != "")
	{
		$(window).scrollTop(prewtop);
	}

	$(".bgWeek .Resv",objPdevtbl).css({
		marginLeft: function () {
			 return ($(this).data("p") * rcolwidth) + "px";
		},
		width: function () {
			if($(this).data("w") == "0")
			{
				return "0px";
			}
			return ($(this).data("w") * rcolwidth)-1 + "px";
		},
		fontSize: function () {
			var textWidth = $(this).text().length;
			var barWidth = ($(this).data("w") * rcolwidth);
			var fontSize = (barWidth/textWidth);
			if(fontSize > 14)fontSize = 14;
			if(fontSize < 11)fontSize = 11;
			return fontSize + "px";
		}
	}).attr("title",function(){
		return $(this).text()
	}).click(function(){
		return false;
	});

	$(".bgWeek",objPdevtbl).css("width", (rcolwidth * colcount) + "px");

	if(AutoScroll){
		function devtbl_show() {
			if(bTblHover_C)
			{
				return;
			}
			var tra = $("tbody tr:first",objPdevtbl);
			tra.appendTo($("tbody",objPdevtbl));
		}

		if(t_C)clearInterval(t_C);
		t_C = setInterval(devtbl_show, ScrollSpeed);
	}


	var startdate = $("#m_dwStartDate");
	if(startdate.length > 0)
	{
		startdate.datepicker({
			showOn: "button",
			buttonImage: MyVPath+"themes/img/calender.png",
			buttonImageOnly: true,
			dateFormat: "yy-mm-dd",
			onSelect: function(dateText,inst){
				ServerCtrls.HF_WinScroll.val($(window).scrollTop());
				ServerCtrls.HF_StartDate.val(dateText);
				if(resvMode == 0)
				{
					ServerCtrls.Button_Week.click();
				}
				else if(resvMode == 1)
				{
					if(ChangeStartDate)
					{
						ChangeStartDate(dateText);
					}
				}
			}
		}).val(SetDate);
		startdate.attr("readonly","readonly");
	}
}