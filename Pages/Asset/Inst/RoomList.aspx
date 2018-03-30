<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomList.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
          <h2 style="margin-top:10px;font-weight:bold">门禁使用状况管理</h2>
           <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="RoomList.aspx" id="roomList">门禁使用状况管理</a>
                <a href="DoorCardRec.aspx" id="doorCardRec">门禁刷卡记录</a>
            </div>
    
    </div>
         <div style="margin-top:30px;width:99%;">
            <div style="text-align:center">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">编号</th>
                        <th><%=ConfigConst.GCRoomName%>名称</th>
                        <th>所属<%=ConfigConst.GCLabName %></th>
                        <th><%=ConfigConst.GCDevName %>数</th>                                             
                        <th>门禁状态</th>                         
                        <th style="width:50px">操作</th>
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
                $(".doorCar").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="远程开门"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="doorCardRec" title="刷卡记录"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".door").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="远程开门"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".Car").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="刷卡记录"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".none").html('<div class="OPTDBtn">\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "50", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });              
                $(".ListTbl").UniTable({});
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
