<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevLendList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCSysKindLend %>ʹ��״��</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="DevLendList.aspx" id="DevLendList"><%=ConfigConst.GCSysKindLend %>ʹ��״��</a>                
                <a href="DevLendUseRec.aspx" id="DevLendUseRec"><%=ConfigConst.GCSysKindLend %>ʹ�ü�¼</a>
               <!-- <a href="DevPCResvRec.aspx" id="DevPCResvRec"><%=ConfigConst.GCSysKindLend %>ԤԼ��¼</a>-->
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
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCDevName %>״̬</td>
                    <td style="width:75%;text-align:left">                        
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > ʹ����</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > ��ԤԼ</LABEL>
                    </td>
                    </tr>
               <%} %>
                <tr>
                    <td><%=ConfigConst.GCDevName %>����</td>
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
                        <th><%=ConfigConst.GCDevName %>��</th>                                                 
                        <th><%=ConfigConst.GCDevName %>״̬</th>                                                                              
                        <th>����<%=ConfigConst.GCRoomName %></th>  
                      <th>����<%=ConfigConst.GCKindName %></th>
                        <th>ʹ����</th>      
                                   <th>ʹ��ʱ��</th>                             
                       
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
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });

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