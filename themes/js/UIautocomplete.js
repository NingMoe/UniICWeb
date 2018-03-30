function AutoUserByName(userObject, level, accnno,handphone, tel, email) {
    AutoUser(userObject, level, accnno,'truename', handphone, tel, email);
}
function AutoUserByLogonname(userObject, level,accnno,handphone, tel, email) {
    AutoUser(userObject, level, accnno, 'logonname', handphone, tel, email);
}
function AutoUserByIdent(userObject, level, accnno, handphone, tel, email) {
    AutoUser(userObject, level, accnno, 'ident', handphone, tel, email);
}
function AutoUserByIdentTeacher(userObject, level, accnno, handphone, tel, email) {
    AutoUser(userObject, level, accnno, 'identTeacher', handphone, tel, email);
}
function AutoUserByIdentClassTeacher(userObject, level, accnno, handphone, tel, email) {
    AutoUser(userObject, level, accnno, 'ClassTeacher', handphone, tel, email);
}
function AutoUser(userObject, level,accnno,type, handphone, tel, email)
{
    var url1="";
    if(level==1)
    {
        var url1="../data/searchAccount.aspx";
    }
    else if(level==2)
    {
        var url1="../../data/searchAccount.aspx";
    }
    if (type == "ClassTeacher") {
        if (level == 1) {
            var url1 = "../data/searchClassTeacher.aspx";
        }
        else if (level == 2) {
            var url1 = "../../data/searchClassTeacher.aspx";
        }
    }

    if (type == null)
    {
        url1 += "?Type=truename";
    }
    else if (type=="truename")
    {
         url1 += "?Type=truename";
    }
    else if (type == "logonname") {
        url1 += "?Type=logonname";
    }
    else if (type == "ident") {
        url1 += "?Type=truename&dwIdent=1048576";
    }
    else if (type == "identTeacher") {
        url1 += "?Type=truename&dwIdent=512";
    }
    else if (type == "ClassTeacher") {
        url1 += "?Type=truename&ClassTeacher=1";
    }
    else {
        url1 += "?Type=logonname";
    }
    userObject.autocomplete({
        source:url1,
        minLength:0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        if (type == "logonname") {
                            userObject.val(ui.item.szLogonName);
                    }
                    else
                    {
                        userObject.val(ui.item.szTrueName);
                    }
                        if (handphone != null) {
                            handphone.val(ui.item.szHandPhone);
                        }
                        if (tel != null) {
                            tel.val(ui.item.szTel);
                        }
                        if (email != null) {
                            email.val(ui.item.szEmail);
                        }
                        if (accnno != null) {
                            accnno.val(ui.item.id);
                        }
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该人员 " });
            }
        }
    });
    userObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            if (accnno != null) {
                accnno.val("");                
            }
        }
    });
    userObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoDept(deptObject, level,deptid,isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchDept.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchDept.aspx?1=1";
    }
    if (isAll != null && isAll == false)
    {
        var url1 = url1+"&InAll=false";
    }
    deptObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        deptObject.val(ui.item.label);
                        deptid.val(ui.item.id);
                        
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该部门 " });
            }
        }
    });
    deptObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            deptid.val("");
            if (deptObject != null) {
                deptObject.val("");
                deptid.val("");
            }
        }
    });
    deptObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoClass(deptObject, level, deptid, isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchcls.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchcls.aspx?1=1";
    }
    if (isAll != null && isAll == false) {
        var url1 = url1 + "&InAll=false";
    }
    deptObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        deptObject.val(ui.item.label);
                        deptid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该班级" });
            }
        }
    });
    deptObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            deptid.val("");
            if (deptObject != null) {
                deptObject.val("");
                deptid.val("");
            }
        }
    });
    deptObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoDevKind(devObject,level,devid,kindType,isAll) {
    var url1 = "";  
    if (level == 1) {
        var url1 = "../data/searchDevKind.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchDevKind.aspx?1=1";
    }
    if (isAll != null && isAll == false) {
        var url1= url1+"&InAll=false";
    }
    if (kindType != null)
    {
        var url1 = url1 + "&kind=" + kindType;
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoLab(devObject, level, devid, kindType, isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchLab.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchLab.aspx?1=1";
    }
    if (isAll != null && isAll == false) {
        var url1 = url1 + "&InAll=false";
    }
    if (kindType != null) {
        var url1 = url1 + "&kind=" + kindType;
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoCourseName(devObject, level, devid,name, isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchCourse.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchCourse.aspx?1=1";
    }
  
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoCourseCode(devObject, level, devid, Code, isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchCourse.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchCourse.aspx?1=1";
    }
    if (name != null && name != "") {
        url1 += "&Code=" + Code;
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoCompany(devObject, level, devid, kind, isAll) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchCompany.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchCompany.aspx?1=1";
    }
   
    if (kind != null && kind != "0") {
        url1 += "&kind=" + kind;
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoRoom(devObject, level, devid, kindType, labid) {
   
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchRoom.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchRoom.aspx?1=1";
    }   
    if (kindType != null) {
        var url1 = url1 + "&kind=" + kindType;
    }
    if (labid != null)
    {

        var url1 = url1 + "&labid=" + labid.val();
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                        if (labid != null) {
                            //labid.val(ui.item.labid);
                        }
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}
function AutoDevice(devObject, level, devid, kindType,roomid,labid,kindid) {
    var url1 = "";
    if (level == 1) {
        var url1 = "../data/searchDeviceList.aspx?1=1";
    }
    else if (level == 2) {
        var url1 = "../../data/searchDeviceList.aspx?1=1";
    }
    if (kindType != null) {
        var url1 = url1 + "&kind=" + kindType;
    }
    devObject.autocomplete({
        source: url1,
        minLength: 0,
        select: function (event, ui) {
            if (ui.item) {
                if (ui.item.id && ui.item.id != "") {
                    setTimeout(function () {
                        devObject.val(ui.item.label);
                        devid.val(ui.item.id);
                        if (roomid != null) {
                            roomid.val(ui.item.dwRoomID);
                        }
                        if (labid != null) {
                            labid.val(ui.item.dwLabID);
                        }
                        if (kindid != null) {
                            kindid.val(ui.item.dwKindID);
                        }
                    }, 5);
                }
            }
        },
        response: function (event, ui) {
            if (ui.content.length == 0) {
                ui.content.push({ label: " 未找到该该项 " });
            }
        }
    });
    devObject.blur(function () {
        if ($(this).val() == "") {
            $(this).val("");
            devid.val("");
            if (devObject != null) {
                devObject.val("");
                devid.val("");
            }
        }
    });
    devObject.click(function () {
        $(this).autocomplete("search", "");
    });
}