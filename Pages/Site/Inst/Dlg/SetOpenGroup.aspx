<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetOpenGroup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwGroupID" name="dwGroupID" />
        <input type="hidden" id="dwAccno" name="dwAccno" />
        <input type="hidden" id="deptID" name="deptID" />
        <input type="hidden" id="szMemberList" name="szMemberList" />
        <div class="formtable">

            <table>
                <tr>
                    <th>学工号：</th>
                    <td style="text-align: left">
                        <input id="szLogonName" name="szLogonName" value="" />
                    <input type="button" value="添加个人" id="btnAddPerson" style="width:80px" /></td>
                </tr>
                <tr>
                       <th>部门：</th>
                    <td style="text-align: left">
                        <input id="szDeptName" name="szDeptName" value="" />
                        <input type="button" value="添加部门" id="btnAddDept" style="width:80px" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                         <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
                    </td>
                </tr>
            </table>
            <div id="GroupMember">
                <fieldset><legend>已添加组成员</legend>
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
            $("#btnAddPerson").button().click(function () {
                debugger;
                var GroupID = $("#dwGroupID").val();
                var MeberID = $("#dwAccno").val();
                var KindID = 2;//个人2
                var Name = $("#szTrueName").val();
                $.get(
               "../../data/AddGroupMember2.aspx",
               { GroupID: GroupID, MemberID: MeberID, KindID: KindID, Name: Name,type:'accno',addlogonname:'true' },
               function (data) {
                   if (data.indexOf("succ") > -1) {
                       var vd = data.replace("succ", "");
                       var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                       szTemp += "<a id='" + MeberID + "' kindid='" + KindID + "' href='#' title='删除' class='ui-icon ui-icon-trash'></a>";
                       szTemp += "<label>" + vd + "</label></li>";
                       $("#gallery").prepend($(szTemp));

                   }
                   else {
                       MessageBox(data, "", 2);
                   }

               }
             );
            });
            $("#btnAddDept").button().click(function () {
                var GroupID = $("#dwGroupID").val();
                var MeberID = $("#deptID").val();
                var KindID = 4;//个人2
                var Name = $("#szDeptName").val();
                if (MeberID == null || MeberID == "")
                {
                    return;
                }
                $.get(
               "../../data/AddGroupMember.aspx",
               { GroupID: GroupID, MemberID: MeberID, KindID: KindID, Name: Name },
               function (data) {
                   if (data == "succ") {
                       var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                       szTemp += "<a id='" + MeberID + "' kindid='" + KindID + "' href='#' title='删除' class='ui-icon ui-icon-trash'></a>";
                       szTemp += "<label>" + Name + "</label></li>";
                       $("#gallery").prepend($(szTemp));

                   }
                   else {
                       MessageBox(data, "", 2);
                   }

               }
             );
            });
            AutoUserByLogonname($("#szLogonName"), 2, $("#dwAccno"), null, null, null);
            AutoDept($("#szDeptName"), 2, $("#deptID"), false);
           
            setTimeout(function () {
                var GroupID = $("#dwGroupID").val();
                $.get(
               "../../data/searchGroupMember.aspx",
               { id: GroupID },
               function (data) {
                   var vMemberList = eval(data);
                   for (var k = 0; k < vMemberList.length; k++) {
                       var MeberID = vMemberList[k].id;
                       var KindID = vMemberList[k].kind;
                       var Name = vMemberList[k].name;
                       var szTemp = "<li class='ui-widget-content ui-corner-tr ui-state-focus'>";
                       szTemp += "<a id='" + MeberID + "' kindid='" + KindID + "' href='#' title='删除' class='ui-icon ui-icon-trash'></a>";
                       szTemp += "<label>" + Name + "</label></li>";
                       $("#gallery").prepend($(szTemp));
                   }
               });
                }, 500);
            $("#GroupMember").on("click", ".ui-icon-trash", function () {
                var li = $(this);
                var GroupID = $("#dwGroupID").val();
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
         });
            });
        });
    </script>
</asp:Content>
