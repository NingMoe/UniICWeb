////==========================================================================;
//
//
//  Copyright (c) 2007 - 2008  Lianchuang Corporation.  All Rights Reserved.
//
//--------------------------------------------------------------------------;
// UniStruct.h: interface for the UniStruct.
//
//@Author unifound
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections;

namespace UniStruct
{
	[Flags]
	public enum UTFLAG:uint
	{
		UTEMPTY     = 0x00000000,
		UTFILL		= 0x40000000,
		UTSTREAM    = 0x80000000,
		UTLOCK		= 0x20000000,
		UTVALID		= 0x10000000,
		UTDWORD		= ~UTSTREAM,
		
		DEFUNIDWTYPE = UTVALID,
		DEFUNISZTYPE = UTSTREAM | UTSTREAM,
	}
	public enum UTMASK:uint
	{
		UTTYPEMASK		= 0x80000000,
		UTSIZEMASK		= 0x0FFFFFFF,
		UTFLAGMASK		= 0xF0000000,
	}
	
	public class UniVarItem
	{
		public UTFLAG m_dwType;
		///
		//判断是否为Stream.
		//@return 是否为空.
		///
		///
		public bool IsStream()
		{
			return ((m_dwType & UTFLAG.UTSTREAM)!=0);
		}

		//判断是否为空.
		//@return 是否为空.
		///
		public bool IsEmpty()
		{
			return ((m_dwType & UTFLAG.UTFILL)==0);
		}
		///
		//判断是否锁住.
		//@return 是否锁住.
		///
		public bool IsLock()
		{
			return ((m_dwType & UTFLAG.UTLOCK)!=0);
		}
		///
		//锁住.
		///
		public void Lock()
		{
			m_dwType |= UTFLAG.UTLOCK;
		}
		///
		//解锁.
		///
		public void UnLock()
		{
			m_dwType &= ~UTFLAG.UTLOCK;
		}
		///
		//判断是否有效.
		//@return 是否有效.
		///
		public bool IsValid()
		{
			return ((m_dwType & UTFLAG.UTVALID)!=0);
		}
		///
		//设置有效值.
		//@param bIsValid 是否有效.
		///
		public void EnableValid(bool bIsValid)
		{
			if(bIsValid)
			{
				m_dwType |= UTFLAG.UTVALID;
			}else{
				m_dwType &= ~UTFLAG.UTVALID;
			}
		}
		public UTFLAG GenerateType(bool bIsStream,bool bIsEmpty,bool bIsLock,bool bIsValid)
		{
			UTFLAG dwType = 0;
			if(bIsStream)
			{
				dwType |= UTFLAG.UTSTREAM;
			}
			if(!bIsEmpty)
			{
				dwType |= UTFLAG.UTFILL;
			}
			if(bIsLock)
			{
				dwType |= UTFLAG.UTLOCK;
			}
			if(bIsValid)
			{
				dwType |= UTFLAG.UTVALID;
			}
			return dwType;
		}
	}
	
	public class UniSZ:UniVarItem
	{
		public byte[] m_pContent;
		
		public UniSZ()
		{
			m_dwType = UTFLAG.UTSTREAM | UTFLAG.UTVALID;
			m_pContent = null;
		}
		public UniSZ(UniSZ _value)
		{
			m_dwType = UTFLAG.UTSTREAM | UTFLAG.UTVALID;
			this.Set(_value);
		}
		public UniSZ(string _value)
		{
			m_dwType = UTFLAG.UTSTREAM | UTFLAG.UTVALID;
			this.Set(_value);
		}
		public override string ToString()
		{
			string sRet = "";
			try
			{
				sRet = CUniStruct<object>.ecodeStream.GetString(m_pContent);
				if(sRet.Length>0)
				{
				    char[] szTrim ={'\0'};
					sRet = sRet.TrimEnd(szTrim);
                }
                //sRet = System.Web.HttpContext.Current.Server.UrlEncode(sRet);
			}catch
			{
				sRet = "";
			}
			return sRet;
		}
		~UniSZ()
		{
			Empty();
		}

