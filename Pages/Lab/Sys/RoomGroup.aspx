<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomGroup.aspx.cs" Inherits="Sub_Lab"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2><%=ConfigConst.GCRoomName %>��Ϲ���</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNew">�½�<%=ConfigConst.GCRoomName %>���</a></div>
        <div class="tb_btn">
               
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>����</th><th>������Ŀ</th><th>����</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
      
    </div>
    <script type="text/javascript">
      
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".delBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                debugger;
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
             });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '�½��������' ,
                    width: '660px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewRoomGroup.aspx?op=new'
                });
            });
          
            
        });
    </script>
</form>
</asp:Content>