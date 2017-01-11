<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddUser.aspx.vb" Inherits="AgentMgmt.AddUser" EnableEventValidation = "false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 30px;
        }
        .style3
        {
            width: 120px;
        }
        .style4
        {
            height: 30px;
            width: 120px;
        }
    </style>
    <script type="text/javascript">

        function deleteConfirmation() {
            return confirm("Are you sure you want to delete this data?");
        }

        function clearConfirmation() {
            return confirm("Are you sure you want to clear the data?");
        }

        function backConfirmation() {
            return confirm("Are you sure you want to go back to previous page? Any changes will not be save.");
        }

        function saveConfirmation() {
            return confirm("Are you sure you want to save the data?");
        }

        function addConfirmation() {
            return confirm("Are you sure you want to add this new data?");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add New User</h2>
    <asp:Panel ID="Panel1" runat="server" class="panel panel-default">
    <div class="panel-body">
        <h2>User Profile</h2>
        <table class="style1">
            <tr>
                <td width="20%">
                    Username</td>
                <td width="80%">
                    <asp:TextBox ID="txt_username" class="form-control" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    Password</td>
                <td width="80%">
                    <asp:TextBox ID="txt_password" class="form-control" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="Label1" runat="server" Text="Confirm Password" Visible="False" ></asp:Label>
                </td>
                <td width="80%">
                    <asp:TextBox ID="txt_cpassword" class="form-control" runat="server"
                        Width="200px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    Email</td>
                <td width="80%">
                    <asp:TextBox ID="txt_email" runat="server" class="form-control" 
                        Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">Full Name</td>
                <td width="80%">
                    <asp:TextBox ID="txt_name" runat="server" class="form-control" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">&nbsp;</td>
                <td width="80%">&nbsp;</td>
            </tr>
            <tr>
                <td width="20%">Department</td>
                <td width="80%">
                    <asp:TextBox ID="txt_Department" runat="server" class="form-control" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">&nbsp;</td>
                <td width="80%">&nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="lbl_roles" runat="server" Text="Roles" Visible="False"></asp:Label>
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ddl_roles" runat="server" Visible="False" AutoPostBack="True">
                        <asp:ListItem>User</asp:ListItem>
                        <asp:ListItem>Administrator</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">&nbsp;</td>
                <td width="80%">&nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="lbl_Modules" runat="server" Text="Modules"></asp:Label>
                </td>
                <td width="80%">
                    <asp:CheckBoxList ID="ModulList" runat="server">
                        <asp:ListItem Value="Agent_Profile">Agent Profile</asp:ListItem>
                        <asp:ListItem Value="Table_Maintenance">Table Maintenance</asp:ListItem>
                        <asp:ListItem Value="TMCONNECT_Version">TMCONNECT Version</asp:ListItem>
                        <asp:ListItem Value="SPAJ_Admin">SPAJ Distribution</asp:ListItem>
                        <asp:ListItem Value="VA_Number_Listing">VA Number Listing</asp:ListItem>
                        <asp:ListItem Value="SPAJ_Submission_List">SPAJ Submission</asp:ListItem>
                        <asp:ListItem Value="STP_Rules_Table">STP Rules Table</asp:ListItem>
                        <asp:ListItem Value="Product_Configurator">Product Configurator</asp:ListItem>
                        <asp:ListItem Value="TMCONNECT_Client_Photos">TMCONNECT Client Photos</asp:ListItem>
                        <asp:ListItem Value="Payment_Lsting">Payment Listing</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;</td>
                <td width="80%">
                    <asp:Button ID="btn_add" class="btn btn-primary" runat="server" Text="Add" Visible="False" style="height: 36px" OnClientClick="return addConfirmation()" />
                    <asp:Button ID="btn_del" class="btn btn-primary" runat="server" Text="Delete" Visible="False" OnClientClick="return deleteConfirmation()" />
                    <asp:Button ID="btn_back" class="btn btn-primary" runat="server" Text="Back"  OnClientClick="return backConfirmation()" />
                    <asp:Button ID="btn_nback" class="btn btn-primary" runat="server" Text="Back" />
                </td>
            </tr>
        </table>
        <br />
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" class="panel panel-default">
    <div class="panel-body">
        <h2>MPOS Server User List</h2>
        <table>
        <tr>
            <td class="style3">Paging</td>
            <td><asp:DropDownList ID="ddlPaging" class="form-control" Width="150px" runat="server" AutoPostBack="True">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td class="style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
            <tr>
                <td class="style4">
                    Search Keywords</td>
                <td class="style2">
                    <asp:TextBox ID="txt_search" runat="server" class="form-control" Width="200px"></asp:TextBox>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btn_search" runat="server" class="btn btn-primary" 
                        Text="Search" />
                    &nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" class="btn btn-primary" visible="false"/>
                    &nbsp;
                    <asp:Button ID="btn_addnew" class="btn btn-primary" runat="server" Text="Add New User" 
                    Visible="False"/>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" class="table table-bordered" runat="server" PageSize="50" AllowPaging="True" OnPageIndexChanging="OnPaging"
    CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
    OnSorting="SortRecords" AllowSorting="True" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UserNo" HeaderText="No" SortExpression="UserNo"/>
                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="CreateDate" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName"/>
                <asp:BoundField DataField="Fullname" HeaderText="Fullname" SortExpression="Fullname"/>
                <asp:BoundField DataField="Modules" HeaderText="Modules"/>
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
    </asp:Panel>

</asp:Content>
