<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewPrize.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwDevID" name="dwDevID" type="hidden" />

        <table>
            <tr>
                <th><%=szPrizeName %>名称：</th><td><input id="szRewardName" name="szRewardName" class="validate[required]" /></td>
                <th><%=szPrizeName %>级别：</th><td><select id="dwRewardLevel" name="dwRewardLevel"><%=szRewardLevel%></select></td>
            </tr>
             <tr>
                <th>科研项目：</th><td><select id="dwRTID" name="dwRTID"><%=szResearch %></select></td>
                    <th><%=szPrizeName %>类型：</th><td><select id="dwRewardType" name="dwRewardType"><%=szRewardType%></select></td>
            </tr>
           <tr>
               <th>仪器：</th><td colspan="3"><input type="text" id="szDevName" name="szDevName" /></td>
           </tr>
           
           
              
         
        </table>
          <div id="divCert" runat="server" > 
         <table>
              <tr>
                
                <th>证书编号：</th><td><input type="text" name="szCertID" id="szCertID" /> </td>
                <th>发证机关：</th><td><input type="text" name="szAuthOrg" id="szAuthOrg" /></td>
                     
            </tr>
         </table>
              </div>
     
         <table>
               <tr><td colspan="4" style="text-align:center"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
         </table>
             
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
      
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
      
        <%if(bSet){%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#szDevName").autocomplete({
            source: "../../data/searchdevice.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szDevName").val(ui.item.label);
                        $("#dwDevID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
               
                    ui.content.push({ label: " 未找到配置项 " });
                }
            }
        }).blur(function () {
            if ($("#dwDevID").val() == "") {
                $(this).val("");
            } else {
             
            }
        });

        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
