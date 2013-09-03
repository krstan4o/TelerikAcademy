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

        load: function (uri) {
            //Loads the page content from the uri. Expected to return a promise with DOM Elements as success value.
            //By default this function just copies the DOM elements from the uri through WinJS.UI.Fragments.renderCopy

            return WinJS.UI.Fragments.render(uri).then(function (domElements) {
                console.log(domElements);

                var currentDateTime = new Date();

                var introTextParagraph = WinJS.Utilities.query("#introducing-text", domElements)[0];
                introTextParagraph.innerHTML += " on " + currentDateTime.toLocaleDateString();

                var picHeader = WinJS.Utilities.query("#pic-header", domElements)[0];

                var currHours = currentDateTime.getHours();

                var daytimeString = currHours > 6 && currHours < 18 ? "day" : "night";
                picHeader.innerHTML += " at " + currentDateTime.toLocaleTimeString() + " (" + daytimeString + ")";

                var picCell = WinJS.Utilities.query("#picture-cell", domElements)[0];
                picCell.setAttribute("data-daytime-string", daytimeString);

                return domElements;
            });
        },

        ready: function (element, options) {

            var picCell = document.getElementById("picture-cell");

            var picture = document.createElement("img");

            picture.width = 240;
            picture.height = 240;

            if (picCell.getAttribute("data-daytime-string") == "day") {
                picture.src = "/images/target.png"
            }
            else {
                picture.src = "/images/target-dark.png";
            }

            picCell.appendChild(picture);

            //using the ready function to get the result from the initialized request in init
            this.dataDownloadPromise.then(function (result) {
                var wikiTextCell = document.getElementById("wiki-text");
                wikiTextCell.innerHTML += toStaticHTML(result.responseText);
            });
        }
    });
})();
