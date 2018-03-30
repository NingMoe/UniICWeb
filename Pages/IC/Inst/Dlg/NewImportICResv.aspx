<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewImportICResv.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server" enctype="multipart/form-data">
    <div class="formtitle"><span>����</span><span name="szTitle"></span></div>
    <div class="formtable">
        <input type="hidden" name="Submit" value="true" />
        <input type="hidden" name="dwStep" />
        <input type="hidden" name="szTitle" />
        <input type="hidden" name="szFileName" />
        <input type="hidden" name="szFilePath" />
        <input type="hidden" name="szTemplateFile" />
        <input type="hidden" name="szDestName" />
        <input type="hidden" name="szDestFieldList" />
<%if(pagedata.dwStep == 0){ %>
        <div id="Step1">
            <p class="header ui-widget-header"><span class="StepName">��һ�����ϴ��ļ�</span><span class="msgtip" name="szMessage"></span></p>
            <table id="tblStep1">
                <tr>
                    <th>�ļ���</th>
                    <td><input type="file" name="UpFile" id="UpFile" size="45"/></td>
                </tr>
                <tr><td colspan="2" class="tblBtn">
                    <button type="submit" id="OK">��һ��</button>
                    <button type="button" id="Cancel">ȡ��</button>
                </td></tr>
            </table>
            <br /><hr />
            <p>�����ļ�����(<a href="meetimport.csv">�������</a>):</p>
            <%=m_szOut %>
        </div>
<%} else if(pagedata.dwStep == 2){ %>
        <div id="Step2">
            <p class="header ui-widget-header"><span class="StepName">�ڶ�����ȷ���ֶθ�ʽ</span><span class="msgtip" name="szMessage"></span></p>
            <div class="result"><p>��������<span name='dwTotalLine'></span>������</p></div>
            <%=m_szOut %>

            <div class="tblBtn">
                <button type="button" id="Prev">��һ��</button>
                <%if(pagedata.dwTotalLine > 0){ %>
                <button type="submit" class="btnClss" id="Button1">ȷ��</button>
                <%} %>
                <button type="button"  class="btnClss" id="Cancel2">ȡ��</button>
            </div>
        </div>
<%} else if(pagedata.dwStep == 3){%>
        <div id="Step3">
            <p class="header ui-widget-header"><span class="StepName">���������������</span></p>
            <div class="result">
                <p>��<span name="szFileName"></span>����<span name='dwTotalLine'></span>�����ݣ�����<span class="Imported" name="dwImported"></span>������ɹ� , <span class="Failed" name="dwFailed"></span>������ʧ�ܡ�</p>
                <%if(pagedata.dwFailed > 0){ %>
                <p>ʧ�������кţ�<span class="ErrLines" name="szErrLines"></span>��</p>
            <!--    <p><a href="<%=pagedata.szErrListFile %>">���ص���ʧ�ܵ�����</a></p>-->
                <%} %>
            </div>
            <br />
            <div class="tblBtn">
                <button type="button" id="Close">�ر�</button>
            </div>
        </div>
<%} else {%>
        <div>
            <p class="header ui-widget-header"><span class="StepName">ҳ�治����</span></p>
            <div class="msg" name="szMessage"></div>
            <div class="tblBtn">
                <button type="button" id="Button3">�ر�</button>
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
        setInterval(function () {
            $("#OK").button();
            $(".btnClss").button();
            $("#Prev").button().click(function () {
                location.href = location.href;
            });
            $("#Cancel").button().click(Dlg_Cancel);
            $("#Cancel2").button().click(Dlg_Cancel);
            $("#Close").button().click(Dlg_OK);
        }, 100);
        
    });
</script>
</asp:Content>