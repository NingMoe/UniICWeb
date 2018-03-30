<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetClassGroupMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <input type="hidden" id="classID" name="classID" />
        <input type="hidden" id="accno" name="accno" />
        <div style="display:none">
          
<div id="content" >

<input type="button" id="mybtn" value="Î÷ÄÏ°¢Å£" />
	Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when 

an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic 

typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently 

with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
	<div class="popModal_footer">
		<button type="button" data-popModalBut="ok">ok</button>
		<button type="button" data-popModalBut="cancel">cancel</button>
	</div>
</div>
        </div>
       
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .tdinput {
            width: 15px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            var vTableContent = $("#ListTbl");
            //AutoClass($("#className"), 2, $("#classID"), false);
          
            $("#OK,#btnAddUser,#btnAddClass,#btnImport").button();
            $("#Cancel").button().click(Dlg_Cancel);
            $("#btnAddClass").click(function () {
                
                var vTr = $("<tr></tr>");
                var vTd=$("<td>"+$("#")+"</td>")
            });
            $("#btnAddUser").click(function () {

            });
            function GetMemberID()
            {

            }
        });
    </script>
</asp:Content>
