//-------------------------------------------------------------------jquery第三方 插件-----------------------------------
//异步上传
jQuery.extend({ handleError: function (e, t, n, r) { e.error && e.error.call(e.context || e, t, n, r), e.global && (e.context ? jQuery(e.context) : jQuery.event).trigger("ajaxError", [t, e, r]) }, createUploadIframe: function (e, t) { var n = "jUploadFrame" + e, r = '<iframe id="' + n + '" name="' + n + '" style="position:absolute; top:-9999px; left:-9999px"'; return window.ActiveXObject && (typeof t == "boolean" ? r += ' src="javascript:false"' : typeof t == "string" && (r += ' src="' + t + '"')), r += " />", jQuery(r).appendTo(document.body), jQuery("#" + n).get(0) }, createUploadForm: function (e, t, n) { var r = "jUploadForm" + e, i = "jUploadFile" + e, s = jQuery('<form  action="" method="POST" name="' + r + '" id="' + r + '" enctype="multipart/form-data"></form>'); if (n) for (var o in n) jQuery('<input type="hidden" name="' + o + '" value="' + n[o] + '" />').appendTo(s); var u = jQuery("#" + t), a = jQuery(u).clone(); return jQuery(u).attr("id", i), jQuery(u).before(a), jQuery(u).appendTo(s), jQuery(s).css("position", "absolute"), jQuery(s).css("top", "-1200px"), jQuery(s).css("left", "-1200px"), jQuery(s).appendTo("body"), s }, ajaxFileUpload: function (e) { e = jQuery.extend({}, jQuery.ajaxSettings, e); var t = (new Date).getTime(), n = jQuery.createUploadForm(t, e.fileElementId, typeof e.data == "undefined" ? !1 : e.data), r = jQuery.createUploadIframe(t, e.secureuri), i = "jUploadFrame" + t, s = "jUploadForm" + t; e.global && !(jQuery.active++) && jQuery.event.trigger("ajaxStart"); var o = !1, u = {}; e.global && jQuery.event.trigger("ajaxSend", [u, e]); var a = function (t) { var r = document.getElementById(i); jQuery("#" + i).unbind("load"); try { r.contentWindow ? (u.responseText = r.contentWindow.document.body ? r.contentWindow.document.body.innerHTML : null, u.responseXML = r.contentWindow.document.XMLDocument ? r.contentWindow.document.XMLDocument : r.contentWindow.document) : r.contentDocument && (u.responseText = r.contentDocument.document.body ? r.contentDocument.document.body.innerHTML : null, u.responseXML = r.contentDocument.document.XMLDocument ? r.contentDocument.document.XMLDocument : r.contentDocument.document) } catch (s) { jQuery.handleError(e, u, null, s) } if (u || t == "timeout") { o = !0; var a; try { a = t != "timeout" ? "success" : "error"; if (a != "error") { var f = jQuery.uploadHttpData(u, e.dataType); e.success && e.success(f, a), e.global && jQuery.event.trigger("ajaxSuccess", [u, e]) } else jQuery.handleError(e, u, a) } catch (s) { a = "error", jQuery.handleError(e, u, a, s) } e.global && jQuery.event.trigger("ajaxComplete", [u, e]), e.global && !--jQuery.active && jQuery.event.trigger("ajaxStop"), e.complete && e.complete(u, a), jQuery(r).unbind(), setTimeout(function () { try { jQuery(r).remove(), jQuery(n).remove() } catch (t) { jQuery.handleError(e, u, null, t) } }, 100), u = null } }; e.timeout > 0 && setTimeout(function () { o || a("timeout") }, e.timeout); try { var n = jQuery("#" + s); jQuery(n).attr("action", e.url), jQuery(n).attr("method", "POST"), jQuery(n).attr("target", i), n.encoding ? jQuery(n).attr("encoding", "multipart/form-data") : jQuery(n).attr("enctype", "multipart/form-data"), jQuery(n).submit() } catch (f) { jQuery.handleError(e, u, null, f) } return jQuery("#" + i).load(a), { abort: function () { } } }, uploadHttpData: function (r, type) { var data = !type; return data = type == "xml" || data ? r.responseXML : r.responseText, type == "script" && jQuery.globalEval(data), type == "json" && (document.all || (data = jQuery(data).text()), data = eval("(" + data + ")")), type == "html" && jQuery("<div>").html(data).evalScripts(), data } });
//jQuery.extend({

//    handleError: function (s, xhr, status, e) {
//        // If a local callback was specified, fire it
//        if (s.error) {
//            s.error.call(s.context || s, xhr, status, e);
//        }

//        // Fire the global callback
//        if (s.global) {
//            (s.context ? jQuery(s.context) : jQuery.event).trigger("ajaxError", [xhr, s, e]);
//        }
//    },
//    createUploadIframe: function (id, uri) {
//        //create frame
//        var frameId = 'jUploadFrame' + id;
//        var iframeHtml = '<iframe id="' + frameId + '" name="' + frameId + '" style="position:absolute; top:-9999px; left:-9999px"';
//        if (window.ActiveXObject) {
//            if (typeof uri == 'boolean') {
//                iframeHtml += ' src="' + 'javascript:false' + '"';

//            }
//            else if (typeof uri == 'string') {
//                iframeHtml += ' src="' + uri + '"';

//            }
//        }
//        iframeHtml += ' />';
//        jQuery(iframeHtml).appendTo(document.body);

//        return jQuery('#' + frameId).get(0);
//    },
//    createUploadForm: function (id, fileElementId, data) {
//        //create form	
//        var formId = 'jUploadForm' + id;
//        var fileId = 'jUploadFile' + id;
//        var form = jQuery('<form  action="" method="POST" name="' + formId + '" id="' + formId + '" enctype="multipart/form-data"></form>');
//        if (data) {
//            for (var i in data) {
//                jQuery('<input type="hidden" name="' + i + '" value="' + data[i] + '" />').appendTo(form);
//            }
//        }
//        var oldElement = jQuery('#' + fileElementId);
//        var newElement = jQuery(oldElement).clone();
//        jQuery(oldElement).attr('id', fileId);
//        jQuery(oldElement).before(newElement);
//        jQuery(oldElement).appendTo(form);



//        //set attributes
//        jQuery(form).css('position', 'absolute');
//        jQuery(form).css('top', '-1200px');
//        jQuery(form).css('left', '-1200px');
//        jQuery(form).appendTo('body');
//        return form;
//    },

//    ajaxFileUpload: function (s) {
//        // TODO introduce global settings, allowing the client to modify them for all requests, not only timeout		
//        s = jQuery.extend({}, jQuery.ajaxSettings, s);
//        var id = new Date().getTime()
//        var form = jQuery.createUploadForm(id, s.fileElementId, (typeof (s.data) == 'undefined' ? false : s.data));
//        var io = jQuery.createUploadIframe(id, s.secureuri);
//        var frameId = 'jUploadFrame' + id;
//        var formId = 'jUploadForm' + id;
//        // Watch for a new set of requests
//        if (s.global && !jQuery.active++) {
//            jQuery.event.trigger("ajaxStart");
//        }
//        var requestDone = false;
//        // Create the request object    
//        var xml = {}
//        if (s.global)
//            jQuery.event.trigger("ajaxSend", [xml, s]);
//        // Wait for a response to come back
//        var uploadCallback = function (isTimeout) {
//            var io = document.getElementById(frameId);
//            jQuery('#' + frameId).unbind("load");
//            try {
//                if (io.contentWindow) {
//                    xml.responseText = io.contentWindow.document.body ? io.contentWindow.document.body.innerHTML : null;
//                    xml.responseXML = io.contentWindow.document.XMLDocument ? io.contentWindow.document.XMLDocument : io.contentWindow.document;

//                } else if (io.contentDocument) {
//                    xml.responseText = io.contentDocument.document.body ? io.contentDocument.document.body.innerHTML : null;
//                    xml.responseXML = io.contentDocument.document.XMLDocument ? io.contentDocument.document.XMLDocument : io.contentDocument.document;
//                }
//            } catch (e) {
//                jQuery.handleError(s, xml, null, e);
//            }
//            if (xml || isTimeout == "timeout") {
//                requestDone = true;
//                var status;
//                try {
//                    status = isTimeout != "timeout" ? "success" : "error";
//                    // Make sure that the request was successful or notmodified
//                    if (status != "error") {
//                        // process the data (runs the xml through httpData regardless of callback)
//                        var data = jQuery.uploadHttpData(xml, s.dataType);
//                        // If a local callback was specified, fire it and pass it the data
//                        if (s.success)
//                            s.success(data, status);

//                        // Fire the global callback
//                        if (s.global)
//                            jQuery.event.trigger("ajaxSuccess", [xml, s]);
//                    } else
//                        jQuery.handleError(s, xml, status);
//                } catch (e) {
//                    status = "error";
//                    jQuery.handleError(s, xml, status, e);
//                }

//                // The request was completed
//                if (s.global)
//                    jQuery.event.trigger("ajaxComplete", [xml, s]);

//                // Handle the global AJAX counter
//                if (s.global && ! --jQuery.active)
//                    jQuery.event.trigger("ajaxStop");

//                // Process result
//                if (s.complete)
//                    s.complete(xml, status);

//                jQuery(io).unbind()

//                setTimeout(function () {
//                    try {
//                        jQuery(io).remove();
//                        jQuery(form).remove();

