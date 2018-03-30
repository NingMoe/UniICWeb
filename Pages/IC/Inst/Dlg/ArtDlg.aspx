<%@ Page Language="C#"  MasterPageFile="~/Templates/Dlg.master"  AutoEventWireup="true" ValidateRequest="false" CodeFile="ArtDlg.aspx.cs" Inherits="Pages_DLG_ArtDlg" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <%--<script charset="utf-8" src="../../../ClientWeb/fm/jquery/jquery-1.7.2.min.js" type="text/javascript"></script>--%>

    <script charset="utf-8" src="../../../ClientWeb/md/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../../ClientWeb/md/ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../../ClientWeb/md/ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <link href="../../../ClientWeb/md/ueditor/themes/default/css/ueditor.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type="radio"], input[type="checkbox"] { vertical-align: middle; margin: 0px 3px; }
        /*IE去除超链接虚框*/
        a, input[type="radio"], input[type="checkbox"], input[type="button"], input[type="submit"] { blr: expression(this.onFocus=this.blur()); }
        /*ff去除超链接虚框*/
        a:focus, input[type="radio"]:focus, input[type="checkbox"]:focus, input[type="button"]:focus, input[type="submit"]:focus { outline: none; -moz-outline: none; }

        .div_optiontitle { color: #fff; padding: 2px 10px; font-size: 105%; font-weight: bold; margin: 5px 0; border: 1px solid #aaa; border-bottom: 1px dashed #aaa; background: #0099CC; }
        .div_option { padding: 3px 10px; height: auto; text-align: left; vertical-align: central; border-bottom: dashed 1px #aaa; position: relative; }
        .tb_sub { cursor: pointer; }
        .hidden { display: none; }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <form id="myform" runat="server">
        <script type="text/javascript">
            $(function () {
                //实例化编辑器
                UE.getEditor('editor');
                //日期控件
                $(".Wdate").datetimepicker({
                    timeFormat: "",
                    dateFormat: "yy-mm-dd",
                });
                $(".button").button();
            })
            function initUpload() {
                $("#uploadify").uploadify({
                    'uploader': '<%=this.ResolveClientUrl("~/ClientWeb/md/") %>uploadify/uploadify.swf',
                    'script': '<%=this.ResolveClientUrl("~/ClientWeb/md/") %>uploadify/net/uploadHandler.ashx',
                    'folder': '<%=this.ResolveClientUrl("~/ClientWeb/upload/") %>AttachFile',
                    'queueID': 'fileQueue',
                    'auto': false,
                    'multi': true
                });
            }
            function check() {
                if (document.getElementById("<%=tbTitle.ClientID %>").value == "") {
                    alert("标题不能为空");
                    return false;
                }
                else {
                    document.getElementById("<%=hfEditor.ClientID %>").value = UE.getEditor('editor').getContent();
                    return true;
                }
            }
        </script>
        <input type="hidden" runat="server" name="curArtId" id="curArtId" />
        <input type="hidden" runat="server" name="curClsSn" id="curClsSn" />
        <input type="hidden" runat="server" name="curGrpSn" id="curGrpSn" />
        <input type="hidden" runat="server" name="curClsName" id="curClsName" />
        <div style="line-height:20px;">新建文章 >><span><%=clsName %></span></div>
        <table style="width:100%; text-align: center; padding: 0;border:none;background-color:#f1f1f1;">
            <tbody>
                <tr>
                    <td>
                        <div style="margin:5px auto;">
                        <span style="font-family:微软雅黑; line-height: 30px; font-size: 20px; color: #333;">标题：</span>
                        <asp:TextBox ID="tbTitle" runat="server" Width="500px" Height="30px" BorderColor="Silver" Font-Size="16px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <textarea style="width: 720px;margin: 0 auto; height: 500px;" id="editor"><%=hfEditor.Value %></textarea>
                        <input type="hidden" runat="server" name="hfEditor" id="hfEditor" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="div_option">
                            <asp:RadioButtonList ID="rblPublish" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True">暂不发布</asp:ListItem>
                                <asp:ListItem Value="1" Selected="false">正常发布</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="div_option">
                            <span>摘要备注：</span>
                            <asp:TextBox ID="tbSummary" runat="server" Width="600px" Height="22px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        </div>
                        <div class="div_option">
                            <span>信息来源：</span>
                            <asp:TextBox ID="tbAuthor" runat="server"></asp:TextBox>
                        </div>
                        <div runat="server" id="divNewDate" class=" div_option hidden">
                            <span>新闻日期：</span>
                            <input type="text" runat="server" id="ipNewsDate" readonly="readonly" class="Wdate" />
                            <span>若不填写，则以创建日期作为新闻日期</span>
                        </div>
                        <div runat="server" id="divCreDate" class=" div_option hidden">
                            <span>创建日期：</span>
                            <asp:TextBox runat="server" ID="tbCreDate"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </tbody>
        </table>
                <div style="text-align: center;">
                    <div><span style="color: green;" class="hidden">建议提交时选择暂不发布，提交后预览无误再发布文章。</span></div>
            <asp:Button runat="server" ID="btSubmit" Text="提交" OnClick="btSubmit_ServerClick" CssClass="button" OnClientClick="javascript:return check()" />
            &nbsp;
           <input type="button" class="button" value="取消" id="Cancel" />
        </div>
    </form>
</asp:Content>
