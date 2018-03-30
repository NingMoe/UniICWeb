<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDevFarDetail.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">        
        <table>    
            <tr>
                <th>�շ����</th>
                <th>�������Էѱ���(%)</th>
                <th>���Ż������(%)</th>
                <th>����ѱ�����%��</th>
            </tr>
            <tr>
                <td>ʹ�÷�</td>
                <td><input type="text" name="useTestRate"  id="useTestRate"  class="txt validate[required,validate[custom[onlyNumber]]" value="0" runat="server" /></td>
                <td><input type="text" name="useOpenRate"  id="useOpenRate" class="txt validate[required,validate[custom[onlyNumber]]" value="0" runat="server" /></td>
                <td><input type="text" name="useServiceRate" id="useServiceRate" class="txt" value="0" runat="server" /></td>
            </tr>  
             <tr>
                <td>��Ʒ��</td>
                <td><input type="text" name="sampleTestRate" id="sampleTestRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0" runat="server" /></td>
                <td><input type="text" name="sampleOpenRate"  id="sampleOpenRate"  class="txt validate[required,validate[custom[onlyNumber]]" value="0"  runat="server" /></td>
                <td><input type="text" name="sampleServiceRate" id="sampleServiceRate" class="txt validate[required,validate[custom[onlyNumber]]" value="0"  runat="server" /></td>
            </tr> 
             <tr>
                <td>�����</td>
                <td><input type="text" name="entTestRate" id="entTestRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0"  runat="server" /></td>
                <td><input type="text" name="entOpenRate" id="entOpenRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0" runat="server" /></td>
                <td><input type="text" name="entServiceRate" id="entServiceRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0"  runat="server" /></td>
            </tr>    
            <tr>
                <td>�Ĳķ�</td>
                <td><input type="text" name="consTestRate" id="consTestRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0"  runat="server" /></td>
                <td><input type="text" name="consOpenRate" id="consOpenRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0" runat="server" /></td>
                <td><input type="text" name="consServiceRate" id="consServiceRate" class="txt validate[required,validate[custom[onlyNumber]]"  value="0"  runat="server" /></td>
            </tr>   
            <tr><td colspan="4" class="btnRow"><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtable input, select, .input
        {
            width: 100px;
        }
        .formtable table{border-bottom:1px solid #000;border-right:1px solid #000;}
        .formtable table td{text-align:center;border-left:1px solid #000;height:30px; border-top:1px solid #000}
        .formtable table th{text-align:center;border-left:1px solid #000;height:30px; border-top:1px solid #000}
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        <%if(bSet){%>       
        <%}%>       
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
       
        setTimeout(function () {
            
        }, 1);
    });
</script>
</asp:Content>
