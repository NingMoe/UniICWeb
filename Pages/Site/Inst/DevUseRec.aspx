<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUseRec.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=ConfigConst.GCDevName %>使用记录</h2>
        <input type="hidden" id="szGetKey" name="szGetKey" />   
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                   <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>日常管理</a>
                <a href="DevUseRec.aspx" id="devUseRec"><%=ConfigConst.GCDevName %>使用记录</a>
                <a href="DevTestData.aspx" id="devTestData"><%=ConfigConst.GCDevName %>实验数据</a>
                </div>
            </div>
        </div>
       <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>开始日期:</th>
                            <td><input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>结束日期:</th>
                            <td> <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>学工号:</th>
                            <td><input type="text" name="dwPID" id="dwPID" /></td>
                            <th><%=ConfigConst.GCDevName %>名称:</th>
                            <td><input type="text" name="devName" id="devName" style="width:180px" /></td>
                          
                        </tr>
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="查询" style="height:25px" /></th>
                        </tr>
                    </table>
             
            </div>
       </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="dwDevSN"><%=ConfigConst.GCDevName%>编号</th>
                        <th><%=ConfigConst.GCDevName%>名称</th>
                        <th><%=ConfigConst.GCClassName%>名称</th>
                        <th>管理员</th>
                        <th>所属<%=ConfigConst.GCRoomName %></th>
                      <!--  <th>所属<%=ConfigConst.GCLabName %></th>-->
                        <th>使用人</th>
                        <th><%=ConfigConst.GCTutorName%></th>
                        <th><%=ConfigConst.GCDeptName %></th>
                        <th name="dwBeginTime">使用时间</th>
                        <th name="dwUseTime">使用时长</th>
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
                $(".UniTab").UniTab();
                //$(".ListTbl").UniTable();
               
                $(".UISelect").UISelect();           
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            });
            $("#devName").autocomplete({
                source: "../data/searchdevice.aspx",
                minLength:0,
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#devName").val(ui.item.label);
                            $("#szGetKey").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {

                        ui.content.push({ label: " 未找到配置项 " });
                    }
                }
            }).blur(function () {
                if ($(this).val() == "") {
                    $("#szGetKey").val("");
                } else {

                }
            }).click(function () {
                $("#devName").autocomplete("search", "");
            });
            $("#btnOK").button();

        </script>
        <style>
          
            .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
            width:120px;
            }
           
        </style>
    </form>
</asp:Content>
