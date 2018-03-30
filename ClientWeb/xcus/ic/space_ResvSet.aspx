<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="space_ResvSet.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="renderer" content="webkit">
    <meta charset="UTF-8" />
    <title>ԤԼ�о���</title>
    <meta content="" name="keywords" />
    <meta content="" name="description" />
    <link rel="stylesheet" href="style/css/main.css" />
    <link rel="stylesheet" href="style/css/calendar.css" />

    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <script type="text/javascript" language="javascript" src="js/datepicker/WdatePicker.js"></script>
    <Uni:include runat="server" />
    <style>
        .seat { display: none; }
        .hidden { display: none; }
        .HeaderStyle { text-align: center; }
    </style>
    <script type="text/javascript">
        function myload() {
            if (!IsLogin()) {
                $("#logindialog").dialog('open');
            }
        }
        function abc() {
            if (uni) { uni.msgBox("���ݿ����:�ڶ�Ӧ�������ƻ������ļ����У�δ�ҵ���Ŀ��") }
            else { alert("���ݿ����:�ڶ�Ӧ�������ƻ������ļ����У�δ�ҵ���Ŀ��") }
        }
        function delMb(m) {
            var tr = $(m).parents("tr:first");
            var mbId = tr.attr("mbId");
            var gId = tr.attr("gId");
            pro.j.rtest.delMem(gId, mbId, function () {
                tr.remove();
            });
        }
        function checkSub() {
            if ($("[name=startDate]").is(":visible")) {
                var start = $("[name=startDate]").val();
                var end = $("[name=endDate]").val();
                if (start && end) {
                    start = uni.parseDate(start);
                    end = uni.parseDate(end);
                    if ("<%=GetConfig("resvAllDay")%>" == "1") {
                        if (uni.compareDate(start, end) > 0) {
                            alert("��ʼ���ڲ��ô��ڽ�������");
                            return false
                        }
                    }
                    else {
                        if (start && uni.compareDate(start, new Date(), 'm') < 0) {
                            alert("��ʼʱ�䲻��С�ڵ�ǰʱ��");
                            return false;
                        }
                        if (uni.compareDate(start, end, 'm') > 0) {
                            alert("��ʼʱ�䲻�ô��ڽ���ʱ��");
                            return false;
                        }
                        var open_start = parseInt($("#open_start").val().replace(':', ''), 10);
                        var open_end = parseInt($("#open_end").val().replace(':', ''), 10);
                        var st = (start.getHours() * 100) + start.getMinutes();
                        var ed = (end.getHours() * 100) + end.getMinutes();
                        if (open_start > st || open_end < ed) {
                            alert("��ѡʱ������ڿ���ʱ����");
                            return false;
                        }
                    }
                }
                else {
                    alert("ԤԼʱ�������ȷ��д");
                    return false;
                }
            }
            if ($(".memo:visible").attr("IsMust") == "true") {
            var memo = $(".memo:visible").val();
            if (memo != undefined) {
                if (memo == "") {
                    alert("����˵��������д");
                    return false;
                }
                else if (memo.length > 64) {
                    alert("����˵��������64����");
                    return false;
                }
            }
        }
            if ($("#need_file").val() == "true" && $("input[name=up_file]").val() == "") {
                alert("���뱨������ϴ�");
                return false;
            }

            return true;
        }
        $(function () {
            $(".upload_file").uploadFile();
        });
        $(function () {
            if ("<%=ViewState["bIsLongTime"]%>" == "true") {
                $(".islong").show();
                $(".nolong").hide();
                return;
            }
            var hstart = $(".hr_start");
            var hend = $(".hr_end");
            var tmp = $(".tmp_end");
            var open_start = timeInt($("#open_start").val());
            var open_end = timeInt($("#open_end").val());
            var old_start=$("#old_start").val();
            var old_end = $("#old_end").val();
            var latest = parseInt($("#latest").val());
            var earliest = parseInt($("#earliest").val());
            var min = parseInt($("#min").val());
            var max = parseInt($("#max").val());
            var unit = parseInt($("#t_unit").val());
            var fix = {<%=GetConfig("fixTimeSpan")%> };
            var cls = eval("({\"arr\":[" + $("#cls_time").val() + "]})");//�ر�ʱ�� ��������[{start,end}]
            if (cls.arr.length > 0) cls = cls.arr;
            else cls = null;
            if (cls)
                for (var m = 0; m < cls.length; m++) {
                    if (!isNaN(cls[0].start)) break;
                    cls[m].start = timeInt(cls[m].start);
                    cls[m].end = timeInt(cls[m].end);
                }
            if (isEmpty(fix)) fix = null;
            if (fix) {
                hstart.val(absMin(parseInt(hstart.val()), fix));
            }
            hstart.children("option").each(function () {
                var it = parseInt($(this).val());
                if ((fix&&!fix[it]) || checkCls(it, cls)) $(this).remove();
            });
            if (hstart.children("option").length == 0) { alert("�������޿���ʱ�Σ�");window.history.go(-1);}
            hstart.change(function () {
                var val = parseInt($(this).val());
                var edge = findClsEdge(val, cls);//Ѱ������ʱ��ı߽�
                var h = toMinute(val);
                var st = toTime(h + min);
                var en = toTime(h + backMin([max, toMinute(open_end) - h]));
                $("option", tmp).each(function () {
                    var v = parseInt($(this).val());
                    if (v > en || v < st||(fix && fix[val] != v)||v>edge)
                        $(this).removeClass("v");
                    else
                        $(this).addClass("v");
                });
                hend.html($("option.v", tmp).clone());
                var sel = parseInt(tmp.val());
                if (sel > en || sel < st) {
                    var i = st % uni;
                    if (i > 0) sel = st - i + uni;
                    else sel = st;
                    hend.css("border-color", "red");
                }
                else {
                    hend.css("border-color", "");
                }
                tmp.val(sel);
                hend.val(sel);
                if (hend.html() == "")
                    hend.css("border-color", "red");
            });
            hstart.trigger("change");
            if (old_start && old_end) {
                hstart.val(old_start);
                tmp.val(old_end);
                hend.val(old_end)
            }
        })
        function backMin(arr) {
            arr.sort(function (a, b) {
                return a - b;
            });
            return arr[0];
        }
        function backMax(arr) {
            arr.sort(function (a, b) {
                return b - a;
            });
            return arr[0];
        }
        function toMinute(v) {
            return parseInt(v / 100) * 60 + (v % 100);
        }
        function toTime(v) {
            return parseInt( v / 60) * 100 + (v % 60);
        }
        function absMin(v, obj) {
            var diff;
            for (var i in obj) {
                var d = parseInt(i) - v;
                if (diff == undefined) diff = d;
                else if (Math.abs(diff) - Math.abs(d) > 0)
                    diff = d;
            }
            if (diff == null) obj = null;
            return v + (diff || 0);
        }
        function isEmpty(obj){
            for (var name in obj)
            {
                return false;
            }
            return true;
        };
        function checkCls(t, cls) {
            if (cls) {
                for (var i = 0; i < cls.length; i++) {
                    if (t > cls[i].start && t < cls[i].end)
                        return true;
                }
            }
            return false;
        }
        function findClsEdge(t, cls) {
            var ret = 9999;
            if (cls) {
                for (var i = 0; i < cls.length; i++) {
                    var start = cls[i].start
                    if (t <= start && start < ret)
                        ret = start;
                }
            }
            return ret;
        }
        function timeInt(v) {
            if (v.length > 5)
                v = v.substr(v.length - 5);
            var tmp = v.split(":");
            if (tmp.length < 2) return 0;
            return parseInt(tmp[0], 10) * 100 + parseInt(tmp[1], 10);
        }
    </script>
