<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="DevWeb_Course" %>
<%@ Register TagPrefix="Uni" TagName="dlg_rtest" Src="~/ClientWeb/pro/net/dlg_rtest.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="theme/TabStyleH.css" rel='stylesheet' />
    <style type="text/css">
        #alterUser { font-size: 14px; }
    </style>
    <script type="text/javascript">
        $(function () {
            $("a.alterCourse").click(function () {
                var id = $(this).parent().parent().find("input.courseId").val();
                if (id != "" && id != undefined) {
                    pro.d.rtest.rtInfoM(id);
                }
            });
            $("a.delCourse").click(function () {
                var pthis=$(this);
                var id = pthis.parent().parent().find("input.courseId").val();
                uni.confirm("确定要删除项目？", function () {
                    if (id != "" && id != undefined) {
                        pro.j.rtest.delRTest(id, function () {
                            uni.msgBox("删除成功！");
                            pthis.parent().parent().hide();
                        });
                    }
                });
            });
            $(".tutor_check").click(function () {
                var obj = $(this);
                if ($(this).hasClass("ok")) {
                    uni.confirm("是否认定申请人为您的学生？", function () {
                        actCheck("ok", obj);
                    });
                }
                else if ($(this).hasClass("del")) {
                    uni.confirm("确定删除本条申请？", function () {
                        actCheck("del", obj);
                    });
                }
                else if ($(this).hasClass("fail")) {
                    uni.confirm("是否撤销此人与您的师生关系？", function () {
                        actCheck("fail", obj);
                    });
                }
            });
        });
        //学生管理
        function mbManage(rt_id, rt_name) {
            pro.d.rtest.rtMbM(rt_id, rt_name);
        }
        function actCheck(order, obj) {
            if (order == undefined || order == "") {
                return;
            }
            var stu_name = obj.parents("tr").find(".stu_name").html();
            var stu_accno=obj.parents("tr").find(".stu_accno").val();
            pro.j.acc.tutorRel(order, stu_accno, stu_name, function () {
                uni.msgBoxR("操作成功,将重新加载页面！");
            });
        }
    </script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <Uni:dlg_rtest runat="server" />
    <div id="templatemo_main">
        <div id="con" class="tabs">
            <ul id="tabs" class="tab_head">
                <li id="tab_rt"><a>我的项目</a></li>
                <li id="memApply" style="display:none"><a>我的学生</a></li>
            </ul>
            <div id="tabContent" class="float_all tab_con">
                <div class="item">
                    <div style="text-align: right;"><a id="addRTest" class="button hidden" onclick="$('#addrtdialog').dialog('open');return false;">新建项目</a></div>
                    <div class="resv_list tbl_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 180px;">项目名称</th>
                                    <th style="width: 80px;">委托授权</th>
                                    <th style="">项目级别</th>
                                    <th style="">下发单位</th>
                                    <th style="">所属部门</th>
                                    <th style="width: 80px;">成员数量</th>
                                    <th style="width: 140px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=rtList %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="item">
                    <div style="text-align: left;"><span style="color:red;">提示：</span>此页面为导师确认申请人是否是自己的学生，以确立师生关系。师生关系与实验预约无关。</div>
                    <div class="tbl_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">学生</th>
                                    <th style="">学号</th>
                                    <th style="">部门</th>
                                    <th style="">联系方式</th>
                                    <th style="width: 60px;">状态</th>
                                    <th style="width: 120px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=stuList %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="cleaner"></div>
    </div>
</asp:Content>
