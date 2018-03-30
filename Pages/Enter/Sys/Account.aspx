<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">�˻�����</h2>
        <input type="hidden" name="dwClassID" id="dwClassID" />
        <input type="hidden" name="dwDeptID" id="dwDeptID" />
        <div style="float:right"><a id="btnNew">�½��˻�</a></div>    
        <div class="toolbar" style="margin: 20px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th>ѧ����:</th>
                        <td>
                            <input type="text" name="szPID" id="szPID" /></td>
                        <th>����:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                        <th><%=ConfigConst.GCDeptName %>:</th>
                        <td>
                            <input type="text" name="dwDeptName" id="dwDeptName" /></td>
                        <th>�༶:</th>
                        <td>
                            <input type="text" name="dwClassName" id="dwClassName" /></td>
                        <th>
                            <input type="submit" id="btn" value="��ѯ" /></th>
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
                        <th name="dwSex">�Ա�</th>
                        <th name="dwIdent">���</th>
                        <th name="szClassName">�༶</th>
                        <th name="szDeptName"><%=ConfigConst.GCDeptName %></th>
                       
                        <th name="szHandPhone">�ֻ�</th>
                        <th name="szEmail">����</th>
                        <th name="dwSubsidy">����</th>
                        <th>��ע</th>
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
               
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="setPasswd" title="��������"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".OPTDSet").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").button().click(function () {
                    $.lhdialog({
                        title: '�½��˻�',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAccount.aspx'
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
                        content: 'url:Dlg/NewAccount.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".setPasswd").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��������',
                        width: '550px',
                        height: '320px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setPasswd.aspx?op=set&dwID=' + dwID
                    });
                });
                
                $("#btn").button();
              
                $(".ListTbl").UniTable();
            });

        </script>
      
    </form>
</asp:Content>
