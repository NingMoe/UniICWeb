<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DoorCardRec.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="szGetKey" name="szGetKey" /> 
          <h2 style="margin-top:10px;font-weight:bold">�Ž�<%=ConfigConst.GCRoomName%>�ճ�����</h2>
            <div class="toolbar">
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="RoomList.aspx" id="roomList">�Ž�<%=ConfigConst.GCRoomName%>�ճ�����</a>
                <a href="DoorCardRec.aspx" id="doorCardRec">�Ž�ˢ����¼</a>
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
                            <th><%=ConfigConst.GCRoomName %>����:</th>
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
                        <th>����(ѧ����)</th>
                        <th><%=ConfigConst.GCTutorName%></th>
                        <th><%=ConfigConst.GCDeptName %></th>
                        <th><%=ConfigConst.GCRoomName %></th>                      
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
                //$(".ListTbl").UniTable();
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
                $("#roomName").autocomplete({
                    source: "../data/GetRoomList.aspx?ctrlType=1",
                    minLength: 0,
                    select: function (event, ui) {
                        if (ui.item) {
                            if (ui.item.id && ui.item.id != "") {
                                $("#roomName").val(ui.item.label);
                                $("#szGetKey").val(ui.item.id);
                            }
                        }
                        return false;
                    },
                    response: function (event, ui) {
                        if (ui.content.length == 0) {
                            ui.content.push({ label: " δ�ҵ������� " });
                        }
                    }
                }).blur(function () {
                    if ($(this).val() == "") {
                        $("#szGetKey").val("");
                    } else {

                    }
                }).click(function () {
                    $("#roomName").autocomplete("search", "");
                });
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
