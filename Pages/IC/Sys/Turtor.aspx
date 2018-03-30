<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Turtor.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCTutorName%>����</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">�½�<%=ConfigConst.GCTutorName%></a>
                <a id="importTutorStu">����ѧ��<%=ConfigConst.GCTutorName%>��Ӧ��</a>
            </div>
            <div class="tb_btn">
            </div>
        </div>
         <input type="hidden" name="dwDeptID" id="dwDeptID" />
        <div class="toolbar" style="margin: 10px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th style="width:10%">����:</th>
                        <td style="width:30%"><input type="text" name="szTrueName" id="szTrueName" style="width:200px" /></td>
                        <td style="width:60%;text-align:left;">
                            <input type="submit" id="btn" value="��ѯ" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th >����</th>
                        <th >�绰</th>
                        <th >�ֻ�</th>
                        <th>����</th>
                        <th>��ע</th>
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
                $(".ListTbl").UniTable();
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
<a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '�½�<%=ConfigConst.GCTutorName%>',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewTutor.aspx?op=new'
                    });
                });
                $("#importTutorStu").click(function () {
                    $.lhdialog({
                        title: '����ѧ��<%=ConfigConst.GCTutorName%>��Ӧ',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importTutorStu.aspx?op=new'
                });
                  });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
        });
        </script>
        <style>
          
        </style>
    </form>
</asp:Content>
