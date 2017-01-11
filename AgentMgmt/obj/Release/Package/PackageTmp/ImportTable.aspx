<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ImportTable.aspx.vb" Inherits="AgentMgmt.ImportTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <%--<asp:Button ID="btn_back" runat="server" Text="Back" />--%>
</p>
<p>
    Select Which Table To Be Imported To:</p>
<div class="panel panel-default">
<div class="panel-body">
<p>
    <asp:DropDownList ID="ddlTable" class="form-control" Width="300px" runat="server">
    </asp:DropDownList>
</p>
<p>
    <asp:FileUpload ID="FileUpload1" class="form-control" width="300px" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="FileUpload1"
            runat="server" Display="Dynamic" ForeColor="Red" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.csv)$$"
            ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage="Please select a valid .csv file."
            Display="Dynamic" />
</p>
<p>
    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Upload" />
&nbsp;
    <asp:Button ID="btn_back" class="btn btn-primary" runat="server" Text="Back" Width="86px" CausesValidation="False" />
</p>
</div>
</div>
</asp:Content>
