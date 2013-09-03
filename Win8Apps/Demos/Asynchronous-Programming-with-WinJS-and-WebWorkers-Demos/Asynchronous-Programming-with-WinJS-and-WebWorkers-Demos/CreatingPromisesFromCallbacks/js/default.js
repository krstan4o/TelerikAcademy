/// <reference path="../Scripts/jquery-2.0.3.intellisense.js" />
// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    function getGeolocationPositionPromise() {
        var promise = new WinJS.Promise(function (success, error, progress) {
            navigator.geolocation.getCurrentPosition(success, error);
        });

        return promise;
    }

    function ajaxPromise(url, data) {
        var promise = new WinJS.Promise(function (success, error, progress) {
            var method = data ? "POST" : "GET";
            data = data || {};
            jQuery.ajax({
                url: url,
                data: data,
                type: method,
                contentType: "application/json",
                success: success,
                error: error
            });
        });

        return promise;
    }

    app.onactivated = function (args) {
        var performRequestsButton = document.getElementById("perform-requests");
        performRequestsButton.addEventListener("click", performRequests);
    };

    function performRequests() {
        getGeolocationPositionPromise().done(function (locationData) {
            var geolocationDataElement = document.getElementById("geolocation-data");
            geolocationDataElement.innerText = locationData.coords.latitude + " " + locationData.coords.latitude;
        });

        ajaxPromise("http://posted.apphb.com/api/posts").done(function (response) {
            var ajaxDataElement = document.getElementById("ajax-data");
            ajaxDataElement.innerText = JSON.stringify(response);
        });
    }

    app.start();
})();
