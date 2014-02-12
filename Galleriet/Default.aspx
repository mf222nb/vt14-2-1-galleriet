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
        <asp:Repeater ID="Repeater1" runat="server" ItemType="System.String" SelectMethod="Repeater1_GetData">
            <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Item %>' ImageUrl='<%# "~/Content/Images/Thumbnails/" + Item %>' NavigateUrl='<%# "?name=" + Item %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
        
        <asp:FileUpload ID="FileUpload" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="FileUpload" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="FileUpload" Display="Dynamic"></asp:RegularExpressionValidator>  
        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" Onclick="UploadButton_Click"/>
    </div>
    </form>
</body>
</html>
