<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindLend.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindLend %>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">
                     <a href="syskindLend.aspx" id="syskindLend"><%=ConfigConst.GCSysKindLend%>����</a>
                    <a href="LendRoom.aspx" id="LendRoom"><%=ConfigConst.GCSysKindLend%>��������</a>
                    <a href="LendKind.aspx" id="LendKind"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="LendClass.aspx" id="LendClass"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                </div>
            </div>

            <div class="FixBtn">               
                <a id="btnNew">�½�<%=ConfigConst.GCDevName %></a>
                <a id="newList">�����½�</a>
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
                          <th name="szDevSN">�豸���</th>                        
                        <th name="szDevName">�豸��</th>                        
                        <th name="szKindName">����<%=ConfigConst.GCKindName %></th>
                        <th name="szModel">�ͺ�</th>
                        <th name="szSpecification">���</th>                        
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>            
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th width="25px">����</th>
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
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�޸�',
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
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID=" + dwLabID);
                    }, '��ʾ', 1, function () { });
                });
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewLendDev.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '�����½�',
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
