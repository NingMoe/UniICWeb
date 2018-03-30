<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="IdentUsingStatPie.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">���ͳ��</h2>        
        <input type="hidden" value="none" name="type" id="type" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                 <div class="tb_info">
              <div class="UniTab" id="tabl">
                   <a href="IdentUsingStat.aspx">���ͳ��</a>
              
                <a href="IdentUsingStatPie.aspx">���ͳ�Ʊ�ͼ</a>
            </div>
        </div>
                <div style="margin-left: 100px; margin-bottom: 30px">
                   <table style="width: 750px">
                         <%if(ConfigConst.GCICTypeMode==1) { %>
                         <tr>
                            <th class="thHead"></th>
                           <td class="tdHead" colspan="3" style="text-align:center;height:35px">
                            <select id="dwYearTerm" name="dwYearTerm">
                                <%=m_TermList %>
                            </select>    
                            </td>
                            
                           
                        </tr>

                     
                        <%} else {%>
                           <tr>
                            <th class="thHead">��ʼ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">��������:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                           
                        </tr>
                        <%} %>
                         <tr>
                           <th class="thHead">
                           <%=ConfigConst.GCDeptName %>:
                               </th>
                           <td colspan="3">
                               <select id="dwDept" name="dwDept">
                                  <%=szDept %>
                                    </select>
                           </td>
                       </tr>
                        <tr>
                             <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="��ѯ" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div class="content">
            <div id="szResv" class="PieStat" data-color="1" style="min-width:0px">
      <%=szResvRate %>
                </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                $("#btnOK,#btnExport").button();
                $("#btnExport").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    $.lhdialog({
                        title: '����',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/IdentUsingStatExport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate
                    });
                });
            });
        </script>
        <style>
            .thHead {
                background: #e5f1f4;
                text-align: right;
            }

            .tdHead {
                text-align: left;
            }

            td input {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
