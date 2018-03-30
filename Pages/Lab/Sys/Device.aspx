<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Device.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>����</h2>

            <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                    <a href="device.aspx" id="deviceTab"><%=ConfigConst.GCDevName %>����</a>
                    <a href="devkind.aspx" id="devkindTab"><%=ConfigConst.GCKindName%>����</a>
                <a href="room.aspx" id="roomTab"><%=ConfigConst.GCRoomName%>����</a>
                <a href="lab.aspx" id="labTab"><%=ConfigConst.GCLabName%>����</a>
                <%if(ConfigConst.GroomNumMode==1) {%><a href="manGroup.aspx" id="A1">����Ա�����</a><%} %>
                </div>
        </div>
        <div class="FixBtn">

               <a id="btnNew" class="btnClss">�½�<%=ConfigConst.GCDevName %></a>
             <a id="newList" class="btnClss">�����½�<%=ConfigConst.GCDevName %></a>   
        </div>
    
    </div>
        <div style="margin-top:8px;">
                <input type="button" value="�����޸�����" id="setDevKind" />
            <input type="button" value="�����޸�<%=ConfigConst.GCRoomName %>" id="setDevRoom" />
  <input type="button" value="����ɾ��" id="btnDelList" />
 
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
                   <th title="���ơ��������ơ��ͺŹ��">�ؼ���:</th>
                   <td><input type="text" name="szSearchKey" id="szSearchKey" /></td>
                    <th>���:</th>
                    <td><input type="text" name="szDevSN" id="szDevSN" /></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwDevSN">���</th>
                         <th name="szAssertSN"><%=ConfigConst.GCDevName %>��</th>
                      <th name="szIP">IP��ַ</th>
                        <th name="szKindName">����<%=ConfigConst.GCKindName %></th>
                        <th name="szModel">�ͺ�</th>
                        <th name="szSpecification">���</th>
                        <th name="dwUnitPrice">����(Ԫ)</th>
                        <th name="dwPurchaseDate">�ɹ�����</th>
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>                      
                        <th width="25px">����</th>
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
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".setDevFar").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '����',
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
                        title: '����',
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
                        title: '�����޸�����',
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
                        title: '����ɾ��',
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
                        title: '�����޸ķ���',
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
                        title: '�޸�',
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
                        title: '����ԤԼ����',
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
                        title: '�շѱ�׼',
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
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID=" + dwLabID);
                    }, '��ʾ', 1, function () { });
                });
                $(".InfoDeviceBtn").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().children().first().val();

                    $.lhdialog({
                        title: '����',
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
                            title: '�½�',
                            width: '660px',
                            height: '520px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewDevice.aspx?op=new'
                        });
                    });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '�����½�',
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