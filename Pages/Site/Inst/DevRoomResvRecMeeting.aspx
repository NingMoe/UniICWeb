<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomResvRecMeeting.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom %>ԤԼ��¼</h2>
        <div class="toolbar">
            <input type="hidden" name="szGetKey" id="szGetKey" />
        <div class="tb_info">
            <div class="UniTab" id="tabl">
            <a href="DevRoomListMeeting.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomList"><%=szDevNameURL %>ʹ��״��</a>
                <a href="DevRoomResvStateMeeting.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomResvState"><%=szDevNameURL %>ԤԼ״��</a>
                <a href="DevRoomDoorCardRecMeeting.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomDoorCardRec"><%=szDevNameURL %>ˢ����¼</a>
                <a href="DevRoomUseRecMeeting.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomUseRec"><%=szDevNameURL %>ʹ�ü�¼</a>            
            </div>
    
    </div>
         
        </div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th>ԤԼ��ʼ����</th>
                   <td>
                    <input type="text" name="dwStartDate" id="dwStartDate" runat="server" />   
                   </td>
                   <th>ԤԼ��������</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" runat="server"  /></td>
                   <th><%=ConfigConst.GCSysKindRoom %>����</th>
                   <td><input type="text" name="devName" id="devName" runat="server"  /></td>
                   <th>������ѧ��</th>
                   <td><input type="text" name="dwPID" id="dwPID" runat="server"  /></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>        
                         <th>ԤԼ��</th>                      
                        <th>������</th>                       
                        <th name="dwPreBegin"><%=ConfigConst.GCSysKindRoom %></th>
                        <th>ԤԼʱ��</th>
                        <th name="szLabName"><%=ConfigConst.GCLabName %></th>                        
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
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#<%=devName.ClientID%>"), 1, $("#szGetKey"), 2, null, null, null);
                AutoUserByLogonname($("#<%=dwPID.ClientID%>"), 1, $("#szGetKey"), null, null, null);
                $("#<%=dwPID.ClientID%>").on("autocompleteselect", function (event, ui) {
                    setTimeout(function () {                        
                        $("#<%=dwPID.ClientID%>").val(ui.item.szLogonName);
                        $("#szKey").val(ui.item.id);
                    }, 10);
                });
                var tabl = $(".UniTab").UniTab();
                $("#btn").button();                                
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
