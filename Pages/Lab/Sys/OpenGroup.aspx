<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="OpenGroup.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>开放人员</h2>
    <div class="toolbar">
        <div class="tb_info">
              <div class="UniTab" id="tabl">
                  <a href="OpenRule.aspx">开放规则</a>
                <a href="OpenGroup.aspx">开放对象</a>
                <a href="holiday.aspx">节假日</a>
            </div>
        </div>
        <div class="FixBtn"><a id="btnResvRule">新建开放对象</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th>课程名称:</th>
                   <td><select id="dwKind" name="dwKind">
                      
                        <option value="8192">开放规则组</option>
                        <option value="2048">特殊(保洁人员)组</option>
                       </select></td>
                   
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>规则名称</th><th>成员</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>       
    </div>
    <script type="text/javascript">
        $(function () {
            $(".UniTab").UniTab();
            $("#btn").button();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改/添加成员"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建开放对象',
                    width: '620px',
                    height: '200px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/newopenGroup.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {
                
                var dwGroup = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改开放对象',
                    width: '620px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setopenGroup.aspx?op=set&dwID=' + dwGroup
                });
            });
            $(".delResvRuleBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '提示', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>