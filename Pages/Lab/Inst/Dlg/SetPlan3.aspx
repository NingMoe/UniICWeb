<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan3.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">

    <form id="Form1">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="Step" id="Step" value="1" type="hidden" />
        <input name="IsSubmit" value="true" type="hidden" />
        <input name="dwTestPlanID" id="dwTestPlanID" type="hidden" />
        <input name="dwCourseID" id="dwCourseID" type="hidden" />
        <input name="dwTeacherID" id="dwTeacherID" type="hidden" />
         <input name="dwKind" id="dwKind" type="hidden" value="1" />
      
        <input type="hidden" id="classID" />
        <input type="hidden" id="accno" />
        <input type="hidden" id="szLogonName" />
        <div id="divAddgroupMember">
            <table class="ListTbl2">
                <tr>
                    <th>班级名称</th>
                    <td>
                        <input type="text" id="className" name="className" style="width: 120px;" />
                        <input type="button" id="btnAddClass" value="添加班级" style="width: 100px; height: 27px" />
                    </td>
                    <th>学生</th>
                    <td>
                        <input type="text" id="szTrueName" name="szTrueName" class="tdinput" style="width: 120px;" />
                        <input type="button" id="btnAddUser" value="添加学生" style="width: 100px; height: 27px" />
                    </td>
                </tr>
                <!--
                <tr>
                    <th>上传名单</th>
                    <td colspan="3">
                        <input type="file" id="file" name="file" style="width: 120px;" />
                        <input type="button" id="btnImport" value="确定导入" style="width: 100px; height: 27px" />
                    </td>
                </tr>
                -->
            </table>
            <div class="content" style="height: 290px; overflow: auto">

                <table class="ListTbl">
                    <thead>
                        <tr>
                            <th>编号</th>
                            <th>名称</th>
                            <th>备注</th>
                        </tr>
                    </thead>
                    <tbody id="tblContent">
                    </tbody>
                </table>

            </div>
        </div>
        <div class="formtable" id="testPlanDiv">
            <table class="ListTbl2">
                <tbody>
                    <tr>
                        <th>学期：</th>
                        <td colspan="3">
                            <select id="dwYearTerm" name="dwYearTerm">
                                <%=m_TermList %>
                            </select>
                        </td>
                        <!--
                        <th>实验计划名称*：</th>
                        <td>
                            <input id="szTestPlanName" name="szTestPlanName" />
                        </td>
                        -->
                    </tr>
                    <tr>
                        <th>教师姓名*:</th>
                        <td>
                            <input type="text" id="szTeacherName" name="szTeacherName" class="validate[required]" /></td>
                        <th>教师所在部门:</th>
                        <td>
                            <label id="szTeacherDeptName" />
                        </td>

                    </tr>
                    <tr>
                        <th>课程*：</th>
                        <td>
                            <input name="szCourseName" type="text" id="szCourseName" />
                        </td>
                        <th>实验学时数*:</th>
                        <td>
                            <input type="text" id="dwTestHour" name="dwTestHour" /></td>
                    </tr>
                    <tr>
                        <th>课程班名称*：</th>
                        <td colspan="3" style="text-align:left">
                           <!-- <input name="classNameInfo" id="classNameInfo" />-->
                            <select id="dwGroupID" name="dwGroupID"></select>
                             <input type="hidden" id="" value="0" />
                     <!--       <a href="#" type="button" id="btnsetGroup" class="ui-state-active">设置名单</a>-->
                        </td>

                    </tr>
                    <tr>
                        <th>所属学科：</th>
                        <td>
                            <select id="szAcademicSubject" name="szAcademicSubject">
                                <%=szAcademicSubject %>
                            </select>
                        </td>
                        <th>实验者类别：</th>
                        <td>

                            <select id="dwTesteeKind" name="dwTesteeKind">
                                <%=szTesteeKind %>
                            </select>
                        </td>
                    </tr>

                    <%if (m_CreateOK || Request["op"] == "set")
                      { %>
                    <tr>
                        <th>实验项目：</th>
                        <td colspan="3">
                            <a class="accNew">添加项目</a>
                            <input id="ItemAllData" name="ItemAllData" type="hidden" />
                            <div id="TestItem"></div>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td colspan="4" class="btnRow">
                            <%if (!m_CreateOK || bSet) { }%>
                            <input type="button" id="OK" value="<%=szNextName %>" />
                            <input type="button" id="Cancel" value="取消" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="TestItemTemp">
            <div style="float: left; width: 30%; text-align: center;">
                <div>
                    <table id="tableLeft">
                        <tr>
                            <td><a id="btnNes">新建实验项目</a></td>
                        </tr>
                    </table>
                </div>

            </div>

            <div style="float: right; width: 70%">
                <input type="hidden" id="testitemid" name="testitemid" />
                <div id="divTestItemHtml" class="formtable">
                    <table class="ListTbl2">
                        <tr>
                            <th>实验名称</th>
                            <td>
                                <input name="szTestName" id="szTestName" /></td>
                            <th>每组人数</th>
                            <td>
                                <input name="dwGroupPeopleNum" id="dwGroupPeopleNum" /></td>
                        </tr>
                        <tr>
                            <th>实验类别</th>
                            <td>
                                <select name="dwTestClass" id="dwTestClass" style="width: 120px;">
                                    <option value="1">基础</option>
                                    <option value="2">专业基础</option>
                                    <option value="3">专业</option>
                                    <option value="4">其他</option>
                                </select>
                            </td>
                            <th>实验类型</th>
                            <td>
                                <select name="dwTestKind" id="dwTestKind" style="width: 120px;">
                                    <option value="1">演示性</option>
                                    <option value="2">验证性</option>
                                    <option value="3">综合性</option>
                                    <option value="4">研究设计</option>
                                    <option value="5">其他</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>学时</th>
                            <td>
                                <input name="dwTestItemTestHour" id="dwTestItemTestHour" /></td>
                            <th>备注</th>
                            <td>
                                <input name="szTestItemMemo" id="szTestItemMemo" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center">
                                <input type="button" id="btnAddTestItem" value="新增项目" />
                                <input type="button" id="btnTestItemCancel" value="完成实验计划" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>


</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">

    <style type="text/css">
        .ui-accordion .ui-accordion-header {
            padding-top: 2px !important;
            padding-bottom: 2px !important;
            height: 25px;
        }

        .ui-accordion h3.ui-accordion-header {
            background: #bdd9f2;
        }

        .ui-accordion h3.newTemp {
            background: #f2d9bd;
        }

        .accHeadText {
            float: left;
            line-height: 25px;
            margin-right: 10px;
        }

        .accHeadOP {
            float: right;
        }

        .tblBtn {
            text-align: center;
        }

        .changeFlag {
            display: inline;
            float: left;
            font-size: 10px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(function () {
            var tblContent = $("#tblContent");
            $("#TestItemTemp").hide();
            $("#btnAddTestItem,#btnTestItemCancel,#OK,#Cancel,#btnNes,#btnImport").button();
            setTimeout(function () {
                AddGroupClass();
            }, 500);
            $("#dwYearTerm").change(function () { AddGroupClass();});
            function AddGroupClass() {
                var vYearTermID = $("#dwYearTerm").val();
                $.get(
             "../../data/searchclassgroup.aspx",
             { termID: vYearTermID},
             function (data) {
                 $("#dwGroupID").empty();
                 var vList = eval(data);
                 for (var i = 0; i < vList.length; i++) {
                     var vVaule = vList[i];
                     $("<option value='" + vVaule.id + "'>" + vVaule.label + "</option>").appendTo($("#dwGroupID"));
                 }
                });
            }
            $("#divAddgroupMember").dialog({
                autoOpen: false,
                height: 520,
                width: 620,
                modal: true
            });

            AutoClass($("#className"), 2, $("#classID"), false);
            AutoCourseName($("#szCourseName"), 2, $("#dwCourseID"), 1, false);
            $("#szCourseName").on("autocompleteselect", function (event, ui) {
                $("#dwTestHour").val(ui.item.testhour);
                $("#szCourseName").val(ui.item.label);
                $("#dwCourseID").val(ui.item.id);

            });
            AutoUserByIdentClassTeacher($("#szTeacherName"), 2, $("#dwTeacherID"), null, null, null);
            $("#szTeacherName").on("autocompleteselect", function (event, ui) {
                $("#dwTeacherID").val(ui.item.id);
                $("#szTeacherName").val(ui.item.szTrueName);
                $("#szTeacherDeptName").text(ui.item.szDeptName);
            });
            $('#btnsetGroup').click(function () {
                var szGroupName=$("#classNameInfo").val();
                if (szGroupName == "")
                {
                    MessageBox("必须填写课程班名", "", 2);
                }
                else
                {
                    $("#divAddgroupMember").dialog("open");
                }
            });
            AutoUserByName($("#szTrueName"), 2, $("#accno"), null, null, null);
            $("#szTrueName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#accno").val(ui.item.id);
                    $("#szTrueName").val(ui.item.szTrueName);
                    $("#szLogonName").text(ui.item.szLogonName);
                }, 5);
            });
            var bNewTestItem = true;
            var tableLeft = $("#tableLeft");
            $("#btnNes").button().click(function () {
                bNewTestItem = true;
                $("#btnAddTestItem").val("新增");
                $("#szTestName").val("");
                $("#dwGroupPeopleNum").val("");
                $("#dwTestItemTestHour").val("");
                $("#szTestItemMemo").val("");
                $("#dwTestClass").val("");
                $("#dwTestKind").val("");
            });
            $("#OK,#btnAddUser,#btnAddClass").button();
            $("#btnAddClass").click(function () {
                var classID = $("#classID").val();
                var className = $("#className").val();
                var groupName = $("#classNameInfo").val();
                if (classID = "" || className == "")
                {
                    return;
                }
                $.get(
              "../../data/setClassGroupMember.aspx",
              { MemberID: classID, szName: className, KindID: 1, GroupName: groupName },
              function (data) {
                  if (data == "success") {
                      var vTr = $("<tr></tr>");
                      var vTd = $("<td memberid='" + classID + "' kind='" + 1 + "'>" + classID + "</td><td>" + className + "</td>");
                      var vTdOP = $("<td></td>");

                      var vInputButton = $("<input type='button' value='删除' />")
                      vInputButton.click(function () { DelMember(vInputButton, classID, null, 1); });
                      vTdOP.append(vInputButton);
                      vTr.append(vTd);
                      vTr.append(vTdOP);
                      tblContent.append(vTr);
                  }
                  else {
                      MessageBox(data, '提示', 3);
                  }
              });
            });
            function DelMember(obj, memberid, groupid, ukind) {
                var vtd = obj.parents("tr").children().first();
                var ukind = vtd.attr("kind");
                var memberid = vtd.attr("memberid");
                $.get(
              "../../data/delClassGroupMember.aspx",
              { MemberID: memberid, KindID: ukind },
              function (data) {
                  if (data == "success") {
                      obj.parents("tr").remove();
                  }
                  else {
                      MessageBox(data, '提示', 3);
                  }
              });
            }
            $("#btnAddUser").click(function () {
                var accno = $("#accno").val();
                var szTrueName = $("#szTrueName").val() + '(' + $("#szLogonName").val() + ')';
                var groupName = $("#classNameInfo").val();
                if (accno === "" || szTrueName == "")
                {
                    return;
                }
                $.get(
              "../../data/setClassGroupMember.aspx",
              { MemberID: accno, szName: szTrueName, KindID: 2, GroupName: groupName },
              function (data) {
                  if (data == "success") {
                      var vTr = $("<tr></tr>");
                      var vTd = $("<td  memberid='" + accno + "' kind='" + 2 + "'>" + accno + "</td><td>" + szTrueName + "</td>");
                      var vTdOP = $("<td></td>");

                      var vInputButton = $("<input type='button' value='删除' />")
                      vInputButton.click(function () { DelMember(vInputButton, classID, null, 1); });
                      vTdOP.append(vInputButton);
                      vTr.append(vTd);
                      vTr.append(vTdOP);
                      tblContent.append(vTr);
                  }
                  else {
                      MessageBox(data, '提示', 3);
                  }
              });
            });

            function fnSetItem(testItemID) {
                debugger;
                bNewTestItem = false;
                $("#btnAddTestItem").val("修改");
                $.get(
               "../../data/searchtestitem.aspx",
               { testItemID: testItemID },
               function (data) {
                   var testitemList = eval(data);
                   var testitem = testitemList[0];
                   $("#testitemid").val(testItemID);
                   $("#szTestName").val(testitem.szTestName);
                   $("#dwGroupPeopleNum").val(testitem.dwGroupPeopleNum);
                   $("#dwTestItemTestHour").val(testitem.dwTestHour);
                   $("#szTestItemMemo").val(testitem.szMemo);
                   $("#dwTestClass").val(testitem.dwTestClass);
                   $("#dwTestKind").val(testitem.dwTestKind);

               });
            }
            $("#btnAddTestItem").button().click(function () {
                if (bNewTestItem == true) {
                    newTestItem();
                }
                else {

                    setTestItem();
                }
            });

            function setTestItem() {
                debugger;
                var testitemid = $("#testitemid").val();
                var szTestName = $("#szTestName").val();
                var dwGroupPeopleNum = $("#dwGroupPeopleNum").val();
                var dwTestItemTestHour = $("#dwTestItemTestHour").val();
                var szTestItemMemo = $("#szTestItemMemo").val();
                var dwTestClass = $("#dwTestClass").val();
                var dwTestKind = $("#dwTestKind").val();
                $.get(
            "../../data/setItemAndCard.aspx",
            { testitemid: testitemid, szTestName: szTestName, dwGroupPeopleNum: dwGroupPeopleNum, dwTestItemTestHour: dwTestItemTestHour, szTestItemMemo: szTestItemMemo, dwTestClass: dwTestClass, dwTestKind: dwTestKind },
            function (data) {
                if (data.indexOf("错误") > -1) {
                    MessageBox(data, "", 2);
                }
                else {
                    if (szTestName.length > 5) {

                        $("#span" + testitemid).attr('title', '修改' + szTestName + '...');
                        $("#span" + testitemid).text('修改【' + szTestName.substring(0, 5) + '】');
                    }
                    else {
                        $("#span" + testitemid).attr('title', '修改' + szTestName + '...');
                        $("#span" + testitemid).text('修改【' + szTestName + '】');
                    }
                    MessageBox('修改成功', "", 1);
                }

            });
            }
            function newTestItem() {
                var testPlanID = $("#dwTestPlanID").val();
                var szTestName = $("#szTestName").val();
                var dwGroupPeopleNum = $("#dwGroupPeopleNum").val();
                var dwTestItemTestHour = $("#dwTestItemTestHour").val();
                var szTestItemMemo = $("#szTestItemMemo").val();
                var dwTestClass = $("#dwTestClass").val();
                var dwTestKind = $("#dwTestKind").val();
                var szTestPlanName = $("#dwTestKind").val();
                $.get(
               "../../data/NewItemAndCard.aspx",
               { testPlanID: testPlanID, szTestName: szTestName, dwGroupPeopleNum: dwGroupPeopleNum, dwTestItemTestHour: dwTestItemTestHour, szTestItemMemo: szTestItemMemo, dwTestClass: dwTestClass, dwTestKind: dwTestKind },
               function (data) {
                   if (data.indexOf("错误") > -1) {
                       MessageBox(data, "", 2);
                   }
                   else {
                       var delTR = $("<tr></tr>");
                       var delTD = $("<td></td>");
                       var del = $("<div id='div" + data.split(',')[0] + "' style='width:260px'></div>");
                       var delSpanName;
                       if (szTestName.length > 5) {
                           delSpanName = $("<span id='span" + data.split(',')[0] + "'  style='display:inline-block' class='ui-button-text' title='点击修改" + szTestName + "'>" + '修改【' + szTestName.substring(0, 5) + "..." + '】' + "</span>");
                       } else {
                           delSpanName = $("<span id='span" + data.split(',')[0] + "' style='display:inline-block' class='ui-button-text' title='" + szTestName + "'>" + '修改【' + szTestName + '】' + "</span>");
                       }
                       delSpanName.button();
                       delSpanName.click(function () {
                           fnSetItem(data.split(',')[0]);
                       });
                       var delSpanDel = $("<span style='display:inline-block' class='ui-button-text'>删除</span>");
                       delSpanDel.button();
                       delSpanDel.click(function () {
                           DelTestItem(data.split(',')[0]);
                       });
                       del.append(delSpanName);
                       del.append(delSpanDel);
                       delTD.append(del);
                       delTR.append(delTD);
                       tableLeft.append(delTR);
                       MessageBox('新增成功', "", 1);
                   }
               });

            }
            function DelTestItem(testItemID) {
                $.get(
                 "../../data/deltestitem.aspx",
                 { testItemID: testItemID },
                 function (data) {
                     if (data.indexOf("错误") > -1) {
                         MessageBox(data, "", 2);
                     }
                     else {
                         var vdiv = $("#div" + testItemID);
                         vdiv.remove();
                     }
                 }
                 );
            }
            $("#btnTestItemCancel").button().click(
                function () {
                    MessageBox("实验计划已设置成功", "提示", 3, function () { Dlg_OK() });
                });
            $("#Cancel").button().click(
         function () {
             //MessageBox("实验计划已设置成功", "提示", 3, function () { Dlg_OK() });
             Dlg_Cancel();
         });

            $("#OK").button().click(function () {
                var vStep = $("#Step").val();
                if (vStep == "1") {
                    NewTestPlan();
                }
            });
            function NewTestPlan() {
                var formData = $("#Form1").serialize();
                   $.get(
                     "../../data/newtestplan.aspx",
                     formData,
                     function (data) {
                         if (data.indexOf("错误") > -1) {
                             MessageBox(data, "", 2);
                         }
                         else {
                             if (data.indexOf("testitemid") > -1) {
                                 MessageBox("实验计划已设置成功", "提示", 3, function () { Dlg_OK() });
                             } else {
                                 $("#dwTestPlanID").val(data);
                                 $("#testPlanDiv").hide();
                                 $("#TestItemTemp").show();
                                 $("#dwTestPlanID").val(data);
                                 $("#testPlanDiv").hide();
                                 $("#TestItemTemp").show();
                             }
                         }

                     }
                   );
            }
        });
    </script>
</asp:Content>
