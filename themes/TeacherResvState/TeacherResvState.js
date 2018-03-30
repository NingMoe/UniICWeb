$.fn.TeachingResvState = function (options) {
    var vClassSec = ["第一节", "第二节", "第三节", "第四节", "第五节", "第六节", "第七节", "第八节", "第九节", "第十节", "第十一节", "第十二节", "第十三节", "第十四节", "第十五节"];
    var vWeekListG = ["第一周", "第二周", "第三周", "第四周", "第五周", "第六周", "第七周", "第八周", "第九周", "第十周", "第十一周", "第十二周", "第十三周", "第十四周", "第十五周", "第十六周","第十七周", "第十八周","第十九周", "第二十周","第二十一周", "第二十二周","第二十三周", "第二十四周","第二十五周","第二十六周","第二十七周","第二十八周","第二十九周","第三十周","第三十一周"];
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
            urlWeek: '',
            devClassKind: 0,
            devid: 0,
            purpose:11
        };
        var opts = $.extend(defaults, options);
        var Week = ['日', '一', '二', '三', '四', '五', '六'];
        var selectDate = defaults.selectDate;
      
        if (selectDate == 0) {
            var myDate = new Date();
            selectDate = myDate.getFullYear() * 10000 + (myDate.getMonth()) * 100 + myDate.getDate();//获取当前日(1-31)
        }
        
        pThis.css("position", "absolute");
        var opentime;
        var resvInfoList;
        var devList;
        var url = defaults.url;
        var urlWeek = defaults.urlWeek;
        var devClassKind = defaults.devClassKind;
        var devid = defaults.devid;
        var purpose = defaults.purpose;
        $.ajax({
            type: "GET",
            dataType: 'json',
            "url": url,
            contentTypeString: "content-type",
            beforeSend: function () {
                pThis.html("加载中...");
            },
            data: { "date": selectDate, "devClassKind": devClassKind, "devID": devid, "purpose": purpose },
            success: function (data, textStatus) {
                data = eval(data);
                resvInfoList = null;
                devList = (data.DevList);
                opentime = 1000000+(data.OpenTime)*100;

                pThis.empty();

                var startTime =parseInt(opentime/10000);
                var endTime = opentime%10000;

                var devOrderList = new Array();//暂时存放设备 列表
                var thHeight = defaults.thHeight;//高度
                
                var thWidth = parseInt(defaults.width / (parseInt(endTime / 100) - parseInt(startTime / 100)));//长度
                if (thWidth > 91)
                {
                    thWidth = 91;
                }
                var strNow = selectDate;
                var dateParse = new Date(strNow / 10000, strNow % 10000 / 100, strNow % 100);
                
                var vDateSelectTempcClick = new Date(strNow / 10000, strNow % 10000 / 100, strNow % 100);
                var vSelectWeek = dateParse.getDay();
                if (vSelectWeek == 0) {
                    vSelectWeek = 7;
                }

                var table = $("<table></table>");
                table.addClass("tblResv");
                table.attr("border", "0");
                table.attr("cellpadding", "0");
                table.attr("cellspacing", "0");
                table.css({ "border-collapse": "collapse", "margin-left": "0px" });
                var tbodyWeek = $("<tbody></body>");
                var tbodyWeektr = $("<tr></tr>");
                var thWeekth = $("<th></th>");
               
                var vSelectWeekTemp = $("<select id='resvTeachselectWeek'></select>");
                vSelectWeekTemp.css("width", "80");
                for (var k = 0; k < vWeekListG.length; k++)
                {
                    vSelectWeekTemp.append($("<option value="+(k+1)+">"+vWeekListG[k]+"</option>"));
                }
                thWeekth.append(vSelectWeekTemp);

                vSelectWeekTemp.change(function () {
                    $.ajax({
                        type: "GET",
                        dataType: 'json',
                        "url": urlWeek,
                        contentTypeString: "content-type",
                        data: { "week": vSelectWeekTemp.val() },
                        success: function (data, textStatus) {
                            data = eval(data);
                            var vDateTemp = data.message;
                            pThis.TeachingResvState({
                                "url": url,
                                "urlWeek": urlWeek,
                                "devClassKind": devClassKind,
                                "selectDate": vDateTemp,
                                "purpose": purpose
                            });
                           
                        }
                        
                    }); 
                });
                setTimeout(function () {
                   
                }, 1000);
                setWeek();
               
                function setWeek()
                {
                  
                    $.ajax({
                        type: "GET",
                        dataType: 'json',
                        "url": urlWeek,
                        data: { "date": selectDate },
                        success: function (data, textStatus) {
                            data = eval(data);
                            //alert(data.message);
                            vSelectWeekTemp.val(data.message);
                        }
                    });
                }
                thWeekth.css("width", thWidth + "px");
                var thWeekthPre = $("<th><a href='#'>上一周</a></th>");
                thWeekthPre.css("width", thWidth + "px");
                tbodyWeektr.append(thWeekth);
             
                thWeekthPre.click(function () {
                    dateClickStr = vDateSelectTempcClick;
                    dateClickStr.setDate(dateClickStr.getDate() - vSelectWeek + 1 - 7);
                    var dateClickStr = dateClickStr.getFullYear() * 10000 + (dateClickStr.getMonth()) * 100 + dateClickStr.getDate();
                    pThis.TeachingResvState({
                        "url": url,
                        "urlWeek": urlWeek,
                        "devClassKind": devClassKind,
                        "selectDate": dateClickStr,
                        "purpose": purpose
                    });
                });

                tbodyWeektr.append(thWeekthPre);
               
                var dateNow = new Date();//new Date();
                var dateNow7 = new Date();
                dateNow7.setDate(dateNow.getDate() + 7);
              
                
                for (var i = 1; i <= 7; i++) {
                    var vDateSelectTemp = new Date(strNow / 10000, strNow % 10000 / 100, strNow % 100);
                    var dateNow = vDateSelectTemp;// new Date();
                    var iTemp = i;
                    if (i == 0)
                    {
                        iTemp = 7;
                    }
                    var vSplitDay = i-vSelectWeek;
                    
                   dateNow.setDate(dateNow.getDate() + vSplitDay);
                    var id = dateNow.getFullYear() * 10000 + (dateNow.getMonth()) * 100 + dateNow.getDate();
                    var vthWeek = $("<th style='font-size:12px' class='resvWeeks' id=" + (id + 0) + ">星期" + Week[dateNow.getDay()] + "</br>" + dateNow.getFullYear() +"-"+ (dateNow.getMonth() + 1) +"-"+ dateNow.getDate() + "</th>");
                    vthWeek.click(function () {
                        var date = parseInt($(this).attr("id"));
                        pThis.TeachingResvState({
                            "url": url,
                            "urlWeek": urlWeek,
                            "devClassKind": devClassKind,
                            "selectDate": date,
                            "purpose": purpose
                        });
                    });
                    if (vSplitDay==0) {
                        vthWeek.addClass("ui-widget-selectResv ");
                    }
                    else {
                        vthWeek.addClass("ui-widget-headerResv ");
                    }
                    vthWeek.css("width", thWidth + "px");
                    tbodyWeektr.append(vthWeek);
                    dateParse.setDate(dateParse.getDate() + 1);
                    dateNow.setDate(dateNow.getDate() + 1);
                }
               /*
                dateNow7.setDate(dateNow.getDate() + 7);
                for (var i = 0; dateNow7 - dateNow > 0; i++) {
                    var id = dateNow.getFullYear() * 10000 + (dateNow.getMonth()) * 100 + dateNow.getDate();
                    var vthWeek = $("<th class='resvWeeks' id=" + (id + 0) + ">星期" + Week[dateNow.getDay()] + "</th>");
                    vthWeek.click(function () {
                        var date = parseInt($(this).attr("id")) + 100;
                        pThis.TeachingResvState({
                            "url": url,
                            "devClassKind": devClassKind,
                            "selectDate": date,
                            "purpose": purpose
                        });
                    });
                    if (id == (selectDate - 100)) {
                        vthWeek.addClass("ui-widget-selectResv ");
                    }
                    else {
                        vthWeek.addClass("ui-widget-headerResv ");
                    }
                    vthWeek.css("width", thWidth + "px");
                    tbodyWeektr.append(vthWeek);
                    dateParse.setDate(dateParse.getDate() + 1);
                    dateNow.setDate(dateNow.getDate() + 1);
                }
                */
                tbodyWeek.append(tbodyWeektr);

                var thWeekthNext = $("<th><a href='#'>下一周</a></th>");
                thWeekthNext.click(function () {
                    dateClickStr = vDateSelectTempcClick;
                    dateClickStr.setDate(dateClickStr.getDate() - vSelectWeek +1+ 7);
                    var dateClickStr = dateClickStr.getFullYear() * 10000 + (dateClickStr.getMonth()) * 100 + dateClickStr.getDate();
                    pThis.TeachingResvState({
                        "url": url,
                        "urlWeek": urlWeek,
                        "devClassKind": devClassKind,
                        "selectDate": dateClickStr,
                        "purpose": purpose
                    });
                });
                thWeekthNext.css("width", thWidth + "px");
                tbodyWeektr.append(thWeekthNext);

                for (var DevI = 0; devList!=null&&DevI < devList.length; DevI++) {
                    var vDevTemp = new Object();
                    
                    var vResvInfo = devList[DevI].resvInfo;
                    vDevTemp.order = (DevI + 1);
                    vDevTemp.devID = devList[DevI].devID;
                    devOrderList[DevI] = vDevTemp;
                    if (vResvInfo != null && vResvInfo.length > 0)
                    {
                     
                        for (var i = 0; vResvInfo != null && i < vResvInfo.length; i++) {
                            var temp = vResvInfo[i];
                            var vdiv = $("<div></div>");
                            var vResvStatus = vResvInfo[i].status;
                            vdiv.css("position", "absolute");
                            /*
                            if ((vResvStatus & 1) > 0) {
                                vdiv.addClass("undo");
                            } else if ((vResvStatus & 2) > 0) {
                                vdiv.addClass("doing");
                            } else if ((vResvStatus & 4) > 0) {
                                vdiv.addClass("done");
                            }*///暂时不区分颜色
                            vdiv.addClass("done");
                            vdiv.addClass("opResvSet");//修改预约
                           // vdiv.addClass("undo");
                            var devIDTemp = temp.devID;
                            var heightCount = (DevI+1);//距离顶部的个数
                            /*
                            for (var j = 0; j < devOrderList.length; j++) {
                                if (devOrderList[j].devID == devIDTemp) {
                                    heightCount = devOrderList[j].order;
                                    break;
                                }
                            }
                            */
                            var tempValueName = temp.szResvName;
                            var vResvTimeSec = (parseInt((temp.value % 10000) / 100)) * 100 * 10000 + (parseInt((temp.value % 10000) % 100)) * 100;//节次转换为自由时间方便计算
                            
                            var vcolum = (parseInt(vResvTimeSec / 1000000) - parseInt(opentime / 1000000)) + 1;//距离左边个数
                            var top = heightCount * thHeight + heightCount + 1;
                            vdiv.css("top", top + "px");
                            var vStart = parseInt(vResvTimeSec / 10000);
                            var vEnd = vResvTimeSec % 10000;

                            var vTetmpLeftStart = parseInt(vStart / 100) + parseInt(vStart) % 100 / 60;
                            var vTetmpLeftEnd = parseInt(vEnd / 100) + parseInt(vEnd) % 100 / 60;
                            var vdivWidth = (vTetmpLeftEnd - vTetmpLeftStart+1) * thWidth;
                            var vcolumSqur = parseInt(vResvTimeSec % 10000 / 100) - parseInt(vResvTimeSec / 1000000) - 1;//预约时间占有的个数
                            vdiv.css("width", vdivWidth + vcolumSqur + "px");
                            vdiv.css("height", thHeight + "px");
                            vdiv.text(temp.szResvName);
                            var vTetmpLeft = parseInt(vResvTimeSec / 1000000) + parseInt(vResvTimeSec / 10000) % 100 / 60;
                            var vOpenLeft = parseInt(opentime / 1000000) + parseInt(opentime / 10000) % 100 / 60;
                            var left = parseInt((vTetmpLeft - vOpenLeft + +parseInt(opentime / 10000) % 100 / 60) * thWidth) + thWidth + vcolum+1;
                            vdiv.css("left", left + "px");

                            pThis.append(vdiv);
                        }
                    }
                }
                for (var DevI = 0; DevI < devList.length; DevI++) {
                    var devName = devList[DevI].devName;
                    var devID = devList[DevI].devID;
                    var tbodyWeekTimetr = $("<tr ></tr>");
                    tbodyWeekTimetr.append($("<th devid=" + devID + ">" + devName + "</th>").css("width", thWidth + "px"));
                    for (var openI = startTime; openI <= endTime; openI = openI + 100) {
                      
                        var vSec=parseInt(openI/100)-1;
                        if (openI == startTime) {
                            var hour = parseInt(openI / 100);
                            var vMin = startTime % 100;
                            var lable;
                            if (vMin < 10) {
                                lable = $("<td><a class='aNewResv' data-devid='" + devID + "' data-sec='" + vSec + "' data-resvDate='" + selectDate + "' title=\"\">" + vClassSec[vSec] + "</a></td>");
                            }
                            else {
                                lable = $("<td><a class='aNewResv' data-devid='" + devID + "' data-sec='" + vSec + "' data-resvDate='" + selectDate + "' title=\"\">" + vClassSec[vSec] + " \"</a></td>");
                            }
                          
                            lable.css("width", thWidth + "px");
                            tbodyWeekTimetr.append(lable);
                        }
                        else {
                            var hour = parseInt(openI / 100);
                            var lable = $("<td><a class='aNewResv' data-devid='" + devID + "' data-sec='" + vSec + "' data-resvDate='" + selectDate + "' title=\"\">" + vClassSec[vSec] + "</a></td>");
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
