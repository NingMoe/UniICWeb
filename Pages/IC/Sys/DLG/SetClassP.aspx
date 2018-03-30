<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetClassP.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">  
        <input id="dwCtrlSN" name="dwCtrlSN" type="hidden" />
        <input id="IsNewCtl" name="bIsNew" type="hidden" runat="server" />
        <table>
            <tr class="trInput">
                <td>名称:</td><td><input id="szCtrlName" name="szCtrlName" class="validate[required]" /></td>
            </tr>
            <tr><td>类型:</td><td><select id="dwCtrlMode" name="dwCtrlMode"><%=m_dwCtrlMode%></select></td></tr>         
            <tr><td>年龄:</td><td><select id="dwForAges" name="dwForAges"><%=m_dwForAges%></select></td></tr>  
             <tr><td>备注:</td><td><input id="szMemo" name="szMemo" /></td></tr>  
            
        <tr><td></td><td><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtitle {
            padding:6px;
            background: #d0d0d0;
            height:30px;
            color: #fff;
            font-size: 20px;
        }
        .formtable table{
            text-align:center;
            margin:auto;
        }
        td {
         padding:6px;
        }
        input, select {
            width: 200px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var idPre = "ctl00_Content_";
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);

        });
        function ChbTimeEable(tar, Time) {

            if (tar.checked) {
                var list = GetDDLByTime(Time, true);//true代表选中
                for (i = 0; i < list.length; i++) {
                    list[i].removeAttr("disabled");
                }
            }
            else {
                var list = GetDDLByTime(Time, false);
                for (i = 0; i < list.length; i++) {
                    list[i].attr("disabled","disabled");
                }
            }
        }
        function ChbWeekEable(tar, Week) {

            if (tar.checked) {
                var list = GetDDLByWeek(Week, true);//true代表选中
                for (i = 0; i < list.length; i++) {
                    list[i].removeAttr("disabled");
                }
            }
            else {
                var list = GetDDLByWeek(Week, false);
                for (i = 0; i < list.length; i++) {
                    list[i].attr("disabled", "disabled");
                }
            }
        }
        
     
    </script>
</asp:Content>
