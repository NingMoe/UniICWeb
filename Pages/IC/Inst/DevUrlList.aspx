<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUrlList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold">上网监控</h2>
       
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                 <a href="DevPCList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindPC %>使用状况</a>                
                 <a href="DevPCCardUser.aspx" id="DevPCCardUser">刷卡用户列表</a>  
                <a href="DevPCUseRec.aspx" id="DevPCUseRec"><%=ConfigConst.GCSysKindPC %>使用记录</a>
                <%if(ConfigConst.GCERoomDoor==1) {%>
                      <a href="DevERoomDoor.aspx" id="DevERoomDoor"><%=ConfigConst.GCSysKindPC %>门禁状态</a>
                    <%} %>

                   <%if((ConfigConst.GCfunctionMode&4)>0) {%>
                      <a href="devURLLIST.aspx" id="devURLLIST">上网监控</a>
                 <a href="devSWLIST.aspx" id="devSWLIST">程序监控</a>
                    <%} %>
            </div>
    
    </div>
        <div style="margin-top:30px;width:99%;">
            <div style="text-align:center">
            <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1">    
                <%if((ConfigConst.GCDevListMode&1)>0) {%>  
                 <tr>
                    <td style="width:20%; text-align:center">校区</td>
                    <td style="width:75%;text-align:left"><%=m_szCamp %></td>
                    </tr> 
                <%} %>
                 <%if((ConfigConst.GCDevListMode&2)>0) {%>  
                <tr>
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCLabName %></td>
                    <td style="width:75%;text-align:left"><%=m_szLab %></td>
                </tr>
                 <%} %>
                 <%if((ConfigConst.GCDevListMode&8)>0) {%>  
                <tr>
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCDevName %>类别</td>
                    <td style="width:75%;text-align:left"><%=m_szDevCls %></td>
                </tr>
                 <%} %>
                <%if((ConfigConst.GCDevListMode&4)>0) {%>  
                <tr>
                    <td style="width:10%; text-align:center"><%=ConfigConst.GCRoomName %>
                    </td>
                        <td style="width:75%;text-align:left">
                    <%=m_szRoom %> <button type="button" id="bturlctrl">设置房间临时监控</button>
                            </td>
                </tr>
                <%} %>
                <%if((ConfigConst.GCDevListMode&16)>0) {%>  
                 <tr>
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCDevName %>状态</td>
                    <td style="width:75%;text-align:left">
                        <LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat" > 开机</LABEL>
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > 使用中</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > 被预约</LABEL>
                    </td>
                    </tr>
               <%} %>
                <tr>
                    <td colspan="2" style="text-align:center"><input type="submit"  value="查询" id="submitbtn" /></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                        <th name="szAssertSN">编号</th>
                        <th><%=ConfigConst.GCDevName %>名</th>                        
                        <th  name="dwRunStat">状态</th>
                        <th  name="szCurTrueName">使用者</th>
                        <th  name="dwURLCtrl">监控模式</th>
                        <th  name="dwURLEndTime">监控终止时间</th>
                        <th style="width:25px;" class="thCenter">操作</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
               var tabl= $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });
                $("#submitbtn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="发消息"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="免登陆"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="需要登陆"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="远程开机"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="报修"><img src=""../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="URLCTRL" title="设置监控模式"><img src="../../../themes/icon_s/5.png"/></a>\</a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
           
                $(".video").click(function () {
                    var roomno =$(this).parents("tr").children().first().attr("data-roomno");
                    $.lhdialog({
                        title: '查看视频',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:video/videoplay.aspx?op=new&roomno=' + roomno
                    });
                });
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '使用人员',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?id=' + groupID
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
             $(".URLCTRL").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '设置临时监控',
                                width: '600px',
                                height: '400px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:Dlg/SetUrlMoniter.aspx?type=52&id=' + dwID + '&labid=' + labid
                            });
                        });
                       $("#bturlctrl").button();
        $("#bturlctrl").click(function () {
             
                            $.lhdialog({
                                title: '设置临时监控',
                                width: '600px',
                                height: '400px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:Dlg/SetRoomUrlMoniter.aspx?room=<%=m_szRoomSel %>'
                                
                               
                                
                            });
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
