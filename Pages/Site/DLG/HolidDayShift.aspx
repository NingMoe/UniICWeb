<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="HolidDayShift.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
  <input type="hidden" id="selectID" name="selectID" />
    <div class="formtable">
        <div style="margin-top:50px">
        <table>
           <tr>
               <td>
                  调课前日期：<input type="text" id="dwOldDate"  name="dwOldDate" readonly="readonly" class="validate[required]" />
               </td>
               <td>
                  调课后日期：<input type="text" id="dwNewDate"  name="dwNewDate" readonly="readonly" class="validate[required]" />
               </td>
           </tr>
        </table>
            </div>
        <div style="margin:0px auto;">
            <div style="width:180px;margin:10px auto;">
            <button type="submit" id="OK">调课</button><button type="button" id="Cancel">关闭窗口</button>
        </div>
         </div> 
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
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        setTimeout(function () {
            var dateNow = new Date();
            var Month = dateNow.getMonth() + 1;
            if (Month < 10) {
                Month = "0" + Month;
            }
            var date = dateNow.getDate();
            if (date < 10) {
                date = "0" + date;
            }
            var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
            $("#dwOldDate").val(dateNowFor);
            $("#dwNewDate").val(dateNowFor);
        }, 1);
        $("#dwOldDate").datepicker();
        $("#dwNewDate").datepicker();
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
</asp:Content>
