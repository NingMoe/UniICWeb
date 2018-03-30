<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Room.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCRoomName%>�б�</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
          <div class="toolbar">
               <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="room.aspx" id="room">ʵ�����б�</a>
                    <a href="LabCodeTable.aspx?dwCodeType=1" id="LabCodeTable1">ʵ�������͹���</a>
                    <a href="LabCodeTable.aspx?dwCodeType=2" id="LabCodeTable2">ʵ���Ҿ�����Դ����</a>
                    <a href="LabCodeTable.aspx?dwCodeType=3" id="LabCodeTable3">ʵ���ҽ���ˮƽ����</a>
                </div>
            </div>

            <div class="FixBtn"> <a id="btnNewRoom">�½�<%=ConfigConst.GCRoomName%></a></div>
           
        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRoomNo">���</th>
                        <th name="szRoomName"><%=ConfigConst.GCRoomName%>����</th>
                        <th name="dwCreateDate">����ʱ��</th>
                        <th name="szLabKindCode">����</th>
                        <th name="szFloorNo">λ��</th>
                        <th name="szLabFromCode">������Դ</th>
                        <th name="szAcademicSubjectCode">ѧ��</th>
                        <th name="szLabLevelCode">����ˮƽ</th>
                        <th name="szDeptName">��λ</th>
                        <th>������</th>
                        <th>ƽ��ͼ</th>
                        <th>��ͼ</th>
                        <th width="25px" title="���ÿ��Ź���" style="text-align:center"></th>
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <style>
            .InfoLabBtn2 {
                cursor:pointer;
            }
            .InfoLabBtn{
                cursor:pointer;
            }
        </style>
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

                $(".class1").html('<div class="OPTDBtn">\
                            <a href="#"  class="setResvRuleBtn" title="���ÿ��Ź���"><img src="../../../themes/icon_s/6.png"/></a>\</div>');
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
                        content: 'url:Dlg/NewRoom.aspx?op=new'
                    });
                });
                $(".InfoLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwLabID + "&type=hard")
                });
                $(".InfoLabBtn2").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwLabID + "&type=hard2")
                });
                
                $(".setResvRuleBtn").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '�޸Ŀ��Ź���',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewOpenRule.aspx?op=set&dwID=' + openruleSN
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
                        content: 'url:Dlg/NewRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("mangroupid");
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
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false});
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
