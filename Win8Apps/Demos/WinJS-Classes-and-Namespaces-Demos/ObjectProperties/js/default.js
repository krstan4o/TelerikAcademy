(function () {
    WinJS.Application.onactivated = function () {
        var smallAnimal = new Animal("Winnie the Pooh", new Date().getFullYear() - 1924, 3)
        var bigAnimal = new Animal("Manfred", 2600000, 80000);

        console = new DomLogger(document.getElementById("output"));

        console.log("small animal:");
        console.log(smallAnimal.descriptionString);
        console.log("");
        console.log("big animal:");
        console.log(bigAnimal.descriptionString);

        bigAnimal.name = "Manny";
    };

    WinJS.Application.start();
})();
