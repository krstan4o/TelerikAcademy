/**************************************************
** GAME BULLET CLASS
**************************************************/
var Bullet = function(_startX, _startY, _src, _direction, _damage) {
    var startX = _startX;
    var startY = _startY;
    var src = _src;
    var direction = _direction;
	var damage = _damage;

    var getCurrentX = function () {
        return startX;
    };

    var getCurrentY = function () {
        return startY;
    };

	var getDamage = function (){
        return damage;
    };
	// Draw player
	var draw = function(ctx) {
		if(direction == 6){
			startX += 10;
		} else if (direction == 4){
			startX -= 10;
		} else if (direction == 8){
			startY -= 10;
		} else if (direction == 5){
			startY += 10;
		}

        var dragonPlayer = new Image();
        dragonPlayer.src = "imgs/shooting/" + src;
        dragonPlayer.onload = (function () {
            ctx.drawImage(dragonPlayer, startX, startY);
        })();
    };

	// Define which variables and methods can be accessed
	return {
	    draw: draw,
	    getCurrentX: getCurrentX,
	    getDamage: getDamage,
	    getCurrentY: getCurrentY
	}
};