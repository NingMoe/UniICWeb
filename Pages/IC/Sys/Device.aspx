<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Device.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>����</h2>
        <div>
            <div>
                  <!---
                 <select class="opt" id="room" name="room">
                    <%=m_szRoom %>
                </select>
                   -->
            <!--    <a id="btnNew" class="btnClss">�½�<%=ConfigConst.GCDevName %></a>-->
            </div>
              
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th><%=ConfigConst.GCLabName %>:</th>
                   <td>
                        <select class="opt" id="lab" name="lab" style="width:auto">
                    <%=m_szLab %>
                </select></td>
                   <th><%=ConfigConst.GCDevName %>��:</th>
                   <td><input type="text" name="szDevName" id="szDevName" /></td>
                    <th>���:</th>
                    <td><input type="text" name="szAssertSN" id="szAssertSN" /></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>


            <div class="tb_info"></div>
          
            <div class="tb_btn">
                <!--<div class="AdvOpts" page="DeviceAdvOpts.aspx">
                    <div class="AdvLab">�߼�ѡ��</div>
                </div>-->
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwDevSN" width="60px">���</th>
                        <th name="szAssertSN"><%=ConfigConst.GCDevName %>��</th>
                        <!--<th name="szIP">IP��ַ</th>-->
                        <!--<th name="szClassName">����<%=ConfigConst.GCClassName %></th>-->
                        <th name="szModel">�ͺ�</th>
                        <th name="szSpecification">���</th>
                        <th name="dwUnitPrice">����(Ԫ)</th>
                        <th name="dwPurchaseDate">�ɹ�����</th>
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>                      
                        <th width="25px">����</th>
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
                $(".opt").css({ width: "150px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setDevSample" href="#" title="���ò�������"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="setDevFar" href="#" title="���þ��ѷ������"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setResvRule" href="#" title="����ԤԼ����"><img src="../../../themes/icon_s/19.png"/></a>\
                    <a class="setFee" href="#" title="�շѱ�׼"><img src="../../../themes/icon_s/20.png"/></a>\
                    </div>');
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

                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");

                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDeviceandkind.aspx?op=set&id=' + dwDevID
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
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });

        </script>
    </form>
</asp:Content>