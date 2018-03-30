<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewAssertList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwAttendantID" name="dwAttendantID" />

        <div class="formtable">
            <table>
               
                <tr>
                    <th>资产号前缀：</th>
                    <td>
                        <input id="szPreName" name="szPreName" class="validate[required]" /></td>

                    <th style="width:150px;">资产号后缀起始编号：</th>
                    <td>
                        <input id="szNextName" name="szNextName" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
             
              
                    <th>后缀长度：</th>
                    <td>
                        <input id="dwNextLen" name="dwNextLen" class="validate[required,validate[custom[onlyNumber]]" style="width: 80px" />
                        <input type="checkbox" checked="checked" name="LenFix" id="LenFix" value="true" style="width: 20px" /><label style="width: 80px">长度是否固定</label>
                    </td>
                        <th>资产名：</th>
                    <td>
                        <input id="szPreDevName" name="szPreDevName" class="validate[required]" /></td>
                    <tr>
                         <th>资产类型：</th>
                    <td>
                        <select id="dwClassID" name="dwClassID"><%=szDevCLS %></select>
                        </td>
                    <th>新建数量：</th>
                    <td>
                        <input id="dwNum" name="dwNum" class="validate[required,validate[custom[onlyNumber]]" />
                    </td>
                        
                </tr>
                <tr>
                    <td colspan="4">
                        <hr size="2" />
                    </td>
                </tr>
                <!--
                <tr>
                  
                    <th>后缀长度：</th>
                    <td colspan="3">
                        <input id="dwNextDevLen" name="dwNextDevLen" class="validate[required,validate[custom[onlyNumber]]" style="width: 80px"  value="4" />
                        <input type="checkbox" name="DevLenFix" checked="checked" id="DevLenFix" value="true" style="width: 20px"  /><label style="width: 80px">长度是否固定</label>
                    </td>
                  
                </tr>
              -->
                <!--
                    <tr>
                      <th style="width:150px">资产名后缀起始编号：</th>
                    <td>
                        <input id="szNextDevName" name="szNextDevName" class="validate[required,validate[custom[onlyNumber]]" value="1" /></td>
                    </tr>-->
                <!--
                <tr>
                    <td colspan="4">
                        <hr size="2" />
                    </td>
                    </tr>
               
                    <tr>
                    <th style="width:120px">原厂序列号前缀：</th>
                    <td>
                        <input id="szPreOriginSN" name="szPreOriginSN"  /></td>

                    <th >原厂序列号后缀起始编号：</th>
                    <td>
                        <input id="szNextOriginSN" name="szNextOriginSN" /></td>
                </tr>
              
                  <tr>
                    <th>后缀长度：</th>
                    <td colspan="3">
                        <input id="dwOriginSNLen" name="dwOriginSNLen" style="width: 80px" />
                        <input type="checkbox" name="dwOriginSNLenFix" checked="checked" id="dwOriginSNLenFix" value="true" style="width: 20px" /><label style="width: 80px">长度是否固定</label>
                    </td>
                </tr>
                      -->
                <tr>
                    <td colspan="4">
                        <hr size="2" />
                    </td>
                    </tr>
                <tr>
                    <th>所属<%=ConfigConst.GCRoomName %>：</th>
                    <td>
                       <select id="dwRoomID" name="dwRoomID"><%=szRoom %></select></td>
                      <th>部门：</th>
                    <td>
                        <select name="dwdept" id="dwdept">
                            <%=szDept%>
                        </select>
                    </td>
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
                       <th>用途：</th>
                     <td>
                        <select id="szFuncCode" name="szFuncCode" ><%=szFunction %></select></td>
                      
                </tr>
                  <!--
                <tr>
                   
                     <th>品牌：</th>
                    <td>
                      <input id="szOriginSN" name="szOriginSN" />
                        
                    </td>
                </tr>
                -->
                 <tr>
                    <td colspan="4">
                        <hr size="2" />
                    </td>
                    </tr>
               <tr>
                     <th>制造商：</th>
                     <td colspan="2">
                         <input type="hidden" id="dwProducerID" name="dwProducerID" />
                         <input type="text" id="szProducerName" name="szProducerName" class="validate[required]" />
                     </td>
                </tr>
                <tr>
                     <th>维保单位：</th>
                     <td >
                          <input type="hidden" id="dwServiceID" name="dwServiceID" />
                         <input type="text" id="szServiceName" name="szServiceName" class="validate[required]" />

                     </td>
                     <th>销售商：</th>
                     <td >
                           <input type="hidden" id="dwSellerID" name="dwSellerID" />
                         <input type="text" id="szSellerName" name="szSellerName" class="validate[required]" />

                     </td>
                </tr>
                  <tr>
                    <td colspan="4">
                        <hr size="2" />
                    </td>
                    </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtable table th {
            width: 80px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoLab($("#szLabName"), 2, $("#dwLabID"), null, false);
            AutoCompany($("#szProducerName"), 2, $("#dwProducerID"), 1, false);
            AutoCompany($("#szServiceName"), 2, $("#dwServiceID"), 4, false);
            AutoCompany($("#szSellerName"), 2, $("#dwSellerID"), 2, false);
            $("#szLabName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#szLabName").val(ui.item.label);
                    $("#dwLabID").val(ui.item.id);
                    debugger;
                    AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, $("#dwLabID"));
                }, 10);
            });
           
            AutoDevKind($("#szKindName"), 2, $("#dwKindID"), null, false);
            AutoUserByName($("#szAttendantName"), 2, $("#dwAttendantID"), null, $("#szAttendantTel"), null);
            $("#szKindName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#dwKindID").val(ui.item.id);
                    $("#szKindName").val(ui.item.label);
                    $("#szModel").val(ui.item.szModel + ui.item.szSpecification);
                }, 10);
            });
            $("#dwPurchaseDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
