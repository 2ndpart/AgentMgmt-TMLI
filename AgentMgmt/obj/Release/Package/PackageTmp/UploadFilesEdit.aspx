<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadFilesEdit.aspx.vb" Inherits="AgentMgmt.UploadFilesEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Upload Files Edit</h2>
    <asp:Panel runat="server" class="panel panel-default">
    <div class="panel-body">
    <table class="style1">
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                Folder Name</td>
            <td>
                <asp:TextBox ID="txt_folder_name" class="form-control" runat="server" 
                    ReadOnly="True" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                Sub Folder Name
             </td>
            <td>
                <asp:TextBox ID="txt_sub_folder_name" class="form-control" runat="server" Width="400px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                File Name</td>
            <td>
                <asp:TextBox ID="txt_file_name" class="form-control" runat="server"
                    Width="400px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>        
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnURL" runat="server" class="btn btn-primary" Text="View File" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </asp:Panel>
</asp:Content>