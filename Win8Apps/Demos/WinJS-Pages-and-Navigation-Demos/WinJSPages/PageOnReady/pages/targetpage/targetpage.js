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
        },
    });
})();