		////
		//数据赋值.
		//@param pValue 指定的数据内容.
		//@param dwSize 数据大小(BYTE为单位)
		////
		public void Set(byte[] pValue,uint nIndex,uint nSize)
		{
			if(IsLock())
			{
				return;
			}
			Empty();
			if(pValue != null && pValue.Length > 0)
			{
				if(pValue.Length < nIndex+nSize)
				{
					nSize = (uint)pValue.Length - nIndex;
				}
				m_dwType |= UTFLAG.UTFILL;
				m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
				m_dwType |= (UTFLAG)(nSize & (uint)UTMASK.UTSIZEMASK);
				m_pContent = new byte[nSize];
				Array.Copy(pValue,nIndex,m_pContent,0,nSize);
			}
		}
		////
		//获取数据大小.
		//@return 数据大小.
		////
		public uint Size()
		{
			//return m_dwType & UTMASK.UTSIZEMASK;
			if(m_pContent != null)
			{
				return (uint)m_pContent.Length;
			}else{
				return 0;
			}
		}
		////
		//获取数据内容.
		//@return 数据内容.
		////
		public byte[] Get()
		{
			return m_pContent;
		}
		////
		//清空数据.
		////
		public void Empty()
		{
			if(IsLock())
			{
				return;
			}
			if (m_pContent != null)
			{
				m_pContent = null;
			}
			m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
			m_dwType &= ~UTFLAG.UTFILL;
		}

		////
		//获取数据(BYTE*类型)
		//@return 返回BYTE *类型字符串.
		////
		public static implicit operator byte[](UniSZ _f)
		{
			return _f.Get();
		}

		public void Set(string _value)
		{
			if(IsLock())
			{
				return;
			}
			if (_value != null)
			{
				_value += "\0";
				uint dwSize = (uint)CUniStruct<object>.ecodeStream.GetByteCount(_value);
				m_pContent = (byte[])CUniStruct<object>.ecodeStream.GetBytes(_value).Clone();
				dwSize = (uint)m_pContent.Length;
				m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
				m_dwType |= UTFLAG.UTFILL | (UTFLAG)(dwSize & (uint)UTMASK.UTSIZEMASK);
			}
			else
			{
				m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
				m_dwType &= ~UTFLAG.UTFILL;
				m_pContent = null;
			}
		}

		public void Set(UniSZ _value)
		{
			if(IsLock())
			{
				return;
			}
			m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
			m_dwType |= _value.m_dwType & (UTFLAG)UTMASK.UTSIZEMASK;
			m_dwType |= _value.m_dwType & (UTFLAG)~UTFLAG.UTFILL;
			byte[] pFContent = _value.Get();
			m_pContent = (byte[])pFContent.Clone();
		}
	};

	////
	//联创 Unifound DWORD 定长简单类型.
	////
	public class UniDW:UniVarItem
	{
		public uint m_dwValue;

		public UniDW()
		{
			m_dwType = UTFLAG.UTVALID;
		}
		public UniDW(uint dwValue)
		{
			m_dwType = UTFLAG.UTVALID | UTFLAG.UTFILL;
			m_dwValue = dwValue;
		}

		public UniDW(int nValue)
		{
			m_dwType = UTFLAG.UTVALID | UTFLAG.UTFILL;
			m_dwValue = (uint)nValue;
		}
		////
		//取值.
		////
		public static implicit operator uint(UniDW _f)
		{
			if(_f == null)
			{
				return 0;
			}
			if(_f.IsEmpty())
			{
				return 0;
			}
			return _f.m_dwValue;
		}

		public static implicit operator int(UniDW _f)
		{
			if(_f == null)
			{
				return 0;
			}
			if(_f.IsEmpty())
			{
				return 0;
			}
			return (int)_f.m_dwValue;
		}
		////
		//清空数据.
		////
		public void Empty()
		{
			if(IsLock())
			{
				return;
			}
			m_dwValue = 0;
			m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
			m_dwType &= ~UTFLAG.UTFILL;
		}
		public override string ToString()
		{
			if(!IsEmpty())
			{
				return m_dwValue.ToString();
			}
			return "";
		}

		public void Set(uint s)
		{
			if(IsLock())
			{
				return;
			}
			m_dwType |= UTFLAG.UTFILL;
			m_dwValue = s;
		}

