<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindLend.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindLend %>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">
                     <a href="syskindLend.aspx" id="syskindLend"><%=ConfigConst.GCSysKindLend%>管理</a>
                    <a href="LendRoom.aspx" id="LendRoom"><%=ConfigConst.GCSysKindLend%>所在区域</a>
                    <a href="LendKind.aspx" id="LendKind"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="LendClass.aspx" id="LendClass"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                </div>
            </div>

            <div class="FixBtn">               
                <a id="btnNew">新建<%=ConfigConst.GCDevName %></a>
                <a id="newList">批量新建</a>
                 <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>
                <select class="opt" id="room" name="room">
                    <%=m_szRoom %>
                </select>
                 <select class="opt" id="kind" name="kind">
                    <%=m_szDevKind %>
                </select>
            </div>

        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>       
                          <th name="szDevSN">设备序号</th>                        
                        <th name="szDevName">设备名</th>                        
                        <th name="szKindName">所属<%=ConfigConst.GCKindName %></th>
                        <th name="szModel">型号</th>
                        <th name="szSpecification">规格</th>                        
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>            
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th width="25px">操作</th>
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
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
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
                        content: 'url:Dlg/SetLendDev.aspx?op=set&id=' + dwDevID
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
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewLendDev.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '批量新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceLendList.aspx?op=new'
                    });
                });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false });

        </script>
    </form>
</asp:Content>
