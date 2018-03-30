<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ManGroupList.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div> 
    <div class="formtable">
        <table>
            <tr>
                <th>管理员名单：</th>
                <td colspan="3">
                    <div class="UISelect" data-id="reserved.RoomGroup" data-name="reserved.RoomGroupName"  data-tip="输入姓名" data-source="../../Data/searchAccount.aspx?dwIdent=268435456">
                        <input name="reserved.RoomGroup" type="hidden"/>
                        <input name="reserved.RoomGroupName" type="hidden"/>
                    </div>
                </td>
                
            </tr>
            <tr><td colspan="4" class="btnRow"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" >
   $(function () {        
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
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
            $("#reserved.RoomGroup").autocomplete({
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#dwAccNo").val(ui.item.id);
                            $("#szTrueName").text(ui.item.label);
                            $("#szHandPhone").text(ui.item.szHandPhone);
                            $("#szTel").text(ui.item.szTel);
                            $("#szEmail").text(ui.item.szEmail);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {
                        $("#dwAccNo").val("");
                        $("#szTrueName").text("");
                        $("#szHandPhone").text("");
                        $("#szTel").text("");
                        $("#szEmail").text("");
                        ui.content.push({ label: " 未找到配置项 " });
                    }

                }
            }).blur(function () {
                if ($("#dwAccNo").val() == "") {
                    $(this).val("");
                } else {
                    $(this).val($("#szLogonName").val());
                }
            });
       
        $(".UISelect").UISelect();
    });
</script>
</asp:Content>
