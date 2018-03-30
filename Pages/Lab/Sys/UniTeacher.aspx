<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="UniTeacher.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">排课教师管理</h2>
          <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                    <a href="uniteacher.aspx" id="deviceTab">排课教师管理</a>
                    <a href="account.aspx" id="devkindTab">人员管理</a>

                </div>
            </div>
            <div class="FixBtn"><a id="btnExport">导入排课教师</a><a id="btnNew">新建排课教师</a></div>
            <div class="tb_btn">
            </div>
        </div>

        <input type="hidden" name="dwClassID" id="dwClassID" />
        <input type="hidden" name="dwDeptID" id="dwDeptID" />        
        <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
              <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="szPID" id="szPID" /></td>
                        <th>姓名:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                      
                      <td>
                            <input type="submit" id="btn" value="查询" /></td>
                    </tr>
           </table>
               </div>
        </div>


        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLogonName">学工号</th>
                        <th name="szTrueName">姓名</th>
                        <th name="szDeptName"><%=ConfigConst.GCDeptName %></th>
                       
                        <th name="szHandPhone">手机</th>
                        <th name="szEmail">邮箱</th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="del" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDSet").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").button().click(function () {
                    $.lhdialog({
                        title: '新建排课教师',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewUniTeacher.aspx'
                    });
                });
                $("#btnExport").button().click(function () {
                    $.lhdialog({
                        title: '导入排课教师',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importUniTeracher.aspx'
                    });
                });
                $(".setAccnoInfo").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改账户',
                        width: '750px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewUniTeacher.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".del").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
                 });
                
                $("#btn").button();
              
                $(".ListTbl").UniTable();
            });

        </script>
      
    </form>
</asp:Content>
