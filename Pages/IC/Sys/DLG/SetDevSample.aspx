<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDevSample.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">    
        <table> <TR><td style="width:80px">测试内容名称:</td><td style="text-align:left"><input type="text"  id="search" name="search" style="width:300px" /></td></tr></table>
        <div style="margin-top:20px">
        <table>
            <tr><td class="btnRow"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">关闭</button></td></tr>    
           <tr><td>
               <%=m_szSample %>
               </td></tr>
            
        </table>
            </div>   
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable input, select, .input
        {
            width: 100px;
        }
          .formtable table tr td label{
        margin-right:15px;
        }
        .formtable table{border-bottom:1px solid #000;border-right:1px solid #000;}
        .formtable table td{text-align:center;border-left:1px solid #000;height:30px; border-top:1px solid #000}
        .formtable table th{text-align:center;border-left:1px solid #000;height:30px; border-top:1px solid #000}
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#search").keyup(function () {
            var sampleName = $(this).val();
            if (sampleName != "") {
                $("input[name='sampleList']").each(function () {
                    var vcheck = $(this).parent();
                    if (vcheck.html().indexOf(sampleName)>-1) {
                        vcheck.css("display","inline");
                    }
                    else {
                        vcheck.css("display", "none");
                    }
                });
            } else {
                $("input[name='sampleList']").each(function () {
                    var vcheck = $(this).parent();
                   {
                       vcheck.css("display", "inline");
                    }
                });
            }

        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
       
        setTimeout(function () {
            
        }, 1);
    });
</script>
</asp:Content>
