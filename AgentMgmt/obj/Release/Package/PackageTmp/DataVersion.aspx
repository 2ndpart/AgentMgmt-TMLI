<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataVersion.aspx.vb" Inherits="AgentMgmt.DataVersion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
<h2>Data Version</h2>
<asp:Panel ID="Panel1" runat="server" class="panel panel-default">
<div class="panel-body">
    <table>
        <tr>
            <td align="left" width="20%">
                Current Version</td>
            <td width="70%">
                : Version
                <asp:Label ID="lbl_dataversion" runat="server"></asp:Label>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td width="70%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                Updated Date</td>
            <td width="70%">
                :
                <asp:Label ID="lbl_dataversiondate" runat="server"></asp:Label>
                </td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td width="70%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                <asp:Label ID="lbl_Newdataversion" runat="server" Text="Deploy New Data Version"></asp:Label>
            </td>
            <td width="70%">
                <asp:TextBox ID="txt_newversion" runat="server" Width="250px" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td width="70%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" valign="top" width="20%">
                <asp:Label ID="lbl_remarks" runat="server" Text="Remarks"></asp:Label>
            </td>
            <td valign="top" width="70%">
                <asp:TextBox ID="txt_dataversionDesc" runat="server" Height="300px" 
                    TextMode="MultiLine" Width="250px" class="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" width="20%">
                &nbsp;</td>
            <td valign="top" width="70%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" width="20%">
                &nbsp;</td>
            <td width="70%">
                <asp:Button ID="btn_edit" runat="server" Text="Edit" class="btn btn-info"/>
                <asp:Button ID="btn_update" runat="server" Text="Save" class="btn btn-primary"/>
                <asp:Button ID="btn_back" runat="server" Text="Back" class="btn btn-warning"/>
            </td>
        </tr>
</table>
</div>
</asp:Panel>
</asp:Content>
