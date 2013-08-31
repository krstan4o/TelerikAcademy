(function () {
    WinJS.Application.onactivated = function () {
        var smallAnimal = new AnimalSprites.SpriteBear("Winnie the Pooh", new Date().getFullYear() - 1924, 3)
        var bigAnimal = new AnimalSprites.SpriteMammoth("Manfred", 2600000, 80000);

        smallAnimal.updateImgSrc("/images/pooh-face.jpg");
        smallAnimal.updatePosition({ topPx: 0, leftPx: 100 });
        smallAnimal.updateToDom();

        bigAnimal.updateImgSrc("/images/mammoth.png");
        bigAnimal.updatePosition({ topPx: 50, leftPx: 500 });
        bigAnimal.updateToDom();
    };

    WinJS.Application.start();
})();
