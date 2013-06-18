/**************************************************
** GAME PLAYER CLASS
**************************************************/
var Player = function (startX, startY, _name, _HP, _damage, _modelId) {

    var x = startX;
    var y = startY;
    var name = _name;
    var hp = _HP;
    var damage = _damage;
    var modelId = parseInt(_modelId);

    var id;
    var moveAmount = 4;
    var currentDirection = 4;
    var state = 1;
    var dragonId = 1;
    var trollId = 2;
    var dogId = 3;
    var sound = new Audio('audio/laser.mp3');
    var shootingItems = ["fireball.png", "baseball.png", "bone.png"];
    var fireCount = 0;

    // Getters and setters
    var getX = function () {
        return x;
    };

    var getY = function () {
        return y;
    };

    var getName = function () {
        return name;
    };

    var getHP = function () {
        return hp;
    };

    var getDamage = function () {
        return damage;
    };

    var getModelId = function () {
        return modelId;
    };

    var getCurrentDirection = function () {
        return currentDirection;
    };

    var setCurrentDirection = function (newDirection) {
        currentDirection = newDirection;
    };

    var improveSpeed = function (newSpeed) {
        moveAmount += newSpeed;
    };

    var setX = function (newX) {
        x = newX;
    };

    var setY = function (newY) {
        y = newY;
    };

    var improveDamage = function (newDamage) {
        damage += newDamage;
    };

    var improveHP = function (newHP) {
        hp += newHP;
    };

    // Update player position
    var update = function (keys) {
        // Previous position
        var prevX = x,
            prevY = y;

        // Up key takes priority over down
        if (keys.up && y > 30) {
            y -= moveAmount;
        } else if (keys.down && y < canvas.height - 75) {
            y += moveAmount;
        }

        // Left key takes priority over right
        if (keys.left && x > 0) {
            x -= moveAmount;
        } else if (keys.right && x < canvas.width - 75) {
            x += moveAmount;
        }

        if (keys.get) {
            for (var i = 0; i < gameObjects.length; i++) {
                if (gameObjects[i].getX() < x && gameObjects[i].getX() + 30 > x &&
                      gameObjects[i].getY() < y && gameObjects[i].getY() + 30 > y) {
                    gameObjects[i].visibleOf();
                }
            };
        }

        return (prevX != x || prevY != y) ? true : false;
    };

    function bulletType() {
        switch (currentDirection) {
            case 4: //left
                var bulletLocation = "";
                if (modelId == dragonId) {
                    bulletLocation = shootingItems[0];
                } else if (modelId == trollId) {
                    bulletLocation = shootingItems[1];
                } else {
                    bulletLocation = shootingItems[2];
                }

                bullets.push(new Bullet(x - 5, y + 35, bulletLocation, currentDirection));
                break;
            case 8: //up
                var bulletLocation = "";
                if (modelId == dragonId) {
                    bulletLocation = shootingItems[0];
                } else if (modelId == trollId) {
                    bulletLocation = shootingItems[1];
                } else {
                    bulletLocation = shootingItems[2];
                }

                bullets.push(new Bullet(x + 25, y - 15, bulletLocation, currentDirection));
                break;
            case 6: //right
                var bulletLocation = "";
                if (modelId == dragonId) {
                    bulletLocation = shootingItems[0];
                } else if (modelId == trollId) {
                    bulletLocation = shootingItems[1];
                } else {
                    bulletLocation = shootingItems[2];
                }

                bullets.push(new Bullet(x + 50, y + 35, bulletLocation, currentDirection));
                break;
            case 5: //down
                var bulletLocation = "";
                if (modelId == dragonId) {
                    bulletLocation = shootingItems[0];
                } else if (modelId == trollId) {
                    bulletLocation = shootingItems[1];
                } else {
                    bulletLocation = shootingItems[2];
                }

                bullets.push(new Bullet(x + 20, y + 70, bulletLocation, currentDirection));
                break;
        }
    };

    //Fire
    var fire = function (keys) {
        if (keys.fire == true) {
            fireCount++;
            if (fireCount % 10 == 0) {
                bulletType(currentDirection);
                sound.play();
                return true;
            }
        }

        return false;
    }

    // Draw player
    var draw = function (ctx) {
        function loadPlayer(ctx) {
            ctx.font = "bold 14px Arial";
            ctx.textAlign = "center";
            ctx.fillText(name, x + 35, y - 10);
            ctx.drawImage(dragonPlayer, x, y);
        }

        function drawPlayerState() {
            dragonPlayer.onload = (function () {
                loadPlayer(ctx);
            })();
        };

        var dragonPlayer = new Image();
        state++;
        if (currentDirection == 5) {
            if (keys.down) {
                dragonPlayer.src = "imgs/0" + modelId + "-front-" + parseInt((state % 30) / 10) + ".png";
            } else {
                dragonPlayer.src = "imgs/0" + modelId + "-front-0.png";
            }

            drawPlayerState();
        }
        else if (currentDirection == 4) {
            if (keys.left) {
                dragonPlayer.src = "imgs/0" + modelId + "-left-" + parseInt((state % 30) / 10) + ".png";
            } else {
                dragonPlayer.src = "imgs/0" + modelId + "-left-0.png";
            }

            drawPlayerState();
        } else if (currentDirection == 6) {
            if (keys.right) {
                dragonPlayer.src = "imgs/0" + modelId + "-right-" + parseInt((state % 30) / 10) + ".png";
            } else {
                dragonPlayer.src = "imgs/0" + modelId + "-right-0.png";
            }

            drawPlayerState();
        } else if (currentDirection == 8) {
            if (keys.up) {
                dragonPlayer.src = "imgs/0" + modelId + "-back-" + parseInt((state % 30) / 10) + ".png";
            } else {
                dragonPlayer.src = "imgs/0" + modelId + "-back-0.png";
            }

            drawPlayerState();
        }
    };

    // Define which variables and methods can be accessed
    return {
        getX: getX,
        getY: getY,
        getName: getName,
        getHP: getHP,
        getDamage: getDamage,
        getCurrentDirection: getCurrentDirection,
        getModelId: getModelId,
        improveSpeed: improveSpeed,
        improveDamage: improveDamage,
        improveHP: improveHP,
        setX: setX,
        setY: setY,
        setCurrentDirection: setCurrentDirection,
        update: update,
        fire: fire,
        draw: draw
    }
};