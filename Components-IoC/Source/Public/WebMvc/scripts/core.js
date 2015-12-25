if ($) {
    $.ajaxSetup({
        async: false,
        cache: false
    });
    $(document).ajaxError(function(event, response, settings) {
        if (response.status == 400) window.location = '/en/error/http400';
        else if (response.status == 403) window.location = '/en/error/http403';
        else if (response.status == 404) window.location = '/en/error/http404';
        else if (response.status == 500) window.location = '/en/error/http500';
    });
}

String.prototype.format = function() {
    var pattern = /\{\d+\}/g;
    var args = arguments;
    return this.replace(pattern, function(capture) { return args[capture.match(/\d+/)]; });
}

function attachCaptchaReload() {
    $("#captcha").live("click", function(event) {
        var r = "r=" + Math.round(Math.random() * 1000);
        var parts = this.src.split("?");
        if (parts.length > 1) {
            var q = parts[1].replace(/^r=\d+/, "").replace(/&r=\d+/, "");
            if (q.length > 0) {
                r = q + "&" + r;
            }
        }

        this.src = parts[0] + "?" + r;
        $("#turingnumber").focus().val("");
    });
}

function ajaxPostOnSubmit(selector) {
    if (!selector) selector = "#placeholder input[type='submit'][class!='noajax']";
    $(selector).live("click", function(event) {
        this.disabled = true;
        var form = $("form");
        var url = form.attr("action");
        var data = null;
        if (this.name) {
            data = form.serializeArray();
            data.push({ name: this.name, value: this.value });
            data = jQuery.param(data);
        }
        else
            data = form.serialize();
        if (form.attr("method").toLowerCase() == "post") {
            $.post(url, data, handleAjaxResponse);
        }
        else {
            $.get(url, data, handleAjaxResponse);
        }
        event.preventDefault();
    });
}

function handleAjaxResponse(result, textStatus, response, placeHolder) {
    if (!placeHolder) {
        placeHolder = "#placeholder";
    }
    if (response.status == 207)
        window.location = response.getResponseHeader('Location');
    else {
        $(placeHolder).html(result);
    }
}

function ajaxPagerClick(placeHolder) {
    if (!placeHolder) placeHolder = "#placeholder";
    $(placeHolder + " div.pager li a").live("click", function(event) {
        var url = this.href;
        $.get(url, null, function(result, textStatus, response) {
            handleAjaxResponse(result, textStatus, response, placeHolder);
        });
        event.preventDefault();
    });
}