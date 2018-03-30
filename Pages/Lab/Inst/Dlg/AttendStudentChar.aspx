<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="AttendStudentChar.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server" enctype="multipart/form-data">
     
   <div id="container" style="width: 650px; height: 380px; margin: 0 auto"></div>

            <div style="margin:10px auto;text-align:center">
                <button type="button" id="Cancel">关闭</button>
            </div>
       
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #addDiv table th
        {       
            height:30px;   
            text-align:right;
        }      
           #addDiv table td input
        {       
             margin-left:10px;
             height:18px;
             width:140px;
        }
             .ui-datepicker select.ui-datepicker-year { width: 43%;}
            .tb_infoInLine td input {
            width:120px;
            }
            .ui-autocomplete{
       z-index: 11111;
}
    </style>
  <script language="javascript" type="text/javascript" src="<%=MyVPath %>themes/js/MainJScript.js"></script>
   <script>
       $(function () {
           $('#container').highcharts({
               chart: {
                   type: 'line'
               },
               title: {
                   text: '到课率'
               },
               subtitle: {
                   text: '上课后到课率'
               },
               xAxis: {
                   categories: [<%=szRateMin %>]
               },
               yAxis: {
                   title: {
                       text: '到课率'
                   },
                   min: 0
               },
               tooltip: {
                   formatter: function () {
                       return '上课后' + this.x + '分钟到课率 ' + this.y;
                   }
               },

               series: [{
                   name: '到课率',
                   data: [<%=szRate %>]
               }]
           });
           $("#Cancel").button().click(Dlg_Cancel);
       }); 
    </script>
</asp:Content>
