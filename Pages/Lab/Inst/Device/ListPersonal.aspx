<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListPersonal.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>����ʹ�����豸</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--������5�����༶��5������������5�� ----->
                <%=m_szOpts %></div>
          
            <div class="tb_btn" style="float:left">
               <table border="1">
                    <tr>
                        <td>
                            <button type="button" id="Back">����</button></td>
                        <td>ʹ���з���:| 
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">����1</label>
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">����2</label>
                        </td>
                        <td>�豸״̬:
                            <label><input class="enum" type="radio" name="devRunState" value="1">����</label>
                            <label><input class="enum" type="radio" name="devRunState" value="4">��ԤԼ</label>
                            <label><input class="enum" type="radio" name="devRunState" value="2">ʹ����</label>
                        </td>
                        <td>�豸����:
                              <label><input class="enum" type="checkbox" name="devRunState" value="1">DELL����</label>
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">ƻ������</label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>���</th>
                        <th>��������</th>
                        <th>�����豸����</th>
                        <th>ʹ����</th>
                        <th>��ϵ��ʽ</th>
                        <th>���Ʒ�ʽ</th>
                        <th>��ʼʱ��</th>
                        <th>��ʹ��ʱ��</th>
                        <th>�Ʒѵ�λ</th>
                        <th width="25px">����</th>
                    </tr>
                    <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                       <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                       <tr>
                        <td>DellPC001</td>
                        <td>����1</td>
                        <td>Dell����</td>
                        <td>����(201100972)</td>
                        <td>15967104608</td>
                        <td>��ֹ��վ��/��ֹ��Ϸ��</td>
                        <td>17:45</td>
                        <td>45����</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>

                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
           <div class="ColumnStat tblBottomStat" data-color="1">
                <h1><span>--------</span><strong>ʹ����Ŀ</strong></h1>
                <p><span>����1</span><strong>12</strong></p>
                <p><span>����2</span><strong>21</strong></p>
                <p><span>����3</span><strong>17</strong></p>
               <p><span>����4</span><strong>17</strong></p>
               <p><span>����5</span><strong>17</strong></p>
            </div>
             <div><label>�豸ʹ����Ŀ��</label><a>30��</a></div>
        <div id="FreeUserStat" class="LineStat" data-color="0" data-name="ʹ����" data-unit="��">
            <p><span>8:00</span><span>7</span></p>
               <p><span>8:00</span><span>6</span></p>
               <p><span>9:00</span><span>9</span></p>
               <p><span>10:00</span><span>11</span></p>
               <p><span>11:00</span><span>20</span></p>
               <p><span>12:00</span><span>15</span></p>
               <p><span>13:00</span><span>21</span></p>
            <p><span>14:00</span><span>24</span></p>
            <p><span>15:00</span><span>25</span></p>
            <p><span>16:00</span><span>26</span></p>
            <p><span>17:00</span><span>7</span></p>
        </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" title="����Ϣ"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="�鿴��Ļ"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="����"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="���ÿ��Ʒ�ʽ"><img src="../../../themes/icon_s/11.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
