<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SPAJDistribution.aspx.vb" Inherits="AgentMgmt.SPAJDistribution" EnableEventValidation = "false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style2
    {
        width: 151px
    }
    .style5
    {
        width: 153px;
    }
        .style7
        {
            width: 205px;
        }
        .style9
        {
            width: 166px;
        }
        .style10
        {
            width: 197px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>
                SPAJ Distribution</h2>
            <table class="nav-justified">
                <tr>
                    <td class="style2">
                        Start Submission Date</td>
                    <td class="style7">
                        <asp:TextBox ID="txtStartDate" runat="server" Height="18px" Width="153px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
                            BehaviorID="txtStartDate_CalendarExtender" PopupButtonID="ImageButton1" Format="yyyy/MM/dd" TargetControlID="txtStartDate" />
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/Images/calendar.png" />
                        
                    </td>
                    <td class="style9">
                        End Submission Date</td>
                    <td class="style10">
                        <asp:TextBox ID="txtEndDate" runat="server" Height="18px" Width="153px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                            BehaviorID="txtEndDate_CalendarExtender" PopupButtonID="ImageButton2" Format="yyyy/MM/dd" TargetControlID="txtEndDate" />
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                            ImageUrl="~/Images/calendar.png" />
                        
                    </td>
                    <td class="style5">
                        PACK ID / Agent Code</td>
                    <td>
                        <asp:TextBox ID="txtFilter" runat="server" Height="18px" Width="153px"></asp:TextBox>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.png" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style7">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style10">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="spajGrid" runat="server" AllowPaging="True" 
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    </asp:Content>
