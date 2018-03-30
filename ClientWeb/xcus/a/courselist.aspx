<%@ Page Language="C#" AutoEventWireup="true" CodeFile="courselist.aspx.cs" Inherits="ClientWeb_xcus_all_courselist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <script>
        $(function () {
            $(".dept_filter").change(function () {
                var v = $(this).val();
                var list=$(".box_list .it");
                if (v == "0") list.show();
                else {
                    list.each(function () {
                        var pthis = $(this);
                        if (pthis.attr("dept") == v) pthis.show();
                        else pthis.hide();
                    });
                }

            }).bsDropdown();
        })
    </script>
        <form runat="server">
        <input runat="server" type="hidden" class="dept_id" name="dept_id" id="deptId" />
        <input runat="server" type="hidden" class="dept_name" name="dept_name" id="deptName" />
    </form>
            <div>
            <h1 class="h_title"><%=infoTitle %></h1>
                <div class="line"></div>
                <div class="course_filter">
                    <strong>学院：</strong>
                    <select class="dept_filter">
                        <%=deptList %>
                    </select>
                </div>
            <div style="margin:10px;margin-bottom:20px;">
                <%=infoIntro %>
            </div>
        </div>
    <div class="select_box">
    <ul class="box_list">
        <%=courseList %>
    </ul></div>
    <script>
        $(".select_box .click_load").clickLoad(function () {
            uni.backTop();
        });
    </script>
</body>
</html>
