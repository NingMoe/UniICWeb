<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevSeatList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCSysKindSeat %>ʹ��״��</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="DevSeatList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindSeat %>ʹ��״��</a> 
                <a href="DevSeatCardUser.aspx" id="DevSeatCardUser"><%=ConfigConst.GCSysKindSeat %>�û�ˢ���б�</a>  
                <a href="DevSeatUseRec.aspx" id="DevSeatUseRec"><%=ConfigConst.GCSysKindSeat %>ʹ�ü�¼</a>
                     <a href="DevSeatUseInfo.aspx" id="DevPCUseinfo"><%=ConfigConst.GCSysKindSeat %>�û�ǩ����¼</a>
               <!-- <a href="DevPCResvRec.aspx" id="DevPCResvRec"><%=ConfigConst.GCSysKindSeat %>ԤԼ��¼</a>-->
                </div> 
    </div>
        <div style="margin-top:30px;width:99%;">
            <div style="text-align:center">
            <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1">                                    
               <tr>
                    <td  style="width:10%; text-align:center">����</td>
                    <td style="width:75%;text-align:left"><%=m_szRoom %></td>
                </tr>
                <tr>
                    <td  style="width:10%; text-align:center"><%=ConfigConst.GCKindName %></td>
                    <td style="width:75%;text-align:left"><%=m_szDevKind %></td>
                </tr>                       
                <%if((ConfigConst.GCDevListMode&16)>0) {%>  
                 <tr>
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCSysKindSeat %>����״̬</td>
                    <td style="width:75%;text-align:left">                        
                          <LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat2" >δ����</LABEL>
                          <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" >�ѷ���</LABEL>                         
                    </td>
                    </tr>
                <%if(isSmart==true) {%>
                 <tr>
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCSysKindSeat %>ʹ��״̬</td>
                    <td style="width:75%;text-align:left">                                                                
                           <LABEL><INPUT class="enum" value="524288" type="checkbox" name="dwRunStat" >����</LABEL>                       
                        <LABEL><INPUT class="enum" value="1048576" type="checkbox" name="dwRunStat" >����</LABEL>     
                    </td>
                    </tr>
                <%} %>
               <%} %>
                <tr>
                    <td><%=ConfigConst.GCSysKindSeat %>����</td>
                    <td style="text-align:left"><input type="text" name="szDevName" id="szDevName" style="margin-left:5px" />
                        <input type="submit" value="��ѯ" id="sub" />
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>                                     
                    <th></th>                       
                        <th><%=ConfigConst.GCSysKindSeat %>��</th>                        
                    
                        <th name="dwRunStat"><%=ConfigConst.GCSysKindSeat %>״̬</th>                                                                              
                        <th>��������</th>  
                      <th>����<%=ConfigConst.GCKindName %></th>
                        <th>ʹ����</th>      
                                                            
                        <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>ʹ��ʱ��</th>                                            
                          <% } else{%>    
                         <th>ʹ��ʱ��</th>                                     
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
               var tabl= $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });
               
                $("#sub").button();
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
                    <a href="#" class="DevUseRec" title="ʹ�ü�¼"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="DevTestData" title="ԤԼ��¼"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
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
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '��ԤԼʹ����Ա',
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
