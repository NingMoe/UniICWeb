<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WDevice.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>资源场所管理</h2>
        <div>
           <div>         
                <a id="btnNew">新建<%=ConfigConst.GCDevName %></a>
               <a id="btnFeeList">新建收费标准</a>
                      </div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
                 
            <table style="width:99%;margin-top:10px">
               <tr>
                   <th>校区:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=m_szCamp %>
                </select></td>
                   <th>楼宇:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=m_szBuilding %>
                </select>
                   </td>
                     <th>名称:</th>
                    <td><input type="text" name="szSearchKey" id="szSearchKey" /></td>
                    <th>编号:</th>
                    <td><input type="text" name="szAssertSN" id="szAssertSN" /></td>

                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>


            <div class="tb_info"></div>
          
            <div class="tb_btn">
                <!--<div class="AdvOpts" page="DeviceAdvOpts.aspx">
                    <div class="AdvLab">高级选项</div>
                </div>-->
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szAssertSN">编号</th>
                         <th name="szDevName">名称</th>
                        <th name="szRoomNo">房间号</th>
                        <th>容量</th>
                        <th name="szBuildingName">所在楼宇</th>
                        <th name="szCampusName">所在校区</th>                     
                         <th name="szClassName">类型</th> 
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
              <style>
            .tb_infoInLine table tr th{
            text-align:center;
            }
            .tb_infoInLine table tr td input{
            margin-left:5px;
            }
            .tb_infoInLine table tr td select{
            margin-left:5px;
            }
        </style>
        <script type="text/javascript">
            $(function () {
                /*
                $(".opt").css({ width: "150px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                */
                $("#btn,#newList,#btnFeeList").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setResvRuleBtn" href="#" title="修改开放时间"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="SetFeeOne" href="#" title="修改收费标准"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="titleset" href="#" title="文字介绍"><img src="../../../themes/icon/12.png"/></a>\
                    <a class="picset" href="#" title="图片介绍"><img src="../../../themes/icon/17.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="getGroup" href="#" title="查看物管管理员"><img src="../../../themes/icon/15.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#szCampusIDs").change(function () {
                    var campidV = $("#szCampusIDs").val();
                    $.get(
               "../data/searchBuilding.aspx",
               { campid: campidV },
               function (data) {

                   var vCamp = eval(data);
                   $('#szBuildingIDs').empty();
                   for (var i = 0; i < vCamp.length; i++) {
                       $('#szBuildingIDs').append($("<option value='" + vCamp[i].id + "'>" + vCamp[i].label + "</option>"));
                   }
               });
                });
                $(".setResvRuleBtn").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '修改开放规则',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewOpenRule.aspx?op=set&dwID=' + openruleSN
                    });
                });

                $(".SetFeeOne").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改收费标准',
                        width: '500px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetFeeOne.aspx?op=set&dwID=' + dwDevID
                    });
                });
                
                $("#btnFeeList").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '新建收费标准',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewFeeList.aspx?op=set&dwID=' + openruleSN
                    });
                });
                $(".getGroup").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().attr("data-mangroupid");
                    $.lhdialog({
                        title: '查看物管管理员',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/GetClassGroupMember.aspx?op=set&GroupID=' + openruleSN
                    });
                });

                
                $(".titleset").click(function () {
                    var title = '文字介绍';
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                   // var title = $(this).parents("tr").children().first().text();
                    window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=dev_intro&id=" + dwDevID + '&title=' + title);
                });
                $(".picset").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var title = '图片介绍';
                    // var title = $(this).parents("tr").children().first().text();
                    window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=dev_intro&id=" + dwDevID + '&title=' + title);
                });
                $(".setDevFar").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '设置',
                        width: '660px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevFarDetail.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setDevSample").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '设置',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevSample.aspx?op=set&id=' + dwDevID
                    });
                });

                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                     $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/WSetDevice.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setResvRule").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");

                    $.lhdialog({
                        title: '设置预约规则',
                        width: '660px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setResvRule.aspx?op=set&devID=' + dwDevID
                    });
                });
                
                $(".setFee").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var kindid = $(this).parents("tr").children().first().next().data("kindid");
                    $.lhdialog({
                        title: '收费标准',
                        width: '660px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetFeeAll.aspx?op=set&kindid=' + kindid
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var dwLabID = $(this).parents("tr").children().first().next().data("labid");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID=" + dwLabID);
                    }, '提示', 1, function () { });
                });
                $(".InfoDeviceBtn").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().children().first().val();

                    $.lhdialog({
                        title: '介绍',
                        width: '760px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwDevID + "&type=DeviceInfo"
                    });
                });
                $("#btnNew").button()
                    .click(function () {
                        $.lhdialog({
                            title: '新建',
                            width: '660px',
                            height: '520px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/WNewDevice.aspx?op=new'
                        });
                    });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '批量新建',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceList.aspx?op=new'
                    });
                });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });

        </script>
    </form>
</asp:Content>