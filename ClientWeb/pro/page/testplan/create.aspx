<%@ Page Language="C#" AutoEventWireup="true" CodeFile="create.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_testplan_create" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        .form-control { width: 200px; }
        .add_input { width: 100%; }
    </style>
    <script>
        function setStudents() {
            var pg_group = $("#pg_group_id");
            var name = $(".course_name").val();
            var para = {};
            para.line = parent.pro.term.end;
            para.need_cls = "true";
            if (pg_group.val()) {
                para.group = pg_group.val();
                para.name = $(".group_name").html();
            }
            else {
                if (name)
                    para.name =parent.pro.acc.name+"_"+ name;
                else {
                    uni.msgBox("请先选择课程");
                    return;
                }
            }
            parent.pro.d.group.manage('设置上课学生', para, function (dlg) {
                if (dlg.group_id) {
                    pg_group.val(dlg.group_id);
                    $(".group_name").html(dlg.group_name);
                    //无效组删除标志
                    var req = uni.getReq();
                    var key = req["dlg_key"];
                    parent.uni.dlgInst[key].group_id = dlg.group_id;
                }
            })
        }
        function creTest() {
            var name = $("#pg_test_name").val();
            var time = $("#pg_test_time").val();
            if (name && time) {
                $("#pg_test_list").append("<tr><td>" + name + "</td><td>" + time + "学时</td><td><span class='glyphicon glyphicon-trash click' title='删除' onclick='$(this).parents(\"tr:first\").remove();'></span></td></tr>");
                $("#pg_test_name").val("");
                $("#pg_test_time").val("");
            }
        }
        $(function () {
            $("#pg_course").procomplete(function (event, ui) {
                var item = ui.item;
                if (item && item.id) {
                    var detail = $("#pg_course_detail");
                    $(".course_id", detail).val(item.id);
                    $(".course_name", detail).val(item.name);
                    $(".code", detail).html(item.code);
                    $(".type", detail).html(item.type);
                    $(".testhour", detail).html(item.testhour);
                    $(".test_hour").val(item.testhour);
                    $(".testnum", detail).html(item.testnum);
                    $(".test_num").val(item.testnum);
                    pro.j.test.getTestitemList("", item.id, function (rlt) {
                        var data = rlt.data;
                    });
                }
            });
            //选择课程班
            if ($("#pg_group_name").length > 0) {
                $("#pg_group_name").procomplete(function (ev, ui) {
                    $("#pg_group_id").val(ev.id);
                    $(".group_name").html("已选：" + ev.name);
                });
            }
            //日期控件
            $(".deadline").datepicker({minDate:0});
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <form class="form" role="form" runat="server">
        <input type="hidden" runat="server" id="term_year" class="term_year" />
        <div class="panel panel-default" style="min-height: 300px;">
            <div class="panel-body">

                <div>
                    <div class="form-group" style="margin-bottom:2px;">
                        <div class="btn-group">
                            <button disabled="disabled" type="button" class="btn btn-info"><%=curTerm %></button>
                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                <span class="caret"></span><span></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <%=termList %>
                            </ul>
                        </div>
                    </div>
                    <div class="line"></div>
                    <h3>选择课程 <small>输入课程名搜索</small></h3>
                    <table class="makeup">
                        <tr>
                            <td>
                                <label class="" for="pg_course">课程搜索</label></td>
                            <td>
                                <input type="text" class="form-control hint" id="pg_course" placeholder="课程名/代码搜索" url="searchCourse.aspx"></td>
                            <td class="th">
                                学生类别</td>
                            <td>
                                <select class="form-control" id="pg_user_kind" name="user_kind">
                                    <option value="1">博士生</option>
                                    <option value="2">硕士生</option>
                                    <option value="3" selected="selected">本科生</option>
                                    <option value="4">专科生</option>
                                    <option value="5">其它</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <%if (GetConfig("clientTab")=="gd")
                              { %><td>实验学时<span style="color:red">*</span></td><td>
                                <input type="text" class="form-control test_hour" name="test_hour">
                                <input type="hidden" class="test_num" name="test_num" />
                            </td><%} else{%>
                            <td>
                                <input type="hidden" class="test_hour" name="test_hour" />
                                <input type="hidden" class="test_num" name="test_num" />
                            </td><td></td>
                            <%} %>
                            <td class="th">所属学科</td>
                            <td>
                                <input type="text" class="form-control" placeholder="选填">
                            </td>
                        </tr>
                    </table>
                    <div class="form-group">
                    </div>
                    <div id="pg_course_detail" class="grey">
                        <input type="hidden" class="course_name" name="course_name" /><input type="hidden" class="course_id" name="course_id" />课程代码：<span class="code text-info"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;课程计划学时数：<span class="testhour text-info"></span>(小时)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;课程计划实验数：<span class="testnum text-info"></span>
                    </div>
                    <div class="line"></div>
                    <h3>设置班级 <small>设置上课学生</small></h3>
                    <div class="form-group" style="display:<%=planKind!=2?"":"none"%>">
                        <input type="hidden" name="group_id" class="group_id" id="pg_group_id" />
                        <%if(Request["sel_group"]=="true"){ %>
                        <input type="text" class="form-control hint" id="pg_group_name" onclick="this.value=''" placeholder="班级名自动搜索" para="line=<%=term.dwEndDate %>&kind=256&need=5" url="searchGroup.aspx">
                        <div class="group_name orange" style="margin:20px 10px;">还未选择班级</div>
<%}else{%>        
                        <div class="btn-group">
                            <button type="button" class="btn btn-default group_name" disabled="disabled">未设置上课学生</button>
                            <button type="button" class="btn btn-primary" onclick="setStudents()">设置</button>
                        </div>
                        <%}%>
                    </div>
                    <div class="form-group" style="display:<%=planKind==2?"":"none"%>">
                    <table class="makeup">
                        <tbody>
                            <tr><td>班级人数</td>
                                <td>
                                    <input style="width:60px;" type="text" name="mb_max" class="form-control must" placeholder="必填">
                                </td>
                                <td>选课截止日期</td>
                                <td>
                                    <input type="text" name="deadline" class="form-control deadline must" placeholder="必填" readonly="true" style="cursor:pointer;"/>
                                </td></tr>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
            <%--            <h4 class="text-primary">&nbsp;<span class="glyphicon glyphicon-plus"></span> 添加实验项目</h4>
            <!-- Table -->
            <table class="table table-striped table-bordered table-condensed">
                <thead>
                    <tr>
                        <th style="width: 70%;">实验项目名称</th>
                        <th>计划实验学时</th>
                        <th>操作
                        </th>
                    </tr>
                </thead>
                <tbody id="pg_test_list">
                    <tr>
                        <td>
                            <input type="text" class="form-control add_input" id="pg_test_name" placeholder="输入名称"></td>
                        <td>
                            <input type="text" class="form-control add_input" id="pg_test_time" placeholder="实验学时（/小时）"></td>
                        <td>
                            <button type="button" class="btn btn-info btn-sm" onclick="creTest()">新建 <span class="glyphicon glyphicon-plus-sign"></span></button>
                        </td>
                    </tr>
                </tbody>
            </table>--%>
        </div>
                    <div class="text-center">
                <button type="button" class="btn btn-info" runat="server" id="cre_course" onserverclick="cre_course_ServerClick">保存</button>
                <button type="button" class="btn btn-default dlg_page_close">返回</button>
            </div>
    </form>
</asp:Content>
