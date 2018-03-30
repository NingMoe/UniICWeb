using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default :UniPage
{
    protected string szTestName = "";
    protected string szDevName = "";
    protected string szCostBig = "";
    protected string szResearchName = "";
    protected string szResearchSN = "";
    protected string szCost= "";
    protected string szDeptName = "";
    protected string szFoundNO = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!this.Page.IsPostBack)
        {
            string szId = Request["id"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTRESVREQ vrGet = new RTRESVREQ();
            vrGet.dwResvID = (uint.Parse(szId));
            RTRESV[] vtRes;
            uResponse = m_Request.Reserve.GetRTResv(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes.Length > 0)
            {
                szTestName = vtRes[0].szTestName.ToString();
                szDevName = vtRes[0].szDevName.ToString();
                szResearchName = vtRes[0].szRTName.ToString();
                RESEARCHTEST research;
                if (GetResearchTestByID(out research, vtRes[0].dwRTID.ToString()))
                {
                    szResearchName = research.szRTName;
                    szResearchSN = research.szRTSN;
                    szDeptName = research.szDeptName;
                }
                szFoundNO = vtRes[0].szFundsNo;
                szCostBig = ToUpper(decimal.Parse((uint)vtRes[0].dwRealCost / 100 + "." + (uint)vtRes[0].dwRealCost % 100));
                szCost = (uint)vtRes[0].dwRealCost / 100 + "." + (uint)vtRes[0].dwRealCost % 100;
            }
        }

    }
    public string ToUpper(decimal d)
    {
        if (d == 0)
            return "零元";

        string je = d.ToString("####.00");
        if (je.Length > 15)
            return "";
        je = new String('0', 15 - je.Length) + je;      //若小于15位长，前面补0

        string stry = je.Substring(0, 4);        //取得'亿'单元
        string strw = je.Substring(4, 4);        //取得'万'单元
        string strg = je.Substring(8, 4);        //取得'元'单元
        string strf = je.Substring(13, 2);        //取得小数部分

        string str1 = "", str2 = "", str3 = "";

        str1 = getupper(stry, "亿");        //亿单元的大写
        str2 = getupper(strw, "万");        //万单元的大写
        str3 = getupper(strg, "元");        //元单元的大写

        string str_y = "", str_w = "";
        if (je[3] == '0' || je[4] == '0')        //亿和万之间是否有0
            str_y = "零";
        if (je[7] == '0' || je[8] == '0')        //万和元之间是否有0
            str_w = "零";

        string ret = str1 + str_y + str2 + str_w + str3;    //亿，万，元的三个大写合并

        for (int i = 0; i < ret.Length; i++)        //去掉前面的"零"   
        {
            if (ret[i] != '零')
            {
                ret = ret.Substring(i);
                break;
            }

        }

        for (int i = ret.Length - 1; i > -1; i--)      //去掉最后的"零" 
        {
            if (ret[i] != '零')
            {
                ret = ret.Substring(0, i + 1);
                break;
            }
        }

        if (ret[ret.Length - 1] != '元')        //若最后不位不是'元'，则加一个'元'字
            ret = ret + "元";

        if (ret == "零零元")           //若为零元，则去掉"元数"，结果只要小数部分
            ret = "";

        if (strf == "00")            //下面是小数部分的转换
        {
            ret = ret + "整";
        }
        else
        {
            string tmp = "";
            tmp = getint(strf[0]);
            if (tmp == "零")
                ret = ret + tmp;
            else
                ret = ret + tmp + "角";
            tmp = getint(strf[1]);
            if (tmp == "零")
                ret = ret + "整";
            else
                ret = ret + tmp + "分";
        }

        if (ret[0] == '零')
        {
            ret = ret.Substring(1);          //防止0.03转为"零叁分"，而直接转为"叁分"
        }

        return ret;             //完成，返回


    }
    private string getupper(string str, string strDW)
    {
        if (str == "0000")
            return "";
        string ret = "";
        string tmp1 = this.getint(str[0]);
        string tmp2 = this.getint(str[1]);
        string tmp3 = this.getint(str[2]);
        string tmp4 = this.getint(str[3]);
        if (tmp1 != "零")
        {
            ret = ret + tmp1 + "仟";
        }
        else
        {
            ret = ret + tmp1;
        }
        if (tmp2 != "零")
        {
            ret = ret + tmp2 + "佰";
        }
        else
        {
            if (tmp1 != "零")//保证若有两个零'00'，结果只有一个零，下同  
                ret = ret + tmp2;
        }
        if (tmp3 != "零")
        {
            ret = ret + tmp3 + "拾";
        }
        else
        {
            if (tmp2 != "零")
                ret = ret + tmp3;
        }
        if (tmp4 != "零")
        {
            ret = ret + tmp4;
        }
        if (ret[0] == '零')//若第一个字符是'零'，则去掉  
            ret = ret.Substring(1);
        if (ret[ret.Length - 1] == '零')//若最后一个字符是'零'，则去掉  
            ret = ret.Substring(0, ret.Length - 1);
        return ret + strDW;//加上本单元的单位  
    }
    /// <summary>  
    /// 单个数字转为大写  
    /// </summary>  
    /// <param name="c">小写阿拉伯数字 0---9</param>  
    /// <returns>大写数字</returns>  
    private string getint(char c)
    {
        string str = "";
        switch (c)
        {
            case '0':
                str = "零";
                break;
            case '1':
                str = "壹";
                break;
            case '2':
                str = "贰";
                break;
            case '3':
                str = "叁";
                break;
            case '4':
                str = "肆";
                break;
            case '5':
                str = "伍";
                break;
            case '6':
                str = "陆";
                break;
            case '7':
                str = "柒";
                break;
            case '8':
                str = "捌";
                break;
            case '9':
                str = "玖";
                break;
        }
        return str;
    }  
}
