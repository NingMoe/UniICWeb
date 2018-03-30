<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLABSUMMARYII.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">�ߵ�ѧУʵ�����ۺ���Ϣ���(�鿴ԭʼ����)</h2>
        <div class="toolbar">
             <div class="tb_info">
                <div class="UniTab" id="tabl">
                      <%if(!bLeader) {%>
                <a href="RLABSUMMARYII.aspx" id="RLABSUMMARYII">�鿴ԭʼ����</a>  
                    <a href="RLABSUMMARYII2.aspx" id="RLABSUMMARYII2">�޸ı�������</a>    
                     <%} %>       
                     <a href="RLABSUMMARYII23.aspx" id="RLABSUMMARYII23">��������</a> 

                </div>
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="3">ѧУ����</th>
                        <th rowspan="3">��λ����</th>
                        <th rowspan="3">ʵ���Ҹ���</th>
                        <th rowspan="3">ʵ�������</th>
                        <th colspan="4">�����豸</th>                        
                    </tr>                    
                    <tr>
                        <th rowspan="2">̨��</th>
                        <th rowspan="2">���(��)</th>
                        <th colspan="2">���й��������豸̨��</th>                      
                    </tr>                                   
                    <tr>
                        <th>̨��</th>
                        <th>���(��)</th>                   
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

