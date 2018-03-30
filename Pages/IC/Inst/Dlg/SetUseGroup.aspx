<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetUseGroup.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server" enctype="multipart/form-data">
        <div class="formtitle"><%=m_Title %></div>        
        <input type="hidden" name="id" id="id" />
        <input type="hidden" name="myop" id="myop" />
        <input type="hidden" name="isImport" id="isImport" runat="server" />
        <div class="ListTbl">
            <div>
                 <!--<button type="button" id="btnAdd">添加学生</button>-->
                <button type="button" id="btnImPort">导入成员</button>
                <a href="../../DownLoadFile/groupmember.csv">下载模板</a>
                    <button type="button" id="btnAdd">添加学生</button>
            </div>
             <div id="addDiv">
                   <input type="hidden" id="dwAccno" name="dwAccno" />
            <table width="550" style="text-align:center;margin:0px auto">
              <tr>
                  <td colspan="4" style="text-align:center;font-size:14px;"><label style="color:blue">添加成员</label></td>
              </tr>
                <tr>
                    <th>学生学号:</th>
                     <td style="text-align:left"><input type="text" id="szPid" name="szPid" /></td>
                     <th>姓名:</th>
                     <td style="text-align:left">
                       <input type="text" id="szTrueName" name="szTrueName" /></td>
                </tr>
                 <tr>
                    <th>学生手机:</th>
                     <td style="text-align:left"><input type="text" id="Handphone" name="Handphone" runat="server" /></td>
                     <th>学生邮箱:</th>
                     <td style="text-align:left"><input type="text" id="email" name="email" runat="server" /></td>
                </tr>
                  <tr>
                    <th>开始日期:</th>
                     <td style="text-align:left"><input type="text" id="dwStartDate" name="dwStartDate" /></td>
                     <th>结束日期:</th>
                     <td style="text-align:left"><input type="text" id="dwEndDate" name="dwEndDate" /></td>
                </tr>
                <tr>
                    <th colspan="4" style="text-align:center;"> 
                        <input  type="button" id="btnAddOK" value="添加" />
                    <input type="button" id="btnaddCancel" value="关闭" /></th>
                </tr>
             <tr>                 
             </tr>
            </table>               
                </div>

            <div id="importTable">
                <table class="ListTbl">
                    <tr>
                        <td>第一步：</td>
                        <td>
                            <input type="file" name="improtFile" id="improtFile" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td>第二步：</td>
                        <td><input type="button" id="btnViewBtn" value="浏览" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="height:200px;overflow:scroll;text-align:center;">
                                <%=szOut %>
                                </div>
                        </td>
                    </tr>
                     <tr>
                        <td>第三步：</td>
                        <td><input type="button" id="btnImportBtn" value="导入" /></td>
                    </tr>
                </table>
            </div>
             <div class="content" style="margin-top:10px">
                 <table class="ListTbl">
                     <tr>
                         <th>
                             学工号：
                         </th>
                         <td style="text-align:left">
                             <input type="text" name="logonname" id="logonname" />
                         </td>
                         <td>
                             <input type="submit"  value="查询" id="btnSearch" />
                              <input type="submit"  value="批量删除" id="delList" />
                         </td>
                     </tr>
                 </table>
                 </div>
               <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                       <th>姓名</th>
                        <th>学工号</th>
                       <th>班级</th>
                        <th>部门</th>
                        <th>手机</th>
                        <th>邮箱</th>
                         <th>开始日期</th>
                         <th>结束日期</th>
                         <th>状态</th>
                           <th width="25px">操作</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
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
            .ui-autocomplete{
       z-index: 11111;
}
    </style>
  <script language="javascript" type="text/javascript" src="<%=MyVPath %>themes/js/MainJScript.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#dwStartDate,#dwEndDate").datepicker();
            $("#btnAdd").click(function () {
                $("#addDiv").dialog("open");
            });
            AutoUserByLogonname($("#szPid"), 2, $("#dwAccno"), $("#Handphone"), null, $("#email"));
            $("#szPid").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#szPid").val(ui.item.szLogonName);
                    $("#szTrueName").val(ui.item.szTrueName);
                }, 100);
            });

            $("#btnAddOK").button().click(function () {
                var GroupID = $("#id").val();
                var MemberID = $("#dwAccno").val();
                var name = $("#szTrueName").val();
                var vBeginDate = $("#dwStartDate").val();
                var vEndDate = $("#dwEndDate").val();
                $.ajax({
                    type: 'get',
                    url: '../../data/addgroupmember.aspx?type=accno',
                    data: { 'GroupID': GroupID, 'MemberID': MemberID, 'KindID': 2, 'Name': name, 'dwStartDate': vBeginDate, 'dwEndDate': vEndDate },
                    dataType: "text",
                    success: function (data) {
                        var message2 = data;
                        if (message2 == 'succ') {
                            $("#dwAccno").val("");
                            ConfirmBox2('添加成功', function () { $("#<%=formAdvOpts.ClientID%>").submit(); });
                        }
                        else {
                            ConfirmBox2('添加失败' + message2, function () { });
                        }
                    },
                    error: function (data) {
                        ConfirmBox2('失败', function () { });
                    }
                });
            });
            $("#btnaddCancel,#delList").button()
           .click(function () {
               $("#addDiv").dialog("close");
           });
            $("#addDiv").dialog({
                autoOpen: false,
                height: 220,
                width: 700,
                modal: true
            });
            $("#btnSearch,#btnAdd,#btnAddOK").button();
            var vImportVal = $("#<%=isImport.ClientID%>").val();
            if (vImportVal != "1") {
                $("#importTable").hide();
            }
            $("#btnImportBtn").button().click(function () {
                $("#myop").val("import");
                $("#<%=formAdvOpts.ClientID%>").submit();
            });
            $("#btnViewBtn").button().click(function () {
                $("#myop").val("view");
                $("#<%=formAdvOpts.ClientID%>").submit();
            });
            $("#btnImPort").button().click(function () {
                $("#importTable").show();
            }
                );
            $("#Cancel").button().click(Dlg_Cancel);
            $(".OPTD").html('<div class="OPTDBtn">\
<a class="delBtn" title="删除"><img src="../../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
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
      
            $("#btnAdd").button()
            $("#btnAdd").click(function () {
                    $("#addDiv").dialog("open");                                    
                });

    });
    </script>
</asp:Content>
