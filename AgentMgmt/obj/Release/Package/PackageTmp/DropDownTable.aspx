<%@ Page Title="Table Maintenance" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DropDownTable.aspx.vb" Inherits="AgentMgmt.DropDownTable" EnableEventValidation="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .hiddencol
        {
            display:none;
        }
        .viscol
        {
            display:block;
        }
        .style4
        {
            width: 45px;
        }
        .style5
        {
            width: 240px;
        }
        .style6
        {
            width: 118px;
        }
        .style7
        {
            width: 65px;
        }
        .style9
        {
            width: 95px;
        }
        .style10
        {
            width: 25px;
        }
        .style13
        {
            width: 125px;
        }
        .auto-style1 {
            width: 95px;
            height: 5px;
        }
        .auto-style2 {
            width: 118px;
            height: 5px;
        }
        .auto-style3 {
            width: 25px;
            height: 5px;
        }
        .auto-style4 {
            width: 125px;
            height: 5px;
        }
        .auto-style5 {
            height: 5px;
        }
    </style>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
         <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
          <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
          <script type="text/javascript">

              //function deleteConfirmation() {
              //    return confirm("Are you sure you want to delete this data?");
              //}

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
    <br />
<asp:Panel ID="Panel3" runat="server" DefaultButton="btn_Search">
    <p>
        <asp:Label ID="Label1" runat="server" Text="Table Search" Font-Size=X-Large></asp:Label>
    </p>
    <table class="style1">
        <tr>
            <td class="style9">
                Search Table :
            </td>
            <td class="style6">
                <asp:DropDownList ID="ddlNumberOfRows" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlNumberOfRows_SelectedIndexChanged" 
                    class="form-control" Width="220px">
                </asp:DropDownList>
            </td>
            <td class="style10">
                &nbsp;</td>
            <td class="style13">
                <asp:Label ID="lbl_search_key" runat="server" Text="Search Keywords : " 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <div class="input-group">
                <asp:TextBox ID="txt_sR" runat="server" Visible="False" Width="200px" class="form-control"></asp:TextBox>
                <asp:Button ID="btn_Search" runat="server" Text="Search" Visible="False" class="btn btn-info"/>
                &nbsp
                <asp:Button ID="btn_cancel" runat="server" Text="clear" Visible="False" class="btn btn-info"/>
                </div>
            </td>
            <td align="Right">
                <asp:Button ID="btn_Import" runat="server" Text="Import Table" Width="130px" class="btn btn-primary"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td class="auto-style2"></td>
            <td class="auto-style3"></td>
            <td class="auto-style4"></td>
            <td class="auto-style5"></td>
            <td align="Right" class="auto-style5"></td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
                <asp:Button ID="btn_addnew" runat="server" class="btn btn-primary" Text="Add New Data" Visible="False" Width="130px" />
            </td>
        </tr>
        <tr>
            <td class="style9">&nbsp;</td>
            <td class="style6">&nbsp;</td>
            <td class="style10">&nbsp;</td>
            <td class="style13">&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right">&nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btn_save" class="panel panel-default">
    <div class="panel-body">
        <asp:Label ID="Label3" runat="server" Text="Table Information" Font-Size=X-Large></asp:Label>
        <br />
        <br />
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" > </ajaxToolkit:ToolkitScriptManager>
        <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <%--<%#Container.DataItem("Label")%>--%>
                                <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Label") %>'></asp:Label>
                            </td>
                            <td width="80%">
                                <asp:TextBox ID="txtTextBox1" runat="server" Text='<%#Bind("TextBox")%>' Width="250px" class="form-control"></asp:TextBox>
                                
                                <asp:TextBox ID="txtDate" runat="server" Width="250px" class="form-control" Visible="false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate"/>
                                <asp:DropDownList ID="DropDownList1" runat="server" Visible="False" class="form-control" Width="100px">
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList2" runat="server" Visible="False" AutoPostBack = "true" OnSelectedIndexChanged = "DDK_SelectedIndexChanged" AppendDataBoundItems="true" class="form-control" Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br />
                </SeparatorTemplate>
            </asp:Repeater>
            <br />
            <asp:Button ID="btn_add" runat="server" Text="Add" Visible="False" class="btn btn-info" OnClientClick="return addConfirmation()" style="height: 36px" />
            <asp:Button ID="btn_clear" runat="server" Text="Clear" Visible="False" class="btn btn-warning" OnClientClick="return clearConfirmation()"/>
            <asp:Button ID="btn_edit" runat="server" Text="Edit" Visible="False" class="btn btn-info"/>
            <asp:Button ID="btn_save" runat="server" Text="Save" Visible="False" class="btn btn-info" OnClientClick="return saveConfirmation()" />
            <asp:Button ID="btn_back" runat="server" Text="Back" Visible="False" class="btn btn-warning"/>
            <asp:Button ID="btn_EdBack" runat="server" class="btn btn-warning" OnClientClick="return backConfirmation()" Text="Back" Visible="False" />
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" class="panel panel-default">
    <div class="panel-body">
        <asp:Label ID="Label2" runat="server" Text="Table List" Font-Size=X-Large></asp:Label>
        <br />
        <table class="style1">
            <tr>
                <td class="style7">
                    Paging:
                    </td>
                    <td class="style5">
                        <asp:DropDownList ID="ddlPaging" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="PageSize_Changed" class="form-control" Width="100px">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem Selected="True">50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    Filter By:
                </td>
                <td class="style5">
                    <asp:CheckBoxList ID="chkStatus" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="Status_select" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Active" Value="A" style="margin-right: 15px" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="I" style="margin-right: 15px"></asp:ListItem>
                    </asp:CheckBoxList>
                    <asp:Label ID="lbl_state" runat="server" 
                        Text="Table do not have Status Column" Visible="False"></asp:Label>
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" class="table table-bordered" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="50" 
            Width="100%" OnSorting="SortRecords" AllowSorting="True" RowStyle-HorizontalAlign="Center">
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
        <br />
        <br />
        <asp:Button ID="btn_dl" runat="server" Text="Download  .csv Template" 
            Width="200px" class="btn btn-warning"/>
        <br />
        <br />
        <asp:Button ID="btn_export" runat="server" Text="Export .csv File" 
            Width="200px" class="btn btn-warning"/>
        <br />
    </div>
    </asp:Panel>
    <br />
    </asp:Content>
