(function () {
    WinJS.Application.onactivated = function () {
        var smallAnimal = new SpriteBear("Winnie the Pooh", new Date().getFullYear() - 1924, 3)
        var bigAnimal = new SpriteMammoth("Manfred", 2600000, 80000);

        console = new DomLogger(document.getElementById("output"));

        console.log("small animal:");
        console.log(smallAnimal.descriptionString);
        console.log("");
        console.log("big animal:");
        console.log(bigAnimal.descriptionString);

        bigAnimal.name = "Manny";
        bigAnimal.goExtinct();
        smallAnimal.eatHoney();

        smallAnimal.updateImgSrc("/images/pooh-face.jpg");
        smallAnimal.updatePosition({ topPx: 250, leftPx: 400 });
        smallAnimal.updateToDom();

        bigAnimal.updateImgSrc("/images/mammoth.png");
        bigAnimal.updatePosition({ topPx: 300, leftPx: 600 });
        bigAnimal.updateToDom();


        //surprise (for fun purposes only)
        setTimeout(function () {
            smallAnimal.updateImgSrc("/images/pooh-face-scary.jpg");
            smallAnimal.spriteContainer.innerHTML +=
                "<p style='font-size:3em;'>IT'S STARING <br>INTO YOUR SOUL</p>";
        }, 7000);
    };

    WinJS.Application.start();
})();
