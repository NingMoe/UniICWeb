<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet version="2.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" indent="no" media-type="text/plain" encoding="gb2312"/>
<xsl:template match="UniInterface">
/*<pre>
<xsl:value-of select="Description"/>
*/
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using UniStruct;
using UniWebLib;
<![CDATA[
/// <summary>
/// UniInterface 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class UniInterface : UniBaseService
{
    public UniInterface () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
]]>
<xsl:apply-templates name="./">
</xsl:apply-templates>
}
//</pre>
</xsl:template>

<xsl:template match="Module">
<xsl:apply-templates name="./"></xsl:apply-templates>
</xsl:template>

<xsl:template match="Commands">
<xsl:if test="@Type='Export'">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:if>
</xsl:template>


<xsl:template match="Command">
	<xsl:if test="@Alias!='ParaDownload' and @Alias!='StaLogin'">
	<xsl:variable name="CommandName"><xsl:value-of select="../@Name"/>_<xsl:value-of select="@Alias"/></xsl:variable>
	[WebMethod (Description = "<xsl:value-of select="Description"/>")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	<xsl:value-of select="$CommandName"/>(out uint code,out string Message <xsl:if test="Parameter and Parameter!=''">,<xsl:value-of select="Parameter"/><xsl:if test="Parameter/@Array='true'">[]</xsl:if> vrParameter</xsl:if> <xsl:if test="Result and Result!=''">,out <xsl:value-of select="Result"/><xsl:if test="Result/@Array='true'">[] vt</xsl:if><xsl:if test="not(Result/@Array='true')"> vr</xsl:if>Res</xsl:if>)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;
<![CDATA[
        uResponse = m_Request.]]><xsl:value-of select="../../@Name"/><![CDATA[.]]><xsl:value-of select="@Alias"/>(<xsl:if test="Parameter and Parameter!=''">vrParameter<xsl:if test="Result and Result!=''">,</xsl:if></xsl:if><xsl:if test="Result and Result!=''"> out <xsl:if test="Result/@Array='true'"> vt</xsl:if><xsl:if test="not(Result/@Array='true')"> vr</xsl:if>Res</xsl:if><![CDATA[);
		Message = m_Request.szErrMsg;
		]]>
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		<xsl:if test="Result and Result!=''">
		if ((object)<xsl:if test="Result/@Array='true'"> vt</xsl:if><xsl:if test="not(Result/@Array='true')"> vr</xsl:if>Res == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		</xsl:if>
		code = 0;
		return;
	}
	</xsl:if>
</xsl:template>

<xsl:template match="Field"></xsl:template>
<xsl:template match="UniStructes"></xsl:template>
<xsl:template match="UniStruct"></xsl:template>
<xsl:template match="Description"></xsl:template>
<xsl:template match="Parameter"></xsl:template>
<xsl:template match="Result"></xsl:template>
<xsl:template match="Const"></xsl:template>
<xsl:template match="Value"></xsl:template>

</xsl:stylesheet>
