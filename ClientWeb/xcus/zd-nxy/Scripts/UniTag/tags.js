$(function () {
    $(".tag_head").each(function () {
        $(this).find("li").click(selectTag);
        $(this).find("li:first").addClass("h_sel");
    });
    $(".tag_con").each(function () {
        $(this).children().addClass("item");
        $(this).children(":first").addClass("c_sel");
    });
});
function selectTag() {
    // 操作标签
    var tags = $(this).parent().find("li");
    tags.each(function () {
        $(this).removeClass("h_sel");
    });
 $(this).addClass("h_sel");
    // 操作内容
 var index=0;
 tags.each(function (i) {
     if ($(this).hasClass("h_sel")) {
         index = i;
     }
 });
 var con = $(this).parents("div.tags:first").find(".tag_con");
 var items = con.find(".item");
 items.each(function () {
     $(this).removeClass("c_sel");
 });
 con.find(".item:eq("+index+")").addClass("c_sel");
}

//tag 要激活的li
function cusSelTag(tag) {
    // 操作标签
    var tags = tag.parent().find("li");
    tags.each(function () {
        $(this).removeClass("h_sel");
    });
    tag.addClass("h_sel");
    // 操作内容
    var index = 0;
    tags.each(function (i) {
        if ($(this).hasClass("h_sel")) {
            index = i;
        }
    });
    var con = tag.parents("div.tags:first").find(".tag_con");
    var items = con.find(".item");
    items.each(function () {
        $(this).removeClass("c_sel");
    });
    con.find(".item:eq(" + index + ")").addClass("c_sel");
}