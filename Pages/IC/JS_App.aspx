<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JS_App.aspx.cs" Inherits="Instance_JS_App" %>

//TODO：以下代码在IE6下性能极低，导致浏览器卡死。
$(function () {
    if(!IsOldIE6Version())
    {
        $("table").delegate(".lnkTeacher", "click", function () {
            $.lhdialog({
                title: '教师',
                width: '930px',
                height: '600px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/ViewForm.aspx?type=teacher&id='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });
       $("table").delegate(".lnkAccount", "click", function () {
        var id=$(this).data('id');
        if(id==null||id=="0")
        {
        return;
        }
            $.lhdialog({
                title: '个人信息',
                width: '680px',
                height: '450px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/ViewForm.aspx?type=teacher&id='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });
$("table").delegate(".RemoveMessage", "click", function () {
            $.lhdialog({
                title: '发送消息',
                width: '550px',
                height: '250px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendMessage.aspx?id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".devCtrlNoLogin", "click", function () {
            $.lhdialog({
                title: '免登录',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=52&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".DevFix", "click", function () {
            $.lhdialog({
                title: '报修',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/DevNeedFix.aspx?id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".devCtrlNeedLogin", "click", function () {
            $.lhdialog({
                title: '需要登录',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=51&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".openDoor", "click", function () {
            $.lhdialog({
                title: '远程开门',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/OpenDoor.aspx?id='+ $(this).parents("tr").children().first().data('roomid')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".devCtrlPowerup", "click", function () {
            $.lhdialog({
                title: '开机',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=11&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
   });
$("table").delegate(".devCtrlPoweroff", "click", function () {
            $.lhdialog({
                title: '关机',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=12&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
   });
$("table").delegate(".devCtrlRestart", "click", function () {
            $.lhdialog({
                title: '重启',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=13&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
$("table").delegate(".devCtrlLogout", "click", function () {
            $.lhdialog({
                title: '强制下机',
                width: '350px',
                height: '180px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/sendctrldev.aspx?type=22&id='+ $(this).parents("tr").children().first().data('id')+'&labid='+ $(this).parents("tr").children().first().data('labid')
            });
        });
        $("table").on("click", ".lnkCourse", function () {
            $.lhdialog({
                title: '课程',
                width: '500px',
                height: '150px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/ViewForm.aspx?type=course&id='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });

        $("table").on("click", ".lnkLab", function () {
            $.lhdialog({
                title:  '<%=ConfigConst.GCLabName %>',
                width: '500px',
                height: '400px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/sys/Dlg/GetLab.aspx?type=lab&dwLabID='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });

        $("table").on("click", ".lnkRoom", function () {
            $.lhdialog({
                title: '<%=ConfigConst.GCRoomName %>',
                width: '500px',
                height: '400px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/sys/Dlg/GetRoom.aspx?type=room&roomid='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });      
 $("table").on("click", ".lnkDevKind", function () {
            $.lhdialog({
                title: '<%=ConfigConst.GCKindName %>',
                width: '500px',
                height: '400px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/sys/Dlg/GetRoom.aspx?type=room&roomid='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });    
        $("table").on("click", ".lnkDevice", function () {
            //window.open('<%=MyVPath %>clientweb/xcus/zyy/devdetail.aspx?type=device&dev='+ $(this).attr('data-id'));
        });
        $("table").on("click", ".lnkUser", function () {
                var id=$(this).data('id');
                if(id==null||id==0)
                {
                return;
                }
            $.lhdialog({
                title: '<%=ConfigConst.GCRoomName %>',
                width: '500px',
                height: '400px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/ViewForm.aspx?type=user&id='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });


        $("table").on("click", ".lnkSta", function () {
            $.lhdialog({
                title: '站点',
                width: '500px',
                height: '400px',
                lock: true,
                data: Dlg_Callback,
                content: 'url:<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Dlg/ViewForm.aspx?type=user&id='+$(this).data('id')+'&msg=' + escape($(this).text())
            });
        });
}
});
