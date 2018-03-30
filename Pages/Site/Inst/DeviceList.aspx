<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>使用状况管理</h2>
       
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>日常管理</a>
                <a href="DevUseRec.aspx" id="devUseRec"><%=ConfigConst.GCDevName %>使用记录</a>
                <a href="DevTestData.aspx" id="devTestData"><%=ConfigConst.GCDevName %>实验数据</a>
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
                    <%=m_szRoom %>
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
                        <th><%=ConfigConst.GCDevName %>状态</th>                                                                     
                        <th>门禁状态</th>
                        <th>所属<%=ConfigConst.GCRoomName %></th>  
                         <th>管理员</th>
                        <th>使用者</th>      
                         <th><%=ConfigConst.GCTutorName%></th>  
                          <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>开机时间</th>                                            
                          <% } else{%>    
                         <th>登陆时长</th>                                     
                          <% }%>    
                         <%if((ConfigConst.GCDevListCol==1)) {%>  
                        <th>预约时间</th>                                             
                          <% }%>  
                        <th style="width:60px;" class="thCenter">操作</th>
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

                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="发消息"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="免登陆"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="需要登陆"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="远程开机"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="报修"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="DevUseGroup" title="使用人员"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a href="#" class="DevUseRec" title="使用记录"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="DevTestData" title="实验数据"><img src="../../../themes/iconpage/edit.png"/></a>\
                          <a href="#" class="video" title="查看视频"><img src="../../../themes/icon_s/15.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
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
