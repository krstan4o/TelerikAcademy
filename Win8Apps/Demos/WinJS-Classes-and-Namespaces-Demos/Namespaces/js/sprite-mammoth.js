/// <reference path="animal.js" />
/// <reference path="mammoth.js" />
/// <reference path="domSpriteMixin.js" />

WinJS.Namespace.define("AnimalSprites", {
    SpriteMammoth: WinJS.Class.mix(AnimalKingdom.Mammals.Mammoth, DomSpriteMixin)
});