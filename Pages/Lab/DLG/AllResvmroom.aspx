<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="AllResvmroom.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><h2><%=m_Title %></h2></div>
  <input type="hidden" id="selectID" name="selectID" />
     
            <div class="tb_infoInLine">
            <table>
                <tr>
                    <th><%=ConfigConst.GCLabName %>：</th>
                    <td><select id="labid" name="labid" style="width:150px;"></select></td>
                    <th><%=ConfigConst.GCRoomName %>：</th>
                    <td><select id="roomid" name="roomid" style="width:150px;"></select></td>
                    <td>
                        <input type="button" value="查询" id="btnSearche" style="width:120px;" />
                    </td>
                </tr>
            </table>
            </div>
     
        <div class="tb_infoInLine" style="height:250px;overflow:scroll;margin-top:20px;width:650px">
            <table id="" class="UniTable" style="width:99%">
                <thead>
                    <tr>
                        <td style="width:60px"><div style="width:60px;"><input type="checkbox" value="1" style="width:20px;" id="checkall" /></div></td>
                      <td><%=ConfigConst.GCDevName %>名</td>
                      
                    </tr>          
                </thead>
                <tbody id="tbldevice">

                </tbody>
            </table>
        </div>
     <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
    <div>
        <div style="margin:10px auto;width:99%">
            开始时间:<input type="text" id="dwBeginTime" name="dwBeginTime" class="validate[required]" />
            结束时间:<input type="text" id="dwEndTime" name="dwEndTime" class="validate[required]" />
        </div>
        </div>
         <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
        <div style="margin:15px auto;">
            <div style="width:250px;margin:10px auto;">
            <button type="submit" id="OK">设置教学使用</button><button type="button" id="Cancel">关闭窗口</button>
        </div>
          
    </div>    
         
</form>
</asp:Content>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
      
        
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK,#btnSearche").button();
        $("#roomListb").buttonset();
        $("#dwBeginTime,#dwEndTime").datetimepicker({
            stepHour: 1,
            stepMinute: 1
        });
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(InitLab, 10);
        function InitLab() {
            $.ajax({
                type: "post",
                url: "../data/searchLab.aspx?needline=1000",
                dataType: "json",
                success: function (data) {
                    $("#labid").empty();
                    data = eval(data);
                    var szRes = "";
                    for (i = 0; i < data.length; i++) {
                        if (data[i].id == 0) {
                            continue;
                        }
                        var vOption = $("<option value='" + data[i].id + "'>" + data[i].label + "</option>");
                        $("#labid").append(vOption);
                    }
                    InitRoom();
                }
            });
        }
        $("#labid").change(function () {
            InitRoom();
        });
        function InitRoom() {
            var vLabID = $("#labid").val();
            $.ajax({
                type: "post",
                url: "../data/searchRoom.aspx?needline=1000&labid=" + vLabID,
                dataType: "json",
                success: function (data) {
                    $("#roomid").empty();
                    data = eval(data);

                    var szRes = "";
                    for (i = 0; i < data.length; i++) {
                        if (data[i].id == 0) {
                            continue;
                        }
                        var vOption = $("<option value='" + data[i].id + "'>" + data[i].label + "</option>");
                        $("#roomid").append(vOption);
                    }
                    getDevList();
                }
            });
        }
        $("#btnSearche").click(function () {
            getDevList();


        });
        function setSelectValue() {
            var vDevid = $("#selectID").val();

            $("#tbldevice input:checkbox").each(function () {

                //$(this).prop("checked", true);

                //ocheck.checked = true;
                if ((',' + vDevid).indexOf(',' + $(this).val() + ',') > -1) {

                    $(this).prop("checked", true);
                }
                else {

                    $(this).prop("checked", false);
                }

            });

        }
        $("#checkall").click(function (event) {
            var ahref = $(this);
            var bSelect = ahref.attr("select");
            if (bSelect == "true") {

                ahref.attr("select", "false");
                $("#tbldevice input:checkbox").each(function () {
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
                ahref.attr("select", "true");
                $("#tbldevice input:checkbox").each(function () {
                    var ocheck = $(this)[0];
                    ocheck.checked = true;
                    AddValue($(this).val());
                });

            }


        });
        $("#tbldevice").on("click", "input:checkbox", function () {
            var oCheck = $(this)[0];
            if (oCheck.checked == true) {
                AddValue($(this).val());
            }
            else {
                RomoveVale($(this).val());
            }

        });

        function getDevList() {
            var vroomid = $("#roomid").val();
            $.ajax({
                type: "post",
                url: "../data/searchdevice.aspx?needline=100000&Type=null&roomid=" + vroomid,
                dataType: "json",
                success: function (data) {
                    $("#tbldevice").empty();
                    data = eval(data);

                    var szRes = "";
                    for (i = 0; i < data.length; i++) {
                        var vOption = $("<tr><td style='width:60px'><input style='width:20px' type='checkbox' name='checkdevid' value='" + data[i].id + "'></td><td>" + data[i].label + "</td></tr>");
                        $("#tbldevice").append(vOption);
                    }
                    setSelectValue();
                }
            });
        }
        function AddValue(nValue) {
            var valueList = $("#selectID").val();
            if (valueList.indexOf(nValue) == -1) {
                $("#selectID").val(valueList + nValue + ",");
            }

        }
        function RomoveVale(nValue) {
            var valueList = $("#selectID").val();
            if (valueList.indexOf(nValue) > -1) {
                var newVal = valueList.replace(nValue + ",", "");
                $("#selectID").val(newVal);
            }

        }
        function GetSelectNum() {
            var valueList = $("#selectID").val();
            num = valueList.split(',').length - 1;
            $("#selectNum").text("已选中【" + num + "】");
        }
    });
</script>
</asp:Content>
