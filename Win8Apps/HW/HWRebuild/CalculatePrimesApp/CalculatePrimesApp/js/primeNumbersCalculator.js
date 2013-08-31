/// <regerence path="//Microsoft.WinJS.1.0/js/base.js" />

var NumbersCalculator = WinJS.Class.define(function () {
    this._workersCount = 0;
    this._workerCalculateNumberRange = Worker("js/workers/calculatenumberRange.js");
    this._workerCalculateNumbersCount = Worker("js/workers/calculateNumbersCount.js");
    this._workerCalculateNumberTo = Worker("/js/workers/calculateNumberTo.js");


}, {
    calculateNumberTo: function (toNumber) {
        var self = this;
        return new WinJS.Promise(function (success, error) {
            if (self._workersCount < 3) {
                self._workersCount++;
                self._workerCalculateNumberTo.onmessage = function (event) {
                    self._workersCount--;
                    var primesList = event.data;
                    success(primesList);
                };
                self._workerCalculateNumberTo.postMessage({
                    toNumber: toNumber
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    },
    calculateNumberRange: function (startNumber, endNumber) {
        var self = this;
        return new WinJS.Promise(function (success, error) {
            if (self._workersCount < 3) {
                self._workersCount++;
                self._workerCalculateNumberRange.onmessage = function (event) {
                    self._workersCount--;
                    var primesList = event.data;
                    success(primesList);
                };
                self._workerCalculateNumberRange.postMessage({
                    startNumber: startNumber,
                    endNumber: endNumber
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    },
    calculateFirstNumbers: function (countNumbers) {
        var self = this;
        return new WinJS.Promise(function (success, error) {
            if (self._workersCount < 3) {
                self._workersCount++;
                self._workerCalculateNumbersCount.onmessage = function (event) {
                    self._workersCount--;
                    var primesList = event.data;
                    success(primesList);
                };
                self._workerCalculateNumbersCount.postMessage({
                   toNumber:countNumbers
                });
            } else {
                error("only 3 workers is allowed at some time");
            }
        });
    }
});