<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevKindAndClass.aspx.cs" Inherits="Sub_Device" %>

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
                <a id="btnNew" class="btnClss">�½�����</a>
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
                        <th name="szKindName"><%=ConfigConst.GCKindName %>����</th>                   
                        <th><%=ConfigConst.GCDevName %>����</th>
                            <th name="dwUsableNum" >������Ŀ</th>
                        <th name="szModel">�ͺ�</th>
                        <th name="szSpecification">���</th>
                        <th name="dwNationCode">������</th>
                        <th name="szProducer">��������</th>
                        <th name="dwProperty">����</th>
                        <th width="25px">����</th>
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
                $(".opt").css({ width:"150px" }).change( function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
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
                        content: 'url:Dlg/SetDevKindAndClass.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var dwLabID = $(this).parents("tr").children().first().next().data("labid");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID="+dwLabID);
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
                        content: 'url:Dlg/NewDevKindAndClass.aspx?op=new'
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