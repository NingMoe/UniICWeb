<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewYardResvRule.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
  <input id="dwRuleSN" name="dwRuleSN" type="hidden" />
             <input id="dwGroupID" name="dwGroupID" type="hidden" value="0" />
            <input id="dwResvPurpose2" name="dwResvPurpose2" type="hidden" value="59" />
           
            <table>
                <tr>
                    <th>��������:</th>
                    <td><input type="text" id="szRuleName" name="szRuleName" class="validate[required]" /></td>
                    <th>�����:</th>
                    <td><select id="dwExtValue" name="dwExtValue"><%=m_szYardActivity %></select></td>
                </tr>
               <tr>
                    <th class="tdheadRight">��ݣ�</th>
                    <td class="tdContextLeft">
                        <select id="dwIdent" name="dwIdent"><%=m_szIdent%></select></td>
                    <th class="tdheadRight"><%=ConfigConst.GCDeptName %>:</th>
                    <td class="tdContextLeft">
                          <select id="dwDeptID" name="dwDeptID"><%=m_szDept%></select>
                      </td>
                </tr>
               
                <tr>
                      <th class="tdheadRight"><%=ConfigConst.GCKindName %>��</th>
                    <td class="tdContextLeft">
                           <input type="text" id="szDevKindName" name="szDevKindName" value="ȫ��" /></td>
                      <th class="tdheadRight"><%=ConfigConst.GCDevName %>��</th>
                    <td class="tdContextLeft">   
                   <select  id="dwDevID" name="dwDevID" ><%=m_szDevice %></select>
                        </td>
                </tr>
           
                <tr>
                    <th class="tdheadRight">���ԤԼʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input type="text"  id="dwMinResvTime" name="dwMinResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                    <th class="tdheadRight">�ԤԼʱ��(����)��</th>
                    <td class="tdContextLeft">
                        <input type="text"  id="dwMaxResvTime" name="dwMaxResvTime" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                </tr> 
                  <tr>
                    <th class="tdheadRight">����ԤԼʱ����(����)��</th>
                    <td class="tdContextLeft">
                        <input  type="text" id="dwSeriesTimeLimit" name="dwSeriesTimeLimit" class="validate[required,validate[custom[onlyNumber]]" value="0" /></td>
                      <td></td>
                      <td class="tdContextLeft">
                      ������ǰ��
                          <input id="dwLatestResvTime" name="dwLatestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="0" />
                          �쵽
                          <input id="dwEarliestResvTime" name="dwEarliestResvTime" style="width: 30px" class="validate[required,validate[custom[onlyNumber]]" value="6" />
                          ��ſ�ԤԼ
                          </td>
                </tr> 
                <tr>
                 <th class="tdheadRight">���ȼ���</th>
                    <td colspan="3" class="tdContextLeft">
                        <input type="text" id="dwPriority" name="dwPriority" /></td>
                  
            </tr>
                 <td colspan="4"><hr /></td>
              <%=m_szExtCheckType %>
                 <tr>
               <td colspan="4"><hr /></td>
                </tr>
                <tr>
                    <td style="" class="btnRow" colspan="4">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .table tr  td{ border:1px #000000 solid;}
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            AutoDevKind($("#szDevKindName"), 2, $("#dwDevKind"), null, true);

        <%if (bSet)
          {%>
       
            <%}%>
            AutoDept($("#szDeptName"),2,$("#dwDeptID"),false);
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
          setTimeout(function () {
           
          }, 1);
        
    });
    </script>
</asp:Content>
