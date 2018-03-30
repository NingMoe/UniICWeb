using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using UniWebLib;
/// <summary>
/// DDLBind 的摘要说明
/// </summary>
public class Bind : System.Web.UI.Page
{
    ArrayList arr = new ArrayList();
    XmlDocument xml = new XmlDocument();
   
    private UniRequest m_Request = new UniRequest();
    public List<bindGrid> gridBind = new List<bindGrid>();//girdview重要绑定的内容
    
    public struct bindGrid
    {
        string fieldName;
        string value;
        public void SetFieldName(string value)
        {
            fieldName = value;
        }
        public void SetVaule(string value)
        {
            this.value = value;
        }
        public string GetFieldName()
        {
            return fieldName;
        }
        public string GetValue()
        {
            return value;
        }
    };
    ///   <summary>   
    ///   girdview中xml的绑定
    ///   </summary>   
    ///   <param   name="fieldName">gridview要绑定的字段名</param>   
    ///   <param   name="value">xml中绑定的fieldname</param>   
    public void GridBindAdd(string fieldName,string value)
    {
        bindGrid item=new bindGrid();
        item.SetFieldName(fieldName);
        item.SetVaule(value);
        gridBind.Add(item);
    }
    public Bind()
	{     
        string strPaht = HttpRuntime.AppDomainAppPath;
        string szLanguage = (string)HttpContext.Current.Session["s_SystemLang"];
        string szSystem = (string)HttpContext.Current.Session["s_System"];
        if (szSystem != null)
        {
            string szXmlName = "Const/Const_" + szSystem + "_" + szLanguage + ".xml";
            xml.Load(strPaht + szXmlName);         		
        }
       
	}     
   /// <summary>
   /// dll中添加要绑定的dropdownlist
   /// </summary>
   /// <param name="ddl">dll控件</param>
   /// <param name="strName">xml中的fieldname</param>
    public void Add(DropDownList ddl, string strName)
    {
        arr.Add(strName);
        arr.Add(ddl);
    }
    /// <summary>
    /// dll的绑定
    /// </summary>
    public void ddlBind()
    {
        //XmlNodeList nodeList = xml.SelectSingleNode("const/field[@name='Dept']");
        for (int count = 0; count < arr.Count; count = count + 2)
        {
            XmlNode nodeTemp = xml.SelectSingleNode("const/field[@name='"+arr[count].ToString()+"']");
            XmlNodeList nodeList2 = nodeTemp.ChildNodes;
            foreach (XmlNode nodeTemp2 in nodeList2)
            {
                if (nodeTemp2.Name.ToString() == "option")
                {
                    ListItem item = new ListItem();
                    DropDownList ddl = arr[count + 1] as DropDownList;
                    item.Value = nodeTemp2.Attributes["value"].Value.ToString();
                    item.Text = nodeTemp2.InnerText.ToString();
                    ddl.Items.Add(item);

                }
            }
             
        }

    }
    ///   <summary>   
    ///   将CUniStructArray转化为datatable，带xml的转换
    ///   </summary>   
    ///   <param   name="list">dt中要转换的列名，不是xml。可以传null表示不转换</param> 
    ///   <param   name="listXml">dt中要转换的列名和类型是xml。1列名2xml中的fieldname。可以传null表示不转换</param>
   
