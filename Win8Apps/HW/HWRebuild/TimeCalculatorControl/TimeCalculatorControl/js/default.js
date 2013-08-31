// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;

    app.onactivated = function (args) {
        WinJS.UI.processAll();
     
    };

   
    app.start();
})();
