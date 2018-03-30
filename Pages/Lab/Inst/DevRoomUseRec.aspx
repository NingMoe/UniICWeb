<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomUseRec.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=szDevNameURL %>ʹ�ü�¼</h2>
        <input type="hidden" id="szGetKey" name="szGetKey" />   
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                   <a href="DevRoomList.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomList"><%=szDevNameURL %>ʹ��״��</a>
                <a href="DevRoomResvState.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomResvState"><%=szDevNameURL %>ԤԼ״��</a>
                <a href="DevRoomDoorCardRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomDoorCardRec"><%=szDevNameURL %>ˢ����¼</a>
                <a href="DevRoomUseRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomUseRec"><%=szDevNameURL %>ʹ�ü�¼</a>            
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
                            <th><%=szDevNameURL %>����:</th>
                            <td><input type="text" name="devName" id="devName" style="width:180px" /></td>
                          
                        </tr>
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="��ѯ" style="height:25px" /></th>
                        </tr>
                    </table>
             
            </div>
       </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>                        
                        <th name="szDevName"><%=szDevNameURL %>����</th>
                        <th><%=ConfigConst.GCKindName%>����</th>                                                
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
                AutoDevice($("#devName"), 1,$("#szGetKey"),<%=uClassKind %>-1,null,null,null);
                $(".UISelect").UISelect();           
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
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
