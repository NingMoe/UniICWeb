using System;
using System.Data;
using System.Configuration;
using UniWebLib;
using System.Threading;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;


/// <summary>
/// SessionMng 的摘要说明
/// </summary>
/// 

namespace UniLibrary
{
    public class ObjHelper
    {
        static public object GetMemberVlaue(object obj, string szName)
        {
            Type it = obj.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            System.Reflection.FieldInfo fi = it.GetField(szName, flag);
            if (fi == null)
            {
                return null;
            }
            return fi.GetValue(obj);
        }
        static public void SetMemberVlaue(object obj, string szName, object val)
        {
            Type it = obj.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            System.Reflection.FieldInfo fi = it.GetField(szName, flag);
            if (fi == null)
            {
                return;
            }
            fi.SetValue(obj,val);
        }

        static public object HTTP2OBJ(object pages, Type it)
        {
            return HTTP2OBJ(pages,"", it);
        }
        static public object HTTP2OBJ(object pages, string szPre, Type it)
        {
            string[] SqlForbid = @"--|'".Split('|');//禁用字符
            string[] anySqlStr = @"<|>|*|+|=".Split('|');//警告字符
            string[] anySqlKey = @"select|insert|delete|from|admin|set|alter|where|or|and|delay".Split('|');//确认字符


            object ret = Activator.CreateInstance(it);
            System.Reflection.FieldInfo[] its = it.GetFields();
            for (int i = 0; i < its.Length; i++)
            {
                string szValue = System.Web.HttpContext.Current.Request[szPre+its[i].Name];
                if (!string.IsNullOrEmpty(szValue))
                {
                    if (szValue.ToLower().IndexOf("script") > -1)
                    {
                        szValue = szValue.ToLower().Replace("script", "");
                    }
                    for (int m = 0; m < SqlForbid.Length; m++)
                    {
                        string szTempForbid = SqlForbid[m];
                        if (szValue.ToLower().IndexOf(szTempForbid) > -1)
                        {
                            szValue = szValue.ToLower().Replace(szTempForbid, "");
                        }
                    }
                    for (int m = 0; m < anySqlStr.Length; m++)
                    {
                        string szTempForbid = anySqlStr[m];
                        if (szValue.ToLower().IndexOf(szTempForbid) > -1)
                        {
                            for (int k = 0; k < anySqlKey.Length; k++)
                            {
                                string szTempForbid2 = anySqlKey[k];
                                if (szValue.ToLower().IndexOf(szTempForbid2) > -1)
                                {
                                    szValue = szValue.ToLower().Replace(szTempForbid, "");
                                    szValue = szValue.ToLower().Replace(szTempForbid2, "");
                                }
                            }
                        }
                    }

                }
                    if (string.IsNullOrEmpty(szValue))
                    {
                        szValue = GetPageControl(pages, szPre + its[i].Name);
                    }
                if (szValue == null)
                {
                    continue;
                }
                Type itt = its[i].FieldType;
                if (itt == typeof(string))
                {
                    its[i].SetValue(ret, szValue);
                }
                else if (itt == typeof(uint) || itt == typeof(uint?) || itt.IsEnum)
                {
                    uint nValue = 0;
                    szValue = szValue.Replace("/", "");
                    szValue = szValue.Replace("-", "");
                    uint.TryParse(szValue, out nValue);
                    its[i].SetValue(ret, nValue);
                }
                else if (itt.IsSerializable)
                {
                    //TODO:检举型没法处理
                }
                else
                {
                    if (itt.IsArray)
                    {
                        //TODO:数组没法处理
                    }else
                    {
                        object subobj = HTTP2OBJ(its[i].Name+"." ,itt);
                        its[i].SetValue(ret, subobj);
                    }
                }
            }

