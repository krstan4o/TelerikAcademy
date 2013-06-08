<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Game:
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
    <p>
    <table border="0">
        <tr>
            <td style="width: 100px" valign="top">
                Participants:<br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="Participant">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voted?">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vote">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Init">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
    
                <asp:Button ID="btnFinish" runat="server" OnClick="btnFinish_Click" Text="Finish Game" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td  valign="top">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlVote" runat="server">
                    Cast your vote:<br />
                    <table border="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/0.jpg" OnClick="ImageButton1_Click" PostBackUrl="~/Game.aspx" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/1.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton2_Click" />&nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/2.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton3_Click" />&nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/3.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton4_Click" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 167px">
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/5.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton5_Click" />&nbsp;
                            </td>
                            <td style="height: 167px">
                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/8.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton6_Click" />&nbsp;
                            </td>
                            <td style="height: 167px">
                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/images/13.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton7_Click" />&nbsp;
                            </td>
                            <td style="height: 167px">
                                <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/images/Coffee.jpg" PostBackUrl="~/Game.aspx" OnClick="ImageButton8_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
    <asp:Panel ID="pnlResults" runat="server" Visible="False">
        <p>
        </p>
        <h2>
            &nbsp;</h2>
        <h2>
            Final Results:
        <asp:Label ID="lblResult" runat="server"></asp:Label></h2>
        <br />
        <p>
        </p>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </p>
</asp:Content>