//                    } catch (e) {
//                        jQuery.handleError(s, xml, null, e);
//                    }

//                }, 100)

//                xml = null

//            }
//        }
//        // Timeout checker
//        if (s.timeout > 0) {
//            setTimeout(function () {
//                // Check to see if the request is still happening
//                if (!requestDone) uploadCallback("timeout");
//            }, s.timeout);
//        }
//        try {

//            var form = jQuery('#' + formId);
//            jQuery(form).attr('action', s.url);
//            jQuery(form).attr('method', 'POST');
//            jQuery(form).attr('target', frameId);
//            if (form.encoding) {
//                jQuery(form).attr('encoding', 'multipart/form-data');
//            }
//            else {
//                jQuery(form).attr('enctype', 'multipart/form-data');
//            }
//            jQuery(form).submit();

//        } catch (e) {
//            jQuery.handleError(s, xml, null, e);
//        }

//        jQuery('#' + frameId).load(uploadCallback);
//        return { abort: function () { } };

//    },

//    uploadHttpData: function (r, type) {
//        var data = !type;
//        data = type == "xml" || data ? r.responseXML : r.responseText;
//        // If the type is "script", eval it in global context
//        if (type == "script")
//            jQuery.globalEval(data);
//        // Get the JavaScript object, if JSON is used.
//        if (type == "json") {
//            if (!document.all)//非IE
//                data = jQuery(data).text();
//            data = eval('(' + data + ')');
//        }
//        // evaluate scripts within html
//        if (type == "html")
//            jQuery("<div>").html(data).evalScripts();

