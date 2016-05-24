<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="AMS.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lbl" runat="server"></asp:Label>
            <asp:TextBox ID="txtpass" runat="server"></asp:TextBox>
            <asp:Button ID="btnClick" runat="server" OnClick="btnClick_Click" Text="Click" />
        </div>
    </form>
</body>
</html>
