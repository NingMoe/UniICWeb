<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomList.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
          <h2 style="margin-top:10px;font-weight:bold">�Ž�ʹ��״������</h2>
         <div class="toolbar">
           <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="RoomList.aspx" id="roomList">�Ž�ʹ��״������</a>
                <a href="DoorCardRec.aspx" id="doorCardRec">�Ž�ˢ����¼</a>
            </div>
    
    </div>
             </div>
          <div style="margin-top:8px;">
                <input type="button" value="����Զ�̿���" id="setOpenDoorList" />
          
        </div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                    <th><%=ConfigConst.GCLabName %>����:</th>
                   <td> <select id="lab" name="lab">
                    <%=m_szLab %>
                </select></td>
                  
                   <th><%=ConfigConst.GCRoomName %>����:</th>
                   <td><input type="text" id="szRoomName" name="szRoomName" /></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
      
         <div style="margin-top:10px;width:99%;">
            <div style="text-align:center">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">���</th>
                        <th><%=ConfigConst.GCRoomName%>����</th>
                        <th>����<%=ConfigConst.GCLabName %></th>
                        <th><%=ConfigConst.GCDevName %>��</th>                                             
                        <th>�Ž�״̬</th>                         
                        <th style="width:50px">����</th>
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
                $(".UniTab").UniTab();
                $("#setOpenDoorList,#btn").button();
                $(".doorCar").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="Զ�̿���"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="doorCardRec" title="ˢ����¼"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".door").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="Զ�̿���"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".Car").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="ˢ����¼"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".none").html('<div class="OPTDBtn">\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "50", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });              
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false, });
                $("#setOpenDoorList").click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            var vid = $(this).parents("td").data("id");
                            vSelectName = vSelectName + vid + ",";
                        }
                    });

                    if (vSelectName == "") {
                        return;
                    }

                    $.lhdialog({
                        title: '����Զ�̿���',
                        width: '420px',
                        height: '200px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/opendoorlist.aspx?op=set&id=' + vSelectName
                    });
                });
            });
            $(".doorCardRec").click(function () {
                var devID = $(this).parents("tr").children().first().attr("data-id");
                var roomName = $(this).parents("tr").children().first().attr("data-roomName");
                fdata = "szGetKey=" + devID + '&roomName=' + roomName;
                TabInJumpReload("doorCardRec", fdata);
            });          
        </script>
    </form>
</asp:Content>
