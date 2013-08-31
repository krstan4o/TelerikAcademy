/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="vegetable.js" />
WinJS.Namespace.defineWithParent(Food, "Vegitable", {
    Tomato : WinJS.Class.derive(Food.Vegitable, function (color,radius) {
        Food.Vegitable.apply(this, [color, true]);
   
        this.radius = radius;
    })
});