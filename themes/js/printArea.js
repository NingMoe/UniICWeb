(function ($) {
    var opt;

    $.fn.jqprint = function (options) {
        opt = $.extend({}, $.fn.jqprint.defaults, options);

        var $element = (this instanceof jQuery) ? this : $(this);
        $(".printframe").each(function () {
            $(this).remove();
        });
        if (opt.operaSupport && $.support.opera) {
            var tab = window.open("", "jqPrint-preview");
            tab.document.open();

            var doc = tab.document;
        } else {
            var $iframe = $("<iframe  class='printframe' />");
            if (!opt.debug) {
                $iframe.css({ position: "absolute", width: "0px", height: "0px", left: "-600px", top: "-600px" });
            }

            $iframe.appendTo("body");
            var doc = $iframe[0].contentWindow.document;
        }

        if (opt.importCSS) {
            if ($("link[media=print]").length > 0) {
                $("link[media=print]").each(function () {
                    doc.write("<link type='text/css' rel='stylesheet' href='" + $(this).attr("href") + "' media='print' />");
                });
            } else {
                $("link").each(function () {
                    doc.write("<link type='text/css' rel='stylesheet' href='" + $(this).attr("href") + "' />");
                });
            }
        }
        if (opt.importJS) {
            $("script").each(function () {
                if ($(this).attr("import")) {
                    //    doc.write("<script type='text/javascript' src='"+$(this).attr("src")+"'></script>");
                    var script = doc.createElement("script");
                    script.src = $(this).attr("src");
                    var head = doc.getElementsByTagName("head")[0];
                    (head || doc.body).appendChild(script);
                }
            });
        }
        if (opt.printContainer) {
            $element.find("object").each(function () {
                $(this).remove();
            });
            $element.each(function () { doc.write($(this).html()); });
        }
        if (opt.removeObj) {
            $("iframe").each(function () {
                $(this).contents().find("object").each(function () {
                    $(this).remove();
                });
            }
            );
        }
        if (opt.docid != '') {
            doc.write("<object style='DISPLAY: none' id='" + opt.docid + "' classid='clsid:D85C89BE-263C-472D-9B6B-5264CD85B36E'><PARAM NAME='DoubleBuffered' VALUE='0'><PARAM NAME='Enabled' VALUE='-1'><PARAM NAME='Visible' VALUE='-1'><PARAM NAME='Cursor' VALUE='0'><PARAM NAME='HelpType' VALUE='1'><PARAM NAME='HelpKeyword' VALUE=''><PARAM NAME='ServiceUrl' VALUE='" + opt.url + "'><PARAM NAME='UserName' VALUE=''><PARAM NAME='ExtParam' VALUE=''><PARAM NAME='FieldsList' VALUE=''><PARAM NAME='AutoSave' VALUE='-1'><PARAM NAME='SaveHistory' VALUE='0'><PARAM NAME='WebCancelOrder' VALUE='0'><PARAM NAME='WebIsProtect' VALUE='1'><PARAM NAME='WebAutoSign' VALUE='0'><PARAM NAME='FieldsXml' VALUE=''><PARAM NAME='CharSetName' VALUE='GBC_'><PARAM NAME='WebUrl' VALUE=''><PARAM NAME='EnableMove' VALUE='-1'><PARAM NAME='ShowHint' VALUE='-1'><PARAM NAME='PassWord' VALUE=''><PARAM NAME='DocumentList' VALUE=''><PARAM NAME='TimerTime' VALUE=''><PARAM NAME='SaveImage' VALUE=''><PARAM NAME='Visiabled' VALUE=''><PARAM NAME='ShowSignatureWindow' VALUE=''><PARAM NAME='ErrorInfo' VALUE=''><PARAM NAME='ExtParam1' VALUE=''><PARAM NAME='HandPenWidth' VALUE='0'><PARAM NAME='HandPenColor' VALUE='255'><PARAM NAME='ProtectType' VALUE=''><PARAM NAME='RelativeTagId' VALUE=''><PARAM NAME='PositionByTagType' VALUE='0'><PARAM NAME='PositionBySignType' VALUE='0'><PARAM NAME='EventResult' VALUE='0'><PARAM NAME='Phrase' VALUE=''><PARAM NAME='ValidateCertTime' VALUE='0'><PARAM NAME='ValidateCertificate' VALUE='0'><PARAM NAME='PrintControlType' VALUE='2'><PARAM NAME='XMLEvent' VALUE='0'><PARAM NAME='MenuDocVerify' VALUE='-1'><PARAM NAME='MenuServerVerify' VALUE='-1'><PARAM NAME='MenuDigitalCert' VALUE='-1'><PARAM NAME='MenuDocLocked' VALUE='-1'><PARAM NAME='MenuDeleteSign' VALUE='-1'><PARAM NAME='MenuMoveSetting' VALUE='-1'><PARAM NAME='MenuAbout' VALUE='-1'><PARAM NAME='PrintWater' VALUE='-1'><PARAM NAME='DefaultSignTimeFormat' VALUE=''><PARAM NAME='HandEvent' VALUE='0'><PARAM NAME='EnableEditPrintCount' VALUE='-1'><PARAM NAME='AutoCloseBatchWindow' VALUE='0'><PARAM NAME='ShowBatchWindow' VALUE='-1'><PARAM NAME='ShowBatchErrorInfo' VALUE='-1'><PARAM NAME='MustSignature' VALUE='0'><PARAM NAME='SignCert' VALUE=''><PARAM NAME='XmlConfigParam' VALUE=''><PARAM NAME='SupportVerifyType' VALUE='0'><PARAM NAME='VerifyType' VALUE='0'><PARAM NAME='ModalPrintUrl' VALUE=''><PARAM NAME='ExtParam2' VALUE=''><PARAM NAME='DocumentName' VALUE=''></object>");
            doc.write("<script type='text/javascript'>");
            doc.write("var revision = document.getElementById('" + opt.docid + "');");
            doc.write("revision.ShowSignature('" + opt.docid + "');");
            doc.write("revision.MovePositionByNoSave(0, -15);");
            doc.write("</script>");
        }
        doc.close();
        setTimeout(function () {
            $iframe[0].contentWindow.focus();
            $iframe[0].contentWindow.print();
            //window.location.replace(window.location.href);

        }, 100);
    };

    $.fn.jqprint.defaults = {
        debug: false,
        importCSS: true,
        importJS: true,
        removeObj: true,
        printContainer: true,
        operaSupport: true,
        docid: ''
    };

    // Thanks to 9__, found at http://users.livejournal.com/9__/380664.html
    jQuery.fn.outer = function () {
        return $($('<div></div>').html(this.clone())).html();
    };
})(jQuery);