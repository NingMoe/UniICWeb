<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckResv.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">

        <input type="hidden" id="szidh" runat="server" />
        <input type="hidden" id="szOwnerID" runat="server" />
        <input type="hidden" id="szOwnerNameH" runat="server" />
        <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size: 18px; font-weight: bold;">ԤԼ���</label>
            </div>
        </div>
        <div style="text-align: center; vertical-align: middle">          
                        <table id="idResv" class="clsResv" style="width:600px; margin: 0 auto; border-top:1px solid #000;border-left:1px solid #000; border-right:1px solid #000";cellspacing="1">
                            <tr>
                                <th class="resvth">����ռ䣺</th>
                                <td class="resvtd"> <label><%=szDevName %></label></td>
                            </tr>
                            <tr>
                                <th class="resvth">
                                    <span>����ʱ�䣺</span></th>
                                <td class="resvtd">
                                    <div>
                                        <label id="lblszResvTime" runat="server"></label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="resvth"><span>����˵����</span></th>
                                <td class="resvtd"> <label><%=szTestName%></label></td>
                            </tr>
                           
                            <tr>
                                <th class="resvth">
                                    <span>�����ˣ�</span></th>
                                <td class="resvtd">
                                    <label><%=szOwnerName%>(<%=szOwneTel%>)</label>
                                </td>
                            </tr>
                             <tr>
                                <th class="resvth">
                                    <span>������Ա��</span></th>
                                <td class="resvtd">
                                    <label><%=szGroupStudent%></label>
                                </td>
                            </tr>
                             <tr>
                    <th>
                        <div style="text-align: right; margin-top: 10px;">
                            <input type="button" id="btnCheckOK" style="height: 30px; display: none" value="������д����" />
                        </div>
                    </th>
                                 <td></td>
                </tr>
                        </table>   
          
        </div>
          <div id="divMemo" style="font-size: 14px; display: none; vertical-align: middle;">
                <div style="width: 1px; height: 10px;">
                </div>
                <div style="vertical-align: middle;">
                    <table>
                        <tr>
                            <td style="vertical-align: middle; width: 80px">
                                <label style="font-size: 14px; font-weight: bold;">��ͨ��˵��:</label>
                            </td>
                            <td style="vertical-align: middle; text-align: left">
                                <select id="szRejectReson" name="szRejectReson">
                                    <option value="�Ѿ�����������ʹ��">�Ѿ�����������ʹ��</option>
                                    <option value="�������ɲ������">�������ɲ������</option>
                                    <option value="0">����Ա�Լ�����</option>
                                </select>
                                <textarea id="szMemo" style="width: 300px; height: 50px; margin-left: 10px;"></textarea>
                            </td>
                            <td style="vertical-align: middle">
                                <div style="margin-left: 10px;">
                                      <asp:Button ID="btnOkCancel" runat="server" Text="��˲�ͨ��" />
                                    
                                </div>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        <div style="width: 99%; text-align: center;margin-top:20px;">
            <div style="margin: 0 auto;">
                <asp:Button ID="btnCheckTempOK" runat="server" Text="���ͨ��" OnClick="btnCheckTempOK_Click" />
                <input type="button" id="btnCheckTempCancel" value="��˲�ͨ��" />
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">             
         .resvth{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: right;
                height: 35px;
         }
         .resvtd{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: left;
                height: 35px;
         }
            .resvtd label {
            margin-left:10px;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {

            $(document).ready(function () {
                $("#btnCancel").button();
                $("#<%=btnCheckTempOK.ClientID%>").button();
                $("#btnCheckTempCancel").button();

                $("#btn").button();
                $("#btnCheckOK").button();
                $("#divMemo").dialog({
                    autoOpen: false,
                    height: 150,
                    width: 650,
                    modal: true,
                    show: {
                        effect: "blind",
                        duration: 1000
                    },
                    hide: {
                        effect: "blind",
                        duration: 1000
                    }
                });
                $("#divSampleFee").dialog({
                    autoOpen: false,
                    height: 450,
                    width: 450,
                    title: '�����嵥',
                    modal: true,
                    show: {
                        effect: "blind",
                        duration: 1000
                    },
                    hide: {
                        effect: "blind",
                        duration: 1000
                    }
                });
                $("#szRejectReson").change(function () {
                    if ($(this).val() == "0") {
                        $("#szMemo").show();
                        $('#<%=btnOkCancel.ClientID%>').attr('disabled', 'disabled');
                        $('#<%=btnOkCancel.ClientID%>').val('������д˵��');
                    }
                    else {
                        $("#szMemo").hide();
                    }
                });
                $("#szMemo").hide();
                $("#<%=btnOkCancel.ClientID%>").click(function (event) {
                    var id = $("input[id='<%=szidh.ClientID%>']").val();
                    var memo = "";
                    if ($("#szRejectReson").val() == "0") {
                        memo = $("#szMemo").val();
                    //} else {
                        memo = $("#szRejectReson").val();
                    }
                    var ownerID = $("#<%=szOwnerID.ClientID%>").val();
                    var ownerName = $("#<%=szOwnerNameH.ClientID%>").val();
                    $.get(
                      "../../ajaxpage/checkok.aspx",
                      { memo: memo, id: id, ownerid: ownerID, ownerName: ownerName },
                      function (data) {
                          if (data == "success") {
                              MessageBox("��˲�ͨ��", "��ʾ", 3, function () { Dlg_OK() });
                          }
                          else {
                              MessageBox("���ʧ��" + data, "��ʾ", 3, function () { Dlg_OK() });
                          }

                      }
                    );
                });

                $("#<%=btnOkCancel.ClientID%>").button();
                $("#btnCancel").button();
                $("#btnChangeTime").button();
                $('#btnCheckOK').attr('disabled', 'disabled');
                //$('#<%=btnOkCancel.ClientID%>').attr('disabled', 'disabled');
                function GetTimeForSecond(uMin) {
                    var szTime = "";
                    if ((uMin / 86400) >= 1) {
                        szTime += parseInt(((uMin / 86400))) + "��";
                    }
                    if (((uMin % 86400) / 3600) >= 1) {
                        szTime += GetInt(((uMin % 86400) / 3600)) + "Сʱ";
                    }
                    if ((((uMin % 86400) % 3600) / 60) >= 1) {
                        szTime += GetInt((((uMin % 86400) % 3600) / 60)) + "����";
                    }
                    return szTime;
                }
                function GetInt(s) {
                    s = s.toString();
                    var rs = s.indexOf('.');
                    if (rs < 0) {
                        rs = s.length;
                        s += "";
                    }
                    s = s.substring(0, rs);
                    return s;
                }
                function GetFee2(uintFee, uintTime, totalTime) {
                    if (totalTime <= 0) {
                        return 0;
                    }
                    var vUintfee = parseFloat(uintFee);
                    var vUintTime = parseFloat(uintTime);
                    var vTotalTime = parseFloat(totalTime);
                    return (vUintfee * vTotalTime / vUintTime).toFixed(2);
                }
                function GetFeeYuan2(uintFee, uintTime, totalTime) {
                    var vUintfee = parseFloat(uintFee);
                    var vUintTime = parseFloat(uintTime);
                    var vTotalTime = parseFloat(totalTime);
                    var vRes = vUintfee * vTotalTime / vUintTime;
                    return toDecimal(vRes / 100);
                }
                $("#aSampleFee").click(function () {

                    $("#divSampleFee").dialog("open");
                });
                // Link to open the dialog
                $("#btnCheckTempOK").click(function (event) {
                    {
                        $("#divMemo").hide();
                        //$("#btnCheckOK").click();
                    }

                });
                $("#btnCheckTempCancel").click(function (event) {
                    {
                        $("#divResvTime").hide();
                        $("#divFee").hide();
                        $("#divMemo").dialog("open");
                    }
                });
                $("#btnChangTime").click(function (event) {
                    if ($("#divResvTime").is(":hidden")) {
                        $("#divResvTime").slideDown();
                    }
                    else {
                        $("#divResvTime").hide();
                    }
                });

                $("#btnCancel").click(function (event) {

                });
                $("#szMemo").change(function () {
                    if ($("#szMemo").val() != "") {
                        $('#<%=btnOkCancel.ClientID%>').removeAttr("disabled");
                        $('#<%=btnOkCancel.ClientID%>').val("��˲�ͨ��");
                    }
                    else {
                        $('#<%=btnOkCancel.ClientID%>').attr('disabled', 'disabled');
                        $('#<%=btnOkCancel.ClientID%>').val("������д˵��");
                    }
                });
                $(".txtRelCost").change(function () {
                    var sum = 0;
                    $(".txtRelCost").each(function () {

                        if ($(this).val() != "" && !isNaN($(this).val())) {
                            sum = sum + parseInt($(this).val());
                        }
                    });
                    $("#txtSum").val(sum.toString());
                });
              
            });

            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
