<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SPAJNumberCreationDetails.aspx.vb" Inherits="AgentMgmt.SPAJNumberCreationDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 310px;
        }
        .auto-style3 {
            height: 31px;
        }
        .auto-style5 {
            width: 310px;
            height: 32px;
        }
        .auto-style6 {
            height: 32px;
        }
        .auto-style7 {
            width: 310px;
            height: 33px;
        }
        .auto-style8 {
            height: 33px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Product Type"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_productType" runat="server" AutoPostBack="True" Width="433px">
                        <asp:ListItem>Syariah</asp:ListItem>
                        <asp:ListItem>Non-Syariah</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label2" runat="server" Text="Bank"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_bankName" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><strong>Control Number (A)</strong></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Number Of Character"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ctrlNumber_numberOfChar" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Value"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ctrlNumber_value" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="2"><strong>Running Number (B) - 1st Character | (C) - 2nd to last character</strong></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label5" runat="server" Text="Number Of Character"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="runningNumber_numberOfChar" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label6" runat="server" Text="Value"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="runningNumber_value" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5"><strong>Check Digit (D)</strong></td>
                <td class="auto-style6"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label7" runat="server" Text="Formula"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="checkDigit_formula" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2"><strong>Bank Code (E)</strong></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label8" runat="server" Text="Value"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="bankCode_value" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label9" runat="server" Text="Example"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:TextBox ID="txt_example" runat="server" Width="423px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="131px" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="131px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
