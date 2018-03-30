// JScript 文件
function GetThemesPath() {
    var jsNodes = document.getElementsByTagName('script');
    for (var i = 0; i < jsNodes.length; i++) {
        var pathArray = jsNodes[i].src.split("/");
        if (pathArray[pathArray.length - 1].toLowerCase() == "jscript.js") {
            pathArray.pop();
            pathArray.pop();
            pathArray.push("");
            var ret = pathArray.join("/");
            return ret;
        }
    }
}

function resizeMax() {
    try {
        window.moveTo(0, 0);
        window.resizeTo(screen.availWidth, screen.availHeight);
        window.outerWidth = screen.availWidth;
        window.outerHeight = screen.availHeight;
    } catch (e) { }

}

$(function () {
    //resizeMax();
    //$('#menu').menu({ position: { my: "left top", at: "left bottom" } });
	$(document).tooltip();
	$("form").validationEngine('attach');
	$.datepicker.setDefaults({
	    dateFormat: "yy-mm-dd"
	});
	
	var quickBtn = $("#QuickBtn");
	if(quickBtn.length > 0)
    {
	    quickBtn.UIAPanel({
	        theme: "box2.png", borderWidth: 10, minWidth: "280", maxWidth: "320", minHeight: "45", maxHeight: "220", speed: 200
	    });
	}
	$(".MainBody").on("click", "input", function () {	    
	    var startLine = $("input[name='_dwStartLine']");	
	    if (startLine != null)
	    {
	        startLine.val(1);
	    }
	});
	$(".MainBody").on("click", "select", function () {	    
	    var startLine = $("input[name='_dwStartLine']");	   
	    if (startLine != null) {
	        startLine.val(1);
	    }
	});
});

function ReloadPage(pThis)
{
    var objForm;
    if (pThis) {
        objForm = $(pThis).parents("form");
    } else {
        objForm = $("form");
    }
    if (objForm.length > 0) {
        objForm.submit();
    } else {
        location.reload();
    }

    return false;
}

function Dlg_Callback(_param){
    if(_param == "cancel")
    {
        this.close();
    }
    else if(_param == "ok")
    {
        this.close();
        ReloadPage();
    }
}
        
function Dlg_Cancel()
{
    var pThis = $.lhdialog.focus;
    if (frameElement && frameElement.api) {
        pThis = frameElement.api;
    }
    if (pThis && pThis.close) {
        pThis.close();
    }
    
    //if(frameElement && frameElement.api && frameElement.api.data)
    //{
    //    frameElement.api.data("cancel");
    //}
}

function Dlg_OK()
{
    var pThis = $.lhdialog.focus;
    if (frameElement && frameElement.api) {
        pThis = frameElement.api;
    }
    if (pThis && pThis.opener && pThis.opener.ReloadPage) {
        pThis.opener.ReloadPage();
    }
    if (pThis && pThis.close) {
        pThis.close();
    }

    //if(frameElement && frameElement.api && frameElement.api.data)
    //{
    //    frameElement.api.data("ok");
    //}
}


function MessageBox(msg ,title,type,fnAction)
{
    var icon = null;
    if(type == 0)
    {
        icon = "tips.gif";
    }else if(type == 1)
    {
        icon = "alert.gif";
    }else if(type == 2)
    {
        icon = "error.gif";
    }else if(type == 3)
    {
        icon = "success.gif";
    } else {
        icon = "alert.gif";
    }
    if (!title) {
        title = msg;//"提示?";
    }

	var dialog = null;
	var dlgClose = null;
	var fnAction2 = function(){
		if(dlgClose)
		{
			dlgClose();
			dlgClose = null;
		}
		if(fnAction)
		{
		    setTimeout(fnAction,100);
		}
	};
	var fnInit = function()
	{
		if(dialog)
		{
			dlgClose = dialog.close;
			dialog.close = fnAction2;
		}
	}

    if($.lhdialog)
    {
        dialog = $.lhdialog({title: title,content: msg,icon: icon,
            lock: true,zIndex:2000,
            okVal: "确定",
            ok: fnAction2,
		    init: fnInit,
		    close: fnAction2
        });
    }else{
        alert(msg);
        if(fnAction)fnAction();
    }
}

