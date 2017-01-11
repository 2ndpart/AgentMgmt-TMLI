<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SPAJSubmission.aspx.vb" Inherits="AgentMgmt.SPAJSubmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table class="style1" __designer:mapid="1">
        <tr __designer:mapid="2">
            <td width="10%" __designer:mapid="3">
                Search Criteria</td>
            <td width="20%" __designer:mapid="4">
                <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" 
                    Width="200px">
                    <asp:ListItem>AgentCode</asp:ListItem>
                    <asp:ListItem>AgentName</asp:ListItem>
                    <asp:ListItem Value="SPAJCode">SPAJ Code</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td __designer:mapid="d">
                &nbsp;</td>
            <td width="5%" __designer:mapid="e">
                Search</td>
            <td __designer:mapid="f">
                <div class="input-group" __designer:mapid="10">
                    <asp:TextBox ID="txt_search" runat="server" Width="250px" class="form-control"></asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" Text="Search" class="btn btn-info" />
                </div>
            </td>
            <td __designer:mapid="13">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="15">
            <td width="10%" __designer:mapid="16">
                &nbsp;</td>
            <td width="20%" __designer:mapid="17">
                &nbsp;</td>
            <td __designer:mapid="18">
                &nbsp;</td>
            <td width="5%" __designer:mapid="19">
                &nbsp;</td>
            <td __designer:mapid="1a">
                &nbsp;</td>
            <td __designer:mapid="1b">
                &nbsp;</td>
        </tr>
        <tr __designer:mapid="1c">
            <td width="10%" __designer:mapid="1d">
                &nbsp;</td>
            <td width="20%" __designer:mapid="1e">
                &nbsp;</td>
            <td __designer:mapid="1f">
                &nbsp;</td>
            <td width="5%" __designer:mapid="20">
                &nbsp;</td>
            <td __designer:mapid="21">
                &nbsp;</td>
            <td __designer:mapid="22">
                &nbsp;</td>
        </tr>
    </table>
    <br />

    <asp:GridView ID="GridViewSubmission" runat="server" AllowPaging="True" 
        AllowSorting="True" CellPadding="4" class="table" ForeColor="#333333" 
        GridLines="None">
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
        
</asp:Content>
