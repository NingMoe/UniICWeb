<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Room.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCRoomName%>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">
                    <a href="device.aspx" id="deviceTab"><%=ConfigConst.GCDevName %>����</a>
                    <a href="devkind.aspx" id="devkindTab"><%=ConfigConst.GCKindName%>����</a>
                <a href="room.aspx" id="roomTab"><%=ConfigConst.GCRoomName%>����</a>
                <a href="lab.aspx" id="labTab"><%=ConfigConst.GCLabName%>����</a>
                     <%if(ConfigConst.GroomNumMode==1) {%><a href="manGroup.aspx" id="A1">����Ա�����</a><%} %>
                </div>
            </div>
             
            <div class="FixBtn">
               
             <a id="btnNewRoom">�½�<%=ConfigConst.GCRoomName%></a>
            </div>
            <div class="tb_btn">             
            </div>
        </div>  
          <div style="margin-top:8px;">
                <input type="button" value="�����޸Ŀ��Ź���" id="setOpenRUle" />
          
        </div>
              <div>
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
        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px" name="szRoomNo">���</th>
                        <th name="szRoomName"><%=ConfigConst.GCRoomName%>����</th>
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th><%=ConfigConst.GCDevName %>��</th>
                        <th>����<%=ConfigConst.GCDevName %>��</th>
                        <th name="szOpenRuleName">���Ź���</th>
                        <th name="szRoomNo">�Ž�״̬</th> 
                        <th name="dwManMode">���Ʒ�ʽ</th>
                        <th width="25px">����</th>
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
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#setOpenRUle").button();
                function fAllOp(op) {
                }

                $("#setOpenRUle").click(function () {
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
                        title: '�����޸Ŀ��Ź���',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setRoomOpenRuleList.aspx?op=set&id=' + vSelectName
                    });
                });
                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a href="#"  class="manGroupList" title="����Ա����"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="Զ�̿���"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="�豸�ػ�"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="�豸����"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="�豸���½"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="�豸��Ҫ��½"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="�豸ж�ؿͻ���"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewOnlyRoom.aspx?op=new'
                    });
                });
                $(".InfoRoomBtn").click(function () {                  
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();                   
                    $.lhdialog({
                        title: '����',
                        width: '720px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwRoomID + "&type=RoomInfo"
                    });
                });
                $(".setRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetOnlyRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });

				 $(".manGroupList").click(function () {
                 var groupID = $(this).parents("tr").children().first().data("mangroupid");
                    $.lhdialog({
                        title: '����Ա����',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../inst/Dlg/SetUseGroup.aspx?op=set&id=' + groupID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                }, '��ʾ', 1, function () { });
                  });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false, });

            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
