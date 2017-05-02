<%@ Page Title="Agent Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AgentProfile.aspx.vb" Inherits="AgentMgmt.AgentProfile" EnableEventValidation = "false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
        <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
        
        <style type="text/css">
            .auto-style1 {
                height: 30px;
            }
            .auto-style3 {
                width: 67px;
            }
        </style>

        <script type="text/javascript" language="javascript">
            function checkAll(objRef) {
                var GridView = objRef.parentNode.parentNode.parentNode;
                var inputList = GridView.getElementsByTagName("input");
                for (var i = 0; i < inputList.length; i++) {
                    //Get the Cell To find out ColumnIndex
                    var row = inputList[i].parentNode.parentNode;
                    if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                        if (objRef.checked) {
                            inputList[i].checked = true;
                        }
                        else {
                            inputList[i].checked = false;
                        }
                    }
                }
            }
        </script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2>Agent Profile</h2>
    <asp:Panel ID="pnl_showgridview" runat="server">
        <br />
        <table>
                <tr>
                    <td class="auto-style3">Search Criteria</td>
                    <td>
                        <asp:DropDownList ID="ddl_criteria" runat="server" class="form-control" Width="300px">
                            <asp:ListItem Value="AgentCode">Agent Number</asp:ListItem>
                            <asp:ListItem Value="FullName">Full Name</asp:ListItem>
                            <asp:ListItem Value="AgentEmail">Email</asp:ListItem>
                            <asp:ListItem Value="UPDATEDATE">Last Update</asp:ListItem>
                            <asp:ListItem Value="DeviceStatus">Device Status</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">Search : </td>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="txt_search" runat="server" class="form-control" Width="300px"></asp:TextBox>
                            <asp:Button ID="btn_search" runat="server" class="btn btn-info" Text="Search" />
                            &nbsp;
                            <asp:Button ID="btn_clear" runat="server" class="btn btn-info" Text="Clear" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">Paging </td>
                    <td>
                        <asp:DropDownList ID="ddlPaging" runat="server" AutoPostBack="True" class="form-control" Width="100px">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem Selected="True">50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
            </tr>
                <tr>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            <tr>
            <td valign="top" class="auto-style3">Filter By </td>
            <td><asp:CheckBoxList ID="chkStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Status_select" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="Active" Value="Active" style="margin-right: 15px" Selected="True" ></asp:ListItem>
                <asp:ListItem Text="Resign" Value="Resign" style="margin-right: 15px"></asp:ListItem>
                <asp:ListItem Text="Terminate" Value="Terminate" style="margin-right: 15px"></asp:ListItem>
                <asp:ListItem Text="Suspend" Value="Suspend" style="margin-right: 15px"></asp:ListItem>
            </asp:CheckBoxList></td>
            </tr>
        </table>
        <br />
        <div class="panel-body" style="overflow: auto;">
            <p>
                <asp:GridView ID="GridView1" runat="server" class="table" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="checkAll(this);"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:BoundField DataField="AgentCode" HeaderText="Agent No." SortExpression="AgentCode" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />                        
                        <asp:BoundField DataField="AgentStatus" HeaderText="Status" SortExpression="AgentStatus" />
                        <asp:BoundField DataField="AgentEmail" HeaderText="Email" SortExpression="AgentName" />
                        <asp:BoundField DataField="InvitationDate" HeaderText="Time Invitation Sended" DataFormatString="{0:MM/dd/yyyy}" SortExpression="InvitationDate" />
                        <asp:BoundField DataField="RegisterDate" HeaderText="Time Registered" DataFormatString="{0:MM/dd/yyyy}" SortExpression="RegisterDate" />
                        <asp:BoundField DataField="DevicePlatform" HeaderText="Platform" SortExpression="DevicePlatform" />
                        <asp:BoundField DataField="UDID" HeaderText="DeviceID" SortExpression="UDID" />
                        <asp:BoundField DataField="DeviceStatus" HeaderText="Device Status" SortExpression="DeviceStatus" />
                        <asp:BoundField DataField="UPDATEDATE" HeaderText="Last Update" DataFormatString="{0:MM/dd/yyyy}" SortExpression="UPDATEDATE" />
                        
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
            </p>
        </div>
        <div>
        <table align="center">
            <tr>
                <td>
                    <asp:Repeater ID="rptPager" runat="server">
                    <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed"></asp:LinkButton>
                    </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        </div>
        <div class="panel-body">
        <p>
            <asp:Button ID="btn_bulksend" runat="server" Text="Bulk Send Invitation" 
                Width="200px" class="btn btn-warning"/>
        </p>
        <p>
            <asp:Button ID="btn_export" runat="server" Text="Export .xls File" 
                Width="200px" class="btn btn-warning"/>
        </p>
    </div>
    </asp:Panel>
    <asp:Panel ID="pnl_showDetails" runat="server">
        <table class="nav-justified">
        <tr>
            <td>Agent Code</td>
            <td>
                <asp:TextBox ID="txt_Code" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address Type</td>
            <td>
                <asp:TextBox ID="txt_AddType" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td>
                <asp:TextBox ID="txt_Lname" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address</td>
            <td>
                <asp:TextBox ID="txt_add1" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>First Name</td>
            <td>
                <asp:TextBox ID="txt_Fname" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address</td>
            <td>
                <asp:TextBox ID="txt_add2" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Gender</td>
            <td>
                <asp:TextBox ID="txt_gender" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address</td>
            <td>
                <asp:TextBox ID="txt_add3" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Date of Birth</td>
            <td>
                <asp:TextBox ID="txt_dob" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address</td>
            <td>
                <asp:TextBox ID="txt_add4" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>ID</td>
            <td>
                <asp:TextBox ID="txt_id" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Address</td>
            <td>
                <asp:TextBox ID="txt_add5" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">Salution</td>
            <td class="auto-style1">
                <asp:TextBox ID="txt_salution" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td class="auto-style1">Postal Code</td>
            <td class="auto-style1">
                <asp:TextBox ID="txt_PosCode" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Nationality</td>
            <td>
                <asp:TextBox ID="txt_nationality" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Email</td>
            <td>
                <asp:DropDownList ID="ddl_email" runat="server" class="form-control" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Country Code</td>
            <td>
                <asp:TextBox ID="txt_CountryCode" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Mobile Phone</td>
            <td>
                <asp:TextBox ID="txt_Mphone" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Married Status</td>
            <td>
                <asp:TextBox ID="txt_marryStatus" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Phone Number</td>
            <td>
                <asp:TextBox ID="txt_hp1" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txt_pass" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td>Phone Number</td>
            <td>
                <asp:TextBox ID="txt_hp2" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4"><h2>Agent Status</h2>
            </td>
        </tr>
        <tr>
            <td>Tax Payer Status</td>
            <td>
                <asp:TextBox ID="txt_TaxStatus" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>License Number (C)</td>
            <td>
                <asp:TextBox ID="txt_LicenseNumC" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>NPWP</td>
            <td>
                <asp:TextBox ID="txt_NPWP" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>License Expiry (C)</td>
            <td>
                <asp:TextBox ID="txt_LicenseExpiryC" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Religion</td>
            <td>
                <asp:TextBox ID="txt_Religion" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>License Number (S)</td>
            <td>
                <asp:TextBox ID="txt_LicenseNumS" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Hire Date</td>
            <td>
                <asp:TextBox ID="txt_HireDate" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>License Expiry (S)</td>
            <td>
                <asp:TextBox ID="txt_LicenseExpiryS" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Terminate Date</td>
            <td>
                <asp:TextBox ID="txt_terminateDate" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Bank Code</td>
            <td>
                <asp:TextBox ID="txt_bankcode" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Agent Type</td>
            <td>
                <asp:TextBox ID="txt_AgentType" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Account Number</td>
            <td>
                <asp:TextBox ID="txt_AccNum" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Agent Status</td>
            <td>
                <asp:TextBox ID="txt_AgentStatus" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Account Holder</td>
            <td>
                <asp:TextBox ID="txt_AccHolder" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Channel</td>
            <td>
                <asp:TextBox ID="txt_channel" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Updated Date</td>
            <td>
                <asp:TextBox ID="txt_UpdateDate" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Direct Leader Code</td>
            <td>
                <asp:TextBox ID="txt_LeaderCode" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
            <td>Last Login </td>
            <td>
                <asp:TextBox ID="txt_LastLogin" runat="server" class="form-control" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>Device Status</td>
                <td>
                    <asp:DropDownList ID="ddlDevStatus" runat="server" class="form-control" Width="100px">
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Inactive</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_resend" runat="server" Text="Resend Invitation" class="btn btn-warning" Width="170px" />
                &nbsp;
                <asp:Button ID="btn_resetPass" runat="server" Text="Reset Password" class="btn btn-default" Width="170px" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btn_save" runat="server" Text="Save" class="btn btn-info"/>
                &nbsp;
                <asp:Button ID="btn_back" runat="server" Text="Back" class="btn btn-info"/>
            </td>
        </tr>
    </table>
    </asp:Panel>
    


</asp:Content>

