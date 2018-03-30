cus = {};
//判断是否登录和导师审核状态
cus.isloginTu = function () {
    if (pro.isloginL()) {
        var acc = pro.acc;
        var s = acc.tsta;
        if (s == "0" || s == "4") {
            return true;
        }
        else if (s == "1") {
            uni.msgBox("你还未指定导师，不能创建。<br/>请到[<a href='User.aspx?tab=2'>用户信息</a>]页面指定导师。");
            return false;
        }
        else if (s == "5" || s == undefined) {
            uni.msgBox("获取导师审核状态失败，请尝试重新登录。");
            return false;
        }
        else {
            uni.msgBox("你还未获取导师项目实验的许可。你可以到[<a href='User.aspx?tab=2'>用户信息</a>]页面查看导师审核状态。");
            return false;
        }
    }
    else {
        return false;
    }
};