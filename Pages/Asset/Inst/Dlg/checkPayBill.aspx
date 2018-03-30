<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="checkPayBill.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <input type="hidden" runat="server" id="hiddenSampleList" />
        <input type="hidden" runat="server" id="kind" />
        <input type="hidden" runat="server" id="devID" />
        <div style="width: 100%">
            <div style="width: 99%; margin: 0 auto; text-align: center">
                <label style="font-size: 18px; font-weight: bold;">
                    结算</label>
            </div>
        </div>
        <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />

        </div>
        <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
        <div class="tem_panel">


            <div align="center">
                <hr style="border: 1px; width: 99%; height: 1px" />
            </div>
            <table style="width: 600px; margin: 0 auto; border-left: 1px solid #000; border-right: 1px solid #000; border-top: 1px solid #000" cellspacing="1">

                <tr style="border: 1px solid #000">
                    <td>
                        <div class="apply_item">
                            <table>
                                <tr>
                                    <td class="tdResvContext">
                                        <div>
                                            <span class="lblHead"><%=ConfigConst.GCDevName %>信息：</span>
                                        </div>

                                    </td>
                                    <td class="tdResvContext2">
                                        <%=szDevName %>
                                    </td>
                                    <!--
                                <td style="text-align: right; width: 400px;">
                                    <div style="margin: 10px auto">
                                        <asp:Image ID="imgPic" runat="server" ImageUrl="801.jpg" Width="189" Height="100" />
                                    </div>
                                </td>
                                -->

                                    <td class="tdResvContext">
                                        <span class="lblHead">申请时间：</span></td>
                                    <td class="tdResvContext2">
                                        <label id="lblszResvTime" runat="server"></label>
                                    </td>
                                </tr>
                                <!--
                            <tr>
                                <td class="tdResvContext">
                                    <div id="divResvTime">
                                        开始时间:<input type="text" id="dwBegin" style="width: 120px;" runat="server" class="txtRelCost" />
                                        结束时间:<input type="text" id="dwEnd" style="width: 120px;" runat="server" class="txtRelCost" />
                                    </div>
                                </td>
                            </tr>
                                -->

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


                                    <!--
                                <tr>
                                    <td class="tdResvContext">
                                        <span  class="lblHead">实验负责人：</span></td>
                                     <td class="tdResvContext2">
                                        <label><%=szManGroupName%></label>
                                    </td>
                                </tr>
                                -->

                                    <td class="tdResvContext">
                                        <span class="lblHead">服务对象：</span></td>
                                    <td class="tdResvContext2">
                                        <label><%=szPurpose%></label>
                                    </td>

                                    <td class="tdResvContext">
                                        <span class="lblHead"><%=ConfigConst.GCReachTestName%>成员：</span></td>
                                    <td class="tdResvContext2">
                                        <label><%=szGroupStudent%></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdResvContext"><span class="lblHead">使用时间:</span></td>
                                    <td style="text-align: center;"><%=szUseTotalTime %></td>
                                    <td class="tdResvContext"><span class="lblHead">申请时长:</span></td>
                                    <td style="text-align: center;"><%=szResvTimeTotal %></td>
                                </tr>
                                  <tr>
                                    <td class="tdResvContext"><span class="lblHead">结算经费卡号(*):</span></td>
                                    <td style="text-align: center;"><input type="text" name="szFundsNo" id="szFundsNo" runat="server" class="validate[required]  txtRelCost2" style="width:120px" /></td>
                                    <td class="tdResvContext"><span class="lblHead"></span></td>
                                    <td style="text-align: center;"></td>
                                </tr>
                                
                            </table>
                        </div>
                    </td>
                </tr>

            </table>
            <div id="divSamleList">
                <div style="margin:10px auto;text-align:left">
                    <div  style="margin:5px auto;text-align:center;font-size:16px;font-weight:bold;">
                        样本总费用:<label id="idTotalFee" runat="server"></label>
                    </div>
                    <div><input type="button" value="添加其他样品" id="btnAddSample" /></div>
                    <div  id="divAddSample" style="margin-top:5px">
                        <input type="hidden" id="AddSampleID" />
                       <table id="tblAdd">
                           <tr>
                               <td>样品名称:</td>
                               <td><input type="text" id="addSampleName" style="width:100px" /></td>
                               <td>单价:</td>
                               <td style="width:40px"><label id="addSamplePrice"></label></td>
                               <td>份数:</td>
                               <td><input type="text" name="addSampleNum" id="addSampleNum" style="width:25px" /></td>
                               <td><input type="button" value="确定增加" id="btnAddSampleOK" /></td>
                           </tr>
                       </table>
                    </div>
                   
                </div>
                <div id="divSampleTableList">
                   <%=szSampleInfoTitle %>
                </div>
                 <div style="margin:10px auto;text-align:center;">
                        <input type="button" value="保存并关闭清单页面" id="saveSampleList" style="height:40px" />
                    </div>
            </div>
            <div class="apply_item" style="width: 99%">
                <table style="margin: 20px auto;" id="tblFeeList">
                    <tr>
                        <th>收费类别</th>
                        <th>费率</th>
                        <th>预估费用
                        </th>
                        <th>实收
                        </th>
                    </tr>
                    <tr id="divUseDev" style="display: none;" runat="server">
                        <td>使用费:</td>
                        <td>
                            <asp:Label ID="lblUseDevFee" runat="server"></asp:Label>
                            元/每天
                        </td>

                        <td>
                            <asp:Label ID="lblUseDevTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblUseDevTotalReal" runat="server" CssClass="txtRelCost" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divOccupy" style="display: none;" runat="server">
                        <td>占用费:</td>
                        <td>
                            <asp:Label ID="lblOccupy" runat="server"></asp:Label>
                            元/每天
                        </td>

                        <td>
                            <asp:Label ID="lblOccupyTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblOccupyTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divASSIST" style="display: none;" runat="server">
                        <td>协助费:</td>
                        <td>
                            <asp:Label ID="lblASSIS" runat="server"></asp:Label>
                            元/每天
                        </td>

                        <td>
                            <asp:Label ID="lblASSISTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblASSISTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divTIMEOUT" style="display: none;" runat="server">
                        <td>超时费:</td>
                        <td>
                            <asp:Label ID="lblTIMEOUT" runat="server"></asp:Label>
                            元(每天)
                        </td>

                        <td>
                            <asp:Label ID="lblTIMEOUTTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblTIMEOUTTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divCONSUMABLE" style="display: none;" runat="server">
                        <td>耗材费:</td>
                        <td>
                            <asp:Label ID="lblCONSUMABLE" runat="server"></asp:Label>

                        </td>

                        <td>
                            <asp:Label ID="lblCONSUMABLETotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblCONSUMABLETotalReal" runat="server" CssClass="txtRelCost txtRelCost2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divSample" style="display: none;" runat="server">
                        <td>样本费:</td>
                        <td colspan="2">
                            <!--
                            <asp:Label ID="lblSampleTotal" runat="server"></asp:Label>
                            <asp:Label ID="lblSample" runat="server"></asp:Label>
                        -->
                            <div><%=szSampleInfo %></div>    
                        </td>
                        <td>
                            <asp:TextBox ID="lblSampleTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="divRESVDEV" style="display: none;" runat="server">
                        <td>预约使用费:</td>
                        <td>
                            <asp:Label ID="lblRESVDEV" runat="server"></asp:Label>元/每天
                        </td>

                        <td>
                            <asp:Label ID="lblRESVDEVTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblRESVDEVTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>

                    <tr id="divENTRUST" style="display: none;" runat="server">
                        <td>代检费:
                        </td>
                        <td>
                            <asp:Label ID="lblENTRUST" runat="server"></asp:Label>元/每天
                        </td>

                        <td>
                            <asp:Label ID="lblENTRUSTTotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="lblENTRUSTTotalReal" runat="server" CssClass="txtRelCost"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="font-size: 14px; text-align: center">预估总计<asp:Label ID="lblSum" runat="server"></asp:Label>;
                                 <!-- 已预收
                            <asp:Label ID="lblYuShou" runat="server"></asp:Label>元-->
                        </td>
                        <td>
                            <table style="width: 156px;">
                                <tr>
                                    <th style="width: 72px">实收</th>
                                    <th style="width: 73px">折扣率(%)</th>
                                </tr>
                                <tr>
                                    <td style="width: 35px">
                                        <asp:TextBox ID="txtSum" runat="server" CssClass="txtRelCost2"></asp:TextBox>
                                    </td>
                                    <td style="width: 40px">
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="txtRelCost2" Text="100" Width="20px"></asp:TextBox>%
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 99%; text-align: center;">
                <div style="margin-top: 10px auto">
                    <!--预收费用:<asp:TextBox ID="idYshou" runat="server"></asp:TextBox>-->
                    <asp:Button ID="btn" runat="server" Text="结算" OnClick="btn_Click" />
                </div>
            </div>

        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .apply_item table {
            width: 600px;
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            border-right: 1px solid #000;
        }

            #divAddSample table td {
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                text-align: center;
                height: 25px;
            }
            #divAddSample table {
            width: 500px;
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            border-right: 1px solid #000;
        }
              #divSampleTableList table td {
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                text-align: center;
                height: 25px;
            }

            #divSampleTableList table {
            width: 500px;
            border-left: 1px solid #000;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            border-right: 1px solid #000;
        }

            .apply_item table td {
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                text-align: center;
                height: 25px;
            }

            .apply_item table th {
                width: 157px;
                text-align: center;
                height: 25px;
                vertical-align: middle;
                font-size: 14px;
                font-weight: bold;
                background-color: #f5f5f5;
            }

        .txtRelCost2 {
            width: 50px;
        }

        .txtRelCost {
            width: 50px;
        }

        .txtRelCost2 {
            width: 50px;
            background-color: #eeeeee;
        }

        .thFee {
            width: 157px;
            text-align: center;
            height: 30px;
            vertical-align: middle;
            font-size: 14px;
            font-weight: bold;
            background-color: #f5f5f5;
        }

        #tblFee table {
            width: 500px;
            border-bottom: 1px solid #000;
        }

            #tblFee table td {
                border-left: 1px solid #000;
                height: 30px;
                border-top: 1px solid #000;
            }

        .tdResvContext {
            text-align: left;
            vertical-align: middle;
            width: 130px;
            height: 22px;
        }

        .tdResvContext2 {
            text-align: left;
            vertical-align: middle;
        }

        .tdResvContext span {
            width:90px;
            margin-left: 25px;
        }
        .lblHead {
            color: #000;
            font-size: 13px;
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            var txtSumTemp = 0;
            var txtUse = 0;
            var txtSample = 0;
            var txtEnt = 0;
            var limtRate = 80;
            var showLine=<%=szSampleLine%>;
            $("#<%=btn.ClientID%>").button();
            $("#btnAddSample").button();
            $("#btnAddSampleOK").button();
            txtToLabel("<%=lblUseDevTotalReal.ClientID%>");
            txtToLabel("<%=lblOccupyTotalReal.ClientID%>");
            txtToLabel("<%=lblASSISTotalReal.ClientID%>");
            txtToLabel("<%=lblTIMEOUTTotalReal.ClientID%>");
            txtToLabel("<%=lblSampleTotalReal.ClientID%>");
            txtToLabel("<%=lblRESVDEVTotalReal.ClientID%>");
            txtToLabel("<%=lblENTRUSTTotalReal.ClientID%>");
            txtToLabel("<%=txtSum.ClientID%>");
            $("#divSamleList").dialog({
                autoOpen: false,
                height: 550,
                width: 550,
                title: '调整样本清单',
                modal: true,
                close:closeDig,
                show: {
                    effect: "blind",
                    duration: 1000
                },
                hide: {
                    effect: "blind",
                    duration: 1000
                }
            });
            $("#saveSampleList").button()
            .click(function () {
                $("#divSamleList").dialog("close");
                closeDig();
               
            });
            $("#divAddSample").hide();
            $("#btnAddSample").click(function () {
                $("#divAddSample").show();
            });
            $("#btnAddSampleOK").click(function () {
                var sn = $("#AddSampleID").val();
                var num = $("#addSampleNum").val();
                var SampleName = $("#addSampleName").val();
                var SampleFee = $("#addSamplePrice").html();
                if(sn==null||sn==""||num==null||num=="")
                {
                    return;
                }
                var vTR = "<tr>";
                vTR += "<td style='height:20px' data-num=" + num + " data-name=" + SampleName + " data-name=" + SampleName + "  data-uintFee=" + SampleFee + " data-id=" + sn + ">" + $("#addSampleName").val() + "</td>";
                vTR += "<td style='height:20px'>" + $("#addSamplePrice").html() + "元/份</td>";
                vTR += "<td style='height:20px'><input class='setSampleNum' type='text' style='width:20px' value='" + $("#addSampleNum").val() + "'>份</td>";
                vTR += "<td><a class='delSample' style='width:25px' href='#' title='删除'><img style='width:25px;height:25px;' src='../../../../themes/iconpage/del.png"'></a></td>";
                vTR += "</tr>";
                
                if (AddSample(sn, SampleName, SampleFee, num)) {
                    $(this).val("继续添加");
                    $("#tblSampleAddList").append($(vTR));
                    countSampleFee();
                }
                else {
                    $(this).val("已存在");
                    setTimeout(function () { $("#btnAddSampleOK").val("继续添加"); }, 2000);
                }
            });
            $("#tblSampleAddList").on("click", ".delSample", function () {
                var sampleSN = $(this).parents("tr").children().first().attr("data-id");
                DelSample(sampleSN);
                $(this).parents("tr").remove();
            });
            $("#tblSamleList").on("click","#aSampleFee",function () {
                $("#divSamleList").dialog("open");
            });
            function closeDig() {
                var tblSamleList = $("#tblSamleList");
                var tbl = "";
                tblSamleList.empty();
                var hiddenSampleList = $("#<%=hiddenSampleList.ClientID%>").val();
                var list = hiddenSampleList.split(';');
                var tbltr = "";
                for (i = 0; i < list.length && i <=showLine; i++) {
                    tbltr += "<tr>";
                    if (list[i] != "") {
                        hiddenSampleListTemp = list[i].split(',');
                        tbltr += "<td data-id='" + hiddenSampleListTemp[0] + "'>" + hiddenSampleListTemp[1] + "</td>" + "<td>" + hiddenSampleListTemp[2] + "元/份</td>" + "<td>" + hiddenSampleListTemp[3] + "份</td>";
                    }
                    tbltr += "</tr>";
                }
                tbltr += "<tr><td colspan='4' style='height:20px'>" + "<a id='aSampleFee' href='#'>点击调整全部样本清单</a>" + "</td></tr>";
                var vtbltr = $(tbltr);
                var vUseTotal = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                var vSampleTotal = $("#<%=lblSampleTotalReal.ClientID%>").val();
                var vEntTotal = $("#<%=lblENTRUSTTotalReal.ClientID%>").val();
                vUseTotal = parseFloat(vUseTotal) + parseFloat(vSampleTotal) + parseFloat(vEntTotal);
                vUseTotal   = vUseTotal.toFixed(2);
                txtSumTemp=vUseTotal;
                txtSample =vSampleTotal;
                //$("#<%=txtSum.ClientID%>").val(vUseTotal);
             
                tblSamleList.append(vtbltr);
            }
            $("#tblSampleAddList").on("change", ".setSampleNum", function () {
                var sampleSN = $(this).parents("tr").children().first().attr("data-id");
                var name= $(this).parents("tr").children().first().attr("data-name");
                var uintFee = $(this).parents("tr").children().first().attr("data-uintFee");
                SetSample(sampleSN, name,uintFee,$(this).val());
            });

            function AddSample(sn,name,fee,num)
            {
                var vRes = $("#<%=hiddenSampleList.ClientID%>").val();
                if (vRes.indexOf(sn + ',') > -1) {
                    return false;
                }
                else {
                    $("#<%=hiddenSampleList.ClientID%>").val(vRes + sn + ',' + name + ',' + fee + ',' + num + ";");
                    return true;
                }
                countSampleFee();
            }
            function SetSample(sn,name,fee,num)
            {
                var vRes = "";
                var hiddenSampleList = $("#<%=hiddenSampleList.ClientID%>").val();
                var list = hiddenSampleList.split(';');
                for (i = 0; i < list.length; i++) {
                    if (list[i] != "") {
                        hiddenSampleListTemp = list[i].split(',');
                        if (hiddenSampleListTemp[0] != sn) {
                            vRes += hiddenSampleListTemp[0] + "," + hiddenSampleListTemp[1] + "," + hiddenSampleListTemp[2] + "," + hiddenSampleListTemp[3] + ";";
                        }
                        else {
                            vRes += sn + "," + name + "," + fee + "," + num + ";";
                        }
                    }
                }
                $("#<%=hiddenSampleList.ClientID%>").val(vRes);
                countSampleFee();
            }
            function DelSample(sn)
            {
                var vRes = "";
                var hiddenSampleList = $("#<%=hiddenSampleList.ClientID%>").val();
                var list= hiddenSampleList.split(';');
                for (i = 0; i < list.length; i++) {
                    if (list[i] != "") {
                        hiddenSampleListTemp = list[i].split(',');
                        if (hiddenSampleListTemp[0] != sn) {
                            vRes += hiddenSampleListTemp[0] + "," + hiddenSampleListTemp[1] + "," + hiddenSampleListTemp[2] + "," + hiddenSampleListTemp[3] + ";";
                        }
                    }
                }
                $("#<%=hiddenSampleList.ClientID%>").val(vRes);
                countSampleFee();
            }
            function countSampleFee() {
                var hiddenSampleList = $("#<%=hiddenSampleList.ClientID%>").val();
                var vSumSampelFee=0;
                var list =hiddenSampleList.split(';');
                for (i = 0; i < list.length; i++) {
                    if (list[i] != "") {
                        hiddenSampleListTemp = list[i].split(',');
                        if (hiddenSampleListTemp[1] != null && hiddenSampleListTemp[3] != null) {
                            vSumSampelFee += hiddenSampleListTemp[2] * hiddenSampleListTemp[3];
                        }
                    }
                }
                $("#<%=idTotalFee.ClientID%>").html(vSumSampelFee);
                $("#<%=lblSampleTotalReal.ClientID%>").val(vSumSampelFee);
                countTotalFee();
            }
            $("#<%=txtSum.ClientID%>").change(function () {
                var valNow = (parseInt($("#<%=txtSum.ClientID%>").val()) * 1.0) / parseInt(txtSumTemp) * 100;
                if (valNow < limtRate) {
                    $("#<%=txtRate.ClientID%>").val("最低80%");
                    $("#<%=txtSum.ClientID%>").val(txtSumTemp);
                }
                else {
                    $("#<%=txtRate.ClientID%>").val(valNow);
                    var vUseTotal = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                    var vSampleTotal = $("#<%=lblSampleTotalReal.ClientID%>").val();
                    var vUseTotalNew = (vUseTotal * valNow / 100).toFixed(2);

                    var vSampleTotalNew = (vSampleTotal * valNow / 100).toFixed(2);
                    var vEntTotalNew = parseFloat($(this).val()) - vUseTotalNew - vSampleTotalNew;
                    $("#<%=lblUseDevTotalReal.ClientID%>").val(vUseTotalNew);
                    $("#<%=lblSampleTotalReal.ClientID%>").val(vSampleTotalNew);
                    $("#<%=lblENTRUSTTotalReal.ClientID%>").val(vEntTotalNew);
                }
            });
      
            var kind= $("#<%=kind.ClientID%>").val();
            var devid=$("#<%=devID.ClientID%>").val();
            $("#addSampleName").autocomplete({
                source: "../../data/searchSamleInfo.aspx?kind="+kind+'&devID='+devid,
                       minLength: 0,
                       select: function (event, ui) {
                           if (ui.item) {
                               if (ui.item.id && ui.item.id != "") {
                                   $("#addSampleName").val(ui.item.label);
                                   $("#AddSampleID").val(ui.item.id);
                                   $("#addSamplePrice").html(ui.item.UnitFee / 100 + "." + ui.item.UnitFee%100);
                               }
                           }
                           return false;
                       },
                       response: function (event, ui) {
                           if (ui.content.length == 0) {
                               ui.content.push({ label: " 未找到配置项 " });
                           }
                       }
                   }).blur(function () {
                       if ($(this).val() == "") {
                           $("#AddSampleID").val("");
                       } else {

                       }
                   }).click(function () {
                       $("#addSampleName").autocomplete("search", "");
                   });
            $("#tblFeeList").on("change",".txtRelCost",countTotalFee);
           
            //原本计算费用和打折函数
            function countTotalFee()
            {
                var vUseDevTotal = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                //var vOccupyTotal = $("#<%=lblOccupyTotalReal.ClientID%>").val();
                //var vAssisTotal = $("#<%=lblASSISTotalReal.ClientID%>").val();
                //var vTimeoutTotal = $("#<%=lblTIMEOUTTotalReal.ClientID%>").val();
                var vconTotal = $("#<%=lblCONSUMABLETotalReal.ClientID%>").val();
                var vSampleTotal = $("#<%=lblSampleTotalReal.ClientID%>").val();
                //var vResvDevTotal = $("#<%=lblRESVDEVTotalReal.ClientID%>").val();
                var vEntTotal = $("#<%=lblENTRUSTTotalReal.ClientID%>").val();

                var vuse=parseFloat(vUseDevTotal);
                var vcon=parseFloat(vconTotal);
                var vsample=parseFloat(vSampleTotal);
                var vent=parseFloat(vEntTotal);
                if(vuse<0){
                    vuse=0;
                    $("#<%=lblUseDevTotalReal.ClientID%>").val(0);
                }
                if(vcon<0){
                    vcon=0;
                    $("#<%=lblCONSUMABLETotalReal.ClientID%>").val(0);
                }
                if(vsample<0){
                    vsample=0;
                    $("#<%=lblSampleTotalReal.ClientID%>").val(0);
                }
                if(vent<0){
                    vent=0;
                    $("#<%=lblENTRUSTTotalReal.ClientID%>").val(0);
                }
                var vTotal = vuse+vcon+vsample+vent;
                $("#<%=txtSum.ClientID%>").val(vTotal.toFixed(2));
            }
            function countTotalFeeBackUP()
            {
                
                var vUseTotal = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                var vSampleTotal = $("#<%=lblSampleTotalReal.ClientID%>").val();
                var vEntTotal = $("#<%=lblENTRUSTTotalReal.ClientID%>").val();
                var vconTotal = $("#<%=lblCONSUMABLETotalReal.ClientID%>").val();
                vUseTotal = parseFloat(vUseTotal)+ parseFloat(vSampleTotal) + parseFloat(vEntTotal)+parseFloat(vconTotal);
                $("#<%=txtSum.ClientID%>").val(vUseTotal);
                var valNow = (parseFloat($("#<%=txtSum.ClientID%>").val()) * 1.0) / parseFloat(txtSumTemp) * 100;
                if (valNow < limtRate) {
                    $("#<%=txtRate.ClientID%>").val("最低80%");
                    $("#<%=txtSum.ClientID%>").val(txtSumTemp);
                }
                else {

                    $("#<%=txtRate.ClientID%>").val(valNow);
                }
            }
            $("#<%=txtRate.ClientID%>").change(function () {
                var Total= $("#<%=txtSum.ClientID%>").val();
                var Rate=parseFloat($("#<%=txtRate.ClientID%>").val())/100;
                if(Rate<0.8)
                {
                    alert('不能低于80%');
                    return;
                }
                var vUseDevTotal = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                var vconTotal = $("#<%=lblCONSUMABLETotalReal.ClientID%>").val();
                var vSampleTotal = $("#<%=lblSampleTotalReal.ClientID%>").val();
                var vEntTotal = $("#<%=lblENTRUSTTotalReal.ClientID%>").val();

                var vuse=parseFloat(vUseDevTotal)*Rate;
                var vcon=parseFloat(vconTotal)*Rate;
                var vsample=parseFloat(vSampleTotal)*Rate;
                var countSum=0;
                if(vuse<0){
                    vuse=0;
                    $("#<%=lblUseDevTotalReal.ClientID%>").val(0);
                }
                else{
                    var temp=vuse.toFixed(2);
                    countSum=countSum+parseFloat(temp);
                    $("#<%=lblUseDevTotalReal.ClientID%>").val(temp);
                }
                if(vcon<0){
                    vuse=0;
                    $("#<%=lblCONSUMABLETotalReal.ClientID%>").val(0);
                }
                else
                {
                    var temp=vcon.toFixed(2);
                    countSum=countSum+parseFloat(temp);
                    $("#<%=lblCONSUMABLETotalReal.ClientID%>").val(temp);
                }
                if(vsample<0){
                    vuse=0;
                    $("#<%=lblSampleTotalReal.ClientID%>").val(0);
                }
                else
                {
                    var temp=vsample.toFixed(2);
                    countSum=countSum+parseFloat(temp);
                    $("#<%=lblSampleTotalReal.ClientID%>").val(temp);
                }
                $("#<%=txtSum.ClientID%>").val((Total*Rate).toFixed(2));
                var less=(Total*Rate)-countSum;
                $("#<%=lblENTRUSTTotalReal.ClientID%>").val(less.toFixed(2));

              
            });
            function txtRateChangebacuup()
            {
                if (parseInt($("#<%=txtRate.ClientID%>").val()) < 80) {
                    $("#<%=txtSum.ClientID%>").val("最低80%");
                      $("#<%=txtRate.ClientID%>").val(100);
                  }
                  else {
                      var valNow = txtSumTemp * (parseInt($("#<%=txtRate.ClientID%>").val()) * 0.01);
                      $("#<%=txtSum.ClientID%>").val(valNow.toFixed(2));
                      var vRate = parseFloat($(this).val()) / 100;
                      var vUseTotalNew = parseFloat((txtUse * vRate).toFixed(2));
                      var vSampleTotalNew = parseFloat((txtSample * vRate).toFixed(2));
                      var vEntTotalNew = parseFloat(valNow.toFixed(2)) - vUseTotalNew - vSampleTotalNew;
                      $("#<%=lblUseDevTotalReal.ClientID%>").val(vUseTotalNew);
                    $("#<%=lblSampleTotalReal.ClientID%>").val(vSampleTotalNew);
                      $("#<%=lblENTRUSTTotalReal.ClientID%>").val(vEntTotalNew.toFixed(2));

                  }
            }
            setTimeout(function () {
                txtSumTemp = $("#<%=txtSum.ClientID%>").val();
                txtUse = $("#<%=lblUseDevTotalReal.ClientID%>").val();
                txtSample = $("#<%=lblSampleTotalReal.ClientID%>").val();
                txtEnt = $("#<%=lblENTRUSTTotalReal.ClientID%>").val();
            }, 2000);

        });
    </script>
</asp:Content>
