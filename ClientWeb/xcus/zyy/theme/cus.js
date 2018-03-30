//-------------------------------------------------------------------初始化样式---------------------------------------------
$(function () {
    $(".tabs").unitab();
});
//日期字符串转Date类型
function NewDate(str) {
    return uni.parseDate(str, '-');
}
var cus = {};
cus.showLogin = function () {
    $("#dlg_login").dialog('open');
}