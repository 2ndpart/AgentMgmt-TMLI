<%@ Page Title="Agent Admin Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditAgentAdmin.aspx.vb" Inherits="AgentMgmt.EditAgentAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Agent Admin Profile</h2>
    <asp:Panel runat="server" DefaultButton="btn_save" class="panel panel-default">
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
                Admin Username</td>
            <td>
                <asp:TextBox ID="txt_username" class="form-control" runat="server" Width="200px" disabled></asp:TextBox>
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
                Admin Password</td>
            <td>
                <asp:TextBox ID="txt_password" class="form-control" runat="server" 
                    Width="200px" TextMode="Password"></asp:TextBox>
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
                Admin New Password</td>
            <td>
                <asp:TextBox ID="txt_new" class="form-control" runat="server" Width="200px" 
                    TextMode="Password"></asp:TextBox>
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
                Confirm New&nbsp; Password</td>
            <td>
                <asp:TextBox ID="txt_cnew" class="form-control" runat="server" Width="200px" 
                    TextMode="Password"></asp:TextBox>
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
                <asp:Button ID="btn_save" runat="server" class="btn btn-primary" Text="Save" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>