		public void Set(UniDW s)
		{
			if(IsLock())
			{
				return;
			}
			m_dwType &= (UTFLAG)UTMASK.UTFLAGMASK;
			m_dwType &= ~UTFLAG.UTFILL;
			m_dwType |= s.m_dwType & (UTFLAG)~UTFLAG.UTFILL;
			m_dwValue = s.m_dwValue;
		}
	};

	public abstract class CUniBaseStruct
	{
		abstract public int GetLength();
		abstract public string GetFieldName(int nIndex);
		abstract public string GetCoreFieldName(int nIndex);
		abstract public int GetFieldIndex(string sName);
		abstract public object GetValue(int nIndex);
		abstract public void SetValue(int nIndex,object _value);

		abstract public uint Import(byte[] pBuffer);   
		abstract public uint Import(byte[] pBuffer,uint nIndex);
		abstract public uint ExportSize();
		abstract public byte[] Export();
	}

	public abstract class CUniBaseStructArray
	{
		abstract public int GetLength();
		abstract public string GetFieldName(int i, int j);
		abstract public string GetCoreFieldName(int i, int j);

		abstract public int GetItemLength();
		abstract public int GetItemLength(int i);
		abstract public object GetCellValue(int i, int j);
		abstract public uint GetDWORDValue(int i, int j);
		abstract public string GetStrValue(int i, int j);


		abstract public uint Import(byte[] pBuffer);
		abstract public uint Import(byte[] pBuffer, uint _index);
		abstract public byte[] Export();
	}

	public enum UNISMAG:uint
	{
		//"UniS"
		UNISTRUCT = 0x53696E55,
		//"UnST"
		UNISARRAY = 0x54536E55,
	}
	public partial class CUniStruct<T>:CUniBaseStruct  where T: new()
	{
		//TODO:
		//public static Encoding ecodeStream = Encoding.GetEncoding("GB2312");
		public static Encoding ecodeStream = new UnicodeEncoding();

		//----------
		public T v;

		public CUniStruct()
		{
			v = new T();
		}
		public CUniStruct(T data)
		{
			this.v = data;
		}
		
		~CUniStruct()
		{
		}
		
		///
		//批设置有效字段.
		//@param szValid 有效标志字符串(0无效,1有效).
		///
		bool EnableValid(string szValid)
		{
			return EnableValid(v,szValid);
		}
		bool EnableValid(object srcObj, string szValid)
		{
			if (srcObj == null)
			{
				return false;
			}
			Type t = srcObj.GetType();
			FieldInfo[] fiarr = t.GetFields();
			if (fiarr == null)
			{
				return false;
			}
			int nIn = 0;
			CUniStruct<object> pUnkownObj = new CUniStruct<object>();
			uint nCount = (uint)fiarr.GetLength(0);
			for(uint i = 0; i < nCount; i++)
			{
				bool bIsValid = (szValid[nIn]=='1');
				object poValue = fiarr[i].GetValue(srcObj);
				Type itt = poValue.GetType();
				if(itt == typeof(UniSZ))
				{
					UniSZ pValue = (UniSZ)poValue;
					pValue.EnableValid(bIsValid);
					nIn++;
				}else if(itt == typeof(UniDW))
				{
					UniDW pValue = (UniDW)poValue;
					pValue.EnableValid(bIsValid);
					nIn++;
				}else{
					if(pUnkownObj.EnableValid(poValue,szValid.Substring(nIn, szValid.Length - nIn)) == false)
					{
						return false;
					}
				}
			}
			return true;
		}

