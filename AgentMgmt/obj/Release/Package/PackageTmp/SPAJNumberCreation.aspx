<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SPAJNumberCreation.aspx.vb" Inherits="AgentMgmt.SPAJNumberCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    &nbsp;
    &nbsp;
    <table class="nav-justified">
        <tr>
            <td>&nbsp;</td>
            <td><asp:LinkButton ID="LinkButton1" runat="server">Add New Rules</asp:LinkButton>
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView></td>
        </tr>
        <tr>
            <td></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    
</asp:Content>
