<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WxSeatSign.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <script type="text/javascript" src="../ClientWeb/md/location/loc.js"></script>
<script type="text/javascript">
function loadBody()
{
<%if(!m_nLocSigned){%>
    loc.coords = "<%=ConfigConst.GCCoords%>";
    loc.locationSignIn(function (ret) {
        if (ret)
        {
			if(location.href.indexOf("?") >= 0)
			{
				location.href = location.href+"&ls=true";
			}else{
				location.href = location.href+"?ls=true";
			}
        }
        else
        {
            document.getElementById("info").innerHTML = "<h1>��ȡλ����Ϣʧ��</h>";
        }
    });
<%}%>
}
</script></head>
<body onload="loadBody()">
<div id="info">�����ж�λ���У����Ժ�..</div>
</body>
</html>
