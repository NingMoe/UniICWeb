<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindSeat.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindSeat %>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    
                   <a href="syskindSeat.aspx" id="syskindSeat"><%=ConfigConst.GCSysKindSeat%>����</a>
                    <a href="SeatRoom.aspx" id="SeatRoom"><%=ConfigConst.GCSysKindSeat%>��������</a>
                    <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="seatLab.aspx" id="seatLab">��������<%=ConfigConst.GCLabName %></a>
                    <%} %>
                    <a href="SeatKind.aspx" id="SeatKind"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="SeatClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleSeat.aspx?kind=64" id="resvrule">ԤԼ����</a>
                </div>
            </div>

            <div class="FixBtn">               
                <a id="export">����</a>
                    <a id="btnNew">�½�<%=ConfigConst.GCSysKindSeat %></a>
                <a id="newList">�����½�</a>
                <!--
                 <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>
                --> 
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
                        <th name="dwDevSN">��λ���</th>                       
                        <th name="szDevName">��λ����</th> 
                        <th name="dwDevStat">״̬</th>                       
                        <th name="szKindName">����<%=ConfigConst.GCKindName %></th>                                              
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>            
                       <!-- <th name="szLabName">����<%=ConfigConst.GCLabName %></th>-->
                        <th name="szAssertSN">�ʲ���</th>      
                        <th>���ܿ���</th>
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
                $("#export").button().click(function () {
                    $.lhdialog({
                        title: '����',
                        width: '150px',
                        height: '70px',
                        lock: true,
                        content: 'url:Dlg/DevListExport.aspx?op=new'
                    });
                });
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="print" href="#" title="�鿴��ά��"><img src="../../../themes/iconpage/film.png"/></a>\
                    <a class="setUnUse" href="#" title="����"><img src="../../../themes/iconpage/psrest.png"/></a>\
                    <a class="setUable" href="#" title="����"><img src="../../../themes/iconpage/record.png""/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".print").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var labid = $(this).parents("tr").children().first().data("labid");
                    var name = $(this).parents("tr").children().first().data("name");
                    window.open("http://update.unifound.net/wxnotice/qrcode.aspx?pcid=" + labid + "&id=" + dwDevID + "&session=Seat&msg=" + name)

                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetSeatDev.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setUnUse").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("ȷ������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=unable&id=" + devid);
                    }, '��ʾ', 1, function () { }); c

                  });
                $(".setUable").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("ȷ������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=usable&id=" + devid);
                    }, '��ʾ', 1, function () { });

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
                        content: 'url:Dlg/NewSeatDev.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '�����½�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceSeatList.aspx?op=new'
                    });
                });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false });

        </script>
    </form>
</asp:Content>
