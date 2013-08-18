/// <reference path="jquery-2.0.2.min.js" />

var ui = (function () {
    function buildLoginForm() {
        var html =
        '<div id="login-form-holder">' +
        '<form>' +
        '<div id="login-form">' +
        '<label for="tb-login-username">Username: </label>' +
        '<input type="text" id="tb-login-username"><br />' +
        '<label for="tb-login-password">Password: </label>' +
        '<input type="password" id="tb-login-password"><br />' +
        '<button id="btn-login" class="button">Login</button>' +
        '</div>' +
        '<div id="register-form" style="display: none">' +
        '<label for="tb-register-username">Username: </label>' +
        '<input type="text" id="tb-register-username"><br />' +
        '<label for="tb-register-nickname">Nickname: </label>' +
        '<input type="text" id="tb-register-nickname"><br />' +
        '<label for="tb-register-password">Password: </label>' +
        '<input type="password" id="tb-register-password"><br />' +
        '<button id="btn-register" class="button">Register</button>' +
        '</div>' +
        '<a href="#" id="btn-show-login" class="button selected">Login</a>' +
        '<a href="#" id="btn-show-register" class="button">Register</a>' +
        '</form>' +
        '<div id="error-messages"></div>' +
        '</div>';
        return html;
    }

    function buildGameUI(nickname) {
        var html = '<span id="user-nickname">' +
                   nickname +
                   '</span>' +
                   '<button id="btn-logout">Logout</button><br/>' +
                   '<div id="create-game-holder">' +
                   'Title: <input type="text" id="tb-create-title" />' +
                   'Password: <input type="password" id="tb-create-pass" />' + 
                   '<button id="btn-create-game">Create</button>' +
                   '</div>' +
                   '<div id="open-games-container">' +
                   '<h2>Open</h2>' +
                   '<div id="open-games"></div>' +
                   '</div>' +
                   '<div id="active-games-container">' +
                   '<h2>Active</h2>' +
                   '<div id="active-games"></div>' +
                   '</div>' +
                   '<div id="game-holder">' +
                   '</div>' +
                   '<div id="messages-holder">' +
                   '</div>' + '<div id="unit-moves">';
        return html;
    }

    function buildOpenGamesList(games) {
        var list = '<ul class="game-list open-games">';
        for (var i = 0; i < games.length; i++) {
            var game = games[i];
            list +=
            '<li data-game-id="' + game.id + '">' +
            '<a href="#" >' +
            $("<div />").html(game.title).text() +
            '</a>' +
            '<span> by ' +
            game.creator +
            '</span>' +
            '</li>';
        }
        list += "</ul>";
        return list;
    }

    function buildActiveGamesList(games) {
        var gamesList = Array.prototype.slice.call(games, 0);
        gamesList.sort(function (g1, g2) {
            if (g1.status == g2.status) {
                return g1.title > g2.title;
            }
            else {
                if (g1.status == "in-progress") {
                    return -1;
                }
            }
            return 1;
        });

        var list = '<ul class="game-list active-games">';
        for (var i = 0; i < gamesList.length; i++) {
            var game = gamesList[i];
            list +=
            '<li class="game-status-' + game.status + '" data-game-id="' + game.id + '" data-creator="' + game.creatorNickname + '">' +
            '<a href="#" class="btn-active-game">' +
            $("<div />").html(game.title).text() +
            '</a>' +
            '<span> by ' +
            game.creatorNickname +
            '</span>' +
            '</li>';
        }
        list += "</ul>";
        return list;
    }

    function buildGameState(gameState) {
        var playerInTurn = gameState.inTurn;

        var redPlayer = gameState.red; //all data for the red player - nick, units
        var redPlayerNick = redPlayer.nickname;
        var redPlayerUnits = redPlayer.units;
        for (var i = 0; i < redPlayerUnits.length; i++) {
            var typeOfUnit = redPlayerUnits[i].type;
            var redUnitId = redPlayerUnits[i].id;

            var position = redPlayerUnits[i].position;
            var positionX = position.x;
            var positionY = position.y;

            var attack = redPlayerUnits[i].attack;
            var armor = redPlayerUnits[i].armor;

            var id = "#" + positionX + positionY;
            var td = $(id);
            td.addClass("red");
            if (typeOfUnit == "warrior") {
                td.html('<div id=' + redUnitId + '>W</div>' + "<div>a:" + attack + '<br/>d:' + armor + '</div>');
            }
            else {
                td.html('<div id=' + redUnitId + '>R</div>' + "<div>a:" + attack + '<br/>d:' + armor + '</div>');
            }
            td.data("attack", attack);
            td.data("armor", armor);
        }
        var bluePlayer = gameState.blue; //all data for the red player - nick, units
        var bluePlayerNick = bluePlayer.nickname;

        var bluePlayerUnits = bluePlayer.units;
        for (var i = 0; i < bluePlayerUnits.length; i++) {
            var typeOfUnit = bluePlayerUnits[i].type;
            var blueUnitId = bluePlayerUnits[i].id;

            var position = bluePlayerUnits[i].position;
            positionX = position.x;
            positionY = position.y;

            attack = bluePlayerUnits[i].attack;
            armor = bluePlayerUnits[i].armor;

            id = "#" + positionX + positionY;
            td = $(id);
            td.addClass("blue");
            if (typeOfUnit == "warrior") {
                td.html('<div id=' + blueUnitId + '>W</div>' + "<div>a:" + attack + '<br/>d:' + armor + '</div>');
                
                td.addClass('wtfff');
            }
            else {
                td.html('<div id=' + blueUnitId + '>R</div>' + "<div>a:" + attack + '<br/>d:' + armor + '</div>');
            }
        }
        $("#red-player").text('RedPlayer: ' + redPlayerNick);
        $("#blue-player").text('BluePlayer: ' + bluePlayerNick);
        $("#inTurn").text("Player's Turn: " + playerInTurn);
    }

    function buildGameField(gameState) {
        var html =
        ' <table>' +
        '      <tr id="0">' +
        '          <td id="00"></td>' +
        '          <td id="01"></td>' +
        '          <td id="02"></td>' +
        '          <td id="03"></td>' +
        '          <td id="04"></td>' +
        '          <td id="05"></td>' +
        '          <td id="06"></td>' +
        '          <td id="07"></td>' +
        '          <td id="08"></td>' +
        '      </tr>' +
        '       <tr id="1">' +
        '          <td id="10"></td>' +
        '          <td id="11"></td>' +
        '          <td id="12"></td>' +
        '          <td id="13"></td>' +
        '          <td id="14"></td>' +
        '          <td id="15"></td>' +
        '          <td id="16"></td>' +
        '          <td id="17"></td>' +
        '          <td id="18"></td>' +
        '      </tr>' +
        '       <tr id="2">' +
        '          <td id="20"></td>' +
        '          <td id="21"></td>' +
        '          <td id="22"></td>' +
        '          <td id="23"></td>' +
        '          <td id="24"></td>' +
        '          <td id="25"></td>' +
        '          <td id="26"></td>' +
        '          <td id="27"></td>' +
        '          <td id="28"></td>' +
        '      </tr>' +
        '       <tr id="3">' +
        '          <td id="30"></td>' +
        '          <td id="31"></td>' +
        '          <td id="32"></td>' +
        '          <td id="33"></td>' +
        '          <td id="34"></td>' +
        '          <td id="35"></td>' +
        '          <td id="36"></td>' +
        '          <td id="37"></td>' +
        '          <td id="38"></td>' +
        '      </tr>' +
        '       <tr id="4">' +
        '          <td id="40"></td>' +
        '          <td id="41"></td>' +
        '          <td id="42"></td>' +
        '          <td id="43"></td>' +
        '          <td id="44"></td>' +
        '          <td id="45"></td>' +
        '          <td id="46"></td>' +
        '          <td id="47"></td>' +
        '          <td id="48"></td>' +
        '      </tr>' +
        '       <tr id="5">' +
        '          <td id="50"></td>' +
        '          <td id="51"></td>' +
        '          <td id="52"></td>' +
        '          <td id="53"></td>' +
        '          <td id="54"></td>' +
        '          <td id="55"></td>' +
        '          <td id="56"></td>' +
        '          <td id="57"></td>' +
        '          <td id="58"></td>' +
        '      </tr>' +
        '       <tr id="6">' +
        '          <td id="60"></td>' +
        '          <td id="61"></td>' +
        '          <td id="62"></td>' +
        '          <td id="63"></td>' +
        '          <td id="64"></td>' +
        '          <td id="65"></td>' +
        '          <td id="66"></td>' +
        '          <td id="67"></td>' +
        '          <td id="68"></td>' +
        '      </tr>' +
        '       <tr id="7">' +
        '          <td id="70"></td>' +
        '          <td id="71"></td>' +
        '          <td id="72"></td>' +
        '          <td id="73"></td>' +
        '          <td id="74"></td>' +
        '          <td id="75"></td>' +
        '          <td id="76"></td>' +
        '          <td id="77"></td>' +
        '          <td id="78"></td>' +
        '      </tr>' +
        '       <tr id="8">' +
        '          <td id="80"></td>' +
        '          <td id="81"></td>' +
        '          <td id="82"></td>' +
        '          <td id="83"></td>' +
        '          <td id="84"></td>' +
        '          <td id="85"></td>' +
        '          <td id="86"></td>' +
        '          <td id="87"></td>' +
        '          <td id="88"></td>' +
        '      </tr>' +
        '  </table><br/>' + '<div id="blue-player" style="display:inline-block;margin-right:50px;"></div>' + '<div id="red-player" style="display:inline-block"></div>' +
        '<br/><div id="inTurn"></div>';
       
        return html;
    }
    function generateUnitTasks() {
        var html =
            
        '<button id="attack">Attack</button>' +
        '<button id="defend">Defend</button>' +
        '<button id="move">Move</button>' +
        '</div>';
        return html;
    }
    function buildMessagesList(messages) {
        var list = '<ul class="messages-list">';
        var msg;
        for (var i = 0; i < messages.length; i += 1) {
            msg = messages[i];
            var item =
            '<li>' +
            '<a href="#" class="message-state-' + msg.state + '">' +
            msg.text +
            '</a>' +
            '</li>';
            list += item;
        }
        list += '</ul>';
        return list;
    }

    return {
        gameUI: buildGameUI,
        openGamesList: buildOpenGamesList,
        loginForm: buildLoginForm,
        activeGamesList: buildActiveGamesList,
        gameField:buildGameField,
        gameState: buildGameState,
        messagesList: buildMessagesList,
        generateTasks:generateUnitTasks
    }
}());