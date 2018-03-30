<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="stockakingDetail.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�̵���ϸ</h2>
         <div class="tb_info" style="margin-bottom:15px">
            <div class="UniTab" id="tabl">
                <a href="stockaking.aspx" id="stockaking">�̵�ƻ�</a>
                <a href="stockakingDetail.aspx" id="stockakingDetail">�̵���ϸ</a>
            </div>
              </div>
       
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th>�̵�ƻ�:</th>
                   <td><select name="dwSTID" id="dwSTID"><%=sz_Stocking %></select></td>
                   
                    <td><input type="submit" id="btn" value="��ѯ" /></td>
                </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>�ʲ����</th>
                        <th>�ʲ�����</th>
                        <th>����(Ԫ)</th>
                        <th>��������</th>
                         <th>����ʵ����</th>
                        <th>��������</th>
                        <th>�̵�Ա</th>
                        <th>�̵�״̬</th>
                        <th>�̵�����</th>
                        <th>����</th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="stnormal" href="#" title="�̵�����"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="stfail" title="�̵��쳣"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btn").button();
         
            $(".setLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ���̵��Ѿ�����?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=over&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
            
            $(".stnormal").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    var dwDevID = $(this).parents("tr").children().first().attr("data-devid");
                   
                ConfirmBox("ȷ���ʲ�����?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=setNormail&delID=" + dwID + '&devid=' + dwDevID);
                }, '��ʾ', 1, function () { });
            });
                $(".stfail").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    var dwDevID = $(this).parents("tr").children().first().attr("data-devid");
                    $.lhdialog({
                        title: '�����̵��쳣',
                        width: '600px',
                        height: '350px',
                        lock: true,
                        content: 'url:Dlg/SetSTDetailStaus.aspx?op=set&delID=' + dwID + '&devid=' + dwDevID
                    });
                });

            $(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
