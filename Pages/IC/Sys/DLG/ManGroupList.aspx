<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ManGroupList.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle">����Ա����</div> 
    <div class="formtable">
        <table id="dlgTable" class="ListTbl UniTable" style="width:450px">
            <thead>
            <tr>
                <th>����</th><th>����</th>
            </tr>
                </thead>
            <%=m_manGroupList %>
            <tbody>
            <tr><td colspan="2" style="text-align:center"><button type="button" id="Cancel">�ر�</button></td></tr>
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
