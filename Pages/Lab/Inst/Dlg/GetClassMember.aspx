<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetClassMember.aspx.cs" Inherits="_Default"%>



<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    12
    <div class="tb_info">
                <div class="UniTab" id="tabl">
                <a href="GetClassGroupMember.aspx" id="GetClassGroupMember">班级成员</a>
                    <a href="GetClassMember.aspx" id="GetClassMember">成员具体名单</a>
                </div>
            </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
    </style>
     <script language="javascript" type="text/javascript">
         $(function () {
             $(".UniTab").UniTab();
         });
    </script>
</asp:Content>
