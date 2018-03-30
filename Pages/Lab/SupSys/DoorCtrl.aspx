<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DoorCtrl.aspx.cs" Inherits="SupSys_DoorCtrl" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>门禁管理</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewDoorCtrl">新建</a></div>
        </div>
         <div class="toolbar" style="margin: 20px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th>集控器:</th>
                        <td>
                            <select id="dwDCSSN" name="dwDCSSN">
                                <%=m_szOutDCS %>
                                </select>
                            </td>
                        
                        <th>
                            <input type="submit" id="btn" value="查询" /></th>
                    </tr>
                </table>
            </div>
            
        </div>

        <div class="content">

            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwCtrlSN">门禁编号</th>
                        <th name="szDCSName">集控器编号</th>
                        <th>集控器名称</th>
                        <th name="szRoomNo">房间号</th>
                        <th>站点</th>
                        <th>备注</th>
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
                $(".ListTbl").UniTable();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDoorCtrlBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDoorCtrlBtn").click(function () {
                    var dwSN = $(this).parents("tr").children().first().text();
                    $.lhdialog({
                        title: '修改控制器',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/SetDoorCtrl.aspx?op=set&dwSN=' + dwSN
                    });
                });
                $("#btnNewDoorCtrl").click(function () {
                    $.lhdialog({
                        title: '新建控制器',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/SetDoorCtrl.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
