<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindPC.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC%>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                     <a href="syskindPC.aspx" id="syskindPC"><%=ConfigConst.GCSysKindPC%>管理</a>
                    <a href="PCRoom.aspx" id="PCRoom"><%=ConfigConst.GCSysKindPC%>所在区域</a>
                     <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="pcLab.aspx" id="seatLab">区域所在<%=ConfigConst.GCLabName %></a>
                    <%} %>   
                    <a href="PCKind.aspx" id="PCKind"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></a>  
                                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="PCClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                    <a href="ResvRulePC.aspx?kind=128" id="resvrule">预约规则</a>
                </div>
            </div>

            <div class="FixBtn">               
                <a id="btnNew">新建<%=ConfigConst.GCDevName %></a>
                <a id="newList">批量新建</a>
               
            </div>

        </div>
        <div style="padding:15px;">
            <table class="ListTbl">
                <tr>
                    <th style="height:35px;"><%= ConfigConst.GCLabName  %></th>
                    <td><select class="opt" id="lab" name="lab"><%=m_szLab %></select></td>
                    <th>选择区域</th>
                    <td>
                      <select class="opt" id="room" name="room">
                    <%=m_szRoom %>
                </select>
                        </td>
                    <th><%=ConfigConst.GCKindName %></th>
                    <td>
                 <select class="opt" id="kind" name="kind">
                    <%=m_szDevKind %>
                </select>
                        </td>
                    <td>
                        <input type="submit" value="查询" id="btn" />
                    </td>
                </tr>
                
            </table>
            
              
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szDevSN">设备序号</th>                        
                        <th name="szPCName">计算机名</th>
                        <th name="szDevName">座位编号</th>
                        <th name="dwDevStat">状态</th>
                        <th name="szIP">IP地址</th>
                        <th name="szKindName">所属<%=ConfigConst.GCKindName %></th>
                        <th name="szModel">型号</th>
                        <th name="szSpecification">规格</th>                        
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>            
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th width="25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">

            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    /*
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                    */
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="setUnUse" href="#" title="禁用"><img src="../../../themes/iconpage/psrest.png"/></a>\
                    <a class="setUable" href="#" title="启用"><img src="../../../themes/iconpage/record.png""/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPC.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setUnUse").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定禁用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=unable&id=" + devid);
                    }, '提示', 1, function () { }); c

                }); 
                $(".setUable").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定启用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=usable&id=" + devid);
                    }, '提示', 1, function () { }); 

                   });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var dwLabID = $(this).parents("tr").children().first().next().data("labid");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID=" + dwLabID);
                    }, '提示', 1, function () { });
                });
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewPC.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '批量新建',
                        width: '820px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceList.aspx?op=new'
                    });
                });

            });
            $(".ListTbl").UniTable({ ShowCheck: false,HeaderIndex:false});

        </script>
    </form>
</asp:Content>
