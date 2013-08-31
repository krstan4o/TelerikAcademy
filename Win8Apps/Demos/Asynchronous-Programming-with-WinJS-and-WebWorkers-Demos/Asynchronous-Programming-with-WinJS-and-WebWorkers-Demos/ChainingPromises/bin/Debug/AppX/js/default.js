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

    var calculatePrimes = function (fromNumber, toNumber) {
        var primesList = new Array();

        for (var num = fromNumber; num <= toNumber; num++) {
            if (isPrime(num)) {
                primesList.push(num);
            }
        }

        return primesList;
    }

    var calculatePrimesPromise= function (fromNumber, toNumber) {
        return new WinJS.Promise(function (complete) {
            setTimeout(function () {
                var primes = calculatePrimes(fromNumber, toNumber);
                complete(primes);
            }, 100);
        });
    }

    var printList = function (list) {
        return new WinJS.Promise(function(complete){
            setTimeout(function () {
                var mainContentElement = document.getElementById("main-content-id");

                //uncomment to see changes
                //mainContentElement.innerText = "";

                for (var i = 0; i < list.length; i++) {
                    mainContentElement.innerText += list[i] + " ";
                }
                complete();
            }, 100);
        });
    }

    var onClicked = function () {
        calculatePrimesPromise(0, 2000)
            .then(printList)
            .then(function () {
                return calculatePrimesPromise(2001, 4000);
            })
            .then(printList)
            .then(function(){
                return calculatePrimesPromise(4001, 6000);
            })
            .then(printList)
            .then(function(){
                return calculatePrimesPromise(6001, 8000);
            })
            .then(printList)
            .then(function(){
                return calculatePrimesPromise(8001, 10000)
            })
            .then(printList);
    }

    app.start();
})();