		///
		//批锁字段.
		//@param szLock 锁标志字符串(0无锁,1锁).
		///
		bool Lock(string szLock)
		{
			return Lock(v,szLock);
		}
		bool Lock(object srcObj, string szLock)
		{
			if (srcObj == null)
			{
				return false;
			}
			Type t = srcObj.GetType();
			FieldInfo[] fiarr = t.GetFields();
			if (fiarr == null)
			{
				return false;
			}
			int nIn = 0;
			CUniStruct<object> pUnkownObj = new CUniStruct<object>();
			uint nCount = (uint)fiarr.GetLength(0);
			for(uint i = 0; i < nCount; i++)
			{
				bool bIsLock = (szLock[nIn]=='1');
				object poValue = fiarr[i].GetValue(srcObj);
				Type itt = poValue.GetType();
				if(itt == typeof(UniSZ))
				{
					UniSZ pValue = (UniSZ)poValue;
					if(bIsLock)
					{
						pValue.Lock();
					}else{
						pValue.UnLock();
					}
					nIn++;
				}else if(itt == typeof(UniDW))
				{
					UniDW pValue = (UniDW)poValue;
					if(bIsLock)
					{
						pValue.Lock();
					}else{
						pValue.UnLock();
					}
					nIn++;
				}else{
					if(pUnkownObj.Lock(poValue,szLock.Substring(nIn, szLock.Length - nIn)) == false)
					{
						return false;
					}
				}
			}
			return true;
		}
		
		public override int GetLength()
		{
			if (this.v == null)
			{
				return 0;
			}

			Type t = this.v.GetType();
			FieldInfo[] fiarr = t.GetFields();

			if (fiarr == null)
			{
				return 0;
			}
			return fiarr.GetLength(0);
		}

		public override string GetFieldName(int nIndex)
		{
			if (nIndex >= GetLength())
			{
				return null;
			}

			Type t = v.GetType();
			FieldInfo[] fiarr = t.GetFields();

			if (fiarr == null)
			{
				return null;
			}

			return t.Name + "." + fiarr[nIndex].Name;
		}

		public override string GetCoreFieldName(int nIndex)
		{
			if (nIndex >= GetLength())
			{
				return null;
			}
			Type t = v.GetType();
			FieldInfo[] fiarr = t.GetFields();
			if (fiarr == null)
			{
				return null;
			}
			return fiarr[nIndex].Name;
		}

		public override int GetFieldIndex(string sName)
		{
			string sStructName = null;
			string sFieldName = null;
			string [] sarr = sName.Split('.');
			if(sarr.Length > 2)
			{
				return -1;
			}
			if(sarr.Length == 1)
			{
				sFieldName = sarr[0];
			}else
			{
				sStructName = sarr[0];
				sFieldName = sarr[1];
			}

			Type t = v.GetType();
			if (sStructName != null)
			{
				if(sStructName != t.Name)
				{
					return -1;
				}
			}

			FieldInfo[] fiarr = t.GetFields();

			if (fiarr == null)
			{
				return -1;
			}

			for (int i = 0; i < fiarr.Length;i++ )
			{
				if(sFieldName == fiarr[i].Name)
				{
					return i;
				}
			}
			return -1;
		}

		public override object GetValue(int nIndex)
		{
			Type t = v.GetType();
			FieldInfo[] fiarr = t.GetFields();

			if (fiarr == null)
			{
				return null;
			}
			if (nIndex < 0 || nIndex >= fiarr.Length)
			{
				return null;
			}
			try
			{
				return fiarr[nIndex].GetValue(v);
			}catch(Exception e)
			{
				Logger.Trace("-->>nIndex = "+nIndex+","+e.ToString()+":");
				Logger.Trace(this);
				return null;
			}
		}
		public override void SetValue(int nIndex,object _value)
		{
			if (nIndex >= GetLength())
			{
				return ;
			}

			Type t = v.GetType();
			FieldInfo[] flds = t.GetFields();

			if (flds == null)
			{
				return ;
			}
			if(((UniVarItem)(flds[nIndex].GetValue(v))).IsLock())
			{
				return;
			}
			flds[nIndex].SetValueDirect(__makeref(v),_value);
		}

		public static uint byte2uint(byte[] value, uint nIndex)
		{
			uint retv = 0;
			retv = value[nIndex];
			retv |= ((uint)(value[nIndex + 1])) << 8;
			retv |= ((uint)(value[nIndex + 2])) << 16;
			retv |= ((uint)(value[nIndex + 3])) << 24;
			return retv;
		}
		public static byte[] uint2byte(uint value)
		{
			byte[] retv = new byte[4];
			retv[0] = (byte)value;
			retv[1] = (byte)(value >> 8);
			retv[2] = (byte)(value >> 16);
			retv[3] = (byte)(value >> 24);
			return retv;
		}
		public static void uint2byte(uint value,ref byte[] pOut,uint nIndex)
		{
			pOut[nIndex + 0] = (byte)value;
			pOut[nIndex + 1] = (byte)(value >> 8);
			pOut[nIndex + 2] = (byte)(value >> 16);
			pOut[nIndex + 3] = (byte)(value >> 24);
		}

