<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevChange2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
         <input type="hidden" name="opSub" id="opSub" value="0" />
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
                   <input type="submit" value="�������޸ĵ�����" id="btnSave" />
                     <input type="button" value="�����޸ĺõ�����" id="btnOp" style="margin-left:10px" />
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
                $("#btnOp,#btnSave").button();
                $("#btnOp").click(function () {

                    $("#opSub").val("1");
                    TabReload($(this).parents("form").serialize());
                });
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });               
            });
            $("input[name='szLab'],input[name='szRoom'],input[name='szDevKind']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
            });
            var oDevchange = new Object();
            oDevchange.dwBDevNum = 0;
            oDevchange.dwBMoney = 0;
            oDevchange.dwBBigDevNum = 0;
            oDevchange.dwBBigMoney = 0;
            oDevchange.dwIncDevNum = 0;
            oDevchange.dwIncMoney = 0;
            oDevchange.dwIncBigDevNum = 0;
            oDevchange.dwIncBigMoney = 0;
            oDevchange.dwEMoney = 0;
            oDevchange.dwEBigMoney = 0;
            oDevchange.dwDecBigDevNum = 0;
            oDevchange.dwDecBigMoney = 0;
            oDevchange.dwDecMoney = 0;
            oDevchange.dwBDevNum = 0;
            $('.tdSet').click(function () {
                var vtd = $(this);
                var devid = vtd.parents("tr").children().first().data("id");
             
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
                    if (type == "dwBDevNum") {
                        
                        oDevchange.dwBDevNum = parseInt(value);
                    }
                    else if (type == "dwBMoney") {
                        
                        oDevchange.dwBMoney = parseInt(value);
                    }
                    else if (type == "dwBBigDevNum") {
                        
                        oDevchange.dwBBigDevNum = parseInt(value);
                    }
                    else if (type == "dwBBigMoney") {
                        
                        oDevchange.dwBBigMoney = parseInt(value);
                    }
                    else if (type == "dwIncDevNum") {
                        
                        oDevchange.dwIncDevNum = parseInt(value);
                    }
                    else if (type == "dwIncMoney") {
                        
                        oDevchange.dwIncMoney = parseInt(value);
                    }
                    else if (type == "dwDecDevNum") {
                        
                        oDevchange.dwDecDevNum = parseInt(value);
                    }
                    else if (type == "dwDecMoney") {
                        
                        oDevchange.dwDecMoney = parseInt(value);
                    }
                    else if (type == "dwEDevNum") {
                        
                        oDevchange.dwEDevNum = parseInt(value);
                    }
                    else if (type == "dwEMoney") {
                        
                        oDevchange.dwEMoney = parseInt(value);
                    }
                    else if (type == "dwEBigDevNum") {
                        
                        oDevchange.dwEBigDevNum = parseInt(value);
                    }
                    else if (type == "dwEBigMoney") {
                        
                        oDevchange.dwEBigMoney = parseInt(value);
                    }
                    szValue = $.toJSON(oDevchange);
                    $("#changeInfo").val(szValue);
                    vtd.empty();
                    vtd.html(value);
                });

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

