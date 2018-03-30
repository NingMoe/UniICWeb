<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="adminxmlcfg.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>���ز�������</h2>
            <div class="toolbar">
        <div class="tb_info">
        </div>
        <div class="FixBtn">
               <a id="btnNew" class="btnClss">�½�</a>
        </div>
    </div>
        <div style="margin-top:8px;">
        </div>
        <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <td>
                       ѡ�����ͣ�
                       <select id="kind" name="kind">
                       <option value="ResvAbsTime">����ʱ��ԤԼ</option>
                       <option value="ResvTheme">ԤԼ����</option>
                       </select></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ѡ��</th>                
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody>
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
                $("#btn,#newList,#setDevKind,#setDevRoom").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var name = $(this).parents("tr").children().first().data("fieldname");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setAdminxmlcfg.aspx?op=set&id=' + dwDevID + '&fieldName=' + name
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var name = $(this).parents("tr").children().first().data("fieldname");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwDevID + '&fieldName=' + name);
                    }, '��ʾ', 1, function () { });
                  });
                $("#btnNew").button()
                    .click(function () {
                        $.lhdialog({
                            title: '�½�',
                            width: '660px',
                            height: '520px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewAdminxmlcfg.aspx?op=new'
                        });
                    });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false,});

        </script>
    </form>
</asp:Content>