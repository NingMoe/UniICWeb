<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewTerm.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  
    <div class="formtable">        
        <br />
        <br />
        <table>
            <tr style="vertical-align:top;"><th>学期编号：</th><td><input id="dwYearTerm" name="dwYearTerm" class="validate[required,validate[custom[onlyNumber]]"" /><br /><label style="color:red">推荐:20121301(2012-2013第一学期)</label></td><th>　　　名称：</th><td><input id="szMemo" name="szMemo" class="validate[required]" /></td></tr>        
           <tr><th>开始日期：</th><td style="text-align:left"><input type="text" id="dwBeginDate"  name="dwBeginDate" readonly="readonly" class="validate[required]" /></td><th>结束日期:</th><td style="text-align:left"><input type="text" id="dwEndDate" name="dwEndDate" readonly="readonly"   class="validate[required]" /></td></tr>
            <tr><td colspan="4" class="tblBtn"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable table{
        }
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {                  
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
            var dateNow = new Date();
            var Month=dateNow.getMonth() + 1;
            if(Month<10)
            {
                Month="0"+Month;
            }
            var date=dateNow.getDate();
            if(date<10)
            {
                date="0"+date;
            }
            var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
            $("#dwEndDate").val(dateNowFor);
            $("#dwBeginDate").val(dateNowFor);
        }, 1);     
        $("#dwBeginDate").datepicker();
        $("#dwEndDate").datepicker();
     
    });
</script>
</asp:Content>
