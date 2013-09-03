// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/targetpage/targetpage.html", {
        init: function (element, options) {
            //init is called before any processing of the page's html
            //We can work with any content already inside the 'element'. This is a good place to start async work (e.g. downloading data)

            console.log(element.innerHTML);

            element.innerHTML = ""; //clearing the container

            //We create a property for the async work on the page object.
            this.dataDownloadPromise = WinJS.xhr({ url: "http://en.wikipedia.org/wiki/Target" });

            //If we return the promise for the async work, the page will not be ready until the promise is fulfilled. 
            //return this.dataDownloadPromise;
        },

        ready: function (element, options) {
            //using the ready function to get the result from the initialized request in init
            this.dataDownloadPromise.then(function (result) {
                element.innerHTML += toStaticHTML(result.responseText);
            });
        }
    });
})();
