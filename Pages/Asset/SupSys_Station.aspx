<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_Station.aspx.cs" Inherits="Sub_Station"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>վ�����</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewStation">�½�վ��</a></div>
      
        <div class="tb_btn">
        <!--
            <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>
                <fieldset><legend>���</legend>
                    <label><input name="kind" value="1" type="checkbox" />���1</label>  <label><input name="kind2" value="2" type="checkbox" />���2</label>
                </fieldset>
                <fieldset><legend>״̬</legend>
                    <label><input name="kind1" value="1" type="checkbox" />������</label>  <label><input name="kind4" value="2" type="checkbox" />δ����</label>
                </fieldset>
            </div>
            -->
        </div>
       
    </div>
   
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">���</th><th>����</th><th>ϵͳ</th><th><%=ConfigConst.GCDeptName %></th><th>����Ա</th><th>ֵ��Ա</th><th>״̬</th><th>��ע</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setStationBtn" title="�޸�"><img src="../themes/iconpage/edit.png"/></a>\
                         <a class="delStationBtn"  href="#" title="ɾ��"><img src="../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                $.lhdialog({
                    title: '�޸�վ��',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetStation.aspx?op=set&dwStaSN=' + dwSN
                });
            });
             $(".delStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                ConfirmBox("ȷ��ɾ��վ��?",function(){                
			        ShowWait();
			        TabReload($("#<%=formAdvOpts.ClientID%>").serialize()+"&delID="+dwSN);
                },'��ʾ',1,function(){});
            });
            $("#btnNewStation").click(function () {
                $.lhdialog({
                    title: '�½�վ��',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_NewStation.aspx?op=new'
                });
            });                                     
        });
    </script>
</form>
</asp:Content>