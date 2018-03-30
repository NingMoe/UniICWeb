<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetUseGroup.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <div class="formtitle"><%=m_Title %></div>        
        <input type="hidden" name="id" id="id" />
         <input type="hidden" name="szTutorLogonName" id="szTutorLogonName" />
        <div class="ListTbl">
            <div id="addDiv">
            <table width="550" style="text-align:center;margin:0px auto">
              <tr>
                  <td colspan="4" style="text-align:center;font-size:14px;"><label style="color:blue">学生必须指定<%=ConfigConst.GCTutorName%>才能操作</label></td>
              </tr>
                <tr>
                    <th>学生学号:</th>
                     <td style="text-align:left"><input type="text" id="ppppp" name="ppppp" /></td>
                     <th><%=ConfigConst.GCTutorName%>姓名:</th>
                     <td style="text-align:left">
                         <input type="text" id="turtorLognName" name="turtorLognName" /></td>
                </tr>
                 <tr>
                    <th>开始日期:</th>
                     <td style="text-align:left"><input type="text" id="dwStartDate" name="dwStartDate" runat="server" /></td>
                     <th>结束日期:</th>
                     <td style="text-align:left"><input type="text" id="dwEndDate" name="dwEndDate" runat="server" /></td>
                </tr>
                 <tr>
                    <th>学生手机:</th>
                     <td style="text-align:left"><input type="text" id="Handphone" name="Handphone" runat="server" /></td>
                     <th>学生邮箱:</th>
                     <td style="text-align:left"><input type="text" id="email" name="email" runat="server" /></td>
                </tr>
                <tr>
                    <th colspan="4" style="text-align:center;"> 
                        <input  type="button" id="btnAddOK" value="添加该学生" />
                    <input type="button" id="btnaddCancel" value="关闭" /></th>
                </tr>
             <tr>                 
             </tr>
            </table>               
                </div>
            <div>
                 <button type="button" id="btnAdd">添加学生</button>
            </div>
               <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                       <th>姓名</th>
                        <th>学工号</th>
                        <th><%=ConfigConst.GCTutorName%></th>
                        <th name="dwBeginDate">开始日期</th>
                        <th>结束日期</th>
                        <th>状态</th>
                        <th>手机</th>
                        <th>邮箱</th>
                           <th width="25px">操作</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
               <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
            <div style="margin:10px auto;text-align:center">
                <button type="button" id="Cancel">关闭</button>
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #addDiv table th
        {       
            height:30px;   
            text-align:right;
        }      
           #addDiv table td input
        {       
             margin-left:10px;
             height:18px;
             width:140px;
        }
             .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
            width:120px;
            }
    </style>
  <script language="javascript" type="text/javascript" src="<%=MyVPath %>themes/js/MainJScript.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {            
            $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setBtn" title="修改"><img src="../../../../themes/iconpage/edit.png"/></a>\
<a class="delBtn" title="删除"><img src="../../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#addDiv").dialog({
                autoOpen: false,
                height:220,
                width: 700,
                modal: true              
            });
            $(".delBtn").click(function () {
                var MeberID = $(this).parents("tr").children().first().attr("data-accno");;
                ConfirmBox("确定删除?", function () {
                    var GroupID = $("#id").val();
                    $.get(
             "../../data/DelGroupMember.aspx",
             { GroupID: GroupID, MemberID: MeberID, KindID: 2 },
             function (data) {
                 if (data == "success") {
                     $("#<%=formAdvOpts.ClientID%>").submit();
                 }
                 else {
                     MessageBox(data, "", 2);
                 }
             }
        );});

            });

            $(".setBtn").click(function () {

                $("#addDiv").dialog("open");
                var sLogonName = $(this).parents("tr").children().first().attr("data-sLogonName");
                var tLogonName = $(this).parents("tr").children().first().attr("data-tLogonName");
                var sAccno = $(this).parents("tr").children().first().attr("data-accno");
                var handphone = $(this).parents("tr").children().first().attr("data-handphone");
                var email = $(this).parents("tr").children().first().attr("data-email");
                var startDate = $(this).parents("tr").children().first().attr("data-begin");
                var endDate = $(this).parents("tr").children().first().attr("data-end");
                var pid2 = $(this).parents("tr").children().first().attr("data-truename");
                var szTtrueName = $(this).parents("tr").children().first().attr("data-szTtrueName");
                $("#dwAccno").val(sAccno);
                $("#ppppp").val(sLogonName);
                $("#turtorLognName").val(szTtrueName);
                $("#szTutorLogonName").val(tLogonName);
                $("#<%=Handphone.ClientID%>").val(handphone);
                $("#<%=email.ClientID%>").val(email);
                $("#<%=dwStartDate.ClientID %>").val(startDate);
                $("#<%=dwEndDate.ClientID %>").val(endDate);                         
                $("#btnAddOK").val("修改该学生");
            });
            $("#Cancel").button().click(Dlg_Cancel);
            $("#btnAddOK").button()
            .click(function () {
                var bAdd = false;
                var bSetTutor = false;

                var sLogonName = $("#ppppp").val();
                var tLogonName = $("#szTutorLogonName").val();
                var sHandPhone = $("#<%=Handphone.ClientID%>").val();
                var sEmail = $("#<%=email.ClientID%>").val();
                $.ajax({
                    type: 'get',
                    url: '../../data/setStudentTur.aspx',
                    data: { 'sLogonName': sLogonName, 'tLogonName': tLogonName, 'Handphone': sHandPhone, "email": sEmail },
                    dataType: "json",
                    success: function (data) {
                        var message=data.message;
                        if (message == 'succ') {
                            bSetTutor = true;
                            var dwStartDate = $("#<%=dwStartDate.ClientID %>").val();
                            var dEndDate = $("#<%=dwEndDate.ClientID %>").val();
                            var name = $("#ppppp").val();
                            var GroupID = $("#id").val();
                            var kind = 2;
                            $.ajax({
                                type: 'get',
                                url: '../../data/addgroupmember.aspx?type=logonname',
                                data: { 'GroupID': GroupID, 'MemberID': name, 'KindID': kind, 'Name': name, 'dwStartDate': dwStartDate, 'dwEndDate': dEndDate },
                                dataType: "json",
                                success: function (data) {
                                    var message2=data.message;
                                    if (message2 == 'succ') {
                                        if (message2 == 'succ') {
                                            bAdd = true;
                                            if (bSetTutor && bAdd) {
                                                ConfirmBox2( $("#btnAddOK").val()+'成功', function () { $("#<%=formAdvOpts.ClientID%>").submit(); });
                                            } else {
                                                alert(bSetTutor);
                                                alert(bAdd);
                                            }
                                        }
                                        else {
                                            ConfirmBox2($("#btnAddOK").val()+'失败' + message2, function () { });
                                        }
                                    }
                                    },
                                error: function (data) {
                                    ConfirmBox2('失败', function () { });
                                }
                            });
                        }
                        else { ConfirmBox2('失败' + message, function () { }); }
                    },
                    error: function (data) {
                        ConfirmBox2(data, function () { });
                    }
                });

              

               
            });
            $("#btnaddCancel").button()
            .click(function () {               
                $("#addDiv").dialog("close");
            });
            setTimeout(function () {
                $("#<%=dwStartDate.ClientID %>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $("#<%=dwEndDate.ClientID %>").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $("#btnAddOK").attr("disabled", true);
            }, 1);           
            $("#btnAdd").button()
            $("#btnAdd").click(function () {
                    $("#addDiv").dialog("open");                                    
                });
            $("#ppppp").autocomplete({
                source: "../../data/searchaccount.aspx?type=logonname",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {                                                   
                            $("#dwAccno").val(ui.item.id);
                            $("#<%=Handphone.ClientID%>").val(ui.item.szHandPhone);
                            $("#<%=email.ClientID%>").val(ui.item.szEmail);      
                            $("#turtorLognName").val(ui.item.szTurtorTrueName);
                            $("#szTutorLogonName").val(ui.item.szTurTorLogonName);
                            if ((ui.item.szIsExistTur.indexOf('true') > -1)) {                               
                                $("#btnAddOK").attr("disabled", false);
                            }
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {                       
                        ui.content.push({ label: " 用户不存在 " });
                    }
                }
            });
            $("#turtorLognName").autocomplete({
                source: "../../data/searchAccount.aspx?type=truename",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $(this).val(ui.item.label);                            
                            $("#szTutorLogonName").val(ui.item.szLogonName);
                            if ($("#ppppp").val() != '') {
                                $("#btnAddOK").attr("disabled", false);
                            }
                        }
                    }
                    return false;
                },
                 response: function (event, ui) {
                     if (ui.content.length == 0) {                      
                         ui.content.push({ label: " 非<%=ConfigConst.GCTutorName%>身份,[管理设置]处添加<%=ConfigConst.GCTutorName%>" });
                     }
                 }
             }).blur(function () { 
                 if ($("#turtorLognName").val() == "") {                    
                     $(this).val("");
                 } else {
                   
                 }
             });
            //$(".ListTbl").UniTable();

    });
    </script>
</asp:Content>
