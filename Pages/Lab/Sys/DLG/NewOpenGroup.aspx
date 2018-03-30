<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewOpenGroup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwID" name="dwID" />
        <div class="formtable">

            <table>
                <tr style="vertical-align: top;">
                    <th>规则名称：</th>
                    <td style="text-align: left">
                      <input id="szName" name="szName" value="" /> </td>
                    <th>备注：</th>
                    <td style="text-align: left">
                        <input id="szMemo" name="szMemo" value="" /></td>
                </tr>
                <tr>
                    <th>类型</th>
                    <td colspan="3">
                        <select id="dwKind" name="dwKind">
                        <option value="8192">开放规则组</option>
                        <option value="2048">特殊(保洁人员)组</option>
                       </select>

                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
                        <br />
                        <label style="color:red">可在修改处添加删除成员</label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">       
        #gallery {
            float: left;
            width: 100%;
            min-height: 12em;
        }

        .gallery.custom-state-active {
            background: #eee;
        }

        .gallery li {
            float: left;
            width: 96px;
            padding: 0.4em;
            margin: 0 0.4em 0.4em 0;
            text-align: center;
        }

            .gallery li h5 {
                margin: 0 0 0.4em;
                cursor: move;
            }

            .gallery li a {
                float: right;
            }

                .gallery li a.ui-icon-zoomin {
                    float: left;
                }

            .gallery li img {
                width: 100%;
                cursor: move;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btnAddRole").button();
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
            $("#btnAddRole").click(function (event) {
                var GroupID = $("#dwID").val();
                var Name = $("#dwIdent  option:selected").text();
                var value = $("#dwIdent  option:selected").val();
                var KindID = 8;//身份8
                $.get(
                   "../../data/AddGroupMember.aspx",
                   { GroupID: GroupID, MemberID: value, KindID: KindID, Name: Name },
                   function (data) {
                       if (data == "success") {
                           var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                           szTemp += "<a id='" + GroupID + "' kindid='" + KindID + "' href='#' title='删除' class='ui-icon ui-icon-trash'></a>";
                           szTemp += "<label>" + Name + "</label></li>";
                           $("#gallery").prepend($(szTemp));

                       }
                       else {
                           MessageBox(data, "", 2);
                       }

                   }
                 );
            });
            $("#szLogonName").autocomplete({
                source: "../../data/searchAccount.aspx?type=LogonName",
                select: function (event, ui) {
                    if (ui.item) {
                        var GroupID = $("#dwID").val();
                        var MeberID = ui.item.id;
                        var KindID = 2;//个人2
                        var Name=ui.item.label;
                        $.get(
                       "../../data/AddGroupMember.aspx",
                       { GroupID: GroupID, MemberID: MeberID, KindID: KindID, Name: Name },
                       function (data) {
                           if (data == "success") {
                               var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                               szTemp += "<a id='" + ui.item.id + "' kindid='" + KindID + "' href='#' title='删除' class='ui-icon ui-icon-trash'></a>";
                               szTemp += "<label>" + ui.item.label + "</label></li>";
                               $("#gallery").prepend($(szTemp));

                           }
                           else {
                               MessageBox(data, "", 2);
                           }

                       }
                     );
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {
                        ui.content.push({ label: " 未找到配置项 " });
                    }

                }
            }).blur(function () {
            });
            $(".ui-icon-trash").click(function (event) {
                var li = $(this);
                var GroupID = $("#dwID").val();
                var MeberID = $(this).attr("id");
                var KindID = $(this).attr("kindid");
                if (GroupID == null || GroupID == "" || MeberID == null || MeberID == "" || KindID == null || KindID == "") {
                    MessageBox("请打开再试，可能信息丢失无法删除", "", 2);
                    return;
                }
                $.get(
         "../../data/DelGroupMember.aspx",
         { GroupID: GroupID, MemberID: MeberID, KindID: KindID },
         function (data) {
             if (data == "success") {
                 li.parent().remove();
             }
             else {
                 MessageBox(data, "", 2);
             }

         }
    );
            });
        });
    </script>
</asp:Content>
