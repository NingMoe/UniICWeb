<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="Reserve.aspx.cs" Inherits="ClientWeb_xcus_jx_Reserve" %>

<%@ MasterType VirtualPath="net/Master.master" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/linkage/linkage.css" rel='stylesheet' />
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        .ui-dialog { overflow: visible; }
        #calendar h3.week { margin-top: 0px; margin-bottom: 10px; padding: 4px 0px; border-bottom: 1px solid #ccc; }
        #info_panel .panel-body { padding-top: 10px; padding-bottom: 2px; }
        #rsv_panel { min-height: 700px; }
        #rsv_panel .panel-body { padding: 2px; }
        #rsv_list .title { font-weight: bold; font-family: 'Microsoft YaHei'; font-size: 14px; border-bottom: 1px dashed #ccc; padding-bottom: 2px; margin-bottom: 2px; }
        #rsv_list .time { font-size: 12px; line-height: 21px; padding: 1px 2px; }
        #rsv_list .time:nth-child(2n+1) { background: #efefef; }
        #rsv_list .act { margin-top: 2px; font-size: 12px; line-height: 22px; padding: 2px; }
        #rsv_list .alert { margin-bottom: 2px; padding: 5px 10px; }
        input.md_ipt { width: 60px; height: 24px; }
        /*.ui-widget-overlay { height: 130%; }*/
        #info_panel .form-group select { width: 200px; }
        #dlg_resv_ret { background: #fafafa; }
        #dlg_resv_ret table { width: 100%; min-height: 460px; }
        #dlg_resv_ret table tbody td { background: #fff; padding: 2px 1px; vertical-align: top; border: solid 1px #eee; }
        #dlg_resv_ret .alert { padding: 2px 5px; margin-bottom: 2px; }
        #dlg_resv_ret .alert .rsv { font-weight: bold; padding: 3px 2px; border-bottom: 1px dashed #ccc; }
        /*[aria-describedby=dlg_resv_ret] .ui-dialog-titlebar-close { display: none; }*/

        .cld-list-qzs { background: #fff; }
        .filter_table td { vertical-align: middle !important; }

        #dlg_resv_ret h4 { font-family: 'Microsoft YaHei'; }

        .cld-obj-title { cursor: pointer; }

        .step_title { font-size: 22px; padding-right: 8px; }
        .info_unitab .tab_head li .caret { color: #fbfbfb; }
        .info_unitab .tab_head li { background-color: #fbfbfb; }
    </style>
    <script>
        //字段解释 tch 标准一节课的数字周次(1+)ww周次(0+)d节次(两位0+)sec
        //ltch 一次上课数字 房间id+tch+diff(前后节次差)
        //test 一次上课标识 ltch-diff
        
        var reserve_cld;
        var chi = ['零','一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二', '十三', '十四', '十五', '十六'];
        var wk=['一', '二', '三', '四', '五', '六','日']
        var objs = {};//房间对象集合
        var tchs = [];
        var tests = [];//上课标识集合
        var testDic = [];//testDic 保存key为test的test对象，保存额外信息
        function removeTest(ltch) {
            reserve_cld.stateSet.removeTest(ltch);
        }
        function addTest(ltch) {
            reserve_cld.stateSet.addTest(ltch);
        }
        function refRsv() {
            $("#rsv_list").html("");
            tests.sort();//排序
            for (var t = 0; t < tests.length; t++) {
                var test = testDic[tests[t]+""];
                //for (var i = 0; i < objs.length; i++) {
                var obj=objs[test.oid];
                //if (objs[i].id == test.oid) {
                if(obj){
                    if(!test.mbNum) test.mbNum=$(".mb_num").val();
                    if(!test.group) test.group=$(".group_id").val();
                    if(!test.name) test.name=obj.roomName;
                    if(!test.devNum) test.devNum=obj.devNum;
                    var title="";//时间列表
                    for (var j = t; j < tests.length; j++) {
                        var tmp=testDic[tests[j]+''];
                        if(j==t||(test.oid==tmp.oid&&(!tmp.group||test.group==tmp.group))){
                            if(!tmp.group){//空组 初始化
                                tmp.mbNum=test.mbNum;
                                tmp.group=test.group;
                                tmp.name=test.name;
                                tmp.devNum=test.devNum;
                            }
                            var ltch=tmp.id*100+tmp.diff;
                            title += "<div class='time'  id='resv_item_"+tmp.id+"' key='"+ltch+"'> 第<code>" + tmp.ww + "</code>周 >>星期<code>" + wk[tmp.d] + "</code> >>";
                            if(tmp.time){
                                var t=tmp.time.substr(8);
                                title+="<code>"+t.substr(0,2)+":"+t.substr(2,2)+"-"+t.substr(4,2)+":"+t.substr(6,2)+"</code>";
                            }
                            else if(tmp.sec<50){//50后为绝对时间
                                title+="第<code>" + chi[tmp.sec] + "</code>节";
                                if (tmp.diff > 0)
                                    title += "--第<code>" + chi[tmp.sec + tmp.diff] + "</code>节";
                            }
                            title+="<a class='close' onclick='removeTest(" + ltch + ");'>&times;</a></div>";
                        }
                        else
                            break;
                    }
                    t=j-1;
                    var md = "<div class='act form-inline'>参与实验:<code>"+test.mbNum+"</code> 人"//
                        +"<div class='pull-right'><button class='btn btn-info btn-xs btn_batch' onclick='batch("+(test.oid*10000000)+")'>批量排课</button></div></div>";//<button class='btn btn-info btn-xs' onclick='splitGroup(\"" +tests[t]+"\")'>更改实验学生</button>
                        //+"<button class='btn btn-info btn-xs sel_rm_group' onclick='rmGroup(\"" +test.id+"\",\"" +obj.roomName+"\",\"" +obj.devNum+"\")'>房间组合</button>
                    var item=$("<div class='alert alert-info'><div class='title'>预约: <span class='obj_name'>" + test.name + "<span>(设备数:<code class='dev_num'>" +test.devNum + "</code>)<a class='close remove_list'>&times;</a></div>" + title  + md + "</div>");
                        <%if (GetConfig("clientTab") == "gs")
                          {%>//浙江工商
                    if(parseInt(test.mbNum)>parseInt(test.devNum)){
                        uni.confirm("房间设备不足，建议预约房间组合",function(){
                            $(".sel_rm_group",item).click();
                        });
                    }
                        <%}%>
                    $("#rsv_list").append(item);
                    $(".remove_list",item).click(function(){//批量删除
                        //var arr=[];
                        //$(".time",$(this).parents(".alert:first")).each(function(){
                        //    var v=parseInt($(this).attr("key"));
                        //    arr.push(v);
                        //});
                        //removeTest(arr);
                        reserve_cld.stateSet.reset();
                    });
                    //break;
                }
                //}
            }
        }

        //选择房间组合
        function rmGroup(key,rmName,devNum){
            var test = testDic[key];
            var tme=(new Date()).getTime();
            pro.j.rm.getRmGroup(test.oid,"",function(rlt){
                var list=rlt.data;
                var panel=$('<div class="rm_group_panel"><div><input type="radio" name="radio_rm_group" id="iradio_'+tme+'" num="'+devNum+'" gname="'+rmName+'" value="'+test.oid+'"/><label class="click" for="iradio_'+tme+'">'+rmName+'(<span class="red">'+devNum+'</span>)</label>&nbsp;&nbsp;&nbsp;</div><div class="line"/></div>');
                if(list.length==0)panel.append("<span class='grey'>没有可用的房间组合</span>");
                else
                    for (var i = 0; i < list.length; i++) {
                        var mb=list[i];
                        var id=("radio_"+i)+tme;
                        var str=$('<div style="float:left;"><input type="radio" name="radio_rm_group" id="'+id+'" num="'+mb.devNum+'" gname="'+mb.name+'" value="'+mb.ids+'"/><label for="'+id+'" class="click">'+mb.name+'(<span class="red">'+mb.devNum+'</span>)</label>&nbsp;&nbsp;&nbsp;</div>');
                        panel.append(str);
                    }
                panel.find("input[type=radio]").each(function () {
                    if($(this).val()==test.ids)$(this).attr("checked",true);
                });
                uni.dlg(panel,"选择房间组合",230,100,function () {
                    var rdo=$("input[type=radio]:checked",panel);
                    var ids=rdo.val();
                    var num=rdo.attr("num");
                    var name=rdo.attr("gname");
                    test.ids=ids;
                    test.name=name;
                    test.devNum=num;
                    testDic[key]=test;
                    refRsv();
                    panel.dialog("close");
                });
            });
        }
        $(function () {
            $(".room_filter").filterItem(RetSelecteds);
            $(".room_group_filter").filterItem(RetSelecteds);
            var term = pro.term;

            //上课人数
            $(".cd_mb_num").html($(".mb_num").val());
            $(".cd_valid_period").html($(".valid_period").val());
            //选择项目
            $(".store-selector").linkage({
                level: <%=GetConfig("scheduleMode")=="2"?1:2%>,
                curSelect:[<%=curLink%>,0,0],
                oneLevelData: <%=planLinkList%>,
                getTwoLevelData:function(id,callback){
                    pro.j.test.getTestitemList(id,"",function(rlt){
                        callback(rlt.data);
                    });
                },
                clickOneLevel:function(id){
                    if("<%=GetConfig("scheduleMode")%>"=="2"){
                        pro.j.test.getTestitemList(id,"",function(rlt){
                            var item=rlt.data[0];
                            var para = uni.url2Obj(location.href);
                            para.test_id = item.id;
                            location.href = location.pathname + "?" + uni.obj2Url(para);
                        });
                    }
                },
                clickTwoLevel: function (id, name) {
                    var para = uni.url2Obj(location.href);
                    para.test_id = id;
                    location.href = location.pathname + "?" + uni.obj2Url(para);
                }
            });
        });
            function RetSelecteds(fl){
                reserve_cld = $("#calendar").uniCalendar({
                    modes: "d",
                    width: 740,
                    height: 500,
                    objTitleMinWidth: 80,
                    cellHeight: 60,
                    borderWidth: 1,
                    dayOpt:{
                        snipTime:2,
                        evSelObj:function(data){
                            batch(parseInt(data.obj.id)*10000000)
                        },
                    },
                    stateSetOpt:{
                        deadline:[5,9],
                        external:"<%=TeachResvMode%>"=="1"//外部控制模式，触发evClickState
                    },
                    relative: false,
                    schedule:true,
                    openWeeks:"<%=TeachResvMode%>"=="1"?[<%=openWeeks%>]:undefined,
                    secnum: term.secnum||8,
                    secTime:["15:00","17:00"],
                    dateStart: term.start,
                    dateEnd: term.end,
                    evInitFaile:function(msg){
                        if(this.openWeeks.length>0)
                            uni.msgBox("无有效的日期！当前课程所允许的周次为:<span style='font-weight:bold;'>"+this.openWeeks.join()+"</span>","课表初始化失败");
                        else
                            uni.msgBox("当前无可预约的周次！");
                    },
                    evClickSchedule:function(tch){
                        batch(tch*100);
                    },
                    evRefState:function (keys,dict,tch) {
                        tests=keys;
                        testDic=dict;
                        refRsv();
                    },
                    evSelTime: function (data) {
                        //var obj=data.obj;
                        //reserve_cld.tmp_ltch=parseInt(obj.id)*10000000+data.wwd*10000+data.sec*100;
                    },
                    evUpTime: function (date, callback) {
                        //callback(objs, "unilab3", { plans: seclist });
                        if(fl.rm_type=="group"){
                            pro.j.rm.getRGRsvSta(date.format("yyyyMMdd"), function (rlt) {
                                suc(rlt);
                            },fl);
                        }
                        else{
                            pro.j.rm.getRsvSta(date.format("yyyyMMdd"), function (rlt) {
                                suc(rlt);
                            },fl);
                        }
                        function suc(rlt){
                            if(rlt.data){
                                for (var i = 0; i < rlt.data.length; i++) {
                                    var obj=rlt.data[i];
                                    objs[obj.id]=obj;
                                }
                            }
                            callback(rlt.data, "unilab3");
                        }
                    },
                    evFinishDraw: function (dt) {
                        var wwd = pro.dt.date2wwd(dt);
                        $("#calendar .cld-top-c").html("<h3 class='text-primary week'>" + $(".cur_term_name").html() + " 第<code>" + parseInt(wwd / 10) + "</code>周</h3>");
                        $("[title]").tooltip();
                    },
                    evUpPlans: function (obj, start, end, callback, opt) {
                        callback();
                    }
                });
                //reserve_cld.stateSet.reset();//重新搜索要清空
            }
            var resvs;
            function delResvs() {
                if (resvs) {
                    resvs = resvs.substr(0, resvs.length - 1);
                    pro.j.rsv.delResv(resvs, function () {
                        uni.msgBox("已撤销全部预约", "", function () {
                            $("#dlg_resv_ret").dialog("close");
                        });
                    });
                }
                else
                    $("#dlg_resv_ret").dialog("close");
            }
            //提交预约
            function subResvList() {
                uni.confirm("确定提交预约？", function () {
                    var list = "";
                    for (var t = 0; t < tests.length; t++) {
                        var v = testDic[tests[t] + ""];
                        list += v.group +","+$(".group_name").html()+ "&" + v.ids+"&"+ v.ltch +(v.time?"&"+v.time:"") +";";
                    }
                    pro.j.rsv.setTchResv(pro.term.year, $(".test_id").val(), list, function (rlt) {
                        $("#rsv_list").html("");
                        $("#resv_report").hide();
                        var list = rlt.data,
                        sucList = "",
                        failList = "";
                        resvs = "",
                        failArr=[],
                        sucArr=[];
                        allsuc=true;
                        for (var i = 0; i < list.length; i++) {
                            if(list[i].state!=1){
                                allsuc=false;
                                break;
                            }
                        }
                        if(allsuc){
                            uni.confirm("预约成功，是否跳转到我的实验计划？",function(){
                                location.href="TestPlan.aspx";
                            },function(){
                                reserve_cld.stateSet.reset();
                                reserve_cld.uploadCld();
                            });
                        }
                        else{
                            for (var k = 0; k < list.length; k++) {
                                var tmp=list[k];
                                var devs=tmp.ids.split(',');
                                var name="";
                                for (var m = 0; m < devs.length; m++) {
                                    if(!devs[m]) continue; 
                                    var obj=objs[devs[m]];
                                    if(obj){
                                        name+=obj.name+'&nbsp;';
                                    }
                                    //for (var i = 0; i < objs.length; i++) {
                                    //    if (objs[i].id == devs[m]) {
                                    //        name+=objs[i].name+'&nbsp;';
                                    //        break;
                                    //    }
                                    //}     
                                }
                                failList+="<h4>"+name+ "</h4>";
                                sucList+="<h4>"+name+ "</h4>";
                                var fail='',suc='';
                                for (var n = k; n < list.length; n++) {
                                    var rs=list[n];
                                    var tch = parseInt(rs.tchTime / 100),
    wwd = parseInt(tch / 100),
    end = rs.tchTime%100;    
                                    if (rs.state == 1)
                                        resvs += rs.resvId + ",";
                                    if(rs.ids==tmp.ids){
                                        var title ="第<code>" + parseInt(wwd/10) + "</code>周/星期<code>" + wk[wwd%10] + "</code>/";
                                        if(rs.time){
                                            var t=rs.time.substr(8);
                                            title+="<code>"+t.substr(0,2)+":"+t.substr(2,2)+"-"+t.substr(4,2)+":"+t.substr(6,2)+"</code>";
                                        }
                                        else{
                                            title+="第<code>" + chi[tch%100] + "</code>节";
                                            if (rs.diff > 0)
                                                title += "--第<code>" + chi[end] + "</code>节";
                                        }

                                        var cause = rs.memo;
                                        var panel="<div class='alert alert-"+(rs.state==1?"info":(rs.state==2?"danger":"warning"))+"'><div class='rsv'>" + title + "</div><div class='cause'>" + cause + "</div></div>";
                                        if (rs.state == 1){
                                            suc += panel;
                                            sucArr.push(parseInt(rs.ltch));
                                        }
                                        else{
                                            fail += panel;
                                        }
                                    }
                                    else
                                        break;
                                }
                                failList+=fail||"无失败记录";
                                sucList+=suc||"无成功记录";
                                k=n-1;
                            }
                            removeTest(sucArr);//去掉成功
                            $("#resv_report").show();
                            $("#resv_ret_suc").html(sucList||"<h4>无成功记录</h4>");
                            $("#resv_ret_fail").html(failList||"<h4>无失败记录</h4>");
                            uni.dlg($("#dlg_resv_ret"), "预约结果", 700, 600,null,null,function(){reserve_cld.uploadCld();});
                        }
                    },{purpose:"<%=purpose%>"});
                });
                }
                // 清除全部排课
                function removeResvList(){
                    $("#rsv_list .remove_list").click();
                }
                //处理组方法
                function openGroup() {
                    var id = $(".group_id").val();
                    pro.d.group.manage("设置上课班级", { group: id,need_cls:"true" }, function (dlg) {
                        if (dlg.group_id) {
                            $("#btn_g_set").children(".group_name").html(dlg.group_name);
                        }
                    });
                }
                function splitGroup(key){
                    var test=testDic[key];
                    if(test.group==$(".group_id").val()){
                        uni.confirm("确定需要重新设置实验上课学生？",function () {
                            showSplitGroup(true,test);
                        });
                    }
                    else{
                        showSplitGroup(false,test);
                    }
                }
                function showSplitGroup(flag,test){
                    var para;
                    var id=test.group;
                    if(flag)
                        para={tp_group:id,name:"实验组_"+id}
                    else
                        para={group:id};
                    parent.pro.d.group.manage('重新设置实验学生', para, function (dlg) {
                        if (dlg.group_id) {
                            test.group=dlg.group_id;
                            test.mbNum=dlg.group_num;
                            actTch();
                        }
                    });
                }
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div>
        <input runat="server" id="test_id" class="test_id" type="hidden" />
        <input runat="server" id="group_id" class="group_id" type="hidden" />
        <input runat="server" id="mb_num" class="mb_num" type="hidden" />
        <input runat="server" id="valid_period" class="valid_period" type="hidden" />
    </div>
    <!--批量排课-->
    <style>
        .batch_panel input[type=text] { width: 40px; height: 22px; }
        .batch_panel select { width: 40px; height: 22px; }
        .batch_panel .batch_line { padding: 8px 0; border-top: dashed 1px #ccc; }
        .batch_panel h3 { font-family: 'Microsoft YaHei'; }
        .solid_diff { display: none; }
    </style>
    <div class="dialog" id="dlg_batch">
        <div class="batch_panel">
            <h3>地点</h3>
            <div class="panel panel-default">
                <div class="panel-body">
                    <h3 class="rm_name" style="margin-top: 5px;"></h3>
                    <div class="batch_line rm_detail">
                    </div>
                </div>
            </div>
            <h3>时间</h3>
            <div class="open_weeks" style="margin-bottom: 5px;"></div>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div style="margin-bottom: 8px;">
                        从第
                        <input type="text" class="start_week" />
                        周 至 第
                        <input type="text" class="end_week" />
                        周，每
                <select class="week_freq">
                    <option value="1">单</option>
                    <option value="2">双</option>
                    <option value="3">三</option>
                </select>
                        周：
                    </div>
                    <div class="batch_line sel_week">
                        <label>
                            <input type="checkbox" name="radio_week" value="0" />周一&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="1" />周二&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="2" />周三&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="3" />周四&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="4" />周五&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="5" />周六&nbsp;</label>
                        <label>
                            <input type="checkbox" name="radio_week" value="6" />周日&nbsp;</label>
                    </div>
                    <%if (TeachResvMode == "1" || GetConfig("clientTab") == "gs")
                      { %>
                    <div class="batch_line" id="batch_pro_jl">
                        <input type="hidden" class="cls_start" />
                        <input type="hidden" class="cls_end" />
                        <input type="hidden" class="cls_time" />
                        <span>上课时间 </span>
                        <select class="solid_diff sel_sec two_sec" style="width: 120px;">
                            <option value="0102">1-2节</option>
                            <option value="0304">3-4节</option>
                            <option value="0506">5-6节</option>
                            <option value="0708">7-8节</option>
                            <option value="0910">9-10节</option>
                            <option value="1112">11-12节</option>
                        </select>
                        <select class="solid_diff sel_sec four_sec" style="width: 120px;">
                            <option value="0104">1-4节</option>
                            <option value="0508">5-8节</option>
                        </select>
                        <select class="solid_diff sel_sec cus_sec" style="width: 120px;">
                            <%=selSecOpt %>
                        </select>
                        <select class="solid_diff sel_time" style="width: 140px;">
                            <%=selTimeOpt %>
                        </select>
                    </div>
                    <%} %>
                    <%else
                      { %>
                    <div class="batch_line general">
                        第&nbsp;<select class="cls_start" style="width: 50px;">
                            <option value="1">一</option>
                            <option value="2">二</option>
                            <option value="3">三</option>
                            <option value="4">四</option>
                            <option value="5">五</option>
                            <option value="6">六</option>
                            <option value="7">七</option>
                            <option value="8">八</option>
                            <option value="9">九</option>
                            <option value="10">十</option>
                            <option value="11">十一</option>
                            <option value="12">十二</option>
                        </select>&nbsp;节到第&nbsp;
                <select class="cls_end" style="width: 50px;">
                    <option value="1">一</option>
                    <option value="2">二</option>
                    <option value="3">三</option>
                    <option value="4">四</option>
                    <option value="5">五</option>
                    <option value="6">六</option>
                    <option value="7">七</option>
                    <option value="8">八</option>
                    <option value="9">九</option>
                    <option value="10">十</option>
                    <option value="11">十一</option>
                    <option value="12">十二</option>
                </select>
                        &nbsp;节。
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
    <script>
        function batch(ltch) {
            var tc = parseInt(ltch / 100),
                diff = ltch % 100,
                oid=parseInt(tc/100000);
            var ww = parseInt((tc % 100000) / 1000),
            d = parseInt((tc % 1000) / 100),
            sec = tc % 100;
            var info;
            for (var i in testDic) {
                if(testDic[i].oid==(oid+"")){
                    info=testDic[i];
                    break;
                }
            }
            if(!info){
                info=objs[oid];
                //for (var i = 0; i < objs.length; i++) {
                //    if(objs[i].id==oid){
                //        info=objs[i];
                //        break;
                //    }
                //}
            }
            if(!info)return;
            var dlg = $($("#dlg_batch").html());
            if("<%=TeachResvMode%>"=="1"){
                initSolidSec(dlg);
                $(".solid_diff.active",dlg).trigger("change");
                if("<%=GetConfig("clientTab")%>" != "gs"){//工商不检查周次
                    var avail=[];//初始化有效周次
                    $("#calendar .cld-sel-ctrl option").each(function(){
                        avail.push(parseInt($(this).attr("value")));
                    });
                    $(".open_weeks",dlg).html("<code>本课程有效周次："+avail.join(',')+"</code>");
                }
            }
            $(".rm_name",dlg).html(info.name);//地点
            $(".rm_detail",dlg).html("<span class='grey'>设备数：<code>"+info.devNum+"</code>人，参与实验：<code>"+(info.mbNum||$(".mb_num").val())+"</code> 人。</span>");
            var wk_start=$(".start_week",dlg);//起始周
            var wk_end=$(".end_week",dlg);//结束周
            var wk_sel=$(".sel_week",dlg);//周次
            var cls_start=$(".cls_start",dlg);//开始节次
            var cls_end=$(".cls_end",dlg);//结束节次
            //时间初始化
            if(sec){//sec==0 则无初始时间
                wk_start.val(ww);
                wk_end.val(ww);
                $("input:eq("+d+")",wk_sel).attr("checked","checked");
                $("option:eq("+(sec-1)+")",cls_start).attr("selected","selected");
                $("option:eq("+(sec+diff-1)+")",cls_end).attr("selected","selected");
            }
            uni.dlg(dlg, "批量排课", 400, 460, function () {
                var start=parseInt(wk_start.val());
                var end=parseInt(wk_end.val());
                if(!start||!end||isNaN(start)||isNaN(end)){
                    uni.msg.warning("请正确填入周次");
                    return;
                }
                var cstart=parseInt(cls_start.val());
                var cend=parseInt(cls_end.val());
                var diff=cend-cstart;
                if(diff<0){uni.msg.error("开始节次不得大于结束节次");return;}
                var freq=parseInt($(".week_freq",dlg).val());
                var arr=[];
                var wks=[];
                $("input:checked",wk_sel).each(function(){
                    wks.push(parseInt(this.value));
                });
                if(wks.length==0){
                    uni.msg.warning("请勾选上课星期");
                    return;
                }
                var id=parseInt(tc / 100000) * 100000;
                for (var i = start; i <= end; i+=freq) {
                    if(avail&&!isArray(i,avail)){//检查有效周次
                        uni.msgBox("所选周次无效！当前课程可预约周次为:<span style='font-weight:bold;'>"+avail.join(',')+"</span>","周次无效");
                        return;
                    }
                    for (var j = 0; j < wks.length; j++) {
                        var test=id + i * 1000 + wks[j] * 100 + cstart;
                        if(cstart>49){//50以后sec默认为绝对时间
                            var dt=pro.dt.wwd2date(i*10+wks[j]);
                            var tp=testDic[test+'']||{};
                            tp.time=(dt.format("yyyyMMdd"))+$(".cls_time",dlg).val();
                            testDic[test+'']=tp;
                        }
                        var lt = test*100+diff;
                        arr.push(lt);
                    }
                }
                if(sec){//sec==0 则无初始时间
                    removeTest(ltch);
                }
                addTest(arr);
                dlg.dialog("close");
            });
        }
        function initSolidSec(dlg){
            //浙江工商
            if ("<%=GetConfig("clientTab")%>" == "gs")
            {
                $(".cus_sec",dlg).show().addClass("active");
                $(".sel_sec",dlg).change(function(){
                    var v=parseInt($(this).val(),10);
                    $(".cls_start",dlg).val(parseInt(v/100));
                    $(".cls_end",dlg).val(v%100);
                });
                return;
            }
            //
            if("<%=myCourse.dwCourseProperty%>"=="2"){
                $(".sel_time",dlg).show().addClass("active");
                $(".sel_time",dlg).change(function(){
                    var ck=$(this).children("option:selected");
                    var sec=parseInt(ck.index())+50;//50以后sec默认为绝对时间
                    $(".cls_start",dlg).val(sec);
                    $(".cls_end",dlg).val(sec);
                    $(".cls_time",dlg).val($(this).val());
                });
            }
            else{
                if("<%=myCourse.szMemo%>"=="1")
                    $(".four_sec",dlg).show().addClass("active");
                else
                    $(".two_sec",dlg).show().addClass("active");
                $(".sel_sec",dlg).change(function(){
                    var v=parseInt($(this).val(),10);
                    $(".cls_start",dlg).val(parseInt(v/100));
                    $(".cls_end",dlg).val(v%100);
                });
            }
        }
        function isArray(v,arr){
            for (var i = 0; i < arr.length; i++) {
                if(arr[i]==v)return true;
            }
            return false;
        }
    </script>
    <!--房间组合-->
    <div class="dialog" id="dlg_alter_rm_group">
        <div class="panel panel-default">
            <div class="panel-body content">
            </div>
        </div>
    </div>
    <!--预约结果-->
    <div class="dialog" id="dlg_resv_ret">
        <table>
            <thead>
                <tr>
                    <td>
                        <h4>已创建的预约</h4>
                    </td>
                    <td>
                        <h4>未创建的预约</h4>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="width: 50%; border-right-width: 3px;">
                        <div id="resv_ret_suc"></div>
                    </td>
                    <td style="width: 50%; border-left-width: 3px;">
                        <div id="resv_ret_fail"></div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="text-center" style="margin-top: 5px;">
            <div class="btn-group">
                <button type="button" class="btn btn-warning" onclick="delResvs()">撤销预约</button>
                <button type="button" class="btn btn-info dlg_close">继续预约</button>
            </div>
        </div>
    </div>
    <div class="panel panel-default" id="info_panel">
        <div class="panel-body">
            <div class="row">
                <div class="col col_7">
                    <h3 class="text-info" style="margin-top: 5px; margin-bottom: 10px; padding-bottom: 5px; border-bottom: 1px dashed #ccc;">实验：<%=testName %></h3>
                    <div style="margin-bottom: 5px;">
                        <div class="btn-group">
                            <button type="button" class="btn btn-info cur_term_name"><%=curTerm %></button>
                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                <span class="caret"></span><span></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <%=termList %>
                            </ul>
                        </div>
                        <span style="padding-left: 30px;">上课班级
                            <button type="button" id="btn_g_set" class="btn btn-info btn-xs" title="设置上课班级" onclick="openGroup()"><span class="group_name"><%=groupName %></span> <span class="glyphicon glyphicon-cog"></span></button>
                            &nbsp&nbsp 上课人数 <code class="cd_mb_num">0</code>
                            &nbsp&nbsp 可预约学时数 <code class="cd_valid_period">0</code>
                        </span>
                    </div>
                </div>
                <div class="col col_5" style="margin-top: 10px;">
                    <h5 class="pull-left">选择实验项目: &nbsp;</h5>
                    <div class="store-selector">
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col col_8">
            <span class="h_title step_title">1</span><code>查询房间预约状态</code>
            <div class="info_unitab" style="<%=GetConfig("clientTab") == "gs"?"":"display:none;" %>">
                <ul class="tab_head">
                    <li>
                        <div class="title">单房间预约状态</div>
                        <div class="caret"></div>
                    </li>
                    <li>
                        <div class="title">组合房间预约状态</div>
                        <div class="caret"></div>
                    </li>
                </ul>
                <div class="tab_con" style="">
                    <div class="item">
                        <div class="panel panel-default room_filter" style="margin-top: 10px;">
                            <div class="panel-heading" style="padding: 5px"><span class="glyphicon glyphicon-list"></span>&nbsp;查询条件</div>
                            <table class="table table-condensed filter_table">
                                <tr>
                                    <td style="width: 40px;">区域
                                    </td>
                                    <td>
                                        <select class="form-control must_sel sel_lab" key="lab_id" style="width: 200px;">
                                            <option value="0">未选择</option>
                                            <%=LabList %>
                                        </select>
                                    </td>
                                    <td style="width: 40px;">容量
                                    </td>
                                    <td>
                                        <input class="form-control must allow_null" data-reg="number" data-ckmsg="只能填入数字" type="text" key="dev_num" style="width: 40px;" />
                                    </td>
                                    <td style="width: 40px;">名称</td>
                                    <td>
                                        <input type="text" class="form-control" key="room_name" style="width: 200px;" />
                                        <button type="button" class="btn btn-info sub_filter pull-right">&nbsp;查询&nbsp;</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="item">
                        <div class="panel panel-default room_group_filter" style="margin-top: 10px;">
                            <div class="panel-heading" style="padding: 5px"><span class="glyphicon glyphicon-list"></span>&nbsp;查询条件</div>
                            <table class="table table-condensed filter_table">
                                <tr>
                                    <td style="width: 40px;">容量
                                    </td>
                                    <td>
                                        <input class="form-control must allow_null" data-reg="number" data-ckmsg="只能填入数字" type="text" key="dev_num" style="width: 40px;" />
                                    </td>
                                    <td>
                                        <input type="hidden" key="rm_type" value="group" />
                                        <button type="button" class="btn btn-info sub_filter pull-right">&nbsp;查询&nbsp;</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                $(".info_unitab").unitab();
            </script>
            <div>
                <div class="text-left"><span class="h_title step_title">2</span><code>点击房间名排课</code></div>
                <div id="resv_report" class="text-right" style="display: none;"><a onclick="uni.dlg($('#dlg_resv_ret'), '预约记录', 700, 600);">查看上一次预约记录</a></div>
            </div>

            <div id="calendar"></div>
        </div>
        <div class="col col_4">
            <div style="font-size: 12px;" class="alert alert-warning">
                <%=resvRule %>
            </div>
            <span class="h_title step_title">3</span><code>提交预约，完成排课</code>
            <div class="panel panel-default" id="rsv_panel" style="margin-top: 3px;">
                <div class="panel-heading">
                    <span class="text-primary" style="line-height: 30px;"><span class="glyphicon glyphicon-list"></span>&nbsp;排课列表</span>
                    <div class="btn-group pull-right">
                        <button class="btn btn-info btn-sm" onclick="subResvList()">提交预约</button>
                        <button class="btn btn-default btn-sm" onclick="removeResvList()">清除全部</button>
                    </div>
                </div>
                <div class="panel-body" id="rsv_list">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

