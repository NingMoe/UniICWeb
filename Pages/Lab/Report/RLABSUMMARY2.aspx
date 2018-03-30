<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLABSUMMARY2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
         <input type="hidden" name="opSub" id="opSub" value="0" />

        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">�ߵ�ѧУʵ�����ۺ���Ϣ��һ(�޸ı�������)</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
               <%if(!bLeader) {%>
            <a href="RLABSUMMARY.aspx" id="RLABSUMMARY">�鿴ԭʼ����</a>  
                    <a href="RLABSUMMARY2.aspx" id="RLABSUMMARY2">�޸ı�������</a>    
                    
 <%} %>                 
                     <a href="RLABSUMMARY3.aspx" id="RDevList3">��������</a>                
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
                var vInputList = $(":input", vtd);
                if (vInputList.length > 0) {
                    return;
                }
                var html = vtd.html();
                var input = $("<input type='text' style='width:40px' />");
                input.val(html);
                vtd.empty();
                vtd.append(input);
                input.focus();
                var type = vtd.data("type");

                $(":input", vtd).on("blur", function (event) {
                    var myObject = new Object();
                    myObject.dwLabNum = 0;
                    myObject.dwLabArea = 0;
                    myObject.dwDevNum = 0;
                    myObject.dwDevMoney = 0;
                    myObject.dwBigDevNum = 0;
                    myObject.dwBigMoney = 0;
                    myObject.dwBigDevNum = 0;
                    myObject.dwTItemNum = 0;
                    myObject.dwTUseTime = 0;
                    myObject.dwDUseTime = 0;
                    myObject.dwMUseTime = 0;
                    myObject.dwUUseTime = 0;
                    myObject.dwJUseTime = 0;
                    myObject.dwRItemNum = 0;
                    myObject.dwHTStaff = 0;
                    myObject.dwMTStaff = 0;
                    myObject.dwHSStaff = 0;
                    myObject.dwMSStaff = 0;
                    myObject.dwOtherStaff = 0;
                    myObject.dwPartTimeStaff = 0;
                    myObject.dwPaperNum = 0;
                    myObject.dwTReward = 0;
                    myObject.dwSReward = 0;
                    var value = $(this).val();
                    var szValue = "";
                    debugger;
                    for (var p in myObject)
                    {
                        if (p.toString() == type)
                        {
                            myObject[p] = parseInt(value);
                        }
                    }
                    szValue = $.toJSON(myObject);
                    if ($("#changeInfo").val() == "") {
                        $("#changeInfo").val(szValue);
                    } else {
                        $("#changeInfo").val($("#changeInfo").val() + "," + szValue);
                    }

                    vtd.empty();
                    vtd.html(value);
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

