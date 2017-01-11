<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AgentResetPwd.aspx.vb" Inherits="AgentMgmt.AgentResetPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Agent Reset Password</h2>
    <asp:Panel ID="Panel1" runat="server" class="panel panel-default">
    <div class="panel-body">
    <table class="style1">
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                &nbsp;</td>
            <td width="40%">
                <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                New Password&nbsp; </td>
            <td width="40%">
                <asp:TextBox ID="TextBox1" runat="server" Width="200px" TextMode="Password" class="form-control"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                &nbsp;</td>
            <td width="40%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                Confirm Password&nbsp; </td>
            <td width="40%">
                <asp:TextBox ID="TextBox2" runat="server" Width="200px" TextMode="Password" class="form-control"></asp:TextBox>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                &nbsp;</td>
            <td width="40%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                &nbsp;</td>
            <td width="40%">
                <asp:Button ID="btn_submit" runat="server" Text="Submit" class="btn btn-info"/>
            </td>
            <td width="20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td width="20%" class="text-right">
                &nbsp;</td>
            <td width="40%">
                &nbsp;</td>
            <td width="20%">
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>
