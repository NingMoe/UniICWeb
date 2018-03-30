<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewFeeList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input name="szDevID" id="szDevID" type="hidden" />
        <input name="szowner" id="szowner" type="hidden" />
        <div class="formtable">
            <table>
               <tr>
                   <th>收费金额:</th>
                    <td>
                        每小时<input id="dwUniFee" name="dwUniFee" style="width:30px" />元</td>
                 <th>收费时间：</th>
                    <td>
                        每<input id="dwUniTime" name="dwUniTime" style="width:30px" />分钟，计费一次</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="height:300px;overflow-y:scroll;">
                        <table class="ListTbl">
                            <tr>
                                <td style="width:60px;text-align:right">校区：</td>
                                <td style="text-align:left"><select style="width:130px"  id="szCamp" name="szCamp"><%=szCamp %></select></td>
                                <td style="width:60px;text-align:right">楼宇：</td>
                                <td style="text-align:left"><select style="width:130px" id="building" name="building"><%=szBuilding %></select></td>
                                <td style="width:60px;text-align:right">名称：</td>
                                <td style="text-align:left"><input  style="width:130px" type="text" name="szDevName" id="szDevName"/></td>
                      
                                <td><input style="width:100px" type="button" id="btnSearch" value="查询" /></td>
                            </tr>
                            <tr>
                                <table id="table" class="gridtable">
                                  
                                        <th style="width:20px"><input style="width:20px" type="checkbox" name="chkAll" id="chkAll" /></th>
                                        <th>名称</th>
                                        <th>楼宇</th>
                                        <th>校区</th>
                                    
                                      <tbody id="theadTable">
                              
                                          </tbody>
                                    
                                </table>
                            </tr>
                        </table>
                            </div>
                    </td>
                </tr>
               
            </table>
             <div style="text-align:center;width:99%">
                 
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
                </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
  <style type="text/css">
table.gridtable {
	font-family: verdana,arial,sans-serif;
	font-size:11px;
	color:#333333;
	border-width: 1px;
	border-color: #666666;
	border-collapse: collapse;
}
table.gridtable th {
	border-width: 1px;
	padding: 8px;
    text-align:center;
	border-style: solid;
	border-color: #666666;
	background-color: #dedede;
}
table.gridtable td {
	border-width: 1px;
    text-align:center;
	padding: 8px;
	border-style: solid;
	border-color: #666666;
	background-color: #ffffff;
}
</style>


    <script language="javascript" type="text/javascript">
        $(function () {
            //AutoDevice($("#szDevName"), 2, $("#szDevID"),null, null, null, null);
            $("#szStartDate,#szEndDate").datepicker({
            });
            $('#szStaName').val($("#dwStaSN").find("option:selected").text());
            $("#dwStaSN").change(function () {
                $("#szStaName").val($(this).find("option:selected").text());
            });
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            $("#btnSearch").button().click(function () {
                var szCamp1 = $("#szCamp").val();
                var building1 = $("#building").val();
                var szDevName1 = $("#szDevName").val();
                $.get(
           "../../data/searchSiteDevice.aspx",
            { szCamp: szCamp1, building: building1, szDevName: szDevName1 },
           function (data) {
               var vCamp = eval(data);
               $('#theadTable').empty();
               for (var i = 0; i < vCamp.length; i++) {
                   var v = vCamp[i];
                   var tr = $("<tr></tr>");
                   tr.append("<td>" + "<input style='width:20px' type='checkbox' name='devidchk' value='" + v.id + "' />" + "</td>");
                   tr.append("<td>" + v.label + "</td>");
                   tr.append("<td>" + v.szBuilding + "</td>");
                   tr.append("<td>" + v.szCamp + "</td>");
                   $('#theadTable').prepend(tr);

               }
               //$(".ListTbl").UniTable();
           }
         );
            });
            $("#chkAll").click(function () {
                var vThis = $("#chkAll");
                if (vThis.is(':checked')) {
                    $("[name='devidchk']").each(function () {
                        $(this).removeAttr("checked");
                        $(this).prop("checked", 'true');//选中所有奇数
                    });
                }
                else {
                    $("[name='devidchk']").each(function () {

                        $(this).removeAttr("checked");

                    });
                }
            });
            $("#szCamp").change(function () {
                var campidV=$("#szCamp").val();
                $.get(
           "../../data/searchBuilding.aspx",
           { campid: campidV },
           function (data) {

               var vCamp = eval(data);
               $('#building').empty();
               for (var i = 0; i < vCamp.length; i++) {
                   $('#building').append($("<option value='" + vCamp[i].id + "'>" + vCamp[i].label + "</option>"));
               }
           }
         );

            });
      
            $("#szLogonName").keyup(function () {
                var vlist = $("#szLogonName").val().split('，');
                var vlistE = $("#szLogonName").val().split(',');
                if (vlist.length < vlistE.length) {
                    vlist = vlistE;
                }
                var vRes = "";
                var vOwner = "";
                var i = 0;
                for (i = 0; i < vlist.length; i++) {
                    var vTemp = vlist[i];
                    $.get(
                         "../../data/searchaccount.aspx",
                         { Type: "logonname", term: vTemp },
                         function (data) {
                             var vJson = eval(data);
                             if (vJson[0] != null && vJson[0].szTrueName != null) {
                                 vRes = vRes + vJson[0].szTrueName + ";";
                                 vOwner = vOwner + vJson[0].id + ";";
                                 $("#szTrueName").html("");
                                 $("#szTrueName").html(vRes);
                                 $("#szowner").val();
                                 $("#szowner").val(vOwner);
                             }

                         }
                       );

                }

            });
        });
    </script>
</asp:Content>
