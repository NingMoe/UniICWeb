<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomUseRec.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=ConfigConst.GCSysKindRoom %>使用记录</h2>
        <input type="hidden" id="szGetKey" name="szGetKey" />   
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="DevRoomResvState.aspx" id="DevRoomResvState"><%=ConfigConst.GCSysKindRoom %>预约状况</a>
                <a href="DevRoomList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindRoom %>使用状况</a>
                 <a href="DevRoomDoorCardRec.aspx" id="DevRoomDoorCardRec"><%=ConfigConst.GCSysKindRoom %>刷卡记录</a>
                <a href="DevRoomUseRec.aspx" id="DevRoomUseRec"><%=ConfigConst.GCSysKindRoom %>使用记录</a>
                <!-- <a href="DevRoomResvRec.aspx" id="DevRoomResvRec"><%=ConfigConst.GCSysKindRoom %>预约记录</a>-->
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
                            <th><%=ConfigConst.GCSysKindRoom %>名称:</th>
                            <td><input type="text" name="devName" id="devName" style="width:180px" /></td>
                          
                        </tr>
                         <tr>
                           <th><%=ConfigConst.GCKindName %>名称</th>
                        <td colspan="3">
                          <%=szKindStr %>
                        </td>
                        </tr>
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="查询" style="height:25px" />
                                <input type="button" id="btnExport" value="导出" style="height: 25px" />
                            </th>
                        </tr>
                    </table>
             
            </div>
       </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>                        
                        <th name="szDevName"><%=ConfigConst.GCSysKindRoom%>名称</th>
                        <th name="szKindName"><%=ConfigConst.GCKindName%>名称</th>                                                
                       <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th name="szTrueName">使用人</th>                        
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
                $(".ListTbl").UniTable();
                AutoDevice($("#devName"), 1,$("#szGetKey"),1,null,null,null);
                $(".UISelect").UISelect();           
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            });
            $("#btnExport").button().click(function () {
                var vDateStart = $("#<%=dwStartDate.ClientID%>").val();
                 var vDateEnd = $("#<%=dwEndDate.ClientID%>").val();
                var vLogonName = $("#dwPID").val();
                var dwDevKind = $('input[name="dwDevKind"]:checked').val();
             
                 var vRoomid = $("#szGetKey").val();
                 $.lhdialog({
                     title: '导出',
                     width: '200px',
                     height: '50px',
                     lock: true,
                     data: Dlg_Callback,
                     content: 'url:Dlg/outPortdevroomuserec.aspx?op=set&dwPID=' + vLogonName + '&dwDevKind=' + dwDevKind + '&szGetKey=' + vRoomid + '&startdate=' + vDateStart + '&enddate=' + vDateEnd
                 });
             });
            $("#btnOK").button();

        </script>
        <style>
          
            .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
          
            }
           
        </style>
    </form>
</asp:Content>
