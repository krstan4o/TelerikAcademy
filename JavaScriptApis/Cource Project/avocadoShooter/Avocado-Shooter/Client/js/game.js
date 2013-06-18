/**************************************************
** GAME VARIABLES
**************************************************/
var canvas;		     // Canvas DOM element
var canvasWidth;
var canvasHeigth;
var ctx;         	// Canvas rendering context
var keys;			// Keyboard input
var localPlayer;
var remotePlayers;
var gameObjects;	// Game bonus objects
var socket;         // Socket connection
var bullets;

/**************************************************
** GAME INITIALISATION
**************************************************/
function init() {
    // Declare the canvas and rendering context
    canvas = $("#gameCanvas")[0];
    ctx = canvas.getContext("2d");

    // Maximise the canvas
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    canvasWidth = canvas.width;
    canvasHeight = canvas.height;

    // Initialise keyboard controls
    keys = new Keys();
    bullets = [];

    // Calculate a random start position for the local player
    // The minus 5 (half a player size) stops the player being
    // placed right on the egde of the screen
    var startX = getRandomX();
    var startY = getRandomY();

    // Initialise the local player
    var localPlayerName = prompt("Enter your name, Master:");
    var startHP = 10;
    var startDamage = 1;

    localPlayer = new Player(startX, startY, localPlayerName,
        startHP, startDamage, (Math.random() * 3) + 1);

    updatePlayerName(localPlayer.getName());
    updateOnlinePlayers(1);
    showDeads();
    updatePlayerDamage(localPlayer.getDamage());
    // Initialise socket connection
    socket = io.connect("http://localhost/", { port: 80, transports: ["websocket"] });

    // Initialise remote players array
    remotePlayers = [];
    gameObjects = [];

    gameObjects.push(new GameObject(getRandomX(), getRandomY(), "boots.png", "speed"));
    gameObjects.push(new GameObject(getRandomX(), getRandomY(), "bow.png", "weapon"));
    gameObjects.push(new GameObject(getRandomX(), getRandomY(), "hp.png", "hp"));

    // Start listening for events
    setEventHandlers();
};

function getRandomX() {
    return Math.round(Math.random() * (canvasWidth - 45) - 30);
}

function getRandomY() {
    return Math.round(Math.random() * (canvasHeight - 75));
}

/**************************************************
** GAME EVENT HANDLERS
**************************************************/
var setEventHandlers = function () {
    // Keyboard
    $(window).on("keydown", onKeydown);
    $(window).on("keyup", onKeyup);

    // Window resize
    $(window).on("resize", onResize);

    // Socket connection successful
    socket.on("connect", onSocketConnected);

    // Socket disconnection
    socket.on("disconnect", onSocketDisconnect);

    // New player message received
    socket.on("new player", onNewPlayer);

    // Player move message received
    socket.on("move player", onMovePlayer);

    // Player removed message received
    socket.on("remove player", onRemovePlayer);

    socket.on("player strike", onPlayerStrike);
};

// Keyboard key down
function onKeydown(e) {
    if (localPlayer) {
        keys.onKeyDown(e);
    }
};

// Keyboard key up
function onKeyup(e) {
    if (localPlayer) {
        keys.onKeyUp(e);
    }
};

// Browser window resize
function onResize(e) {
    // Maximise the canvas
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
};

// Socket connected
function onSocketConnected() {
    console.log("Connected to socket server");

    // Send local player data to the game server
    socket.emit("new player", {
        x: localPlayer.getX(), y: localPlayer.getY(),
        name: localPlayer.getName(), modelId: localPlayer.getModelId()
    });
};

// Socket disconnected
function onSocketDisconnect() {
    console.log("Disconnected from socket server");
};

// New player
function onNewPlayer(data) {
    console.log("New player connected: " + data.name);

    // Initialise the new player
    var newPlayer = new Player(data.x, data.y, data.name, 0, 0, data.modelId);
    console.log(data.modelId);
    newPlayer.id = data.id;
    newPlayer.currentDirection = data.currentDirection;

    // Add new player to the remote players array
    remotePlayers.push(newPlayer);
    updateOnlinePlayers(remotePlayers.length + 1);
};

