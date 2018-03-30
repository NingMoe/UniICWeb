<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResvRulePC.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>ԤԼ����</h2>
     <div class="toolbar">
           <div class="tb_info">
              <div class="UniTab" id="tabl">
                <a href="syskindPC.aspx" id="syskindPC"><%=ConfigConst.GCSysKindPC%>����</a>
                    <a href="PCRoom.aspx" id="PCRoom"><%=ConfigConst.GCSysKindPC%>��������</a>
                     <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="pcLab.aspx" id="seatLab">��������<%=ConfigConst.GCLabName %></a>
                    <%} %>   
                    <a href="PCKind.aspx" id="PCKind"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></a>  
                                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="PCClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="ResvRulePC.aspx?kind=128" id="resvrule">ԤԼ����</a>
            </div>
        </div>
         <input type="hidden" id="kind" name="kind" />
        <div class="FixBtn"><a id="btnResvRule">�½�ԤԼ����</a></div>
        </div>
            <div class="content">
                <table class="ListTbl">
                    <thead>
                        <tr>
                            <th name="szRuleName">��������</th>
                            <th name="dwIdent">���</th>
                            <th>ԤԼʱ��</th>
                            <th>ԤԼ����ȡ��</th>
                            <th>��ԤԼʱ�䷶Χ</th>
                            <th>����</th>
                            <th style="width:25px;"></th>
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
                    $(".UniTab").UniTab();
                    $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\<%if (ConfigConst.GCDebug == 1)
                                                                                                                    {%><a class="CopyResvRule"  href="#" title="���Ƹ������豸"><img src="../../../themes/iconpage/edit.png"/></a>\<%}%></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    var vKind = $("#kind").val();
                    $.lhdialog({
                        title: '�½�',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewResvRule.aspx?op=new&kind='+vKind
                    });
                });
                $(".CopyResvRule").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���ƹ���������豸',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CopyResvRule.aspx?op=set&dwID=' + dwLabID
                    });
                });

                $(".setResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    var vKind = $("#kind").val();
                  
                    $.lhdialog({
                        title: '�޸�',
                        width: '920px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetResvRule.aspx?op=set&dwID=' + dwLabID+'&kind='+vKind
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                    }, '��ʾ', 1, function () { });
                });
                //$(".ListTbl").UniTable();
            });
            </script>
    </form>
</asp:Content>
