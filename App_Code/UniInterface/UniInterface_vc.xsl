<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet version="2.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" indent="no" media-type="text/plain" encoding="GB2312"/>

<xsl:template match="UniInterface">
//<pre>
/*<xsl:value-of select="Description"/>*/
#ifndef _<xsl:value-of select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>_UNIINTERFACE_H_
#define _<xsl:value-of select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>_UNIINTERFACE_H_

<xsl:if test="count(./Const)>0">
//��ʼ��������<xsl:for-each select="./Const">
<xsl:if test="@Type='String'">
#define <xsl:value-of select="@Name"/><xsl:text>	</xsl:text>_T(<xsl:value-of select="Value"/>)<xsl:if test="Description !=''">		/*<xsl:value-of select="Description"/>*/</xsl:if>
</xsl:if>
<xsl:if test="@Type!='String'">
#define <xsl:value-of select="@Name"/><xsl:text>	</xsl:text><xsl:value-of select="Value"/><xsl:if test="Description !=''">		/*<xsl:value-of select="Description"/>*/</xsl:if>
</xsl:if>
</xsl:for-each>
//������������

</xsl:if>

<xsl:apply-templates name="./">
</xsl:apply-templates>
#endif
//</pre>
</xsl:template>

<xsl:template name="UniSystem" match="UniSystem">
/*==========================<xsl:value-of select="Description"/>==========================*/
<xsl:apply-templates name="./">
</xsl:apply-templates>
/*====================================================*/
</xsl:template>

<xsl:template name="Systems" match="Systems">
/*-----------��ʼ��ϵͳ-----------*/
<xsl:apply-templates name="./">
</xsl:apply-templates>
/*-----------������ϵͳ-----------*/
</xsl:template>

<xsl:template name="CommonModules" match="CommonModules">
/*-----------��ʼ����ģ��-----------*/
<xsl:apply-templates name="./">
</xsl:apply-templates>
/*-----------��������ģ��-----------*/
</xsl:template>

<xsl:template name="System" match="System">
/*<xsl:value-of select="Description"/>*/
#define <xsl:value-of select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/><xsl:text>	</xsl:text><xsl:value-of select="@UID"/>
</xsl:template>

<xsl:template name="CommonModule" match="CommonModule">
/*<xsl:value-of select="Description"/>*/
#define <xsl:value-of select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/><xsl:text>	</xsl:text><xsl:value-of select="@UID"/>
</xsl:template>


<xsl:template name="Module" match="Module">
/*==========================��ʼ�ӿ�-<xsl:value-of select="@Name"/>-<xsl:value-of select="Description"/>==========================*/
<xsl:apply-templates name="./">
</xsl:apply-templates>
/*==========================�����ӿ�-<xsl:value-of select="@Name"/>==========================*/
</xsl:template>

<xsl:template name="Commands" match="Commands">
/*-----------��ʼ����-----------*/
#define <xsl:value-of select="translate(../@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/><xsl:text>_BASE	</xsl:text><xsl:value-of select="@UID"/>
<xsl:apply-templates name="./">
</xsl:apply-templates>
/*-----------��������-----------*/
</xsl:template>

<xsl:template name="UniStructes" match="UniStructes">
<xsl:if test="count(UniStruct)>0">/*��ʼ���ݽṹ*/</xsl:if>
<xsl:if test="count(./Const)>0">//��ʼ��������
<xsl:for-each select="./Const">
<xsl:if test="@Type='String'">
#define <xsl:value-of select="@Name"/><xsl:text>	</xsl:text>_T(<xsl:value-of select="Value"/>)<xsl:if test="Description !=''">		/*<xsl:value-of select="Description"/>*/</xsl:if>
</xsl:if>
<xsl:if test="@Type!='String'">
#define <xsl:value-of select="@Name"/><xsl:text>	</xsl:text><xsl:value-of select="Value"/><xsl:if test="Description !=''">		/*<xsl:value-of select="Description"/>*/</xsl:if>
</xsl:if>
</xsl:for-each>
//������������</xsl:if>
<xsl:apply-templates name="./">
</xsl:apply-templates>
<xsl:if test="count(UniStruct)>0">/*�������ݽṹ*/</xsl:if>
</xsl:template>

<xsl:template name="Command" match="Command">

/*<xsl:value-of select="Description"/> �����:<xsl:value-of select="Parameter"/>,Ӧ���:<xsl:value-of select="Result"/><xsl:if test="Result=''">null</xsl:if><xsl:if test="Result/@Array='true'">(Array)</xsl:if>*/
#define <xsl:value-of select="@Name"/><xsl:text>	(</xsl:text><xsl:value-of select="translate(../../@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>_BASE + <xsl:value-of select="@UID"/>)
</xsl:template>

<xsl:template name="UniStruct" match="UniStruct">
<xsl:for-each select="./Field">
<xsl:if test="count(./Const) > 0">
//<xsl:value-of select="../@Name"/>.<xsl:value-of select="@Name"/>����<xsl:for-each select=".//Const">
#define <xsl:value-of select="@Name"/><![CDATA[ 		]]><xsl:value-of select="Value"/><xsl:if test="Description !=''">		/*<xsl:value-of select="Description"/>*/</xsl:if>
</xsl:for-each>
</xsl:if>
</xsl:for-each>
/*<xsl:value-of select="Description"/>*/
typedef struct{
<xsl:apply-templates name="./">
</xsl:apply-templates>
}<xsl:value-of select="@Name"/>;
</xsl:template>

<xsl:template name="Field" match="Field"><![CDATA[    ]]><xsl:value-of select="@Type"/><xsl:text> </xsl:text><xsl:value-of select="@Name"/><xsl:if test="@Array">[<xsl:value-of select="@Array"/>]</xsl:if>;		/*<xsl:value-of select="Description"/>*/
</xsl:template>

<xsl:template match="Description"></xsl:template>
<xsl:template match="Parameter"></xsl:template>
<xsl:template match="Result"></xsl:template>
<xsl:template match="Const"></xsl:template>
<xsl:template match="Value"></xsl:template>

</xsl:stylesheet>
