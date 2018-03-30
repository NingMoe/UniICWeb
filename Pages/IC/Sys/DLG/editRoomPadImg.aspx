<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="editRoomPadImg.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwKindID" name="dwKindID" type="hidden" />
            <table>

                <tr>
                    <td colspan="4" style="text-align:center">
                        <div style="width:700px;margin:0 auto;">
                        <script id="editor" type="text/plain"></script>
                            </div>
                  </td>
                  
                </tr>  
                          
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
    </style>
    <script type="text/javascript" charset="gbk" src="../../../../ClientWeb/md/ueditor/ueditor.cfg.js"></script>
<script type="text/javascript" charset="gbk" src="../../../../ClientWeb/md/ueditor/ueditor.all.js"> </script >
    <script language="javascript" type="text/javascript">
        
        $(function () {
    var ue = UE.getEditor('editor', {
        autoHeightEnabled: true,
        autoFloatEnabled: true,
        initialFrameWidth: 690,
        initialFrameHeight:483
    });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);

    });
    </script>
</asp:Content>
