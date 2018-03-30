// MainJScript 文件
var tab_data = "";
function OnBeforeLoad(event, ui) {
    ui.ajaxSettings.type = 'POST';
    ui.ajaxSettings.hasContent = true;
    ui.ajaxSettings.dataTypes[0] = "text";
    ui.jqXHR.setRequestHeader("Content-Type", ui.ajaxSettings.contentType);
    ui.ajaxSettings.data = tab_data;
    tab_data = "";
    return true;
}
var g_tab = null;
$(function () {
    $(".btnClss").button();
});
function TabReload(fdata) {
    tab_data = fdata;
    if (!g_tab) return;
    var i = g_tab.tabs('option', 'active');
    ShowWait();
    g_tab.tabs("load", i);
}
function TabJumpReload(fdata,i) {
    tab_data = fdata;
    if (!g_tab) return;     
    ShowWait();
    g_tab.tabs("option","active", i);
}
function TabInJumpReload(aID,fata) {
    $("#" + aID).data("fparam", fata);  
    $("#" + aID).click();
}
function OnTabLoad(event, ui) {
    HideWait();

    InitSummary(event, ui);
    InitFixBtn(event, ui);
    InitAdvOpts(event, ui);
    $("form", ui.panel).submit(function () {    
        if (!$(this).validationEngine("validate")) {
            return false;
        }
        if (TabReload) {
            TabReload($(this).serialize());
            return false;
        }
        return false;
    });

    var alltbline = $("#ListTbl tr", ui.panel);
    if (alltbline.length == 0) alltbline = $(".ListTbl tbody tr", ui.panel);
    alltbline.hover(function () {
        alltbline.css("background", "");
        $(this).css("background", "#ddeeff");
    }, function () {
        alltbline.css("background", "");
    });
}

function OnTabActivate(event, ui)
{
    if(ui.oldPanel)
    {
        ui.oldPanel.empty();
    }
}

$(function () {
    g_tab = $("#tabs");
    g_tab.tabs({
        beforeLoad: OnBeforeLoad,
        load: OnTabLoad,
        activate: OnTabActivate
    }).addClass("ui-tabs-vertical ui-helper-clearfix");
    $("li", g_tab).removeClass("ui-corner-top").addClass("ui-corner-left");
    
    $("#btnLogout").button();
});
//==============================================

//点击综合信息图表事件
function OnClickPie(grp, type) {
    if (grp == "PieCheck") {
        g_tab.tabs("option", "active", 3);
    } else {
        //    alert(grp + "," + type);
    }
    return false;
}
function OnClickChart(e) {
    var p = $(this.graphic.element).parents(".PieStat").attr("id");
    return OnClickPie(p, this.name.split("：")[0]);
}
Highcharts.setOptions({ plotOptions: { pie: { cursor: 'pointer', point: { events: { click: OnClickChart, legendItemClick: OnClickChart } } } } });
var gcolors = [
    ['#058DC7'/*好*/, '#50B432'/*好*/, '#FF9655'/*坏*/, '#ED561B'/*坏*/],
    ['#058DC7'/*好*/, '#50B432'/*好*/, '#50B492'/*好*/, '#ED561B'/*坏*/],
    ['#058DC7'/*好*/, '#ED561B'/*坏*/],
    ['#ED561B'/*坏*/, '#058DC7'/*好*/]
];

var bgColor = "rgb(252, 253, 253)";//transparent

