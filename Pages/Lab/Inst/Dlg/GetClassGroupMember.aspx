<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetClassGroupMember.aspx.cs" Inherits="_Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
  <div class="tb_info" style="margin-bottom:10px;">
                <div class="UniTab" id="tabl">
                <a href="GetClassGroupMember.aspx" id="GetClassGroupMember">�༶��Ա</a>
                    <a href="GetClassMember.aspx" id="GetClassMember">��Ա��������</a>
                </div>
            </div>
    <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>                                        
                        <th>����</th>  
                        <th>˵��</th>  
                        <th class="thCenter" style="width:25px">����</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
        </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script language="javascript" type="text/javascript" src="<%=MyVPath %>themes/js/MainJScript.js"></script>
    <style type="text/css">
       
    </style>
     <script language="javascript" type="text/javascript">
         $(function () {
            $(".UniTab").UniTab();
             $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="del" title="ɾ��"><img src="../../../../themes/iconpage/del.png""/></a>\</div>');
             $(".OPTDBtn").UIAPanel({
                 theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
             });
             $(".del").click(function () {
                 var memberid = $(this).parents("tr").data("id");
                 var kindid = $(this).parents("tr").data("kindid");
                 ConfirmBox("ȷ��ɾ��?", function () {
                 });
             });
         });
    </script>
</asp:Content>