		public override uint Import(byte[] pBuffer)
		{
			return Import(ref this.v,pBuffer, 0,true);
		}
		public override uint Import(byte[] pBuffer,uint _nIndex)
		{
			return Import(ref this.v,pBuffer, _nIndex,true);
		}
		public uint Import(ref T destObj,byte[] pBuffer,uint _nIndex)
		{
			return Import(ref destObj,pBuffer, _nIndex,true);
		}
		public uint Import(ref T destObj,byte[] pBuffer,uint _nIndex,bool bHavMag)
		{
			uint nIndex = _nIndex;
			try
			{
				if(pBuffer == null)
				{
					throw(new Exception("0"));
				}
				if (destObj == null)
				{
					destObj = new T();
				}
				Type t = destObj.GetType();
				FieldInfo[] fiarr = t.GetFields();
				if (fiarr == null)
				{
					throw(new Exception("1"));
				}
				if(bHavMag)
				{
					if(pBuffer.Length < nIndex + 4)
					{
						throw(new Exception("2"));
					}
					uint nValue = byte2uint(pBuffer,nIndex);nIndex += 4;
					if(nValue != (uint)UNISMAG.UNISTRUCT)
					{
						throw(new Exception("3,nIndex="+nIndex));
					}
				}
				CUniStruct<object> pUnkownObj = new CUniStruct<object>();
				uint nCount = (uint)fiarr.GetLength(0);
				UniVarItem emptyValue = new UniVarItem();
				for(uint i = 0; i < nCount; i++)
				{
					object poValue = fiarr[i].GetValue(destObj);
					Type itt = fiarr[i].FieldType;
					UniVarItem puiValue = emptyValue;
					if(pBuffer.Length < nIndex+4)
					{
						throw(new Exception("4"));
					}
					UTFLAG dwFType = (UTFLAG)byte2uint(pBuffer,nIndex);nIndex += 4;
					
					//Logger.Trace("Type="+itt.ToString()+",Name="+fiarr[i].Name+",dwFType="+dwFType.ToString("x"));
					if((poValue != null) && ((itt == typeof(UniSZ)) || (itt == typeof(UniDW))))
					{
						puiValue = (UniVarItem)poValue;
						if(!puiValue.IsValid())
						{
							continue;
						}
						if(((uint)puiValue.m_dwType & (uint)UTMASK.UTTYPEMASK) != ((uint)dwFType & (uint)UTMASK.UTTYPEMASK))
						{
							throw(new Exception("5,"+fiarr[i].Name+"(i="+i.ToString()+":nIndex="+nIndex.ToString()+")类型错误:dwFType="+dwFType.ToString("x")));
						}
					}
					if(itt == typeof(UniSZ))
					{
						
						UniSZ newValue = new UniSZ();
						uint nSize = (uint)(dwFType & (UTFLAG)UTMASK.UTSIZEMASK);
						if( (dwFType & UTFLAG.UTFILL) == 0)
						{
							nSize = 0;
						}
						if(pBuffer.Length < nIndex+nSize)
						{
							throw(new Exception("6,pBuffer.Length="+pBuffer.Length+",nIndex="+nIndex+",dwFType="+dwFType));
						}
						if(!puiValue.IsLock()
								&&((dwFType & UTFLAG.UTFILL) != 0)
								&&((dwFType & UTFLAG.UTLOCK) == 0)
								)
						{
							newValue.Set(pBuffer,nIndex,nSize);nIndex += nSize;
							fiarr[i].SetValueDirect(__makeref(destObj),newValue);
						}else{
							if(((dwFType&UTFLAG.UTFILL) !=0)
									&& ((dwFType&UTFLAG.UTLOCK) ==0))
							{
								nIndex += nSize;
							}
							if(!puiValue.IsLock())
							{
								fiarr[i].SetValueDirect(__makeref(destObj),newValue);
							}
						}
					}else if(itt == typeof(UniDW))
					{
						UniDW newValue = new UniDW();
						if(!puiValue.IsLock()
								&&((dwFType & UTFLAG.UTFILL) != 0)
								&&((dwFType & UTFLAG.UTLOCK) == 0)
								)
						{
							if(pBuffer.Length < nIndex+4)
							{
								throw(new Exception("8"));
							}
							uint nValue = byte2uint(pBuffer,nIndex);nIndex += 4;
							newValue.Set(nValue);
							fiarr[i].SetValueDirect(__makeref(destObj),newValue);
							//fiarr[i].SetValue(destObj,newValue);
						}else{
							if(((dwFType&UTFLAG.UTFILL) !=0)
									&& ((dwFType&UTFLAG.UTLOCK) ==0))
							{
								nIndex += 4;
							}
							if(!puiValue.IsLock())
							{
								fiarr[i].SetValueDirect(__makeref(destObj),newValue);
							}
						}
					}else{
						nIndex -= 4;
						uint nSize = pUnkownObj.Import(ref poValue,pBuffer,nIndex,false);
						fiarr[i].SetValueDirect(__makeref(destObj),poValue);
						if(nSize == 0)
						{
							throw(new Exception("9"));
						}
						nIndex += nSize;
					}
				}
			}
			catch(Exception e)
			{
				Logger.Trace("UniStruct.Import faild:"+e.Message+","+e.ToString());
				return 0;
			}
			return (uint)nIndex - _nIndex;
		}
		
