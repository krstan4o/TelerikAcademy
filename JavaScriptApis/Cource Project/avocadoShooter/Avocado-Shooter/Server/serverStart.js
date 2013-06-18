/**************************************************
** NODE.JS REQUIREMENTS
**************************************************/
var util = require("util"),					// Utility resources (logging, object inspection, etc)
	io = require("socket.io"),				// Socket.IO
	Player = require("./Player").Player;	// Player class


/**************************************************
** GAME VARIABLES
**************************************************/
var socket,		// Socket controller
	players;	// Array of connected players


/**************************************************
** GAME INITIALISATION
**************************************************/
function init() {
    // Create an empty array to store players
    players = [];

    // Set up Socket.IO to listen on port 8000
    socket = io.listen(80);

    // Configure Socket.IO
    socket.configure(function () {
        // Only use WebSockets
        socket.set("transports", ["websocket"]);

        // Restrict log output
        socket.set("log level", 2);
    });

    // Start listening for events
    setEventHandlers();
};


/**************************************************
** GAME EVENT HANDLERS
**************************************************/
var setEventHandlers = function () {
    // Socket.IO
    socket.sockets.on("connection", onSocketConnection);
};

// New socket connection
function onSocketConnection(client) {
    util.log("New player has connected: " + client.id);

    // Listen for client disconnected
    client.on("disconnect", onClientDisconnect);

    // Listen for new player message
    client.on("new player", onNewPlayer);

    // Listen for move player message
    client.on("move player", onMovePlayer);
    
    client.on("player strike", onPlayerStrike);
};

function onPlayerStrike(data){
    switch (data.currentDirection) {
            case 4: //left
            this.broadcast.emit("player strike", { x: data.x - 5, y: data.y + 35, currentDirection: data.currentDirection, damage: data.damage});
            break;
        case 8: //up
            this.broadcast.emit("player strike", { x: data.x + 25, y: data.y - 15, currentDirection: data.currentDirection, damage: data.damage});
            break;
        case 6: //right
            this.broadcast.emit("player strike", { x: data.x + 50, y: data.y + 35, currentDirection: data.currentDirection, damage: data.damage});
            break;
        case 5: //down
            this.broadcast.emit("player strike", { x: data.x + 20, y: data.y + 70, currentDirection: data.currentDirection, damage: data.damage});
            break;
    }
}

// Socket client has disconnected
function onClientDisconnect() {
    util.log("Player has disconnected: " + this.id);

    var removePlayer = playerById(this.id);

    // Player not found
    if (!removePlayer) {
        util.log("Player not found: " + this.id);
        return;
    };

    // Remove player from players array
    players.splice(players.indexOf(removePlayer), 1);

    // Broadcast removed player to connected socket clients
    this.broadcast.emit("remove player", { id: this.id });
};

// New player has joined
function onNewPlayer(data) {
    // Create a new player
    var newPlayer = new Player(data.x, data.y, data.name, data.modelId);
    newPlayer.id = this.id;

    // Broadcast new player to connected socket clients
    this.broadcast.emit("new player", { id: newPlayer.id, x: newPlayer.getX(), y: newPlayer.getY(), name: newPlayer.getName(), modelId: newPlayer.getModelId()});
    // Send existing players to the new player
    var i, existingPlayer;
    for (i = 0; i < players.length; i++) {
        existingPlayer = players[i];
        this.emit("new player", { id: existingPlayer.id, x: existingPlayer.getX(), y: existingPlayer.getY(), name: existingPlayer.getName(), modelId: existingPlayer.getModelId()});
        util.log(existingPlayer.getModelId());
    };

    // Add new player to the players array
    players.push(newPlayer);
};

// Player has moved
function onMovePlayer(data) {
    // Find player in array
    var movePlayer = playerById(this.id);

    // Player not found
    if (!movePlayer) {
        util.log("Player not found: " + this.id);
        return;
    };

    // Update player position
    movePlayer.setX(data.x);
    movePlayer.setY(data.y);
    // Broadcast updated position to connected socket clients
    this.broadcast.emit("move player", { id: movePlayer.id, x: movePlayer.getX(), y: movePlayer.getY(),  currentDirection: data.currentDirection});
};


/**************************************************
** GAME HELPER FUNCTIONS
**************************************************/
// Find player by ID
function playerById(id) {
    var i;
    for (i = 0; i < players.length; i++) {
        if (players[i].id == id)
            return players[i];
    };

    return false;
};


/**************************************************
** RUN THE GAME
**************************************************/
init();