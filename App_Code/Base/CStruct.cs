
public class GETUSER
{
    public int Type;
    public PARAM_EXT ext;
}

public class USER
{
    public int dwUserID;
    public string szLogonName;
    public string szTrueName;
    public string szPassword;
    public int dwType;
}

public class CHECKUSER
{
    public string szLogonName;
    public string szPassword;
}

public class GETNEWS
{
    public int dwStartDate;
    public int dwEndDate;
    public int dwClsID;
    public int dwType;
    public PARAM_EXT ext;
}

public class NEWS
{
    public int dwNewsID;
    public int dwDate;
    public int dwTime;
    public int dwClsID;
    public int dwType;
    public string szTitle;
    public string szContent;
}

public class MODULES
{
    public int dwModuleID;
    public string szModuleName;
}

public class CLASS
{
    public int dwClsID;
    public int dwModuleID;
    public string szClassName;
}