    //public void VtTableConvert<T>(CUniStructArray<T> vtTable, out System.Data.DataTable table,ArrayList list,ArrayList listXml) where T:new()
    //{
    //    table = new DataTable("dataTable");
    //    if (vtTable == null || vtTable.GetLength() == 0)
    //        return;
    //    for (int i = 0; i < vtTable[0].GetLength(); i++)
    //    {
    //        DataColumn colum = new DataColumn();
    //        colum.ColumnName = vtTable[0].GetCoreFieldName(i).ToString();
    //        table.Columns.Add(colum);
    //    }
    //    for (int i = 0; i < vtTable.GetLength(); i++)
    //    {
    //        DataRow row = table.NewRow();
    //        for (int j = 0; j < vtTable[0].GetLength(); j++)
    //        {
    //            if (list!=null&&list.IndexOf(vtTable[0].GetCoreFieldName(j).ToString()) >= 0)//要转换的字段
    //            {
    //                string style = list[list.IndexOf(vtTable[0].GetCoreFieldName(j)) + 1].ToString();
    //                row[j] = GetConvertValue(style, vtTable[i].GetValue(j));
    //            }
    //            else if (listXml!=null&&listXml.IndexOf(vtTable[0].GetCoreFieldName(j).ToString()) >= 0)//xml要转换字段
    //            {
    //                string fieldname=listXml[listXml.IndexOf(vtTable[0].GetCoreFieldName(j))+1].ToString();
    //                 row[j]=GetXmlValue(fieldname,vtTable[i].GetValue(j).ToString());
    //            }
    //            else 
    //            {
    //                try
    //                {
    //                    row[j] = vtTable[i].GetValue(j).ToString();
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    row[j] = "";
    //                }
                   
    //            }
                
    //        }
    //        table.Rows.Add(row);
    //    }
       
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vtTable">CUniStructArray</param>
    /// <param name="table">datatable</param>
    /// <param name="xmlColumn">要转换的xml列<列名，xml文件中的name></param>
    /// <param name="valueColumn">要转换的value,比如fee,time1970</param>
    //public void VtTableConvertUpdate<T>(CUniStructArray<T> vtTable, out System.Data.DataTable table, Dictionary<string, string> xmlColumn, Dictionary<string, string> valueColumn) where T : new()
    //{             
    //    table = new DataTable("dataTable");
    //    if (vtTable == null || vtTable.GetLength() == 0)
    //        return;
    //    for (int i = 0; i < vtTable[0].GetLength(); i++)
    //    {
    //        DataColumn colum = new DataColumn();
    //        colum.ColumnName = vtTable[0].GetCoreFieldName(i).ToString();
    //        table.Columns.Add(colum);
    //    }
    //    if (xmlColumn != null)
    //    {
    //        foreach (KeyValuePair<string, string> temp in xmlColumn)
    //        {
    //            DataColumn colum = new DataColumn();
    //            colum.ColumnName = temp.Key.ToString() + "xml";
    //            table.Columns.Add(colum);
    //        }
    //    }
    //    if (valueColumn != null)
    //    {
    //        foreach (KeyValuePair<string, string> temp in valueColumn)
    //        {
    //            DataColumn colum = new DataColumn();
    //            colum.ColumnName = temp.Key.ToString() + "value";
    //            table.Columns.Add(colum);
    //        }
    //    }
    //    for (int i = 0; i < vtTable.GetLength(); i++)
    //    {
    //        DataRow row = table.NewRow();
    //        for (int j = 0; j < vtTable[0].GetLength(); j++)
    //        {               
    //            string valueXml="";
    //            string valueValue;
    //            string name = vtTable[0].GetCoreFieldName(j).ToString();
    //            if (vtTable[0].GetCoreFieldName(j).ToString() == "szState")
    //            {
    //                string id = "12";
    //            }
    //            if (xmlColumn!=null&&xmlColumn.TryGetValue(vtTable[0].GetCoreFieldName(j).ToString(), out valueXml))
    //            {
    //                string fieldname = vtTable[0].GetCoreFieldName(j).ToString()+"xml";
    //                row[j] = vtTable[i].GetValue(j).ToString();
    //                row[fieldname] = GetXmlConstValue(valueXml, vtTable[i].GetValue(j).ToString());
    //            }
    //            if (valueColumn != null && valueColumn.TryGetValue(vtTable[0].GetCoreFieldName(j).ToString(), out valueValue))
    //            {
    //                string fieldname = vtTable[0].GetCoreFieldName(j).ToString() + "value";
    //                row[j] = vtTable[i].GetValue(j).ToString();
    //                row[fieldname] = GetConvertValue(valueValue, vtTable[i].GetValue(j).ToString());
    //            }
    //            else
    //            {
    //                try
    //                {
    //                    row[j] = vtTable[i].GetValue(j).ToString();
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    row[j] = "";
    //                }

