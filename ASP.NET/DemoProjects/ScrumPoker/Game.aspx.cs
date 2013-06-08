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
using codexplorers.SCRUMPoker;
public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlResults.Visible = ScrumPokerSession.CurrentGame.FinishedVoting;
        pnlVote.Visible = !ScrumPokerSession.CurrentGame.FinishedVoting & 
            !ScrumPokerSession.CurrentGame.HasVoted(ScrumPokerSession.GameParticipant);
        if (ScrumPokerSession.CurrentGame.FinishedVoting)
        {
            lblResult.Text = ScrumPokerSession.CurrentGame.GameResult();
        }
        if (!IsPostBack)
        {
            pnlVote.Visible = !ScrumPokerSession.CurrentGame.HasVoted(ScrumPokerSession.GameParticipant);
            Label1.Text = ScrumPokerSession.CurrentGame.Subject;
            btnFinish.Visible = (ScrumPokerSession.CurrentGame.Facilitator.Name.Equals(ScrumPokerSession.GameParticipant.Name));
            RebindGrid();
        }
    }

    private void RebindGrid()
    {
        GridView1.DataSource = ScrumPokerSession.CurrentGame.Participants;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label nameLabel = (Label) e.Row.Cells[0].Controls[1];
            Label hasVoted = (Label)e.Row.Cells[1].Controls[1];
            Image imgCard = (Image) e.Row.Cells[2].Controls[1];
            Participant participant = ((Participant)e.Row.DataItem);
            nameLabel.Text = participant.Name;
            if (ScrumPokerSession.CurrentGame.HasVoted(participant))
            {
                hasVoted.Text = "<b><font color=\"green\">YES</font></b>";
            }
            else 
            {
                hasVoted.Text = "<font color=\"red\">NO</font>";
            }
            
            if (!ScrumPokerSession.CurrentGame.FinishedVoting)
            {
                imgCard.ImageUrl = "~/images/SQuestion.JPG";
            }
            else
            {
                if (!ScrumPokerSession.CurrentGame.HasVoted(participant))
                {
                    imgCard.ImageUrl = "~/images/SQuestion.JPG";
                }
                else
                {
                    PokerVote vote = ScrumPokerSession.CurrentGame.GetParticipantVote(participant);

                    switch (vote.VoteCard)
                    {
                        case PokerCard.c0:
                            imgCard.ImageUrl = "~/images/s0.jpg";
                            break;
                        case PokerCard.c1:
                            imgCard.ImageUrl = "~/images/s1.jpg";
                            break;
                        case PokerCard.c2:
                            imgCard.ImageUrl = "~/images/s2.jpg";
                            break;
                        case PokerCard.c3:
                            imgCard.ImageUrl = "~/images/s3.jpg";
                            break;
                        case PokerCard.c5:
                            imgCard.ImageUrl = "~/images/s5.jpg";
                            break;
                        case PokerCard.c8:
                            imgCard.ImageUrl = "~/images/s8.jpg";
                            break;
                        case PokerCard.c13:
                            imgCard.ImageUrl = "~/images/s13.jpg";
                            break;
                        case PokerCard.cCoffee:
                            imgCard.ImageUrl = "~/images/sCoffee.jpg";
                            break;
                        
                    }
                }

            }
        }
    }
    protected void Timer1_Init(object sender, EventArgs e)
    {
        RebindGrid();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c0);        
    }

    private void SendVote(PokerCard pokerCard)
    {
        PokerVote vote = new PokerVote();
        vote.VoteParticipant = ScrumPokerSession.GameParticipant;
        vote.VoteCard = pokerCard;
        ScrumPokerSession.CurrentGame.SendVote(vote);
        pnlVote.Visible = false;
    }
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        ScrumPokerSession.CurrentGame.FinishedVoting = !ScrumPokerSession.CurrentGame.FinishedVoting;
        pnlResults.Visible = ScrumPokerSession.CurrentGame.FinishedVoting;
        pnlVote.Visible = !ScrumPokerSession.CurrentGame.FinishedVoting;
        if (ScrumPokerSession.CurrentGame.FinishedVoting)
        {
            btnFinish.Text = "Restart Game";

        }
        else
        {
            btnFinish.Text = "Finish Game";
            lblResult.Text = "";
            ScrumPokerSession.CurrentGame.Votes.Clear();
        }

        RebindGrid();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c1);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c2);
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c3);
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c5);
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c8);
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.c13);
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        SendVote(PokerCard.cCoffee);
    }
}
