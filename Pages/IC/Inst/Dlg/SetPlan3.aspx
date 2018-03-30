<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan3.aspx.cs" Inherits="_Default" %>

<%--  ʵʱ�޸���Ŀ��ʹ��TestItemAjax.aspx����̨���� --%>

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
                    <th>ѧ�ڣ�</th>
                    <td>
                    <%if (m_CreateOK){ %><%=m_TermText%><%}else{ %><select id="dwYearTerm" name="dwYearTerm" class="validate[required]" >
                         <%if ((m_TermList & 1) != 0){ %><option value="1">��ѧ��</option><%} %>
                         <%if ((m_TermList & 2) != 0){ %><option value="0">��ѧ��</option><%} %>
                         <%if ((m_TermList & 4) != 0){ %><option value="2">��ѧ��</option><%} %>
                        </select>
                    <%} %>
                    </td>
                    <th>ʵ��ƻ����ƣ�</th>
                    <td><%if (m_CreateOK) {%><div class="input" name="szTestPlanName"></div><input name="szTestPlanName" type="hidden"/><% }else{ %><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" /><%} %>
                    </td>
                </tr>                
                <tr>
                    <th>����ѧ�ƣ�</th>
                    <td><%if (m_CreateOK) {%><div name="szAcademicSubject"></div><% }else{ %><select id="szAcademicSubject" name="szAcademicSubject" class="validate[required]"><option value="ѧ��һ">ѧ��һ</option><option value="ѧ�ƶ�">ѧ�ƶ�</option></select><%} %></td>
                    <th>ʵ�������</th>
                    <td>
                    <%if (m_CreateOK) {%><%=m_szTesteeKind %><% }else{ %>
                        <select id="dwTesteeKind" name="dwTesteeKind" class="validate[required]" >
                            <option value="1">��ʿ��</option>
                            <option value="2">˶ʿ��</option>
                            <option value="3">������</option>
                            <option value="4">ר����</option>
                            <option value="5">����</option>
                        </select>
                        <%} %>
                    </td>
                </tr>                
                 <tr>
                    <th>�Ͽΰ༶��</th>
                    <td><%if (m_CreateOK) {%><%=m_szGroupName %><input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/><input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/><% }else{ %>
                    <div class="UISelect" data-id="GroupList" data-name="GroupListName" data-tip="����༶���ƣ�����ԤԼ�༶" data-source="../../Data/searchCls.aspx">
                        <input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/>
                        <input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/>
                    </div>
                        <%} %>
                    </td>


                    <th>�γ̣�</th>
                    <td><%if (m_CreateOK) {%><div name="szCourseName"></div><input name="dwCourseID" type="hidden"/><input name="szCourseName" type="hidden"/><% }else{ %>
                        <div class="UISelect" data-id="dwCourseID" data-name="szCourseName" class="validate[required]" data-tip="����γ�����" data-single="true" data-source="../../Data/searchCourse.aspx">
                            <input name="dwCourseID" type="hidden"/>
                            <input name="szCourseName" type="hidden"/>
                        </div>
                        <%} %>
                    </td>
                </tr>
                 <tr>
                    <th>��ϸ������</th>
                    <td><%if (m_CreateOK) {%><div name="szTestPlanURL"></div><% }else{ %><input id="szTestPlanURL" name="szTestPlanURL"  /><%} %></td>


                    <th>��ע��</th>
                    <td>
                        <%if (m_CreateOK) {%><div name="szMemo"></div><% }else{ %><input id="szMemo" name="szMemo"  /><%} %></td>
                </tr>
                <%if(m_CreateOK || Request["op"] == "set"){ %>
                 <tr>
                    <th>ʵ����Ŀ��</th>
                    <td colspan="3">
                        <a class="accNew">�����Ŀ</a>
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
                <tr><td>ʵ������</td><td><input name="szTestName" class="validate[required]" /><div class="hidden szTestName"></div></td></tr>
                <tr><td>ÿ������</td><td><input name="dwGroupPeopleNum" class="validate[required]" /></td></tr>
                <tr><td>ѧʱ</td><td><input name="dwTestHour" class="validate[required]" /></td></tr>
                <tr><td>��ע</td><td><input name="szMemo" /></td></tr>
                <tr><td colspan="2" class="btnRow"><a class="accSave">����</a></td></tr>
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
                ShowWait();
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
                var newItemHead = $('<h3><div class="accHeadText">'+szName+'</div><div class="changeFlag"></div><div class="accHeadOP"><a class="accDel">ɾ��</a></div></h3>');
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
                        ConfirmBox("��ȷ��Ҫɾ����Ŀ��",function(){
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
                                    MessageBox("��Ŀɾ��ʧ��"+data);
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
                    MessageBox("ʵ�����Ʋ���Ϊ��");
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
                            MessageBox("��Ŀ����ɹ�","��Ŀ����ɹ�",3);
                        }else{
                            MessageBox("��Ŀ����ʧ��"+data,"��Ŀ����ʧ��", 2);
                            $(pThis).button("enable");
                        }
                    },
                    error:function(data){
                        MessageBox("��Ŀ����ʧ��"+data,"��Ŀ����ʧ��", 2);
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
            
            $(".accDel").button({ icons: { primary: "ui-icon-trash" }, text: true }).bind("click",fnDelItem);

            $(".accNew").button({ icons: { primary: "ui-icon-plusthick" }, text: true }).click(fnAddItem);
        });
    </script>
</asp:Content>
