// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    
    var primesWorker = {};

    app.onactivated = function (args) {
        //worker created on app activation, NOT on demand (worker creation is slow, better pre-initialize than lazy-initialize)
        primesWorker = new Worker("/js/primesWorker.js");
        primesWorker.onmessage = function (event) {
            var primesPar = document.createElement("p");
            primesPar.innerText = event.data.join(", ");
            document.body.appendChild(primesPar);
        }

        var calculatePrimesButton = document.getElementById("calculate-primes");
        var primesFirstInput = document.getElementById("primes-first");
        var primesLastInput = document.getElementById("primes-last");

        calculatePrimesButton.addEventListener("click", function () {
            primesWorker.postMessage({ firstNumber: primesFirstInput.value, lastNumber: primesLastInput.value});
        });
    };

    app.start();
})();
