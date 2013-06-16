var canvas = document.createElement("canvas");
var ctx = canvas.getContext("2d");
canvas.width = 400;
canvas.height = 400;
document.body.appendChild(canvas);

var heroReady = false;
var heroImg = new Image();

var hero = {
    speed: 200    
}

var backet = [];

var grid = new GameGrid(10,10, 20);
//var genFigures = grid.GenerateFigures(1);

var customFigures = [{ type: "ninetile", x: 2, y: 2 }, { type: "angle-dr", x: 1, y: 1 },
{ type: "angle-dr", x: 1, y: 1 }, { type: "hline", x: 3, y: 1 }];

var render = function () {
    ctx.fillStyle = "#00f"
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    produceFigures();

    ctx.drawImage(heroImg, hero.x, hero.y);          
    
    // FF
    // heroImg.onload = function () {              
    //     //ctx.fillStyle = "#00f"
    //     //ctx.fillRect(0, 0, canvas.width, canvas.height);
    //};
    heroImg.src = "Images/hero.png";    
}


var produceFigures = function () {
     var cell = {
        width: 20,
        height: 20
    }      

     function produceNinetile(x, y) {        
        ctx.fillStyle = "#ff0"
        ctx.fillRect(x - 20, y - 20, 3 * 20, 3 * 20);

        ctx.fillStyle = "#fA0"
        ctx.fillRect(x, y, 20, 20);
    }
    
    for (var cell = 0; cell < customFigures.length; cell++) {
        var currFigure = customFigures[cell];
        
        switch (currFigure.type) {
            case "ninetile":produceNinetile(currFigure.x * 20, currFigure.y * 20); break;
        }        
    }

}

addEventListener("keydown", function (e) {
    update(e.keyCode); 
}, false);

// Display the figures that are present at 1 1 - dummy function for testing purposes
var displayResidents = function (row, col) {
    var residentsOfCell = grid.GetCellResidents(row, col);
    var collision = ("--- Residents of cell at row : " + row + " col : " + col + " ---");
    for (var i in residentsOfCell) {
       collision += i + " : " + residentsOfCell[i];
    }
    
    return collision;
}

var moveFigure = function (key, figure) {

    //if (key >= 37 && key <= 40) {
    //    update(key);
    //}

    if (key == 32) {
        backet.push(figure);
        console.log(backet); 

    }   
}

var update = function (key) {

    switch (key) {
        case 38: hero.y -= 20; break; // Uo
        case 40: hero.y += 20; break; // Down
        case 37: hero.x -= 20; break; // Left
        case 39: hero.x += 20; break; // Right
    }

    // Lower border
    if (hero.y > 400) {
        hero.y -= 380;
    }

    // Upper border
    if (hero.y < 0) {
        hero.y = 0;
    }

    // Right border
    if (hero.x > 400) {
        hero.x -= 380;
    }

    // Left border
    if (hero.x < 0) {
        hero.x = 0;
    }

    var htmlCell = document.getElementById("currcell");
    var currFig = document.getElementById("currfig");
           

    for (var i = 0; i < customFigures.length; i++) {
        var currFigure = customFigures[i];
        
        
        if (currFigure.x * 20 == hero.x && currFigure.y * 20 == hero.y) {

            currFig.innerHTML = "X: " + currFigure.x * 20 + "<br>Y: " + currFigure.y * 20 + "<br> Type:" + currFigure.type;
            
            document.onkeydown = function (e) {                
                moveFigure(e.keyCode, currFigure);
            }                    
                        
            htmlCell.innerHTML = displayResidents(currFigure.y, currFigure.x);
            break;
        }

    }
   
    var coords = document.getElementById("coords");
    coords.innerHTML = "X: " + hero.x + "<br/> Y: " + hero.y;


}


var reset = function () {
    hero.x = 0;
    hero.y = 0;
}


var main = function () {
    var now = Date.now();
    var delta = now - then;

    update();
    render();

    then = now;
};

reset();
var then = Date.now();
setInterval(main, 100);


//grid.PopulateGameField(customFigures);
//console.log("Level completed: " + grid.CheckLevelCompleted()); // false
//grid.DisplayOverlaps();

//grid.TakeFigure("angle-dr", 1, 1);
//console.log("Level completed: " + grid.CheckLevelCompleted()); // false
//grid.DisplayOverlaps();

//grid.PutFigure("angle-dr", 5, 5);
//console.log("Level completed: " + grid.CheckLevelCompleted()); // false
//grid.DisplayOverlaps();

//grid.TakeFigure("angle-dr", 1, 1);
//grid.PutFigure("angle-dr", 0, 0);
//console.log("Level completed: " + grid.CheckLevelCompleted()); // true
//grid.DisplayOverlaps();