<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvGet.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">

        <input type="hidden" id="szidh" runat="server" />
        <input type="hidden" id="szOwnerID" runat="server" />
        <input type="hidden" id="szOwnerNameH" runat="server" />
        <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size: 18px; font-weight: bold;">�鿴ԤԼ</label>
            </div>
        </div>
        <div style="text-align: center; vertical-align: middle">          
                        <table id="idResv" class="clsResv" style="width:600px; margin: 0 auto; border-top:1px solid #000;border-left:1px solid #000; border-right:1px solid #000";cellspacing="1">
                            <%if(szTestName!="") {%>
                             <tr>
                                <th class="resvth">�������ݣ�</th>
                                <td class="resvtd"> <label><%=szTestName %></label></td>
                            </tr>
                            <%} %>
                            <tr>
                                <th class="resvth">����ռ䣺</th>
                                <td class="resvtd"> <label><%=szDevName %></label></td>
                            </tr>
                            <tr>
                                <th class="resvth">
                                    <span>����ʱ�䣺</span></th>
                                <td class="resvtd">
                                    <div>
                                        <label id="lblszResvTime" runat="server"></label>
                                    </div>
                                </td>
                            </tr>
  <%if(szMemo!="") {%>
                            <tr>
                                <th class="resvth"><span>����˵����</span></th>
                                <td class="resvtd"> <label><%=szMemo%></label></td>
                            </tr>
                            <%} %>
                            <tr>
                                <th class="resvth">
                                    <span>�����ˣ�</span></th>
                                <td class="resvtd">
                                    <label><%=szOwnerName%>(<%=szOwneTel%>)</label>
                                </td>
                            </tr>
                             <tr>
                                <th class="resvth">
                                    <span>������Ա��</span></th>
                                <td class="resvtd">
                                    <label><%=szGroupStudent%></label>
                                </td>
                            </tr>
                             <tr>
                                <th class="resvth">
                                    <span>���븽����</span></th>
                                <td class="resvtd">
                                    <label><a target="_blank" href="<%=szURl %>"><%=szOpHref %></a></label>
                                </td>
                            </tr>
                             <tr>
                    <th>
                        <div style="text-align: right; margin-top: 10px;">
                            <input type="button" id="btnCheckOK" style="height: 30px; display: none" value="������д����" />
                        </div>
                    </th>
                                 <td></td>
                </tr>
                        </table>   
          
        </div>
        
        <div style="width: 99%; text-align: center;margin-top:20px;">
            <div style="margin: 0 auto;">
             
                <input type="button" id="btnCancel" value="�ر�" />
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">             
         .resvth{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: right;
                height: 35px;
         }
         .resvtd{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: left;
                height: 35px;
         }
            .resvtd label {
            margin-left:10px;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btnCancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
