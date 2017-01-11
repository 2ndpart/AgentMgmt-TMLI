<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ImportAgent.aspx.vb" Inherits="AgentMgmt.ImportAgent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%--<asp:Button ID="Button1" runat="server" Text="Back" />--%>
        <br />
    </p>
    <div class="panel panel-default">
    <div class="panel-body">
    <p>
        <asp:FileUpload ID="FileUpload1" class="form-control" Width="300px" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="FileUpload1"
            runat="server" Display="Dynamic" ForeColor="Red" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.csv)$$"
            ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage="Please select a valid .csv file."
            Display="Dynamic" />
    </p>
    <p>
        <asp:Button ID="btn_upload" class="btn btn-primary" runat="server" Text="Upload" />
    </p>
    </div>
    </div>
</asp:Content>
