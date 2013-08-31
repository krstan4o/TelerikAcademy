/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="tomato.js" />
/// <reference path="mushroom-mixin.js" />
WinJS.Namespace.define("GmoVegitables", {

    TomatoGmo: WinJS.Class.mix(Food.Vegitable.Tomato, MushroomMixin)
});