var calculations = (function () {
    function isPrime(number) {
        for (var i = 2; i < number; i++)
        {
            if (number % i == 0) {
                return false;
            }
        }
        return true;
    }
    function calculateNumbersWithRange(start, end) {
        var primeNumbers = [];
        for (var i = start; i < end; i++) {
            if (isPrime(i)) {
                primeNumbers.push(i);
            }
        }
        return primeNumbers;
    }
    function calculateNumbersTo(number) {
        var primeNumbers = [];
        for (var i = 0; i < number; i++) {
            if (isPrime(i)) {
                primeNumbers.push(i);
            }
        }
        return primeNumbers;
    }
    function calculateNumbersCount(count) {
        var primeNumbers = [];
        var i = -1;
        while (count > primeNumbers.length) {
            i++;
            if (isPrime(i)) {
                primeNumbers.push(i);
            }
        }
        return primeNumbers;
    }

    return {
        calculateNumbersTo: function (toNumber) {
            return calculateNumbersTo(toNumber);
        },
        calculateNumbersWithRange: function (start, end) {
            return calculateNumbersWithRange(start, end);
        },
        calculateNumbersCount: function (count) {
            return calculateNumbersCount(count);
        }
    };
})();