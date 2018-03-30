<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomDoorCardRecGradute.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="szGetKey" name="szGetKey" /> 
          <h2 style="margin-top:10px;font-weight:bold"><%=szDevNameURL %>ˢ����¼</h2>
      <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                <a href="DevRoomListGradute.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomList"><%=szDevNameURL %>ʹ��״��</a>
                <a href="DevRoomDoorCardRecGradute.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomDoorCardRec"><%=szDevNameURL %>ˢ����¼</a>   
                </div>
            </div>
        </div>
         <div>
              <div  class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>��ʼ����:</th>
                            <td>
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td>
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th class="thHead">ѧ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwPID" id="dwPID" />

                            </td>
                            <th><%=szDevNameURL %>����:</th>
                               <td><input type="text" name="roomName" id="roomName" style="width:180px" /></td>
                        </tr>
                        <tr>
                            <th colspan="4">
                                <input type="submit" id="btnOK" value="��ѯ" /></th>
                        </tr>
                    </table>
                </div>
           <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>���</th>
                        <th name="szTrueName">����(ѧ����)</th>                        
                        <th><%=ConfigConst.GCDeptName %></th>
                        <th name="szRoomName"><%=szDevNameURL %>����</th>                      
                        <th name="dwCardTime">ˢ��ʱ��</th>   
                        <th>˵��</th>    
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
                    <a class="playVideo" href="#" title="���ż��"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
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
                        title: '���ż��',
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
