<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabInfo3.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">ʵ���һ��������(�޸ı�������)</h2>
        <div class="toolbar">
           <div class="tb_info">
               <div class="UniTab" id="tabl">
               <%if(!bLeader) {%>
                <a href="RlabInfo.aspx" id="RlabInfo">�鿴ԭʼ����</a>  
                    <a href="RlabInfo2.aspx" id="RlabInfo2">�޸ı�������</a>       
                     <%} %> 
                      <a href="RlabInfo3.aspx" id="RDevList3">��������</a>            
                </div>
                <div style="margin:10px">
                  
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
                $("#btnOp,#btnSave").button();
                $(".UniTab").UniTab();
                $("#subMit").button();
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
                    myObject.dwLabID = devid;
                    myObject.dwTNReward = 0;
                    myObject.dwTPReward = 0;
                    myObject.dwTPatent = 0;
                    myObject.dwTThreeIndex = 0;
                    myObject.dwRThreeIndex = 0;
                    myObject.dwTKernelJournal = 0;
                    myObject.dwRKernelJournal = 0;
                    myObject.dwTestBookNum = 0;
                    myObject.dwPRItemNum = 0;
                    myObject.dwRItemNum = 0;
                    myObject.dwSItemNum = 0;
                    myObject.dwPRItemNum = 0;
                    myObject.dwPTItemNum = 0;
                    myObject.dwBKThesisUsers = 0;
                    myObject.dwZKThesisUsers = 0;
                    myObject.dwSSThesisUsers = 0;
                    myObject.dwItemNum = 0;
                    myObject.dwOtherItemNum = 0;
                    myObject.dwUseUsers = 0;
                    myObject.dwOtherUsers = 0;
                    myObject.dwUseTime = 0;
                    myObject.dwOtherTime = 0;
                    myObject.dwPartTimeUsers = 0;
                    myObject.dwTotalCost = 0;
                    myObject.dwConsumeCost = 0;

                    var value = $(this).val();
                    var szValue = "";
                    if (type == "dwTNReward") {
                        myObject.dwTNReward = parseInt(value);
                    }
                    else if (type == "dwTPReward") {
                        myObject.dwTPReward = parseInt(value);
                    }
                    else if (type == "dwTPatent") {
                        myObject.dwTPatent = parseInt(value);
                    }
                    else if (type == "dwTThreeIndex") {
                        myObject.dwTThreeIndex = parseInt(value);
                    }
                    else if (type == "dwRThreeIndex") {
                        myObject.dwRThreeIndex = parseInt(value);
                    }
                    else if (type == "dwTThreeIndex") {
                        myObject.dwTThreeIndex = parseInt(value);
                    }
                    else if (type == "dwRKernelJournal") {
                        myObject.dwRKernelJournal = parseInt(value);
                    }
                    else if (type == "dwTestBookNum") {
                        myObject.dwTestBookNum = parseInt(value);
                    }
                    else if (type == "dwPRItemNum") {
                        myObject.dwPRItemNum = parseInt(value);
                    }
                    else if (type == "dwRItemNum") {
                        myObject.dwRItemNum = parseInt(value);
                    }
                    else if (type == "dwSItemNum") {
                        myObject.dwSItemNum = parseInt(value);
                    }
                    else if (type == "dwPRItemNum") {
                        myObject.dwPRItemNum = parseInt(value);
                    }
                    else if (type == "dwPTItemNum") {
                        myObject.dwPTItemNum = parseInt(value);
                    }
                    else if (type == "dwBKThesisUsers") {
                        myObject.dwBKThesisUsers = parseInt(value);
                    }
                    else if (type == "dwZKThesisUsers") {
                        myObject.dwZKThesisUsers = parseInt(value);
                    }
                    else if (type == "dwSSThesisUsers") {
                        myObject.dwSSThesisUsers = parseInt(value);
                    }
                    else if (type == "dwItemNum") {
                        myObject.dwItemNum = parseInt(value);
                    }
                    else if (type == "dwOtherItemNum") {
                        myObject.dwOtherItemNum = parseInt(value);
                    }
                    else if (type == "dwUseUsers") {
                        myObject.dwUseUsers = parseInt(value);
                    }
                    else if (type == "dwOtherUsers") {
                        myObject.dwOtherUsers = parseInt(value);
                    }
                    else if (type == "dwUseTime") {
                        myObject.dwUseTime = parseInt(value);
                    }
                    else if (type == "dwOtherTime") {
                        myObject.dwOtherTime = parseInt(value);
                    }
                    else if (type == "dwPartTimeUsers") {
                        myObject.dwPartTimeUsers = parseInt(value);
                    }
                    else if (type == "dwTotalCost") {
                        myObject.dwTotalCost = parseInt(value);
                    }
                    else if (type == "dwConsumeCost") {
                        myObject.dwConsumeCost = parseInt(value);
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

