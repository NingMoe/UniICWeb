<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevUsingTable.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold"><%=ConfigConst.GCDevName %>ʹ����ͳ��ͼ</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div style="margin-left: 30px; margin-bottom: 20px">
                   <table style="width:600px">
                        <tr>
                          <td style="text-align:center"> ��ʼ����:</td>
                            <td style="text-align:center">  <input type="text" name="dwStartDate" id="dwStartDate" runat="server" readonly="true" /></td>
                             <td style="text-align:center"> ��������:</td>
                            <td style="text-align:center"><input type="text" name="dwEndDate" id="dwEndDate" runat="server" readonly="true" /></td>
                            <td style="text-align:center">
                           <input type="submit" id="search" value="��ѯ" />
                            </td>                                              
                        </tr>
                      
                    </table>
                </div>
            </div>

        </div>
     <div id="szResv" class="LineStat" data-color="1" style="min-width:0px" data-unit="����" data-name="ʹ�ô���">
                   <h1><span></span><strong>ʹ���豸</strong><strong>�����豸</strong></h1>
         <%=szResvRate %>
                </div>
        <script type="text/javascript">
            $(function () {
                $('#search').button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
           });
		</script>
        <style>
            .tb_info table           
            .thHead
            {
                width: 80px;
                text-align: center;
            }
            .context2 input
            {
                margin-right: 20px;
            }

            .context input
            {
                margin-left: 15px;
            }

            .context select
            {
                margin-left: 15px;
            }
        </style>
    </form>
</asp:Content>

