<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="pclab.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��������<%=ConfigConst.GCLabName %></h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
           <div class="tb_info">
                <div class="UniTab" id="tabl">
                     <a href="syskindPC.aspx" id="syskindPC"><%=ConfigConst.GCSysKindPC%>����</a>
                    <a href="PCRoom.aspx" id="PCRoom"><%=ConfigConst.GCSysKindPC%>��������</a>
                     <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="pcLab.aspx" id="seatLab">��������<%=ConfigConst.GCLabName %></a>
                    <%} %>   
                    <a href="PCKind.aspx" id="PCKind"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></a>  
                                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="PCClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="ResvRulePC.aspx?kind=128" id="resvrule">ԤԼ����</a>
                </div>
                 </div>
            <div class="FixBtn">
                <a id="btnNewRoom">�½�<%=ConfigConst.GCLabName %></a>
             
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
                       
                        <th name="szLabName"><%=ConfigConst.GCLabName %>����</th>
                    
                        <th name="szMemo">��ע</th>                                                
                        <th width="25px"></th>
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
                            <a href="#"  class="manGroupList" title="����Ա����"><img src="../../../themes/iconpage/mangerlist.png"/></a>\
                            <a href="#"  class="setPostion" title="�༭�ֻ���λ�÷ֲ�ͼ"><img src="../../../themes/iconpage/pin.png"/></a>\</div>');
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
                        content: 'url:Dlg/newlab.aspx?op=new&dwLabClass=2'
                    });
                });
                $(".setPostion").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    window.open("../../../mobileclient/test.aspx?op=getPostion&szlabid="+dwRoomID);
                });
                
                $(".InfoRoomBtn").click(function () {                  
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
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
                        content: 'url:Dlg/setlab.aspx?op=set&dwLabClass=2&dwLabID=' + dwRoomID
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
