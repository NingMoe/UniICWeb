<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>����</h2>
       
        <div class="tb_info" style="display:inline">
            <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>����</a>
                 <a href="devListUse.aspx" id="devListUse">�ʲ����</a>
                    <a href="DevDamage.aspx" id="DevDamage">�豸����</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">�豸���ϼƻ�</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">�豸������ϸ</a>
            </div>
    
    </div>
        <div style="margin-top:30px;width:99%;">
            <div style="text-align:center;">
            <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1;width:99%">    
                <tr><td colspan="5" style="text-align:center">��ѯ����</td></tr>
                <tr><td rowspan="6" style="text-align:center">��ѯ����</td></tr>
                <%if((ConfigConst.GCDevListMode&1)>0) {%>  
                 <tr>
                    <td style="width:20%; text-align:center">У��</td>
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
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCDevName %>����</td>
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
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCDevName %>״̬</td>
                    <td style="width:75%;text-align:left" colspan="3">
                        <!--<LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat" > ����</LABEL>-->
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > ʹ����</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > ��ԤԼ</LABEL>
                    </td>
                    </tr>
               <%} %>
               <tr>
                    <td>�ʲ����:</td>
                    <td style="text-align:left"><input type="text" name="szAssertSN" id="szAssertSN" style="width:188px" /></td>
                    <td><%=ConfigConst.GCDevName%>�ؼ���:</td>
                   <td style="text-align:left"><input type="text" name="szSearchKey" id="szSearchKey" style="width:188px" /></td>
                
               </tr>
                    <tr>
                    <td style="width:80px">�ʲ�����:</td>
                    <td  style="text-align:left"><input type="text" name="dwMinUnitPrice" id="dwMinUnitPrice" style="width:80px" />��
                        <input type="text" name="dwMaxUnitPrice" id="dwMaxUnitPrice" style="width:80px" /></td>
                    <td>�ɹ�����:</td>
                     <td  style="text-align:left"><input type="text" name="dwSPurchaseDate" id="dwSPurchaseDate" style="width:80px" />��
                         <input type="text" name="dwEPurchaseDate" id="dwEPurchaseDate" style="width:80px" /></td>
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                        <input type="submit" id="btn" value="��ѯ" />
                        <input type="button" id="chgRoomList" value="λ���������" />
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
                        <th name="szAssertSN"><%=ConfigConst.GCDevName %>���</th>
                        <th name="szDevName"><%=ConfigConst.GCDevName %>����</th>                        
                                                                            
                      <!--  <th>�Ž�״̬</th>-->
                         <th  name="szClassName">����</th> 
                        <th>Ʒ���ͺ�</th>  
                        <th name="dwUnitPrice">����(Ԫ)</th>
                         <th name="dwPurchaseDate">�ɹ�����</th>         
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>  
                      <!--   <th>����Ա</th>-->
                                   
                        <th name="dwDevStat"><%=ConfigConst.GCDevName %>״̬</th>               
                        <th name="dwRunStat"><%=ConfigConst.GCDevName %>ʹ��״̬</th>  
                       
                        <th>ʹ����</th>      
                       <!--  <th><%=ConfigConst.GCTutorName%></th>  -->
                          <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>ʹ��ʱ��</th>                                            
                          <% } else{%>    
                         <th>��½ʱ��</th>                                     
                          <% }%>    
                         <%if((ConfigConst.GCDevListCol==1)) {%>  
                        <th>ԤԼʱ��</th>                                             
                          <% }%>  
                        <th>�ʽ���Դ</th>
                        <th style="width:25px;" class="thCenter">����</th>
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
                        title: '�鿴�ʲ�',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../sys/Dlg/GetAssert.aspx?op=set&id=' + dwID
                    });
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="����Ϣ"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="���½"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="��Ҫ��½"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="Զ�̿���"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="����"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="setLC" title="���"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="devDeamage" title="����"><img src="../../../themes/icon_s/7.png"/></a>\
                    <a href="#" class="devUnUse" title="����"><img src="../../../themes/icon_s/6.png"/></a>\</div>');

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
                        title: '�鿴��Ƶ',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:video/videoplay.aspx?op=new&roomno=' + roomno
                    });
                });
                $("#chgRoomList").button().click(function () {
                  
                    $.lhdialog({
                        title: 'λ���������',
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
                        title: '�ʲ�����',
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
                        title: '�ʲ�����',
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
                        title: '���',
                        width: '720px',
                        height: '400px',
                        lock: true,
                        content: 'url:Dlg/SetAttend2.aspx?op=set&opext=lc&id=' + dwLabID
                    });
                });
                
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: 'ʹ����Ա',
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
                    ConfirmBox("ȷ������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=limit&dwID=" + dwID);
                    }, '��ʾ', 1, function () { });
                });
                $(".setUse").click(function () {
                    var dwID =$(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=open&dwID=" + dwID);
                    }, '��ʾ', 1, function () { });
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
