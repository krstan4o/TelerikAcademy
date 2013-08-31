/// <reference group="Dedicated Worker" />

var isPrime = function (number) {
    for (var i = 2; i < number; i++) {
        if (number % i == 0) {
            return false;
        }
    }
    return true;
}

var calculatePrimes = function (fromNumber, toNumber, primesToFind) {
    var primesList = [];
    var primesFound = 0;

    for (var num = fromNumber; num <= toNumber; num++) {
        if (isPrime(num)) {
            primesFound++;
            primesList.push(num);
        }
        if (primesToFind && primesFound >= primesToFind) {
            break;
        }
    }

    return primesList;
}

onmessage = function (event) {
    var firstNumber = event.data.firstNumber || 0;
    var lastNumber = event.data.lastNumber;
    var count = event.data.numberOfPrimes;

    var primes = calculatePrimes(firstNumber, lastNumber, count);

    postMessage(primes);
}