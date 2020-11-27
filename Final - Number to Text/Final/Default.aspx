<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <input type="number" id="input_number" runat="server"/>
            <input type="submit" value="Submit" class="submit" />
            <br />
            <asp:Literal ID="output" runat="server"/>
            <br />
            <asp:Button ID="Reset" runat="server" onclick="refresh" Text="Reset"/>

        </div>
    </form>
</body>
</html>
