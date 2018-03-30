<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan1.aspx.cs" Inherits="_Default" %>

<%--  一起提交实验项目，实验项目全部删除，全部新建 --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle">手动添加实验计划</div>
        <input name="IsSubmit" value="true" type="hidden"/>
        <input name="dwTestPlanID" value="" type="hidden"/>
        <input name="dwGroupID" value="" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>学期：</th>
                    <td><select id="dwYearTerm" name="dwYearTerm" class="validate[required]" >
                         <option value="1">上学期</option><option value="0">本学期</option><option value="2">下学期</option>
                        </select>
                    </td>
                    <th>实验计划名称：</th>
                    <td><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" />
                    </td>
                </tr>                
                <tr>
                    <th>所属学科：</th>
                    <td> <select id="szAcademicSubject" name="szAcademicSubject" class="validate[required]"><option value="学科一">学科一</option><option value="学科二">学科二</option></select></td>
                    <th>实验者类别：</th>
                    <td>
                        <select id="dwTesteeKind" name="dwTesteeKind" class="validate[required]" >
                            <option value="1">博士生</option>
                            <option value="2">硕士生</option>
                            <option value="3">本科生</option>
                            <option value="4">专科生</option>
                            <option value="5">其它</option>
                        </select>
                    </td>
                </tr>                
                 <tr>
                    <th>上课班级：</th>
                    <td><div class="UISelect" data-id="GroupList" data-name="GroupListName" data-tip="输入班级名称，增加预约班级" data-source="../../Data/searchCls.aspx">
                        <input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/>
                        <input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/>
                    </div>
                    </td>


                    <th>课程：</th>
                    <td> <div class="UISelect" data-id="dwCourseID" data-name="szCourseName" class="validate[required]" data-tip="输入课程名称" data-single="true" data-source="../../Data/searchCourse.aspx">
                            <input name="dwCourseID" type="hidden"/>
                            <input name="szCourseName" type="hidden"/>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <th>详细描述：</th>
                    <td> <input id="szTestPlanURL" name="szTestPlanURL"  /></td>


                    <th>备注：</th>
                    <td>
                        <input id="szMemo" name="szMemo"  /></td>
                </tr>
                 <tr>
                    <th>实验项目：</th>
                    <td colspan="3">
                        <a class="accNew">添加项目</a>
                        <input id="ItemAllData" name="ItemAllData" type="hidden" />
                        <div id="TestItem"></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
                    </tbody>
            </table>
        </div>
    </form>
    
    <div id="TestItemTemp" class="hidden NotHttpValue">
        <div>
            <table>
                <tr><td>实验名称</td><td><input name="szTestName" /></td></tr>
                <tr><td>每组人数</td><td><input name="dwGroupPeopleNum" /></td></tr>
                <tr><td>学时</td><td><input name="dwTestHour" /></td></tr>
                <tr><td>备注</td><td><input name="szMemo" /></td></tr>
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
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            var Items = $("#TestItem");
            function fnOK() {

                var itemdata = "";
                var itemindex = 0;
                var contents = $("div.content", Items);
                var FormTestItemTemp = $("#FormTestItemTemp");
                contents.each(function () {
                    FormTestItemTemp.empty();
                    $(this).clone().appendTo(FormTestItemTemp);
                    var sd = FormTestItemTemp.serialize();
                    itemdata += "&ItemData" + itemindex + "=" + escape(sd);
                    itemindex++;
                });
                itemdata += "&ItemDataCount=" + itemindex;

                $("#ItemAllData").val(itemdata);
                return true;
            }

            $("#dwEnrollDeadline").datepicker();
            
            $("#OK").button().click(function(){
                ShowWait();
                $("#OK").button("disable");
                $("#Cancel").button("disable");
                fnOK();
            });
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
                var newItemHead = $('<h3><div class="accHeadText">'+szName+'</div><div class="accHeadOP"><a class="accDel">删除</a></div></h3>');
                var newItem = $('<div><div class="content"></div></div>');
                
                var newTestItemTemp = $("#TestItemTemp").children().clone();
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
                //$("input[name='szTestName']", newItem).bind("change",function(){fnSaveItem(newItem);});
                $("input", newItem).bind("change",function(){fnSaveItem(newItem);});
                $("select", newItem).bind("change",function(){fnSaveItem(newItem);});
                return newItemHead;
            }

            function fnDelItem() {
                $("input[name='SetTestItem']").val("true");                               

                Items.accordion("destroy");
                var head = $(this).parents("h3");
                if (head.length == 1) {
                    var content = head.next();
                    head.remove();
                    content.remove();
                    head.empty();
                    content.empty();
                }
                Items.accordion({
                    collapsible: true, active: false, heightStyle: "content", header: "h3"
                });

                $(".newTemp").click();
            }

            function fnSaveItem(evdata) {
                var ItemHead = evdata.prev();
                if(ItemHead.hasClass("newTemp"))
                {
                    ItemHead.removeClass("newTemp");
                }

                $("input[name='SetTestItem']").val("true");

                $(".accHeadText", ItemHead).text(
                    $("input[name='szTestName']", evdata).val()
                );
            }

            function fnAddItem() {
                var newTemp = $(".newTemp");
                if (newTemp.length > 0) {
                    return;
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
