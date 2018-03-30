<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabCostInfo.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">ʵ���Ҿ��������(�鿴ԭʼ����)</h2>
        <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                       <%if(!bLeader) {%>
                    <a href="RLabCostInfo.aspx" id="RLabConstInfo">�鿴ԭʼ����</a>  
                    <a href="RLabCostInfo2.aspx" id="RLabConstInfo2">�޸ı�������</a>  
                       <%} %>                
                       <a href="RLabCostInfo3.aspx" id="RDevList3">��������</a> 
                </div>
                <div style="margin:10px">
                   <input type="submit" value="�����������¼��" id="subMit" title="�Ѿ��޸ĵ����ݽ�����գ�������" />
                </div>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="3">ѧУ����</th>
                        <th rowspan="3">ʵ���Ҹ���</th>
                        <th rowspan="3">ʵ���ҷ������</th>
                        <th colspan="10">����Ͷ�루��Ԫ��</th>                       
                    </tr>
                    <tr>
                        <th rowspan="2">�ܼ�</th>
                        <th colspan="2">�����豸���þ���</th>
                        <th colspan="2">�����豸ά������</th>
                        <th colspan="2">ʵ���ѧ���о���</th>
                        <th rowspan="2">ʵ���ҽ��辭��</th>
                        <th rowspan="2">ʵ���ѧ�о���ĸﾭ��</th>
                        <th rowspan="2">����</th>                         
                    </tr>
                    <tr>
                        <th>С��</th>
                        <th>���н�ѧ�������þ���</th>
                        <th>С��</th>
                        <th>���н�ѧ����ά������</th>
                        <th>С��</th>
                        <th>������������ľ���</th>                       
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>         
        </div>
        <script type="text/javascript">
            $(function () {
                $("#btnOp,#btnSave,#subMit").button();
                $(".UniTab").UniTab();
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

