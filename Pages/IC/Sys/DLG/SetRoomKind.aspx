<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetRoomKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />     
<input type="hidden" value="0" id="dwUsableNum" name="dwUsableNum" />        
            <input id="dwClassKind" name="dwClassKind" type="hidden" value="1" /><!--2:电脑-->
               <%if(ConfigConst.GCKindAndClass==1) {%>                    
            <%} %>
            <table>
                <tr>
                    <th>名称：</th>
                    <td colspan="3"><input id="szKindName" name="szKindName" class="validate[required]" /></td>
                   
                </tr>
                <tr>
                    <th>最少使用人数：</th>
                    <td><input type="text" id="dwMinUsers" name="dwMinUsers" /></td>
                    <th>最多使用人数：</th>
                    <td><input type="text" id="dwMaxUsers" name="dwMaxUsers" /></td>
                </tr>
                <tr>
                    <%if(ConfigConst.GCKindAndClass==0) {%>
                     <th>所属<%=ConfigConst.GCClassName %>：</th>
                    <td colspan="3"><select id="dwClassID" name="dwClassID" ><%=m_dwDevClass %></select></td>
                  <%} else{%>
                    <input type="hidden" id="Hidden1" name="dwClassID" />
                    <%}  %>
                </tr>
                <tr>
                    
                    <th>属性：</th>
                    <td><%=m_KindProperty %></td>
                    <th></th>
                    <td>   <LABEL><INPUT class="enum" value="1" type="checkbox" name="isOpen" /> 不对外开放</LABEL></td>
                </tr>
             <tr>
                 <th>申请附件上传：</th>
                <td colspan="3">
                        <input type="file" name="fileurl" id="fileurl" size="45" />
                    <label><input class="enum" type="checkbox" name="isUpload" value="1">清空附件内容</label>

                      <a style="text-decoration:underline" href="<%=szDownLoadUrl %>"><%=szOpDownload %></a>
                </td>
             </tr>
                          <tr>
                 <th>显示顺序：</th>
                <td colspan="3">
                <select name="dwResv1" id="dwResv1">
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                    <option value="9">9</option>
                    <option value="10">10</option>

                </select>
                </td>
             </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>                        
                    </td>
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
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
          
            setTimeout(function () {
                
            }, 1);
        });
    </script>
</asp:Content>
