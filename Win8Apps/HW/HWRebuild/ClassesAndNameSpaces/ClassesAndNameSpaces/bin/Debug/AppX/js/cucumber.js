/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />
WinJS.Namespace.defineWithParent(Food, "Vegitable", {
    Cucumber: WinJS.Class.derive(Food.Vegitable, function (color, length) {
        Food.Vegitable.apply(this, [color, false]);

        this.length = length;
    })
});