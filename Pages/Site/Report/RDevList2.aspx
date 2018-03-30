<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevList2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" name="changeInfo" id="changeInfo" />
        <input type="hidden" name="opSub" id="opSub" value="0" />

        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">��ѧ���������豸��(�޸Ĳ���������)</h2>        
        <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                      <%if(!bLeader) {%>
                    <a href="RDevList.aspx" id="RDevList">�鿴ԭʼ����</a>  
                    <a href="RDevList2.aspx" id="RDevList2">�޸Ĳ���������</a>                
                     <%} %> 
                      <a href="RDevList3.aspx" id="RDevList3">��������</a>                
                </div>
                <div style="margin:10px">
                    <input type="submit" value="�������޸ĵ�����" id="btnSave" />
                     <input type="button" value="�����޸ĺõ�����" id="btnOp" style="margin-left:10px" />

                </div>
             
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ѧУ����</th>
                        <th>�������</th>
                        <th>�����</th>
                        <th>��������</th>
                        <th>�ͺ�</th>                      
                        <th>���</th>                  
                        <th>������Դ</th>
                        <th>������</th>
                        <th>����</th>                                               
                        <th>��������</th>  
                        <th>��״��</th>  
                        <th>ʹ�÷���</th>  
                        <th>��λ���</th>  
                        <th>��λ����</th>  
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
                $("#btnOp,#btnSave").button();
                $("#btnOp").click(function () {
                     $("#opSub").val("1");
                     TabReload($(this).parents("form").serialize());
                });
                $(".UniTab").UniTab();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });               
            });
            $('.tdSet').click(function () {
                var vtd = $(this);
                var devid = vtd.parents("tr").children().first().data("id");
                var YearTerm = vtd.parents("tr").children().first().data("yearterm");
               
                var labid = vtd.parents("tr").children().first().data("labid");
             
                var vInputList = $(":input", vtd);
                if (vInputList.length > 0) {
                    return;
                }
                var html = vtd.html();
                var input = $("<input type='text' style='width:70px' />");
                input.val(html);
                vtd.empty();
                vtd.append(input);
                input.focus();
                var type = vtd.data("type");
                $(":input", vtd).on("blur", function (event) {
                    var value = $(this).val();
                    var szValue = "";
                    if (type == "dwStatCode") {
                        szValue = "{\"dwYearTerm\":" + YearTerm + ",\"dwDevID\":" + devid + ",\"dwStatCode\":" + value + ",\"szDeptSN\":\"" + "" + "\",\"dwLabID\":" + labid + "},";
                    }
                    else if (type == "szDeptSN") {
                        szValue = "{\"dwYearTerm\":" + YearTerm + ",\"dwDevID\":" + devid + ",\"dwStatCode\":" + "0" + ",\"szDeptSN\":\"" + value + "\",\"dwLabID\":" + labid + "},";
                    }
                    $("#changeInfo").val($("#changeInfo").val() + szValue);
                    vtd.empty();
                    vtd.html(value);
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

