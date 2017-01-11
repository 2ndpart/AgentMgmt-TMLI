<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AdminResetPwd.aspx.vb" Inherits="AgentMgmt.AdminResetPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin Reset Password</h2>
    <asp:Panel ID="Panel1" runat="server" class="panel panel-default">
    <div class="panel-body">
    <table class="style1">
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                New Password&nbsp; </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width="200px" TextMode="Password" class="form-control"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                Confirm Password&nbsp; </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Width="200px" TextMode="Password" class="form-control"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td class="text-right">
                &nbsp;</td>
            <td>
                <asp:Button ID="btn_save" runat="server" Text="Submit" class="btn btn-primary" />
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>