    //            }

    //        }
    //        table.Rows.Add(row);
    //    }

    //}
    ///   <summary>   
    ///   将CUniStruct转化为datatable，就一行。带xml的转换
    ///   </summary>   
    ///   <param   name="list">dt中要转换的列名，不是xml。可以传null表示不转换</param> 
    ///   <param   name="listXml">dt中要转换的列名和类型是xml。1列名2xml中的fieldname。可以传null表示不转换</param>
    //public void VtTableConvert<T>(CUniStruct<T> vtTable, out System.Data.DataTable table, ArrayList list, ArrayList listXml) where T : new()
    //{
    //    table = new DataTable("dataTable");
    //    if (vtTable == null || vtTable.GetLength() == 0)
    //        return;
    //    for (int i = 0; i < vtTable.GetLength(); i++)
    //    {
    //        DataColumn colum = new DataColumn();
    //        colum.ColumnName = vtTable.GetCoreFieldName(i).ToString();
    //        table.Columns.Add(colum);
    //    }
    //    DataRow row = table.NewRow();
    //    for (int j = 0; j < vtTable.GetLength(); j++)
    //    {
    //        if (list!=null&&list.IndexOf(vtTable.GetCoreFieldName(j).ToString()) > 0)
    //        {
    //            row[j] = GetConvertValue((vtTable.GetCoreFieldName(j)+1).ToString(), vtTable.GetValue(j));
    //        }
    //        else if (listXml != null && listXml.IndexOf(vtTable.GetCoreFieldName(j).ToString()) >= 0)//xml要转换字段
    //        {
    //            string fieldname = listXml[listXml.IndexOf(vtTable.GetCoreFieldName(j)) + 1].ToString();
    //            row[j] = GetXmlValue(fieldname, vtTable.GetValue(j).ToString());
    //        }
    //        else
    //        {
    //            row[j] = vtTable.GetValue(j).ToString();
    //        }
    //    }
    //    table.Rows.Add(row);

    //}
    ///   <summary>   
    ///   获得xml值
    ///   </summary>   
    ///   <param   name="fieldName">xml中fieldname的值</param>  
    ///   <param   name="value">xml中option的value的值</param>  
    public string GetXmlValue(string fieldName,string value)
    {
        XmlNode nodeTemp = xml.SelectSingleNode("const/field[@name='" + fieldName + "']");
        if(nodeTemp==null)
        {
            return string.Empty;
        }
        XmlNodeList nodeList2 = nodeTemp.ChildNodes;
        foreach (XmlNode nodeTemp2 in nodeList2)
        {
            if (nodeTemp2.Attributes["value"] != null && nodeTemp2.Attributes["value"].Value.ToString() == value)
            {
                return nodeTemp2.InnerText.ToString();
            }
           
        }
        return string.Empty;
    }

    public string GetXmlConstValue(string fieldName, string value)
    {
        string strRes=string.Empty;
        XmlNode nodeTemp = xml.SelectSingleNode("const/field[@name='" + fieldName + "']");
        if (nodeTemp == null)
        {
            return string.Empty;
        }
        XmlNodeList nodeList2 = nodeTemp.ChildNodes;
        foreach (XmlNode nodeTemp2 in nodeList2)
        {
            uint temp = uint.Parse(nodeTemp2.Attributes["value"].Value.ToString());
            if (nodeTemp2.Attributes["value"] != null && (temp & uint.Parse(value)) > 0)
            {
                strRes += nodeTemp2.InnerText.ToString()+";"; 
            }   
        }
        return strRes;
    }

