<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan2Open.aspx.cs" Inherits="_Default" %>
<%--  һ���ύʵ����Ŀ��ɾ����������Ŀ���������޸���Ŀ --%>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="IsSubmit" value="true" type="hidden"/>
        <input name="dwTestPlanID" id="dwTestPlanID" value="" type="hidden"/>
       <input name="dwCourseID" id="dwCourseID" type="hidden" />
        <input name="dwTeacherID" id="dwTeacherID" type="hidden" />
          <input name="dwGroupIDTemp" id="dwGroupIDTemp" type="hidden" />
        <input name="dwGroupID" id="dwGroupID" type="hidden" />
        <input type="hidden" id="classID" />
        <input type="hidden" id="accno" />
        <input name="SetTestItem" value="" type="hidden"/>
        <input name="TestItemDelList" value="" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>ѧ�ڣ�</th>
                    <td colspan="3"><select id="dwYearTerm" name="dwYearTerm">
                         <%=m_TermList %>
                         </select>
                    </td>
                    <!--
                    <th>ʵ��ƻ����ƣ�</th>
                    <td><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" />
                    </td>
                    -->
                </tr>
                     <tr>
                        <th>��ʦ����*:</th>
                        <td>
                            <input type="text" id="szTeacherName" name="szTeacherName" /></td>
                        <th>��ʦ���ڲ���:</th>
                        <td>
                            <label id="szTeacherDeptName"><%=szTeacherDeptName %></label>
                        </td>

                    </tr>                
                <tr>
                    <th>����ѧ�ƣ�</th>
                        <td>
                            <select id="szAcademicSubject" name="szAcademicSubject">
                                <%=szAcademicSubject %>
                            </select>
                        </td>
                        <th>ʵ�������</th>
                        <td>

                            <select id="dwTesteeKind" name="dwTesteeKind">
                                <%=szTesteeKind %>
                            </select>
                        </td>
                </tr>                
                 <tr>
                       <th>�γ�*��</th>
                        <td>
                            <input name="szCourseName" type="text" id="szCourseName" />
                        </td>
                        <th>ʵ��ѧʱ��*:</th>
                        <td>
                            <input type="text" id="dwTotalTestHour2" name="dwTotalTestHour2" /></td>
                </tr>
            
                    <% if(ConfigConst.GCscheduleMode==2) { %>
                    <!--
                      <%} %>
                 <tr>
                    <th>ʵ����Ŀ��</th>
                    <td colspan="3">
                        <a class="accNew">�����Ŀ</a>
                        <input id="ItemAllData" name="ItemAllData" type="hidden" />
                        <div id="TestItem"></div>
                    </td>
                </tr>
                    <% if(ConfigConst.GCscheduleMode==2) { %>
                   -->
                      <%} %>
                      <tr>
<th>
    ��౨������:
</th>
                        <td><input type="text" id="dwMaxUsers" name="dwMaxUsers" /></td>
<th>
    ��ֹ��������:
</th>
                        <td><input type="text" id="dwEnrollDeadline" name="dwEnrollDeadline" /></td>

                    </tr>
                    <tr>
<th>
    ����״̬:
</th>
                        <td colspan="3"><select id="dwStatus" name="dwStatus"><%=szStatus %></select></td>
                    </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">�ر�</button></td>
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
                    <td>ʵ������</td><td><input type="text" name="szTestName" class="validate[required]" /></td>
                    <td>ÿ������</td><td><input type="text" name="dwGroupPeopleNum" class="validate[required]" /></td></tr>
                  <tr><td>ʵ�����</td>
                            <td>
                                <select name="dwTestClass" class="dwTestClass">
                                    <option value="1">����</option>
                                    <option value="2">רҵ����</option>
                                    <option value="3">רҵ</option>
                                    <option value="4">����</option>
                                </select>
                            </td>
                            <td>ʵ������</td>
                            <td>
                                <select name="dwTestKind" class="dwTestKind">
                                    <option value="1">��ʾ��</option>
                                    <option value="2">��֤��</option>
                                    <option value="3">�ۺ���</option>
                                    <option value="4">�о����</option>
                                    <option value="5">����</option>
                                </select>
                            </td>
                        </tr>
                <tr>
                    <td>ѧʱ</td><td><input type="text" name="dwTestHour" class="validate[required]" /></td>
                    <td>��ע</td><td><input type="text" name="szMemo" /></td></tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="button" class="btnSetItem" value="�޸�" /></td>
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
                AddGroupClass();
              
            }, 400);
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
                   if (data.indexOf("����") > -1) {
                       MessageBox(data, "", 2);
                   }
                   else {
                       $(".accHeadText", head).text(varszTestName);
                       MessageBox('�޸ĳɹ�', "", 1);
                   }
               });}else
                    {
                        var varTestcardID = varTestcard.val();
                        $.get(
               "../../data/newItemAndCard.aspx",
               { testPlanID: varTestPlanID, szTestName: varszTestName, dwGroupPeopleNum: vardwGroupPeopleNum,dwTestClass:vardwTestClass,dwTestKind:vardwTestKind,dwTestItemTestHour:vardwTestHour,szTestItemMemo:varszMemo },
               function (data) {
                   if (data.indexOf("����") > -1) {
                       MessageBox(data, "", 2);
                   }
                   else {
                       $(".accHeadText", head).text(varszTestName);
                       MessageBox('��ӳɹ�', "", 1);
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
                var newItemHead = $('<h3><div class="accHeadText">'+szName+'</div><div class="changeFlag"></div><div class="accHeadOP"><a class="accDel">ɾ��</a></div></h3>');
                var newItem = $('<div><div class="content"></div></div>');
                
                var newTestItemTemp = $("#TestItemTemp").children().clone();//����
                if(bNew)
                {
                    $(".btnSetItem", newTestItemTemp).val("�����Ŀ");
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
                /*//��ʱȡ��
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
            if (data.indexOf("����") > -1) {
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
                
                var newItem = fnNewItem(true,"���½���Ŀ��",null);
            
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
