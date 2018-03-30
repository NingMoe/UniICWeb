<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewYardActivityp.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwDeptID" name="dwDeptID" type="hidden" />
             <input id="dwGroupID" name="dwGroupID" type="hidden" value="0" />
            <input id="dwResvPurpose" name="dwResvPurpose" type="hidden" value="11" />
            <input type="hidden" value="dwActivitySN" name="dwActivitySN" />
            <input type="hidden" id="kinds" name="kinds" />
            <input type="hidden" id="kindsTemp" name="kindsTemp" />
            <table>
                <tr>
                    <th>名称：</th>
                    <td>
                        <input type="text" id="szActivityName" name="szActivityName" class="validate[required]" /></td>
                   <th>申请模板：</th>
                    <td>
                    <select id="dwKindModul" name="dwKindModul">
                        <%=szSiteAppleModel %>
                    </select>   
                    </td>
                   
                </tr>
              <tr>
                       <th style="width:100px;">待审核类型：</th>
                    <td colspan="3">
                      <%=m_szCheckType %>
                    </td>
                </tr>
                 <tr>
                       <th>可提供服务：</th>
                    <td colspan="3">
                      <%=m_szCheckSerType %>
                    </td>
                </tr>
                 <tr>
                       <th>申请条件：</th>
                    <td colspan="3">
                      <%=m_szYardActivityMemo %>
                    </td>
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
            AutoDevKind($("#szDevKindName"), 2, $("#dwDevKind"), null, true);

        <%if (bSet)
          {%>
       
            <%}%>
            AutoDept($("#szDeptName"),2,$("#dwDeptID"),false);
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
          setTimeout(function () {
              SetChkValue();
          }, 100);

          $("#btnSearch").button().click(function () {
              var szCamp1 = $("#szCamp").val();
              var building1 = $("#building").val();
              var szDevName1 = $("#szDevName").val();
                  $.get(
             "../../data/searchSiteDevice.aspx?type=kind",
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
                 SetChkValue();
             }
           );
          });
            $("#szCamp").change(function () {
                var campidV = $("#szCamp").val();
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
            $("#chkAll").click(function () {
                var vThis = $("#chkAll");
                if (vThis.is(':checked')) {
                    $("[name='devidchk']").each(function () {
                        $(this).removeAttr("checked");
                        $(this).prop("checked", 'true');//选中所有奇数
                        SetChkClickValue('add', $(this).val());
                    });
                }
                else {
                    $("[name='devidchk']").each(function () {
                        $(this).removeAttr("checked");
                        SetChkClickValue('del', $(this).val());
                    });
                }
            });
            function SetChkValue()
            {
                debugger;
                var vKindsVal = $("#kindsTemp").val();
                $("[name='devidchk']").each(function () {
                    var vCheckValue = "," + $(this).val() + ",";
                    if (vKindsVal.indexOf(vCheckValue) > -1) {
                        $(this).removeAttr("checked");
                        $(this).prop("checked", 'true');//选中所有奇数
                        
                    }
                    else {
                        $(this).removeAttr("checked");
                       
                    }
                });
            }
                $(document).on( "click", ".gridtable [name='devidchk']", function() {
                var vThis = $(this);
                var vVal = $(this).val();
                if (vThis.is(':checked')) {
                    SetChkClickValue('add', vVal);
                }
                else {
                    SetChkClickValue('del', vVal);
                }
               
            });
                function SetChkClickValue(type, val) {
                    var vTemp = val;
                    var vKindsVal = $("#kindsTemp").val();
                    val = "," + val + ",";
                    if (type == 'add') {
                        if (!(vKindsVal.indexOf(val) > -1)) {
                            vKindsVal = vKindsVal + val;
                            $("#kindsTemp").val(vKindsVal);
                        }
                    }
                    else if (type == 'del') {
                        if (vKindsVal.indexOf(val) > -1) {
                            vKindsVal = vKindsVal.replace(vTemp, "");
                            $("#kindsTemp").val(vKindsVal);
                        }
                    }

                }
    });
    </script>
</asp:Content>
