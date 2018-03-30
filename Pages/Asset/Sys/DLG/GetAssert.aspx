<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetAssert.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
      <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwAttendantID" name="dwAttendantID" />
        <input type="hidden" id="szmemoid" name="szmemoid" />
        <input type="hidden" id="dwClassKind" name="dwClassKind" />
        <input type="hidden" id="devClassHtml" name="devClassHtml" value="<%=szDEVCLSHtml %>" />
        <div class="formtable">
            <table>
                <tr>
                    <th>资产编号(*)：</th>
                    <td><input id="szAssertSN" name="szAssertSN" class="validate[required]" value="<%=szDevSN %>" /></td>
                    <th>资产名称(*)：</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
              <tr>
                    <th>单价(元)：</th>
                    <td>
                        <input id="dwUnitPrice" name="dwUnitPrice" class="validate[validate[custom[integer]]"  /></td>
                    <th>采购日期：</th>
                    <td>
                        <input id="dwPurchaseDate" name="dwPurchaseDate" /></td>
                </tr>
                <tr>
                     <th>品牌型号：</th>
                    <td>
                      <input id="szModel" name="szModel" /></td>
                       <th>  <!--品牌：--></th>

                    <td>
                        <!--
                        <input id="szOriginSN" name="szOriginSN" />
                        -->
                    </td>
                </tr>
                <tr>
                    <th>资产类型(*)：</th>
                    <td>
                        <div id="dwClassID" name="dwClassID"><%=szDevCLS %></div>
                        </td>
                       <th>用途：</th>
                     <td>
                        <div id="szFuncCode" name="szFuncCode" ><%=szFunction %></div></td>
                </tr>
                  
                 <tr>
                     <th>所在实验室：</th>
                     <td colspan="1">
                         <div id="dwRoomID" name="dwRoomID"><%=szRoom %></div></td>
                       <th>借用方式：</th>
                    <td colspan="3"><div id="isLease"></div></td>
                </tr>
                 <tr>
                     <th>资金来源：</th>
                     <td><div id="szFrom"></div></td>
                     
                     <th>位置：</th>
                     <td><div id="szPostion"></div></td>
                </tr>
                <tr>
                  
                </tr>
                <tr>
                     <th>维保单位：</th>
                     <td><input type="text" name="szSpecification" id="szSpecification" /></td>
                     
                     <th style="width:120px">维保单位电话：</th>
                     <td><input type="text" name="szMemo" id="szMemo" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%if(szOp2=="set") {%>
                        <!--
                       <div style="width:700px;height:200px;overflow:scroll">
                           <%=szUrl %>
                       </div>
                           -->
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtable table th {
        width:80px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {

            var vIsLease = "可外借";
            var vIsNoLease = "不可外借";
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            $("input").attr("disable", "true");
            $("input").attr("readOnly", "true");
            $("input").css("border-style", "none");

            $("select").attr("disable", "true");
            $("select").attr("readOnly", "true");
            $("select").css("border-style", "none");
            $("select").css("width","140");
            $("select").css("overflow", "hidden");
            
            AutoUserByName($("#szAttendantName"), 2, $("#dwAttendantID"), null, $("#szAttendantTel"), null);
           // AutoUserByName($("#szMemo"), 2, $("#szmemoid"), null, $("#szAttendantTel"), null);
            AutoDevKind($("#szKindName"), 2, $("#dwKindID"), null, false);
            AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, null);
            $("#szKindName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    debugger;
                    var vClassKind = ui.item.dwClassKind;
                    if ((vClassKind & 4) > 0) {
                        $("#isLease").text(vIsLease);
                        $("#dwClassKind").val(4);
                    }
                    else {
                        $("#isLease").text(vIsNoLease);
                        $("#dwClassKind").val(1);
                    }
                    $("#dwKindID").val(ui.item.id);
                    $("#szKindName").val(ui.item.label);
                    $("#szModel").val(ui.item.szModel + ui.item.szSpecification);
                }, 10);
            });
            $("#dwRoomID").change(function () {
                getRoomDetail();
            });
            $("#dwClassID").change(function () {
                  var classID = $(this).val();
                var devClsssHtml = $("#devClassHtml").val();
                devClsssHtml = devClsssHtml.toString();
                var vClassOld = $("#dwClassKind").val();
                var classList = devClsssHtml.split(";");
                for (var i = 0; i < classList.length; i++)
                {
                    var devClass = classList[i].split(":");
                    if (devClass[0] == classID)
                    {
                        var kind = devClass[1];
                        if ((kind & 4) > 0) {
                            $("#isLease").text(vIsLease);
                            $("#dwClassKind").val(4);
                        }
                        else {
                            $("#isLease").text(vIsNoLease);
                            $("#dwClassKind").val(1);
                        }
                    }
                }
            });
            setTimeout(function () {
                var vClassKind = $("#dwClassKind").val();
                if ((vClassKind & 4) > 0) {
                    $("#isLease").text(vIsLease);
                }
                else {
                    $("#isLease").text(vIsNoLease);
                }
            }, 100);
            getRoomDetail();
            function getRoomDetail()
            {
                setTimeout(function () {
                    var roomid = $("#dwRoomID").val();
                    
                    $.ajax({
                        type: "GET",
                        url: "../../data/searchroomdetail.aspx?term=" + roomid,
                        dataType: "json",
                        success: function (object) {
                            var vList = eval(object);
                            var vVaule = vList[0];
                            $("#szFrom").text(vVaule.labFrom);
                            $("#szPostion").text(vVaule.postion);
                        }
                    });
                }, 100);
            }
            $("#dwPurchaseDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
