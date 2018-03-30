<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckBG.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">

        <input type="hidden" id="szidh" runat="server" />
        <input type="hidden" id="szResvT" runat="server" />
        <input type="hidden" id="feeType" runat="server" />
        <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size: 18px; font-weight: bold;">
                    预约审核</label>
            </div>
        </div>
        <div style="text-align: center; vertical-align: middle">
            <table style="width: 600px; margin: 0 auto; border-left: 1px solid #000;border-right: 1px solid #000;border-top: 1px solid #000" cellspacing="1">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="text-align: center; width: 300px;">
                                    <div>
                                        <span class="lblHead">仪器名称：</span><%=szDevName %>
                                    </div>
                                    <div>
                                        <span class="lblHead">所属实验室：</span><%=szLabName%>
                                    </div>
                                </td>
                                <td style="text-align: right; width: 300px;">
                                    <div style="margin: 10px auto">
                                        <asp:Image ID="imgPic" runat="server" ImageUrl="801.jpg" Width="120" Height="85" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="border: 1px solid #000">
                    <td>
                        <table>
                            <tr>
                                <td rowspan="2" style="width: 100px; background-color: #f5f5f5; height: 45px;">
                                    <span style="font-size: 14px; font-weight: bold; color: #1668b9">申请时间：</span></td>
                                <td>
                                    <div style="margin-left: 15px;">
                                       <label id="lblszResvTime" runat="server"></label>
                                        <a href="#" id="btnChangTime" style="color: blue">调整时间</a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divResvTime" style="display: none">
                                        开始时间:<input type="text" id="dwBegin" style="width: 120px;" runat="server" class="txtRelCost" />
                                        结束时间:<input type="text" id="dwEnd" style="width: 120px;" runat="server" class="txtRelCost" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="border: 0px solid #000">
                   
                </tr>
                <tr style="border:1px solid #000">
                    <td>
                        <div style="background-color: #f5f5f5; height: 20px; text-align: left;vertical-align:middle;">
                          
                        <span style="font-size: 14px; font-weight: bold; color: #1668b9; margin-left: 15px;">实验相关信息</span>
                    
                        </div>
                        <div class="apply_item">
                            <table>
                                 <tr>
                                    <td class="tdResvContext">
                                        <span class="lblHead">实验<%=ConfigConst.GCReachTestName%>：</span></td>
                                      <td class="tdResvContext2"><%=szRTName%>
                                    </td>
                               
                                    <td class="tdResvContext">
                                        <span class="lblHead">实验名称：</span></td>
                                      <td class="tdResvContext2"><%=szTestName%>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="tdResvContext">
                                        <span class="lblHead"><%=ConfigConst.GCTutorName%>：</span></td>
                                     <td class="tdResvContext2">
                                        <label><%=szTutorName%>(<%=szTurtorTel%>)</label>
                                    </td>
                                    <td class="tdResvContext">
                                        <span class="lblHead">申请人：</span></td>
                                      <td class="tdResvContext2">
                                        <label><%=szOwnerName%>(<%=szOwneTel%>)</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdResvContext">
                                        <span class="lblHead">委托检测：</span></td>
                                     <td class="tdResvContext2">
                                        <label id="dwCheckBy"><%=dwProperty%></label>
                                    </td>
                              
                                    <td class="tdResvContext">
                                        <span class="lblHead">耗材自带：</span></td>
                                     <td class="tdResvContext2">
                                        <label><%=dwComsubleProperty%></label>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="tdResvContext">
                                       <span class="lblHead"><%=ConfigConst.GCReachTestName%>成员：</span></td>
                                    <td class="tdResvContext2">
                                        <label><%=szGroupStudent%></label>
                                    </td>
                                     <td class="tdResvContext">
                                         <span class="lblHead">服务对象：</span>
                                       <!-- <span class="lblHead">样本数：</span>--></td>
                                      <td class="tdResvContext2">
                                           <label><%=szPurpose%></label>
                                       <!--  <label><%=szConsumables%></label>-->
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                 <tr style="border: 1px solid #000">
                     <td>
                         <table>
                             <tr>
                                 <td style="width: 100px; background-color: #f5f5f5; height: 30px;">
                        <div>
                            <span class="lblHead" style="font-size: 14px; font-weight: bold; color: #1668b9">申请说明：</span>
                           
                        </div>
                    </td>
                     <td> 
                         <div style="margin-left:20px;"><label><%=szResvInfo%></label>
                             </div></td>
                             </tr>

                         </table>
                     </td>
                    
                </tr>
                <tr>
                    <td>
                   <div id="tblFee">
                            <table>
                                <tr>
                                    <th  class="thFee">收费类别</th>
                                    <th  class="thFee">费率</th>
                                    <th  class="thFee">计费单位</th>
                                    <th  class="thFee">预估费用</th>
                                </tr>
                                <tr id="divUseDev" style="display: none;" runat="server">
                                    <td>使用费:</td>
                                    <td>
                                         <asp:Label ID="lblUseDev" runat="server"></asp:Label>元/每小时
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUseDevFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblUseDevTotal" runat="server"></label>
                                        <input type="hidden" id="hiddenUseDevTotal" name="hiddenUseDevTotal" runat="server" />
                                    </td>
                                </tr>
                                <tr id="divOccupy" style="display: none;border: 1px solid #000" runat="server">
                                    <td>占用费:</td>
                                    <td>
                                    <asp:Label ID="lblOccupy" runat="server"></asp:Label>元/每小时
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOccupyFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblOccupyTotal" runat="server"></label>                                     
                                    </td>
                                </tr>
                                <tr id="divASSIST" style="display: none;border: 1px solid #000" runat="server">
                                    <td>协助费:</td>
                                    <td>
                                        <asp:Label ID="lblASSIS" runat="server"></asp:Label>元/每小时
                                    </td>
                                    <td>
                                        <asp:Label ID="lblASSISFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblASSISTotal" runat="server"></label>                                     
                                    </td>
                                </tr>
                                <tr id="divTIMEOUT" style="display: none;border: 1px solid #000" runat="server">
                                    <td>超时费:</td>
                                    <td>
                                        <asp:Label ID="lblTIMEOUT" runat="server"></asp:Label>元(每小时)
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTIMEOUTFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblTIMEOUTTotal" runat="server"></label>
                                    </td>
                                </tr>
                                 <tr id="divSample" style="display: none;border: 1px solid #000" runat="server">
                                    <td>样本费:</td>
                                    
                                     <td colspan="2">
                                         <!-- <asp:Label ID="lblSample" runat="server"></asp:Label>元&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-->
                                         <!--<asp:Label ID="lblSampleFee" runat="server"></asp:Label>份-->
                                         <div>
                                             <%=szSampleInfo %>
                                         </div>
                                     </td>
                                    <td>
                                        <label id="lblSampleTotal" runat="server"></label>
                                        <input type="hidden" id="hiddenSampleTotal" name="hiddenSampleTotal" runat="server" />
                                    </td>
                                </tr>
                                
                                <tr id="divRESVDEV" style="display: none;border: 1px solid #000" runat="server">
                                    <td>预约使用费:</td>
                                    <td>
                                        <asp:Label ID="lblRESVDEV" runat="server"></asp:Label> 元/每小时
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRESVDEVFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblRESVDEVTotal" runat="server"></label>
                                    </td>
                                </tr>
                                <tr id="divENTRUST" style="display: none;border: 1px solid #000" runat="server">
                                    <td>代检费:
                                    </td>
                                    <td>
                                         <asp:Label ID="lblENTRUST" runat="server"></asp:Label>元/每小时
                                    </td>
                                    <td>
                                        <asp:Label ID="lblENTRUSTFee" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <label id="lblENTRUSTTotal" runat="server"></label>
                                        <input type="hidden" id="hiddenENTRUSTTotal" name="hiddenENTRUSTTotal" runat="server" />
                                    </td>
                                </tr>
                                <tr id="divCONSUMABLE" style="display: none;border: 1px solid #000" runat="server">
                                    <td>耗材费:</td>
                                    <td colspan="3" style="text-align:right;">
                                          <!--<asp:Label ID="lblCONSUMABLEFee" runat="server"></asp:Label>-->
                                         <!--<asp:Label ID="lblCONSUMABLE" runat="server"></asp:Label>元-->
                                        <label style="color:blue" href="#">此处输入</label>:<input id="txtCONSUMABLETotal" runat="server" value="0" />
                                        <!--<label id="lblCONSUMABLETotal" runat="server"></label>-->
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align:right">合计</td>
                                    <td style="text-align: center">                                      
                                         <label id="lblSum" runat="server" style="width: 50px"></label>
                                    </td>
                                </tr>
                            </table>
                       </div>
                          </td>                   
                         </tr>
                    <tr>
                        <td>
                        <div style="text-align: right; margin-top: 10px; width: 630px">
                                <input type="button" id="btnCheckOK" style="width: 120px; height: 30px; display: none" value="必须填写费用" />
                            </div>
                     
                    </td>
                </tr>
              
            </table>
            <div id="divSampleFee">
                <%=szSampleInfoTitle %>
            </div>
            <div id="divMemo" style="font-size: 14px; display: none; vertical-align: middle;">
                            <div style="width: 1px; height: 10px;">
                               
                            </div>
                <div style="vertical-align:middle;">
                    <table>
                        <tr>
                            <td style="vertical-align:middle;width:80px">
                            <label style="font-size:14px;font-weight:bold;">不通过说明:</label> 
                                </td>
                            <td style="vertical-align:middle;text-align:left">
                                <select id="szRejectReson" name="szRejectReson">
                                    <option value="已经安排其他人使用">已经安排其他人使用</option>
                                    <option value="申请理由不够充分">申请理由不够充分</option>
                                    <option value="0">管理员自己输入</option>
                                </select>
            <textarea id="szMemo" style="width: 300px; height: 50px;margin-left:10px;"></textarea>
                                </td>
                            <td style="vertical-align:middle">
                                <div style="margin-left:10px;">
                                    
                            <input type="button" id="btnOkCancel" value="审核不通过" style="width: 120px; height: 30px;" />
                                </div>

                                </td>
                            </tr>
                        </table>
                    </div>
                        </div>
      <div style="width: 99%; text-align: center">
                            <div style="margin: 0 auto;">                                
                                <asp:Button ID="btnCheckTempOK" runat="server" Text="审核通过" OnClick="btnCheckTempOK_Click" />
                                <input type="button" id="btnCheckTempCancel" value="审核不通过" />                                
                            </div>
                        </div>       
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .txtRelCost
        {
            width: 50px;
        }

        .txtRelCost2
        {
            width: 50px;
            background-color:#eeeeee;
        }
        .thFee
        {
            width:157px;text-align:center;height:30px; vertical-align:middle;font-size:14px;font-weight:bold;background-color:#f5f5f5;
        }
        #tblFee table{width:650px;border-bottom:1px solid #000}
        #tblFee table td{border-left:1px solid #000;height:30px; border-top:1px solid #000}
         #divSampleFee table{width:650px;border-bottom:1px solid #000}
        #divSampleFee table td{border:1px solid #000;height:30px; border-top:1px solid #000}
       
        .tdResvContext
        {
            text-align: left;
            vertical-align:middle;
            width:110px;
            height:22px;
        }
         .tdResvContext2{
          text-align: left;
          vertical-align:middle;  
        }
         .tdResvContext span
        {
           margin-left:25px;           
        }
        .lblHead
        {
            color: #000;
            width: 120px;
            font-size: 13px;
            font-weight: bold;
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
                $( "#divMemo" ).dialog({
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
                    title:'样本清单',
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
                $("#<%=txtCONSUMABLETotal.ClientID%>").change(function () {
                    $("#<%=lblSum.ClientID%>").html(parseFloat($("#<%=lblSum.ClientID%>").html()) + parseFloat($(this).val()));
                });
                $("#szRejectReson").change(function () {
                    if ($(this).val() == "0") {
                        $("#szMemo").show();
                        $('#btnOkCancel').attr('disabled', 'disabled');
                        $('#btnOkCancel').val('必须填写说明');
                    }
                    else {
                        $("#szMemo").hide();
                    }
                });
                $("#szMemo").hide();
                $("#btnOkCancel").click(function (event) {
                    var id = $("input[id='<%=szidh.ClientID%>']").val();      
                    var memo="";
                    if ($("#szRejectReson").val() == "0") {
                        memo = $("#szMemo").val();
                    } else {
                        memo = $("#szRejectReson").val();
                    }
                    $.get(
                      "../../ajaxpage/checkok.aspx",
                      { memo: memo, id: id },
                      function (data) {
                          if (data == "success") {
                              MessageBox("审核不通过", "提示", 3, function () { Dlg_OK() });
                          }
                          else {
                              MessageBox("审核失败" + data, "提示", 3, function () { Dlg_OK() });
                          }

                      }
                    );
                });

                $("#btnOkCancel").button();
                $("#btnCancel").button();
                $("#btnChangeTime").button();
                $('#btnCheckOK').attr('disabled', 'disabled');
                //$('#btnOkCancel').attr('disabled', 'disabled');

                $("#<%=dwEnd.ClientID%>,#<%=dwBegin.ClientID%>").datetimepicker({   
                    stepHour: 1,
                    stepMinute: 10                  
                });
                $("#<%=dwEnd.ClientID%>,#<%=dwBegin.ClientID%>").change(function () {
                    $("#<%=lblszResvTime.ClientID%>").css({ color: "red" });
                    var dateBegin = $("#<%=dwBegin.ClientID%>").val();
                    var dateEnd = $("#<%=dwEnd.ClientID%>").val();
                    dateBegin = dateBegin.replace(/-/g, "/");
                    dateEnd = dateEnd.replace(/-/g, "/");
                    dateBegin = new Date(dateBegin);
                    dateEnd = new Date(dateEnd);

                    var minSeconds = (dateEnd.getTime() - dateBegin.getTime());
                    var Mins = minSeconds/(1000);
                    var Minstr = GetTimeForSecond(Mins);
                   

                    //设置表单时间
                    $("#<%=lblUseDevFee.ClientID%>").html(Minstr);                    
                    $("#<%=lblENTRUSTFee.ClientID%>").html(Minstr);
                    Mins = Mins / 60;
                    var UseDevFee = GetFee2($("#<%=lblUseDev.ClientID%>").html(), 60, Mins);
                    var EntrusFee = GetFee2($("#<%=lblENTRUST.ClientID%>").html(), 60, Mins);
                    var total = parseFloat(UseDevFee) + parseFloat(GetFee2($("#<%=lblSampleTotal.ClientID%>").html(), 60, Mins));
                    var isCheckBy = $("#dwCheckBy").html();
                    if(isCheckBy=="是")
                    {
                        total = total + parseFloat(EntrusFee);
                    }                  

                    $("#<%=hiddenUseDevTotal.ClientID%>").val(UseDevFee);                    
                    $("#<%=hiddenENTRUSTTotal.ClientID%>").val(EntrusFee);
                    $("#<%=lblUseDevTotal.ClientID%>").html(UseDevFee);
                    $("#<%=lblENTRUSTTotal.ClientID%>").html(EntrusFee);

                    $("#<%=lblSum.ClientID%>").html(total.toFixed(2));
                });
                function GetTimeForSecond(uMin)
                {
                    var szTime ="";
                    if((uMin / 86400)>=1)            
                    {
                        szTime += parseInt(((uMin / 86400))) + "天";
                    }
                    if (((uMin % 86400) / 3600)>=1)
                    {
                        szTime += GetInt(((uMin % 86400) / 3600)) + "小时";
                    }
                    if ((((uMin % 86400) % 3600) / 60) >= 1)
                    {
                        szTime += GetInt((((uMin % 86400) % 3600) / 60)) + "分钟";
                    }                       
                    return szTime;
                }
                function GetInt(s)
                {
                    s = s.toString();
                    var rs = s.indexOf('.');
                    if (rs < 0)
                    {
                        rs = s.length;
                        s += "";
                    }
                    s = s.substring(0,rs);                  
                    return s;
                }              
                function GetFee2(uintFee, uintTime, totalTime) {
                    if (totalTime <= 0)
                    {
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
                        $('#btnOkCancel').removeAttr("disabled");
                        $('#btnOkCancel').val("审核不通过");
                    }
                    else {
                        $('#btnOkCancel').attr('disabled', 'disabled');
                        $('#btnOkCancel').val("必须填写说明");
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
                var resvTime = $("#<%=szResvT.ClientID %>").val();
               
            });
           
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