//初始化综合信息图表
function InitSummary(event, ui) {
    //卡片上的饼图
    var cstat = $(".PieStat", ui.panel);
    if (cstat.length > 0) cstat.each(function () {
        var pThis = $(this);
        var cid = parseInt(pThis.data("color"));
        var piedata = [];
        $("p", pThis).each(function () {
            var ndata = { name: $(this).text(), y: $(this).data("value") };
            piedata.push(ndata);
        });

        var chart = new Highcharts.Chart({
            chart: {
                renderTo: pThis.get(0),
                type: 'pie',
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            colors: gcolors[cid],
            title: {
                text: ''
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                    }
                }
            },
            series: [{
                type: 'pie',
                name: '比例',
                data:piedata
            }]
        });
    });


    //卡片上的柱状图
    var cstat = $(".BarStat", ui.panel);
    if (cstat.length > 0) cstat.each(function () {
        var pThis = $(this);
        var cid = parseInt(pThis.data("color"));
        var categories = [];
        var barSeries = [];

        var objTemp = $("p span", pThis);
        for (var i = 0; i < objTemp.length; i++) {
            categories.push($(objTemp[i]).text());
        }
        objTemp = $("h1 strong", pThis);
        for (var i = objTemp.length - 1; i >= 0; i--) {
            var sdata = [];
            var objD = $("p", pThis).find("strong:eq(" + i + ")");
            for (var k = 0; k < objD.length; k++) {
                sdata.push(parseInt($(objD[k]).text()));
            }
            barSeries.push({ name: $(objTemp[i]).text(), data: sdata });
        }

        var chart = new Highcharts.Chart({
            chart: {
                renderTo: pThis.get(0),
                type: 'bar',
                backgroundColor: bgColor
            },
            colors: gcolors[cid],
            title: null,
            xAxis: {
                categories: categories
            },
            yAxis: {
                min: 0,
                title: null
            },
            legend: {
                backgroundColor: '#FFFFFF',
                reversed: true
            },
            tooltip: {
                formatter: function () {
                    return '' +
                        this.series.name + ': ' + this.y + '';
                }
            },
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            },
            series: barSeries
        });
    });
    //卡片上的柱状图X
    var cstat = $(".ColumnStat", ui.panel);
    if (cstat.length > 0) cstat.each(function () {
        var pThis = $(this);
        var cid = parseInt(pThis.data("color"));
        var categories = [];
        var barSeries = [];

        var objTemp = $("p span", pThis);
        for (var i = 0; i < objTemp.length; i++) {
            categories.push($(objTemp[i]).text());
        }
        objTemp = $("h1 strong", pThis);
        for (var i = objTemp.length - 1; i >= 0; i--) {
            var sdata = [];
            var objD = $("p", pThis).find("strong:eq(" + i + ")");
            for (var k = 0; k < objD.length; k++) {
                sdata.push(parseInt($(objD[k]).text()));
            }
            barSeries.push({ name: $(objTemp[i]).text(), data: sdata });
        }
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: pThis.get(0),
                type: 'column',
                backgroundColor: bgColor
            },
            colors: gcolors[cid],
            title: null,
            xAxis: {
                categories: categories
            },
            yAxis: {
                min: 0,
                title: null
            },
            legend: {
                backgroundColor: '#FFFFFF',
                reversed: true
            },
            tooltip: {
                formatter: function () {
                    return '' +
                        this.series.name + ': ' + this.y + '';
                }
            },
            plotOptions: {
                column:{
                    pointPadding:0.2,
                    borderWidth:0
                }
            },
            series: barSeries
        });
    });

    //卡片上的线图
    var cstat = $(".LineStat", ui.panel);
    if (cstat.length > 0) cstat.each(function () {
        var pThis = $(this);
        var cid = parseInt(pThis.data("color"));
        var name = pThis.data("name");
        var unit = pThis.data("unit");
        var categories = [];
        var ldata = [];

        var objP = $("p", pThis);
        var objTemp = objP.find("span:eq(0)");
        for (var i = 0; i < objTemp.length; i++) {
            categories.push($(objTemp[i]).text());
        }
        objTemp = objP.find("span:eq(1)");
        for (var i = 0; i < objTemp.length; i++) {
            ldata.push(parseInt($(objTemp[i]).text()));
        }

        var chart = new Highcharts.Chart({
            chart: {
                renderTo: pThis.get(0),
                type: 'spline',
                backgroundColor: bgColor
            },
            colors: gcolors[cid],
            title: null,
            xAxis: {
                categories: categories
            },
            yAxis: {
                min:0,
                title: null,
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                    this.x + ': ' + this.y + unit;
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
            series: [{
                name: name,
                data: ldata
            }]
        });
    });

    //卡片上的区域图
    var cstat = $(".AreaStat", ui.panel);
    if (cstat.length > 0) cstat.each(function () {
        var pThis = $(this);
        var cid = parseInt(pThis.data("color"));
        var name = pThis.data("name");
        var unit = pThis.data("unit");
        var categories = [];
        var ldata = [];

        var objP = $("p", pThis);
        var objTemp = objP.find("span:eq(0)");
        for (var i = 0; i < objTemp.length; i++) {
            categories.push($(objTemp[i]).text());
        }
        objTemp = objP.find("span:eq(1)");
        for (var i = 0; i < objTemp.length; i++) {
            ldata.push(parseInt($(objTemp[i]).text()));
        }

        var chart = new Highcharts.Chart({
            chart: {
                renderTo: pThis.get(0),
                type: 'areaspline',
                backgroundColor: bgColor
            },
            colors: gcolors[cid],
            title: null,
            xAxis: {
                categories: categories
            },
            yAxis: {
                min: 0,
                title: null,
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                    this.x + ': ' + this.y + unit;
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
            plotOptions: {
                areaspline: {
                    fillOpacity: 0.5
                }
            },
            series: [{
                name: name,
                data: ldata
            }]
        });
    });
}

//初始化
function InitFixBtn(event, ui) {
    $(".FixBtn a", ui.panel).button();
}

//初始化高级选项
function InitAdvOpts(event, ui) {
    var oAdvOpts = $(".AdvOpts", ui.panel);
    if (oAdvOpts.length > 0) oAdvOpts.each(function () {
        var pThis = $(this);

        pThis.UIAPanel({
            theme: "box2.png", borderWidth: 20, minWidth: "80", maxWidth: "300", minHeight: "22", maxHeight: "auto", speed: 300,
            OnClose: function (userChanged) {
                if (TabReload && userChanged) {
                    TabReload(pThis.parents("form").serialize());
                }
            }
        });
    });
}
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
