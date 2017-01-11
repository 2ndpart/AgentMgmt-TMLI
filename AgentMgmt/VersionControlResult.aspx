<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VersionControlResult.aspx.vb" Inherits="AgentMgmt.VersionControlResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style>
    .Over { overflow: auto; width: 95%; }
</style>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Tokio Marine TMConnect Client Version <asp:Label ID="VerNo" runat="server"></asp:Label></h2>
        <br />
        <asp:TextBox ID="myText" runat="server" BorderStyle="None" Height="500px" 
            TextMode="MultiLine" Width="500px" onkeypress="return false;" CssClass="Over"></asp:TextBox>
    </div>
    </form>
</body>
</html>
