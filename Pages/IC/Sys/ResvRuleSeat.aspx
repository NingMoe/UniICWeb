<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResvRuleSeat.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>预约规则</h2>
     <div class="toolbar">
           <div class="tb_info">
              <div class="UniTab" id="tabl"> 
                   <a href="syskindSeat.aspx" id="syskindSeat"><%=ConfigConst.GCSysKindSeat%>管理</a>
                    <a href="SeatRoom.aspx" id="SeatRoom"><%=ConfigConst.GCSysKindSeat%>所在区域</a>
                    <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="seatLab.aspx" id="seatLab">区域所在<%=ConfigConst.GCLabName %></a>
                    <%} %>
                    <a href="SeatKind.aspx" id="SeatKind"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="SeatClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleSeat.aspx?kind=64" id="resvrule">预约规则</a>
            </div>
        </div>
         <input type="hidden" id="kind" name="kind" />
        <div class="FixBtn"><a id="btnResvRule">新建预约规则</a></div>
        </div>
            <div class="content">
                <table class="ListTbl">
                    <thead>
                        <tr>
                            <th name="szRuleName">规则名称</th>
                            <th name="dwIdent">身份</th>
                            <th>预约时长</th>
                            <th>预约不来取消</th>
                            <th>可预约时间范围</th>
                            <th>限制</th>
                            <th style="width:25px;"></th>
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
                    $(".ListTbl").UniTable();
                    $(".UniTab").UniTab();
                    $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\<%if (ConfigConst.GCDebug == 1)
                                                                                                                    {%><a class="CopyResvRule"  href="#" title="复制给其他设备"><img src="../../../themes/iconpage/edit.png"/></a>\<%}%></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    var vKind = $("#kind").val();
                    $.lhdialog({
                        title: '新建',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewResvRule.aspx?op=new&kind='+vKind
                    });
                });
                $(".CopyResvRule").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '复制规则给其他设备',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CopyResvRule.aspx?op=set&dwID=' + dwLabID
                    });
                });

                $(".setResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    var vKind = $("#kind").val();
                  
                    $.lhdialog({
                        title: '修改',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetResvRule.aspx?op=set&dwID=' + dwLabID+'&kind='+vKind
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                    }, '提示', 1, function () { });
                });
                //$(".ListTbl").UniTable();
            });
            </script>
    </form>
</asp:Content>
