/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="cucumber.js" />
/// <reference path="mushroom-mixin.js" />
WinJS.Namespace.define("GmoVegitables", {
    CucumberGmo: WinJS.Class.mix(Food.Vegitable.Cucumber, MushroomMixin)
});


