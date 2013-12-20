/// <reference path="jquery-2.0.3.js" />
/// <reference path="ui.js" />
/// <reference path="persisters.js" />
var timer = null;
var fieldTimer = null;
var action = null;
var unitId = -1;
var controlers = (function () {
    function displayErrorMsg(err) {
        var container = $('#error-container');
        container.fadeIn(0, function () {

            container.html('<p>' + err.responseJSON.Message + '</p>');

        });
        container.fadeOut(4500, function () {
            container.html('');
        });
    }
    var MainControler = Class.create({
        init: function () {
            this.persister = persisters.get("http://battlegameserver-2.apphb.com/api");
            var self = this;
        },
        loadUI: function (selector) {                      
            if (this.persister.isUserLoggedIn()) {
                this.loadGameUI(selector);
            } else {
                this.loadLoginFormUI(selector);
            }
            this.attachEventListeners(selector);
        },
        loadScores: function (selector) {
            this.persister.userPersister.scores(function (data) {
                var scoresContainer = $('#scores');             
                scoresContainer.show(0, function () {
                    
                    scoresContainer.animate({ width: '820px' }, "slow");
                    scoresContainer.animate({ height: '900px', left: '212px', opacity: '0.95' }, "slow");
                    
                    ui.buildScores(scoresContainer, data);
                });

               
            }, function (err) {
                displayErrorMsg(err);
            });
        },
        loadGameUI: function (selector) {
            var gameUIHtml =
				ui.buildGameUI(this.persister.nickname());
            $(selector).html(gameUIHtml);
            var self = this;
            self.updateUI();
             timer = setInterval(function () {
                self.updateUI();
            }, 4000);
        },
        updateField: function (gameId) {
            var self = this;
            this.persister.gamePersister.field(gameId, function (data) {
                if (!((data.inTurn == "red" && data.red.nickname == self.persister.nickname())
                           || (data.inTurn == "blue" && data.blue.nickname == self.persister.nickname()))) {
                    
                    clearInterval(fieldTimer);

                        $('#battle-holder').html(ui.buildBattleField(data));
                        var gameField = $('#field');
                        ui.populateGameFieldData(gameField, data);

                    fieldTimer = setInterval(function () {
                        self.updateField(gameId);
                    }, 3000);

                } else {
                    clearInterval(fieldTimer);                    
                    $('#battle-holder').html(ui.buildBattleField(data));
                    var gameField = $('#field');
                    ui.populateGameFieldData(gameField, data);
                }                              
            }, function (err) {
                displayErrorMsg(err);
            });
        },
        updateUI: function () {
            var self = this;
            this.persister.gamePersister.open(function (data) {
                $('#open-games').html(ui.buildOpenGamesList(data));

            }, function (err) {
                displayErrorMsg(err);
            });
            this.persister.gamePersister.active(function (data) {
                $('#active-games').html(ui.buildActiveGamesList(data));
                self.loadMessages('#messages-holder');
            }, function (err) {
                displayErrorMsg(err);
            });
        },
        loadMessages: function (selector) {
            this.persister.messagesPersister.all(function (data) {
                var html = ui.buildMessages(data);
                $(selector).html(html);
            }, function (err) {
                displayErrorMsg(err);
            });
        },
        loadLoginFormUI: function (selector) {           
            var html = ui.buildLoginFormUI();            
            $(selector).html(html);
        },
        loadRegisterFormUI:function (selector) {
           
            var html = ui.buildRegisterFormUI();
            $(selector).html(html);
        },
        attachEventListeners: function (selector) {
            var self = this;
            $(selector).on('click', "#btn-login", function () {
                var user = {
                    username: $(selector + ' #tb-login-username').val(),
                    password: $(selector + ' #tb-login-password').val()
                };
                self.persister.userPersister.login(user, function () {
                    self.loadGameUI(selector);
                }, displayErrorMsg);
            });
            $(selector).on('click', "#btn-register", function () {
                var user = {
                    username: $(selector + ' #tb-register-username').val(),
                    nickname: $(selector + ' #tb-register-nickname').val(),
                    password: $(selector + ' #tb-register-password').val()
                };
                self.persister.userPersister.register(user, function () {
                    self.loadGameUI(selector);
                }, displayErrorMsg);
            });
            $(selector).on('click', "#btn-logout", function () {
                self.persister.userPersister.logout(function () {
                    clearInterval(timer);
                    clearInterval(fieldTimer);
                    timer = 0;
                    timer = null;

                    self.loadLoginFormUI(selector);

                }, displayErrorMsg);
            });            
            $(selector).on('click', "#btn-show-register", function () {
                self.loadRegisterFormUI(selector);
            });
            $(selector).on('click', "#btn-show-login", function () {
                self.loadLoginFormUI(selector);
            });
            $(selector).on('click', ".open-game", function (ev) {
                var element = $(ev.target);
                $('#unit-info').html('');
                
                var gameId = element.data('game-id');
              
                var password = prompt("Please enter game password", '');
                if (password != null) {
                    var gameData = {
                        id: gameId
                    };
                    if (password.trim() != "") {
                        gameData.password = password;
                    }
                    self.persister.gamePersister.join(gameData, function () {
                        alert('joined game');
                    }, function (err) {
                        displayErrorMsg(err);
                    });
                }
         
            });
            $(selector).on('click', "#new-game-btn", function () {               
                var gameData = {
                    title: $("#new-game-title").val(),                    
                };
                if ($("#new-game-password").val().trim() != "") {
                    gameData.password = $("#new-game-password").val();
                }
                self.persister.gamePersister.create(gameData, function (data) {
               
                    self.updateUI();
                }, function (err) {
                    displayErrorMsg(err);
                });
            });
            $(selector).on('click', ".active-game", function (ev) {
                var element = $(ev.target);
                var gameStatus = element.data('status');
                var gameId = element.data('game-id');
                var creator = element.data('creator');
                element.removeClass('unread');
                $('#unit-info').html('');
                clearInterval(fieldTimer);
                fieldTimer = null;
                if (gameStatus == 'full') {
                    if (creator === self.persister.nickname()) {
                        var html = '<button id="start-game-btn" type="button"' + 'data-game-id="' + element.data('game-id') + '">Start Game</button>';
                        $('#battle-holder').html('<strong>This game is not started yet...</strong><br>' + html);
                    } else {
                        $('#battle-holder').html('<strong>Waiting for the game creator to start the game.</strong>');
                        self.updateField(gameId);
                    }
                } else if (gameStatus == 'open') {
                    $('#battle-holder').html('<strong>Waiting for other player.</strong>');
                }
                else if (gameStatus == 'finished') {
                    $('#battle-holder').html('<strong>This game was finished.</strong>');
                }
                else if (gameStatus == 'in-progress') {
                    self.persister.changeGame(gameId);                    
                    self.updateField(gameId);                                        
                }
                
            });
            $(selector).on('click', "#start-game-btn", function (ev) {
                var element = $(ev.target);
                var gameId = element.data('game-id');
                self.persister.gamePersister.start(gameId, function (data) {
                    timer = setInterval(function () {
                        self.updateUI();                      
                    }, 5000);
                    self.updateField(gameId);
                }, function (err) {
                    displayErrorMsg(err);
                });
            });
            $(selector).on('click', "td", function (ev) {
                var unitElement = $(ev.target);
                if (action != null && unitId != -1) {
                    var obj = {
                        unitId: unitId,
                        position: {
                            x: unitElement.data('x'), y: unitElement.data('y')
                        }
                    };
                    switch (action) {
                        case 'attack':
                            self.persister.battlePersister.attack(obj, function (data) {
                            self.updateField(self.persister.battlePersister.gameId);
                        }, function (err) {
                            displayErrorMsg(err);
                        });                            
                            break;
                        case 'move': self.persister.battlePersister.move(obj, function (data) {
                            self.updateField(self.persister.battlePersister.gameId);
                        }, function (err) {
                            displayErrorMsg(err);
                        });
                            break;
                        default: alert('fuck');
                            break;
                    }
                    action = null;
                    unitId = null;
                }
                if (unitElement.attr('hit-points')) {
                    $('.clicked').removeClass('clicked');
                    unitElement.addClass('clicked');
                    $('#unit-info').html(ui.buildUnitInfo(unitElement));
                } else {
                    $('#unit-info').html('');
                }
                
               
            });
            $(selector).on('click', "#defend-btn", function (ev) {
                var element = $(ev.target);
                var unitId = element.data('id');
                self.persister.battlePersister.defend(unitId, function (data) {
                    self.updateField(self.persister.battlePersister.gameId);
                }, function (err) {
                    displayErrorMsg(err);
                });
            });
            $(selector).on('click', "#attack-btn", function (ev) {
                var element = $(ev.target);
                unitId = element.data('id');
                action = 'attack';
            });
            $(selector).on('click', "#move-btn", function (ev) {
                var element = $(ev.target);
                unitId = element.data('id');
                action = 'move';
            });
            $(selector).on('click', "#scores-btn", function (ev) {
                self.loadScores('#scores');
            });
            $("#scores").on('click', "#close-scores", function (ev) {
                var scoresContainer = $('#scores');
                scoresContainer.animate({ width: '20px' }, "slow");
                scoresContainer.animate({ height: '20px', left: '12px;', opacity: '0.95' }, "slow");
                scoresContainer.html('');
                scoresContainer.fadeOut(2000, null);
            });
        }
    });

    return {
        get: function (selector) {
            return new MainControler(selector);
        }
    };
})();
$(document).ready(function () {
    var controler = controlers.get();
    controler.loadUI("#wrapper");
});
