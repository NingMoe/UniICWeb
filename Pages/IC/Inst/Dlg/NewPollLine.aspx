<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewPollLine.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwPollID" name="dwPollID" />
        <div id="itemDiag">
            名称：<input type="text" id="itemName" name="itemName" />
            <input type="button" id="itemAdd" value="新增" />
        </div>
        <div class="formtable">
            <table>
                <tr>
                    <th>投票主题：</th>
                    <td colspan="3"> <input id="szPollSubject" name="szPollSubject" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>开始日期：</th>
                    <td>
                        <input type="text" id="dwBeginDate" name="dwBeginDate" />
                    </td>
                    <th>结束日期：</th>
                    <td><input type="text" id="dwEndDate" name="dwEndDate" /></td>
                </tr>
                
              
                <tr>
                     <th>状态：</th>
                    <td>
                        <select id="dwVoteStat" name="dwVoteStat">
                            <option value="1">未开放</option>
                            <option value="2">开放中</option>
                            <option value="4">已关闭</option>
                            </select>
                    </td>
                    <th>备注：</th>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
                </tr>
            </table>
           
            <div style="text-align:center;width:99%;margin-top:10px;">
                    <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
            </div>
            <div style="margin:10px;">
                <hr width=100% size=1 color"=#bbbcbc" style="FILTER: alpha(opacity=100,finishopacity=0);"> 
            </div>
             <div>
                <input type="button" value="增加投票选项" id="addItem" style="width:100px" />
                </div>
            <div style="margin-top:10px">
                <table id="tableItem" class="ListTbl">
                    </table>
            </div>
        </div>
        
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
       
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#dwBeginDate,#dwEndDate").datepicker();
            $("#dwOwnerDept").change(function () {
                $("#szDeptName").val($(this).find("option:selected").text());
            });
            $("#itemDiag").dialog({
                autoOpen: false,
                height: 100,
                width: 300,
                modal: true,
                show: {
                    effect: "blind",
                    duration: 500
                },
                hide: {
                    effect: "blind",
                    duration: 500
                }
            });
            $("#addItem").click(function () {
                $("#itemDiag").dialog("open");
            });
            $("#OK,#addItem,#itemAdd").button();
            $("#itemAdd").click(function () {
                var vValue = $("#itemName").val();
                $.ajax({
                    type: 'GET',
                    url: '../../data/AddPollItem.aspx',
                    data: { name: vValue },
                    dataType: 'json',
                    timeout: 3000,
                    success: function (data) {
                        $("#itemDiag").dialog("close");
                        var item = eval(data);
                        var vTable = $("#tableItem");
                        vTable.empty();
                        for (var i = 0; i < item.length; i++) {
                            var vTemp = item[i];
                            var vTr = $("<tr></tr>");
                            var vTd = $("<td>" + vTemp.label + "</td>");
                            var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">删除</a>";
                            var vTdDel = $("<td></td>");
                            vTdDel.append(vDel);

                            vTr.append(vTd);
                            vTr.append(vTdDel);
                            vTable.append(vTr);
                        }
                    },
                    error: function (xhr, type) {

                    }
                });
            });
            var vTatble = $("#tableItem");
            
            $("#tableItem").on("click", ".delA", function () {
                debugger;
                var vthis = $(this);
                var vid = vthis.attr("aid");
                var vName = vthis.attr("aName");
                $.ajax({
                    type: 'GET',
                    url: '../../data/AddPollItem.aspx?op=del',
                    data: { name: vName, id: vid },
                    dataType: 'json',
                    timeout: 3000,
                    success: function (data) {
                        $("#itemDiag").dialog("close");
                        var item = eval(data);
                        var vTable = $("#tableItem");
                        vTable.empty();
                        for (var i = 0; i < item.length; i++) {
                            var vTemp = item[i];
                            var vTr = $("<tr></tr>");
                            var vTd = $("<td>" + vTemp.label + "</td>");
                            var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">删除</a>";
                            var vTdDel = $("<td></td>");
                            vTdDel.append(vDel);

                            vTr.append(vTd);
                            vTr.append(vTdDel);
                            vTable.append(vTr);
                        }
                    },
                    error: function (xhr, type) {

                    }
                });
            });
        $("#Cancel").button().click(Dlg_Cancel);
      
        setTimeout(function () {
            $.ajax({
                type: 'GET',
                url: '../../data/AddPollItem.aspx?op=get',
                data: {},
                dataType: 'json',
                timeout: 3000,
                success: function (data) {
                    $("#itemDiag").dialog("close");
                    var item = eval(data);
                    var vTable = $("#tableItem");
                    vTable.empty();
                    for (var i = 0; i < item.length; i++) {
                        var vTemp = item[i];
                        var vTr = $("<tr></tr>");
                        var vTd = $("<td>" + vTemp.label + "</td>");
                        var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">删除</a>";
                        var vTdDel = $("<td></td>");
                        vTdDel.append(vDel);

                        vTr.append(vTd);
                        vTr.append(vTdDel);
                        vTable.append(vTr);
                    }
                },
                error: function (xhr, type) {

                }
            });
        }, 1);
    });
    </script>
</asp:Content>
