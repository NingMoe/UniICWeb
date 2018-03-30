<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabInfo.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">ʵ���һ��������(�鿴ԭʼ����)</h2>
        <div class="toolbar">
           <div class="tb_info">
               <div class="UniTab" id="tabl">
                     <%if(!bLeader) {%>
                <a href="RlabInfo.aspx" id="RlabInfo">�鿴ԭʼ����</a>  
                    <a href="RlabInfo2.aspx" id="RlabInfo2">�޸ı�������</a>       
                     <%} %> 
                      <a href="RlabInfo3.aspx" id="RDevList3">��������</a>            
                </div>
                <div style="margin:10px;">
                    <input type="submit" value="�����������¼��" id="subMit" title="�Ѿ��޸ĵ����ݽ�����գ�������" />
                </div>
            </div>

            </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="3">ѧУ����</th>
                        <th rowspan="3">ʵ���ұ��</th>
                        <th rowspan="3">ʵ��������</th>
                        <th rowspan="3">ʵ�������</th>
                        <th rowspan="3">�������</th>
                        <th rowspan="3">����ʹ�����</th>
                        <th rowspan="3">����ѧ��</th>
                        <th colspan="3">��ʦ����ɹ�</th>
                        <th rowspan="3">ѧ�������</th>
                        <th colspan="5">���ĺͽ̲����</th>
                        <th colspan="5">���м����������</th>
                        <th colspan="3">��ҵ��ƺ���������</th>
                        <th colspan="6">����ʵ��</th>
                        <th rowspan="3">������Ա��</th>
                        <th colspan="2">ʵ���ѧ���о���</th>
                    </tr>
                    <tr>
                        <th rowspan="2">���Ҽ�</th>
                        <th rowspan="2">ʡ����</th>
                        <th rowspan="2">����ר��</th>
                        <th colspan="2">���������¼</th>
                        <th colspan="2">���Ŀ���</th>
                        <th rowspan="2">ʵ��̲�</th>
                        <th colspan="2">������Ŀ��</th>
                        <th rowspan="2">��������Ŀ��</th>
                        <th colspan="2">������Ŀ��</th>
                        <th rowspan="2">ר��������</th>
                        <th rowspan="2">����������</th>
                        <th rowspan="2">�о�������</th>
                        <th colspan="2">ʵ�����</th>
                        <th colspan="2">ʵ������</th>
                        <th colspan="2">ʵ����ʱ��</th>
                        <th rowspan="2">С��</th>
                        <th rowspan="2">���н�ѧʵ����������ķ�</th>
                    </tr>
                    <tr>
                        <th>��ѧ</th>
                        <th>����</th>
                        <th>��ѧ</th>
                        <th>����</th>
                        <th>ʡ��������</th>
                        <th>����</th>
                        <th>ʡ��������</th>
                        <th>����</th>
                        <th>У��</th>
                        <th>У��</th>
                        <th>У��</th>
                        <th>У��</th>
                        <th>У��</th>
                        <th>У��</th>
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
            .thHead
            {
                width: 80px;
                text-align: center;
            }

            .ListTbl th
            {
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

