<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>总账</h2>
       
        <div class="tb_info" style="display:inline">
            <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>总账</a>
                 <a href="devListUse.aspx" id="devListUse">资产变更</a>
                    <a href="DevDamage.aspx" id="DevDamage">设备报修</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">设备报废计划</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">设备报废明细</a>
            </div>
    
    </div>
        <div style="margin-top:30px;width:99%;">
            <div style="text-align:center;">
            <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1;width:99%">    
                <tr><td colspan="5" style="text-align:center">查询条件</td></tr>
                <tr><td rowspan="6" style="text-align:center">查询分类</td></tr>
                <%if((ConfigConst.GCDevListMode&1)>0) {%>  
                 <tr>
                    <td style="width:20%; text-align:center">校区</td>
                    <td style="width:75%;text-align:left" colspan="3"><%=m_szCamp %></td>
                    </tr> 
                <%} %>
                 <%if((ConfigConst.GCDevListMode&2)>0) {%>  
                <tr>
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCLabName %></td>
                    <td style="width:75%;text-align:left" colspan="3"><%=m_szLab %></td>
                </tr>
                 <%} %>
                 <%if((ConfigConst.GCDevListMode&8)>0) {%>  
                <tr>
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCDevName %>类型</td>
                    <td style="width:75%;text-align:left"  colspan="3"><%=m_szDevKind %></td>
                </tr>
                 <%} %>
                <%if((ConfigConst.GCDevListMode&4)>0) {%>  
                <tr>
                    <td style="width:10%; text-align:center"><%=ConfigConst.GCRoomName %>
                    </td>
                        <td style="width:75%;text-align:left"  colspan="3">
                    <%=m_szRoom %>
                            </td>
                </tr>
                <%} %>
                <%if((ConfigConst.GCDevListMode&16)>0) {%>  
                 <tr>
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCDevName %>状态</td>
                    <td style="width:75%;text-align:left" colspan="3">
                        <!--<LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat" > 开机</LABEL>-->
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > 使用中</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > 被预约</LABEL>
                    </td>
                    </tr>
               <%} %>
               <tr>
                    <td>资产编号:</td>
                    <td style="text-align:left"><input type="text" name="szAssertSN" id="szAssertSN" style="width:188px" /></td>
                    <td><%=ConfigConst.GCDevName%>关键字:</td>
                   <td style="text-align:left"><input type="text" name="szSearchKey" id="szSearchKey" style="width:188px" /></td>
                
               </tr>
                    <tr>
                    <td style="width:80px">资产单价:</td>
                    <td  style="text-align:left"><input type="text" name="dwMinUnitPrice" id="dwMinUnitPrice" style="width:80px" />到
                        <input type="text" name="dwMaxUnitPrice" id="dwMaxUnitPrice" style="width:80px" /></td>
                    <td>采购日期:</td>
                     <td  style="text-align:left"><input type="text" name="dwSPurchaseDate" id="dwSPurchaseDate" style="width:80px" />到
                         <input type="text" name="dwEPurchaseDate" id="dwEPurchaseDate" style="width:80px" /></td>
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                        <input type="submit" id="btn" value="查询" />
                        <input type="button" id="chgRoomList" value="位置批量变更" />
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                       <!-- <th></th>-->
                        <th name="szAssertSN"><%=ConfigConst.GCDevName %>编号</th>
                        <th name="szDevName"><%=ConfigConst.GCDevName %>名称</th>                        
                                                                            
                      <!--  <th>门禁状态</th>-->
                         <th  name="szClassName">类型</th> 
                        <th>品牌型号</th>  
                        <th name="dwUnitPrice">单价(元)</th>
                         <th name="dwPurchaseDate">采购日期</th>         
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>  
                      <!--   <th>管理员</th>-->
                                   
                        <th name="dwDevStat"><%=ConfigConst.GCDevName %>状态</th>               
                        <th name="dwRunStat"><%=ConfigConst.GCDevName %>使用状态</th>  
                       
                        <th>使用者</th>      
                       <!--  <th><%=ConfigConst.GCTutorName%></th>  -->
                          <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>使用时间</th>                                            
                          <% } else{%>    
                         <th>登陆时长</th>                                     
                          <% }%>    
                         <%if((ConfigConst.GCDevListCol==1)) {%>  
                        <th>预约时间</th>                                             
                          <% }%>  
                        <th>资金来源</th>
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
                $("#dwSPurchaseDate,#dwEPurchaseDate").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $(".setDev").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '查看资产',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../sys/Dlg/GetAssert.aspx?op=set&id=' + dwID
                    });
                });
                $("#btn").button();
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
                    <a href="#" class="setLC" title="变更"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="devDeamage" title="报修"><img src="../../../themes/icon_s/7.png"/></a>\
                    <a href="#" class="devUnUse" title="报废"><img src="../../../themes/icon_s/6.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                /*
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl(); */
                   // TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
            //});
          
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
                $("#chgRoomList").button().click(function () {
                  
                    $.lhdialog({
                        title: '位置批量变更',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/chgRoomList.aspx'
                    });
                });
                
                $(".devDeamage").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    var assertsn =$(this).parents("tr").children().first().attr("data-assertsn");
                    var szRoomName =$(this).parents("tr").children().first().attr("data-szRoomName");
                    $.lhdialog({
                        title: '资产报修',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevDemage.aspx?op=new&dwDevID=' + devID + '&szDevName=' + devName + '&dwDevSN=' + assertsn + '&szRoomName=' + szRoomName
                        });
                });
                $(".devUnUse").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    var assertsn =$(this).parents("tr").children().first().attr("data-assertsn");
                    var szRoomName =$(this).parents("tr").children().first().attr("data-szRoomName");
                    $.lhdialog({
                        title: '资产报废',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevUnuseOne.aspx?op=new&dwDevID=' + devID + '&szDevName=' + devName + '&dwDevSN=' + assertsn + '&szRoomName=' + szRoomName
                    });
                });

                $(".setLC").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '变更',
                        width: '720px',
                        height: '400px',
                        lock: true,
                        content: 'url:Dlg/SetAttend2.aspx?op=set&opext=lc&id=' + dwLabID
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
                $(".setLimit").click(function () {
                    var dwID =$(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定禁用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=limit&dwID=" + dwID);
                    }, '提示', 1, function () { });
                });
                $(".setUse").click(function () {
                    var dwID =$(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定启用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=open&dwID=" + dwID);
                    }, '提示', 1, function () { });
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
