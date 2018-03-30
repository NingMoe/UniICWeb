<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomDoorCardRec.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="szGetKey" name="szGetKey" /> 
          <h2 style="margin-top:10px;font-weight:bold"><%=szDevNameURL %>刷卡记录</h2>
      <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                 <a href="DevRoomList.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomList"><%=szDevNameURL %>使用状况</a>
                <a href="DevRoomResvState.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomResvState"><%=szDevNameURL %>预约状况</a>
                <a href="DevRoomDoorCardRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomDoorCardRec"><%=szDevNameURL %>刷卡记录</a>
                <a href="DevRoomUseRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomUseRec"><%=szDevNameURL %>使用记录</a>           
                </div>
            </div>
        </div>
         <div>
              <div  class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>开始日期:</th>
                            <td>
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>结束日期:</th>
                            <td>
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th class="thHead">学工号:</th>
                            <td class="tdHead">
                                <input type="text" name="dwPID" id="dwPID" />

                            </td>
                            <th><%=szDevNameURL %>名称:</th>
                               <td><input type="text" name="roomName" id="roomName" style="width:180px" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">
                                <input type="submit" id="btnOK" value="查询" /></th>
                        </tr>
                    </table>
                </div>
           <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th name="szTrueName">姓名(学工号)</th>                        
                        <th><%=ConfigConst.GCDeptName %></th>
                        <th name="szRoomName"><%=szDevNameURL %>名称</th>                      
                        <th name="dwCardTime">刷卡时间</th>   
                        <th>说明</th>    
                           <th width="25px"></th>                                            
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
               </div>
        <script type="text/javascript">
           
            $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="playVideo" href="#" title="播放监控"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".ListTbl").UniTable();
                $(".UniTab").UniTab();
                $("#btnOK").button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $(".playVideo").click(function () {
                    var roomno = $(this).parents("tr").children().first().attr("data-roomno");
                    var time = $(this).parents("tr").children().first().attr("data-time");
                    $.lhdialog({
                        title: '播放监控',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/playVideo.aspx?op=set&szRoomNO=' + roomno+'&time='+time
                    });
                });
                AutoRoom($("#roomName"), 1, $("#szGetKey"),<%=uClassKind %>-1, null);               
            });
        </script>
         <style>
              .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
           
        </style>
    </form>
</asp:Content>
