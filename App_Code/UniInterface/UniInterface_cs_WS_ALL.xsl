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
using System.Web.Script.Services;
using UniStruct;
using UniWebLib;

<![CDATA[
/// <summary>
/// UniLab 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public partial class UniLabAll : UniBaseService
{
    public UniLabAll () {

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
	<xsl:if test="./Commands/@Name!='THIRDIF'">

	//#region <xsl:value-of select="./Commands/@Name"/>部分
	/*<xsl:value-of select="Description"/>*/<xsl:apply-templates name="./"></xsl:apply-templates>
	//#endregion <xsl:value-of select="./Commands/@Name"/>部分
	</xsl:if>
	<xsl:if test="./Commands/@Name='THIRDIF'">
	//<xsl:value-of select="./Commands/@Name"/>部分
	</xsl:if>
</xsl:template>

<xsl:template match="Commands">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>


<xsl:template match="Command">
	<xsl:variable name="CommandName"><xsl:value-of select="../@Name"/>_<xsl:value-of select="@Alias"/></xsl:variable>
	<xsl:if test="$CommandName!='Admin_StaLogin' and $CommandName!='Device_DevLogon' and $CommandName!='Device_DevLogout' and $CommandName!='Device_DevHandShake' and $CommandName!='Account_Act' and $CommandName!='Account_Set' and $CommandName!='Account_Check' and $CommandName!='Device_DevRegist'">
	
	public struct <xsl:value-of select="$CommandName"/>Result
	{
		public uint code;
		public string Message;
		<xsl:if test="Result and Result!=''">public  <xsl:value-of select="Result"/><xsl:if test="Result/@Array='true'">[] vt</xsl:if><xsl:if test="not(Result/@Array='true')"> vr</xsl:if>Res;</xsl:if>
	}
	<xsl:if test="not(Parameter/@Array='true')">
	[WebMethod (EnableSession = true, Description = "<xsl:value-of select="Description"/>")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public <xsl:value-of select="$CommandName"/>Result 
	<xsl:value-of select="$CommandName"/>(<xsl:if test="Parameter and Parameter!=''"><xsl:value-of select="Parameter"/> vrParameter</xsl:if>)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		<xsl:value-of select="$CommandName"/>Result ret = new <xsl:value-of select="$CommandName"/>Result();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;
<![CDATA[
        uResponse = m_Request.]]><xsl:value-of select="../@Name"/><![CDATA[.]]><xsl:value-of select="@Alias"/>(<xsl:if test="Parameter and Parameter!=''">vrParameter<xsl:if test="Result and Result!=''">,</xsl:if></xsl:if><xsl:if test="Result and Result!=''"> out <xsl:if test="Result/@Array='true'"> ret.vt</xsl:if><xsl:if test="not(Result/@Array='true')"> ret.vr</xsl:if>Res</xsl:if><![CDATA[);
		ret.Message = m_Request.szErrMsg;
		]]>
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		<xsl:if test="Result and Result!=''">
		if ((object)<xsl:if test="Result/@Array='true'"> ret.vt</xsl:if><xsl:if test="not(Result/@Array='true')"> ret.vr</xsl:if>Res == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		</xsl:if>
		ret.code = 0;
		return ret;
	}
	</xsl:if>
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