            //TODO:把结构外的值添加到reserved里。
            Reserved res = new Reserved();
            bool bFind = false;
            foreach (string szKey in System.Web.HttpContext.Current.Request.QueryString.Keys)
            {
                if (szKey.StartsWith(szPre + "reserved."))
                {
                    if (!bFind)
                    {
                        res = (Reserved)GetMemberVlaue(ret, "reserved");
                        if (res.Ext == null)
                        {
                            res.Ext = new System.Collections.Generic.Dictionary<string, object>();
                        }
                        bFind = true;
                    }
                    string szName = szKey.Substring((szPre + "reserved.").Length);
                    res.Ext[szName] = (string)System.Web.HttpContext.Current.Request.QueryString[szKey];
                }
            }
            foreach (string szKey in System.Web.HttpContext.Current.Request.Form.Keys)
            {
                if (szKey!=null&&szKey.StartsWith(szPre + "reserved."))
                {
                    if (!bFind)
                    {
                        res = (Reserved)GetMemberVlaue(ret, "reserved");
                        if (res.Ext == null)
                        {
                            res.Ext = new System.Collections.Generic.Dictionary<string, object>();
                        }
                        bFind = true;
                    }
                    string szName = szKey.Substring((szPre + "reserved.").Length);
                    res.Ext[szName] = (string)System.Web.HttpContext.Current.Request.Form[szKey];
                }
            }
            if (bFind)
            {
                UniLibrary.ObjHelper.SetMemberVlaue(ret, "reserved", res);
            }


            return ret;            
        }

        static public void GetPageAllControl(ref ArrayList ret, System.Web.UI.ControlCollection cc)
        {
            foreach (System.Web.UI.Control ctl in cc)
            {
                if (ctl.Controls != null && ctl.Controls.Count > 0)
                {
                    GetPageAllControl(ref ret, ctl.Controls);
                }
                else if (ctl.GetType().ToString().StartsWith("System.Web.UI.WebControls"))
                {
                    ret.Add(ctl);
                }
                else if (ctl.GetType().ToString().StartsWith("System.Web.UI.HtmlControls"))
                {
                    ret.Add(ctl);
                }
            }
        }

