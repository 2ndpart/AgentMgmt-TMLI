<%@ Page Title="Version Control" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VersionControl.aspx.vb" Inherits="AgentMgmt.VersionControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/Javascript" language="Javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>
    <h2>Version</h2>
    <br />
    <div class="panel panel-default">
    <div class="panel-body">
    <table class="style1">
        <tr>
            <td align="left" width="20%">
                Current Version</td>
            <td>
                Version
                <asp:Label ID="lbl_version" runat="server"></asp:Label>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                Updated Date</td>
            <td>
                <asp:Label ID="lbl_VersionDate" runat="server"></asp:Label>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                Deploy New Version</td>
            <td>
                <asp:TextBox ID="txt_Version" class="form-control" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%" valign="top">
                Version Description</td>
            <td valign="top">
                <asp:TextBox ID="txt_versionDesc" class="form-control" runat="server" Height="300px" 
                    TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%" valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td>
                <asp:Button ID="btn_upgrade" class="btn btn-primary" runat="server" Text="Save" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
</table>
    </div>
    </div>
    <br />
    <div>
    <h2>Change log</h2>
    <asp:Repeater ID="Rept" runat="server">
    <HeaderTemplate>
    <ul>
    </HeaderTemplate>
    <ItemTemplate>
    <li><asp:LinkButton ID="LinkButton1" runat="server" >Version <%#Eval("VersionNo") %></asp:LinkButton></li>
    </ItemTemplate>
    <FooterTemplate>
    </ul>
    </FooterTemplate>
    </asp:Repeater>
    </div>
    <div>
    
    </div>
</asp:Content>
