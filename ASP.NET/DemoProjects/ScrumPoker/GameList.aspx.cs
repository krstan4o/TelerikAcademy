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

public partial class GamesListForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RebindView();
    }

    private void RebindView()
    {
        GridView1.DataSource = ScrumPokerApp.Games;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Game game = ScrumPokerApp.CreateGame(TextBox1.Text);
        game.AddParticipant(ScrumPokerSession.GameParticipant);
        RebindView();
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        RebindView();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = (LinkButton) e.Row.Cells[0].Controls[1];
            link.Text = ((Game)e.Row.DataItem).Subject;
            //link.PostBackUrl = "~/Game.aspx";
            // Retrieve the LinkButton control from the first column.
            Button addButton = (Button)e.Row.Cells[1].Controls[1];
            
            //((SCRUMMasterPage)Master).RegisterAsyncPostBack(addButton);
            // Set the LinkButton's CommandArgument property with the
            // row's index.
            
            addButton.CommandArgument = link.Text;
            
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
       ScrumPokerApp.RemoveGame(((Button)sender).CommandArgument.ToString()); 
    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        ScrumPokerSession.CurrentGame = ScrumPokerApp.FindGame(((LinkButton)sender).Text);
        ScrumPokerSession.CurrentGame.AddParticipant(ScrumPokerSession.GameParticipant);
        Response.Redirect("~/Game.aspx");
    }
}
