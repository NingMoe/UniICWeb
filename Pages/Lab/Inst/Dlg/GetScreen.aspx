<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetScreen.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
       <input type="hidden" id="ipHidden" runat="server" />
    <applet archive="jviewer.jar" code="com.glavsoft.viewer.Viewer" width="100%" height="100%" style="top:-28px;position:absolute;margin:0px;padding:0px;">
			<param name="Host" value="" id="Host" /> <!-- Host to connect. Default:  the host from which the applet was loaded. -->
			<param name="Port" value="10405" /> <!-- Port number to connect. Default: 5900 -->
			<param name="Password" value="lchz8848" /> <!-- Password to the server (not recommended to use this parameter here) -->
			<param name="OpenNewWindow" value="no" /> <!-- yes/true or no/false. Default: yes/true -->
			<param name="ShowControls" value="yes" /> <!-- yes/true or no/false. Default: yes/true -->
			<param name="ViewOnly" value="yes" /> <!-- yes/true or no/false. Default: no/false -->
			<param name="AllowClipboardTransfer" value="no" /> <!-- yes/true or no/false. Default: yes/true -->
            <param name="RemoteCharset" value="standard" /> <!-- Charset encoding is used on remote system. Use this option to specify character encoding will be used for encoding clipboard text content to. Default value (when parameter is empty): local system default character encoding. Set the value to 'standard' for using 'Latin-1' charset which is only specified by rfb standard for clipboard transfers. -->

			<param name="ShareDesktop" value="yes" /> <!-- yes/true or no/false. Default: yes/true -->
			<param name="AllowCopyRect" value="yes" /> <!-- yes/true or no/false. Default: yes/true -->
			<param name="Encoding" value="Tight" /> <!-- Possible values: "Tight", "Hextile", "ZRLE", and "Raw". Default: Tight -->
			<param name="CompressionLevel" value="" /> <!-- 1-9 or empty. Empty means server default -->
			<param name="JpegImageQuality" value="" /> <!-- 1-9, Lossless or empty. When param is set to "Lossless" no jpeg compression used. Empty means server default -->
			<param name="LocalPointer" value="On" /> <!-- Possible values: on/yes/true (draw pointer locally), off/no/false (let server draw pointer), hide). Default: "On"-->
			<param name="ConvertToASCII" value="no" /> <!-- Whether to convert keyboard input to ASCII ignoring locale. Possible values: yes/true, no/false). Default: "No"-->

			<param name="colorDepth" value="" /> <!-- Reserved for future. Possible values: 6, 8, 16, 24, 32 (equals to 24). Only 24/32 is supported now -->
			<param name="ScalingFactor" value="100" /> <!-- Scale local representation of the remote desktop on startup. Default is 100 means 100% -->
		</applet>
		<div style="position:absolute;bottom:0px;">查看远程桌面中。。。</div>
        <div>
            
        </div>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
 
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
     
        var vIp = $("#<%=ipHidden.ClientID%>").val();   
        $("#Host").prop("value", vIp);
       
        setTimeout(function () {
          

        }, 10);
       
    });
</script>
</asp:Content>