        static public string GetPageControl(object page, string szName)
        {
            System.Web.UI.ControlCollection cc = null;
            ArrayList ret = new ArrayList();
            if (page is System.Web.UI.Page)
            {
                cc = (page as System.Web.UI.Page).Controls;
            }
            else if (page is System.Web.UI.UserControl)
            {
                cc = (page as System.Web.UI.UserControl).Controls;
            }
            else
            {
                return null;
            }
            GetPageAllControl(ref ret, cc);
            foreach (object ctrInObj in ret)
            {
                System.Web.UI.Control ctr = (System.Web.UI.Control)ctrInObj;
                Type controlType = ctr.GetType();
                switch (controlType.ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        System.Web.UI.WebControls.TextBox controlTextBoxObj = (System.Web.UI.WebControls.TextBox)ctr;
                        if (controlTextBoxObj.ID == szName)
                        {
                            return controlTextBoxObj.Text;
                        }
                        break;
                    case "System.Web.UI.WebControls.Label":
                        System.Web.UI.WebControls.Label controlLabelObj = (System.Web.UI.WebControls.Label)ctr;
                        if (controlLabelObj.ID == szName)
                        {
                            return controlLabelObj.Text;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlInputText":
                        System.Web.UI.HtmlControls.HtmlInputText controlInputObj = (System.Web.UI.HtmlControls.HtmlInputText)ctr;
                        if (controlInputObj.Name == szName || controlInputObj.Name.EndsWith("$" + szName))
                        {
                            return controlInputObj.Value;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlSelect":
                        System.Web.UI.HtmlControls.HtmlSelect controlSelectObj = (System.Web.UI.HtmlControls.HtmlSelect)ctr;
                        if (controlSelectObj.Name == szName || controlSelectObj.Name.EndsWith("$" + szName))
                        {
                            return controlSelectObj.Value;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlInputRadioButton":
                        System.Web.UI.HtmlControls.HtmlInputRadioButton controlRadioButtonObj = (System.Web.UI.HtmlControls.HtmlInputRadioButton)ctr;
                        if (controlRadioButtonObj.Name == szName || controlRadioButtonObj.Name.EndsWith("$" + szName))
                        {
                            if (controlRadioButtonObj.Checked)
                            {
                                return controlRadioButtonObj.Value;
                            }
                        }
                        break;
                    default:
                        //TODO:其它控件
                        break;
                }
            }
            return null;
        }

        static public void SetPageControl(object page, string szName, string szValue)
        {
            System.Web.UI.ControlCollection cc = null;
            ArrayList ret = new ArrayList();
            if (page is System.Web.UI.Page)
            {
                cc = (page as System.Web.UI.Page).Controls;
            }
            else if (page is System.Web.UI.UserControl)
            {
                cc = (page as System.Web.UI.UserControl).Controls;
            }
            else
            {
                return ;
            }
            GetPageAllControl(ref ret, cc);
            foreach (object ctrInObj in ret)
            {
                System.Web.UI.Control ctr = (System.Web.UI.Control)ctrInObj;
                Type controlType = ctr.GetType();
                switch (controlType.ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        System.Web.UI.WebControls.TextBox controlTextBoxObj = (System.Web.UI.WebControls.TextBox)ctr;
                        if (controlTextBoxObj.ID == szName)
                        {
                            controlTextBoxObj.Text = szValue;
                        }
                        break;
                    case "System.Web.UI.WebControls.Label":
                        System.Web.UI.WebControls.Label controlLabelObj = (System.Web.UI.WebControls.Label)ctr;
                        if (controlLabelObj.ID == szName)
                        {
                            controlLabelObj.Text = szValue;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlInputText":
                        System.Web.UI.HtmlControls.HtmlInputText controlInputObj = (System.Web.UI.HtmlControls.HtmlInputText)ctr;
                        if (controlInputObj.Name == szName || controlInputObj.Name.EndsWith("$" + szName))
                        {
                            controlInputObj.Value = szValue;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlSelect":
                        System.Web.UI.HtmlControls.HtmlSelect controlSelectObj = (System.Web.UI.HtmlControls.HtmlSelect)ctr;
                        if (controlSelectObj.Name == szName || controlSelectObj.Name.EndsWith("$" + szName))
                        {
                            controlSelectObj.Value = szValue;
                        }
                        break;
                    case "System.Web.UI.HtmlControls.HtmlInputRadioButton":
                        System.Web.UI.HtmlControls.HtmlInputRadioButton controlRadioButtonObj = (System.Web.UI.HtmlControls.HtmlInputRadioButton)ctr;
                        if (controlRadioButtonObj.Name == szName || controlRadioButtonObj.Name.EndsWith("$" + szName))
                        {
                            if (controlRadioButtonObj.Value == szValue)
                            {
                                controlRadioButtonObj.Checked = true;
                            }
                            else
                            {
                                controlRadioButtonObj.Checked = false;
                            }
                        }
                        break;
                    default:
                        //TODO:其它控件
                        break;
                }
            }
        }

        static public string OBJ2JS<T>(T obj) where T : new()
        {
            return OBJ2JS(obj, "", typeof(T));
        }
        static public string OBJ2JS(object obj, string szPre, Type it)
        {
            if (obj == null) return "";
            string szResult = "[";

            System.Reflection.FieldInfo[] its = it.GetFields();
            for (int i = 0; i < its.Length; i++)
            {
                string szValue = null;
                Type itt = its[i].FieldType;
                if (itt == typeof(string) || itt == typeof(uint) || itt == typeof(uint?) || itt.IsEnum)
                {
                    object memobj = its[i].GetValue(obj);
                    if (memobj != null)
                    {
                        szValue = its[i].GetValue(obj).ToString();
                        szValue = szValue.Replace("\r", "");
                        szValue = szValue.Replace("\n", "");
                        szValue = szValue.Replace("\\", "\\\\").Replace("'", "\\'");
                        szResult += "['" + szPre + its[i].Name + "','" + szValue + "'],";
                    }
                }
                else if (itt.IsSerializable)
                {
                    //TODO:检举型没法处理
                }
                else
                {
                    if (itt.IsArray)
                    {
                        //TODO:数组没法处理
                    }
                    else
                    {
                        object subobj = its[i].GetValue(obj);
                        OBJ2JS(subobj, its[i].Name + ".", itt);
                    }
                }
            }

            if (string.IsNullOrEmpty(szPre) || szPre != "reserved.")
            {
                //增加支持Reserved里的字段。
                Reserved reserved = (Reserved)GetMemberVlaue(obj, "reserved");
                if (reserved.Ext != null)
                {
                    foreach (string szKey in reserved.Ext.Keys)
                    {
                        object v = reserved.Ext[szKey];
                        if (v != null)
                        {
                            string szValue = v.ToString();
                            szValue = szValue.Replace("\\", "\\\\").Replace("'", "\\'");
                            szValue = szValue.Replace("\r", "");
                            szValue = szValue.Replace("\n", "");
                            szResult += "['" + szPre + "reserved." + szKey + "','" + szValue + "'],";
                        }
                    }
                }
            }

            if (szResult.EndsWith(","))
            {
                szResult = szResult.Substring(0, szResult.Length - 1);
            }
            szResult += "]";

            return szResult;
        }

        static public void OBJ2HTTP<T>(object pages, T obj) where T : new()
        {
            OBJ2HTTP(pages,obj,"", typeof(T));
        }
        static public void OBJ2HTTP(object pages,object obj, string szPre, Type it)
        {
            if (obj == null) return;
            System.Reflection.FieldInfo[] its = it.GetFields();
            for (int i = 0; i < its.Length; i++)
            {
                string szValue = null;
                Type itt = its[i].FieldType;
                if (itt == typeof(string) || itt == typeof(uint) || itt == typeof(uint?) || itt.IsEnum)
                {
                    object memobj = its[i].GetValue(obj);
                    if (memobj != null)
                    {
                        szValue = its[i].GetValue(obj).ToString();
                        SetPageControl(pages, szPre + its[i].Name, szValue);
                    }
                }
                else if (itt.IsSerializable)
                {
                    //TODO:检举型没法处理
                }
                else
                {
                    if (itt.IsArray)
                    {
                        //TODO:数组没法处理
                    }
                    else
                    {
                        object subobj = its[i].GetValue(obj);
                        OBJ2HTTP(pages,subobj, its[i].Name + ".", itt);
                    }
                }
            }
        }

        static public object NameValue2OBJ(System.Collections.Specialized.NameValueCollection pages, string szPre,Type it)
        {
            object ret = Activator.CreateInstance(it);
            System.Reflection.FieldInfo[] its = it.GetFields();
            for (int i = 0; i < its.Length; i++)
            {
                string szValue = pages[szPre + its[i].Name];
                if (szValue == null)
                {
                    continue;
                }
                Type itt = its[i].FieldType;
                if (itt == typeof(string))
                {
                    its[i].SetValue(ret, szValue);
                }
                else if (itt == typeof(uint) || itt == typeof(uint?) || itt.IsEnum)
                {
                    uint nValue = 0;
                    szValue = szValue.Replace("/", "");
                    szValue = szValue.Replace("-", "");
                    uint.TryParse(szValue, out nValue);
                    its[i].SetValue(ret, nValue);
                }
                else if (itt.IsSerializable)
                {
                    //TODO:检举型没法处理
                }
                else
                {
                    if (itt.IsArray)
                    {
                        //TODO:数组没法处理
                    }
                    else
                    {
                        object subobj = NameValue2OBJ(pages,its[i].Name + ".", itt);
                        its[i].SetValue(ret, subobj);
                    }
                }
            }
            return ret;
        }
    }
}