    public void SetTable(DataTable dt)
    {
        if (gridBind.Count==0)
        {
            return;
        }
        for(int i=0;i<dt.Rows.Count;i++)
        {
            for(int j=0;j<gridBind.Count;j++)
            {
                bindGrid item = new bindGrid();
                item = gridBind[j];
                dt.Rows[i][item.GetFieldName()] = GetXmlValue(item.GetValue(), dt.Rows[i][item.GetFieldName()].ToString()); 
                //传两次第一次fieldname为gridview中的字段名,value为要绑定const.xml的name的名称
                //第二次，fieldname为const.xml的name名称 value为option的中的值
            }
        }
    }
    ///   <summary>   
    ///   dt的显示
    ///   </summary>   
    ///   <param   name="fieldName">xml中fieldname的值</param>  
    ///   <param   name="value">xml中option的value的值</param>  
    public void BindContent(System.Web.UI.ControlCollection page,DataTable dt)
    {
        if(page==null||dt==null||dt.Rows.Count==0)
        {
            return;
        }
        int count = page.Count;
        for(int i=0;i<count;i++)
        {            
            foreach ( System.Web.UI.Control control in page[i].Controls)
            {
                if(control is TextBox)
                {
                    TextBox text=control as TextBox;
                    if(dt.Columns.IndexOf(control.ID.ToString())>=0)
                    {
                        text.Text = dt.Rows[0][control.ID.ToString()].ToString();
                    }
                   
                }
            }
        }
    }
    ///   <summary>   
    ///   页面文件转换为cunistruct
    ///   </summary>   
    ///   <param   name="page">this.Form.Controls</param>  
    ///   <param   name="vrRes">传出的结果</param>  
    //public void ContentToStruct<T>(System.Web.UI.ControlCollection page,out CUniStruct<T> vrResPass) where T : new()
    //{
    //    CUniStruct<T> vrRes = new CUniStruct<T>();
    //    if (page == null)
    //    {
    //        vrResPass = vrRes;
    //        return;
    //    }
    //    int count = page.Count;
    //    for (int i = 0; i < count; i++)
    //    {
    //        foreach (System.Web.UI.Control control in page[i].Controls)
    //        {
    //            if (control is TextBox)
    //            {
    //                TextBox text = control as TextBox;
    //                //int index = vrRes.GetFieldIndex(control.ID.ToString());
    //                //if(index>=0)
    //                //{
    //                //   // byte[] inpass;
    //                //    //inpass[0]=text.Text.ToString();
    //                //    //vrRes.Import(inpass,(uint)index);
    //                //}                    
    //            }
    //        }
    //    }
    //    vrResPass = vrRes;
    //}
    /// <summary>
    /// datetime转换
    /// </summary>
    /// <param name="szDatatype">类型：time1970</param>
    /// <param name="Value">传入要转换的值</param>
    /// <returns></returns>
    public string GetConvertValue(string szDatatype, object Value)
    {
        string szValue="";
        if (!string.IsNullOrEmpty(szDatatype))
        {
            if (szDatatype == "fee")
            {
                uint dwValue = ((uint)Value);
                float fValue = (float)(int)dwValue;
                fValue /= 100;
                szValue = fValue.ToString("0.00");
            }
            else if (szDatatype == "time")
            {
                uint dwValue = ((uint)Value);
                if (dwValue == 0xffffffff)
                {
                    szValue = "";
                }
                else
                {
                    uint dwHour = (dwValue / 100) % 100;
                    uint dwMint = (dwValue) % 100;
                    szValue = dwHour.ToString("00") + ":" + dwMint.ToString("00");
                }
            }
            else if (szDatatype == "time1970")//从1970-01-01 08:00:00来的秒数
            {
                uint dwValue = ((uint)Value);
                if ((dwValue == 0xffffffff) || (dwValue == 0))
                {
                    szValue = "";
                }
                else
                {
                    DateTime dtBaseTime = new DateTime(1970, 01, 01, 08, 0, 0);
                    DateTime dt;
                    dt = dtBaseTime.AddSeconds(dwValue);
                    szValue = dt.Month.ToString("00") + "月" + dt.Day.ToString("00") + "日 " + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00");
                }
            }
            else if (szDatatype == "timespan")
            {
                szValue = "";
                uint dwValue = ((uint)Value);
                if (dwValue >= 60)
                {
                    szValue += (dwValue / 60) + "小时";
                    dwValue = dwValue % 60;
                }
                if (dwValue > 0)
                {
                    szValue += dwValue + "分钟";
                }
                if (szValue == "")
                {
                    szValue = "0分钟";
                }
            }
            else if (szDatatype == "timespan2")
            {
                szValue = "";
                uint dwValue = ((uint)Value);
                if (dwValue >= 100)
                {
                    szValue += (dwValue / 100) + "小时";
                    dwValue = dwValue % 100;
                }
                if (dwValue > 0)
                {
                    szValue += dwValue + "分钟";
                }
                if (szValue == "")
                {
                    szValue = "0分钟";
                }
            }
            else if (szDatatype == "date")
            {
                uint dwValue = (uint.Parse(Value.ToString()));
                uint dwYear = (dwValue / 10000);
                uint dwMonth = (dwValue / 100) % 100;
                uint dwDay = (dwValue) % 100;
                szValue = dwYear.ToString() + "-" + dwMonth.ToString("00") + "-" + dwDay.ToString("00");
            }
            else if (szDatatype == "date1970")//从1970-01-01 08:00:00来的秒数
            {
                uint dwValue = ((uint)Value);
                if ((dwValue == 0xffffffff) || (dwValue == 0))
                {
                    szValue = "";
                }
                else
                {
                    DateTime dtBaseTime = new DateTime(1970, 01, 01, 08, 0, 0);
                    DateTime dt;
                    dt = dtBaseTime.AddSeconds(dwValue);
                    szValue = dt.Year.ToString("00") + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00");
                }
            }
            else if (szDatatype == "datetime1970")//从1970-01-01 08:00:00来的秒数
            {
                uint dwValue = ((uint)Value);
                if ((dwValue == 0xffffffff) || (dwValue == 0))
                {
                    szValue = "";
                }
                else
                {
                    DateTime dtBaseTime = new DateTime(1970, 01, 01, 08, 0, 0);
                    DateTime dt;
                    dt = dtBaseTime.AddSeconds(dwValue);
                    szValue = dt.Year.ToString("00") + "年" + dt.Month.ToString("00") + "月" + dt.Day.ToString("00") + "日 " + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00"); ;
                }
            }
        }
        return szValue;
    }
    public void MessageBoxShow(string value,System.Web.UI.Page page)
    {
        ClientScriptManager CSM = page.ClientScript;
        CSM.RegisterStartupScript(page.GetType(),"", "<script>alert('" + value + "')</script>");
      
    }
    public void MessageBoxShow(string value, System.Web.UI.Page page,string url)
    {
        ClientScriptManager CSM = page.ClientScript;
       // CSM.RegisterStartupScript(page.GetType(), "", "<script>alert('" + value + "');window.opener.location.href='" + url + "';window.close();</script>");
        if (value != "")
        {
            CSM.RegisterStartupScript(page.GetType(), "", "<script>alert('" + value + "');window.location.href='" + url + "';</script>");
        }
        else
        {
            CSM.RegisterStartupScript(page.GetType(), "", "<script>window.location.href='" + url + "';</script>");
        }
    }
    public void MessageBoxShowUrl(string value, System.Web.UI.Page page, string url)
    {
        ClientScriptManager CSM = page.ClientScript;
        CSM.RegisterStartupScript(page.GetType(), "", "<script>alert('" + value + "');window.opener.parent.location.href='" + url + "';window.close();</script>");
      //  CSM.RegisterStartupScript(page.GetType(), "", "<script>alert('" + value + "');window.location.href='" + url + "';</script>");
    }
    public void MessageBoxShowClose(string value, System.Web.UI.Page page)
    {
        ClientScriptManager CSM = page.ClientScript;
        CSM.RegisterStartupScript(page.GetType(), "", "<script>alert('" + value + "'); window.close();</script>");
    }
    public void Close()
    {
        System.Web.HttpContext.Current.Response.Write("<script>window.close();</script>");
    }
    public XmlDocument GetXmlDoc()
    {
        return xml;
    }

