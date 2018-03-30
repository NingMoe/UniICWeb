<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_DCS.aspx.cs" Inherits="SupSys_DCS"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2><%=szTitle %>����</h2>
    <input type="hidden" name="dcsKind" id="dcsKind" />
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewDCS">�½�</a></div>
        <div class="tb_btn">
        <!--
            <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>                          
                </fieldset>
               
            </div>
            -->
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="85px"><%=szTitle %>���</th><th><%=szTitle %>����</th><th>վ��</th><th><%=szTitle %>״̬</th><th>��ע</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
                 var kind="";
                if($('#dcsKind').val()=="1")                       
                {
                    kind="������";
                }
                else if($('#dcsKind').val()=="2")
                {
                    kind="�����";
                }
                
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDCSBtn" title="�޸�"><img src="../themes/iconpage/edit.png"/></a>\
                         <a class="delDCSBtn"  href="#" title="ɾ��"><img src="../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setDCSBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                var dwStaSN=$(this).parents("tr").children().first().attr("dwStaSN");                                  
                $.lhdialog({
                    title: '�޸�'+kind,
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDCS.aspx?op=set&dwSN=' + dwSN+'&dwDCSKind='+$('#dcsKind').val()
                });
            });
              $(".delDCSBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                var dwStaSN=$(this).parents("tr").children().first().attr("dwStaSN");            
                ConfirmBox("ȷ��ɾ��?",function(){                
			        ShowWait();
			        TabReload($("#<%=formAdvOpts.ClientID%>").serialize()+"&delID="+dwSN+"&dwStaSN="+dwStaSN);
                },'��ʾ',1,function(){});
            });
            $("#btnNewDCS").click(function () {
                $.lhdialog({
                    title: '�½�'+kind,
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_NewDCS.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>