		public override uint ExportSize()
		{
			return ExportSize(this.v,true);
		}
		
		public uint ExportSize(T srcObj,bool bHavMag)
		{
			if (srcObj == null)
			{
				return 0;
			}
			Type t = srcObj.GetType();
			FieldInfo[] fiarr = t.GetFields();
			if (fiarr == null)
			{
				return 0;
			}
			uint nExportSize = 0;
			if(bHavMag)
			{
				nExportSize += 4;
			}
			CUniStruct<object> pUnkownObj = new CUniStruct<object>();
			uint nCount = (uint)fiarr.GetLength(0);
			for(uint i = 0; i < nCount; i++)
			{
				object poValue = fiarr[i].GetValue(srcObj);
				Type itt = fiarr[i].FieldType;
				if((poValue != null) && ((itt == typeof(UniSZ)) || (itt == typeof(UniDW))))
				{
					UniVarItem puiValue = (UniVarItem)poValue;
					if(!puiValue.IsValid())
					{
						continue;
					}
				}
				nExportSize += 4;
				if(poValue == null)
				{
					continue;
				}
				if(itt == typeof(UniSZ))
				{
					UniSZ pValue = (UniSZ)poValue;
					if(!pValue.IsEmpty() && !pValue.IsLock())
					{
						nExportSize += pValue.Size();
					}
				}else if(itt == typeof(UniDW))
				{
					UniDW pValue = (UniDW)poValue;
					if(!pValue.IsEmpty() && !pValue.IsLock())
					{
						nExportSize += 4;
					}
				}else{
					nExportSize -= 4;
					uint nSize = pUnkownObj.ExportSize(poValue,false);
					nExportSize += nSize;
				}
			}
			return (uint)nExportSize;
		}

		public override byte[] Export()
		{
			return Export(this.v,true);
		}
		
		public byte[] Export(T srcObj,bool bHavMag)
		{
			byte[] pRet = new byte[ExportSize(srcObj,bHavMag)];
			if(Export(srcObj,bHavMag,ref pRet,0) == 0)
			{
				return null;
			}else{
				return pRet;
			}			
		}
		
