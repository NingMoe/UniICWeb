<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan3.aspx.cs" Inherits="_Default" %>

<%--  实时修改项目，使用TestItemAjax.aspx作后台操作 --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="Step" value="<%=(m_CreateOK||bSet)?1:0 %>" type="hidden"/>
        <input name="IsSubmit" value="true" type="hidden"/>
        <input name="dwTestPlanID"  type="hidden"/>
        <input name="SetTestItem"  type="hidden"/>
        <input name="TestItemDelList"  type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>学期：</th>
                    <td>
                    <%if (m_CreateOK){ %><%=m_TermText%><%}else{ %><select id="dwYearTerm" name="dwYearTerm" class="validate[required]" >
                         <%if ((m_TermList & 1) != 0){ %><option value="1">上学期</option><%} %>
                         <%if ((m_TermList & 2) != 0){ %><option value="0">本学期</option><%} %>
                         <%if ((m_TermList & 4) != 0){ %><option value="2">下学期</option><%} %>

                        </select>
                    <%} %>
                    </td>
                    <th>实验计划名称：</th>
                    <td><%if (m_CreateOK) {%><div class="input" name="szTestPlanName"></div><input name="szTestPlanName" type="hidden"/><% }else{ %><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" /><%} %>
                    </td>
                </tr>                
                <tr>
                    <th>所属学科：</th>
                    <td><%if (m_CreateOK) {%><div name="szAcademicSubject"></div><% }else{ %><select id="szAcademicSubject" name="szAcademicSubject" class="validate[required]"><option value="学科一">学科一</option><option value="学科二">学科二</option></select><%} %></td>
                    <th>实验者类别：</th>
                    <td>
                    <%if (m_CreateOK) {%><%=m_szTesteeKind %><% }else{ %>
                        <select id="dwTesteeKind" name="dwTesteeKind" class="validate[required]" >
                            <option value="1">博士生</option>
                            <option value="2">硕士生</option>
                            <option value="3">本科生</option>
                            <option value="4">专科生</option>
                            <option value="5">其它</option>
                        </select>
                        <%} %>
                    </td>
                </tr>                
                 <tr>
                    <th>上课班级：</th>
                    <td><%if (m_CreateOK) {%><%=m_szGroupName %><input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/><input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/><% }else{ %>
                    <div class="UISelect" data-id="GroupList" data-name="GroupListName" data-tip="输入班级名称，增加预约班级" data-source="../../Data/searchCls.aspx">
                        <input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/>
                        <input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/>
                    </div>
                        <%} %>
                    </td>


                    <th>课程：</th>
                    <td><%if (m_CreateOK) {%><div name="szCourseName"></div><input name="dwCourseID" type="hidden"/><input name="szCourseName" type="hidden"/><% }else{ %>
                        <div class="UISelect" data-id="dwCourseID" data-name="szCourseName" class="validate[required]" data-tip="输入课程名称" data-single="true" data-source="../../Data/searchCourse.aspx">
                            <input name="dwCourseID" type="hidden"/>
                            <input name="szCourseName" type="hidden"/>
                        </div>
                        <%} %>
                    </td>
                </tr>
                 <tr>
                    <th>详细描述：</th>
                    <td><%if (m_CreateOK) {%><div name="szTestPlanURL"></div><% }else{ %><input id="szTestPlanURL" name="szTestPlanURL"  /><%} %></td>


                    <th>备注：</th>
                    <td>
                        <%if (m_CreateOK) {%><div name="szMemo"></div><% }else{ %><input id="szMemo" name="szMemo"  /><%} %></td>
                </tr>
                <%if(m_CreateOK || Request["op"] == "set"){ %>
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
                        <%if(!m_CreateOK || bSet){ %><button type="submit" id="OK"><%=m_szOKBtnText %></button><%} %>
                        <button type="button" id="Cancel"><%=m_szCancelBtnText %></button></td>
                </tr>
                    </tbody>
            </table>
        </div>
    </form>
    
    <div id="TestItemTemp" class="hidden NotHttpValue">
        <div>
        <input name="dwTestItemID" type="hidden"/>
        <input name="dwTestCardID" type="hidden"/>
            <table>
                <tr><td>实验名称</td><td><input name="szTestName" class="validate[required]" /><div class="hidden szTestName"></div></td></tr>
                <tr><td>每组人数</td><td><input name="dwGroupPeopleNum" class="validate[required]" /></td></tr>
                <tr><td>学时</td><td><input name="dwTestHour" class="validate[required]" /></td></tr>
                <tr><td>备注</td><td><input name="szMemo" /></td></tr>
                <tr><td colspan="2" class="btnRow"><a class="accSave">保存</a></td></tr>
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
               // ShowWait();
                $("#OK").button("disable");
                $("#Cancel").button("disable");
                fnOK();
            });
            
            $("#Cancel").button().click(function(){
                $("#OK").button("disable");
                $("#Cancel").button("disable");
                
                <%if(m_CreateOK || bSet){ %>
                Dlg_OK();
                <%}else{ %>
                Dlg_Cancel();
                <%} %>
            });

            function fnNewItem(bNew,szName,value){
                if(szName == null)
                {
                    szName = "";
                }
                var newItemHead = $('<h3><div class="accHeadText">'+szName+'</div><div class="changeFlag"></div><div class="accHeadOP"><a class="accDel">删除</a></div></h3>');
                var newItem = $('<div><div class="content"></div></div>');
                
                var newTestItemTemp = $("#TestItemTemp").children().clone();
                newTestItemTemp.appendTo($("div.content", newItem));

                newItemHead.appendTo(Items);
                newItem.appendTo(Items);
                $(".accDel", newItemHead).button({ icons: { primary: "ui-icon-trash" }, text: true }).bind("click",fnDelItem);

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
                }else{
                    var szTestName = $("input[name='szTestName']", newItem).attr("type","hidden").val();
                    $("div.szTestName", newItem).removeClass("hidden").text(szTestName);
                }
                
                $("input", newItem).bind("change",function(){                    
                    fnMakeChgItem(newItem);
                });
                $("select", newItem).bind("change",function(){
                    fnMakeChgItem(newItem);
                });
                $(".accSave",newItem).button().bind("click",function(){                    
                    fnSaveItem(this,newItem);
                });
                
                return newItemHead;
            }

            function fnDelItem() {
                $(this).button("disable");
                var head = $(this).parents("h3");
                if (head.length == 1)
                {
                    if(head.hasClass("newTemp"))
                    {
                        var content = head.next();
                        Items.accordion("destroy");
                        head.remove();
                        content.remove();
                        head.empty();
                        content.empty();
                        Items.accordion({
                            collapsible: true, active: false, heightStyle: "content", header: "h3"
                        });
                    }else{
                        ConfirmBox("请确定要删除项目吗？",function(){
                            var content = head.next();
                            
                            var dwTestPlanID = $("input[name='dwTestPlanID']").val();
                            var testitemID = $("input[name='dwTestItemID']",content).val();
                            var testcardID = $("input[name='dwTestCardID']",content).val();
                            $.ajax({
                                type:'POST',
                                url:'TestItemAjax.aspx?op=del',
                                data:'dwTestItemID='+testitemID+'&dwTestCardID='+testcardID+'&dwTestPlanID='+dwTestPlanID,
                                async:false,
                                dataType:"html",
                                success:function(data){
                                    if(data == "OK")
                                    {                                
                                        Items.accordion("destroy");
                                        head.remove();
                                        content.remove();
                                        head.empty();
                                        content.empty();
                                        Items.accordion({
                                            collapsible: true, active: false, heightStyle: "content", header: "h3"
                                        });
                                    }
                                },
                                error:function(data){
                                    MessageBox("项目删除失败"+data);
                                }
                            });            
                        });
                    }
                }
                
                $(this).button("enable");
                
                return false;
            }
            
            function fnMakeChgItem(evdata) {
                var ItemHead = evdata.prev();
                $(".accSave",evdata).button("enable");
                
                if(!ItemHead.hasClass("newTemp"))
                {
                    var accHeadText = $(".accHeadText", ItemHead);
                
                    var changeFlag = $(".changeFlag", ItemHead);
                    if(changeFlag.length == 0)
                    {
                        $("<div class='changeFlag'>*</div>").insertAfter(accHeadText);
                    }else{
                        changeFlag.text("*");
                    }
                }
            }
            
            function fnSaveItem(pThis,evdata) {
                var szTestName = $("input[name='szTestName']", evdata).val();
                if(szTestName == null || szTestName == "")
                {
                    MessageBox("实验名称不能为空");
                    return;
                }
                $(pThis).button("disable");
                
                var FormTestItemTemp = $("#FormTestItemTemp");
                FormTestItemTemp.empty();
                $("div.content",evdata).clone().appendTo(FormTestItemTemp);
                
                var sd = FormTestItemTemp.serialize();
                FormTestItemTemp.empty();
                var dwTestPlanID = $("input[name='dwTestPlanID']").val();
                
                $.ajax({
                    type:'POST',
                    url:'TestItemAjax.aspx?op=set&dwTestPlanID='+dwTestPlanID,
                    data:sd,
                    async:false,
                    dataType:"html",
                    success:function(data){
                        if(data == "OK")
                        {
                            
                            var szTestName = $("input[name='szTestName']", evdata).attr("type","hidden").val();
                            $("div.szTestName", evdata).removeClass("hidden").text(szTestName);
                            
                            var ItemHead = evdata.prev();
                            if(ItemHead.hasClass("newTemp"))
                            {
                                ItemHead.removeClass("newTemp");
                            }
                            
                            var accHeadText = $(".accHeadText", ItemHead).text(szTestName);
                            var changeFlag = $(".changeFlag", ItemHead);
                            if(changeFlag.length == 0)
                            {
                                $("<div class='changeFlag'></div>").insertAfter(accHeadText);
                            }else{
                                changeFlag.text("");
                            }
                            MessageBox("项目保存成功","项目保存成功",3);
                        }else{
                            MessageBox("项目保存失败"+data,"项目保存失败", 2);
                            $(pThis).button("enable");
                        }
                    },
                    error:function(data){
                        MessageBox("项目保存失败"+data,"项目保存失败", 2);
                        $(pThis).button("enable");
                    }
                });                
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
            
            $(".accDel").button({ icons: { primary: "ui-icon-trash" }, text: true }).bind("click",fnDelItem);

            $(".accNew").button({ icons: { primary: "ui-icon-plusthick" }, text: true }).click(fnAddItem);
        });
    </script>
</asp:Content>
