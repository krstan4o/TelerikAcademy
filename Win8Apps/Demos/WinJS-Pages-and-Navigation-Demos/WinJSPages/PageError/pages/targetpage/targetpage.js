// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/targetpage/targetpage.html", {
        ready: function (element, options) {
            //ready is called when the page is visualized (either navigated to through WinJS.Navigation, or directly through a hyperlink, or rendered with the .render() method)
            //We can do anything with the page content here - The page's html is loaded into 'element', WinJS controls are processed, bindings are applied, etc.

            var targetModeSwitch = WinJS.Utilities.id("target-mode-switch").get(0);
            targetModeSwitch.winControl.onchange = function () {
                var targetImg = WinJS.Utilities.id("target-image");
                
                if (targetModeSwitch.winControl.checked) {
                    targetImg.setAttribute("src", "/images/nuke-fireball.jpg");
                }
                else {
                    targetImg.setAttribute("src", "/images/target.png");
                }
            };

            //The following line will cause an exception, which will propagate as a promise in the error state up to the error function
            someundeclaredvariableandgenerallywrongcode
        },

        error: function (error) {
            //This function handles any errors which may occur while loading the page (in any of the methods ready, load, processed, render, init)
            //The error object is the value of a promise in the error state. Implement this function ONLY if you are sure you are processing all errors correctly - any exceptions will be absorbed by this function, so if you cannot handle the error, return a promise in the error state or re-throw the exception

            var pageHolder = document.getElementById("page-holder");

            pageHolder.innerHTML = "<img src='/images/error.png'/>";
            pageHolder.innerHTML += "<p>The stringified error is:</p>"

            var errorContainer = document.createElement("p");
            errorContainer.innerText = JSON.stringify(error);
            pageHolder.appendChild(errorContainer);

            console.log(error);
        }
    });
})();
