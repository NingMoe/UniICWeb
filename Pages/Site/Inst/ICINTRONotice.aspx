<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICIntroNotice.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>֪ͨ</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
         <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                 <a  href="ICINTRONotice.ASPX" id="ICINTRONotice">֪ͨ</a>                
                <a  href="ICINTROClass.ASPX" id="ovclass">�ռ���������</a>                
                <a href="ICINTRO.ASPX" id="ov">�ռ����</a>           
                </div>
            </div>
                <div class="FixBtn"><a id="btnNew">������֪ͨ</a></div>
        </div> 
        <div class="content">
          
        <table class="ListTbl">
            <thead>
                <tr><th>����</th><th>����</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>    
      
    </div>
        <style>
           
        </style>
        <script type="text/javascript">
            $("#btnNew").button();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="set" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="del" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(function () {              
                $(".UniTab").UniTab();
               
            });
            $(".del").click(function () {                
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
              });
            $("#btnNew").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=notice&id="+<%=szTimeID%>);
            });
            $(".set").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                var title = $(this).parents("tr").children().first().text();
                window.open("../../../ClientWeb/editContent.aspx?h=400&w=720&type=notice&id="+dwID+'&title='+title);
              });
        </script>
    </form>
</asp:Content>
