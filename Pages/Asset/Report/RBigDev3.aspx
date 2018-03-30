<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RBigDev3.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">���������豸��(�鿴ԭʼ����)</h2>
        <div class="toolbar">
            <div class="tb_info">
                  
                <div class="UniTab" id="tabl">
                     <%if(!bLeader) {%>
                <a href="RBigDev.aspx" id="RBigDev">�鿴ԭʼ����</a>  
                    <a href="RBigDev2.aspx" id="RBigDev2">�޸ı�������</a> 
                     <%} %> 
                      <a href="RBigDev3.aspx" id="RDevList3">��������</a>                 
                </div>
                 
                
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="2">ѧУ����</th>
                        <th rowspan="2">�������</th>
                        <th rowspan="2">�����</th>
                        <th rowspan="2">��������</th>
                        <th rowspan="2">����</th>
                        <th rowspan="2">�ͺ�</th>
                        <th rowspan="2">���</th>
                        <th colspan="4">ʹ�û�ʱ</th>
                        <th rowspan="2">������</th>
                        <th colspan="3">��ѵ��Ա��</th>
                        <th rowspan="2">��ѧʵ����Ŀ��</th>
                        <th rowspan="2">������Ŀ��</th>
                        <th rowspan="2">��������Ŀ��</th>
                        <th colspan="2">�����</th>
                        <th colspan="2">����ר��</th>
                        <th colspan="2">�������</th>
                        <th rowspan="2">����������</th>
                    </tr>
                    <tr>
                        <th>��ѧ</th>
                        <th>����</th>
                        <th>������</th>
                        <th>���п���ʹ�û�ʱ</th>
                        <th>ѧ��</th>
                        <th>��ʦ</th>
                        <th>����</th>
                        <th>���Ҽ�</th>
                        <th>ʡ����</th>
                        <th>��ʦ</th>
                        <th>ѧ��</th>
                        <th>�������</th>
                        <th>�����ڿ�</th>
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
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
            $("input[name='szLab'],input[name='szRoom']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
             });
            $("#btnOK").button();
         
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

