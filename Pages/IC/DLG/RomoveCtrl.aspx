<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="RomoveCtrl.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  <input type="hidden" id="selectID" name="selectID" />
    <div class="formtable">
        <table>
           <tr>
                 <td style="text-align:right">
                   <div id="roomList">
                   勾选<%=ConfigConst.GCRoomName %>:
                 </div>
               </td>
               <td> <div id="roomListb">
                    <%=m_szRoom %>
                     </div>
               </td>
           </tr>
        </table>
        <div style="width:80%;margin:0px auto;" >
            勾选<%=ConfigConst.GCDevName %>:<a href="#" style="color:blue;text-decoration:underline" id="aSelect" select="false">全选</a>
            <div id="szDevList"><%=m_szDev %></div>
        </div>
        <div style="width:80%;margin:0px auto;" >
           <div style="width:80px;margin:0px auto; color:red;" id="selectNum"> 已选中【0】</div>
        </div>
        
        <div style="margin:0px auto;">
            <div style="width:200px;margin:10px auto;">
            <button type="submit" id="OK">发送<%=m_Title %></button><button type="button" id="Cancel">关闭窗口</button>
        </div>
          
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
      
        input, select {
            width: 200px;
        }
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        $("#roomListb").buttonset();
        setTimeout(function () {
           
        }, 1);
        $("#Cancel").button().click(Dlg_Cancel);
        $("#aSelect").click(function (event) {
            var ahref = $(this);
            var bSelect = ahref.attr("select");
            if (bSelect == "true") {
                ahref.text("全选");
                ahref.attr("select", "false");
                $("#szDevList input:checkbox").each(function (){
                    var ocheck = $(this)[0];
                    if (ocheck.checked == true) {
                        ocheck.checked = false;
                        RomoveVale($(this).val());
                    }
                    else {
                        ocheck.checked = true;
                        AddValue($(this).val());
                    }
                   
                });
            }
            else {
                ahref.text("反选");
                ahref.attr("select", "true");
                $("#szDevList input:checkbox").each(function () {     
                    var ocheck = $(this)[0];
                    ocheck.checked = true;
                    AddValue($(this).val());
                });
                
            }
            
           
        });
        $("#szDevList input:checkbox").click(function () {
            var oCheck = $(this)[0];
            if (oCheck.checked == true) {
                AddValue($(this).val());
            }
            else {
                RomoveVale($(this).val());
            }
        });
        $("#roomList,input:radio").change(function (event) {
            var roomID = ($(event.target).attr("id"));
            var valueList = $("#selectID").val();
            ShowWait();
            $.ajax({
                type: "post",
                url: "../data/GetDevByRoomID.aspx?roomid=" + roomID,
                dataType:"json",
                success: function (data) {
                    //var arr = $(data).toArray();
                    $("#szDevList").empty();
                    var szRes = "";
                    for (i = 0; i < data.length; i++)
                    {
                        var obj = data[i];
                        var szChecked = "";
                        $("#aSelect").text("反选");
                        $("#aSelect").attr("select","true");
                        if (valueList.indexOf(obj.id) > -1)
                        {
                            szChecked = "checked=true";
                        }
                       
                        szRes += "<label><input class=\"enum\" type=\"checkbox\" " + szChecked + " name=\"" + "devID" + "\" value=\"" + obj.id + "\" /> " + obj.name + "</label>,";
                    }
                    $("#szDevList").append("" + szRes);
                    $("#szDevList input:checkbox").click(function () {
                        var oCheck = $(this)[0];
                        if (oCheck.checked == true) {
                            AddValue($(this).val());
                        }
                        else {
                            RomoveVale($(this).val());
                        }
                    });
                    HideWait();
                }
            });
        });
    });
    function AddValue(nValue)
    {
        var valueList = $("#selectID").val();
        if (valueList.indexOf(nValue) == -1) {
            $("#selectID").val(valueList + nValue + ",");
        }
        GetSelectNum();
    }
    function RomoveVale(nValue)
    {
        var valueList = $("#selectID").val();
        if (valueList.indexOf(nValue) > -1) {
            var newVal= valueList.replace(nValue+",","");
           $("#selectID").val(newVal);
        }
        GetSelectNum();
    }
    function GetSelectNum()
    {
        var valueList = $("#selectID").val();
        num = valueList.split(',').length-1;
        $("#selectNum").text("已选中【"+num+"】");
    }
</script>
</asp:Content>
