<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet version="2.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" indent="no" media-type="text/plain" encoding="gb2312"/>

<xsl:template match="UniInterface">
public class CmdList{static public object[] g_cmdlist = {
<xsl:apply-templates name="./">
</xsl:apply-templates>
null};}
</xsl:template>

<xsl:template match="Module">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>

<xsl:template match="Commands">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>

<xsl:template match="UniStructes">
</xsl:template>

<xsl:template match="Command">
<xsl:value-of select="../@UID"/>+<xsl:value-of select="@UID"/>,"<xsl:value-of select="../@Name"/>.<xsl:value-of select="@Alias"/>","<xsl:value-of select="@Name"/>","<xsl:value-of select="Description"/>",
</xsl:template>

<xsl:template match="UniStruct">
</xsl:template>

<xsl:template match="Description"></xsl:template>
<xsl:template match="Parameter"></xsl:template>
<xsl:template match="Result"></xsl:template>
<xsl:template match="Const"></xsl:template>
<xsl:template match="Value"></xsl:template>

</xsl:stylesheet>