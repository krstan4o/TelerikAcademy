// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {

            WinJS.Utilities.id("open-targetpage-button").listen("click", function () {
                var pageHolder = document.getElementById("page-holder");
                WinJS.UI.Pages.render("/pages/targetpage/targetpage.html", pageHolder);
            });

            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.start();
})();
