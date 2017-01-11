<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SPAJDistributionDetails.aspx.vb" Inherits="AgentMgmt.SPAJDistributionDetails" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 165px;
        }
        .style2
        {
            width: 206px;
        }
        .style3
        {
            width: 163px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <h2>SPAJ Distribution Details</h2>
    <table class="nav-justified">
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="Agent Code"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtAgentCode" runat="server" ReadOnly="True" Width="161px"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Text="Agent Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAgentName" runat="server" ReadOnly="True" Width="242px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label3" runat="server" Text="Allocated SPAJ"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtAllocatedSpaj" runat="server" ReadOnly="True" Width="161px"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:Label ID="Label4" runat="server" style="font-weight: 700" 
        Text="Distribution History"></asp:Label>
    <br />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="SPAJAllocation" runat="server" class="table" AllowPaging="True" 
                AllowSorting="True" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
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
            </asp:GridView>
            <br />
            <asp:Label ID="Label5" runat="server" style="font-weight: 700" 
                Text="Submission History"></asp:Label>
            <br />
            <asp:GridView ID="SPAJSubmission" runat="server" AllowPaging="True" 
                AllowSorting="True" CellPadding="4" 
                class="table" ForeColor="#333333" GridLines="None">
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
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
