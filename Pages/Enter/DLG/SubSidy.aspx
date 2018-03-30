<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SubSidy.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <div>
                <table>
                   
                    <tr>
                        <th>学工号：</th>
                        <td>
                            <textarea id="szLogonName" style="width:500px;height:50px" name="szLogonName" title="多个工号用中文，隔开" class="validate[required]"></textarea>
                        </td>
                        </tr>
                      <tr>
                        <th>姓名：</th>
                        <td>
                            <label id="szTrueName"></label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr width="100%"/>
                        </td>
                    </tr>
                     <tr>
                        <th colspan="4" style="text-align:center">
                                <LABEL><INPUT class="enum" value="8193" type="radio" checked="checked" name="dwKind" >加补助</LABEL>
                            <LABEL><INPUT class="enum" value="8194" type="radio" name="dwKind" >减补助</LABEL>
                            <LABEL><INPUT class="enum" value="8196" type="radio" name="dwKind" >补足清零</LABEL>
                        </th>
                    </tr>
                     <tr>
                        <td colspan="2">
                            <hr width="100%"/>
                        </td>
                    </tr>
                     <tr>
                       
                        <th>金额（元）</th>
                        <td>
                            <input type="text" id="money" name="money" class="validate[required,validate[custom[onlyNumber]]" /></td>
                    </tr>
                </table>
            </div>
            <div style="margin: 0px auto; text-align: center">
                <div style="margin: 10px auto;">
                    <button type="submit" id="OK">确定</button>
                    <button type="button" id="Cancel">关闭</button>
                </div>
            </div>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
       
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $('input[name="dwKind"]').change(function () {
                if ($(this).val() == "8196") {
                    $("#money").val("0");
                }
                else { $("#money").val(""); }
            });
            $("#szLogonName").keyup(function () {
                var vlist = $("#szLogonName").val().split('，');
                var vlistE = $("#szLogonName").val().split(',');
                if (vlist.length < vlistE.length) {
                    vlist = vlistE;
                }
                var vRes = "";
                var vOwner = "";
                var i = 0;
                for (i = 0; i < vlist.length; i++) {
                    var vTemp = vlist[i];
                    $.get(
                         "../data/searchaccount.aspx",
                         { Type: "logonname", term: vTemp },
                         function (data) {
                             var vJson = eval(data);
                             if (vJson[0] != null && vJson[0].szTrueName != null) {
                                 vRes = vRes + vJson[0].szTrueName + ";";
                                 vOwner = vOwner + vJson[0].id + ";";
                                 $("#szTrueName").html("");
                                 $("#szTrueName").html(vRes);
                                 $("#szowner").val();
                                 $("#szowner").val(vOwner);
                             }

                         }
                       );

                }

            });
            $("#Cancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
