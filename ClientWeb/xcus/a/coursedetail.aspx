<%@ Page Language="C#" AutoEventWireup="true" CodeFile="coursedetail.aspx.cs" Inherits="ClientWeb_xcus_all_coursedetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .detail_list { width: 80%; }
        .detail_list td { line-height: 28px; text-indent: 0; }
        .detail_list td.dt { border-right: 1px solid #aaa; color: #666; font-weight: 600; padding-left: 40px; }
        .detail_list td.dd { color: #999; padding-left: 30px; }
    </style>
    <div class="click btn_back" onclick="uni.hr.back();"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;返回</div>
    <h2><%=CourseName %></h2>
    <div class="line"></div>
    <div class="info_unitab">
        <ul class="tab_head">
            <li>
                <div class="title">课程属性</div>
                <div class="caret"></div>
            </li>
            <li>
                <div class="title">详细介绍</div>
                <div class="caret"></div>
            </li>
        </ul>
        <div class="tab_con tab-content">
            <div>
                <table class="detail_list">
                    <tbody>
                        <tr>
                            <td class="dt">课程名称</td>
                            <td class="dd"><%=CourseName %></td>
                            <td class="dt">课程学分</td>
                            <td class="dd"><span runat="server" id="Credit"></span></td>
                        </tr>
                        <tr>
                            <td class="dt">课程代码</td>
                            <td class="dd"><span runat="server" id="CourseCode"></span></td>
                            <td class="dt">理论学时</td>
                            <td class="dd"><span runat="server" id="TheoryHour"></span></td>
                        </tr>
                        <tr>
                            <td class="dt">课程类型</td>
                            <td class="dd"><span runat="server" id="CourseType"></span></td>
                            <td class="dt">实验学时</td>
                            <td class="dd"><span runat="server" id="TestHour"></span></td>
                        </tr>
                        <tr>
                            <td class="dt">开课学院</td>
                            <td class="dd"><span runat="server" id="Dept"></span></td>
                            <td class="dt">实践学时</td>
                            <td class="dd"><span runat="server" id="PracticeHour"></span></td>
                        </tr>
                        <tr>
                            <td class="dt">所属专业</td>
                            <td class="dd"><span runat="server" id="Major"></span></td>
                            <td class="dt">实验次数</td>
                            <td class="dd"><span runat="server" id="TestNum"></span></td>
                        </tr>
                    </tbody>
                </table>
                <div style="border-bottom:dotted 1px #ddd;height:1px;margin:20px 10px;"></div>
                <style>
                    .plan_list h4 { border-left: 3px solid #777;padding-left: 5px;}
                </style>
                <div style="margin:15px 5px;"><h3 style="display:inline;color:#666;">本学期实验计划</h3><span class="badge" style="background-color: #E2001B;display:inline;vertical-align: top;"><%=planCount %></span></div>
                <ul class="plan_list">
                    <%=testPlanList %>
                </ul>
            </div>
            <div>
                <%=courseIntro %>
            </div>
        </div>
    </div>
    <script>
        $(".info_unitab").unitab();
    </script>
</body>
</html>
