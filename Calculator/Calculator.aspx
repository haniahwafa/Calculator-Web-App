<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="Calculator.Calculator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox Font-Size="30px" BorderWidth="2px" Height="72px" Width="318px" BorderColor="Black" ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Height="72px" Text="7" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button2" runat="server" Height="72px" Text="8" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button3" runat="server" Height="72px" Text="9" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button4" runat="server" Height="72px" Text=":" Width="72px" OnClick="Button_Click_Num_Op" />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Height="72px" Text="4" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button6" runat="server" Height="72px" Text="5" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button7" runat="server" Height="72px" Text="6" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button8" runat="server" Height="72px" Text="x" Width="72px" OnClick="Button_Click_Num_Op" />
        <br />
        <br />
        <asp:Button ID="Button9" runat="server" Height="72px" Text="1" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button10" runat="server" Height="72px" Text="2" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button11" runat="server" Height="72px" Text="3" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button12" runat="server" Height="72px" Text="-" Width="72px" OnClick="Button_Click_Num_Op" />
        <br />
        <br />
        <asp:Button ID="Button13" runat="server" Height="72px" Text="0" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button14" runat="server" Height="72px" Text="(" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button15" runat="server" Height="72px" Text=")" Width="72px" OnClick="Button_Click_Num_Op" />
        &nbsp;
        <asp:Button ID="Button16" runat="server" Height="72px" Text="+" Width="72px" OnClick="Button_Click_Num_Op" />
        <br />
        <br />
        <asp:Button ID="Button17" runat="server" Height="72px" Text="C" Width="72px" OnClick="Button_Click_C" />
        &nbsp;
        <asp:Button ID="Button18" runat="server" Height="72px" Text="CE" Width="72px" OnClick="Button_Click_CE" />
        &nbsp;
        <asp:Button ID="Button19" runat="server" Height="72px" Text="Enter" Width="154px" OnClick="Button_Click_Enter" />
        <br />
        <br />
    </form>
</body>
</html>
