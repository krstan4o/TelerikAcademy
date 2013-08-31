(function () {
    WinJS.Application.onactivated = function () {
        var blueTomatto = new Food.Vegitable.Tomato("blue", 5);

        console = new DomLogger(document.getElementById("output"));
        console.log("BlueTomatto: ");
        console.log(blueTomatto.canBeDirectlyEaten);
        console.log(blueTomatto.color);
        console.log(blueTomatto.radius);
        blueTomatto.eat();
       

        var cucumber = new Food.Vegitable.Cucumber("red", 20);
        console.log("Cucumber: ");
        cucumber.eat();
        console.log(cucumber.color);
        console.log(cucumber.length);
        
        var cucumberGmo = new GmoVegitables.CucumberGmo("purple-blue", 30);
        console.log("cucumber GMO: ");
        cucumberGmo.eat();
        console.log(cucumberGmo.color);
        console.log(cucumberGmo.length);
        cucumberGmo.grow(50);
        console.log(cucumberGmo.length);
        
        var tomattoGmo = new GmoVegitables.TomatoGmo("redishblue", 100);
        console.log("Tomato GMO: ");
        tomattoGmo.eat();
        console.log(tomattoGmo.color);
        console.log(tomattoGmo.radius);
        tomattoGmo.grow(150);
        console.log(tomattoGmo.radius);
    };

    WinJS.Application.start();
})();
