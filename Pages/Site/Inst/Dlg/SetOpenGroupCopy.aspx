<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetOpenGroupCopy.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
          <input type="hidden" id="dwAccNo" name="dwAccNo" />    
        <input type="hidden" id="dwGroupID" name="dwGroupID" />    
    <input type="hidden" id="hiddenRoomID" name="hiddenRoomID" />    
    <input type="hidden" id="szLogonName" name="szLogonName" />    
    <input type="hidden" id="hidenManrole" name="hidenManrole" runat="server" />      
            <input type="hidden" id="hiddenRoomIDTemp" name="hiddenRoomIDTemp" />
        <div class="formtable">
            <table>
                <tr>
                    <td colspan="4" class="tblBtn">
                         <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
                    </td>
                </tr>
                    <tr>
                <td colspan="4">
                      <div style="height:270px;overflow-y:scroll;">
                      <table class="ListTbl UniTable">
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
                                <table id="table" class="ListTbl UniTable">
                                  
                                       <th style="width:20px"><input style="width:20px" type="checkbox" name="chkAll" id="chkAll" /></th>
                                        <th style="text-align:center">名称</th>
                                        <th style="text-align:center">楼宇</th>
                                        <th style="text-align:center">校区</th>
                                    
                                      <tbody id="theadTable">
                              
                                          </tbody>
                                    
                                </table>
                            </tr>
                        </table>
                            </div>
                </td>
            </tr>
            </table>
           
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #gallery {
            float: left;
            width: 100%;
            min-height: 12em;
        }

        .gallery.custom-state-active {
            background: #eee;
        }
        #btnAddRole {
            color:blue;
            text-decoration:underline;
        }
        .gallery li {
            float: left;
            width: 96px;
            padding: 0.4em;
            margin: 0 0.4em 0.4em 0;
            text-align: center;
        }

            .gallery li h5 {
                margin: 0 0 0.4em;
                cursor: move;
            }

            .gallery li a {
                float: right;
            }

                .gallery li a.ui-icon-zoomin {
                    float: left;
                }

            .gallery li img {
                width: 100%;
                cursor: move;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#Cancel").button().click(Dlg_Cancel);
            $("#btnSearch").button().click(function () {
                var szCamp1 = $("#szCamp").val();
                var building1 = $("#building").val();
                var szDevName1 = $("#szDevName").val();
                $.get(
           "../../data/searchSiteDevice.aspx?room=true",
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
        });
    </script>
</asp:Content>
