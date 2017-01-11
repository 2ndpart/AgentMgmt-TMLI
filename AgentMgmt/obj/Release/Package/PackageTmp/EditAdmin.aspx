<%@ Page Title="MPOS Server Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditAdmin.aspx.vb" Inherits="AgentMgmt.EditAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>TMConnect Server Profile</h2>
    <asp:Panel runat="server" DefaultButton="Button1" class="panel panel-default">
    <div class="panel-body">
    <table class="style1">
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                Username</td>
            <td>
                <asp:TextBox ID="txt_username" class="form-control" runat="server" 
                    ReadOnly="True" Width="200px" disabled></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                Old
                Password</td>
            <td>
                <asp:TextBox ID="txt_old" class="form-control" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                New Password</td>
            <td>
                <asp:TextBox ID="txt_password" class="form-control" runat="server" TextMode="Password" 
                    Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                Confirm New Password</td>
            <td>
                <asp:TextBox ID="TextBox4" class="form-control" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Save" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>
