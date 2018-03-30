<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceAllList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>�б�</h2>
       
       
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                        <th>���</th>
                        <th><%=ConfigConst.GCDevName %>��</th>                        
                        <th><%=ConfigConst.GCDevName %>״̬</th>                                                                     
                        <th>�Ž�״̬</th>
                        <th>����<%=ConfigConst.GCRoomName %></th>  
                         <th>����Ա</th>
                        <th>ʹ����</th>      
                         <th><%=ConfigConst.GCTutorName%></th>                                         
                        <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>����ʱ��</th>                                            
                          <% } else{%>    
                         <th>��½ʱ��</th>                                     
                          <% }%>  
                         <%if((ConfigConst.GCDevListCol==1)) {%>  
                        <th>ԤԼʱ��</th>                                             
                          <% }%>  
                        
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
                $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });
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
                    <a href="#" class="DevUseGroup" title="ʹ����Ա"><img src="../../../themes/icon_s/5.png"/></a>\
                    <a href="#" class="DevUseRec" title="ʹ�ü�¼"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="DevTestData" title="ʵ������"><img src="../../../themes/icon_s/3.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "75", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                 
                });              
                $(".DevUseRec").click(function () {
                    var devID = $(this).parents("tr").children().first().attr("data-id");
                    var devName = $(this).parents("tr").children().first().attr("data-name");
                    urlPar = [["szGetKey",devID], ['devName',escape(devName)]];
                    TabInJumpReload(("DevUseRec.aspx?szGetKey=" + devID + '&devName=' + escape(devName)), 1, "tabl");
                });
                $(".DevTestData").click(function () {
                    var devID = $(this).parents("tr").children().first().attr("data-id");
                    var devName = $(this).parents("tr").children().first().attr("data-name");
                    urlPar = [["szGetKey", devID], ['devName', escape(devName)]];
                    TabInJumpReload(("DevTestData.aspx?szGetKey=" + devID + '&devName=' + escape(devName)), 1, "tabl");
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
                margin-left:20px;
            }

        </style>
    </form>
</asp:Content>
