/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
WinJS.Namespace.define("Food", {
    Vegitable: WinJS.Class.define(function (color, canBeDirectlyEaten) {
        this._color = color;
        this._canBeDirectlyEaten = canBeDirectlyEaten;
    },
          {
              canBeDirectlyEaten: {
                  get: function () { return this._canBeDirectlyEaten; }
              },
              color: {
                  get: function () { return this._color; },
                  set: function (val) { this._color = val; }
              },
              eat: function () {
                  if (this._canBeDirectlyEaten == false) {
                      console.log("This vegitable cannot be eaten directly.");
                  }
                  else {

                      console.log("You eated vegitable directly.");
                  }
              }

          })
});