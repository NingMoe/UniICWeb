<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlgdiskupload.aspx.cs" Inherits="tdata_redsupload" %>
<%@ Register Src="~/Modules/HeadInclude.ascx" TagPrefix="unifound" TagName="HeadInclude" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <unifound:HeadInclude runat="server" ID="HeadInclude" />  
 <script>
     $(function () {
         $("#btnsave").button();
     });
     </script>
</head>
<body>
    <form id="form1" runat="server">
 <div style="margin:5px;"><Upload:InputFile ID="AttachFile" runat="server"/>
         <Upload:ProgressBar ID="ProgressBar1" runat='server'>
          </Upload:ProgressBar>     

 </div>
        <div style="margin:5px;">说明：<input type="text" name="szmemo" /></div>
          <div style="margin-top:10px;margin-left:120px;">
              <input type="submit" id="btnsave" value="上传" />
         </div>
    </form>
</body>
</html>
