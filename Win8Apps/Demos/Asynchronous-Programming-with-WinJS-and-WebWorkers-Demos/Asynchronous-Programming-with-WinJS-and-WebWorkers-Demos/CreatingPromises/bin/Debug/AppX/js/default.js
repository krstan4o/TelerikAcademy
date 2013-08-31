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

    var calculatePrimes = function (toNumber) {
        var primesList = new Array();

        for (var ind = 1; ind < toNumber; ind++) {
            if (isPrime(ind)) {
                primesList.push(ind);
            }
        }

        return primesList;
    }

    var calculatePrimesPromise = function (toNumber) {
        return new WinJS.Promise(function (complete) {
            var primes = calculatePrimes(toNumber);
            complete(primes);
        });
    }

    var onClicked = function () {
        calculatePrimesPromise(1000).then(function (primes) {
            var mainContentElement = document.getElementById("main-content-id");

            mainContentElement.innerHTML = "";

            for (var i = 0; i < primes.length; i++) {
                mainContentElement.innerText += primes[i] + " ";
            }
        });
    }

    app.start();
})();
