<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLABSUMMARY.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">�ߵ�ѧУʵ�����ۺ���Ϣ��һ(�鿴ԭʼ����)</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                      <%if(!bLeader) {%>
            <a href="RLABSUMMARY.aspx" id="RLABSUMMARY">�鿴ԭʼ����</a>  
                    <a href="RLABSUMMARY2.aspx" id="RLABSUMMARY2">�޸ı�������</a>    
                    
 <%} %>                 
                     <a href="RLABSUMMARY3.aspx" id="RDevList3">��������</a> 

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
                        <th rowspan="4">ѧУ����</th>
                        <th rowspan="4">��λ����</th>
                        <th rowspan="4">ʵ���Ҹ���</th>
                        <th rowspan="4">ʵ���ҷ������</th>
                        <th colspan="4">�����豸</th>
                        <th colspan="7">��ѧ����</th>
                        <th rowspan="4">��������е�<%=ConfigConst.GCReachTestName%>��������Ŀ��</th>
                        <th colspan="7">������Ա��</th>
                        <th colspan="3">�ɹ�</th>
                    </tr>                    
                    <tr>
                        <th rowspan="3">̨��</th>
                        <th rowspan="3">���(��)</th>
                        <th rowspan="2" colspan="2">���й��������豸̨��</th>
                        <th rowspan="2" colspan="2">��ѧʵ��</th>
                        <th rowspan="2" colspan="5">��ʱ��</th>
                        <th rowspan="3">�ϼ�</th>
                        <th colspan="5">ר��</th>
                        <th rowspan="3">������Ա��</th>               
                        <th rowspan="3">������</th>
                        <th rowspan="3">��ʦ����ɹ���</th>
                        <th rowspan="3">ѧ������</th>

                    </tr>
                    <tr>
                   <th colspan="2">��ʦ</th>
                        <th colspan="2">ʵ�鼼����Ա</th>
                        <th rowspan="2">������Ա</th>
                        </tr>
                    <tr>
                        <th>̨��</th>
                        <th>���(��)</th>
                        <th>��Ŀ��</th>
                        <th>ʱ��</th>
                        <th>�ϼ�</th>
                        <th>��ʿ�о���</th>                       
                        <th>˶ʿ�о���</th>                       
                        <th>������</th>                       
                        <th>ר����</th>                       
                        <th>�߼�ְ��</th>    
                        <th>�м�ְ��</th>                                             
                        <th>�߼�ְ��</th>    
                        <th>�м�ְ��</th>    
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
          
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

