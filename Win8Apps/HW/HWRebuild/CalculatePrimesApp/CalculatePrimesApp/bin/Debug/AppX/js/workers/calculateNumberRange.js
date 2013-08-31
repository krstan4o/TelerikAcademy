/// <reference group="Dedicated Worker" />
self.importScripts("/js/helpers/calculations.js");
onmessage = function (event) {
    var startNumber = event.data.startNumber;
    var endNumber = event.data.endNumber;
    var result = calculations.calculateNumbersWithRange(startNumber, endNumber);
    postMessage(result);
}
