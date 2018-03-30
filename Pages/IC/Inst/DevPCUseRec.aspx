<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevPCUseRec.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=ConfigConst.GCSysKindPC %>ʹ�ü�¼</h2>
        <input type="hidden" id="szGetKey" name="szGetKey" />   
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
               <a href="DevPCList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindPC %>ʹ��״��</a>                
                 <a href="DevPCCardUser.aspx" id="DevPCCardUser">ˢ���û��б�</a>  
                <a href="DevPCUseRec.aspx" id="DevPCUseRec"><%=ConfigConst.GCSysKindPC %>ʹ�ü�¼</a>
                <%if(ConfigConst.GCERoomDoor==1) {%>
                      <a href="DevERoomDoor.aspx" id="DevERoomDoor"><%=ConfigConst.GCSysKindPC %>�Ž�״̬</a>
                    <%} %>

                   <%if((ConfigConst.GCfunctionMode&4)>0) {%>
                      <a href="devURLLIST.aspx" id="devURLLIST">�������</a>
                 <a href="devSWLIST.aspx" id="devSWLIST">�������</a>
                    <%} %>
              
                </div>
            </div>
        </div>
       <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>��ʼ����:</th>
                            <td><input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td> <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>ѧ����:</th>
                            <td><input type="text" name="dwPID" id="dwPID" /></td>
                            <th>�豸����:</th>
                            <td><input type="text" name="devName" id="devName" style="width:180px" /></td>
                          
                        </tr>
                       
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="��ѯ" style="height:25px" />
                                  <input type="button" id="btnExport" value="����" style="height: 25px" />
                            </th>
                        </tr>
                    </table>
             
            </div>
       </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>                        
                        <th name="szDevName">�豸����</th>                      
                        <th  name="szKindName"><%=ConfigConst.GCKindName%>����</th>     
                        <th>�ͺ�</th>                                           
                        <th>���</th>                                           
                       <th>����<%=ConfigConst.GCLabName %></th>
                        <th name="szTrueName">ʹ����</th>                        
                        <th><%=ConfigConst.GCDeptName %></th>
                        <th name="dwBeginTime">ʹ��ʱ��</th>
                        <th name="dwUseTime">ʹ��ʱ��</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />

        </div>
        <script type="text/javascript">
            $(function () {
                $(".UniTab").UniTab();
                $(".ListTbl").UniTable();
                AutoDevice($("#devName"), 1,$("#szGetKey"),2,null,null,null);
                $(".UISelect").UISelect();           
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnExport").button().click(function () {
                    var vDateStart = $("#<%=dwStartDate.ClientID%>").val();
                       var vDateEnd = $("#<%=dwEndDate.ClientID%>").val();
                       var vLogonName = $("#dwPID").val();
                       var dwDevKind = 0;

                       var vRoomid = $("#szGetKey").val();
                       $.lhdialog({
                           title: '����',
                           width: '200px',
                           height: '50px',
                           lock: true,
                           data: Dlg_Callback,
                           content: 'url:Dlg/outPortdevpcuserec.aspx?op=set&dwPID=' + vLogonName + '&dwDevKind=' + dwDevKind + '&szGetKey=' + vRoomid + '&startdate=' + vDateStart + '&enddate=' + vDateEnd
                       });
                   });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
            });           
            $("#btnOK").button();

        </script>
        <style>
          
            .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
            width:120px;
            }
           
        </style>
    </form>
</asp:Content>