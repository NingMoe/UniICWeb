
//公共方法对象
var uni = {
    //语言 语言字典对象
    language: undefined,
    //语言翻译
    translate: function (key) {
        if (!key) return "";
        if (!this.language) return key;
        var dic = this.language;
        if (dic[key] != undefined)
            return dic[key];
        else
            return key;
    },
    //获取req
    getReq: function () {
        var req = Dom7.parseUrlQuery(location.href);
        return req;
    },
    //获取哈希表
    getHash: function () {
        return new uni.hash();
    },
    //判断是否在数组内
    isInArray: function (v, arr) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == v)
                return true;
        }
        return false;
    },
    //复制对象
    getObj: function (i) {
        var objClone;
        if (!i) { uni.log('uni.getObj null'); return null; }
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
    //判断对象是否存在 仅判断是否存在 0与false将认为存在
    isNull: function (obj) {
        var type = typeof (obj);
        if (type == "undefined" || obj == null || (type != "function" && obj.length == 0))
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
    //日期字符串转Date类型
    parseDate: function (str, s) {
        if (!str || typeof (str) != 'string') return null;
        str = str.split(s || '-');
        var y = str[0];
        var M = str[1];
        str = str[2].split(' ');
        var d = str[0];
        var H, m, s;
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
    //格式化日期
    formatDate: function (fmt, date) {
        var d = date || new Date();
        if (d instanceof Date) return d.format(fmt);
        return "type error";
    },
    //比较日期 s参数为 h细化到时  m到分 默认天 返回dt1-dt2
    compareDate: function (dt1, dt2, s) {
        var date1, date2;
        if (typeof (dt1) == "string") date1 = this.parseDate(dt1);
        else date1 = this.getObj(dt1);
        if (typeof (dt2) == "string") date2 = this.parseDate(dt2);
        else date2 = this.getObj(dt2);
        if (!date1 || !date2) { uni.msgBox("compareDate error"); return; }
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
    locationPara: {
        locFlag: true,
        beginLoc: null,
        endLoc: null
    },
    getLocation: function (callback) {
        var beginLoc, endLoc;
        if (typeof (this.locationPara.beginLoc) == "function") beginLoc = this.locationPara.beginLoc;
        if (typeof (this.locationPara.endLoc) == "function") endLoc = this.locationPara.endLoc;
        if (this.locationPara.locFlag) {
            this.locationPara.locFlag = false;
            setTimeout(function () { uni.locationPara.locFlag = true; }, 1000);
            if (navigator.geolocation) {
                if (!uni.isNull(beginLoc)) { beginLoc(); }
                navigator.geolocation.getCurrentPosition(function (position) {
                    if (!uni.isNull(endLoc)) { endLoc(); }
                    var lon = position.coords.longitude;
                    var lat = position.coords.latitude;
                    callback(lon, lat);
                }, showError);
            }
            else {
                alert(uni.translate("当前应用不支持定位功能"));
            }
        }
        function showError(error) {
            if (!uni.isNull(endLoc)) { endLoc(); }
            callback();
            alert(uni.translate("获取位置时发生错误"));
        }
    },
    //两点之间距离 lat为纬度, lng为经度
    getDisance: function (lng1, lat1, lng2, lat2) {
        var dis = 0;
        var radLat1 = toRad(lat1);
        var radLat2 = toRad(lat2);
        var deltaLat = radLat1 - radLat2;
        var deltaLng = toRad(lng1) - toRad(lng2);
        var dis = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(deltaLat / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(deltaLng / 2), 2)));
        return dis * 6378137;
        function toRad(d) { return d * Math.PI / 180; }
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
    this.onSet = undefined;
    this.onRemove = undefined;
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
            if (this.onSet) this.onSet(key, value);
        }
    }
    function hashtable_remove(key) {
        var rtn = this.hashtable[key];
        var old_value = (this.hashtable[key]);
        if (this.onRemove) {
            this.onRemove(key, uni.getObj(this.hashtable[key]));
        }
        //this.hashtable.splice(key, 1);
        this.hashtable[key] = null;
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
//插入日志
uni.log = function (msg, detail) {
    if (console) {
        var dt = (new Date()).format("yyyyMMddHHmmss");
        console.log(dt + "_msg:" + msg);
        if (typeof (detail) == "string")
            console.log(dt + "_detail:" + detail);
    }
}
//消息框
uni.msgBox = uni.msgBoxR = uni.msgBoxRT = function (msg) {
    alsert(msg);
}
/*--------------------------Date对象拓展-------------------------------------*/

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
        "E": uni.translate(week[this.getDay()]),//周
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

/*------------------------------------------Dom7方法拓展---------------------------------------------*/

Dom7.trim = function (str) {
    return str.replace(/(^\s*)|(\s*$)/g, '');
}
Dom7.extend = function () {
    function _extend(dest, source) {
        for (var name in dest) {
            if (dest.hasOwnProperty(name)) {
                if ((dest[name] instanceof Object) && (source[name] instanceof Object)) {
                    _extend(dest[name], source[name]);
                }
                if (source.hasOwnProperty(name)) {
                    continue;
                } else {
                    source[name] = dest[name];
                }
            }
        }
    }
    var _result = {},
        arr = arguments;
    if (!arr.length) return {};
    for (var i = arr.length - 1; i >= 0; i--) {
        _extend(arr[i], _result);
    }
    return _result;
}
//支持Dom7的append方法
Dom7.fn.appendExt = function (v) {
    var dm = this[0];
    if (v instanceof Object) {
        for (var i = 0; i < v.length; i++) {
            dm.appendChild(v[i]);
        }
    }
}
//支持Dom7的html方法
Dom7.fn.htmlExt = function (v) {
    var dm = this[0];
    if (v instanceof Object) {
        dm.innerHTML = "";
        for (var i = 0; i < v.length; i++) {
            dm.appendChild(v[i]);
        }
    }
    return dm.innerHTML;
}
Dom7.fn.clone = function () {
    var ds = this;
    var ret = new Dom7;
    for (var i = 0; i < ds.length; i++) {
        ret.add(ds[i].cloneNode(true));
    }
    return ret;
}
Dom7.fn.siblings = function (v) {
    return this.parent().children(v);
}
Dom7.fn.after = function (v) {
    if (v instanceof Object) {
        v.insertAfter(this);
    }
    return this;
}
Dom7.fn.before = function (v) {
    if (v instanceof Object) {
        v.insertBefore(this);
    }
    return this;
}
Dom7.fn.unbind = function (v) {
    if (!v) { alert("Dom7.fn.unbind error"); return; }
    this.off(v);
}
Dom7.fn.fadeIn = function (t) {
    if (!t) t = 200;
    var i = 0;
    var target = this[0];
    target.style.cssText = "filter :alpha(opacity=0);-moz-opacity:0;opacity:0;display:block;";
    var itv = setInterval(function () {
        i++;
        target.style.cssText = "filter :alpha(opacity=" + i + ");-moz-opacity:" + i * 0.01 + ";opacity: " + i * 0.01 + ";";
        if (i == 100) { clearInterval(itv); target.style.cssText = "display:block;"; }
    }, t / 100);
    return this;
}
Dom7.fn.fadeOut = function (t) {
    if (!t) t = 200;
    var i = j = parseInt(t / 10);
    var target = this[0];
    var itv = setInterval(function () {
        i--;
        var o = i / j;
        target.style.cssText = "filter :alpha(opacity=" + o * 100 + ");-moz-opacity:" + o + ";opacity: " + o + ";";
        if (i == 0) { clearInterval(itv); target.style.cssText = "display:none;"; }
    }, 10);
    return this;
}
//cookie
Dom7.cookie = function (name, value, options) { if (typeof value == "undefined") { var cookieValue = null; if (document.cookie && document.cookie != "") { var cookies = document.cookie.split(";"); for (var i = 0; i < cookies.length; i++) { var cookie = Dom7.trim(cookies[i]); if (cookie.substring(0, name.length + 1) == name + "=") { cookieValue = decodeURIComponent(cookie.substring(name.length + 1)); break } } } return cookieValue } options = options || {}, value === null && (value = "", options.expires = -1); var expires = ""; if (options.expires && (typeof options.expires == "number" || options.expires.toUTCString)) { var date; typeof options.expires == "number" ? (date = new Date, date.setTime(date.getTime() + options.expires * 24 * 60 * 60 * 1e3)) : date = options.expires, expires = "; expires=" + date.toUTCString() } var path = options.path ? "; path=" + options.path : "", domain = options.domain ? "; domain=" + options.domain : "", secure = options.secure ? "; secure" : ""; document.cookie = [name, "=", encodeURIComponent(value), expires, path, domain, secure].join("") };
Dom7.fn.visible = function () {
    if (this.css("display") == "none" || this.attr("type") == "hidden" || (this.width() == 0 && this.height() == 0))
        return false;
    var ps = this.parents();
    for (var i = 0; i < ps.length - 1; i++) {//-1过滤document
        if (ps[i].style.display == "none" || ps[i].type == "hidden" || (ps[i].clientWidth == 0 && ps[i].clientHeight == 0)) {
            return false;
        }
    }
    return true;
}