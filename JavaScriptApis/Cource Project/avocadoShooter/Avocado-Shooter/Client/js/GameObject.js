/**************************************************
** GAME OBJECT CLASS
**************************************************/
var GameObject = function (_x, _y, _src, _type) {
    var x = _x;
    var y = _y;
    var src = _src;
    var type = _type;
    var visible = true;

    // Getters and setters
    var getX = function () {
        return x;
    };

    var getY = function () {
        return y;
    };

    var getType = function () {
        return type;
    };

    // Update player position
    var update = function (keys) {

    };

    var visibleOf = function () {
        if (visible == true) {
            if (type == "speed") {
                localPlayer.improveSpeed(2);
            }
            else if (type == "weapon") {
                localPlayer.improveDamage(1);
                updatePlayerDamage(localPlayer.getDamage());
            }
            else if (type == "hp") {
                localPlayer.improveHP(5);
                updatePlayerHP(localPlayer.getHP());
            }
        }
        visible = false;
    };
    // Draw player
    var draw = function (ctx) {
        if (visible == true) {

            var dragonPlayer = new Image();
            dragonPlayer.src = "imgs/objects/" + src;
            dragonPlayer.onload = (function () {
                ctx.drawImage(dragonPlayer, x, y)
            })();
        }
    };

    // Define which variables and methods can be accessed
    return {
        getX: getX,
        getY: getY,
        getType: getType,
        visibleOf: visibleOf,
        update: update,
        draw: draw
    }
};