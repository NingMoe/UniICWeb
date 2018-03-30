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
using System.Xml.Serialization;

<![CDATA[
namespace UniWebLib]]>
{
<xsl:apply-templates name="./">
</xsl:apply-templates>
}
//</pre>
</xsl:template>

<xsl:template match="Module">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>

<xsl:template match="UniStructes">
	/*开始数据结构*/
<xsl:apply-templates name="./">
</xsl:apply-templates>
	/*结束数据结构*/
</xsl:template>

<xsl:template match="UniStruct">
	/*<xsl:value-of select="Description"/>*/
	public struct <xsl:value-of select="@Name"/>
	{
		private Reserved reserved;
		<xsl:apply-templates name="./"></xsl:apply-templates>	};
</xsl:template>


<xsl:template name="FieldLoop">
<xsl:param name="MaxCount"/>
<xsl:param name="Count"/>
<xsl:if test="$Count and $Count&gt;$MaxCount"></xsl:if>
<xsl:if test="not($Count) or $Count&lt;$MaxCount">
	<xsl:if test="@ExtType">
	public <xsl:value-of select="@ExtType"/><xsl:text> </xsl:text><xsl:value-of select="concat(@Name,$Count)"/>;			/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	<xsl:if test="not(@ExtType)">
	<xsl:if test="@Type='UniSZ'">
		public string<xsl:text> </xsl:text><xsl:value-of select="concat(@Name,$Count)"/>;			/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	<xsl:if test="@Type='UniDW'">
		public uint?<xsl:text> </xsl:text><xsl:value-of select="concat(@Name,$Count)"/>;			/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	</xsl:if>
<xsl:call-template name="FieldLoop">
<xsl:with-param name="MaxCount" select="$MaxCount"></xsl:with-param>
<xsl:with-param name="Count"><xsl:value-of select="number($Count)+1"/></xsl:with-param>
</xsl:call-template>
</xsl:if>
</xsl:template>

<xsl:template match="Field">
<xsl:param name="Count"/>

<xsl:if test="@Array">
		//public <xsl:value-of select="@Type"/><xsl:text> </xsl:text><xsl:value-of select="@Name"/><xsl:if test="$Count"><xsl:value-of select="$Count"/></xsl:if><xsl:if test="@Array">[<xsl:value-of select="@Array"/>]</xsl:if>;		/*<xsl:value-of select="Description"/>*/
	<xsl:variable name="CSArray">
		<xsl:if test="not(boolean(number(@Array)))">
			<xsl:variable name="Array" select="@Array"/>
			<xsl:value-of select="number(//Const[@Name=$Array]/Value)"/>
		</xsl:if>
		<xsl:if test="boolean(number(@Array))">
			<xsl:value-of select="@Array"/>
		</xsl:if>
	</xsl:variable>
	<xsl:call-template name="FieldLoop">
		<xsl:with-param name="MaxCount"><xsl:value-of select="$CSArray"/></xsl:with-param>
		<xsl:with-param name="Count" select="0"></xsl:with-param>
	</xsl:call-template>
</xsl:if>

<xsl:if test="not(@Array)">
	<xsl:if test="@ExtType">
	public <xsl:value-of select="@ExtType"/><xsl:text> </xsl:text><xsl:value-of select="@Name"/><xsl:if test="$Count"><xsl:value-of select="$Count"/></xsl:if>;		/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	<xsl:if test="not(@ExtType)">
	<xsl:if test="@Type='UniSZ'">
		public string<xsl:text> </xsl:text><xsl:value-of select="@Name"/><xsl:if test="$Count"><xsl:value-of select="$Count"/></xsl:if>;		/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	<xsl:if test="@Type='UniDW'">
		public uint?<xsl:text> </xsl:text><xsl:value-of select="@Name"/><xsl:if test="$Count"><xsl:value-of select="$Count"/></xsl:if>;		/*<xsl:value-of select="Description"/>*/
	</xsl:if>
	<xsl:if test="count(./Const) > 0">
		<xsl:variable name="ENUMNAME" select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')" />
		[FlagsAttribute]
		public enum <xsl:value-of select="$ENUMNAME"/> : uint
		{
			<xsl:for-each select="./Const">
			<xsl:if test="not(@Type) or @Type!='String'">
				[EnumDescription("<xsl:value-of select="Description"/>")]
				<xsl:value-of select="@Name"/> = <xsl:value-of select="Value"/>,
			</xsl:if>
			</xsl:for-each>
		}

	</xsl:if>
	</xsl:if>
</xsl:if>
</xsl:template>

<xsl:template match="Description"></xsl:template>
<xsl:template match="Parameter"></xsl:template>
<xsl:template match="Result"></xsl:template>
<xsl:template match="Const"></xsl:template>
<xsl:template match="Value"></xsl:template>

</xsl:stylesheet>
