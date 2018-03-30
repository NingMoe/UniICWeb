<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PersonUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">����ʹ�������а�</h2>        
        <input type="hidden" value="none" name="type" id="type" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
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
                            <th class="thHead">��������:</th>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                            
                        </tr>
                        <%} %>
                       <tr>
                           <th class="thHead">��ϵͳ:</th>
                          <td  class="tdHead">
                              <select id="dwClassKind" name="dwClassKind">
                                    <option value="0">ȫ��</option>
                                 <%if ((ConfigConst.GCSysKind & 1) > 0)
                                    { %>
                                <option value="1">���޼�</option>
                                <%} if((ConfigConst.GCSysKind & 8) > 0) {%>
                                <option value="8">��λ</option>
                                 <%} if((ConfigConst.GCSysKind & 2) > 0) {%>
                                <option value="2">����������</option>
                                <%} %>
                              </select>
                          </td>
                            <th class="thHead">����ʽ:</th>
                               <td  class="tdHead">
                                   <select id="orderkey" name="orderkey">
                                       <option value="dwUseTime">ʹ����ʱ��</option>
                                        <option value="dwUseTimes">ʹ�ô���</option>
                                       </select>
                               </td>
                           
                       </tr>
                        <tr>
                             <td class="tdHead" colspan="6" style="text-align:center">
                                <input type="submit" id="btnOK" value="��ѯ" />
                                <input type="button" id="btnExport" value="����" />
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div class="content">
            <div style="float:left">
                
                </div>
            <table class="ListTbl">
                <thead>
                    
                    <tr>
                        <th width="100px">ѧ����</th>
                        <th>����</th>
                        <th>�༶</th>
                         <th>ѧԺ</th>
                        <th name="dwUseTimes">ʹ�ô���</th>
                        <th name="dwUseTime">ʹ����ʱ��</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />

        </div>
        <script type="text/javascript">
            $(function () {
                //$(".ListTbl").UniTable();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                $("#btnOK,#btnExport").button();
                $("#btnExport").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var orderkey = $("#orderkey").val();
                    $.lhdialog({
                        title: '����',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/personusingstatexport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate + '&orderkey=' + orderkey
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
