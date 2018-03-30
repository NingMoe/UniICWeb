$(function () {
    $(".UISelect").UISelect();
    var tbl = $(".formtable table.ListTbl2");
    tbl.find(">tbody>tr:even").addClass("tblEven");
    tbl.find(">tbody>tr:odd").addClass("tblOdd");
    setTimeout(function () {
        $("body").focus();
    }, 100);
});
