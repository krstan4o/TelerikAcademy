/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="bear.js" />
/// <reference path="domSpriteMixin.js" />
WinJS.Namespace.define("AnimalSprites", {
    SpriteBear: WinJS.Class.mix(AnimalKingdom.Mammals.Bear, DomSpriteMixin)
});