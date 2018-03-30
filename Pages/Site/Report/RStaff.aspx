<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RStaff.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">ר��ʵ������Ա��(�鿴ԭʼ����)</h2>        
        <div class="toolbar">
            <div class="tb_info">
               <div class="UniTab" id="tabl">
                 <%if(!bLeader) {%>
                    <a href="RStaff.aspx" id="RStaff">�鿴ԭʼ����</a>  
                    <a href="RStaff2.aspx" id="RStaff2">�޸ı�������</a>  
                       <%} %>         
                    <a href="RStaff3.aspx" id="RStaff3">��������</a>                
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
                        <th rowspan="2">ѧУ����</th>
                        <th rowspan="2">��Ա����</th>
                        <th rowspan="2">ʵ���ұ��</th>
                        <th rowspan="2">ʵ��������</th>
                        <th rowspan="2">����</th>                      
                        <th rowspan="2">�Ա�</th>                  
                        <th rowspan="2">��������</th>
                        <th rowspan="2">����ѧ��</th>
                        <th rowspan="2">רҵ����ְ��</th>                                               
                        <th rowspan="2">�Ļ��̶�</th>  
                        <th rowspan="2">ר�����</th>  
                        <th colspan="2">������ѵ</th>  
                        <th colspan="2">������ѵ</th>                         
                    </tr>    
                    <tr>                  
                        <th>ѧ������ʱ��</th>  
                        <th>��ѧ������ʵ��</th>                         
                    
                        <th>ѧ������ʱ��</th>  
                        <th>��ѧ������ʵ��</th>                         
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
          
            $("#btnOK").button();           
        </script>
        <style>
            .ListTbl th
            {
                text-align:center;
            }
             .ListTbl td
            {
                text-align:center;
            }
            .tb_info table{border-bottom:1px solid #000}
            .tb_info table td{text-align:center; border-left:1px solid #000;border-right:1px solid #000;height:15px; border-top:1px solid #000}       
            .tb_info table th{border-left:1px solid #000;border-right:1px solid #000;height:15px; border-top:1px solid #000;}       
            .thHead
            {
                width:80px;
                text-align:center;
            }
            .context input
            {
                margin-left:15px;
            }
             .context select
            {
                margin-left:15px;

            }
        </style>
    </form>
</asp:Content>

