(function () {
    WinJS.Application.onactivated = function () {
        var smallAnimal = new Animal("Winnie the Pooh", new Date().getYear() - 1924, 3)
        var bigAnimal = new Animal("Manfred", 2600000, 80000);

        console = new DomLogger(document.getElementById("output"));

        smallAnimal.makeSound();
        bigAnimal.makeSound();

        console.log(smallAnimal.name + " is " + smallAnimal.weightKg + "kg");
        console.log(bigAnimal.name + " is " + bigAnimal.weightKg + "kg");

        var strongerAnimal = Animal.getStronger(smallAnimal, bigAnimal);

        console.log(strongerAnimal.name + " is the stronger animal");
    };

    WinJS.Application.start();
})();
