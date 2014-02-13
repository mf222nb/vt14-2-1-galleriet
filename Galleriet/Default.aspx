<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
    <header class="header">
        <h1>Galleriet</h1>
    </header>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:Panel ID="SuccessPanel" runat="server" Visible="false">
                <asp:Literal ID="SuccessLiteral" runat="server"></asp:Literal>
            </asp:Panel>
        </p>
        <asp:Image ID="FullSiezeImage" runat="server" Visible="false" Width="800"/>
        <div class="thumbs">
            <asp:Repeater ID="Repeater1" runat="server" ItemType="System.String" SelectMethod="Repeater1_GetData">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Item %>' ImageUrl='<%# "~/Content/Images/Thumbnails/" + Item %>' NavigateUrl='<%# "?name=" + Item %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="En fil måste väljas" ControlToValidate="FileUpload" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna" ControlToValidate="FileUpload" Display="Dynamic" ValidationExpression="^.*\.(gif|jpg|png)$"></asp:RegularExpressionValidator>
        <div>
            <asp:FileUpload ID="FileUpload" runat="server" />  
            <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" Onclick="UploadButton_Click"/>
        </div>
    </div>
    </form>
</body>
</html>
