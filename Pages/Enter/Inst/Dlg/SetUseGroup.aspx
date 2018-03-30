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
                         </td>
                     </tr>
                 </table>
                 </div>
               <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                       <th>姓名</th>
                        <th>学工号</th>
                       <th>部门</th>
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
            $("#btnSearch").button();
            var vImportVal = $("#<%=isImport.ClientID%>").val();
            debugger;
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
