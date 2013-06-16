function Engine(width, height, cellSize, serverMode) {
    var gridWidth = width / cellSize;
    var gridHeight = height / cellSize;
    var halfCellSize = cellSize >> 1;
    var totalWidth = $("#wrapper").width();
    var figuresMenuX = totalWidth - ((totalWidth - width) >> 1);
    var figuresMenuY = .4 * height + cellSize;
    var menuGroup;
    var carrying = false;
    var carriedFigure;
    var maxPath;
    var currentPathLen = 0;
    var nervata = 0;
    var levelCompleted = false;
    var figuresDict = new Dictionary();
    var grid = new GameGrid(gridWidth, gridHeight, cellSize);
    var win = false;
    var connectionEstablished = false;
    var connectionRecieved = false;
    var serverActive = serverMode;
    var inGame = false;
    var playerName;

    var log = $("#chat");
    log.writeLine = function (text) {
        var li = document.createElement("li");
        li.innerHTML = text;
        log.prepend(li);
    }

    var cover = document.getElementById("cover");
    cover.setAttribute('style', "width:" + totalWidth + "px;height:" + height + "px;");


    var overlaps = document.getElementById("overlaps");
    overlaps.update = function (data) {
        var output = "";

        var index = 0;
        for (var r = 0; r < gridHeight; r++) {
            for (var c = 0; c < gridWidth; c++) {
                if (parseInt(data[index]) > 1) {
                    output += '<span class="overlap">' + data[index++] + '&nbsp;&nbsp;</span>';
                }
                else {
                    output += data[index++] + '&nbsp;&nbsp;';
                }
            }
            output += "<br />";
        }

        overlaps.innerHTML = output;
    }

    this.log = function () {
        return log;
    }

    var sendMsg = function () {
        if ($("#chat-input").val().length > 0) {
            sHub.server.checkUserAuth().done(function () {
                sHub.server.sendMsg($("#chat-input").val(), false).done(function () {
                    $("#chat-input").val("")
                });
            });
        }
    }

    var hideControlls = function (inGame) {
        $("#queue-btn").hide();
        $("#unqueue-btn").hide();
        if (inGame) {
            $("#unqueue-btn").show();
        }
    }

    if (serverActive) {

        $("#unqueue-btn").hide();

        //The three main hubs CGWeb provides
        var lobby = $.connection.lobby;
        var games = $.connection.games;
        var chat = $.connection.chat;
        var sHub = $.connection.sinkBreakerHub;

        sHub.client.printMsg = function (msg) {
            if (!inGame) {
                log.writeLine(msg);
            }
        }

        sHub.client.hideControlls = function (inGame) {
            hideControlls(inGame);
        }

        sHub.client.setUserName = function (setName) {
            connectionEstablished = true;
            playerName = setName;

            // Set player name in UI
            var currPlayerName = document.getElementById("currPlayer");
            currPlayerName.innerHTML = playerName;

            log.writeLine("Server: Подвизаваш се като: " + playerName);
        }

        sHub.client.redirectMe = function (target) {
            window.location = target;
        }

        sHub.client.endGame = function (win) {
            endGame(win);
        }

        sHub.client.populateFigures = function (figures) {
            startGame(figures);
            initPlayer();
        }

        sHub.client.updateEnemyOverlaps = function (data) {
            overlaps.update(data);
        }

        sHub.client.log = function (data) {
            log.writeLine(data);
        }
    }

    var Player = function (originalBody, x, y) {
        var bodyTransform;
        var _originalBody = originalBody;

        this.originalBodySet = function (body) {
            _originalBody = body;
        }

        this.body = function (body) {
            if (body) {
                bodyTransform = body;
            }
            else {
                return bodyTransform;
            }
        }

        this.enterCanvas = function addingPlayerToCanvas() {
            canvas.add(bodyTransform);
        }

        this.exitCanvas = function removingPlayerFromCanvas() {
            canvas.remove(bodyTransform);
        }

        this.restoreBody = function createPlayer(x, y) {
            var bodyRestore = _originalBody;
            bodyRestore.set("left", x ? x + cellSize : cellSize + halfCellSize);
            bodyRestore.set("top", y ? y + cellSize : cellSize + halfCellSize);
            return bodyRestore;
        }

        this.x = function () {
            return Math.round(parseInt((bodyTransform.get("left") - halfCellSize)) / cellSize) * cellSize;
        }

        this.y = function () {
            return Math.round(parseInt((bodyTransform.get("top") - halfCellSize)) / cellSize) * cellSize;
        }

        var translate = function playerTranslate(axis, destination) {

            bodyTransform.set(axis, destination);
            updateFiguresMenu(player.x() + " " + player.y());
            canvas.renderAll();

            if (carrying) {
                updateNervata();
            }
        }

        this.move = function playerMovement(direction) {
            if (levelCompleted) {
                return;
            }

            if (direction == "left") {
                _originalBody.setFlipX(true);
            }
            else if (direction == "right") {
                _originalBody.setFlipX(false);
            }

            var currentPositionX = bodyTransform.get("left");
            var currentPositionY = bodyTransform.get("top");
            var destination;

            switch (direction) {
                case "up":
                    if (currentPositionY > halfCellSize) {
                        translate("top", currentPositionY - cellSize);
                    }
                    break;
                case "down":
                    if (currentPositionY < height - cellSize) {
                        translate("top", currentPositionY + cellSize);
                    }
                    break;
                case "left":
                    if (currentPositionX > halfCellSize) {
                        translate("left", currentPositionX - cellSize);
                    }
                    break;
                case "right":
                    if (currentPositionX < width - cellSize) {
                        translate("left", currentPositionX + cellSize);
                    }
                    break;
                default:

            }
        }

        // INIT
        bodyTransform = (this.restoreBody(x, y));
    }

    var KeyboardController = function (keys, repeat) {
        // Lookup of key codes to timer ID, or null for no repeat
        var timers = {};

        // When key is pressed and we don't already think it's pressed, call the
        // key action callback and set a timer to generate another one after a delay
        document.onkeydown = function (event) {
            var key = (event || window.event).keyCode;
            if (!(key in keys))
                return true;
            if (!(key in timers)) {
                timers[key] = null;
                keys[key]();
                if (repeat !== 0)
                    timers[key] = setInterval(keys[key], repeat);
            }
            return false;
        };

        // Cancel timeout and mark key as released on keyup
        document.onkeyup = function (event) {
            var key = (event || window.event).keyCode;
            if (key in timers) {
                if (timers[key] !== null)
                    clearInterval(timers[key]);
                delete timers[key];
            }
        };

        // When window is unfocused we may not get key events. To prevent this
        // causing a key to 'get stuck down', cancel all held keys
        window.onblur = function () {
            for (key in timers)
                if (timers[key] !== null)
                    clearInterval(timers[key]);
            timers = {};
        };
    };

    var currentCanvas = $("#c");
    currentCanvas.attr("width", totalWidth + "px");
    currentCanvas.attr("height", height + "px");
    currentCanvas.css("background", "url(Images/tile.gif)")


    var playerSet = function (newPlayer) {
        player = newPlayer;
    }

    var playerGet = function () {
        return player;
    }

    var canvas = new fabric.Canvas('c');

    this.generateFigures = function (dificultyLevel) {
        return grid.generateFigures(dificultyLevel);
    }

    InitFiguresDictionary(figuresDict, gridWidth, gridHeight, cellSize);

    var player;

    var endGame = function (win) {
        levelCompleted = true;
        if (win) {
            log.writeLine("БАНЯТА Е НАРЕДЕНА!!!");
            canvas.add(new fabric.Text("ДОООБРЕ БЕ!!!", { top: height >> 1, left: width >> 1, fill: "#14ba03", textShadow: 'white 2px 2px 2px' }));
        }
        else {
            canvas.add(new fabric.Text("ОПЛЕСКА РАБОТАТА!!!", { top: height >> 1, left: width >> 1, fill: "#ff0000", textShadow: 'white 2px 2px 2px' }));
        }
    }

    var serverTakeFigureFunction = function (figureType, x, y) {
        sHub.server.takeFigure(figureType, x, y);
    };

    this.serverTakeFigureFunction = function (func) {
        serverTakeFigureFunction = func;
    }

    var serverPlaceFigureFunction = function (figureType, x, y) {
        sHub.server.placeFigure(figureType, x, y);
    };


    this.serverPlaceFigureFunction = function (func) {
        serverPlaceFigureFunction = func;
    }

    this.levelCompleted = function () {
        return levelCompleted;
    }

    var startGame = function (figures) {
        grid.populateGameField(figures);
        initFigures(figures);
    }


    var canvas = new fabric.Canvas('c');

    function initPlayer() {
        fabric.Image.fromURL('Images/hero_big.png', function (oImg) {
            oImg.scale(1.4);
            playerSet(new Player(oImg, 30, 30));
            player = playerGet();
            player.enterCanvas();
        });
    }

    KeyboardController({
        37: function () { player.move("left") },
        38: function () { player.move("up") },
        39: function () { player.move("right") },
        40: function () { player.move("down") },
        86: function () { handleFigure() },
        13: function () { sendMsg() },
        32: function () { toggleResidentFigures() }
    }, 150);

    if (serverActive) {
        games.client.gameStarting = function () {
            //This method will be called when the game is starting
            inGame = true;
            log.writeLine("Банята се зарежда...");
            sHub.server.checkStatus(true).done(function () {
                sHub.server.getFigures().done(function () {
                    log.writeLine("Действай!");
                    $("#unqueue-btn").show();
                    sHub.server.sendMsg("Започва игра...", true);
                });
            });
        };

        games.client.gameEnded = function () {
            //This method will be called after the game has ended

        };
    }

    this.startGame = function (figures) {
        startGame(figures);
        initPlayer();
        if (!serverActive) {
            overlaps.update(grid.getOverlaps());
        }
    };

    if (serverActive) {
        try {
            $.connection.hub.start(
            ).done(function () {

                sHub.server.checkUserAuth().done(function () {
                    sHub.server.playerName().done(function () {
                        sHub.server.checkStatus(true);
                    })
                });


                $("#queue-btn").click(function () {
                    var args = "{ RequiredPlayers: 2 }";
                    games.server.tryQueueForGame("SinkBreakerGame", args).done(function () {
                        sHub.server.checkStatus(false);
                        sHub.server.sendMsg("Търсят се балами за игра... :)", true);
                    });
                    log.writeLine("Game: Търсиме сега други балами за игра...");
                    //sHub.server.checkStatus();
                });

                $("#chat-btn").click(function () {
                    sendMsg();
                });

                $("#unqueue-btn").click(function () {
                    log.writeLine("Айде стига толкова...");
                    sHub.server.sendMsg("Напусна играта...", false);
                    games.server.leaveGameroom().done(function () {
                        location.reload();
                    });
                });

                log.writeLine("Game: Търся си сървъра.... ЙООООООО СЪРВЪРЕ!");

            });
        } catch (e) {
            location.reload();
        }


        setTimeout(function () {
            if (!connectionEstablished) {
                setTimeout(function () {
                    log.writeLine("Game: Нещо го няма сървъра...");
                    log.writeLine("Game: Ще взема да си набия един рефреш...");
                }, 2000);
                location.reload();
            }
        }, 2000);
    }


    var handleFigure = function () {
        var currentX = player.x();
        var currentY = player.y();

        var position = currentX + " " + currentY;

        if (!carrying) {
            carriedFigure = figuresDict.getItem(position).shift();

            if (carriedFigure != undefined) {
                // Play sound
                document.getElementById("clickAudio").play();
                canvas.remove(carriedFigure);
                player.exitCanvas();

                var carrier = new fabric.Group([player.body(), carriedFigure]);

                player.body(carrier);
                player.enterCanvas();

                canvas.renderAll();
                carrying = true;
                currentPathLen = 0;
                updateFiguresMenu(position);
                grid.takeFigure(carriedFigure.get('type'), currentX, currentY);

                if (serverActive && serverTakeFigureFunction != undefined) {
                    serverTakeFigureFunction(carriedFigure.get('type'), currentX, currentY);
                }

                maxPath = grid.getShortestPathLen(carriedFigure.get('type'), currentX, currentY) / cellSize;

                // Curr move length
                var currMoveUi = document.getElementById("maxPath");
                currMoveUi.innerHTML = "Max Path: " + maxPath;

                log.writeLine("MAX PATH: " + maxPath);
            }
        }
        else {
            if (carriedFigure != undefined) {

                if (!grid.CheckFigureDestinationForOverlaps(carriedFigure.get('type'), currentX, currentY)) {
                    grid.putFigure(carriedFigure.get('type'), currentX, currentY);

                    if (serverActive && serverPlaceFigureFunction != undefined) {
                        serverPlaceFigureFunction(carriedFigure.get('type'), currentX, currentY);
                    }

                    if (!serverActive) {
                        overlaps.update(grid.getOverlaps());
                    }

                    carriedFigure.set({ left: currentX + halfCellSize, top: currentY + halfCellSize });
                    figuresDict.getItem(position).push(carriedFigure);
                    canvas.add(carriedFigure);

                    // Play sound
                    document.getElementById("clickAudio").play();
                    player.exitCanvas();
                    player.body(player.restoreBody(player.x() - halfCellSize, player.y() - halfCellSize));
                    player.enterCanvas();

                    if (!serverActive && grid.checkIfLevelCompleted()) {
                        endGame(true);
                    }
                    carrying = false;
                }
                else {
                    log.writeLine("Къде се опитваш да я ръгнеш сега тая фигура!?");
                }
            }
        }
    }

    var updateFiguresMenu = function (cellPosition) {
        if (menuGroup) {
            canvas.remove(menuGroup);
        }

        var currentResidents = figuresDict.getItem(cellPosition);
        if (currentResidents && currentResidents.length > 0) {
            var clones = [];

            for (var i = 0; i < currentResidents.length; i++) {
                var currentResident = currentResidents[i];
                var newFigure = createFigure(currentResident.type, 0, (i * (cellSize << 2)));
                clones.push(newFigure);
            }

            menuGroup = new fabric.Group(clones, {
                left: figuresMenuX,
                top: figuresMenuY
            });

            canvas.add(menuGroup);
            canvas.renderAll();
        }
    }

    function InitFiguresDictionary(figuresDict, gridWidth, gridHeight, cellSize) {
        for (var r = 0; r < gridHeight; r++) {
            for (var c = 0; c < gridWidth; c++) {
                figuresDict.add((c * cellSize) + " " + (r * cellSize), []);
            }
        }
    }

    var toggleResidentFigures = function () {
        var currentResidents = figuresDict.getItem((player.x()) + " " + (player.y()));
        currentResidents.push(currentResidents.shift());
        updateFiguresMenu(player.x() + " " + player.y());
        canvas.renderAll();
    }

    ////overlaps.update(grid.getOverlaps());
    //log.writeLine("Level starts now!");
    //log.writeLine("WASD to move arround");
    //log.writeLine("Spacebar to pick up or put down a figure");
    //log.writeLine("Make sure no figures overlap :)");

    function createPlus(x, y) {
        var matrix = [
            [0, 1, 0],
            [1, 1, 1],
            [0, 1, 0]
        ];

        return initFigure(matrix, x, y, "#9599fc", "plus");
    }

    function createNinetile(x, y) {
        var matrix = [
            [1, 1, 1],
            [1, 1, 1],
            [1, 1, 1]
        ];

        return initFigure(matrix, x, y, "#61d555", "ninetile");
    }

    function createVline(x, y) {
        var matrix = [
            [0, 1, 0],
            [0, 1, 0],
            [0, 1, 0]
        ];

        return initFigure(matrix, x, y, "#ffb27e", "vline");
    }

    function createHline(x, y) {
        var matrix = [
            [0, 0, 0],
            [1, 1, 1],
            [0, 0, 0]
        ];

        return initFigure(matrix, x, y, "#ff8bf6", "hline");
    }

    function createAngleUL(x, y) {
        var matrix = [
            [0, 1, 0],
            [1, 1, 0],
            [0, 0, 0]
        ];

        return initFigure(matrix, x, y, "#faf76c", "angle-ul");
    }

    function createAngleUR(x, y) {
        var matrix = [
            [0, 1, 0],
            [0, 1, 1],
            [0, 0, 0]
        ];

        return initFigure(matrix, x, y, "#7fb0ff", "angle-ur");
    }

    function createAngleDL(x, y) {
        var matrix = [
            [0, 0, 0],
            [1, 1, 0],
            [0, 1, 0]
        ];

        return initFigure(matrix, x, y, "#ff3d60", "angle-dl");
    }

    function createAngleDR(x, y) {
        var matrix = [
            [0, 0, 0],
            [0, 1, 1],
            [0, 1, 0]
        ];

        return initFigure(matrix, x, y, "#ff795c", "angle-dr");
    }

    function initFigure(matrix, x, y, color, type) {
        var tiles = new Array();

        for (var r = 0; r < 3; r++) {
            for (var c = 0; c < 3; c++) {

                var tile = new fabric.Rect({
                    width: cellSize,
                    height: cellSize,
                    //fill: r == 1 && c == 1 ? centerColor : tilesColor,
                    fill: color,
                    left: c * cellSize,
                    top: r * cellSize,
                    opacity: (matrix[r][c] != 0 ? r == 1 && c == 1 ? 1 : .5 : 0),
                    strokeWidth: 3,
                    stroke: 'rgba(255,255,255,0.5)'
                });


                tiles.push(tile);
            }
        }

        var g = new fabric.Group(tiles, {
            left: x + halfCellSize,
            top: y + halfCellSize,
            type: type
        });

        return g;
    }

    function initFigures(figures) {
        var fLen = figures.length;
        for (var i = 0; i < fLen; i++) {
            var figure = figures[i];
            var newFigure = createFigure(figure.type, figure.x, figure.y);
            figuresDict.getItem(figure.x + " " + figure.y).push(newFigure);
            canvas.add(newFigure);
        }
    }

    function createFigure(figureType, x, y) {
        switch (figureType) {
            case "ninetile":
                return createNinetile(x, y)
            case "plus":
                return createPlus(x, y);
            case "vline":
                return createVline(x, y);
            case "hline":
                return (createHline(x, y));
            case "angle-ul":
                return createAngleUL(x, y);
            case "angle-ur":
                return createAngleUR(x, y);
            case "angle-dl":
                return createAngleDL(x, y);
            case "angle-dr":
                return createAngleDR(x, y);
            default:

        }
    }


    function updateNervataElement() {
        var nervaEl = document.getElementById("nerva");
        var positionX = (nervata / 10) * 60;
        nervaEl.style.backgroundPosition = "-" + positionX + "px 0";

    }

    function updateNervata() {

        // Your moves ui
        var currMove = document.getElementById("currMove");
        currMove.innerHTML = ++currentPathLen + " / " + maxPath;

        //log.writeLine(++currentPathLen + " / " + maxPath);
        if (currentPathLen > maxPath) {
            nervata += 10;
            updateNervataElement();
            log.writeLine("Game: Нервата се покачва на " + nervata + "% !!!");
            if (nervata >= 100) {

                // Sink break
                var sink = document.getElementById("sink");
                sink.style.backgroundPosition = "-115px 0";

                endGame(false);
            }
        }
    }

    canvas.add(new fabric.Rect({
        width: totalWidth - width,
        height: height,
        left: totalWidth - ((totalWidth - width) >> 1),
        top: height >> 1,
        fill: "#b0eefd"
    }));

}