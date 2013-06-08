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
using codexplorers.SCRUMPoker.Web;

public partial class SCRUMMasterPage : System.Web.UI.MasterPage
{
    public void RegisterAsyncPostBack(Control control)
    {
        ScriptManager1.RegisterAsyncPostBackControl(control);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = ScrumPokerSession.GameParticipant.Name;
    }
    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        Response.Write(e.Exception.StackTrace.ToString());
    }
}
