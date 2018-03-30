<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAdminPer.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">

            <table>
                <tr>
                    <th>
                        ����Ա��
                    </th>
                    <td colspan="2">
                        <div id="divName"></div>
                    </td>
                </tr>
                <tr>
                    <th style="text-align: center;font-size:large;font-weight:bolder">�ճ�����</th>
                    <th style="text-align: center;font-size:large;font-weight:bolder">����ͳ��</th>
                    <th style="text-align: center;font-size:large;font-weight:bolder">��������</th>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <div style="text-align: center;">
                            <label>
                                <input type="checkbox" name="LV1" value="DevRoomResvState.aspx" class="enum" /><%=ConfigConst.GCSysKindRoom %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevPCList.aspx" class="enum" /><%=ConfigConst.GCSysKindPC %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevLendList.aspx" class="enum" /><%=ConfigConst.GCSysKindLend %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevSeatList.aspx" class="enum" /><%=ConfigConst.GCSysKindSeat %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="Activityplan.aspx" class="enum" />�����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="ReserveRoomList.aspx" class="enum" />ԤԼ״��</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DisciList.aspx" class="enum" />ΥԼ�봦��</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="ICINTROClass.aspx" class="enum" />�ռ�չʾ����</label>
                            <br />
                        </div>
                    </td>
                    <td style="text-align: center">
                        <div>
                            <label>
                                <input type="checkbox" name="LV2" value="DevUsingStat.aspx" class="enum" /><%=ConfigConst.GCDevName %>ʹ����ͳ��</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="DevKindUsingStat.aspx" class="enum" /><%=ConfigConst.GCKindName %>ʹ����ͳ��</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="PersonUsingStat.aspx" class="enum" />����ʹ�����а�</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="LabUsingStat.aspx" class="enum" /><%=ConfigConst.GCLabName %>ʹ����ͳ��</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="RDevUsingTable.aspx" class="enum" /><%=ConfigConst.GCDevName %>ʹ����ͳ��ͼ</label>
                            <br />

                        </div>

                    </td>
                    <td style="text-align: center">

                          <label>
                                <input type="checkbox" name="LV3" value="SysKindRoom.aspx" class="enum" /><%=ConfigConst.GCSysKindRoom %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindPC.aspx" class="enum" /><%=ConfigConst.GCSysKindPC %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindLend.aspx" class="enum" /><%=ConfigConst.GCSysKindLend %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindSeat.aspx" class="enum" /><%=ConfigConst.GCSysKindSeat %>����</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="DevKind.aspx" class="enum" /><%=ConfigConst.GCKindName %>����</label>
                            <br />
                        <label>
                                <input type="checkbox" name="LV3" value="Control.aspx" class="enum" />����̨����</label>
                            <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            <%if (bSet)
              {%>
           <%}%>

            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);


            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
