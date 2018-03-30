$(function(){

	if(getUrlParam('date') == null){
		var date = new Date();
		date.setDate(date.getDate()+1); 
		var y = date.getFullYear();
		var m = (date.getMonth()+1);
		var d = date.getDate();

		$('input[name=date]').val(y+'-'+(m<10?'0':'')+m+'-'+(d<10?'0':'')+d);
	}

    // salon_last
    $(".opt .btn_detail").click(function(){
        $(this).toggleClass("close");
        $(this).parent().prev().toggleClass("open");
        return false;
    })

    // tab
		$('#login').tabs();
    $('#space_tabs').tabs();
      $('#space_tabs2').tabs();
    $('#overview_tabs').tabs();
    // news
    $(".space_news th span").hover(function(){
        $(this).toggleClass("hover");
    });
    $(".space_news th span").click(function(){
        $(this).parent().parent().next().toggle();
    })

    // gallery
    $(".img_thumb a").click(function(){
        var thumbimgurl = $(this).children().attr('src');
        var largeimagenurl = thumbimgurl.replace("","");
        $(".img_large img").attr('src',largeimagenurl);
        $(".img_thumb a").each(function(){
            if ($(this).hasClass('cur')) {
                $(this).removeClass('cur');
            };
        })
        $(this).addClass('cur');
        return false;
    })

		$("div.opt a.goRes").click(function(){
			if(!IsLogin()){
				$("#logindialog").dialog('open');
				return false;
			}
		});

		$('form select.hour').change(function(){
			//alert($(this).find(":selected"));
		});

		$("select.orderstatusSelect").change(function(){
			$("table.orderstatus").hide();
			$("table.orderstatus[DevId="+this.value+"]").show();
		});

    //GetRoomRes();
});

function ActionResBut(){
	if(IsLogin(false)){
		return true;
	}else{
		$("#logindialog").dialog('open');
		return false ;
	}
}


function GetRoomRes(){
	$.ajax({
				type:"GET",
					url:"/Ajax_Code/reserve.php?act=room",
					dataType:"json",
					success:function(object){
						if(object.MsgId>0){
							alert(object.Message);
							return ;
						}

						$.each($("table.orderstatus[roomid]"),function(index,value){
							$(value).empty();
							$(value).append(object[$(value).attr('roomid')].html);
						});
						$('select.orderstatusSelect[Kind]').empty();
						var type = $('select.orderstatusSelect[Kind]').attr('Kind');
						$.each($("table.orderstatus[Kind="+type+"]"),function(index,value){
							$(value).empty();
							var select = object[type][$(value).attr('DevId')];
							var Id = select.dwDevID!=undefined?select.dwDevID:select.dwKindID;
							var name = select.Name!=undefined?select.Name:select.szKindName;
							switch(name){
								case "多媒体空间A":name = "Windows系统";break;
								case "多媒体空间B":name = "Mac系统";break;
							}
							$('select.orderstatusSelect[Kind]').append("<option value=\""+Id+"\">"+name+"</option>");
							$(value).append(select.html);
						});
						$('select.orderstatusSelect[Kind]').change();

/*
						$.each($("table.orderstatus[DevId]"),function(index,value){
							$(value).empty();
							$(value).append(object.DevIds[$(value).attr('DevId')].html);
						});
*/

						$("table td span.yes a").click(function(){
							var id =  $(this).attr("rid");
							if(id>0){
								var url = rooms[id].page+"?id="+ id +'#'+ rooms[id].tag;
								location = url;
								return false;
							}
						});
						$('a.goRes').click(function(){
							if(!IsLogin())
								return false;
						});
						$('span.yes a').click(function(){
							if(!IsLogin()){
								$("#logindialog").dialog('open');
								return false;
							}
						});
						$('a.no').click(function(){
									return false;
						});
					}
			});
}


function getUrlParam(name){
	var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
	var r = window.location.search.substr(1).match(reg);  //匹配目标参数
	if (r!=null) return unescape(r[2]); return null; //返回参数值
} 

function check_email(str)	{
	var regex=/[_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+$/;
	return regex.test(str);
}

function CheckMobile(mobile)
{
    return mobile.length == 11;
}

function IsLogin(){
	if($("#nav ul.unlogin").is(":visible")){
		return false;
	}else{
		return true;
	}
}

function LoadTimeSpan(date,DId){
	$.ajax({
			type:"GET",
				url:"/app/action.reserve.php?act=timespan&date="+date+"&did="+DId,
				dataType:"json",
				success:function(object){
				$("select[name=time]").empty();
				for(var row in object.Rows){
					$("select[name=time]").append(object.Rows[row].html);
				}
			}
		});
}

function pad(num, n) {
    var len = num.toString().length;
    while(len < n) {
        num = '0' + num;
        len++;
    }
    return num;
}



function myload(szHref) 
{
    if(!IsLogin())    
    {
        HrefPage = szHref;
     $("#logindialog").dialog('open');	
     return false;			
	}else{
	    return true;
	}
}	