<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SCRUM Planning Poker</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   
    
        <img src="images/header.jpg" /><p>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
        Please identify yourself using your name or nickname:<br />
                &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="326px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Enter SCRUM Poker!" />
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                        ErrorMessage="Please inform your name or nickname above." SetFocusOnError="True"></asp:RequiredFieldValidator>
                </p>
            </asp:Panel>
            &nbsp;</p>
    </form>
</body>
</html>
