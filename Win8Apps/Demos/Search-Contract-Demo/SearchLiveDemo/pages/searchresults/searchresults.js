﻿// For an introduction to the Search Contract template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232512

// TODO: Add the following script tag to the start page's head to
// subscribe to search contract events.
//  
// <script src="/pages/searchresults/searchresults.js"></script>

(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var appModel = Windows.ApplicationModel;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var utils = WinJS.Utilities;
    var searchPageURI = "/pages/searchresults/searchresults.html";
    var searchPane = appModel.Search.SearchPane.getForCurrentView();

    searchPane.placeholderText = "Search for a computer";
    searchPane.showOnKeyboardInput = true;

    ui.Pages.define(searchPageURI, {
        ready: function (element, options) {
            WinJS.Binding.processAll(element, ViewModels);
            ViewModels.submitSearchText(options.queryText);
            //ViewModels.submitSearchQuery({
            //    queryText: options.queryText,
            //    priceRangeStart: 1800,
            //    priceRangeLast: 2100
            //});
        }
    });

    WinJS.Application.addEventListener("activated", function (args) {
        if (args.detail.kind === appModel.Activation.ActivationKind.search) {
            args.setPromise(ui.processAll().then(function () {
                if (!nav.location) {
                    nav.history.current = { location: Application.navigator.home, initialState: {} };
                }

                return nav.navigate(searchPageURI, { queryText: args.detail.queryText });
            }));
        }
    });

    searchPane.onquerysubmitted = function (args) { nav.navigate(searchPageURI, args); };
})();
