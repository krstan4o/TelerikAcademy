// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;

    app.onactivated = function (args) {
        WinJS.UI.processAll();
        var buttonShowTime = document.getElementById("show-time-control");
        var self = this;
        buttonShowTime.addEventListener('click', function () {
            var elements = document.getElementsByClassName("time-control");
            if (elements.length != 0) {
               
                for (var i = 0; i < elements.length; i++) {
                    var nodeValueOfDivClass = elements[i].attributes.getNamedItem('class').nodeValue;
                    if (nodeValueOfDivClass == "time-control") {
                        self.className += " hidden";
                    }
                    else {
                        self.className = "time-control"; 
                    }
                }
            }
        });

        var buttonCalculate = document.getElementById("calculate-btn");

    };

   
    app.start();
})();
