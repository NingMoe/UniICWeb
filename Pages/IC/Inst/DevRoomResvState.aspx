<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomResvState.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="startLine" />
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCSysKindRoom %>ʹ��״��</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                   <a href="DevRoomResvState.aspx" id="DevRoomResvState"><%=ConfigConst.GCSysKindRoom %>ԤԼ״��</a>
                <a href="DevRoomList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindRoom %>ʹ��״��</a>
                 <a href="DevRoomDoorCardRec.aspx" id="DevRoomDoorCardRec"><%=ConfigConst.GCSysKindRoom %>ˢ����¼</a>
                <a href="DevRoomUseRec.aspx" id="DevRoomUseRec"><%=ConfigConst.GCSysKindRoom %>ʹ�ü�¼</a>                
                </div> 
                <div class="FixBtn">
               
            </div>
    </div>
        <div style="margin-top:30px;width:99%;">  
             <a id="newResv" class="newResv">����Ա�½�ԤԼ</a> 
           <!-- <a id="resvImport" class="resvImport">����Ա����ԤԼ</a>     -->
        </div>
        <div class="content">
            <div style="width:99%;margin-top:15px">
                <div>
                   <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1;width:90%"> 
                       <tbody style="padding:0px;">
                       <tr>
                        <th>����</th>
                        <td><select id="kindi" name="kindid">
                            <%=m_szDevKind  %>

                            </select> <input type="submit" value="��ѯ" id="sub" style="margin-left:50px;" /></td>
                           </tr>
                           </tbody>        </table>
                </div>
            <div style="margin-top:10px;text-align:center; MARGIN-RIGHT: auto;height:350px; overflow-y:hidden;" id="divResvStatue">
                <iframe runat="server" id="iframe" style="width:80%;border:0px;height:99%">

                </iframe>
                </div>
                </div>
        </div>
        <script type="text/javascript">
            $(function () {
               var tabl= $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });

                /*
                $("#divResvStatue").ResvState({
                    "url": "../data/devResvstate.aspx",
                    "devClassKind": 1,
                    "purpose": 11,
                    "startLine": 1,
                    "needLine":10

                 });
                 */
                $("#sub").button();
                $("#newResv").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="����Ϣ"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="���¼"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="��Ҫ��¼"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="Զ�̿���"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="����"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="DevUseGroup" title="��ԤԼʹ����Ա"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a class="openDoor" href="#" title="Զ�̿���"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="devDoorCard" title="ˢ����¼"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab'],input[name='szRoom'],input[name='szDevKinds'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $(".video").click(function () {
                    var roomno =$(this).parents("tr").children().first().attr("data-roomno");
                    $.lhdialog({
                        title: '�鿴��Ƶ',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:video/videoplay.aspx?op=new&roomno=' + roomno
                    });
                });
                $(".devDoorCard").click(function () {
                    var roomid =$(this).parents("tr").children().first().attr("data-roomid");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    fdata = "szGetKey=" + roomid + '&roomName=' + devName;
                    TabInJumpReload("DevRoomDoorCardRec", fdata);
                });
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '��ԤԼʹ����Ա',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetGroupMember.aspx?op=set&dwID=' + groupID
                    });
                });
                $(".newResv").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '����Ա�½�ԤԼ',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewICResv.aspx?op=set&dwID=' + groupID
                    });
                });
                $(".resvImport").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '����Ա����ԤԼ',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newImportICResv.aspx?op=set&dwID='
                    });
                });
                
                $("table").delegate(".DevUseGroup", "click", function () {
                   
                });
                
                $(".DevUseRec").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                  
                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devUseRec", fdata);
                });
                $(".DevTestData").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    urlPar = [["szGetKey", devID], ['devName', (devName)]];
                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devTestData", fdata);
                });
                $(".ListTbl").UniTable();               
            });
        </script>
        <style>
            #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
            .thCenter
            {
                text-align:center;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:8px;
            }

        </style>
    </form>
</asp:Content>
