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
using System.Configuration;
using System.Xml;
using UniStruct;
<![CDATA[
namespace UniWebLib]]><![CDATA[
{
	#region 共用部分
	public class UniRequest
	{
		public UniDCom m_UniDCom = null;]]>
		public ErrorHandler OnError
		{
			set{
			<xsl:for-each select="./Module">
				<xsl:value-of select="@Name"/>.OnError = value;
				</xsl:for-each>
			}
		}
<xsl:for-each select="./Module">
		public PR<xsl:value-of select="@Name"/><xsl:text> </xsl:text><xsl:value-of select="@Name"/> = null;</xsl:for-each>
		public UniRequest()
		{
			m_UniDCom = new UniDCom();
<xsl:for-each select="./Module">
<![CDATA[			]]><xsl:value-of select="@Name"/> = new PR<xsl:value-of select="@Name"/>(m_UniDCom);
</xsl:for-each>
		}
<![CDATA[

		public string szErrMessage
		{
			get
			{
				string szMessage;
				m_UniDCom.GetLastErr(out szMessage);
				return szMessage;
			}
		}
		public string szErrMsg
		{
			get
			{
				string szMessage;
				m_UniDCom.GetLastErr(out szMessage);
				return szMessage;
			}
		}
		public uint GetLastErr(out string strErrMsg)
		{
			uint nRet = m_UniDCom.GetLastErr(out strErrMsg);
			return nRet;
		}
	};
	public partial class PRModule
	{
		protected uint m_nModule = 0;
		protected UniDCom m_UniDCom = null;
		byte[] detail = null;
		uint m_uLastCommand = 0;

		public PRModule(UniDCom _UniDCom)
		{
			m_UniDCom = _UniDCom;
		}
		public PRModule()
		{
			m_UniDCom = null;
			m_nModule = 0;
		}
		public uint GetCMD(uint nSubCmdCode)
		{
			uint uCmd = m_nModule | nSubCmdCode;
			m_uLastCommand = uCmd;
			return uCmd;
		}
    
    /*==================================================
    
		public bool GetDetail<T>(out CUniStruct<T> vrDetail) where T:new()
		{
			vrDetail = new CUniStruct<T>();
			if(detail == null)
			{
				return false;
			}
			Import(vrDetail, detail);
			return true;
		}
		public bool GetDetail<T>(out CUniStructArray<T> vtDetail) where T:new()
		{
			vtDetail = new CUniStructArray<T>();
			if(detail == null)
			{
				return false;
			}
			Import(vtDetail, detail);
			return true;
		}
		public REQUESTCODE Cmd<T>(uint uCmd,out CUniStructArray<T> vtResult) where T:new()
		{
			return Cmd<T,T>(uCmd,null,out vtResult);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,CUniStruct<T> vrInput) where T:new()
		{
			CUniStructArray<T> result = null;
			return Cmd<T,T>(uCmd,vrInput,out result);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,out CUniStruct<T> vrResult) where T:new()
		{
			CUniStruct<T> vrInput = null;
			return Cmd<T,T>(uCmd,vrInput,out vrResult);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,CUniStructArray<T> vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE Cmd(uint uCmd)
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, null, out result);
			return uRequest;
		}
		public REQUESTCODE Cmd<T,V>(uint uCmd,CUniStruct<T> vrInput,out CUniStructArray<V> vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vtResult = new CUniStructArray<V>();
				uRequest = Import(vtResult, result);
			}
			return uRequest;
		}
		public REQUESTCODE Cmd<T,V>(uint uCmd,CUniStruct<T> vrInput,out CUniStruct<V> vrResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = null;
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new CUniStruct<V>();
				uRequest = Import(vrResult, result);
			}
			return uRequest;
		}

		protected REQUESTCODE Import<T>(CUniStructArray<T> vtRet, byte[] result) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			if (result != null && result.Length > 0)
			{
				n = vtRet.Import(result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			
			if(result != null && result.Length - n > 0)
			{
				byte[] newdetail = new byte[result.Length - n];
				Array.Copy(result,n,newdetail,0,result.Length - n);
				detail = newdetail;
			}else{
				detail = null;
			}
			return uRequest;
		}
		protected REQUESTCODE Import<T>(CUniStruct<T> vrRet, byte[] result) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;

			if (result != null && result.Length > 0)
			{
				n = vrRet.Import(result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			if(result != null && result.Length - n > 0)
			{
				byte[] newdetail = new byte[result.Length - n];
				Array.Copy(result,n,newdetail,0,result.Length - n);
				detail = newdetail;
			}else{
				detail = null;
			}
			return uRequest;
		}
    
    ==================================================*/
    //==================================================
    	public bool UTPeekDetail<T>(out T vrDetail) where T:new()
		{
			vrDetail = new T();
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vrDetail, detail, false);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name);
			}
			return true;
		}
		public bool UTPeekDetail<T>(out T[] vtDetail) where T:new()
		{
			vtDetail = null;
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vtDetail, detail, false);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name+"[]");
			}
			return true;
		}
		public bool UTGetDetail<T>(out T vrDetail) where T:new()
		{
			vrDetail = new T();
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vrDetail, detail, true);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name);
			}
			return true;
		}
		public bool UTGetDetail<T>(out T[] vtDetail) where T:new()
		{
			vtDetail = null;
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vtDetail, detail, true);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name+"[]");
			}
			return true;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,out T[] vtResult) where T:new()
		{
			return UTCmd<T,T>(uCmd,out vtResult);
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,T vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,out T vrResult) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = new T();
			byte[] result = null;
			byte[] input = null;

			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new T();
				uRequest = UTImport(out vrResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,T[] vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructArrayCS ucacs = new UniStructCS.CUniStructArrayCS();
			if(vrInput != null)
			{
				input = ucacs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd(uint uCmd)
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, null, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,T vrInput,out V[] vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				uRequest = UTImport(out vtResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,out V[] vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				uRequest = UTImport(out vtResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,T vrInput,out V vrResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = new V();
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}

			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new V();
				uRequest = UTImport(out vrResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		protected REQUESTCODE UTImport<T>(out T[] vtRet, byte[] result, bool bNoPeek) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			vtRet = (T[])(object)null;
			if (result != null && result.Length > 0)
			{
				UniStructCS.CUniStructArrayCS ucacs = new UniStructCS.CUniStructArrayCS();
				n = ucacs.Import(out vtRet,result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			
			if(bNoPeek)
			{
				if(result != null && result.Length - n > 0)
				{
					byte[] newdetail = new byte[result.Length - n];
					Array.Copy(result,n,newdetail,0,result.Length - n);
					detail = newdetail;
				}else{
					detail = null;
				}
			}
			return uRequest;
		}
		protected REQUESTCODE UTImport<T>(out T vrRet, byte[] result,bool bNoPeek) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			vrRet = new T();
			if (result != null && result.Length > 0)
			{
				UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
				n = uccs.Import(out vrRet,result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			if(bNoPeek)
			{
				if(result != null && result.Length - n > 0)
				{
					byte[] newdetail = new byte[result.Length - n];
					Array.Copy(result,n,newdetail,0,result.Length - n);
					detail = newdetail;
				}else{
					detail = null;
				}
			}
			return uRequest;
		}
	};
	#endregion 共用部分
]]>
<xsl:apply-templates name="./">
</xsl:apply-templates>}
//</pre>
</xsl:template>

<xsl:template match="Module">
	#region PR<xsl:value-of select="@Name"/>部分
	/*<xsl:value-of select="Description"/>*/
	public partial class PR<xsl:value-of select="@Name"/>:PRModule
	{
		public PR<xsl:value-of select="@Name"/>(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = <xsl:value-of select="translate(@Name,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>_BASE;
		}
		<xsl:apply-templates name="./"></xsl:apply-templates>	
	}
	#endregion PR<xsl:value-of select="@Name"/>部分
</xsl:template>

<xsl:template match="Commands">
<xsl:apply-templates name="./">
</xsl:apply-templates>
</xsl:template>


<xsl:template match="Command">
		/*public REQUESTCODE <xsl:value-of select="@Alias"/>(<xsl:if test="Parameter and Parameter!=''">CUniStruct<xsl:if test="Parameter/@Array='true'">Array</xsl:if><![CDATA[<]]><xsl:value-of select="Parameter"/><![CDATA[>]]> vrParameter</xsl:if><xsl:if test="Parameter and Parameter!='' and Result and Result!=''">,</xsl:if><xsl:if test="Result and Result!=''">out CUniStruct<xsl:if test="Result/@Array='true'">Array</xsl:if><![CDATA[<]]><xsl:value-of select="Result"/><![CDATA[>]]> vrResult</xsl:if>)
		{
			return Cmd(<xsl:value-of select="@Name"/><xsl:if test="Parameter and Parameter!=''">,vrParameter</xsl:if><xsl:if test="Result and Result!=''">,out vrResult</xsl:if>);
		}*/
		
		public REQUESTCODE <xsl:value-of select="@Alias"/>(<xsl:if test="Parameter and Parameter!=''"><xsl:value-of select="Parameter"/><xsl:if test="Parameter/@Array='true'">[]</xsl:if><![CDATA[ ]]> vrParameter</xsl:if><xsl:if test="Parameter and Parameter!='' and Result and Result!=''">,</xsl:if><xsl:if test="Result and Result!=''">out <xsl:value-of select="Result"/><xsl:if test="Result/@Array='true'">[]</xsl:if><![CDATA[ ]]> vrResult</xsl:if>)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(<xsl:value-of select="@Name"/><xsl:if test="Parameter and Parameter!=''">,vrParameter</xsl:if><xsl:if test="Result and Result!=''">,out vrResult</xsl:if>);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(<xsl:value-of select="@Name"/><xsl:if test="Parameter and Parameter!=''">,vrParameter</xsl:if><xsl:if test="Result and Result!=''">,out vrResult</xsl:if>);
			}
		}

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