    public class ResvTable
    {
        public int beginTime;
        public int endTime;
        public int status;
    }  
    public int Get1970Seconds(string Date)//返回和1970的差距秒数
    {
        int result = 0;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        try
        {
            DateTime dtDate = DateTime.Parse(Date);
            TimeSpan spDate = dtDate.Subtract(dt1970);
            result = (int)spDate.TotalSeconds;
        }
        catch
        {
            return -1;
        }
        return result;
    }
    public int Get1970Seconds(int nYear,int nMonth,int nDay,int nHour,int nMin)
    {
        DateTime dtTime = new DateTime(nYear, nMonth, nDay, nHour, nMin,0);
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        int result = 0;
        try
        {           
            TimeSpan spDate = dtTime.Subtract(dt1970);
            result = (int)spDate.TotalSeconds;
        }
        catch
        {
            return -1;
        }
        return result;
    }
    public int Get1970Seconds(int nDate, int nHour, int nMin)
    {
        DateTime dtTime = new DateTime(nDate / 10000, (nDate / 100) % 100, nDate%100, nHour, nMin, 0);
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        int result = 0;
        try
        {
            TimeSpan spDate = dtTime.Subtract(dt1970);
            result = (int)spDate.TotalSeconds;
        }
        catch
        {
            return -1;
        }
        return result;
    }
    public string Get1970Date(int TotalSeconds)//根据差距秒数 算出现在是日期
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("yyyy-MM-dd HH:mm");
    }
    public string ConvertDateToDisplay(int intDate)
    {
        int intYearLocal = intDate / 10000;
        int intMonthLocal = (intDate % 10000) / 100;
        int intDateLocal = intDate % 100;

        string szDate = intYearLocal.ToString() + "年" + intMonthLocal.ToString() + "月" + intDateLocal.ToString() + "日";
        return szDate;
    }
    public void SetResvddl(DropDownList ddlList,int intStart,int intEnd)
    {
        for (int i = intStart; i <= intEnd; i++)
        {
            {
                ListItem item = new ListItem();
                item.Text = i.ToString("00");
                item.Value = i.ToString("00");
                ddlList.Items.Add(item);
            }
        }
    }
    public void SetResvddl30(DropDownList ddlList, int intStart, int intEnd)
    {
        for (int i = intStart; i <= intEnd; i=i+30)
        {
            {
                ListItem item = new ListItem();
                item.Text = i.ToString("00");
                item.Value = i.ToString("00");
                ddlList.Items.Add(item);
            }
        }
    }
    public string GetTimeToDisplay(int timeStart,int timeEnd)
    {
        string szMemo="";
        if (timeEnd <= 1200&&timeStart>0)
        {
           
        }else if ((timeEnd <= 1800) && (timeStart >= 1200))
        {
           
        }
        else if ((timeEnd <= 2359) && (timeStart >= 1800))
        {
          
        }
        else if ((timeEnd >1800) && (timeStart < 1200))
        {
           
        }
        string szStart = (timeStart / 100).ToString("00") + ":" + (timeStart % 100).ToString("00");
        string szEnd = (timeEnd / 100).ToString("00") + ":" + (timeEnd % 100).ToString("00");
        return szMemo+""+szStart + "-" + szEnd+"";
    }


    //获取开始时间
    //返回uint列表，uint格式为HHMM
    //nStartTime：开始时间以分钟为单位，nEndTime开始时间以分钟为单位。
    //nMinTime：最小预约时间，以分钟为单位。
    //nTimeSpan：显示间隔，以分钟为单位
    //szUsableNumArray：GetDevKindForResv结果里的szUsableNumArray字段
    public ArrayList GetBeginTimeList(uint nStartTime, uint nEndTime, uint nMinTime, uint nTimeSpan, string szUsableNumArray)
    {
        //nEndTime++;
        ArrayList alRet = new ArrayList();

        nEndTime -= nMinTime;

        uint i = nStartTime;
        uint nBegin = i;
        bool cBeginC = szUsableNumArray[(int)i] != '0';
        i++;
        uint nEndPos = (uint)szUsableNumArray.Length;


        if (nEndPos > nEndTime)
        {
            nEndPos = nEndTime;
        }
        bool bAddFirst = true;
        if ((nStartTime + nTimeSpan) < (nEndTime+nMinTime))
        {
            for (uint j = nStartTime; j < nStartTime + nTimeSpan; j++)
            {
                if (szUsableNumArray[(int)i] == '0')
                {
                    bAddFirst = false;
                }
            }
            if (bAddFirst)
            {
                alRet.Add((nStartTime / 60) * 100 + (nStartTime % 60));
            }
        }
      
        for (; i < nEndPos; i++)
        {
            bool c = szUsableNumArray[(int)i] != '0';

            if (c != cBeginC || (i - nBegin >= nTimeSpan))
            {
                if (cBeginC && (i - nStartTime) >= nMinTime)
                {
                    if (!c && (i - nBegin) < nMinTime)
                    {
                        //时是小于最小时间
                    }
                    else
                    {
                        uint dwBegin = (nBegin / 60) * 100 + (nBegin % 60);
                        alRet.Add(dwBegin);
                    }
                }
                nBegin = i;

                //TODO:>>>>>
                if (c != cBeginC && c)
                {
                    nBegin--;
                }
                //<<<<<<

                cBeginC = c;

            }
        }
        if (nBegin != i)
        {
            if (cBeginC && (i - nStartTime) >= nMinTime)
            {
                uint dwBegin = (nBegin / 60) * 100 + (nBegin % 60);
                alRet.Add(dwBegin);
            }
        }
        bool bIsLastAdd = true;
        for (uint j = nEndTime - nMinTime; j < nEndTime; j++)
        {
            if (szUsableNumArray[(int)i] == '0')
            {
                bAddFirst = false;
            }
        }
        nEndTime += nMinTime;
        if (bIsLastAdd && ((nStartTime+nMinTime)<nEndTime))
        {
            alRet.Add(((nEndTime - nMinTime) / 60) * 100 + ((nEndTime - nMinTime) % 60));
        }
        return alRet;
    }

    //获取结束时间
    //返回uint列表，uint格式为HHMM
    //nStartTime：开始时间以分钟为单位，nEndTime开始时间以分钟为单位。
    //nMinTime：最小预约时间，以分钟为单位。
    //nTimeSpan：显示间隔，以分钟为单位
    //szUsableNumArray：GetDevKindForResv结果里的szUsableNumArray字段
    public ArrayList GetEndTimeList(uint nStartTime, uint nEndTime, uint nMinTime, uint nTimeSpan, string szUsableNumArray)
    {
        ArrayList alRet = new ArrayList();
        uint i = nStartTime;
        uint nBegin = i;
        i++;
        bool cBeginC = szUsableNumArray[(int)i] != '0';
        if (!cBeginC)
        {
            return alRet;
        }
      
        uint nEndPos = (uint)szUsableNumArray.Length;
        if (nEndPos > nEndTime)
        {
            nEndPos = nEndTime;
        }
        for (; i < nEndPos; i++)
        {
            bool c = szUsableNumArray[(int)i] != '0';
            if (!c)
            {
                break;
            }
            if (i - nBegin >= nTimeSpan)
            {
                if ((i - nStartTime) >= nMinTime)
                {
                    uint nEnd = (i);
                    uint dwEnd = (nEnd / 60) * 100 + (nEnd % 60);
                    alRet.Add(dwEnd);
                }
                nBegin = i;
                cBeginC = c;
            }
        }
        if (nBegin != i)
        {
            if ((i - nStartTime) >= nMinTime)
            {
                uint nEnd = (i);
                uint dwEnd = (nEnd / 60) * 100 + (nEnd % 60);
                alRet.Add(dwEnd);
            }
        }
        return alRet;
    }
   //判断时间段内是否有预约
    public bool GetIsReserve(uint nStartTime, uint nEndTime, string szUsableNumArray)
    {    
        uint i = nStartTime;
        bool bRes = false;
        for (; i < nEndTime; i++)
        {
            if (szUsableNumArray[(int)i] == '0')
            {
                return true;
            }
           
        }
        return bRes;
    }
}
