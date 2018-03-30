<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetOpenGroup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwID" name="dwID" />
        <div class="formtable">

            <table>
                 <tr style="vertical-align: top;">
                    <th>�������ƣ�</th>
                    <td style="text-align: left">
                      <input id="szName" name="szName" value="" /> </td>
                    <th>��ע��</th>
                    <td style="text-align: left">
                        <input id="szMemo" name="szMemo" value="" /></td>
                </tr>
                <tr style="vertical-align: top;">
                    <th>��ݣ�</th>
                    <td style="text-align: left;width:200px;">
                        <select id="dwIdent" name="dwManRole"><%=szIdent %></select><a href="#" id="btnAddRole">������</a></td>
                    <th>��Աѧ���ţ�</th>
                    <td style="text-align: left">
                        <input id="szLogonName" value="" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                         <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button>
                    </td>
                </tr>
            </table>
            <div id="GroupMember">
                <fieldset><legend>��������Ա</legend>
                <ul id="gallery" class="gallery ui-helper-reset ui-helper-clearfix">
                    <%=szGroupMember %>
                </ul>
                </fieldset>
            </div>
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
        #btnAddRole {
            color:blue;
            text-decoration:underline;
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
           
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
            $("#btnAddRole").click(function (event) {
                var GroupID = $("#dwID").val();
                var Name = $("#dwIdent  option:selected").text();
                var value = $("#dwIdent  option:selected").val();
                var KindID = 8;//���8
                $.get(
                   "../../data/AddGroupMember.aspx",
                   { GroupID: GroupID, MemberID: value, KindID: KindID, Name: Name },
                   function (data) {
                       if (data == "succ") {
                           var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                           szTemp += "<a id='" + GroupID + "' kindid='" + KindID + "' href='#' title='ɾ��' class='ui-icon ui-icon-trash'></a>";
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
                        var KindID = 2;//����2
                        var Name=ui.item.label;
                        $.get(
                       "../../data/AddGroupMember.aspx",
                       { GroupID: GroupID, MemberID: MeberID, KindID: KindID, Name: Name },
                       function (data) {
                           if (data == "succ") {
                               var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                               szTemp += "<a id='" + ui.item.id + "' kindid='" + KindID + "' href='#' title='ɾ��' class='ui-icon ui-icon-trash'></a>";
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
                        ui.content.push({ label: " δ�ҵ������� " });
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
                    MessageBox("������ԣ�������Ϣ��ʧ�޷�ɾ��", "", 2);
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
