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
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using UniStruct;
<![CDATA[
/// <summary>
/// Provides a description for an enumerated type.
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumDescriptionAttribute : Attribute
{
	private string m_Description;
	public string Description
	{
		get
		{
			return this.m_Description;
		}
	}
	public EnumDescriptionAttribute(string description)
		: base()
	{
		this.m_Description = description;
	}
}
/// <summary>
/// Provides a static utility object of methods and properties to interact
/// with enumerated types.
/// </summary>
public static class EnumHelper
{
	/// <summary>
	/// Gets the <see cref=”DescriptionAttribute” /> of an <see cref=”Enum” /> type value.
	/// </summary>
	public static string GetDescription(Enum value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		string description = value.ToString();
		FieldInfo fieldInfo = value.GetType().GetField(description);
		EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
		if (attributes != null && attributes.Length > 0)
		{
			description = attributes[0].Description;
		}
		return description;
	}
	
	/// <summary>
	/// Converts the <see cref=”Enum” /> type to an <see cref=”IList” /> compatible object. 
	/// </summary>
	public static IList ToList(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		ArrayList list = new ArrayList();
		Array enumValues = Enum.GetValues(type);
		foreach (Enum value in enumValues)
		{
			list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}
		return list;
	}
}

namespace UniWebLib]]>
{
<![CDATA[
	//模块列表
	public delegate void ErrorHandler(PRModule module,REQUESTCODE ret);
	public partial class PRModule
	{
		public ErrorHandler OnError;
]]>
<xsl:for-each select="./Module">
		/*<xsl:value-of select="Description"/>*/
		public const uint <xsl:value-of select="translate(./Commands/@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>_BASE	= (<xsl:value-of select="Commands/@UID"/>);
</xsl:for-each>
	}
<xsl:apply-templates name="./">
</xsl:apply-templates>
}
//</pre>
</xsl:template>

<xsl:template match="Module">
<![CDATA[	]]>/*开始接口--<xsl:value-of select="@Name"/>--<xsl:value-of select="Description"/>*/
/*开始命令*/
	public partial class PR<xsl:value-of select="@Name"/>
	{
<xsl:apply-templates name="./">
</xsl:apply-templates>

	}
/*结束命令*/
<![CDATA[	]]>/*结束接口--<xsl:value-of select="@Name"/>*/
</xsl:template>

<xsl:template match="Commands">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>

<xsl:template match="UniStructes">
</xsl:template>

<xsl:template match="Command">
		/*<xsl:value-of select="Description"/>,	参数:<xsl:value-of select="Parameter"/>,	结果:<xsl:value-of select="Result"/><xsl:if test="Result=''">null</xsl:if><xsl:if test="Result/@Array='true'">(Array)</xsl:if>*/
		public const uint <xsl:value-of select="@Name"/> 	= <xsl:value-of select="@UID"/>;
</xsl:template>

<xsl:template match="UniStruct">
</xsl:template>

<xsl:template match="Description"></xsl:template>
<xsl:template match="Parameter"></xsl:template>
<xsl:template match="Result"></xsl:template>
<xsl:template match="Const"></xsl:template>
<xsl:template match="Value"></xsl:template>

</xsl:stylesheet>
