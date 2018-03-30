<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="attendStudentRec.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server" enctype="multipart/form-data">
        <div class="formtitle">到课情况</div>        
         <div style="margin:10px auto;text-align:center">
                <button type="button" id="Cancel">关闭</button>
            </div>
               <div class="content" style="margin-top:10px">
                   <div><%=szOut %></div>
            <table class="ListTbl">
                <thead>
                    <tr>
                         <th>学号</th>
        <th>姓名</th>
        <th>使用总时间(分钟)</th>
        <th>状态</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
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
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#Cancel").button().click(Dlg_Cancel);
    });
    </script>
</asp:Content>
