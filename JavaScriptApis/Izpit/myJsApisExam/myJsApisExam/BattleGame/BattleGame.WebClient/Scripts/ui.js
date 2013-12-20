/// <reference path="jquery-2.0.3.js" />
var ui = (function () {
    function buildGameUI(nickname) {
        var html = '<img id="logo" src="images/icon.jpg" /><h1>BattleGame</h1><span>Welcome, ' + nickname + '</span>&nbsp&nbsp<button id="btn-logout">Logout</button><a id="scores-btn" href="javascript:void(0)">Scores</a><br /><hr /> <br> ' +
            '<div id="battle-holder"></div><aside><h3>Open Games</h3><ul id="open-games"></ul><h3>Active Games</h3><ul id="active-games"></ul></aside><div id="create-game-wrapper"><strong>Create New Game</strong><br>Game Title: <input type="text" id="new-game-title" /><br />Game Password: <input type="password" id="new-game-password" /><br /> <button type="button" id="new-game-btn">Create</button></div><div id="unit-info"></div><div id="error-container"></div><div id="messages-holder"></div>';
        return html;
    }
    function buildLoginFormUI() {
        var html = '<img id="logo" src="images/icon.jpg" /><h1>Battle Game</h1><hr /><div id="login-form"><h3>Login</h3><form>Username: <input type="text" id="tb-login-username" /><br />' +
                'Password: <input type="password" id="tb-login-password" /><br /><button type="button" id="btn-login">Login</button> or <a href="javascript:void(0)" id="btn-show-register">Register</a></form><div id="error-container"></div></div>';
        return html;
    }
    function buildRegisterFormUI() {
        var html = '<img id="logo" src="images/icon.jpg" /><h1>Battle Game</h1><hr /><div id="login-form"><h3>Register</h3><form>Username: <input type="text" id="tb-register-username" /><br />Nickname: <input type="text" id="tb-register-nickname" /><br />' +
                'Password: <input type="password" id="tb-register-password" /><br /><button type="button" id="btn-register">Register</button> or <a href="javascript:void(0)" id="btn-show-login">Login</a></form><div id="error-container"></div></div>';
        return html;
    }
    function buildOpenGamesList(data) {
        var html = '';
        for (var i = 0; i < data.length; i++) {
            html += '<li class="open-game" data-game-id="' + data[i].id + '">' + data[i].title + '</li>';
        }
        return html;
    }
    function buildActiveGamesList(data) {
        var html = '';
        for (var i = 0; i < data.length; i++) {
            html += '<li class="active-game" data-game-id="' + data[i].id +
                '" data-creator="'+ data[i].creator + '" data-status="'+ data[i].status +'">' + data[i].title + '</li>';
        }
        return html;
    }
    function buildBattleField(data) {
        var html = '<strong>Game: ' + data.title + '</strong><br>';
        html += '<strong>Round: ' + data.turn + '</strong><br>';
        html += '<strong>Turn: ' + data.inTurn + '</strong><br>';
        html += '<table id="field" border="1">';
        for (var y = 0; y < 9; y++) {
            html += '<tr>';
            for (var x = 0; x < 9; x++) {
                html += '<td data-x="' + x + '" data-y="' + y + '" >';
                html += '</td>';
            }
            html += '</tr>';
        }
        html += '</table>';
        return html;
    }
    function populateGameFieldData(gameField, data) {
        $.fn.filterByData = function (x, xval, y, yval) {
            return this.filter(
                function () { return $(this).data(x) == xval && $(this).data(y) == yval; }
            );
        };

        var redPlayerUnits = data.red.units;
        var bluePlayerUnits = data.blue.units;
        for (var i = 0; i < redPlayerUnits.length; i++) {
            var currentUnit = redPlayerUnits[i];
            var position = currentUnit.position;
            var type = currentUnit.type;
            var unitId = currentUnit.id;
            var attack = currentUnit.attack;
            var armor = currentUnit.armor;
            var hitPoints = currentUnit.hitPoints;
            var owner = currentUnit.owner;
            var mode = currentUnit.mode;
            var range = currentUnit.range;
            var htmlElement = $("td").filterByData('x', position.x, 'y', position.y);
            debugger;
            
            htmlElement.attr('Id', unitId);
            htmlElement.attr('Attack', attack);
            htmlElement.attr('armor', armor);
            htmlElement.attr('hit-points', hitPoints);
            htmlElement.addClass(owner);
            htmlElement.addClass(mode);
            htmlElement.addClass(type);
            htmlElement.append('<span class="mode"></span>');
        }
        for (var i = 0; i < bluePlayerUnits.length; i++) {
            var currentUnit = bluePlayerUnits[i];
            var position = currentUnit.position;
            var type = currentUnit.type;
            var unitId = currentUnit.id;
            var attack = currentUnit.attack;
            var armor = currentUnit.armor;
            var hitPoints = currentUnit.hitPoints;
            var owner = currentUnit.owner;
            var mode = currentUnit.mode;
            var range = currentUnit.range;

            var htmlElement = $("td").filterByData('x', position.x, 'y', position.y);        
            htmlElement.attr('id', unitId);
            htmlElement.attr('attack', attack);
            htmlElement.attr('armor', armor);
            htmlElement.attr('range', range);
            htmlElement.attr('hit-points', hitPoints);
            htmlElement.addClass(owner);
            htmlElement.addClass(mode);
            htmlElement.addClass(type);
            htmlElement.append('<span class="mode"></span>');
        }
        //debugger;
        //var wtf = 2;
    }
    function buildUnitInfo(unitElement) {
        var html = '<p> HitPoints: ' + unitElement.attr('hit-points') + '</p>';
        html += '<p> Attack: ' + unitElement.attr('attack') + '</p>';
        html += '<p> Armor: ' + unitElement.attr('armor') + '</p>';
        html += '<button id="attack-btn" data-id="' + unitElement.attr('id') + '" type="button">Attack</button>';
        html += '<button id="move-btn" data-id="' + unitElement.attr('id') + '" type="button">Move</button>';
        html += '<button id="defend-btn" data-id="' + unitElement.attr('id') + '" type="button">Defend</button><br />';
        return html;
    }
    function buildScores(element, data) {
            element.html('<a href="javascript:void(0)" id="close-scores">[x]Close</a>');
            element.append('<table id="scores-holder"></table>');
           
            var table = $('#scores-holder');
        
            var html = '<tr style="display: none;"><th>Name</th><th>Score</th></tr>';
            for (var i = 0; i < data.length;i++) {
           
                html += '<tr style="display: none;">';
                html+= '<td>' + data[i].nickname + '</td>';
                html += '<td>' + data[i].score + '</td>';
                html+= '</tr>';
            }
            table.append(html);
            var handler;
            var counter = 2000;
            $('#scores-holder tr').each(function (index, el) {
                var el = $(el);
               
                handler = setTimeout(function () {
                    el.fadeIn(1000, function () { });
                }, counter);
                counter += 700;
               
            });
            var stopHandler = setTimeout(function () {
                clearTimeout(handler);
                handler = 0;
            }, 20000)
            
            clearTimeout(stopHandler);
            stopHandler = 0;
        }
    function buildMessages(data) {
        var html = "";
        $.fn.filterByData = function (x, xval) {
            return this.filter(
                function () { return $(this).data(x) == xval; }
            );
        };
        for (var i = 0; i < data.length; i++) {
           
            if (data[i].state == "unread") {
                var gameId = data[i].gameId;
                $('.active-game').filterByData('game-id', gameId).addClass('unread');
                html += '<p style="color:orange;">' + data[i].text + '</p>';
            } else {
                html += '<p>' + data[i].text + '</p>';
            }
        }
        return html;
    }

    return {
        buildGameUI: buildGameUI,
        buildLoginFormUI: buildLoginFormUI,
        buildRegisterFormUI: buildRegisterFormUI,
        buildOpenGamesList: buildOpenGamesList,
        buildActiveGamesList: buildActiveGamesList,
        buildBattleField: buildBattleField,
        populateGameFieldData: populateGameFieldData,
        buildUnitInfo: buildUnitInfo,
        buildScores: buildScores,
        buildMessages: buildMessages
    };
}());