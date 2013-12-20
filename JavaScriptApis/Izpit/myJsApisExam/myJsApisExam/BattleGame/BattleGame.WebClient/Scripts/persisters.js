/// <reference path="http-requester.js" />
/// <reference path="Class.js" />
/// <reference path="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha1.js" />

var persisters = (function () {
    var nickname = localStorage.getItem("nickname");
    var sessionKey = localStorage.getItem("sessionKey");
    function saveUserData(userData) {
        localStorage.setItem("nickname", userData.nickname);
        localStorage.setItem("sessionKey", userData.sessionKey);
        nickname = userData.nickname;
        sessionKey = userData.sessionKey;
    }
    function clearUserData() {
        localStorage.removeItem("nickname");
        localStorage.removeItem("sessionKey");
        nickname = "";
        sessionKey = "";
    }

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.userPersister = new UserPersister(this.rootUrl);
            this.gamePersister = new GamePersister(this.rootUrl);
            this.battlePersister = new BattlePersister(this.rootUrl);
            this.messagesPersister = new MessagesPersister(this.rootUrl);
        },
        nickname: function () {
            return nickname;
        },
        isUserLoggedIn: function () {
            var isLoggedIn = nickname != null && sessionKey != null;
            return isLoggedIn;
        },
        gameId: function () {
            return this.battlePersister.gameId;
        },
        changeGame: function (gameId) {
            this.battlePersister.changeGame(gameId);
        }
    });
  
    var UserPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "/user";
        },          
        register: function (user, success, error) {
            var url = this.rootUrl + '/register';
            var userJson = {
                nickname: user.nickname,
                username: user.username,
                authCode: CryptoJS.SHA1(user.username + user.password).toString()
            };
            httpRequester.post(url, userJson, function (data) {
                saveUserData(data);
                success(data);
            }, error);
        },
        login: function (user, success, error) {
            var url = this.rootUrl + '/login';
            var userJson = {
                username: user.username,
                authCode: CryptoJS.SHA1(user.username + user.password).toString()                
            };
            httpRequester.post(url, userJson, function (data) {
                saveUserData(data);
                success(data);
            },error);
        },
        logout: function (success, error) {
            var url = this.rootUrl + '/logout' + '/' + sessionKey;
            httpRequester.get(url, function () {
                clearUserData();
                success();
            }, error);
        },
        scores: function (success, error) {
            var url = this.rootUrl + "/scores/" + sessionKey;
            httpRequester.get(url, success, error);
        }
    });
    var GamePersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "/game";
        },
        create: function (game, success, error) {
            var url = this.rootUrl + '/create/' + sessionKey;
            var data = {
                title: game.title
            };
            if (game.password) {
                data.password = CryptoJS.SHA1(game.password).toString();
            }
            httpRequester.post(url, data, function (data) {
                success(data);
            }, function (err) {
                error(err);
            });
        },
        join: function (game, success, error) {
            var url = this.rootUrl + "/join/" + sessionKey;
            data = {
                id:game.id
            };
            if (game.password) {
                data.password = CryptoJS.SHA1(game.password).toString();
            }
            httpRequester.post(url, data, success, error);
        },
        open: function (success, error) {
            var url = this.rootUrl + "/open/" + sessionKey;
            httpRequester.get(url, success, error);
        },
        active: function (success, error) {
            var url = this.rootUrl + "/my-active/" + sessionKey;
            httpRequester.get(url, success, error);
        },
        start: function (gameId, success, error) {
            var url = this.rootUrl + "/" + gameId + "/start/" + sessionKey;
            httpRequester.get(url, success, error);
        },
        field: function (gameId, success, error) {
            var url = this.rootUrl + "/" + gameId + "/field/" + sessionKey;
            httpRequester.get(url, success, error);
        }
    });
    var BattlePersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "/battle";
            this.gameId = -1;
        },
        changeGame: function (gameId) {
            this.gameId = gameId;
        },
        move: function (data, success, error) {
            var url = this.rootUrl + "/" + this.gameId + "/move/" + sessionKey;
            httpRequester.post(url, data, success, error);
        },
        attack: function (data, success, error) {
            var url = this.rootUrl + "/" + this.gameId + "/attack/" + sessionKey;
            httpRequester.post(url, data, success, error);
        },
        defend: function (unitId, success, error) {
            var url = this.rootUrl + "/" + this.gameId + "/defend/" + sessionKey;
            httpRequester.post(url, unitId, success, error);
        }
    });
    var MessagesPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "/messages";
        },
        all: function (success, error) {
            var url = this.rootUrl + "/all/" + sessionKey;
            httpRequester.get(url, success, error);
        },
        unread: function (success, error) {
            var url = this.rootUrl + "/unread/" + sessionKey;
            httpRequester.get(url, success, error);
        }
    });
    return {
        get: function (rootUrl) {
            return new MainPersister(rootUrl);
        }
    };
}());

