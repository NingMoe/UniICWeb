<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetSW.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">  
        <input id="dwID" name="dwID" type="hidden" runat="server" />
        <input id="IsNewCtl" name="bIsNew" type="hidden" runat="server" />
        <table>
            <tr class="trInput">
                <td>������:</td>
                <td>
                    <div class="UISelect" data-id="GroupList" data-name="GroupListName" data-tip="����������ƣ���ӵ�����" data-source="../../Data/searchSW.aspx">
                        <input name="GroupList" value="<%=m_szSWID %>" type="hidden"/>
                        <input name="GroupListName" value="<%=m_szSWName %>" type="hidden"/>
                    </div>
                      
                    </td>
            </tr>
           
             <tr><td>��ע:</td><td><input id="szMemo" name="szMemo" runat="server" /></td></tr>  
            
        <tr><td></td><td><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
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
       
        
     
    </script>
</asp:Content>
