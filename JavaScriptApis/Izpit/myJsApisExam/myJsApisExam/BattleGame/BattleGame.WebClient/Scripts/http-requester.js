/// <reference path="jquery-2.0.3.js" />
var httpRequester = (function () {
    function get(url, success, error) {
        $.ajax({
            url: url,
            contentType: "application/json",
            method: "GET",
            success: success,
            error: error
        });
    }
    function post(url, data, success, error) {
        $.ajax({
            url: url,
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify(data),
            success: success,
            error: error
        });
    }
    return {
        get: get,
        post: post
    };
}());