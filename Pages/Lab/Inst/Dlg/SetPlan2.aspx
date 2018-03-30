<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan2.aspx.cs" Inherits="_Default" %>

<%--  一起提交实验项目，删除不存在项目，新增或修改项目 --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="IsSubmit" value="true" type="hidden"/>
        <input name="dwTestPlanID" id="dwTestPlanID" value="" type="hidden"/>
       <input name="dwCourseID" id="dwCourseID" type="hidden" />
        <input name="dwTeacherID" id="dwTeacherID" type="hidden" />
          <input name="dwGroupIDTemp" id="dwGroupIDTemp" type="hidden" />
        <input type="hidden" id="classID" />
        <input type="hidden" id="accno" />
        <input name="SetTestItem" value="" type="hidden"/>
        <input name="TestItemDelList" value="" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>学期：</th>
                    <td colspan="3"><select id="dwYearTerm" name="dwYearTerm">
                         <%=m_TermList %>
                         </select>
                    </td>
                    <!--
                    <th>实验计划名称：</th>
                    <td><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" />
                    </td>
                    -->
                </tr>
                     <tr>
                        <th>教师姓名*:</th>
                        <td>
                            <input type="text" id="szTeacherName" name="szTeacherName" /></td>
                        <th>教师所在部门:</th>
                        <td>
                            <label id="szTeacherDeptName"><%=szTeacherDeptName %></label>
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
                 <tr>
                       <th>课程*：</th>
                        <td>
                            <input name="szCourseName" type="text" id="szCourseName" />
                        </td>
                        <th>实验学时数*:</th>
                        <td>
                            <input type="text" id="dwTotalTestHour2" name="dwTotalTestHour2" /></td>
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
                    <% if(ConfigConst.GCscheduleMode==2) { %>
                   
                      <%} %>
                 <tr>
                    <th>实验项目：</th>
                    <td colspan="3">
                        <a class="accNew">添加项目</a>
                        <input id="ItemAllData" name="ItemAllData" type="hidden" />
                        <div id="TestItem"></div>
                    </td>
                </tr>
                    <% if(ConfigConst.GCscheduleMode==2) { %>
                 
                      <%} %>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">关闭</button></td>
                </tr>
                    </tbody>
            </table>
        </div>
    </form>
    
    <div id="TestItemTemp" class="hidden NotHttpValue">
        <div>
        <input name="dwTestItemID" type="hidden"/>
        <input name="dwTestCardID" type="hidden"/>
            <table class="tbItem">
                <tr>
                    <td>实验名称</td><td><input type="text" name="szTestName" class="validate[required]" /></td>
                    <td>每组人数</td><td><input type="text" name="dwGroupPeopleNum" class="validate[required]" /></td></tr>
                  <tr><td>实验类别</td>
                            <td>
                                <select name="dwTestClass" class="dwTestClass">
                                    <option value="1">基础</option>
                                    <option value="2">专业基础</option>
                                    <option value="3">专业</option>
                                    <option value="4">其他</option>
                                </select>
                            </td>
                            <td>实验类型</td>
                            <td>
                                <select name="dwTestKind" class="dwTestKind">
                                    <option value="1">演示性</option>
                                    <option value="2">验证性</option>
                                    <option value="3">综合性</option>
                                    <option value="4">研究设计</option>
                                    <option value="5">其他</option>
                                </select>
                            </td>
                        </tr>
                <tr>
                    <td>学时</td><td><input type="text" name="dwTestHour" class="validate[required]" /></td>
                    <td>备注</td><td><input type="text" name="szMemo" /></td></tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="button" class="btnSetItem" value="修改" /></td>
                </tr>
            </table>
        </div>
    </div>
    <form id="FormTestItemTemp" class="hidden" action="#">
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ui-accordion .ui-accordion-header {
            padding-top:2px !important;
            padding-bottom:2px !important;
            height:25px;
        }
        .ui-accordion h3.ui-accordion-header {
            background: #bdd9f2;
        }
        
        .ui-accordion h3.newTemp
        {
            background: #f2d9bd;
        }
        
        .accHeadText {
            float:left;
            line-height:25px;
            margin-right:10px;
        }
        .accHeadOP {
            float:right;
        }

        .tblBtn {
            text-align:center;
        }
        
        .changeFlag
        {
            display:inline;
            float:left;
            font-size: 10px;
        }
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            setTimeout(function () {
                debugger;
                AddGroupClass();
              
            }, 400);
            setTimeout(function(){
              
            },500);
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
                     var vVauleInfo = vList[i];
                     $("<option value='" + vList[i].id + "'>" + vList[i].label + "</option>").appendTo($("#dwGroupID"));

                     $("#dwGroupID").val($("#dwGroupIDTemp").val());
                 }
             });
            }
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
            $(".btnSetItem").button();
           
            var Items = $("#TestItem");
            function SetTestItem(myobj)
            {
                debugger;
                var head = myobj.parents("h3");
               
                var vTableTest= myobj.parents("table .tbItem");
                var varTestPlanID= $("#dwTestPlanID").val();
                var varTestitem = vTableTest.siblings("input[name='dwTestItemID']");
                var varTestcard= vTableTest.siblings("input[name='dwTestCardID']");
                var varszTestName=$("input[name='szTestName']",vTableTest).val();
                var vardwGroupPeopleNum=$("input[name='dwGroupPeopleNum']",vTableTest).val();
                var vardwTestClass=$(".dwTestClass",vTableTest).val();
                var vardwTestKind=$(".dwTestKind",vTableTest).val();
                var vardwTestHour=$("input[name='dwTestHour']",vTableTest).val();
                var varszMemo=$("input[name='szMemo']",vTableTest).val();
                if (varTestitem.length == 1&&varTestcard.length==1) {
                    var varTestitemID = varTestitem.val();
                    var varTestcardID = varTestcard.val();
                    if(varTestitemID!=""&&varTestcardID!="")
                    {
                        $.get(
               "../../data/SetItemAndCard.aspx",
               { testitemid: varTestitemID, dwTestPlanID: varTestcardID, szTestName: varszTestName, dwGroupPeopleNum: vardwGroupPeopleNum,dwTestClass:vardwTestClass,dwTestKind:vardwTestKind,dwTestItemTestHour:vardwTestHour,szTestItemMemo:varszMemo },
               function (data) {
                   if (data.indexOf("错误") > -1) {
                       MessageBox(data, "", 2);
                   }
                   else {
                       $(".accHeadText", head).text(varszTestName);
                       MessageBox('修改成功', "", 1);
                   }
               });}else
                    {
                        var varTestcardID = varTestcard.val();
                        $.get(
               "../../data/newItemAndCard.aspx",
               { testPlanID: varTestPlanID, szTestName: varszTestName, dwGroupPeopleNum: vardwGroupPeopleNum,dwTestClass:vardwTestClass,dwTestKind:vardwTestKind,dwTestItemTestHour:vardwTestHour,szTestItemMemo:varszMemo },
               function (data) {
                   if (data.indexOf("错误") > -1) {
                       MessageBox(data, "", 2);
                   }
                   else {
                       $(".accHeadText", head).text(varszTestName);
                       MessageBox('添加成功', "", 1);
                       Items.accordion({
                           collapsible: true, active: false, heightStyle: "content", header: "h3"
                       });

                       $(".newTemp").click();
                   }
               });
                }
                
                }
            }
            $("#dwEnrollDeadline").datepicker();
            
            $("#OK").button();
            
            $("#Cancel").button().click(Dlg_Cancel);

            var tbl = $(".formtable table.ListTbl2");
            tbl.find(">tbody>tr:even").addClass("tblEven");
            tbl.find(">tbody>tr:odd").addClass("tblOdd");

            $(".UISelect").UISelect();
            function fnNewItem(bNew,szName,value){
                if(szName == null)
                {
                    szName = "";
                }
                var newItemHead = $('<h3><div class="accHeadText">'+szName+'</div><div class="changeFlag"></div><div class="accHeadOP"><a class="accDel">删除</a></div></h3>');
                var newItem = $('<div><div class="content"></div></div>');
                
                var newTestItemTemp = $("#TestItemTemp").children().clone();//内容
                if(bNew)
                {
                    $(".btnSetItem", newTestItemTemp).val("添加项目");
                }
                    $(".btnSetItem", newTestItemTemp).bind("click",function(){
                        SetTestItem($(this));
                    });
                
                newTestItemTemp.appendTo($("div.content", newItem));

                newItemHead.appendTo(Items);
                newItem.appendTo(Items);
                $(".accDel", newItemHead).button({ icons: { primary: "ui-icon-trash" }, text: true }).click(fnDelItem);
                                
                if(value)
                {
                    PutHttpValue(value, newTestItemTemp);
                    $(".accHeadText", newItemHead).text(
                        $("input[name='szTestName']", newItem).val()
                    );
                }
                
                if(bNew)
                {
                    newItemHead.addClass("newTemp");                    
                }
                /*//暂时取消
                $("input", newItem).bind("change",function(){
                    if($(this).attr("name") == "szTestName")
                    {
                        var testitemID = $("input[name='dwTestItemID']",newItem).val();
                        var testcardID = $("input[name='dwTestCardID']",newItem).val();
                        var pTestItemDelList = $("input[name='TestItemDelList']");
                        pTestItemDelList.val(pTestItemDelList.val() + "," + testitemID+":"+testcardID);
                        $("input[name='dwTestItemID']", newItem).val("");
                        $("input[name='dwTestCardID']", newItem).val("");
                        
                        if(newItemHead.hasClass("newTemp"))
                        {
                            newItemHead.removeClass("newTemp");
                        }
                    }
                    fnSaveItem(newItem);
                });
                $("select", newItem).bind("change",function(){
                    fnSaveItem(newItem);
                });
                */
                return newItemHead;
            }

            function fnDelItem() {
                $("input[name='SetTestItem']").val("true");
                
                var pTestItemDelList = $("input[name='TestItemDelList']");

                Items.accordion("destroy");
                var head = $(this).parents("h3");
                if (head.length == 1) {
                    var content = head.next();
                    
                    var testitemID = $("input[name='dwTestItemID']",content).val();
                    var testcardID = $("input[name='dwTestCardID']",content).val();
                    $.get(
        "../../data/deltestitem.aspx",
        { testItemID: testitemID},
        function (data) {
            if (data.indexOf("错误") > -1) {
                MessageBox(data, "", 2);
            }
            else {
                head.remove();
                content.remove();
                head.empty();
                content.empty();
                Items.accordion({
                    collapsible: true, active: false, heightStyle: "content", header: "h3"
                });

                $(".newTemp").click();
            }
        });
                }
                   
            }

            function fnSaveItem(evdata) {
                /*
                var ItemHead = evdata.prev();
                
                $("input[name='SetTestItem']").val("true");

                if(!ItemHead.hasClass("newTemp"))
                {
                    var accHeadText = $(".accHeadText", ItemHead).text(
                        $("input[name='szTestName']", evdata).val()
                    );
                }
                
                var changeFlag = $(".changeFlag", ItemHead);
                if(changeFlag.length == 0)
                {
                    $("<div class='changeFlag'>*</div>").insertAfter(accHeadText);
                }else{
                    changeFlag.text("*");
                }
                */
            }

            function fnAddItem() {
                var newTemp = $(".newTemp");
                if (newTemp.length > 0) {
                  //  return;
                }

                Items.accordion("destroy");
                
                var newItem = fnNewItem(true,"【新建项目】",null);
            
                Items.accordion({
                    collapsible: true, active: false, heightStyle: "content", header: "h3"
                });
                
                newItem.click();
            }
            
            function fnInitTestItem()
            {
                Items.empty();
                var ItemInitData = <%=m_szTestItemJSData %>;
                for(var i = 0; i < ItemInitData.length; i++)
                {
                    if(ItemInitData[i])
                    {
                        fnNewItem(false,"",ItemInitData[i]);
                    }
                }
                
                Items.accordion({
                    collapsible: true, active: false, heightStyle: "content", header: "h3"
                });
            }
            setTimeout(fnInitTestItem,1);
            
            $(".accDel").button({ icons: { primary: "ui-icon-trash" }, text: true }).click(fnDelItem);

            $(".accNew").button({ icons: { primary: "ui-icon-plusthick" }, text: true }).click(fnAddItem);
        });
    </script>
</asp:Content>
