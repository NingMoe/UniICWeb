<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadfile.aspx.cs" Inherits="apkuploadfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <form name="upform" method="POST" enctype="multipart/form-data">  
　　参数<input type="text" name="username"/><br/>  
　　文件1<input type="file" name="file1"/><br/>  
　　文件2<input type="file" name="file2"/><br/>  
　　<input type="submit" value="Submit" /><br/>  
  </form>  
</body>
</html>
