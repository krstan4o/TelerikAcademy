/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

WinJS.Namespace.define("AnimalKingdom", {
    Animal : WinJS.Class.define(function (name, age, weightKg) {
        this._name = name;
        this.age = age;
        this.weightKg = weightKg;
    }, {
        //notice the _name field in the constructor, which is used in this property
        name: {
            get: function () { return this._name; },
            set: function (val) {
                var oldName = this._name;
                this._name = val;
                console.log(oldName + "'s name changed to: " + this._name);
            }
        },

        //all following methods use the name property, instead of the _name field
        descriptionString: { get: function () { return "name: " + this.name + ", age: " + this.age + ", weight: " + this.weightKg + "kg" } },

        makeSound: function makeSound() {
            console.log(this.name + " made a sound");
        }
    }, {
        getStronger: function (animalA, animalB) {
            return animalA.weight > animalB.weight ? animalA : animalB;
        }
    })
});