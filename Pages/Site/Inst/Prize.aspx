<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Prize.aspx.cs" Inherits="Sub_Lab"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2><%=szPrizeName %>����</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                 <a href="Prize.aspx?type=1">�񽱹���</a><a href="Prize.aspx?type=2">ר������</a><a href="Prize.aspx?type=4">���ļ���</a><a href="Prize.aspx?type=8">���ķ���</a><a href="Prize.aspx?type=16">�̲�</a>
            </div>
        </div>
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNew">¼��<%=szPrizeName %></a></div>
        <div class="tb_btn">
               
        </div>
        
    </div>
    <div style="margin-top:10px;margin-bottom:25px;text-align:center;width:99%">
        <input type="hidden" id="hiddenType" runat="server" />
                <div class="tb_info" style="margin:0 auto;">
                    <table>                      
                        <tr>
                            <td class="thHead" style="width:80px">��ѯ����:</td>
                            <td colspan="3" class="context2" style="width:450px">��ʼ���ڣ�<input type="text" name="dwStartDate" id="dwStartDate" runat="server" readonly="true" />
                                ��������:<input type="text" name="dwEndDate" id="dwEndDate" runat="server" readonly="true" />
                                <input type="submit" id="btnOK" value="��ѯ" />
                            </td>
                        </tr>
                    </table>
                </div>
            
    </div>
    <div class="content" style="margin-top:60px;">
        <table class="ListTbl">
            <thead>
                 <tr><th>��ˮ��</th><th><%=szPrizeName %>����</th><th><%=szPrizeName %>����</th><th>��֤����</th><th><%=szPrizeName %>����</th><th>֤����</th><th>����������Ŀ</th><th width="25px">����</th></tr>
                    </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
        <uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <style>
         .tb_info table td
            {
                border-left: 1px solid #000;
                border-right: 1px solid #000;
                height: 30px;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
            }

            .tb_info table th
            {
                border-left: 1px solid #000;
                border-right: 1px solid #000;
                height: 30px;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
            }
    </style>
    <script type="text/javascript">
      
        $(function () {
            $(".UniTab").UniTab();
            $("#btnOK").button();
            $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
            });
            $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });           
            $(".setBtn").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                var szType = $("#<%=hiddenType.ClientID%>").val();
                $.lhdialog({
                    title: '�޸�',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setPrize.aspx?op=set&id=' + dwLabID+'&type='+szType
                });
            });
          
            $(".delLabBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
             });
            $("#btnNew").click(function () {
             
                var szType=$("#<%=hiddenType.ClientID%>").val();
                $.lhdialog({
                    title: '�½�'+'<%=szPrizeName %>',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/newPrize.aspx?op=new&type=' + szType
                });
            });
            $(".ListTbl").UniTable();
            
        });
    </script>
</form>
</asp:Content>