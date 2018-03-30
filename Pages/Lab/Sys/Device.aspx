<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Device.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>管理</h2>

            <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                    <a href="device.aspx" id="deviceTab"><%=ConfigConst.GCDevName %>管理</a>
                    <a href="devkind.aspx" id="devkindTab"><%=ConfigConst.GCKindName%>管理</a>
                <a href="room.aspx" id="roomTab"><%=ConfigConst.GCRoomName%>管理</a>
                <a href="lab.aspx" id="labTab"><%=ConfigConst.GCLabName%>管理</a>
                <%if(ConfigConst.GroomNumMode==1) {%><a href="manGroup.aspx" id="A1">管理员组管理</a><%} %>
                </div>
        </div>
        <div class="FixBtn">

               <a id="btnNew" class="btnClss">新建<%=ConfigConst.GCDevName %></a>
             <a id="newList" class="btnClss">批量新建<%=ConfigConst.GCDevName %></a>   
        </div>
    
    </div>
        <div style="margin-top:8px;">
                <input type="button" value="批量修改类型" id="setDevKind" />
            <input type="button" value="批量修改<%=ConfigConst.GCRoomName %>" id="setDevRoom" />
  <input type="button" value="批量删除" id="btnDelList" />
 
        </div>
        <input type="hidden" id="szRoomIDs" name="szRoomIDs" />
        <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th><%=ConfigConst.GCLabName %>:</th>
                   <td><select id="lab" name="lab" style="width:auto"><%=m_szLab %></select></td>
                   <th><%=ConfigConst.GCRoomName %>:</th>
                   <td><input type="text" id="szRoomName" name="szRoomName" /></td>
                   <th title="名称、类型名称、型号规格">关键字:</th>
                   <td><input type="text" name="szSearchKey" id="szSearchKey" /></td>
                    <th>编号:</th>
                    <td><input type="text" name="szDevSN" id="szDevSN" /></td>
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwDevSN">编号</th>
                         <th name="szAssertSN"><%=ConfigConst.GCDevName %>名</th>
                      <th name="szIP">IP地址</th>
                        <th name="szKindName">所属<%=ConfigConst.GCKindName %></th>
                        <th name="szModel">型号</th>
                        <th name="szSpecification">规格</th>
                        <th name="dwUnitPrice">单价(元)</th>
                        <th name="dwPurchaseDate">采购日期</th>
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>                      
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody>
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

                AutoRoom($("#szRoomName"), 1, $("#szRoomIDs"), null, $("#lab"));
                
                var tabl = $(".UniTab").UniTab();
              

                $("#lab").change(function () {
                    $("#szRoomName").val("");
                    $("#szRoomIDs").val("");
                    AutoRoom($("#szRoomName"), 1, $("#szRoomIDs"), null, $("#lab"));
                });
                $("#btn,#newList,#setDevKind,#setDevRoom").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
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

                $("#setDevKind").click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            var vid=$(this).parents("td").data("id");
                            vSelectName = vSelectName + vid+",";
                        }
                    });

                    if (vSelectName == "") {
                        return;
                    }
                    
                    $.lhdialog({
                        title: '批量修改类型',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setDevKindList.aspx?op=set&id=' + vSelectName
                    });
                });
                $("#btnDelList").button().click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            var vid = $(this).parents("td").data("id");
                            vSelectName = vSelectName + vid + ",";
                        }
                    });

                    if (vSelectName == "") {
                        return;
                    }
                    $.lhdialog({
                        title: '批量删除',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/DelDevList.aspx?op=set&id=' + vSelectName
                    });
                });
                

                $("#setDevRoom").click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            var vid = $(this).parents("td").data("id");
                            vSelectName = vSelectName + vid + ",";
                        }
                    });

                    if (vSelectName == "") {
                        return;
                    }

                    $.lhdialog({
                        title: '批量修改房间',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setDevRoomList.aspx?op=set&id=' + vSelectName
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
                        content: 'url:Dlg/setDevice.aspx?op=set&id=' + dwDevID
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
                            content: 'url:Dlg/NewDevice.aspx?op=new'
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
            $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false,});

        </script>
    </form>
</asp:Content>