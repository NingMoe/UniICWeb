$.fn.ResvState = function (options) {

    var pThis = $(this);
    $(this).each(function () {
        var pThis = $(this);
        var defaults = {
            ShowCheck: false,
            HeaderOrder: true,
            OpenModule: 1,//1绝对时间 2节次
            selectDate: 0,
            OpenTime: 0,
            OpenSec: 113,
            thHeight: 35,
            width: 850,
            url: '',
            devClassKind: 0,
            devid: 0,
            purpose: 11,
            startLine: 1,
            needLine: 10
        };
        var opts = $.extend(defaults, options);
        var Week = ['日', '一', '二', '三', '四', '五', '六'];
        var selectDate = defaults.selectDate;
        if (selectDate == 0) {
            var myDate = new Date();
            selectDate = myDate.getFullYear() * 10000 + (myDate.getMonth() + 1) * 100 + myDate.getDate() + 0;//获取当前日(1-31)
        }
        pThis.css("position", "absolute");
        var opentime;
        var resvInfoList;
        var devList;
        var startLine = defaults.startLine;
        var needLine = defaults.needLine;
        var url = defaults.url;
        var devClassKind = defaults.devClassKind;
        var devid = defaults.devid;
        var purpose = defaults.purpose;

        $.ajax({
            type: "GET",
            dataType: 'json',
            "url": url,
            contentTypeString: "application/x-www-form-urlencoded",
            beforeSend: function () {
                pThis.html("加载中...");
            },
            data: { "date": selectDate, "devClassKind": devClassKind, "devID": devid, "purpose": purpose, "needLine": needLine, "startLine": startLine },
            success: function (data, textStatus) {
                debugger;
                data = eval(data);
                resvInfoList = (data.resvInfo);
                devList = (data.DevList);

                opentime = (data.OpenTime);
                startLineRes = data.startLine;//后台返回的startLine
                totalLineRes = data.totalLine;//后台返回的totalLine
                //debugger;
                $("#startLine").val(startLineRes);
                $("#totalLine").val(totalLineRes);

                startLineTemp = $("#startLine").val();
                if (startLineTemp != null && startLineTemp != "") {
                    $("#startLine").val(parseInt(startLineTemp));
                    startLine = startLine + needLine;
                }

                pThis.empty();
                var startTime = parseInt(opentime / 10000);
                var endTime = parseInt(opentime % 10000);

                var devOrderList = new Array();//暂时存放设备 列表
                var thHeight = defaults.thHeight;//高度
                var thWidth = parseInt(defaults.width / (parseInt(endTime / 100) - parseInt(startTime / 100)));//长度

                var table = $("<table></table>");
                table.addClass("tblResv");
                table.attr("border", "0");
                table.attr("cellpadding", "0");
                table.attr("cellspacing", "0");
                table.css({ "border-collapse": "collapse", "margin-left": "0px" });
                var tbodyWeek = $("<tbody></body>");
                var tbodyWeektr = $("<tr></tr>");
                var thWeekth = $("<th><input type='hidden' name='startLine' id='startLine' value='1'><input type='hidden' name='totalLine' id='totalLine' value='0'></th>");
                thWeekth.css("width", thWidth + "px");
                tbodyWeektr.append(thWeekth);
                var strNow = selectDate;
                var dateParse = new Date(strNow / 10000, strNow % 10000 / 100, strNow % 100);

                var dateNow = new Date();
                var dateNow7 = new Date();
                dateNow7.setDate(dateNow.getDate() + 7);
                for (var i = 0; dateNow7 - dateNow > 0; i++) {
                    var id = dateNow.getFullYear() * 10000 + (dateNow.getMonth()) * 100 + dateNow.getDate();
                    var vthWeek = $("<th class='resvWeeks' id=" + (id + 0) + ">星期" + Week[dateNow.getDay()] + "</th>");
                    vthWeek.click(function () {
                        var date = parseInt($(this).attr("id")) + 100;
                        pThis.ResvState({
                            "url": url,
                            "devClassKind": devClassKind,
                            "selectDate": date,
                            "purpose": purpose,
                            "needLine": needLine,
                            "startLine": $("#startLine").val
                        });
                    });

                    if (id == (selectDate - 100)) {
                        vthWeek.addClass("ui-widget-selectResv ");
                    }
                    else if (id == selectDate)
                    { vthWeek.addClass("ui-widget-selectResv "); }
                    else {
                        vthWeek.addClass("ui-widget-headerResv ");
                    }
                    vthWeek.css("width", thWidth + "px");
                    tbodyWeektr.append(vthWeek);
                    dateParse.setDate(dateParse.getDate() + 1);
                    dateNow.setDate(dateNow.getDate() + 1);
                }

                var thPre = $("<th></th>");
                var btnPre = $("<input type='button' id='btnPre' value='上一页'>");
                btnPre.button();
                btnPre.click(function () {
                    pThis.ResvState({
                        "url": url,
                        "devClassKind": devClassKind,
                        "selectDate": selectDate,
                        "purpose": purpose,
                        "needLine": needLine,
                        "startLine": parseInt($("#startLine").val()) - needLine
                    });
                });
                thPre.append(btnPre);
                //debugger;
                var thNext = $("<th></th>");
                var btnNext = $("<input type='button' id='btnNext' value='下一页'>");
                if (data!=null&&data.DevList.length < needLine) {
                    btnNext.attr("disabled", "disabled");
                }
                btnNext.button();

                btnNext.click(function () {
                    pThis.ResvState({
                        "url": url,
                        "devClassKind": devClassKind,
                        "selectDate": selectDate,
                        "purpose": purpose,
                        "needLine": needLine,
                        "startLine": parseInt($("#startLine").val()) + needLine
                    });
                });
                thNext.append(btnNext);
                tbodyWeektr.append($("<th></th>"));
                tbodyWeektr.append($("<th></th>"));
                tbodyWeektr.append($("<th></th>"));
                tbodyWeektr.append(thPre);
                tbodyWeektr.append(thNext);

                tbodyWeek.append(tbodyWeektr);

                for (var DevI = 0; devList != null && DevI < devList.length; DevI++) {
                    var vDevTemp = new Object();
                    vDevTemp.order = (DevI + 1);
                    vDevTemp.devID = devList[DevI].devID;
                    devOrderList[DevI] = vDevTemp;
                }


                for (var i = 0; resvInfoList != null && i < resvInfoList.length; i++) {
                    var temp = resvInfoList[i];
                    var vdiv = $("<div></div>");
                    vdiv.css("position", "absolute");
                    /*
                    if ((temp.status & 2) > 0) {
                        vdiv.addClass("undo");
                    }
                    else if ((temp.status & 4) > 0) {
                        vdiv.addClass("doing");
                    }
                    else if ((temp.status & 8) > 0) {
                        vdiv.addClass("done");
                    }*/
                    //todo
                    vdiv.addClass("undo");

                    var devIDTemp = temp.devID;
                    var heightCount = 0;//距离顶部的个数
                    for (var j = 0; j < devOrderList.length; j++) {
                        if (devOrderList[j].devID == devIDTemp) {
                            heightCount = devOrderList[j].order;
                            break;
                        }
                    }
                    var vcolum = (parseInt(temp.value / 1000000) - parseInt(opentime / 1000000)) + 1;//距离左边个数
                    var top = heightCount * thHeight + heightCount + 1;
                    vdiv.css("top", top + "px");
                    var vStart = parseInt(temp.value / 10000);
                    var vEnd = temp.value % 10000;
                    var vTetmpLeftStart = parseInt(vStart / 100) + parseInt(vStart) % 100 / 60;
                    var vTetmpLeftEnd = parseInt(vEnd / 100) + parseInt(vEnd) % 100 / 60;
                    var vdivWidth = (vTetmpLeftEnd - vTetmpLeftStart) * thWidth;
                    var vcolumSqur = parseInt(temp.value % 10000 / 100) - parseInt(temp.value / 1000000) - 1;//预约时间占有的个数
                    vdiv.css("width", vdivWidth + vcolumSqur + "px");
                    vdiv.css("height", thHeight + "px");
                    vdiv.text(temp.name);
                    var vTetmpLeft = parseInt(temp.value / 1000000) + parseInt(temp.value / 10000) % 100 / 60;
                    var vOpenLeft = parseInt(opentime / 1000000) + parseInt(opentime / 10000) % 100 / 60;
                    var left = parseInt((vTetmpLeft - vOpenLeft + +parseInt(opentime / 10000) % 100 / 60) * thWidth) + thWidth + vcolum + 1;
                    vdiv.css("left", left + "px");

                    pThis.append(vdiv);
                }


                for (var DevI = 0; DevI < devList.length; DevI++) {
                    var devName = devList[DevI].devName;
                    var devID = devList[DevI].devID;
                    var tbodyWeekTimetr = $("<tr ></tr>");
                    tbodyWeekTimetr.append($("<th devid=" + devID + ">" + devName + "</th>").css("width", thWidth + "px"));
                    for (var openI = startTime; openI <= endTime; openI = openI + 100) {
                        if (openI == startTime) {
                            var hour = parseInt(openI / 100);
                            var vMin = startTime % 100;
                            var lable;
                            if (vMin < 10) {
                                lable = $("<td><a title=" + hour + ":" + (vMin) + "0到" + (hour + 1) + ":00>" + hour + ":" + (vMin) + "0</a></td>");
                            }
                            else {
                                lable = $("<td><a title=" + hour + ":" + (vMin) + "到" + (hour + 1) + ":00>" + hour + ":" + (vMin) + "</a></td>");
                            }

                            lable.css("width", thWidth + "px");
                            tbodyWeekTimetr.append(lable);
                        }
                        else {
                            var hour = parseInt(openI / 100);
                            var lable = $("<td><a title=" + hour + ":00到" + (hour + 1) + ":00>" + hour + ":00</a></td>");
                            lable.css("width", thWidth + "px");
                            tbodyWeekTimetr.append(lable);
                        }

                    }
                    tbodyWeek.append(tbodyWeekTimetr);
                }
                table.append(tbodyWeek);
                pThis.append(table);

            }
        });
    });
};
