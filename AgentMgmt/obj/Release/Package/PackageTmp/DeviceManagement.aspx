<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DeviceManagement.aspx.vb" Inherits="AgentMgmt.DeviceManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Device Management</h2>
    <div>
        
        <table class="nav-justified">
            <tr>
                <td width="60">Search</td>
                <td>
                    <div class="input-group">
                        <asp:TextBox ID="txt_search" runat="server" Width="250px" class="form-control"></asp:TextBox>
                        <asp:Button ID="btn_search" runat="server" Text="Search" class="btn btn-info" />
                    </div>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td width="60">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td width="60">Paging</td>
                <td><asp:DropDownList ID="ddlPaging" runat="server" AutoPostBack="True" class="form-control" Width="100px">
                        <asp:ListItem value="10"  Selected="True">1</asp:ListItem>
                        <asp:ListItem value="50">2</asp:ListItem>
                        <asp:ListItem value="100">3</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td width="60">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td width="60">Filter By</td>
                <td><asp:CheckBoxList ID="chkStatus" runat="server" 
                AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Text="Downloaded" Value="downloaded" style="margin-right: 15px" ></asp:ListItem>
                        <asp:ListItem Text="Uploaded" Value="uploaded" style="margin-right: 15px"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" PageSize="10" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRow" runat="server" style="visibility:hidden"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField />
                <asp:BoundField DataField="Agent_ID" HeaderText="Agent ID" SortExpression="Agent_ID" />
                <asp:BoundField DataField="UDID" HeaderText="UDID" SortExpression="UDID" />
                <asp:BoundField DataField="Agent_Name" HeaderText="Agent Name" SortExpression="Agent_Name" />
                <asp:BoundField DataField="TMLI_Version" HeaderText="TMLI Version" SortExpression="TMLI_Version" />
                <asp:BoundField DataField="Last_UploadDate" HeaderText="Upload Date" SortExpression="Last_UploadDate" />
                <asp:BoundField DataField="Last_DownloadDate" HeaderText="Download Date" SortExpression="Last_DownloadDate" />
                <asp:BoundField DataField="Backup_URL" HeaderText="BackUp URL" SortExpression="Backup_URL" />
            </Columns>
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
    </div>
    
</asp:Content>
