/// <reference path="//Microsoft.WinJS.1.0/js/base.js" />

var Animal = WinJS.Class.define(function (name, age, weightKg) {
    this.name = name;
    this.age = age;
    this.weightKg = weightKg;
}, {
    makeSound: function makeSound() {
        console.log(this.name + " made a sound");
    }
}, {
    getStronger: function (animalA, animalB) {
        return animalA.weight > animalB.weight ? animalA : animalB;
    }
});