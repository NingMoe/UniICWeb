<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAdmin.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  <input type="hidden" id="dwProperty" name="dwProperty" />
    <input type="hidden" id="dwAccNo" name="dwAccNo" />    
    <input type="hidden" id="hiddenRoomID" name="hiddenRoomID" />    
    <input type="hidden" id="szLogonName" name="szLogonName" />    
    <input type="hidden" id="hidenManrole" name="hidenManrole" runat="server" />      
    <div class="formtable">

        <table>
            <tr>
                <th>工号：</th><td style="text-align:left;vertical-align:top"><div id="szLogonNamePut" /></td>
                <th>角色：</th><td style="text-align:left"><select id="dwManRole" name="dwManRole"><%=szManRole %></select></td>
              </tr>   
             <tr>
                   <th>姓名：</th><td  style="text-align:left"><div id="szTrueName"></div></td>
                 <th>手机：</th><td style="text-align:left"><input type="text" id="szHandPhone" name="szHandPhone"  class="validate[required,validate[custom[handphone]]"/></td></tr>
                 
           <tr><th>电子邮件：</th><td style="text-align:left"><input type="text" id="szEmail" name="szEmail" /></td>
               <th>电话：</th><td style="text-align:left"><input type="text" id="szTel" name="szTel" /></td>
           </tr>
           <tr>
                <th>备注：</th>
                <td colspan="3"><input type="text" name="szMemo" id="szMemo" /></td>
            </tr>
            <tr>
                    <td colspan="4"><div id="checkLab"><a style="color:blue;text-decoration:underline" id="selectLab">全选<%=ConfigConst.GCLabName %></a>
                        <a style="color:blue;text-decoration:underline" id="selectLabBack">反选<%=ConfigConst.GCLabName %></a><br />
                        管理<%=ConfigConst.GCLabName %>：<%=m_checkLab %></div></td>
                </tr> 
            <tr>
                  
                    <td colspan="4"><div id="radLab">管理<%=ConfigConst.GCRoomName %>：<%=m_szLab %></div></td>
                </tr> 
                <tr>
                    <td colspan="4"><div id="divRoom">
                        <a style="color:blue;text-decoration:underline" id="selectRoom">全选<%=ConfigConst.GCRoomName %></a>
                        <a style="color:blue;text-decoration:underline" id="selectRoomBack">反选<%=ConfigConst.GCRoomName %></a>
                        <br /><%=m_szRoom %></div></td>
                </tr>
            <tr><td colspan="4" class="btnRow"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
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
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {        
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        var vManRole = $("#<%=hidenManrole.ClientID%>").val();
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
        $("#selectLab").click(function () {
            $("input[name='labCheckList']").each(function () {
                $(this).prop("checked", true);
            });
        });
        $("#selectRoom").click(function () {
            $("input[name='roomID']").each(function () {
                $(this).prop("checked", true);
            });
        });
        $("#selectLabBack").click(function () {
            $("input[name='labCheckList']").each(function () {
                if ($(this).prop("checked") == true) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        });
        $("#selectRoomBack").click(function () {
            $("input[name='roomID']").each(function () {
                if ($(this).prop("checked") == true) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });
        });
        function SetManagerArea(vManRole) {
                var vdivcheckLab = $("#checkLab");
                var vdivradLab = $("#radLab");
                var vdivRoom = $("#divRoom");
                if (vManRole == 132097) {
                    vdivcheckLab.hide();
                    vdivradLab.hide();
                    vdivRoom.hide();
                }
                else if (vManRole == 524801) {
                    vdivcheckLab.show();
                    vdivradLab.hide();
                    vdivRoom.hide();
                }
                else if (vManRole == 1049105) {
                    vdivradLab.show();
                    vdivcheckLab.hide();
                    vdivRoom.show();
                }
                else if (vManRole == 2049) {
                    vdivradLab.show();
                    vdivcheckLab.hide();
                    vdivRoom.show();
                }
        }
        $("#radLab").on("click","input[name='labList']",function () {
            var labid = $(this).val();
            $(".labClass").each(function () {
                var obj = $(this);
                if (obj.attr("id") == "divLab" + labid) {
                    obj.show();
                }
                else { obj.hide();}
            });
           
        });
        $(".formtable label").each(function () {
            var obj = $(this);
            var str = obj.html();
            var text=str.substring(str.lastIndexOf(">") + 1, str.length - 1)
            if (text.length > 8) {
                obj.attr("title",text);
                var vCheck = $(str.substring(0, str.lastIndexOf(">") + 1));
                vCheck.css("enum");
                obj.empty();
                obj.append(vCheck);
                obj.append(text.substring(0, 8) + '..');
                $(document).tooltip();
            }
            else {

            }
            $(document).tooltip();
        });
        setTimeout(function () {
           
            var dateNow = new Date();
            var Month=dateNow.getMonth() + 1;
            if(Month<10)
            {
                Month="0"+Month;
            }
            var date=dateNow.getDate();
            if(date<10)
            {
                date="0"+date;
            }
            var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
            $("#dwEndDate").val(dateNowFor);
            $("#dwBeginDate").val(dateNowFor);
        }, 1);     
        AutoUserByName($("#szTrueName"), 2, $("#szHandPhone"), $("#szTel"), $("#szEmail"), $("#dwAccNo"));
    });
</script>
</asp:Content>
