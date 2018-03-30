using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ConfigConst 的摘要说明
/// </summary>
public static class ConfigConst
{
    public static string GCLabName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["LabName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSysName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCRoomName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["RoomName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCDevName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCSysKind
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysKind"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCKindName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["KindName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSchoolCode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SchoolCode"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCTurtorReacher
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["TurtorReacher"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCDevListMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevListMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCDevListCol
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevListCol"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCResvCheck
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ResvCheck"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCDeptName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DeptName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCDevAndKind
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevAndKind"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCDebug
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["Debug"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCClassName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ClassName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return (s);
            }
        }
    }
    public static int GCDevLoginTime
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevLoginTime"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCDeptKind
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DeptKind"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCReachTestName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ReachTestName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCDevSNStart
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["DevSNStart"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCTutorName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["TutorName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCLeadName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["LeadName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCRTRepay
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["RTRepay"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCKindAndClass
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["KindAndClass"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCSysKindRoom
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysKindRoom"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSysKindPC
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysKindPC"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSysKindLend
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysKindLend"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSysKindSeat
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysKindSeat"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCSysAutoSchoolName
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysAutoSchoolName"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
 public static int GCERoomDoor
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ERoomDoor"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCSysFrame
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["SysFrame"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static string GCRemoveCtrl
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["RemoveCtrl"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
   
    public static int GCscheduleMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["scheduleMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCICTypeMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ICTypeMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCTeacResvMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["TeachResvMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GroomNumMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["roomNumMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static int GCICLabRoom
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["ICLabRoom"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
    public static string GCCoords
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["Coords"];
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            else
            {
                return s;
            }
        }
    }
    public static int GCfunctionMode
    {
        get
        {
            string s = System.Web.Configuration.WebConfigurationManager.AppSettings["functionMode"];
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return int.Parse(s);
            }
        }
    }
}