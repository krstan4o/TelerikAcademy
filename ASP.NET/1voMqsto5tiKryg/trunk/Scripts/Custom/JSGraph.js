// Game grid v1.0
// Holds the game grid like a two dimensional matrix would, but it is insted represented with an undirected weightless graph.
// All game logic is controlled from here.
function GameGrid(width, height, cellSize) {
    var figureTypes = ['ninetile', 'plus', 'hline', 'vline', 'angle-ur', 'angle-dr', 'angle-dl', 'angle-ul'];
    var gameField = new Dictionary();
    var gridWidth;
    var gridHeight;

    // The component that the game grid is build from
    var GameGridCell = function (x, y) {

        //  NEIGHBOURS
        var _top;
        var _bottom;
        var _left;
        var _right;

        //  DIAGONALS
        var _topRight;
        var _topLeft;
        var _bottomLeft;
        var _bottomRight;

        // OTHER MEMBERS
        var _name;
        var _visited;
        var _originalY;
        var _originalX;
        var _covered;
        var _overlapTilesCount;
        var _currentFiguresResidents;

        //  INIT
        _currentFiguresResidents = new Dictionary();
        var len = figureTypes.length;

        for (var i = 0; i < len; i++) {
            _currentFiguresResidents.add(figureTypes[i], { count: 0 });
        }

        _name = x + " " + y;
        _visited = false;
        _originalY = y;
        _originalX = x;
        _overlapTilesCount = 0;

        // adds or removes a figure type as resident for the current cell. 1 for adding -1 for substracting
        this.adjustResident = function (figureType, adjustValue) {
            _currentFiguresResidents.getItem(figureType).count += parseInt(adjustValue);
        }

        // Returns an object holding all the residents for the current cell
        this.getResidents = function () {
            var arr = new Dictionary();

            for (var resident in _currentFiguresResidents.items) {
                var currentResidentcount = _currentFiguresResidents.getItem(resident).count;

                if (currentResidentcount > 0) {
                    arr.add(resident, currentResidentcount);
                }
            }

            return arr.items;
        }

        // Increases or decreases the amount of overlaping tiles for the current cell
        this.overlapTilesCountAdjust = function (amount) {
            _overlapTilesCount += amount;
        }

        // Returns boolean true if more than one tile is resident at this moment for this cell and false if there is one or less.
        this.overlappingTilesGet = function () {
            return _overlapTilesCount > 1 ? true : false;
        }

        // Returns the number of overlaps for the current cell
        this.overlappingTilesCountGet = function () {
            return _overlapTilesCount;
        }

        // Marks or unmarks a cell as visited by the search
        this.visitedSet = function (value) {
            _visited = value;
        }

        // Returns the current visited mark
        this.visitedGet = function () {
            return _visited;
        }

        // Returns boolean true if the cell is empty, meaning no tiles are currently covering it
        this.coveredGet = function () {
            return _overlapTilesCount > 0 ? true : false;
        }

        // Gets the name of the cell in "row col" format
        this.name = function () {
            return _name;
        }

        // The col of the cell given at creation time
        this.originalX = function () {
            return _originalX;
        }

        // The row of the cell given at creation time
        this.originalY = function () {
            return _originalY;
        }

        //  NEIGHBOURS PROPERTIES

        // Sets the top neighbour of the cell
        this.topSet = function (value) {
            _top = value;
        }

        // Returns the top neighbour of the cell
        this.topGet = function () {
            return _top;
        }

        // Sets the bottom neighbour of the cell
        this.bottomSet = function (value) {
            _bottom = value;
        }

        // Returns the bottom neighbour of the cell
        this.bottomGet = function () {
            return _bottom;
        }

        // Sets the left neighbour of the cell
        this.leftSet = function (value) {
            _left = value;
        }

        // Returns the left neighbour of the cell
        this.leftGet = function () {
            return _left;
        }

        // Sets the right neighbour of the cell
        this.rightSet = function (value) {
            _right = value;
        }

        // Returns the right neighbour of the cell
        this.rightGet = function () {
            return _right;
        }

        //  DIAGONALS PROPERTIES

        // Sets the top-left neighbour of the cell
        this.topLeftSet = function (value) {
            _topLeft = value;
        }

        // Returns the top-left neighbour of the cell
        this.topLeftGet = function () {
            return _topLeft;
        }

        // Sets the top-right neighbour of the cell
        this.topRightSet = function (value) {
            _topRight = value;
        }

        // Returns the top-right neighbour of the cell
        this.topRightGet = function () {
            return _topRight;
        }

        // Sets the bottom-left neighbour of the cell
        this.bottomLeftSet = function (value) {
            _bottomLeft = value;
        }

        // Returns the bottom-left neighbour of the cell
        this.bottomLeftGet = function () {
            return _bottomLeft;
        }

        // Sets the bottom-right neighbour of the cell
        this.bottomRightSet = function (value) {
            _bottomRight = value;
        }

        // Returns the bottom-right neighbour of the cell
        this.bottomRightGet = function () {
            return _bottomRight;
        }
    }

    //  INIT THE GAME GRID (GRAPH)
    gridWidth = width * cellSize;
    gridHeight = height * cellSize;
    initGraph();
    connectCells();


    function ArgumentException(message) {
        this.message = message;
        this.name = "ArgumentException";
    }

    if (width == undefined || height == undefined || cellSize == undefined) {
        throw new ArgumentException("One ore more parameters have not been given to the GameGrid. All three must be given - width, height, cellSize");
    }

    // PRIVATE METHODS
    function initGraph() {

        for (var y = 0; y < gridHeight; y += cellSize) {
            for (var x = 0; x < gridWidth; x += cellSize) {
                var name = x + " " + y;
                gameField.add(name, new GameGridCell(x, y));
            }
        }
    }

    function connectCells() {

        for (var cell in gameField.items) {
            var up = false;
            var down = false;
            var left = false;
            var right = false;

            var currentCell = gameField.items[cell];

            //  CONNECTING NEIGHBOURS
            if (currentCell.originalY() > 0) {
                up = true;
                currentCell.topSet(gameField.getItem(currentCell.originalX() + " " + (currentCell.originalY() - cellSize)));
            }
            if (currentCell.originalX() > 0) {
                left = true;;
                currentCell.leftSet(gameField.getItem((currentCell.originalX() - cellSize) + " " + currentCell.originalY()));
            }
            if (currentCell.originalY() < (gridHeight - cellSize)) {
                down = true;
                currentCell.bottomSet(gameField.getItem(currentCell.originalX() + " " + (currentCell.originalY() + cellSize)));
            }
            if (currentCell.originalX() < (gridWidth - cellSize)) {
                right = true;
                currentCell.rightSet(gameField.getItem((currentCell.originalX() + cellSize) + " " + currentCell.originalY()));
            }

            //  CONNECTING DIAGONALS
            if (up) {
                if (right) {
                    currentCell.topRightSet(gameField.getItem((currentCell.originalX() + cellSize) + " " + (currentCell.originalY() - cellSize)));
                }
                if (left) {
                    currentCell.topLeftSet(gameField.getItem((currentCell.originalX() - cellSize) + " " + (currentCell.originalY() - cellSize)));
                }
            }
            if (down) {
                if (left) {
                    currentCell.bottomLeftSet(gameField.getItem((currentCell.originalX() - cellSize) + " " + (currentCell.originalY() + cellSize)));
                }
                if (right) {
                    currentCell.bottomRightSet(gameField.getItem((currentCell.originalX() + cellSize) + " " + (currentCell.originalY() + cellSize)));
                }
            }
        };
    }

    var fitNinetile = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.topGet() != undefined && gameCell.bottomGet() != undefined && gameCell.leftGet() != undefined &&
            gameCell.rightGet() != undefined && gameCell.topLeftGet() != undefined && gameCell.topRightGet() != undefined &&
            gameCell.bottomLeftGet() != undefined && gameCell.bottomRightGet() != undefined) {

            if (!gameCell.topGet().coveredGet() && !gameCell.bottomGet().coveredGet() && !gameCell.leftGet().coveredGet() &&
            !gameCell.rightGet().coveredGet() && !gameCell.topLeftGet().coveredGet() && !gameCell.topRightGet().coveredGet() &&
            !gameCell.bottomLeftGet().coveredGet() && !gameCell.bottomRightGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitPlus = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.topGet() != undefined && gameCell.bottomGet() != undefined && gameCell.leftGet() != undefined &&
            gameCell.rightGet() != undefined) {

            if (!gameCell.topGet().coveredGet() && !gameCell.bottomGet().coveredGet() && !gameCell.leftGet().coveredGet() &&
            !gameCell.rightGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitHorizontal = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.leftGet() != undefined && gameCell.rightGet() != undefined) {

            if (!gameCell.leftGet().coveredGet() && !gameCell.rightGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitVertical = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.topGet() != undefined && gameCell.bottomGet() != undefined) {

            if (!gameCell.topGet().coveredGet() && !gameCell.bottomGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitAngleUL = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.topGet() != undefined && gameCell.leftGet() != undefined) {

            if (!gameCell.topGet().coveredGet() && !gameCell.leftGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitAngleUR = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.topGet() != undefined && gameCell.rightGet() != undefined) {

            if (!gameCell.topGet().coveredGet() && !gameCell.rightGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitAngleDL = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.bottomGet() != undefined && gameCell.leftGet() != undefined) {

            if (!gameCell.bottomGet().coveredGet() && !gameCell.leftGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var fitAngleDR = function (gameCell) {
        if (!gameCell.coveredGet() && gameCell.bottomGet() != undefined && gameCell.rightGet() != undefined) {

            if (!gameCell.bottomGet().coveredGet() && !gameCell.rightGet().coveredGet()) {
                return true;
            }
        }
        return false;
    }

    var handleNinetile = function (gridCell, adjustValue) {
        var name = gridCell.name();
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.topGet().overlapTilesCountAdjust(adjustValue);
        gridCell.bottomGet().overlapTilesCountAdjust(adjustValue);
        gridCell.leftGet().overlapTilesCountAdjust(adjustValue);
        gridCell.rightGet().overlapTilesCountAdjust(adjustValue);
        gridCell.topLeftGet().overlapTilesCountAdjust(adjustValue);
        gridCell.topRightGet().overlapTilesCountAdjust(adjustValue);
        gridCell.bottomLeftGet().overlapTilesCountAdjust(adjustValue);
        gridCell.bottomRightGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("ninetile", adjustValue);

    }

    var handlePlus = function (gridCell, adjustValue) {
        var name = gridCell.name();
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.topGet().overlapTilesCountAdjust(adjustValue);
        gridCell.bottomGet().overlapTilesCountAdjust(adjustValue);
        gridCell.leftGet().overlapTilesCountAdjust(adjustValue);
        gridCell.rightGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("plus", adjustValue);

    }

    var handleHorizontalLine = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.leftGet().overlapTilesCountAdjust(adjustValue);
        gridCell.rightGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("hline", adjustValue);
    }

    var handleVerticalLine = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.topGet().overlapTilesCountAdjust(adjustValue);
        gridCell.bottomGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("vline", adjustValue);
    }

    var handleAngleUL = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.topGet().overlapTilesCountAdjust(adjustValue);
        gridCell.leftGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("angle-ul", adjustValue);
    }

    var handleAngleUR = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.topGet().overlapTilesCountAdjust(adjustValue);
        gridCell.rightGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("angle-ur", adjustValue);
    }

    var handleAngleDL = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.bottomGet().overlapTilesCountAdjust(adjustValue);
        gridCell.leftGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("angle-dl", adjustValue);
    }

    var handleAngleDR = function (gridCell, adjustValue) {
        gridCell.overlapTilesCountAdjust(adjustValue);
        gridCell.bottomGet().overlapTilesCountAdjust(adjustValue);
        gridCell.rightGet().overlapTilesCountAdjust(adjustValue);

        gridCell.adjustResident("angle-dr", adjustValue);
    }

    // Puts or takes away a given figure at the specified position in the grid.
    var handleFigure = function (figureType, x, y, put) {
        var cell = gameField.getItem(x + " " + y);
        var adjustValue = put ? 1 : -1;
        try {
            switch (figureType) {
                case "ninetile":
                    handleNinetile(cell, adjustValue);
                    break;
                case "plus":
                    handlePlus(cell, adjustValue);
                    break;
                case "hline":
                    handleHorizontalLine(cell, adjustValue);
                    break;
                case "vline":
                    handleVerticalLine(cell, adjustValue);
                    break;
                case "angle-ul":
                    handleAngleUL(cell, adjustValue);
                    break;
                case "angle-ur":
                    handleAngleUR(cell, adjustValue);
                    break;
                case "angle-dl":
                    handleAngleDL(cell, adjustValue);
                    break;
                case "angle-dr":
                    handleAngleDR(cell, adjustValue);
                    break;
                    // TODO add THE REST OF THE FIGURES...
                default:
                    console.error("Unknown Figure: " + figureType);
                    throw new ArgumentException("Unknown Figure!");

            }
        } catch (e) {
            console.error(e.message)
            throw new ArgumentException("Bad coordinates!");
        }

    }

    var randomFromInterval = function (from, to) {
        return Math.floor(Math.random() * (to - from) + from);
    }


    // PUBLIC METHODS


    this.CheckFigureDestinationForOverlaps = function (figureType, x, y) {
        var cellDestination = gameField.getItem(x + " " + y);
        if (cellDestination.coveredGet()) {
            return true;
        }
        var neighbours = [];

        switch (figureType) {
            case "ninetile":
                if (x >= cellSize && y >= cellSize &&
                    x < gridWidth - cellSize && y < gridHeight - cellSize) {
                    neighbours.push(cellDestination.topGet());
                    neighbours.push(cellDestination.bottomGet());
                    neighbours.push(cellDestination.leftGet());
                    neighbours.push(cellDestination.rightGet());
                    neighbours.push(cellDestination.topLeftGet());
                    neighbours.push(cellDestination.topRightGet());
                    neighbours.push(cellDestination.bottomLeftGet());
                    neighbours.push(cellDestination.bottomRightGet());
                    break;
                }
                return true;

            case "plus":
                if (x >= cellSize && y >= cellSize &&
                    x < gridWidth - cellSize && y < gridHeight - cellSize) {
                    neighbours.push(cellDestination.topGet());
                    neighbours.push(cellDestination.bottomGet());
                    neighbours.push(cellDestination.leftGet());
                    neighbours.push(cellDestination.rightGet());
                    break;
                }
                return true;

            case "vline":
                if (y >= cellSize && y < gridHeight - cellSize) {
                    neighbours.push(cellDestination.topGet());
                    neighbours.push(cellDestination.bottomGet());
                    break;
                }
                return true;

            case "hline":
                if (x >= cellSize & x < gridWidth - cellSize) {
                    neighbours.push(cellDestination.leftGet());
                    neighbours.push(cellDestination.rightGet());
                    break;
                }
                return true;

            case "angle-ul":
                if (x >= cellSize && y >= cellSize) {
                    neighbours.push(cellDestination.topGet());
                    neighbours.push(cellDestination.leftGet());
                    break;
                }
                return true;

            case "angle-ur":
                if (y >= cellSize && x < gridWidth - cellSize) {
                    neighbours.push(cellDestination.topGet());
                    neighbours.push(cellDestination.rightGet());
                    break;
                }
                return true;

                break;
            case "angle-dl":
                if (x >= cellSize && y < gridHeight - cellSize) {
                    neighbours.push(cellDestination.bottomGet());
                    neighbours.push(cellDestination.leftGet());
                    break;
                }
                return true;

            case "angle-dr":
                if (x < gridWidth - cellSize && y < gridHeight - cellSize) {
                    neighbours.push(cellDestination.bottomGet());
                    neighbours.push(cellDestination.rightGet());
                    break;
                }
                return true;

            default:
                throw new ArgumentException("Unknown figure");
        }

        for (var i = 0; i < neighbours.length; i++) {
            if (neighbours[i].coveredGet()) {
                return true;
            }
        }

        return false;
    }

    // Returns an array with the names of all the figures in the game
    this.getAllFigureTypes = function () {
        return figureTypes;
    }

    // Returns an object holding all the figures at a given cell position: Object {"figureType" : countAsInt}
    this.getResidentsAt = function (x, y) {
        return gameField.getItem(x + " " + y).getResidents();
    }

    // Returns a random integer between two given integers where the second one is not inclusive
    this.RandomRange = function (from, to) {
        return randomFromInterval(from, to);
    }

    // Returns and array of randomly generated figures, based on given difficulty level (1-3) where 0 is default.
    this.generateFigures = function (difficultyLevel, debug) {
        var arr = new Array();
        // NUMBER OF FIGURES IS BASED ON GIVEN DIFFICULTY LEVEL 1-3; DEFAULT IS 0;
        var count = Math.floor((width * height) / (difficultyLevel == 1 ? 16 : difficultyLevel == 2 ? 12 : difficultyLevel == 3 ? 8 : 18));
        var doubleCellSize = cellSize << 1;
        for (var i = 0; i < count; i++) {
            arr.push({ type: figureTypes[randomFromInterval(0, figureTypes.length)], x: (Math.round(randomFromInterval(cellSize, gridWidth - doubleCellSize) / cellSize) * cellSize), y: (Math.round(randomFromInterval(cellSize, gridHeight - doubleCellSize) / cellSize) * cellSize) });
        }

        if (debug) {
            console.log("Number of figures: " + count);
            for (var i = 0; i < count; i++) {
                var currentFigure = arr[i];
                console.log(currentFigure.type + " x:" + currentFigure.x + " y: " + currentFigure.y);
            }
            console.log("");
        }

        return arr;
    }

    // Adds the given array of figures to the game grid.
    this.populateGameField = function (figures) {
        var len = figures.length;
        for (var i = 0; i < len; i++) {
            var currentFigure = figures[i];
            handleFigure(currentFigure.type, currentFigure.x, currentFigure.y, true);
        }
    }

    // Takes a specified figure from a given location on the grid
    this.takeFigure = function (figureType, x, y) {
        handleFigure(figureType, x, y, false);
    }

    // Puts a specified figure at a given location on the grid
    this.putFigure = function (figureType, x, y) {
        handleFigure(figureType, x, y, true);
    }

    // Checks if there is overlaping of figures. If no overlaping occurs - level is completed;
    this.checkIfLevelCompleted = function () {
        var levelCompleted = true;

        for (var cell in gameField.items) {
            if (gameField.getItem(cell).overlappingTilesGet()) {
                levelCompleted = false;
                break;
            }
        }

        return levelCompleted;
    }

    // Returns the length of the shortest path from a given location to the nearest cell that can fit the given figure
    this.getShortestPathLen = function (figureType, x, y) {
        var len = 0;

        var name = this.findClosestFittingCell(figureType, x, y).split(" ");

        var destinationX = parseInt(name[0]);
        var destinationY = parseInt(name[1]);

        len += Math.abs(x - destinationX);
        len += Math.abs(y - destinationY);
        //alert(x + " " + y + " --> " + destinationX + " " + destinationY + "\nPath len: " + len);

        return len;
    }

    // Performs a BFS search to find the position of the nearest cell that can hold a given figure
    this.findClosestFittingCell = function (figureType, x, y) {
        var queue = new Queue();
        var visited = new Array();
        var rootCell = gameField.getItem(x + " " + y);

        queue.enqueue(rootCell)

        var foundIt = false;
        var name = "";

        rootCell.visitedSet(true);
        visited.push(rootCell);

        while (!foundIt) {
            var currentCell = queue.dequeue();

            switch (figureType) {
                case "ninetile":
                    if (fitNinetile(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "plus":
                    if (fitPlus(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "hline":
                    if (fitHorizontal(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "vline":
                    if (fitVertical(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "angle-ul":
                    if (fitAngleUL(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "angle-ur":
                    if (fitAngleUR(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "angle-dl":
                    if (fitAngleDL(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                case "angle-dr":
                    if (fitAngleDR(currentCell)) {
                        foundIt = true;
                        name = currentCell.name();
                    }
                    break;
                default:
                    console.error("Unknown Figure: " + figureType);
            }

            if (!foundIt) {
                if (currentCell.topGet() && !currentCell.topGet().visitedGet()) {
                    var topCell = currentCell.topGet();
                    queue.enqueue(topCell);
                    topCell.visitedSet(true);
                    visited.push(topCell);
                }
                if (currentCell.bottomGet() && !currentCell.bottomGet().visitedGet()) {
                    var bottomCell = currentCell.bottomGet();
                    queue.enqueue(bottomCell);
                    bottomCell.visitedSet(true);
                    visited.push(bottomCell);
                }
                if (currentCell.leftGet() && !currentCell.leftGet().visitedGet()) {
                    var leftCell = currentCell.leftGet();
                    queue.enqueue(leftCell);
                    leftCell.visitedSet(true);
                    visited.push(leftCell);
                }
                if (currentCell.rightGet() && !currentCell.rightGet().visitedGet()) {
                    var rightCell = currentCell.rightGet();
                    queue.enqueue(rightCell);
                    rightCell.visitedSet(true);
                    visited.push(rightCell);
                }
            }
        }

        // RESETS
        queue.clear();
        var len = visited.length;
        for (var i = 0; i < len; i++) {
            //console.log("cleared visited element");
            visited.pop().visitedSet(false);
        }

        // RETURNS A STRING "x y"
        return name;
    }

    // Debug log in the console for the grid cells connections
    this.displayConnections = function () {
        for (var cell in gameField.items) {
            var current = gameField.items[cell];
            console.log("CELL: " + current.name());
            if (current.topGet()) {
                console.log("Top: " + current.topGet().name());
            }
            if (current.bottomGet()) {
                console.log("Bottom: " + current.bottomGet().name());
            }
            if (current.leftGet()) {
                console.log("Left: " + current.leftGet().name());
            }
            if (current.rightGet()) {
                console.log("Right: " + current.rightGet().name());
            }

            if (current.topLeftGet()) {
                console.log("TopLeft: " + current.topLeftGet().name());
            }
            if (current.topRightGet()) {
                console.log("TopRight: " + current.topRightGet().name());
            }
            if (current.bottomLeftGet()) {
                console.log("BottomLeft: " + current.bottomLeftGet().name());
            }
            if (current.bottomRightGet()) {
                console.log("BottomRight: " + current.bottomRightGet().name());
            }

            console.log("");
        }
    }

    // Debug display current cells overlapping levels.
    this.getOverlaps = function () {
        var output = [];
        var cellsTemp = new Array();

        for (var cell in gameField.items) {
            cellsTemp.push(gameField.getItem(cell));
        }

        var cellIndex = 0;
        var cellsLen = cellsTemp.length;

        for (var c = 0; c < cellsLen; c++) {
            output.push(cellsTemp.shift().overlappingTilesCountGet());
        }

        return output;
    }
}

// Light replica of the .NET Queue class adapted for JavaScript
function Queue() {
    var arr = new Array();

    // adds an element to the queue
    this.enqueue = function (item) {
        arr.push(item);
    }

    // Removes and returns the first element of the queue
    this.dequeue = function () {
        return arr.shift();
    }

    // Deletes all elements of the queue
    this.clear = function () {
        arr = new Array();
    }

    // Returns the current number of elements stored in the queue
    this.count = function () {
        return arr.length;
    }
}

// Light replica of the .NET Dictionary class adapted for JavaScript
function Dictionary() {
    var length = 0;
    this.items = {};

    // Returns the current number of elemnts stored in the dictionary
    this.count = function () {
        return length;
    }

    // adds a new entry in the dictionary
    this.add = function (key, value) {
        //var previous = undefined;
        //if (this.hasItem(key)) {
        //    previous = this.items[key];
        //}
        //else {
        //    length++;
        //}
        //this.items[key] = value;
        //return previous;

        this.items[key] = value;
        length++;
    }

    // Returns an item by its key value
    this.getItem = function (key) {
        //return this.hasItem(key) ? this.items[key] : undefined;
        return this.items[key];
    }

    // Checks for the existance of a key
    this.hasItem = function (key) {
        return this.items.hasOwnProperty(key);
    }

    // Returns all keys as an array
    this.keys = function () {
        var keys = [];
        for (var k in this.items) {
            if (this.hasItem(k)) {
                keys.push(k);
            }
        }
        return keys;
    }

    // Returns all values as an array
    this.values = function () {
        var values = [];
        for (var k in this.items) {
            if (this.hasItem(k)) {
                values.push(this.items[k]);
            }
        }
        return values;
    }

    // Deletes all entries
    this.clear = function () {
        this.items = {}
        length = 0;
    }
}
