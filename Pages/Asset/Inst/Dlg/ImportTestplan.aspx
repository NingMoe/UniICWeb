<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ImportTestplan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server" enctype="multipart/form-data">
    <div class="formtitle"><span>导入</span><span name="szTitle"></span></div>
    <div class="formtable">
        <input type="hidden" name="Submit" value="true" />
        <input type="hidden" name="dwStep" id="dwStep" />
        <input type="hidden" name="szTitle" />
        <input type="hidden" name="szFileName" />
        <input type="hidden" name="szFilePath" />
        <input type="hidden" name="szTemplateFile" />
        <input type="hidden" name="szDestName" />
        <input type="hidden" name="szDestFieldList" />

<%if(pagedata.dwStep == 0){ %>
        <div id="Step1">
            <p class="header ui-widget-header"><span class="StepName">第一步：上传文件</span><span class="msgtip" name="szMessage"></span></p>
            <table id="tblStep1">
                <tr>
                    <th>文件：</th>
                    <td><input type="file" name="UpFile" id="UpFile" size="45"/></td>
                </tr>
                <tr><td colspan="2" class="tblBtn">
                    <button type="submit" id="OK">下一步</button>
                    <button type="button" id="Cancel">取消</button>
                </td></tr>
            </table>
            <br /><hr />
            <p>导入文件例子(<a href="../../../upload/TestPlan.csv">点击下载</a>):</p>
            <%=m_szOut %>
        </div>
<%} else if(pagedata.dwStep == 2){ %>
        <div id="Step2">
          
            <p class="header ui-widget-header"><span class="StepName">第二步：确认字段格式</span><span class="msgtip" name="szMessage"></span></p>
            <div class="result"><p>共解析到<span name='dwTotalLine'></span>条数据</p></div>
              选择学期：<select id="dwYearTerm" name="dwYearTerm"><%=m_szTerm %></select>
            <%=m_szOut %>

            <div class="tblBtn">
                <button type="button" id="Prev">上一步</button>
                <%if(pagedata.dwTotalLine > 0){ %>
                <button type="submit" class="btnClss" id="btnOk2">确定</button>
                <%} %>
                <button type="button"  class="btnClss" id="btnCanle2">取消</button>
            </div>
        </div>
<%} else if(pagedata.dwStep == 3){%>
        <div id="Step3">
            <p class="header ui-widget-header"><span class="StepName">第三步：导入结束</span></p>
            <div class="result">
                <p>【<span name="szFileName"></span>】总<span name='dwTotalLine'></span>条数据，其中<span class="Imported" name="dwImported"></span>条导入成功 , <span class="Failed" name="dwFailed"></span>条导入失败。</p>
                <%if(pagedata.dwFailed > 0){ %>
                <p>失败数据行号：<span class="ErrLines" name="szErrLines"></span>。</p>
                <p><a href="<%=pagedata.szErrListFile %>">下载导入失败的数据</a></p>
                <%} %>
            </div>
            <br />
            <div class="tblBtn">
                <button type="button" id="Close">关闭</button>
            </div>
        </div>
<%} else {%>
        <div>
            <p class="header ui-widget-header"><span class="StepName">页面不存在</span></p>
            <div class="msg" name="szMessage"></div>
            <div class="tblBtn">
                <button type="button" id="Button3">关闭</button>
            </div>
        </div>
<%} %>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable {
            padding: 10px;
        }

        #tblStep1 {
            width:400px;
        }
        #tblStep1 table td {
            text-align:center;
        }

        .header {
            margin-bottom: 10px;
            padding:3px;
        }
        .tblBtn {
            margin-top: 10px;
        }

        .msgtip {
            font-size: 12px;
            color: yellow;
            padding-left:30px;
        }
        .msg {
            font-size:16px;
            text-align:center;
        }
        .result p{
            font-size:15px;
            text-align:left;
        }
        .result span {
            font-size:17px;
            font-family:Cambria;
            color:blue;
            padding: 3px;
        }
        .result .Imported {
            
        }
        .result .Failed {
            color:red;
        }
        .result .ErrLines {
            color:red;            
        }

        .StepName {
            font-size:15px;
        }
        #Step2 .tblCSV th{
            background: #eeeeee;
            text-align:center;
            border:1px solid #808080;
        }
        #Step2 .tblCSV td{
            border:1px solid #808080;
        }
        
        #Step1 .tblCSV th{
            background: #f0f0f0;
            text-align:center;
            border:1px solid #dddddd;
            color: #666666;
        }
        #Step1 .tblCSV td{
            border:1px solid #dddddd;
            color: #666666;
        }
        #UpFile {
            width:320px;
            border:1px solid #666666;
        }
        table.tblCSV {
        }

        table.tblCSV td.importTblMore {
            text-align:center;
            border:1.5px dashed #808080 !important;
        }
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        $("#Prev").button().click(function () {
            location.href = location.href;
        });
        $(".btnClss").button();

        $("#Cancel").button().click(Dlg_Cancel);
        $("#btnCanle2").click(Dlg_Cancel);
        $("#Close").click(Dlg_OK);
    });
</script>
</asp:Content>