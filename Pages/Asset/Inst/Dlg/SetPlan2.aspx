<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPlan2.aspx.cs" Inherits="_Default" %>

<%--  һ���ύʵ����Ŀ��ɾ����������Ŀ���������޸���Ŀ --%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="IsSubmit" value="true" type="hidden"/>
        <input name="dwTestPlanID" value="" type="hidden"/>
        <input name="dwGroupID" value="" type="hidden"/>
        <input name="SetTestItem" value="" type="hidden"/>
        <input name="TestItemDelList" value="" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>ѧ�ڣ�</th>
                    <td><select id="dwYearTerm" name="dwYearTerm" class="validate[required]" >
                         <%if ((m_TermList & 1) != 0){ %><option value="1">��ѧ��</option><%} %>
                         <%if ((m_TermList & 2) != 0){ %><option value="0">��ѧ��</option><%} %>
                         <%if ((m_TermList & 4) != 0){ %><option value="2">��ѧ��</option><%} %>
                         </select>
                    </td>
                    <th>ʵ��ƻ����ƣ�</th>
                    <td><input id="szTestPlanName" name="szTestPlanName" class="validate[required]" />
                    </td>
                </tr>                
                <tr>
                    <th>����ѧ�ƣ�</th>
                    <td> <select id="szAcademicSubject" name="szAcademicSubject" class="validate[required]"><option value="ѧ��һ">ѧ��һ</option><option value="ѧ�ƶ�">ѧ�ƶ�</option></select></td>
                    <th>ʵ�������</th>
                    <td>
                        <select id="dwTesteeKind" name="dwTesteeKind" class="validate[required]" >
                            <option value="1">��ʿ��</option>
                            <option value="2">˶ʿ��</option>
                            <option value="3">������</option>
                            <option value="4">ר����</option>
                            <option value="5">����</option>
                        </select>
                    </td>
                </tr>                
                 <tr>
                    <th>�Ͽΰ༶��</th>
                    <td><div class="UISelect" data-id="GroupList" data-name="GroupListName" data-tip="����༶���ƣ�����ԤԼ�༶" data-source="../../Data/searchCls.aspx">
                        <input name="GroupList" value="<%=m_szGroupID %>" type="hidden"/>
                        <input name="GroupListName" value="<%=m_szGroupName %>" type="hidden"/>
                    </div>
                    </td>


                    <th>�γ̣�</th>
                    <td> <div class="UISelect" data-id="dwCourseID" data-name="szCourseName" class="validate[required]" data-tip="����γ�����" data-single="true" data-source="../../Data/searchCourse.aspx">
                            <input name="dwCourseID" type="hidden"/>
                            <input name="szCourseName" type="hidden"/>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <th>��ϸ������</th>
                    <td> <input id="szTestPlanURL" name="szTestPlanURL"  /></td>


                    <th>��ע��</th>
                    <td>
                        <input id="szMemo" name="szMemo"  /></td>
                </tr>
                 <tr>
                    <th>ʵ����Ŀ��</th>
                    <td colspan="3">
                        <a class="accNew">�����Ŀ</a>
                        <input id="ItemAllData" name="ItemAllData" type="hidden" />
                        <div id="TestItem"></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
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
                <tr><td>ʵ������</td><td><input name="szTestName" class="validate[required]" /></td></tr>
                <tr><td>ÿ������</td><td><input name="dwGroupPeopleNum" class="validate[required]" /></td></tr>
                <tr><td>ѧʱ</td><td><input name="dwTestHour" class="validate[required]" /></td></tr>
                <tr><td>��ע</td><td><input name="szMemo" /></td></tr>
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
                    pTestItemDelList.val(pTestItemDelList.val() + "," + testitemID+":"+testcardID);
                    
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
            
            $(".accDel").button({ icons: { primary: "ui-icon-trash" }, text: true }).click(fnDelItem);

            $(".accNew").button({ icons: { primary: "ui-icon-plusthick" }, text: true }).click(fnAddItem);
        });
    </script>
</asp:Content>
