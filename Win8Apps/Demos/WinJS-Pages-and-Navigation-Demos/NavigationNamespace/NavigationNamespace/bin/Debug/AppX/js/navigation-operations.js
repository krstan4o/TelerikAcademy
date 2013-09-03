(function () {
    var hyperlinkNavigation = function (e) {
        e.preventDefault();
        WinJS.Navigation.navigate(e.target.href);
    }

    WinJS.Namespace.define("NavigationOperations", {
        hyperlinkNavigation: hyperlinkNavigation
    });
})();