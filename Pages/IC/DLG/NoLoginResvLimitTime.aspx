<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NoLoginResvLimitTime.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><h2><%=m_Title %></h2></div>
  <input type="hidden" id="selectID" name="selectID" />
     <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
    <div class="formtable">
        <table>
           <tr>
               <td style="text-align:right">
                   <div id="roomList">
                <!--   勾选<%=ConfigConst.GCRoomName %>:-->
                 </div>
               </td>
               <td>
                    <div id="roomListb">
                    <%=m_szRoom %>
                        </div>
               </td>
           </tr>
        </table>
        </div>
     <div style="margin-top:10px">
            <hr style="color:#999" width="99%" size="1" />
             </div>
        <div style="width:80%;margin:10px auto;" >
            勾选<%=ConfigConst.GCDevName %>:<a href="#" style="color:blue;text-decoration:underline" id="aSelect" select="false">全选</a>
            <div id="szDevList"><%=m_szDev %></div>
        </div>
         <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
        <div style="width:80%;margin:0px auto;" >
           <div style="width:80px;margin:0px auto; color:red;" id="selectNum"> 已选中【0】</div>
        </div>
     <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
    <div>
        <div style="margin:10px auto;width:700px">
           限定时长：<select id="LimitTime" name="LimitTime">
               <%=szLimiTime %>
               </select>
        </div>
       
        </div>
         <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
        <div style="margin:15px auto;">
            <div style="width:250px;margin:10px auto;">
            <button type="submit" id="OK">设置免登陆预约</button><button type="button" id="Cancel">关闭窗口</button>
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
        setTimeout(setDevList, 100);
        function setDevList() {
            var vDevList = $("#szDevList label");
            var vTable = $("<table></table>");
            var vTr = $("<tr></tr>");
            for (var i = 0; i < vDevList.length; i++) {
                var vtd = $("<td></td>");
                vtd.css("width", "85px")
                var vTemp = $(vDevList[i]);
                vtd.append(vTemp);
                vTr.append(vtd);
                if ((i + 1) % 6 == 0 || i == (vDevList.length - 1)) {
                    vTable.append(vTr);
                    vTr = $("<tr></tr>");
                }
            }
            $("#szDevList").empty();
            $("#szDevList").append(vTable);
        }
        $("#dwBeginTime,#dwEndTime").datetimepicker({
            stepHour: 1,
            stepMinute: 1
        });
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
                    setDevList();
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