		public uint Export(T srcObj,bool bHavMag,ref byte[] pOut,uint _nIndex)
		{
			if (srcObj == null || pOut == null || pOut.Length < _nIndex+4)
			{
				return 0;
			}
			Type t = srcObj.GetType();
			FieldInfo[] fiarr = t.GetFields();
			if (fiarr == null)
			{
				return 0;
			}
			uint nIndex = _nIndex;
			if(bHavMag)
			{
				uint2byte((uint)UNISMAG.UNISTRUCT,ref pOut,nIndex);nIndex += 4;
			}
			CUniStruct<object> pUnkownObj = new CUniStruct<object>();
			uint nCount = (uint)fiarr.GetLength(0);
			for(uint i = 0; i < nCount; i++)
			{
				object poValue = fiarr[i].GetValue(srcObj);
				Type itt = fiarr[i].FieldType;
				if((poValue != null) && ((itt == typeof(UniSZ)) || (itt == typeof(UniDW))))
				{
					UniVarItem puiValue = (UniVarItem)poValue;
					if(!puiValue.IsValid())
					{
						continue;
					}
				}
				if(itt == typeof(UniSZ))
				{
					if (pOut.Length < nIndex+4)
					{
						return 0;
					}
					if(poValue != null)
					{
						UniSZ pValue = (UniSZ)poValue;
						uint2byte((uint)pValue.m_dwType,ref pOut,nIndex);nIndex += 4;
						if(!pValue.IsEmpty() && !pValue.IsLock())
						{
							if (pOut.Length < nIndex+pValue.Size())
							{
								return 0;
							}
							Array.Copy(pValue.Get(),0,pOut,nIndex,pValue.Size());
							nIndex += pValue.Size();
						}
					}else{
						uint2byte((uint)UTFLAG.DEFUNISZTYPE,ref pOut,nIndex);nIndex += 4;
					}
				}else if(itt == typeof(UniDW))
				{
					if (pOut.Length < nIndex+4)
					{
						return 0;
					}
					if(poValue != null)
					{
						UniDW pValue = (UniDW)poValue;
						uint2byte((uint)pValue.m_dwType,ref pOut,nIndex);nIndex += 4;
						if(!pValue.IsEmpty() && !pValue.IsLock())
						{
							if (pOut.Length < nIndex+4)
							{
								return 0;
							}
							uint2byte((uint)pValue.m_dwValue,ref pOut,nIndex);nIndex += 4;
						}
					}else{
						uint2byte((uint)UTFLAG.DEFUNIDWTYPE,ref pOut,nIndex);nIndex += 4;
					}
				}else{
					uint nSize = pUnkownObj.Export(poValue,false,ref pOut,nIndex);
					nIndex += nSize;
				}
			}
			return nIndex - _nIndex;
		}
	}

	public class CUniStructArray<T> : CUniBaseStructArray where T:new()
	{
		//public CUniStruct<T>[] m_Data;
		public Hashtable m_Data;
		
		public CUniStruct<T> this[int it]
		{
			get
			{
				if (it >= GetLength())
				{
					return null;
				}
				return (CUniStruct<T>)m_Data[(uint)it];
			}
			set
			{
				m_Data[(uint)it] = value;
			}
		}
		public CUniStruct<T> this[uint it]
		{
			get
			{
				if (it >= GetLength())
				{
					return null;
				}
				return (CUniStruct<T>)m_Data[(uint)it];
			}
			set
			{
				m_Data[(uint)it] = value;
			}
		}
		
		public CUniStructArray()
		{
			m_Data = new Hashtable();
		}
		public CUniStructArray(uint nLength)
		{
			if (nLength == 0)
			{
				m_Data = new Hashtable();
			}
			else
			{
				m_Data = new Hashtable((int)nLength);
				for (uint i = 0; i < nLength; i++)
				{
					m_Data[i] = new CUniStruct<T>();
				}
			}
		}

		public override int GetLength()
		{
			if (m_Data == null)
			{
				return 0;
			}

			return m_Data.Count;
		}

		//获取数组中单个元素中字段的个数
		public override int GetItemLength()
		{
			if((m_Data == null))
			{
				return 0;
			}
			foreach(CUniStruct<T> vrItem in m_Data)
			{
				return vrItem.GetLength();
			}
			return 0;
		}

		public override int GetItemLength(int i)
		{
			if ((m_Data == null) || (i >= GetLength()))
			{
				return 0;
			}
			return ((CUniStruct<T>)m_Data[i]).GetLength();
		}

