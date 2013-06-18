/**************************************************
** GAME PLAYER CLASS
**************************************************/
var Player = function (startX, startY, _name, _modelId) {
    var x = startX,
		y = startY,
		id,
		name = _name,
		currentDirection = 5,
        modelId = parseInt(_modelId);

    // Getters and setters
    
    var getName = function () {
        return name;
    };
    
    var getModelId = function () {
        return modelId;
    };

    var getX = function () {
        return x;
    };

    var getY = function () {
        return y;
    };
    
    var setCurrentDirection = function (newDirection) {
        currentDirection = newDirection;
    };
    
    var getCurrentDirection = function () {
        return currentDirection;
    };
    
    var setX = function (newX) {
        x = newX;
    };

    var setY = function (newY) {
        y = newY;
    };

    // Define which variables and methods can be accessed
    return {
        getX: getX,
        getY: getY,
        getName: getName,
        name: name,
        setX: setX,
        setY: setY,
        getModelId: getModelId,
        setCurrentDirection: setCurrentDirection,
        getCurrentDirection: getCurrentDirection,
        id: id
    }
};

// Export the Player class so you can use it in
// other files by using require("Player").Player
exports.Player = Player;