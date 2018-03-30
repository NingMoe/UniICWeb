<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAdmin.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
    <input type="hidden" id="dwAccNo" name="dwAccNo" />    
    <input type="hidden" id="hiddenRoomID" name="hiddenRoomID" />    
    <input type="hidden" id="szLogonName" name="szLogonName" />    
    <input type="hidden" id="hidenManrole" name="hidenManrole" runat="server" />      
            <input type="hidden" id="hiddenRoomIDTemp" name="hiddenRoomIDTemp" />
    <div class="formtable">

        <table>
            <tr>
                <th>姓名：</th><td colspan="3" style="text-align:left;vertical-align:top"><div id="szLogonNamePut" /></td>
               
              </tr>   
             <tr>
                   <th>姓名：</th><td  style="text-align:left"><div id="szTrueName"></div></td>
                 <th>手机：</th><td style="text-align:left"><input type="text" id="szHandPhone" name="szHandPhone"  class="validate[custom[handphone]"/></td></tr>
                 
           <tr><th>电子邮件：</th><td style="text-align:left"><input type="text" id="szEmail" name="szEmail" /></td>
               <th>电话：</th><td style="text-align:left"><input type="text" id="szTel" name="szTel" /></td>
           </tr>
              <tr>
                    <th>管理员类型：</th>
                    <td>
                       <%=m_adminKind %></td>
                    <th>管理员级别：</th>
                    <td>
                      <select id="dwManLevel" name="dwManLevel"><%=m_adminLevle %></select></td>
                </tr>
           <tr>
                <th>备注：</th>
                <td colspan="3"><input type="text" name="szMemo" id="szMemo" /></td>
            </tr>
            <tr>
                <td colspan="4">
                      <div style="height:270px;overflow-y:scroll;">
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
            <!--
            <tr>
                    <td colspan="4"><div id="checkLab">管理<%=ConfigConst.GCLabName %>：<%=m_checkLab %></div></td>
                </tr> 
            <tr>
                  
                    <td colspan="4"><div id="radLab">管理<%=ConfigConst.GCLabName %>：<%=m_szLab %></div></td>
                </tr> 
                <tr>
                    <td colspan="4"><div id="divRoom"><%=m_szRoom %></div></td>
                </tr>
            -->
            <tr><td colspan="4" style="text-align:center" class="btnRow">
                <div style="text-align:center;margin:0px auto">
                <button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button>
                    </div>
                    </td></tr>
        </table>
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable table th {
        width:80px;
        }
        .formtable table tr td label{     
        }
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
<script language="javascript" type="text/javascript" >
   $(function () {        
       $(function () {


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
                  tr.append("<td>" + "<input style='width:20px' type='checkbox' name='devidchk' value='" + v.szManGroupID + "' />" + "</td>");
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
                       var vVal = $(this).val();
                       $(this).removeAttr("checked");
                       $(this).prop("checked", 'true');//选中所有奇数
                       SetChkClickValue('add', vVal);
                   });
               }
               else {
                   $("[name='devidchk']").each(function () {
                       var vVal = $(this).val();
                       $(this).removeAttr("checked");
                       SetChkClickValue('del', vVal);
                   });
               }
               
           });
           function SetChkValue() {
               var vKindsVal = $("#hiddenRoomIDTemp").val();
               $("[name='devidchk']").each(function () {
                   var vCheckValue = "," + $(this).val() + ",";
                   if (vKindsVal.indexOf(vCheckValue) > -1) {
                       $(this).removeAttr("checked");
                       $(this).prop("checked", 'true');
                   }
                   else {
                       $(this).removeAttr("checked");
                   }
               });
           }
           $(document).on("click", ".gridtable [name='devidchk']", function () {
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
               var vKindsVal = $("#hiddenRoomIDTemp").val();
               val = "," + val + ",";
               if (type == 'add') {
                   if (!(vKindsVal.indexOf(val) > -1)) {
                       vKindsVal = vKindsVal + val;
                       $("#hiddenRoomIDTemp").val(vKindsVal);
                   }
               }
               else if (type == 'del') {
                   if (vKindsVal.indexOf(val) > -1) {
                       vKindsVal = vKindsVal.replace(vTemp, "");
                       $("#hiddenRoomIDTemp").val(vKindsVal);
                   }
               }

           }



           $("#OK").button();
           $("#Cancel").button().click(Dlg_Cancel);
           var vManRole = 524816;
           SetManagerArea(vManRole);
           $("#dwManRole").change(function () {
               SetManagerArea($("#dwManRole").val());
           });
           $(".labClass").each(function () {
               $(this).hide();
               $("input[name='labList']:checked").each(function () {
                   var obj = $(this);
                   $("#divLab" + obj.val()).show();
               });
           });

           function SetManagerArea(vManRole) {
               //vManRole = 524801;
               var vdivcheckLab = $("#checkLab");
               var vdivradLab = $("#radLab");
               var vdivRoom = $("#divRoom");
               if (vManRole == 132097) {
                   vdivcheckLab.hide();
                   vdivradLab.hide();
                   vdivRoom.hide();
               }
               else if (vManRole == 524816) {
                   vdivcheckLab.show();
                   vdivradLab.hide();
                   vdivRoom.hide();
               }
               else if (vManRole == 1049089) {
                   vdivradLab.show();
                   vdivcheckLab.hide();
                   vdivRoom.show();
               }
           }
           $("input[name='labList']").click(function () {
               var labid = $(this).val();
               $(".labClass").each(function () {
                   var obj = $(this);
                   if (obj.attr("id") == "divLab" + labid) {
                       obj.show();
                   }
                   else { obj.hide(); }
               });

           });

           setTimeout(function () {
               var dateNow = new Date();
               var Month = dateNow.getMonth() + 1;
               if (Month < 10) {
                   Month = "0" + Month;
               }
               var date = dateNow.getDate();
               if (date < 10) {
                   date = "0" + date;
               }
               var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
               $("#dwEndDate").val(dateNowFor);
               $("#dwBeginDate").val(dateNowFor);
           }, 1);
           AutoUserByName($("#szTrueName"), 2, $("#szHandPhone"), $("#szTel"), $("#szEmail"), $("#dwAccNo"));
           $("#szTrueName").on("autocompleteselect", function (event, ui) {
               setTimeout(function () {
                   $("#szTrueName").val(ui.item.szTrueName);
                   $("#szHandPhone").val(ui.item.szHandPhone);
                   $("#szTel").val(ui.item.szTel);
                   $("#szEmail").val(ui.item.szEmail);
                   $("#dwAccNo").val(ui.item.id);
                   $("#szLogonName").val(ui.item.szLogonName);
               }, 10);
           });
       });
    });
</script>
</asp:Content>
