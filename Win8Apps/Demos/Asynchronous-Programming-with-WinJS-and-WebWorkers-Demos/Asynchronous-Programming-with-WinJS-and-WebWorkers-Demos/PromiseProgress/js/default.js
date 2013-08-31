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

            document.getElementById("calculate-primes-button-id").addEventListener("click", onClicked);
        }
    };

    app.oncheckpoint = function (args) {
    };

    var isPrime = function (number) {
        for (var i = 2; i < number; i++) {
            if (number % i == 0) {
                return false;
            }
        }
        return true;
    }

    var calculatePrimesPromise = function (fromNumber, toNumber) {
        return new WinJS.Promise(function (complete, error, progress) {
            setTimeout(function () {
                var primesList = [];
                for (var num = fromNumber; num <= toNumber; num++) {
                    if (isPrime(num)) {
                        primesList.push(num);
                    }
                    progress((num - fromNumber) / (toNumber - fromNumber) * 100);
                }
                complete(primesList);
            }, 100);
        });
    }

    var onClicked = function () {
        var progressElement = document.getElementById("progress-id");
        var resultElement = document.getElementById("result-id");

        calculatePrimesPromise(0, 10000).then(
            function (primesList) {
                for (var i = 0; i < primesList.length; i++) {
                    resultElement.innerText += primesList[i] + " ";
                }
            },
            function (error) {
                resultElement.innerText = "An error occured";
            },
            function (progress) {
                console.log(progress);
            });
    }

    app.start();
})();
