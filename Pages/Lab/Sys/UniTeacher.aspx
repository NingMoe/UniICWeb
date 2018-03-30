<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="UniTeacher.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">�ſν�ʦ����</h2>
          <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                    <a href="uniteacher.aspx" id="deviceTab">�ſν�ʦ����</a>
                    <a href="account.aspx" id="devkindTab">��Ա����</a>

                </div>
            </div>
            <div class="FixBtn"><a id="btnExport">�����ſν�ʦ</a><a id="btnNew">�½��ſν�ʦ</a></div>
            <div class="tb_btn">
            </div>
        </div>

        <input type="hidden" name="dwClassID" id="dwClassID" />
        <input type="hidden" name="dwDeptID" id="dwDeptID" />        
        <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
              <tr>
                        <th>ѧ����:</th>
                        <td>
                            <input type="text" name="szPID" id="szPID" /></td>
                        <th>����:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                      
                      <td>
                            <input type="submit" id="btn" value="��ѯ" /></td>
                    </tr>
           </table>
               </div>
        </div>


        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLogonName">ѧ����</th>
                        <th name="szTrueName">����</th>
                        <th name="szDeptName"><%=ConfigConst.GCDeptName %></th>
                       
                        <th name="szHandPhone">�ֻ�</th>
                        <th name="szEmail">����</th>
                        <th style="width: 25px"></th>
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
                        <a href="#" class="setAccnoInfo" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="del" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDSet").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").button().click(function () {
                    $.lhdialog({
                        title: '�½��ſν�ʦ',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewUniTeacher.aspx'
                    });
                });
                $("#btnExport").button().click(function () {
                    $.lhdialog({
                        title: '�����ſν�ʦ',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importUniTeracher.aspx'
                    });
                });
                $(".setAccnoInfo").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸��˻�',
                        width: '750px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewUniTeacher.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".del").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
                 });
                
                $("#btn").button();
              
                $(".ListTbl").UniTable();
            });

        </script>
      
    </form>
</asp:Content>
