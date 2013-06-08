using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace codexplorers.SCRUMPoker.Web {
    /// <summary>
    /// Summary description for ScrumPokerSession
    /// </summary>
    public class ScrumPokerSession
    {
        private ScrumPokerSession()
        {
            
        }

		public static Game CurrentGame {
            get { return (Game)HttpContext.Current.Session["Game"]; }
            set { HttpContext.Current.Session["Game"] = value; }
        }

        public static Participant GameParticipant {
            get { return (Participant)HttpContext.Current.Session["Participant"]; }
            set { HttpContext.Current.Session["Participant"] = value; }
        }
    }
}