</head>
<body onload="myload()">
    <Uni:dialog ID="Dialog1" runat="server" />
    <form runat="server">
        <div class="body">
            <input type="hidden" id="open_start" runat="server" />
            <input type="hidden" id="open_end" runat="server" />
            <input type="hidden" id="latest" runat="server" />
            <input type="hidden" id="earliest" runat="server" />
            <input type="hidden" id="min" runat="server" />
            <input type="hidden" id="max" runat="server" />
            <input type="hidden" id="t_unit" runat="server" />
            <input type="hidden" id="need_file" runat="server" />
            <input type="hidden" id="old_start" runat="server" />
            <input type="hidden" id="old_end" runat="server" />
            <input type="hidden" id="cls_time" runat="server" />
            <Uni:sidebar runat="server" />

            <div class="content">
                <Uni:nav runat="server" />
                <div class="reservation">
                    <h1>ԤԼ�о���</h1>
                    <table>
                        <tr>
                            <th><p style="font-size: 14px;">ԤԼ����</p></th>
                            <td><asp:Label ID="curObj" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>
                                <p style="font-size: 14px;">ԤԼ���ƣ�</p>
                            </th>
                            <td>
                                <div id="hint" runat="server" style="font-weight:bold;font-size:16px;line-height:20px;"></div>
                                <div id="divUserLimit" runat="server" style="margin-bottom: 10px;">
                                </div>
                            </td>
                        </tr>
                        <tr class="nolong">
                            <th>
                                <p>ѡ�����ڣ�</p>
                            </th>
                            <td>
                                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <p class="islong" style="display:none;">ѡ�����ڣ�</p>
                                <p class="nolong">ѡ��ʱ�䣺</p>
                            </th>
                            <td>
                                <div id="divFreeTime" runat="server">
                                    <asp:DropDownList ID="ddlHourStart" runat="server" CssClass="hour hr_start">
                                    </asp:DropDownList>&nbsp;��&nbsp;
                             <asp:DropDownList ID="ddlHourEnd" runat="server" CssClass="hour hr_end">
                             </asp:DropDownList>
                             <asp:DropDownList ID="tempHourEnd" runat="server" CssClass="hour tmp_end">
                             </asp:DropDownList>
                                </div>
                                <div id="divLimit" runat="server">
                                    <asp:DropDownList ID="ddlPartTime" runat="server" CssClass="hour" Width="200px">
                                    </asp:DropDownList>
                                </div>
                                <script>
                                    var start_date={
                                        dateFmt:'yyyy-MM-dd<%=GetConfig("resvAllDay")=="1"?"":" HH:mm"%>',
                                        minDate: '%y-%M-#{%d+<%=ToUInt(latest.Value)/1440%>}<%=GetConfig("resvAllDay")=="1"?"":" "+open_start.Value%>',
                                        maxDate: '%y-%M-#{%d+<%=ToUInt(earliest.Value)/1440%>}<%=GetConfig("resvAllDay")=="1"?"":" "+open_end.Value%>'
                                    };
                                    var end_date = {
                                        dateFmt: 'yyyy-MM-dd<%=GetConfig("resvAllDay")=="1"?"":" HH:mm"%>',
                                        minDate: '%y-%M-#{%d+<%=ToUInt(latest.Value)/1440%>}<%=GetConfig("resvAllDay")=="1"?"":" "+open_start.Value%>',
                                        maxDate: '%y-%M-#{%d+<%=ToUInt(earliest.Value)/1440%>}<%=GetConfig("resvAllDay")=="1"?"":" "+open_end.Value%>'
                                    };
                                </script>
                                <div id="divLongTime" runat="server">��
                                    <input type="text" class="input_text" onclick="WdatePicker(start_date)" id="startDate" runat="server" />
                                    ��
                           <input type="text" class="input_text" onclick="WdatePicker(end_date)" id="endDate" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <div id="divMemberAdd1" runat="server">
                                    <p>������Աѧ��/���ţ�</p>
                                </div>
                            </th>
                            <td>
                                <div id="divMemberAdd2" runat="server" style="padding-top: 10px;">
                                    <input class="input_txt _LoginName" type="text" runat="server" id="txtPerson" />
                                    <input type="button" class="addMember" value="���" runat="server" id="AddMb" onserverclick="AddMb_ServerClick" />
                                    <input class="input_txt groupid" type="hidden" id='groupIDHidden' value="" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <p style="font-size: 14px;"></p>
                            </th>
                            <td>
                                <asp:Label ID="szMemo" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr id="divUpLoadFile" runat="server">
                            <th>
                                <p>���뱨�棺</p>
                            </th>
                            <td>
                                <div>
                                    <div>
                                        <span>���ϴ����������Թ�����Ա���</span>
                                        					<br>
					<span style="color:#fd0101;">���λͬѧע�⣺���ύ���Ӱ�����뱨��֮���뽫ֽ�ʰ�����뱨���ڸ���Աǩ�ֻ�ѧԺ����֮����IC�ռ������ʦ��</span>
                                    </div>
                                    <div>
                                                                <div>
                            <input type="file" name="ic_file_name" id="ic_file_name" />
                        </div>
                        <div style="height: 24px; line-height: 24px;">
                            <input type="button" style="cursor: pointer;" class="upload_file" file="ic_file_name" value="�ϴ����뱨��" /><span class="cur_file_name" style="color:#0094ff;"></span>
                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <table class="list_tbl" style="width: 400px;">
                            <thead>
                                <tr>
                                    <th>���</th>
                                    <th>����</th>
                                    <th>����</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=memList %>
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <div style="margin-top: 10px;">
                            <div id="divMemo" runat="server">
                                <table>
                                    <tr>
                                        <th>
                                            <div runat="server">
                                                <asp:Label ID="lblszMemo" runat="server" Text="����˵����"></asp:Label>
                                            </div>
                                        </th>
                                        <td>
                                            <div runat="server">
                                                <asp:TextBox ID="txtMemo" runat="server" class="input_txt groupid memo" TextMode="MultiLine" Height="66" Width="280" Text=""></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="submitarea">
                                <input type="button" class="input_submit" value="ȷ��ԤԼ" runat="server" id="Sub" onserverclick="Sub_ServerClick" style="margin-left: 100px;" onclick="if (!checkSub()) return false;"/>
                                <input type="button" class="input_submit" value="����" runat="server" id="btnBack" style="margin-left: 50px; background: url(style/img/back.jpg);" onclick="history.go(-1);" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="copyright">��Ȩ˵��</div>
            </div>
        </div>
    </form>
</body>


</html>