		//获取单元格的值
		public override object GetCellValue(int i, int j)
		{
			if ((m_Data == null) || (i >= GetLength()))
			{
				return null;
			}

			return ((CUniStruct<T>)m_Data[i]).GetValue(j);
		}

		public override uint GetDWORDValue(int i, int j)
		{
			object oValue = GetCellValue(i, j);
			uint uValue = 0;

			if (oValue.GetType() == typeof(int))
			{
				int nValue = (int)oValue;
				uValue = (uint)nValue;
			}
			else if (oValue.GetType() == typeof(uint))
			{
				uValue = (uint)oValue;
			}
			else if (oValue.GetType() == typeof(UniDW))
			{
				uValue = (uint)(UniDW)oValue;
			}

			return uValue;
		}

		public override string GetStrValue(int i, int j)
		{
			return (string)GetCellValue(i, j);
		}

		//public override uint GetCellValue(int i, int j)
		//{
		//    if ((m_Data == null) || (i >= GetLength()))
		//    {
		//        return null;
		//    }

		//    return m_Data[i].GetValue(j);
		//}

		public override string GetFieldName(int i, int j)
		{
			if ((m_Data == null) || (i >= GetLength()))
			{
				return null;
			}

			return ((CUniStruct<T>)m_Data[i]).GetFieldName(j);
		}

		public override string GetCoreFieldName(int i, int j)
		{
			if ((m_Data == null) || (i >= GetLength()))
			{
				return null;
			}

			return ((CUniStruct<T>)m_Data[i]).GetCoreFieldName(j);
		}

		public override uint Import(byte[] pBuffer)
		{
			return Import(pBuffer, 0);
		}

		public override uint Import(byte[] pBuffer, uint _index)
		{
			uint nIndex = _index;
			try
			{
				if (pBuffer == null || pBuffer.Length < 8)
				{
					throw(new Exception("0"));
				}
				uint nMag = CUniStruct<T>.byte2uint(pBuffer, nIndex);
				if (nMag != (uint)UNISMAG.UNISARRAY)
				{
					throw(new Exception("1"));
				}
				nIndex += 4;
				uint nLength = CUniStruct<T>.byte2uint(pBuffer, nIndex);
				nIndex += 4;
				m_Data = null;
				m_Data = new Hashtable((int)nLength);
				for (uint i = 0; i < nLength; i++)
				{
					CUniStruct<T> vrItem = new CUniStruct<T>();
					uint nImported = vrItem.Import(pBuffer, nIndex);
					if (nImported == 0)
					{
						m_Data = null;
						throw(new Exception("2,index="+i));
					}
					m_Data[i] = vrItem;
					nIndex += nImported;
				}
			}catch(Exception e)
			{
				Logger.Trace("UniStructArray faild:"+e.Message);
				return 0;
			}
			return (uint)nIndex - _index;
		}

		public override byte[] Export()
		{
			uint nExportSize = 8;
			uint nDindex = 0;
			byte[] retBytes = null;
			byte[] bytes = null;
			uint nLen = 0;
			if (m_Data != null)
			{
				for (uint i = 0; i < m_Data.Count; i++)
				{
					if (m_Data[i] == null)
					{
						continue;
					}
					CUniStruct<T> vrItem = (CUniStruct<T>)m_Data[i];
					nExportSize += vrItem.ExportSize();
					nLen++;
				}
			}
			retBytes = new byte[nExportSize];
			CUniStruct<T>.uint2byte((uint)UNISMAG.UNISARRAY,ref retBytes,nDindex);
			nDindex += 4;
			CUniStruct<T>.uint2byte((uint)nLen,ref retBytes,nDindex);
			nDindex += 4;
			if (m_Data != null)
			{
				for (uint i = 0; i < m_Data.Count; i++)
				{
					if (m_Data[i] == null)
					{
						continue;
					}
					CUniStruct<T> vrItem = (CUniStruct<T>)m_Data[i];
					bytes = vrItem.Export();
					Array.Copy(bytes, 0, retBytes, nDindex, bytes.Length);
					nDindex += (uint)bytes.Length;
				}
			}
			return retBytes;
		}
	}
}