function DoDel(fnAction)
{
    ConfirmBox("请确定要删除吗？",fnAction);
}

function DoSet(fnAction)
{
    ConfirmBox("请确定要修改吗？",fnAction);
}
function ConfirmBox(msg,fnAction,title,type,fnCancelAction)
{
    if(!fnAction)
    {
        return;
    }
    var icon = null;
    if(type == 0)
    {
        icon = "tips.gif";
    }else if(type == 1)
    {
        icon = "alert.gif";
    }else if(type == 2)
    {
        icon = "error.gif";
    }else{
        icon = "prompt.gif";
    }
    if(!title)
    {
        title = msg;//"提示?";
    }
    if(!fnCancelAction)
    {
        fnCancelAction = function(){};
    }
    
    var dialog = $.lhdialog({title: title,content: msg,icon: icon,
        lock: true,
        okVal: "确定",
        ok: fnAction,
        cancelVal: "取消",
        cancel: fnCancelAction
    });
}
function ConfirmBox2(msg, fnAction, title, type, fnCancelAction) {
    if (!fnAction) {
        return;
    }
    var icon = null;
    if (type == 0) {
        icon = "tips.gif";
    } else if (type == 1) {
        icon = "alert.gif";
    } else if (type == 2) {
        icon = "error.gif";
    } else {
        icon = "prompt.gif";
    }
    if (!title) {
        title = msg;//"提示?";
    }
    if (!fnCancelAction) {
        fnCancelAction = function () { };
    }

    var dialog = $.lhdialog({
        title: title, content: msg, icon: icon,
        lock: true,
        okVal: "确定",
        ok: fnAction      
    });
}
function ShowWait()
{
    if(!$(document).find("#waitDialog").html())
    {
        $('<div id="waitDialog"><img src="' + GetThemesPath() + 'img/wait.gif" /><span>加载中,请稍候...</span></div>').appendTo("body");
        /*$("#waitDialog").dialog({
            width: 100, autoOpen: false, overlay: {
                backgroundColor: '#000',
                opacity: 0.5
            }, modal: false, minHeight: 32
        });*/
    }
    $("#waitDialog").progressbar({
        value: false
    })
    $("#waitDialog").show();
    //$("#waitDialog").dialog('open');
}

function HideWait()
{
    //$("#waitDialog").dialog('close');
    $("#waitDialog").hide();
}

var g_bBusy = false;
function OnBusy()
{
    if(g_bBusy)
    {
        return false;
    }else{
        g_bBusy = true;
        ShowWait();
        return true;
    }
}
function OnUnBusy()
{
    g_bBusy = false;
    HideWait();
}


