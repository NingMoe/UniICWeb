using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using UniStruct;
using UniWebLib;
using System.Reflection;
using System.Threading;
using UniLibrary;

public class UniSoapHeader : SoapHeader
{
    private uint m_szSessionID = 0;
    private uint m_szStationSN = 1;

    public UniSoapHeader()
    {
    }
    public UniSoapHeader(uint szSessionID, uint szStationSN)
    {
        m_szSessionID = szSessionID;
        m_szStationSN = szStationSN;
    }

    public uint SessionID
    {
        get { return m_szSessionID; }
        set { m_szSessionID = value; }
    }
    public uint StationSN
    {
        get { return m_szStationSN; }
        set { m_szStationSN = value; }
    }
}

/// <summary>
/// UniLab 的摘要说明
/// </summary>
public abstract partial class UniBaseService : System.Web.Services.WebService
{
    SessionMng sessionMng = new SessionMng();
    public UniRequest GetRequest()
    {
        return sessionMng.GetRequest(soaphead.SessionID, soaphead.StationSN);
    }
    public void ReleaseRequest()
    {
        if (soaphead.SessionID != 0)
        {
            SysConsole.ReleaseSession(soaphead.SessionID.ToString());
        }

        sessionMng.ReleaseRequest(soaphead.SessionID);
    }
    public UniRequest m_Request
    {
        get
        {
            return GetRequest();
        }
    }

    [WebMethod (Description = "关闭连接")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    public void
    Release(out uint code, out string Message)
    {
        code = 0;
        Message = "";
        ReleaseRequest();

        return;
    }

    public UniBaseService () {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    public UniSoapHeader soaphead = new UniSoapHeader();

    static public void trace(string szMessage)
    {
        Logger.Trace(szMessage);
    }

    static public void TRACE(string szMessage)
    {
        Logger.Trace(szMessage);
    }

    static public void Trace(string szMessage)
    {
        Logger.Trace(szMessage);
    }

    public uint ToUint(string szValue)
    {
        uint nValue = 0;
        uint.TryParse(szValue, out nValue);
        return nValue;
    }
    public uint ToUint(uint? szValue)
    {
        if (szValue == null)
            return 0;
        else
            return (uint)szValue;
    }

    public string GetRoomSNFromSN(string szPCSN)
    {
        if (szPCSN.Length < 10)
        {
            return "";
        }
        return szPCSN.Substring(0, 10);
    }
    public string GetStaSNFromSN(string szPCSN)
    {
        if (szPCSN.Length < 6)
        {
            return "";
        }
        return szPCSN.Substring(0, 6);
    }

    /*
    public object Struct2Struct2<T>(T vrStruct) where T : new()
    {
        object ret;
        string szNewType = typeof(T).Name;
        szNewType = szNewType.Replace("UT_", "PRMSCommon.");
        
        Type t3 = Type.GetType(szNewType);
        ret = Activator.CreateInstance(t3);

        Type t = typeof(T);
        Type t2 = ret.GetType();
        FieldInfo[] fiarr = t.GetFields();
        FieldInfo[] fiarr2 = t2.GetFields();
        uint nCount = (uint)fiarr2.GetLength(0);
        for (uint i = 0; i < nCount; i++)
        {
            object poValue1 = fiarr[i].GetValue(vrStruct);
            object p2 = fiarr2[i].GetValue(ret);
            if (poValue1.GetType() == typeof(uint))
            {
                //UniDW pd = (UniDW)p2;
               // pd.Set((uint)poValue1);
                fiarr2[i].SetValueDirect(__makeref(ret), new UniDW((uint)poValue1));
            }
            else if (poValue1.GetType() == typeof(string))
            {
                //UniSZ ps = (UniSZ)p2;
               // ps.Set((string)poValue1);
                fiarr2[i].SetValueDirect(__makeref(ret), new UniSZ((string)poValue1));
            }
        }
        return ret;
    }

    public void Struct2Struct<T1, T2>(T1 vrStruct, out T2 ret) where T2 : new()
    {
        ret = new T2();

        Type t1 = typeof(T1);
        Type t2 = typeof(T2);
        FieldInfo[] fiarr1 = t1.GetFields();
        FieldInfo[] fiarr2 = t2.GetFields();
        uint nCount = (uint)fiarr2.GetLength(0);
        for (uint i = 0; i < nCount; i++)
        {
            object p1 = fiarr1[i].GetValue(vrStruct);
            object p2 = fiarr2[i].GetValue(ret);
            Type t1m = p1.GetType();
            if (t1m == typeof(uint))
            {
                fiarr2[i].SetValueDirect(__makeref(ret), new UniDW((uint)p1));
            }
            else if (t1m == typeof(string))
            {
                fiarr2[i].SetValueDirect(__makeref(ret), new UniSZ((string)p1));
            }
            else if (t1m == typeof(UniDW))
            {
                fiarr2[i].SetValueDirect(__makeref(ret), ((UniDW)p1).m_dwValue);
            }
            else if (t1m == typeof(UniSZ))
            {
                fiarr2[i].SetValueDirect(__makeref(ret), ((UniSZ)p1).ToString());
            }
        }
    }

    public void UTStruct2StructArray<T1, T2>(CUniStructArray<T1> vtStruct, out T2[] retarray)
        where T1 : new()
        where T2 : new()  
    {
        retarray = new T2[vtStruct.GetLength()];
        for (int i = 0; i < vtStruct.GetLength(); i++)
        {
            Struct2Struct(vtStruct[i].v, out retarray[i]);
        }
    }
    */
}
