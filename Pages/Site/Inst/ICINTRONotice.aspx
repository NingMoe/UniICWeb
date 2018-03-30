<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICIntroNotice.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>通知</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
         <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                 <a  href="ICINTRONotice.ASPX" id="ICINTRONotice">通知</a>                
                <a  href="ICINTROClass.ASPX" id="ovclass">空间内容描述</a>                
                <a href="ICINTRO.ASPX" id="ov">空间概述</a>           
                </div>
            </div>
                <div class="FixBtn"><a id="btnNew">发布新通知</a></div>
        </div> 
        <div class="content">
          
        <table class="ListTbl">
            <thead>
                <tr><th>标题</th><th>日期</th><th width="25px">操作</th></tr>
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
                        <a href="#" class="set" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="del" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(function () {              
                $(".UniTab").UniTab();
               
            });
            $(".del").click(function () {                
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
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
