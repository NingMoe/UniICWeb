using System;
using System.Collections.Generic;
using System.Text;
using UniWebLib;

namespace Util
{
    public static class Converter
    {
        private static readonly object 类型预约;

        public static DateTime ValueToDateConverter(Int64 value)
        {
            DateTime dt = new DateTime();
            if (value != 0)
            {
                dt = DateTime.FromFileTime(value);

            }
            return dt;
        }
        public static Int64 DateToValueConverter(DateTime dt)
        {
            Int64 v = -1;
            if (dt != null)
            {
                v = dt.ToFileTime();
            }
            return v;
        }
        public static string StringToSubConverter(string str, int length)
        {
            if (str.Length > length)
            {
                str = str.Substring(0, length - 1) + "...";
            }
            return str;
        }
        public static string toDate(uint date)
        {
            uint y = date / 10000;
            uint m = (date % 10000) / 100;
            uint d = date % 100;
            return y + "-" + m + "-" + d + " ";
        }
        //逗号分隔传值处理函数
        public static string CommaValue(string str)
        {
            if (string.IsNullOrEmpty(str) || str == "0")
            {
                return null;
            }
            if (str.Substring(str.Length - 1, 1) == ",")
            {
                return str.Substring(0, str.Length - 1);
            }
            else
            {
                return str;
            }
        }
        public static int Get1970Seconds(string Date)//返回和1970的差距秒数
        {
            DateTime dtDate = DateTime.Parse(Date);
            return Get1970Seconds(dtDate);
        }
        public static int Get1970Seconds(DateTime dtDate)//返回和1970的差距秒数
        {
            int result = 0;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            try
            {
                TimeSpan spDate = dtDate.Subtract(dt1970);
                result = (int)spDate.TotalSeconds;
            }
            catch
            {
                return -1;
            }
            return result;
        }
        public static string Get1970Date(int TotalSeconds)
        {
            return Get1970Date(TotalSeconds, "yyyy-MM-dd HH:mm");
        }
        public static string Get1970Date(int TotalSeconds, string format)//根据差距秒数 算出现在是日期
        {
            string result = string.Empty;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
            return result = dtNow.ToString(format);
        }
        public static DateTime Get1970DateTime(int TotalSeconds)//根据差距秒数 算出现在是日期对象
        {
            string result = string.Empty;
            DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
            DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
            return dtNow;
        }
        //日期字符串转数字格式
        public static uint DateToUint(string date)
        {
            uint rlt = 0;
            string d = "";
            char[] c = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char item in date)
            {
                for (int i = 0; i < c.Length; i++)
                {
                    if (item == c[i])
                    {
                        d += item;
                        break;
                    }
                }
            }
            if (d != "")
            {
                rlt = Convert.ToUInt32(d);
            }
            return rlt;
        }
        //连续日期字符串转日期对象
        public static DateTime StrToDate(string date)
        {
            string str = date + "00000000000000";
            int y = Convert.ToInt32(str.Substring(0, 4));
            int M = Convert.ToInt32(str.Substring(4, 2));
            int d = Convert.ToInt32(str.Substring(6, 2));
            int H = Convert.ToInt32(str.Substring(8, 2));
            int m = Convert.ToInt32(str.Substring(10, 2));
            int s = Convert.ToInt32(str.Substring(12, 2));
            DateTime rlt = new DateTime(y, M, d, H, m, s);
            return rlt;
        }
        //连续数字转日期字符串
        public static string UintToDateStr(uint? date)
        {
            uint? y = date / 10000;
            string m = ((int)(date % 10000) / 100).ToString("00");
            string d = ((int)date % 100).ToString("00");
            return y + "-" + m + "-" + d;
        }
        //身份
        public static string ConvertIdent(uint? ident)
        {
            string ret = "";
            if ((ident & 256) > 0) ret = "学生";
            if ((ident & 512) > 0) ret = "教师";
            if ((ident & (int)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0) ret = "导师";
            if ((ident & (int)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0) ret = "管理员";
            return ret;
        }
        //预约规则详情
        public static string GetRsvRuleDetail(UNIRESVRULE rule)
        {
            UniClientModule module = new UniClientModule();
            string str = "";
            if (rule.CheckTbl != null && rule.CheckTbl.Length > 0&&(rule.CheckTbl[0].dwProperty&(uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN)>0)
                str += "<span class='uni_trans'>需审核</span>、";
            if (((uint)rule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NEEDAPP) > 0)
                str += "<span class='uni_trans'>需提交申请报告</span>、";
            if (((uint)rule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEVKIND) > 0)
                str += "<span class='uni_trans'>"+ module.Translate("类型预约")+"</span>";
            if (((uint)rule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_DEV) > 0)
                str += "<span class='uni_trans'>预约具体对象</span>、";
            if (((uint)rule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NOCONFLICTCHECK) > 0)
                str += "<span class='uni_trans'>不检查设备冲突</span>、";
            if (str.Length > 0) str = str.Substring(0, str.Length - 1);
            return str;
        }
        //设备类型属性
        public static string GetDevKindPropDetail(uint? prop)
        {
            UniClientModule module = new UniClientModule();
            string str = "";
            if (prop == null) return str;
            if (((uint)prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE) > 0)
                str += "<span class='uni_trans'>多人共享</span>、";
            if (((uint)prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                str += "<span class='uni_trans'>支持长期</span>、";
            if (((uint)prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
                str += "<span class='uni_trans'>"+ module.Translate("类型预约")+" </span>、";
            if (((uint)prop & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE) > 0)
                str += "<span class='uni_trans'>"+ module.Translate("支持外借")+" </span>、";
            if (str.Length > 0) str = str.Substring(0, str.Length - 1);
            return str;
        }
        //预约状态
        public static string ResvStatusConverter(uint? sta)
        {
            UniClientModule module = new UniClientModule();
            if (sta == null)
            {
                return "";
            }
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT)) > 0)
                return "<span class='red uni_trans'>已违约</span>";
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE)) > 0)
                return "<span class='grey uni_trans'>已结束</span>";
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_CANCEL)) > 0)
                return "<span class='grey uni_trans'>已取消</span>";
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING)) > 0)
                return "<span class='green uni_trans'>"+ module.Translate("已生效")+" </span>";
            if ((sta & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)) > 0)
                return "<span class='orange uni_trans'>审核未过</span>";
            if ((sta & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING)) > 0)
                return "<span class='orange uni_trans'>"+ module.Translate("等待审核")+" </span>";
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO)) > 0)
                return "<span class='orange uni_trans'>"+ module.Translate("预约成功") + "</span>";

            return "<span class='grey uni_trans'>无状态</span>"; ;
        }
        //预约状态含审核
        public static string ResvStatusWithCheck(uint? sta,bool isDetail)
        {
            UniClientModule module = new UniClientModule();
            if (sta == null)
            {
                return "";
            }
            string str = "<span class='grey uni_trans'>无状态</span>";
            string detail = "";
            if (isDetail)
            {
                if ((sta & (int)UNIRESERVE.DWSTATUS.RESVSTAT_FORMAL) > 0) { detail += "<span style='color:green' class='uni_trans'>预约成功</span>" + ","; }
                if ((sta & (int)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0) { detail += "<span style='color:orange' class='uni_trans'>未生效</span>" + ","; }
            }
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_WAIT) > 0) { str = "<span style='color:orange' class='uni_trans'>等待上级审核</span>"; detail += str + ","; }
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0) { str = "<span style='color:yellowgreen' class='uni_trans'>正在审核</span>"; detail += str + ","; }
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) { str = "<span style='color:orange' class='uni_trans'>审核未通过</span>"; detail += str + ","; }
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK) > 0) { str = "<span style='color:green' class='uni_trans'>审核通过</span>"; detail += str + ","; }
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING)) > 0) { str = "<span class='green uni_trans'>"+module.Translate("已生效") +"</span>"; detail += str + ","; }
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_CANCEL)) > 0) { str = "<span class='grey uni_trans'>已取消</span>"; detail += str + ","; }
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE)) > 0) { str = "<span class='grey uni_trans'>已结束</span>"; detail += str + ","; }
            if ((sta & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT)) > 0) { str = "<span class='red uni_trans'>已违约</span>"; detail += str + ","; }

            if (detail != "") detail = detail.Substring(0, detail.Length - 1);
            if (isDetail) return detail;
            else return str;
        }
        //预约状态含审核
        public static string ResvStatusWithCheck(uint? sta)
        {
            return ResvStatusWithCheck(sta, false);
        }
        public static string RsvCheckStaConverter(uint? sta)
        {
            UniClientModule module = new UniClientModule();
            if (sta == null)
            {
                return "";
            }
            if ((sta & 1073741824) > 0) return "<span style='color:grey'>已过期</span>";
            if ((sta & 512) > 0) return "<span style='color:green'>"+module.Translate("已生效") +"</span>";
            if ((sta & 4) > 0) return "<span style='color:red'>审核未过</span>";
            if ((sta & 2) > 0) return "<span style='color:green'>审核通过</span>";
            if ((sta & 1) > 0) return "<span style='color:red'>未审核</span>";

            if ((sta & 256) > 0) return "<span style='color:green'>审核通过</span>";

            return "";
        }
        public static string RsvCheckStaConverterDtail(uint? sta)
        {
            if (sta == null)
            {
                return "";
            }
            if ((sta & (int)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0) return "<span style='color:grey'>预约已结束</span>";
            if ((sta & (int)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0) return "<span style='color:green'>预约已生效</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) return "<span style='color:orange'>管理员审核未通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:green'>管理员审核通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:green'>导师审核通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) return "<span style='color:orange'>导师审核未通过</span>";
            if ((sta & 1) > 0) return "<span style='color:red'>未审核</span>";

            if ((sta & 256) > 0) return "<span style='color:green'>预约未生效</span>";

            return "";
        }
        public static string RsvCheckStaConverterT(uint? sta)
        {
            UniClientModule module = new UniClientModule();
            if (sta == null)
            {
                return "";
            }
            if ((sta & 1073741824) > 0) return "<span style='color:grey'>已过期</span>";
            if ((sta & 512) > 0) return "<span style='color:green'>"+ module.Translate("已生效") +" </span>";
            if ((sta & 4) > 0) return "<span style='color:green'>通过</span>";
            if ((sta & 2) > 0) return "<span style='color:green'>通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:green'>通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) return "<span style='color:orange'>未通过</span>";
            if ((sta & 1) > 0) return "<span style='color:red'>未审核</span>";

            return "<span style='color:red'>未审核</span>";
        }
        public static string RsvCheckStaConverterM(uint? sta)
        {
            if (sta == null)
            {
                return "";
            }
            if ((sta & 1073741824) > 0) return "<span style='color:grey'>已过期</span>";
            if ((sta & 512) > 0) return "<span style='color:green'>已生效</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) return "<span style='color:orange'>未通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:green'>通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:red'>未审核</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0) return "<span style='color:red'>未审核</span>";
            if ((sta & 1) > 0) return "<span style='color:red'>未审核</span>";

            return "<span style='color:red'>未审核</span>";
        }
        //信用状态
        public static string GetCreditRecState(uint? sta)
        {
            UniClientModule module = new UniClientModule();
            string str = "";
            if (sta == null) return str;
            if ((sta & (int)CREDITREC.DWUSERCSTAT.USERCSTAT_VALID) > 0) str = "<span style='color:orange'>"+ module.Translate("有效")+" </span>";
            if ((sta & (int)CREDITREC.DWUSERCSTAT.USERCSTAT_CANCEL) > 0) str = "<span style='color:green'>"+ module.Translate("管理员取消")+" </span>";
            if ((sta & (int)CREDITREC.DWUSERCSTAT.USERCSTAT_OVER) > 0) str = "<span style='color:grey'>"+ module.Translate("已过期") + " </span>";
            return str;
        }
        //设备状态
        public static bool GetDevStat(uint? sta)
        {
            if (sta == null)
            {
                return false;
            }

            if ((sta & 1) > 0) return false;
            if ((sta & 2) > 0) return false;
            if ((sta & 4) > 0) return false;
            if ((sta & 8) > 0) return false;
            if ((sta & 255) > 0) return false;

            return true;
        }
        public static string GetDevRunStat(uint? sta)
        {
            if (sta == null)
            {
                return "";
            }
            if ((sta & (int)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0) return "<span style='color:orange'>暂时离开</span>";

            if (sta == 0) return "<span style='color:green'>空闲</span>";
            if ((sta & 1) > 0) return "<span style='color:green'>运行中</span>";
            if ((sta & 2) > 0) return "<span style='color:green'>使用中</span>";
            if ((sta & 4) > 0) return "<span style='color:green'>有预约</span>";
            if ((sta & 8) > 0) return "<span style='color:red'>超时运行</span>";

            if ((sta & (int)UNIDEVICE.DWRUNSTAT.DRUNSTAT_POWERWORKING) > 0) return "<span style='color:green'>工作中</span>";
            if ((sta & (int)UNIDEVICE.DWRUNSTAT.DRUNSTAT_POWERON) > 0) return "<span style='color:green'>已通电</span>";
            if ((sta & (int)UNIDEVICE.DWRUNSTAT.DRUNSTAT_POWEROFF) > 0) return "<span style='color:green'>未通电</span>";
            return "<span style='color:green'>空闲</span>";
        }
        //科研项目状态
        public static string GetRTLevel(uint? sta)
        {
            if (sta == null)
            {
                return "<span style='color:green'>无</span>";
            }
            if (sta == 1) return "<span style='color:green'>国家级</span>";
            if (sta == 2) return "<span style='color:green'>省部级</span>";
            if (sta == 3) return "<span style='color:green'>厅局级</span>";
            if (sta == 4) return "<span style='color:green'>校级</span>";
            if (sta == 4096) return "<span style='color:green'>其它</span>";

            if (sta == 5) return "<span style='color:green'>国家社科基金单列学科项目</span>";
            if (sta == 6) return "<span style='color:green'>国家社科基金项目</span>";
            if (sta == 7) return "<span style='color:green'>教育部人文社科研究项目</span>";
            if (sta == 8) return "<span style='color:green'>全国教育科学规划（教育部）项目</span>";
            if (sta == 9) return "<span style='color:green'>国家自然科学基金项目</span>";
            if (sta == 10) return "<span style='color:green'>中央其他部门社科专门项目</span>";
            if (sta == 11) return "<span style='color:green'>高校古籍整理研究项目</span>";
            if (sta == 12) return "<span style='color:green'>省、市、自治区社科基金项目</span>";
            if (sta == 13) return "<span style='color:green'>省教育厅社科项目</span>";
            if (sta == 14) return "<span style='color:green'>地、市、厅、局等政府部门项目</span>";
            if (sta == 15) return "<span style='color:green'>国际合作研究项目</span>";
            if (sta == 16) return "<span style='color:green'>与港、澳、台合作研究项目</span>";
            if (sta == 17) return "<span style='color:green'>企事业单位委托项目</span>";
            if (sta == 18) return "<span style='color:green'>外资项目</span>";
            if (sta == 19) return "<span style='color:green'>学校社科项目</span>";
            if (sta == 20) return "<span style='color:green'>学生毕业论文</span>";
            if (sta == 21) return "<span style='color:green'>其它科研项目</span>";

            return "<span style='color:green'>无</span>";
        }
        //权限状态
        public static string GetRoleState(uint? sta)
        {
            if (sta == null)
            {
                return "<span style='color:grey'>未定义</span>";
            }
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0) return "<span style='color:yellowgreen'>正在审核</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0) return "<span style='color:green'>可以使用</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_FAIL) > 0) return "<span style='color:orange'>未通过</span>";
            if ((sta & ((int)SFROLEINFO.DWSTATUS.SFROLESTAT_AUTO)) > 0) return "<span style='color:green'>可以使用</span>";
            if ((sta & (int)SFROLEINFO.DWSTATUS.SFROLESTAT_EXPIRED) > 0) return "<span style='color:grey'>已过期</span>";
            if ((sta & (int)SFROLEINFO.DWSTATUS.SFROLESTAT_NOAPPLY) > 0) return "<span style='color:orange'>未申请</span>";
            if ((sta & (int)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT) > 0) return "<span style='color:red'>审核拒绝</span>";
            if ((sta & (int)SFROLEINFO.DWSTATUS.SFROLESTAT_FORBID) > 0) return "<span style='color:grey'>无权限</span>";
            return "<span style='color:red'>未定义</span>";
        }
        //审核状态
        public static string GetCheckState(uint? sta)
        {
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK) > 0) return "<span style='color:green'>审核通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_FAIL) > 0) return "<span style='color:orange'>未通过</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_DOING) > 0) return "<span style='color:yellowgreen'>正在审核</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO) > 0) return "<span style='color:yellowgreen'>等待审核</span>";
            if ((sta & (int)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_WAIT) > 0) return "<span style='color:orange'>等待上级审核</span>";
            return "<span style='color:grey'>未审核</span>";
        }
        //教学实验状态
        public static string GetTestItemState(uint? sta)
        {
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_REPORTSCORE) > 0) return "<span style='color:green'>实验报告已批改</span>";
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_REPORTDONE) > 0) return "<span style='color:yellowgreen'>已交实验报告</span>";
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_DONE) > 0) return "<span style='color:yellowgreen'>已执行</span>";
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_RESVED) > 0) return "<span style='color:yellowgreen'>已预约</span>";
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_PARTDONE) > 0) return "<span style='color:yellowgreen'>部分已执行</span>";
            if ((sta & (int)UNITESTITEM.DWSTATUS.TESTITEMSTAT_PARTRESVED) > 0) return "<span style='color:orange'>部分已预约</span>";
            return "<span style='color:orange'>无预约状态</span>";
        }
        //考勤状态
        public static string GetAttendState(uint? sta)
        {
            UniClientModule module = new UniClientModule();
            string str = "";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_SICK) > 0) str += "<span style='color:orange'>病假</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_PRIVATE) > 0) str += "<span style='color:orange'>事假</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_UNSIGN) > 0) str += "<span style='color:red'>"+ module.Translate("未签到") + "</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_LOGINED) > 0) str += "<span style='color:yellowgreen'>已登录</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_SIGNED) > 0) str += "<span style='color:green'>已签到</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_ATTEND) > 0) str += "<span style='color:green'>出席</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_ABSENT) > 0) str += "<span style='color:orange'>缺席</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_LATE) > 0) str += "<span style='color:orange'>迟到</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_LEAVE) > 0) str += "<span style='color:orange'>早退</span> ";
            if ((sta & (int)UNIRESVREC.DWSTATUS.RESVRECSTAT_USELESS) > 0) str += "<span style='color:orange'>使用时间不达标</span> ";
            return str;
        }
    }
}