//        return data;
//    }
//})
//cookie
jQuery.cookie = function (name, value, options) { if (typeof value == "undefined") { var cookieValue = null; if (document.cookie && document.cookie != "") { var cookies = document.cookie.split(";"); for (var i = 0; i < cookies.length; i++) { var cookie = jQuery.trim(cookies[i]); if (cookie.substring(0, name.length + 1) == name + "=") { cookieValue = decodeURIComponent(cookie.substring(name.length + 1)); break } } } return cookieValue } options = options || {}, value === null && (value = "", options.expires = -1); var expires = ""; if (options.expires && (typeof options.expires == "number" || options.expires.toUTCString)) { var date; typeof options.expires == "number" ? (date = new Date, date.setTime(date.getTime() + options.expires * 24 * 60 * 60 * 1e3)) : date = options.expires, expires = "; expires=" + date.toUTCString() } var path = options.path ? "; path=" + options.path : "", domain = options.domain ? "; domain=" + options.domain : "", secure = options.secure ? "; secure" : ""; document.cookie = [name, "=", encodeURIComponent(value), expires, path, domain, secure].join("") };
// jQuery JSON plugin 2.4.0
(function ($) { "use strict"; var escape = /["\\\x00-\x1f\x7f-\x9f]/g, meta = { "\b": "\\b", "	": "\\t", "\n": "\\n", "\f": "\\f", "\r": "\\r", '"': '\\"', "\\": "\\\\" }, hasOwn = Object.prototype.hasOwnProperty; $.toJSON = typeof JSON == "object" && JSON.stringify ? JSON.stringify : function (e) { if (e === null) return "null"; var t, n, r, i, s = $.type(e); if (s === "undefined") return undefined; if (s === "number" || s === "boolean") return String(e); if (s === "string") return $.quoteString(e); if (typeof e.toJSON == "function") return $.toJSON(e.toJSON()); if (s === "date") { var o = e.getUTCMonth() + 1, u = e.getUTCDate(), a = e.getUTCFullYear(), f = e.getUTCHours(), l = e.getUTCMinutes(), c = e.getUTCSeconds(), h = e.getUTCMilliseconds(); return o < 10 && (o = "0" + o), u < 10 && (u = "0" + u), f < 10 && (f = "0" + f), l < 10 && (l = "0" + l), c < 10 && (c = "0" + c), h < 100 && (h = "0" + h), h < 10 && (h = "0" + h), '"' + a + "-" + o + "-" + u + "T" + f + ":" + l + ":" + c + "." + h + 'Z"' } t = []; if ($.isArray(e)) { for (n = 0; n < e.length; n++) t.push($.toJSON(e[n]) || "null"); return "[" + t.join(",") + "]" } if (typeof e == "object") { for (n in e) if (hasOwn.call(e, n)) { s = typeof n; if (s === "number") r = '"' + n + '"'; else { if (s !== "string") continue; r = $.quoteString(n) } s = typeof e[n], s !== "function" && s !== "undefined" && (i = $.toJSON(e[n]), t.push(r + ":" + i)) } return "{" + t.join(",") + "}" } }, $.evalJSON = typeof JSON == "object" && JSON.parse ? JSON.parse : function (str) { return eval("(" + str + ")") }, $.secureEvalJSON = typeof JSON == "object" && JSON.parse ? JSON.parse : function (str) { var filtered = str.replace(/\\["\\\/bfnrtu]/g, "@").replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]").replace(/(?:^|:|,)(?:\s*\[)+/g, ""); if (/^[\],:{}\s]*$/.test(filtered)) return eval("(" + str + ")"); throw new SyntaxError("Error parsing JSON, source is not valid.") }, $.quoteString = function (e) { return e.match(escape) ? '"' + e.replace(escape, function (e) { var t = meta[e]; return typeof t == "string" ? t : (t = e.charCodeAt(), "\\u00" + Math.floor(t / 16).toString(16) + (t % 16).toString(16)) }) + '"' : '"' + e + '"' } })(jQuery);
//-------------------------------------------------------------------uni对象-----------------------------------
//联创前端公共方法库
var uni = {
    //版本
    v: "1.0",
    //版权信息
    copyright: "杭州联创信息技术有限公司",
    //语言 语言字典对象
    language: undefined,
    //语言翻译
    translate: function (key) {
        if (!key) return "";
        var dic = this.language;
        if (!dic) return key;
        if (typeof (key) == "string") {
            if (dic[key] != undefined)
                return dic[key];
            else
                return key;
        }
        else if (typeof (key) == "object") {
            if (Object.prototype.toString.call(key) == '[object Array]') {
                for (var i = 0; i < key.length; i++)
                    key[i] = uni.translate(key[i]);
            }
            else {
                $(".uni_trans", key).each(function () {
                    $(this).html(uni.translate($(this).text()));
                });
            }
            return key;
        }
        return "";
    },
    //字符串截断
    cutStr: function (s, n) {
        s = s + '';
        if (s.length > n) {
            s = s.substr(0, n - 1) + '...';
        }
        return s;
    },
    //字符串截断带title
    cutStrT: function (s, n) {
        s = s + '';
        if (s.length > n) {
            s = "<span title='" + s + "'>" + s.substr(0, n - 1) + '...</span>';
        }
        return s;
    },
    //email格式检查
    ckEmail: function (str) {
        var regex = /[_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+$/;
        return regex.test(str);
    },
    //手机格式检查
    ckMobile: function (str) {
        var regex = /^0*\d{11}$/;
        return regex.test(str);
    },
    //对象数组排序 对象sn属性作为排序标准    还需优化 cg2/apply使用
    sort: function (array, desc, compare) {
        var sorFun;
        if (typeof (compare) == "function") {//自定义规则
            sorFun = compare;
        }
        else if (compare == "number") {//数字
            sorFun = function (a, b) {
                var v1 = a.sn;
                var v2 = b.sn;
                if (desc)
                    return v2 - v1;
                else
                    return v1 - v2;
            }
        }
        else {//默认字符串 区分大小写
            sorFun = function (a, b) {
                var v1 = a.sn;
                var v2 = b.sn;
                if (desc)
                    return v2.localeCompare(v1);
                else
                    return v1.localeCompare(v2);
            }
        }
        array.sort(sorFun);
    },
    //隐藏文本
    hide: function (str) {
        return "<div style='display:none;'>" + str + "</div>";
    },
    //回到顶部
    backTop: function () {
        $('body,html').animate({ scrollTop: 0 }, 300);
    },
    //返回最小数值
    backMin: function (arr) {
        arr.sort(function (a, b) {
            return a - b;
        });
        return arr[0];
    },
    //返回最大数值
    backMax: function (arr) {
        arr.sort(function (a, b) {
            return b - a;
        });
        return arr[0];
    },
    //过滤掉html
    backText: function (v) {
        return $("<div>" + v + "</div>").text();
    },
    //日期字符串转Date类型
    parseDate: function (str, s) {
        if (!str || typeof (str) != 'string') return null;
        str = str.split(s || '-');
        y = str[0];
        M = str[1];
        str = str[2].split(' ');
        d = str[0];
        if (str.length > 1) {
            H = str[1].split(':')[0] || 0;
            m = str[1].split(':')[1] || 0;
            s = str[1].split(':')[2] || 0;
        }
        else {
            H = 0;
            m = 0;
            s = 0;
        }
        var date = new Date();
        date.setFullYear(y, M - 1, d);
        date.setHours(H, m, s, 0);
        return date;
    },
    //比较日期 s参数为 h细化到时  m到分 默认天 返回dt1-dt2
    compareDate: function (dt1, dt2, s) {
        var date1, date2;
        if (typeof (dt1) == "string") date1 = uni.parseDate(dt1);
        else date1 = uni.getObj(dt1);
        if (typeof (dt2) == "string") date2 = uni.parseDate(dt2);
        else date2 = uni.getObj(dt2);
        if (!date1 || !date2) { uni.msg.error("compareDate error"); return; }
        var d1, d2;
        if (s == "m") {
            d1 = parseInt(date1.getTime() / 1000 / 60);
            d2 = parseInt(date2.getTime() / 1000 / 60);
        }
        else if (s == "h") {
            date1.setMinutes(0);
            date2.setMinutes(0);
            d1 = parseInt(date1.getTime() / 1000 / 60);
            d2 = parseInt(date2.getTime() / 1000 / 60);
            d1 = d1 / 60;
            d2 = d2 / 60;
        }
        else {// day
            date1.setHours(0);
            date2.setHours(0);
            date1.setMinutes(0);
            date2.setMinutes(0);
            d1 = parseInt(date1.getTime() / 1000 / 60);
            d2 = parseInt(date2.getTime() / 1000 / 60);
            d1 = d1 / 1440;
            d2 = d2 / 1440;
        }
        return parseInt(d1) - parseInt(d2);
    },
    //复制对象
    getObj: function (i) {
        var objClone;
        if (!i) { uni.log.set("warning", 'uni.getObj null'); return null; }
        if (i.constructor == Object) {
            objClone = new i.constructor();
        }
        else {
            objClone = new i.constructor(i.valueOf());
        }
        for (var key in i) {
            if (objClone[key] != i[key]) {
                objClone[key] = typeof (i[key]) == 'object' ? this.getObj(i[key]) : i[key];
            }
        }
        return objClone;
    },
    //数字转指定位数字符串
    num2Str: function (num, len) {
        var str = "";
        if (!len || isNaN(len)) len = 2;
        for (var i = 0; i < len; i++) {
            str += "0";
        }
        str = str + num;
        return str.substring(str.length - len, str.length);
    },
    //时间字符串转分钟数
    str2m: function (t) {
        if (!t) return 0;
        var v = t;
        var n = parseInt(v.replace(':', ''), 10);
        return parseInt(n / 100) * 60 + (n % 100);
    },
    //删除弹出框
    doDel: function (fun) {
        uni.confirm("确定要删除吗？", fun);
    },
    //url参数转js对象
    url2Obj: function (url) {
        var search = url.replace(/^\s+/, '').replace(/\s+$/, '').match(/([^?#]*)(#.*)?$/);
        if (!search) {
            return {};
        }
        var searchStr = search[1];
        var searchHash = searchStr.split('&');
        var ret = {};
        for (var i = 0, len = searchHash.length; i < len; i++) {
            var pair = searchHash[i];
            if ((pair = pair.split('='))[0]) {
                var key = decodeURIComponent(pair.shift());
                var value = pair.length > 1 ? pair.join('=') : pair[0];
                if (value != undefined) {
                    value = decodeURIComponent(value);
                }
                if (key in ret) {
                    if (ret[key].constructor != Array) {
                        ret[key] = [ret[key]];
                    }
                    ret[key].push(value);
                } else {
                    ret[key] = value;
                }
            }
        }
        return ret;
    },
    //url参数 键值拼接
    _toQueryPair: function (key, value) {
        if (typeof value == 'undefined') {
            return key;
        }
        return key + '=' + encodeURIComponent(value === null ? '' : String(value));
    },
    //js对象转url参数
    obj2Url: function (obj) {
        var ret = [];
        for (var key in obj) {
            key = encodeURIComponent(key);
            var values = obj[key];
            if (values == undefined) continue;
            if (values && values.constructor == Array) {//数组 
                var queryValues = [];
                for (var i = 0, len = values.length, value; i < len; i++) {
                    value = values[i];
                    queryValues.push(this._toQueryPair(key, value));
                }
                ret = ret.concat(queryValues);
            } else { //字符串 
                ret.push(this._toQueryPair(key, values));
            }
        }
        return ret.join('&');
    },
    //向url追加参数字符串
    appendUrl: function (key, value) {
        var para = this.url2Obj(location.href);
        para[key] = value;
        location.href = location.pathname + "?" + this.obj2Url(para);
    },
    //判断是否在数组内
    isInArray: function (v, arr) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == v)
                return true;
        }
        return false;
    },
    //判断字符串是否为空
    isNull: function (obj) {
        if (typeof (obj) == "undefined" || obj == null || obj.length == 0)
            return true;
        else
            return false;
    },
    //判断不为空
    isNoNull: function (its) {
        if (its instanceof Array) {
            for (var i = 0; i < its.length; i++) {
                if (this.isNull(its[i])) return false;
            }
        }
        else {
            if (this.isNull(its)) return false;
        }
        return true;
    },
    //判断对象有无属性
    isEmpty: function (obj) {
        for (var name in obj)
            return false;
        return true;
    },
    //客户端QueryString对象
    _QueryString: function () {
        var name, value, i;
        var str = location.href;
        var num = str.indexOf("?")
        str = str.substr(num + 1);
        str = str.split("#")[0];
        var arrtmp = str.split("&");
        for (i = 0; i < arrtmp.length; i++) {
            num = arrtmp[i].indexOf("=");
            if (num > 0) {
                name = arrtmp[i].substring(0, num);
                value = arrtmp[i].substr(num + 1);
                this[name] = value;
            }
        }
    },
    //获取req
    getReq: function () {
        var req = new this._QueryString();
        return req;
    },
    //触发函数
    triggerFun: function (fun, i) {
        if (fun && typeof (fun) == "function")
            fun(i);
    },
    //内置页面重新加载方法
    reload: function (suc) {
        if (uni.hr.para)
            uni.hr.reload(suc, true);
        else if (uni.tab.sta)
            uni.tab.reload();
        else if ($("body").hasClass("uni_postback"))//回发
            $("body form:first").submit();
        else
            location.reload();
    },
    //加载css文件
    loadCss: function (path) {
        if (!this.isNull(path)) {
            var head = document.getElementsByTagName('head')[0];
            var link = document.createElement('link');
            link.href = path;
            link.rel = 'stylesheet';
            link.type = 'text/css';
            head.appendChild(link);
        }
    },
    //加载js文件
    loadScript: function (path) {
        if (!this.isNull(path)) {
            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.src = path;
            script.type = 'text/javascript';
            head.appendChild(script);
        }
    },
    //判断是否支持bootstrap
    isBootstrap: function () {
        if ($.fn.popover) return true;
        else
            return false;
    },
    //获取哈希表
    getHash: function () {
        return new uni.hash();
    },
    //获取标识数组
    getAarry: function () {
        return new uni.uniArray();
    }
}
//生命周期事件库 全生命周期只注册一次 key标识符 callback(fun) 注册事件 参数fun为处理函数=uni.lifeEvent[key]
uni.lifeEvent = {
    add: function (key, callback) {
        if (!this[key]) {//只会调用一次即只会注册一次
            this[key] = function () { };
            callback(function (e) {
                uni.lifeEvent[key](e);
            });
        }
    }
};
//标识数组 内容唯一
uni.uniArray = function () {
    this.arr = new Array();
    this.get = function (i) {
        return this.arr[i];
    }
    this.contains = function (v) {
        return uni.isInArray(v, this.arr);
    }
    this.set = function (v) {
        if (!uni.isInArray(v, this.arr))
            this.arr.push(v);
    }
    this.remove = function (v) {
        for (var i = 0, a; a = this.arr[i]; i++) {
            if (a == v) {
                this.arr.splice(i, 1);
                return;
            }
        }
    }
    this.size = function () {
        return this.arr.length;
    }
    this.join = function (x) {
        return this.arr.join(x);
    }
}
//哈希表
uni.hash = function () {
    this.clear = hashtable_clear;
    this.containsKey = hashtable_containsKey;
    this.containsValue = hashtable_containsValue;
    this.get = hashtable_get;
    this.isEmpty = hashtable_isEmpty;
    this.keys = hashtable_keys;
    this.set = hashtable_put;
    this.remove = hashtable_remove;
    this.size = hashtable_size;
    this.join = hashtable_toString;
    this.values = hashtable_values;
    this.hashtable = new Array();
    function hashtable_clear() {
        this.hashtable = new Array();
    }
    function hashtable_containsKey(key) {
        var exists = false;
        for (var i in this.hashtable) {
            if (i == key && this.hashtable[i] != null) {
                exists = true;
                break;
            }
        }
        return exists;
    }
    function hashtable_containsValue(value) {
        var contains = false;
        if (value != null) {
            for (var i in this.hashtable) {
                if (this.hashtable[i] == value) {
                    contains = true;
                    break;
                }
            }
        }
        return contains;
    }
    function hashtable_get(key) {
        return this.hashtable[key];
    }
    function hashtable_isEmpty() {
        return (this.size == 0) ? true : false;
    }
    function hashtable_keys() {
        var keys = new Array();
        for (var i in this.hashtable) {
            if (this.hashtable[i] != null)
                keys.push(i);
        }
        return keys;
    }
    function hashtable_put(key, value) {
        if (key == null || value == null) {
            throw 'NullPointerException {' + key + '},{' + value + '}';
        } else {
            this.hashtable[key] = value;
        }
    }
    function hashtable_remove(key) {
        var rtn = this.hashtable[key];
        this.hashtable[key] = null;
        //this.hashtable.splice(key, 1);
        return rtn;
    }
    function hashtable_size() {
        var size = 0;
        for (var i in this.hashtable) {
            if (this.hashtable[i] != null)
                size++;
        }
        return size;
    }
    function hashtable_toString(x) {
        var result = '';
        if (!x) x = ',';
        for (var i in this.hashtable) {
            var v = this.hashtable[i];
            if (v != null) {
                if (typeof (v) == "string" || typeof (v) == "number" || typeof (v) == "boolean")
                    result += v + x;
                else if (typeof (v) == "object") {
                    if ($.toJSON)//需jqeury json 支持
                        result += $.toJSON(v) + x;
                }
            }
        }
        if (result != '') result = result.substr(0, result.length - 1);
        return result;
    }
    function hashtable_values() {
        var values = new Array();
        for (var i in this.hashtable) {
            if (this.hashtable[i] != null)
                values.push(this.hashtable[i]);
        }
        return values;
    }
}
//公共哈希表
uni.ihash = (function () { return uni.getHash(); })();
//日志对象 哈希表
uni.log = {
    //数据对象
    data: (function () { return uni.getHash(); })(),
    //插入日志
    set: function (type, con, detail) {
        var dt = (new Date()).format("yyyyMMddHHmmss") + "_" + type;
        this.data.set(dt, con);
        if (typeof (detail) == "string")
            this.data.set(dt + "_detail", detail);
    },
    //获取日志
    get: function () {
        return this.data;
    }
};

//ajax操作方法库
uni.j = {
    //form方式 data参数结构体
    dForm: function (act, $f) {
        return { "act": act, "$f": $f };
    },
    //loading开关
    swit: 0,
    //访问记录
    history: [],
    //错误信息
    error: {},
    //默认ajax 连接错误回调函数
    _ajaxErr: function (error) {
        this.error = error;
        var msg = "异步连接出现异常！";
        if (error.statusText == "timeout")
            msg = "服务器没有响应，连接超时！";
        uni.msgBox(msg);
        uni.log.set("msg", msg + "/status:" + error.statusText, error.responseText);
    },
    //基础ajax方法 get方式 flag=1 js对象转url参数 flag=2 form提交 flag=3 uri编码
    _get: function (url, data, suc, err, flag, type) {
        var d = data;
        if (flag === 1) {
            d = uni.obj2Url(data);
        }
        else if (flag === 2) {
            d = $(data.$f).serialize() + "&act=" + data.act;
        }
        else if (flag === 3) {
            d = encodeURI(data);
        }
        if (d != null && d != "") {
            if (d.length > 2000) {//当参数超出url长度限制，转post方式
                this.post(url, uni.url2Obj(d), suc, err);
                return;
            }
            else {
                url = url + "?" + d;
            }
        }
        var len = this.history.length;
        if (len > 0) {
            var last = this.history[len - 1];
            if (last.act == url && last.ajax_state == "sending") {
                return;//重复提交 丢弃
            }
        }
        var sta = { ajax_state: "sending", act: url };
        this.history.push(sta);
        $.ajax({
            type: "GET",
            cache: false,
            timeout: 150000,
            buf: data,
            url: url,
            dataType: type || "json",
            success: suc,
            error: err,
            beforeSend: function () {
                uni.j.swit++;
                uni.showWait();
            },
            complete: function () {
                uni.j.swit--;
                if (uni.j.swit == 0) uni.hideWait();
                sta.ajax_state = "complete";
            }
        });
    },
    //基础ajax方法 post方式 flag=2 form提交
    _post: function (url, data, suc, err, flag, type) {
        var d = data;
        if (flag === 2) {
            d = $(data.$f).getFormJson();
            d.act = data.act;
        }
        var len = this.history.length;
        if (len > 0) {
            var last = this.history[len - 1];
            if (last.act == d.act && last.ajax_state == "sending") {
                return;//重复提交 丢弃
            }
        }
        var sta = { ajax_state: "sending", act: d.act, data: d };
        this.history.push(sta);
        $.ajax({
            type: "POST",
            timeout: 200000,
            buf: data,
            url: url,
            data: d,
            dataType: type || "json",
            success: suc,
            error: err,
            beforeSend: function () {
                uni.j.swit++;
                uni.showWait();
            },
            complete: function () {
                uni.j.swit--;
                if (uni.j.swit == 0) uni.hideWait();
                sta.ajax_state = "complete";
            }
        });
    },
    //AJAX Get方式
    get: function (url, suc, err) {
        var e = err || this._ajaxErr;
        this._get(url, "", suc, e);
    },
    //get方式 form提交(data.act 操作命令 data.$f 表单Jquery对象)
    fGet: function (url, data, suc, err) {
        var e = err || this._ajaxErr;
        this._get(url, data, suc, e, 2);
    },
    //get方式 js对象转url参数
    objGet: function (url, data, suc, err) {
        var e = err || this._ajaxErr;
        this._get(url, data, suc, e, 1);
    },
    //get方式 参数部分uri编码
    ecGet: function (url, data, suc, err) {
        var e = err || this._ajaxErr;
        this._get(url, data, suc, e, 3);
    },
    //AJAX Post方式
    post: function (url, data, suc, err) {
        var e = err || this._ajaxErr;
        this._post(url, data, suc, e);
    },
    //post方式 form提交(data.act 操作命令 data.$f 表单Jquery对象)
    fPost: function (url, data, suc, err) {
        var e = err || this._ajaxErr;
        this._post(url, data, suc, e, 2);
    }
}

//显示等待图标
uni.showWait = function (obj) {
    var wait = $("#uni_wait_icon");
    var top = $(document).scrollTop() + $(window).height() / 2;
    var left = $(document).scrollLeft() + $(window).width() / 2;
    wait.css({ top: top, left: left, display: 'block' });
}
//隐藏等待图标
uni.hideWait = function (obj) {
    $("#uni_wait_icon").css('display', 'none');
}
//判断框
uni.confirm = function (msg, okFun, bkFun, title, para) {
    if (!para) para = {};
    msg = uni.translate(msg);
    title = uni.translate(title);
    var dlg = $("#uni_confirm");
    var con = msg || "";
    dlg.html("<p><span class='ui-state-highlight' style='border: 0;'><span class='ui-icon ui-icon-alert' style='float: left;'></span></span> " + con + "</p>");
    dlg.dialog({
        title: title || uni.translate("提醒"),
        autoOpen: true,
        modal: true,
        resizable: false,
        width: para.width || 320,
        minHeight: para.height || 160,
        buttons: [{
            text: para.okText || uni.translate("确定"),
            click: function () {
                dlg.dialog("close");
                if (okFun != undefined) okFun();
            }
        },
            {
                text: para.backText || uni.translate("返回"),
                click: function () {
                    dlg.dialog("close");
                    if (bkFun != undefined) bkFun();
                }
            }
        ]
    });
    return dlg;
}
//消息框
uni.msgBox = function (msg, title, fun, type, para) {
    //正则表达式汉语翻译替换，只能对字符串使用，通用性不好 暂时西交利物浦用
    //if (uni.language) {
    //    var reg = /([\u4E00-\u9FA5]|[\uFE30-\uFFA0]|\u3002|\u3010|\u3011)+/g;///[\u2E80-\u9FFF]+/g;
    //    var str = msg;
    //    var arr = reg.exec(msg);
    //    while (arr && reg.lastIndex <= msg.length) {
    //        str = str.replace(arr[0], uni.translate(arr[0]));
    //        arr = reg.exec(msg);
    //    }
    //    msg = str;
    //}
    //
    title = uni.translate(title);
    if (type || (!title && !fun)) { uni.msg.m(type, msg, fun); return; }
    msg = uni.translate(msg);
    var dlg = $("#uni_alert");
    if (dlg.length == 0) {
        $(function () { uni.msgBox(msg, title, fun); });
        return;
    }
    if (uni.isNull(title)) {
        title = uni.translate("消息");
    }
    var con = msg || "";
    dlg.html("<p><span class='ui-state-highlight' style='border: 0;'><span class='ui-icon ui-icon-info' style='float: left;'></span></span> " + con + "</p>");
    dlg.dialog({
        title: title,
        autoOpen: true,
        modal: false,
        resizable: false,
        close: fun,
        buttons: [{
            text: uni.translate("确定"),
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
//消息框，确定后刷新
uni.msgBoxR = function (msg, title, fun, type) {
    var f = function () {
        uni.reload();
    }
    if (fun) f = fun;
    uni.msgBox(msg, title, f, type);
}
//消息框，跳转
uni.msgBoxRT = function (msg, title, url, type) {
    if (!url)
        uni.msgBoxR(msg, title, null);
    else {
        uni.msgBox(msg, title, function () {
            window.top.location.href = url;
        }, type);
    }
}
//基础会话框，con为jquery对象,dlg为会话框
uni.dlgBasic = function (dlg, title, width, height, okFun, bkFun, clsFun, modal) {
    if (uni.isNull(dlg)) return;
    uni.dlgInst.dlg = dlg;
    dlg = $(dlg);
    if (dlg.parents(".uni_hr_load").length > 0) uni.hr.tmp.push(dlg);//hr内dlg资源标记刷新时释放
    dlg.addClass("dialog");
    $(".dlg_close_r", dlg).click(function () { dlg.dialog("close"); uni.reload(); });
    $(".dlg_close", dlg).click(function () { dlg.dialog("close"); });
    var tp = $(".tooltip", dlg).addClass("ui-tooltip ui-widget ui-widget-content ui-corner-all");
    if (tp.css("display") == "inline") tp.css("line-height", "30px");
    var mk = $(".remark", dlg);
    if ($(".ui_icon", mk).length == 0) mk.prepend("<span class='glyphicon glyphicon-exclamation-sign uni_icon red'></span>&nbsp;");
    if (modal == null) modal = true;
    //翻译
    uni.translate(dlg);
    title = uni.translate(title);
    dlg.dialog({
        title: title || "",
        autoOpen: false,
        resizable: false,
        draggable: false,
        show: {
            effect: "fadeIn",
            duration: 300
        },
        hide: {
            effect: "fadeOut",
            duration: 300
        },
        modal: modal,
        minWidth: width || 300,
        minHeight: height || 200,
        close: clsFun
    });
    var funs = new Array();
    if (okFun != undefined) {
        funs.push({ text: uni.translate("提交"), click: function () { okFun(dlg[0], $("form:first", dlg)[0]); } });
    }
    if (okFun != undefined && bkFun == undefined) {
        funs.push({ text: uni.translate("返回"), click: function () { dlg.dialog("close"); } });
    }
    if (bkFun != undefined) funs.push({ text: uni.translate("返回"), click: function () { bkFun(dlg[0], $("form:first", dlg)[0]) } });
    if (funs.length > 0) dlg.dialog("option", "buttons", funs);
    dlg.dialog("open");
}
//会话框
uni.dlg = function ($dlg, title, width, height, okFun, bkFun, clsFun, modal) {
    uni.dlgBasic($dlg, title, width, height, okFun, bkFun, clsFun, modal);
}
//非模会话框
uni.dlgM = function ($dlg, title, width, height, okFun, bkFun, clsFun) {
    uni.dlgBasic($dlg, title, width, height, okFun, bkFun, clsFun, false);
}
//会话框，关闭后刷新
uni.dlgR = function ($dlg, title, width, height, okFun, bkFun, modal) {
    var f = function () { uni.reload(); }
    uni.dlg($dlg, title, width, height, okFun, bkFun, f, modal);
}
//初始化会话框
uni.dlgInit = function ($dlg) {
    var dlg = $($dlg);
    $("textarea,input[type=text],input[type=hidden],select", dlg).val("");
    $(".init", dlg).html("");
}
//新页面会话框 btns：按钮数组 {text,click}
uni.dlgPage = function (url, title, width, height, clsFun, btns, modal) {
    var key = "dlg_" + (new Date()).format("MMddHHmmssS");
    if (url.indexOf('?') < 0)
        url += "?dlg_key=" + key;
    else
        url += "&dlg_key=" + key;
    var dlg = $("<div class='page_dialog dialog' style='padding:0;margin:0;height:100%;width:100%;position:relative;'></div>");
    var ifm = $('<iframe src="' + url + '" frameborder="0" height="100%" width="100%" scrolling="auto"></iframe>');
    var wait = $("<div class='uni_dlg_loading_icon wait_icon' style='top:\"49%\";left:\"49%\";'></div>");
    ifm.load(function () {
        wait.remove();
    });
    //dlg.append(ifm);
    dlg.dialog({
        title: uni.translate(title) || "",
        autoOpen: true,
        modal: modal || true,
        minWidth: width || 300,
        minHeight: height || 200,
        resizable: false,
        draggable: false,
        show: {
            effect: "fadeIn",
            duration: 400
        },
        hide: {
            effect: "fadeOut",
            duration: 300
        },
        beforeClose: function () {
            dlg.remove();//从dom中清理掉dlg
            if (typeof (clsFun) == "function")
                clsFun(dlg, key)
        },
        open: function () {
            var pthis = $(this),
                width = parseInt(pthis.css("width")),
                height = parseInt(pthis.css("height"));//(height / 2 - 16) + (width / 2 - 16) + 
            //pthis.parent().css("position", "relative");
            pthis.append(ifm).after(wait);
            ifm.css("width", width - 2);
            ifm.css("height", height);
        }
    });
    uni.dlgInst[key] = dlg;
    if (btns) dlg.dialog("option", "buttons", btns);
}
uni.dlgInst = {};
//冒泡提示列表 需bootstrap
uni.pop = function ($refer, opt, title, clsFun, openFun) {
    var pop;
    var act;
    //已初始化
    if (!uni.isNull($refer) && $refer.attr("pop_key")) { pop = uni.ihash.get($refer.attr("pop_key")); reset(); }
    else {//初始化
        var key = "uni_pop_" + (new Date()).getTime();
        pop = $("<div id='" + key + "' class='popover uni_popover'><button type='button' class='close'><span>×<span></button>" +
        "<div class='arrow'></div><h3 class='popover-title'></h3><div class='popover-content'></div></div>");
        if (!uni.isBootstrap()) {
            uni.msgBox("pop error");
            return;
        }
        if (!opt) opt = {};
        if (uni.isNull($refer) || $refer.parents(".uni_hr_load").length > 0) uni.hr.tmp.push(pop);// hr内dlg资源标记刷新时释放
        $(".dlg_close_r", pop).click(function () { close(); uni.reload(); });
        $(".dlg_close,.close", pop).click(function () { close(); });
        var ptitle = pop.children(".popover-title");
        var pcon = pop.children(".popover-content");
        var ul = $("<ul class='ul_items'></ul>");//内置列表
        var mbs = uni.getHash();//获取哈希表 列表用
        var vessel = uni.getVessel(ul);
        if (opt && opt.delItemFun)
            vessel.delFun = opt.delItemFun;
        vessel.para.except = opt.except;
        pop.mbs = vessel.mbs;
        if (title) ptitle.html(title);
        else ptitle.hide();
        if (opt.con)
            pcon.html($(opt.con).show());
        pcon.append(ul);
        //配置
        if (opt.colseBtn != true)//隐藏关闭按钮
            pop.find("button.close").hide();
        if (!uni.isNull($refer)) {
            var ref = $($refer);
            ref.attr("pop_key", key);//标注已附加pop
            uni.ihash.set(key, pop);
            //注册显隐事件
            if (opt.trigger)
                ref.on(opt.trigger, function () {
                    if (pop.is(":hidden"))
                        open();
                    else
                        close();
                });
            if (ref.parents(".dialog").length > 0) {
                //若存在弹窗 注册与参考对象同消失
                ref.parents(".dialog:first").on({
                    dialogbeforeclose: function () { close(); },
                    dialogopen: function () { open(); }
                });
                $("body").append(pop);
            }
            else
                ref.after(pop);
        }
        else
            $("body").append(pop);
        if (opt.autoOpen == true)
            open();
    }
    return act = { open: open, close: close, reset: reset, addItem: addItem, items: pop.mbs }
    function open() {
        if (pop.is(":hidden"))
            pop.fadeIn("100");
        reset();
        if (openFun)
            openFun(act, pop);
    }
    function close() {
        pop.fadeOut("100");
        if (clsFun)
            clsFun(act, pop);
    }
    function reset() {
        var top;
        var left;
        if (!opt.orien) opt.orien = "top";
        if (!uni.isNull($refer)) {
            var refer = $refer;
            if ($refer.is(":hidden")) { return; }
            var h = refer.height();
            var w = refer.width();
            var ih = pop.height();
            var iw = pop.width();
            var offset = refer.offset();
            var orien = opt.orien;
            if (orien == "top") {
                top = offset.top - ih;
                left = offset.left - iw / 2 + w / 2;
            }
            else if (orien == "right") {
                top = offset.top - ih / 2 + h / 2;
                left = offset.left + w;
            }
            else if (orien == "bottom") {
                top = offset.top + h;
                left = offset.left - iw / 2 + w / 2;
            }
            else if (orien == "left") {
                top = offset.top - ih / 2 + h / 2;
                left = offset.left - iw;
            }
        }
        if (top)
            opt.top = top;
        if (left)
            opt.left = left;
        pop.offset({ top: opt.top || 0, left: opt.left || 0 });
        pop.addClass(opt.orien);
        if (opt.fixed) pop.css("position", "fixed");
    }
    function addItem(id, v) {
        //pop.mbs.set(id, { id: id, v: v });
        //refMember();
        vessel.addItem(id, v, function () {
            open();
        });
        return pop.mbs;
    }
    function refMember() {
        //var mbs = pop.mbs;
        //var ul = pop.ul;
        //var list = mbs.values();
        //ul.html("");
        //for (var i = 0; i < list.length; i++) {
        //    var li = $("<li>" + (i + 1) + ".<span>" + list[i].v + "</span></li>");
        //    $("<a  key='" + list[i].id + "' class='click del'> ×</a>").click(function () {
        //        var pthis = $(this);
        //        pthis.parent().remove();
        //        var key = pthis.attr("key");
        //        mbs.remove(key);
        //        if (opt && opt.delItemFun) opt.delItemFun(mbs);
        //        refMember();
        //    }).appendTo(li);
        //    ul.append(li);
        //}
        vessel.refMember(function () {
            reset();
        });
    }
}
//动态选择容器
uni.getVessel = function (ul, delFun, storer, para) {
    return {
        ul: $(ul),//容器ul对象
        delFun: delFun,//删除回调
        storer: storer,//存值对象
        para: para || {},//其它参数
        mbs: (function () { return uni.getHash(); })(),
        addItem: function (id, v, suc) {
            this.mbs.set(id, { id: id, v: v });
            this.refMember(suc);
            return this.mbs;
        },
        refMember: function (suc) {
            var ve = this;
            var mbs = this.mbs;
            var ul = this.ul;
            var para = this.para || {};
            var list = mbs.values();
            ul.html("");
            for (var i = 0; i < list.length; i++) {
                var li = $("<li>" + (i + 1) + ".<span>" + list[i].v + "</span></li>");
                if (!para.except || !uni.isInArray(list[i].id, para.except)) {
                    $("<a  key='" + list[i].id + "' class='click del'> ×</a>").click(function () {
                        var pthis = $(this);
                        pthis.parent().remove();
                        var key = pthis.attr("key");
                        mbs.remove(key);
                        if (ve.delFun) ve.delFun(mbs, ul);
                        ve.refMember();
                    }).appendTo(li);
                }
                ul.append(li);
            }
            if (this.storer) $(this.storer).val(mbs.keys());//storer 存值的dom对象或jqeury对象
            if (suc) suc(mbs, ul);
        }
    }
}

//获取IE版本
uni.getIEVer = function () {
    var vuserAgent = navigator.userAgent.toLowerCase();
    var i = vuserAgent.indexOf("msie") + 5;
    var j = vuserAgent.indexOf(";", i);
    if (document.all) {
        return parseInt(vuserAgent.substr(i, j - i));
    } else {
        if (vuserAgent.indexOf("trident") > -1) {//检测 IE11
            return parseInt(vuserAgent.match(/rv:([\d.]+)/)[1]);
        }
        else {
            return 0;
        }
    }
}
//判断是否IE浏览器版本
uni.isIE = function () {
    var v = uni.getIEVer();
    if (v > 0) {
        return true;
    } else {
        return false;
    }
}
//判断是否IE6浏览器版本
uni.isIE6 = function () {
    var v = uni.getIEVer();
    if (v > 0 && v <= 6) {
        return true;
    } else {
        return false;
    }
}
//-------------------------------------------------------------------js对象 拓展 --------------------------------------
//Date对象拓展

//格式化
Date.prototype.format = function (fmt) {
    if (isNaN(this.getFullYear())) return '';
    var week = ['日', '一', '二', '三', '四', '五', '六'];
    uni.translate(week);
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "E": week[this.getDay()],//周
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};
Date.prototype.addMinutes = function (m) {
    m = parseInt(m);
    this.setMinutes(this.getMinutes() + m);
    return this;
};
Date.prototype.addHours = function (h) {
    h = parseInt(h);
    this.setHours(this.getHours() + h);
    return this;
};
Date.prototype.addDays = function (d) {
    d = parseInt(d);
    this.setDate(this.getDate() + d);
    return this;
};
Date.prototype.addWeeks = function (w) {
    w = parseInt(w);
    this.addDays(w * 7);
    return this;
};
Date.prototype.addMonths = function (m) {
    m = parseInt(m);
    var d = this.getDate();
    this.setMonth(this.getMonth() + m);
    if (this.getDate() < d)
        this.setDate(0);
    return this;
};
Date.prototype.addYears = function (y) {
    y = parseInt(y);
    var m = this.getMonth();
    this.setFullYear(this.getFullYear() + y);
    if (m < this.getMonth()) {
        this.setDate(0);
    }
    return this;
};
Date.prototype.getWeek = function () {
    return 6 - ((7 - this.getDay()) % 7);//0-6
};
//获取对象
Date.prototype.getObj = function () {
    var t = this;
    var date = new Date(t.getFullYear(), t.getMonth(), t.getDate(), t.getHours(), t.getMinutes(), t.getSeconds(), t.getMilliseconds());
    return date;
};
//格式化日期
uni.formatDate = function (fmt, date) {
    var d = date || new Date();
    if (d instanceof Date) return d.format(fmt);
    return uni.translate("非日期对象");
};
//异步载入html 重新载入
uni.hr = {
    //第一次加载参数
    para: undefined,
    //页面历史
    history: [],
    //二级html传参 一次读取即释放
    _cachePara: undefined,
    getPara: function () {
        var data = uni.getObj(this._cachePara);
        this._cachePara = null;
        return data;
    },
    //html载入模式会在dialog操作时在body下留下无法释放的资源，所以把此类资源存入temp，在刷新时释放掉
    tmp: [],
    //重新加载html
    reload: function (success, refresh) {
        var p = this.para;
        var vp = {};
        vp.url = p.url;
        if (p && p.con && p.url) {
            p.con.hide();
            uni.showWait();
            $.ajaxSetup({
                timeout: 30000,
                cache: false
            });
            vp.con = p.con;
            if (p.cp) this._cachePara = p.cp; else this._cachePara = null;
            if (p.$cache) { vp.con = p.$cache; vp.url = p.curl; }
            for (var i = 0; i < this.tmp.length; i++)//手动卸载无法释放的资源 hr内加载到外部的资源都应该清理掉
                $(this.tmp[i]).remove();
            this.tmp = [];
            vp.con.addClass("uni_hr_load");//标注hr方式加载，通知子元素把dlg资源放入tmp
            $("body").trigger("uni_hr_load_ref", vp);//触发刷新事件 加载页内只能使用one绑定 慎用
            vp.con.load(vp.url, p.data, function (rlt, sta) {
                uni.hideWait();
                vp.con.fadeIn("300");
                if (sta == "success") {//成功
                    uni.translate(vp.con);//翻译内容
                    if (refresh != true && refresh != "true")//非刷新 加入历史
                        uni.hr.history.push(uni.getObj(p));
                    $("body").trigger("uni_hr_load_success", vp);//触发加载成功事件 加载页内只能使用one绑定 否则会重复绑定 慎用
                    if (success)
                        success(vp);
                }
                else {//失败
                    if (sta == "timeout") {
                        uni.msgBox("加载超时！");
                    }
                    else {
                        if (p.fail)
                            p.fail(vp, sta);
                        else
                            uni.msgBox("加载失败！");
                    }
                    if (p.$cache) {//二级页面加载不成功则返回
                        uni.hr.back();
                    }
                }
            });
        }
    },
    //加载html
    loadHtml: function (url, data, $con, cachePara, suc, $cache, fail) {
        if (!this.para) this.para = {};
        if (url) {
            if ($cache)
                this.para.curl = url;//二级页面地址 为了保留原url值
            else
                this.para.url = url;
        }
        this.para.data = data;
        if ($con)
            this.para.con = $con;
        this.para.fail = fail;
        if ($cache)
            this.para.$cache = $cache;
        else if (this.para.$cache) {
            this.para.$cache.html("").hide();
            this.para.$cache = null;
        }
        this.para.cp = cachePara;//页面间传值
        this.reload(suc);
    },
    //加载二级html
    loadCache: function (url, data, $cache, cachePara, suc, fail) {
        this.loadHtml(url, data, null, cachePara, suc, $cache, fail);
    },
    //返回
    back: function (fun) {
        this._cachePara = null;
        if (this.para && this.para.$cache) {
            this.para.$cache.html("");
            this.para.$cache.hide();
            this.para.con.fadeIn('300');
            this.para.curl = null;
            this.para.$cache = null;
        }
        else {
            this.history.pop();//删除当前
            var p = this.history.pop();
            if (p) {
                this.para = p;
                this.reload();
            }
        }
        if (typeof fun == "function") {
            fun();
        }
    },
    //判断是否为hr加载内容
    isHrLoad: function ($obj) { return ($obj.parents(".uni_hr_load").length > 0); },
    //绑定一次性加载完成事件
    loadSuccess: function (fun) {
        $("body").one("uni_hr_load_success", fun);
    },
    //绑定一次性重加载事件
    loadRef: function (fun) {
        $("body").one("uni_hr_load_ref", fun);
    }
}
//-------------------------------------------------------------------jquery 拓展 ---------------------------------------------
$.fn.extend({
    //Form转json格式字符串
    getFormJson: function () {
        var o = {};
        var a = $(this).serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    },
    //切换类
    switchover: function (cls) {
        var p = $(this);
        if (p.hasClass(cls)) p.removeClass(cls);
        else p.addClass(cls);
    },
    //点击后load方式异步加载html data：js对象参数
    clickLoad: function (suc, fail) {

        var objs = $(this);
        objs.click(function () {
            var pthis = $(this);
            var url = pthis.attr("url");
            var data = pthis.attr("data");
            var cache = pthis.attr("cache");
            var para = pthis.attr("para");
            if (cache) cache = $(cache);
            if (data)//向服务端传值
                data = eval('(' + data + ')');
            if (para)//页面间传值
                para = eval('(' + para + ')');
            var con = pthis.attr("con");
            if (con) con = $(con);
            uni.hr.loadHtml(url, data, con, para, suc, cache, fail);
        });
    },
    //转url参数
    obj2Url: function () {
        return uni.obj2Url(this[0]);
    },
    //子tr和li元素列表双色交替背景
    zebra: function () {
        $(this).each(function () {
            $(this).find('tbody tr,li').each(function (i) {
                if (i % 2 == 0) {
                    $(this).addClass('even');
                }
                else {
                    $(this).addClass('odd');
                }
            });
        });
    },
    //分页  $ctrl分页控制, num每页条数 
    pctrl: function ($ctrl, num, opt) {
        var pthis = $(this);
        setPCtrl();
        function setPCtrl() {
            pthis.each(function () {
                if (!$ctrl || $ctrl.length == 0) return;
                if (!opt) opt = {};
                var list = $(this).addClass("unipctrl uni_list").find(opt.item || ".it");
                var ctrl = $($ctrl).addClass("unipctrl uni_ctrl");
                var len = list.length;
                var count = Math.ceil(len / num);
                var ready = 0;
                list.each(function (i) {
                    var pthis = $(this);
                    var index = parseInt(i / num);
                    pthis.attr("index", index);
                    pthis.hide();
                });
                if (opt.type == "dropdown") {
                    ctrl.html("<div class='uni_more_panel'><span class='uni_m_info'>总<span class='uni_m_total'>" + len + "</span>条数据，已显示<span class='uni_m_ready'></span>条，" + (opt.trigger == "drop" ? "滚动显示更多" : "点击加载更多") + "...</span></div>");
                    if (opt.trigger == "drop") {
                        $(window).scroll(function () {//不得在hr页内使用
                            if (ready < count && ($(window).height() - (ctrl.height() + (ctrl.offset().top - $(document).scrollTop())) > (opt.triggerHeight || 120))) {
                                more();
                            }
                        });
                    }
                    else {
                        ctrl.css("cursor", "pointer");
                        ctrl.click(function () {
                            more();
                        });
                    }
                    more();
                }
                else {
                    debugger
                    var bottomTotal = uni.translate("总");
                    var bottomFirst = uni.translate("首页");
                    var pagebottom = "<div class='uni_pc_panel' onselectstart='return false'><span class='uni_pc_info'>" + bottomTotal + "<span class='uni_pc_total uni_pc_num'>" + len + "</span>" + uni.translate("条数据，分") + "<span class='uni_pc_pages uni_pc_num'>" + count + "</span>" + uni.translate("页") + " | " + uni.translate("当前第") + "<span class='uni_pc_here uni_pc_num'>0</span>" + uni.translate("页") + "</span><span class='uni_pc_ctrl' data-here='-1'><span class='uni_pc_flip uni_pc_first'>" + bottomFirst + "</span>|<span class='uni_pc_pre uni_pc_flip'>" + uni.translate("上一页") + "</span>|<span class='uni_pc_next uni_pc_flip'>" + uni.translate("下一页") + "</span>|<span class='uni_pc_flip uni_pc_last'>" + uni.translate("尾页") + "</span></span></div>";
                    ctrl.html(pagebottom);
                    //个人中心->信用明细->成员管理 中分页控件初始化(弹窗函数影响uni对象，uni对象重置了，只能通过重新获取语言翻译对象)
                    if (uni.language == null || uni.language == undefined) {
                        SetEnlanguage(ctrl,len,count)
                    }
                    ctrl.find(".uni_pc_flip").click(function () {
                        var pthis = $(this);
                        var v = parseInt(pthis.parent().data("here"));
                        if (pthis.hasClass("uni_pc_pre")) {
                            if (v > 0)
                                page(v - 1);
                        }
                        else if (pthis.hasClass("uni_pc_next")) {
                            if (v < count - 1)
                                page(v + 1);
                        }
                        else if (pthis.hasClass("uni_pc_first")) {
                            if (v > -1)
                                page(0);
                        }
                        else if (pthis.hasClass("uni_pc_last")) {
                            if (v > -1)
                                page(count - 1);
                        }
                    });
                    page(0);
                }
                function more() {
                    if (ready < count) {
                        list.each(function () {
                            var pthis = $(this);
                            if (parseInt(pthis.attr("index")) == ready)
                                pthis.show();
                        });
                        ready++;
                    }
                    if (ready < count)
                        ctrl.find(".uni_m_ready").html(ready * num);
                    else
                        $(".uni_more_panel", ctrl).html(len == 0 ? "没有获取到任何数据" : ("Load：" + len + "row Data"));
                }
                function page(p) {
                    var hr = ctrl.find(".uni_pc_here");
                    var cr = ctrl.find(".uni_pc_ctrl");
                    if (count == 0) {
                        ctrl.find(".uni_pc_flip").addClass("grey");
                        hr.html("0");
                        cr.data("here", "-1");
                        return;
                    }
                    var pre = ctrl.find(".uni_pc_pre");
                    var next = ctrl.find(".uni_pc_next");
                    var first = ctrl.find(".uni_pc_first");
                    var last = ctrl.find(".uni_pc_last");
                    list.each(function () {
                        var pthis = $(this);
                        if (parseInt(pthis.attr("index")) == p)
                            pthis.show();
                        else
                            pthis.hide();
                    });
                    hr.html(p + 1);
                    cr.data("here", p);
                    if (p == 0) {
                        pre.addClass("grey");
                        first.addClass("grey");
                    }
                    else {
                        pre.removeClass("grey");
                        first.removeClass("grey");
                    }
                    if (p == count - 1) {
                        next.addClass("grey");
                        last.addClass("grey");
                    }
                    else {
                        next.removeClass("grey");
                        last.removeClass("grey");
                    }
                }
            });
        }
        return { reset: setPCtrl };
    },
    //列表排序 基础方法 this需为直接父级元素 (getSN sn计算函数)
    unisort: function (getSN, desc, type) {
        var pthis = $(this);
        var list = pthis.children();
        if (getSN) {
            for (var i = 0; i < list.length; i++) {
                var tr = list[i];
                tr.sn = getSN(tr);
            }
        }
        uni.sort(list, desc, type);
        for (var j = 0; j < list.length; j++) {
            pthis.append(list[j]);
        }
    },
    //标准表格排序 thead标头  单tbody
    //类sort_asc/sort_desc初始化 类no_sort不排序 属性tp指定比较类型 默认字符串 number=数字
    tblsort: function (suc) {
        $(this).each(function () {
            var pthis = $(this);
            var ths = pthis.find("th");
            var con = pthis.find("tbody");
            ths.each(function () {
                var pthis = $(this);
                if (!pthis.hasClass("no_sort")) pthis.css("cursor", "pointer");
            });
            ths.toggle(function () {
                _v(this, false);
            },
            function () {
                _v(this, true);
            });
            //初始化 定义排序的标头
            var ith = pthis.find(".sort_asc,.sort_desc");
            if (ith.length > 0) {
                _v(ith[0], ith.hasClass("sort_desc"));
            }

            function _v(th, desc) {
                var th = $(th);
                if (th.hasClass("no_sort")) return;
                ths.removeClass("sort_asc sort_desc");
                if (desc) th.addClass("sort_desc");
                else th.addClass("sort_asc");
                var index = th.index();
                var type = th.attr("tp");
                var getsn;
                if (type == "strong_number") {//强转换数字比较
                    type = "number";
                    getsn = function (li) {
                        return ($(li).children("td:eq(" + index + ")").text()).replace(/[^0-9]/ig, "");
                    }
                }
                else//默认
                    getsn = function (li) {
                        return $(li).children("td:eq(" + index + ")").text();
                    }
                con.unisort(getsn, desc, type);
                if (suc) suc();
            }
        });
    },
    //输入框提示
    hint: function (v) {
        $(this).each(function () {
            //存在bug 无法验空
            //var pthis = $(this);
            //var t = pthis.attr("placeholder");
            //if (!t && v) t = v;
            //if (t) {
            //    pthis.focus(function () { if(this.value==t)this.value = ''; });
            //    pthis.blur(function () { if (this.value == '') this.value = t; });
            //}
        });
    }
});
//------------------------------------------------------模块拓展--------------------------------------

//标签页
uni.tab = {
    //页面unitab对象
    sta: undefined,
    //tab 激活标签页
    selTab: function ($tab, i) {
        var h_list = $(".tab_head:first", $tab).children("li");
        h_list.removeClass("h_sel active");
        h_list.eq(i).addClass("h_sel active");
        var c_list = $(".tab_con:first", $tab).children(".item");
        c_list.removeClass("c_sel").hide();
        c_list.eq(i).addClass("c_sel").fadeIn('100');
    },
    //保留tab状态刷新
    reload: function () {
        var pars = uni.url2Obj(location.href);
        var arr = [];
        $(".unitab .tab_head").each(function (i) {
            $(this).children("li").each(function (i) {
                if ($(this).hasClass("h_sel")) {
                    arr.push(i);
                    return;
                }
            });
        });
        pars.tab = arr.join('_');
        if (pars.tabx) pars.tabx = "";
        pars.t = "_" + (new Date()).valueOf();
        var p = uni.obj2Url(pars);
        location.href = location.pathname + "?" + p;
    },
    //tab完成后触发
    evAfterTab: null,
    //初始化tab，必须执行
    initTab: function (unitab, cvt, para) {
        this.sta = true;
        $(unitab).each(function (i) {
            var tab = this;
            var head = $(".tab_head:first", tab);
            var h_list = head.children("li");
            var trigger = "click";
            if (para && para.trigger) trigger = para.trigger;
            h_list.bind(trigger, function () {
                // 操作标签
                var tabs = $(this).parent().children("li");
                tabs.removeClass("h_sel active");
                $(this).addClass("h_sel active");
                var index = $(this).attr("index");
                // 操作内容
                var con = $(this).parents(".unitab:first").find(".tab_con:first");
                var items = con.children(".item");
                items.removeClass("c_sel").hide();
                var csel = con.children(".item:eq(" + index + ")").addClass("c_sel");
                if (para && para.changeTab)//选择标签页事件
                    para.changeTab(parseInt(index), tab);
                if (para && para.fade == "close")//关闭渐入效果
                    csel.show();
                else
                    csel.fadeIn('100');
            });
            h_list.each(function (j) {
                $(this).attr("index", j);
            });
            var con = $(".tab_con:first", this);
            var c_list = con.children("div,tbody");
            c_list.addClass("item");

            var req = uni.getReq();
            var line;
            if (req["tab"]) {
                var str = req["tab"];
                line = str.split("_");
            }
            var index = 0;
            if (line && line[i]) {
                if (cvt && !isNaN(cvt[line[i]])) {
                    line[i] = cvt[line[i]];
                }
                index = parseInt(line[i]);
            }
            else {
                h_list.each(function (k) {
                    if ($(this).is(":visible")) {
                        index = k;
                        return false;
                    }
                });
            }
            if (para && para.changeTab)//选择标签页事件 首次
                para.changeTab(index, tab);
            h_list.eq(index).addClass("h_sel active");
            c_list.eq(index).addClass("c_sel").show();
        });
        uni.triggerFun(this.evAfterTab, $(".unitab"));
    }
};
//创建tab
$.fn.extend({
    //cvt 默认跳转｛url传参：索引（从0开始）｝ para 参数对象
    unitab: function (cvt, para) {
        var pthis = $(this);
        if (!para) para = {};
        pthis.addClass("unitab");
        if (!para.custom) {//不自动指定
            pthis.children("ul:first").addClass("tab_head");
            pthis.children("div:first,table:first").addClass("tab_con");
        }
        if (para.hide) {//隐藏指定索引的标签
            if (!isNaN(para.hide)) $(".tab_head li:eq(" + para.hide + ")", pthis).hide();
            else if (typeof (para.hide) == "string") {
                var hs = para.hide.split(",");
                for (var i = 0; i < hs.length; i++) {
                    if (hs[i] && !isNaN(hs[i]))
                        $(".tab_head li:eq(" + hs[i] + ")", pthis).hide();
                }
            }
        }
        if (para.pctrl) {//分页 pctrl 每页显示数目 只支持table
            var need = parseInt(para.pctrl);
            if (!isNaN(need)) {
                pthis.each(function () {
                    var tbody = $(".tab_con tbody", this);
                    var list = $(".tab_con tbody tr", this);
                    var pctrl = $(".tab_head", this);
                    var len = Math.ceil(list.length / need);
                    if (len > 0) pctrl.append("<li>1</li>");
                    for (var i = 1; i < len; i++) {
                        var min = i * need;
                        var max = min + need;
                        if (max > list.length) max = list.length;
                        var newb = $("<tbody></tbody>");
                        tbody.after(newb);
                        for (var j = min; j < max; j++) {
                            newb.append($(list[j]));
                        }
                        tbody = newb;
                        pctrl.append("<li>" + (i + 1) + "</li>");
                    }
                    if (para.pctrlFun) //翻页事件
                    {
                        this.need = need;
                        this.total = list.length;
                        this.ptotal = len;
                        para.changeTab = function (index, obj) {
                            para.pctrlFun(index, obj.need, obj.total, obj);
                        }
                    }
                });
            }
        }
        uni.tab.initTab(pthis, cvt, para);
    }
});
(function ($) {
    // 显示
    var show = function (type, info, source, fun) {
        var $source,
            // 元素
            $window = $(window),
            $body = $(document.body),
            $container,
            $messager = $('<div class="uni_messager"><span class="close">x</span></div>'),
            $icon = $('<div class="msg-icon"></div>'),
            $text = $('<span class="text">' + info + '</span>'),
            // 位置计算
            offset,
            containerWidth,
            containerHeight,
            messagerWidth,
            messagerHeight;
        if (source) {
            $source = $(source);
            if ($source.hasClass('uni_msg_con'))
                $container = $source;
            else if ($source.parents(".uni_msg_con").length > 0)
                $container = $source.parents(".uni_msg_con:first");
            else if ($source.find(".uni_msg_con").length > 0)
                $container = $source.find(".uni_msg_con:first");
            else
                $container = $body;
        }
        else if ($(".uni_msg_con:first", $body).length > 0)
            $container = $(".uni_msg_con:first", $body);
        else
            $container = $body;
        if (type === 'error') {
            $messager.removeClass('success,warning,info');
            $messager.addClass('error');
        }
        else if (type === 'success') {
            $messager.removeClass('error,warning,info');
            $messager.addClass('success');
        }
        else if (type === 'warning') {
            $messager.removeClass('error,success,info');
            $messager.addClass('warning');
        }
        else {
            $messager.removeClass('error,warning,success');
            $messager.addClass('info');
        }
        $messager.click(function () { clearTimeout(timeout_set); hide(fun); });
        $messager.append($icon, $text).appendTo($body);
        //// 监听窗口变化
        $window.on('resize', function (e) {
            offset = $container.offset(),
            containerWidth = $container.innerWidth(),
            containerHeight = $container.innerHeight(),
            messagerWidth = $messager.innerWidth(),
            messagerHeight = $messager.innerHeight();
            maxWidth = window.screen.availWidth,
            maxHeight = window.screen.availHeight;
            if (containerWidth > maxWidth) containerWidth = maxWidth;
            if (containerHeight > maxHeight) containerHeight = maxHeight;
            $messager.css({
                'top': offset.top + (containerHeight - messagerHeight) * 2 / 5 + $(document).scrollTop(),
                'left': offset.left + (containerWidth - messagerWidth) / 2 + $(document).scrollLeft()
            });
        }).resize();

        $messager.show();
        return $messager;
    },
    // 隐藏
    hide = function (fun) {
        $('.uni_messager').remove();
        if (typeof (fun) == 'function') fun();
    },
    messager = {};

    var getInterval = function (text) {
        text = uni.backText(text);
        if (text.length > 5) {
            return (Math.ceil(text.length / 5) * 1000);
        }
        else {
            return 2000;
        }
    }
    var timeout_set;
    messager.m = function (type, info, fun, source) {
        info = uni.translate(info);//翻译
        var msg = show(type, info, source, fun);
        var t = getInterval(info);
        timeout_set = setTimeout(function () { hide(fun); }, t); // 默认自动关闭
    }
    messager.success = function (info, fun, source) {
        messager.m("success", info, fun, source);
    };
    messager.error = function (info, fun, source) {
        messager.m("error", info, fun, source);
    };
    messager.warning = function (info, fun, source) {
        messager.m("warning", info, fun, source);
    };
    messager.info = function (info, fun, source) {
        messager.m("info", info, fun, source);
    };
    messager.hide = hide;
    uni.msg = messager;
})(jQuery);
//-------------------------------------------------------------------初始化 ---------------------------------------------

$(function () {
    if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
        var msViewportStyle = document.createElement("style")
        msViewportStyle.appendChild(
          document.createTextNode(
            "@-ms-viewport{width:auto!important}"
          )
        )
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle)
    }
    $("body").prepend("<div class='uni_res'>" +
        "<span id='uni_wait_icon' class='wait_icon'></span>" +
        "<div id='uni_alert' class='dialog'></div>" +
        "<div id='uni_confirm' class='dialog'></div>" +
        "<div id='uni_dlg' class='dialog'></div>" +
        "</div>").keydown(function (event) {
            if (event.keyCode == 13) {
                var dfts = $(".default:visible");
                if (dfts.length > 0) {
                    var dft;
                    var p = 0;
                    dfts.each(function (i) {
                        var pthis = $(this);
                        if (i == 0) dft = pthis;
                        var pri = pthis.attr("priority");
                        if (pri && parseInt(pri) > p) { p = parseInt(pri); dft = pthis; }
                    });
                    dft.trigger("click");
                    event.returnValue = false;
                }
            }
        });
});

//解决个人中心->信用明细->成员管理弹出框下分页控件英文显示问题单独添加方法
function SetEnlanguage(ctrl,len,count) {
    var path;
    var i = location.href.toLowerCase().indexOf("/clientweb/");
    if (i < 0) path = location.origin;
    else path = location.href.substring(0, i);
    path += "/ClientWeb/pro/";
    var url = path + "ajax/util.aspx";
    $.ajax({
        type: "get",
        url: url,
        data: "act=" + "get_language",
        async: false,
        success: function (data) {
            var rlt = JSON.parse(data);
            if (rlt.ret == 1) {
                if (rlt.data) {
                    uni.language = rlt.data;
                    var bottomTotal = uni.translate("总");
                    var bottomFirst = uni.translate("首页");
                    var pagebottom = "<div class='uni_pc_panel' onselectstart='return false'><span class='uni_pc_info'>" + bottomTotal + "<span class='uni_pc_total uni_pc_num'>" + len + "</span>" + uni.translate("条数据，分") + "<span class='uni_pc_pages uni_pc_num'>" + count + "</span>" + uni.translate("页") + " | " + uni.translate("当前第") + "<span class='uni_pc_here uni_pc_num'>0</span>" + uni.translate("页") + "</span><span class='uni_pc_ctrl' data-here='-1'><span class='uni_pc_flip uni_pc_first'>" + bottomFirst + "</span>|<span class='uni_pc_pre uni_pc_flip'>" + uni.translate("上一页") + "</span>|<span class='uni_pc_next uni_pc_flip'>" + uni.translate("下一页") + "</span>|<span class='uni_pc_flip uni_pc_last'>" + uni.translate("尾页") + "</span></span></div>";
                    //$(".tab_con").pctrl($(".tab_head"), 10);
                    ctrl.html(pagebottom);
                }
            }
            else {
                alert(rlt.msg);
            }
        }
    });
}

