<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ManGroupList.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle">管理员名单</div> 
    <div class="formtable">
        <table id="dlgTable" class="ListTbl UniTable" style="width:450px">
            <thead>
            <tr>
                <th>工号</th><th>姓名</th>
            </tr>
                </thead>
            <%=m_manGroupList %>
            <tbody>
            <tr><td colspan="2" style="text-align:center"><button type="button" id="Cancel">关闭</button></td></tr>
                </tbody>
        </table>
    </div>    
         
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" >
   $(function () {        
    
        $("#Cancel").button().click(Dlg_Cancel);
       
    });
</script>
    <style>
        #dlgTable th {
        text-align:center;
        }
        #dlgTable td{
        text-align:center;
        }
    </style>
</asp:Content>
