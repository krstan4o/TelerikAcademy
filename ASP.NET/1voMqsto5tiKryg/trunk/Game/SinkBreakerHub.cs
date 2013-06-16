using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CGWeb;
using CGWeb.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SinkBreaker
{
    /// <summary>
    /// This class handles the communication between the js clients and the game they are playing.
    /// </summary>
    //[AuthorizeGame(typeof(SinkBreakerGame))]
    //[HubName("sinkhub")]
    public class SinkBreakerHub : Hub
    {
        public void GetFigures()
        {
            while (true)
            {
                try
                {
                    var user = Context.User as User;
                    var gameRoom = user.GetGameroom();
                    var game = gameRoom.Game as SinkBreakerGame;

                    Clients.Caller.populateFigures(game.Figures);
                    break;
                }
                catch (Exception)
                {
                    // LOG TO DATAVASE - TODO
                }
            }
        }

        public void PlayerName()
        {
            var user = Context.User as User;
            if (user.IsAuthenticated)
            {
                Clients.Caller.setUserName(user.Name);
                Clients.All.printMsg(string.Format("Server: {0} се вписа в играта", user.Name));
            }
            else
            {
                Clients.Caller.redirectMe("http://sinkbreaker.apphb.com/");
            }
        }

        public void CheckUserAuth()
        {
            User user = Context.User as User;
            if (!user.IsAuthenticated)
            {
                Clients.Caller.redirectMe("http://sinkbreaker.apphb.com/");
            }
        }

        public void CheckStatus(bool updateGameInfo)
        {
            var user = Context.User as User;
            if (user.PlayerState == PlayerState.Queued)
            {
                Clients.Caller.log("Server: Записах те за следващата игра. Стягай гащеризона!");
                Clients.Caller.hideControlls(false);
            }
            else if (user.PlayerState == PlayerState.Playing)
            {
                if (updateGameInfo)
                {
                    var gameRoom = user.GetGameroom();
                    var game = gameRoom.Game as SinkBreakerGame;
                    Clients.Caller.log("Server: В момента играеш със: " + game.OtherPlayerName(user));
                }

                Clients.Caller.hideControlls(true);
            }
        }

        public void SendMsg(string message, bool toAll)
        {
            User user = Context.User as User;
            string parsed = message.Replace("<", "&lt;").Replace(">", "&gt;");
            if (!toAll)
            {
                try
                {
                    var gameRoom = user.GetGameroom();
                    var game = gameRoom.Game as SinkBreakerGame;
                    Clients.Caller.log(string.Format("{0}: {1}", user.Name, parsed));
                    Clients.Client(game.OtherPlayerID(user)).log(string.Format("{0}: {1}", user.Name, parsed));
                }
                catch (Exception)
                {
                    Clients.All.printMsg(string.Format("{0}: {1}", user.Name, parsed));
                }
            }
            else
            {
                Clients.All.printMsg(string.Format("{0}: {1}", user.Name, parsed));
            }
            

        }

        public void TakeFigure(string figureType, int x, int y)
        {
            var user = Context.User as User;
            var gameRoom = user.GetGameroom();
            var game = gameRoom.Game as SinkBreakerGame;
            MoveData moveResult = game.TakeFigure(user, figureType, x, y);
        }

        public void PlaceFigure(string figureType, int x, int y)
        {
            var user = Context.User as User;
            var gameRoom = user.GetGameroom();
            var game = gameRoom.Game as SinkBreakerGame;

            MoveData moveResult = game.PlaceFigure(user, figureType, x, y);
            if (moveResult.Success)
            {
                if (moveResult.GameEnded)
                {
                    Clients.Caller.endGame(true);
                    Clients.Client(game.OtherPlayerID(user)).endGame(false);
                }
                Clients.Client(game.OtherPlayerID(user)).updateEnemyOverlaps(moveResult.Overlaps);
                //Clients.Caller.log(string.Format("{0} се премести на {1} {2}", figureType, x, y));
            }
            else
            {
                // move failed - inform players and stop game, possible cheating ot harmful behavior
                //Clients.All.log(string.Format("Corrupted move!!! Stoping game..."));
                //Clients.All.log(string.Format("{0} tryed to place {1} at {2} {3}\n, Gamename: {4} - {5}", user.Name, figureType, x, y, gameRoom.Name, game.State));
                //Clients.All.log(string.Format("{0}", moveResult.Message));
            }
        }

    }
}