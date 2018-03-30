<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>�ճ�����</h2>
        <div class="toolbar">
         <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>�ճ�����</a>
                <a href="DevUseRec.aspx" id="devUseRec"><%=ConfigConst.GCDevName %>ʹ�ü�¼</a>
             <!--   <a href="DevTestData.aspx" id="devTestData"><%=ConfigConst.GCDevName %>ʵ������</a>
                  <a href="DevUrlList.aspx" id="devUrlList">�������</a>
             <a href="DevSWList.aspx" id="devSWList">������</a>-->
            </div>
    
    </div>
          
        </div>


        <div  class="tb_infoInLine"    style="margin:5px 0px">
            <table style="width:99%">
               
                <tr>
                    <th><%=ConfigConst.GCLabName %></th>
                    <td><select id="lab" name="lab"><%=m_szLabOption %></select></td>
                     <th><%=ConfigConst.GCRoomName %></th>
                    <td><select id="szRoomSelect" name="szRoomSelect"><%=m_szRoomOption %></select></td>
                    </tr>
                <tr>
                    <th>״̬��</th>
                     <td style="text-align:left" colspan="3">
                        <LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat" > ����</LABEL>
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > ʹ����</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > ��ԤԼ</LABEL>
                    </td>
                    </tr>
                 <tr>
                    <th>�ؼ��֣�</th>
                     <td style="text-align:left" colspan="3">
                     <input type="text" id="szSearchKey" name="szSearchKey" />
                    </td>
                    </tr>
                <tr>
                    <th colspan="4">
                        <input type="submit" value="��ѯ" id="submit" />
                    </th>
                </tr>
            </table>
               </div>

        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                        <!--<th name="szAssertSN">���</th>-->
                        <th name="szDevName"><%=ConfigConst.GCDevName %>��</th>                        
                        <th><%=ConfigConst.GCDevName %>״̬</th>                                                                     
                    <!--    <th>�Ž�״̬</th>-->
                        <th name="szRoomName">����<%=ConfigConst.GCRoomName %></th>  
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>  
                       <!--  <th>����Ա</th>-->
                        <th>ʹ����</th>      
                        <!-- <th><%=ConfigConst.GCTutorName%></th>  -->
                          <%if ((ConfigConst.GCDevLoginTime == 1))
                            {%>  
                         <th>����ʱ��</th>                                            
                          <% } else{%>    
                         <th>��½ʱ��</th>                                     
                          <% }%>    
                         <%if((ConfigConst.GCDevListCol==1)) {%>  
                        <th>ԤԼʱ��</th>                                             
                          <% }%>  
                        <th style="width:60px;" class="thCenter">����</th>
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
                $("#submit").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="����Ϣ"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="���½"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="��Ҫ��½"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="Զ�̿���"><img src="../../../themes/icon_s/7.png"/></a>\
                    <a href="#" class="GetScreen" title="��Ļ�鿴"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="����"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="DevUseGroup" title="��ԤԼʹ����Ա"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a href="#" class="DevUseRec" title="ʹ�ü�¼"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="RemoveMessage" title="����Ϣ"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="���½"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="��Ҫ��½"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="Զ�̿���"><img src="../../../themes/icon_s/7.png"/></a>\
                    <a href="#" class="devCtrlRemove" title="ж��"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="GetScreen" title="��Ļ�鿴"><img src="../../../themes/icon_s/7.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "460", minHeight: "25", maxHeight: "25", speed: 50
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
                /*
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: 'ʹ����Ա',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetGroupMember.aspx?op=set&dwID=' + groupID + '&devID=' + devID
                    });
                });
                */
                $(".DevUseGroup").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-groupid");

                    $.lhdialog({
                        title: '��ԤԼʹ����Ա',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwID
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
                $(".GetScreen").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-ip");
                    $.lhdialog({
                        title: '��Ļ�鿴',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getScreen.aspx?op=set&ip=' + dwID
                    });
                });

                
                $(".ListTbl").UniTable();
               
                $("#lab").change(function () {
                    var labid = $("#lab").val();

                    $.ajax({
                        type: "post",
                        url: "../data/GetRoomList.aspx?type=Lab&szkey=" + labid,
                        dataType: "json",
                        success: function (data) {
                            if (data != "") {
                                $("#szRoomSelect").empty();
                                var roomlist = eval(data);
                                var optionRoom = "<option value='0'>" + "ȫ��" + "</option>";
                                for (var i = 0; i < roomlist.length; i++) {
                                    optionRoom += "<option value='" + roomlist[i].id + "'>" + roomlist[i].name + "</option>";
                                }

                                $("#szRoomSelect").html(optionRoom);
                                //$("#szRoomSelect").val(0);
                            }
                            else {
                                MessageBox(data, "", 2);
                            }

                        }
                    });
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
