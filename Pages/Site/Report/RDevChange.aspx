<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevChange.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">��ѧ���������豸�����䶯�����(�鿴ԭʼ����)</h2>        
        <div class="toolbar">
               
             
            <div class="tb_info">

                 <div class="UniTab" id="tabl">
                       <%if(!bLeader) {%>

                <a href="RDevChange.aspx" id="RDevChange">�鿴ԭʼ����</a>  
                    <a href="RDevChange2.aspx" id="RDevChange2">�޸Ĳ���������</a>  

                      <%} %>               
                     <a href="RDevChange3.aspx" id="RDevList3">��������</a> 
   
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
                        <th colspan="4">��ѧ��ĩʵ����</th>
                        <th colspan="2">��ѧ��������</th>
                        <th colspan="2">��ѧ�������</th>
                        <th colspan="4">��ѧ��ĩʵ����</th>                       
                    </tr>
                     <tr>
                        <th rowspan="2">̨��</th>
                        <th rowspan="2">���(Ԫ)</th>
                        <th colspan="2">����10��Ԫ(��)����</th>
                      
                        <th rowspan="2">̨��</th>
                        <th rowspan="2">���(Ԫ)</th>                          
                        <th rowspan="2">̨��</th>
                        <th rowspan="2">���(Ԫ)</th>                         
                        <th rowspan="2">̨��</th>
                        <th rowspan="2">���(Ԫ)</th>   
                        <th colspan="2">����10��Ԫ(��)����</th>                 
                    </tr>
                     <tr>
                         <th>̨��</th>
                        <th>���(Ԫ)</th> 
                           <th>̨��</th>
                        <th>���(Ԫ)</th>                                       
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
            $("input[name='szLab'],input[name='szRoom'],input[name='szDevKind']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
              });

            $("#btnOK").button();           
        </script>
        <style>
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

