<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListTeachingUnuse.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�������豸</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--������5�����༶��5������������5�� ----->
                <%=m_szOpts %>
            </div>
            <button type="button" id="Back">����</button>
            <div class="tb_btn">
                <div class="AdvOpts">
                    <div class="AdvLab">�߼�ѡ��</div>
                    <fieldset>
                        <legend>����</legend>
                        <label>
                            <input name="room" value="1" type="checkbox" />����1</label>
                        <label>
                            <input name="room" value="2" type="checkbox" />����2</label>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <tdead>
                    <tr>
                       
                        <th>�������!</th>
                         <th>����!</th>
                        <th>״̬!</th>
                        <th>�Ͽΰ༶!</th>
                        <th>�γ�</th>
                        <th>ʵ����Ŀ��</th>
                        <th>�Ͽν�ʦ</th>
                        <th>�Ͽ�ʱ��</th>
                        <th width="25px">����</th>
                    </tr>
                     <tr>
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="������" />Dev001</td>
                          <td>����1</td>
                        <td>������</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="������" />Dev001</td>
                          <td>����1</td>
                        <td>������</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                    <tr>
                    
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="������" />Dev001</td>
                            <td>����1</td>
                        <td>������</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="�ػ���" />Dev001</td>
                         
                        <td>����1</td>
                        <td>�ػ���</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="�ػ���" />Dev001</td>
                          <td>����1</td>
                        <td>�ػ���</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                      <tr>
                     
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="�ػ���" />Dev001</td>
                             <td>����1</td>
                        <td>�ػ���</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                      
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="�ػ���" />Dev001</td>
                           <td>����1</td>
                        <td>�ػ���</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                    <tr>
                     
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="�ػ���" />Dev001</td>
                           <td>����1</td>
                        <td>�ػ���</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="������" />Dev001</td>
                          <td>����1</td>
                        <td>������</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                      
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="������" />Dev001</td>  
                         <td>����1</td>
                        <td>������</td>
                      
                        <td>���0605</td>
                        <td>��ƻ���</td>
                        <td>��������һ�ο�</td>
                        <td>�Ժ���</td>
                        <td>��һ����</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                </tdead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
              <fieldset style="width:99%">
                  <legend  align="center" >��ѧ�豸�������</legend>
            <div class="LineStat tblBottomStat" data-color="1" data-unit="̨" data-name="�����豸��">
                <h1><span></span><strong>ʹ���豸</strong><strong>�����豸</strong></h1>
                <p><span>��һ��</span><span>3</span></p>
                <p><span>�ڶ���</span><span>5</span></p>
                <p><span>������</span><span>8</span></p>
                <p><span>���Ľ�</span><span>10</span></p>
                <p><span>�����</span><span>12</span></p>
                <p><span>������</span><span>15</span></p>
                <p><span>���߽�</span><span>7</span></p>
                <p><span>�ڰ˽�</span><span>7</span></p>
                <p><span>�ھŽ�</span><span>9</span></p>
                <p><span>��ʮ��</span><span>15</span></p>
                <p><span>��ʮһ��</span><span>15</span></p>
                <p><span>��ʮ����</span><span>15</span></p>
            </div>
              </fieldset>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTDON").html('<div class="OPTDBtn">\
                    <a href="#" title="���½"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="��Ҫ��½"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="�ػ�"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="����"><img src="../../../themes/icon_s/11.png"/></a></div>');

                $(".OPTDOFF").html('<div class="OPTDBtn">\
                    <a href="#" title="����"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="���½"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="��Ҫ��½"><img src="../../../themes/icon_s/17.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
