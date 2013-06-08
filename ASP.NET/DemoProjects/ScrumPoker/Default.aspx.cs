using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using codexplorers.SCRUMPoker;
using codexplorers.SCRUMPoker.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox1.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;
        Participant part = new Participant();
        part.Name = TextBox1.Text;
        ScrumPokerSession.GameParticipant = part;
        Response.Redirect("~/GameList.aspx");
    }
}
