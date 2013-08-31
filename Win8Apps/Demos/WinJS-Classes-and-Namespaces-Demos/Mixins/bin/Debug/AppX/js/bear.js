/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="animal.js" />

/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />
/// <reference path="animal.js" />

var Bear = WinJS.Class.derive(Animal, function () {
    //if there's something that happens in the Animal constructor, which this class needs (in this case the _name field, used by the inherited name property), you have to explicitly call the Animal constructor
    //only prototype fields are inherited (i.e. those from the second parameter of the WinJS.Class.define function) 
    Animal.apply(this, arguments);
}, {
    eatHoney: function () {
        console.log(this.name + " ate some honey");
    }
})