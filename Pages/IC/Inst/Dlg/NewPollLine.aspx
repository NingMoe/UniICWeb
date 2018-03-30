<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewPollLine.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwPollID" name="dwPollID" />
        <div id="itemDiag">
            ���ƣ�<input type="text" id="itemName" name="itemName" />
            <input type="button" id="itemAdd" value="����" />
        </div>
        <div class="formtable">
            <table>
                <tr>
                    <th>ͶƱ���⣺</th>
                    <td colspan="3"> <input id="szPollSubject" name="szPollSubject" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>��ʼ���ڣ�</th>
                    <td>
                        <input type="text" id="dwBeginDate" name="dwBeginDate" />
                    </td>
                    <th>�������ڣ�</th>
                    <td><input type="text" id="dwEndDate" name="dwEndDate" /></td>
                </tr>
                
              
                <tr>
                     <th>״̬��</th>
                    <td>
                        <select id="dwVoteStat" name="dwVoteStat">
                            <option value="1">δ����</option>
                            <option value="2">������</option>
                            <option value="4">�ѹر�</option>
                            </select>
                    </td>
                    <th>��ע��</th>
                    <td>
                        <input id="szMemo" name="szMemo" /></td>
                </tr>
            </table>
           
            <div style="text-align:center;width:99%;margin-top:10px;">
                    <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button>
            </div>
            <div style="margin:10px;">
                <hr width=100% size=1 color"=#bbbcbc" style="FILTER: alpha(opacity=100,finishopacity=0);"> 
            </div>
             <div>
                <input type="button" value="����ͶƱѡ��" id="addItem" style="width:100px" />
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
                            var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">ɾ��</a>";
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
                            var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">ɾ��</a>";
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
                        var vDel = "<a class='delA' aName=" + vTemp.label + " aid=" + vTemp.id + ">ɾ��</a>";
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
