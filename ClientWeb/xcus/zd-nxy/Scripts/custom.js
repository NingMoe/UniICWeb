$(function () {
    $("[title]").tooltip();
    $('.button').button();
    //子tr和li元素双色交替背景列表
    $('.zebra').children('tr,li').each(function (i) {
        if (i%2==0) {
            $(this).css('background', '#edf2f5');
        }
        else {
            $(this).css('background', '#fafafa');
        }
    });
});

//全局对象 全局方法
var cus = new Object();
cus.isTutor = function (i) {
    return (parseInt(i) & 1048576) > 0;
}
cus.cutStr = function (s,n) {
    s=s+'';
    if (s.length > n) {
        s = s.substr(0, n - 1) + '...';
    }
}
cus.cutStr = function (s, n) {
    s = s + '';
    if (s.length > n) {
        s = s.substr(0, n - 1) + '...';
    }
    return s;
}
cus.cutStrT = function (s,n) {
    s = s + '';
    if (s.length > n) {
        s ="<span title='"+s+"'>" +s.substr(0, n - 1) + '...</span>';
    }
    return s;
}
var cutStrT = cus.cutStrT;//*
cus.ckEmail = function (str) {
    var regex = /[_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+$/;
    return regex.test(str);
}
cus.ckMobil = function (str) {
    return (str.length == 11);
}

function check_email(str) {
    var regex = /[_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+$/;
    return regex.test(str);
}

function CheckMobile(str) {
    debugger;
    return (str.length == 11);
}

function IsLogin() {
    if ($("#cur_logonname").val() == "") {
        return false;
    } else {
        return true;
    }
}
function isloginL() {
    if (!IsLogin()) {
        $("#logindialog").dialog('open');
        return false;
    }
    else {
        return true;
    }
};
//判断是否登录和导师审核状态
function isloginTu() {
    if (!IsLogin()) {
        $("#logindialog").dialog('open');
        return false;
    }
    else {
        var idt = $("#cur_ident").val();
        if ((parseInt(idt) & 512) > 0) return true;
        var s = $("#cur_tsta").val();
        if (s == "0" || s == "4") {
            return true;
        }
        else if (s == "1") {
            MessageBox("你还未指定导师，不能预约。<br/>请到[<a href='UserCenter.aspx?act=info'>个人信息</a>]页面指定导师。");
            return false;
        }
        else if (s == "5" || s == undefined) {
            MessageBox("获取导师审核状态失败，请尝试重新登录。");
            return false;
        }
        else {
            MessageBox("你还未获取导师课题实验的许可。你可以到[<a href='UserCenter.aspx?act=info'>个人信息</a>]页面查看导师审核状态。");
            return false;
        }
    }
};

function actByLogin(sucFun, failFun) {
    var is = false;
    $.ajax({
        type: "GET",
        timeout: 5000,
        url: "Ajax_Code/account.aspx?act=islg",
        dataType: "json",
        success: function (object) {
            if (object.MsgId == 0) {
                sucFun();
            }
            else {
                failFun(0);
            }
        },
        error: function () {
            failFun(1);
        }
    });
}
function ShowWait() {
    $("#wait_icon").css('display', 'block');
}
function HideWait() {
    $("#wait_icon").css('display', 'none');
}

//获取关键字
function QueryString() {
    var name, value, i;
    var str = location.href;
    var num = str.indexOf("?")
    str = str.substr(num + 1);
    var arrtmp = str.split("&");
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = value;
        }
    }
}

//Form Ajax
function getFormJson() {
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
}
//日期字符串转Date类型
function NewDate(str) {
    str = str.split('-');
    y = str[0];
    M = str[1];
    str = str[2].split(' ');
    d = str[0];
    H = str[1].split(':')[0] || 0;
    m = str[1].split(':')[1] || 0;
    s = str[1].split(':')[2] || 0;
    var date = new Date();
    date.setUTCFullYear(y, M - 1, d);
    date.setUTCHours(H, m, s, 0);
    return date;
}
//浏览器相关
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

//提示框相关
function MessageBox(msg, title) {
    var dlg = $("#alertL");
    if (title != undefined) {
        dlg.parent().find(".ui-dialog-titlebar").find(".ui-dialog-title").html(title);
    }
    var con = msg || "";
    dlg.html("<p>" + con + "</p>");
    dlg.dialog({
        modal: false,
        buttons: {
            确定: function () {
                $(this).dialog("close");
            }
        }
    });
}
function MsgBoxR(msg, title) {
    var dlg = $("#alertL");
    if (title != undefined) {
        dlg.parent().find(".ui-dialog-titlebar").find(".ui-dialog-title").html(title);
    }
    var con = msg || "";
    dlg.html("<p>" + con + "</p>");
    dlg.dialog({
        modal: false,
        close: function () {
            location.reload();
        },
        buttons: {
            确定: function () {
                $(this).dialog("close");
            }
        }
    });
}

function DoDel(fnAction) {
    ConfirmBox("请确定要删除吗？", fnAction);
}

function DoSet(fnAction) {
    ConfirmBox("请确定要修改吗？", fnAction);
}

function ConfirmBox(msg, actFun, bkFun, title) {

    var dlg = $("#confirmL");
    if (title != undefined) {
        dlg.parent().find(".ui-dialog-titlebar").find(".ui-dialog-title").html(title);
    }
    var con = msg || "";
    dlg.html("<p>" + con + "</p>");
    dlg.dialog({
        modal: true,
        buttons: {
            确定: function () {
                dlg.dialog("close");
                actFun();
            },
            返回: function () {
                dlg.dialog("close");
                bkFun();
            }
        }
    });
}

//浮点转两位小数字符串
function GetInt2(s) {
    s = s.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        return s + ".00";
    }
    if (rs > 0) {
        if ((s.length - rs) >= 2) {
            s = s.substring(0, rs + 3);
        }
        else { s = s.substring(0, rs + 2); }
    }
    return s;
}
//获取费用
function GetFee2(uintFee, uintTime, totalTime, k) {
    if (totalTime <= 0) {
        return 0;
    }
    if (k != undefined && !isNaN(k)) {
        return parseFloat((GetFee2(uintFee, uintTime, k) * (totalTime / k)).toFixed(2));
    }
    else {
        var vUintfee = parseFloat(uintFee);
        var vUintTime = parseFloat(uintTime);
        var vTotalTime = parseFloat(totalTime);
        return parseFloat((vUintfee * vTotalTime / vUintTime).toFixed(2));
    }
}

//日期格式化
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "H+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
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



