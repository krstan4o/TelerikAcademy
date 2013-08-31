/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />



WinJS.Namespace.define("LocalNameSpace",
{
    PrimesCalculator: WinJS.Class.define(function () {
        this._primesWorkers = new Array();
        this._freeWorkers = new Array();
        this.setWorkers(3);
        //this._primesWorkers[0] = new Worker("/js/primesWorker.js");
        //this._freeWorkers[0] = true;

        //this._primesWorkers[1] = new Worker("/js/primesWorker.js");
        //this._freeWorkers[1] = true;

        //this._primesWorkers[2] = new Worker("/js/primesWorker.js");
        //this._freeWorkers[2] = true;
    },
    {
        setWorkers: function (count) {
            this._primesWorkers = new Array();
            this._freeWorkers = new Array();
            for (var i = 0; i < count; i++) {
                this._primesWorkers[i] = new Worker("/js/primesWorker.js");
                this._freeWorkers[i] = true;
            }
        },

        calculate: function (firstNumber, secondNumber, numberOfPrimes) {
            var self = this;
            return new WinJS.Promise(function (complete, error) {
                var localFolder = Windows.Storage.ApplicationData.current.localFolder;
                var fileName = firstNumber.toString() + "-" + secondNumber.toString() + "-" + numberOfPrimes.toString() + ".txt";
                localFolder.
                createFileAsync(fileName,
                Windows.Storage.CreationCollisionOption.failIfExists).then(
                function (file) {
                    self._calculateNew(firstNumber, secondNumber, numberOfPrimes).then(function (primes) {
                        file.openTransactedWriteAsync().then(function (transaction) {
                            var writer = Windows.Storage.Streams.DataWriter(transaction.stream);
                            writer.writeString(JSON.stringify(primes));
                            writer.storeAsync().done(function () {
                                transaction.commitAsync().done(function () {
                                    transaction.close();
                                    complete(primes);
                                });
                            });
                        },
                        function (message) {
                            WinJS.Application.local.remove(fileName);
                        });
                    }, function (messager) {
                        error(messager);
                    });
                },
                function () {
                    //WinJS.Application.local.remove(fileName);

                    WinJS.Application.local.readText(fileName, "failed to open file").then(function (content) {
                        var primes = JSON.parse(content);
                        complete(primes);
                    });
                });

            });
            //return this._calculateNew(firstNumber, secondNumber, numberOfPrimes);
        },

        _calculateNew: function (firstNumber, secondNumber, numberOfPrimes) {
            var freeworkerIndex = -1;
            for (var i = 0; i < this._primesWorkers.length; i++) {
                if (this._freeWorkers[i] == true) {
                    freeworkerIndex = i;
                    break;
                }
            }

            if (freeworkerIndex == -1) {
                return new WinJS.Promise(function (complete, error) {
                    error("There is no free worker available now");
                });
            }
            else {
                var self = this;
                return new WinJS.Promise(function (complete) {
                    var primes = {};
                    self._primesWorkers[freeworkerIndex].onmessage = function (event) {
                        self._freeWorkers[freeworkerIndex] = true;
                        self._primesWorkers[freeworkerIndex].onmessage = null;
                        primes = event.data;
                        complete(primes);
                    }
                    self._freeWorkers[freeworkerIndex] = false;
                    self._primesWorkers[freeworkerIndex].postMessage({ firstNumber: firstNumber, lastNumber: secondNumber, numberOfPrimes: numberOfPrimes });
                });
            }
        }
    })
})