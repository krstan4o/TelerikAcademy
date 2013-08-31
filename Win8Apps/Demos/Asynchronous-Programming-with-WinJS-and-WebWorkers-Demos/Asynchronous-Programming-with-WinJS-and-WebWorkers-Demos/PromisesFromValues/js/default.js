// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var isPrime = function (number) {
        for (var i = 2; i < number; i++) {
            if (number % i == 0) {
                return false;
            }
        }
        return true;
    }

    var calculatePrimes = function (fromNumber, toNumber) {
        var primesList = [];

        for (var num = fromNumber; num <= toNumber; num++) {
            if (isPrime(num)) {
                primesList.push(num);
            }
        }

        return primesList;
    }

    app.onactivated = function (args) {
        var calculatePrimesButton = document.getElementById("calculate-primes-button-id");
        var contentElement = document.getElementById("main-content-id");
        
        calculatePrimesButton.addEventListener("click", function () {
            WinJS.Promise.as(calculatePrimes(0, 1000)).then(function (primes) {
                contentElement.innerText += primes.join(", ");
            });
        });
    };

    app.start();
})();
