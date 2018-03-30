<%@ Page Language="C#" AutoEventWireup="true" CodeFile="redsupload.aspx.cs" Inherits="tdata_redsupload" %>
<%@ Register Src="~/Modules/HeadInclude.ascx" TagPrefix="unifound" TagName="HeadInclude" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <unifound:HeadInclude runat="server" ID="HeadInclude" />
    <style>
        .href {
        color:blue;
        text-decoration:underline;
        }
    </style>
    <script>
        $(function () {
            var vTab = $("#tabid").val();
            $("#tabs").tabs({ active: vTab });
            $(".subReport").click(function () {
              
                var testitemid = $(this).data("id");
                    $.lhdialog({
                        title: '上传',
                        width: '330px',
                        height: '170px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlgredsupload.aspx?id=' + testitemid
                    });
            });
            $(".del").click(function () {
               
                var testitemid = $(this).data("id");
                var vFromid = $("#fromID").val();
                ConfirmBox("确定删除?", function () {
                    var vPostion = -1;
                    var vHref = location.href;
                    if (vHref.indexOf("&delID") > -1) {
                        vPostion = vHref.indexOf("&delID");
                    }
                    else if (vHref.indexOf("?delID") > -1) {
                        vPostion = vHref.indexOf("?delID");
                    }
                    if (vPostion > -1) {
                        vHref = vHref.substring(0, vPostion);
                    }
                    
                    if (location.href.indexOf("?") >= 0) {
                        location.href = vHref + "&delID=" + testitemid;
                    } else {
                        location.href = vHref + "?delID=" + testitemid;

                    }
                    
                    }, '提示', 1, function () { });
              
            });
            $("#btnUpLoad").button().click(function () {
            
                    $.lhdialog({
                        title: '上传',
                        width: '330px',
                        height: '170px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlgdiskupload.aspx' 
                    });
            });
        });
        
  </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="tabid" name="tabid" value="<%=m_tab %>" />
         <input type="hidden" id="fromID"  value="<%=szFormID %>" />
        <div id="tabs">
  <ul>
    <li><a href="#tabs-1">上传实验报告</a></li>
    <li><a href="#tabs-2">个人网盘</a></li>  
  </ul>
       
  <div id="tabs-1">
     
    <table class="ListTbl">
                <thead>
                    <tr>
                        <th>教师姓名</th>
                        <th>课程名称</th>
                        <th>项目名</th>
                         <th>状态</th>
                         <th name="szFeeName">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
        </div>
  <div id="tabs-2">
     
      <div>
          <input type="button" id="btnUpLoad" value="上传文件" />
      </div>
      <div style="color:red">硬盘统计说明：<%=m_szOutDISKSTATE %></div>
      
      <div style="margin:10px">
     <table class="ListTbl">
                <thead>
                    <tr>
                        <th>文件名</th>
                        <th>大小(mb)</th>
                        <th>上传日期</th>
                         <th>说明</th>
                         <th>操作</th>
                    </tr>
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOutDISK %>
                </tbody>
            </table>
          </div>
  </div>
    </div>  
    </form>
</body>
</html>
