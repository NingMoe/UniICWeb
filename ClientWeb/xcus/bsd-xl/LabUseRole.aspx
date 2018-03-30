<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabUseRole.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_LabUseRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
    </script>
</head>
<body>
            <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
            <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">实验室使用资格</a></li>
        </ul>
    <div class="box_tbl zebra">
                    <table class="tblResvs">
                    <thead>
                        <tr>
                            <th>实验室类型</th>
                            <th>状态</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <%=labRoleList %>
                </table>
    </div>
</body>
</html>
