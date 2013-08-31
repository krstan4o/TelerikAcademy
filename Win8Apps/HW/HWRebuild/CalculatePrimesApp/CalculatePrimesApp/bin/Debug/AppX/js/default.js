// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
   

    app.onactivated = function (args) {
      
            
            var Calculator = new NumbersCalculator();
            var btnCalculatePrimes = document.getElementById("calculate-primes");

            btnCalculatePrimes.addEventListener("click", function () {
                var resultContainer = document.getElementById("calculate-primes-result");
                var numberTo = document.getElementById("number-to").value;
                Calculator.calculateNumberTo(numberTo).then(function (data) {
                    resultContainer.innerHTML = data;
                }, function (error) {
                    resultContainer.innerHTML = error;
                }).done();

            });
            var btnCalculatePrimesToCount = document.getElementById("number-to-count");
            btnCalculatePrimesToCount.addEventListener("click", function () {
                var resultContainer = document.getElementById("calculate-primes-to-count-result");
                var numberTo = document.getElementById("number-to-count-value").value;
                Calculator.calculateFirstNumbers(numberTo).then(function (data) {
                    resultContainer.innerHTML = data;
                }, function (error) {
                    resultContainer.innerHTML = error;
                }).done();

            });
            var btnCalculatePrimesToRange = document.getElementById("number-to-range");
            btnCalculatePrimesToRange.addEventListener("click", function () {
                var resultContainer = document.getElementById("number-to-range-result");
                var numberStart = document.getElementById("start-number").value;
                var numberEnd = document.getElementById("end-number").value;
                Calculator.calculateNumberRange(numberStart, numberEnd).then(function (data) {
                    resultContainer.innerHTML = data;
                }, function (error) {
                    resultContainer.innerHTML = error;
                });
            });
    };
   
  

    app.start();
})();
