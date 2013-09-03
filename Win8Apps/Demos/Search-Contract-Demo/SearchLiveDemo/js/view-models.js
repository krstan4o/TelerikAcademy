(function () {
    var computer = {
        name: "",
        imageUrl: "",
        price: 0,
        processor: {
            modelName: "",
            frequencyGHz: 0
        }
    }

    var getComputer = function (name, price, imageUrl, processorName, processorGHz) {
        return new ObservableComputer({
            name: name,
            imageUrl: imageUrl,
            price: price,
            processor: {
                modelName: processorName,
                frequencyGHz: processorGHz
            }
        });
    }

    var ObservableComputer = WinJS.Binding.define(computer);
    var firstComputerObservable = getComputer("Dell Studio 1535", 2000, "/images/studio-1535.png", "Core i5", 2.0);
    var secondComputerObservable = getComputer("HP 650", 1500, "/images/hp-650.jpg", "Intel 1000M", 1.8)

    var computersList = new WinJS.Binding.List([firstComputerObservable, secondComputerObservable]);

    var searchQuery = WinJS.Binding.as({ queryText: "", priceRangeStart: 0, priceRangeLast: 9999 });

    var filteredComputers = computersList.createFiltered(function (item) {
        var queryIndexInItemString =
            JSON.stringify(item).indexOf(searchQuery.queryText);

        var isSelected = queryIndexInItemString > -1 &&
            item.price >= searchQuery.priceRangeStart && 
            item.price <= searchQuery.priceRangeLast;

        return isSelected;
    });

    var changeSearchQuery = function(text){
        searchQuery.queryText = text;
        computersList.notifyReload();
    }

    var submitQuery = function (query) {
        searchQuery.queryText = query.queryText;
        searchQuery.priceRangeStart = query.priceRangeStart;
        searchQuery.priceRangeLast = query.priceRangeLast;
        computersList.notifyReload();
    }

    WinJS.Namespace.define("ViewModels", {
        computers: computersList,
        searchComputers: filteredComputers,
        submitSearchText: changeSearchQuery,
        submitSearchQuery: submitQuery,
        searchQuery: searchQuery
    });
})()