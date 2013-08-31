/// <reference group="Dedicated Worker" />
self.importScripts("/js/helpers/calculations.js");
onmessage = function (event) {

    var number = event.data.toNumber;
    var result = calculations.calculateNumbersCount(number);
    postMessage(result);
}
