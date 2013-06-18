/**************************************************
** GAME KEYBOARD CLASS
**************************************************/
var Keys = function (up, left, right, down, get, fire) {
    var up = up || false;
	var left = left || false;
	var right = right || false;
	var down = down || false;
	var get = get || false;
    var fire = fire || false;
    
    var onKeyDown = function (e) {
        var that = this,
			c = e.keyCode;
        switch (c) {
            // Controls
            case 37: // Left
                that.left = true;
                localPlayer.setCurrentDirection(4);
                break;
            case 38: // Up
                that.up = true;
                localPlayer.setCurrentDirection(8);
                break;
            case 39: // Right
                that.right = true; // Will take priority over the left key
                localPlayer.setCurrentDirection(6);
                break;
            case 40: // Down
                that.down = true;
                localPlayer.setCurrentDirection(5);
                break;
            case 87: // W button for get
                that.get = true;
                break;
            case 32: // SPACEBAR button for get
                that.fire = true;
                break;
        };
    };

    var onKeyUp = function (e) {
        var that = this,
			c = e.keyCode;
        switch (c) {
            case 37: // Left
                that.left = false;
                break;
            case 38: // Up
                that.up = false;
                break;
            case 39: // Right
                that.right = false;
                break;
            case 40: // Down
                that.down = false;
                break;
            case 87: // W button for get
                that.get = false;
                break;
            case 32: // SPACEBAR button for get
                that.fire = false;
                break;
        };
    };

    return {
        up: up,
        left: left,
        right: right,
        down: down,
        get: get,
        fire:fire,
        onKeyDown: onKeyDown,
        onKeyUp: onKeyUp
    };
};