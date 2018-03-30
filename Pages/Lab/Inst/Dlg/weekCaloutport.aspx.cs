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
using System.IO;
using System.Text;
using MSWord = Microsoft.Office.Interop.Word;
using System.Reflection;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szRoom="";
    protected string m_szDev = "";
    protected string szWeekIndex = "";  
    protected bool bStupied = false;
    protected void Page_Load(object sender, EventArgs e)
    {
     
        int weekNow = GetWeekNow();
        UNITERM termNow;
        if (GetTermNow(out termNow))
        {
            uint uCount = 1;
            for (uint i = (uint)termNow.dwBeginDate; i <= (uint)termNow.dwEndDate;)
            {
                uint uStare = i;
                DateTime date = new DateTime();
                date = DateTime.Parse(GetDateStr(uStare));

                if (i == (uint)termNow.dwBeginDate)
                {
                    i = Parse(date.AddDays((uint)termNow.dwFirstWeekDays - 1).ToString("yyyyMMdd"));
                }
                else
                {
                    uStare = Parse(date.AddDays(1).ToString("yyyyMMdd"));
                    i = Parse(date.AddDays(7).ToString("yyyyMMdd"));
                }
                if (uCount == weekNow)
                {
                    szWeekIndex += GetInputItemHtml(CONSTHTML.option, "", "第" + uCount + "周", uStare + "," + i + "," + uCount, true);
                }
                else
                {
                    szWeekIndex += GetInputItemHtml(CONSTHTML.option, "", "第" + uCount + "周", uStare + "," + i + "," + uCount);
                }
                uCount = uCount + 1;

            }
        }
        string szWeek = Request["week"];
        if (IsPostBack)
        {


            MSWord.Application wordApp;
            MSWord.Document wordDoc;
            wordApp = new MSWord.ApplicationClass();
         
            TEACHINGRESVREQ vrParameter = new TEACHINGRESVREQ();
            TEACHINGRESV[] vrResult;

            vrParameter.dwBeginDate = Parse(szWeek.Split(',')[0]);
            vrParameter.dwEndDate = Parse(szWeek.Split(',')[1]);
            vrParameter.szReqExtInfo = new REQEXTINFO();
            vrParameter.szReqExtInfo.szOrderKey = "dwPreDate asc,BeginTime";
            vrParameter.szReqExtInfo.szOrderMode = "asc";
            string[,] resvWeek = new string[3, 7];
            if (m_Request.Reserve.GetTeachingResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    uint uResvTime = (uint)vrResult[i].dwTeachingTime;
                    uint uWeekDate = uResvTime % 100000 / 10000;
                    uint uBeginSec = uResvTime % 10000 / 100;
                    uint uSJD = 0;
                    if (uBeginSec > 5 & uBeginSec < 10)
                    {
                        uSJD = 1;
                    }
                    else if (uBeginSec >= 10)
                    {
                        uSJD = 2;
                    }
                    RESVDEV[] resvDev = vrResult[i].ResvDev;
                    string szResvRoom = "";
                    if (resvDev != null)
                    {
                        for (int k = 0; k < resvDev.Length; k++)
                        {
                            if (!(szResvRoom.IndexOf(resvDev[k].szRoomName) > -1))
                            {
                                if (szResvRoom == "")
                                {
                                    szResvRoom = szResvRoom + resvDev[k].szRoomName;
                                }
                                else
                                {
                                    szResvRoom = "," + szResvRoom + resvDev[k].szRoomName;
                                }
                            }
                        }
                        szResvRoom = "【" + szResvRoom + "】";

                    }
                    resvWeek[uSJD, uWeekDate] = resvWeek[uSJD, uWeekDate] + vrResult[i].szCourseName + "(" + vrResult[i].dwGroupUsers + "人)" + "," + vrResult[i].szGroupName + "," + vrResult[i].szTeacherName + "，" + uBeginSec + "-" + uResvTime % 100 + "节" + szResvRoom + (char)11;
                }
            }
            Object nothing = Missing.Value;
            object oMissing = System.Reflection.Missing.Value;

            wordDoc = wordApp.Documents.Add(ref nothing, ref nothing, ref nothing, ref nothing);
            wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientLandscape; //设置页面为纵向

            string content = "实验室排课表第" + szWeek.Split(',')[2] + "周(" + GetDateStr(Parse(szWeek.Split(',')[0])) + "～" + GetDateStr(Parse(szWeek.Split(',')[1])) + ")";
            Microsoft.Office.Interop.Word.Paragraph oPara1;

            /*
            oPara1 = wordDoc.Content.Paragraphs.Add(ref oMissing);//MSWord.Paragraph
            oPara1.Range.InsertParagraph();
            oPara1.Range.Text = content;
            oPara1.Range.Font.Size = 18;
            oPara1.Range.Font.Name = "黑体";
            // ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
            oPara1.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;
          //  oPara1.Range.InsertParagraphBefore();
        */    

            wordApp.Selection.TypeParagraph();


            object filePath = Server.MapPath("/") + "weekCalWord\\" + "第" + szWeek.Split(',')[2] + "周.docx";

            // object filePath = Server.MapPath("/") + "weekCalWord\\" + "my.docx";
       


            if (File.Exists(filePath.ToString()))
            {
                File.Delete(filePath.ToString());
            }
            int nColumnCount = 6;
            int nRows = 4;
            for (int i = 0; i < 3; i++)
            {
                if (resvWeek[i, 5] != "" && resvWeek[i, 5] != null)
                {
                    nColumnCount = 7;
                    break;
                }
            }

      

            object start = 1;
            object end = 1;
            Microsoft.Office.Interop.Word.Range tableLocation = wordDoc.Range(ref start, ref end);

            MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, nRows, nColumnCount, ref nothing, ref nothing);
          
            table.Borders.Enable = 1;
            table.Range.Font.Name = "宋体";
            table.Range.Font.Size = 12;

            System.Threading.Thread.Sleep(1000);

            string[] szSJDTemp = new string[4] { "", "上午", "下午", "晚上" };
            string[] szWeekTemp = new string[7] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            try
            {
                for (int i = 2; i <= nColumnCount; i++)
                {
                    table.Cell(1, i).Range.Text = szWeekTemp[(i - 2)];
                }
                for (int i = 2; i <= (nRows); i++)
                {
                    for (int j = 1; j <= (nColumnCount); j++)
                    {

                        if (j == 1)
                        {
                            table.Cell(i, j).Range.Text = szSJDTemp[(i - 1)];
                        }
                        else
                        {
                            table.Cell(i, j).Range.Text = resvWeek[(i - 2), (j - 2)];
                        }

                    }
                }

               

                wordDoc.SaveAs(ref filePath, ref oMissing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);
                wordDoc.Close(ref nothing, ref nothing, ref nothing);
                wordApp.Quit(ref nothing, ref nothing, ref nothing);
                //string downLoadPath = MyVPath + "\\\\" + "第" + ;
                Response.Redirect("../../../../weekCalWord/第"+ szWeek.Split(',')[2] + "周.docx", true);
                //Response.Write("<script>var newWindow =window.open('_blank');newWindow.location ='" + downLoadPath + "';//newWindow.close()</script>");
            }
            catch (Exception ex)
            {
                string szError = ex.ToString();
            }
            finally
            {



            }
        }

        /*

        System.IO.StringWriter swCSV = new System.IO.StringWriter();
        swCSV.WriteLine("<html><style>table#border{border-top:#000 1px solid;border-left:#000 1px solid;}table#border td{width:200px;text-align:center;font-size:12px;border-bottom:#000 1px solid;border-right:#000 1px solid;}</style><body>");
        if (bStupied != true)
        {
            swCSV.WriteLine("<h1 style='text-align:center'>实验室排课表第" + (szWeek.Split(',')[2]) + "周(" + GetDateStr(Parse(szWeek.Split(',')[0])) + "～" + GetDateStr(Parse(szWeek.Split(',')[1])) + ")</h1>");
        }
         swCSV.WriteLine("<table id='border' border='0' cellspacing='0'>");
         if (bStupied != true)
         {
             swCSV.WriteLine("<tr><td style='width:35px;'>时间</td><td>星期一</td><td>星期二</td><td>星期三</td><td>星期四</td><td>星期五</td><td>星期六</td></tr>");
         }
             string[] szSJDTemp = new string[3] { "上午", "下午", "晚上" };
             if (bStupied != true)
             {
                 for (int i = 0; i < 3; i++)
                 {

                     swCSV.WriteLine("<tr><td style='height:100px'>" + szSJDTemp[i] + "</td>");

                     for (int j = 0; j < 6; j++)
                     {
                         // if (resvWeek[i, j] != null)
                         {
                             swCSV.WriteLine("<td>" + resvWeek[i, j] + "</td>");

                         }
                     }
                     swCSV.WriteLine("</tr>");
                 }
             }
             else
             {
                 int k = 5;
                 for (int i = 0; i < 3;i++ )
                 {
                     if (resvWeek[i, 5] != ""&&resvWeek[i,5]!=null)
                     {
                         k = 6;
                         break;
                     }
                 }
                     for (int i = 0; i < 3; i++)
                     {
                         swCSV.WriteLine("<tr>");
                         for (int j = 0; j < k; j++)
                         {
                             // if (resvWeek[i, j] != null)
                             {
                                 swCSV.WriteLine("<td>" + resvWeek[i, j] + "</td>");

                             }
                         }
                         swCSV.WriteLine("</tr>");
                     }
             }

        swCSV.WriteLine("</table>");

        swCSV.WriteLine("</body></html>");
        DownloadFile(Response, swCSV.GetStringBuilder(),  "第"+szWeek.Split(',')[2]+"周.html");
        swCSV.Close();
        Response.End();
        */
    }
    public void output()
    {
        string content;
        MSWord.Application wordApp;
        MSWord.Document wordDoc;
        object filePath = Server.MapPath("/") + "weekCalWord\\" + @"\testWord.docx";
        wordApp = new MSWord.ApplicationClass();

        if (File.Exists(filePath.ToString()))
        {
            File.Delete(filePath.ToString());
        }
        Object nothing = Missing.Value;
        wordDoc = wordApp.Documents.Add(ref nothing, ref nothing, ref nothing, ref nothing);


        MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, 6, 6, ref nothing, ref nothing);
        table.Borders.Enable = 1;
        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j <6; j++)
            {
                table.Cell(i, j).Range.Text = i.ToString() + "行" + j.ToString() + "列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列列?~p列列列列列列列列列列列列列列列列";
            }
        }
        object format = MSWord.WdSaveFormat.wdFormatDocument;
        object oMissing = System.Reflection.Missing.Value;
        wordDoc.SaveAs(ref filePath, ref oMissing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);
        wordDoc.Close(ref nothing, ref nothing, ref nothing);
        wordApp.Quit(ref nothing, ref nothing, ref nothing);
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }
    public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode(strFileName);
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}