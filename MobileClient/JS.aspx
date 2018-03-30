<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JS.aspx.cs" Inherits="_Default" %>
document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
	WeixinJSBridge.call('hideOptionMenu');
	WeixinJSBridge.call('hideToolbar');
});
window.history.forward(1);

var szClickName = "click";

function OnContentLoad()
{
    $(".tblEContent tr").forEach(function(item, index, array){
	    if(!$(item).hasClass("btnline"))
	    {
	        if(index % 2 == 0)
	        {
	            $(item).addClass("tr-odd-bg");
	            $(item).removeClass("tr-even-bg");
	        }else{
	            $(item).addClass("tr-even-bg");
	            $(item).removeClass("tr-odd-bg");
	        }
	    }
	});
if($(".msg").text().trim()!=""){
alert($(".msg").text());
$(".msg").text("");
}
 

	$(".navback").on(szClickName,function(e){
	    e.preventDefault();
	    OnBack();
	});
}

var g_backitem = [];

function OnBack()
{
     var item1=g_backitem.pop();
    var item = g_backitem.pop();
    if(item)
    {
        LoadContentByAjax("DContent",item);
    }
}

function LoadContentByAjax(szParentID , item , data)
{
    var bIsAdd=false;
    var vTemp=g_backitem.pop();
 g_backitem.push(vTemp);
  if(vTemp==null)
{
bIsAdd=true;
}
else
{
var vTempUrl=vTemp.url.substring(0,vTemp.url.indexOf('?'));
var vItemUrl=item.url.substring(0,item.url.indexOf('?'));
if(vTempUrl!=vItemUrl)
{
bIsAdd=true;
}
}
    if(bIsAdd==true)
{
    g_backitem.push(item);
}

    $(".Div").empty();
    
    if($("#"+item.id).length > 0)
    {
        $("#"+item.id).remove();
    }
    
    $.ajax({type:'post',url:item.url,data:data,success:function(data){
	    $("#"+szParentID).append(data);
        $("#"+item.id).show();
        OnContentLoad();
        if(item.onload)
        {
            if( typeof(item.onload) == "function")
            {
                item.onload();
            }else if(typeof(item.onload) == "string"){
                eval(item.onload);
            }
        }
    }});
}

function LoadContentByAjax2(id , url ,onload , data)
{    
    var item = {id: id, url: url , onload: onload};
    LoadContentByAjax("DContent", item , data);
}

function ShowMsg(msg)
{
    LoadContentByAjax2("msgDiv","subMsg.aspx?msg="+escape(msg));
}

$(function(){
    var items = $("#menu>a");
    
    items.on(szClickName,function(e){
        e.preventDefault();
        
        var pThis = $(this);
        $("#menu>a").removeClass("current");
        pThis.addClass("current");
        
        var item = {id: pThis.attr("tid"), url: pThis.attr("href") , onload: pThis.attr("onload")};
        LoadContentByAjax("DContent", item);
    });
    items.last().addClass("itemlast");
    items.first().addClass("itemfirst").trigger(szClickName);
    
	OnContentLoad();
});

function DrawBar(canvasID,start, end ,data , fnOnClickBar)
{
    var padding = 2;  //边框留白    
    var maxwidth = 1000;  //总宽
    var enableStatColor = "#3c3"; //可预约状态颜色
    var disableStatColor = "#eee"; //不可预约状态颜色
    var textColor = "#333"; //文字颜色
    var focusColor = "#f00"; //活动颜色
    
    var cx = 1;
    var cy = 25;
    var height = 30;
    var maxcount = end-start+1;
    cx = parseInt(maxwidth / maxcount);
    var width = maxcount * cx;
    cy = parseInt(cx*0.5);
    height = parseInt(cy+cx+cx/1.9);
    //var canvas =  document.getElementById(canvasID);
    canvasID.width = width + padding*2;
    canvasID.height = height;
    var context = canvasID.getContext('2d');
    
    context.translate(padding,padding);
    
    context.beginPath();
    context.rect( 0, 0 , width, cy);
    context.fillStyle = enableStatColor;
    context.fill();
    
    context.lineWidth = 1;
    context.textAlign = 'center';
    context.textBaseline = 'middle';
    
    for(var i = 0; data!=null&&i < data.length; i++)
    {
        if(data[i] > 0)
        {
            var vTemp=parseInt(data[i]);
            var vStart=parseInt(vTemp/10000);
            var vEnd=parseInt(vTemp%10000);
            var vTempWidth=(vEnd-vStart)/100*cx;

            var x = (vStart/100-start) * cx;
            
            context.fillStyle = disableStatColor;
            context.beginPath();
            context.rect( x, 0 , vTempWidth, cy);
            context.fill();
        }
    }
    
    var fontsize = parseInt(cx/2.4);
    context.font = fontsize+"pt Arial";
    context.fillStyle = textColor;
    
    var ti = -9999;
    for(var i = start; i <= end; i=i+2)
    {
        var x = (i-start) * cx;
        
        if( (x - ti) > 20)
        {
            ti = x;
            context.fillText(i.toString(), x + fontsize, cy + fontsize);
            context.fillText("点", x+fontsize, cy + fontsize * 2.8);
        }
    }
 
    context.translate(0,0);

    if(fnOnClickBar)
    {
        $(canvasID).on(szClickName,function(e){        
            e.preventDefault();
            
            var selectValue = 0;
            var mw = this.offsetWidth;
            var fx = mw / maxwidth;
            if(e.iniTouch)
            {
                selectValue = e.iniTouch.x - this.offsetLeft;
            }else if(e.changedTouches && e.changedTouches.length){
                var touch = e.changedTouches[0];
                if(touch)
                {
                    selectValue = touch.pageX - this.offsetLeft;
                }else{
                    selectValue = e.pageX - this.offsetLeft;
                }
            }else{
                selectValue = e.pageX - this.offsetLeft;
            }
            
            selectValue = parseInt(selectValue / (cx*fx));
            
            selectValue+=start;
            
            for(var i = 0; data!=null&&i < data.length; i++)
            {
                if(data[i] == selectValue)
                {
                    return;
                }
            }
            if(selectValue < start || selectValue > end)
            {
                return;
            }

//处理点击不跳转时间，因为选择时间已经被预约
            for(var i = 0; data!=null&&i < data.length; i++)
            {
var vTempselectValue=selectValue*100;
                var vTemp=parseInt(data[i]);
                var vStart=parseInt(vTemp/10000);
                var vEnd=parseInt(vTemp%10000);
                if(vTempselectValue>=vStart&&vTempselectValue<vEnd)
                {
//alert(vTempselectValue);
                    return;
                }
            }

            var x = (selectValue-start) * cx;
            
            context.fillStyle = focusColor;
            context.beginPath();
            context.rect( x, 0 , cx, cy);
            context.fill();
            
            fnOnClickBar(selectValue , this);
            
            context.fillStyle = enableStatColor;
            context.beginPath();
            context.rect( x, 0 , cx, cy);
            context.fill();
            return false;
        });
    }
}
