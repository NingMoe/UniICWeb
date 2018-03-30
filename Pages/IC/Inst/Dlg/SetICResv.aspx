<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetICResv.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <table>
            <tr><th>���ƣ�</th><td><div id="resvDevName"><%=m_szDevName %></div></td></tr>
         <tr>
                    <th>ԤԼ���ڣ�</th>
                    <td>
                        <input id="szPreDate" name="szPreDate" class="validate[required]"  value="<%=szPreDate %>"/></td>
                </tr>
                <tr>
                    <th>��ʼʱ�䣺</th>
                    <td>
                        <select name="startTimeHour" id="startTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>ʱ
                        <select  name="startTimeMin" id="startTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>��
                     </td>
                    <th>����ʱ�䣺</th>
                    <td>
                         <select name="endTimeHour" id="endTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>ʱ
                        <select  name="endTimeMin" id="endTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>��
                        </td>
                </tr>
            <tr><td colspan="2" style="text-align:center"><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
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
<script language="javascript" type="text/javascript" >
    $(function () {
      
        $("#szPreDate").datepicker({
        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
       
    });
</script>
</asp:Content>
