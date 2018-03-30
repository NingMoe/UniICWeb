using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using UniStruct;
using System.Reflection;
using log4net;

// Load the configuration from  file
//[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension="log4net", Watch=true)]
[assembly: log4net.Config.XmlConfigurator(ConfigFile="log4net.xml", Watch=true)]

public class Logger
{
	private static ILog m_log = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

	static public void Trace(string szMessage)
	{
		if(m_log == null)
		{
			m_log = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);
		}
		if (m_log.IsDebugEnabled)
		{
			System.Diagnostics.StackFrame sf = null;
			System.Reflection.MethodBase caller = null;
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
			int i = 0;
			for(i = 0; i < st.FrameCount;i++)
			{
				sf = st.GetFrame(i);
				System.Reflection.MethodBase tcaller = sf.GetMethod();
				if(string.IsNullOrEmpty(tcaller.DeclaringType.FullName))
				{
					caller = tcaller;
					break;
				}
				if(string.Compare(tcaller.DeclaringType.FullName,MethodInfo.GetCurrentMethod().DeclaringType.FullName,true) == 0)
				{
					continue;
				}
				if(string.Compare(tcaller.Name,MethodInfo.GetCurrentMethod().Name,true) == 0)
				{
					continue;
				}
				caller = tcaller;
				break;
			}
			if (i >= st.FrameCount)
			{
				m_log.Debug(szMessage);
				return;
			}
			System.Diagnostics.StackFrame locationFrame = st.GetFrame(i);
			string szFileName = locationFrame.GetFileName();
			try
			{
				szFileName = szFileName.Substring(szFileName.LastIndexOf('\\')+1);
			}catch{
				szFileName = "";
			}
			string szCaller = "("+szFileName+":"+String.Format("{0,5:D}",locationFrame.GetFileLineNumber())+") ";
			if(caller != null)
			{
				if(string.IsNullOrEmpty(caller.DeclaringType.Namespace))
				{
					//szCaller += "鍏ㄥ眬.";
				}
				if(!string.IsNullOrEmpty(caller.DeclaringType.FullName))
				{
					szCaller += caller.DeclaringType.FullName + ".";
				}
				if(!string.IsNullOrEmpty(caller.Name))
				{
					szCaller += caller.Name;
				}else{
					//szCaller += "鏈煡鍑芥暟";
					szCaller += "?";
				}
			}
			m_log.Debug(szCaller + ">"+ szMessage);
		}
	}

    static public void Trace<T>(T vrObject) where T : new()
    {
        if (vrObject == null)
        {
            Logger.Trace("null");
            return;
        }

        Type t = vrObject.GetType();

        UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();

        string[] sfiarr = UniWebLib.Struct_ST.GetStructMember(t.Name);

        if (sfiarr == null)
        {
            FieldInfo[] ttfiarr = t.GetFields();
            sfiarr = new string[ttfiarr.Length];
            for (int fi = 0; fi < ttfiarr.Length; fi++)
            {
                sfiarr[fi] = ttfiarr[fi].Name;
            }
            //throw (new Exception("1," + t.Name));
        }

        string szMessage = t.Name + "{\r\n";
        for (int i = 0; i < sfiarr.Length; i++)
        {
            FieldInfo fino = t.GetField(sfiarr[i]);
            if (fino == null) continue;
            Type itt = fino.FieldType;

            if (!itt.IsPublic)
            {
                continue;
            }
            //一般Reserved只放在第一个成员，为加速优化
            if (i == 0)
            {
                if (itt == typeof(Reserved))
                {
                    continue;
                }
            }
            object poValue = fino.GetValue(vrObject);

            szMessage += sfiarr[i] + ": ";
            if (poValue == null)
            {
                szMessage += "<Empty>\r\n";
            }
            else
            {
                szMessage += poValue.ToString() + "\r\n";
            }
        }
        szMessage += "}";
        Logger.Trace(szMessage);
    }

    static public void Trace<T>(CUniStruct<T> vrObject) where T : new()
    {
		if(vrObject == null)
		{
			Logger.Trace("null");
			return;
		}
		string szMessage = vrObject.GetType().ToString()+"{\r\n";
		for (int i = 0; i < vrObject.GetLength(); i++)
		{
			szMessage += vrObject.GetCoreFieldName(i) + ": ";
			object Value = vrObject.GetValue(i);
			if (Value == null)
			{
				szMessage += "<Empty>\r\n";
			}else if(Value.GetType() == typeof(UniSZ) && ((UniSZ)Value).IsEmpty())
			{
				szMessage += "<Empty>\r\n";
			}else if(Value.GetType() == typeof(UniDW) && ((UniDW)Value).IsEmpty())
			{
				szMessage += "<Empty>\r\n";
			}else
			{
				szMessage += Value.ToString()+"\r\n";
			}
		}
		szMessage += "}";
		Logger.Trace(szMessage);
	}
	
	static public void Trace(byte[] pObject)
	{
		if(pObject == null)
		{
			Logger.Trace("null");
			return;
		}
		string szMessage = "byte[]{";
		for (int i = 0; i < pObject.Length; i++)
		{
			szMessage += i.ToString() +":0x"+pObject[i].ToString("X2")+",";
		}
		szMessage += "}";
		Logger.Trace(szMessage);
	}

	static public void trace(string szMessage)
	{
		Logger.Trace(szMessage);
	}

	static public void TRACE(string szMessage)
	{
		Logger.Trace(szMessage);
	}
};
