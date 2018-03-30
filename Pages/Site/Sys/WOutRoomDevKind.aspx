<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WOutRoomDevKind.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                  <a href="WOutRoomDev.aspx" id="WDevTeachingRoom">室外资源管理</a>
                    <a href="WOutRoomDevKind.aspx" id="WInRoomDevKind">室外资源类型管理</a>
                    <a href="WOutRoomDevLab.aspx" id="WInRoomDevLab">室外资源楼宇管理</a>
                </div>
            </div>
            <div class="FixBtn">
                <a id="btnNew" class="btnClss">新建资源类型</a>
            </div>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 5px 0px">                
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                        <tr>
                        <th name="szKindName">室内资源类型名称</th>                   
                        <th>资源数目</th>                                              
                        <th name="dwProperty">属性</th>
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
            .tb_infoInLine table tr th {
                text-align: center;
            }

            .tb_infoInLine table tr td input {
                margin-left: 5px;
            }

            .tb_infoInLine table tr td select {
                margin-left: 5px;
            }
        </style>
        <script type="text/javascript">
            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "150px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".setDev").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevSeatKind.aspx?op=set&dwClassKind=8&id=' + dwDevKind
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var dwLabID = $(this).parents("tr").children().first().next().data("labid");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                    }, '提示', 1, function () {
                    });
                });
                $("#btnNew").button()
                    .click(function () {
                        $.lhdialog({
                            title: '新建',
                            width: '660px',
                            height: '300px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/WNewOutRoomKind.aspx?op=new&dwClassKind=1025'
                        });
                    });              
            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });

        </script>
    </form>
</asp:Content>
