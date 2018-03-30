<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>供应商</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
     
        <div class="toolbar">
               <div class="tb_info">
                  <div class="UniTab" id="tabl">
                   <a href="assertlist.aspx" id="assertlist">设备资产列表</a>
                   <a href="AssertCodeTable.aspx?dwCodeType=5" id="dwCodeType5">用途</a>
                        <%if (nIsAdminSup == 1){%><a href="assertDevCls.aspx" id="assertDevCls">类型</a><%} %>
                    <a href="company.aspx" id="company">供应商</a>
                </div>
            </div>

            <div id="btnDevCls" class="FixBtn"><a>新建供应商</a></div>
           
        </div>
          <div  class="tb_infoInLine"  style="margin-top:5px;margin-left:0px; margin-right:0px;margin-bottom:10px;">
            <table style="margin:0px auto;border:1px solid #d1c1c1;width:99%">    
                    <tr>
                   <th>资产名称:</th>
                   <td>
                     <input type="text" id="szDevName" name="szDevName" />
                     </td>
                      
                        <th>
                            类型
                        </th>
                        <td>
                            <select id="dwComKind" name="dwComKind">
                                <%=m_kind %>
                            </select>
                        </td>
                       
                         <td colspan="4" style="text-align:center"><input type="submit" id="btn" value="查询" />   </td>
                   </tr>
                </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>联系人</th>
                        <th>类型</th>
                        <th>手机</th>
                        <th>电话</th>
                        <th width="25px">操作</th>
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
                $("#btn").button();
                function fAllOp(op) {

                }                              
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a class="DelbtnDevKind" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });             
                $("#btnDevCls").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewCompay.aspx?op=new'
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '780px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newCompay.aspx?op=set&dwLabID=' + dwDevKind
                    });
                });
                $(".DelbtnDevKind").click(function () {                    
                    var devKindID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                }, '提示', 1, function () { });
                 });
                $(".ListTbl").UniTable({ HeaderIndex: false });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
