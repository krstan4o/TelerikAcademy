// For an introduction to the Navigation template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232506
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var nav = WinJS.Navigation;

    var operationsLogger = null;
    var historyLogger = null;
    var backLogger = null;
    var forwardLogger = null;
    var pageStateLogger = null;

    app.addEventListener("activated", function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }

            if (app.sessionState.history) {
                nav.history = app.sessionState.history;
            }

            args.setPromise(WinJS.UI.processAll().then(function () {
                operationsLogger = new DomLogger(document.getElementById("nav-operations"), "Navigation operations");
                backLogger = new DomLogger(document.getElementById("nav-back"), "Back");
                forwardLogger = new DomLogger(document.getElementById("nav-forward"), "Forward");
                pageStateLogger = new DomLogger(document.getElementById("page-state"), "Page State");

                attachEventListeners();

                if (nav.location) {
                    nav.history.current.initialPlaceholder = true;
                    return nav.navigate(nav.location, nav.state);
                } else {
                    return nav.navigate(Application.navigator.home);
                }
            }));
        }
    });

    function attachEventListeners() {
        WinJS.Navigation.addEventListener("navigated", function (event) {
            operationsLogger.log("reached: " + event.detail.location);

            backLogger.log("", true);
            forwardLogger.log("", true);

            for (var i in WinJS.Navigation.history.backStack) {
                backLogger.log(WinJS.Navigation.history.backStack[i].location);
            }

            for (var i in WinJS.Navigation.history.forwardStack) {
                forwardLogger.log(WinJS.Navigation.history.forwardStack[i].location);
            }

            pageStateLogger.log("", true);
            pageStateLogger.log(JSON.stringify(WinJS.Navigation.history.current));
            //pageStateLogger.log(JSON.stringify(WinJS.Navigation.state));
        });

        WinJS.Navigation.addEventListener("navigating", function (event) {
            operationsLogger.log("navigating to: " + event.detail.location);
        });
    }

    app.start();
})();
