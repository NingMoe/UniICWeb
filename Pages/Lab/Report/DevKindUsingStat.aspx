<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevKindUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold"><%=ConfigConst.GCKindName %>ʹ����ͳ��</h2>
        <input type="hidden" value="11" name="dwPurpose" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div style="margin-left: 100px; margin-bottom: 30px">
                    <table style="width: 650px">
                        <tr>
                            <th class="thHead">��ʼ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">��������:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                             <td class="tdHead">
                                <input type="submit" id="btnOK" value="��ѯ" /></td>
                        </tr>
                    
                    </table>
                </div>
            </div>

        </div>
        <div class="content" style="margin-top:10px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="100px"><%=ConfigConst.GCKindName%>����</th>
                         <th>�ͺ�/���</th>
                        <th><%=ConfigConst.GCDevName%>��</th>
                        <th name="dwPIDNum">ʹ������</th>
                        <th name="dwUseTimes">ʹ���˴���</th>
                        <th name="dwTotalUseTime">ʹ����ʱ��</th>
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
               // $(".ListTbl").UniTable();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50

                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
            });
            $("#btnOK").button();
        </script>
        <style>
            .thHead {
                background: #e5f1f4;
                text-align: right;
            }
           
            .tdHead {
                text-align: left;
            }

            td input {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
