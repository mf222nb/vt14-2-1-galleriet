<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <header>
        <h1>Galleriet</h1>
    </header>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
        
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>  
        <asp:Button ID="ChoseButton" runat="server" Text="Välj fil" Onclick="ChoseButton_Click"/>
    </div>
    </form>
</body>
</html>
