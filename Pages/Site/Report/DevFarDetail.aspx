<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevFarDetail.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold"><%=ConfigConst.GCDevName %>���ѷ�����ϸ  </h2>
        <input type="hidden" value="11" name="dwPurpose" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div style="margin-left: 100px; margin-bottom: 30px">
                    <table style="width: 650px">
                        <tr>
                            <th class="thHead">��ʼ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" />
                            </td>
                            <td class="thHead">��������:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>                            
                        </tr>
                        <tr>
                             <th class="thHead"><%=ConfigConst.GCDevName %>:</th>
                            <td class="tdHead">
                              <select id="dwDevID" name="dwDevID" style="width:120px">
                            <%=m_szDev%>
                        </select></td>                         
                            <td class="thHead"></td>
                             <td class="tdHead">
                                <input type="submit" id="btnOK" value="��ѯ" />
                                    <asp:button runat="server" text="����" id="btnExport" />
                             </td>
                        </tr>
                    
                    </table>
                </div>
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="2">ʱ��</th>
                        <th rowspan="2">ʹ����</th>
                        <th rowspan="2"><%=ConfigConst.GCTutorName%></th>
                        <th rowspan="2">�ܽ��</th>                      
                        <th colspan="4">���ѷ���</th>
                    </tr>
                    <tr>
                         <th>�������Է�</th>
                        <th>���Ż���</th>
                        <th>�����</th>                                               
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
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({                  
                });
            });
          
            $("#btnOK").button();
            $("#<%=btnExport.ClientID%>").button();
            $("#<%=btnExport.ClientID%>").click(function () {
                $("#type").val("export");
                var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                var dwDevID = $("#dwDevID").val();
                $.lhdialog({
                    title: '����',
                    width: '300px',
                    height: '100px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:DLG/DevRtResvDetailExport.aspx?dwStartDate=' + dwStartDate + '&dwEndDate=' + dwEndDate + '&dwDevID=' + dwDevID
                });
            });
        </script>
        <style>
            .ListTbl th
            {
                text-align:center;
            }
        </style>
    </form>
</asp:Content>
