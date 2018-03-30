<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DoorCtrl.aspx.cs" Inherits="SupSys_DoorCtrl" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�Ž�����</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewDoorCtrl">�½�</a></div>
        </div>
         <div class="toolbar" style="margin: 20px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th>������:</th>
                        <td>
                            <select id="dwDCSSN" name="dwDCSSN">
                                <%=m_szOutDCS %>
                                </select>
                            </td>
                        
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
                        <th name="dwCtrlSN">�Ž����</th>
                        <th name="szDCSName">���������</th>
                        <th>����������</th>
                        <th name="szRoomNo">�����</th>
                        <th>վ��</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDoorCtrlBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDoorCtrlBtn").click(function () {
                    var dwSN = $(this).parents("tr").children().first().text();
                    $.lhdialog({
                        title: '�޸Ŀ�����',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/SetDoorCtrl.aspx?op=set&dwSN=' + dwSN
                    });
                });
                $("#btnNewDoorCtrl").click(function () {
                    $.lhdialog({
                        title: '�½�������',
                        width: '750px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/SetDoorCtrl.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
