<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="LendRoom.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindLend%>��������</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
           <div class="tb_info">
                <div class="UniTab" id="tabl">
                      <a href="syskindLend.aspx" id="syskindLend"><%=ConfigConst.GCSysKindLend%>����</a>
                    <a href="LendRoom.aspx" id="LendRoom"><%=ConfigConst.GCSysKindLend%>��������</a>
                       <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="lendlab.aspx" id="seatLab">��������¥��</a>
                    <%} %>   
                    <a href="LendKind.aspx" id="LendKind"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="LendClass.aspx" id="LendClass"><%=ConfigConst.GCSysKindLend%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                </div>
                 </div>
            <div class="FixBtn">
                <a id="btnNewRoom">�½�����</a>
                <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>
             
            </div>
            <div class="tb_btn">
                <!--<div class="AdvOpts" page="RoomAdvOpts.aspx">
                    <div class="AdvLab">�߼�ѡ��</div>
                </div>-->
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
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });

                function fAllOp(op) {
                }

                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a href="#"  class="manGroupList" title="����Ա����"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });              
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '720px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoom.aspx?op=new&dwInClassKind=4'
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
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetRoom.aspx?op=set&dwInClassKind=4&roomid=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().next().attr("ManGroupID");
                    $.lhdialog({
                        title: '����Ա����',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwRoomID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                }, '��ʾ', 1, function () { });
                  });
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true});
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
