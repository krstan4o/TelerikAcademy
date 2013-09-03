// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    WinJS.strictProcessing();

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {

            } else {

            }
            args.setPromise(WinJS.UI.processAll());

            var divideButton = document.getElementById("divide-button-id").addEventListener("click", onClicked);
        }
    };

    app.oncheckpoint = function (args) {
    };

    var divideAsync = function (number, divisor) {
        return new WinJS.Promise(function (complete, error) {
            if (divisor == 0) {
                error("divisor cannot be 0");
            }
            else {
                complete(number / divisor);
            }
        });
    }

    var onClicked = function () {
        var numberInputElement = document.getElementById("number-input-id");
        var divisorInputElement = document.getElementById("divisor-input-id");
        var mainContentElement = document.getElementById("main-content-id");

        divideAsync(numberInputElement.value, divisorInputElement.value)
            .then(
            function (result) {
                mainContentElement.innerText = result;
            },
            function (error) {
                mainContentElement.innerText = error;
            });
    }

    app.start();
})();