function onPlayerStrike(data) {
    var item = "";
    if (localPlayer.getModelId() == 1) {
        item = "fireball.png";
    } else if (localPlayer.getModelId() == 2) {
        item = "bone.png";
    } else if (localPlayer.getModelId() == 3) {
        item = "baseball.png";
    }

    bullets.push(new Bullet(data.x, data.y, item, data.currentDirection, data.damage));
}
// Move player
function onMovePlayer(data) {
    var movePlayer = playerById(data.id);

    // Player not found
    if (!movePlayer) {
        console.log("Player not found: " + data.id);
        return;
    }

    // Update player position
    movePlayer.setCurrentDirection(data.currentDirection)
    movePlayer.setX(data.x);
    movePlayer.setY(data.y);
};

// Remove player
function onRemovePlayer(data) {
    var removePlayer = playerById(data.id);

    // Player not found
    if (!removePlayer) {
        console.log("Player not found: " + data.id);
        return;
    }

    // Remove player from array
    remotePlayers.splice(remotePlayers.indexOf(removePlayer), 1);
    updateOnlinePlayers(remotePlayers.length + 1);
};


/**************************************************
** GAME ANIMATION LOOP
**************************************************/
function animate() {
    update();
    draw();

    // Request a new animation frame using Paul Irish's shim
    window.requestAnimFrame(animate);
};


/**************************************************
** GAME UPDATE
**************************************************/
function update() {
    // Update local player and check for change
    if (localPlayer.update(keys)) {
        // Send local player data to the game server
        socket.emit("move player", {
            x: localPlayer.getX(), y: localPlayer.getY(),
            currentDirection: localPlayer.getCurrentDirection()
        });
    }

    if (localPlayer.fire(keys)) {
        socket.emit("player strike", {
            x: localPlayer.getX(), y: localPlayer.getY(),
            currentDirection: localPlayer.getCurrentDirection(), damage: localPlayer.getDamage()
        });
    }

    collision();
};


/**************************************************
** GAME DRAW
**************************************************/
function draw() {

    // Wipe the canvas clean
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Draw the local player
    localPlayer.draw(ctx);

    for (var i = 0; i < gameObjects.length; i++) {
        gameObjects[i].draw(ctx);
    };

    // Draw the remote players
    for (var i = 0; i < remotePlayers.length; i++) {
        remotePlayers[i].draw(ctx);
    };

    for (var i = 0; i < bullets.length; i++) {

        if (bullets[i].getCurrentX() < 0 || bullets[i].getCurrentX() > canvasWidth ||
		      bullets[i].getCurrentY() < 0 || bullets[i].getCurrentY() > canvasHeigth) {
            bullets.splice(bullets.indexOf(bullets[i]), 1);
            continue;
        }

        bullets[i].draw(ctx);
    };
};

/**************************************************
** GAME COLLISION
**************************************************/
function collision() {
    sessionStorage.clickcount = 0;
    for (var i = 0; i < bullets.length; i++) {
        if ((bullets[i].getCurrentX() > localPlayer.getX() && bullets[i].getCurrentX() < localPlayer.getX() + 11)
            && bullets[i].getCurrentY() > localPlayer.getY() && bullets[i].getCurrentY() < localPlayer.getY() + 40) {
            //Get HP from the player
            localPlayer.improveHP(-1 * (bullets[i].getDamage()));
            updatePlayerHP(localPlayer.getHP());
            if (localPlayer.getHP() <= 0) {
                if (typeof (Storage) !== "undefined") {
                    if (sessionStorage.clickcount) {
                        sessionStorage.clickcount = Number(sessionStorage.clickcount) + 1;
                    }
                    else {
                        sessionStorage.clickcount = 1;
                    }
                }
                window.location.replace("\dead.html");
            }
        }
    };
};

/**************************************************
** GAME AUDIO
**************************************************/
function gameAudio() {
    var sound = new Audio("audio/gameMusic.mp3"); // buffers automatically when created
    sound.play();
};

/**************************************************
** GAME HELPER FUNCTIONS
**************************************************/
// Find player by ID
function playerById(id) {
    var i;
    for (i = 0; i < remotePlayers.length; i++) {
        if (remotePlayers[i].id == id)
            return remotePlayers[i];
    };

    return false;
};

function updatePlayerName(name) {
    document.getElementById('name').innerHTML = "Name: " + name;
}

function updateOnlinePlayers(number) {
    document.getElementById('playersOnline').innerHTML = "Players online: " + number;
}

function updatePlayerHP(hp) {
    document.getElementById('hp').innerHTML = "HP: " + hp;
}

function updatePlayerDamage(damage) {
    document.getElementById('damage').innerHTML = "Damage: " + damage;
}

function showDeads() {
    document.getElementById('deads').innerHTML = "Deads: " + sessionStorage.clickcount;
}