using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using codexplorers.SCRUMPoker;


namespace codexplorers.SCRUMPoker.Web {
    /// <summary>
    /// Summary description for ScrumPokerApp
    /// </summary>
    public static class ScrumPokerApp
    {

		public static List<Game> Games {
            get
            {
                if (HttpContext.Current.Application["games"]  == null)
                {
                    HttpContext.Current.Application["games"] = new List<Game>();
                }
                return (List<Game>) HttpContext.Current.Application["games"]; 
            }
        }

        public static Game CreateGame(string subject)
        {
            Game game = new Game();
            game.Subject = subject;
            Games.Add(game);
            return game;
        }
		public static Game FindGame(string subject)
        {
            foreach (Game game in Games)
            {
                if (game.Subject.Equals(subject))
                {
                    return game;
                }
            }
            return null;
        }
        public static void RemoveGame(string subject)
        {
            Game gameToRemove = null;
            foreach (Game game in Games)
            {
                if (game.Subject.Equals(subject))
                {
                    gameToRemove = game;
                }
            }
            if (gameToRemove != null)
            {
                Games.Remove(gameToRemove);
            }
        }
    }
}