//使用Ajax动态无刷新更新页面
function AjaxUpdate(contentID,_otherdata,_ajaxUrl,callback)
{
    var fs = $("form").serialize();
    var szAjax = location.href;
    if(_ajaxUrl)
    {
        szAjax = _ajaxUrl;
    }
    fs += "&_AjaxID="+contentID;
    if(_otherdata)
    {
        fs += "&"+_otherdata;
    }
    if(!OnBusy())
    {
        return;
    }

    $.ajax({
	    url: szAjax,
	    type:"POST",
	    timeout:600000,
	    data:fs,
	    async:true,
	    dataType:"html",
	    success:function(data,status)
	    {
	        ReplaceContent2(data,contentID);
	        OnUnBusy();
	        if(callback)
	        {
	            callback();
	        }
	    },
	    error:function(data, status, error)
	    {
	        OnUnBusy();
		    MessageBox("连接【"+szAjax+"】失败","",2);
	    }
    });	    
}
function ReplaceContent2(ajaxData,contentID)
{
    if(ajaxData == null)return;
    var content = $("#"+contentID);
    if(content)
    {
        content.html(ajaxData);
    }
}
function ReplaceContent(ajaxData,contentID)
{
    if(ajaxData == null)return;
    var ajaxdom = $(ajaxData);
    var content = $("#"+contentID,ajaxdom);
    if(content)
    {
        var content2 = $("#"+contentID);
        if(content2)
        {
            content2.html(content.html());
        }
    }
    ajaxdom.empty();
}
//-----------------------------
var escapeable = /["\\\x00-\x1f\x7f-\x9f]/g,
   meta = {
       '\b': '\\b',
       '\t': '\\t',
       '\n': '\\n',
       '\f': '\\f',
       '\r': '\\r',
       '"': '\\"',
       '\\': '\\\\'
   };

var replaceEscape = function (string) {
    if (string.match(escapeable)) {
        return '"' + string.replace(escapeable, function (a) {
            var c = meta[a];
            if (typeof c === 'string') {
                return c;
            }
            c = a.charCodeAt();
            return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
        }) + '"';
    }
    return string;
}
function PutHttpValue(thePreHttpValue,parentObj)
{
    if (!thePreHttpValue) return;

    for (var i = 0; i < thePreHttpValue.length; i++) {
        var item = thePreHttpValue[i];
        if (!item || item.length != 2) {
            continue;
        }
        if (item[0].substr(0,2) == "__") {
        //if (item[0] == "__VIEWSTATE" || item[0] == "__EVENTVALIDATION") {
            continue;
        }
        if (item[0].indexOf("$") >= 0) {
            item[0] = item[0].replace(/\$/g, "_");
        }

        var objItem = $("#" + item[0],parentObj);
        if (objItem.length == 0) {
            objItem = $("[name='" + item[0] + "']",parentObj);
            if (objItem.length == 0) {
                continue;
            }
        }
        
        objItem.each(function () {
            var pThis = $(this);
            var type = pThis.attr("type");
            var tagname = pThis.get(0).tagName.toUpperCase();
            
            if(!parentObj && pThis.parents(".NotHttpValue").length > 0)
            {
                return;
            }
            
            if(type == "checkbox")
            {
                if(item[1].indexOf(",")>=0 || !objItem.hasClass("enum"))
                {
                    var sv = ","+item[1]+",";
                    if (sv.indexOf("," + pThis.val() + ",") >= 0) {
                        pThis.attr("checked", true);
                    } else {
                        pThis.attr("checked", false);
                    }
                }else{
                    var nv = parseInt(item[1]);
                    if (parseInt(pThis.val()) & nv) {
                        pThis.attr("checked", true);
                    } else {
                        pThis.attr("checked", false);
                    }
                }
            }else if(type == "radio")
            {
                if (pThis.val() == item[1]) {
                    pThis[0].checked = true;
                    //pThis.attr("checked", true);
                } else {
                    pThis.attr("checked", false);
                }
            }else if(tagname == "INPUT")
            {
                var ivalue = item[1];
                if(objItem.hasClass("hasDatepicker"))
                {
                    ivalue = GetDateStr(ivalue);
                }
                else if (objItem.hasClass("TimePicker-time-input"))
                {
                    ivalue = GetTimeStr(ivalue);
                }
                ivalue = replaceEscape(ivalue);
                pThis.val(ivalue);
                if (pThis.val() != item[1]) {
                    pThis.attr("value", ivalue);
                }
            }else if(tagname == "SELECT"){
                pThis.val(item[1]);
            } else if(tagname == "DIV" || tagname == "SPAN"){
                pThis.text(item[1]);
            }
            else if (tagname == "TEXTAREA") {                
                var ivalue = item[1];              
                pThis.val(ivalue);
                if (pThis.val() != item[1]) {
                    pThis.attr("value", ivalue);
                }
            }

        });
        
        /*
        if (objItem.attr("type") == "checkbox") {
            if(item[1].indexOf(",")>=0 || !objItem.hasClass("enum"))
            {
                var sv = ","+item[1]+",";
                objItem.each(function () {
                    if (sv.indexOf("," + $(this).val() + ",") >= 0) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
            }else{
                var nv = parseInt(item[1]);
                objItem.each(function () {
                    if (parseInt($(this).val()) & nv) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
            }
        } else if (objItem.attr("type") == "radio") {
            objItem.each(function () {
                if ($(this).val() == item[1]) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
        } else if(objItem.get(0).tagName.toUpperCase() == "INPUT"){
            var ivalue = item[1];
            if(objItem.hasClass("hasDatepicker"))
            {
                ivalue = GetDateStr(ivalue);
            }
            objItem.val(ivalue);
            if (objItem.val() != item[1]) {
                objItem.attr("value", ivalue);
            }
        } else if(objItem.get(0).tagName.toUpperCase() == "SELECT"){
            objItem.val(item[1]);
        } else if(objItem.get(0).tagName.toUpperCase() == "DIV"){
            objItem.text(item[1]);
        }*/
    }
}

function PutHttpValue2(thePreHttpValue, parentObj) {
    if (!thePreHttpValue) return;

    for (var i = 0; i < thePreHttpValue.length; i++) {
        var item = thePreHttpValue[i];
        if (!item || item.length != 2) {
            continue;
        }
        if (item[0].substr(0, 2) == "__") {
            //if (item[0] == "__VIEWSTATE" || item[0] == "__EVENTVALIDATION") {
            continue;
        }
        if (item[0].indexOf("$") >= 0) {
            item[0] = item[0].replace(/\$/g, "_");
        }
        var objItem = $("#" + item[0], parentObj);
        if (objItem.length == 0) {
            objItem = $("[name='" + item[0] + "']", parentObj);
            if (objItem.length == 0) {
                continue;
            }
        }

        objItem.each(function () {
            var pThis = $(this);
            var type = pThis.attr("type");
            var tagname = pThis.get(0).tagName.toUpperCase();

            if (!parentObj && pThis.parents(".NotHttpValue").length > 0) {
                return;
            }

            if (type == "checkbox") {
                if (item[1].indexOf(",") >= 0 || !objItem.hasClass("enum")) {
                    var sv = "," + item[1] + ",";
                    if (sv.indexOf("," + pThis.val() + ",") >= 0) {
                        pThis.attr("checked", true);
                    } else {
                        pThis.attr("checked", false);
                    }
                } else {
                    var nv = parseInt(item[1]);
                    if (parseInt(pThis.val()) == nv) {
                        pThis.attr("checked", true);
                    } else {
                        pThis.attr("checked", false);
                    }
                }
            } else if (type == "radio") {
                if (pThis.val() == item[1]) {
                    pThis[0].checked = true;
                    //pThis.attr("checked", true);
                } else {
                    pThis.attr("checked", false);
                }
            } else if (tagname == "INPUT") {
                var ivalue = item[1];
                if (objItem.hasClass("hasDatepicker")) {
                    ivalue = GetDateStr(ivalue);
                }
                else if (objItem.hasClass("TimePicker-time-input")) {
                    ivalue = GetTimeStr(ivalue);
                }
                pThis.val(ivalue);
                if (pThis.val() != item[1]) {
                    pThis.attr("value", ivalue);
                }
            } else if (tagname == "SELECT") {
                pThis.val(item[1]);
            } else if (tagname == "DIV" || tagname == "SPAN") {
                pThis.text(item[1]);
            }
            else if (tagname == "TEXTAREA") {
                var ivalue = item[1];
                pThis.val(ivalue);
                if (pThis.val() != item[1]) {
                    pThis.attr("value", ivalue);
                }
            }

        });

        /*
        if (objItem.attr("type") == "checkbox") {
            if(item[1].indexOf(",")>=0 || !objItem.hasClass("enum"))
            {
                var sv = ","+item[1]+",";
                objItem.each(function () {
                    if (sv.indexOf("," + $(this).val() + ",") >= 0) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
            }else{
                var nv = parseInt(item[1]);
                objItem.each(function () {
                    if (parseInt($(this).val()) & nv) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
            }
        } else if (objItem.attr("type") == "radio") {
            objItem.each(function () {
                if ($(this).val() == item[1]) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
        } else if(objItem.get(0).tagName.toUpperCase() == "INPUT"){
            var ivalue = item[1];
            if(objItem.hasClass("hasDatepicker"))
            {
                ivalue = GetDateStr(ivalue);
            }
            objItem.val(ivalue);
            if (objItem.val() != item[1]) {
                objItem.attr("value", ivalue);
            }
        } else if(objItem.get(0).tagName.toUpperCase() == "SELECT"){
            objItem.val(item[1]);
        } else if(objItem.get(0).tagName.toUpperCase() == "DIV"){
            objItem.text(item[1]);
        }*/
    }
}
function GetDateStr(vd) {
    var a = parseInt(vd);
    if (a != vd) return vd;

    var s = parseInt(a / 10000) + "-";
    var i = (parseInt(a / 100) % 100);
    if (i >= 10) {
        s += i + "-";
    } else {
        s += "0" + i + "-";
    }
    i = (a % 100);
    if (i >= 10) {
        s += i;
    } else {
        s += "0" + i;
    }
    return s;
}
function GetTimeStr(vd) {
    var a = parseInt(vd);
    if (a != vd) return vd;

    var s = parseInt(a/100);
    var i = (parseInt(a%100));
    if (s < 10) {
        s = "0" + s;
    }  
    if (i < 10) {
        i = "0" + i;
   }   
    return s+":"+i;
}
function GetIEVersion() {
    var vuserAgent = navigator.userAgent.toLowerCase();
    var i = vuserAgent.indexOf("msie") + 5;
    var j = vuserAgent.indexOf(";", i);
    if (document.all) {
        return parseInt(vuserAgent.substr(i, j - i));
    } else {
        return 0;
    }
}

function IsOldIEVersion() {
    var v = GetIEVersion();
    if (v > 0 && v <= 8) {
        return true;
    } else {
        return false;
    }
}

function IsOldIE6Version() {
    var v = GetIEVersion();
    if (v > 0 && v <= 6) {
        return true;
    } else {
        return false;
    }
}

var s_correctPNGed = false
function correctPNG() {
    if (s_correctPNGed) {
        return;
    }
    s_correctPNGed = true;
    var vuserAgent = navigator.userAgent.toLowerCase();
    var i = vuserAgent.indexOf("msie") + 5;
    var j = vuserAgent.indexOf(";", i);
    if (!document.all || parseInt(vuserAgent.substr(i, j - i)) > 6) {
        return;
    }
	
	ConfirmBox("您的浏览器版本过低，请升级到最新版本。<b><a href='http://windows.microsoft.com/zh-cn/internet-explorer/download-ie'>升级</a></b><br/>是否继续打开网页？",function(){
	},"重要提示",1,function(){
		document.write("您的浏览器版本过低，请升级到最新版本。");
		window.close();
	})


    for (var i = 0; i < document.all.length; i++) {
        var obj = document.all[i];
        if (obj == null || obj.tagName == null) {
            continue;
        }

        if(obj.tagName.toLowerCase() !="img" && obj.tagName.toLowerCase()!="td"){
          continue;
        }
        var imgName = null;
        var LW = obj.width;
        var LH = obj.height;
        if (!LW || LW <= 0) {
            LW = obj.offsetWidth;
            LH = obj.offsetHeight;
        }
        if (obj.tagName.toLowerCase() == "img") {
            imgName = obj.src;
        } else {
            imgName = obj.currentStyle.backgroundImage;
            if (imgName == null || imgName == "none") {
                imgName = obj.getAttribute("background");
            }
        }
        if (imgName == null || imgName == "") {
            continue;
        }
        imgName = imgName.toUpperCase();
        if (imgName.substring(imgName.length - 3, imgName.length) == "PNG" || imgName.substring(imgName.length - 5, imgName.length) == "PNG\")") {
            var newimgName = imgName.match(/URL\(\"(.+)\"\)/);
            if (newimgName != null && newimgName.length > 0) {
                imgName = newimgName[1];
            }
            if (obj.tagName.toLowerCase() == "img") {
                obj.src = GetThemesPath()+"img/spacer.gif";//spacer.gif为1px*1px的透明gif图片
            }
            obj.style.filter += "progid:DXImageTransform.Microsoft.AlphaImageLoader(src=" + imgName + ", sizingmethod=scale);"

            obj.style.background = "none";
            obj.setAttribute("background", null);
            obj.width = LW;
            obj.height = LH;
            obj.style.width = LW;
            obj.style.height = LH;
        }
    }
}

$(function () {
    correctPNG();
});


function GetOptsFromAttr(opts, obj) {
    for (var i in opts) {
        var cattr = obj.attr(i);
        if (cattr && cattr != "") {
            opts[i] = cattr;
        }
    }
}

function TabJump(url) {
    var pForm = $("form");
    if (pForm.length == 0) {
        pForm = $(".tabs-summary");
    }
    pForm.data("CurrentURL", url);
    $.ajax({
        url: url,
        type: "POST",
        timeout: 600000,
        async: true,
        dataType: "html",
        success: function (data, status) {
            pForm.empty()
            var pData = $("<div>" + data + "</div>").appendTo(pForm);
            if (OnTabLoad) {
                try{
                    OnTabLoad(null, { panel: pData });
                } catch (eee) {
                }
            }
        },
        error: function (data, status, error) {
            MessageBox("连接失败", "", 2);
        }
    });
}
function txtToLabel(id) {
    var obj = $("#" + id);
    if (obj != null) {
        obj.css('border', '0px');
        obj.css('background-color', '#ffffff');
        obj.css('overflow', 'hidden');
        obj.attr('disabled', "true");
        obj.attr("readonly", "readonly");
    }
}
// jQuery JSON plugin 2.4.0
(function ($) {
    'use strict'; var escape = /["\\\x00-\x1f\x7f-\x9f]/g, meta = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' }, hasOwn = Object.prototype.hasOwnProperty; $.toJSON = typeof JSON === 'object' && JSON.stringify ? JSON.stringify : function (o) {
        if (o === null) { return 'null'; }
        var pairs, k, name, val, type = $.type(o); if (type === 'undefined') { return undefined; }
        if (type === 'number' || type === 'boolean') { return String(o); }
        if (type === 'string') { return $.quoteString(o); }
        if (typeof o.toJSON === 'function') { return $.toJSON(o.toJSON()); }
        if (type === 'date') {
            var month = o.getUTCMonth() + 1, day = o.getUTCDate(), year = o.getUTCFullYear(), hours = o.getUTCHours(), minutes = o.getUTCMinutes(), seconds = o.getUTCSeconds(), milli = o.getUTCMilliseconds(); if (month < 10) { month = '0' + month; }
            if (day < 10) { day = '0' + day; }
            if (hours < 10) { hours = '0' + hours; }
            if (minutes < 10) { minutes = '0' + minutes; }
            if (seconds < 10) { seconds = '0' + seconds; }
            if (milli < 100) { milli = '0' + milli; }
            if (milli < 10) { milli = '0' + milli; }
            return '"' + year + '-' + month + '-' + day + 'T' +
            hours + ':' + minutes + ':' + seconds + '.' + milli + 'Z"';
        }
        pairs = []; if ($.isArray(o)) {
            for (k = 0; k < o.length; k++) { pairs.push($.toJSON(o[k]) || 'null'); }
            return '[' + pairs.join(',') + ']';
        }
        if (typeof o === 'object') {
            for (k in o) {
                if (hasOwn.call(o, k)) {
                    type = typeof k; if (type === 'number') { name = '"' + k + '"'; } else if (type === 'string') { name = $.quoteString(k); } else { continue; }
                    type = typeof o[k]; if (type !== 'function' && type !== 'undefined') { val = $.toJSON(o[k]); pairs.push(name + ':' + val); }
                }
            }
            return '{' + pairs.join(',') + '}';
        }
    }; $.evalJSON = typeof JSON === 'object' && JSON.parse ? JSON.parse : function (str) { return eval('(' + str + ')'); }; $.secureEvalJSON = typeof JSON === 'object' && JSON.parse ? JSON.parse : function (str) {
        var filtered = str.replace(/\\["\\\/bfnrtu]/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''); if (/^[\],:{}\s]*$/.test(filtered)) { return eval('(' + str + ')'); }
        throw new SyntaxError('Error parsing JSON, source is not valid.');
    }; $.quoteString = function (str) {
        if (str.match(escape)) {
            return '"' + str.replace(escape, function (a) {
                var c = meta[a]; if (typeof c === 'string') { return c; }
                c = a.charCodeAt(); return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
            }) + '"';
        }
        return '"' + str + '"';
    };
}(jQuery));