<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WDevice.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��Դ��������</h2>
        <div>
           <div>         
                <a id="btnNew">�½�<%=ConfigConst.GCDevName %></a>
               <a id="btnFeeList">�½��շѱ�׼</a>
                      </div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
                 
            <table style="width:99%;margin-top:10px">
               <tr>
                   <th>У��:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=m_szCamp %>
                </select></td>
                   <th>¥��:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=m_szBuilding %>
                </select>
                   </td>
                     <th>����:</th>
                    <td><input type="text" name="szSearchKey" id="szSearchKey" /></td>
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
                        <th name="szAssertSN">���</th>
                         <th name="szDevName">����</th>
                        <th name="szRoomNo">�����</th>
                        <th>����</th>
                        <th name="szBuildingName">����¥��</th>
                        <th name="szCampusName">����У��</th>                     
                         <th name="szClassName">����</th> 
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
                /*
                $(".opt").css({ width: "150px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                */
                $("#btn,#newList,#btnFeeList").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setResvRuleBtn" href="#" title="�޸Ŀ���ʱ��"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="SetFeeOne" href="#" title="�޸��շѱ�׼"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="titleset" href="#" title="���ֽ���"><img src="../../../themes/icon/12.png"/></a>\
                    <a class="picset" href="#" title="ͼƬ����"><img src="../../../themes/icon/17.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="getGroup" href="#" title="�鿴��ܹ���Ա"><img src="../../../themes/icon/15.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#szCampusIDs").change(function () {
                    var campidV = $("#szCampusIDs").val();
                    $.get(
               "../data/searchBuilding.aspx",
               { campid: campidV },
               function (data) {

                   var vCamp = eval(data);
                   $('#szBuildingIDs').empty();
                   for (var i = 0; i < vCamp.length; i++) {
                       $('#szBuildingIDs').append($("<option value='" + vCamp[i].id + "'>" + vCamp[i].label + "</option>"));
                   }
               });
                });
                $(".setResvRuleBtn").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '�޸Ŀ��Ź���',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewOpenRule.aspx?op=set&dwID=' + openruleSN
                    });
                });

                $(".SetFeeOne").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�޸��շѱ�׼',
                        width: '500px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetFeeOne.aspx?op=set&dwID=' + dwDevID
                    });
                });
                
                $("#btnFeeList").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '�½��շѱ�׼',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewFeeList.aspx?op=set&dwID=' + openruleSN
                    });
                });
                $(".getGroup").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().attr("data-mangroupid");
                    $.lhdialog({
                        title: '�鿴��ܹ���Ա',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/GetClassGroupMember.aspx?op=set&GroupID=' + openruleSN
                    });
                });

                
                $(".titleset").click(function () {
                    var title = '���ֽ���';
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                   // var title = $(this).parents("tr").children().first().text();
                    window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=dev_intro&id=" + dwDevID + '&title=' + title);
                });
                $(".picset").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var title = 'ͼƬ����';
                    // var title = $(this).parents("tr").children().first().text();
                    window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=dev_intro&id=" + dwDevID + '&title=' + title);
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
                        content: 'url:Dlg/WSetDevice.aspx?op=set&id=' + dwDevID
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
                            content: 'url:Dlg/WNewDevice.aspx?op=new'
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