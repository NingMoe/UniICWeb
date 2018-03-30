<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PollLine.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwAccNo" name="dwAccNo" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
            <h2 style="margin-top:10px;font-weight:bold">ͶƱ</h2>       
        <div class="tb_info">
          
    </div>
                <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNew">�½�ͶƱ</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div style="margin-top:10px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                            <th>��ʼ����:</th>
                            <td><input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td> <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                     <tr>
                 <th>״̬��</th>
                    <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwVoteStat" checked="checked">ȫ��</label>
                            <label><input class="enum" value="1" type="radio" name="dwVoteStat">δ����</label>    
                            <label><input class="enum" value="2" type="radio" name="dwVoteStat">ͶƱ��</label>
                            <label><input class="enum" value="4" type="radio" name="dwVoteStat">�ѹر�</label>
                       </td>
            </tr>
                <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="��ѯ" style="height:25px" /></th>
                        </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>                                     
                    <th>����</th>
                         <th>��ͶƱ����</th>
                        <th>��ͶƱ����</th>
                        <th>״̬</th>
                         <th>�鿴ͶƱ��Ϣ</th>
                        <th style="width:25px;" class="thCenter">����</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1" style="">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
                $(".getPollInfo").click(function () {
                    var id = $(this).prop("id");
                    $.lhdialog({
                        title: '�鿴ͶƱ��Ϣ',
                        width: '650px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/GetPollLineItem.aspx?op=set&id=' + id
                    });
                });
                $("#btnOK").button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                $("#sub").button();
               AutoUserByName($("#szTrueName"), 1, $("#dwAccNo"),null, null, null);
               AutoLab($("#szLabName"), 1, $("#dwLabID"), null, false);
                $("#sub").button();
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                      <a href="#" class="set" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a class="editInfo"  href="#" title="�༭ͼ����Ϣ"><img src="../../../themes/icon_s/16.png"/></a>\</div>');
                $(".OPTD2").html('<div class="OPTDBtn">\
                      <a href="#" class="get" title="�鿴��Ϣ"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".editInfo").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/pro/page/editContent.aspx?name=�༭ͶƱͼ�Ľ�����Ϣ&type=poll&id=" + dwLabID);
                });

                $(".del").click(function () {
                    var devKindID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                    }, '��ʾ', 1, function () { });
                  });
                $("input[name='lab'],input[name='szRoom'],input[name='szDevKinds'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $("#btnNew").click(function () {
                    var id =$(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�½�',
                        width: '650px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/NewPollLine.aspx?op=new'
                    });
                });
                $(".set").click(function () {
                    var id = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '650px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/NewPollLine.aspx?op=set&id='+id
                    });
                });
                $(".ListTbl").UniTable();
            });
        </script>
        <style>
              .getPollInfo
            {
                text-decoration: underline;                              
                color:blue;
            }
            .thCenter
            {
                text-align:center;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:8px;
            }

        </style>
    </form>
</asp:Content>
